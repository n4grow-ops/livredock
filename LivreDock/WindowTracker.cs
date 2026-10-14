using System.IO;
using System.Text;
using System.Windows.Interop;

namespace LivreDock;

/// <summary>
/// Rastreia janelas de aplicativo abertas via shell hook (sem polling pesado) e
/// mantem o conjunto de executaveis em execucao, para o dock mostrar o indicador
/// de "app rodando". So leitura; nao injeta nada em outro processo.
/// </summary>
public sealed class WindowTracker : IDisposable
{
    private readonly Dictionary<IntPtr, string> _windows = new();
    private IntPtr _hwnd;
    private uint _shellMsg;
    private HwndSource? _source;

    /// <summary>Nomes de arquivo (lowercase) dos exes com janela visivel agora.</summary>
    public HashSet<string> RunningExeNames { get; private set; } = new();

    public event Action? Changed;

    public void Attach(HwndSource source)
    {
        _source = source;
        _hwnd = source.Handle;
        _shellMsg = NativeMethods.RegisterWindowMessage("SHELLHOOK");
        NativeMethods.RegisterShellHookWindow(_hwnd);
        source.AddHook(WndProc);
        SeedExisting();
        Recompute();
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == (int)_shellMsg)
        {
            int ev = wParam.ToInt32() & 0x7FFF;
            IntPtr target = lParam;
            switch (ev)
            {
                case NativeMethods.HSHELL_WINDOWCREATED:
                case NativeMethods.HSHELL_WINDOWACTIVATED:
                case NativeMethods.HSHELL_RUDEAPPACTIVATED:
                    if (IsAppWindow(target))
                        _windows[target] = ExeNameOf(target);
                    Recompute();
                    break;
                case NativeMethods.HSHELL_WINDOWDESTROYED:
                    if (_windows.Remove(target))
                        Recompute();
                    break;
            }
        }
        return IntPtr.Zero;
    }

    private void SeedExisting()
    {
        NativeMethods.EnumWindows((h, _) =>
        {
            if (IsAppWindow(h))
                _windows[h] = ExeNameOf(h);
            return true;
        }, IntPtr.Zero);
    }

    private void Recompute()
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var name in _windows.Values)
            if (!string.IsNullOrEmpty(name))
                set.Add(name);
        RunningExeNames = set;
        Changed?.Invoke();
    }

    private static bool IsAppWindow(IntPtr hwnd)
    {
        if (!NativeMethods.IsWindowVisible(hwnd)) return false;
        if (NativeMethods.GetWindow(hwnd, NativeMethods.GW_OWNER) != IntPtr.Zero) return false;
        if (NativeMethods.GetWindowTextLength(hwnd) == 0) return false;

        int ex = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_EXSTYLE);
        if ((ex & NativeMethods.WS_EX_TOOLWINDOW) != 0 &&
            (ex & NativeMethods.WS_EX_APPWINDOW) == 0) return false;

        // Ignora janelas "cloaked" (ex.: UWP suspensas em outro desktop virtual).
        if (NativeMethods.DwmGetWindowAttribute(hwnd, NativeMethods.DWMWA_CLOAKED, out int cloaked, sizeof(int)) == 0
            && cloaked != 0) return false;

        return true;
    }

    private static string ExeNameOf(IntPtr hwnd)
    {
        try
        {
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
            if (pid == 0) return "";
            IntPtr h = NativeMethods.OpenProcess(NativeMethods.PROCESS_QUERY_LIMITED_INFORMATION, false, pid);
            if (h == IntPtr.Zero) return "";
            try
            {
                var sb = new StringBuilder(1024);
                int size = sb.Capacity;
                if (NativeMethods.QueryFullProcessImageName(h, 0, sb, ref size))
                    return Path.GetFileName(sb.ToString());
            }
            finally { NativeMethods.CloseHandle(h); }
        }
        catch { /* processo protegido: ignora */ }
        return "";
    }

    public void Dispose()
    {
        if (_hwnd != IntPtr.Zero)
            NativeMethods.DeregisterShellHookWindow(_hwnd);
        _source?.RemoveHook(WndProc);
    }
}
