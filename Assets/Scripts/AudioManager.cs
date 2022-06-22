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

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
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
}
