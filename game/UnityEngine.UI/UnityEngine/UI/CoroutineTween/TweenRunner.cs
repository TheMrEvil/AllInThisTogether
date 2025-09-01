using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UI.CoroutineTween
{
	// Token: 0x02000049 RID: 73
	internal class TweenRunner<T> where T : struct, ITweenValue
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x00017189 File Offset: 0x00015389
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

		// Token: 0x060004F3 RID: 1267 RVA: 0x00017198 File Offset: 0x00015398
		public void Init(MonoBehaviour coroutineContainer)
		{
			this.m_CoroutineContainer = coroutineContainer;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000171A4 File Offset: 0x000153A4
		public void StartTween(T info)
		{
			if (this.m_CoroutineContainer == null)
			{
				Debug.LogWarning("Coroutine container not configured... did you forget to call Init?");
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

		// Token: 0x060004F5 RID: 1269 RVA: 0x00017213 File Offset: 0x00015413
		public void StopTween()
		{
			if (this.m_Tween != null)
			{
				this.m_CoroutineContainer.StopCoroutine(this.m_Tween);
				this.m_Tween = null;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017235 File Offset: 0x00015435
		public TweenRunner()
		{
		}

		// Token: 0x040001A3 RID: 419
		protected MonoBehaviour m_CoroutineContainer;

		// Token: 0x040001A4 RID: 420
		protected IEnumerator m_Tween;

		// Token: 0x020000BB RID: 187
		[CompilerGenerated]
		private sealed class <Start>d__2 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600070E RID: 1806 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
			[DebuggerHidden]
			public <Start>d__2(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600070F RID: 1807 RVA: 0x0001C2C7 File Offset: 0x0001A4C7
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x0001C2CC File Offset: 0x0001A4CC
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

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001C3BB File Offset: 0x0001A5BB
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000712 RID: 1810 RVA: 0x0001C3C3 File Offset: 0x0001A5C3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001C3CA File Offset: 0x0001A5CA
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400031C RID: 796
			private int <>1__state;

			// Token: 0x0400031D RID: 797
			private object <>2__current;

			// Token: 0x0400031E RID: 798
			public T tweenInfo;

			// Token: 0x0400031F RID: 799
			private float <elapsedTime>5__2;
		}
	}
}
