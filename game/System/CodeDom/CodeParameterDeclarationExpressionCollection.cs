using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects.</summary>
	// Token: 0x02000322 RID: 802
	[Serializable]
	public class CodeParameterDeclarationExpressionCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> class.</summary>
		// Token: 0x06001996 RID: 6550 RVA: 0x0004E358 File Offset: 0x0004C558
		public CodeParameterDeclarationExpressionCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06001997 RID: 6551 RVA: 0x00060AF5 File Offset: 0x0005ECF5
		public CodeParameterDeclarationExpressionCollection(CodeParameterDeclarationExpressionCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> objects with which to initialize the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">one or more objects in the array are <see langword="null" />.</exception>
		// Token: 0x06001998 RID: 6552 RVA: 0x00060B04 File Offset: 0x0005ED04
		public CodeParameterDeclarationExpressionCollection(CodeParameterDeclarationExpression[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> at the specified index in the collection.</summary>
		/// <param name="index">The index of the collection to access.</param>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> at each valid index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection.</exception>
		// Token: 0x17000525 RID: 1317
		public CodeParameterDeclarationExpression this[int index]
		{
			get
			{
				return (CodeParameterDeclarationExpression)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to add.</param>
		/// <returns>The index at which the new element was inserted.</returns>
		// Token: 0x0600199B RID: 6555 RVA: 0x00051042 File Offset: 0x0004F242
		public int Add(CodeParameterDeclarationExpression value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600199C RID: 6556 RVA: 0x00060B28 File Offset: 0x0005ED28
		public void AddRange(CodeParameterDeclarationExpression[] value)
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

		/// <summary>Adds the contents of another <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600199D RID: 6557 RVA: 0x00060B5C File Offset: 0x0005ED5C
		public void AddRange(CodeParameterDeclarationExpressionCollection value)
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

		/// <summary>Gets a value indicating whether the collection contains the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" />.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to search for in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600199E RID: 6558 RVA: 0x000510DC File Offset: 0x0004F2DC
		public bool Contains(CodeParameterDeclarationExpression value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to begin inserting.</param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.  
		///  -or-  
		///  The number of elements in the <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index.</exception>
		// Token: 0x0600199F RID: 6559 RVA: 0x000510EA File Offset: 0x0004F2EA
		public void CopyTo(CodeParameterDeclarationExpression[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" />, if it exists in the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to locate in the collection.</param>
		/// <returns>The index in the collection of the specified object, if found; otherwise, -1.</returns>
		// Token: 0x060019A0 RID: 6560 RVA: 0x000510F9 File Offset: 0x0004F2F9
		public int IndexOf(CodeParameterDeclarationExpression value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted.</param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to insert.</param>
		// Token: 0x060019A1 RID: 6561 RVA: 0x00051107 File Offset: 0x0004F307
		public void Insert(int index, CodeParameterDeclarationExpression value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection.</exception>
		// Token: 0x060019A2 RID: 6562 RVA: 0x00051159 File Offset: 0x0004F359
		public void Remove(CodeParameterDeclarationExpression value)
		{
			base.List.Remove(value);
		}
	}
}
