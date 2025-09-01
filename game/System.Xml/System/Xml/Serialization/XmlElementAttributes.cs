using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> objects used by the <see cref="T:System.Xml.Serialization.XmlSerializer" /> to override the default way it serializes a class.</summary>
	// Token: 0x020002D1 RID: 721
	public class XmlElementAttributes : CollectionBase
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> is not a valid index in the <see cref="T:System.Xml.Serialization.XmlElementAttributes" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Xml.Serialization.XmlElementAttributes" /> is read-only. </exception>
		// Token: 0x1700058A RID: 1418
		public XmlElementAttribute this[int index]
		{
			get
			{
				return (XmlElementAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to add. </param>
		/// <returns>The zero-based index of the newly added item.</returns>
		// Token: 0x06001BF1 RID: 7153 RVA: 0x0009B522 File Offset: 0x00099722
		public int Add(XmlElementAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> into the collection.</summary>
		/// <param name="index">The zero-based index where the member is inserted. </param>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to insert. </param>
		// Token: 0x06001BF2 RID: 7154 RVA: 0x0009B530 File Offset: 0x00099730
		public void Insert(int index, XmlElementAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Xml.Serialization.XmlElementAttribute" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> whose index is being retrieved.</param>
		/// <returns>The zero-based index of the <see cref="T:System.Xml.Serialization.XmlElementAttribute" />.</returns>
		// Token: 0x06001BF3 RID: 7155 RVA: 0x0009B53F File Offset: 0x0009973F
		public int IndexOf(XmlElementAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		/// <summary>Determines whether the collection contains the specified object.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to look for. </param>
		/// <returns>
		///     <see langword="true" /> if the object exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001BF4 RID: 7156 RVA: 0x0009B54D File Offset: 0x0009974D
		public bool Contains(XmlElementAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		/// <summary>Removes the specified object from the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> to remove from the collection. </param>
		// Token: 0x06001BF5 RID: 7157 RVA: 0x0009B55B File Offset: 0x0009975B
		public void Remove(XmlElementAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		/// <summary>Copies the <see cref="T:System.Xml.Serialization.XmlElementAttributes" />, or a portion of it to a one-dimensional array.</summary>
		/// <param name="array">The <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> array to hold the copied elements. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		// Token: 0x06001BF6 RID: 7158 RVA: 0x0009B569 File Offset: 0x00099769
		public void CopyTo(XmlElementAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlElementAttributes" /> class. </summary>
		// Token: 0x06001BF7 RID: 7159 RVA: 0x0009B578 File Offset: 0x00099778
		public XmlElementAttributes()
		{
		}
	}
}
