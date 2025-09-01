using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200001F RID: 31
	public class MagicFX5_SkinnedMeshBonesAtractor : MonoBehaviour
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00005938 File Offset: 0x00003B38
		private void OnEnable()
		{
			this._leftTime = 0f;
			this._isFinished = false;
			this._stateChanged = false;
			MagicFX5_EffectSettings effectSettings = this.EffectSettings;
			effectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(effectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnCollisionImpactEnter));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005988 File Offset: 0x00003B88
		private void OnDisable()
		{
			MagicFX5_EffectSettings effectSettings = this.EffectSettings;
			effectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Remove(effectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnCollisionImpactEnter));
			this.SetSkinState(true);
			this._bones.Clear();
			this._skins.Clear();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000059DC File Offset: 0x00003BDC
		public void OnCollisionImpactEnter(MagicFX5_EffectSettings.EffectCollisionHit hitInfo)
		{
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in hitInfo.Target.GetComponentsInChildren<SkinnedMeshRenderer>())
			{
				foreach (Transform transform in skinnedMeshRenderer.rootBone.GetComponentsInChildren<Transform>())
				{
					if (!(transform == skinnedMeshRenderer.rootBone))
					{
						this._bones.Add(new MagicFX5_SkinnedMeshBonesAtractor.VirtualBone(transform));
					}
				}
				this._skins.Add(skinnedMeshRenderer);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005A58 File Offset: 0x00003C58
		private void LateUpdate()
		{
			if (this._isFinished)
			{
				return;
			}
			float deltaTime = Time.deltaTime;
			this._leftTime += deltaTime;
			if (this._leftTime < this.StartDelay)
			{
				return;
			}
			if (!this._stateChanged)
			{
				this._stateChanged = true;
			}
			Vector3 target = this.OverrideTransform ? this.OverrideTransform.position : base.transform.position;
			Mathf.Clamp01((this._leftTime - this.StartDelay) / this.Duration);
			float num = this.AttractionStrength.Evaluate((this._leftTime - this.StartDelay) / this.Duration) * this.StrengthMultiplier;
			for (int i = 0; i < this._bones.Count; i++)
			{
				Vector3 pos = Vector3.MoveTowards(this._bones[i].GetBonePosition(), target, deltaTime * num);
				this._bones[i].UpdateBone(pos);
			}
			if (this._leftTime - this.StartDelay > this.Duration)
			{
				this.SetSkinState(false);
				this._isFinished = true;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005B70 File Offset: 0x00003D70
		private void SetSkinState(bool isEnabled)
		{
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in this._skins)
			{
				if (skinnedMeshRenderer != null)
				{
					skinnedMeshRenderer.enabled = isEnabled;
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005BCC File Offset: 0x00003DCC
		public MagicFX5_SkinnedMeshBonesAtractor()
		{
		}

		// Token: 0x040000F5 RID: 245
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x040000F6 RID: 246
		public AnimationCurve AttractionStrength = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x040000F7 RID: 247
		public float StrengthMultiplier = 1f;

		// Token: 0x040000F8 RID: 248
		public float Duration = 5f;

		// Token: 0x040000F9 RID: 249
		public float StartDelay = 3f;

		// Token: 0x040000FA RID: 250
		public Transform OverrideTransform;

		// Token: 0x040000FB RID: 251
		private float _leftTime;

		// Token: 0x040000FC RID: 252
		private bool _isFinished;

		// Token: 0x040000FD RID: 253
		private bool _stateChanged;

		// Token: 0x040000FE RID: 254
		private List<SkinnedMeshRenderer> _skins = new List<SkinnedMeshRenderer>();

		// Token: 0x040000FF RID: 255
		private List<MagicFX5_SkinnedMeshBonesAtractor.VirtualBone> _bones = new List<MagicFX5_SkinnedMeshBonesAtractor.VirtualBone>();

		// Token: 0x02000037 RID: 55
		private class VirtualBone
		{
			// Token: 0x060000E7 RID: 231 RVA: 0x000074F7 File Offset: 0x000056F7
			public VirtualBone(Transform bone)
			{
				this.Bone = bone;
				this.IsBoneInitialized = false;
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x0000750D File Offset: 0x0000570D
			public Vector3 GetBonePosition()
			{
				if (!this.IsBoneInitialized)
				{
					this._virtualPos = this.Bone.position;
					this.IsBoneInitialized = true;
				}
				return this._virtualPos;
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x00007535 File Offset: 0x00005735
			public void UpdateBone(Vector3 pos)
			{
				this.Bone.position = pos;
				this._virtualPos = pos;
			}

			// Token: 0x0400018B RID: 395
			public Transform Bone;

			// Token: 0x0400018C RID: 396
			private Vector3 _virtualPos;

			// Token: 0x0400018D RID: 397
			public bool IsBoneInitialized;
		}
	}
}
