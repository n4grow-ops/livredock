# Backlog de segurança e qualidade

Atualizado em 2026-10-14. Fonte da verdade: `.ciber/backlog.json` (editar com `ciber.py status`).

Resumo: pendente 345

## Vol. 01: controles de segurança

| ID | Pri | Item | Status | Evidência | Data |
|---|---|---|---|---|---|
| BUG-01 | P0 | Race condition | pendente |  |  |
| BUG-02 | P0 | Double submit | pendente |  |  |
| BUG-03 | P0 | Bug de autorização | pendente |  |  |
| BUG-04 | P0 | Cache cruzado | pendente |  |  |
| BUG-05 | P0 | Sync incorreta | pendente |  |  |
| BUG-06 | P0 | Upgrade interrompido | pendente |  |  |
| BUG-07 | P0 | Migração quebrada | pendente |  |  |
| BUG-08 | P0 | Validação parcial | pendente |  |  |
| BUG-09 | P0 | Erro inseguro | pendente |  |  |
| BUG-14 | P0 | Estado nulo inesperado | pendente |  |  |
| BUG-16 | P0 | Precisão monetária | pendente |  |  |
| BUG-17 | P0 | Fusos horários | pendente |  |  |
| BUG-19 | P0 | Rollback transacional | pendente |  |  |
| BUG-20 | P0 | Lost update | pendente |  |  |
| BUG-23 | P0 | Confirmação prematura | pendente |  |  |
| BUG-26 | P0 | Falha silenciosa | pendente |  |  |
| BUG-27 | P0 | Reprocessamento de eventos | pendente |  |  |
| BUG-29 | P0 | Exclusão indevida | pendente |  |  |
| BUG-32 | P0 | Parser de arquivo | pendente |  |  |
| BUG-34 | P0 | Troca de tenant | pendente |  |  |
| CORE-01 | P0 | HTTPS e TLS | pendente |  |  |
| CORE-02 | P0 | Autenticação segura | pendente |  |  |
| CORE-03 | P0 | Autorização por recurso | pendente |  |  |
| CORE-04 | P0 | Isolamento multi-tenant | pendente |  |  |
| CORE-05 | P0 | Proteção de sessão | pendente |  |  |
| CORE-06 | P0 | MFA administrativo | pendente |  |  |
| CORE-07 | P0 | Banco não exposto | pendente |  |  |
| CORE-08 | P0 | RLS e grants no Supabase | pendente |  |  |
| CORE-09 | P0 | Gestão de segredos | pendente |  |  |
| CORE-10 | P0 | Entradas confiáveis | pendente |  |  |
| CORE-11 | P0 | Rate limiting | pendente |  |  |
| CORE-13 | P0 | Segurança de arquivos | pendente |  |  |
| CORE-14 | P0 | Criptografia | pendente |  |  |
| CORE-15 | P0 | Auditoria | pendente |  |  |
| CORE-16 | P0 | Backups restauráveis | pendente |  |  |
| CORE-18 | P0 | CI/CD seguro | pendente |  |  |
| CORE-20 | P0 | Teste de autorização | pendente |  |  |
| DESK-01 | P0 | Assinatura | pendente |  |  |
| DESK-02 | P0 | Origem oficial | pendente |  |  |
| DESK-03 | P0 | Integridade | pendente |  |  |
| DESK-04 | P0 | Atualizador | pendente |  |  |
| DESK-05 | P0 | Atualização atômica | pendente |  |  |
| DESK-07 | P0 | Secrets locais | pendente |  |  |
| DESK-08 | P0 | Arquivos privados | pendente |  |  |
| DESK-09 | P0 | Configurações | pendente |  |  |
| DESK-10 | P0 | Comandos do SO | pendente |  |  |
| DESK-11 | P0 | Argument injection | pendente |  |  |
| DESK-12 | P0 | Path traversal | pendente |  |  |
| DESK-13 | P0 | Symlink attacks | pendente |  |  |
| DESK-14 | P0 | Library hijack | pendente |  |  |
| DESK-15 | P0 | IPC | pendente |  |  |
| DESK-16 | P0 | Serviço local | pendente |  |  |
| DESK-18 | P0 | Elevação controlada | pendente |  |  |
| DESK-19 | P0 | WebView | pendente |  |  |
| DESK-20 | P0 | Protocolos externos | pendente |  |  |
| DESK-26 | P0 | Logout no desktop | pendente |  |  |
| DESK-35 | P0 | Cache multi-tenant EXE, MSI e MSIX e o mecanismo de atualização CONTROLE / RISCO | pendente |  |  |
| DEV-01 | P0 | Acesso ao Git | pendente |  |  |
| DEV-02 | P0 | MFA da equipe | pendente |  |  |
| DEV-03 | P0 | Secret scanning | pendente |  |  |
| DEV-04 | P0 | SAST | pendente |  |  |
| DEV-05 | P0 | SCA | pendente |  |  |
| DEV-08 | P0 | Branch protection | pendente |  |  |
| DEV-09 | P0 | Revisão humana | pendente |  |  |
| DEV-10 | P0 | CI/CD isolado | pendente |  |  |
| DEV-11 | P0 | OIDC para deploy | pendente |  |  |
| DEV-14 | P0 | Infra as Code | pendente |  |  |
| DEV-16 | P0 | Testes de autorização | pendente |  |  |
| DEV-20 | P0 | Aprovação de migrações | pendente |  |  |
| LAB-01 | P0 | Android sem internet | pendente |  |  |
| LAB-07 | P0 | Android deep link hostil | pendente |  |  |
| LAB-09 | P0 | iOS suspenso durante envio | pendente |  |  |
| LAB-13 | P0 | iOS Universal Link inválido | pendente |  |  |
| LAB-15 | P0 | Windows upgrade interrompido | pendente |  |  |
| LAB-19 | P0 | macOS assinatura inválida | pendente |  |  |
| LAB-23 | P0 | Logout A/login B | pendente |  |  |
| LAB-24 | P0 | Pagamento API indisponível | pendente |  |  |
| LAB-25 | P0 | Banco fora do ar | pendente |  |  |
| LIN-01 | P0 | Permissões POSIX | pendente |  |  |
| LIN-02 | P0 | Execução sem root | pendente |  |  |
| LIN-07 | P0 | Unix sockets | pendente |  |  |
| MAC-01 | P0 | Developer ID | pendente |  |  |
| MAC-02 | P0 | Notarization | pendente |  |  |
| MAC-03 | P0 | Hardened Runtime | pendente |  |  |
| MAC-05 | P0 | macOS Keychain | pendente |  |  |
| MON-01 | P0 | Eventos de login | pendente |  |  |
| MON-02 | P0 | Acessos negados | pendente |  |  |
| MON-03 | P0 | Auditoria administrativa | pendente |  |  |
| MON-04 | P0 | Integridade do log | pendente |  |  |
| MON-05 | P0 | Minimização no log | pendente |  |  |
| MON-13 | P0 | Plano de incidentes | pendente |  |  |
| MON-14 | P0 | Revogação emergencial | pendente |  |  |
| POL-01 | P0 | Exploração crítica | pendente |  |  |
| POL-02 | P0 | Falha entre tenants | pendente |  |  |
| POL-03 | P0 | Dupla cobrança | pendente |  |  |
| POL-04 | P0 | Perda irreversível de dados | pendente |  |  |
| POL-07 | P0 | Atualização sem assinatura | pendente |  |  |
| POL-08 | P0 | Backup sem teste | pendente |  |  |
| POL-09 | P0 | Testes automáticos falham | pendente |  |  |
| POL-10 | P0 | Incidente em produção | pendente |  |  |
| PRIV-01 | P0 | Mapa de dados | pendente |  |  |
| PRIV-02 | P0 | Base legal | pendente |  |  |
| PRIV-03 | P0 | Minimização | pendente |  |  |
| PRIV-04 | P0 | Finalidade | pendente |  |  |
| PRIV-05 | P0 | Acesso a dados | pendente |  |  |
| PRIV-08 | P0 | Retenção | pendente |  |  |
| PRIV-11 | P0 | Comunicação de incidentes | pendente |  |  |
| QA-01 | P0 | Unitários | pendente |  |  |
| QA-02 | P0 | Integração | pendente |  |  |
| QA-03 | P0 | End-to-end | pendente |  |  |
| QA-05 | P0 | Regressão | pendente |  |  |
| QA-06 | P0 | Autorização | pendente |  |  |
| QA-07 | P0 | SAST | pendente |  |  |
| QA-16 | P0 | Upgrade e rollback | pendente |  |  |
| QA-17 | P0 | Restauração | pendente |  |  |
| REL-01 | P0 | Zero P0 explorável | pendente |  |  |
| REL-02 | P0 | Autorização aprovada | pendente |  |  |
| REL-03 | P0 | Suite automatizada | pendente |  |  |
| REL-05 | P0 | Restauração comprovada | pendente |  |  |
| REL-06 | P0 | Contrato cliente/API | pendente |  |  |
| REL-07 | P0 | Android release | pendente |  |  |
| REL-09 | P0 | iOS release | pendente |  |  |
| REL-11 | P0 | Desktop assinado | pendente |  |  |
| REL-12 | P0 | Atualizador testado | pendente |  |  |
| REL-13 | P0 | Privilégios desktop | pendente |  |  |
| TEC-01 | P0 | React Native | pendente |  |  |
| TEC-02 | P0 | Expo | pendente |  |  |
| TEC-03 | P0 | Flutter | pendente |  |  |
| TEC-04 | P0 | Kotlin Android | pendente |  |  |
| TEC-05 | P0 | Swift/SwiftUI | pendente |  |  |
| TEC-06 | P0 | Electron | pendente |  |  |
| TEC-07 | P0 | Tauri 2 | pendente |  |  |
| TEC-08 | P0 | .NET/WPF/WinUI | pendente |  |  |
| TEC-09 | P0 | Qt/C++ | pendente |  |  |
| TEC-11 | P0 | Capacitor/Ionic | pendente |  |  |
| TST-01 | P0 | Rota anônima | pendente |  |  |
| TST-02 | P0 | Token expirado | pendente |  |  |
| TST-03 | P0 | JWT adulterado | pendente |  |  |
| TST-04 | P0 | Token de outra audiência | pendente |  |  |
| TST-05 | P0 | Sessão revogada | pendente |  |  |
| TST-06 | P0 | Leitura cruzada | pendente |  |  |
| TST-07 | P0 | Escrita cruzada | pendente |  |  |
| TST-08 | P0 | Exclusão cruzada | pendente |  |  |
| TST-09 | P0 | Operação admin | pendente |  |  |
| TST-10 | P0 | Alteração de role | pendente |  |  |
| TST-11 | P0 | Tenant ID falsificado | pendente |  |  |
| TST-12 | P0 | Arquivo privado | pendente |  |  |
| TST-13 | P0 | Tipo de arquivo | pendente |  |  |
| TST-15 | P0 | Entrada SQL hostil | pendente |  |  |
| TST-16 | P0 | HTML não confiável | pendente |  |  |
| TST-17 | P0 | Pagamento duplicado | pendente |  |  |
| TST-18 | P0 | Webhook inválido | pendente |  |  |
| TST-19 | P0 | Repetição de webhook | pendente |  |  |
| TST-20 | P0 | Login repetitivo | pendente |  |  |
| TST-21 | P0 | SSRF | pendente |  |  |
| TST-22 | P0 | Supabase anon | pendente |  |  |
| TST-23 | P0 | Supabase tenant | pendente |  |  |
| TST-24 | P0 | Realtime privado | pendente |  |  |
| TST-25 | P0 | Relatório adulterado | pendente |  |  |
| TST-27 | P0 | Cache pós-logout | pendente |  |  |
| TST-28 | P0 | Reset reutilizado | pendente |  |  |
| TST-29 | P0 | Conta desativada | pendente |  |  |
| TST-31 | P0 | Perda de dados | pendente |  |  |
| TST-32 | P0 | Chave vazada | pendente |  |  |
| TST-33 | P0 | Prompt malicioso | pendente |  |  |
| TST-34 | P0 | Abuso de IA | pendente |  |  |
| TST-35 | P0 | Role alterada | pendente |  |  |
| TST-36 | P0 | Restore isolado | pendente |  |  |
| UX-01 | P0 | Exclusão segura | pendente |  |  |
| UX-02 | P0 | Revisão financeira | pendente |  |  |
| UX-03 | P0 | Sucesso verdadeiro | pendente |  |  |
| UX-04 | P0 | Duplo clique | pendente |  |  |
| UX-08 | P0 | Formulário resiliente | pendente |  |  |
| UX-15 | P0 | Identidade ativa | pendente |  |  |
| UX-19 | P0 | Dados confidenciais | pendente |  |  |
| UX-23 | P0 | Permissão refletida | pendente |  |  |
| WIN-01 | P0 | Authenticode | pendente |  |  |
| WIN-02 | P0 | MSIX | pendente |  |  |
| WIN-04 | P0 | DPAPI | pendente |  |  |
| WIN-05 | P0 | NTFS ACL | pendente |  |  |
| WIN-06 | P0 | DLL Search Order | pendente |  |  |
| WIN-07 | P0 | UAC | pendente |  |  |
| WIN-10 | P0 | Named Pipes | pendente |  |  |
| WIN-11 | P0 | localhost | pendente |  |  |
| WIN-20 | P0 | Servidor de atualização | pendente |  |  |
| BUG-10 | P1 | Memory leak | pendente |  |  |
| BUG-11 | P1 | Deadlock | pendente |  |  |
| BUG-12 | P1 | UI Freeze/ANR | pendente |  |  |
| BUG-13 | P1 | Crash loop | pendente |  |  |
| BUG-15 | P1 | Overflow/Underflow | pendente |  |  |
| BUG-18 | P1 | Relógio adulterado | pendente |  |  |
| BUG-21 | P1 | Retry sem limite | pendente |  |  |
| BUG-22 | P1 | Timeout ausente | pendente |  |  |
| BUG-24 | P1 | Versão incompatível | pendente |  |  |
| BUG-25 | P1 | Dependência quebrada | pendente |  |  |
| BUG-28 | P1 | UI desatualizada | pendente |  |  |
| BUG-30 | P1 | Corrupção de arquivos | pendente |  |  |
| BUG-31 | P1 | Unicode e encoding | pendente |  |  |
| BUG-33 | P1 | Paginação incorreta | pendente |  |  |
| BUG-35 | P1 | Feature flag | pendente |  |  |
| BUG-36 | P1 | Falha de recuperação | pendente |  |  |
| CORE-12 | P1 | WAF e anti-DDoS | pendente |  |  |
| CORE-17 | P1 | Dependências | pendente |  |  |
| CORE-19 | P1 | Monitoramento | pendente |  |  |
| DESK-06 | P1 | Rollback | pendente |  |  |
| DESK-17 | P1 | Privilégios | pendente |  |  |
| DESK-21 | P1 | Memória nativa | pendente |  |  |
| DESK-22 | P1 | Logs locais | pendente |  |  |
| DESK-23 | P1 | Clipboard | pendente |  |  |
| DESK-24 | P1 | Início automático | pendente |  |  |
| DESK-25 | P1 | Background/tray | pendente |  |  |
| DESK-27 | P1 | Múltiplas instâncias | pendente |  |  |
| DESK-28 | P1 | Instalador seguro | pendente |  |  |
| DESK-29 | P1 | Desinstalação | pendente |  |  |
| DESK-30 | P1 | Crash dumps | pendente |  |  |
| DESK-31 | P1 | Atualização do SO | pendente |  |  |
| DESK-32 | P1 | Plugins | pendente |  |  |
| DESK-33 | P1 | Antivírus/EDR | pendente |  |  |
| DESK-34 | P1 | Instalação offline | pendente |  |  |
| DEV-06 | P1 | DAST | pendente |  |  |
| DEV-07 | P1 | SBOM | pendente |  |  |
| DEV-12 | P1 | Containers analisados | pendente |  |  |
| DEV-13 | P1 | Artefatos assinados | pendente |  |  |
| DEV-15 | P1 | Policy as Code | pendente |  |  |
| DEV-17 | P1 | Threat modeling | pendente |  |  |
| DEV-18 | P1 | Patching de dependências | pendente |  |  |
| DEV-19 | P1 | Ambiente temporário | pendente |  |  |
| DEV-21 | P1 | Código gerado por IA | pendente |  |  |
| DEV-22 | P1 | Rollback de release | pendente |  |  |
| LAB-02 | P1 | Android sem câmera | pendente |  |  |
| LAB-03 | P1 | Android processo encerrado | pendente |  |  |
| LAB-04 | P1 | Android troca de rede | pendente |  |  |
| LAB-05 | P1 | Android baixo desempenho | pendente |  |  |
| LAB-06 | P1 | Android gira tela | pendente |  |  |
| LAB-08 | P1 | iOS sem Face ID | pendente |  |  |
| LAB-10 | P1 | iOS sob pressão de memória | pendente |  |  |
| LAB-11 | P1 | iOS reinstalado | pendente |  |  |
| LAB-12 | P1 | iOS bloqueado | pendente |  |  |
| LAB-14 | P1 | Windows sem admin | pendente |  |  |
| LAB-16 | P1 | Windows antivírus restringe | pendente |  |  |
| LAB-17 | P1 | Windows app duplo | pendente |  |  |
| LAB-18 | P1 | Windows arquivo config inválido | pendente |  |  |
| LAB-20 | P1 | macOS permissão revogada | pendente |  |  |
| LAB-21 | P1 | Linux sem permissão | pendente |  |  |
| LAB-22 | P1 | Linux biblioteca ausente | pendente |  |  |
| LAB-26 | P1 | Contrato API alterado | pendente |  |  |
| LIN-03 | P1 | AppArmor/SELinux | pendente |  |  |
| LIN-04 | P1 | Flatpak | pendente |  |  |
| LIN-05 | P1 | Pacotes Linux | pendente |  |  |
| LIN-06 | P1 | systemd hardening | pendente |  |  |
| LIN-08 | P1 | Bibliotecas dinâmicas | pendente |  |  |
| LIN-09 | P1 | Variáveis de ambiente | pendente |  |  |
| LIN-10 | P1 | Distribuições | pendente |  |  |
| MAC-04 | P1 | App Sandbox | pendente |  |  |
| MAC-06 | P1 | Entitlements | pendente |  |  |
| MAC-07 | P1 | Gatekeeper | pendente |  |  |
| MAC-08 | P1 | TCC | pendente |  |  |
| MAC-09 | P1 | Arquiteturas | pendente |  |  |
| MAC-10 | P1 | LaunchAgents | pendente |  |  |
| MON-06 | P1 | Logs estruturados | pendente |  |  |
| MON-07 | P1 | Alerta de abuso de login | pendente |  |  |
| MON-08 | P1 | Mudanças sensíveis | pendente |  |  |
| MON-09 | P1 | Exfiltração | pendente |  |  |
| MON-10 | P1 | Métricas API | pendente |  |  |
| MON-11 | P1 | Integrações | pendente |  |  |
| MON-12 | P1 | Uso financeiro | pendente |  |  |
| MON-15 | P1 | Evidências | pendente |  |  |
| MON-16 | P1 | Exercícios | pendente |  |  |
| MON-17 | P1 | Responsáveis | pendente |  |  |
| MON-18 | P1 | Retenção de logs | pendente |  |  |
| MON-19 | P1 | Cobertura | pendente |  |  |
| MON-20 | P1 | Pós-morte técnico | pendente |  |  |
| POL-05 | P1 | Crash no fluxo principal | pendente |  |  |
| POL-11 | P1 | Indicador de crash | pendente |  |  |
| POL-12 | P1 | Indicador de ANR/hangs | pendente |  |  |
| POL-13 | P1 | Vulnerabilidades abertas | pendente |  |  |
| POL-14 | P1 | Transações falhas | pendente |  |  |
| POL-15 | P1 | Sincronização | pendente |  |  |
| POL-16 | P1 | Upgrade | pendente |  |  |
| POL-17 | P1 | Detecção e resolução | pendente |  |  |
| POL-18 | P1 | RPO/RTO | pendente |  |  |
| POL-19 | P1 | Regressão | pendente |  |  |
| POL-20 | P1 | Cobertura funcional | pendente |  |  |
| PRIV-06 | P1 | Transparência | pendente |  |  |
| PRIV-07 | P1 | Direitos dos titulares | pendente |  |  |
| PRIV-09 | P1 | Terceiros | pendente |  |  |
| PRIV-10 | P1 | Transferência internacional | pendente |  |  |
| PRIV-12 | P1 | Registros de decisão | pendente |  |  |
| PRIV-13 | P1 | RIPD | pendente |  |  |
| PRIV-14 | P1 | Privacy by design | pendente |  |  |
| PRIV-15 | P1 | Analytics e logs | pendente |  |  |
| QA-04 | P1 | Contrato API | pendente |  |  |
| QA-08 | P1 | DAST | pendente |  |  |
| QA-09 | P1 | Mobile security testing | pendente |  |  |
| QA-10 | P1 | Fuzzing | pendente |  |  |
| QA-11 | P1 | Property-based testing | pendente |  |  |
| QA-12 | P1 | Performance | pendente |  |  |
| QA-13 | P1 | Carga e stress | pendente |  |  |
| QA-14 | P1 | Soak testing | pendente |  |  |
| QA-15 | P1 | Failure testing | pendente |  |  |
| QA-18 | P1 | Acessibilidade | pendente |  |  |
| QA-19 | P1 | Regressão visual | pendente |  |  |
| QA-20 | P1 | Pentest independente | pendente |  |  |
| REL-04 | P1 | Crashes triados | pendente |  |  |
| REL-08 | P1 | Android vitals | pendente |  |  |
| REL-10 | P1 | iOS beta | pendente |  |  |
| TEC-10 | P1 | Rust Desktop | pendente |  |  |
| TEC-12 | P1 | PWA | pendente |  |  |
| TST-14 | P1 | Payload grande | pendente |  |  |
| TST-26 | P1 | Exportação anormal | pendente |  |  |
| TST-30 | P1 | Banco indisponível | pendente |  |  |
| UX-05 | P1 | Estado de loading | pendente |  |  |
| UX-06 | P1 | Erros úteis | pendente |  |  |
| UX-07 | P1 | Recuperação amigável | pendente |  |  |
| UX-09 | P1 | Acessibilidade | pendente |  |  |
| UX-10 | P1 | Contraste | pendente |  |  |
| UX-11 | P1 | Dark/light mode | pendente |  |  |
| UX-12 | P1 | Fonte ampliada | pendente |  |  |
| UX-13 | P1 | Responsividade | pendente |  |  |
| UX-14 | P1 | Navegação pós-logout | pendente |  |  |
| UX-16 | P1 | Permissões de dispositivo | pendente |  |  |
| UX-17 | P1 | Biometria falhou | pendente |  |  |
| UX-18 | P1 | Sessão expirada | pendente |  |  |
| UX-20 | P1 | Estado da conexão | pendente |  |  |
| UX-21 | P1 | Dados antigos | pendente |  |  |
| UX-22 | P1 | Operações pendentes | pendente |  |  |
| UX-24 | P1 | Desfazer | pendente |  |  |
| UX-25 | P1 | Releases | pendente |  |  |
| UX-26 | P1 | Notificações privadas | pendente |  |  |
| UX-27 | P1 | Recuperação de acesso | pendente |  |  |
| UX-28 | P1 | Localização | pendente |  |  |
| UX-29 | P1 | Tela vazia | pendente |  |  |
| UX-30 | P1 | Consistência de transação REGRA DE NEGÓCIO Nenhuma operação foi Servidor ainda não confirmou; Estado validado no backend; Consultar estado real antes Exibir motivo seguro e orientação e conflito gerar dados duplicados, corrupção CONTROLE / RISCO | pendente |  |  |
| WIN-03 | P1 | SmartScreen | pendente |  |  |
| WIN-08 | P1 | Registro | pendente |  |  |
| WIN-09 | P1 | Windows Services | pendente |  |  |
| WIN-12 | P1 | Microsoft Defender | pendente |  |  |
| WIN-13 | P1 | Mitigações nativas | pendente |  |  |
| WIN-14 | P1 | Event Log | pendente |  |  |
| WIN-15 | P1 | Hibernação | pendente |  |  |
| WIN-16 | P1 | DPI | pendente |  |  |
| WIN-17 | P1 | Windows Update | pendente |  |  |
| WIN-18 | P1 | Instalador temporário | pendente |  |  |
| WIN-19 | P1 | Usuários locais | pendente |  |  |
| POL-06 | P2 | Bug puramente visual | pendente |  |  |
