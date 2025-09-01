using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x0200000A RID: 10
	public class FadeTest : MonoBehaviour
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000035D1 File Offset: 0x000017D1
		private void Start()
		{
			this.canFade = true;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000035DA File Offset: 0x000017DA
		private void Update()
		{
			if (Input.GetKeyDown("r") && this.canFade)
			{
				base.StartCoroutine(this.FadeQuick());
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000035FD File Offset: 0x000017FD
		private IEnumerator FadeQuick()
		{
			this.canFade = false;
			base.GetComponent<Animator>().SetBool("Fade", true);
			yield return new WaitForSeconds(1.5f);
			base.GetComponent<Animator>().SetBool("Fade", false);
			yield return new WaitForSeconds(0.15f);
			this.canFade = true;
			yield break;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000360C File Offset: 0x0000180C
		public FadeTest()
		{
		}

		// Token: 0x04000054 RID: 84
		private bool canFade;

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		private sealed class <FadeQuick>d__3 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600006E RID: 110 RVA: 0x00004138 File Offset: 0x00002338
			[DebuggerHidden]
			public <FadeQuick>d__3(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600006F RID: 111 RVA: 0x00004147 File Offset: 0x00002347
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000070 RID: 112 RVA: 0x0000414C File Offset: 0x0000234C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				FadeTest fadeTest = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					fadeTest.canFade = false;
					fadeTest.GetComponent<Animator>().SetBool("Fade", true);
					this.<>2__current = new WaitForSeconds(1.5f);
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					fadeTest.GetComponent<Animator>().SetBool("Fade", false);
					this.<>2__current = new WaitForSeconds(0.15f);
					this.<>1__state = 2;
					return true;
				case 2:
					this.<>1__state = -1;
					fadeTest.canFade = true;
					return false;
				default:
					return false;
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000071 RID: 113 RVA: 0x000041F3 File Offset: 0x000023F3
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000072 RID: 114 RVA: 0x000041FB File Offset: 0x000023FB
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000073 RID: 115 RVA: 0x00004202 File Offset: 0x00002402
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000093 RID: 147
			private int <>1__state;

			// Token: 0x04000094 RID: 148
			private object <>2__current;

			// Token: 0x04000095 RID: 149
			public FadeTest <>4__this;
		}
	}
}
