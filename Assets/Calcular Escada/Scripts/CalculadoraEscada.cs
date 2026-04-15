using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script principal da calculadora de escadas - Controla toda a lógica de cálculo e UI.
/// Este script vai no objeto canvas da cena EscadaApp
/// </summary>
public class CalculadoraEscada : MonoBehaviour
{
    [Header("Referências de Input")]
    public TMP_InputField inputAlturaEscada;
    public TMP_InputField inputAlturaDegrau;
    public TMP_InputField inputLarguraDegrau;

    [Header("Referências de Texto")]
    public TMP_Text txtNumDegraus;
    public TMP_Text txtComprimentoEscada;
    public TMP_Text txtAnguloEscada;
    public TMP_Text txtHipotenusaEscada;

    [Header("Referências de UI")]
    public GameObject panelCalcular;
    public AudioSettingsMenu audioSettingsMenu;

    [Header("Configurações de Cena")]
    [SerializeField] private string nameScene;

    // Componente de áudio
    private AudioPlayer audioPlayer;

    // =========================================================================
    // MÉTODOS DE INICIALIZAÇÃO
    // =========================================================================

    /// <summary>
    /// Inicializa componentes e configura valores padrão
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //// Era assim antes: início
        //// Inicializar componente de áudio
        //audioPlayer = gameObject.AddComponent<AudioPlayer>();

        //// Valores iniciais
        //inputAlturaEscada.text = "1.65";
        //inputAlturaDegrau.text = "0.15";
        //inputLarguraDegrau.text = "0.26";

        //// Validar nome da cena
        //int buildIndex = SceneManager.GetActiveScene().buildIndex;
        //string currentSceneName = SceneManager.GetSceneByBuildIndex(buildIndex).name;
        //Debug.Log($"BuildIndex: {buildIndex} | SceneName: {currentSceneName}");

        //// Garantir que o painel de cálculo esteja visível no início
        //if (panelCalcular != null)
        //{
        //    panelCalcular.SetActive(true);
        //}

        //// Inicializar textos
        //ResetTexts();
        //// Era assim antes: fim

