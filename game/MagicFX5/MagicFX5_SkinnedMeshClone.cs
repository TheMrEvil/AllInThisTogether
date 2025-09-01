using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace MagicFX5
{
	// Token: 0x02000020 RID: 32
	public class MagicFX5_SkinnedMeshClone : MonoBehaviour
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00005C38 File Offset: 0x00003E38
		private void OnEnable()
		{
			this._shaderColorID = Shader.PropertyToID(this.ShaderNameColor.ToString());
			this._startColor = this.CloneMaterial.GetColor(this._shaderColorID);
			MagicFX5_EffectSettings effectSettings = this.EffectSettings;
			effectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(effectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnCollisionImpactEnter));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00005CA0 File Offset: 0x00003EA0
		private void OnDisable()
		{
			MagicFX5_EffectSettings effectSettings = this.EffectSettings;
			effectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Remove(effectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnCollisionImpactEnter));
			base.StopCoroutine(this.UpdateMaterial());
			if (this._cloneInstance != null)
			{
				UnityEngine.Object.Destroy(this._cloneInstance);
			}
			if (this._materialInstance != null)
			{
				UnityEngine.Object.Destroy(this._materialInstance);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005D14 File Offset: 0x00003F14
		public void OnCollisionImpactEnter(MagicFX5_EffectSettings.EffectCollisionHit hitInfo)
		{
			Animator component = hitInfo.Target.GetComponent<Animator>();
			if (component == null)
			{
				return;
			}
			this._cloneInstance = UnityEngine.Object.Instantiate<GameObject>(component.gameObject, component.transform.position, component.transform.rotation);
			this._materialInstance = new Material(this.CloneMaterial)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
			this._materialInstance.SetVector(this._shaderColorID, this.ColorOverLifetime.Evaluate(0f) * this._startColor);
			Animator component2 = this._cloneInstance.GetComponent<Animator>();
			SkinnedMeshRenderer[] componentsInChildren = this._cloneInstance.GetComponentsInChildren<SkinnedMeshRenderer>();
			if (this.DeactivateNonSkinnedMeshes)
			{
				ParticleSystem[] componentsInChildren2 = this._cloneInstance.GetComponentsInChildren<ParticleSystem>();
				for (int i = 0; i < componentsInChildren2.Length; i++)
				{
					componentsInChildren2[i].gameObject.SetActive(false);
				}
			}
			component2.runtimeAnimatorController = this.AnimController;
			SkinnedMeshRenderer[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				Renderer component3 = array[i].GetComponent<Renderer>();
				component3.sharedMaterial = this._materialInstance;
				component3.shadowCastingMode = ShadowCastingMode.Off;
			}
			UnityEngine.Object.Destroy(this._cloneInstance, this.LifeTime);
			base.StartCoroutine(this.UpdateMaterial());
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005E51 File Offset: 0x00004051
		private IEnumerator UpdateMaterial()
		{
			while (this._leftTime < this.LifeTime)
			{
				this._leftTime += Time.deltaTime;
				float time = this._leftTime / this.LifeTime;
				this._materialInstance.SetVector(this._shaderColorID, this.ColorOverLifetime.Evaluate(time) * this._startColor);
				yield return null;
			}
			yield break;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005E60 File Offset: 0x00004060
		public MagicFX5_SkinnedMeshClone()
		{
		}

		// Token: 0x04000100 RID: 256
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x04000101 RID: 257
		public Material CloneMaterial;

		// Token: 0x04000102 RID: 258
		public RuntimeAnimatorController AnimController;

		// Token: 0x04000103 RID: 259
		public bool DeactivateNonSkinnedMeshes = true;

		// Token: 0x04000104 RID: 260
		public Gradient ColorOverLifetime = new Gradient();

		// Token: 0x04000105 RID: 261
		public MagicFX5_ShaderColorCurve.ME2_ShaderPropertyName ShaderNameColor;

		// Token: 0x04000106 RID: 262
		public float LifeTime = 8f;

		// Token: 0x04000107 RID: 263
		private GameObject _cloneInstance;

		// Token: 0x04000108 RID: 264
		private Material _materialInstance;

		// Token: 0x04000109 RID: 265
		private float _leftTime;

		// Token: 0x0400010A RID: 266
		private int _shaderColorID;

		// Token: 0x0400010B RID: 267
		private Color _startColor;

		// Token: 0x02000038 RID: 56
		[CompilerGenerated]
		private sealed class <UpdateMaterial>d__15 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000EA RID: 234 RVA: 0x0000754A File Offset: 0x0000574A
			[DebuggerHidden]
			public <UpdateMaterial>d__15(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000EB RID: 235 RVA: 0x00007559 File Offset: 0x00005759
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000EC RID: 236 RVA: 0x0000755C File Offset: 0x0000575C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MagicFX5_SkinnedMeshClone magicFX5_SkinnedMeshClone = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
				}
				if (magicFX5_SkinnedMeshClone._leftTime >= magicFX5_SkinnedMeshClone.LifeTime)
				{
					return false;
				}
				magicFX5_SkinnedMeshClone._leftTime += Time.deltaTime;
				float time = magicFX5_SkinnedMeshClone._leftTime / magicFX5_SkinnedMeshClone.LifeTime;
				magicFX5_SkinnedMeshClone._materialInstance.SetVector(magicFX5_SkinnedMeshClone._shaderColorID, magicFX5_SkinnedMeshClone.ColorOverLifetime.Evaluate(time) * magicFX5_SkinnedMeshClone._startColor);
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x060000ED RID: 237 RVA: 0x000075FC File Offset: 0x000057FC
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000EE RID: 238 RVA: 0x00007604 File Offset: 0x00005804
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x060000EF RID: 239 RVA: 0x0000760B File Offset: 0x0000580B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400018E RID: 398
			private int <>1__state;

			// Token: 0x0400018F RID: 399
			private object <>2__current;

			// Token: 0x04000190 RID: 400
			public MagicFX5_SkinnedMeshClone <>4__this;
		}
	}
}
