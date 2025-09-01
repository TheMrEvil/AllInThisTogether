using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000021 RID: 33
	public class MagicFX5_Spawner : MonoBehaviour
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00005E85 File Offset: 0x00004085
		private void OnEnable()
		{
			base.StartCoroutine(this.CoroutineLoop());
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005E94 File Offset: 0x00004094
		private void OnDisable()
		{
			base.StopCoroutine(this.CoroutineLoop());
			foreach (GameObject gameObject in this._instances)
			{
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005EFC File Offset: 0x000040FC
		private IEnumerator CoroutineLoop()
		{
			yield return null;
			this._instances.Clear();
			this._leftTime = 0f;
			this._targetIndex = 0;
			float percent = this.Interval * (this.IntervalRandomPercent / 100f);
			for (;;)
			{
				if (this.SpawnMode == MagicFX5_Spawner.SpawnModeEnum.Interval)
				{
					if (this._leftTime >= this.Duration)
					{
						break;
					}
				}
				else if (this.SpawnMode != MagicFX5_Spawner.SpawnModeEnum.TargetsCount || this._targetIndex >= this.EffectSettings.Targets.Length)
				{
					break;
				}
				float num = this.Interval + UnityEngine.Random.Range(-percent, percent);
				this._leftTime += num;
				Vector3 b = this.OnUnitSphere ? this.GetRandomPositionOnCircle() : Vector3.Scale(UnityEngine.Random.insideUnitSphere, this.SpawnRadius);
				Vector3 vector = base.transform.position + b;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SpawnedObject, vector, base.transform.rotation, base.transform);
				this._instances.Add(gameObject);
				UnityEngine.Object.Destroy(gameObject, this.InstanceLifeTime);
				MagicFX5_EffectSettings component = gameObject.GetComponent<MagicFX5_EffectSettings>();
				if (this.TargetMode != MagicFX5_Spawner.TargetModeEnum.None)
				{
					Transform transform = null;
					switch (this.TargetMode)
					{
					case MagicFX5_Spawner.TargetModeEnum.NearTarget:
						transform = this.FindNearTarget(this.EffectSettings.Targets, vector);
						break;
					case MagicFX5_Spawner.TargetModeEnum.TargetRoot:
					{
						Transform[] targets = this.EffectSettings.Targets;
						int targetIndex = this._targetIndex;
						this._targetIndex = targetIndex + 1;
						transform = this.FindTargetRoot(targets[targetIndex]);
						break;
					}
					case MagicFX5_Spawner.TargetModeEnum.TransformCenter:
						transform = base.transform;
						break;
					}
					Vector3 forward = transform.position - vector;
					forward.y = vector.y;
					Quaternion rotation = Quaternion.LookRotation(forward);
					gameObject.transform.SetPositionAndRotation(vector, rotation);
					component.Targets = new Transform[]
					{
						transform
					};
				}
				if (component != null)
				{
					MagicFX5_EffectSettings magicFX5_EffectSettings = component;
					magicFX5_EffectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(magicFX5_EffectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(delegate(MagicFX5_EffectSettings.EffectCollisionHit hit)
					{
						Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter = this.EffectSettings.OnEffectCollisionEnter;
						if (onEffectCollisionEnter == null)
						{
							return;
						}
						onEffectCollisionEnter(hit);
					}));
				}
				yield return new WaitForSeconds(num);
			}
			yield break;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005F0C File Offset: 0x0000410C
		public Vector3 GetRandomPositionOnCircle()
		{
			float f = UnityEngine.Random.Range(0f, 6.2831855f);
			float x = Mathf.Cos(f) * this.SpawnRadius.x;
			float z = Mathf.Sin(f) * this.SpawnRadius.z;
			Vector3 b = Vector3.Scale(UnityEngine.Random.insideUnitSphere, this.SpawnRadius * (this.SpawnRandomPercent / 100f));
			return new Vector3(x, 0f, z) + b;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005F84 File Offset: 0x00004184
		private Transform FindNearTarget(Transform[] targets, Vector3 currentPos)
		{
			float num = float.MaxValue;
			Transform result = base.transform;
			foreach (Transform transform in targets)
			{
				float sqrMagnitude = (transform.position - currentPos).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					result = transform;
				}
			}
			return result;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005FD8 File Offset: 0x000041D8
		private Transform FindTargetRoot(Transform target)
		{
			SkinnedMeshRenderer componentInChildren = target.GetComponentInChildren<SkinnedMeshRenderer>();
			if (!(componentInChildren != null))
			{
				return target;
			}
			return componentInChildren.rootBone;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005FFD File Offset: 0x000041FD
		private void OnDrawGizmosSelected()
		{
			Gizmos.matrix = Matrix4x4.TRS(base.transform.position, Quaternion.identity, this.SpawnRadius);
			Gizmos.DrawWireSphere(Vector3.zero, 1f);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006030 File Offset: 0x00004230
		public MagicFX5_Spawner()
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000060A6 File Offset: 0x000042A6
		[CompilerGenerated]
		private void <CoroutineLoop>b__18_0(MagicFX5_EffectSettings.EffectCollisionHit hit)
		{
			Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter = this.EffectSettings.OnEffectCollisionEnter;
			if (onEffectCollisionEnter == null)
			{
				return;
			}
			onEffectCollisionEnter(hit);
		}

		// Token: 0x0400010C RID: 268
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x0400010D RID: 269
		public GameObject SpawnedObject;

		// Token: 0x0400010E RID: 270
		public MagicFX5_Spawner.SpawnModeEnum SpawnMode;

		// Token: 0x0400010F RID: 271
		public MagicFX5_Spawner.TargetModeEnum TargetMode;

		// Token: 0x04000110 RID: 272
		public float InstanceLifeTime = 8f;

		// Token: 0x04000111 RID: 273
		[Space]
		public Vector3 SpawnRadius = new Vector3(5f, 0f, 5f);

		// Token: 0x04000112 RID: 274
		public bool OnUnitSphere = true;

		// Token: 0x04000113 RID: 275
		[Range(0f, 100f)]
		public float SpawnRandomPercent = 25f;

		// Token: 0x04000114 RID: 276
		public float Duration = 4f;

		// Token: 0x04000115 RID: 277
		public float Interval = 0.5f;

		// Token: 0x04000116 RID: 278
		[Range(0f, 100f)]
		public float IntervalRandomPercent = 25f;

		// Token: 0x04000117 RID: 279
		private float _leftTime;

		// Token: 0x04000118 RID: 280
		private int _targetIndex;

		// Token: 0x04000119 RID: 281
		private List<GameObject> _instances = new List<GameObject>();

		// Token: 0x02000039 RID: 57
		public enum SpawnModeEnum
		{
			// Token: 0x04000192 RID: 402
			Interval,
			// Token: 0x04000193 RID: 403
			TargetsCount
		}

		// Token: 0x0200003A RID: 58
		public enum TargetModeEnum
		{
			// Token: 0x04000195 RID: 405
			None,
			// Token: 0x04000196 RID: 406
			NearTarget,
			// Token: 0x04000197 RID: 407
			TargetRoot,
			// Token: 0x04000198 RID: 408
			TransformCenter
		}

		// Token: 0x0200003B RID: 59
		[CompilerGenerated]
		private sealed class <CoroutineLoop>d__18 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000F0 RID: 240 RVA: 0x00007613 File Offset: 0x00005813
			[DebuggerHidden]
			public <CoroutineLoop>d__18(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000F1 RID: 241 RVA: 0x00007622 File Offset: 0x00005822
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x00007624 File Offset: 0x00005824
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MagicFX5_Spawner magicFX5_Spawner = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					magicFX5_Spawner._instances.Clear();
					magicFX5_Spawner._leftTime = 0f;
					magicFX5_Spawner._targetIndex = 0;
					percent = magicFX5_Spawner.Interval * (magicFX5_Spawner.IntervalRandomPercent / 100f);
					break;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				if (magicFX5_Spawner.SpawnMode == MagicFX5_Spawner.SpawnModeEnum.Interval)
				{
					if (magicFX5_Spawner._leftTime >= magicFX5_Spawner.Duration)
					{
						return false;
					}
				}
				else if (magicFX5_Spawner.SpawnMode != MagicFX5_Spawner.SpawnModeEnum.TargetsCount || magicFX5_Spawner._targetIndex >= magicFX5_Spawner.EffectSettings.Targets.Length)
				{
					return false;
				}
				float num2 = magicFX5_Spawner.Interval + UnityEngine.Random.Range(-percent, percent);
				magicFX5_Spawner._leftTime += num2;
				Vector3 b = magicFX5_Spawner.OnUnitSphere ? magicFX5_Spawner.GetRandomPositionOnCircle() : Vector3.Scale(UnityEngine.Random.insideUnitSphere, magicFX5_Spawner.SpawnRadius);
				Vector3 vector = magicFX5_Spawner.transform.position + b;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(magicFX5_Spawner.SpawnedObject, vector, magicFX5_Spawner.transform.rotation, magicFX5_Spawner.transform);
				magicFX5_Spawner._instances.Add(gameObject);
				UnityEngine.Object.Destroy(gameObject, magicFX5_Spawner.InstanceLifeTime);
				MagicFX5_EffectSettings component = gameObject.GetComponent<MagicFX5_EffectSettings>();
				if (magicFX5_Spawner.TargetMode != MagicFX5_Spawner.TargetModeEnum.None)
				{
					Transform transform = null;
					switch (magicFX5_Spawner.TargetMode)
					{
					case MagicFX5_Spawner.TargetModeEnum.NearTarget:
						transform = magicFX5_Spawner.FindNearTarget(magicFX5_Spawner.EffectSettings.Targets, vector);
						break;
					case MagicFX5_Spawner.TargetModeEnum.TargetRoot:
					{
						MagicFX5_Spawner magicFX5_Spawner2 = magicFX5_Spawner;
						Transform[] targets = magicFX5_Spawner.EffectSettings.Targets;
						MagicFX5_Spawner magicFX5_Spawner3 = magicFX5_Spawner;
						int targetIndex = magicFX5_Spawner._targetIndex;
						magicFX5_Spawner3._targetIndex = targetIndex + 1;
						transform = magicFX5_Spawner2.FindTargetRoot(targets[targetIndex]);
						break;
					}
					case MagicFX5_Spawner.TargetModeEnum.TransformCenter:
						transform = magicFX5_Spawner.transform;
						break;
					}
					Vector3 forward = transform.position - vector;
					forward.y = vector.y;
					Quaternion rotation = Quaternion.LookRotation(forward);
					gameObject.transform.SetPositionAndRotation(vector, rotation);
					component.Targets = new Transform[]
					{
						transform
					};
				}
				if (component != null)
				{
					MagicFX5_EffectSettings magicFX5_EffectSettings = component;
					magicFX5_EffectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(magicFX5_EffectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(delegate(MagicFX5_EffectSettings.EffectCollisionHit hit)
					{
						Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter = magicFX5_Spawner.EffectSettings.OnEffectCollisionEnter;
						if (onEffectCollisionEnter == null)
						{
							return;
						}
						onEffectCollisionEnter(hit);
					}));
				}
				this.<>2__current = new WaitForSeconds(num2);
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x060000F3 RID: 243 RVA: 0x00007893 File Offset: 0x00005A93
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x0000789B File Offset: 0x00005A9B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x000078A2 File Offset: 0x00005AA2
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000199 RID: 409
			private int <>1__state;

			// Token: 0x0400019A RID: 410
			private object <>2__current;

			// Token: 0x0400019B RID: 411
			public MagicFX5_Spawner <>4__this;

			// Token: 0x0400019C RID: 412
			private float <percent>5__2;
		}
	}
}
