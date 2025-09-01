using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Linq.Parallel
{
	// Token: 0x020001E7 RID: 487
	internal static class ExceptionAggregator
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x0002A20B File Offset: 0x0002840B
		internal static IEnumerable<TElement> WrapEnumerable<TElement>(IEnumerable<TElement> source, CancellationState cancellationState)
		{
			using (IEnumerator<TElement> enumerator = source.GetEnumerator())
			{
				for (;;)
				{
					TElement telement = default(TElement);
					try
					{
						if (!enumerator.MoveNext())
						{
							yield break;
						}
						telement = enumerator.Current;
					}
					catch (Exception ex)
					{
						ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
					}
					yield return telement;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002A222 File Offset: 0x00028422
		internal static IEnumerable<TElement> WrapQueryEnumerator<TElement, TIgnoreKey>(QueryOperatorEnumerator<TElement, TIgnoreKey> source, CancellationState cancellationState)
		{
			TElement elem = default(TElement);
			TIgnoreKey ignoreKey = default(TIgnoreKey);
			try
			{
				for (;;)
				{
					try
					{
						if (!source.MoveNext(ref elem, ref ignoreKey))
						{
							yield break;
						}
					}
					catch (Exception ex)
					{
						ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
					}
					yield return elem;
				}
			}
			finally
			{
				source.Dispose();
			}
			yield break;
			yield break;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002A239 File Offset: 0x00028439
		internal static void ThrowOCEorAggregateException(Exception ex, CancellationState cancellationState)
		{
			if (ExceptionAggregator.ThrowAnOCE(ex, cancellationState))
			{
				CancellationState.ThrowWithStandardMessageIfCanceled(cancellationState.ExternalCancellationToken);
				return;
			}
			throw new AggregateException(new Exception[]
			{
				ex
			});
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002A25F File Offset: 0x0002845F
		internal static Func<T, U> WrapFunc<T, U>(Func<T, U> f, CancellationState cancellationState)
		{
			return delegate(T t)
			{
				U result = default(U);
				try
				{
					result = f(t);
				}
				catch (Exception ex)
				{
					ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
				}
				return result;
			};
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002A280 File Offset: 0x00028480
		private static bool ThrowAnOCE(Exception ex, CancellationState cancellationState)
		{
			OperationCanceledException ex2 = ex as OperationCanceledException;
			return (ex2 != null && ex2.CancellationToken == cancellationState.ExternalCancellationToken && cancellationState.ExternalCancellationToken.IsCancellationRequested) || (ex2 != null && ex2.CancellationToken == cancellationState.MergedCancellationToken && cancellationState.MergedCancellationToken.IsCancellationRequested && cancellationState.ExternalCancellationToken.IsCancellationRequested);
		}

		// Token: 0x020001E8 RID: 488
		[CompilerGenerated]
		private sealed class <WrapEnumerable>d__0<TElement> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06000BFF RID: 3071 RVA: 0x0002A2EF File Offset: 0x000284EF
			[DebuggerHidden]
			public <WrapEnumerable>d__0(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000C00 RID: 3072 RVA: 0x0002A30C File Offset: 0x0002850C
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

			// Token: 0x06000C01 RID: 3073 RVA: 0x0002A344 File Offset: 0x00028544
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
						enumerator = source.GetEnumerator();
						this.<>1__state = -3;
					}
					TElement telement = default(TElement);
					try
					{
						if (!enumerator.MoveNext())
						{
							result = false;
							goto IL_82;
						}
						telement = enumerator.Current;
					}
					catch (Exception ex)
					{
						ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
					}
					this.<>2__current = telement;
					this.<>1__state = 1;
					return true;
					IL_82:
					this.<>m__Finally1();
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000C02 RID: 3074 RVA: 0x0002A400 File Offset: 0x00028600
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0002A41C File Offset: 0x0002861C
			TElement IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000C04 RID: 3076 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002A424 File Offset: 0x00028624
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000C06 RID: 3078 RVA: 0x0002A434 File Offset: 0x00028634
			[DebuggerHidden]
			IEnumerator<TElement> IEnumerable<!0>.GetEnumerator()
			{
				ExceptionAggregator.<WrapEnumerable>d__0<TElement> <WrapEnumerable>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<WrapEnumerable>d__ = this;
				}
				else
				{
					<WrapEnumerable>d__ = new ExceptionAggregator.<WrapEnumerable>d__0<TElement>(0);
				}
				<WrapEnumerable>d__.source = source;
				<WrapEnumerable>d__.cancellationState = cancellationState;
				return <WrapEnumerable>d__;
			}

			// Token: 0x06000C07 RID: 3079 RVA: 0x0002A483 File Offset: 0x00028683
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<TElement>.GetEnumerator();
			}

			// Token: 0x0400087F RID: 2175
			private int <>1__state;

			// Token: 0x04000880 RID: 2176
			private TElement <>2__current;

			// Token: 0x04000881 RID: 2177
			private int <>l__initialThreadId;

			// Token: 0x04000882 RID: 2178
			private IEnumerable<TElement> source;

			// Token: 0x04000883 RID: 2179
			public IEnumerable<TElement> <>3__source;

			// Token: 0x04000884 RID: 2180
			private CancellationState cancellationState;

			// Token: 0x04000885 RID: 2181
			public CancellationState <>3__cancellationState;

			// Token: 0x04000886 RID: 2182
			private IEnumerator<TElement> <enumerator>5__2;
		}

		// Token: 0x020001E9 RID: 489
		[CompilerGenerated]
		private sealed class <WrapQueryEnumerator>d__1<TElement, TIgnoreKey> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06000C08 RID: 3080 RVA: 0x0002A48B File Offset: 0x0002868B
			[DebuggerHidden]
			public <WrapQueryEnumerator>d__1(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000C09 RID: 3081 RVA: 0x0002A4A8 File Offset: 0x000286A8
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

			// Token: 0x06000C0A RID: 3082 RVA: 0x0002A4E0 File Offset: 0x000286E0
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
						elem = default(TElement);
						ignoreKey = default(TIgnoreKey);
						this.<>1__state = -3;
					}
					try
					{
						if (!source.MoveNext(ref elem, ref ignoreKey))
						{
							result = false;
							goto IL_8A;
						}
					}
					catch (Exception ex)
					{
						ExceptionAggregator.ThrowOCEorAggregateException(ex, cancellationState);
					}
					this.<>2__current = elem;
					this.<>1__state = 1;
					return true;
					IL_8A:
					this.<>m__Finally1();
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000C0B RID: 3083 RVA: 0x0002A5A4 File Offset: 0x000287A4
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				source.Dispose();
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000C0C RID: 3084 RVA: 0x0002A5B8 File Offset: 0x000287B8
			TElement IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0002A5C0 File Offset: 0x000287C0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000C0F RID: 3087 RVA: 0x0002A5D0 File Offset: 0x000287D0
			[DebuggerHidden]
			IEnumerator<TElement> IEnumerable<!0>.GetEnumerator()
			{
				ExceptionAggregator.<WrapQueryEnumerator>d__1<TElement, TIgnoreKey> <WrapQueryEnumerator>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<WrapQueryEnumerator>d__ = this;
				}
				else
				{
					<WrapQueryEnumerator>d__ = new ExceptionAggregator.<WrapQueryEnumerator>d__1<TElement, TIgnoreKey>(0);
				}
				<WrapQueryEnumerator>d__.source = source;
				<WrapQueryEnumerator>d__.cancellationState = cancellationState;
				return <WrapQueryEnumerator>d__;
			}

			// Token: 0x06000C10 RID: 3088 RVA: 0x0002A61F File Offset: 0x0002881F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<TElement>.GetEnumerator();
			}

			// Token: 0x04000887 RID: 2183
			private int <>1__state;

			// Token: 0x04000888 RID: 2184
			private TElement <>2__current;

			// Token: 0x04000889 RID: 2185
			private int <>l__initialThreadId;

			// Token: 0x0400088A RID: 2186
			private QueryOperatorEnumerator<TElement, TIgnoreKey> source;

			// Token: 0x0400088B RID: 2187
			public QueryOperatorEnumerator<TElement, TIgnoreKey> <>3__source;

			// Token: 0x0400088C RID: 2188
			private CancellationState cancellationState;

			// Token: 0x0400088D RID: 2189
			public CancellationState <>3__cancellationState;

			// Token: 0x0400088E RID: 2190
			private TElement <elem>5__2;

			// Token: 0x0400088F RID: 2191
			private TIgnoreKey <ignoreKey>5__3;
		}

		// Token: 0x020001EA RID: 490
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0<T, U>
		{
			// Token: 0x06000C11 RID: 3089 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000C12 RID: 3090 RVA: 0x0002A628 File Offset: 0x00028828
			internal U <WrapFunc>b__0(T t)
			{
				U result = default(U);
				try
				{
					result = this.f(t);
				}
				catch (Exception ex)
				{
					ExceptionAggregator.ThrowOCEorAggregateException(ex, this.cancellationState);
				}
				return result;
			}

			// Token: 0x04000890 RID: 2192
			public Func<T, U> f;

			// Token: 0x04000891 RID: 2193
			public CancellationState cancellationState;
		}
	}
}
