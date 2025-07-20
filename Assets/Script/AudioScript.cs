using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioScript : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip NextPlay_clip;
    static bool Audio_Loaded = false;
    bool Function_BGM_VolumeDown = false;
    const float Down_Value = 0.1f;
    float CurrentVolume;
    float CurrentPitch;

    // Start is called before the first frame update
    void Awake()
    {
        //重複防止処理
        if (Audio_Loaded == true)
        {
            Destroy(this.gameObject);
        }
        Audio_Loaded = true;
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        CurrentVolume = audioSource.volume;
        CurrentPitch = audioSource.pitch;
    }

    void Update()
    {
        if (Function_BGM_VolumeDown == true)
        {
            if (Return_AudioPlaying() == true)
            {
                Fadeout_AudioVolume();
            }
            else
            {
                Function_BGM_VolumeDown = false;
                Change_PlayAudio(NextPlay_clip);
            }
        }
    }

    //音楽再生
    public void PlayAudio(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

    //音楽をすぐに変更する
    public void Change_PlayAudio(AudioClip audio)
    {
        if (audioSource.clip == audio) return;
        if (Return_AudioPlaying() == true) Stop_Audio();
        audioSource.clip = audio;
        audioSource.Play();
        Debug.Log("Audio Changed");
    }

    //音楽をフェードアウトしてから変更する
    public void Change_PlayAudio_with_VolumeDown(AudioClip audio)
    {
        if (audioSource.clip == audio || Function_BGM_VolumeDown == true) return;
        NextPlay_clip = audio;
        Function_BGM_VolumeDown = true;
    }

    //効果音再生用
    public void OneShot_Play(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    //音楽の再生を停止
    public void Stop_Audio()
    {
        Function_BGM_VolumeDown = false;
        audioSource.Stop();
        Reset_AudioValue();
    }

    //パラメータを初期化
    public void Reset_AudioValue()
    {
        audioSource.volume = CurrentVolume;
        audioSource.pitch = CurrentPitch;
    }

    //任意のパラメータを設定（ただし、エフェクト実行中は無視）
    public void Set_AudioParameter(float Volume)
    {
        if (Function_BGM_VolumeDown == true) return;
        audioSource.volume = Volume;
    }
    public void Set_AudioParameter(float Volume, float Pitch)
    {
        if (Function_BGM_VolumeDown == true) return;
        audioSource.volume = Volume;
        audioSource.pitch = Pitch;
    }

    //フェードアウト処理
    public void Fadeout_AudioVolume()
    {
        if (audioSource.volume > 0)
        {
            audioSource.volume -= Down_Value * Time.deltaTime;
            if (audioSource.volume <= 0)
            {
                Stop_Audio();
                Reset_AudioValue();
            }
        }
    }

    public void Fadeout_AudioPitch()
    {
        if (audioSource.pitch > 0)
        {
            audioSource.pitch -= Down_Value * Time.deltaTime;
            if (audioSource.pitch <= 0)
            {
                Stop_Audio();
                Reset_AudioValue();
            }
        }
    }

    //再生中かを判定
    public bool Return_AudioPlaying() { return audioSource.isPlaying; }
}
