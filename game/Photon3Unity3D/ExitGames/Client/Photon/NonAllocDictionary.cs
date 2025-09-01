using System;
using System.Collections;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000004 RID: 4
	public class NonAllocDictionary<K, V> : IDictionary<K, V>, ICollection<KeyValuePair<K, V>>, IEnumerable<KeyValuePair<K, V>>, IEnumerable where K : IEquatable<K>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002578 File Offset: 0x00000778
		public NonAllocDictionary<K, V>.KeyIterator Keys
		{
			get
			{
				return new NonAllocDictionary<K, V>.KeyIterator(this);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002590 File Offset: 0x00000790
		ICollection<V> IDictionary<!0, !1>.Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000025A8 File Offset: 0x000007A8
		ICollection<K> IDictionary<!0, !1>.Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000025C0 File Offset: 0x000007C0
		public NonAllocDictionary<K, V>.ValueIterator Values
		{
			get
			{
				return new NonAllocDictionary<K, V>.ValueIterator(this);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000025D8 File Offset: 0x000007D8
		public int Count
		{
			get
			{
				return this._usedCount - this._freeCount - 1;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000025FC File Offset: 0x000007FC
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002614 File Offset: 0x00000814
		public uint Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000262C File Offset: 0x0000082C
		public NonAllocDictionary(uint capacity = 29U)
		{
			this._capacity = (NonAllocDictionary<K, V>.IsPrimeFromList(capacity) ? capacity : NonAllocDictionary<K, V>.GetNextPrime(capacity));
			this._usedCount = 1;
			this._buckets = new int[this._capacity];
			this._nodes = new NonAllocDictionary<K, V>.Node[this._capacity];
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002684 File Offset: 0x00000884
		public bool ContainsKey(K key)
		{
			return this.FindNode(key) != 0;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026A0 File Offset: 0x000008A0
		public bool Contains(KeyValuePair<K, V> item)
		{
			int num = this.FindNode(item.Key);
			return num >= 0 && EqualityComparer<V>.Default.Equals(this._nodes[num].Val, item.Value);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026F4 File Offset: 0x000008F4
		public bool TryGetValue(K key, out V val)
		{
			int num = this.FindNode(key);
			bool flag = num != 0;
			bool result;
			if (flag)
			{
				val = this._nodes[num].Val;
				result = true;
			}
			else
			{
				val = default(V);
				result = false;
			}
			return result;
		}

		// Token: 0x1700000A RID: 10
		public V this[K key]
		{
			get
			{
				int num = this.FindNode(key);
				bool flag = num != 0;
				if (flag)
				{
					return this._nodes[num].Val;
				}
				string str = "Key does not exist: ";
				K k = key;
				throw new InvalidOperationException(str + ((k != null) ? k.ToString() : null));
			}
			set
			{
				int num = this.FindNode(key);
				bool flag = num == 0;
				if (flag)
				{
					this.Insert(key, value);
				}
				else
				{
					NonAllocDictionary<K, V>.Assert(this._nodes[num].Key.Equals(key));
					this._nodes[num].Val = value;
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002800 File Offset: 0x00000A00
		public void Set(K key, V val)
		{
			int num = this.FindNode(key);
			bool flag = num == 0;
			if (flag)
			{
				this.Insert(key, val);
			}
			else
			{
				NonAllocDictionary<K, V>.Assert(this._nodes[num].Key.Equals(key));
				this._nodes[num].Val = val;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002864 File Offset: 0x00000A64
		public void Add(K key, V val)
		{
			int num = this.FindNode(key);
			bool flag = num == 0;
			if (flag)
			{
				this.Insert(key, val);
				return;
			}
			string str = "Duplicate key ";
			K k = key;
			throw new InvalidOperationException(str + ((k != null) ? k.ToString() : null));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028BC File Offset: 0x00000ABC
		public void Add(KeyValuePair<K, V> item)
		{
			int num = this.FindNode(item.Key);
			bool flag = num == 0;
			if (flag)
			{
				this.Insert(item.Key, item.Value);
				return;
			}
			string str = "Duplicate key ";
			K key = item.Key;
			throw new InvalidOperationException(str + ((key != null) ? key.ToString() : null));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000292C File Offset: 0x00000B2C
		public bool Remove(K key)
		{
			uint hashCode = (uint)key.GetHashCode();
			int i = this._buckets[(int)(hashCode % this._capacity)];
			int num = 0;
			while (i != 0)
			{
				bool flag = this._nodes[i].Hash == hashCode && this._nodes[i].Key.Equals(key);
				if (flag)
				{
					bool flag2 = num == 0;
					if (flag2)
					{
						this._buckets[(int)(hashCode % this._capacity)] = this._nodes[i].Next;
					}
					else
					{
						this._nodes[num].Next = this._nodes[i].Next;
					}
					this._nodes[i].Used = false;
					this._nodes[i].Next = this._freeHead;
					this._nodes[i].Val = default(V);
					this._freeHead = i;
					this._freeCount++;
					return true;
				}
				num = i;
				i = this._nodes[i].Next;
			}
			return false;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002A74 File Offset: 0x00000C74
		public bool Remove(KeyValuePair<K, V> item)
		{
			bool flag = this.Contains(item);
			return flag && this.Remove(item.Key);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002AA4 File Offset: 0x00000CA4
		IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<!0, !1>>.GetEnumerator()
		{
			return new NonAllocDictionary<K, V>.PairIterator(this);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public NonAllocDictionary<K, V>.PairIterator GetEnumerator()
		{
			return new NonAllocDictionary<K, V>.PairIterator(this);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002ADC File Offset: 0x00000CDC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new NonAllocDictionary<K, V>.PairIterator(this);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002AFC File Offset: 0x00000CFC
		private int FindNode(K key)
		{
			uint hashCode = (uint)key.GetHashCode();
			for (int i = this._buckets[(int)(hashCode % this._capacity)]; i != 0; i = this._nodes[i].Next)
			{
				bool flag = this._nodes[i].Hash == hashCode && this._nodes[i].Key.Equals(key);
				if (flag)
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B8C File Offset: 0x00000D8C
		private void Insert(K key, V val)
		{
			bool flag = this._freeCount > 0;
			int num;
			if (flag)
			{
				num = this._freeHead;
				this._freeHead = this._nodes[num].Next;
				this._freeCount--;
			}
			else
			{
				bool flag2 = (long)this._usedCount == (long)((ulong)this._capacity);
				if (flag2)
				{
					this.Expand();
				}
				int usedCount = this._usedCount;
				this._usedCount = usedCount + 1;
				num = usedCount;
			}
			uint hashCode = (uint)key.GetHashCode();
			uint num2 = hashCode % this._capacity;
			this._nodes[num].Used = true;
			this._nodes[num].Hash = hashCode;
			this._nodes[num].Next = this._buckets[(int)num2];
			this._nodes[num].Key = key;
			this._nodes[num].Val = val;
			this._buckets[(int)num2] = num;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C90 File Offset: 0x00000E90
		private void Expand()
		{
			NonAllocDictionary<K, V>.Assert(this._buckets.Length == this._usedCount);
			uint nextPrime = NonAllocDictionary<K, V>.GetNextPrime(this._capacity);
			NonAllocDictionary<K, V>.Assert(nextPrime > this._capacity);
			int[] array = new int[nextPrime];
			NonAllocDictionary<K, V>.Node[] array2 = new NonAllocDictionary<K, V>.Node[nextPrime];
			Array.Copy(this._nodes, 0, array2, 0, this._nodes.Length);
			for (int i = 1; i < this._nodes.Length; i++)
			{
				NonAllocDictionary<K, V>.Assert(array2[i].Used);
				uint num = array2[i].Hash % nextPrime;
				array2[i].Next = array[(int)num];
				array[(int)num] = i;
			}
			this._nodes = array2;
			this._buckets = array;
			this._capacity = nextPrime;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002D5C File Offset: 0x00000F5C
		public void Clear()
		{
			bool flag = this._usedCount > 1;
			if (flag)
			{
				Array.Clear(this._nodes, 0, this._nodes.Length);
				Array.Clear(this._buckets, 0, this._buckets.Length);
				this._freeHead = 0;
				this._freeCount = 0;
				this._usedCount = 1;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002DB8 File Offset: 0x00000FB8
		void ICollection<KeyValuePair<!0, !1>>.CopyTo(KeyValuePair<K, V>[] array, int index)
		{
			bool flag = array == null;
			if (flag)
			{
				throw new ArgumentNullException("array");
			}
			bool flag2 = index < 0 || index > array.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException();
			}
			bool flag3 = array.Length - index < this.Count;
			if (flag3)
			{
				throw new ArgumentException("Array plus offset are too small to fit all items in.");
			}
			for (int i = 1; i < this._nodes.Length; i++)
			{
				bool used = this._nodes[i].Used;
				if (used)
				{
					array[index++] = new KeyValuePair<K, V>(this._nodes[i].Key, this._nodes[i].Val);
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E78 File Offset: 0x00001078
		private static bool IsPrimeFromList(uint value)
		{
			for (int i = 0; i < NonAllocDictionary<K, V>._primeTableUInt.Length; i++)
			{
				bool flag = NonAllocDictionary<K, V>._primeTableUInt[i] == value;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002EB8 File Offset: 0x000010B8
		private static uint GetNextPrime(uint value)
		{
			for (int i = 0; i < NonAllocDictionary<K, V>._primeTableUInt.Length; i++)
			{
				bool flag = NonAllocDictionary<K, V>._primeTableUInt[i] > value;
				if (flag)
				{
					return NonAllocDictionary<K, V>._primeTableUInt[i];
				}
			}
			throw new InvalidOperationException("NonAllocDictionary can't get larger than" + NonAllocDictionary<K, V>._primeTableUInt[NonAllocDictionary<K, V>._primeTableUInt.Length - 1].ToString());
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F20 File Offset: 0x00001120
		private static void Assert(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				throw new InvalidOperationException("Assert Failed");
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002F42 File Offset: 0x00001142
		// Note: this type is marked as 'beforefieldinit'.
		static NonAllocDictionary()
		{
		}

		// Token: 0x04000009 RID: 9
		private static uint[] _primeTableUInt = new uint[]
		{
			3U,
			7U,
			17U,
			29U,
			53U,
			97U,
			193U,
			389U,
			769U,
			1543U,
			3079U,
			6151U,
			12289U,
			24593U,
			49157U,
			98317U,
			196613U,
			393241U,
			786433U,
			1572869U,
			3145739U,
			6291469U,
			12582917U,
			25165843U,
			50331653U,
			100663319U,
			201326611U,
			402653189U,
			805306457U,
			1610612741U
		};

		// Token: 0x0400000A RID: 10
		private int _freeHead;

		// Token: 0x0400000B RID: 11
		private int _freeCount;

		// Token: 0x0400000C RID: 12
		private int _usedCount;

		// Token: 0x0400000D RID: 13
		private uint _capacity;

		// Token: 0x0400000E RID: 14
		private int[] _buckets;

		// Token: 0x0400000F RID: 15
		private NonAllocDictionary<K, V>.Node[] _nodes;

		// Token: 0x04000010 RID: 16
		private bool isReadOnly;

		// Token: 0x04000011 RID: 17
		private ICollection<K> keys;

		// Token: 0x04000012 RID: 18
		private ICollection<V> values;

		// Token: 0x0200004E RID: 78
		public struct KeyIterator : IEnumerator<K>, IEnumerator, IDisposable
		{
			// Token: 0x0600041C RID: 1052 RVA: 0x00020AC5 File Offset: 0x0001ECC5
			public KeyIterator(NonAllocDictionary<K, V> dictionary)
			{
				this._index = 0;
				this._dict = dictionary;
			}

			// Token: 0x0600041D RID: 1053 RVA: 0x00020AD8 File Offset: 0x0001ECD8
			public NonAllocDictionary<K, V>.KeyIterator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x00020AF0 File Offset: 0x0001ECF0
			public void Reset()
			{
				this._index = 0;
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x0600041F RID: 1055 RVA: 0x00020AFC File Offset: 0x0001ECFC
			object IEnumerator.Current
			{
				get
				{
					bool flag = this._index == 0;
					if (flag)
					{
						throw new InvalidOperationException();
					}
					return this._dict._nodes[this._index].Key;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000420 RID: 1056 RVA: 0x00020B44 File Offset: 0x0001ED44
			public K Current
			{
				get
				{
					bool flag = this._index == 0;
					K result;
					if (flag)
					{
						result = default(K);
					}
					else
					{
						result = this._dict._nodes[this._index].Key;
					}
					return result;
				}
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x00020B8C File Offset: 0x0001ED8C
			public bool MoveNext()
			{
				bool used;
				do
				{
					int num = this._index + 1;
					this._index = num;
					if (num >= this._dict._usedCount)
					{
						goto Block_2;
					}
					used = this._dict._nodes[this._index].Used;
				}
				while (!used);
				return true;
				Block_2:
				this._index = 0;
				return false;
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x00020BF0 File Offset: 0x0001EDF0
			public void Dispose()
			{
			}

			// Token: 0x04000218 RID: 536
			private int _index;

			// Token: 0x04000219 RID: 537
			private NonAllocDictionary<K, V> _dict;
		}

		// Token: 0x0200004F RID: 79
		public struct ValueIterator : IEnumerator<V>, IEnumerator, IDisposable
		{
			// Token: 0x06000423 RID: 1059 RVA: 0x00020BF3 File Offset: 0x0001EDF3
			public ValueIterator(NonAllocDictionary<K, V> dictionary)
			{
				this._index = 0;
				this._dict = dictionary;
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x00020C04 File Offset: 0x0001EE04
			public NonAllocDictionary<K, V>.ValueIterator GetEnumerator()
			{
				return this;
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x00020C1C File Offset: 0x0001EE1C
			public void Reset()
			{
				this._index = 0;
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000426 RID: 1062 RVA: 0x00020C28 File Offset: 0x0001EE28
			public V Current
			{
				get
				{
					bool flag = this._index == 0;
					V result;
					if (flag)
					{
						result = default(V);
					}
					else
					{
						result = this._dict._nodes[this._index].Val;
					}
					return result;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000427 RID: 1063 RVA: 0x00020C70 File Offset: 0x0001EE70
			object IEnumerator.Current
			{
				get
				{
					bool flag = this._index == 0;
					if (flag)
					{
						throw new InvalidOperationException();
					}
					return this._dict._nodes[this._index].Val;
				}
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x00020CB8 File Offset: 0x0001EEB8
			public bool MoveNext()
			{
				bool used;
				do
				{
					int num = this._index + 1;
					this._index = num;
					if (num >= this._dict._usedCount)
					{
						goto Block_2;
					}
					used = this._dict._nodes[this._index].Used;
				}
				while (!used);
				return true;
				Block_2:
				this._index = 0;
				return false;
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x00020D1C File Offset: 0x0001EF1C
			public void Dispose()
			{
			}

			// Token: 0x0400021A RID: 538
			private int _index;

			// Token: 0x0400021B RID: 539
			private NonAllocDictionary<K, V> _dict;
		}

		// Token: 0x02000050 RID: 80
		public struct PairIterator : IEnumerator<KeyValuePair<K, V>>, IEnumerator, IDisposable
		{
			// Token: 0x0600042A RID: 1066 RVA: 0x00020D1F File Offset: 0x0001EF1F
			public PairIterator(NonAllocDictionary<K, V> dictionary)
			{
				this._index = 0;
				this._dict = dictionary;
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x00020D30 File Offset: 0x0001EF30
			public void Reset()
			{
				this._index = 0;
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600042C RID: 1068 RVA: 0x00020D3C File Offset: 0x0001EF3C
			object IEnumerator.Current
			{
				get
				{
					bool flag = this._index == 0;
					if (flag)
					{
						throw new InvalidOperationException();
					}
					return this.Current;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600042D RID: 1069 RVA: 0x00020D70 File Offset: 0x0001EF70
			public KeyValuePair<K, V> Current
			{
				get
				{
					bool flag = this._index == 0;
					KeyValuePair<K, V> result;
					if (flag)
					{
						result = default(KeyValuePair<K, V>);
					}
					else
					{
						result = new KeyValuePair<K, V>(this._dict._nodes[this._index].Key, this._dict._nodes[this._index].Val);
					}
					return result;
				}
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x00020DD8 File Offset: 0x0001EFD8
			public bool MoveNext()
			{
				bool used;
				do
				{
					int num = this._index + 1;
					this._index = num;
					if (num >= this._dict._usedCount)
					{
						goto Block_2;
					}
					used = this._dict._nodes[this._index].Used;
				}
				while (!used);
				return true;
				Block_2:
				this._index = 0;
				return false;
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x00020E3C File Offset: 0x0001F03C
			public void Dispose()
			{
			}

			// Token: 0x0400021C RID: 540
			private int _index;

			// Token: 0x0400021D RID: 541
			private NonAllocDictionary<K, V> _dict;
		}

		// Token: 0x02000051 RID: 81
		private struct Node
		{
			// Token: 0x0400021E RID: 542
			public bool Used;

			// Token: 0x0400021F RID: 543
			public int Next;

			// Token: 0x04000220 RID: 544
			public uint Hash;

			// Token: 0x04000221 RID: 545
			public K Key;

			// Token: 0x04000222 RID: 546
			public V Val;
		}
	}
}
