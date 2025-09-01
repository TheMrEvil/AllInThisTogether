using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000AB3 RID: 2739
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(StackDebugView<>))]
	[TypeForwardedFrom("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Stack<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		// Token: 0x060061F9 RID: 25081 RVA: 0x001479C4 File Offset: 0x00145BC4
		public Stack()
		{
			this._array = Array.Empty<T>();
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x001479D7 File Offset: 0x00145BD7
		public Stack(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", capacity, "Non-negative number required.");
			}
			this._array = new T[capacity];
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x00147A05 File Offset: 0x00145C05
		public Stack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._array = EnumerableHelpers.ToArray<T>(collection, out this._size);
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060061FC RID: 25084 RVA: 0x00147A2D File Offset: 0x00145C2D
		public int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060061FD RID: 25085 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060061FE RID: 25086 RVA: 0x00147A35 File Offset: 0x00145C35
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x00147A57 File Offset: 0x00145C57
		public void Clear()
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				Array.Clear(this._array, 0, this._size);
			}
			this._size = 0;
			this._version++;
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x00147A87 File Offset: 0x00145C87
		public bool Contains(T item)
		{
			return this._size != 0 && Array.LastIndexOf<T>(this._array, item, this._size - 1) != -1;
		}

		// Token: 0x06006201 RID: 25089 RVA: 0x00147AB0 File Offset: 0x00145CB0
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int i = 0;
			int num = arrayIndex + this._size;
			while (i < this._size)
			{
				array[--num] = this._array[i++];
			}
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x00147B34 File Offset: 0x00145D34
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (array.GetLowerBound(0) != 0)
			{
				throw new ArgumentException("The lower bound of target array must be zero.", "array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (array.Length - arrayIndex < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			try
			{
				Array.Copy(this._array, 0, array, arrayIndex, this._size);
				Array.Reverse(array, arrayIndex, this._size);
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x00147C04 File Offset: 0x00145E04
		public Stack<T>.Enumerator GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x06006204 RID: 25092 RVA: 0x00147C0C File Offset: 0x00145E0C
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x00147C0C File Offset: 0x00145E0C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Stack<T>.Enumerator(this);
		}

		// Token: 0x06006206 RID: 25094 RVA: 0x00147C1C File Offset: 0x00145E1C
		public void TrimExcess()
		{
			int num = (int)((double)this._array.Length * 0.9);
			if (this._size < num)
			{
				Array.Resize<T>(ref this._array, this._size);
				this._version++;
			}
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x00147C68 File Offset: 0x00145E68
		public T Peek()
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				this.ThrowForEmptyStack();
			}
			return array[num];
		}

		// Token: 0x06006208 RID: 25096 RVA: 0x00147C98 File Offset: 0x00145E98
		public bool TryPeek(out T result)
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				result = default(T);
				return false;
			}
			result = array[num];
			return true;
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x00147CD4 File Offset: 0x00145ED4
		public T Pop()
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				this.ThrowForEmptyStack();
			}
			this._version++;
			this._size = num;
			T result = array[num];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				array[num] = default(T);
			}
			return result;
		}

		// Token: 0x0600620A RID: 25098 RVA: 0x00147D30 File Offset: 0x00145F30
		public bool TryPop(out T result)
		{
			int num = this._size - 1;
			T[] array = this._array;
			if (num >= array.Length)
			{
				result = default(T);
				return false;
			}
			this._version++;
			this._size = num;
			result = array[num];
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				array[num] = default(T);
			}
			return true;
		}

		// Token: 0x0600620B RID: 25099 RVA: 0x00147D98 File Offset: 0x00145F98
		public void Push(T item)
		{
			int size = this._size;
			T[] array = this._array;
			if (size < array.Length)
			{
				array[size] = item;
				this._version++;
				this._size = size + 1;
				return;
			}
			this.PushWithResize(item);
		}

		// Token: 0x0600620C RID: 25100 RVA: 0x00147DE0 File Offset: 0x00145FE0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void PushWithResize(T item)
		{
			Array.Resize<T>(ref this._array, (this._array.Length == 0) ? 4 : (2 * this._array.Length));
			this._array[this._size] = item;
			this._version++;
			this._size++;
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x00147E3C File Offset: 0x0014603C
		public T[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x00147E91 File Offset: 0x00146091
		private void ThrowForEmptyStack()
		{
			throw new InvalidOperationException("Stack empty.");
		}

		// Token: 0x04003A1C RID: 14876
		private T[] _array;

		// Token: 0x04003A1D RID: 14877
		private int _size;

		// Token: 0x04003A1E RID: 14878
		private int _version;

		// Token: 0x04003A1F RID: 14879
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04003A20 RID: 14880
		private const int DefaultCapacity = 4;

		// Token: 0x02000AB4 RID: 2740
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600620F RID: 25103 RVA: 0x00147E9D File Offset: 0x0014609D
			internal Enumerator(Stack<T> stack)
			{
				this._stack = stack;
				this._version = stack._version;
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x06006210 RID: 25104 RVA: 0x00147EC6 File Offset: 0x001460C6
			public void Dispose()
			{
				this._index = -1;
			}

			// Token: 0x06006211 RID: 25105 RVA: 0x00147ED0 File Offset: 0x001460D0
			public bool MoveNext()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._index == -2)
				{
					this._index = this._stack._size - 1;
					bool flag = this._index >= 0;
					if (flag)
					{
						this._currentElement = this._stack._array[this._index];
					}
					return flag;
				}
				if (this._index == -1)
				{
					return false;
				}
				int num = this._index - 1;
				this._index = num;
				bool flag2 = num >= 0;
				if (flag2)
				{
					this._currentElement = this._stack._array[this._index];
					return flag2;
				}
				this._currentElement = default(T);
				return flag2;
			}

			// Token: 0x1700116E RID: 4462
			// (get) Token: 0x06006212 RID: 25106 RVA: 0x00147F92 File Offset: 0x00146192
			public T Current
			{
				get
				{
					if (this._index < 0)
					{
						this.ThrowEnumerationNotStartedOrEnded();
					}
					return this._currentElement;
				}
			}

			// Token: 0x06006213 RID: 25107 RVA: 0x00147FA9 File Offset: 0x001461A9
			private void ThrowEnumerationNotStartedOrEnded()
			{
				throw new InvalidOperationException((this._index == -2) ? "Enumeration has not started. Call MoveNext." : "Enumeration already finished.");
			}

			// Token: 0x1700116F RID: 4463
			// (get) Token: 0x06006214 RID: 25108 RVA: 0x00147FC6 File Offset: 0x001461C6
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006215 RID: 25109 RVA: 0x00147FD3 File Offset: 0x001461D3
			void IEnumerator.Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -2;
				this._currentElement = default(T);
			}

			// Token: 0x04003A21 RID: 14881
			private readonly Stack<T> _stack;

			// Token: 0x04003A22 RID: 14882
			private readonly int _version;

			// Token: 0x04003A23 RID: 14883
			private int _index;

			// Token: 0x04003A24 RID: 14884
			private T _currentElement;
		}
	}
}
