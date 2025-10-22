using System;
using _RaghuvanshAgarwal.Modules.Counters.Cutting.Scripts;
using _RaghuvanshAgarwal.Modules.Counters.Delivery.Scripts;
using _RaghuvanshAgarwal.Modules.Counters.Scripts;
using _RaghuvanshAgarwal.Modules.Counters.Trash;
using _RaghuvanshAgarwal.Modules.Delivery;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _RaghuvanshAgarwal.Modules.Audio.Sound {
    public class SoundManager : MonoBehaviour
    {
        private const string PLAYER_PREF_SOUND_VOLUME_KEY = "PLAYER_PREF_SOUND_VOLUME_KEY";
        public static SoundManager Instance {get; private set;}
        
        [SerializeField] SoundRefSO soundRef;
        public float SoundVolume { get; private set; } = 1f;
        

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            SoundVolume =  PlayerPrefs.GetFloat(PLAYER_PREF_SOUND_VOLUME_KEY, 1f);
            DeliveryManager.Instance.OnCorrectRecipeDelivered += DeliveryManager_OnCorrectRecipeDelivered;
            DeliveryManager.Instance.OnWrongRecipeDelivered += DeliveryManager_OnWrongRecipeDelivered;
            
            CuttingCounter.OnAnyCuttingActionPerformed += CuttingCounter_OnAnyCuttingActionPerformed;
            
            Player.Scripts.Player.Instance.OnPickedSomething += Player_OnPickedSomething;
            
            BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
            
            TrashCounter.OnAnyTrashDropped += TrashCounter_OnAnyTrashDropped;
        }

        


        private void OnDestroy() {
            DeliveryManager.Instance.OnCorrectRecipeDelivered -= DeliveryManager_OnCorrectRecipeDelivered;
            DeliveryManager.Instance.OnWrongRecipeDelivered -= DeliveryManager_OnWrongRecipeDelivered;
            
            CuttingCounter.OnAnyCuttingActionPerformed -= CuttingCounter_OnAnyCuttingActionPerformed;
            
            Player.Scripts.Player.Instance.OnPickedSomething -= Player_OnPickedSomething;
            
            BaseCounter.OnAnyObjectPlacedHere  -= BaseCounter_OnAnyObjectPlacedHere;
            
            TrashCounter.OnAnyTrashDropped += TrashCounter_OnAnyTrashDropped;
        }

        private void DeliveryManager_OnWrongRecipeDelivered(object sender, EventArgs e) {
            Vector3 position = DeliveryCounter.Instance.transform.position;
            PlaySound(soundRef.deliveryFails, position);
        }

        private void DeliveryManager_OnCorrectRecipeDelivered(object sender, EventArgs e) {
            Vector3 position = DeliveryCounter.Instance.transform.position;
            PlaySound(soundRef.deliverySuccesses, position);
        }
        
        private void CuttingCounter_OnAnyCuttingActionPerformed(object sender, EventArgs e) {
            CuttingCounter counter = sender as CuttingCounter;
            PlaySound(soundRef.chops, counter!.transform.position);
        }
        
        private void Player_OnPickedSomething(object sender, EventArgs e) {
            Player.Scripts.Player player = sender as Player.Scripts.Player;
            PlaySound(soundRef.objectPickups, player!.transform.position);
        }
        
        private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e) {
            BaseCounter baseCounter = sender as BaseCounter;
            PlaySound(soundRef.objectDrops, baseCounter!.transform.position);
        }
        
        private void TrashCounter_OnAnyTrashDropped(object sender, EventArgs e) {
            TrashCounter trashCounter = sender as TrashCounter;
            PlaySound(soundRef.trash, trashCounter!.transform.position);
        }
        
        private void PlaySound(AudioClip[] clips, Vector3 position, float volume = 1f) {
            PlaySound(clips[Random.Range(0,clips.Length)], position, volume);
        }
        
        private void PlaySound(AudioClip clip, Vector3 position, float volumeMultiplier = 1f) {
            AudioSource.PlayClipAtPoint(clip, position, SoundVolume * volumeMultiplier);
        }

        public void PlayFootstepSounds(Vector3 transformPosition, float volume) {
            PlaySound(soundRef.footsteps, transformPosition, volume);
        }

        public void ChangeVolume() {
            SoundVolume += 0.1f;
            if (SoundVolume > 1f) {
                SoundVolume = 0f;
            }
            PlayerPrefs.SetFloat(PLAYER_PREF_SOUND_VOLUME_KEY, SoundVolume);
        }
    }
}
