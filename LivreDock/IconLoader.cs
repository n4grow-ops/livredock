using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Drawing = System.Drawing;

namespace LivreDock;

/// <summary>Extrai o ícone associado a um exe/caminho e devolve como ImageSource.</summary>
public static class IconLoader
{
    public static ImageSource? Load(string path)
    {
        try
        {
            // Protocolos (ms-settings:) ou caminhos sem ícone: devolve genérico.
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                return GenericIcon();

            using var icon = Drawing.Icon.ExtractAssociatedIcon(path);
            if (icon is null)
                return GenericIcon();

            var src = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            src.Freeze();
            return src;
        }
        catch
        {
            return GenericIcon();
        }
    }

    private static ImageSource? GenericIcon()
    {
        try
        {
            using var icon = Drawing.SystemIcons.Application;
            var src = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            src.Freeze();
            return src;
        }
        catch
        {
            return null;
        }
    }
}
