using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000006 RID: 6
	public class MagicFX5_ChainEffect : MagicFX5_IScriptInstance
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002825 File Offset: 0x00000A25
		internal override void OnEnableExtended()
		{
			this._isFinished = false;
			this._leftTime = 0f;
			this._instances.Clear();
			this._ropes.Clear();
			base.StartCoroutine(this.Initialize());
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000285C File Offset: 0x00000A5C
		internal override void OnDisableExtended()
		{
			base.StopCoroutine(this.Initialize());
			foreach (GameObject obj in this._instances)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000028B8 File Offset: 0x00000AB8
		internal override void ManualUpdate()
		{
			if (this._isFinished || this._lineRenderers == null || this._lineRenderers.Length == 0)
			{
				return;
			}
			this._leftTime += Time.deltaTime;
			Color endColor = this.LineRendererColorOverTime.Evaluate(this._leftTime / this.Duration);
			foreach (LineRenderer lineRenderer in this._lineRenderers)
			{
				if (lineRenderer != null)
				{
					lineRenderer.startColor = (lineRenderer.endColor = endColor);
				}
			}
			if (this._leftTime > this.Duration)
			{
				this._isFinished = true;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002953 File Offset: 0x00000B53
		private IEnumerator Initialize()
		{
			yield return null;
			foreach (Transform transform in this.EffectSettings.Targets)
			{
				Vector3 position = this.OverrideStartPositionToChainCenter ? base.transform.position : transform.position;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ChainPrefab, position, Quaternion.identity, base.transform);
				this._instances.Add(gameObject);
				MagicFX5_RopePhysics[] componentsInChildren = gameObject.GetComponentsInChildren<MagicFX5_RopePhysics>(true);
				this._ropes.AddRange(componentsInChildren);
				if (this.TargetMode == MagicFX5_ChainEffect.TargetModeEnum.UseRoot)
				{
					SkinnedMeshRenderer componentInChildren = transform.GetComponentInChildren<SkinnedMeshRenderer>();
					Transform transform2 = (componentInChildren != null) ? componentInChildren.rootBone : transform;
					MagicFX5_RopePhysics[] array = componentsInChildren;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].Target = transform2;
					}
					if (this.UpdateFollowTarget)
					{
						this.UpdateFollowTargetScripts(gameObject, transform2);
					}
				}
				if (this.TargetMode == MagicFX5_ChainEffect.TargetModeEnum.UseRandomBone)
				{
					Rigidbody[] componentsInChildren2 = transform.GetComponentsInChildren<Rigidbody>();
					MagicFX5_RopePhysics[] array = componentsInChildren;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].Target = ((componentsInChildren2 == null || componentsInChildren2.Length == 0) ? transform : componentsInChildren2[UnityEngine.Random.Range(0, componentsInChildren2.Length - 1)].transform);
					}
					if (this.UpdateFollowTarget)
					{
						this.UpdateFollowTargetScripts(gameObject, (componentsInChildren2 == null) ? transform : componentsInChildren2[UnityEngine.Random.Range(0, componentsInChildren2.Length - 1)].transform);
					}
				}
				if (this.ParticlesAtStartRopePositions != null)
				{
					this.InstantiateParticlesAtRopePosition(componentsInChildren);
				}
				UnityEngine.Object.Destroy(gameObject, this.DestroyTime);
			}
			this._lineRenderers = base.GetComponentsInChildren<LineRenderer>(true);
			Color endColor = this.LineRendererColorOverTime.Evaluate(0f);
			foreach (LineRenderer lineRenderer in this._lineRenderers)
			{
				lineRenderer.startColor = (lineRenderer.endColor = endColor);
			}
			yield break;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002964 File Offset: 0x00000B64
		private void InstantiateParticlesAtRopePosition(MagicFX5_RopePhysics[] ropes)
		{
			foreach (MagicFX5_RopePhysics magicFX5_RopePhysics in ropes)
			{
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				emitParams.position = magicFX5_RopePhysics.transform.position;
				emitParams.velocity = (magicFX5_RopePhysics.Target.position - magicFX5_RopePhysics.transform.position) * 1E-05f;
				this.ParticlesAtStartRopePositions.Emit(emitParams, 1);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000029D8 File Offset: 0x00000BD8
		private void UpdateFollowTargetScripts(GameObject instance, Transform currentTarget)
		{
			MagicFX5_FollowTarget componentInChildren = instance.GetComponentInChildren<MagicFX5_FollowTarget>(true);
			if (componentInChildren != null)
			{
				componentInChildren.Target = currentTarget;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000029FD File Offset: 0x00000BFD
		public MagicFX5_ChainEffect()
		{
		}

		// Token: 0x0400002C RID: 44
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x0400002D RID: 45
		public MagicFX5_ChainEffect.TargetModeEnum TargetMode;

		// Token: 0x0400002E RID: 46
		public bool OverrideStartPositionToChainCenter;

		// Token: 0x0400002F RID: 47
		public GameObject ChainPrefab;

		// Token: 0x04000030 RID: 48
		public float DestroyTime = 10f;

		// Token: 0x04000031 RID: 49
		[Space]
		public Gradient LineRendererColorOverTime;

		// Token: 0x04000032 RID: 50
		public float Duration = 6f;

		// Token: 0x04000033 RID: 51
		[Space]
		public ParticleSystem ParticlesAtStartRopePositions;

		// Token: 0x04000034 RID: 52
		public bool UpdateFollowTarget;

		// Token: 0x04000035 RID: 53
		private float _leftTime;

		// Token: 0x04000036 RID: 54
		private bool _isFinished;

		// Token: 0x04000037 RID: 55
		private LineRenderer[] _lineRenderers;

		// Token: 0x04000038 RID: 56
		private List<MagicFX5_RopePhysics> _ropes = new List<MagicFX5_RopePhysics>();

		// Token: 0x04000039 RID: 57
		private List<GameObject> _instances = new List<GameObject>();

		// Token: 0x0200002A RID: 42
		public enum TargetModeEnum
		{
			// Token: 0x04000150 RID: 336
			UseRoot,
			// Token: 0x04000151 RID: 337
			UseRandomBone
		}

		// Token: 0x0200002B RID: 43
		[CompilerGenerated]
		private sealed class <Initialize>d__18 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000D0 RID: 208 RVA: 0x000068E7 File Offset: 0x00004AE7
			[DebuggerHidden]
			public <Initialize>d__18(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x000068F6 File Offset: 0x00004AF6
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x000068F8 File Offset: 0x00004AF8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MagicFX5_ChainEffect magicFX5_ChainEffect = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				foreach (Transform transform in magicFX5_ChainEffect.EffectSettings.Targets)
				{
					Vector3 position = magicFX5_ChainEffect.OverrideStartPositionToChainCenter ? magicFX5_ChainEffect.transform.position : transform.position;
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(magicFX5_ChainEffect.ChainPrefab, position, Quaternion.identity, magicFX5_ChainEffect.transform);
					magicFX5_ChainEffect._instances.Add(gameObject);
					MagicFX5_RopePhysics[] componentsInChildren = gameObject.GetComponentsInChildren<MagicFX5_RopePhysics>(true);
					magicFX5_ChainEffect._ropes.AddRange(componentsInChildren);
					if (magicFX5_ChainEffect.TargetMode == MagicFX5_ChainEffect.TargetModeEnum.UseRoot)
					{
						SkinnedMeshRenderer componentInChildren = transform.GetComponentInChildren<SkinnedMeshRenderer>();
						Transform transform2 = (componentInChildren != null) ? componentInChildren.rootBone : transform;
						MagicFX5_RopePhysics[] array = componentsInChildren;
						for (int j = 0; j < array.Length; j++)
						{
							array[j].Target = transform2;
						}
						if (magicFX5_ChainEffect.UpdateFollowTarget)
						{
							magicFX5_ChainEffect.UpdateFollowTargetScripts(gameObject, transform2);
						}
					}
					if (magicFX5_ChainEffect.TargetMode == MagicFX5_ChainEffect.TargetModeEnum.UseRandomBone)
					{
						Rigidbody[] componentsInChildren2 = transform.GetComponentsInChildren<Rigidbody>();
						MagicFX5_RopePhysics[] array = componentsInChildren;
						for (int j = 0; j < array.Length; j++)
						{
							array[j].Target = ((componentsInChildren2 == null || componentsInChildren2.Length == 0) ? transform : componentsInChildren2[UnityEngine.Random.Range(0, componentsInChildren2.Length - 1)].transform);
						}
						if (magicFX5_ChainEffect.UpdateFollowTarget)
						{
							magicFX5_ChainEffect.UpdateFollowTargetScripts(gameObject, (componentsInChildren2 == null) ? transform : componentsInChildren2[UnityEngine.Random.Range(0, componentsInChildren2.Length - 1)].transform);
						}
					}
					if (magicFX5_ChainEffect.ParticlesAtStartRopePositions != null)
					{
						magicFX5_ChainEffect.InstantiateParticlesAtRopePosition(componentsInChildren);
					}
					UnityEngine.Object.Destroy(gameObject, magicFX5_ChainEffect.DestroyTime);
				}
				magicFX5_ChainEffect._lineRenderers = magicFX5_ChainEffect.GetComponentsInChildren<LineRenderer>(true);
				Color endColor = magicFX5_ChainEffect.LineRendererColorOverTime.Evaluate(0f);
				foreach (LineRenderer lineRenderer in magicFX5_ChainEffect._lineRenderers)
				{
					lineRenderer.startColor = (lineRenderer.endColor = endColor);
				}
				return false;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006B10 File Offset: 0x00004D10
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00006B18 File Offset: 0x00004D18
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x060000D5 RID: 213 RVA: 0x00006B1F File Offset: 0x00004D1F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000152 RID: 338
			private int <>1__state;

			// Token: 0x04000153 RID: 339
			private object <>2__current;

			// Token: 0x04000154 RID: 340
			public MagicFX5_ChainEffect <>4__this;
		}
	}
}
