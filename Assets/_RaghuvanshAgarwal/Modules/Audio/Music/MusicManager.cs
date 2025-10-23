using System;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Audio.Music {
    public class MusicManager : MonoBehaviour {
        private const string PlayerPrefMusicVolumeKey = "PLAYER_MUSIC_VOLUME_KEY";
        public static MusicManager Instance {get; private set;}
        [SerializeField] private AudioSource audioSource;
        public float MusicVolume { get; private set; } = 0.3f;

        private void Awake() {
            Instance = this;
            MusicVolume = PlayerPrefs.GetFloat(PlayerPrefMusicVolumeKey, 0.3f);
            audioSource.volume = MusicVolume;
        }

        public void ChangeVolume() {
            MusicVolume += 0.1f;
            if (MusicVolume > 1f) {
                MusicVolume = 0f;
            }
            audioSource.volume = MusicVolume;
            PlayerPrefs.SetFloat(PlayerPrefMusicVolumeKey, MusicVolume);
        }
    }
}
