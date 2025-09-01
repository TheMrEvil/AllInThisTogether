using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a node in a <see cref="T:System.Collections.Generic.LinkedList`1" />. This class cannot be inherited.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	// Token: 0x020004CC RID: 1228
	public sealed class LinkedListNode<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> class, containing the specified value.</summary>
		/// <param name="value">The value to contain in the <see cref="T:System.Collections.Generic.LinkedListNode`1" />.</param>
		// Token: 0x060027CE RID: 10190 RVA: 0x00089C27 File Offset: 0x00087E27
		public LinkedListNode(T value)
		{
			this.item = value;
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x00089C36 File Offset: 0x00087E36
		internal LinkedListNode(LinkedList<T> list, T value)
		{
			this.list = list;
			this.item = value;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to.</summary>
		/// <returns>A reference to the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to, or <see langword="null" /> if the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> is not linked.</returns>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x00089C4C File Offset: 0x00087E4C
		public LinkedList<T> List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or <see langword="null" /> if the current node is the last element (<see cref="P:System.Collections.Generic.LinkedList`1.Last" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060027D1 RID: 10193 RVA: 0x00089C54 File Offset: 0x00087E54
		public LinkedListNode<T> Next
		{
			get
			{
				if (this.next != null && this.next != this.list.head)
				{
					return this.next;
				}
				return null;
			}
		}

		/// <summary>Gets the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or <see langword="null" /> if the current node is the first element (<see cref="P:System.Collections.Generic.LinkedList`1.First" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060027D2 RID: 10194 RVA: 0x00089C79 File Offset: 0x00087E79
		public LinkedListNode<T> Previous
		{
			get
			{
				if (this.prev != null && this != this.list.head)
				{
					return this.prev;
				}
				return null;
			}
		}

		/// <summary>Gets the value contained in the node.</summary>
		/// <returns>The value contained in the node.</returns>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060027D3 RID: 10195 RVA: 0x00089C99 File Offset: 0x00087E99
		// (set) Token: 0x060027D4 RID: 10196 RVA: 0x00089CA1 File Offset: 0x00087EA1
		public T Value
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x00089CAA File Offset: 0x00087EAA
		internal void Invalidate()
		{
			this.list = null;
			this.next = null;
			this.prev = null;
		}

		// Token: 0x0400156D RID: 5485
		internal LinkedList<T> list;

		// Token: 0x0400156E RID: 5486
		internal LinkedListNode<T> next;

		// Token: 0x0400156F RID: 5487
		internal LinkedListNode<T> prev;

		// Token: 0x04001570 RID: 5488
		internal T item;
	}
}
