using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000009 RID: 9
	public class MagicFX5_CollisionImpactEffect : MagicFX5_IScriptInstance
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002AAC File Offset: 0x00000CAC
		internal override void OnEnableExtended()
		{
			this._materialInstances.Clear();
			this._instances.Clear();
			this._shaderColorID = Shader.PropertyToID(this.ShaderNameColor.ToString());
			if (this.ImpactSkinMaterial != null)
			{
				this._startColor = this.ImpactSkinMaterial.GetColor(this._shaderColorID);
			}
			MagicFX5_EffectSettings effectSettings = this.EffectSettings;
			effectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(effectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnCollisionImpactEnter));
			MagicFX5_EffectSettings effectSettings2 = this.EffectSettings;
			effectSettings2.OnEffectSkinActivated = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(effectSettings2.OnEffectSkinActivated, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnSkinImpactActivated));
			MagicFX5_EffectSettings effectSettings3 = this.EffectSettings;
			effectSettings3.OnEffectImpactActivated = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(effectSettings3.OnEffectImpactActivated, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectImpactActivated));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B88 File Offset: 0x00000D88
		internal override void OnDisableExtended()
		{
			MagicFX5_EffectSettings effectSettings = this.EffectSettings;
			effectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Remove(effectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnCollisionImpactEnter));
			MagicFX5_EffectSettings effectSettings2 = this.EffectSettings;
			effectSettings2.OnEffectSkinActivated = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Remove(effectSettings2.OnEffectSkinActivated, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnSkinImpactActivated));
			MagicFX5_EffectSettings effectSettings3 = this.EffectSettings;
			effectSettings3.OnEffectImpactActivated = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Remove(effectSettings3.OnEffectImpactActivated, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectImpactActivated));
			if (this._materialInstances.Count > 0)
			{
				this.RemoveMaterialFromSkinMeshes();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C1E File Offset: 0x00000E1E
		public void OnCollisionImpactEnter(MagicFX5_EffectSettings.EffectCollisionHit hitInfo)
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C20 File Offset: 0x00000E20
		public void OnSkinImpactActivated(MagicFX5_EffectSettings.EffectCollisionHit hitInfo)
		{
			if (!this.EffectSettings.UseSkinMeshImpactEffects)
			{
				return;
			}
			Material material = null;
			if (this.ImpactSkinMaterial != null)
			{
				material = new Material(this.ImpactSkinMaterial)
				{
					hideFlags = HideFlags.HideAndDontSave
				};
				material.SetVector(this._shaderColorID, this.ColorOverLifetime.Evaluate(0f) * this._startColor);
			}
			SkinnedMeshRenderer[] componentsInChildren = hitInfo.Target.GetComponentsInChildren<SkinnedMeshRenderer>();
			if (componentsInChildren.Length > 5)
			{
				Debug.LogError("KriptoFX Warning: The skinned mesh (" + hitInfo.Target.name + ") has multiple skin meshes, so particles/materials will be applied to each of them. This can significantly impact performance! Combine the skinned mesh to optimize performance.");
			}
			foreach (SkinnedMeshRenderer skin in componentsInChildren)
			{
				this.AddSkinEffect(hitInfo, skin, null, material);
			}
			MeshRenderer[] componentsInChildren2 = hitInfo.Target.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer meshRenderer in componentsInChildren2)
			{
				this.AddSkinEffect(hitInfo, null, meshRenderer, material);
			}
			if (this.ImpactSkinMaterial != null)
			{
				MagicFX5_CollisionImpactEffect.MaterialSettings item = new MagicFX5_CollisionImpactEffect.MaterialSettings(hitInfo.Target, material, componentsInChildren, componentsInChildren2);
				this._materialInstances.Add(item);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002D40 File Offset: 0x00000F40
		private void OnEffectImpactActivated(MagicFX5_EffectSettings.EffectCollisionHit hitInfo)
		{
			if (this.ImpactPrefab != null)
			{
				Quaternion rotation = this.UseImpactZeroRotationInsteadHitNormal ? Quaternion.Euler(0f, 0f, 0f) : Quaternion.LookRotation(-hitInfo.Normal);
				GameObject key = UnityEngine.Object.Instantiate<GameObject>(this.ImpactPrefab, hitInfo.Position, rotation);
				this._instances.Add(key, hitInfo.Target);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002DB0 File Offset: 0x00000FB0
		private void AddSkinEffect(MagicFX5_EffectSettings.EffectCollisionHit hitInfo, SkinnedMeshRenderer skin, MeshRenderer meshRenderer, Material matInstance)
		{
			if (this.ImpactSkinParticles != null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ImpactSkinParticles, base.transform.position, base.transform.rotation, this.EffectSettings.transform);
				ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					ParticleSystem.ShapeModule shape = componentsInChildren[i].shape;
					shape.enabled = true;
					if (skin != null)
					{
						shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
						shape.skinnedMeshRenderer = skin;
					}
					else if (meshRenderer != null)
					{
						shape.shapeType = ParticleSystemShapeType.MeshRenderer;
						shape.meshRenderer = meshRenderer;
					}
				}
				this._instances.Add(gameObject, hitInfo.Target);
			}
			if (this.ImpactSkinMaterial != null)
			{
				Renderer renderer = null;
				if (skin != null)
				{
					renderer = skin;
				}
				if (meshRenderer != null)
				{
					renderer = meshRenderer;
				}
				if (renderer != null)
				{
					renderer.AddMaterialInstance(matInstance);
					if (this.UseMaterialDirection)
					{
						this.SetMaterialDirection(hitInfo.Target, hitInfo.Normal, matInstance);
					}
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002EC4 File Offset: 0x000010C4
		private void RemoveMaterialFromSkinMeshes()
		{
			foreach (KeyValuePair<GameObject, Transform> keyValuePair in this._instances)
			{
				UnityEngine.Object.Destroy(keyValuePair.Key);
			}
			this._instances.Clear();
			foreach (MagicFX5_CollisionImpactEffect.MaterialSettings materialSettings in this._materialInstances)
			{
				materialSettings.Restore(this.DeactivateMesh, false);
				UnityEngine.Object.Destroy(materialSettings.MaterialInstance);
			}
			this._materialInstances.Clear();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002F84 File Offset: 0x00001184
		private void SetMaterialDirection(Transform target, Vector3 normal, Material mat)
		{
			Quaternion quaternion = Quaternion.Inverse(target.transform.rotation) * Quaternion.FromToRotation(Vector3.right, -normal);
			Matrix4x4 value = Matrix4x4.Rotate(quaternion);
			mat.SetMatrix(this._rotationMatrixID, value);
			mat.SetVector(this._rotationVectorID, -(quaternion * Vector3.right));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002FEC File Offset: 0x000011EC
		internal override void ManualUpdate()
		{
			foreach (MagicFX5_CollisionImpactEffect.MaterialSettings materialSettings in this._materialInstances)
			{
				materialSettings.AnimationLeftTime += Time.deltaTime;
				float num = this.DeactivateMesh ? this.DeactivateMeshDelay : this.DestroyTime;
				float num2 = materialSettings.AnimationLeftTime / num;
				if (this.DeactivateMesh && materialSettings.IsMeshActive && materialSettings.AnimationLeftTime > this.DeactivateMeshDelay)
				{
					materialSettings.IsMeshActive = false;
					foreach (Renderer renderer in materialSettings.Renderers)
					{
						renderer.enabled = false;
					}
				}
				if (num2 > 1f)
				{
					foreach (Renderer renderer2 in materialSettings.Renderers)
					{
						if (renderer2 != null)
						{
							renderer2.RemoveMaterialInstance(materialSettings.MaterialInstance);
						}
					}
					UnityEngine.Object.Destroy(materialSettings.MaterialInstance);
				}
				else
				{
					Color c = this.ColorOverLifetime.Evaluate(num2) * this._startColor;
					materialSettings.MaterialInstance.SetVector(this._shaderColorID, c);
				}
			}
			foreach (KeyValuePair<GameObject, Transform> keyValuePair in this._instances)
			{
				keyValuePair.Key.transform.position = keyValuePair.Value.position;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000031FC File Offset: 0x000013FC
		public MagicFX5_CollisionImpactEffect()
		{
		}

		// Token: 0x0400003C RID: 60
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x0400003D RID: 61
		public Gradient ColorOverLifetime = new Gradient();

		// Token: 0x0400003E RID: 62
		public MagicFX5_ShaderColorCurve.ME2_ShaderPropertyName ShaderNameColor;

		// Token: 0x0400003F RID: 63
		public float DestroyTime = 10f;

		// Token: 0x04000040 RID: 64
		public Material ImpactSkinMaterial;

		// Token: 0x04000041 RID: 65
		public GameObject ImpactSkinParticles;

		// Token: 0x04000042 RID: 66
		public GameObject ImpactPrefab;

		// Token: 0x04000043 RID: 67
		public bool UseImpactZeroRotationInsteadHitNormal;

		// Token: 0x04000044 RID: 68
		[Space]
		public bool UseMaterialDirection;

		// Token: 0x04000045 RID: 69
		public bool DeactivateMesh;

		// Token: 0x04000046 RID: 70
		public float DeactivateMeshDelay = 3f;

		// Token: 0x04000047 RID: 71
		private List<MagicFX5_CollisionImpactEffect.MaterialSettings> _materialInstances = new List<MagicFX5_CollisionImpactEffect.MaterialSettings>();

		// Token: 0x04000048 RID: 72
		private Dictionary<GameObject, Transform> _instances = new Dictionary<GameObject, Transform>();

		// Token: 0x04000049 RID: 73
		private int _shaderColorID;

		// Token: 0x0400004A RID: 74
		private Color _startColor;

		// Token: 0x0400004B RID: 75
		private int _rotationMatrixID = Shader.PropertyToID("_RotationMatrix");

		// Token: 0x0400004C RID: 76
		private int _rotationVectorID = Shader.PropertyToID("_RotationVector");

		// Token: 0x0200002C RID: 44
		private class MaterialSettings
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x00006B28 File Offset: 0x00004D28
			public MaterialSettings(Transform target, Material instance, SkinnedMeshRenderer[] skinsRenderers, MeshRenderer[] meshRenderers)
			{
				this.Target = target;
				this.MaterialInstance = instance;
				this.AnimationLeftTime = 0f;
				this.StartScale = this.Target.localScale;
				if (skinsRenderers != null)
				{
					this.Renderers.AddRange(skinsRenderers);
				}
				if (meshRenderers != null)
				{
					this.Renderers.AddRange(meshRenderers);
				}
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x00006B98 File Offset: 0x00004D98
			public void Restore(bool deactivateMeshFeatureEnabled, bool reduceMeshSizeFeatureEnabled)
			{
				foreach (Renderer renderer in this.Renderers)
				{
					if (!(renderer == null))
					{
						renderer.RemoveMaterialInstance(this.MaterialInstance);
						if (deactivateMeshFeatureEnabled)
						{
							renderer.enabled = true;
						}
						if (reduceMeshSizeFeatureEnabled)
						{
							this.Target.localScale = this.StartScale;
						}
					}
				}
			}

			// Token: 0x04000155 RID: 341
			public Transform Target;

			// Token: 0x04000156 RID: 342
			public Material MaterialInstance;

			// Token: 0x04000157 RID: 343
			public float AnimationLeftTime;

			// Token: 0x04000158 RID: 344
			public bool IsMeshActive = true;

			// Token: 0x04000159 RID: 345
			public List<Renderer> Renderers = new List<Renderer>();

			// Token: 0x0400015A RID: 346
			public Vector3 StartScale;
		}
	}
}