        InitializeAudio();
        SetDefaultValues();
        ValidateSceneSettings();
        InitializeUI();

    }

    /// <summary>
    /// Configura sistema de áudio
    /// </summary>
    private void InitializeAudio()
    {
        audioPlayer = gameObject.AddComponent<AudioPlayer>();
    }

    /// <summary>
    /// Define valores iniciais nos campos de input
    /// </summary>
    private void SetDefaultValues()
    {
        inputAlturaEscada.text = "1.65";
        inputAlturaDegrau.text = "0.15";
        inputLarguraDegrau.text = "0.26";
    }

    /// <summary>
    /// Valida configurações de cena e debug
    /// </summary>
    private void ValidateSceneSettings()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        string currentSceneName = SceneManager.GetSceneByBuildIndex(buildIndex).name;
        Debug.Log($"BuildIndex: {buildIndex} | SceneName: {currentSceneName}");
    }

    /// <summary>
    /// Inicializa elementos da UI
    /// </summary>
    private void InitializeUI()
    {
        if (panelCalcular != null)
        {
            panelCalcular.SetActive(true);
        }
        ResetTexts();
    }

    // =========================================================================
    // MÉTODOS DE CICLO DE VIDA E EVENTOS
    // =========================================================================

    /// <summary>
    /// Registra eventos quando o objeto é ativado
    /// </summary>
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Remove eventos quando o objeto é desativado
    /// </summary>
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Processa input do usuário a cada frame
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        HandleEscapeInput();
        HandleDebugInput();

        //// Era assim antes: início
        //// Fechar menu de áudio com ESC se estiver aberto
        //if (audioSettingsMenu != null && audioSettingsMenu.IsAudioMenuOpen())
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        CloseAudioMenu();
        //    }
        //}

        //// Debug com F1
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    DebugReferences();
        //}
        //// Era assim antes: fim
        
    }

    /// <summary>
    /// Manipula pressionamento da tecla ESC para fechar menu de áudio
    /// </summary>
    private void HandleEscapeInput()
    {
        if (audioSettingsMenu != null && audioSettingsMenu.IsAudioMenuOpen())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseAudioMenu();
            }
        }
    }

    /// <summary>
    /// Manipula input de debug (tecla F1)
    /// </summary>
    private void HandleDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            DebugReferences();
        }
    }

    /// <summary>
    /// Callback chamado quando uma cena é carregada
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Cena carregada: {scene.name}");

        if (scene.name == "EscadaApp")
        {
            StartCoroutine(RefreshReferencesAfterFrame());
        }
    }

    // =========================================================================
    // MÉTODOS DE GERENCIAMENTO DE REFERÊNCIAS
    // =========================================================================

    /// <summary>
    /// Atualiza referências após um frame (para garantir que objetos estejam carregados)
    /// </summary>
    private IEnumerator RefreshReferencesAfterFrame()
    {
        yield return null;
        RefreshAllReferences();
    }

    /// <summary>
    /// Atualiza todas as referências de objetos da cena
    /// </summary>
    private void RefreshAllReferences()
    {
        Debug.Log("=== ATUALIZANDO TODAS AS REFERÊNCIAS ===");

        RefreshAudioSettingsMenu();
        RefreshPanelCalcular();
        RefreshInputReferences();
        RefreshTextReferences();

        Debug.Log("=== TODAS AS REFERÊNCIAS ATUALIZADAS ===");
        EnsureCorrectUIState();

        /**
        //// Era assim antes: início
        //// Buscar AudioSettingsMenu
        //AudioSettingsMenu audioMenu = FindObjectOfType<AudioSettingsMenu>(true);
        //if (audioMenu != null)
        //{
        //    audioSettingsMenu = audioMenu;
        //    Debug.Log("✓ AudioSettingsMenu reatribuído");

        //    // Atualizar referências do AudioSettingsMenu também
        //    audioMenu.RefreshUIReferences();
        //}

        //// Buscar panelCalcular
        //GameObject panelObj = GameObject.Find("PanelCalcular");
        //if (panelObj != null)
        //{
        //    panelCalcular = panelObj;
        //    Debug.Log("✓ PanelCalcular reatribuído");
        //}

        //// Buscar inputs
        //RefreshInputReferences();

        //// Buscar textos
        //RefreshTextReferences();

        //Debug.Log("=== TODAS AS REFERÊNCIAS ATUALIZADAS ===");

        //// Garantir estados corretos
        //if (panelCalcular != null)
        //{
        //    panelCalcular.SetActive(true);
        //}
        //// Era assim antes: início
        */

    }


    /// <summary>
    /// Busca e atualiza referência do AudioSettingsMenu
    /// </summary>
    private void RefreshAudioSettingsMenu()
    {
        AudioSettingsMenu audioMenu = FindObjectOfType<AudioSettingsMenu>(true);
        if (audioMenu != null)
        {
            audioSettingsMenu = audioMenu;
            Debug.Log("✓ AudioSettingsMenu reatribuído");
            audioMenu.RefreshUIReferences();
        }
    }

    /// <summary>
    /// Busca e atualiza referência do painel principal
    /// </summary>
    private void RefreshPanelCalcular()
    {
        GameObject panelObj = GameObject.Find("PanelCalcular");
        if (panelObj != null)
        {
            panelCalcular = panelObj;
            Debug.Log("✓ PanelCalcular reatribuído");
        }
    }

    /// <summary>
    /// Busca e atualiza referências dos campos de input
    /// </summary>
    private void RefreshInputReferences()
    {
        TMP_InputField[] inputs = FindObjectsOfType<TMP_InputField>();
        foreach (var input in inputs)
        {
            if (input.gameObject.name == "InputAlturaEscada")
            {
                inputAlturaEscada = input;
                Debug.Log("✓ InputAlturaEscada reatribuído");
            }
            else if (input.gameObject.name == "InputAlturaDegrau")
            {
                inputAlturaDegrau = input;
                Debug.Log("✓ InputAlturaDegrau reatribuído");
            }
            else if (input.gameObject.name == "InputLarguraDegrau")
            {
                inputLarguraDegrau = input;
                Debug.Log("✓ inputLarguraDegrau reatribuído");
            }
        }
    }

    /// <summary>
    /// Busca e atualiza referências dos textos de resultado
    /// </summary>
    private void RefreshTextReferences()
    {
        TMP_Text[] texts = FindObjectsOfType<TMP_Text>();
        foreach (var text in texts)
        {
            if (text.gameObject.name == "txtNumDegraus")
            {
                txtNumDegraus = text;
                Debug.Log("✓ txtNumDegraus reatribuído");
            }
            else if (text.gameObject.name == "txtComprimentoEscada")
            {
                txtComprimentoEscada = text;
                Debug.Log("✓ txtComprimentoEscada reatribuído");
            }
            else if (text.gameObject.name == "txtAnguloEscada")
            {
                txtAnguloEscada = text;
                Debug.Log("✓ txtAnguloEscada reatribuído");
            }
            else if (text.gameObject.name == "txtHipotenusaEscada")
            {
                txtHipotenusaEscada = text;
                Debug.Log("✓ txtHipotenusaEscada reatribuído");
            }
        }
    }


    /// <summary>
    /// Garante que a UI esteja no estado correto após atualizar referências
    /// </summary>
    private void EnsureCorrectUIState()
    {
        if (panelCalcular != null)
        {
            panelCalcular.SetActive(true);
        }
    }








    




    // =========================================================================
    // MÉTODOS DE LÓGICA DE CÁLCULO
    // =========================================================================

    /// <summary>
    /// Executa cálculo completo da escada baseado nos inputs do usuário
    /// </summary>
    public void Calcular()
    {
        // Tocar som de clique
        audioPlayer.PlayButtonClick();

        /**
        // Era assim antes: início
        // Validar inputs
        if (string.IsNullOrWhiteSpace(inputAlturaEscada.text) ||
            string.IsNullOrWhiteSpace(inputAlturaDegrau.text) ||
            string.IsNullOrWhiteSpace(inputLarguraDegrau.text))
        {
            Debug.Log("Campos vazios! Insira os dados.");
            return;
        }
        
        float alturaEscada = float.Parse(inputAlturaEscada.text);
        float alturaDegrau = float.Parse(inputAlturaDegrau.text);
        float larguraDegrau = float.Parse(inputLarguraDegrau.text);

        int numDegraus = Mathf.CeilToInt(alturaEscada / alturaDegrau);
        float comprimentoEscada = (numDegraus - 1) * larguraDegrau;
        float anguloEscada = Mathf.Atan(alturaEscada / comprimentoEscada) * Mathf.Rad2Deg;
        float hipotenusaEscada = Mathf.Sqrt(Mathf.Pow(alturaEscada, 2) + Mathf.Pow(comprimentoEscada, 2));

        txtNumDegraus.text = $"Número de Degraus: {numDegraus}";
        txtComprimentoEscada.text = $"Comprimento Total (Base): {comprimentoEscada:F2} centímetros";
        txtAnguloEscada.text = $"Inclinação: {anguloEscada:F2}°";
        txtHipotenusaEscada.text = $"Inclinação em Comprimento: {hipotenusaEscada:F2} centímetros";
        // Era assim antes: fim
        */
        
        if (!ValidateInputs())
        {
            Debug.Log("Campos vazios! Insira os dados.");
            return;
        }

        PerformCalculations();

        // Tocar som de cálculo concluído
        audioPlayer.PlayCalculateSound();
    }

    /// <summary>
    /// Valida se todos os campos de input estão preenchidos
    /// </summary>
    private bool ValidateInputs()
    {
        return !string.IsNullOrWhiteSpace(inputAlturaEscada.text) &&
               !string.IsNullOrWhiteSpace(inputAlturaDegrau.text) &&
               !string.IsNullOrWhiteSpace(inputLarguraDegrau.text);
    }

    /// <summary>
    /// Executa todos os cálculos matemáticos da escada
    /// </summary>
    private void PerformCalculations()
    {
        float alturaEscada = float.Parse(inputAlturaEscada.text);
        float alturaDegrau = float.Parse(inputAlturaDegrau.text);
        float larguraDegrau = float.Parse(inputLarguraDegrau.text);

        int numDegraus = Mathf.CeilToInt(alturaEscada / alturaDegrau);
        float comprimentoEscada = (numDegraus - 1) * larguraDegrau;
        float anguloEscada = Mathf.Atan(alturaEscada / comprimentoEscada) * Mathf.Rad2Deg;
        float hipotenusaEscada = Mathf.Sqrt(Mathf.Pow(alturaEscada, 2) + Mathf.Pow(comprimentoEscada, 2));

        DisplayResults(numDegraus, comprimentoEscada, anguloEscada, hipotenusaEscada);
    }

    /// <summary>
    /// Exibe os resultados dos cálculos na UI
    /// </summary>
    private void DisplayResults(int numDegraus, float comprimento, float angulo, float hipotenusa)
    {
        txtNumDegraus.text = $"Número de Degraus: {numDegraus}";
        txtComprimentoEscada.text = $"Comprimento Total (Base): {comprimento:F2} centímetros";
        txtAnguloEscada.text = $"Inclinação: {angulo:F2}°";
        txtHipotenusaEscada.text = $"Inclinação em Comprimento: {hipotenusa:F2} centímetros";
    }


    // =========================================================================
    // MÉTODOS DE GERENCIAMENTO DE UI
    // =========================================================================

    /// <summary>
    /// Limpa todos os campos de input e resultados
    /// </summary>
    public void ClearValues()
    {
        audioPlayer.PlayButtonClick();

        inputAlturaEscada.text = "";
        inputAlturaDegrau.text = "";
        inputLarguraDegrau.text = "";

        ResetTexts();
        Debug.Log("Todos os Campos zerados!");
    }

    /// <summary>
    /// Reseta textos para valores padrão
    /// </summary>
    private void ResetTexts()
    {
        if (txtNumDegraus != null) txtNumDegraus.text = "Número de Degraus:";
        if (txtComprimentoEscada != null) txtComprimentoEscada.text = "Comprimento Total (Base):";
        if (txtAnguloEscada != null) txtAnguloEscada.text = "Inclinação:";
        if (txtHipotenusaEscada != null) txtHipotenusaEscada.text = "Comprimento da Inclinação:";
    }


    // =========================================================================
    // MÉTODOS DE NAVEGAÇÃO E GERENCIAMENTO DE CENAS
    // =========================================================================

    /// <summary>
    /// Navega para cena específica (método genérico)
    /// </summary>
    public void Config(string sceneName)
    {
        audioPlayer.PlayButtonClick();
        PrepareSceneChange(sceneName);
    }

    /// <summary>
    /// Navega para cena Thanks a partir do menu de áudio
    /// </summary>
    public void OpenAboutFromAudioMenu()
    {
        Debug.Log("Abrindo About do menu de áudio...");
        // Tocar som de clique
        audioPlayer.PlayButtonClick();
        PrepareSceneChange("Thanks");
    }

    /// <summary>
    /// Prepara transição entre cenas com salvamento de configurações
    /// </summary>
    private void PrepareSceneChange(string sceneName)
    {

        //// Era assim antes: início
        //if (AudioManager.Instance != null)
        //{
        //    AudioManager.Instance.SaveAudioSettings();
        //}

        //if (audioSettingsMenu != null && audioSettingsMenu.IsAudioMenuOpen())
        //{
        //    audioSettingsMenu.CloseAudioMenu();
        //}

        //Debug.Log($"Carregando cena: {sceneName}");
        //SceneManager.LoadScene(sceneName);
        //// Era assim antes: fim
        ///
        SaveAudioSettings();
        CloseAudioMenuIfOpen();
        ExecuteSceneChange(sceneName);


    }


    /// <summary>
    /// Salva configurações de áudio antes da transição
    /// </summary>
    private void SaveAudioSettings()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveAudioSettings();
        }
    }

    /// <summary>
    /// Fecha menu de áudio se estiver aberto
    /// </summary>
    private void CloseAudioMenuIfOpen()
    {
        if (audioSettingsMenu != null && audioSettingsMenu.IsAudioMenuOpen())
        {
            audioSettingsMenu.CloseAudioMenu();
        }
    }

    /// <summary>
    /// Executa carregamento da cena
    /// </summary>
    private void ExecuteSceneChange(string sceneName)
    {
        Debug.Log($"Carregando cena: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }


    // =========================================================================
    // MÉTODOS DE GERENCIAMENTO DE MENU DE ÁUDIO
    // =========================================================================

    /// <summary>
    /// Abre menu de configurações de áudio
    /// </summary>
    public void OpenAudioMenu()
    {
        Debug.Log("=== CALCULADORA: Abrindo menu de áudio ===");

        if (audioSettingsMenu != null)
        {
            Debug.Log("AudioSettingsMenu encontrado, chamando OpenAudioMenu()");

            HideCalculationPanel();

            /**
            //// Era assim antes: início
            //// Esconde painel de cálculo ao abrir menu de áudio
            //if (panelCalcular != null)
            //{
            //    Debug.Log($"Escondendo PanelCalcular, estado atual: {panelCalcular.activeInHierarchy}");
            //    panelCalcular.SetActive(false);
            //}
            //// Era assim antes: início
            */

            audioSettingsMenu.OpenAudioMenu();
        }
        else
        {
            Debug.LogError("AudioSettingsMenu NÃO ENCONTRADO no CalculadoraEscada!");
        }
    }

    /// <summary>
    /// Esconde painel de cálculo quando menu de áudio abre
    /// </summary>
    private void HideCalculationPanel()
    {
        if (panelCalcular != null)
        {
            Debug.Log($"Escondendo PanelCalcular, estado atual: {panelCalcular.activeInHierarchy}");
            panelCalcular.SetActive(false);
        }
    }

    /// <summary>
    /// Fecha menu de configurações de áudio
    /// </summary>
    public void CloseAudioMenu()
    {
        Debug.Log("Fechando menu de áudio---");
        if (audioSettingsMenu != null)
        {
            // Fechar menu de áudio
            audioSettingsMenu.CloseAudioMenu();

            // Reexibir painel de cálculo  ao fechar menu de áudio
            ShowCalculationPanel();
            /**
            // Era assim antes: início
            // Reexibe painel de cálculo ao fechar menu de áudio
            if (panelCalcular != null)
            {
                panelCalcular.SetActive(true);
            }
            // Era assim antes: fim
            */

        }
    }


    /// <summary>
    /// Mostra painel de cálculo quando menu de áudio fecha
    /// </summary>
    private void ShowCalculationPanel()
    {
        if (panelCalcular != null)
        {
            panelCalcular.SetActive(true);
        }
    }

    /// <summary>
    /// Verifica se menu de áudio está aberto
    /// </summary>
    public bool IsAudioMenuOpen()
    {
        return audioSettingsMenu != null && audioSettingsMenu.IsAudioMenuOpen();
    }


    // =========================================================================
    // MÉTODOS DE DEBUG E DIAGNÓSTICO
    // =========================================================================

    /// <summary>
    /// Exibe estado atual das referências no console (Debug)
    /// </summary>
    public void DebugReferences()
    {
        Debug.Log("=== DEBUG DE REFERÊNCIAS ===");
        Debug.Log($"AudioSettingsMenu: {audioSettingsMenu != null}");
        Debug.Log($"PanelCalcular: {panelCalcular != null}");
        if (audioSettingsMenu != null)
        {
            Debug.Log($"AudioSettingsPanel: {audioSettingsMenu.audioSettingsPanel != null}");
        }
        Debug.Log("=============================");
    }

}
