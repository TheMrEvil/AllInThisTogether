using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Collections.Generic
{
	/// <summary>Represents a set of values.To browse the .NET Framework source code for this type, see the Reference Source.</summary>
	/// <typeparam name="T">The type of elements in the hash set.</typeparam>
	// Token: 0x0200035C RID: 860
	[DebuggerTypeProxy(typeof(ICollectionDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class HashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ISet<T>, IReadOnlyCollection<T>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that is empty and uses the default equality comparer for the set type.</summary>
		// Token: 0x06001A25 RID: 6693 RVA: 0x000576B2 File Offset: 0x000558B2
		public HashSet() : this(EqualityComparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that is empty and uses the specified equality comparer for the set type.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing values in the set, or <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> implementation for the set type.</param>
		// Token: 0x06001A26 RID: 6694 RVA: 0x000576BF File Offset: 0x000558BF
		public HashSet(IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			this._comparer = comparer;
			this._lastIndex = 0;
			this._count = 0;
			this._freeList = -1;
			this._version = 0;
		}

		/// <summary>
		/// 			Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that is empty, but has reserved space for <paramref name="capacity" /> items and uses the default equality comparer for the set type.
		/// 		</summary>
		/// <param name="capacity">The initial size of the <see cref="T:System.Collections.Generic.HashSet`1" /></param>
		// Token: 0x06001A27 RID: 6695 RVA: 0x000576F4 File Offset: 0x000558F4
		public HashSet(int capacity) : this(capacity, EqualityComparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that uses the default equality comparer for the set type, contains elements copied from the specified collection, and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="collection" /> is <see langword="null" />.</exception>
		// Token: 0x06001A28 RID: 6696 RVA: 0x00057702 File Offset: 0x00055902
		public HashSet(IEnumerable<T> collection) : this(collection, EqualityComparer<T>.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that uses the specified equality comparer for the set type, contains elements copied from the specified collection, and has sufficient capacity to accommodate the number of elements copied.</summary>
		/// <param name="collection">The collection whose elements are copied to the new set.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing values in the set, or <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> implementation for the set type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="collection" /> is <see langword="null" />.</exception>
		// Token: 0x06001A29 RID: 6697 RVA: 0x00057710 File Offset: 0x00055910
		public HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer) : this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			HashSet<T> hashSet = collection as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				this.CopyFrom(hashSet);
				return;
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			int capacity = (collection2 == null) ? 0 : collection2.Count;
			this.Initialize(capacity);
			this.UnionWith(collection);
			if (this._count > 0 && this._slots.Length / this._count > 3)
			{
				this.TrimExcess();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class with serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		// Token: 0x06001A2A RID: 6698 RVA: 0x00057791 File Offset: 0x00055991
		protected HashSet(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000577A0 File Offset: 0x000559A0
		private void CopyFrom(HashSet<T> source)
		{
			int count = source._count;
			if (count == 0)
			{
				return;
			}
			int num = source._buckets.Length;
			if (HashHelpers.ExpandPrime(count + 1) >= num)
			{
				this._buckets = (int[])source._buckets.Clone();
				this._slots = (HashSet<T>.Slot[])source._slots.Clone();
				this._lastIndex = source._lastIndex;
				this._freeList = source._freeList;
			}
			else
			{
				int lastIndex = source._lastIndex;
				HashSet<T>.Slot[] slots = source._slots;
				this.Initialize(count);
				int num2 = 0;
				for (int i = 0; i < lastIndex; i++)
				{
					int hashCode = slots[i].hashCode;
					if (hashCode >= 0)
					{
						this.AddValue(num2, hashCode, slots[i].value);
						num2++;
					}
				}
				this._lastIndex = num2;
			}
			this._count = count;
		}

		/// <summary>
		///   Initializes a new instance of the <see cref="T:System.Collections.Generic.HashSet`1" /> class that uses the specified equality comparer for the set type, and has sufficient capacity to accommodate <paramref name="capacity" /> elements.
		/// 		</summary>
		/// <param name="capacity">The initial size of the <see cref="T:System.Collections.Generic.HashSet`1" /></param>
		/// <param name="comparer">
		/// 				The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing values in the set, or null (Nothing in Visual Basic) to use the default <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation for the set type.
		/// 			</param>
		// Token: 0x06001A2C RID: 6700 RVA: 0x0005787B File Offset: 0x00055A7B
		public HashSet(int capacity, IEqualityComparer<T> comparer) : this(comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0005789F File Offset: 0x00055A9F
		void ICollection<!0>.Add(T item)
		{
			this.AddIfNotPresent(item);
		}

		/// <summary>Removes all elements from a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		// Token: 0x06001A2E RID: 6702 RVA: 0x000578AC File Offset: 0x00055AAC
		public void Clear()
		{
			if (this._lastIndex > 0)
			{
				Array.Clear(this._slots, 0, this._lastIndex);
				Array.Clear(this._buckets, 0, this._buckets.Length);
				this._lastIndex = 0;
				this._count = 0;
				this._freeList = -1;
			}
			this._version++;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object contains the specified element.</summary>
		/// <param name="item">The element to locate in the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Collections.Generic.HashSet`1" /> object contains the specified element; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A2F RID: 6703 RVA: 0x0005790C File Offset: 0x00055B0C
		public bool Contains(T item)
		{
			if (this._buckets != null)
			{
				int num = 0;
				int num2 = this.InternalGetHashCode(item);
				HashSet<T>.Slot[] slots = this._slots;
				for (int i = this._buckets[num2 % this._buckets.Length] - 1; i >= 0; i = slots[i].next)
				{
					if (slots[i].hashCode == num2 && this._comparer.Equals(slots[i].value, item))
					{
						return true;
					}
					if (num >= slots.Length)
					{
						throw new InvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
					}
					num++;
				}
			}
			return false;
		}

		/// <summary>Copies the elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.HashSet`1" /> object. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="arrayIndex" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="arrayIndex" /> is greater than the length of the destination <paramref name="array" />.</exception>
		// Token: 0x06001A30 RID: 6704 RVA: 0x0005799A File Offset: 0x00055B9A
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex, this._count);
		}

		/// <summary>Removes the specified element from a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <param name="item">The element to remove.</param>
		/// <returns>
		///     <see langword="true" /> if the element is successfully found and removed; otherwise, <see langword="false" />.  This method returns <see langword="false" /> if <paramref name="item" /> is not found in the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</returns>
		// Token: 0x06001A31 RID: 6705 RVA: 0x000579AC File Offset: 0x00055BAC
		public bool Remove(T item)
		{
			if (this._buckets != null)
			{
				int num = this.InternalGetHashCode(item);
				int num2 = num % this._buckets.Length;
				int num3 = -1;
				int num4 = 0;
				HashSet<T>.Slot[] slots = this._slots;
				for (int i = this._buckets[num2] - 1; i >= 0; i = slots[i].next)
				{
					if (slots[i].hashCode == num && this._comparer.Equals(slots[i].value, item))
					{
						if (num3 < 0)
						{
							this._buckets[num2] = slots[i].next + 1;
						}
						else
						{
							slots[num3].next = slots[i].next;
						}
						slots[i].hashCode = -1;
						if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
						{
							slots[i].value = default(T);
						}
						slots[i].next = this._freeList;
						this._count--;
						this._version++;
						if (this._count == 0)
						{
							this._lastIndex = 0;
							this._freeList = -1;
						}
						else
						{
							this._freeList = i;
						}
						return true;
					}
					if (num4 >= slots.Length)
					{
						throw new InvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
					}
					num4++;
					num3 = i;
				}
			}
			return false;
		}

		/// <summary>Gets the number of elements that are contained in a set.</summary>
		/// <returns>The number of elements that are contained in the set.</returns>
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x00057B0E File Offset: 0x00055D0E
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x000023D1 File Offset: 0x000005D1
		bool ICollection<!0>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns an enumerator that iterates through a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.HashSet`1.Enumerator" /> object for the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</returns>
		// Token: 0x06001A34 RID: 6708 RVA: 0x00057B16 File Offset: 0x00055D16
		public HashSet<T>.Enumerator GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00057B1E File Offset: 0x00055D1E
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06001A36 RID: 6710 RVA: 0x00057B1E File Offset: 0x00055D1E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06001A37 RID: 6711 RVA: 0x00057B2C File Offset: 0x00055D2C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this._version);
			info.AddValue("Comparer", this._comparer, typeof(IComparer<T>));
			info.AddValue("Capacity", (this._buckets == null) ? 0 : this._buckets.Length);
			if (this._buckets != null)
			{
				T[] array = new T[this._count];
				this.CopyTo(array);
				info.AddValue("Elements", array, typeof(T[]));
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.HashSet`1" /> object is invalid.</exception>
		// Token: 0x06001A38 RID: 6712 RVA: 0x00057BC4 File Offset: 0x00055DC4
		public virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				return;
			}
			int @int = this._siInfo.GetInt32("Capacity");
			this._comparer = (IEqualityComparer<T>)this._siInfo.GetValue("Comparer", typeof(IEqualityComparer<T>));
			this._freeList = -1;
			if (@int != 0)
			{
				this._buckets = new int[@int];
				this._slots = new HashSet<T>.Slot[@int];
				T[] array = (T[])this._siInfo.GetValue("Elements", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException("The keys for this dictionary are missing.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.AddIfNotPresent(array[i]);
				}
			}
			else
			{
				this._buckets = null;
			}
			this._version = this._siInfo.GetInt32("Version");
			this._siInfo = null;
		}

		/// <summary>Adds the specified element to a set.</summary>
		/// <param name="item">The element to add to the set.</param>
		/// <returns>
		///     <see langword="true" /> if the element is added to the <see cref="T:System.Collections.Generic.HashSet`1" /> object; <see langword="false" /> if the element is already present.</returns>
		// Token: 0x06001A39 RID: 6713 RVA: 0x00057CA2 File Offset: 0x00055EA2
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		/// <summary>Searches the set for a given value and returns the equal value it finds, if any.</summary>
		/// <param name="equalValue">The value to search for.</param>
		/// <param name="actualValue">The value from the set that the search found, or the default value of T when the search yielded no match.</param>
		/// <returns>A value indicating whether the search was successful.</returns>
		// Token: 0x06001A3A RID: 6714 RVA: 0x00057CAC File Offset: 0x00055EAC
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			if (this._buckets != null)
			{
				int num = this.InternalIndexOf(equalValue);
				if (num >= 0)
				{
					actualValue = this._slots[num].value;
					return true;
				}
			}
			actualValue = default(T);
			return false;
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.HashSet`1" /> object to contain all elements that are present in itself, the specified collection, or both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A3B RID: 6715 RVA: 0x00057CF0 File Offset: 0x00055EF0
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T value in other)
			{
				this.AddIfNotPresent(value);
			}
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.HashSet`1" /> object to contain only elements that are present in that object and in the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A3C RID: 6716 RVA: 0x00057D48 File Offset: 0x00055F48
		public void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return;
			}
			if (other == this)
			{
				return;
			}
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					this.Clear();
					return;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					this.IntersectWithHashSetWithSameEC(hashSet);
					return;
				}
			}
			this.IntersectWithEnumerable(other);
		}

		/// <summary>Removes all elements in the specified collection from the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <param name="other">The collection of items to remove from the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A3D RID: 6717 RVA: 0x00057DAC File Offset: 0x00055FAC
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			foreach (T item in other)
			{
				this.Remove(item);
			}
		}

		/// <summary>Modifies the current <see cref="T:System.Collections.Generic.HashSet`1" /> object to contain only elements that are present either in that object or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A3E RID: 6718 RVA: 0x00057E18 File Offset: 0x00056018
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				this.SymmetricExceptWithUniqueHashSet(hashSet);
				return;
			}
			this.SymmetricExceptWithEnumerable(other);
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object is a subset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is a subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A3F RID: 6719 RVA: 0x00057E70 File Offset: 0x00056070
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return true;
			}
			if (other == this)
			{
				return true;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				return this._count <= hashSet.Count && this.IsSubsetOfHashSetWithSameEC(hashSet);
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this._count && elementCount.unfoundCount >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object is a proper subset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is a proper subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A40 RID: 6720 RVA: 0x00057EEC File Offset: 0x000560EC
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other == this)
			{
				return false;
			}
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return false;
				}
				if (this._count == 0)
				{
					return collection.Count > 0;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					return this._count < hashSet.Count && this.IsSubsetOfHashSetWithSameEC(hashSet);
				}
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this._count && elementCount.unfoundCount > 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object is a superset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is a superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A41 RID: 6721 RVA: 0x00057F80 File Offset: 0x00056180
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other == this)
			{
				return true;
			}
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return true;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet) && hashSet.Count > this._count)
				{
					return false;
				}
			}
			return this.ContainsAllElements(other);
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object is a proper superset of the specified collection.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object. </param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is a proper superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A42 RID: 6722 RVA: 0x00057FE0 File Offset: 0x000561E0
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return false;
			}
			if (other == this)
			{
				return false;
			}
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return true;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					return hashSet.Count < this._count && this.ContainsAllElements(hashSet);
				}
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.uniqueCount < this._count && elementCount.unfoundCount == 0;
		}

		/// <summary>Determines whether the current <see cref="T:System.Collections.Generic.HashSet`1" /> object and a specified collection share common elements.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Collections.Generic.HashSet`1" /> object and <paramref name="other" /> share at least one common element; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A43 RID: 6723 RVA: 0x0005806C File Offset: 0x0005626C
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._count == 0)
			{
				return false;
			}
			if (other == this)
			{
				return true;
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

		/// <summary>Determines whether a <see cref="T:System.Collections.Generic.HashSet`1" /> object and the specified collection contain the same elements.</summary>
		/// <param name="other">The collection to compare to the current <see cref="T:System.Collections.Generic.HashSet`1" /> object.</param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Collections.Generic.HashSet`1" /> object is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06001A44 RID: 6724 RVA: 0x000580DC File Offset: 0x000562DC
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other == this)
			{
				return true;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				return this._count == hashSet.Count && this.ContainsAllElements(hashSet);
			}
			ICollection<T> collection = other as ICollection<!0>;
			if (collection != null && this._count == 0 && collection.Count > 0)
			{
				return false;
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.uniqueCount == this._count && elementCount.unfoundCount == 0;
		}

		/// <summary>Copies the elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to an array.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.HashSet`1" /> object. The array must have zero-based indexing.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="array" /> is <see langword="null" />.</exception>
		// Token: 0x06001A45 RID: 6725 RVA: 0x00058167 File Offset: 0x00056367
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this._count);
		}

		/// <summary>Copies the specified number of elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.HashSet`1" /> object. The array must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <param name="count">The number of elements to copy to <paramref name="array" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="arrayIndex" /> is less than 0.-or-
		///         <paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="arrayIndex" /> is greater than the length of the destination <paramref name="array" />.-or-
		///         <paramref name="count" /> is greater than the available space from the <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06001A46 RID: 6726 RVA: 0x00058178 File Offset: 0x00056378
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Non negative number is required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Non negative number is required.");
			}
			if (arrayIndex > array.Length || count > array.Length - arrayIndex)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			int num = 0;
			int num2 = 0;
			while (num2 < this._lastIndex && num < count)
			{
				if (this._slots[num2].hashCode >= 0)
				{
					array[arrayIndex + num] = this._slots[num2].value;
					num++;
				}
				num2++;
			}
		}

		/// <summary>Removes all elements that match the conditions defined by the specified predicate from a <see cref="T:System.Collections.Generic.HashSet`1" /> collection.</summary>
		/// <param name="match">The <see cref="T:System.Predicate`1" /> delegate that defines the conditions of the elements to remove.</param>
		/// <returns>The number of elements that were removed from the <see cref="T:System.Collections.Generic.HashSet`1" /> collection.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="match" /> is <see langword="null" />.</exception>
		// Token: 0x06001A47 RID: 6727 RVA: 0x0005822C File Offset: 0x0005642C
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			for (int i = 0; i < this._lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0)
				{
					T value = this._slots[i].value;
					if (match(value) && this.Remove(value))
					{
						num++;
					}
				}
			}
			return num;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> object that is used to determine equality for the values in the set.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> object that is used to determine equality for the values in the set.</returns>
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00058297 File Offset: 0x00056497
		public IEqualityComparer<T> Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000582A0 File Offset: 0x000564A0
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			int num = (this._slots == null) ? 0 : this._slots.Length;
			if (num >= capacity)
			{
				return num;
			}
			if (this._buckets == null)
			{
				return this.Initialize(capacity);
			}
			int prime = HashHelpers.GetPrime(capacity);
			this.SetCapacity(prime);
			return prime;
		}

		/// <summary>Sets the capacity of a <see cref="T:System.Collections.Generic.HashSet`1" /> object to the actual number of elements it contains, rounded up to a nearby, implementation-specific value.</summary>
		// Token: 0x06001A4A RID: 6730 RVA: 0x000582F8 File Offset: 0x000564F8
		public void TrimExcess()
		{
			if (this._count == 0)
			{
				this._buckets = null;
				this._slots = null;
				this._version++;
				return;
			}
			int prime = HashHelpers.GetPrime(this._count);
			HashSet<T>.Slot[] array = new HashSet<T>.Slot[prime];
			int[] array2 = new int[prime];
			int num = 0;
			for (int i = 0; i < this._lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0)
				{
					array[num] = this._slots[i];
					int num2 = array[num].hashCode % prime;
					array[num].next = array2[num2] - 1;
					array2[num2] = num + 1;
					num++;
				}
			}
			this._lastIndex = num;
			this._slots = array;
			this._buckets = array2;
			this._freeList = -1;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEqualityComparer" /> object that can be used for equality testing of a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEqualityComparer" /> object that can be used for deep equality testing of the <see cref="T:System.Collections.Generic.HashSet`1" /> object.</returns>
		// Token: 0x06001A4B RID: 6731 RVA: 0x000583CD File Offset: 0x000565CD
		public static IEqualityComparer<HashSet<T>> CreateSetComparer()
		{
			return new HashSetEqualityComparer<T>();
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000583D4 File Offset: 0x000565D4
		private int Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this._buckets = new int[prime];
			this._slots = new HashSet<T>.Slot[prime];
			return prime;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00058404 File Offset: 0x00056604
		private void IncreaseCapacity()
		{
			int num = HashHelpers.ExpandPrime(this._count);
			if (num <= this._count)
			{
				throw new ArgumentException("HashSet capacity is too big.");
			}
			this.SetCapacity(num);
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00058438 File Offset: 0x00056638
		private void SetCapacity(int newSize)
		{
			HashSet<T>.Slot[] array = new HashSet<T>.Slot[newSize];
			if (this._slots != null)
			{
				Array.Copy(this._slots, 0, array, 0, this._lastIndex);
			}
			int[] array2 = new int[newSize];
			for (int i = 0; i < this._lastIndex; i++)
			{
				int num = array[i].hashCode % newSize;
				array[i].next = array2[num] - 1;
				array2[num] = i + 1;
			}
			this._slots = array;
			this._buckets = array2;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x000584B4 File Offset: 0x000566B4
		private bool AddIfNotPresent(T value)
		{
			if (this._buckets == null)
			{
				this.Initialize(0);
			}
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			int num3 = 0;
			HashSet<T>.Slot[] slots = this._slots;
			for (int i = this._buckets[num2] - 1; i >= 0; i = slots[i].next)
			{
				if (slots[i].hashCode == num && this._comparer.Equals(slots[i].value, value))
				{
					return false;
				}
				if (num3 >= slots.Length)
				{
					throw new InvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
				}
				num3++;
			}
			int num4;
			if (this._freeList >= 0)
			{
				num4 = this._freeList;
				this._freeList = slots[num4].next;
			}
			else
			{
				if (this._lastIndex == slots.Length)
				{
					this.IncreaseCapacity();
					slots = this._slots;
					num2 = num % this._buckets.Length;
				}
				num4 = this._lastIndex;
				this._lastIndex++;
			}
			slots[num4].hashCode = num;
			slots[num4].value = value;
			slots[num4].next = this._buckets[num2] - 1;
			this._buckets[num2] = num4 + 1;
			this._count++;
			this._version++;
			return true;
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0005860C File Offset: 0x0005680C
		private void AddValue(int index, int hashCode, T value)
		{
			int num = hashCode % this._buckets.Length;
			this._slots[index].hashCode = hashCode;
			this._slots[index].value = value;
			this._slots[index].next = this._buckets[num] - 1;
			this._buckets[num] = index + 1;
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00058670 File Offset: 0x00056870
		private bool ContainsAllElements(IEnumerable<T> other)
		{
			foreach (T item in other)
			{
				if (!this.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000586C4 File Offset: 0x000568C4
		private bool IsSubsetOfHashSetWithSameEC(HashSet<T> other)
		{
			foreach (T item in this)
			{
				if (!other.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0005871C File Offset: 0x0005691C
		private void IntersectWithHashSetWithSameEC(HashSet<T> other)
		{
			for (int i = 0; i < this._lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0)
				{
					T value = this._slots[i].value;
					if (!other.Contains(value))
					{
						this.Remove(value);
					}
				}
			}
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00058774 File Offset: 0x00056974
		private unsafe void IntersectWithEnumerable(IEnumerable<T> other)
		{
			int lastIndex = this._lastIndex;
			int num = BitHelper.ToIntArrayLength(lastIndex);
			BitHelper bitHelper;
			if (num <= 100)
			{
				bitHelper = new BitHelper(stackalloc int[checked(unchecked((UIntPtr)num) * 4)], num);
			}
			else
			{
				bitHelper = new BitHelper(new int[num], num);
			}
			foreach (T item in other)
			{
				int num2 = this.InternalIndexOf(item);
				if (num2 >= 0)
				{
					bitHelper.MarkBit(num2);
				}
			}
			for (int i = 0; i < lastIndex; i++)
			{
				if (this._slots[i].hashCode >= 0 && !bitHelper.IsMarked(i))
				{
					this.Remove(this._slots[i].value);
				}
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00058844 File Offset: 0x00056A44
		private int InternalIndexOf(T item)
		{
			int num = 0;
			int num2 = this.InternalGetHashCode(item);
			HashSet<T>.Slot[] slots = this._slots;
			for (int i = this._buckets[num2 % this._buckets.Length] - 1; i >= 0; i = slots[i].next)
			{
				if (slots[i].hashCode == num2 && this._comparer.Equals(slots[i].value, item))
				{
					return i;
				}
				if (num >= slots.Length)
				{
					throw new InvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000588CC File Offset: 0x00056ACC
		private void SymmetricExceptWithUniqueHashSet(HashSet<T> other)
		{
			foreach (T t in other)
			{
				if (!this.Remove(t))
				{
					this.AddIfNotPresent(t);
				}
			}
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00058924 File Offset: 0x00056B24
		private unsafe void SymmetricExceptWithEnumerable(IEnumerable<T> other)
		{
			int lastIndex = this._lastIndex;
			int num = BitHelper.ToIntArrayLength(lastIndex);
			BitHelper bitHelper;
			checked
			{
				BitHelper bitHelper2;
				if (num <= 50)
				{
					bitHelper = new BitHelper(stackalloc int[unchecked((UIntPtr)num) * 4], num);
					bitHelper2 = new BitHelper(stackalloc int[unchecked((UIntPtr)num) * 4], num);
				}
				else
				{
					bitHelper = new BitHelper(new int[num], num);
					bitHelper2 = new BitHelper(new int[num], num);
				}
				foreach (T value in other)
				{
					int num2 = 0;
					if (this.AddOrGetLocation(value, out num2))
					{
						bitHelper2.MarkBit(num2);
					}
					else if (num2 < lastIndex && !bitHelper2.IsMarked(num2))
					{
						bitHelper.MarkBit(num2);
					}
				}
			}
			for (int i = 0; i < lastIndex; i++)
			{
				if (bitHelper.IsMarked(i))
				{
					this.Remove(this._slots[i].value);
				}
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00058A18 File Offset: 0x00056C18
		private bool AddOrGetLocation(T value, out int location)
		{
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			int num3 = 0;
			HashSet<T>.Slot[] slots = this._slots;
			for (int i = this._buckets[num2] - 1; i >= 0; i = slots[i].next)
			{
				if (slots[i].hashCode == num && this._comparer.Equals(slots[i].value, value))
				{
					location = i;
					return false;
				}
				if (num3 >= slots.Length)
				{
					throw new InvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
				}
				num3++;
			}
			int num4;
			if (this._freeList >= 0)
			{
				num4 = this._freeList;
				this._freeList = slots[num4].next;
			}
			else
			{
				if (this._lastIndex == slots.Length)
				{
					this.IncreaseCapacity();
					slots = this._slots;
					num2 = num % this._buckets.Length;
				}
				num4 = this._lastIndex;
				this._lastIndex++;
			}
			slots[num4].hashCode = num;
			slots[num4].value = value;
			slots[num4].next = this._buckets[num2] - 1;
			this._buckets[num2] = num4 + 1;
			this._count++;
			this._version++;
			location = num4;
			return true;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00058B68 File Offset: 0x00056D68
		private unsafe HashSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			HashSet<T>.ElementCount result;
			if (this._count == 0)
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
				result.uniqueCount = 0;
				result.unfoundCount = num;
				return result;
			}
			int num2 = BitHelper.ToIntArrayLength(this._lastIndex);
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
			result.uniqueCount = num4;
			result.unfoundCount = num3;
			return result;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00058C80 File Offset: 0x00056E80
		internal static bool HashSetEquals(HashSet<T> set1, HashSet<T> set2, IEqualityComparer<T> comparer)
		{
			if (set1 == null)
			{
				return set2 == null;
			}
			if (set2 == null)
			{
				return false;
			}
			if (!HashSet<T>.AreEqualityComparersEqual(set1, set2))
			{
				foreach (T x in set2)
				{
					bool flag = false;
					foreach (T y in set1)
					{
						if (comparer.Equals(x, y))
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
			if (set1.Count != set2.Count)
			{
				return false;
			}
			foreach (T item in set2)
			{
				if (!set1.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00058D8C File Offset: 0x00056F8C
		private static bool AreEqualityComparersEqual(HashSet<T> set1, HashSet<T> set2)
		{
			return set1.Comparer.Equals(set2.Comparer);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00058D9F File Offset: 0x00056F9F
		private int InternalGetHashCode(T item)
		{
			if (item == null)
			{
				return 0;
			}
			return this._comparer.GetHashCode(item) & int.MaxValue;
		}

		// Token: 0x04000C8B RID: 3211
		private const int Lower31BitMask = 2147483647;

		// Token: 0x04000C8C RID: 3212
		private const int StackAllocThreshold = 100;

		// Token: 0x04000C8D RID: 3213
		private const int ShrinkThreshold = 3;

		// Token: 0x04000C8E RID: 3214
		private const string CapacityName = "Capacity";

		// Token: 0x04000C8F RID: 3215
		private const string ElementsName = "Elements";

		// Token: 0x04000C90 RID: 3216
		private const string ComparerName = "Comparer";

		// Token: 0x04000C91 RID: 3217
		private const string VersionName = "Version";

		// Token: 0x04000C92 RID: 3218
		private int[] _buckets;

		// Token: 0x04000C93 RID: 3219
		private HashSet<T>.Slot[] _slots;

		// Token: 0x04000C94 RID: 3220
		private int _count;

		// Token: 0x04000C95 RID: 3221
		private int _lastIndex;

		// Token: 0x04000C96 RID: 3222
		private int _freeList;

		// Token: 0x04000C97 RID: 3223
		private IEqualityComparer<T> _comparer;

		// Token: 0x04000C98 RID: 3224
		private int _version;

		// Token: 0x04000C99 RID: 3225
		private SerializationInfo _siInfo;

		// Token: 0x0200035D RID: 861
		internal struct ElementCount
		{
			// Token: 0x04000C9A RID: 3226
			internal int uniqueCount;

			// Token: 0x04000C9B RID: 3227
			internal int unfoundCount;
		}

		// Token: 0x0200035E RID: 862
		internal struct Slot
		{
			// Token: 0x04000C9C RID: 3228
			internal int hashCode;

			// Token: 0x04000C9D RID: 3229
			internal int next;

			// Token: 0x04000C9E RID: 3230
			internal T value;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.HashSet`1" /> object.</summary>
		// Token: 0x0200035F RID: 863
		[Serializable]
		public struct Enumerator : IEnumerator<!0>, IDisposable, IEnumerator
		{
			// Token: 0x06001A5D RID: 6749 RVA: 0x00058DBD File Offset: 0x00056FBD
			internal Enumerator(HashSet<T> set)
			{
				this._set = set;
				this._index = 0;
				this._version = set._version;
				this._current = default(T);
			}

			/// <summary>Releases all resources used by a <see cref="T:System.Collections.Generic.HashSet`1.Enumerator" /> object.</summary>
			// Token: 0x06001A5E RID: 6750 RVA: 0x00003A59 File Offset: 0x00001C59
			public void Dispose()
			{
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.HashSet`1" /> collection.</summary>
			/// <returns>
			///     <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06001A5F RID: 6751 RVA: 0x00058DE8 File Offset: 0x00056FE8
			public bool MoveNext()
			{
				if (this._version != this._set._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				while (this._index < this._set._lastIndex)
				{
					if (this._set._slots[this._index].hashCode >= 0)
					{
						this._current = this._set._slots[this._index].value;
						this._index++;
						return true;
					}
					this._index++;
				}
				this._index = this._set._lastIndex + 1;
				this._current = default(T);
				return false;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.HashSet`1" /> collection at the current position of the enumerator.</returns>
			// Token: 0x1700047F RID: 1151
			// (get) Token: 0x06001A60 RID: 6752 RVA: 0x00058EA3 File Offset: 0x000570A3
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator, as an <see cref="T:System.Object" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000480 RID: 1152
			// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00058EAB File Offset: 0x000570AB
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._set._lastIndex + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.Current;
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
			// Token: 0x06001A62 RID: 6754 RVA: 0x00058EE0 File Offset: 0x000570E0
			void IEnumerator.Reset()
			{
				if (this._version != this._set._version)
				{
					throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
				}
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x04000C9F RID: 3231
			private HashSet<T> _set;

			// Token: 0x04000CA0 RID: 3232
			private int _index;

			// Token: 0x04000CA1 RID: 3233
			private int _version;

			// Token: 0x04000CA2 RID: 3234
			private T _current;
		}
	}
}
