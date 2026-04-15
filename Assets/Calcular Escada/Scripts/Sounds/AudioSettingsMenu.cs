using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Este script deve ser componente do objeto AudioSettings da cena EscadaApp.
/// Controla o menu de configurações de áudio do aplicativo
/// Gerencia volumes, mute e configurações de 
/// </summary>
public class AudioSettingsMenu : MonoBehaviour
{
    [Header("Sliders de Volume")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    [Header("Textos dos Valores")]
    public TMP_Text masterVolumeText;
    public TMP_Text musicVolumeText;
    public TMP_Text sfxVolumeText;

    [Header("Toggle e Botões")]
    public Toggle muteToggle;
    public Button closeButton;
    public Button resetButton;

    [Header("UI Elements")]
    public GameObject audioSettingsPanel;
    public Button openAudioMenuButton;

    [Header("Referências")]
    public CalculadoraEscada calculadoraEscada;

    // Controle de estado interno
    private bool isFirstLoad = true;

    // =========================================================================
    // MÉTODOS DE INICIALIZAÇÃO
    // =========================================================================

    /// <summary>
    /// Inicializa o menu de áudio na cena
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        calculadoraEscada = FindObjectOfType<CalculadoraEscada>();
        Debug.Log("AudioSettingsMenu - CalculadoraEscada encontrada: " + (calculadoraEscada != null));

        Debug.Log("AudioSettingsMenu Start() - Iniciando");
        InitializeUI();
        LoadCurrentSettings();
        isFirstLoad = false;

        ValidatePanelReference();

        //if (audioSettingsPanel == null)
        //{
        //    Debug.LogError("AudioSettingsPanel não foi atribuído no Inspector!");
        //}
        //else
        //{
        //    Debug.Log($"AudioSettingsPanel encontrado: {audioSettingsPanel.name}");
        //}

        SetDefaultSound();
    }

    /// <summary>
    /// Valida referência do painel principal
    /// </summary>
    private void ValidatePanelReference()
    {
        if (audioSettingsPanel == null)
        {
            Debug.LogError("AudioSettingsPanel não foi atribuído no Inspector!");
        }
        else
        {
            Debug.Log($"AudioSettingsPanel encontrado: {audioSettingsPanel.name}");
        }
    }

    /// <summary>
    /// Atualiza a cada frame - gerencia input do usuário
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        HandleEscapeKey();

