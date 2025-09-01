using System;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000A1C RID: 2588
	[Serializable]
	internal class ListDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06005BA5 RID: 23461 RVA: 0x0000259F File Offset: 0x0000079F
		public ListDictionaryInternal()
		{
		}

		// Token: 0x17000FCE RID: 4046
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
				{
					if (next.key.Equals(key))
					{
						return next.value;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", "Key cannot be null.");
				}
				this.version++;
				ListDictionaryInternal.DictionaryNode dictionaryNode = null;
				ListDictionaryInternal.DictionaryNode next = this.head;
				while (next != null && !next.key.Equals(key))
				{
					dictionaryNode = next;
					next = next.next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06005BA8 RID: 23464 RVA: 0x00134D93 File Offset: 0x00132F93
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06005BA9 RID: 23465 RVA: 0x00134D9B File Offset: 0x00132F9B
		public ICollection Keys
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, true);
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06005BAB RID: 23467 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06005BAD RID: 23469 RVA: 0x00134DA4 File Offset: 0x00132FA4
		public object SyncRoot
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

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06005BAE RID: 23470 RVA: 0x00134DC6 File Offset: 0x00132FC6
		public ICollection Values
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, false);
			}
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x00134DD0 File Offset: 0x00132FD0
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					throw new ArgumentException(SR.Format("Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'", next.key, key));
				}
				dictionaryNode = next;
			}
			if (next != null)
			{
				next.value = value;
				return;
			}
			ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x00134E7A File Offset: 0x0013307A
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x00134E98 File Offset: 0x00133098
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x00134EDC File Offset: 0x001330DC
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException("Index was out of range. Must be non-negative and less than the size of the collection.", "index");
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x00134F74 File Offset: 0x00133174
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x00134F74 File Offset: 0x00133174
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x00134F7C File Offset: 0x0013317C
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "Key cannot be null.");
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next = this.head;
			while (next != null && !next.key.Equals(key))
			{
				dictionaryNode = next;
				next = next.next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x0400386E RID: 14446
		private ListDictionaryInternal.DictionaryNode head;

		// Token: 0x0400386F RID: 14447
		private int version;

		// Token: 0x04003870 RID: 14448
		private int count;

		// Token: 0x04003871 RID: 14449
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000A1D RID: 2589
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06005BB6 RID: 23478 RVA: 0x00135004 File Offset: 0x00133204
			public NodeEnumerator(ListDictionaryInternal list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x17000FD6 RID: 4054
			// (get) Token: 0x06005BB7 RID: 23479 RVA: 0x0013502D File Offset: 0x0013322D
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000FD7 RID: 4055
			// (get) Token: 0x06005BB8 RID: 23480 RVA: 0x0013503A File Offset: 0x0013323A
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x17000FD8 RID: 4056
			// (get) Token: 0x06005BB9 RID: 23481 RVA: 0x0013506A File Offset: 0x0013326A
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.current.key;
				}
			}

			// Token: 0x17000FD9 RID: 4057
			// (get) Token: 0x06005BBA RID: 23482 RVA: 0x0013508A File Offset: 0x0013328A
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.current.value;
				}
			}

			// Token: 0x06005BBB RID: 23483 RVA: 0x001350AC File Offset: 0x001332AC
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x06005BBC RID: 23484 RVA: 0x0013511B File Offset: 0x0013331B
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x04003872 RID: 14450
			private ListDictionaryInternal list;

			// Token: 0x04003873 RID: 14451
			private ListDictionaryInternal.DictionaryNode current;

			// Token: 0x04003874 RID: 14452
			private int version;

			// Token: 0x04003875 RID: 14453
			private bool start;
		}

		// Token: 0x02000A1E RID: 2590
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06005BBD RID: 23485 RVA: 0x00135149 File Offset: 0x00133349
			public NodeKeyValueCollection(ListDictionaryInternal list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x06005BBE RID: 23486 RVA: 0x00135160 File Offset: 0x00133360
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (array.Rank != 1)
				{
					throw new ArgumentException("Only single dimensional arrays are supported for the requested action.");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
				}
				if (array.Length - index < this.list.Count)
				{
					throw new ArgumentException("Index was out of range. Must be non-negative and less than the size of the collection.", "index");
				}
				for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x17000FDA RID: 4058
			// (get) Token: 0x06005BBF RID: 23487 RVA: 0x00135204 File Offset: 0x00133404
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x17000FDB RID: 4059
			// (get) Token: 0x06005BC0 RID: 23488 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000FDC RID: 4060
			// (get) Token: 0x06005BC1 RID: 23489 RVA: 0x00135230 File Offset: 0x00133430
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x06005BC2 RID: 23490 RVA: 0x0013523D File Offset: 0x0013343D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionaryInternal.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x04003876 RID: 14454
			private ListDictionaryInternal list;

			// Token: 0x04003877 RID: 14455
			private bool isKeys;

			// Token: 0x02000A1F RID: 2591
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x06005BC3 RID: 23491 RVA: 0x00135250 File Offset: 0x00133450
				public NodeKeyValueEnumerator(ListDictionaryInternal list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x17000FDD RID: 4061
				// (get) Token: 0x06005BC4 RID: 23492 RVA: 0x00135280 File Offset: 0x00133480
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException("Enumeration has either not started or has already finished.");
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x06005BC5 RID: 23493 RVA: 0x001352B4 File Offset: 0x001334B4
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x06005BC6 RID: 23494 RVA: 0x00135323 File Offset: 0x00133523
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04003878 RID: 14456
				private ListDictionaryInternal list;

				// Token: 0x04003879 RID: 14457
				private ListDictionaryInternal.DictionaryNode current;

				// Token: 0x0400387A RID: 14458
				private int version;

				// Token: 0x0400387B RID: 14459
				private bool isKeys;

				// Token: 0x0400387C RID: 14460
				private bool start;
			}
		}

		// Token: 0x02000A20 RID: 2592
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x06005BC7 RID: 23495 RVA: 0x0000259F File Offset: 0x0000079F
			public DictionaryNode()
			{
			}

			// Token: 0x0400387D RID: 14461
			public object key;

			// Token: 0x0400387E RID: 14462
			public object value;

			// Token: 0x0400387F RID: 14463
			public ListDictionaryInternal.DictionaryNode next;
		}
	}
}
