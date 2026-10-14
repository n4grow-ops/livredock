using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Forms = System.Windows.Forms;
using Drawing = System.Drawing;
using Win32 = Microsoft.Win32;
using Path = System.IO.Path;

namespace LivreDock;

public partial class MainWindow : Window
{
    private const double IconSize = 48;
    private const double HoverScale = 1.6;
    private const double Peek = 3; // faixa visivel quando auto-oculto

    private readonly WindowTracker _tracker = new();
    private List<DockItem> _items = new();
    private readonly Dictionary<DockItem, Ellipse> _dots = new();

    private Forms.NotifyIcon? _tray;
    private FileSystemWatcher? _configWatcher;
    private DispatcherTimer? _autoHideTimer;
    private DispatcherTimer? _reloadDebounce;

    private double _shownTop;
    private bool _revealed = true;

    // Estado de arraste para reordenar.
    private Point _pressPoint;
    private DockItem? _pressItem;
    private bool _dragging;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        SizeChanged += (_, _) => { UpdateRegion(); RepositionHorizontal(); };
        Closed += OnClosed;
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        var src = (HwndSource)PresentationSource.FromVisual(this)!;
        EnableAcrylic(src.Handle);
        _tracker.Attach(src);
        _tracker.Changed += () => Dispatcher.BeginInvoke(UpdateIndicators);
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _items = DockConfig.LoadOrCreateDefault();
        BuildItems();
        BuildDockContextMenu();
        SetupTray();
        SetupConfigWatcher();

