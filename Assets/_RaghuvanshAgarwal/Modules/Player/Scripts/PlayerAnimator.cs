using System;
using UnityEngine;

namespace _RaghuvanshAgarwal.Modules.Player.Scripts {
	public class PlayerAnimator : MonoBehaviour {

		[SerializeField] private Player player;
		
		private static readonly int IsWalking = Animator.StringToHash("IsWalking");
		private Animator _animator;

		private void Awake() {
			_animator = GetComponent<Animator>();
		}

		private void Update() {
			_animator.SetBool(IsWalking, player.IsWalking());
		}
	}
}
