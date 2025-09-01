using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200002A RID: 42
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00017A05 File Offset: 0x00015C05
		private static IEnumerator Start(T tweenInfo)
		{
			if (!tweenInfo.ValidTarget())
			{
				yield break;
			}
			float elapsedTime = 0f;
			while (elapsedTime < tweenInfo.duration)
			{
				elapsedTime += (tweenInfo.ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
				float floatPercentage = Mathf.Clamp01(elapsedTime / tweenInfo.duration);
				tweenInfo.TweenValue(floatPercentage);
				yield return null;
			}
			tweenInfo.TweenValue(1f);
			yield break;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00017A14 File Offset: 0x00015C14
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00017A20 File Offset: 0x00015C20
		public void StartTween(T info)
		{
			if (this.m_CoroutineContainer == null)
			{
				UnityEngine.Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
				return;
			}
			this.StopTween();
			if (!this.m_CoroutineContainer.gameObject.activeInHierarchy)
			{
				info.TweenValue(1f);
				return;
			}
			this.m_Tween = TweenRunner<T>.Start(info);
			this.m_CoroutineContainer.StartCoroutine(this.m_Tween);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00017A8F File Offset: 0x00015C8F
		public void StopTween()
		{
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00017AB1 File Offset: 0x00015CB1
		public TweenRunner()
		{
		}

		// Token: 0x04000158 RID: 344
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x04000159 RID: 345
		protected IEnumerator m_Tween;

		// Token: 0x0200007C RID: 124
		[CompilerGenerated]
		private sealed class <Start>d__2 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060005DF RID: 1503 RVA: 0x00038409 File Offset: 0x00036609
			[DebuggerHidden]
			public <Start>d__2(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x00038418 File Offset: 0x00036618
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x0003841C File Offset: 0x0003661C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
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
					if (!tweenInfo.ValidTarget())
					{
						return false;
					}
					elapsedTime = 0f;
				}
				if (elapsedTime >= tweenInfo.duration)
				{
					tweenInfo.TweenValue(1f);
					return false;
				}
				elapsedTime += (tweenInfo.ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
				float floatPercentage = Mathf.Clamp01(elapsedTime / tweenInfo.duration);
				tweenInfo.TweenValue(floatPercentage);
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0003850B File Offset: 0x0003670B
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x00038513 File Offset: 0x00036713
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0003851A File Offset: 0x0003671A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000587 RID: 1415
			private int <>1__state;

			// Token: 0x04000588 RID: 1416
			private object <>2__current;

			// Token: 0x04000589 RID: 1417
			public T tweenInfo;

			// Token: 0x0400058A RID: 1418
			private float <elapsedTime>5__2;
		}
	}
}
