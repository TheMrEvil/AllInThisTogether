using System;
using System.Configuration.Internal;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Wraps the corresponding <see cref="T:System.Xml.XmlDocument" /> type and also carries the necessary information for reporting file-name and line numbers.</summary>
	// Token: 0x0200019F RID: 415
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public sealed class ConfigXmlDocument : XmlDocument, IConfigXmlNode, IConfigErrorInfo
	{
		/// <summary>Creates a configuration element attribute.</summary>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name that is used locally.</param>
		/// <param name="namespaceUri">The URL that is assigned to the namespace.</param>
		/// <returns>The <see cref="P:System.Xml.Serialization.XmlAttributes.XmlAttribute" /> attribute.</returns>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002EE33 File Offset: 0x0002D033
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlDocument.ConfigXmlAttribute(this, prefix, localName, namespaceUri);
		}

		/// <summary>Creates an XML CData section.</summary>
		/// <param name="data">The data to use.</param>
		/// <returns>The <see cref="T:System.Xml.XmlCDataSection" /> value.</returns>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002EE3E File Offset: 0x0002D03E
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new ConfigXmlDocument.ConfigXmlCDataSection(this, data);
		}

		/// <summary>Create an XML comment.</summary>
		/// <param name="data">The comment data.</param>
		/// <returns>The <see cref="T:System.Xml.XmlComment" /> value.</returns>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002EE47 File Offset: 0x0002D047
		public override XmlComment CreateComment(string data)
		{
			return new ConfigXmlDocument.ConfigXmlComment(this, data);
		}

		/// <summary>Creates a configuration element.</summary>
		/// <param name="prefix">The prefix definition.</param>
		/// <param name="localName">The name used locally.</param>
		/// <param name="namespaceUri">The namespace for the URL.</param>
		/// <returns>The <see cref="T:System.Xml.XmlElement" /> value.</returns>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002EE50 File Offset: 0x0002D050
		public override XmlElement CreateElement(string prefix, string localName, string namespaceUri)
		{
			return new ConfigXmlDocument.ConfigXmlElement(this, prefix, localName, namespaceUri);
		}

		/// <summary>Creates white spaces.</summary>
		/// <param name="data">The data to use.</param>
		/// <returns>The <see cref="T:System.Xml.XmlSignificantWhitespace" /> value.</returns>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002EE5B File Offset: 0x0002D05B
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string data)
		{
			return base.CreateSignificantWhitespace(data);
		}

		/// <summary>Create a text node.</summary>
		/// <param name="text">The text to use.</param>
		/// <returns>The <see cref="T:System.Xml.XmlText" /> value.</returns>
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002EE64 File Offset: 0x0002D064
		public override XmlText CreateTextNode(string text)
		{
			return new ConfigXmlDocument.ConfigXmlText(this, text);
		}

		/// <summary>Creates white space.</summary>
		/// <param name="data">The data to use.</param>
		/// <returns>The <see cref="T:System.Xml.XmlWhitespace" /> value.</returns>
		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002EE6D File Offset: 0x0002D06D
		public override XmlWhitespace CreateWhitespace(string data)
		{
			return base.CreateWhitespace(data);
		}

		/// <summary>Loads the configuration file.</summary>
		/// <param name="filename">The name of the file.</param>
		// Token: 0x06000AEA RID: 2794 RVA: 0x0002EE78 File Offset: 0x0002D078
		public override void Load(string filename)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(filename);
			try
			{
				xmlTextReader.MoveToContent();
				this.LoadSingleElement(filename, xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Loads a single configuration element.</summary>
		/// <param name="filename">The name of the file.</param>
		/// <param name="sourceReader">The source for the reader.</param>
		// Token: 0x06000AEB RID: 2795 RVA: 0x0002EEB4 File Offset: 0x0002D0B4
		public void LoadSingleElement(string filename, XmlTextReader sourceReader)
		{
			this.fileName = filename;
			this.lineNumber = sourceReader.LineNumber;
			string s = sourceReader.ReadOuterXml();
			this.reader = new XmlTextReader(new StringReader(s), sourceReader.NameTable);
			this.Load(this.reader);
			this.reader.Close();
		}

		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0002EF09 File Offset: 0x0002D109
		public string Filename
		{
			get
			{
				if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
				}
				return this.fileName;
			}
		}

		/// <summary>Gets the current node line number.</summary>
		/// <returns>The line number for the current node.</returns>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002EF3F File Offset: 0x0002D13F
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the configuration file name.</summary>
		/// <returns>The file name.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0002EF47 File Offset: 0x0002D147
		string IConfigErrorInfo.Filename
		{
			get
			{
				return this.Filename;
			}
		}

		/// <summary>Gets the configuration line number.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0002EF4F File Offset: 0x0002D14F
		int IConfigErrorInfo.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0002EF47 File Offset: 0x0002D147
		string IConfigXmlNode.Filename
		{
			get
			{
				return this.Filename;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0002EF4F File Offset: 0x0002D14F
		int IConfigXmlNode.LineNumber
		{
			get
			{
				return this.LineNumber;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigXmlDocument" /> class.</summary>
		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002EF57 File Offset: 0x0002D157
		public ConfigXmlDocument()
		{
		}

		// Token: 0x04000733 RID: 1843
		private XmlTextReader reader;

		// Token: 0x04000734 RID: 1844
		private string fileName;

		// Token: 0x04000735 RID: 1845
		private int lineNumber;

		// Token: 0x020001A0 RID: 416
		private class ConfigXmlAttribute : XmlAttribute, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AF3 RID: 2803 RVA: 0x0002EF5F File Offset: 0x0002D15F
			public ConfigXmlAttribute(ConfigXmlDocument document, string prefix, string localName, string namespaceUri) : base(prefix, localName, namespaceUri, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0002EF84 File Offset: 0x0002D184
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0002EFBA File Offset: 0x0002D1BA
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000736 RID: 1846
			private string fileName;

			// Token: 0x04000737 RID: 1847
			private int lineNumber;
		}

		// Token: 0x020001A1 RID: 417
		private class ConfigXmlCDataSection : XmlCDataSection, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AF6 RID: 2806 RVA: 0x0002EFC2 File Offset: 0x0002D1C2
			public ConfigXmlCDataSection(ConfigXmlDocument document, string data) : base(data, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002EFE4 File Offset: 0x0002D1E4
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0002F01A File Offset: 0x0002D21A
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x04000738 RID: 1848
			private string fileName;

			// Token: 0x04000739 RID: 1849
			private int lineNumber;
		}

		// Token: 0x020001A2 RID: 418
		private class ConfigXmlComment : XmlComment, IConfigXmlNode
		{
			// Token: 0x06000AF9 RID: 2809 RVA: 0x0002F022 File Offset: 0x0002D222
			public ConfigXmlComment(ConfigXmlDocument document, string comment) : base(comment, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002F044 File Offset: 0x0002D244
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0002F07A File Offset: 0x0002D27A
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x0400073A RID: 1850
			private string fileName;

			// Token: 0x0400073B RID: 1851
			private int lineNumber;
		}

		// Token: 0x020001A3 RID: 419
		private class ConfigXmlElement : XmlElement, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AFC RID: 2812 RVA: 0x0002F082 File Offset: 0x0002D282
			public ConfigXmlElement(ConfigXmlDocument document, string prefix, string localName, string namespaceUri) : base(prefix, localName, namespaceUri, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0002F0A7 File Offset: 0x0002D2A7
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0002F0DD File Offset: 0x0002D2DD
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x0400073C RID: 1852
			private string fileName;

			// Token: 0x0400073D RID: 1853
			private int lineNumber;
		}

		// Token: 0x020001A4 RID: 420
		private class ConfigXmlText : XmlText, IConfigXmlNode, IConfigErrorInfo
		{
			// Token: 0x06000AFF RID: 2815 RVA: 0x0002F0E5 File Offset: 0x0002D2E5
			public ConfigXmlText(ConfigXmlDocument document, string data) : base(data, document)
			{
				this.fileName = document.fileName;
				this.lineNumber = document.LineNumber;
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0002F107 File Offset: 0x0002D307
			public string Filename
			{
				get
				{
					if (this.fileName != null && this.fileName.Length > 0 && SecurityManager.SecurityEnabled)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
					}
					return this.fileName;
				}
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0002F13D File Offset: 0x0002D33D
			public int LineNumber
			{
				get
				{
					return this.lineNumber;
				}
			}

			// Token: 0x0400073E RID: 1854
			private string fileName;

			// Token: 0x0400073F RID: 1855
			private int lineNumber;
		}
	}
}
