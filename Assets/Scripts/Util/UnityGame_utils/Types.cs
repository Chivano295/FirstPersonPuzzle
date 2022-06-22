using System;
using UnityEngine;
using UnityEngine.Audio;

namespace UnityUtils
{
    [Serializable]
    public class Sound
    {
        private static int autoId = 0;

        public AudioClip clip;

        public int ID;
        public string Name;
        [Range(0f, 1f)]
        public float Volume = 0.5f;
        [Range(.1f, 3f)]
        public float Pitch = 1;

        [HideInInspector]
        public AudioSource Source;

        public bool PlayOnAwake;
        public bool Loop;
        public float SpacialBlend;
        public AudioMixerGroup MixerGroup;

        public Sound()
        {
            this.ID = autoId;
            autoId++;
        }
    }
}