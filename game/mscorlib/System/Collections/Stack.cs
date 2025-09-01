using System;
using System.Diagnostics;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a simple last-in-first-out (LIFO) non-generic collection of objects.</summary>
	// Token: 0x02000A32 RID: 2610
	[DebuggerTypeProxy(typeof(Stack.StackDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class Stack : ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the default initial capacity.</summary>
		// Token: 0x06005CB6 RID: 23734 RVA: 0x00137901 File Offset: 0x00135B01
		public Stack()
		{
			this._array = new object[10];
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that is empty and has the specified initial capacity or the default initial capacity, whichever is greater.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.Stack" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero.</exception>
		// Token: 0x06005CB7 RID: 23735 RVA: 0x00137924 File Offset: 0x00135B24
		public Stack(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", "Non-negative number required.");
			}
			if (initialCapacity < 10)
			{
				initialCapacity = 10;
			}
			this._array = new object[initialCapacity];
			this._size = 0;
			this._version = 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Stack" /> class that contains elements copied from the specified collection and has the same initial capacity as the number of elements copied.</summary>
		/// <param name="col">The <see cref="T:System.Collections.ICollection" /> to copy elements from.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is <see langword="null" />.</exception>
		// Token: 0x06005CB8 RID: 23736 RVA: 0x00137964 File Offset: 0x00135B64
		public Stack(ICollection col) : this((col == null) ? 32 : col.Count)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			foreach (object obj in col)
			{
				this.Push(obj);
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06005CB9 RID: 23737 RVA: 0x001379AF File Offset: 0x00135BAF
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" />, if access to the <see cref="T:System.Collections.Stack" /> is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06005CBA RID: 23738 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06005CBB RID: 23739 RVA: 0x001379B7 File Offset: 0x00135BB7
		public virtual object SyncRoot
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

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Stack" />.</summary>
		// Token: 0x06005CBC RID: 23740 RVA: 0x001379D9 File Offset: 0x00135BD9
		public virtual void Clear()
		{
			Array.Clear(this._array, 0, this._size);
			this._size = 0;
			this._version++;
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x06005CBD RID: 23741 RVA: 0x00137A04 File Offset: 0x00135C04
		public virtual object Clone()
		{
			Stack stack = new Stack(this._size);
			stack._size = this._size;
			Array.Copy(this._array, 0, stack._array, 0, this._size);
			stack._version = this._version;
			return stack;
		}

		/// <summary>Determines whether an element is in the <see cref="T:System.Collections.Stack" />.</summary>
		/// <param name="obj">The object to locate in the <see cref="T:System.Collections.Stack" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" />, if <paramref name="obj" /> is found in the <see cref="T:System.Collections.Stack" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005CBE RID: 23742 RVA: 0x00137A50 File Offset: 0x00135C50
		public virtual bool Contains(object obj)
		{
			int size = this._size;
			while (size-- > 0)
			{
				if (obj == null)
				{
					if (this._array[size] == null)
					{
						return true;
					}
				}
				else if (this._array[size] != null && this._array[size].Equals(obj))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Stack" /> to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Stack" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Stack" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Stack" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06005CBF RID: 23743 RVA: 0x00137A9C File Offset: 0x00135C9C
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.", "array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (array.Length - index < this._size)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			int i = 0;
			object[] array2 = array as object[];
			if (array2 != null)
			{
				while (i < this._size)
				{
					array2[i + index] = this._array[this._size - i - 1];
					i++;
				}
				return;
			}
			while (i < this._size)
			{
				array.SetValue(this._array[this._size - i - 1], i + index);
				i++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x06005CC0 RID: 23744 RVA: 0x00137B58 File Offset: 0x00135D58
		public virtual IEnumerator GetEnumerator()
		{
			return new Stack.StackEnumerator(this);
		}

		/// <summary>Returns the object at the top of the <see cref="T:System.Collections.Stack" /> without removing it.</summary>
		/// <returns>The <see cref="T:System.Object" /> at the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty.</exception>
		// Token: 0x06005CC1 RID: 23745 RVA: 0x00137B60 File Offset: 0x00135D60
		public virtual object Peek()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("Stack empty.");
			}
			return this._array[this._size - 1];
		}

		/// <summary>Removes and returns the object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <returns>The <see cref="T:System.Object" /> removed from the top of the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.Stack" /> is empty.</exception>
		// Token: 0x06005CC2 RID: 23746 RVA: 0x00137B84 File Offset: 0x00135D84
		public virtual object Pop()
		{
			if (this._size == 0)
			{
				throw new InvalidOperationException("Stack empty.");
			}
			this._version++;
			object[] array = this._array;
			int num = this._size - 1;
			this._size = num;
			object result = array[num];
			this._array[this._size] = null;
			return result;
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Stack" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to push onto the <see cref="T:System.Collections.Stack" />. The value can be <see langword="null" />.</param>
		// Token: 0x06005CC3 RID: 23747 RVA: 0x00137BD8 File Offset: 0x00135DD8
		public virtual void Push(object obj)
		{
			if (this._size == this._array.Length)
			{
				object[] array = new object[2 * this._array.Length];
				Array.Copy(this._array, 0, array, 0, this._size);
				this._array = array;
			}
			object[] array2 = this._array;
			int size = this._size;
			this._size = size + 1;
			array2[size] = obj;
			this._version++;
		}

		/// <summary>Returns a synchronized (thread safe) wrapper for the <see cref="T:System.Collections.Stack" />.</summary>
		/// <param name="stack">The <see cref="T:System.Collections.Stack" /> to synchronize.</param>
		/// <returns>A synchronized wrapper around the <see cref="T:System.Collections.Stack" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stack" /> is <see langword="null" />.</exception>
		// Token: 0x06005CC4 RID: 23748 RVA: 0x00137C47 File Offset: 0x00135E47
		public static Stack Synchronized(Stack stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			return new Stack.SyncStack(stack);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Stack" /> to a new array.</summary>
		/// <returns>A new array containing copies of the elements of the <see cref="T:System.Collections.Stack" />.</returns>
		// Token: 0x06005CC5 RID: 23749 RVA: 0x00137C60 File Offset: 0x00135E60
		public virtual object[] ToArray()
		{
			if (this._size == 0)
			{
				return Array.Empty<object>();
			}
			object[] array = new object[this._size];
			for (int i = 0; i < this._size; i++)
			{
				array[i] = this._array[this._size - i - 1];
			}
			return array;
		}

		// Token: 0x040038B6 RID: 14518
		private object[] _array;

		// Token: 0x040038B7 RID: 14519
		private int _size;

		// Token: 0x040038B8 RID: 14520
		private int _version;

		// Token: 0x040038B9 RID: 14521
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040038BA RID: 14522
		private const int _defaultCapacity = 10;

		// Token: 0x02000A33 RID: 2611
		[Serializable]
		private class SyncStack : Stack
		{
			// Token: 0x06005CC6 RID: 23750 RVA: 0x00137CAD File Offset: 0x00135EAD
			internal SyncStack(Stack stack)
			{
				this._s = stack;
				this._root = stack.SyncRoot;
			}

			// Token: 0x17001027 RID: 4135
			// (get) Token: 0x06005CC7 RID: 23751 RVA: 0x000040F7 File Offset: 0x000022F7
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001028 RID: 4136
			// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x00137CC8 File Offset: 0x00135EC8
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x17001029 RID: 4137
			// (get) Token: 0x06005CC9 RID: 23753 RVA: 0x00137CD0 File Offset: 0x00135ED0
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._s.Count;
					}
					return count;
				}
			}

			// Token: 0x06005CCA RID: 23754 RVA: 0x00137D18 File Offset: 0x00135F18
			public override bool Contains(object obj)
			{
				object root = this._root;
				bool result;
				lock (root)
				{
					result = this._s.Contains(obj);
				}
				return result;
			}

			// Token: 0x06005CCB RID: 23755 RVA: 0x00137D60 File Offset: 0x00135F60
			public override object Clone()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = new Stack.SyncStack((Stack)this._s.Clone());
				}
				return result;
			}

			// Token: 0x06005CCC RID: 23756 RVA: 0x00137DB4 File Offset: 0x00135FB4
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._s.Clear();
				}
			}

			// Token: 0x06005CCD RID: 23757 RVA: 0x00137DFC File Offset: 0x00135FFC
			public override void CopyTo(Array array, int arrayIndex)
			{
				object root = this._root;
				lock (root)
				{
					this._s.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06005CCE RID: 23758 RVA: 0x00137E44 File Offset: 0x00136044
			public override void Push(object value)
			{
				object root = this._root;
				lock (root)
				{
					this._s.Push(value);
				}
			}

			// Token: 0x06005CCF RID: 23759 RVA: 0x00137E8C File Offset: 0x0013608C
			public override object Pop()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = this._s.Pop();
				}
				return result;
			}

			// Token: 0x06005CD0 RID: 23760 RVA: 0x00137ED4 File Offset: 0x001360D4
			public override IEnumerator GetEnumerator()
			{
				object root = this._root;
				IEnumerator enumerator;
				lock (root)
				{
					enumerator = this._s.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06005CD1 RID: 23761 RVA: 0x00137F1C File Offset: 0x0013611C
			public override object Peek()
			{
				object root = this._root;
				object result;
				lock (root)
				{
					result = this._s.Peek();
				}
				return result;
			}

			// Token: 0x06005CD2 RID: 23762 RVA: 0x00137F64 File Offset: 0x00136164
			public override object[] ToArray()
			{
				object root = this._root;
				object[] result;
				lock (root)
				{
					result = this._s.ToArray();
				}
				return result;
			}

			// Token: 0x040038BB RID: 14523
			private Stack _s;

			// Token: 0x040038BC RID: 14524
			private object _root;
		}

		// Token: 0x02000A34 RID: 2612
		[Serializable]
		private class StackEnumerator : IEnumerator, ICloneable
		{
			// Token: 0x06005CD3 RID: 23763 RVA: 0x00137FAC File Offset: 0x001361AC
			internal StackEnumerator(Stack stack)
			{
				this._stack = stack;
				this._version = this._stack._version;
				this._index = -2;
				this._currentElement = null;
			}

			// Token: 0x06005CD4 RID: 23764 RVA: 0x000231CD File Offset: 0x000213CD
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x06005CD5 RID: 23765 RVA: 0x00137FDC File Offset: 0x001361DC
			public virtual bool MoveNext()
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
				this._currentElement = null;
				return flag2;
			}

			// Token: 0x1700102A RID: 4138
			// (get) Token: 0x06005CD6 RID: 23766 RVA: 0x00138091 File Offset: 0x00136291
			public virtual object Current
			{
				get
				{
					if (this._index == -2)
					{
						throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
					}
					if (this._index == -1)
					{
						throw new InvalidOperationException("Enumeration already finished.");
					}
					return this._currentElement;
				}
			}

			// Token: 0x06005CD7 RID: 23767 RVA: 0x001380C2 File Offset: 0x001362C2
			public virtual void Reset()
			{
				if (this._version != this._stack._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = -2;
				this._currentElement = null;
			}

			// Token: 0x040038BD RID: 14525
			private Stack _stack;

			// Token: 0x040038BE RID: 14526
			private int _index;

			// Token: 0x040038BF RID: 14527
			private int _version;

			// Token: 0x040038C0 RID: 14528
			private object _currentElement;
		}

		// Token: 0x02000A35 RID: 2613
		internal class StackDebugView
		{
			// Token: 0x06005CD8 RID: 23768 RVA: 0x001380F1 File Offset: 0x001362F1
			public StackDebugView(Stack stack)
			{
				if (stack == null)
				{
					throw new ArgumentNullException("stack");
				}
				this._stack = stack;
			}

			// Token: 0x1700102B RID: 4139
			// (get) Token: 0x06005CD9 RID: 23769 RVA: 0x0013810E File Offset: 0x0013630E
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public object[] Items
			{
				get
				{
					return this._stack.ToArray();
				}
			}

			// Token: 0x040038C1 RID: 14529
			private Stack _stack;
		}
	}
}
