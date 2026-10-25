using UnityEngine;
using UnityEngine.Audio;

public enum AudioName
{
    BGM,

    //SFX underneath
    SHOOT,
    EXPLODE,
    BUTTON
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource musicSource, sfxSource;
    [SerializeField] private AudioClip[] audioList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = audioList[(int)AudioName.BGM];
        musicSource.Play();
    }

    public void PlaySfx(AudioName au, float volume = 1)
    {
        sfxSource.PlayOneShot(audioList[(int)au], volume);
    }

    public void SetVolume(MixerParameter parameter, float volume)
    {
        mixer.SetFloat(parameter.ToString(), ToDecibel(volume));
    }

    private static float ToDecibel(float volume)
    {
        // A Magic Formula
        // https://en.wikipedia.org/wiki/Decibel#Acoustics
        // https://youtu.be/V_Bf__ynKLE
        return Mathf.Max(19.9316f * Mathf.Log10(volume), -80f);
    }
}

public enum MixerParameter
{
    MasterVolume,
    MusicVolume,
    SFXVolume
}