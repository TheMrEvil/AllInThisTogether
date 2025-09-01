using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Provides data for the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event.</summary>
	// Token: 0x0200030F RID: 783
	public class XmlElementEventArgs : EventArgs
	{
		// Token: 0x06002090 RID: 8336 RVA: 0x000D15E7 File Offset: 0x000CF7E7
		internal XmlElementEventArgs(XmlElement elem, int lineNumber, int linePosition, object o, string qnames)
		{
			this.elem = elem;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the object the <see cref="T:System.Xml.Serialization.XmlSerializer" /> is deserializing.</summary>
		/// <returns>The object that is being deserialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</returns>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x000D1614 File Offset: 0x000CF814
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		/// <summary>Gets the object that represents the unknown XML element.</summary>
		/// <returns>The object that represents the unknown XML element.</returns>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x000D161C File Offset: 0x000CF81C
		public XmlElement Element
		{
			get
			{
				return this.elem;
			}
		}

		/// <summary>Gets the line number where the unknown element was encountered if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />.</summary>
		/// <returns>The line number where the unknown element was encountered if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />; otherwise, -1.</returns>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x000D1624 File Offset: 0x000CF824
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the place in the line where the unknown element occurs if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />.</summary>
		/// <returns>The number in the line where the unknown element occurs if the XML reader is an <see cref="T:System.Xml.XmlTextReader" />; otherwise, -1.</returns>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002094 RID: 8340 RVA: 0x000D162C File Offset: 0x000CF82C
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets a comma-delimited list of XML element names expected to be in an XML document instance.</summary>
		/// <returns>A comma-delimited list of XML element names. Each name is in the following format: <paramref name="namespace" />:<paramref name="name" />.</returns>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x000D1634 File Offset: 0x000CF834
		public string ExpectedElements
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

		// Token: 0x06002096 RID: 8342 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlElementEventArgs()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B34 RID: 6964
		private object o;

		// Token: 0x04001B35 RID: 6965
		private XmlElement elem;

		// Token: 0x04001B36 RID: 6966
		private string qnames;

		// Token: 0x04001B37 RID: 6967
		private int lineNumber;

		// Token: 0x04001B38 RID: 6968
		private int linePosition;
	}
}
