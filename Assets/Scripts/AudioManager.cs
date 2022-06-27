using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

//Mini-library
using UnityUtils;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public List<Sound> sounds;
    public float fadeOutDuration;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {

            Destroy(this);
        }
        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.clip;
            s.Source.playOnAwake = s.PlayOnAwake;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.outputAudioMixerGroup = s.MixerGroup;
            if (s.PlayOnAwake)
            {
                s.Source.Play();
            }
        }
    }


    //Play the sound specified
    public void Play(string name)
    {
        Sound s = sounds.Find(sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound with name {name} is not found! Did you made a typo?");
            return;
        }
        s.Source.Play();
    }

    public void Pause(string name)
    {
        Sound s = sounds.Find(sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound with name {name} is not found! Did you made a typo?");
            return;
        }
        if (!s.Source.isPlaying)
        {
            Debug.LogWarning($"Sound with name {name} is not playing!");
            return;
        }
        s.Source.Pause();
    }
    public void Resume(string name)
    {
        Sound s = sounds.Find(sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound with name {name} is not found! Did you made a typo?");
            return;
        }

        s.Source.UnPause();
    }

    public AudioClip GetClip(string name)
    {
        Sound s = sounds.Find(sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound with name {name} is not found! Did you made a typo?");
            return null;
        }

        return s.clip;
    }
    public AudioSource GetSource(string name)
    {
        Sound s = sounds.Find(sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound with name {name} is not found! Did you made a typo?");
            return null;
        }

        return s.Source;
    }

    public void FadeOut(string name)
    {
        StartCoroutine(FadeOutCurrentAudio(GetSource(name)));
    }

    IEnumerator FadeOutCurrentAudio(AudioSource source)
    {
        float currentTime = 0;
        float start = source.volume;
        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, 0, currentTime / fadeOutDuration);
            yield return null;
        }
        source.Stop();
        source.volume = start;
        yield break;
    }
}
