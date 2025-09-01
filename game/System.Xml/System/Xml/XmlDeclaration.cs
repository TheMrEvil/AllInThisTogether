using System;
using System.Text;

namespace System.Xml
{
	/// <summary>Represents the XML declaration node &lt;?xml version='1.0'...?&gt;.</summary>
	// Token: 0x020001BB RID: 443
	public class XmlDeclaration : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDeclaration" /> class.</summary>
		/// <param name="version">The XML version; see the <see cref="P:System.Xml.XmlDeclaration.Version" /> property.</param>
		/// <param name="encoding">The encoding scheme; see the <see cref="P:System.Xml.XmlDeclaration.Encoding" /> property.</param>
		/// <param name="standalone">Indicates whether the XML document depends on an external DTD; see the <see cref="P:System.Xml.XmlDeclaration.Standalone" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06001048 RID: 4168 RVA: 0x000678FC File Offset: 0x00065AFC
		protected internal XmlDeclaration(string version, string encoding, string standalone, XmlDocument doc) : base(doc)
		{
			if (!this.IsValidXmlVersion(version))
			{
				throw new ArgumentException(Res.GetString("Wrong XML version information. The XML must match production \"VersionNum ::= '1.' [0-9]+\"."));
			}
			if (standalone != null && standalone.Length > 0 && standalone != "yes" && standalone != "no")
			{
				throw new ArgumentException(Res.GetString("Wrong value for the XML declaration standalone attribute of '{0}'.", new object[]
				{
					standalone
				}));
			}
			this.Encoding = encoding;
			this.Standalone = standalone;
			this.Version = version;
		}

		/// <summary>Gets the XML version of the document.</summary>
		/// <returns>The value is always <see langword="1.0" />.</returns>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0006797F File Offset: 0x00065B7F
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x00067987 File Offset: 0x00065B87
		public string Version
		{
			get
			{
				return this.version;
			}
			internal set
			{
				this.version = value;
			}
		}

		/// <summary>Gets or sets the encoding level of the XML document.</summary>
		/// <returns>The valid character encoding name. The most commonly supported character encoding names for XML are the following: Category Encoding Names Unicode UTF-8, UTF-16 ISO 10646 ISO-10646-UCS-2, ISO-10646-UCS-4 ISO 8859 ISO-8859-n (where "n" is a digit from 1 to 9) JIS X-0208-1997 ISO-2022-JP, Shift_JIS, EUC-JP This value is optional. If a value is not set, this property returns String.Empty.If an encoding attribute is not included, UTF-8 encoding is assumed when the document is written or saved out.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00067990 File Offset: 0x00065B90
		// (set) Token: 0x0600104C RID: 4172 RVA: 0x00067998 File Offset: 0x00065B98
		public string Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the value of the standalone attribute.</summary>
		/// <returns>Valid values are <see langword="yes" /> if all entity declarations required by the XML document are contained within the document or <see langword="no" /> if an external document type definition (DTD) is required. If a standalone attribute is not present in the XML declaration, this property returns String.Empty.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x000679AB File Offset: 0x00065BAB
		// (set) Token: 0x0600104E RID: 4174 RVA: 0x000679B4 File Offset: 0x00065BB4
		public string Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				if (value == null)
				{
					this.standalone = string.Empty;
					return;
				}
				if (value.Length == 0 || value == "yes" || value == "no")
				{
					this.standalone = value;
					return;
				}
				throw new ArgumentException(Res.GetString("Wrong value for the XML declaration standalone attribute of '{0}'.", new object[]
				{
					value
				}));
			}
		}

		/// <summary>Gets or sets the value of the <see langword="XmlDeclaration" />.</summary>
		/// <returns>The contents of the <see langword="XmlDeclaration" /> (that is, everything between &lt;?xml and ?&gt;).</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x00066758 File Offset: 0x00064958
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x00066760 File Offset: 0x00064960
		public override string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		/// <summary>Gets or sets the concatenated values of the <see langword="XmlDeclaration" />.</summary>
		/// <returns>The concatenated values of the <see langword="XmlDeclaration" /> (that is, everything between &lt;?xml and ?&gt;).</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x00067A14 File Offset: 0x00065C14
		// (set) Token: 0x06001052 RID: 4178 RVA: 0x00067AA8 File Offset: 0x00065CA8
		public override string InnerText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("version=\"" + this.Version + "\"");
				if (this.Encoding.Length > 0)
				{
					stringBuilder.Append(" encoding=\"");
					stringBuilder.Append(this.Encoding);
					stringBuilder.Append("\"");
				}
				if (this.Standalone.Length > 0)
				{
					stringBuilder.Append(" standalone=\"");
					stringBuilder.Append(this.Standalone);
					stringBuilder.Append("\"");
				}
				return stringBuilder.ToString();
			}
			set
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				string text4 = this.Encoding;
				string text5 = this.Standalone;
				string text6 = this.Version;
				XmlLoader.ParseXmlDeclarationValue(value, out text, out text2, out text3);
				try
				{
					if (text != null && !this.IsValidXmlVersion(text))
					{
						throw new ArgumentException(Res.GetString("Wrong XML version information. The XML must match production \"VersionNum ::= '1.' [0-9]+\"."));
					}
					this.Version = text;
					if (text2 != null)
					{
						this.Encoding = text2;
					}
					if (text3 != null)
					{
						this.Standalone = text3;
					}
				}
				catch
				{
					this.Encoding = text4;
					this.Standalone = text5;
					this.Version = text6;
					throw;
				}
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For <see langword="XmlDeclaration" /> nodes, the name is <see langword="xml" />.</returns>
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00067B44 File Offset: 0x00065D44
		public override string Name
		{
			get
			{
				return "xml";
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For <see langword="XmlDeclaration" /> nodes, the local name is <see langword="xml" />.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x00067B4B File Offset: 0x00065D4B
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For <see langword="XmlDeclaration" /> nodes, this value is XmlNodeType.XmlDeclaration.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x00067B53 File Offset: 0x00065D53
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. Because <see langword="XmlDeclaration" /> nodes do not have children, the cloned node always includes the data value, regardless of the parameter setting. </param>
		/// <returns>The cloned node.</returns>
		// Token: 0x06001056 RID: 4182 RVA: 0x00067B57 File Offset: 0x00065D57
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateXmlDeclaration(this.Version, this.Encoding, this.Standalone);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x06001057 RID: 4183 RVA: 0x00067B76 File Offset: 0x00065D76
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.Name, this.InnerText);
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because <see langword="XmlDeclaration" /> nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x06001058 RID: 4184 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00067B8A File Offset: 0x00065D8A
		private bool IsValidXmlVersion(string ver)
		{
			return ver.Length >= 3 && ver[0] == '1' && ver[1] == '.' && XmlCharType.IsOnlyDigits(ver, 2, ver.Length - 2);
		}

		// Token: 0x04001041 RID: 4161
		private const string YES = "yes";

		// Token: 0x04001042 RID: 4162
		private const string NO = "no";

		// Token: 0x04001043 RID: 4163
		private string version;

		// Token: 0x04001044 RID: 4164
		private string encoding;

		// Token: 0x04001045 RID: 4165
		private string standalone;
	}
}
