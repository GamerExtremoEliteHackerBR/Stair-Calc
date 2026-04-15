using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThanksManager2 : MonoBehaviour
{
    [Header("Referências UI")]
    public TMP_Text thanksText;
    public GameObject backMainMenuButton;
    public GameObject quitButton;

    private AudioPlayer audioPlayer;

    void Start()
    {
        audioPlayer = gameObject.AddComponent<AudioPlayer>();

        if (thanksText != null)
        {
            thanksText.text = "Obrigado por usar nosso aplicativo!";
        }

        Debug.Log("ThanksManager inicializado");
    }



    void Update()
    {
        // Voltar com ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
    }

    public void BackToMainMenu()
    {
        audioPlayer.PlayButtonClick();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveAudioSettings();
        }

        Debug.Log("Voltando para o menu principal...");
        SceneManager.LoadScene("EscadaApp");
    }

    public void QuitApplication()
    {
        audioPlayer.PlayButtonClick();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SaveAudioSettings();
        }

        Debug.Log("Saindo do aplicativo...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }



}
