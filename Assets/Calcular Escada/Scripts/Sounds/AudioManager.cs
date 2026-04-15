using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;


/// <summary>
/// Vai no objeto AudioManager da cena EscadaApp
/// AJUSTE OS VALORES NO INSPECTOR, O PTCH DEVE SER 1 NO MÍNIMO
/// </summary>
public class AudioManager : MonoBehaviour
{

    // =========================================================================
    // SINGLETON PATTERN
    // =========================================================================

    /// <summary>
    /// Instância singleton do AudioManager - acesso global
    /// </summary>
    public static AudioManager Instance;

    // =========================================================================
    // CONFIGURAÇÕES DE ÁUDIO
    // =========================================================================

    [Header("Configurações de Áudio")]
    public AudioMixer audioMixer;
    public Sound[] sounds;

    // =========================================================================
    // CONSTANTES E PARÂMETROS
    // =========================================================================
    /// <summary>
    /// Parâmetros do AudioMixer para controle de volume.
    /// Os nomes devem corresponder aos parâmetros definidos no AudioMixer.
    /// Os parametros são: MasterVolume, MusicVolume, SFXVolume e devem 
    /// estar expostos no AudioMixer para que possam ser controlados via código.
    /// 
    /// Para expor os parametro é só clicar em um filho do AudioMixer (Master, Music, SFX)
    /// e depois no Inspector clicar em uma propriedade (Volume) com o botão direito e escolher
    /// Expose 'Volume' to script. Vodê pode expor a propriedade que quiser, aqui só estou usando Volume
    /// dos 3 parâmetros que criei.
    /// 
    /// AO EXPOR OS PARAMÊTROS NO AUDIOMIXER, LEMBRE DE NOMEAR IGUAL AQUI!
    /// </summary>
    private const string MASTER_VOLUME_PARAM = "MasterVolume";
    private const string MUSIC_VOLUME_PARAM = "MusicVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";

    /// <summary>
    /// Controla estado de mute do áudio mestre
    /// </summary>
    private bool isMuted = false;

    // =========================================================================
    // DEFINIÇÃO DA CLASSE SOUND
    // =========================================================================

    /// <summary>
    /// Estrutura que define um som no sistema de áudio
    /// </summary
    [System.Serializable]
    public class Sound
    {
        [Tooltip("Nome único do som (usado para referência)")]
        public string name;

        [Tooltip("Clip de áudio a ser reproduzido")]
        public AudioClip clip;

        [Tooltip("Volume do som (0-1)")]
        [Range(0f, 1f)]
        public float volume = 1f;

        [Tooltip("Pitch do som (0.1-3) - DEVE SER 1 NO MÍNIMO para sons normais")]
        [Range(0.1f, 3f)]
        public float pitch = 1f;

        [Tooltip("Se o som deve loopar automaticamente")]
        public bool loop = false;

        [Tooltip("Se o som deve tocar automaticamente ao iniciar")]
        public bool playOnAwake = false;

        [HideInInspector] // Evita que o campo apareça no inspector
        public AudioSource source;// Referência ao AudioSource criado em tempo de execução
    }

    // =========================================================================
    // MÉTODOS DE INICIALIZAÇÃO
    // =========================================================================

    /// <summary>
    /// Inicializa o singleton e configura todos os sons
    /// Chamado antes de Start quando o objeto é criado
    /// </summary>
    void Awake()
    {
        InitializeSingleton();
        InitializeAudioSources();
        LoadAudioSettings();

        /**
        ////Era assim antes; início
        //// É uma boa prática separar a inicialização em métodos distintos
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //foreach (Sound s in sounds)
        //{
        //    s.source = gameObject.AddComponent<AudioSource>();
        //    s.source.clip = s.clip;
        //    s.source.volume = s.volume;
        //    s.source.pitch = s.pitch;
        //    s.source.loop = s.loop;
        //    s.source.playOnAwake = s.playOnAwake;

        //    if (s.name == "BackgroundMusic")
        //    {
        //        s.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        //    }
        //    else
        //    {
        //        s.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        //    }
        //}
        
        //LoadAudioSettings();
        ////Era assim antes; fim
        */


    }


    /// <summary>
    /// Configura o padrão singleton - garante apenas uma instância
    /// </summary>
    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Cria e configura AudioSources para todos os sons definidos
    /// </summary>
    private void InitializeAudioSources()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;

