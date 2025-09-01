using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	/// <summary>Enables iteration over a collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects. </summary>
	// Token: 0x020002E7 RID: 743
	public class XmlSchemaEnumerator : IEnumerator<XmlSchema>, IDisposable, IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlSchemaEnumerator" /> class. </summary>
		/// <param name="list">The <see cref="T:System.Xml.Serialization.XmlSchemas" /> object you want to iterate over.</param>
		// Token: 0x06001D45 RID: 7493 RVA: 0x000AB253 File Offset: 0x000A9453
		public XmlSchemaEnumerator(XmlSchemas list)
		{
			this.list = list;
			this.idx = -1;
			this.end = list.Count - 1;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Xml.Serialization.XmlSchemaEnumerator" />.</summary>
		// Token: 0x06001D46 RID: 7494 RVA: 0x0000B528 File Offset: 0x00009728
		public void Dispose()
		{
		}

		/// <summary>Advances the enumerator to the next item in the collection.</summary>
		/// <returns>
		///     <see langword="true" /> if the move is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D47 RID: 7495 RVA: 0x000AB277 File Offset: 0x000A9477
		public bool MoveNext()
		{
			if (this.idx >= this.end)
			{
				return false;
			}
			this.idx++;
			return true;
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current <see cref="T:System.Xml.Schema.XmlSchema" /> object in the collection.</returns>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x000AB298 File Offset: 0x000A9498
		public XmlSchema Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Gets the current element in the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</summary>
		/// <returns>The current element in the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</returns>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x000AB298 File Offset: 0x000A9498
		object IEnumerator.Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection of <see cref="T:System.Xml.Schema.XmlSchema" /> objects.</summary>
		// Token: 0x06001D4A RID: 7498 RVA: 0x000AB2AB File Offset: 0x000A94AB
		void IEnumerator.Reset()
		{
			this.idx = -1;
		}

		// Token: 0x04001A42 RID: 6722
		private XmlSchemas list;

		// Token: 0x04001A43 RID: 6723
		private int idx;

		// Token: 0x04001A44 RID: 6724
		private int end;
	}
}
