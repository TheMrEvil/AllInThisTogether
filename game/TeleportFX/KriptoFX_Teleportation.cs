using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TeleportFX
{
	// Token: 0x02000002 RID: 2
	[DisallowMultipleComponent]
	public class KriptoFX_Teleportation : TeleportFX_IScriptInstance
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal override void OnEnableExtended()
		{
			this._lastState = KriptoFX_Teleportation.TeleportationStateEnum.DefaultEnabled;
			this.ManualUpdate();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205F File Offset: 0x0000025F
		internal override void OnDisableExtended()
		{
			base.StopAllCoroutines();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		private void Awake()
		{
			if (this.AppearTeleportationEffect == null || this.DisappearTeleportationEffect == null)
			{
				Debug.LogError("You have to set the 'Appear and Dissapear Teleportation Effect' before using!");
				return;
			}
			this._appearEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.AppearTeleportationEffect);
			this._appearEffectInstance.SetActive(false);
			this._disappearEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.DisappearTeleportationEffect);
			this._disappearEffectInstance.SetActive(false);
			this._appearSettings = this._appearEffectInstance.GetComponentInChildren<TeleportFX_Settings>();
			this._disappearSettings = this._disappearEffectInstance.GetComponentInChildren<TeleportFX_Settings>();
			this._animator = base.GetComponent<Animator>();
			Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>(true);
			foreach (Renderer rend in componentsInChildren)
			{
				this._meshRenderers.Add(new KriptoFX_Teleportation.MeshRendererInfo(rend, this._materialInstances));
			}
			if (this.OverrideEffectPosition)
			{
				this._appearEffectInstance.transform.parent = this.OverrideEffectPosition;
				this._disappearEffectInstance.transform.parent = this.OverrideEffectPosition;
			}
			else
			{
				this._appearEffectInstance.transform.parent = base.transform;
				this._disappearEffectInstance.transform.parent = base.transform;
			}
			if (!this.UseEffectLighting)
			{
				foreach (GameObject gameObject in this._appearSettings.LightObjects)
				{
					if (gameObject != null)
					{
						gameObject.SetActive(false);
					}
				}
				foreach (GameObject gameObject2 in this._disappearSettings.LightObjects)
				{
					if (gameObject2 != null)
					{
						gameObject2.SetActive(false);
					}
				}
			}
			if (this.UseAutoScale)
			{
				float num = 0f;
				float num2 = 0f;
				foreach (Renderer renderer in componentsInChildren)
				{
					num = Mathf.Min(num, renderer.bounds.min.y);
					num2 = Mathf.Max(num2, renderer.bounds.max.y);
				}
				float num3 = num2 - num;
				Vector3 localScale = Vector3.one * (num3 / base.transform.transform.localScale.y / 2.67f);
				this._appearEffectInstance.transform.localScale = localScale;
				this._disappearEffectInstance.transform.localScale = localScale;
			}
			if (Math.Abs(this.AppearTimeScale - 1f) > 0.0001f)
			{
				this.SetTimeScale(this._appearSettings, this._appearEffectInstance);
			}
			if (Math.Abs(this.DisappearTimeScale - 1f) > 0.0001f)
			{
				this.SetTimeScale(this._disappearSettings, this._disappearEffectInstance);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002324 File Offset: 0x00000524
		private void SetTimeScale(TeleportFX_Settings settings, GameObject effectInstance)
		{
			settings.CutoutTime /= this.AppearTimeScale;
			settings.VertexTeleportationTime /= this.AppearTimeScale;
			settings.VertexTeleportationTime /= this.AppearTimeScale;
			ParticleSystem[] componentsInChildren = effectInstance.GetComponentsInChildren<ParticleSystem>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ParticleSystem.MainModule mainModule;
				componentsInChildren[i].main.simulationSpeed = mainModule.simulationSpeed * this.AppearTimeScale;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000239D File Offset: 0x0000059D
		private void OnDestroy()
		{
			this.ClearMaterials();
			if (this._appearEffectInstance != null)
			{
				UnityEngine.Object.Destroy(this._appearEffectInstance);
			}
			if (this._disappearEffectInstance != null)
			{
				UnityEngine.Object.Destroy(this._disappearEffectInstance);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023D7 File Offset: 0x000005D7
		internal override void ManualUpdate()
		{
			if (this.AppearTeleportationEffect == null)
			{
				return;
			}
			if (this._lastState != this.TeleportationState)
			{
				this._lastState = this.TeleportationState;
				this.TeleportationStateChanged(false);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000240C File Offset: 0x0000060C
		private void TeleportationStateChanged(bool isInitialisationState)
		{
			base.StopAllCoroutines();
			this._appearEffectInstance.SetActive(false);
			this._disappearEffectInstance.SetActive(false);
			this._appearEffectInstance.transform.position = (this.OverrideEffectPosition ? this.OverrideEffectPosition.position : base.transform.position);
			this._disappearEffectInstance.transform.position = this._appearEffectInstance.transform.position;
			if (this.TeleportationState == KriptoFX_Teleportation.TeleportationStateEnum.Appear)
			{
				this._appearEffectInstance.SetActive(true);
				foreach (KriptoFX_Teleportation.MeshRendererInfo meshRendererInfo in this._meshRenderers)
				{
					meshRendererInfo.SwapToTeleportMaterial(0f, 0f, this._appearSettings.Shader);
				}
				this.TriggerAnimator(this.AppearAnimationTriggerName);
				if (!isInitialisationState)
				{
					base.StartCoroutine(this.TeleportationTick());
				}
			}
			if (this.TeleportationState == KriptoFX_Teleportation.TeleportationStateEnum.Disappear)
			{
				this._disappearEffectInstance.SetActive(true);
				foreach (KriptoFX_Teleportation.MeshRendererInfo meshRendererInfo2 in this._meshRenderers)
				{
					meshRendererInfo2.SwapToTeleportMaterial(0f, 0f, this._disappearSettings.Shader);
				}
				this.TriggerAnimator(this.DisappearAnimationTriggerName);
				if (!isInitialisationState)
				{
					base.StartCoroutine(this.TeleportationTick());
				}
			}
			if (this.TeleportationState == KriptoFX_Teleportation.TeleportationStateEnum.DefaultEnabled)
			{
				foreach (KriptoFX_Teleportation.MeshRendererInfo meshRendererInfo3 in this._meshRenderers)
				{
					meshRendererInfo3.SwapToDefaultMaterial();
				}
			}
			if (this.TeleportationState == KriptoFX_Teleportation.TeleportationStateEnum.DefaultDisabled)
			{
				foreach (KriptoFX_Teleportation.MeshRendererInfo meshRendererInfo4 in this._meshRenderers)
				{
					meshRendererInfo4.SwapToTeleportMaterial(1f, 1f, this._appearSettings.Shader);
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002640 File Offset: 0x00000840
		private void ClearMaterials()
		{
			foreach (KriptoFX_Teleportation.MeshRendererInfo meshRendererInfo in this._meshRenderers)
			{
				meshRendererInfo.Clear();
			}
			this._meshRenderers.Clear();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000269C File Offset: 0x0000089C
		private IEnumerator TeleportationTick()
		{
			TeleportFX_Settings settings = this._appearSettings;
			if (this.TeleportationState == KriptoFX_Teleportation.TeleportationStateEnum.Disappear)
			{
				settings = this._disappearSettings;
			}
			float maxTime = 0f;
			if (settings.UseVertexTeleportation)
			{
				maxTime = Mathf.Max(settings.VertexTeleportationTime, maxTime);
			}
			if (settings.UseDissolveByTime)
			{
				maxTime = Mathf.Max(settings.CutoutTime, maxTime);
			}
			if (settings.UseDissolveByHeight)
			{
				maxTime = Mathf.Max(settings.DissolveByHeightDuration, maxTime);
			}
			if (settings.UseDissolveByTime || settings.UseDissolveByHeight)
			{
				int num = settings.UseVertexPositionAsUV ? 1 : 0;
				foreach (KeyValuePair<Material, Material> keyValuePair in this._materialInstances)
				{
					Material value = keyValuePair.Value;
					value.SetFloat(KriptoFX_Teleportation._UseVertexPositionAsUVID, (float)num);
					if (settings.OverrideTexture)
					{
						value.SetTexture(KriptoFX_Teleportation._DissolveNoiseID, settings.NoiseTexture);
					}
					value.SetVector(KriptoFX_Teleportation._NoiseStrengthID, settings.NoiseStrength);
					value.SetVector(KriptoFX_Teleportation._NoiseScaleID, settings.NoiseScale);
					value.SetColor(KriptoFX_Teleportation._DissolveColor1ID, settings.DissolveColor1);
					value.SetColor(KriptoFX_Teleportation._DissolveColor2ID, settings.DissolveColor2);
					value.SetColor(KriptoFX_Teleportation._DissolveColor3ID, settings.DissolveColor3);
					value.SetVector(KriptoFX_Teleportation._DissolveThresholdID, settings.DissolveThresold);
				}
			}
			this._leftTime = 0f;
			while (this._leftTime <= maxTime)
			{
				this._leftTime += Time.deltaTime;
				float value2 = 0f;
				float value3 = 1f;
				float value4 = 0f;
				if (settings.UseVertexTeleportation)
				{
					float time = Mathf.Clamp01(this._leftTime / settings.VertexTeleportationTime);
					value2 = settings.VertexTeleporationCurve.Evaluate(time);
				}
				if (settings.UseDissolveByTime)
				{
					float time2 = Mathf.Clamp01(this._leftTime / settings.CutoutTime);
					value3 = settings.CutoutCurve.Evaluate(time2);
				}
				if (settings.UseDissolveByHeight)
				{
					value4 = settings.DissolveAnchor.transform.position.y;
				}
				foreach (KeyValuePair<Material, Material> keyValuePair2 in this._materialInstances)
				{
					Material value5 = keyValuePair2.Value;
					value5.SetFloat(KriptoFX_Teleportation._TeleportThresholdID, value2);
					value5.SetFloat(KriptoFX_Teleportation._DissolveCutoutID, value3);
					value5.SetFloat(KriptoFX_Teleportation._DissolveCutoutHeightID, value4);
				}
				yield return null;
			}
			if (this.TeleportationState == KriptoFX_Teleportation.TeleportationStateEnum.Appear)
			{
				foreach (KriptoFX_Teleportation.MeshRendererInfo meshRendererInfo in this._meshRenderers)
				{
					meshRendererInfo.SwapToDefaultMaterial();
				}
			}
			Action isTeleportationFinished = this.IsTeleportationFinished;
			if (isTeleportationFinished != null)
			{
				isTeleportationFinished();
			}
			yield break;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000026AB File Offset: 0x000008AB
		private void TriggerAnimator(string triggerName)
		{
			if (!this.UseAnimatorTriggers || this._animator == null || triggerName.Length == 0)
			{
				return;
			}
			this._animator.SetTrigger(triggerName);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000026D8 File Offset: 0x000008D8
		public KriptoFX_Teleportation()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000272C File Offset: 0x0000092C
		// Note: this type is marked as 'beforefieldinit'.
		static KriptoFX_Teleportation()
		{
		}

		// Token: 0x04000001 RID: 1
		public KriptoFX_Teleportation.TeleportationStateEnum TeleportationState = KriptoFX_Teleportation.TeleportationStateEnum.Appear;

		// Token: 0x04000002 RID: 2
		public GameObject AppearTeleportationEffect;

		// Token: 0x04000003 RID: 3
		public GameObject DisappearTeleportationEffect;

		// Token: 0x04000004 RID: 4
		public bool UseEffectLighting = true;

		// Token: 0x04000005 RID: 5
		public float AppearTimeScale = 1f;

		// Token: 0x04000006 RID: 6
		public float DisappearTimeScale = 1f;

		// Token: 0x04000007 RID: 7
		internal bool UseAutoScale = true;

		// Token: 0x04000008 RID: 8
		internal Transform OverrideEffectPosition;

		// Token: 0x04000009 RID: 9
		[Space]
		public bool UseAnimatorTriggers;

		// Token: 0x0400000A RID: 10
		public string AppearAnimationTriggerName;

		// Token: 0x0400000B RID: 11
		public string DisappearAnimationTriggerName;

		// Token: 0x0400000C RID: 12
		[HideInInspector]
		public Action IsTeleportationFinished;

		// Token: 0x0400000D RID: 13
		private KriptoFX_Teleportation.TeleportationStateEnum _lastState;

		// Token: 0x0400000E RID: 14
		private Dictionary<Material, Material> _materialInstances = new Dictionary<Material, Material>();

		// Token: 0x0400000F RID: 15
		private List<KriptoFX_Teleportation.MeshRendererInfo> _meshRenderers = new List<KriptoFX_Teleportation.MeshRendererInfo>();

		// Token: 0x04000010 RID: 16
		private GameObject _appearEffectInstance;

		// Token: 0x04000011 RID: 17
		private GameObject _disappearEffectInstance;

		// Token: 0x04000012 RID: 18
		private TeleportFX_Settings _appearSettings;

		// Token: 0x04000013 RID: 19
		private TeleportFX_Settings _disappearSettings;

		// Token: 0x04000014 RID: 20
		private float _leftTime;

		// Token: 0x04000015 RID: 21
		private static int _TeleportThresholdID = Shader.PropertyToID("_TeleportThreshold");

		// Token: 0x04000016 RID: 22
		private static int _DissolveCutoutID = Shader.PropertyToID("_DissolveCutout");

		// Token: 0x04000017 RID: 23
		private static int _DissolveCutoutHeightID = Shader.PropertyToID("_DissolveCutoutHeight");

		// Token: 0x04000018 RID: 24
		private static int _NoiseStrengthID = Shader.PropertyToID("_NoiseStrength");

		// Token: 0x04000019 RID: 25
		private static int _DissolveColor1ID = Shader.PropertyToID("_DissolveColor1");

		// Token: 0x0400001A RID: 26
		private static int _DissolveColor2ID = Shader.PropertyToID("_DissolveColor2");

		// Token: 0x0400001B RID: 27
		private static int _DissolveColor3ID = Shader.PropertyToID("_DissolveColor3");

		// Token: 0x0400001C RID: 28
		private static int _DissolveThresholdID = Shader.PropertyToID("_DissolveThreshold");

		// Token: 0x0400001D RID: 29
		private static int _UseVertexPositionAsUVID = Shader.PropertyToID("_UseVertexPositionAsUV");

		// Token: 0x0400001E RID: 30
		private static int _NoiseScaleID = Shader.PropertyToID("_NoiseScale");

		// Token: 0x0400001F RID: 31
		private static int _DissolveNoiseID = Shader.PropertyToID("_DissolveNoise");

		// Token: 0x04000020 RID: 32
		private static int _AlphaCutoffEnableID = Shader.PropertyToID("_AlphaCutoffEnable");

		// Token: 0x04000021 RID: 33
		private Animator _animator;

		// Token: 0x04000022 RID: 34
		private const float DefaultModelScaleMeters = 2.67f;

		// Token: 0x02000008 RID: 8
		public enum TeleportationStateEnum
		{
			// Token: 0x04000048 RID: 72
			DefaultEnabled,
			// Token: 0x04000049 RID: 73
			DefaultDisabled,
			// Token: 0x0400004A RID: 74
			Appear,
			// Token: 0x0400004B RID: 75
			Disappear
		}

		// Token: 0x02000009 RID: 9
		private class MeshRendererInfo
		{
			// Token: 0x0600002A RID: 42 RVA: 0x00002DBC File Offset: 0x00000FBC
			public MeshRendererInfo(Renderer rend, Dictionary<Material, Material> materialInstances)
			{
				this.MeshRenderer = rend;
				this.DefaultMaterial = rend.sharedMaterial;
				if (!materialInstances.ContainsKey(this.DefaultMaterial))
				{
					this.InstanceMaterial = new Material(this.DefaultMaterial)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
					materialInstances.Add(this.DefaultMaterial, this.InstanceMaterial);
					return;
				}
				this.InstanceMaterial = materialInstances[this.DefaultMaterial];
			}

			// Token: 0x0600002B RID: 43 RVA: 0x00002E30 File Offset: 0x00001030
			public void SwapToTeleportMaterial(float thresholdCutout, float dissolveFade, Shader shader)
			{
				this.InstanceMaterial.shader = shader;
				this.InstanceMaterial.SetFloat(KriptoFX_Teleportation._TeleportThresholdID, thresholdCutout);
				this.InstanceMaterial.SetFloat(KriptoFX_Teleportation._DissolveCutoutID, dissolveFade);
				this.InstanceMaterial.SetFloat(KriptoFX_Teleportation._AlphaCutoffEnableID, 1f);
				this.MeshRenderer.sharedMaterial = this.InstanceMaterial;
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00002E91 File Offset: 0x00001091
			public void SwapToDefaultMaterial()
			{
				this.MeshRenderer.sharedMaterial = this.DefaultMaterial;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002EA4 File Offset: 0x000010A4
			public void Clear()
			{
				if (this.InstanceMaterial != null)
				{
					UnityEngine.Object.Destroy(this.InstanceMaterial);
				}
				if (this.MeshRenderer != null && this.DefaultMaterial != null)
				{
					this.MeshRenderer.sharedMaterial = this.DefaultMaterial;
				}
			}

			// Token: 0x0400004C RID: 76
			public Renderer MeshRenderer;

			// Token: 0x0400004D RID: 77
			public Material DefaultMaterial;

			// Token: 0x0400004E RID: 78
			public Material InstanceMaterial;
		}

		// Token: 0x0200000A RID: 10
		[CompilerGenerated]
		private sealed class <TeleportationTick>d__44 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600002E RID: 46 RVA: 0x00002EF7 File Offset: 0x000010F7
			[DebuggerHidden]
			public <TeleportationTick>d__44(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002F06 File Offset: 0x00001106
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000030 RID: 48 RVA: 0x00002F08 File Offset: 0x00001108
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				KriptoFX_Teleportation kriptoFX_Teleportation = this;
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
					settings = kriptoFX_Teleportation._appearSettings;
					if (kriptoFX_Teleportation.TeleportationState == KriptoFX_Teleportation.TeleportationStateEnum.Disappear)
					{
						settings = kriptoFX_Teleportation._disappearSettings;
					}
					maxTime = 0f;
					if (settings.UseVertexTeleportation)
					{
						maxTime = Mathf.Max(settings.VertexTeleportationTime, maxTime);
					}
					if (settings.UseDissolveByTime)
					{
						maxTime = Mathf.Max(settings.CutoutTime, maxTime);
					}
					if (settings.UseDissolveByHeight)
					{
						maxTime = Mathf.Max(settings.DissolveByHeightDuration, maxTime);
					}
					if (settings.UseDissolveByTime || settings.UseDissolveByHeight)
					{
						int num2 = settings.UseVertexPositionAsUV ? 1 : 0;
						foreach (KeyValuePair<Material, Material> keyValuePair in kriptoFX_Teleportation._materialInstances)
						{
							Material value = keyValuePair.Value;
							value.SetFloat(KriptoFX_Teleportation._UseVertexPositionAsUVID, (float)num2);
							if (settings.OverrideTexture)
							{
								value.SetTexture(KriptoFX_Teleportation._DissolveNoiseID, settings.NoiseTexture);
							}
							value.SetVector(KriptoFX_Teleportation._NoiseStrengthID, settings.NoiseStrength);
							value.SetVector(KriptoFX_Teleportation._NoiseScaleID, settings.NoiseScale);
							value.SetColor(KriptoFX_Teleportation._DissolveColor1ID, settings.DissolveColor1);
							value.SetColor(KriptoFX_Teleportation._DissolveColor2ID, settings.DissolveColor2);
							value.SetColor(KriptoFX_Teleportation._DissolveColor3ID, settings.DissolveColor3);
							value.SetVector(KriptoFX_Teleportation._DissolveThresholdID, settings.DissolveThresold);
						}
					}
					kriptoFX_Teleportation._leftTime = 0f;
				}
				if (kriptoFX_Teleportation._leftTime > maxTime)
				{
					if (kriptoFX_Teleportation.TeleportationState == KriptoFX_Teleportation.TeleportationStateEnum.Appear)
					{
						foreach (KriptoFX_Teleportation.MeshRendererInfo meshRendererInfo in kriptoFX_Teleportation._meshRenderers)
						{
							meshRendererInfo.SwapToDefaultMaterial();
						}
					}
					Action isTeleportationFinished = kriptoFX_Teleportation.IsTeleportationFinished;
					if (isTeleportationFinished != null)
					{
						isTeleportationFinished();
					}
					return false;
				}
				kriptoFX_Teleportation._leftTime += Time.deltaTime;
				float value2 = 0f;
				float value3 = 1f;
				float value4 = 0f;
				if (settings.UseVertexTeleportation)
				{
					float time = Mathf.Clamp01(kriptoFX_Teleportation._leftTime / settings.VertexTeleportationTime);
					value2 = settings.VertexTeleporationCurve.Evaluate(time);
				}
				if (settings.UseDissolveByTime)
				{
					float time2 = Mathf.Clamp01(kriptoFX_Teleportation._leftTime / settings.CutoutTime);
					value3 = settings.CutoutCurve.Evaluate(time2);
				}
				if (settings.UseDissolveByHeight)
				{
					value4 = settings.DissolveAnchor.transform.position.y;
				}
				foreach (KeyValuePair<Material, Material> keyValuePair2 in kriptoFX_Teleportation._materialInstances)
				{
					Material value5 = keyValuePair2.Value;
					value5.SetFloat(KriptoFX_Teleportation._TeleportThresholdID, value2);
					value5.SetFloat(KriptoFX_Teleportation._DissolveCutoutID, value3);
					value5.SetFloat(KriptoFX_Teleportation._DissolveCutoutHeightID, value4);
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000031 RID: 49 RVA: 0x000032E8 File Offset: 0x000014E8
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000032 RID: 50 RVA: 0x000032F0 File Offset: 0x000014F0
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000033 RID: 51 RVA: 0x000032F7 File Offset: 0x000014F7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400004F RID: 79
			private int <>1__state;

			// Token: 0x04000050 RID: 80
			private object <>2__current;

			// Token: 0x04000051 RID: 81
			public KriptoFX_Teleportation <>4__this;

			// Token: 0x04000052 RID: 82
			private TeleportFX_Settings <settings>5__2;

			// Token: 0x04000053 RID: 83
			private float <maxTime>5__3;
		}
	}
}
