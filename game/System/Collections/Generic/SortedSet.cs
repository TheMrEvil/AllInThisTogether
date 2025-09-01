using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of objects that is maintained in sorted order.</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x020004E1 RID: 1249
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[Serializable]
	public class SortedSet<T> : ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class.</summary>
		// Token: 0x060028B6 RID: 10422 RVA: 0x0008BE0E File Offset: 0x0008A00E
		public SortedSet()
		{
			this.comparer = Comparer<T>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that uses a specified comparer.</summary>
		/// <param name="comparer">The default comparer to use for comparing objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x060028B7 RID: 10423 RVA: 0x0008BE21 File Offset: 0x0008A021
		public SortedSet(IComparer<T> comparer)
		{
			this.comparer = (comparer ?? Comparer<T>.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection.</summary>
		/// <param name="collection">The enumerable collection to be copied.</param>
		// Token: 0x060028B8 RID: 10424 RVA: 0x0008BE39 File Offset: 0x0008A039
		public SortedSet(IEnumerable<T> collection) : this(collection, Comparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains elements copied from a specified enumerable collection and that uses a specified comparer.</summary>
		/// <param name="collection">The enumerable collection to be copied.</param>
		/// <param name="comparer">The default comparer to use for comparing objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is <see langword="null" />.</exception>
		// Token: 0x060028B9 RID: 10425 RVA: 0x0008BE48 File Offset: 0x0008A048
		public SortedSet(IEnumerable<T> collection, IComparer<T> comparer) : this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			SortedSet<T> sortedSet = collection as SortedSet<T>;
			if (sortedSet != null && !(sortedSet is SortedSet<T>.TreeSubSet) && this.HasEqualComparer(sortedSet))
			{
				if (sortedSet.Count > 0)
				{
					this.count = sortedSet.count;
					this.root = sortedSet.root.DeepClone(this.count);
				}
				return;
			}
			int num;
			T[] array = EnumerableHelpers.ToArray<T>(collection, out num);
			if (num > 0)
			{
				comparer = this.comparer;
				Array.Sort<T>(array, 0, num, comparer);
				int num2 = 1;
				for (int i = 1; i < num; i++)
				{
					if (comparer.Compare(array[i], array[i - 1]) != 0)
					{
						array[num2++] = array[i];
					}
				}
				num = num2;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedSet`1" /> class that contains serialized data.</summary>
		/// <param name="info">The object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">The structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		// Token: 0x060028BA RID: 10426 RVA: 0x0008BF29 File Offset: 0x0008A129
		protected SortedSet(SerializationInfo info, StreamingContext context)
		{
			this.siInfo = info;
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x0008BF38 File Offset: 0x0008A138
		private void AddAllElements(IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				if (!this.Contains(item))
				{
					this.Add(item);
				}
			}
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0008BF8C File Offset: 0x0008A18C
		private void RemoveAllElements(IEnumerable<T> collection)
		{
			T min = this.Min;
			T max = this.Max;
			foreach (T t in collection)
			{
				if (this.comparer.Compare(t, min) >= 0 && this.comparer.Compare(t, max) <= 0 && this.Contains(t))
				{
					this.Remove(t);
				}
			}
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0008C00C File Offset: 0x0008A20C
		private bool ContainsAllElements(IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				if (!this.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x0008C060 File Offset: 0x0008A260
		internal virtual bool InOrderTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(this.Count + 1));
			for (SortedSet<T>.Node node = this.root; node != null; node = node.Left)
			{
				stack.Push(node);
			}
			while (stack.Count != 0)
			{
				SortedSet<T>.Node node = stack.Pop();
				if (!action(node))
				{
					return false;
				}
				for (SortedSet<T>.Node node2 = node.Right; node2 != null; node2 = node2.Left)
				{
					stack.Push(node2);
				}
			}
			return true;
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0008C0E0 File Offset: 0x0008A2E0
		internal virtual bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
		{
			if (this.root == null)
			{
				return true;
			}
			Queue<SortedSet<T>.Node> queue = new Queue<SortedSet<T>.Node>();
			queue.Enqueue(this.root);
			while (queue.Count != 0)
			{
				SortedSet<T>.Node node = queue.Dequeue();
				if (!action(node))
				{
					return false;
				}
				if (node.Left != null)
				{
					queue.Enqueue(node.Left);
				}
				if (node.Right != null)
				{
					queue.Enqueue(node.Right);
				}
			}
			return true;
		}

		/// <summary>Gets the number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x0008C14E File Offset: 0x0008A34E
		public int Count
		{
			get
			{
				this.VersionCheck();
				return this.count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> object that is used to order the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>The comparer that is used to order the values in the <see cref="T:System.Collections.Generic.SortedSet`1" />.</returns>
		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x0008C15C File Offset: 0x0008A35C
		public IComparer<T> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x0008C164 File Offset: 0x0008A364
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void VersionCheck()
		{
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0000390E File Offset: 0x00001B0E
		internal virtual bool IsWithinRange(T item)
		{
			return true;
		}

		/// <summary>Adds an element to the set and returns a value that indicates if it was successfully added.</summary>
		/// <param name="item">The element to add to the set.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="item" /> is added to the set; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028C7 RID: 10439 RVA: 0x0008C186 File Offset: 0x0008A386
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x0008C18F File Offset: 0x0008A38F
		void ICollection<!0>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0008C19C File Offset: 0x0008A39C
		internal virtual bool AddIfNotPresent(T item)
		{
			if (this.root == null)
			{
				this.root = new SortedSet<T>.Node(item, NodeColor.Black);
				this.count = 1;
				this.version++;
				return true;
			}
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node greatGrandParent = null;
			this.version++;
			int num = 0;
			while (node != null)
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					this.root.ColorBlack();
					return false;
				}
				if (node.Is4Node)
				{
					node.Split4Node();
					if (SortedSet<T>.Node.IsNonNullRed(node2))
					{
						this.InsertionBalance(node, ref node2, node3, greatGrandParent);
					}
				}
				greatGrandParent = node3;
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			SortedSet<T>.Node node4 = new SortedSet<T>.Node(item, NodeColor.Red);
			if (num > 0)
			{
				node2.Right = node4;
			}
			else
			{
				node2.Left = node4;
			}
			if (node2.IsRed)
			{
				this.InsertionBalance(node4, ref node2, node3, greatGrandParent);
			}
			this.root.ColorBlack();
			this.count++;
			return true;
		}

		/// <summary>Removes a specified item from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <param name="item">The element to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the element is found and successfully removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028CA RID: 10442 RVA: 0x0008C2A6 File Offset: 0x0008A4A6
		public bool Remove(T item)
		{
			return this.DoRemove(item);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0008C2B0 File Offset: 0x0008A4B0
		internal virtual bool DoRemove(T item)
		{
			if (this.root == null)
			{
				return false;
			}
			this.version++;
			SortedSet<T>.Node node = this.root;
			SortedSet<T>.Node node2 = null;
			SortedSet<T>.Node node3 = null;
			SortedSet<T>.Node node4 = null;
			SortedSet<T>.Node parentOfMatch = null;
			bool flag = false;
			while (node != null)
			{
				if (node.Is2Node)
				{
					if (node2 == null)
					{
						node.ColorRed();
					}
					else
					{
						SortedSet<T>.Node sibling = node2.GetSibling(node);
						if (sibling.IsRed)
						{
							if (node2.Right == sibling)
							{
								node2.RotateLeft();
							}
							else
							{
								node2.RotateRight();
							}
							node2.ColorRed();
							sibling.ColorBlack();
							this.ReplaceChildOrRoot(node3, node2, sibling);
							node3 = sibling;
							if (node2 == node4)
							{
								parentOfMatch = sibling;
							}
							sibling = node2.GetSibling(node);
						}
						if (sibling.Is2Node)
						{
							node2.Merge2Nodes();
						}
						else
						{
							SortedSet<T>.Node node5 = node2.Rotate(node2.GetRotation(node, sibling));
							node5.Color = node2.Color;
							node2.ColorBlack();
							node.ColorRed();
							this.ReplaceChildOrRoot(node3, node2, node5);
							if (node2 == node4)
							{
								parentOfMatch = node5;
							}
						}
					}
				}
				int num = flag ? -1 : this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					flag = true;
					node4 = node;
					parentOfMatch = node2;
				}
				node3 = node2;
				node2 = node;
				node = ((num < 0) ? node.Left : node.Right);
			}
			if (node4 != null)
			{
				this.ReplaceNode(node4, parentOfMatch, node2, node3);
				this.count--;
			}
			SortedSet<T>.Node node6 = this.root;
			if (node6 != null)
			{
				node6.ColorBlack();
			}
			return flag;
		}

		/// <summary>Removes all elements from the set.</summary>
		// Token: 0x060028CC RID: 10444 RVA: 0x0008C41C File Offset: 0x0008A61C
		public virtual void Clear()
		{
			this.root = null;
			this.count = 0;
			this.version++;
		}

		/// <summary>Determines whether the set contains a specific element.</summary>
		/// <param name="item">The element to locate in the set.</param>
		/// <returns>
		///   <see langword="true" /> if the set contains <paramref name="item" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060028CD RID: 10445 RVA: 0x0008C43A File Offset: 0x0008A63A
		public virtual bool Contains(T item)
		{
			return this.FindNode(item) != null;
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the beginning of the target array.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedSet`1" /> exceeds the number of elements that the destination array can contain.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		// Token: 0x060028CE RID: 10446 RVA: 0x0008C446 File Offset: 0x0008A646
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this.Count);
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x060028CF RID: 10447 RVA: 0x0008C456 File Offset: 0x0008A656
		public void CopyTo(T[] array, int index)
		{
			this.CopyTo(array, index, this.Count);
		}

		/// <summary>Copies a specified number of elements from <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.</exception>
		// Token: 0x060028D0 RID: 10448 RVA: 0x0008C468 File Offset: 0x0008A668
		public void CopyTo(T[] array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (count > array.Length - index)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			count += index;
			this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
			{
				if (index >= count)
				{
					return false;
				}
				T[] array2 = array;
				int index2 = index;
				index = index2 + 1;
				array2[index2] = node.Item;
				return true;
			});
		}

		/// <summary>Copies the complete <see cref="T:System.Collections.Generic.SortedSet`1" /> to a compatible one-dimensional array, starting at the specified array index.</summary>
		/// <param name="array">A one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedSet`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source array is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x060028D1 RID: 10449 RVA: 0x0008C528 File Offset: 0x0008A728
		void ICollection.CopyTo(Array array, int index)
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
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", index, "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			object[] objects = array as object[];
			if (objects == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
			try
			{
				this.InOrderTreeWalk(delegate(SortedSet<T>.Node node)
				{
					object[] objects = objects;
					int index2 = index;
					index = index2 + 1;
					objects[index2] = node.Item;
					return true;
				});
			}
			catch (ArrayTypeMismatchException)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection.", "array");
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <returns>An enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedSet`1" /> in sorted order.</returns>
		// Token: 0x060028D2 RID: 10450 RVA: 0x0008C63C File Offset: 0x0008A83C
		public SortedSet<T>.Enumerator GetEnumerator()
		{
			return new SortedSet<T>.Enumerator(this);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x0008C644 File Offset: 0x0008A844
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x060028D4 RID: 10452 RVA: 0x0008C644 File Offset: 0x0008A844
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x0008C654 File Offset: 0x0008A854
		private void InsertionBalance(SortedSet<T>.Node current, ref SortedSet<T>.Node parent, SortedSet<T>.Node grandParent, SortedSet<T>.Node greatGrandParent)
		{
			bool flag = grandParent.Right == parent;
			bool flag2 = parent.Right == current;
			SortedSet<T>.Node node;
			if (flag == flag2)
			{
				node = (flag2 ? grandParent.RotateLeft() : grandParent.RotateRight());
			}
			else
			{
				node = (flag2 ? grandParent.RotateLeftRight() : grandParent.RotateRightLeft());
				parent = greatGrandParent;
			}
			grandParent.ColorRed();
			node.ColorBlack();
			this.ReplaceChildOrRoot(greatGrandParent, grandParent, node);
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x0008C6B9 File Offset: 0x0008A8B9
		private void ReplaceChildOrRoot(SortedSet<T>.Node parent, SortedSet<T>.Node child, SortedSet<T>.Node newChild)
		{
			if (parent != null)
			{
				parent.ReplaceChild(child, newChild);
				return;
			}
			this.root = newChild;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0008C6D0 File Offset: 0x0008A8D0
		private void ReplaceNode(SortedSet<T>.Node match, SortedSet<T>.Node parentOfMatch, SortedSet<T>.Node successor, SortedSet<T>.Node parentOfSuccessor)
		{
			if (successor == match)
			{
				successor = match.Left;
			}
			else
			{
				SortedSet<T>.Node right = successor.Right;
				if (right != null)
				{
					right.ColorBlack();
				}
				if (parentOfSuccessor != match)
				{
					parentOfSuccessor.Left = successor.Right;
					successor.Right = match.Right;
				}
				successor.Left = match.Left;
			}
			if (successor != null)
			{
				successor.Color = match.Color;
			}
			this.ReplaceChildOrRoot(parentOfMatch, match, successor);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0008C740 File Offset: 0x0008A940
		internal virtual SortedSet<T>.Node FindNode(T item)
		{
			int num;
			for (SortedSet<T>.Node node = this.root; node != null; node = ((num < 0) ? node.Left : node.Right))
			{
				num = this.comparer.Compare(item, node.Item);
				if (num == 0)
				{
					return node;
				}
			}
			return null;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0008C788 File Offset: 0x0008A988
		internal virtual int InternalIndexOf(T item)
		{
			SortedSet<T>.Node node = this.root;
			int num = 0;
			while (node != null)
			{
				int num2 = this.comparer.Compare(item, node.Item);
				if (num2 == 0)
				{
					return num;
				}
				node = ((num2 < 0) ? node.Left : node.Right);
				num = ((num2 < 0) ? (2 * num + 1) : (2 * num + 2));
			}
			return -1;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x0008C7E0 File Offset: 0x0008A9E0
		internal SortedSet<T>.Node FindRange(T from, T to)
		{
			return this.FindRange(from, to, true, true);
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0008C7EC File Offset: 0x0008A9EC
		internal SortedSet<T>.Node FindRange(T from, T to, bool lowerBoundActive, bool upperBoundActive)
		{
			SortedSet<T>.Node node = this.root;
			while (node != null)
			{
				if (lowerBoundActive && this.comparer.Compare(from, node.Item) > 0)
				{
					node = node.Right;
				}
				else
				{
					if (!upperBoundActive || this.comparer.Compare(to, node.Item) >= 0)
					{
						return node;
					}
					node = node.Left;
				}
			}
			return null;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0008C84B File Offset: 0x0008AA4B
		internal void UpdateVersion()
		{
			this.version++;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object that can be used to create a collection that contains individual sets.</summary>
		/// <returns>A comparer for creating a collection of sets.</returns>
		// Token: 0x060028DD RID: 10461 RVA: 0x0008C85B File Offset: 0x0008AA5B
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer()
		{
			return SortedSet<T>.CreateSetComparer(null);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object, according to a specified comparer, that can be used to create a collection that contains individual sets.</summary>
		/// <param name="memberEqualityComparer">The comparer to use for creating the returned comparer.</param>
		/// <returns>A comparer for creating a collection of sets.</returns>
		// Token: 0x060028DE RID: 10462 RVA: 0x0008C863 File Offset: 0x0008AA63
		public static IEqualityComparer<SortedSet<T>> CreateSetComparer(IEqualityComparer<T> memberEqualityComparer)
		{
			return new SortedSetEqualityComparer<T>(memberEqualityComparer);
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x0008C86C File Offset: 0x0008AA6C
		internal static bool SortedSetEquals(SortedSet<T> set1, SortedSet<T> set2, IComparer<T> comparer)
		{
			if (set1 == null)
			{
				return set2 == null;
			}
			if (set2 == null)
			{
				return false;
			}
			if (set1.HasEqualComparer(set2))
			{
				return set1.Count == set2.Count && set1.SetEquals(set2);
			}
			bool flag = false;
			foreach (T x in set1)
			{
				flag = false;
				foreach (T y in set2)
				{
					if (comparer.Compare(x, y) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x0008C938 File Offset: 0x0008AB38
		private bool HasEqualComparer(SortedSet<T> other)
		{
			return this.Comparer == other.Comparer || this.Comparer.Equals(other.Comparer);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains all elements that are present in either the current object or the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028E1 RID: 10465 RVA: 0x0008C95C File Offset: 0x0008AB5C
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.count == 0)
			{
				SortedSet<T> sortedSet2 = new SortedSet<T>(sortedSet, this.comparer);
				this.root = sortedSet2.root;
				this.count = sortedSet2.count;
				this.version++;
				return;
			}
			if (sortedSet != null && treeSubSet == null && this.HasEqualComparer(sortedSet) && sortedSet.Count > this.Count / 2)
			{
				T[] array = new T[sortedSet.Count + this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						array[num++] = enumerator.Current;
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						array[num++] = enumerator2.Current;
						flag2 = !enumerator2.MoveNext();
					}
				}
				if (!flag || !flag2)
				{
					SortedSet<T>.Enumerator enumerator3 = flag ? enumerator2 : enumerator;
					do
					{
						array[num++] = enumerator3.Current;
					}
					while (enumerator3.MoveNext());
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.AddAllElements(other);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x0008CB48 File Offset: 0x0008AD48
		private static SortedSet<T>.Node ConstructRootFromSortedArray(T[] arr, int startIndex, int endIndex, SortedSet<T>.Node redNode)
		{
			int num = endIndex - startIndex + 1;
			SortedSet<T>.Node node;
			switch (num)
			{
			case 0:
				return null;
			case 1:
				node = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				if (redNode != null)
				{
					node.Left = redNode;
				}
				break;
			case 2:
				node = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				node.Right = new SortedSet<T>.Node(arr[endIndex], NodeColor.Black);
				node.Right.ColorRed();
				if (redNode != null)
				{
					node.Left = redNode;
				}
				break;
			case 3:
				node = new SortedSet<T>.Node(arr[startIndex + 1], NodeColor.Black);
				node.Left = new SortedSet<T>.Node(arr[startIndex], NodeColor.Black);
				node.Right = new SortedSet<T>.Node(arr[endIndex], NodeColor.Black);
				if (redNode != null)
				{
					node.Left.Left = redNode;
				}
				break;
			default:
			{
				int num2 = (startIndex + endIndex) / 2;
				node = new SortedSet<T>.Node(arr[num2], NodeColor.Black);
				node.Left = SortedSet<T>.ConstructRootFromSortedArray(arr, startIndex, num2 - 1, redNode);
				node.Right = ((num % 2 == 0) ? SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 2, endIndex, new SortedSet<T>.Node(arr[num2 + 1], NodeColor.Red)) : SortedSet<T>.ConstructRootFromSortedArray(arr, num2 + 1, endIndex, null));
				break;
			}
			}
			return node;
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are also in a specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028E3 RID: 10467 RVA: 0x0008CC74 File Offset: 0x0008AE74
		public virtual void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return;
			}
			if (other == this)
			{
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			SortedSet<T>.TreeSubSet treeSubSet = this as SortedSet<T>.TreeSubSet;
			if (treeSubSet != null)
			{
				this.VersionCheck();
			}
			if (sortedSet != null && treeSubSet == null && this.HasEqualComparer(sortedSet))
			{
				T[] array = new T[this.Count];
				int num = 0;
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				T max = this.Max;
				T min = this.Min;
				while (!flag && !flag2 && this.Comparer.Compare(enumerator2.Current, max) <= 0)
				{
					int num2 = this.Comparer.Compare(enumerator.Current, enumerator2.Current);
					if (num2 < 0)
					{
						flag = !enumerator.MoveNext();
					}
					else if (num2 == 0)
					{
						array[num++] = enumerator2.Current;
						flag = !enumerator.MoveNext();
						flag2 = !enumerator2.MoveNext();
					}
					else
					{
						flag2 = !enumerator2.MoveNext();
					}
				}
				this.root = null;
				this.root = SortedSet<T>.ConstructRootFromSortedArray(array, 0, num - 1, null);
				this.count = num;
				this.version++;
				return;
			}
			this.IntersectWithEnumerable(other);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0008CDD4 File Offset: 0x0008AFD4
		internal virtual void IntersectWithEnumerable(IEnumerable<T> other)
		{
			List<T> list = new List<T>(this.Count);
			foreach (T item in other)
			{
				if (this.Contains(item))
				{
					list.Add(item);
				}
			}
			this.Clear();
			foreach (T item2 in list)
			{
				this.Add(item2);
			}
		}

		/// <summary>Removes all elements that are in a specified collection from the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="other">The collection of items to remove from the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028E5 RID: 10469 RVA: 0x0008CE78 File Offset: 0x0008B078
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				if (this.comparer.Compare(sortedSet.Max, this.Min) < 0 || this.comparer.Compare(sortedSet.Min, this.Max) > 0)
				{
					return;
				}
				T min = this.Min;
				T max = this.Max;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						if (this.comparer.Compare(t, min) >= 0)
						{
							if (this.comparer.Compare(t, max) > 0)
							{
								break;
							}
							this.Remove(t);
						}
					}
					return;
				}
			}
			this.RemoveAllElements(other);
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object so that it contains only elements that are present either in the current object or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028E6 RID: 10470 RVA: 0x0008CF70 File Offset: 0x0008B170
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				this.SymmetricExceptWithSameComparer(sortedSet);
				return;
			}
			int length;
			T[] array = EnumerableHelpers.ToArray<T>(other, out length);
			Array.Sort<T>(array, 0, length, this.Comparer);
			this.SymmetricExceptWithSameComparer(array, length);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0008CFE0 File Offset: 0x0008B1E0
		private void SymmetricExceptWithSameComparer(SortedSet<T> other)
		{
			foreach (T item in other)
			{
				if (!this.Contains(item))
				{
					this.Add(item);
				}
				else
				{
					this.Remove(item);
				}
			}
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0008D044 File Offset: 0x0008B244
		private void SymmetricExceptWithSameComparer(T[] other, int count)
		{
			if (count == 0)
			{
				return;
			}
			T y = other[0];
			for (int i = 0; i < count; i++)
			{
				while (i < count && i != 0 && this.comparer.Compare(other[i], y) == 0)
				{
					i++;
				}
				if (i >= count)
				{
					break;
				}
				T t = other[i];
				if (!this.Contains(t))
				{
					this.Add(t);
				}
				else
				{
					this.Remove(t);
				}
				y = t;
			}
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028E9 RID: 10473 RVA: 0x0008D0B4 File Offset: 0x0008B2B4
		[SecuritySafeCritical]
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				return this.Count <= sortedSet.Count && this.IsSubsetOfSortedSetWithSameComparer(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount >= 0;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0008D12C File Offset: 0x0008B32C
		private bool IsSubsetOfSortedSetWithSameComparer(SortedSet<T> asSorted)
		{
			SortedSet<T> viewBetween = asSorted.GetViewBetween(this.Min, this.Max);
			foreach (T item in this)
			{
				if (!viewBetween.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028EB RID: 10475 RVA: 0x0008D198 File Offset: 0x0008B398
		[SecuritySafeCritical]
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && this.Count == 0)
			{
				return (other as ICollection).Count > 0;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				return this.Count < sortedSet.Count && this.IsSubsetOfSortedSetWithSameComparer(sortedSet);
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount > 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028EC RID: 10476 RVA: 0x0008D220 File Offset: 0x0008B420
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !this.HasEqualComparer(sortedSet))
			{
				return this.ContainsAllElements(other);
			}
			if (this.Count < sortedSet.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T item in sortedSet)
			{
				if (!viewBetween.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object is a proper superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028ED RID: 10477 RVA: 0x0008D2DC File Offset: 0x0008B4DC
		[SecuritySafeCritical]
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection && (other as ICollection).Count == 0)
			{
				return true;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet == null || !this.HasEqualComparer(sortedSet))
			{
				SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
				return elementCount.UniqueCount < this.Count && elementCount.UnfoundCount == 0;
			}
			if (sortedSet.Count >= this.Count)
			{
				return false;
			}
			SortedSet<T> viewBetween = this.GetViewBetween(sortedSet.Min, sortedSet.Max);
			foreach (T item in sortedSet)
			{
				if (!viewBetween.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and the specified collection contain the same elements.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028EE RID: 10478 RVA: 0x0008D3C0 File Offset: 0x0008B5C0
		[SecuritySafeCritical]
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet))
			{
				SortedSet<T>.Enumerator enumerator = this.GetEnumerator();
				SortedSet<T>.Enumerator enumerator2 = sortedSet.GetEnumerator();
				bool flag = !enumerator.MoveNext();
				bool flag2 = !enumerator2.MoveNext();
				while (!flag && !flag2)
				{
					if (this.Comparer.Compare(enumerator.Current, enumerator2.Current) != 0)
					{
						return false;
					}
					flag = !enumerator.MoveNext();
					flag2 = !enumerator2.MoveNext();
				}
				return flag && flag2;
			}
			SortedSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.UniqueCount == this.Count && elementCount.UnfoundCount == 0;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object and a specified collection share common elements.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedSet`1" /> object and <paramref name="other" /> share at least one common element; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x060028EF RID: 10479 RVA: 0x0008D47C File Offset: 0x0008B67C
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.Count == 0)
			{
				return false;
			}
			if (other is ICollection<!0> && (other as ICollection<!0>).Count == 0)
			{
				return false;
			}
			SortedSet<T> sortedSet = other as SortedSet<T>;
			if (sortedSet != null && this.HasEqualComparer(sortedSet) && (this.comparer.Compare(this.Min, sortedSet.Max) > 0 || this.comparer.Compare(this.Max, sortedSet.Min) < 0))
			{
				return false;
			}
			foreach (T item in other)
			{
				if (this.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0008D548 File Offset: 0x0008B748
		private unsafe SortedSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			SortedSet<T>.ElementCount result;
			if (this.Count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						!0 ! = enumerator.Current;
						num++;
					}
				}
				result.UniqueCount = 0;
				result.UnfoundCount = num;
				return result;
			}
			int num2 = BitHelper.ToIntArrayLength(this.Count);
			BitHelper bitHelper;
			if (num2 <= 100)
			{
				bitHelper = new BitHelper(stackalloc int[checked(unchecked((UIntPtr)num2) * 4)], num2);
			}
			else
			{
				bitHelper = new BitHelper(new int[num2], num2);
			}
			int num3 = 0;
			int num4 = 0;
			foreach (T item in other)
			{
				int num5 = this.InternalIndexOf(item);
				if (num5 >= 0)
				{
					if (!bitHelper.IsMarked(num5))
					{
						bitHelper.MarkBit(num5);
						num4++;
					}
				}
				else
				{
					num3++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			result.UniqueCount = num4;
			result.UnfoundCount = num3;
			return result;
		}

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <param name="match">The delegate that defines the conditions of the elements to remove.</param>
		/// <returns>The number of elements that were removed from the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x060028F1 RID: 10481 RVA: 0x0008D660 File Offset: 0x0008B860
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<T> matches = new List<T>(this.Count);
			this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
			{
				if (match(n.Item))
				{
					matches.Add(n.Item);
				}
				return true;
			});
			int num = 0;
			for (int i = matches.Count - 1; i >= 0; i--)
			{
				if (this.Remove(matches[i]))
				{
					num++;
				}
			}
			return num;
		}

		/// <summary>Gets the minimum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The minimum value in the set.</returns>
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x0008D6E4 File Offset: 0x0008B8E4
		public T Min
		{
			get
			{
				return this.MinInternal;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x0008D6EC File Offset: 0x0008B8EC
		internal virtual T MinInternal
		{
			get
			{
				if (this.root == null)
				{
					return default(T);
				}
				SortedSet<T>.Node left = this.root;
				while (left.Left != null)
				{
					left = left.Left;
				}
				return left.Item;
			}
		}

		/// <summary>Gets the maximum value in the <see cref="T:System.Collections.Generic.SortedSet`1" />, as defined by the comparer.</summary>
		/// <returns>The maximum value in the set.</returns>
		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060028F4 RID: 10484 RVA: 0x0008D729 File Offset: 0x0008B929
		public T Max
		{
			get
			{
				return this.MaxInternal;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x0008D734 File Offset: 0x0008B934
		internal virtual T MaxInternal
		{
			get
			{
				if (this.root == null)
				{
					return default(T);
				}
				SortedSet<T>.Node right = this.root;
				while (right.Right != null)
				{
					right = right.Right;
				}
				return right.Item;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.Generic.IEnumerable`1" /> that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</summary>
		/// <returns>An enumerator that iterates over the <see cref="T:System.Collections.Generic.SortedSet`1" /> in reverse order.</returns>
		// Token: 0x060028F6 RID: 10486 RVA: 0x0008D771 File Offset: 0x0008B971
		public IEnumerable<T> Reverse()
		{
			SortedSet<T>.Enumerator e = new SortedSet<T>.Enumerator(this, true);
			while (e.MoveNext())
			{
				T t = e.Current;
				yield return t;
			}
			yield break;
		}

		/// <summary>Returns a view of a subset in a <see cref="T:System.Collections.Generic.SortedSet`1" />.</summary>
		/// <param name="lowerValue">The lowest desired value in the view.</param>
		/// <param name="upperValue">The highest desired value in the view.</param>
		/// <returns>A subset view that contains only the values in the specified range.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="lowerValue" /> is more than <paramref name="upperValue" /> according to the comparer.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">A tried operation on the view was outside the range specified by <paramref name="lowerValue" /> and <paramref name="upperValue" />.</exception>
		// Token: 0x060028F7 RID: 10487 RVA: 0x0008D781 File Offset: 0x0008B981
		public virtual SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
		{
			if (this.Comparer.Compare(lowerValue, upperValue) > 0)
			{
				throw new ArgumentException("Must be less than or equal to upperValue.", "lowerValue");
			}
			return new SortedSet<T>.TreeSubSet(this, lowerValue, upperValue, true, true);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and returns the data that you need to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060028F8 RID: 10488 RVA: 0x0008D7AD File Offset: 0x0008B9AD
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.GetObjectData(info, context);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data that you must have to serialize a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information that is required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060028F9 RID: 10489 RVA: 0x0008D7B8 File Offset: 0x0008B9B8
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Count", this.count);
			info.AddValue("Comparer", this.comparer, typeof(IComparer<T>));
			info.AddValue("Version", this.version);
			if (this.root != null)
			{
				T[] array = new T[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("Items", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.IDeserializationCallback" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
		// Token: 0x060028FA RID: 10490 RVA: 0x0008D842 File Offset: 0x0008BA42
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface, and raises the deserialization event when the deserialization is completed.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> object is invalid.</exception>
		// Token: 0x060028FB RID: 10491 RVA: 0x0008D84C File Offset: 0x0008BA4C
		protected virtual void OnDeserialization(object sender)
		{
			if (this.comparer != null)
			{
				return;
			}
			if (this.siInfo == null)
			{
				throw new SerializationException("OnDeserialization method was called while the object was not being deserialized.");
			}
			this.comparer = (IComparer<T>)this.siInfo.GetValue("Comparer", typeof(IComparer<T>));
			int @int = this.siInfo.GetInt32("Count");
			if (@int != 0)
			{
				T[] array = (T[])this.siInfo.GetValue("Items", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The values for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.Add(array[i]);
				}
			}
			this.version = this.siInfo.GetInt32("Version");
			if (this.count != @int)
			{
				throw new SerializationException("The serialized Count information doesn't match the number of items.");
			}
			this.siInfo = null;
		}

		/// <summary>Searches the set for a given value and returns the equal value it finds, if any.</summary>
		/// <param name="equalValue">The value to search for.</param>
		/// <param name="actualValue">The value from the set that the search found, or the default value of T when the search yielded no match.</param>
		/// <returns>A value indicating whether the search was successful.</returns>
		// Token: 0x060028FC RID: 10492 RVA: 0x0008D92C File Offset: 0x0008BB2C
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			SortedSet<T>.Node node = this.FindNode(equalValue);
			if (node != null)
			{
				actualValue = node.Item;
				return true;
			}
			actualValue = default(T);
			return false;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x0008D95C File Offset: 0x0008BB5C
		private static int Log2(int value)
		{
			int num = 0;
			while (value > 0)
			{
				num++;
				value >>= 1;
			}
			return num;
		}

		// Token: 0x040015A5 RID: 5541
		private SortedSet<T>.Node root;

		// Token: 0x040015A6 RID: 5542
		private IComparer<T> comparer;

		// Token: 0x040015A7 RID: 5543
		private int count;

		// Token: 0x040015A8 RID: 5544
		private int version;

		// Token: 0x040015A9 RID: 5545
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x040015AA RID: 5546
		private SerializationInfo siInfo;

		// Token: 0x040015AB RID: 5547
		private const string ComparerName = "Comparer";

		// Token: 0x040015AC RID: 5548
		private const string CountName = "Count";

		// Token: 0x040015AD RID: 5549
		private const string ItemsName = "Items";

		// Token: 0x040015AE RID: 5550
		private const string VersionName = "Version";

		// Token: 0x040015AF RID: 5551
		private const string TreeName = "Tree";

		// Token: 0x040015B0 RID: 5552
		private const string NodeValueName = "Item";

		// Token: 0x040015B1 RID: 5553
		private const string EnumStartName = "EnumStarted";

		// Token: 0x040015B2 RID: 5554
		private const string ReverseName = "Reverse";

		// Token: 0x040015B3 RID: 5555
		private const string EnumVersionName = "EnumVersion";

		// Token: 0x040015B4 RID: 5556
		private const string MinName = "Min";

		// Token: 0x040015B5 RID: 5557
		private const string MaxName = "Max";

		// Token: 0x040015B6 RID: 5558
		private const string LowerBoundActiveName = "lBoundActive";

		// Token: 0x040015B7 RID: 5559
		private const string UpperBoundActiveName = "uBoundActive";

		// Token: 0x040015B8 RID: 5560
		internal const int StackAllocThreshold = 100;

		// Token: 0x020004E2 RID: 1250
		[Serializable]
		internal sealed class TreeSubSet : SortedSet<T>, ISerializable, IDeserializationCallback
		{
			// Token: 0x060028FE RID: 10494 RVA: 0x0008D97C File Offset: 0x0008BB7C
			public TreeSubSet(SortedSet<T> Underlying, T Min, T Max, bool lowerBoundActive, bool upperBoundActive) : base(Underlying.Comparer)
			{
				this._underlying = Underlying;
				this._min = Min;
				this._max = Max;
				this._lBoundActive = lowerBoundActive;
				this._uBoundActive = upperBoundActive;
				this.root = this._underlying.FindRange(this._min, this._max, this._lBoundActive, this._uBoundActive);
				this.count = 0;
				this.version = -1;
				this.VersionCheckImpl();
			}

			// Token: 0x060028FF RID: 10495 RVA: 0x0008D9F7 File Offset: 0x0008BBF7
			internal override bool AddIfNotPresent(T item)
			{
				if (!this.IsWithinRange(item))
				{
					throw new ArgumentOutOfRangeException("item");
				}
				bool result = this._underlying.AddIfNotPresent(item);
				this.VersionCheck();
				return result;
			}

			// Token: 0x06002900 RID: 10496 RVA: 0x0008DA1F File Offset: 0x0008BC1F
			public override bool Contains(T item)
			{
				this.VersionCheck();
				return base.Contains(item);
			}

			// Token: 0x06002901 RID: 10497 RVA: 0x0008DA2E File Offset: 0x0008BC2E
			internal override bool DoRemove(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return false;
				}
				bool result = this._underlying.Remove(item);
				this.VersionCheck();
				return result;
			}

			// Token: 0x06002902 RID: 10498 RVA: 0x0008DA50 File Offset: 0x0008BC50
			public override void Clear()
			{
				if (this.count == 0)
				{
					return;
				}
				List<T> toRemove = new List<T>();
				this.BreadthFirstTreeWalk(delegate(SortedSet<T>.Node n)
				{
					toRemove.Add(n.Item);
					return true;
				});
				while (toRemove.Count != 0)
				{
					this._underlying.Remove(toRemove[toRemove.Count - 1]);
					toRemove.RemoveAt(toRemove.Count - 1);
				}
				this.root = null;
				this.count = 0;
				this.version = this._underlying.version;
			}

			// Token: 0x06002903 RID: 10499 RVA: 0x0008DAF4 File Offset: 0x0008BCF4
			internal override bool IsWithinRange(T item)
			{
				return (this._lBoundActive ? base.Comparer.Compare(this._min, item) : -1) <= 0 && (this._uBoundActive ? base.Comparer.Compare(this._max, item) : 1) >= 0;
			}

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x06002904 RID: 10500 RVA: 0x0008DB48 File Offset: 0x0008BD48
			internal override T MinInternal
			{
				get
				{
					SortedSet<T>.Node node = this.root;
					T result = default(T);
					while (node != null)
					{
						int num = this._lBoundActive ? base.Comparer.Compare(this._min, node.Item) : -1;
						if (num == 1)
						{
							node = node.Right;
						}
						else
						{
							result = node.Item;
							if (num == 0)
							{
								break;
							}
							node = node.Left;
						}
					}
					return result;
				}
			}

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x06002905 RID: 10501 RVA: 0x0008DBAC File Offset: 0x0008BDAC
			internal override T MaxInternal
			{
				get
				{
					SortedSet<T>.Node node = this.root;
					T result = default(T);
					while (node != null)
					{
						int num = this._uBoundActive ? base.Comparer.Compare(this._max, node.Item) : 1;
						if (num == -1)
						{
							node = node.Left;
						}
						else
						{
							result = node.Item;
							if (num == 0)
							{
								break;
							}
							node = node.Right;
						}
					}
					return result;
				}
			}

			// Token: 0x06002906 RID: 10502 RVA: 0x0008DC10 File Offset: 0x0008BE10
			internal override bool InOrderTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(this.count + 1));
				SortedSet<T>.Node node = this.root;
				while (node != null)
				{
					if (this.IsWithinRange(node.Item))
					{
						stack.Push(node);
						node = node.Left;
					}
					else if (this._lBoundActive && base.Comparer.Compare(this._min, node.Item) > 0)
					{
						node = node.Right;
					}
					else
					{
						node = node.Left;
					}
				}
				while (stack.Count != 0)
				{
					node = stack.Pop();
					if (!action(node))
					{
						return false;
					}
					SortedSet<T>.Node node2 = node.Right;
					while (node2 != null)
					{
						if (this.IsWithinRange(node2.Item))
						{
							stack.Push(node2);
							node2 = node2.Left;
						}
						else if (this._lBoundActive && base.Comparer.Compare(this._min, node2.Item) > 0)
						{
							node2 = node2.Right;
						}
						else
						{
							node2 = node2.Left;
						}
					}
				}
				return true;
			}

			// Token: 0x06002907 RID: 10503 RVA: 0x0008DD18 File Offset: 0x0008BF18
			internal override bool BreadthFirstTreeWalk(TreeWalkPredicate<T> action)
			{
				this.VersionCheck();
				if (this.root == null)
				{
					return true;
				}
				Queue<SortedSet<T>.Node> queue = new Queue<SortedSet<T>.Node>();
				queue.Enqueue(this.root);
				while (queue.Count != 0)
				{
					SortedSet<T>.Node node = queue.Dequeue();
					if (this.IsWithinRange(node.Item) && !action(node))
					{
						return false;
					}
					if (node.Left != null && (!this._lBoundActive || base.Comparer.Compare(this._min, node.Item) < 0))
					{
						queue.Enqueue(node.Left);
					}
					if (node.Right != null && (!this._uBoundActive || base.Comparer.Compare(this._max, node.Item) > 0))
					{
						queue.Enqueue(node.Right);
					}
				}
				return true;
			}

			// Token: 0x06002908 RID: 10504 RVA: 0x0008DDE4 File Offset: 0x0008BFE4
			internal override SortedSet<T>.Node FindNode(T item)
			{
				if (!this.IsWithinRange(item))
				{
					return null;
				}
				this.VersionCheck();
				return base.FindNode(item);
			}

			// Token: 0x06002909 RID: 10505 RVA: 0x0008DE00 File Offset: 0x0008C000
			internal override int InternalIndexOf(T item)
			{
				int num = -1;
				foreach (T y in this)
				{
					num++;
					if (base.Comparer.Compare(item, y) == 0)
					{
						return num;
					}
				}
				return -1;
			}

			// Token: 0x0600290A RID: 10506 RVA: 0x0008DE64 File Offset: 0x0008C064
			internal override void VersionCheck()
			{
				this.VersionCheckImpl();
			}

			// Token: 0x0600290B RID: 10507 RVA: 0x0008DE6C File Offset: 0x0008C06C
			private void VersionCheckImpl()
			{
				if (this.version != this._underlying.version)
				{
					this.root = this._underlying.FindRange(this._min, this._max, this._lBoundActive, this._uBoundActive);
					this.version = this._underlying.version;
					this.count = 0;
					this.InOrderTreeWalk(delegate(SortedSet<T>.Node n)
					{
						this.count++;
						return true;
					});
				}
			}

			// Token: 0x0600290C RID: 10508 RVA: 0x0008DEE0 File Offset: 0x0008C0E0
			public override SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
			{
				if (this._lBoundActive && base.Comparer.Compare(this._min, lowerValue) > 0)
				{
					throw new ArgumentOutOfRangeException("lowerValue");
				}
				if (this._uBoundActive && base.Comparer.Compare(this._max, upperValue) < 0)
				{
					throw new ArgumentOutOfRangeException("upperValue");
				}
				return (SortedSet<T>.TreeSubSet)this._underlying.GetViewBetween(lowerValue, upperValue);
			}

			// Token: 0x0600290D RID: 10509 RVA: 0x0008D7AD File Offset: 0x0008B9AD
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				this.GetObjectData(info, context);
			}

			// Token: 0x0600290E RID: 10510 RVA: 0x00011F54 File Offset: 0x00010154
			protected override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x0600290F RID: 10511 RVA: 0x00011F54 File Offset: 0x00010154
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06002910 RID: 10512 RVA: 0x00011F54 File Offset: 0x00010154
			protected override void OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06002911 RID: 10513 RVA: 0x0008DF4F File Offset: 0x0008C14F
			[CompilerGenerated]
			private bool <VersionCheckImpl>b__20_0(SortedSet<T>.Node n)
			{
				this.count++;
				return true;
			}

			// Token: 0x040015B9 RID: 5561
			private SortedSet<T> _underlying;

			// Token: 0x040015BA RID: 5562
			private T _min;

			// Token: 0x040015BB RID: 5563
			private T _max;

			// Token: 0x040015BC RID: 5564
			private bool _lBoundActive;

			// Token: 0x040015BD RID: 5565
			private bool _uBoundActive;

			// Token: 0x020004E3 RID: 1251
			[CompilerGenerated]
			private sealed class <>c__DisplayClass9_0
			{
				// Token: 0x06002912 RID: 10514 RVA: 0x0000219B File Offset: 0x0000039B
				public <>c__DisplayClass9_0()
				{
				}

				// Token: 0x06002913 RID: 10515 RVA: 0x0008DF60 File Offset: 0x0008C160
				internal bool <Clear>b__0(SortedSet<T>.Node n)
				{
					this.toRemove.Add(n.Item);
					return true;
				}

				// Token: 0x040015BE RID: 5566
				public List<T> toRemove;
			}
		}

		// Token: 0x020004E4 RID: 1252
		[Serializable]
		internal sealed class Node
		{
			// Token: 0x06002914 RID: 10516 RVA: 0x0008DF74 File Offset: 0x0008C174
			public Node(T item, NodeColor color)
			{
				this.Item = item;
				this.Color = color;
			}

			// Token: 0x06002915 RID: 10517 RVA: 0x0008DF8A File Offset: 0x0008C18A
			public static bool IsNonNullBlack(SortedSet<T>.Node node)
			{
				return node != null && node.IsBlack;
			}

			// Token: 0x06002916 RID: 10518 RVA: 0x0008DF97 File Offset: 0x0008C197
			public static bool IsNonNullRed(SortedSet<T>.Node node)
			{
				return node != null && node.IsRed;
			}

			// Token: 0x06002917 RID: 10519 RVA: 0x0008DFA4 File Offset: 0x0008C1A4
			public static bool IsNullOrBlack(SortedSet<T>.Node node)
			{
				return node == null || node.IsBlack;
			}

			// Token: 0x17000876 RID: 2166
			// (get) Token: 0x06002918 RID: 10520 RVA: 0x0008DFB1 File Offset: 0x0008C1B1
			// (set) Token: 0x06002919 RID: 10521 RVA: 0x0008DFB9 File Offset: 0x0008C1B9
			public T Item
			{
				[CompilerGenerated]
				get
				{
					return this.<Item>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Item>k__BackingField = value;
				}
			}

			// Token: 0x17000877 RID: 2167
			// (get) Token: 0x0600291A RID: 10522 RVA: 0x0008DFC2 File Offset: 0x0008C1C2
			// (set) Token: 0x0600291B RID: 10523 RVA: 0x0008DFCA File Offset: 0x0008C1CA
			public SortedSet<T>.Node Left
			{
				[CompilerGenerated]
				get
				{
					return this.<Left>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Left>k__BackingField = value;
				}
			}

			// Token: 0x17000878 RID: 2168
			// (get) Token: 0x0600291C RID: 10524 RVA: 0x0008DFD3 File Offset: 0x0008C1D3
			// (set) Token: 0x0600291D RID: 10525 RVA: 0x0008DFDB File Offset: 0x0008C1DB
			public SortedSet<T>.Node Right
			{
				[CompilerGenerated]
				get
				{
					return this.<Right>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Right>k__BackingField = value;
				}
			}

			// Token: 0x17000879 RID: 2169
			// (get) Token: 0x0600291E RID: 10526 RVA: 0x0008DFE4 File Offset: 0x0008C1E4
			// (set) Token: 0x0600291F RID: 10527 RVA: 0x0008DFEC File Offset: 0x0008C1EC
			public NodeColor Color
			{
				[CompilerGenerated]
				get
				{
					return this.<Color>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Color>k__BackingField = value;
				}
			}

			// Token: 0x1700087A RID: 2170
			// (get) Token: 0x06002920 RID: 10528 RVA: 0x0008DFF5 File Offset: 0x0008C1F5
			public bool IsBlack
			{
				get
				{
					return this.Color == NodeColor.Black;
				}
			}

			// Token: 0x1700087B RID: 2171
			// (get) Token: 0x06002921 RID: 10529 RVA: 0x0008E000 File Offset: 0x0008C200
			public bool IsRed
			{
				get
				{
					return this.Color == NodeColor.Red;
				}
			}

			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x06002922 RID: 10530 RVA: 0x0008E00B File Offset: 0x0008C20B
			public bool Is2Node
			{
				get
				{
					return this.IsBlack && SortedSet<T>.Node.IsNullOrBlack(this.Left) && SortedSet<T>.Node.IsNullOrBlack(this.Right);
				}
			}

			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x06002923 RID: 10531 RVA: 0x0008E02F File Offset: 0x0008C22F
			public bool Is4Node
			{
				get
				{
					return SortedSet<T>.Node.IsNonNullRed(this.Left) && SortedSet<T>.Node.IsNonNullRed(this.Right);
				}
			}

			// Token: 0x06002924 RID: 10532 RVA: 0x0008E04B File Offset: 0x0008C24B
			public void ColorBlack()
			{
				this.Color = NodeColor.Black;
			}

			// Token: 0x06002925 RID: 10533 RVA: 0x0008E054 File Offset: 0x0008C254
			public void ColorRed()
			{
				this.Color = NodeColor.Red;
			}

			// Token: 0x06002926 RID: 10534 RVA: 0x0008E060 File Offset: 0x0008C260
			public SortedSet<T>.Node DeepClone(int count)
			{
				Stack<SortedSet<T>.Node> stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(count) + 2);
				Stack<SortedSet<T>.Node> stack2 = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(count) + 2);
				SortedSet<T>.Node node = this.ShallowClone();
				SortedSet<T>.Node node2 = this;
				SortedSet<T>.Node node3 = node;
				while (node2 != null)
				{
					stack.Push(node2);
					stack2.Push(node3);
					SortedSet<T>.Node node4 = node3;
					SortedSet<T>.Node left = node2.Left;
					node4.Left = ((left != null) ? left.ShallowClone() : null);
					node2 = node2.Left;
					node3 = node3.Left;
				}
				while (stack.Count != 0)
				{
					node2 = stack.Pop();
					node3 = stack2.Pop();
					SortedSet<T>.Node node5 = node2.Right;
					SortedSet<T>.Node node6 = (node5 != null) ? node5.ShallowClone() : null;
					node3.Right = node6;
					while (node5 != null)
					{
						stack.Push(node5);
						stack2.Push(node6);
						SortedSet<T>.Node node7 = node6;
						SortedSet<T>.Node left2 = node5.Left;
						node7.Left = ((left2 != null) ? left2.ShallowClone() : null);
						node5 = node5.Left;
						node6 = node6.Left;
					}
				}
				return node;
			}

			// Token: 0x06002927 RID: 10535 RVA: 0x0008E154 File Offset: 0x0008C354
			public TreeRotation GetRotation(SortedSet<T>.Node current, SortedSet<T>.Node sibling)
			{
				bool flag = this.Left == current;
				if (!SortedSet<T>.Node.IsNonNullRed(sibling.Left))
				{
					if (!flag)
					{
						return TreeRotation.LeftRight;
					}
					return TreeRotation.Left;
				}
				else
				{
					if (!flag)
					{
						return TreeRotation.Right;
					}
					return TreeRotation.RightLeft;
				}
			}

			// Token: 0x06002928 RID: 10536 RVA: 0x0008E185 File Offset: 0x0008C385
			public SortedSet<T>.Node GetSibling(SortedSet<T>.Node node)
			{
				if (node != this.Left)
				{
					return this.Left;
				}
				return this.Right;
			}

			// Token: 0x06002929 RID: 10537 RVA: 0x0008E19D File Offset: 0x0008C39D
			public SortedSet<T>.Node ShallowClone()
			{
				return new SortedSet<T>.Node(this.Item, this.Color);
			}

			// Token: 0x0600292A RID: 10538 RVA: 0x0008E1B0 File Offset: 0x0008C3B0
			public void Split4Node()
			{
				this.ColorRed();
				this.Left.ColorBlack();
				this.Right.ColorBlack();
			}

			// Token: 0x0600292B RID: 10539 RVA: 0x0008E1D0 File Offset: 0x0008C3D0
			public SortedSet<T>.Node Rotate(TreeRotation rotation)
			{
				switch (rotation)
				{
				case TreeRotation.Left:
					this.Right.Right.ColorBlack();
					return this.RotateLeft();
				case TreeRotation.LeftRight:
					return this.RotateLeftRight();
				case TreeRotation.Right:
					this.Left.Left.ColorBlack();
					return this.RotateRight();
				case TreeRotation.RightLeft:
					return this.RotateRightLeft();
				default:
					return null;
				}
			}

			// Token: 0x0600292C RID: 10540 RVA: 0x0008E234 File Offset: 0x0008C434
			public SortedSet<T>.Node RotateLeft()
			{
				SortedSet<T>.Node right = this.Right;
				this.Right = right.Left;
				right.Left = this;
				return right;
			}

			// Token: 0x0600292D RID: 10541 RVA: 0x0008E25C File Offset: 0x0008C45C
			public SortedSet<T>.Node RotateLeftRight()
			{
				SortedSet<T>.Node left = this.Left;
				SortedSet<T>.Node right = left.Right;
				this.Left = right.Right;
				right.Right = this;
				left.Right = right.Left;
				right.Left = left;
				return right;
			}

			// Token: 0x0600292E RID: 10542 RVA: 0x0008E2A0 File Offset: 0x0008C4A0
			public SortedSet<T>.Node RotateRight()
			{
				SortedSet<T>.Node left = this.Left;
				this.Left = left.Right;
				left.Right = this;
				return left;
			}

			// Token: 0x0600292F RID: 10543 RVA: 0x0008E2C8 File Offset: 0x0008C4C8
			public SortedSet<T>.Node RotateRightLeft()
			{
				SortedSet<T>.Node right = this.Right;
				SortedSet<T>.Node left = right.Left;
				this.Right = left.Left;
				left.Left = this;
				right.Left = left.Right;
				left.Right = right;
				return left;
			}

			// Token: 0x06002930 RID: 10544 RVA: 0x0008E30A File Offset: 0x0008C50A
			public void Merge2Nodes()
			{
				this.ColorBlack();
				this.Left.ColorRed();
				this.Right.ColorRed();
			}

			// Token: 0x06002931 RID: 10545 RVA: 0x0008E328 File Offset: 0x0008C528
			public void ReplaceChild(SortedSet<T>.Node child, SortedSet<T>.Node newChild)
			{
				if (this.Left == child)
				{
					this.Left = newChild;
					return;
				}
				this.Right = newChild;
			}

			// Token: 0x040015BF RID: 5567
			[CompilerGenerated]
			private T <Item>k__BackingField;

			// Token: 0x040015C0 RID: 5568
			[CompilerGenerated]
			private SortedSet<T>.Node <Left>k__BackingField;

			// Token: 0x040015C1 RID: 5569
			[CompilerGenerated]
			private SortedSet<T>.Node <Right>k__BackingField;

			// Token: 0x040015C2 RID: 5570
			[CompilerGenerated]
			private NodeColor <Color>k__BackingField;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedSet`1" /> object.</summary>
		/// <typeparam name="T" />
		// Token: 0x020004E5 RID: 1253
		[Serializable]
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator, ISerializable, IDeserializationCallback
		{
			// Token: 0x06002932 RID: 10546 RVA: 0x0008E342 File Offset: 0x0008C542
			internal Enumerator(SortedSet<T> set)
			{
				this = new SortedSet<T>.Enumerator(set, false);
			}

			// Token: 0x06002933 RID: 10547 RVA: 0x0008E34C File Offset: 0x0008C54C
			internal Enumerator(SortedSet<T> set, bool reverse)
			{
				this._tree = set;
				set.VersionCheck();
				this._version = set.version;
				this._stack = new Stack<SortedSet<T>.Node>(2 * SortedSet<T>.Log2(set.Count + 1));
				this._current = null;
				this._reverse = reverse;
				this.Initialize();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</summary>
			/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.SortedSet`1" /> instance.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="info" /> is <see langword="null" />.</exception>
			// Token: 0x06002934 RID: 10548 RVA: 0x00011F54 File Offset: 0x00010154
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
			/// <param name="sender">The source of the deserialization event.</param>
			/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.SortedSet`1" /> instance is invalid.</exception>
			// Token: 0x06002935 RID: 10549 RVA: 0x00011F54 File Offset: 0x00010154
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06002936 RID: 10550 RVA: 0x0008E3A0 File Offset: 0x0008C5A0
			private void Initialize()
			{
				this._current = null;
				SortedSet<T>.Node node = this._tree.root;
				while (node != null)
				{
					SortedSet<T>.Node node2 = this._reverse ? node.Right : node.Left;
					SortedSet<T>.Node node3 = this._reverse ? node.Left : node.Right;
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node2 == null || !this._tree.IsWithinRange(node2.Item))
					{
						node = node3;
					}
					else
					{
						node = node2;
					}
				}
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedSet`1" /> collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x06002937 RID: 10551 RVA: 0x0008E438 File Offset: 0x0008C638
			public bool MoveNext()
			{
				this._tree.VersionCheck();
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this._stack.Count == 0)
				{
					this._current = null;
					return false;
				}
				this._current = this._stack.Pop();
				SortedSet<T>.Node node = this._reverse ? this._current.Left : this._current.Right;
				while (node != null)
				{
					SortedSet<T>.Node node2 = this._reverse ? node.Right : node.Left;
					SortedSet<T>.Node node3 = this._reverse ? node.Left : node.Right;
					if (this._tree.IsWithinRange(node.Item))
					{
						this._stack.Push(node);
						node = node2;
					}
					else if (node3 == null || !this._tree.IsWithinRange(node3.Item))
					{
						node = node2;
					}
					else
					{
						node = node3;
					}
				}
				return true;
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedSet`1.Enumerator" />.</summary>
			// Token: 0x06002938 RID: 10552 RVA: 0x00003917 File Offset: 0x00001B17
			public void Dispose()
			{
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x1700087E RID: 2174
			// (get) Token: 0x06002939 RID: 10553 RVA: 0x0008E530 File Offset: 0x0008C730
			public T Current
			{
				get
				{
					if (this._current != null)
					{
						return this._current.Item;
					}
					return default(T);
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x1700087F RID: 2175
			// (get) Token: 0x0600293A RID: 10554 RVA: 0x0008E55A File Offset: 0x0008C75A
			object IEnumerator.Current
			{
				get
				{
					if (this._current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._current.Item;
				}
			}

			// Token: 0x17000880 RID: 2176
			// (get) Token: 0x0600293B RID: 10555 RVA: 0x0008E57F File Offset: 0x0008C77F
			internal bool NotStartedOrEnded
			{
				get
				{
					return this._current == null;
				}
			}

			// Token: 0x0600293C RID: 10556 RVA: 0x0008E58A File Offset: 0x0008C78A
			internal void Reset()
			{
				if (this._version != this._tree.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._stack.Clear();
				this.Initialize();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x0600293D RID: 10557 RVA: 0x0008E5BB File Offset: 0x0008C7BB
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x0600293E RID: 10558 RVA: 0x0008E5C4 File Offset: 0x0008C7C4
			// Note: this type is marked as 'beforefieldinit'.
			static Enumerator()
			{
			}

			// Token: 0x040015C3 RID: 5571
			private static readonly SortedSet<T>.Node s_dummyNode = new SortedSet<T>.Node(default(T), NodeColor.Red);

			// Token: 0x040015C4 RID: 5572
			private SortedSet<T> _tree;

			// Token: 0x040015C5 RID: 5573
			private int _version;

			// Token: 0x040015C6 RID: 5574
			private Stack<SortedSet<T>.Node> _stack;

			// Token: 0x040015C7 RID: 5575
			private SortedSet<T>.Node _current;

			// Token: 0x040015C8 RID: 5576
			private bool _reverse;
		}

		// Token: 0x020004E6 RID: 1254
		internal struct ElementCount
		{
			// Token: 0x040015C9 RID: 5577
			internal int UniqueCount;

			// Token: 0x040015CA RID: 5578
			internal int UnfoundCount;
		}

		// Token: 0x020004E7 RID: 1255
		[CompilerGenerated]
		private sealed class <>c__DisplayClass52_0
		{
			// Token: 0x0600293F RID: 10559 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass52_0()
			{
			}

			// Token: 0x06002940 RID: 10560 RVA: 0x0008E5E8 File Offset: 0x0008C7E8
			internal bool <CopyTo>b__0(SortedSet<T>.Node node)
			{
				if (this.index >= this.count)
				{
					return false;
				}
				T[] array = this.array;
				int num = this.index;
				this.index = num + 1;
				array[num] = node.Item;
				return true;
			}

			// Token: 0x040015CB RID: 5579
			public int index;

			// Token: 0x040015CC RID: 5580
			public int count;

			// Token: 0x040015CD RID: 5581
			public T[] array;
		}

		// Token: 0x020004E8 RID: 1256
		[CompilerGenerated]
		private sealed class <>c__DisplayClass53_0
		{
			// Token: 0x06002941 RID: 10561 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass53_0()
			{
			}

			// Token: 0x06002942 RID: 10562 RVA: 0x0008E628 File Offset: 0x0008C828
			internal bool <System.Collections.ICollection.CopyTo>b__0(SortedSet<T>.Node node)
			{
				object[] array = this.objects;
				int num = this.index;
				this.index = num + 1;
				array[num] = node.Item;
				return true;
			}

			// Token: 0x040015CE RID: 5582
			public int index;

			// Token: 0x040015CF RID: 5583
			public object[] objects;
		}

		// Token: 0x020004E9 RID: 1257
		[CompilerGenerated]
		private sealed class <>c__DisplayClass85_0
		{
			// Token: 0x06002943 RID: 10563 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass85_0()
			{
			}

			// Token: 0x06002944 RID: 10564 RVA: 0x0008E659 File Offset: 0x0008C859
			internal bool <RemoveWhere>b__0(SortedSet<T>.Node n)
			{
				if (this.match(n.Item))
				{
					this.matches.Add(n.Item);
				}
				return true;
			}

			// Token: 0x040015D0 RID: 5584
			public Predicate<T> match;

			// Token: 0x040015D1 RID: 5585
			public List<T> matches;
		}

		// Token: 0x020004EA RID: 1258
		[CompilerGenerated]
		private sealed class <Reverse>d__94 : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06002945 RID: 10565 RVA: 0x0008E680 File Offset: 0x0008C880
			[DebuggerHidden]
			public <Reverse>d__94(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06002946 RID: 10566 RVA: 0x00003917 File Offset: 0x00001B17
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06002947 RID: 10567 RVA: 0x0008E69C File Offset: 0x0008C89C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				SortedSet<T> set = this;
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
					e = new SortedSet<T>.Enumerator(set, true);
				}
				if (!e.MoveNext())
				{
					return false;
				}
				this.<>2__current = e.Current;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000881 RID: 2177
			// (get) Token: 0x06002948 RID: 10568 RVA: 0x0008E705 File Offset: 0x0008C905
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002949 RID: 10569 RVA: 0x000044FA File Offset: 0x000026FA
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000882 RID: 2178
			// (get) Token: 0x0600294A RID: 10570 RVA: 0x0008E70D File Offset: 0x0008C90D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600294B RID: 10571 RVA: 0x0008E71C File Offset: 0x0008C91C
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				SortedSet<T>.<Reverse>d__94 <Reverse>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<Reverse>d__ = this;
				}
				else
				{
					<Reverse>d__ = new SortedSet<T>.<Reverse>d__94(0);
					<Reverse>d__.<>4__this = this;
				}
				return <Reverse>d__;
			}

			// Token: 0x0600294C RID: 10572 RVA: 0x0008E75F File Offset: 0x0008C95F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x040015D2 RID: 5586
			private int <>1__state;

			// Token: 0x040015D3 RID: 5587
			private T <>2__current;

			// Token: 0x040015D4 RID: 5588
			private int <>l__initialThreadId;

			// Token: 0x040015D5 RID: 5589
			public SortedSet<T> <>4__this;

			// Token: 0x040015D6 RID: 5590
			private SortedSet<T>.Enumerator <e>5__2;
		}
	}
}
