using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeNamespace" /> objects.</summary>
	// Token: 0x0200031D RID: 797
	[Serializable]
	public class CodeNamespaceCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> class.</summary>
		// Token: 0x06001957 RID: 6487 RVA: 0x0004E358 File Offset: 0x0004C558
		public CodeNamespaceCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> class that contains the elements of the specified source collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespaceCollection" /> with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001958 RID: 6488 RVA: 0x00060659 File Offset: 0x0005E859
		public CodeNamespaceCollection(CodeNamespaceCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> class that contains the specified array of <see cref="T:System.CodeDom.CodeNamespace" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeNamespace" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are <see langword="null" />.</exception>
		// Token: 0x06001959 RID: 6489 RVA: 0x00060668 File Offset: 0x0005E868
		public CodeNamespaceCollection(CodeNamespace[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespace" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000514 RID: 1300
		public CodeNamespace this[int index]
		{
			get
			{
				return (CodeNamespace)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeNamespace" /> object to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x0600195C RID: 6492 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(CodeNamespace value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeNamespace" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeNamespace" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600195D RID: 6493 RVA: 0x0006068C File Offset: 0x0005E88C
		public void AddRange(CodeNamespace[] value)
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

		/// <summary>Adds the contents of the specified <see cref="T:System.CodeDom.CodeNamespaceCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeNamespaceCollection" /> that contains the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600195E RID: 6494 RVA: 0x000606C0 File Offset: 0x0005E8C0
		public void AddRange(CodeNamespaceCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeNamespace" /> object.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.CodeDom.CodeNamespace" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600195F RID: 6495 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(CodeNamespace value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x06001960 RID: 6496 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(CodeNamespace[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeNamespace" /> object in the <see cref="T:System.CodeDom.CodeNamespaceCollection" />, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to locate.</param>
		/// <returns>The index of the specified <see cref="T:System.CodeDom.CodeNamespace" />, if it is found, in the collection; otherwise, -1.</returns>
		// Token: 0x06001961 RID: 6497 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(CodeNamespace value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeNamespace" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the new item should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to insert.</param>
		// Token: 0x06001962 RID: 6498 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, CodeNamespace value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeNamespace" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x06001963 RID: 6499 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(CodeNamespace value)
		{
			base.List.Remove(value);
		}
	}
}
