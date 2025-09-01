using System;
using UnityEngine;

namespace EZCameraShake
{
	// Token: 0x020003B9 RID: 953
	public class CameraShakeInstance
	{
		// Token: 0x06001F6D RID: 8045 RVA: 0x000BB96C File Offset: 0x000B9B6C
		public CameraShakeInstance(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
		{
			this.Magnitude = magnitude;
			this.fadeOutDuration = fadeOutTime;
			this.fadeInDuration = fadeInTime;
			this.Roughness = roughness;
			if (fadeInTime > 0f)
			{
				this.sustain = true;
				this.currentFadeTime = 0f;
			}
			else
			{
				this.sustain = false;
				this.currentFadeTime = 1f;
			}
			this.tick = (float)UnityEngine.Random.Range(-100, 100);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x000BB9F8 File Offset: 0x000B9BF8
		public CameraShakeInstance(CameraEffectNode node)
		{
			this.nodeRef = node;
			this.sustain = true;
			this.currentFadeTime = 0f;
			this.tick = (float)UnityEngine.Random.Range(-100, 100);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x000BBA54 File Offset: 0x000B9C54
		public CameraShakeInstance(float magnitude, float roughness)
		{
			this.Magnitude = magnitude;
			this.Roughness = roughness;
			this.sustain = true;
			this.tick = (float)UnityEngine.Random.Range(-100, 100);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000BBAAC File Offset: 0x000B9CAC
		public Vector3 UpdateShake()
		{
			this.amt.x = Mathf.PerlinNoise(this.tick, 0f) - 0.5f;
			this.amt.y = Mathf.PerlinNoise(0f, this.tick) - 0.5f;
			this.amt.z = Mathf.PerlinNoise(this.tick, this.tick) - 0.5f;
			if (this.nodeRef != null)
			{
				return this.UpdateNodeShake();
			}
			if (this.fadeInDuration > 0f && this.sustain)
			{
				if (this.currentFadeTime < 1f)
				{
					this.currentFadeTime += Time.deltaTime / this.fadeInDuration;
				}
				else if (this.fadeOutDuration > 0f)
				{
					this.sustain = false;
				}
			}
			if (!this.sustain)
			{
				this.currentFadeTime -= Time.deltaTime / this.fadeOutDuration;
			}
			if (this.sustain)
			{
				this.tick += Time.deltaTime * this.Roughness * this.roughMod;
			}
			else
			{
				this.tick += Time.deltaTime * this.Roughness * this.roughMod * this.currentFadeTime;
			}
			return this.amt * this.Magnitude * this.magnMod * this.currentFadeTime;
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000BBC1C File Offset: 0x000B9E1C
		private Vector3 UpdateNodeShake()
		{
			this.nodeTime += Time.deltaTime;
			float num = this.nodeRef.RoughnessCurve.Evaluate(this.nodeTime);
			float d = this.nodeRef.IntensityCurve.Evaluate(this.nodeTime);
			this.currentFadeTime = 0f;
			this.sustain = (this.nodeTime < this.nodeRef.Duration);
			this.tick += Time.deltaTime * num;
			return this.amt * d;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x000BBCAD File Offset: 0x000B9EAD
		public void StartFadeOut(float fadeOutTime)
		{
			if (fadeOutTime == 0f)
			{
				this.currentFadeTime = 0f;
			}
			this.fadeOutDuration = fadeOutTime;
			this.fadeInDuration = 0f;
			this.sustain = false;
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000BBCDB File Offset: 0x000B9EDB
		public void StartFadeIn(float fadeInTime)
		{
			if (fadeInTime == 0f)
			{
				this.currentFadeTime = 1f;
			}
			this.fadeInDuration = fadeInTime;
			this.fadeOutDuration = 0f;
			this.sustain = true;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x000BBD09 File Offset: 0x000B9F09
		// (set) Token: 0x06001F75 RID: 8053 RVA: 0x000BBD11 File Offset: 0x000B9F11
		public float ScaleRoughness
		{
			get
			{
				return this.roughMod;
			}
			set
			{
				this.roughMod = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x000BBD1A File Offset: 0x000B9F1A
		// (set) Token: 0x06001F77 RID: 8055 RVA: 0x000BBD22 File Offset: 0x000B9F22
		public float ScaleMagnitude
		{
			get
			{
				return this.magnMod;
			}
			set
			{
				this.magnMod = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x000BBD2B File Offset: 0x000B9F2B
		public float NormalizedFadeTime
		{
			get
			{
				return this.currentFadeTime;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x000BBD33 File Offset: 0x000B9F33
		private bool IsShaking
		{
			get
			{
				return this.currentFadeTime > 0f || this.sustain;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x000BBD4A File Offset: 0x000B9F4A
		private bool IsFadingOut
		{
			get
			{
				return !this.sustain && this.currentFadeTime > 0f;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x000BBD63 File Offset: 0x000B9F63
		private bool IsFadingIn
		{
			get
			{
				return this.currentFadeTime < 1f && this.sustain && this.fadeInDuration > 0f;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x000BBD89 File Offset: 0x000B9F89
		public CameraShakeState CurrentState
		{
			get
			{
				if (this.IsFadingIn)
				{
					return CameraShakeState.FadingIn;
				}
				if (this.IsFadingOut)
				{
					return CameraShakeState.FadingOut;
				}
				if (this.IsShaking)
				{
					return CameraShakeState.Sustained;
				}
				return CameraShakeState.Inactive;
			}
		}

		// Token: 0x04001FB6 RID: 8118
		private float Magnitude;

		// Token: 0x04001FB7 RID: 8119
		private float Roughness;

		// Token: 0x04001FB8 RID: 8120
		public Vector3 PositionInfluence;

		// Token: 0x04001FB9 RID: 8121
		public Vector3 RotationInfluence;

		// Token: 0x04001FBA RID: 8122
		public bool DeleteOnInactive = true;

		// Token: 0x04001FBB RID: 8123
		private CameraEffectNode nodeRef;

		// Token: 0x04001FBC RID: 8124
		private float nodeTime;

		// Token: 0x04001FBD RID: 8125
		private float roughMod = 1f;

		// Token: 0x04001FBE RID: 8126
		private float magnMod = 1f;

		// Token: 0x04001FBF RID: 8127
		private float fadeOutDuration;

		// Token: 0x04001FC0 RID: 8128
		private float fadeInDuration;

		// Token: 0x04001FC1 RID: 8129
		private bool sustain;

		// Token: 0x04001FC2 RID: 8130
		private float currentFadeTime;

		// Token: 0x04001FC3 RID: 8131
		private float tick;

		// Token: 0x04001FC4 RID: 8132
		private Vector3 amt;
	}
}
