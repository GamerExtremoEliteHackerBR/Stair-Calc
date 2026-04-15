using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject audioSettingsPanel;
    //public Button btnPanelConfig;
    public Button btnBackMainMenu;

    [SerializeField] private string nameScene; // Aqui vai o nome da minha cena principal EscadaApp

    ////3. Alternativa se você quiser manter o texto no Inspector:
    ////Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:
    //[SerializeField] private Text thanksText;
    //// Ou
    //[SerializeField] private string thanksMessageTemplate;
    ////3. Alternativa se você quiser manter o texto no Inspector:
    ////Se preferir configurar o texto diretamente no Inspector, você pode fazer assim:

    public List<string> namesGameScenes = new List<string>();

    // Componente de áudio
    private AudioPlayer audioPlayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Inicializar componente de áudio
        audioPlayer = gameObject.AddComponent<AudioPlayer>();


        //namesGameScenes.Contains(nameScene);
        //Debug.Log("Cenas: " + namesGameScenes.Contains(nameScene));
        //Debug.Log("Teste: " + namesGameScenes.Count);
        //Debug.Log("Teste4: " + SceneManager.GetAllScenes());
        //Debug.Log("Teste4: " + SceneManager.sceneCount);


        // Exemplo: pega o buildIndex atual
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        // Pega o nome da cena pelo buildIndex
        string currentSceneName = SceneManager.GetSceneByBuildIndex(buildIndex).name;

        Debug.Log($"BuildIndex: {buildIndex} | SceneName: {currentSceneName}");


        //Descomentar para garantir o nome correto da cena, Comentei só pra testes
        // Validando o nome da cena
        if (nameScene == "" || nameScene != currentSceneName)
        {
            //nameScene = "EscadaApp"; // Esse é o nome da cena que quero abir, esse script vai no objeto ThanksManager da cena Thanks
        }


        // Atualiza o texto de agradecimento com o nome do jogo
        //UpdateThanksText();

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
                BackMainMenu(nameScene);//Aqui, volta pra cena EscadaApp
            });
            Debug.Log("ATENÇÃO AO NOME DA CENA.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackMainMenu(string sceneName)//chamado no clique do botão BackMainMenuButton da cena Thanks, passe o nome da cena alvo: EscadaApp
    {
        // Tocar som do botão
        audioPlayer.PlayButtonClick();

        SceneManager.LoadScene(sceneName);


        if (sceneName == "" || sceneName != nameScene)
        {
            sceneName = nameScene; //Atenção ao nome da cena, o nome deve ser exato da cena alvo: EscadaApp
            SceneManager.LoadScene(sceneName);
        }

        Debug.Log("Lembre de add as cenas: Thanks, e outras no BuildIndex.");
    }

    public void ExitGame()//chamado no clique do botão QuitButton da cena Thanks
    {
        // Tocar som do botão
        audioPlayer.PlayButtonClick();

        // Exibe a mensagem no console
        Debug.Log("Fechando aplicação...");

        // Para o jogo em builds
        Application.Quit();

        // No Editor do Unity, parar o modo Play
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Debug.Log("Quit.");
    }


}
