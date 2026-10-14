using Microsoft.Win32;

namespace LivreDock;

/// <summary>Preferencias simples do usuario em HKCU\Software\LivreDock (sem segredos).</summary>
public static class Settings
{
    private const string Key = @"Software\LivreDock";
    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
    private const string RunName = "LivreDock";

    public static bool AutoHide
    {
        get => ReadBool(nameof(AutoHide));
        set => WriteBool(nameof(AutoHide), value);
    }

    public static bool StartWithWindows
    {
        get
        {
            using var k = Registry.CurrentUser.OpenSubKey(RunKey);
            return k?.GetValue(RunName) is string;
        }
        set
        {
            using var k = Registry.CurrentUser.CreateSubKey(RunKey);
            if (k is null) return;
            if (value)
            {
                var exe = Environment.ProcessPath;
                if (!string.IsNullOrEmpty(exe))
                    k.SetValue(RunName, $"\"{exe}\"");
            }
            else
            {
                k.DeleteValue(RunName, throwOnMissingValue: false);
            }
        }
    }

    private static bool ReadBool(string name)
    {
        using var k = Registry.CurrentUser.OpenSubKey(Key);
        return k?.GetValue(name) is int i && i != 0;
    }

    private static void WriteBool(string name, bool value)
    {
        using var k = Registry.CurrentUser.CreateSubKey(Key);
        k?.SetValue(name, value ? 1 : 0, RegistryValueKind.DWord);
    }
}
