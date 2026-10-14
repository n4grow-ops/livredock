using System.Threading;
using System.Windows;

namespace LivreDock;

public partial class App : Application
{
    // Mantido vivo durante toda a execucao para segurar a trava de instancia unica.
    private Mutex? _singleInstance;

    protected override void OnStartup(StartupEventArgs e)
    {
        // Trava por sessao de usuario (prefixo Local\) para nao bloquear
        // entre contas diferentes num Windows multiusuario.
        _singleInstance = new Mutex(initiallyOwned: true, @"Local\LivreDock.SingleInstance", out bool isNew);
        if (!isNew)
        {
            // Ja ha um dock rodando nesta sessao: sai sem empilhar outro.
            Shutdown();
            return;
        }

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _singleInstance?.ReleaseMutex();
        _singleInstance?.Dispose();
        base.OnExit(e);
    }
}
