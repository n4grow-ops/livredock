# LivreDock

Dock minimalista para Windows 10/11 — barra de ícones flutuante no rodapé da tela, com ampliação ao passar o mouse e um clique para abrir apps. Código original em C# + WPF, **domínio público** (Unlicense): copie, modifique, venda, redistribua, sem pedir nada a ninguém.

> Projeto independente. Não é fork, cópia nem redistribuição de nenhum produto comercial — escrito do zero.

## Screenshot

_(em breve)_

## Requisitos

- Windows 10 (1809+) ou Windows 11
- Para compilar: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Para só rodar o build pronto: nada (publicado self-contained)

## Compilar e rodar

```bash
git clone https://github.com/<voce>/livredock.git
cd livredock
dotnet run --project LivreDock
```

## Gerar executável único (portátil)

```bash
dotnet publish LivreDock -c Release -r win-x64 --self-contained true ^
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o publish
```

O `publish\LivreDock.exe` roda em qualquer Windows x64 sem instalar .NET.

## Configuração

No primeiro uso, o LivreDock cria `config.json` ao lado do executável com alguns apps padrão. Edite para fixar os seus:

```json
[
  { "Name": "Explorador", "Path": "C:\\Windows\\explorer.exe" },
  { "Name": "VS Code",    "Path": "C:\\Users\\voce\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe" },
  { "Name": "Navegador",  "Path": "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", "Args": "--new-window" }
]
```

`Path` aceita exe, atalho, pasta ou protocolo (`ms-settings:`). `Args` é opcional.

## Como funciona

- Janela WPF sem borda, transparente, `Topmost`, fora da barra de tarefas.
- Fica centralizada no rodapé da área de trabalho (`SystemParameters.WorkArea`, respeita a taskbar).
- Ícones extraídos em runtime do próprio exe (`ExtractAssociatedIcon`).
- Roda como **usuário comum** (`asInvoker`) — sem serviço SYSTEM, sem driver de kernel, sem injeção de DLL. Dock simples e seguro.

## Roadmap

- [ ] Rastrear janelas abertas (`RegisterShellHookWindow` + `SetWinEventHook`) e mostrar indicador de app rodando
- [ ] Auto-hide / revelar ao encostar o mouse na borda
- [ ] Blur real de fundo (DWM / DirectComposition)
- [ ] Separadores e área de recentes
- [ ] Arrastar para reordenar e fixar
- [ ] Multi-monitor
- [ ] Tema claro/escuro automático

## Licença

[Unlicense](LICENSE) — domínio público. Sem restrições, sem atribuição obrigatória, qualquer um instala e usa.
