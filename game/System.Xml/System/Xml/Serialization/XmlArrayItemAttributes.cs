using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> objects.</summary>
	// Token: 0x020002C8 RID: 712
	public class XmlArrayItemAttributes : CollectionBase
	{
		/// <summary>Gets or sets the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the collection member to get or set. </param>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> at the specified index.</returns>
		// Token: 0x17000547 RID: 1351
		public XmlArrayItemAttribute this[int index]
		{
			get
			{
				return (XmlArrayItemAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to add to the collection. </param>
		/// <returns>The index of the added item.</returns>
		// Token: 0x06001B13 RID: 6931 RVA: 0x0009B522 File Offset: 0x00099722
		public int Add(XmlArrayItemAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> into the collection at the specified index. </summary>
		/// <param name="index">The index at which the attribute is inserted.</param>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" />  to insert.</param>
		// Token: 0x06001B14 RID: 6932 RVA: 0x0009B530 File Offset: 0x00099730
		public void Insert(int index, XmlArrayItemAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> in the collection or -1 if the attribute is not found in the collection. </summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to locate in the collection.</param>
		/// <returns>The first index of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> in the collection or -1 if the attribute is not found in the collection.</returns>
		// Token: 0x06001B15 RID: 6933 RVA: 0x0009B53F File Offset: 0x0009973F
		public int IndexOf(XmlArrayItemAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		/// <summary>Determines whether the collection contains the specified <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" />. </summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to check for.</param>
		/// <returns>
		///     <see langword="true" /> if the collection contains the specified <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001B16 RID: 6934 RVA: 0x0009B54D File Offset: 0x0009974D
		public bool Contains(XmlArrayItemAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		/// <summary>Removes an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> from the collection, if it is present. </summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> to remove.</param>
		// Token: 0x06001B17 RID: 6935 RVA: 0x0009B55B File Offset: 0x0009975B
		public void Remove(XmlArrayItemAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		/// <summary>Copies an <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> array to the collection, starting at a specified target index. </summary>
		/// <param name="array">The array of <see cref="T:System.Xml.Serialization.XmlArrayItemAttribute" /> objects to copy to the collection.</param>
		/// <param name="index">The index at which the copied attributes begin.</param>
		// Token: 0x06001B18 RID: 6936 RVA: 0x0009B569 File Offset: 0x00099769
		public void CopyTo(XmlArrayItemAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlArrayItemAttributes" /> class. </summary>
		// Token: 0x06001B19 RID: 6937 RVA: 0x0009B578 File Offset: 0x00099778
		public XmlArrayItemAttributes()
		{
		}
	}
}
