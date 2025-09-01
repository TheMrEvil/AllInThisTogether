using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeExpression" /> objects.</summary>
	// Token: 0x0200030E RID: 782
	[Serializable]
	public class CodeExpressionCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionCollection" /> class.</summary>
		// Token: 0x060018DF RID: 6367 RVA: 0x0004E358 File Offset: 0x0004C558
		public CodeExpressionCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpressionCollection" /> with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060018E0 RID: 6368 RVA: 0x0005FBB5 File Offset: 0x0005DDB5
		public CodeExpressionCollection(CodeExpressionCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeExpression" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are <see langword="null" />.</exception>
		// Token: 0x060018E1 RID: 6369 RVA: 0x0005FBC4 File Offset: 0x0005DDC4
		public CodeExpressionCollection(CodeExpression[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeExpression" /> object at the specified index in the collection.</summary>
		/// <param name="index">The zero-based index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> object at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x170004EA RID: 1258
		public CodeExpression this[int index]
		{
			get
			{
				return (CodeExpression)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeExpression" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x060018E4 RID: 6372 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(CodeExpression value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeExpression" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060018E5 RID: 6373 RVA: 0x0005FBE8 File Offset: 0x0005DDE8
		public void AddRange(CodeExpression[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeExpressionCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060018E6 RID: 6374 RVA: 0x0005FC1C File Offset: 0x0005DE1C
		public void AddRange(CodeExpressionCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeExpression" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060018E7 RID: 6375 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(CodeExpression value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeExpressionCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x060018E8 RID: 6376 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(CodeExpression[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeExpression" /> object in the collection, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to locate in the collection.</param>
		/// <returns>The index of the specified object, if found, in the collection; otherwise, -1.</returns>
		// Token: 0x060018E9 RID: 6377 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(CodeExpression value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeExpression" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to insert.</param>
		// Token: 0x060018EA RID: 6378 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, CodeExpression value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeExpression" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x060018EB RID: 6379 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(CodeExpression value)
		{
			base.List.Remove(value);
		}
	}
}
