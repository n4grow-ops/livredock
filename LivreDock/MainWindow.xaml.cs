using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace LivreDock;

public partial class MainWindow : Window
{
    private const double IconSize = 48;
    private const double HoverScale = 1.6;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        BuildItems(DockConfig.LoadOrCreateDefault());
        PositionAtBottomCenter();
        // Reposiciona quando o tamanho real é conhecido.
        Dispatcher.BeginInvoke(PositionAtBottomCenter, DispatcherPriority.Loaded);
    }

    private void BuildItems(IEnumerable<DockItem> items)
    {
        Items.Children.Clear();
        foreach (var item in items)
        {
            var img = new Image
            {
                Width = IconSize,
                Height = IconSize,
                Margin = new Thickness(8, 0, 8, 0),
                Source = IconLoader.Load(item.Path),
                Stretch = Stretch.Uniform,
                ToolTip = item.Name,
                Cursor = Cursors.Hand,
                RenderTransformOrigin = new Point(0.5, 1.0),
                RenderTransform = new ScaleTransform(1, 1),
            };

            img.MouseEnter += (_, _) => Animate(img, HoverScale);
            img.MouseLeave += (_, _) => Animate(img, 1.0);
            img.MouseLeftButtonUp += (_, _) => Launch(item);

            Items.Children.Add(img);
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

    private static void Launch(DockItem item)
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

    private void PositionAtBottomCenter()
    {
        var area = SystemParameters.WorkArea;
        Left = area.Left + (area.Width - ActualWidth) / 2;
        Top = area.Bottom - ActualHeight;
    }
}
