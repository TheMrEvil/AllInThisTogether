using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x0200039F RID: 927
	public class AudioControl : MonoBehaviour
	{
		// Token: 0x06001EA3 RID: 7843 RVA: 0x000B7318 File Offset: 0x000B5518
		private void Start()
		{
			this.controller = base.GetComponent<Controller>();
			this.animator = base.GetComponentInChildren<Animator>();
			this.mover = base.GetComponent<Mover>();
			this.tr = base.transform;
			Controller controller = this.controller;
			controller.OnLand = (Action<Vector3, Vector3, Vector3>)Delegate.Combine(controller.OnLand, new Action<Vector3, Vector3, Vector3>(this.OnLand));
			Controller controller2 = this.controller;
			controller2.OnJump = (Controller.VectorEvent)Delegate.Combine(controller2.OnJump, new Controller.VectorEvent(this.OnJump));
			if (!this.animator)
			{
				this.useAnimationBasedFootsteps = false;
			}
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x000B73B8 File Offset: 0x000B55B8
		private void Update()
		{
			this.FootStepUpdate(VectorMath.RemoveDotVector(this.controller.GetVelocity(), this.tr.up).magnitude);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x000B73F0 File Offset: 0x000B55F0
		private void FootStepUpdate(float _movementSpeed)
		{
			float num = 0.05f;
			if (this.useAnimationBasedFootsteps)
			{
				float @float = this.animator.GetFloat("FootStep");
				if (((this.currentFootStepValue <= 0f && @float > 0f) || (this.currentFootStepValue >= 0f && @float < 0f)) && this.mover.IsGrounded() && _movementSpeed > num)
				{
					this.PlayFootstepSound(_movementSpeed);
				}
				this.currentFootStepValue = @float;
				return;
			}
			this.currentFootstepDistance += Time.deltaTime * _movementSpeed;
			if (this.currentFootstepDistance > this.footstepDistance)
			{
				if (this.mover.IsGrounded() && _movementSpeed > num)
				{
					this.PlayFootstepSound(_movementSpeed);
				}
				this.currentFootstepDistance = 0f;
			}
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x000B74AC File Offset: 0x000B56AC
		private void PlayFootstepSound(float _movementSpeed)
		{
			int num = UnityEngine.Random.Range(0, this.footStepClips.Length);
			this.audioSource.PlayOneShot(this.footStepClips[num], this.audioClipVolume + this.audioClipVolume * UnityEngine.Random.Range(-this.relativeRandomizedVolumeRange, this.relativeRandomizedVolumeRange));
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000B74FB File Offset: 0x000B56FB
		private void OnLand(Vector3 momentum, Vector3 point, Vector3 surfaceNormal)
		{
			if (VectorMath.GetDotProduct(momentum, this.tr.up) > -this.landVelocityThreshold)
			{
				return;
			}
			this.audioSource.PlayOneShot(this.landClip, this.audioClipVolume);
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x000B752F File Offset: 0x000B572F
		private void OnJump(Vector3 _v)
		{
			this.audioSource.PlayOneShot(this.jumpClip, this.audioClipVolume);
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000B7548 File Offset: 0x000B5748
		public AudioControl()
		{
		}

		// Token: 0x04001EDB RID: 7899
		private Controller controller;

		// Token: 0x04001EDC RID: 7900
		private Animator animator;

		// Token: 0x04001EDD RID: 7901
		private Mover mover;

		// Token: 0x04001EDE RID: 7902
		private Transform tr;

		// Token: 0x04001EDF RID: 7903
		public AudioSource audioSource;

		// Token: 0x04001EE0 RID: 7904
		public bool useAnimationBasedFootsteps = true;

		// Token: 0x04001EE1 RID: 7905
		public float landVelocityThreshold = 5f;

		// Token: 0x04001EE2 RID: 7906
		public float footstepDistance = 0.2f;

		// Token: 0x04001EE3 RID: 7907
		private float currentFootstepDistance;

		// Token: 0x04001EE4 RID: 7908
		private float currentFootStepValue;

		// Token: 0x04001EE5 RID: 7909
		[Range(0f, 1f)]
		public float audioClipVolume = 0.1f;

		// Token: 0x04001EE6 RID: 7910
		public float relativeRandomizedVolumeRange = 0.2f;

		// Token: 0x04001EE7 RID: 7911
		public AudioClip[] footStepClips;

		// Token: 0x04001EE8 RID: 7912
		public AudioClip jumpClip;

		// Token: 0x04001EE9 RID: 7913
		public AudioClip landClip;
	}
}
