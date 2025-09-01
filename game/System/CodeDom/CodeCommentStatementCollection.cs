using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeCommentStatement" /> objects.</summary>
	// Token: 0x02000301 RID: 769
	[Serializable]
	public class CodeCommentStatementCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> class.</summary>
		// Token: 0x06001894 RID: 6292 RVA: 0x0004E358 File Offset: 0x0004C558
		public CodeCommentStatementCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001895 RID: 6293 RVA: 0x0005F726 File Offset: 0x0005D926
		public CodeCommentStatementCollection(CodeCommentStatementCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeCommentStatement" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeCommentStatement" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are <see langword="null" />.</exception>
		// Token: 0x06001896 RID: 6294 RVA: 0x0005F735 File Offset: 0x0005D935
		public CodeCommentStatementCollection(CodeCommentStatement[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeCommentStatement" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatement" /> object at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x170004D4 RID: 1236
		public CodeCommentStatement this[int index]
		{
			get
			{
				return (CodeCommentStatement)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06001899 RID: 6297 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(CodeCommentStatement value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeCommentStatement" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600189A RID: 6298 RVA: 0x0005F758 File Offset: 0x0005D958
		public void AddRange(CodeCommentStatement[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600189B RID: 6299 RVA: 0x0005F78C File Offset: 0x0005D98C
		public void AddRange(CodeCommentStatementCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600189C RID: 6300 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(CodeCommentStatement value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to the specified one-dimensional <see cref="T:System.Array" /> beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x0600189D RID: 6301 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(CodeCommentStatement[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> object in the collection, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> object to locate.</param>
		/// <returns>The index of the specified object, if found, in the collection; otherwise, -1.</returns>
		// Token: 0x0600189E RID: 6302 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(CodeCommentStatement value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.CodeDom.CodeCommentStatement" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the item should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> object to insert.</param>
		// Token: 0x0600189F RID: 6303 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, CodeCommentStatement value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeCommentStatement" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCommentStatement" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x060018A0 RID: 6304 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(CodeCommentStatement value)
		{
			base.List.Remove(value);
		}
	}
}
