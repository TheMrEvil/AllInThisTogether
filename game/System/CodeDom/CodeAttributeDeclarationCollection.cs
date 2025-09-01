using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects.</summary>
	// Token: 0x020002F7 RID: 759
	[Serializable]
	public class CodeAttributeDeclarationCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> class.</summary>
		// Token: 0x0600184B RID: 6219 RVA: 0x0004E358 File Offset: 0x0004C558
		public CodeAttributeDeclarationCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600184C RID: 6220 RVA: 0x0005F33C File Offset: 0x0005D53C
		public CodeAttributeDeclarationCollection(CodeAttributeDeclarationCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are <see langword="null" />.</exception>
		// Token: 0x0600184D RID: 6221 RVA: 0x0005F34B File Offset: 0x0005D54B
		public CodeAttributeDeclarationCollection(CodeAttributeDeclaration[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object at the specified index.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x170004C4 RID: 1220
		public CodeAttributeDeclaration this[int index]
		{
			get
			{
				return (CodeAttributeDeclaration)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object with the specified value to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06001850 RID: 6224 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(CodeAttributeDeclaration value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001851 RID: 6225 RVA: 0x0005F370 File Offset: 0x0005D570
		public void AddRange(CodeAttributeDeclaration[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001852 RID: 6226 RVA: 0x0005F3A4 File Offset: 0x0005D5A4
		public void AddRange(CodeAttributeDeclarationCollection value)
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

		/// <summary>Gets or sets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to locate.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001853 RID: 6227 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(CodeAttributeDeclaration value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x06001854 RID: 6228 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(CodeAttributeDeclaration[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object in the collection, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to locate in the collection.</param>
		/// <returns>The index in the collection of the specified object, if found; otherwise, -1.</returns>
		// Token: 0x06001855 RID: 6229 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(CodeAttributeDeclaration value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to insert.</param>
		// Token: 0x06001856 RID: 6230 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, CodeAttributeDeclaration value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06001857 RID: 6231 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(CodeAttributeDeclaration value)
		{
			base.List.Remove(value);
		}
	}
}
