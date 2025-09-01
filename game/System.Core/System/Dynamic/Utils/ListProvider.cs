using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x0200032B RID: 811
	internal abstract class ListProvider<T> : IList<!0>, ICollection<!0>, IEnumerable<!0>, IEnumerable where T : class
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001875 RID: 6261
		protected abstract T First { get; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001876 RID: 6262
		protected abstract int ElementCount { get; }

		// Token: 0x06001877 RID: 6263
		protected abstract T GetElement(int index);

		// Token: 0x06001878 RID: 6264 RVA: 0x0005297C File Offset: 0x00050B7C
		public int IndexOf(T item)
		{
			if (this.First == item)
			{
				return 0;
			}
			int i = 1;
			int elementCount = this.ElementCount;
			while (i < elementCount)
			{
				if (this.GetElement(i) == item)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public void Insert(int index, T item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public void RemoveAt(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x1700043F RID: 1087
		public T this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.First;
				}
				return this.GetElement(index);
			}
			[ExcludeFromCodeCoverage]
			set
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public void Add(T item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public void Clear()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x000529DB File Offset: 0x00050BDB
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x000529EC File Offset: 0x00050BEC
		public void CopyTo(T[] array, int index)
		{
			ContractUtils.RequiresNotNull(array, "array");
			if (index < 0)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			int elementCount = this.ElementCount;
			if (index + elementCount > array.Length)
			{
				throw new ArgumentException();
			}
			array[index++] = this.First;
			for (int i = 1; i < elementCount; i++)
			{
				array[index++] = this.GetElement(i);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00052A59 File Offset: 0x00050C59
		public int Count
		{
			get
			{
				return this.ElementCount;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00007E1D File Offset: 0x0000601D
		[ExcludeFromCodeCoverage]
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00034D18 File Offset: 0x00032F18
		[ExcludeFromCodeCoverage]
		public bool Remove(T item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00052A61 File Offset: 0x00050C61
		public IEnumerator<T> GetEnumerator()
		{
			yield return this.First;
			int i = 1;
			int j = this.ElementCount;
			while (i < j)
			{
				yield return this.GetElement(i);
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00052A70 File Offset: 0x00050C70
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00002162 File Offset: 0x00000362
		protected ListProvider()
		{
		}

		// Token: 0x0200032C RID: 812
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__20 : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06001887 RID: 6279 RVA: 0x00052A78 File Offset: 0x00050C78
			[DebuggerHidden]
			public <GetEnumerator>d__20(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06001888 RID: 6280 RVA: 0x00003A59 File Offset: 0x00001C59
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001889 RID: 6281 RVA: 0x00052A88 File Offset: 0x00050C88
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ListProvider<T> listProvider = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = listProvider.First;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					i = 1;
					j = listProvider.ElementCount;
					break;
				case 2:
				{
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
					break;
				}
				default:
					return false;
				}
				if (i >= j)
				{
					return false;
				}
				this.<>2__current = listProvider.GetElement(i);
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x17000442 RID: 1090
			// (get) Token: 0x0600188A RID: 6282 RVA: 0x00052B30 File Offset: 0x00050D30
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600188B RID: 6283 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000443 RID: 1091
			// (get) Token: 0x0600188C RID: 6284 RVA: 0x00052B38 File Offset: 0x00050D38
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000BDF RID: 3039
			private int <>1__state;

			// Token: 0x04000BE0 RID: 3040
			private T <>2__current;

			// Token: 0x04000BE1 RID: 3041
			public ListProvider<T> <>4__this;

			// Token: 0x04000BE2 RID: 3042
			private int <i>5__2;

			// Token: 0x04000BE3 RID: 3043
			private int <n>5__3;
		}
	}
}
