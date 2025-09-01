using System;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000359 RID: 857
	internal class BuilderInfo
	{
		// Token: 0x06002352 RID: 9042 RVA: 0x000DBAFD File Offset: 0x000D9CFD
		internal BuilderInfo()
		{
			this.Initialize(string.Empty, string.Empty, string.Empty);
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x000DBB26 File Offset: 0x000D9D26
		internal void Initialize(string prefix, string name, string nspace)
		{
			this.prefix = prefix;
			this.localName = name;
			this.namespaceURI = nspace;
			this.name = null;
			this.htmlProps = null;
			this.htmlAttrProps = null;
			this.TextInfoCount = 0;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000DBB5C File Offset: 0x000D9D5C
		internal void Initialize(BuilderInfo src)
		{
			this.prefix = src.Prefix;
			this.localName = src.LocalName;
			this.namespaceURI = src.NamespaceURI;
			this.name = null;
			this.depth = src.Depth;
			this.nodeType = src.NodeType;
			this.htmlProps = src.htmlProps;
			this.htmlAttrProps = src.htmlAttrProps;
			this.TextInfoCount = 0;
			this.EnsureTextInfoSize(src.TextInfoCount);
			src.TextInfo.CopyTo(this.TextInfo, 0);
			this.TextInfoCount = src.TextInfoCount;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000DBBF8 File Offset: 0x000D9DF8
		private void EnsureTextInfoSize(int newSize)
		{
			if (this.TextInfo.Length < newSize)
			{
				string[] array = new string[newSize * 2];
				Array.Copy(this.TextInfo, array, this.TextInfoCount);
				this.TextInfo = array;
			}
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000DBC32 File Offset: 0x000D9E32
		internal BuilderInfo Clone()
		{
			BuilderInfo builderInfo = new BuilderInfo();
			builderInfo.Initialize(this);
			return builderInfo;
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x000DBC40 File Offset: 0x000D9E40
		internal string Name
		{
			get
			{
				if (this.name == null)
				{
					string text = this.Prefix;
					string text2 = this.LocalName;
					if (text != null && 0 < text.Length)
					{
						if (text2.Length > 0)
						{
							this.name = text + ":" + text2;
						}
						else
						{
							this.name = text;
						}
					}
					else
					{
						this.name = text2;
					}
				}
				return this.name;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x000DBCA2 File Offset: 0x000D9EA2
		// (set) Token: 0x06002359 RID: 9049 RVA: 0x000DBCAA File Offset: 0x000D9EAA
		internal string LocalName
		{
			get
			{
				return this.localName;
			}
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x000DBCB3 File Offset: 0x000D9EB3
		// (set) Token: 0x0600235B RID: 9051 RVA: 0x000DBCBB File Offset: 0x000D9EBB
		internal string NamespaceURI
		{
			get
			{
				return this.namespaceURI;
			}
			set
			{
				this.namespaceURI = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x000DBCC4 File Offset: 0x000D9EC4
		// (set) Token: 0x0600235D RID: 9053 RVA: 0x000DBCCC File Offset: 0x000D9ECC
		internal string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x000DBCD8 File Offset: 0x000D9ED8
		// (set) Token: 0x0600235F RID: 9055 RVA: 0x000DBD69 File Offset: 0x000D9F69
		internal string Value
		{
			get
			{
				int textInfoCount = this.TextInfoCount;
				if (textInfoCount == 0)
				{
					return string.Empty;
				}
				if (textInfoCount != 1)
				{
					int num = 0;
					for (int i = 0; i < this.TextInfoCount; i++)
					{
						string text = this.TextInfo[i];
						if (text != null)
						{
							num += text.Length;
						}
					}
					StringBuilder stringBuilder = new StringBuilder(num);
					for (int j = 0; j < this.TextInfoCount; j++)
					{
						string text2 = this.TextInfo[j];
						if (text2 != null)
						{
							stringBuilder.Append(text2);
						}
					}
					return stringBuilder.ToString();
				}
				return this.TextInfo[0];
			}
			set
			{
				this.TextInfoCount = 0;
				this.ValueAppend(value, false);
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x000DBD7C File Offset: 0x000D9F7C
		internal void ValueAppend(string s, bool disableEscaping)
		{
			if (s == null || s.Length == 0)
			{
				return;
			}
			this.EnsureTextInfoSize(this.TextInfoCount + (disableEscaping ? 2 : 1));
			int textInfoCount;
			if (disableEscaping)
			{
				string[] textInfo = this.TextInfo;
				textInfoCount = this.TextInfoCount;
				this.TextInfoCount = textInfoCount + 1;
				textInfo[textInfoCount] = null;
			}
			string[] textInfo2 = this.TextInfo;
			textInfoCount = this.TextInfoCount;
			this.TextInfoCount = textInfoCount + 1;
			textInfo2[textInfoCount] = s;
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x000DBDDE File Offset: 0x000D9FDE
		// (set) Token: 0x06002362 RID: 9058 RVA: 0x000DBDE6 File Offset: 0x000D9FE6
		internal XmlNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType = value;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000DBDEF File Offset: 0x000D9FEF
		// (set) Token: 0x06002364 RID: 9060 RVA: 0x000DBDF7 File Offset: 0x000D9FF7
		internal int Depth
		{
			get
			{
				return this.depth;
			}
			set
			{
				this.depth = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x000DBE00 File Offset: 0x000DA000
		// (set) Token: 0x06002366 RID: 9062 RVA: 0x000DBE08 File Offset: 0x000DA008
		internal bool IsEmptyTag
		{
			get
			{
				return this.isEmptyTag;
			}
			set
			{
				this.isEmptyTag = value;
			}
		}

		// Token: 0x04001C87 RID: 7303
		private string name;

		// Token: 0x04001C88 RID: 7304
		private string localName;

		// Token: 0x04001C89 RID: 7305
		private string namespaceURI;

		// Token: 0x04001C8A RID: 7306
		private string prefix;

		// Token: 0x04001C8B RID: 7307
		private XmlNodeType nodeType;

		// Token: 0x04001C8C RID: 7308
		private int depth;

		// Token: 0x04001C8D RID: 7309
		private bool isEmptyTag;

		// Token: 0x04001C8E RID: 7310
		internal string[] TextInfo = new string[4];

		// Token: 0x04001C8F RID: 7311
		internal int TextInfoCount;

		// Token: 0x04001C90 RID: 7312
		internal bool search;

		// Token: 0x04001C91 RID: 7313
		internal HtmlElementProps htmlProps;

		// Token: 0x04001C92 RID: 7314
		internal HtmlAttributeProps htmlAttrProps;
	}
}