        //if (audioSettingsPanel != null && audioSettingsPanel.activeInHierarchy)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        CloseAudioMenu();
        //    }
        //}
    }

    /// <summary>
    /// Fecha menu com tecla ESC quando aberto
    /// </summary>
    private void HandleEscapeKey()
    {
        if (audioSettingsPanel != null && audioSettingsPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAudioMenu();
            }
        }
    }


    // =========================================================================
    // MÉTODOS DE GERENCIAMENTO DE REFERÊNCIAS
    // =========================================================================

    /// <summary>
    /// Busca e atualiza automaticamente referências da UI
    /// Útil quando referências são perdidas entre cenas
    /// </summary>
    public void RefreshUIReferences()
    {
        ////Era assim antes: início
        //Debug.Log("AudioSettingsMenu: Atualizando referências da UI...");

        //// Buscar o painel principal
        //if (audioSettingsPanel == null)
        //{
        //    audioSettingsPanel = GameObject.Find("AudioSettingsPanel");
        //    if (audioSettingsPanel != null)
        //        Debug.Log("✓ AudioSettingsPanel encontrado e atribuído");
        //}

        //// Buscar sliders
        //if (masterVolumeSlider == null)
        //    masterVolumeSlider = GameObject.Find("MasterVolumeSlider")?.GetComponent<Slider>();
        //if (musicVolumeSlider == null)
        //    musicVolumeSlider = GameObject.Find("MusicVolumeSlider")?.GetComponent<Slider>();
        //if (sfxVolumeSlider == null)
        //    sfxVolumeSlider = GameObject.Find("SFXVolumeSlider")?.GetComponent<Slider>();

        //// Buscar textos
        //if (masterVolumeText == null)
        //    masterVolumeText = GameObject.Find("MasterVolumeText")?.GetComponent<TMP_Text>();
        //if (musicVolumeText == null)
        //    musicVolumeText = GameObject.Find("MusicVolumeText")?.GetComponent<TMP_Text>();
        //if (sfxVolumeText == null)
        //    sfxVolumeText = GameObject.Find("SFXVolumeText")?.GetComponent<TMP_Text>();

        //// Buscar botões
        //if (closeButton == null)
        //    closeButton = GameObject.Find("CloseButton")?.GetComponent<Button>();
        //if (resetButton == null)
        //    resetButton = GameObject.Find("ResetButton")?.GetComponent<Button>();
        //if (muteToggle == null)
        //    muteToggle = GameObject.Find("MuteToggle")?.GetComponent<Toggle>();
        //if (openAudioMenuButton == null)
        //    openAudioMenuButton = GameObject.Find("btnConfig")?.GetComponent<Button>();

        //// Reconfigurar eventos
        //InitializeUIEvents();
        ////Era assim antes: fim


        Debug.Log("AudioSettingsMenu: Atualizando referências da UI...");

        FindPanelReference();
        FindSliderReferences();
        FindTextReferences();
        FindButtonReferences();

        // Reconfigurar eventos
        InitializeUIEvents();
    }

    /// <summary>
    /// Busca referência do painel principal
    /// </summary>
    private void FindPanelReference()
    {
        if (audioSettingsPanel == null)
        {
            audioSettingsPanel = GameObject.Find("AudioSettingsPanel");
            if (audioSettingsPanel != null)
                Debug.Log("✓ AudioSettingsPanel encontrado e atribuído");
        }
    }

    /// <summary>
    /// Busca referências dos sliders de volume
    /// </summary>
    private void FindSliderReferences()
    {
        if (masterVolumeSlider == null)
            masterVolumeSlider = GameObject.Find("MasterVolumeSlider")?.GetComponent<Slider>();
        if (musicVolumeSlider == null)
            musicVolumeSlider = GameObject.Find("MusicVolumeSlider")?.GetComponent<Slider>();
        if (sfxVolumeSlider == null)
            sfxVolumeSlider = GameObject.Find("SFXVolumeSlider")?.GetComponent<Slider>();
    }

    /// <summary>
    /// Busca referências dos textos de porcentagem
    /// </summary>
    private void FindTextReferences()
    {
        if (masterVolumeText == null)
            masterVolumeText = GameObject.Find("MasterVolumeText")?.GetComponent<TMP_Text>();
        if (musicVolumeText == null)
            musicVolumeText = GameObject.Find("MusicVolumeText")?.GetComponent<TMP_Text>();
        if (sfxVolumeText == null)
            sfxVolumeText = GameObject.Find("SFXVolumeText")?.GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Busca referências dos botões e toggle
    /// </summary>
    private void FindButtonReferences()
    {
        if (closeButton == null)
            closeButton = GameObject.Find("CloseButton")?.GetComponent<Button>();
        if (resetButton == null)
            resetButton = GameObject.Find("ResetButton")?.GetComponent<Button>();
        if (muteToggle == null)
            muteToggle = GameObject.Find("MuteToggle")?.GetComponent<Toggle>();
        if (openAudioMenuButton == null)
            openAudioMenuButton = GameObject.Find("btnConfig")?.GetComponent<Button>();
    }


    // =========================================================================
    // MÉTODOS DE CONFIGURAÇÃO DA UI
    // =========================================================================

    /// <summary>
    /// Configura inicialmente todos os elementos da UI
    /// </summary>

    void InitializeUI()
    {
        Debug.Log("Inicializando UI do AudioSettingsMenu");
        InitializeUIEvents();
        SetInitialPanelState();

        ////Era assim antes: início
        //if (audioSettingsPanel != null)
        //{
        //    audioSettingsPanel.SetActive(false);
        //    Debug.Log("AudioSettingsPanel fechado inicialmente");
        //}
        ////Era assim antes: fim
    }

    /// <summary>
    /// Configura eventos para todos os elementos interativos
    /// </summary>
    void InitializeUIEvents()
    {
        ////Era assim antes: início
        //// Configurar sliders
        //if (masterVolumeSlider != null)
        //{
        //    masterVolumeSlider.onValueChanged.RemoveAllListeners();
        //    masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        //    masterVolumeSlider.minValue = 0f;
        //    masterVolumeSlider.maxValue = 1f;
        //}

        //if (musicVolumeSlider != null)
        //{
        //    musicVolumeSlider.onValueChanged.RemoveAllListeners();
        //    musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        //    musicVolumeSlider.minValue = 0f;
        //    musicVolumeSlider.maxValue = 1f;
        //}

        //if (sfxVolumeSlider != null)
        //{
        //    sfxVolumeSlider.onValueChanged.RemoveAllListeners();
        //    sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        //    sfxVolumeSlider.minValue = 0f;
        //    sfxVolumeSlider.maxValue = 1f;
        //}

        //// Configurar toggle
        //if (muteToggle != null)
        //{
        //    muteToggle.onValueChanged.RemoveAllListeners();
        //    muteToggle.onValueChanged.AddListener(OnMuteToggleChanged);
        //}

        //// Configurar botões
        //if (closeButton != null)
        //{
        //    closeButton.onClick.RemoveAllListeners();
        //    closeButton.onClick.AddListener(CloseAudioMenu);
        //}

        //if (resetButton != null)
        //{
        //    resetButton.onClick.RemoveAllListeners();
        //    resetButton.onClick.AddListener(ResetToDefault);
        //}

        //if (openAudioMenuButton != null)
        //{
        //    openAudioMenuButton.onClick.RemoveAllListeners();
        //    openAudioMenuButton.onClick.AddListener(OpenAudioMenu);
        //}
        ////Era assim antes: fim

        ConfigureVolumeSliders();
        ConfigureMuteToggle();
        ConfigureActionButtons();

    }

    /// <summary>
    /// Configura sliders de volume com ranges e eventos
    /// </summary>
    private void ConfigureVolumeSliders()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.onValueChanged.RemoveAllListeners();
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            masterVolumeSlider.minValue = 0f;
            masterVolumeSlider.maxValue = 1f;
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.RemoveAllListeners();
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            musicVolumeSlider.minValue = 0f;
            musicVolumeSlider.maxValue = 1f;
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.RemoveAllListeners();
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            sfxVolumeSlider.minValue = 0f;
            sfxVolumeSlider.maxValue = 1f;
        }
    }

    /// <summary>
    /// Configura toggle de mute
    /// </summary>
    private void ConfigureMuteToggle()
    {
        if (muteToggle != null)
        {
            muteToggle.onValueChanged.RemoveAllListeners();
            muteToggle.onValueChanged.AddListener(OnMuteToggleChanged);
        }
    }

    /// <summary>
    /// Configura botões de ação
    /// </summary>
    private void ConfigureActionButtons()
    {
        if (closeButton != null)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(CloseAudioMenu);
        }

        if (resetButton != null)
        {
            resetButton.onClick.RemoveAllListeners();
            resetButton.onClick.AddListener(ResetToDefault);
        }

        if (openAudioMenuButton != null)
        {
            openAudioMenuButton.onClick.RemoveAllListeners();
            openAudioMenuButton.onClick.AddListener(OpenAudioMenu);
        }
    }

    /// <summary>
    /// Define estado inicial do painel (fechado)
    /// </summary>
    private void SetInitialPanelState()
    {
        if (audioSettingsPanel != null)
        {
            audioSettingsPanel.SetActive(false);
            Debug.Log("AudioSettingsPanel fechado inicialmente");
        }
    }

    // =========================================================================
    // MÉTODOS DE GERENCIAMENTO DE CONFIGURAÇÕES
    // =========================================================================

    /// <summary>
    /// Carrega configurações atuais do AudioManager
    /// </summary>
    void LoadCurrentSettings()
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager Instance não encontrado!");
            return;
        }

        Debug.Log("Carregando configurações atuais de áudio");

        float masterVolume = AudioManager.Instance.GetMasterVolume();
        float musicVolume = AudioManager.Instance.GetMusicVolume();
        float sfxVolume = AudioManager.Instance.GetSFXVolume();

        Debug.Log($"Volumes carregados - Master: {masterVolume}, Music: {musicVolume}, SFX: {sfxVolume}");

        //// Era assim antes: início
        //if (masterVolumeSlider != null)
        //{
        //    masterVolumeSlider.SetValueWithoutNotify(masterVolume);
        //    UpdateVolumeText(masterVolumeText, masterVolume);
        //}

        //if (musicVolumeSlider != null)
        //{
        //    musicVolumeSlider.SetValueWithoutNotify(musicVolume);
        //    UpdateVolumeText(musicVolumeText, musicVolume);
        //}

        //if (sfxVolumeSlider != null)
        //{
        //    sfxVolumeSlider.SetValueWithoutNotify(sfxVolume);
        //    UpdateVolumeText(sfxVolumeText, sfxVolume);
        //}

        //if (muteToggle != null)
        //{
        //    muteToggle.SetIsOnWithoutNotify(AudioManager.Instance.IsMasterMuted());
        //}
        //// Era assim antes: fim

        ApplySettingsToUI(masterVolume, musicVolume, sfxVolume);
    }


    /// <summary>
    /// Aplica configurações carregadas nos elementos da UI
    /// </summary>
    private void ApplySettingsToUI(float masterVolume, float musicVolume, float sfxVolume)
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.SetValueWithoutNotify(masterVolume);
            UpdateVolumeText(masterVolumeText, masterVolume);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.SetValueWithoutNotify(musicVolume);
            UpdateVolumeText(musicVolumeText, musicVolume);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.SetValueWithoutNotify(sfxVolume);
            UpdateVolumeText(sfxVolumeText, sfxVolume);
        }

        if (muteToggle != null)
        {
            muteToggle.SetIsOnWithoutNotify(AudioManager.Instance.IsMasterMuted());
        }
    }

    // =========================================================================
    // EVENT HANDLERS - VOLUME E MUTE
    // =========================================================================

    /// <summary>
    /// Manipula mudanças no volume mestre
    /// </summary>
    public void OnMasterVolumeChanged(float volume)
    {
        Debug.Log($"MasterVolume alterado para: {volume}");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMasterVolume(volume);
            UpdateVolumeText(masterVolumeText, volume);

            //// Era assim antes: início
            //if (muteToggle != null)
            //{
            //    muteToggle.SetIsOnWithoutNotify(volume <= 0.01f);
            //}
            //// Era assim antes: fim
            UpdateMuteToggleState(volume);
        }
    }

    /// <summary>
    /// Manipula mudanças no volume da música
    /// </summary>
    public void OnMusicVolumeChanged(float volume)
    {
        Debug.Log($"MusicVolume alterado para: {volume}");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(volume);
            UpdateVolumeText(musicVolumeText, volume);
        }
    }

    /// <summary>
    /// Manipula mudanças no volume de efeitos sonoros
    /// </summary>
    public void OnSFXVolumeChanged(float volume)
    {
        Debug.Log($"SFXVolume alterado para: {volume}");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSFXVolume(volume);
            UpdateVolumeText(sfxVolumeText, volume);

            //// Era assim antes: início
            //if (volume > 0.01f && !isFirstLoad)
            //{
            //    Invoke(nameof(PlayTestSFX), 0.1f);
            //}
            //// Era assim antes: fim

            PlayTestSoundIfNeeded(volume);
        }
    }

    /// <summary>
    /// Manipula mudanças no toggle de mute
    /// </summary>
    public void OnMuteToggleChanged(bool isMuted)
    {
        Debug.Log($"MuteToggle alterado para: {isMuted}");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMasterMute(isMuted);


            //// Era assim antes: início
            //if (isMuted)
            //{
            //    UpdateVolumeText(masterVolumeText, 0f);
            //    if (masterVolumeSlider != null)
            //        masterVolumeSlider.SetValueWithoutNotify(0f);
            //}
            //else
            //{
            //    float currentVolume = AudioManager.Instance.GetMasterVolume();
            //    UpdateVolumeText(masterVolumeText, currentVolume);
            //    if (masterVolumeSlider != null)
            //        masterVolumeSlider.SetValueWithoutNotify(currentVolume);
            //}
            //// Era assim antes: fim

            HandleMuteStateChange(isMuted);
        }
    }

    // =========================================================================
    // MÉTODOS AUXILIARES DE EVENTOS
    // =========================================================================

    /// <summary>
    /// Atualiza estado do toggle de mute baseado no volume
    /// </summary>
    private void UpdateMuteToggleState(float volume)
    {
        if (muteToggle != null)
        {
            muteToggle.SetIsOnWithoutNotify(volume <= 0.01f);
        }
    }

    /// <summary>
    /// Toca som de teste quando ajustar SFX (apenas se não for carregamento inicial)
    /// </summary>
    private void PlayTestSoundIfNeeded(float volume)
    {
        if (volume > 0.01f && !isFirstLoad)
        {
            Invoke(nameof(PlayTestSFX), 0.1f);
        }
    }

    /// <summary>
    /// Gerencia mudança de estado do mute
    /// </summary>
    private void HandleMuteStateChange(bool isMuted)
    {
        if (isMuted)
        {
            UpdateVolumeText(masterVolumeText, 0f);
            if (masterVolumeSlider != null)
                masterVolumeSlider.SetValueWithoutNotify(0f);
        }
        else
        {
            float currentVolume = AudioManager.Instance.GetMasterVolume();
            UpdateVolumeText(masterVolumeText, currentVolume);
            if (masterVolumeSlider != null)
                masterVolumeSlider.SetValueWithoutNotify(currentVolume);
        }
    }


    // =========================================================================
    // MÉTODOS DE CONTROLE DO MENU
    // =========================================================================

    /// <summary>
    /// Abre o menu de configurações de áudio
    /// </summary>
    public void OpenAudioMenu()
    {
        Debug.Log("=== TENTANDO ABRIR MENU DE ÁUDIO ===");

        if (audioSettingsPanel != null)
        {
            Debug.Log($"AudioSettingsPanel encontrado, estado atual: {audioSettingsPanel.activeInHierarchy}");
            audioSettingsPanel.SetActive(true);
            Debug.Log($"AudioSettingsPanel aberto, novo estado: {audioSettingsPanel.activeInHierarchy}");

            LoadCurrentSettings();
            PlayTestSFX();
            Debug.Log("Menu de áudio aberto com sucesso!");
        }
        else
        {
            Debug.LogError("AudioSettingsPanel NÃO ENCONTRADO - Verifique a referência no Inspector!");
        }
    }

    /// <summary>
    /// Fecha o menu de configurações de áudio
    /// </summary>
    public void CloseAudioMenu()
    {
        Debug.Log("=== FECHANDO MENU DE ÁUDIO ===");

        if (audioSettingsPanel != null)
        {
            Debug.Log($"Fechando AudioSettingsPanel, estado atual: {audioSettingsPanel.activeInHierarchy}");
            audioSettingsPanel.SetActive(false);
            Debug.Log($"AudioSettingsPanel fechado, novo estado: {audioSettingsPanel.activeInHierarchy}");

            //// Era assim antes: início
            //// Salvando configurações ao fechar
            //if (AudioManager.Instance != null)
            //{
            //    AudioManager.Instance.SaveAudioSettings();
            //}
            //// Era assim antes: fim

            SaveAudioSettings();
            PlayTestSFX();
            Debug.Log("Menu de áudio fechado com sucesso!");
        }
        else
        {
            Debug.LogError("AudioSettingsPanel NÃO ENCONTRADO ao tentar fechar!");
        }
    }


    /// <summary>
    /// Reseta todas as configurações para valores padrão
    /// CORREÇÃO: Também desativa mute se estiver ativo
    /// CORREÇÃO: Garante que o mute seja desativado e o estado da UI sincronizado
    /// </summary>
    public void SetDefaultSound()
    {
        Debug.Log("=== INICIANDO RESET DE CONFIGURAÇÕES ===");
        Debug.Log("Resetando configurações de áudio para padrão");


        // VALORES PADRÃO - Máximo é 1f
        float defaultMaster = 0.8f;
        float defaultMusic = 0.7f;
        float defaultSFX = 0.9f;

        /**
        //// Era assim antes: início
        //// Aplicar valores padrão no AudioManager
        //if (AudioManager.Instance != null)
        //{
        //    AudioManager.Instance.SetMasterVolume(defaultMaster);
        //    AudioManager.Instance.SetMusicVolume(defaultMusic);
        //    AudioManager.Instance.SetSFXVolume(defaultSFX);
        //}

        //if (masterVolumeSlider != null)
        //{
        //    masterVolumeSlider.SetValueWithoutNotify(defaultMaster);
        //    UpdateVolumeText(masterVolumeText, defaultMaster);
        //}
        //if (musicVolumeSlider != null)
        //{
        //    musicVolumeSlider.SetValueWithoutNotify(defaultMusic);
        //    UpdateVolumeText(musicVolumeText, defaultMusic);
        //}
        //if (sfxVolumeSlider != null)
        //{
        //    sfxVolumeSlider.SetValueWithoutNotify(defaultSFX);
        //    UpdateVolumeText(sfxVolumeText, defaultSFX);
        //}
        //if (muteToggle != null)
        //{
        //    muteToggle.SetIsOnWithoutNotify(false);
        //}
        //// Era assim antes: fim
        */

        // ✅ CORREÇÃO 1: VERIFICAR E DESATIVAR MUTE SE NECESSÁRIO
        bool wasMuted = false;
        if (AudioManager.Instance != null)
        {
            wasMuted = AudioManager.Instance.IsMasterMuted();
            if (wasMuted)
            {
                Debug.Log("🔊 Desativando mute antes do reset...");
                AudioManager.Instance.ToggleMasterMute(false);
            }
        }

        ////ok
        //// ✅ CORREÇÃO: DESATIVAR MUTE PRIMEIRO
        //if (AudioManager.Instance != null && AudioManager.Instance.IsMasterMuted())
        //{
        //    Debug.Log("✅ Desativando mute durante reset...");
        //    AudioManager.Instance.ToggleMasterMute(false);
        //}

        // APLICAR VALORES PADRÃO
        ApplyDefaultSettings(defaultMaster, defaultMusic, defaultSFX);

        // ATUALIZAR UI
        UpdateUIWithDefaultValues(defaultMaster, defaultMusic, defaultSFX);

        // ✅ CORREÇÃO 2: LOG DETALHADO PARA DEBUG
        Debug.Log($"Reset concluído - Mute anterior: {wasMuted}, Volume Mestre: {defaultMaster}");

        // ✅ AGORA O SOM DEVE TOCAR CORRETAMENTE
        // ✅ CORREÇÃO 3: TOCAR SOM DE CONFIRMAÇÃO (AGORA FUNCIONA!)

        //PlayTestSFX();
        //AudioManager.Instance.Play("ButtonClick"); // É MESMO QUE PlayTestSFX();

        ///POSSO FAZER ASSIM PARA EVITAR O CLIQUE INICIAL AO ABRIR O MENU, POIS ESSE MÉTODO É CHAMADO NO START, ENTÃO O SOM TOCA LOGO AO INICIAR O JOGO, O QUE NÃO É DESEJADO.
        ///Para esse vou add o som direto no click do botão, pois preciso chamar ele no Start
        ///por isso devo passar o som do clique direto no botão para assim evitar um clique ao iniciar
        ///Devo fazer isso em todos os botões que usam esse método.

        Debug.Log("Configurações de áudio resetadas para padrão");
    }

    /// <summary>
    /// Reseta todas as configurações para valores padrão
    /// CORREÇÃO: Também desativa mute se estiver ativo
    /// CORREÇÃO: Garante que o mute seja desativado e o estado da UI sincronizado
    /// </summary>
    public void ResetToDefault()
    {
        Debug.Log("=== INICIANDO RESET DE CONFIGURAÇÕES ===");
        Debug.Log("Resetando configurações de áudio para padrão");


        // VALORES PADRÃO
        float defaultMaster = 0.8f;
        float defaultMusic = 0.7f;
        float defaultSFX = 0.9f;

        /**
        //// Era assim antes: início
        //// Aplicar valores padrão no AudioManager
        //if (AudioManager.Instance != null)
        //{
        //    AudioManager.Instance.SetMasterVolume(defaultMaster);
        //    AudioManager.Instance.SetMusicVolume(defaultMusic);
        //    AudioManager.Instance.SetSFXVolume(defaultSFX);
        //}

        //if (masterVolumeSlider != null)
        //{
        //    masterVolumeSlider.SetValueWithoutNotify(defaultMaster);
        //    UpdateVolumeText(masterVolumeText, defaultMaster);
        //}
        //if (musicVolumeSlider != null)
        //{
        //    musicVolumeSlider.SetValueWithoutNotify(defaultMusic);
        //    UpdateVolumeText(musicVolumeText, defaultMusic);
        //}
        //if (sfxVolumeSlider != null)
        //{
        //    sfxVolumeSlider.SetValueWithoutNotify(defaultSFX);
        //    UpdateVolumeText(sfxVolumeText, defaultSFX);
        //}
        //if (muteToggle != null)
        //{
        //    muteToggle.SetIsOnWithoutNotify(false);
        //}
        //// Era assim antes: fim
        */

        // ✅ CORREÇÃO 1: VERIFICAR E DESATIVAR MUTE SE NECESSÁRIO
        bool wasMuted = false;
        if (AudioManager.Instance != null)
        {
            wasMuted = AudioManager.Instance.IsMasterMuted();
            if (wasMuted)
            {
                Debug.Log("🔊 Desativando mute antes do reset...");
                AudioManager.Instance.ToggleMasterMute(false);
            }
        }

        ////ok
        //// ✅ CORREÇÃO: DESATIVAR MUTE PRIMEIRO
        //if (AudioManager.Instance != null && AudioManager.Instance.IsMasterMuted())
        //{
        //    Debug.Log("✅ Desativando mute durante reset...");
        //    AudioManager.Instance.ToggleMasterMute(false);
        //}

        // APLICAR VALORES PADRÃO
        ApplyDefaultSettings(defaultMaster, defaultMusic, defaultSFX);

        // ATUALIZAR UI
        UpdateUIWithDefaultValues(defaultMaster, defaultMusic, defaultSFX);

        // ✅ CORREÇÃO 2: LOG DETALHADO PARA DEBUG
        Debug.Log($"Reset concluído - Mute anterior: {wasMuted}, Volume Mestre: {defaultMaster}");

        // ✅ AGORA O SOM DEVE TOCAR CORRETAMENTE
        // ✅ CORREÇÃO 3: TOCAR SOM DE CONFIRMAÇÃO (AGORA FUNCIONA!)
        PlayTestSFX();
        Debug.Log("Configurações de áudio resetadas para padrão");
    }


    // =========================================================================
    // MÉTODOS AUXILIARES
    // =========================================================================

    /// <summary>
    /// Aplica configurações padrão no AudioManager
    /// Aplica configurações padrão no AudioManager - CORREÇÃO: Força atualização
    /// </summary>
    private void ApplyDefaultSettings(float master, float music, float sfx)
    {
        if (AudioManager.Instance != null)
        {
            // ✅ CORREÇÃO: FORÇAR ATUALIZAÇÃO MESMO SE ESTAVA MUDO
            AudioManager.Instance.SetMasterVolume(master);
            AudioManager.Instance.SetMusicVolume(music);
            AudioManager.Instance.SetSFXVolume(sfx);

            Debug.Log($"Volumes aplicados - Mestre: {master}, Música: {music}, SFX: {sfx}");
        }
        else
        {
            Debug.LogError("❌ AudioManager não encontrado durante reset!");
        }
    }

    /// <summary>
    /// Atualiza UI com valores padrão
    /// Atualiza UI com valores padrão - CORREÇÃO: Sincroniza toggle de mute
    /// </summary>
    private void UpdateUIWithDefaultValues(float master, float music, float sfx)
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.SetValueWithoutNotify(master);
            UpdateVolumeText(masterVolumeText, master);
        }
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.SetValueWithoutNotify(music);
            UpdateVolumeText(musicVolumeText, music);
        }
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.SetValueWithoutNotify(sfx);
            UpdateVolumeText(sfxVolumeText, sfx);
        }
        //if (muteToggle != null)
        //{
        //    muteToggle.SetIsOnWithoutNotify(false);
        //}
        // ✅ CORREÇÃO: SINCRONIZAR TOGGLE COM ESTADO REAL
        if (muteToggle != null)
        {
            // Verificar estado atual do AudioManager, não assumir false
            bool isCurrentlyMuted = AudioManager.Instance != null && AudioManager.Instance.IsMasterMuted();
            muteToggle.SetIsOnWithoutNotify(isCurrentlyMuted);
            Debug.Log($"✅ Toggle de mute sincronizado: {isCurrentlyMuted}");
        }
    }

    /// <summary>
    /// Toca efeito sonoro de teste
    /// </summary>
    private void PlayTestSFX()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Play("ButtonClick");
        }
    }

    /// <summary>
    /// Salva configurações atuais
    /// </summary>
    private void SaveAudioSettings()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveAudioSettings();
        }
    }

    /// <summary>
    /// Atualiza texto de porcentagem do volume
    /// </summary>

    private void UpdateVolumeText(TMP_Text textElement, float volume)
    {
        if (textElement != null)
        {
            int percentage = Mathf.RoundToInt(volume * 100);
            textElement.text = $"{percentage}%";
        }
    }


    // =========================================================================
    // MÉTODOS DE CONSULTA DE ESTADO
    // =========================================================================

    /// <summary>
    /// Verifica se o menu de áudio está atualmente aberto
    /// </summary>
    public bool IsAudioMenuOpen()
    {
        bool isOpen = audioSettingsPanel != null && audioSettingsPanel.activeInHierarchy;
        Debug.Log($"Verificando se menu está aberto: {isOpen}");
        return isOpen;
    }

}
