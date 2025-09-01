using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe last in-first out (LIFO) collection.</summary>
	/// <typeparam name="T">The type of the elements contained in the stack.</typeparam>
	// Token: 0x02000A5E RID: 2654
	[DebuggerTypeProxy(typeof(IProducerConsumerCollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class ConcurrentStack<T> : IProducerConsumerCollection<!0>, IEnumerable<!0>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> class.</summary>
		// Token: 0x06005F50 RID: 24400 RVA: 0x0000259F File Offset: 0x0000079F
		public ConcurrentStack()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> class that contains elements copied from the specified collection</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="collection" /> argument is null.</exception>
		// Token: 0x06005F51 RID: 24401 RVA: 0x00140998 File Offset: 0x0013EB98
		public ConcurrentStack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InitializeFromCollection(collection);
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x001409B8 File Offset: 0x0013EBB8
		private void InitializeFromCollection(IEnumerable<T> collection)
		{
			ConcurrentStack<T>.Node node = null;
			foreach (!0 value in collection)
			{
				node = new ConcurrentStack<T>.Node(value)
				{
					_next = node
				};
			}
			this._head = node;
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06005F53 RID: 24403 RVA: 0x00140A10 File Offset: 0x0013EC10
		public bool IsEmpty
		{
			get
			{
				return this._head == null;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</returns>
		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06005F54 RID: 24404 RVA: 0x00140A20 File Offset: 0x0013EC20
		public int Count
		{
			get
			{
				int num = 0;
				for (ConcurrentStack<T>.Node node = this._head; node != null; node = node._next)
				{
					num++;
				}
				return num;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>Always returns <see langword="false" /> to indicate access is not synchronized.</returns>
		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06005F55 RID: 24405 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>Returns null (Nothing in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The SyncRoot property is not supported</exception>
		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06005F56 RID: 24406 RVA: 0x0013E29F File Offset: 0x0013C49F
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		/// <summary>Removes all objects from the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		// Token: 0x06005F57 RID: 24407 RVA: 0x00140A49 File Offset: 0x0013EC49
		public void Clear()
		{
			this._head = null;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional. -or- <paramref name="array" /> does not have zero-based indexing. -or- <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. -or- The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06005F58 RID: 24408 RVA: 0x00140A54 File Offset: 0x0013EC54
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			((ICollection)this.ToList()).CopyTo(array, index);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06005F59 RID: 24409 RVA: 0x00140A71 File Offset: 0x0013EC71
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			this.ToList().CopyTo(array, index);
		}

		/// <summary>Inserts an object at the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		/// <param name="item">The object to push onto the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06005F5A RID: 24410 RVA: 0x00140A90 File Offset: 0x0013EC90
		public void Push(T item)
		{
			ConcurrentStack<T>.Node node = new ConcurrentStack<T>.Node(item);
			node._next = this._head;
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, node, node._next) == node._next)
			{
				return;
			}
			this.PushCore(node, node);
		}

		/// <summary>Inserts multiple objects at the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> atomically.</summary>
		/// <param name="items">The objects to push onto the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06005F5B RID: 24411 RVA: 0x00140AD5 File Offset: 0x0013ECD5
		public void PushRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.PushRange(items, 0, items.Length);
		}

		/// <summary>Inserts multiple objects at the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> atomically.</summary>
		/// <param name="items">The objects to push onto the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <param name="startIndex">The zero-based offset in <paramref name="items" /> at which to begin inserting elements onto the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <param name="count">The number of elements to be inserted onto the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is negative. Or <paramref name="startIndex" /> is greater than or equal to the length of <paramref name="items" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="items" />.</exception>
		// Token: 0x06005F5C RID: 24412 RVA: 0x00140AF0 File Offset: 0x0013ECF0
		public void PushRange(T[] items, int startIndex, int count)
		{
			ConcurrentStack<T>.ValidatePushPopRangeInput(items, startIndex, count);
			if (count == 0)
			{
				return;
			}
			ConcurrentStack<T>.Node node2;
			ConcurrentStack<T>.Node node = node2 = new ConcurrentStack<T>.Node(items[startIndex]);
			for (int i = startIndex + 1; i < startIndex + count; i++)
			{
				node2 = new ConcurrentStack<T>.Node(items[i])
				{
					_next = node2
				};
			}
			node._next = this._head;
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, node2, node._next) == node._next)
			{
				return;
			}
			this.PushCore(node2, node);
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x00140B70 File Offset: 0x0013ED70
		private void PushCore(ConcurrentStack<T>.Node head, ConcurrentStack<T>.Node tail)
		{
			SpinWait spinWait = default(SpinWait);
			do
			{
				spinWait.SpinOnce();
				tail._next = this._head;
			}
			while (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, head, tail._next) != tail._next);
			if (CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPushFailed(spinWait.Count);
			}
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x00140BD4 File Offset: 0x0013EDD4
		private static void ValidatePushPopRangeInput(T[] items, int startIndex, int count)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "The count argument must be greater than or equal to zero.");
			}
			int num = items.Length;
			if (startIndex >= num || startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", "The startIndex argument must be greater than or equal to zero.");
			}
			if (num - count < startIndex)
			{
				throw new ArgumentException("The sum of the startIndex and count arguments must be less than or equal to the collection's Count.");
			}
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x00140C30 File Offset: 0x0013EE30
		bool IProducerConsumerCollection<!0>.TryAdd(T item)
		{
			this.Push(item);
			return true;
		}

		/// <summary>Attempts to return an object from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> without removing it.</summary>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> or an unspecified value if the operation failed.</param>
		/// <returns>true if and object was returned successfully; otherwise, false.</returns>
		// Token: 0x06005F60 RID: 24416 RVA: 0x00140C3C File Offset: 0x0013EE3C
		public bool TryPeek(out T result)
		{
			ConcurrentStack<T>.Node head = this._head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			result = head._value;
			return true;
		}

		/// <summary>Attempts to pop and return the object at the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		/// <param name="result">When this method returns, if the operation was successful, <paramref name="result" /> contains the object removed. If no object was available to be removed, the value is unspecified.</param>
		/// <returns>true if an element was removed and returned from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> successfully; otherwise, false.</returns>
		// Token: 0x06005F61 RID: 24417 RVA: 0x00140C6C File Offset: 0x0013EE6C
		public bool TryPop(out T result)
		{
			ConcurrentStack<T>.Node head = this._head;
			if (head == null)
			{
				result = default(T);
				return false;
			}
			if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, head._next, head) == head)
			{
				result = head._value;
				return true;
			}
			return this.TryPopCore(out result);
		}

		/// <summary>Attempts to pop and return multiple objects from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> atomically.</summary>
		/// <param name="items">The <see cref="T:System.Array" /> to which objects popped from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> will be added.</param>
		/// <returns>The number of objects successfully popped from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> and inserted in <paramref name="items" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is a null argument (Nothing in Visual Basic).</exception>
		// Token: 0x06005F62 RID: 24418 RVA: 0x00140CB8 File Offset: 0x0013EEB8
		public int TryPopRange(T[] items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			return this.TryPopRange(items, 0, items.Length);
		}

		/// <summary>Attempts to pop and return multiple objects from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> atomically.</summary>
		/// <param name="items">The <see cref="T:System.Array" /> to which objects popped from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> will be added.</param>
		/// <param name="startIndex">The zero-based offset in <paramref name="items" /> at which to begin inserting elements from the top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</param>
		/// <param name="count">The number of elements to be popped from top of the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> and inserted into <paramref name="items" />.</param>
		/// <returns>The number of objects successfully popped from the top of the stack and inserted in <paramref name="items" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="items" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> or <paramref name="count" /> is negative. Or <paramref name="startIndex" /> is greater than or equal to the length of <paramref name="items" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> + <paramref name="count" /> is greater than the length of <paramref name="items" />.</exception>
		// Token: 0x06005F63 RID: 24419 RVA: 0x00140CD4 File Offset: 0x0013EED4
		public int TryPopRange(T[] items, int startIndex, int count)
		{
			ConcurrentStack<T>.ValidatePushPopRangeInput(items, startIndex, count);
			if (count == 0)
			{
				return 0;
			}
			ConcurrentStack<T>.Node head;
			int num = this.TryPopCore(count, out head);
			if (num > 0)
			{
				ConcurrentStack<T>.CopyRemovedItems(head, items, startIndex, num);
			}
			return num;
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x00140D08 File Offset: 0x0013EF08
		private bool TryPopCore(out T result)
		{
			ConcurrentStack<T>.Node node;
			if (this.TryPopCore(1, out node) == 1)
			{
				result = node._value;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x00140D38 File Offset: 0x0013EF38
		private int TryPopCore(int count, out ConcurrentStack<T>.Node poppedHead)
		{
			SpinWait spinWait = default(SpinWait);
			int num = 1;
			Random random = null;
			ConcurrentStack<T>.Node head;
			int num2;
			for (;;)
			{
				head = this._head;
				if (head == null)
				{
					break;
				}
				ConcurrentStack<T>.Node node = head;
				num2 = 1;
				while (num2 < count && node._next != null)
				{
					node = node._next;
					num2++;
				}
				if (Interlocked.CompareExchange<ConcurrentStack<T>.Node>(ref this._head, node._next, head) == head)
				{
					goto Block_5;
				}
				for (int i = 0; i < num; i++)
				{
					spinWait.SpinOnce();
				}
				if (spinWait.NextSpinWillYield)
				{
					if (random == null)
					{
						random = new Random();
					}
					num = random.Next(1, 8);
				}
				else
				{
					num *= 2;
				}
			}
			if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
			}
			poppedHead = null;
			return 0;
			Block_5:
			if (count == 1 && CDSCollectionETWBCLProvider.Log.IsEnabled())
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentStack_FastPopFailed(spinWait.Count);
			}
			poppedHead = head;
			return num2;
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x00140E24 File Offset: 0x0013F024
		private static void CopyRemovedItems(ConcurrentStack<T>.Node head, T[] collection, int startIndex, int nodesCount)
		{
			ConcurrentStack<T>.Node node = head;
			for (int i = startIndex; i < startIndex + nodesCount; i++)
			{
				collection[i] = node._value;
				node = node._next;
			}
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x00140E55 File Offset: 0x0013F055
		bool IProducerConsumerCollection<!0>.TryTake(out T item)
		{
			return this.TryPop(out item);
		}

		/// <summary>Copies the items stored in the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" /> to a new array.</summary>
		/// <returns>A new array containing a snapshot of elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</returns>
		// Token: 0x06005F68 RID: 24424 RVA: 0x00140E60 File Offset: 0x0013F060
		public T[] ToArray()
		{
			ConcurrentStack<T>.Node head = this._head;
			if (head != null)
			{
				return this.ToList(head).ToArray();
			}
			return Array.Empty<T>();
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x00140E8B File Offset: 0x0013F08B
		private List<T> ToList()
		{
			return this.ToList(this._head);
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x00140E9C File Offset: 0x0013F09C
		private List<T> ToList(ConcurrentStack<T>.Node curr)
		{
			List<T> list = new List<T>();
			while (curr != null)
			{
				list.Add(curr._value);
				curr = curr._next;
			}
			return list;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Collections.Concurrent.ConcurrentStack`1" />.</returns>
		// Token: 0x06005F6B RID: 24427 RVA: 0x00140EC9 File Offset: 0x0013F0C9
		public IEnumerator<T> GetEnumerator()
		{
			return this.GetEnumerator(this._head);
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x00140ED9 File Offset: 0x0013F0D9
		private IEnumerator<T> GetEnumerator(ConcurrentStack<T>.Node head)
		{
			for (ConcurrentStack<T>.Node current = head; current != null; current = current._next)
			{
				yield return current._value;
			}
			yield break;
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06005F6D RID: 24429 RVA: 0x0013E2AB File Offset: 0x0013C4AB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x04003956 RID: 14678
		private volatile ConcurrentStack<T>.Node _head;

		// Token: 0x04003957 RID: 14679
		private const int BACKOFF_MAX_YIELDS = 8;

		// Token: 0x02000A5F RID: 2655
		[Serializable]
		private class Node
		{
			// Token: 0x06005F6E RID: 24430 RVA: 0x00140EE8 File Offset: 0x0013F0E8
			internal Node(T value)
			{
				this._value = value;
				this._next = null;
			}

			// Token: 0x04003958 RID: 14680
			internal readonly T _value;

			// Token: 0x04003959 RID: 14681
			internal ConcurrentStack<T>.Node _next;
		}

		// Token: 0x02000A60 RID: 2656
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__35 : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06005F6F RID: 24431 RVA: 0x00140EFE File Offset: 0x0013F0FE
			[DebuggerHidden]
			public <GetEnumerator>d__35(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06005F70 RID: 24432 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06005F71 RID: 24433 RVA: 0x00140F10 File Offset: 0x0013F110
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
					current = current._next;
				}
				else
				{
					this.<>1__state = -1;
					current = head;
				}
				if (current == null)
				{
					return false;
				}
				this.<>2__current = current._value;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170010C3 RID: 4291
			// (get) Token: 0x06005F72 RID: 24434 RVA: 0x00140F7D File Offset: 0x0013F17D
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06005F73 RID: 24435 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170010C4 RID: 4292
			// (get) Token: 0x06005F74 RID: 24436 RVA: 0x00140F85 File Offset: 0x0013F185
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400395A RID: 14682
			private int <>1__state;

			// Token: 0x0400395B RID: 14683
			private T <>2__current;

			// Token: 0x0400395C RID: 14684
			public ConcurrentStack<T>.Node head;

			// Token: 0x0400395D RID: 14685
			private ConcurrentStack<T>.Node <current>5__2;
		}
	}
}
