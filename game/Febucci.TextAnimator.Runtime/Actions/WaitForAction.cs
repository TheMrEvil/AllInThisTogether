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
	// Token: 0x02000032 RID: 50
	[CreateAssetMenu(fileName = "WaitFor Action", menuName = "Text Animator/Actions/Wait For", order = 1)]
	[TagInfo("waitfor")]
	[Serializable]
	public sealed class WaitForAction : ActionScriptableBase
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00004B15 File Offset: 0x00002D15
		public override IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo)
		{
			float targetTime = this.defaultTime;
			if (action.parameters.Length != 0)
			{
				FormatUtils.TryGetFloat(action.parameters[0], this.defaultTime, out targetTime);
			}
			float t = 0f;
			while (t <= targetTime)
			{
				t += typewriter.TextAnimator.time.deltaTime;
				yield return null;
			}
			yield break;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004B32 File Offset: 0x00002D32
		public WaitForAction()
		{
		}

		// Token: 0x0400009E RID: 158
		[Tooltip("Time used in case the action does not have the first parameter")]
		public float defaultTime = 1f;

		// Token: 0x02000055 RID: 85
		[CompilerGenerated]
		private sealed class <DoAction>d__1 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060001A6 RID: 422 RVA: 0x00007C30 File Offset: 0x00005E30
			[DebuggerHidden]
			public <DoAction>d__1(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x00007C3F File Offset: 0x00005E3F
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x00007C44 File Offset: 0x00005E44
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				WaitForAction waitForAction = this;
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
					targetTime = waitForAction.defaultTime;
					if (action.parameters.Length != 0)
					{
						FormatUtils.TryGetFloat(action.parameters[0], waitForAction.defaultTime, out targetTime);
					}
					t = 0f;
				}
				if (t > targetTime)
				{
					return false;
				}
				t += typewriter.TextAnimator.time.deltaTime;
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007D00 File Offset: 0x00005F00
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060001AA RID: 426 RVA: 0x00007D08 File Offset: 0x00005F08
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060001AB RID: 427 RVA: 0x00007D0F File Offset: 0x00005F0F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000129 RID: 297
			private int <>1__state;

			// Token: 0x0400012A RID: 298
			private object <>2__current;

			// Token: 0x0400012B RID: 299
			public WaitForAction <>4__this;

			// Token: 0x0400012C RID: 300
			public ActionMarker action;

			// Token: 0x0400012D RID: 301
			public TypewriterCore typewriter;

			// Token: 0x0400012E RID: 302
			private float <targetTime>5__2;

			// Token: 0x0400012F RID: 303
			private float <t>5__3;
		}
	}
}
