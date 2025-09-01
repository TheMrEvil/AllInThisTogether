using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using UnityEngine;

namespace Febucci.UI.Actions
{
	// Token: 0x02000031 RID: 49
	[CreateAssetMenu(fileName = "WaitAnyInput Action", menuName = "Text Animator/Actions/Wait Any Input", order = 1)]
	[TagInfo("waitinput")]
	[Serializable]
	public sealed class WaitAnyInputAction : ActionScriptableBase
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004B05 File Offset: 0x00002D05
		public override IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo)
		{
			while (!Input.anyKeyDown)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004B0D File Offset: 0x00002D0D
		public WaitAnyInputAction()
		{
		}

		// Token: 0x02000054 RID: 84
		[CompilerGenerated]
		private sealed class <DoAction>d__0 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060001A0 RID: 416 RVA: 0x00007BC3 File Offset: 0x00005DC3
			[DebuggerHidden]
			public <DoAction>d__0(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060001A1 RID: 417 RVA: 0x00007BD2 File Offset: 0x00005DD2
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x00007BD4 File Offset: 0x00005DD4
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
				}
				if (Input.anyKeyDown)
				{
					return false;
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007C19 File Offset: 0x00005E19
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x00007C21 File Offset: 0x00005E21
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007C28 File Offset: 0x00005E28
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000127 RID: 295
			private int <>1__state;

			// Token: 0x04000128 RID: 296
			private object <>2__current;
		}
	}
}