            ConfigureAudioMixerGroups(s);
        }
    }


    /// <summary>
    /// Configura grupos do AudioMixer baseado no tipo de som
    /// </summary>
    private void ConfigureAudioMixerGroups(Sound s)
    {
        if (s.name == "BackgroundMusic")
        {
            s.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
        }
        else
        {
            s.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        }
    }

    /// <summary>
    /// Inicia música de fundo automaticamente
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Play("BackgroundMusic");
    }

    // =========================================================================
    // MÉTODOS DE CONTROLE DE REPRODUÇÃO
    // =========================================================================

    /// <summary>
    /// Reproduz um som pelo nome
    /// </summary>
    /// <param name="soundName">Nome do som definido no inspector</param>
    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Som não encontrado: " + soundName);
            return;
        }
        s.source.Play();
    }

    /// <summary>
    /// Para a reprodução de um som pelo nome
    /// </summary>
    /// <param name="soundName">Nome do som definido no inspector</param>
    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Som não encontrado: " + soundName);
            return;
        }
        s.source.Stop();
    }


    // =========================================================================
    // MÉTODOS DE CONTROLE DE VOLUME
    // =========================================================================

    /// <summary>
    /// Define volume do áudio mestre (0-1)
    /// Respeita estado de mute - só aplica se não estiver mudo
    /// </summary>
    public void SetMasterVolume(float volume)
    {
        if (!isMuted)
        {
            float volumeDB = ConvertToDecibel(volume);
            audioMixer.SetFloat(MASTER_VOLUME_PARAM, volumeDB);
        }
        PlayerPrefs.SetFloat(MASTER_VOLUME_PARAM, volume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Define volume da música (0-1)
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        float volumeDB = ConvertToDecibel(volume);
        audioMixer.SetFloat(MUSIC_VOLUME_PARAM, volumeDB);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PARAM, volume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Define volume dos efeitos sonoros (0-1)
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        float volumeDB = ConvertToDecibel(volume);
        audioMixer.SetFloat(SFX_VOLUME_PARAM, volumeDB);
        PlayerPrefs.SetFloat(SFX_VOLUME_PARAM, volume);
        PlayerPrefs.Save();
    }


    // =========================================================================
    // MÉTODOS DE CONSULTA DE VOLUME
    // =========================================================================

    /// <summary>
    /// Obtém volume atual do áudio mestre
    /// </summary>
    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_PARAM, 0.8f);
    }

    /// <summary>
    /// Obtém volume atual da música
    /// </summary>
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME_PARAM, 0.7f);
    }


    /// <summary>
    /// Obtém volume atual dos efeitos sonoros
    /// </summary>
    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME_PARAM, 0.9f);
    }

    // =========================================================================
    // MÉTODOS DE CONVERSÃO E CÁLCULO
    // =========================================================================

    /// <summary>
    /// Converte valor linear (0-1) para escala logarítmica em decibéis (-80 a 0)
    /// Usado pelo AudioMixer que trabalha com escala dB
    /// </summary>
    private float ConvertToDecibel(float volume)
    {
        if (volume <= 0.0001f)
            return -80f;
        return Mathf.Log10(volume) * 20f;
    }

    // =========================================================================
    // MÉTODOS DE PERSISTÊNCIA E CARREGAMENTO
    // =========================================================================

    /// <summary>
    /// Carrega configurações salvas do PlayerPrefs
    /// </summary>
    private void LoadAudioSettings()
    {
        isMuted = PlayerPrefs.GetInt("MasterMuted", 0) == 1;

        if (isMuted)
        {
            audioMixer.SetFloat(MASTER_VOLUME_PARAM, -80f);
        }
        else
        {
            SetMasterVolume(GetMasterVolume());
        }

        SetMusicVolume(GetMusicVolume());
        SetSFXVolume(GetSFXVolume());
    }

    /// <summary>
    /// Salva todas as configurações atuais no PlayerPrefs
    /// </summary>
    public void SaveAudioSettings()
    {
        PlayerPrefs.Save();
    }

    // =========================================================================
    // MÉTODOS DE CONTROLE DE MUTE
    // =========================================================================

    /// <summary>
    /// Alterna estado de mute do áudio mestre
    /// </summary>
    public void ToggleMasterMute(bool isMuted)
    {
        Debug.Log("Mute toggled: " + isMuted);
        this.isMuted = isMuted;

        if (isMuted)
        {
            audioMixer.SetFloat(MASTER_VOLUME_PARAM, -80f);
            PlayerPrefs.SetInt("MasterMuted", 1);
        }
        else
        {
            float currentVolume = GetMasterVolume();
            float volumeDB = ConvertToDecibel(currentVolume);
            audioMixer.SetFloat(MASTER_VOLUME_PARAM, volumeDB);
            PlayerPrefs.SetInt("MasterMuted", 0);
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Verifica se o áudio mestre está mudo
    /// </summary>
    public bool IsMasterMuted()
    {
        return PlayerPrefs.GetInt("MasterMuted", 0) == 1;
    }



}
