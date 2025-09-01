using System;
using System.Collections;

namespace System.Xml.Serialization
{
	/// <summary>Represents a collection of <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> objects.</summary>
	// Token: 0x020002C5 RID: 709
	public class XmlAnyElementAttributes : CollectionBase
	{
		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> at the specified index.</summary>
		/// <param name="index">The index of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />. </param>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> at the specified index.</returns>
		// Token: 0x17000539 RID: 1337
		public XmlAnyElementAttribute this[int index]
		{
			get
			{
				return (XmlAnyElementAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> to the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> to add. </param>
		/// <returns>The index of the newly added <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />.</returns>
		// Token: 0x06001AEB RID: 6891 RVA: 0x0009B522 File Offset: 0x00099722
		public int Add(XmlAnyElementAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		/// <summary>Inserts an <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> into the collection at the specified index.</summary>
		/// <param name="index">The index where the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> is inserted. </param>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> to insert. </param>
		// Token: 0x06001AEC RID: 6892 RVA: 0x0009B530 File Offset: 0x00099730
		public void Insert(int index, XmlAnyElementAttribute attribute)
		{
			base.List.Insert(index, attribute);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> whose index you want. </param>
		/// <returns>The index of the specified <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />.</returns>
		// Token: 0x06001AED RID: 6893 RVA: 0x0009B53F File Offset: 0x0009973F
		public int IndexOf(XmlAnyElementAttribute attribute)
		{
			return base.List.IndexOf(attribute);
		}

		/// <summary>Gets a value that indicates whether the specified <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> exists in the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> you are interested in. </param>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> exists in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001AEE RID: 6894 RVA: 0x0009B54D File Offset: 0x0009974D
		public bool Contains(XmlAnyElementAttribute attribute)
		{
			return base.List.Contains(attribute);
		}

		/// <summary>Removes the specified <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> from the collection.</summary>
		/// <param name="attribute">The <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" /> to remove. </param>
		// Token: 0x06001AEF RID: 6895 RVA: 0x0009B55B File Offset: 0x0009975B
		public void Remove(XmlAnyElementAttribute attribute)
		{
			base.List.Remove(attribute);
		}

		/// <summary>Copies the entire collection to a compatible one-dimensional array of <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> objects, starting at the specified index of the target array. </summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> objects that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06001AF0 RID: 6896 RVA: 0x0009B569 File Offset: 0x00099769
		public void CopyTo(XmlAnyElementAttribute[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttributes" /> class. </summary>
		// Token: 0x06001AF1 RID: 6897 RVA: 0x0009B578 File Offset: 0x00099778
		public XmlAnyElementAttributes()
		{
		}
	}
}
