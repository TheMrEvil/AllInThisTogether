using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x02000009 RID: 9
	public class DemoOpening : MonoBehaviour
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000358C File Offset: 0x0000178C
		private void Start()
		{
			this.popUpWelcome.SetActive(false);
			base.StartCoroutine(this.LoadPopUpWelcome());
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000035A7 File Offset: 0x000017A7
		private IEnumerator LoadPopUpWelcome()
		{
			yield return new WaitForSeconds(this.waitTime);
			this.popUpWelcome.SetActive(true);
			yield break;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000035B6 File Offset: 0x000017B6
		public void ChangeArrowDirection(int x)
		{
			this.inputArrow.SetInteger("direction", x);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000035C9 File Offset: 0x000017C9
		public DemoOpening()
		{
		}

		// Token: 0x04000051 RID: 81
		public GameObject popUpWelcome;

		// Token: 0x04000052 RID: 82
		public Animator inputArrow;

		// Token: 0x04000053 RID: 83
		public float waitTime;

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		private sealed class <LoadPopUpWelcome>d__4 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000068 RID: 104 RVA: 0x000040B7 File Offset: 0x000022B7
			[DebuggerHidden]
			public <LoadPopUpWelcome>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000069 RID: 105 RVA: 0x000040C6 File Offset: 0x000022C6
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600006A RID: 106 RVA: 0x000040C8 File Offset: 0x000022C8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				DemoOpening demoOpening = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = new WaitForSeconds(demoOpening.waitTime);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				demoOpening.popUpWelcome.SetActive(true);
				return false;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600006B RID: 107 RVA: 0x00004121 File Offset: 0x00002321
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600006C RID: 108 RVA: 0x00004129 File Offset: 0x00002329
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600006D RID: 109 RVA: 0x00004130 File Offset: 0x00002330
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000090 RID: 144
			private int <>1__state;

			// Token: 0x04000091 RID: 145
			private object <>2__current;

			// Token: 0x04000092 RID: 146
			public DemoOpening <>4__this;
		}
	}
}
