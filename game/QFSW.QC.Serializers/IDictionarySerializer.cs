using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Serializers
{
	// Token: 0x02000003 RID: 3
	public class IDictionarySerializer : IEnumerableSerializer<IDictionary>
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000208D File Offset: 0x0000028D
		protected override IEnumerable GetObjectStream(IDictionary value)
		{
			foreach (object obj in value)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				yield return dictionaryEntry;
			}
			IDictionaryEnumerator dictionaryEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000209D File Offset: 0x0000029D
		public IDictionarySerializer()
		{
		}

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		private sealed class <GetObjectStream>d__0 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000021 RID: 33 RVA: 0x00002441 File Offset: 0x00000641
			[DebuggerHidden]
			public <GetObjectStream>d__0(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000022 RID: 34 RVA: 0x0000245C File Offset: 0x0000065C
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

			// Token: 0x06000023 RID: 35 RVA: 0x00002494 File Offset: 0x00000694
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
						dictionaryEnumerator = value.GetEnumerator();
						this.<>1__state = -3;
					}
					if (!dictionaryEnumerator.MoveNext())
					{
						this.<>m__Finally1();
						dictionaryEnumerator = null;
						result = false;
					}
					else
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)dictionaryEnumerator.Current;
						this.<>2__current = dictionaryEntry;
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

			// Token: 0x06000024 RID: 36 RVA: 0x0000253C File Offset: 0x0000073C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				IDisposable disposable = dictionaryEnumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000025 RID: 37 RVA: 0x00002565 File Offset: 0x00000765
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000026 RID: 38 RVA: 0x0000256D File Offset: 0x0000076D
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000027 RID: 39 RVA: 0x00002574 File Offset: 0x00000774
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000028 RID: 40 RVA: 0x0000257C File Offset: 0x0000077C
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				IDictionarySerializer.<GetObjectStream>d__0 <GetObjectStream>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetObjectStream>d__ = this;
				}
				else
				{
					<GetObjectStream>d__ = new IDictionarySerializer.<GetObjectStream>d__0(0);
				}
				<GetObjectStream>d__.value = value;
				return <GetObjectStream>d__;
			}

			// Token: 0x06000029 RID: 41 RVA: 0x000025BF File Offset: 0x000007BF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x04000005 RID: 5
			private int <>1__state;

			// Token: 0x04000006 RID: 6
			private object <>2__current;

			// Token: 0x04000007 RID: 7
			private int <>l__initialThreadId;

			// Token: 0x04000008 RID: 8
			private IDictionary value;

			// Token: 0x04000009 RID: 9
			public IDictionary <>3__value;

			// Token: 0x0400000A RID: 10
			private IDictionaryEnumerator <>7__wrap1;
		}
	}
}
