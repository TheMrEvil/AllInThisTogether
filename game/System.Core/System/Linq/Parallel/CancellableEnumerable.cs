using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001E5 RID: 485
	internal static class CancellableEnumerable
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002A04C File Offset: 0x0002824C
		internal static IEnumerable<TElement> Wrap<TElement>(IEnumerable<TElement> source, CancellationToken token)
		{
			int count = 0;
			foreach (TElement telement in source)
			{
				int num = count;
				count = num + 1;
				if ((num & 63) == 0)
				{
					CancellationState.ThrowIfCanceled(token);
				}
				yield return telement;
			}
			IEnumerator<TElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x020001E6 RID: 486
		[CompilerGenerated]
		private sealed class <Wrap>d__0<TElement> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06000BF1 RID: 3057 RVA: 0x0002A063 File Offset: 0x00028263
			[DebuggerHidden]
			public <Wrap>d__0(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000BF2 RID: 3058 RVA: 0x0002A080 File Offset: 0x00028280
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06000BF3 RID: 3059 RVA: 0x0002A0B8 File Offset: 0x000282B8
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						count = 0;
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						TElement telement = enumerator.Current;
						int num2 = count;
						count = num2 + 1;
						if ((num2 & 63) == 0)
						{
							CancellationState.ThrowIfCanceled(token);
						}
						this.<>2__current = telement;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000BF4 RID: 3060 RVA: 0x0002A180 File Offset: 0x00028380
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002A19C File Offset: 0x0002839C
			TElement IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000BF6 RID: 3062 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002A1A4 File Offset: 0x000283A4
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000BF8 RID: 3064 RVA: 0x0002A1B4 File Offset: 0x000283B4
			[DebuggerHidden]
			IEnumerator<TElement> IEnumerable<!0>.GetEnumerator()
			{
				CancellableEnumerable.<Wrap>d__0<TElement> <Wrap>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<Wrap>d__ = this;
				}
				else
				{
					<Wrap>d__ = new CancellableEnumerable.<Wrap>d__0<TElement>(0);
				}
				<Wrap>d__.source = source;
				<Wrap>d__.token = token;
				return <Wrap>d__;
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x0002A203 File Offset: 0x00028403
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<TElement>.GetEnumerator();
			}

			// Token: 0x04000876 RID: 2166
			private int <>1__state;

			// Token: 0x04000877 RID: 2167
			private TElement <>2__current;

			// Token: 0x04000878 RID: 2168
			private int <>l__initialThreadId;

			// Token: 0x04000879 RID: 2169
			private IEnumerable<TElement> source;

			// Token: 0x0400087A RID: 2170
			public IEnumerable<TElement> <>3__source;

			// Token: 0x0400087B RID: 2171
			private CancellationToken token;

			// Token: 0x0400087C RID: 2172
			public CancellationToken <>3__token;

			// Token: 0x0400087D RID: 2173
			private int <count>5__2;

			// Token: 0x0400087E RID: 2174
			private IEnumerator<TElement> <>7__wrap2;
		}
	}
}
