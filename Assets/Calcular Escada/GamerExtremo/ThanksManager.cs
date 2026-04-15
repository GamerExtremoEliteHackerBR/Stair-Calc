using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
//using static Unity.Burst.Intrinsics.X86;

/// <summary>
/// Controla a cena de agradecimentos e créditos do aplicativo
/// Este script vai no objeto ThanksManager da cena Thanks
/// </summary>
public class ThanksManager : MonoBehaviour
{
    // =========================================================================
    // REFERÊNCIAS DE UI
    // =========================================================================

    [Header("Referências de UI")]
    public Button btnBackMainMenu;
    //public TMP_Text thanksText;
    public Text thanksText; // Referência para o componente Text que exibe a mensagem
    [SerializeField] private string nameScene; // Aqui vai o nome da minha cena principal EscadaApp

    ////3. Alternativa se você quiser manter o texto no Inspector:
    ////Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:
    //[SerializeField] private Text thanksText;
    //// Ou
    //[SerializeField] private string thanksMessageTemplate;
    ////3. Alternativa se você quiser manter o texto no Inspector:
    ////Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:

    [Header("Configurações de Cenas")]
    public List<string> namesGameScenes = new List<string>();

    // =========================================================================
    // COMPONENTES E ESTADO
    // =========================================================================

    /// <summary>
    /// Componente de áudio para feedback sonoro
    /// </summary>
    private AudioPlayer audioPlayer;


    // =========================================================================
    // MÉTODOS DE INICIALIZAÇÃO
    // =========================================================================

    /// <summary>
    /// Inicializa a cena de agradecimentos
    /// Configura áudio, textos e botões
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        InitializeAudio();
        ValidateSceneSettings();
        UpdateThanksText();
        ConfigureBackButton();

        ////Era assim antes: start
        //// Inicializar componente de áudio
        //audioPlayer = gameObject.AddComponent<AudioPlayer>();

        ////namesGameScenes.Contains(nameScene);
        ////Debug.Log("Cenas: " + namesGameScenes.Contains(nameScene));
        ////Debug.Log("Teste: " + namesGameScenes.Count);
        ////Debug.Log("Teste4: " + SceneManager.GetAllScenes());
        ////Debug.Log("Teste4: " + SceneManager.sceneCount);


        //// Exemplo: pega o buildIndex atual
        //int buildIndex = SceneManager.GetActiveScene().buildIndex;

        //// Pega o nome da cena pelo buildIndex
        //string currentSceneName = SceneManager.GetSceneByBuildIndex(buildIndex).name;

        //Debug.Log($"BuildIndex: {buildIndex} | SceneName: {currentSceneName}");


        ////Descomentar para garantir o nome correto da cena, Comentei só pra testes
        //// Validando o nome da cena
        //if (nameScene == "" || nameScene != currentSceneName)
        //{
        //    //nameScene = "EscadaApp"; // Esse é o nome da cena que quero abir, esse script vai no objeto ThanksManager da cena Thanks
        //}


        //// Atualiza o texto de agradecimento com o nome do jogo
        //UpdateThanksText();

        ////// Configura o botão (se necessário)
        ////if (btnBackMainMenu != null)
        ////{
        ////    btnBackMainMenu.onClick.AddListener(() => BackMainMenu(nameScene));//Aqui, volta pra cena EscadaApp
        ////    Debug.Log("ATENÇÃO AO NOME DA CENA.");
        ////}

