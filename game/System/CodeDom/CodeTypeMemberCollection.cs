using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeMember" /> objects.</summary>
	// Token: 0x02000337 RID: 823
	[Serializable]
	public class CodeTypeMemberCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> class.</summary>
		// Token: 0x06001A1F RID: 6687 RVA: 0x0004E358 File Offset: 0x0004C558
		public CodeTypeMemberCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> with which to initialize the collection.</param>
		// Token: 0x06001A20 RID: 6688 RVA: 0x000614A3 File Offset: 0x0005F6A3
		public CodeTypeMemberCollection(CodeTypeMemberCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeTypeMember" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeTypeMember" /> objects with which to initialize the collection.</param>
		// Token: 0x06001A21 RID: 6689 RVA: 0x000614B2 File Offset: 0x0005F6B2
		public CodeTypeMemberCollection(CodeTypeMember[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeMember" /> at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeMember" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x1700054D RID: 1357
		public CodeTypeMember this[int index]
		{
			get
			{
				return (CodeTypeMember)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeTypeMember" /> with the specified value to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06001A24 RID: 6692 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(CodeTypeMember value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeTypeMember" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeTypeMember" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001A25 RID: 6693 RVA: 0x000614D4 File Offset: 0x0005F6D4
		public void AddRange(CodeTypeMember[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the contents of another <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001A26 RID: 6694 RVA: 0x00061508 File Offset: 0x0005F708
		public void AddRange(CodeTypeMemberCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Gets a value indicating whether the collection contains the specified <see cref="T:System.CodeDom.CodeTypeMember" />.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A27 RID: 6695 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(CodeTypeMember value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance, beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x06001A28 RID: 6696 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(CodeTypeMember[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeTypeMember" />, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to locate in the collection.</param>
		/// <returns>The index in the collection of the specified object, if found; otherwise, -1.</returns>
		// Token: 0x06001A29 RID: 6697 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(CodeTypeMember value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeTypeMember" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to insert.</param>
		// Token: 0x06001A2A RID: 6698 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, CodeTypeMember value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes a specific <see cref="T:System.CodeDom.CodeTypeMember" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeMember" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06001A2B RID: 6699 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(CodeTypeMember value)
		{
			base.List.Remove(value);
		}
	}
}
