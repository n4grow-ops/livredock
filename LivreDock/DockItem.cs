using System.IO;
using System.Text.Json;

namespace LivreDock;

/// <summary>Um item fixado no dock.</summary>
public sealed class DockItem
{
    public string Name { get; set; } = "";
    public string Path { get; set; } = "";
    public string? Args { get; set; }

    /// <summary>Nome do arquivo do alvo (para casar com janelas em execucao). Vazio p/ protocolos.</summary>
    public string ExeName
    {
        get
        {
            try { return System.IO.Path.GetFileName(Path); }
            catch { return ""; }
        }
    }
}

/// <summary>Carrega/salva a lista de itens de um config.json ao lado do exe.</summary>
public static class DockConfig
{
    public static string ConfigPath { get; } =
        System.IO.Path.Combine(AppContext.BaseDirectory, "config.json");

    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };

    public static List<DockItem> LoadOrCreateDefault()
    {
        if (File.Exists(ConfigPath))
        {
            try
            {
                var json = File.ReadAllText(ConfigPath);
                var items = JsonSerializer.Deserialize<List<DockItem>>(json);
                if (items is { Count: > 0 })
                    return items;
            }
            catch
            {
                // config corrompido: cai no default.
            }
        }

        var defaults = DefaultItems();
        Save(defaults);
        return defaults;
    }

    public static void Save(IEnumerable<DockItem> items)
    {
        try
        {
            File.WriteAllText(ConfigPath, JsonSerializer.Serialize(items, JsonOpts));
        }
        catch
        {
            // somente leitura: segue sem salvar.
        }
    }

    private static List<DockItem> DefaultItems()
    {
        var win = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        var sys = Environment.GetFolderPath(Environment.SpecialFolder.System);
        return new List<DockItem>
        {
            new() { Name = "Explorador", Path = System.IO.Path.Combine(win, "explorer.exe") },
            new() { Name = "Bloco de Notas", Path = System.IO.Path.Combine(sys, "notepad.exe") },
            new() { Name = "Calculadora", Path = System.IO.Path.Combine(sys, "calc.exe") },
            new() { Name = "Terminal", Path = System.IO.Path.Combine(sys, "cmd.exe") },
            new() { Name = "Configurações", Path = "ms-settings:" },
        };
    }
}
