using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QFSW.QC
{
	// Token: 0x02000012 RID: 18
	public class LambdaCommandData : CommandData
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002DA3 File Offset: 0x00000FA3
		public LambdaCommandData(Delegate lambda, string commandName, string commandDescription = "") : base(lambda.Method, new CommandAttribute(commandName, commandDescription, MonoTargetType.Registry, Platform.AllPlatforms), 0)
		{
			this._lambdaTarget = lambda.Target;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002DC8 File Offset: 0x00000FC8
		protected override IEnumerable<object> GetInvocationTargets(MethodInfo invokingMethod)
		{
			yield return this._lambdaTarget;
			yield break;
		}

		// Token: 0x04000025 RID: 37
		private readonly object _lambdaTarget;

		// Token: 0x02000089 RID: 137
		[CompilerGenerated]
		private sealed class <GetInvocationTargets>d__2 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060002B8 RID: 696 RVA: 0x0000AF39 File Offset: 0x00009139
			[DebuggerHidden]
			public <GetInvocationTargets>d__2(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x0000AF53 File Offset: 0x00009153
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060002BA RID: 698 RVA: 0x0000AF58 File Offset: 0x00009158
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				LambdaCommandData lambdaCommandData = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = lambdaCommandData._lambdaTarget;
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x060002BB RID: 699 RVA: 0x0000AFA0 File Offset: 0x000091A0
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002BC RID: 700 RVA: 0x0000AFA8 File Offset: 0x000091A8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x060002BD RID: 701 RVA: 0x0000AFAF File Offset: 0x000091AF
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002BE RID: 702 RVA: 0x0000AFB8 File Offset: 0x000091B8
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				LambdaCommandData.<GetInvocationTargets>d__2 <GetInvocationTargets>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetInvocationTargets>d__ = this;
				}
				else
				{
					<GetInvocationTargets>d__ = new LambdaCommandData.<GetInvocationTargets>d__2(0);
					<GetInvocationTargets>d__.<>4__this = this;
				}
				return <GetInvocationTargets>d__;
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0000AFFB File Offset: 0x000091FB
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x0400018F RID: 399
			private int <>1__state;

			// Token: 0x04000190 RID: 400
			private object <>2__current;

			// Token: 0x04000191 RID: 401
			private int <>l__initialThreadId;

			// Token: 0x04000192 RID: 402
			public LambdaCommandData <>4__this;
		}
	}
}
