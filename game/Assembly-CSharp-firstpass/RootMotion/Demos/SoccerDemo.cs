using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200014B RID: 331
	public class SoccerDemo : MonoBehaviour
	{
		// Token: 0x06000D30 RID: 3376 RVA: 0x000596EA File Offset: 0x000578EA
		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
			this.defaultPosition = base.transform.position;
			this.defaultRotation = base.transform.rotation;
			base.StartCoroutine(this.ResetDelayed());
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00059727 File Offset: 0x00057927
		private IEnumerator ResetDelayed()
		{
			for (;;)
			{
				yield return new WaitForSeconds(3f);
				base.transform.position = this.defaultPosition;
				base.transform.rotation = this.defaultRotation;
				this.animator.CrossFade("SoccerKick", 0f, 0, 0f);
				yield return null;
			}
			yield break;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00059736 File Offset: 0x00057936
		public SoccerDemo()
		{
		}

		// Token: 0x04000ADE RID: 2782
		private Animator animator;

		// Token: 0x04000ADF RID: 2783
		private Vector3 defaultPosition;

		// Token: 0x04000AE0 RID: 2784
		private Quaternion defaultRotation;

		// Token: 0x02000235 RID: 565
		[CompilerGenerated]
		private sealed class <ResetDelayed>d__4 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060011B5 RID: 4533 RVA: 0x0006DE5E File Offset: 0x0006C05E
			[DebuggerHidden]
			public <ResetDelayed>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060011B6 RID: 4534 RVA: 0x0006DE6D File Offset: 0x0006C06D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060011B7 RID: 4535 RVA: 0x0006DE70 File Offset: 0x0006C070
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				SoccerDemo soccerDemo = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					break;
				case 1:
					this.<>1__state = -1;
					soccerDemo.transform.position = soccerDemo.defaultPosition;
					soccerDemo.transform.rotation = soccerDemo.defaultRotation;
					soccerDemo.animator.CrossFade("SoccerKick", 0f, 0, 0f);
					this.<>2__current = null;
					this.<>1__state = 2;
					return true;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				this.<>2__current = new WaitForSeconds(3f);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700025F RID: 607
			// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0006DF1B File Offset: 0x0006C11B
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060011B9 RID: 4537 RVA: 0x0006DF23 File Offset: 0x0006C123
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x060011BA RID: 4538 RVA: 0x0006DF2A File Offset: 0x0006C12A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400109F RID: 4255
			private int <>1__state;

			// Token: 0x040010A0 RID: 4256
			private object <>2__current;

			// Token: 0x040010A1 RID: 4257
			public SoccerDemo <>4__this;
		}
	}
}
