using System;
using UnityEngine;

namespace UnityUtils
{
    [Serializable]
    public class Sound
    {
        public AudioClip clip;

        public int ID;
        public string name;
        [Range(0f, 1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        [HideInInspector]
        public AudioSource source;

        public bool loop;
        public float spacialBlend;
    }
}