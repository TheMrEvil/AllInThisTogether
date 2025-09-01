using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeCatchClause" /> objects.</summary>
	// Token: 0x020002FD RID: 765
	[Serializable]
	public class CodeCatchClauseCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> class.</summary>
		// Token: 0x06001872 RID: 6258 RVA: 0x0004E358 File Offset: 0x0004C558
		public CodeCatchClauseCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001873 RID: 6259 RVA: 0x0005F581 File Offset: 0x0005D781
		public CodeCatchClauseCollection(CodeCatchClauseCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are <see langword="null" />.</exception>
		// Token: 0x06001874 RID: 6260 RVA: 0x0005F590 File Offset: 0x0005D790
		public CodeCatchClauseCollection(CodeCatchClause[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeCatchClause" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeCatchClause" /> object at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x170004CD RID: 1229
		public CodeCatchClause this[int index]
		{
			get
			{
				return (CodeCatchClause)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06001877 RID: 6263 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(CodeCatchClause value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeCatchClause" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeCatchClause" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001878 RID: 6264 RVA: 0x0005F5B4 File Offset: 0x0005D7B4
		public void AddRange(CodeCatchClause[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001879 RID: 6265 RVA: 0x0005F5E8 File Offset: 0x0005D7E8
		public void AddRange(CodeCatchClauseCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600187A RID: 6266 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(CodeCatchClause value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x0600187B RID: 6267 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(CodeCatchClause[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object in the collection, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to locate in the collection.</param>
		/// <returns>The index of the specified object, if found, in the collection; otherwise, -1.</returns>
		// Token: 0x0600187C RID: 6268 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(CodeCatchClause value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to insert.</param>
		// Token: 0x0600187D RID: 6269 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, CodeCatchClause value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x0600187E RID: 6270 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(CodeCatchClause value)
		{
			base.List.Remove(value);
		}
	}
}
