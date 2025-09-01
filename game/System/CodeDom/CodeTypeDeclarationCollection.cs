using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects.</summary>
	// Token: 0x02000334 RID: 820
	[Serializable]
	public class CodeTypeDeclarationCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> class.</summary>
		// Token: 0x06001A01 RID: 6657 RVA: 0x0004E358 File Offset: 0x0004C558
		public CodeTypeDeclarationCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> class that contains the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object with which to initialize the collection.</param>
		// Token: 0x06001A02 RID: 6658 RVA: 0x00061279 File Offset: 0x0005F479
		public CodeTypeDeclarationCollection(CodeTypeDeclarationCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> class that contains the specified array of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects with which to initialize the collection.</param>
		// Token: 0x06001A03 RID: 6659 RVA: 0x00061288 File Offset: 0x0005F488
		public CodeTypeDeclarationCollection(CodeTypeDeclaration[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000543 RID: 1347
		public CodeTypeDeclaration this[int index]
		{
			get
			{
				return (CodeTypeDeclaration)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x06001A06 RID: 6662 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(CodeTypeDeclaration value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001A07 RID: 6663 RVA: 0x000612AC File Offset: 0x0005F4AC
		public void AddRange(CodeTypeDeclaration[] value)
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

		/// <summary>Adds the contents of another <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001A08 RID: 6664 RVA: 0x000612E0 File Offset: 0x0005F4E0
		public void AddRange(CodeTypeDeclarationCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001A09 RID: 6665 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(CodeTypeDeclaration value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the elements in the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object to a one-dimensional <see cref="T:System.Array" /> instance, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x06001A0A RID: 6666 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(CodeTypeDeclaration[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object in the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" />, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> to locate in the collection.</param>
		/// <returns>The index of the specified object, if it is found, in the collection; otherwise, -1.</returns>
		// Token: 0x06001A0B RID: 6667 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(CodeTypeDeclaration value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to insert.</param>
		// Token: 0x06001A0C RID: 6668 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, CodeTypeDeclaration value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06001A0D RID: 6669 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(CodeTypeDeclaration value)
		{
			base.List.Remove(value);
		}
	}
}