        Dispatcher.BeginInvoke(() =>
        {
            RecomputePositions();
            UpdateRegion();
            SetupAutoHide();
            UpdateIndicators();
        }, DispatcherPriority.Loaded);
    }

    // ---------------- Itens ----------------

    private void BuildItems()
    {
        Items.Children.Clear();
        _dots.Clear();

        foreach (var item in _items)
        {
            var img = new Image
            {
                Width = IconSize,
                Height = IconSize,
                Source = IconLoader.Load(item.Path),
                Stretch = Stretch.Uniform,
                Cursor = Cursors.Hand,
                RenderTransformOrigin = new Point(0.5, 1.0),
                RenderTransform = new ScaleTransform(1, 1),
            };

            var dot = new Ellipse
            {
                Width = 5,
                Height = 5,
                Margin = new Thickness(0, 4, 0, 0),
                Fill = new SolidColorBrush(Color.FromRgb(0xE8, 0xE8, 0xE8)),
                HorizontalAlignment = HorizontalAlignment.Center,
                Visibility = Visibility.Hidden,
            };
            _dots[item] = dot;

            var cell = new Grid { Margin = new Thickness(8, 0, 8, 0), ToolTip = item.Name };
            cell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            cell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10) });
            Grid.SetRow(img, 0);
            Grid.SetRow(dot, 1);
            cell.Children.Add(img);
            cell.Children.Add(dot);

            cell.Tag = item;
            cell.AllowDrop = true;

            img.MouseEnter += (_, _) => Animate(img, HoverScale);
            img.MouseLeave += (_, _) => Animate(img, 1.0);

            cell.PreviewMouseLeftButtonDown += OnItemPress;
            cell.PreviewMouseMove += OnItemMove;
            cell.MouseLeftButtonUp += OnItemClick;
            cell.DragOver += (_, ev) => ev.Effects = DragDropEffects.Move;
            cell.Drop += OnItemDrop;
            cell.ContextMenu = BuildItemContextMenu(item);

            Items.Children.Add(cell);
        }
    }

    private static void Animate(Image img, double to)
    {
        var anim = new DoubleAnimation(to, TimeSpan.FromMilliseconds(140))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
        };
        var scale = (ScaleTransform)img.RenderTransform;
        scale.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
        scale.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
    }

    private void UpdateIndicators()
    {
        var running = _tracker.RunningExeNames;
        foreach (var (item, dot) in _dots)
        {
            bool on = !string.IsNullOrEmpty(item.ExeName) && running.Contains(item.ExeName);
            dot.Visibility = on ? Visibility.Visible : Visibility.Hidden;
        }
    }

    private void Launch(DockItem item)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = item.Path,
                Arguments = item.Args ?? string.Empty,
                UseShellExecute = true,
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Falha ao abrir {item.Name}:\n{ex.Message}",
                "LivreDock", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    // ---------------- Arraste para reordenar ----------------

    private void OnItemPress(object sender, MouseButtonEventArgs e)
    {
        _pressPoint = e.GetPosition(this);
        _pressItem = (sender as FrameworkElement)?.Tag as DockItem;
        _dragging = false;
    }

    private void OnItemMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed || _pressItem is null || _dragging)
            return;

        var p = e.GetPosition(this);
        if (Math.Abs(p.X - _pressPoint.X) < SystemParameters.MinimumHorizontalDragDistance &&
            Math.Abs(p.Y - _pressPoint.Y) < SystemParameters.MinimumVerticalDragDistance)
            return;

        _dragging = true;
        DragDrop.DoDragDrop((DependencyObject)sender, _pressItem, DragDropEffects.Move);
        _dragging = false;
        _pressItem = null;
    }

    private void OnItemDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetData(typeof(DockItem)) is not DockItem dragged) return;
        if ((sender as FrameworkElement)?.Tag is not DockItem target) return;
        if (ReferenceEquals(dragged, target)) return;

        _items.Remove(dragged);
        int idx = _items.IndexOf(target);
        var p = e.GetPosition((IInputElement)sender);
        if (p.X > ((FrameworkElement)sender).ActualWidth / 2) idx++;
        _items.Insert(Math.Clamp(idx, 0, _items.Count), dragged);

        DockConfig.Save(_items);
        BuildItems();
        Dispatcher.BeginInvoke(() => { RecomputePositions(); UpdateRegion(); UpdateIndicators(); },
            DispatcherPriority.Loaded);
    }

    private void OnItemClick(object sender, MouseButtonEventArgs e)
    {
        if (_dragging) return;
        if ((sender as FrameworkElement)?.Tag is DockItem item)
            Launch(item);
    }

    // ---------------- Menus ----------------

    private ContextMenu BuildItemContextMenu(DockItem item)
    {
        var menu = new ContextMenu();
        var remove = new MenuItem { Header = $"Remover \"{item.Name}\"" };
        remove.Click += (_, _) =>
        {
            _items.Remove(item);
            DockConfig.Save(_items);
            BuildItems();
            Dispatcher.BeginInvoke(() => { RecomputePositions(); UpdateRegion(); UpdateIndicators(); },
                DispatcherPriority.Loaded);
        };
        menu.Items.Add(remove);
        return menu;
    }

    private void BuildDockContextMenu()
    {
        var menu = new ContextMenu();

        var add = new MenuItem { Header = "Adicionar app..." };
        add.Click += (_, _) => AddApp();
        menu.Items.Add(add);

        menu.Items.Add(new Separator());

        var autoHide = new MenuItem { Header = "Auto-ocultar", IsCheckable = true, IsChecked = Settings.AutoHide };
        autoHide.Click += (_, _) =>
        {
            Settings.AutoHide = autoHide.IsChecked;
            SetupAutoHide();
        };
        menu.Items.Add(autoHide);

        var startup = new MenuItem { Header = "Iniciar com o Windows", IsCheckable = true, IsChecked = Settings.StartWithWindows };
        startup.Click += (_, _) => Settings.StartWithWindows = startup.IsChecked;
        menu.Items.Add(startup);

        menu.Items.Add(new Separator());

        var quit = new MenuItem { Header = "Sair" };
        quit.Click += (_, _) => Application.Current.Shutdown();
        menu.Items.Add(quit);

        DockRoot.ContextMenu = menu;
    }

    private void AddApp()
    {
        var dlg = new Win32.OpenFileDialog
        {
            Title = "Escolha um programa",
            Filter = "Programas (*.exe)|*.exe|Todos os arquivos (*.*)|*.*",
        };
        if (dlg.ShowDialog(this) != true) return;

        _items.Add(new DockItem
        {
            Name = Path.GetFileNameWithoutExtension(dlg.FileName),
            Path = dlg.FileName,
        });
        DockConfig.Save(_items);
        BuildItems();
        Dispatcher.BeginInvoke(() => { RecomputePositions(); UpdateRegion(); UpdateIndicators(); },
            DispatcherPriority.Loaded);
    }

    // ---------------- Bandeja ----------------

    private void SetupTray()
    {
        _tray = new Forms.NotifyIcon
        {
            Icon = Drawing.SystemIcons.Application, // compartilhado: nao dar Dispose
            Text = "LivreDock",
            Visible = true,
        };
        var menu = new Forms.ContextMenuStrip();
        menu.Items.Add("Mostrar/ocultar", null, (_, _) => ToggleVisible());
        menu.Items.Add("Sair", null, (_, _) => Application.Current.Shutdown());
        _tray.ContextMenuStrip = menu;
        _tray.DoubleClick += (_, _) => ToggleVisible();
    }

    private void ToggleVisible()
    {
        Visibility = Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
    }

    // ---------------- Hot-reload do config ----------------

    private void SetupConfigWatcher()
    {
        try
        {
            var dir = Path.GetDirectoryName(DockConfig.ConfigPath);
            if (string.IsNullOrEmpty(dir)) return;
            _configWatcher = new FileSystemWatcher(dir, Path.GetFileName(DockConfig.ConfigPath))
            {
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
                EnableRaisingEvents = true,
            };
            _configWatcher.Changed += (_, _) => Dispatcher.BeginInvoke(DebouncedReload);
        }
        catch { /* sem watcher: segue */ }
    }

    private void DebouncedReload()
    {
        _reloadDebounce?.Stop();
        _reloadDebounce = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
        _reloadDebounce.Tick += (_, _) =>
        {
            _reloadDebounce!.Stop();
            var fresh = DockConfig.LoadOrCreateDefault();
            _items = fresh;
            BuildItems();
            RecomputePositions();
            UpdateRegion();
            UpdateIndicators();
        };
        _reloadDebounce.Start();
    }

    // ---------------- Posicao / auto-hide ----------------

    private void RecomputePositions()
    {
        var area = SystemParameters.WorkArea;
        _shownTop = area.Bottom - ActualHeight;
        RepositionHorizontal();
        Top = (Settings.AutoHide && !_revealed) ? area.Bottom - Peek : _shownTop;
    }

    private void RepositionHorizontal()
    {
        var area = SystemParameters.WorkArea;
        Left = area.Left + (area.Width - ActualWidth) / 2;
    }

    private void SetupAutoHide()
    {
        _autoHideTimer?.Stop();
        if (!Settings.AutoHide)
        {
            _revealed = true;
            RecomputePositions();
            return;
        }

        _revealed = false;
        RecomputePositions();
        _autoHideTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(60) };
        _autoHideTimer.Tick += AutoHideTick;
        _autoHideTimer.Start();
    }

    private void AutoHideTick(object? sender, EventArgs e)
    {
        if (!NativeMethods.GetCursorPos(out var p)) return;
        var dpi = VisualTreeHelper.GetDpi(this);
        double cx = p.X / dpi.DpiScaleX;
        double cy = p.Y / dpi.DpiScaleY;

        var area = SystemParameters.WorkArea;
        bool overDockX = cx >= Left && cx <= Left + ActualWidth;

        if (!_revealed)
        {
            if (overDockX && cy >= area.Bottom - 2)
                SlideTo(_shownTop, () => _revealed = true);
        }
        else
        {
            bool inside = overDockX && cy >= _shownTop - 4;
            if (!inside)
                SlideTo(area.Bottom - Peek, () => _revealed = false);
        }
    }

    private void SlideTo(double top, Action done)
    {
        // Evita reanimacao redundante.
        if (Math.Abs(Top - top) < 0.5) { done(); return; }
        var anim = new DoubleAnimation(top, TimeSpan.FromMilliseconds(160))
        {
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut },
        };
        anim.Completed += (_, _) => { BeginAnimation(TopProperty, null); Top = top; done(); };
        BeginAnimation(TopProperty, anim);
    }

    // ---------------- Acrylic + regiao arredondada ----------------

    private static void EnableAcrylic(IntPtr hwnd)
    {
        try
        {
            var accent = new NativeMethods.AccentPolicy
            {
                AccentState = NativeMethods.ACCENT_ENABLE_ACRYLICBLURBEHIND,
                GradientColor = unchecked((int)0x99202124), // AABBGGRR: tom escuro translucido
            };
            int size = Marshal.SizeOf(accent);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(accent, ptr, false);
                var data = new NativeMethods.WindowCompositionAttributeData
                {
                    Attribute = NativeMethods.WCA_ACCENT_POLICY,
                    Data = ptr,
                    SizeOfData = size,
                };
                NativeMethods.SetWindowCompositionAttribute(hwnd, ref data);
            }
            finally { Marshal.FreeHGlobal(ptr); }
        }
        catch { /* sem acrylic: o Border translucido ja garante o visual */ }
    }

    private void UpdateRegion()
    {
        if (ActualWidth <= 0 || ActualHeight <= 0) return;
        var src = PresentationSource.FromVisual(this) as HwndSource;
        if (src is null) return;

        var dpi = VisualTreeHelper.GetDpi(this);
        // Clipa o acrylic ao retangulo arredondado do Border (margem 12, raio 18).
        int m = (int)Math.Round(12 * dpi.DpiScaleX);
        int w = (int)Math.Round(ActualWidth * dpi.DpiScaleX);
        int h = (int)Math.Round(ActualHeight * dpi.DpiScaleY);
        int r = (int)Math.Round(36 * dpi.DpiScaleX);

        IntPtr rgn = NativeMethods.CreateRoundRectRgn(m, m, w - m, h - m, r, r);
        NativeMethods.SetWindowRgn(src.Handle, rgn, true);
    }

    // ---------------- Encerramento ----------------

    private void OnClosed(object? sender, EventArgs e)
    {
        _autoHideTimer?.Stop();
        _configWatcher?.Dispose();
        _tracker.Dispose();
        if (_tray is not null) { _tray.Visible = false; _tray.Dispose(); }
    }
}
