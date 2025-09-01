using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Photon.Realtime
{
	// Token: 0x0200003B RID: 59
	internal class MonoBehaviourEmpty : MonoBehaviour
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00009C0F File Offset: 0x00007E0F
		public static MonoBehaviourEmpty BuildInstance(string id = null)
		{
			GameObject gameObject = new GameObject(id ?? "MonoBehaviourEmpty");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			return gameObject.AddComponent<MonoBehaviourEmpty>();
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00009C2B File Offset: 0x00007E2B
		public void SelfDestroy()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00009C38 File Offset: 0x00007E38
		private void Update()
		{
			if (this.obj != null)
			{
				this.onCompleteCall(this.obj);
				this.obj = null;
				this.onCompleteCall = null;
				this.SelfDestroy();
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009C67 File Offset: 0x00007E67
		public void CompleteOnMainThread(RegionHandler obj)
		{
			this.obj = obj;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009C70 File Offset: 0x00007E70
		public void StartCoroutineAndDestroy(IEnumerator coroutine)
		{
			MonoBehaviourEmpty.<>c__DisplayClass6_0 CS$<>8__locals1 = new MonoBehaviourEmpty.<>c__DisplayClass6_0();
			CS$<>8__locals1.coroutine = coroutine;
			CS$<>8__locals1.<>4__this = this;
			base.StartCoroutine(CS$<>8__locals1.<StartCoroutineAndDestroy>g__Routine|0());
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009C9E File Offset: 0x00007E9E
		public MonoBehaviourEmpty()
		{
		}

		// Token: 0x040001E8 RID: 488
		internal Action<RegionHandler> onCompleteCall;

		// Token: 0x040001E9 RID: 489
		private RegionHandler obj;

		// Token: 0x02000049 RID: 73
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000235 RID: 565 RVA: 0x0000BAA3 File Offset: 0x00009CA3
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000236 RID: 566 RVA: 0x0000BAAB File Offset: 0x00009CAB
			internal IEnumerator <StartCoroutineAndDestroy>g__Routine|0()
			{
				MonoBehaviourEmpty.<>c__DisplayClass6_0.<<StartCoroutineAndDestroy>g__Routine|0>d <<StartCoroutineAndDestroy>g__Routine|0>d = new MonoBehaviourEmpty.<>c__DisplayClass6_0.<<StartCoroutineAndDestroy>g__Routine|0>d(0);
				<<StartCoroutineAndDestroy>g__Routine|0>d.<>4__this = this;
				return <<StartCoroutineAndDestroy>g__Routine|0>d;
			}

			// Token: 0x0400022E RID: 558
			public IEnumerator coroutine;

			// Token: 0x0400022F RID: 559
			public MonoBehaviourEmpty <>4__this;

			// Token: 0x0200004C RID: 76
			private sealed class <<StartCoroutineAndDestroy>g__Routine|0>d : IEnumerator<object>, IEnumerator, IDisposable
			{
				// Token: 0x06000238 RID: 568 RVA: 0x0000BAC2 File Offset: 0x00009CC2
				[DebuggerHidden]
				public <<StartCoroutineAndDestroy>g__Routine|0>d(int <>1__state)
				{
					this.<>1__state = <>1__state;
				}

				// Token: 0x06000239 RID: 569 RVA: 0x0000BAD1 File Offset: 0x00009CD1
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x0600023A RID: 570 RVA: 0x0000BAD4 File Offset: 0x00009CD4
				bool IEnumerator.MoveNext()
				{
					int num = this.<>1__state;
					MonoBehaviourEmpty.<>c__DisplayClass6_0 CS$<>8__locals1 = this.<>4__this;
					if (num == 0)
					{
						this.<>1__state = -1;
						this.<>2__current = CS$<>8__locals1.coroutine;
						this.<>1__state = 1;
						return true;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					CS$<>8__locals1.<>4__this.SelfDestroy();
					return false;
				}

				// Token: 0x17000078 RID: 120
				// (get) Token: 0x0600023B RID: 571 RVA: 0x0000BB27 File Offset: 0x00009D27
				object IEnumerator<object>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x0600023C RID: 572 RVA: 0x0000BB2F File Offset: 0x00009D2F
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x17000079 RID: 121
				// (get) Token: 0x0600023D RID: 573 RVA: 0x0000BB36 File Offset: 0x00009D36
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x0400023B RID: 571
				private int <>1__state;

				// Token: 0x0400023C RID: 572
				private object <>2__current;

				// Token: 0x0400023D RID: 573
				public MonoBehaviourEmpty.<>c__DisplayClass6_0 <>4__this;
			}
		}
	}
}
