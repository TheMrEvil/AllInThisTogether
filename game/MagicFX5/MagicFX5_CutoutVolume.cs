using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200000C RID: 12
	public class MagicFX5_CutoutVolume : MonoBehaviour
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000033DB File Offset: 0x000015DB
		private void Awake()
		{
			this._shaderID = Shader.PropertyToID("_VolumeCutoutPos");
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000033F0 File Offset: 0x000015F0
		private void OnEnable()
		{
			Vector3 position = base.transform.position;
			Vector3 lossyScale = base.transform.lossyScale;
			float w = Mathf.Max(lossyScale.x, Mathf.Max(lossyScale.y, lossyScale.z));
			this._volumeData = new Vector4(position.x, position.y, position.z, w);
			if (this.OverrideMaterial != null)
			{
				this.OverrideMaterial.SetVector(this._shaderID, this._volumeData);
			}
			else
			{
				Renderer[] renderers = this.Renderers;
				for (int i = 0; i < renderers.Length; i++)
				{
					renderers[i].SetColorPropertyBlock(this._shaderID, this._volumeData);
				}
			}
			if (this.AffectTarget != null)
			{
				MagicFX5_EffectSettings affectTarget = this.AffectTarget;
				affectTarget.OnEffectSkinActivated = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(affectTarget.OnEffectSkinActivated, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectSkinActivated));
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000034DF File Offset: 0x000016DF
		private void OnDisable()
		{
			if (this.AffectTarget != null)
			{
				MagicFX5_EffectSettings affectTarget = this.AffectTarget;
				affectTarget.OnEffectSkinActivated = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Remove(affectTarget.OnEffectSkinActivated, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectSkinActivated));
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003518 File Offset: 0x00001718
		private void OnEffectSkinActivated(MagicFX5_EffectSettings.EffectCollisionHit hit)
		{
			Renderer[] componentsInChildren = hit.Target.GetComponentsInChildren<Renderer>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetColorPropertyBlock(this._shaderID, this._volumeData);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003558 File Offset: 0x00001758
		private void OnDrawGizmosSelected()
		{
			Vector3 lossyScale = base.transform.lossyScale;
			float radius = Mathf.Max(lossyScale.x, Mathf.Max(lossyScale.y, lossyScale.z));
			Gizmos.DrawWireSphere(base.transform.position, radius);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000359F File Offset: 0x0000179F
		public MagicFX5_CutoutVolume()
		{
		}

		// Token: 0x0400004F RID: 79
		public Renderer[] Renderers;

		// Token: 0x04000050 RID: 80
		public Material OverrideMaterial;

		// Token: 0x04000051 RID: 81
		public MagicFX5_EffectSettings AffectTarget;

		// Token: 0x04000052 RID: 82
		private int _shaderID;

		// Token: 0x04000053 RID: 83
		private Vector4 _volumeData;

		// Token: 0x0200002D RID: 45
		public enum ModeEnum
		{
			// Token: 0x0400015C RID: 348
			UpdateMaterial,
			// Token: 0x0400015D RID: 349
			UpdatePropertyBlock
		}
	}
}
