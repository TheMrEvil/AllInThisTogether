using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeAttributeArgument" /> objects.</summary>
	// Token: 0x020002F5 RID: 757
	[Serializable]
	public class CodeAttributeArgumentCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> class.</summary>
		// Token: 0x06001835 RID: 6197 RVA: 0x0004E358 File Offset: 0x0004C558
		public CodeAttributeArgumentCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001836 RID: 6198 RVA: 0x0005F1CB File Offset: 0x0005D3CB
		public CodeAttributeArgumentCollection(CodeAttributeArgumentCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeAttributeArgument" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeAttributeArgument" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are <see langword="null" />.</exception>
		// Token: 0x06001837 RID: 6199 RVA: 0x0005F1DA File Offset: 0x0005D3DA
		public CodeAttributeArgumentCollection(CodeAttributeArgument[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeAttributeArgument" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeArgument" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x170004C0 RID: 1216
		public CodeAttributeArgument this[int index]
		{
			get
			{
				return (CodeAttributeArgument)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeAttributeArgument" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeArgument" /> object to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x0600183A RID: 6202 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(CodeAttributeArgument value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeAttributeArgument" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600183B RID: 6203 RVA: 0x0005F1FC File Offset: 0x0005D3FC
		public void AddRange(CodeAttributeArgument[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600183C RID: 6204 RVA: 0x0005F230 File Offset: 0x0005D430
		public void AddRange(CodeAttributeArgumentCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeAttributeArgument" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeArgument" /> object to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600183D RID: 6205 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(CodeAttributeArgument value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x0600183E RID: 6206 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(CodeAttributeArgument[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeAttributeArgument" /> object in the collection, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeArgument" /> object to locate in the collection.</param>
		/// <returns>The index of the specified object, if found, in the collection; otherwise, -1.</returns>
		// Token: 0x0600183F RID: 6207 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(CodeAttributeArgument value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeAttributeArgument" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeArgument" /> object to insert.</param>
		// Token: 0x06001840 RID: 6208 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, CodeAttributeArgument value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeAttributeArgument" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeAttributeArgument" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06001841 RID: 6209 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(CodeAttributeArgument value)
		{
			base.List.Remove(value);
		}
	}
}
