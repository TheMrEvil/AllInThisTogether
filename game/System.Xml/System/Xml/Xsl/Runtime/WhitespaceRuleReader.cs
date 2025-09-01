using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000462 RID: 1122
	internal class WhitespaceRuleReader : XmlWrappingReader
	{
		// Token: 0x06002B70 RID: 11120 RVA: 0x00104488 File Offset: 0x00102688
		public static XmlReader CreateReader(XmlReader baseReader, WhitespaceRuleLookup wsRules)
		{
			if (wsRules == null)
			{
				return baseReader;
			}
			XmlReaderSettings settings = baseReader.Settings;
			if (settings != null)
			{
				if (settings.IgnoreWhitespace)
				{
					return baseReader;
				}
			}
			else
			{
				XmlTextReader xmlTextReader = baseReader as XmlTextReader;
				if (xmlTextReader != null && xmlTextReader.WhitespaceHandling == WhitespaceHandling.None)
				{
					return baseReader;
				}
				XmlTextReaderImpl xmlTextReaderImpl = baseReader as XmlTextReaderImpl;
				if (xmlTextReaderImpl != null && xmlTextReaderImpl.WhitespaceHandling == WhitespaceHandling.None)
				{
					return baseReader;
				}
			}
			return new WhitespaceRuleReader(baseReader, wsRules);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x001044E0 File Offset: 0x001026E0
		private WhitespaceRuleReader(XmlReader baseReader, WhitespaceRuleLookup wsRules) : base(baseReader)
		{
			this.val = null;
			this.stkStrip = new BitStack();
			this.shouldStrip = false;
			this.preserveAdjacent = false;
			this.wsRules = wsRules;
			this.wsRules.Atomize(baseReader.NameTable);
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002B72 RID: 11122 RVA: 0x00104537 File Offset: 0x00102737
		public override string Value
		{
			get
			{
				if (this.val != null)
				{
					return this.val;
				}
				return base.Value;
			}
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x00104550 File Offset: 0x00102750
		public override bool Read()
		{
			XmlCharType instance = XmlCharType.Instance;
			string text = null;
			this.val = null;
			while (base.Read())
			{
				XmlNodeType nodeType = base.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType - XmlNodeType.Text > 1)
					{
						switch (nodeType)
						{
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							break;
						case XmlNodeType.EndElement:
							this.shouldStrip = this.stkStrip.PopBit();
							goto IL_10E;
						case XmlNodeType.EndEntity:
							continue;
						default:
							goto IL_10E;
						}
					}
					else
					{
						if (this.preserveAdjacent)
						{
							return true;
						}
						if (!this.shouldStrip)
						{
							goto IL_10E;
						}
						if (!instance.IsOnlyWhitespace(base.Value))
						{
							if (text != null)
							{
								this.val = text + base.Value;
							}
							this.preserveAdjacent = true;
							return true;
						}
					}
					if (this.preserveAdjacent)
					{
						return true;
					}
					if (this.shouldStrip)
					{
						if (text == null)
						{
							text = base.Value;
							continue;
						}
						text += base.Value;
						continue;
					}
				}
				else if (!base.IsEmptyElement)
				{
					this.stkStrip.PushBit(this.shouldStrip);
					this.shouldStrip = (this.wsRules.ShouldStripSpace(base.LocalName, base.NamespaceURI) && base.XmlSpace != XmlSpace.Preserve);
				}
				IL_10E:
				this.preserveAdjacent = false;
				return true;
			}
			return false;
		}

		// Token: 0x04002287 RID: 8839
		private WhitespaceRuleLookup wsRules;

		// Token: 0x04002288 RID: 8840
		private BitStack stkStrip;

		// Token: 0x04002289 RID: 8841
		private bool shouldStrip;

		// Token: 0x0400228A RID: 8842
		private bool preserveAdjacent;

		// Token: 0x0400228B RID: 8843
		private string val;

		// Token: 0x0400228C RID: 8844
		private XmlCharType xmlCharType = XmlCharType.Instance;
	}
}
