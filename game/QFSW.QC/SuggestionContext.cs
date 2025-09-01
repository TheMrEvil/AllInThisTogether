using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace QFSW.QC
{
	// Token: 0x02000046 RID: 70
	public struct SuggestionContext
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00007A8C File Offset: 0x00005C8C
		public bool HasTag<T>() where T : IQcSuggestorTag
		{
			if (this.Tags == null)
			{
				return false;
			}
			IQcSuggestorTag[] tags = this.Tags;
			for (int i = 0; i < tags.Length; i++)
			{
				if (tags[i] is T)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007AC8 File Offset: 0x00005CC8
		public T GetTag<T>() where T : IQcSuggestorTag
		{
			if (this.Tags != null)
			{
				foreach (IQcSuggestorTag qcSuggestorTag in this.Tags)
				{
					if (qcSuggestorTag is T)
					{
						return (T)((object)qcSuggestorTag);
					}
				}
			}
			throw new KeyNotFoundException(string.Format("No tags of type {0} could be found.", typeof(T)));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007B20 File Offset: 0x00005D20
		public IEnumerable<T> GetTags<T>() where T : IQcSuggestorTag
		{
			if (this.Tags != null)
			{
				foreach (IQcSuggestorTag qcSuggestorTag in this.Tags)
				{
					if (qcSuggestorTag is T)
					{
						T t = (T)((object)qcSuggestorTag);
						yield return t;
					}
				}
				IQcSuggestorTag[] array = null;
			}
			yield break;
		}

		// Token: 0x04000116 RID: 278
		public int Depth;

		// Token: 0x04000117 RID: 279
		public string Prompt;

		// Token: 0x04000118 RID: 280
		public Type TargetType;

		// Token: 0x04000119 RID: 281
		public IQcSuggestorTag[] Tags;

		// Token: 0x0200009D RID: 157
		[CompilerGenerated]
		private sealed class <GetTags>d__6<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IEnumerator, IDisposable where T : IQcSuggestorTag
		{
			// Token: 0x06000309 RID: 777 RVA: 0x0000BBBA File Offset: 0x00009DBA
			[DebuggerHidden]
			public <GetTags>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600030A RID: 778 RVA: 0x0000BBD4 File Offset: 0x00009DD4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600030B RID: 779 RVA: 0x0000BBD8 File Offset: 0x00009DD8
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
					if (this.Tags != null)
					{
						array = this.Tags;
						i = 0;
						goto IL_80;
					}
					return false;
				}
				IL_72:
				i++;
				IL_80:
				if (i >= array.Length)
				{
					array = null;
				}
				else
				{
					IQcSuggestorTag qcSuggestorTag = array[i];
					if (qcSuggestorTag is T)
					{
						T t = (T)((object)qcSuggestorTag);
						this.<>2__current = t;
						this.<>1__state = 1;
						return true;
					}
					goto IL_72;
				}
				return false;
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x0600030C RID: 780 RVA: 0x0000BC7D File Offset: 0x00009E7D
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600030D RID: 781 RVA: 0x0000BC85 File Offset: 0x00009E85
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600030E RID: 782 RVA: 0x0000BC8C File Offset: 0x00009E8C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600030F RID: 783 RVA: 0x0000BC9C File Offset: 0x00009E9C
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				SuggestionContext.<GetTags>d__6<T> <GetTags>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetTags>d__ = this;
				}
				else
				{
					<GetTags>d__ = new SuggestionContext.<GetTags>d__6<T>(0);
				}
				<GetTags>d__.<>4__this = ref this;
				return <GetTags>d__;
			}

			// Token: 0x06000310 RID: 784 RVA: 0x0000BCDF File Offset: 0x00009EDF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x040001E1 RID: 481
			private int <>1__state;

			// Token: 0x040001E2 RID: 482
			private T <>2__current;

			// Token: 0x040001E3 RID: 483
			private int <>l__initialThreadId;

			// Token: 0x040001E4 RID: 484
			public SuggestionContext <>4__this;

			// Token: 0x040001E5 RID: 485
			public SuggestionContext <>3__<>4__this;

			// Token: 0x040001E6 RID: 486
			private IQcSuggestorTag[] <>7__wrap1;

			// Token: 0x040001E7 RID: 487
			private int <>7__wrap2;
		}
	}
}
