# 🪜 Calculadora de Escadas

**Unity 3D Utility Tool** | C# | Professional Stair Dimensioning Calculator

![Unity Version](https://img.shields.io/badge/Unity-2022.3+-blue)
![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20Android-lightgrey)
![License](https://img.shields.io/badge/License-MIT-green)
![Status](https://img.shields.io/badge/Status-MVP%20Complete-brightgreen)

## 📐 Sobre o Projeto

**Calculadora de Escadas** é uma ferramenta utilitária desenvolvida em Unity para cálculo rápido e preciso de dimensionamento de escadas. O projeto nasceu de uma necessidade real durante uma reforma residencial e evoluiu para um aplicativo completo com sistema de áudio imersivo e interface intuitiva.

### ✨ Funcionalidades Implementadas (MVP)

| Categoria | Funcionalidades |
|-----------|-----------------|
| **Cálculos** | Número de degraus, comprimento total, ângulo de inclinação, comprimento da hipotenusa |
| **Sistema de Áudio** | Mixer com volumes independentes (Master/Music/SFX), mute global, persistência via PlayerPrefs |
| **UI/UX** | Interface responsiva, feedback sonoro, validação de inputs, navegação entre cenas |
| **Configurações** | Menu de áudio com sliders (0-100%), reset para valores padrão |

### 🎮 Controles

| Tecla | Ação | Contexto |
|-------|------|----------|
| `ESC` | Fechar menu de áudio / Voltar | Todos os menus |
| `F1` | Sistema de debug | Cena EscadaApp |
| `F11` / `Alt+Enter` | Alternar tela cheia | Todas as cenas |
| **Mouse** | | |
| Cursor | Apontar para selecionar | Menus |
| Botão Esquerdo | Confirmar seleção | Todos os botões |

### 📊 Fórmulas de Cálculo

```csharp
// Algoritmos implementados conforme GDD
Número de Degraus   = Mathf.CeilToInt(alturaEscada / alturaDegrau);
Comprimento Total   = (numDegraus - 1) * larguraDegrau;
Ângulo de Inclinação = Mathf.Atan(alturaEscada / comprimentoTotal) * Mathf.Rad2Deg;
Hipotenusa          = Mathf.Sqrt(Mathf.Pow(alturaEscada, 2) + Mathf.Pow(comprimentoTotal, 2));


🏗️ Arquitetura Técnica

Assets/
├── Scripts/
│   ├── CalculadoraEscada.cs   # Lógica principal e cálculos
│   ├── AudioManager.cs        # Singleton pattern - sistema de áudio
│   ├── AudioPlayer.cs         # Componente auxiliar para sons
│   ├── AudioSettingsMenu.cs   # Menu de configurações de áudio
│   ├── ThanksManager.cs       # Cena de agradecimentos
│   ├── GameManager.cs         # Gerenciamento de navegação
│   └── ExitGame.cs            # Saída da aplicação
├── Scenes/
│   ├── EscadaApp.unity        # Cena principal (calculadora)
│   └── Thanks.unity           # Cena de créditos
└── AudioMixer/
    └── MainMixer.mixer        # Mixer com grupos Master/Music/SFX

🎵 Sistema de Áudio (PlayerPrefs)
🎵 Sistema de Áudio (PlayerPrefs)
Chave	Tipo	Valor Padrão
MasterVolume	float	0.8f (80%)
MusicVolume	float	0.7f (70%)
SFXVolume	float	0.9f (90%)
MasterMuted	int (0/1)	0
🚀 Como Executar
Clone o repositório:

bash
git clone https://github.com/GamerExtremo/calculadora-escadas.git
Abra o projeto no Unity (2022.3 ou superior)

Adicione as cenas ao Build Settings:

EscadaApp (Index 0)

Thanks (Index 1)

Pressione Play para testar

📱 Build para Mobile (Android)
bash
# Configurações recomendadas no Player Settings
- Package Name: com.gamerextremo.calculadoraescadas
- Minimum API Level: Android 7.0 (API 24)
- Target API Level: Android 13 (API 33)
- Scripting Backend: IL2CPP
- Target Architecture: ARM64
🗺️ Roadmap
Fase	Status	Features
Fase 1 - MVP	✅ Concluído	Cálculos básicos, áudio, navegação
Fase 2	🔄 Em desenvolvimento	Histórico, exportação, temas
Fase 3	📋 Planejado	Visualização 3D, cálculo de materiais
📄 Licença
Distribuído sob licença MIT.

---


### 🙏 Créditos

Desenvolvido com dedicação por **Gamer Extremo**

---


## 🎥 Gameplay

<!--[![Gameplay Preview](COLE_AQUI_O_LINK_RAW_DA_IMAGEM)](COLE_AQUI_O_LINK_DO_VIDEO_NO_YOUTUBE)-->
[![Gameplay Preview](COLE_AQUI_O_LINK_RAW_DA_IMAGEM)]([COLE_AQUI_O_LINK_DO_VIDEO_NO_YOUTUBE](https://vimeo.com/1184582477))

[<img src="https://raw.githubusercontent.com/GamerExtremoEliteHackerBR/Stair-Calc/refs/heads/main/Screenshots/Screens%20Editor/Captura%20de%20tela%202025-09-27%20232011.png" width="600" alt="Gameplay Screenshot">](https://vimeo.com/1184582477)]

*Clique na imagem para assistir ao vídeo de gameplay*


