using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000015 RID: 21
	public class MagicFX5_ParticleSystemChainLighting : MonoBehaviour
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000042B9 File Offset: 0x000024B9
		private void OnEnable()
		{
			base.transform.localPosition = Vector3.zero;
			this._currentTargetIndex = 0;
			this._leftTime = 0f;
			this.IsFinishedJump = true;
			base.StartCoroutine(this.MoveToNextTarget());
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000042F1 File Offset: 0x000024F1
		private void OnDisable()
		{
			base.StopCoroutine(this.MoveToNextTarget());
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000042FF File Offset: 0x000024FF
		private IEnumerator MoveToNextTarget()
		{
			yield return null;
			Transform[] targets = this.EffectSettings.Targets;
			if (this.EffectSettings.Targets.Length == 0)
			{
				Debug.LogError("You must set targets in the EffectSettings script!");
				yield break;
			}
			while (this._currentTargetIndex < targets.Length)
			{
				if (this.IsFinishedJump)
				{
					this.IsFinishedJump = false;
					this._leftTime = 0f;
					Transform transform = targets[this._currentTargetIndex];
					if (transform != null)
					{
						Vector3 footPos = Vector3.zero;
						Vector3 targetCenter = this.EffectSettings.GetTargetCenter(transform);
						MagicFX5_ParticleSystemChainLighting.MoveModeEnum moveMode = this.MoveMode;
						if (moveMode != MagicFX5_ParticleSystemChainLighting.MoveModeEnum.Parabolic)
						{
							if (moveMode == MagicFX5_ParticleSystemChainLighting.MoveModeEnum.Straight)
							{
								footPos = targetCenter;
							}
						}
						else
						{
							footPos = transform.position;
						}
						Vector3 normalized = (footPos - base.transform.position).normalized;
						Vector3 endPoint = footPos + normalized * this.TargetBehindOffsetMeters;
						Vector3 apex = targetCenter;
						float triggerTime = this.CalculateTriggerTime(apex, endPoint);
						if (this.ChainTrailEffect != null)
						{
							this._currentChainInstance = UnityEngine.Object.Instantiate<GameObject>(this.ChainTrailEffect, base.transform.position, base.transform.rotation, this.EffectSettings.transform);
							UnityEngine.Object.Destroy(this._currentChainInstance, this.ChainTrailEffectDestroyTime);
						}
						if (this.StartChainImpactEffect != null)
						{
							UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate<GameObject>(this.StartChainImpactEffect, base.transform.position, base.transform.rotation, this.EffectSettings.transform), this.StartChainImpactEffectDestroyTime);
						}
						this._currentTargetImpactEffectsPerChain = 0;
						this._startJumpPosition = base.transform.position;
						while (!this.IsFinishedJump)
						{
							this._leftTime += Time.deltaTime;
							float num = (this.AnimationTimeToNextJump > 1E-05f) ? (this._leftTime / this.AnimationTimeToNextJump) : 1f;
							moveMode = this.MoveMode;
							if (moveMode != MagicFX5_ParticleSystemChainLighting.MoveModeEnum.Parabolic)
							{
								if (moveMode == MagicFX5_ParticleSystemChainLighting.MoveModeEnum.Straight)
								{
									base.transform.position = Vector3.Lerp(this._startJumpPosition, footPos, num);
								}
							}
							else
							{
								base.transform.position = this.ParabolicLerp(this._startJumpPosition, apex, endPoint, num);
							}
							if (this._currentChainInstance != null)
							{
								this._currentChainInstance.transform.position = base.transform.position;
							}
							if (this._currentTargetImpactEffectsPerChain < 1 && num >= triggerTime)
							{
								this._currentTargetImpactEffectsPerChain++;
								Vector3 normalized2 = (this._startJumpPosition - base.transform.position).normalized;
								MagicFX5_EffectSettings.EffectCollisionHit obj = new MagicFX5_EffectSettings.EffectCollisionHit
								{
									Normal = normalized2,
									Position = apex,
									Target = this.EffectSettings.Targets[this._currentTargetIndex]
								};
								Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter = this.EffectSettings.OnEffectCollisionEnter;
								if (onEffectCollisionEnter != null)
								{
									onEffectCollisionEnter(obj);
								}
								Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectSkinActivated = this.EffectSettings.OnEffectSkinActivated;
								if (onEffectSkinActivated != null)
								{
									onEffectSkinActivated(obj);
								}
								Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectImpactActivated = this.EffectSettings.OnEffectImpactActivated;
								if (onEffectImpactActivated != null)
								{
									onEffectImpactActivated(obj);
								}
							}
							if (this._leftTime >= this.AnimationTimeToNextJump)
							{
								this.IsFinishedJump = true;
								this._currentTargetIndex++;
							}
							yield return null;
						}
						footPos = default(Vector3);
						endPoint = default(Vector3);
						apex = default(Vector3);
					}
				}
			}
			yield break;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004310 File Offset: 0x00002510
		private float CalculateTriggerTime(Vector3 apex, Vector3 endPoint)
		{
			float num = Vector3.Distance(base.transform.position, apex);
			float num2 = Vector3.Distance(base.transform.position, endPoint);
			return num / num2;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004344 File Offset: 0x00002544
		private Vector3 ParabolicLerp(Vector3 start, Vector3 apex, Vector3 end, float t)
		{
			float x = start.x;
			float y = start.y;
			float x2 = apex.x;
			float y2 = apex.y;
			float x3 = end.x;
			float num = (end.y - y - (y2 - y) * (x3 - x) / (x2 - x)) / (x3 * x3 - x * x - (x2 * x2 - x * x) * (x3 - x) / (x2 - x));
			float num2 = (y2 - y - num * (x2 * x2 - x * x)) / (x2 - x);
			float num3 = y - num * x * x - num2 * x;
			float num4 = Mathf.Lerp(start.x, end.x, t);
			float num5 = num * num4 * num4 + num2 * num4 + num3;
			float z = Mathf.Lerp(start.z, end.z, t);
			if (float.IsNaN(num5))
			{
				num5 = y;
			}
			return new Vector3(num4, num5, z);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000441E File Offset: 0x0000261E
		public MagicFX5_ParticleSystemChainLighting()
		{
		}

		// Token: 0x04000090 RID: 144
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x04000091 RID: 145
		public float TargetBehindOffsetMeters = 2f;

		// Token: 0x04000092 RID: 146
		public GameObject ChainTrailEffect;

		// Token: 0x04000093 RID: 147
		public float ChainTrailEffectDestroyTime = 5f;

		// Token: 0x04000094 RID: 148
		public GameObject StartChainImpactEffect;

		// Token: 0x04000095 RID: 149
		public float StartChainImpactEffectDestroyTime = 5f;

		// Token: 0x04000096 RID: 150
		public float ColliderSize = 0.25f;

		// Token: 0x04000097 RID: 151
		public float AnimationTimeToNextJump = 0.17f;

		// Token: 0x04000098 RID: 152
		public MagicFX5_ParticleSystemChainLighting.MoveModeEnum MoveMode;

		// Token: 0x04000099 RID: 153
		internal bool IsFinishedJump;

		// Token: 0x0400009A RID: 154
		private int _currentTargetIndex;

		// Token: 0x0400009B RID: 155
		private GameObject _currentChainInstance;

		// Token: 0x0400009C RID: 156
		private int _currentTargetImpactEffectsPerChain;

		// Token: 0x0400009D RID: 157
		private const int MaxTargetImpactEffectsPerChain = 1;

		// Token: 0x0400009E RID: 158
		private Vector3 _startJumpPosition;

		// Token: 0x0400009F RID: 159
		private float _leftTime;

		// Token: 0x02000030 RID: 48
		public enum MoveModeEnum
		{
			// Token: 0x0400016D RID: 365
			Parabolic,
			// Token: 0x0400016E RID: 366
			Straight
		}

		// Token: 0x02000031 RID: 49
		[CompilerGenerated]
		private sealed class <MoveToNextTarget>d__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000D9 RID: 217 RVA: 0x00006D9E File Offset: 0x00004F9E
			[DebuggerHidden]
			public <MoveToNextTarget>d__19(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00006DAD File Offset: 0x00004FAD
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00006DB0 File Offset: 0x00004FB0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MagicFX5_ParticleSystemChainLighting magicFX5_ParticleSystemChainLighting = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					targets = magicFX5_ParticleSystemChainLighting.EffectSettings.Targets;
					if (magicFX5_ParticleSystemChainLighting.EffectSettings.Targets.Length == 0)
					{
						Debug.LogError("You must set targets in the EffectSettings script!");
						return false;
					}
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_3B1;
				default:
					return false;
				}
				IL_3E0:
				MagicFX5_ParticleSystemChainLighting.MoveModeEnum moveMode;
				while (magicFX5_ParticleSystemChainLighting._currentTargetIndex < targets.Length)
				{
					if (magicFX5_ParticleSystemChainLighting.IsFinishedJump)
					{
						magicFX5_ParticleSystemChainLighting.IsFinishedJump = false;
						magicFX5_ParticleSystemChainLighting._leftTime = 0f;
						Transform transform = targets[magicFX5_ParticleSystemChainLighting._currentTargetIndex];
						if (transform != null)
						{
							footPos = Vector3.zero;
							Vector3 targetCenter = magicFX5_ParticleSystemChainLighting.EffectSettings.GetTargetCenter(transform);
							moveMode = magicFX5_ParticleSystemChainLighting.MoveMode;
							if (moveMode != MagicFX5_ParticleSystemChainLighting.MoveModeEnum.Parabolic)
							{
								if (moveMode == MagicFX5_ParticleSystemChainLighting.MoveModeEnum.Straight)
								{
									footPos = targetCenter;
								}
							}
							else
							{
								footPos = transform.position;
							}
							Vector3 normalized = (footPos - magicFX5_ParticleSystemChainLighting.transform.position).normalized;
							endPoint = footPos + normalized * magicFX5_ParticleSystemChainLighting.TargetBehindOffsetMeters;
							apex = targetCenter;
							triggerTime = magicFX5_ParticleSystemChainLighting.CalculateTriggerTime(apex, endPoint);
							if (magicFX5_ParticleSystemChainLighting.ChainTrailEffect != null)
							{
								magicFX5_ParticleSystemChainLighting._currentChainInstance = UnityEngine.Object.Instantiate<GameObject>(magicFX5_ParticleSystemChainLighting.ChainTrailEffect, magicFX5_ParticleSystemChainLighting.transform.position, magicFX5_ParticleSystemChainLighting.transform.rotation, magicFX5_ParticleSystemChainLighting.EffectSettings.transform);
								UnityEngine.Object.Destroy(magicFX5_ParticleSystemChainLighting._currentChainInstance, magicFX5_ParticleSystemChainLighting.ChainTrailEffectDestroyTime);
							}
							if (magicFX5_ParticleSystemChainLighting.StartChainImpactEffect != null)
							{
								UnityEngine.Object.Destroy(UnityEngine.Object.Instantiate<GameObject>(magicFX5_ParticleSystemChainLighting.StartChainImpactEffect, magicFX5_ParticleSystemChainLighting.transform.position, magicFX5_ParticleSystemChainLighting.transform.rotation, magicFX5_ParticleSystemChainLighting.EffectSettings.transform), magicFX5_ParticleSystemChainLighting.StartChainImpactEffectDestroyTime);
							}
							magicFX5_ParticleSystemChainLighting._currentTargetImpactEffectsPerChain = 0;
							magicFX5_ParticleSystemChainLighting._startJumpPosition = magicFX5_ParticleSystemChainLighting.transform.position;
							goto IL_3B1;
						}
					}
				}
				return false;
				IL_3B1:
				if (magicFX5_ParticleSystemChainLighting.IsFinishedJump)
				{
					footPos = default(Vector3);
					endPoint = default(Vector3);
					apex = default(Vector3);
					goto IL_3E0;
				}
				magicFX5_ParticleSystemChainLighting._leftTime += Time.deltaTime;
				float num2 = (magicFX5_ParticleSystemChainLighting.AnimationTimeToNextJump > 1E-05f) ? (magicFX5_ParticleSystemChainLighting._leftTime / magicFX5_ParticleSystemChainLighting.AnimationTimeToNextJump) : 1f;
				moveMode = magicFX5_ParticleSystemChainLighting.MoveMode;
				if (moveMode != MagicFX5_ParticleSystemChainLighting.MoveModeEnum.Parabolic)
				{
					if (moveMode == MagicFX5_ParticleSystemChainLighting.MoveModeEnum.Straight)
					{
						magicFX5_ParticleSystemChainLighting.transform.position = Vector3.Lerp(magicFX5_ParticleSystemChainLighting._startJumpPosition, footPos, num2);
					}
				}
				else
				{
					magicFX5_ParticleSystemChainLighting.transform.position = magicFX5_ParticleSystemChainLighting.ParabolicLerp(magicFX5_ParticleSystemChainLighting._startJumpPosition, apex, endPoint, num2);
				}
				if (magicFX5_ParticleSystemChainLighting._currentChainInstance != null)
				{
					magicFX5_ParticleSystemChainLighting._currentChainInstance.transform.position = magicFX5_ParticleSystemChainLighting.transform.position;
				}
				if (magicFX5_ParticleSystemChainLighting._currentTargetImpactEffectsPerChain < 1 && num2 >= triggerTime)
				{
					magicFX5_ParticleSystemChainLighting._currentTargetImpactEffectsPerChain++;
					Vector3 normalized2 = (magicFX5_ParticleSystemChainLighting._startJumpPosition - magicFX5_ParticleSystemChainLighting.transform.position).normalized;
					MagicFX5_EffectSettings.EffectCollisionHit obj = new MagicFX5_EffectSettings.EffectCollisionHit
					{
						Normal = normalized2,
						Position = apex,
						Target = magicFX5_ParticleSystemChainLighting.EffectSettings.Targets[magicFX5_ParticleSystemChainLighting._currentTargetIndex]
					};
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter = magicFX5_ParticleSystemChainLighting.EffectSettings.OnEffectCollisionEnter;
					if (onEffectCollisionEnter != null)
					{
						onEffectCollisionEnter(obj);
					}
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectSkinActivated = magicFX5_ParticleSystemChainLighting.EffectSettings.OnEffectSkinActivated;
					if (onEffectSkinActivated != null)
					{
						onEffectSkinActivated(obj);
					}
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectImpactActivated = magicFX5_ParticleSystemChainLighting.EffectSettings.OnEffectImpactActivated;
					if (onEffectImpactActivated != null)
					{
						onEffectImpactActivated(obj);
					}
				}
				if (magicFX5_ParticleSystemChainLighting._leftTime >= magicFX5_ParticleSystemChainLighting.AnimationTimeToNextJump)
				{
					magicFX5_ParticleSystemChainLighting.IsFinishedJump = true;
					magicFX5_ParticleSystemChainLighting._currentTargetIndex++;
				}
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x060000DC RID: 220 RVA: 0x000071B1 File Offset: 0x000053B1
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060000DD RID: 221 RVA: 0x000071B9 File Offset: 0x000053B9
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x060000DE RID: 222 RVA: 0x000071C0 File Offset: 0x000053C0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400016F RID: 367
			private int <>1__state;

			// Token: 0x04000170 RID: 368
			private object <>2__current;

			// Token: 0x04000171 RID: 369
			public MagicFX5_ParticleSystemChainLighting <>4__this;

			// Token: 0x04000172 RID: 370
			private Transform[] <targets>5__2;

			// Token: 0x04000173 RID: 371
			private Vector3 <footPos>5__3;

			// Token: 0x04000174 RID: 372
			private Vector3 <endPoint>5__4;

			// Token: 0x04000175 RID: 373
			private Vector3 <apex>5__5;

			// Token: 0x04000176 RID: 374
			private float <triggerTime>5__6;
		}
	}
}
