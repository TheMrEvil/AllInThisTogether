using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event.</summary>
	// Token: 0x0200030D RID: 781
	public class XmlAttributeEventArgs : EventArgs
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x000D1584 File Offset: 0x000CF784
		internal XmlAttributeEventArgs(XmlAttribute attr, int lineNumber, int linePosition, object o, string qnames)
		{
			this.attr = attr;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the object being deserialized.</summary>
		/// <returns>The object being deserialized.</returns>
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x000D15B1 File Offset: 0x000CF7B1
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets an object that represents the unknown XML attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlAttribute" /> that represents the unknown XML attribute.</returns>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x000D15B9 File Offset: 0x000CF7B9
		public XmlAttribute Attr
		{
			get
			{
				return this.attr;
			}
		}

		/// <summary>Gets the line number of the unknown XML attribute.</summary>
		/// <returns>The line number of the unknown XML attribute.</returns>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x000D15C1 File Offset: 0x000CF7C1
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the position in the line of the unknown XML attribute.</summary>
		/// <returns>The position number of the unknown XML attribute.</returns>
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x000D15C9 File Offset: 0x000CF7C9
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets a comma-delimited list of XML attribute names expected to be in an XML document instance.</summary>
		/// <returns>A comma-delimited list of XML attribute names. Each name is in the following format: <paramref name="namespace" />:<paramref name="name" />.</returns>
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x000D15D1 File Offset: 0x000CF7D1
		public string ExpectedAttributes
		{
			get
			{
				if (this.qnames != null)
				{
					return this.qnames;
				}
				return string.Empty;
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlAttributeEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B2F RID: 6959
		private object o;

		// Token: 0x04001B30 RID: 6960
		private XmlAttribute attr;

		// Token: 0x04001B31 RID: 6961
		private string qnames;

		// Token: 0x04001B32 RID: 6962
		private int lineNumber;

		// Token: 0x04001B33 RID: 6963
		private int linePosition;
	}
}
