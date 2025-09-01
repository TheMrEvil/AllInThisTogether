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
	// Token: 0x02000030 RID: 48
	[CreateAssetMenu(fileName = "Speed Action", menuName = "Text Animator/Actions/Speed", order = 1)]
	[TagInfo("speed")]
	[Serializable]
	public sealed class SpeedAction : ActionScriptableBase
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00004AD5 File Offset: 0x00002CD5
		public override IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo)
		{
			float speed = this.defaultSpeed;
			if (action.parameters.Length != 0)
			{
				FormatUtils.TryGetFloat(action.parameters[0], this.defaultSpeed, out speed);
			}
			typingInfo.speed = speed;
			yield break;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004AF2 File Offset: 0x00002CF2
		public SpeedAction()
		{
		}

		// Token: 0x0400009D RID: 157
		[Tooltip("Speed used in case the action does not have the first parameter")]
		public float defaultSpeed = 2f;

		// Token: 0x02000053 RID: 83
		[CompilerGenerated]
		private sealed class <DoAction>d__1 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600019A RID: 410 RVA: 0x00007B36 File Offset: 0x00005D36
			[DebuggerHidden]
			public <DoAction>d__1(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600019B RID: 411 RVA: 0x00007B45 File Offset: 0x00005D45
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600019C RID: 412 RVA: 0x00007B48 File Offset: 0x00005D48
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				SpeedAction speedAction = this;
				if (num != 0)
				{
					return false;
				}
				this.<>1__state = -1;
				float defaultSpeed = speedAction.defaultSpeed;
				if (action.parameters.Length != 0)
				{
					FormatUtils.TryGetFloat(action.parameters[0], speedAction.defaultSpeed, out defaultSpeed);
				}
				typingInfo.speed = defaultSpeed;
				return false;
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x0600019D RID: 413 RVA: 0x00007BAC File Offset: 0x00005DAC
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600019E RID: 414 RVA: 0x00007BB4 File Offset: 0x00005DB4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x0600019F RID: 415 RVA: 0x00007BBB File Offset: 0x00005DBB
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000122 RID: 290
			private int <>1__state;

			// Token: 0x04000123 RID: 291
			private object <>2__current;

			// Token: 0x04000124 RID: 292
			public SpeedAction <>4__this;

			// Token: 0x04000125 RID: 293
			public ActionMarker action;

			// Token: 0x04000126 RID: 294
			public TypingInfo typingInfo;
		}
	}
}
