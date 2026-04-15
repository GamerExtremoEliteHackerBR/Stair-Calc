using UnityEngine;

/// <summary>
/// Componente auxiliar para reprodução facilitada de efeitos sonoros
/// Vai nos objetos que tocam o som, OU ATÉ MESMO NO objeto AudioManager
/// SE QUISER NÃO PRECISA ADD EM NENHUM OBJETO.
/// Vou ADD no objeto AudioManager da cena EscadaApp e configurar o OnClick() para chamar AudioPlayer.PlayButtonClick() 
/// 
/// Também posso fazer assim:
/// Configure os botões:
/// Em cada botão, adicione o componente AudioPlayer
/// Ou configure o OnClick() para chamar AudioPlayer.PlayButtonClick()
/// </summary>
public class AudioPlayer : MonoBehaviour
{

    // =========================================================================
    // MÉTODOS DE CICLO DE VIDA
    // =========================================================================

    /// <summary>
    /// Inicialização do componente
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Intencionalmente vazio - inicialização pode ser feita sob demanda

    }

    /// <summary>
    /// Atualização a cada frame
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        // Intencionalmente vazio - componente reativo a eventos

    }

    // =========================================================================
    // MÉTODOS DE REPRODUÇÃO DE SOM
    // =========================================================================

    /// <summary>
    /// Reproduz um som específico pelo nome
    /// </summary>
    /// <param name="soundName">Nome do som definido no AudioManager</param>
    public void PlaySound(string soundName)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.Play(soundName);
        }
        else
        {
            Debug.LogWarning("AudioManager não encontrado!");
        }
    }


    // =========================================================================
    // MÉTODOS DE CONVENIÊNCIA - SONS ESPECÍFICOS
    // =========================================================================

    /// <summary>
    /// Reproduz som padrão de clique em botão
    /// Método específico para sons de clique em botões
    /// </summary>
    public void PlayButtonClick()
    {
        PlaySound("ButtonClick");
    }

    /// <summary>
    /// Reproduz som de cálculo concluído
    /// Método específico para som de cálculo
    /// </summary>
    public void PlayCalculateSound()
    {
        PlaySound("Calculate");
    }

}
