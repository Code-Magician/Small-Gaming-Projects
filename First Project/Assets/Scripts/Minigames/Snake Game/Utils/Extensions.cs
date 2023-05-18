using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Extensions
    {
       
        public static T RandomElement<T>(this IList<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        
        public static AudioSource AddAudio(this GameObject obj, AudioClip clip, bool loop, bool playAwake, float volume)
        {
            var newAudio = obj.AddComponent<AudioSource>();
            newAudio.clip = clip;
            newAudio.loop = loop;
            newAudio.playOnAwake = playAwake;
            newAudio.volume = volume;

            return newAudio;
        }
    }
}
