using System;
using System.Collections;
using Unity;

namespace System.Xml.Schema
{
	/// <summary>Supports a simple iteration over a collection. This class cannot be inherited. </summary>
	// Token: 0x0200059E RID: 1438
	public sealed class XmlSchemaCollectionEnumerator : IEnumerator
	{
		// Token: 0x06003A30 RID: 14896 RVA: 0x0014C7FA File Offset: 0x0014A9FA
		internal XmlSchemaCollectionEnumerator(Hashtable collection)
		{
			this.enumerator = collection.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollectionEnumerator.System#Collections#IEnumerator#Reset" />.</summary>
		// Token: 0x06003A31 RID: 14897 RVA: 0x0014C80E File Offset: 0x0014AA0E
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Schema.XmlSchemaCollectionEnumerator.MoveNext" />.</summary>
		/// <returns>Returns the next node.</returns>
		// Token: 0x06003A32 RID: 14898 RVA: 0x0014C81B File Offset: 0x0014AA1B
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Advances the enumerator to the next schema in the collection.</summary>
		/// <returns>
		///     <see langword="true" /> if the move was successful; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		// Token: 0x06003A33 RID: 14899 RVA: 0x0014C81B File Offset: 0x0014AA1B
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.Schema.XmlSchemaCollectionEnumerator.Current" />.</summary>
		/// <returns>Returns the current node.</returns>
		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x0014C828 File Offset: 0x0014AA28
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Xml.Schema.XmlSchema" /> in the collection.</summary>
		/// <returns>The current <see langword="XmlSchema" /> in the collection.</returns>
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06003A35 RID: 14901 RVA: 0x0014C830 File Offset: 0x0014AA30
		public XmlSchema Current
		{
			get
			{
				XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.enumerator.Value;
				if (xmlSchemaCollectionNode != null)
				{
					return xmlSchemaCollectionNode.Schema;
				}
				return null;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x0014C859 File Offset: 0x0014AA59
		internal XmlSchemaCollectionNode CurrentNode
		{
			get
			{
				return (XmlSchemaCollectionNode)this.enumerator.Value;
			}
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlSchemaCollectionEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002AF6 RID: 10998
		private IDictionaryEnumerator enumerator;
	}
}
