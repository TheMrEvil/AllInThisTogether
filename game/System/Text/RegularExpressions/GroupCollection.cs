using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Returns the set of captured groups in a single match.</summary>
	// Token: 0x020001EE RID: 494
	[DebuggerTypeProxy(typeof(CollectionDebuggerProxy<Group>))]
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	public class GroupCollection : IList<Group>, ICollection<Group>, IEnumerable<Group>, IEnumerable, IReadOnlyList<Group>, IReadOnlyCollection<Group>, IList, ICollection
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x00035A3D File Offset: 0x00033C3D
		internal GroupCollection(Match match, Hashtable caps)
		{
			this._match = match;
			this._captureMap = caps;
		}

		/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns the number of groups in the collection.</summary>
		/// <returns>The number of groups in the collection.</returns>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00035A53 File Offset: 0x00033C53
		public int Count
		{
			get
			{
				return this._match._matchcount.Length;
			}
		}

		/// <summary>Enables access to a member of the collection by integer index.</summary>
		/// <param name="groupnum">The zero-based index of the collection member to be retrieved.</param>
		/// <returns>The member of the collection specified by <paramref name="groupnum" />.</returns>
		// Token: 0x1700023F RID: 575
		public Group this[int groupnum]
		{
			get
			{
				return this.GetGroup(groupnum);
			}
		}

		/// <summary>Enables access to a member of the collection by string index.</summary>
		/// <param name="groupname">The name of a capturing group.</param>
		/// <returns>The member of the collection specified by <paramref name="groupname" />.</returns>
		// Token: 0x17000240 RID: 576
		public Group this[string groupname]
		{
			get
			{
				if (this._match._regex != null)
				{
					return this.GetGroup(this._match._regex.GroupNumberFromName(groupname));
				}
				return Group.s_emptyGroup;
			}
		}

		/// <summary>Provides an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that contains all <see cref="T:System.Text.RegularExpressions.Group" /> objects in the <see cref="T:System.Text.RegularExpressions.GroupCollection" />.</returns>
		// Token: 0x06000D00 RID: 3328 RVA: 0x00035A97 File Offset: 0x00033C97
		public IEnumerator GetEnumerator()
		{
			return new GroupCollection.Enumerator(this);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00035A97 File Offset: 0x00033C97
		IEnumerator<Group> IEnumerable<Group>.GetEnumerator()
		{
			return new GroupCollection.Enumerator(this);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00035AA0 File Offset: 0x00033CA0
		private Group GetGroup(int groupnum)
		{
			if (this._captureMap != null)
			{
				int groupnum2;
				if (this._captureMap.TryGetValue(groupnum, out groupnum2))
				{
					return this.GetGroupImpl(groupnum2);
				}
			}
			else if (groupnum < this._match._matchcount.Length && groupnum >= 0)
			{
				return this.GetGroupImpl(groupnum);
			}
			return Group.s_emptyGroup;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00035AF4 File Offset: 0x00033CF4
		private Group GetGroupImpl(int groupnum)
		{
			if (groupnum == 0)
			{
				return this._match;
			}
			if (this._groups == null)
			{
				this._groups = new Group[this._match._matchcount.Length - 1];
				for (int i = 0; i < this._groups.Length; i++)
				{
					string name = this._match._regex.GroupNameFromNumber(i + 1);
					this._groups[i] = new Group(this._match.Text, this._match._matches[i + 1], this._match._matchcount[i + 1], name);
				}
			}
			return this._groups[groupnum - 1];
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Text.RegularExpressions.GroupCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00003062 File Offset: 0x00001262
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Text.RegularExpressions.GroupCollection" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Text.RegularExpressions.Match" /> object to synchronize.</returns>
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00035B95 File Offset: 0x00033D95
		public object SyncRoot
		{
			get
			{
				return this._match;
			}
		}

		/// <summary>Copies all the elements of the collection to the given array beginning at the given index.</summary>
		/// <param name="array">The array the collection is to be copied into.</param>
		/// <param name="arrayIndex">The position in the destination array where the copying is to begin.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="arrayIndex" /> is outside the bounds of <paramref name="array" />.  
		/// -or-  
		/// <paramref name="arrayIndex" /> plus <see cref="P:System.Text.RegularExpressions.GroupCollection.Count" /> is outside the bounds of <paramref name="array" />.</exception>
		// Token: 0x06000D06 RID: 3334 RVA: 0x00035BA0 File Offset: 0x00033DA0
		public void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], num);
				num++;
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00035BE0 File Offset: 0x00033DE0
		public void CopyTo(Group[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException("Destination array is not long enough to copy all the items in the collection. Check array index and length.");
			}
			int num = arrayIndex;
			for (int i = 0; i < this.Count; i++)
			{
				array[num] = this[i];
				num++;
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00035C4C File Offset: 0x00033E4C
		int IList<Group>.IndexOf(Group item)
		{
			EqualityComparer<Group> @default = EqualityComparer<Group>.Default;
			for (int i = 0; i < this.Count; i++)
			{
				if (@default.Equals(this[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0003577D File Offset: 0x0003397D
		void IList<Group>.Insert(int index, Group item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0003577D File Offset: 0x0003397D
		void IList<Group>.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000243 RID: 579
		Group IList<Group>.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003577D File Offset: 0x0003397D
		void ICollection<Group>.Add(Group item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003577D File Offset: 0x0003397D
		void ICollection<Group>.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00035C8C File Offset: 0x00033E8C
		bool ICollection<Group>.Contains(Group item)
		{
			return ((IList<Group>)this).IndexOf(item) >= 0;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003577D File Offset: 0x0003397D
		bool ICollection<Group>.Remove(Group item)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0003577D File Offset: 0x0003397D
		int IList.Add(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.Clear()
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00035C9B File Offset: 0x00033E9B
		bool IList.Contains(object value)
		{
			return value is Group && ((ICollection<Group>)this).Contains((Group)value);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00035CB3 File Offset: 0x00033EB3
		int IList.IndexOf(object value)
		{
			if (!(value is Group))
			{
				return -1;
			}
			return ((IList<Group>)this).IndexOf((Group)value);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.Insert(int index, object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0000390E File Offset: 0x00001B0E
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.Remove(object value)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003577D File Offset: 0x0003397D
		void IList.RemoveAt(int index)
		{
			throw new NotSupportedException("Collection is read-only.");
		}

		// Token: 0x17000245 RID: 581
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotSupportedException("Collection is read-only.");
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal GroupCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007E6 RID: 2022
		private readonly Match _match;

		// Token: 0x040007E7 RID: 2023
		private readonly Hashtable _captureMap;

		// Token: 0x040007E8 RID: 2024
		private Group[] _groups;

		// Token: 0x020001EF RID: 495
		private sealed class Enumerator : IEnumerator<Group>, IDisposable, IEnumerator
		{
			// Token: 0x06000D1C RID: 3356 RVA: 0x00035CCB File Offset: 0x00033ECB
			internal Enumerator(GroupCollection collection)
			{
				this._collection = collection;
				this._index = -1;
			}

			// Token: 0x06000D1D RID: 3357 RVA: 0x00035CE4 File Offset: 0x00033EE4
			public bool MoveNext()
			{
				int count = this._collection.Count;
				if (this._index >= count)
				{
					return false;
				}
				this._index++;
				return this._index < count;
			}

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00035D1F File Offset: 0x00033F1F
			public Group Current
			{
				get
				{
					if (this._index < 0 || this._index >= this._collection.Count)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this._collection[this._index];
				}
			}

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00035D59 File Offset: 0x00033F59
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000D20 RID: 3360 RVA: 0x00035D61 File Offset: 0x00033F61
			void IEnumerator.Reset()
			{
				this._index = -1;
			}

			// Token: 0x06000D21 RID: 3361 RVA: 0x00003917 File Offset: 0x00001B17
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040007E9 RID: 2025
			private readonly GroupCollection _collection;

			// Token: 0x040007EA RID: 2026
			private int _index;
		}
	}
}
