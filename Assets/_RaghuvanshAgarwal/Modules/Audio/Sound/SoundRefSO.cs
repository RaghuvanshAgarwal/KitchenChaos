using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Audio.Sound {
    [CreateAssetMenu(fileName = "Sound Ref", menuName = "RaghuvanshAgarwal/SoundRef", order = 0)]
    public class SoundRefSO : ScriptableObject {
        public AudioClip[] chops;
        public AudioClip[] deliveryFails;
        public AudioClip[] deliverySuccesses;
        public AudioClip[] footsteps;
        public AudioClip[] objectDrops;
        public AudioClip[] objectPickups;
        public AudioClip panSizzle;
        public AudioClip[] trash;
        public AudioClip[] warning;
        
    }
}