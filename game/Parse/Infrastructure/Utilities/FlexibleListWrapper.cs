using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000053 RID: 83
	[Preserve(AllMembers = true, Conditional = false)]
	public class FlexibleListWrapper<TOut, TIn> : IList<TOut>, ICollection<TOut>, IEnumerable<TOut>, IEnumerable
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x0000CFC5 File Offset: 0x0000B1C5
		public FlexibleListWrapper(IList<TIn> toWrap)
		{
			this.toWrap = toWrap;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		public int IndexOf(TOut item)
		{
			return this.toWrap.IndexOf((TIn)((object)Conversion.ConvertTo<TIn>(item)));
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000CFF1 File Offset: 0x0000B1F1
		public void Insert(int index, TOut item)
		{
			this.toWrap.Insert(index, (TIn)((object)Conversion.ConvertTo<TIn>(item)));
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000D00F File Offset: 0x0000B20F
		public void RemoveAt(int index)
		{
			this.toWrap.RemoveAt(index);
		}

		// Token: 0x1700014B RID: 331
		public TOut this[int index]
		{
			get
			{
				return (TOut)((object)Conversion.ConvertTo<TOut>(this.toWrap[index]));
			}
			set
			{
				this.toWrap[index] = (TIn)((object)Conversion.ConvertTo<TIn>(value));
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000D058 File Offset: 0x0000B258
		public void Add(TOut item)
		{
			this.toWrap.Add((TIn)((object)Conversion.ConvertTo<TIn>(item)));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000D075 File Offset: 0x0000B275
		public void Clear()
		{
			this.toWrap.Clear();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000D082 File Offset: 0x0000B282
		public bool Contains(TOut item)
		{
			return this.toWrap.Contains((TIn)((object)Conversion.ConvertTo<TIn>(item)));
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000D09F File Offset: 0x0000B29F
		public void CopyTo(TOut[] array, int arrayIndex)
		{
			(from item in this.toWrap
			select (TOut)((object)Conversion.ConvertTo<TOut>(item))).ToList<TOut>().CopyTo(array, arrayIndex);
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000D0D7 File Offset: 0x0000B2D7
		public int Count
		{
			get
			{
				return this.toWrap.Count;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000D0E4 File Offset: 0x0000B2E4
		public bool IsReadOnly
		{
			get
			{
				return this.toWrap.IsReadOnly;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000D0F1 File Offset: 0x0000B2F1
		public bool Remove(TOut item)
		{
			return this.toWrap.Remove((TIn)((object)Conversion.ConvertTo<TIn>(item)));
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000D10E File Offset: 0x0000B30E
		public IEnumerator<TOut> GetEnumerator()
		{
			foreach (object value in this.toWrap)
			{
				yield return (TOut)((object)Conversion.ConvertTo<TOut>(value));
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000D11D File Offset: 0x0000B31D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000C9 RID: 201
		private IList<TIn> toWrap;

		// Token: 0x0200011E RID: 286
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600075D RID: 1885 RVA: 0x000166A8 File Offset: 0x000148A8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600075E RID: 1886 RVA: 0x000166B4 File Offset: 0x000148B4
			public <>c()
			{
			}

			// Token: 0x0600075F RID: 1887 RVA: 0x000166BC File Offset: 0x000148BC
			internal TOut <CopyTo>b__11_0(TIn item)
			{
				return (TOut)((object)Conversion.ConvertTo<TOut>(item));
			}

			// Token: 0x0400029D RID: 669
			public static readonly FlexibleListWrapper<TOut, TIn>.<>c <>9 = new FlexibleListWrapper<TOut, TIn>.<>c();

			// Token: 0x0400029E RID: 670
			public static Func<TIn, TOut> <>9__11_0;
		}

		// Token: 0x0200011F RID: 287
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__17 : IEnumerator<TOut>, IEnumerator, IDisposable
		{
			// Token: 0x06000760 RID: 1888 RVA: 0x000166CE File Offset: 0x000148CE
			[DebuggerHidden]
			public <GetEnumerator>d__17(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000761 RID: 1889 RVA: 0x000166E0 File Offset: 0x000148E0
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

			// Token: 0x06000762 RID: 1890 RVA: 0x00016718 File Offset: 0x00014918
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					FlexibleListWrapper<TOut, TIn> flexibleListWrapper = this;
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
						enumerator = flexibleListWrapper.toWrap.GetEnumerator();
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
						object value = enumerator.Current;
						this.<>2__current = (TOut)((object)Conversion.ConvertTo<TOut>(value));
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

			// Token: 0x06000763 RID: 1891 RVA: 0x000167C8 File Offset: 0x000149C8
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x06000764 RID: 1892 RVA: 0x000167F1 File Offset: 0x000149F1
			TOut IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x000167F9 File Offset: 0x000149F9
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x06000766 RID: 1894 RVA: 0x00016800 File Offset: 0x00014A00
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400029F RID: 671
			private int <>1__state;

			// Token: 0x040002A0 RID: 672
			private TOut <>2__current;

			// Token: 0x040002A1 RID: 673
			public FlexibleListWrapper<TOut, TIn> <>4__this;

			// Token: 0x040002A2 RID: 674
			private IEnumerator <>7__wrap1;
		}
	}
}