        //// Configura o botão (se necessário)
        //if (btnBackMainMenu != null)
        //{
        //    btnBackMainMenu.onClick.AddListener(() =>
        //    {
        //        // Tocar som do botão
        //        audioPlayer.PlayButtonClick();
        //        BackToMainMenu();//Aqui, volta pra cena EscadaApp
        //    });
        //    Debug.Log("ATENÇÃO AO NOME DA CENA.");
        //}
        ////Era assim antes: end
    }


    /// <summary>
    /// Inicializa sistema de áudio
    /// </summary>
    private void InitializeAudio()
    {
        audioPlayer = gameObject.AddComponent<AudioPlayer>();
    }

    /// <summary>
    /// Valida configurações de cena e debug
    /// </summary>
    private void ValidateSceneSettings()
    {
        //namesGameScenes.Contains(nameScene);
        //Debug.Log("Cenas: " + namesGameScenes.Contains(nameScene));
        //Debug.Log("Teste: " + namesGameScenes.Count);
        //Debug.Log("Teste4: " + SceneManager.GetAllScenes());
        //Debug.Log("Teste4: " + SceneManager.sceneCount);

        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        string currentSceneName = SceneManager.GetSceneByBuildIndex(buildIndex).name;

        Debug.Log($"BuildIndex: {buildIndex} | SceneName: {currentSceneName}");

        //Descomentar para garantir o nome correto da cena, Comentei só pra testes
        // Validando o nome da cena
        if (nameScene == "" || nameScene != currentSceneName)
        {
            //nameScene = "EscadaApp"; // Esse é o nome da cena que quero abir, esse script vai no objeto ThanksManager da cena Thanks
        }
    }

    /// <summary>
    /// Configura botão de voltar ao menu principal
    /// </summary>
    private void ConfigureBackButton()
    {
        //// Configura o botão (se necessário)
        //if (btnBackMainMenu != null)
        //{
        //    btnBackMainMenu.onClick.AddListener(() => BackMainMenu(nameScene));//Aqui, volta pra cena EscadaApp
        //    Debug.Log("ATENÇÃO AO NOME DA CENA.");
        //}

        // Configura o botão (se necessário)
        if (btnBackMainMenu != null)
        {
            btnBackMainMenu.onClick.AddListener(() =>
            {
                // Tocar som do botão
                audioPlayer.PlayButtonClick();
                BackToMainMenu();//Aqui, volta pra cena EscadaApp
            });
            Debug.Log("ATENÇÃO AO NOME DA CENA.");
        }
    }



    // =========================================================================
    // MÉTODOS DE ATUALIZAÇÃO
    // =========================================================================

    /// <summary>
    /// Processa input do usuário a cada frame
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        // Voltar com ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
    }

    // =========================================================================
    // MÉTODOS DE GERENCIAMENTO DE TEXTO
    // =========================================================================

    /// <summary>
    /// Atualiza o texto de agradecimento com o nome do aplicativo
    /// </summary>
    private void UpdateThanksText()
    {
        if (thanksText != null)
        {
            string gameName = Application.productName; // Obtém o nome do jogo das Player Settings
            string thanksMessage = GenerateThanksMessage(gameName);
            thanksText.text = thanksMessage;
        }

        ////3. Alternativa se você quiser manter o texto no Inspector:
        ////Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:
        ////E usar string.Format() para substituir o nome do jogo:
        //if (thanksText != null)
        //{
        //    string gameName = Application.productName;
        //    thanksText.text = string.Format(thanksMessageTemplate, gameName);
        //}
        ////3. Alternativa se você quiser manter o texto no Inspector:
        ////Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:
        ////E usar string.Format() para substituir o nome do jogo:
    }


    /// <summary>
    /// Gera mensagem de agradecimento personalizada com nome do aplicativo
    /// </summary>
    private string GenerateThanksMessage(string appName)
    {
        return $@"Agradecimentos Especiais

        Nós da Gamer Extremo queremos expressar nossa mais profunda gratidão a todos vocês que utilizaram o {appName}. 
        Este aplicativo foi desenvolvido com dedicação, estudo e cuidado para oferecer uma ferramenta simples e útil 
        no cálculo e planejamento de escadas.

        Agradecemos a cada usuário que confiou no nosso trabalho, testou o app e nos ajudou a melhorar com feedbacks 
        e sugestões. O apoio de vocês é o que nos motiva a continuar criando soluções práticas e acessíveis.

        Nosso sincero agradecimento também vai para todos que contribuíram direta ou indiretamente para o 
        desenvolvimento do {appName}. O conhecimento compartilhado e a colaboração foram fundamentais 
        para transformar esta ideia em realidade.

        Esperamos que o {appName} tenha sido útil no seu dia a dia, facilitando cálculos, economizando tempo 
        e trazendo mais praticidade para seus projetos.

        E lembrem-se: essa é apenas uma das ferramentas que queremos compartilhar com vocês. 
        Fiquem atentos, pois novidades e melhorias virão em breve!

        Com gratidão,

        Equipe Gamer Extremo";
    }

    //    // Gera a mensagem de agradecimento com o nome do jogo
    //    private string GenerateThanksMessage(string gameName)
    //    {
    //        return $@"Agradecimentos Especiais

    //Nós da Gamer Extremo queremos expressar nossa mais profunda gratidão a todos vocês que embarcaram conosco na incrível jornada de {gameName}. Este projeto foi construído com muito empenho, paixão e carinho, e sua participação foi fundamental para que ele se tornasse realidade.

    //Agradecemos a cada jogador que acreditou no nosso trabalho e se desafiou em um universo repleto de matemática e diversão. O seu apoio, feedback e entusiasmo nos motivaram a criar uma experiência única e envolvente.

    //Nosso sincero agradecimento também vai para todos que contribuíram direta ou indiretamente para o desenvolvimento de {gameName}. O talento e dedicação de cada colaborador foram essenciais para dar vida a este projeto.

    //Esperamos que tenham se divertido, aprendido e sentido a magia que colocamos em cada fase do jogo.

    //E lembrem-se: essa é apenas a primeira de muitas aventuras que queremos compartilhar com vocês. Fiquem atentos para novos desafios e surpresas no futuro!

    //Com muita gratidão,

    //Equipe Gamer Extremo";
    //    }

    // =========================================================================
    // MÉTODOS DE NAVEGAÇÃO
    // =========================================================================

    /// <summary>
    /// Método atualizado para voltar ao menu principal - preserva referências de áudio
    /// Versão moderna que gerencia corretamente as configurações
    /// </summary>
    public void BackToMainMenu()
    {
        audioPlayer.PlayButtonClick();

        // Salvar configurações antes de voltar
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveAudioSettings();
        }

        Debug.Log("Voltando para o menu principal...");

        // Usar o nome da cena do inspector ou padrão
        string targetScene = string.IsNullOrEmpty(nameScene) ? "EscadaApp" : nameScene;
        SceneManager.LoadScene(targetScene);
    }

    /// <summary>
    /// Volta para o menu principal - método legado mantido para compatibilidade
    /// chamado no clique do botão BackMainMenuButton da cena Thanks, passe o nome da cena alvo: EscadaApp
    /// </summary>
    public void BackMainMenu(string sceneName)
    {
        // Tocar som do botão
        audioPlayer.PlayButtonClick();

        // NOVO - Salvar configurações antes de voltar
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveAudioSettings();
        }

        // Validando o nome da cena
        if (sceneName == "" || sceneName != nameScene)
        {
            sceneName = nameScene; //Atenção ao nome da cena, o nome deve ser exato da cena alvo: EscadaApp
        }

        Debug.Log($"Carregando cena: {sceneName}");
        SceneManager.LoadScene(sceneName);

        Debug.Log("Lembre de add as cenas: Thanks, e outras no BuildIndex.");
    }

    // =========================================================================
    // MÉTODOS DE CONTROLE DE APLICAÇÃO
    // =========================================================================

    /// <summary>
    /// Fecha o aplicativo - chamado no clique do botão QuitButton da cena Thanks
    /// </summary>
    public void ExitGame()
    {
        // Tocar som do botão
        audioPlayer.PlayButtonClick();

        // NOVO - Salvar configurações antes de sair
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveAudioSettings();
        }

        // Exibe a mensagem no console
        Debug.Log("Fechando aplicação...");

        // Parar o jogo em builds
        Application.Quit();

        // No Editor do Unity, parar o modo Play
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Debug.Log("Quit.");
    }

    // =========================================================================
    // MÉTODOS DE LIMPEZA (MANTIDOS PARA COMPATIBILIDADE)
    // =========================================================================

    //public void ClaearData()//chamar no clique dos botões
    //{

    //    TangramPieceDataManager.instance.TangramDeleteDataFile();

    //    Debug.Log("Thanks ClaearData()");
    //}

    /*
     * 
     * MENSAGEM DE AGRADECIMENTO NO FINAL DO JOGO
        Agradecimentos Especiais

        Nós da Gamer Extremo queremos expressar nossa mais profunda gratidão a todos vocês que embarcaram conosco na incrível jornada de Math Dash. Este projeto foi construído com muito empenho, paixão e carinho, e sua participação foi fundamental para que ele se tornasse realidade.

        Agradecemos a cada jogador que acreditou no nosso trabalho e se desafiou em um universo repleto de matemática e diversão. O seu apoio, feedback e entusiasmo nos motivaram a criar uma experiência única e envolvente.

        Nosso sincero agradecimento também vai para todos que contribuíram direta ou indiretamente para o desenvolvimento de Math Dash. O talento e dedicação de cada colaborador foram essenciais para dar vida a este projeto.

        Esperamos que tenham se divertido, aprendido e sentido a magia que colocamos em cada fase do jogo.

        E lembrem-se: essa é apenas a primeira de muitas aventuras que queremos compartilhar com vocês. Fiquem atentos para novos desafios e surpresas no futuro!

        Com muita gratidão,

        Equipe Gamer Extremo
     */

}
