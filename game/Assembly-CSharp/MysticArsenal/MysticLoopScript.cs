using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MysticArsenal
{
	// Token: 0x020003D7 RID: 983
	public class MysticLoopScript : MonoBehaviour
	{
		// Token: 0x06002011 RID: 8209 RVA: 0x000BE7AE File Offset: 0x000BC9AE
		private void Start()
		{
			this.PlayEffect();
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000BE7B6 File Offset: 0x000BC9B6
		public void PlayEffect()
		{
			base.StartCoroutine("EffectLoop");
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000BE7C4 File Offset: 0x000BC9C4
		private IEnumerator EffectLoop()
		{
			GameObject effectPlayer = UnityEngine.Object.Instantiate<GameObject>(this.chosenEffect);
			effectPlayer.transform.position = base.transform.position;
			yield return new WaitForSeconds(this.loopTimeLimit);
			UnityEngine.Object.Destroy(effectPlayer);
			this.PlayEffect();
			yield break;
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000BE7D3 File Offset: 0x000BC9D3
		public MysticLoopScript()
		{
		}

		// Token: 0x04002060 RID: 8288
		public GameObject chosenEffect;

		// Token: 0x04002061 RID: 8289
		public float loopTimeLimit = 2f;

		// Token: 0x020006A4 RID: 1700
		[CompilerGenerated]
		private sealed class <EffectLoop>d__4 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06002839 RID: 10297 RVA: 0x000D840C File Offset: 0x000D660C
			[DebuggerHidden]
			public <EffectLoop>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600283A RID: 10298 RVA: 0x000D841B File Offset: 0x000D661B
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600283B RID: 10299 RVA: 0x000D8420 File Offset: 0x000D6620
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				MysticLoopScript mysticLoopScript = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					effectPlayer = UnityEngine.Object.Instantiate<GameObject>(mysticLoopScript.chosenEffect);
					effectPlayer.transform.position = mysticLoopScript.transform.position;
					this.<>2__current = new WaitForSeconds(mysticLoopScript.loopTimeLimit);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				UnityEngine.Object.Destroy(effectPlayer);
				mysticLoopScript.PlayEffect();
				return false;
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x0600283C RID: 10300 RVA: 0x000D84AA File Offset: 0x000D66AA
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600283D RID: 10301 RVA: 0x000D84B2 File Offset: 0x000D66B2
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x0600283E RID: 10302 RVA: 0x000D84B9 File Offset: 0x000D66B9
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04002C76 RID: 11382
			private int <>1__state;

			// Token: 0x04002C77 RID: 11383
			private object <>2__current;

			// Token: 0x04002C78 RID: 11384
			public MysticLoopScript <>4__this;

			// Token: 0x04002C79 RID: 11385
			private GameObject <effectPlayer>5__2;
		}
	}
}
