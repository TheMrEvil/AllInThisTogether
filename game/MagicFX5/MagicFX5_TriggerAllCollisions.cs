using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000025 RID: 37
	public class MagicFX5_TriggerAllCollisions : MonoBehaviour
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x000067F8 File Offset: 0x000049F8
		private void OnEnable()
		{
			this._waitTime = new WaitForSeconds(this.DelayBetweenCollisions);
			base.StartCoroutine(this.LateInitialize());
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006818 File Offset: 0x00004A18
		private void OnDisable()
		{
			base.StopCoroutine(this.LateInitialize());
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006826 File Offset: 0x00004A26
		private IEnumerator LateInitialize()
		{
			yield return null;
			yield return new WaitForSeconds(this.Delay);
			foreach (Transform transform in this.EffectSettings.Targets)
			{
				Vector3 position = transform.position;
				Vector3 normalized = (position - base.transform.position).normalized;
				MagicFX5_EffectSettings.EffectCollisionHit obj = new MagicFX5_EffectSettings.EffectCollisionHit
				{
					Target = transform,
					Position = position,
					Normal = normalized
				};
				switch (this.TriggerType)
				{
				case MagicFX5_TriggerAllCollisions.TriggerTypeEnum.All:
				{
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
					break;
				}
				case MagicFX5_TriggerAllCollisions.TriggerTypeEnum.CollisionEnter:
				{
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter2 = this.EffectSettings.OnEffectCollisionEnter;
					if (onEffectCollisionEnter2 != null)
					{
						onEffectCollisionEnter2(obj);
					}
					break;
				}
				case MagicFX5_TriggerAllCollisions.TriggerTypeEnum.SkinEnter:
				{
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectSkinActivated2 = this.EffectSettings.OnEffectSkinActivated;
					if (onEffectSkinActivated2 != null)
					{
						onEffectSkinActivated2(obj);
					}
					break;
				}
				case MagicFX5_TriggerAllCollisions.TriggerTypeEnum.ImpactEnter:
				{
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectImpactActivated2 = this.EffectSettings.OnEffectImpactActivated;
					if (onEffectImpactActivated2 != null)
					{
						onEffectImpactActivated2(obj);
					}
					break;
				}
				}
				yield return this._waitTime;
			}
			Transform[] array = null;
			yield break;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006835 File Offset: 0x00004A35
		public MagicFX5_TriggerAllCollisions()
		{
		}

		// Token: 0x0400013E RID: 318
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x0400013F RID: 319
		public MagicFX5_TriggerAllCollisions.TriggerTypeEnum TriggerType;

		// Token: 0x04000140 RID: 320
		public float Delay = 1f;

		// Token: 0x04000141 RID: 321
		public float DelayBetweenCollisions;

		// Token: 0x04000142 RID: 322
		private WaitForSeconds _waitTime;

		// Token: 0x0200003E RID: 62
		public enum TriggerTypeEnum
		{
			// Token: 0x040001A8 RID: 424
			All,
			// Token: 0x040001A9 RID: 425
			CollisionEnter,
			// Token: 0x040001AA RID: 426
			SkinEnter,
			// Token: 0x040001AB RID: 427
			ImpactEnter
		}

		// Token: 0x0200003F RID: 63
		[CompilerGenerated]
		private sealed class <LateInitialize>d__8 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060000FC RID: 252 RVA: 0x00007AC0 File Offset: 0x00005CC0
			[DebuggerHidden]
			public <LateInitialize>d__8(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060000FD RID: 253 RVA: 0x00007ACF File Offset: 0x00005CCF
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060000FE RID: 254 RVA: 0x00007AD4 File Offset: 0x00005CD4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MagicFX5_TriggerAllCollisions magicFX5_TriggerAllCollisions = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					this.<>2__current = new WaitForSeconds(magicFX5_TriggerAllCollisions.Delay);
					this.<>1__state = 2;
					return true;
				case 2:
					this.<>1__state = -1;
					array = magicFX5_TriggerAllCollisions.EffectSettings.Targets;
					i = 0;
					break;
				case 3:
					this.<>1__state = -1;
					i++;
					break;
				default:
					return false;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				Transform transform = array[i];
				Vector3 position = transform.position;
				Vector3 normalized = (position - magicFX5_TriggerAllCollisions.transform.position).normalized;
				MagicFX5_EffectSettings.EffectCollisionHit obj = new MagicFX5_EffectSettings.EffectCollisionHit
				{
					Target = transform,
					Position = position,
					Normal = normalized
				};
				switch (magicFX5_TriggerAllCollisions.TriggerType)
				{
				case MagicFX5_TriggerAllCollisions.TriggerTypeEnum.All:
				{
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter = magicFX5_TriggerAllCollisions.EffectSettings.OnEffectCollisionEnter;
					if (onEffectCollisionEnter != null)
					{
						onEffectCollisionEnter(obj);
					}
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectSkinActivated = magicFX5_TriggerAllCollisions.EffectSettings.OnEffectSkinActivated;
					if (onEffectSkinActivated != null)
					{
						onEffectSkinActivated(obj);
					}
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectImpactActivated = magicFX5_TriggerAllCollisions.EffectSettings.OnEffectImpactActivated;
					if (onEffectImpactActivated != null)
					{
						onEffectImpactActivated(obj);
					}
					break;
				}
				case MagicFX5_TriggerAllCollisions.TriggerTypeEnum.CollisionEnter:
				{
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter2 = magicFX5_TriggerAllCollisions.EffectSettings.OnEffectCollisionEnter;
					if (onEffectCollisionEnter2 != null)
					{
						onEffectCollisionEnter2(obj);
					}
					break;
				}
				case MagicFX5_TriggerAllCollisions.TriggerTypeEnum.SkinEnter:
				{
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectSkinActivated2 = magicFX5_TriggerAllCollisions.EffectSettings.OnEffectSkinActivated;
					if (onEffectSkinActivated2 != null)
					{
						onEffectSkinActivated2(obj);
					}
					break;
				}
				case MagicFX5_TriggerAllCollisions.TriggerTypeEnum.ImpactEnter:
				{
					Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectImpactActivated2 = magicFX5_TriggerAllCollisions.EffectSettings.OnEffectImpactActivated;
					if (onEffectImpactActivated2 != null)
					{
						onEffectImpactActivated2(obj);
					}
					break;
				}
				}
				this.<>2__current = magicFX5_TriggerAllCollisions._waitTime;
				this.<>1__state = 3;
				return true;
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x060000FF RID: 255 RVA: 0x00007CB8 File Offset: 0x00005EB8
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000100 RID: 256 RVA: 0x00007CC0 File Offset: 0x00005EC0
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00007CC7 File Offset: 0x00005EC7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040001AC RID: 428
			private int <>1__state;

			// Token: 0x040001AD RID: 429
			private object <>2__current;

			// Token: 0x040001AE RID: 430
			public MagicFX5_TriggerAllCollisions <>4__this;

			// Token: 0x040001AF RID: 431
			private Transform[] <>7__wrap1;

			// Token: 0x040001B0 RID: 432
			private int <>7__wrap2;
		}
	}
}
