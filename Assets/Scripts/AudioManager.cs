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
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.playOnAwake = false;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;   
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }


    //Play the sound specified
    public void Play(string name)
    {   
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound with name {name} is not found! Did you made a typo?");
            return;
        }
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound with name {name} is not found! Did you made a typo?");
            return;
        }
        if (!s.source.isPlaying)
        {
            Debug.LogWarning($"Sound with name {name} is not playing!");
            return;
        }
        s.source.Pause();
    }
    public void Resume(string name) 
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound with name {name} is not found! Did you made a typo?");
            return;
        }

        s.source.UnPause();
    }

    public AudioClip GetClip(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound with name {name} is not found! Did you made a typo?");
            return null;
        }

        return s.clip;
    }
}
