using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000401 RID: 1025
	internal class XslNode
	{
		// Token: 0x060028B1 RID: 10417 RVA: 0x000F4F11 File Offset: 0x000F3111
		public XslNode(XslNodeType nodeType, QilName name, object arg, XslVersion xslVer)
		{
			this.NodeType = nodeType;
			this.Name = name;
			this.Arg = arg;
			this.XslVersion = xslVer;
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x000F4F36 File Offset: 0x000F3136
		public XslNode(XslNodeType nodeType)
		{
			this.NodeType = nodeType;
			this.XslVersion = XslVersion.Version10;
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x000F4F4C File Offset: 0x000F314C
		public string Select
		{
			get
			{
				return (string)this.Arg;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x000F4F59 File Offset: 0x000F3159
		public bool ForwardsCompatible
		{
			get
			{
				return this.XslVersion == XslVersion.ForwardsCompatible;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x000F4F64 File Offset: 0x000F3164
		public IList<XslNode> Content
		{
			get
			{
				IList<XslNode> list = this.content;
				return list ?? XslNode.EmptyList;
			}
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000F4F82 File Offset: 0x000F3182
		public void SetContent(List<XslNode> content)
		{
			this.content = content;
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000F4F8B File Offset: 0x000F318B
		public void AddContent(XslNode node)
		{
			if (this.content == null)
			{
				this.content = new List<XslNode>();
			}
			this.content.Add(node);
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000F4FAC File Offset: 0x000F31AC
		public void InsertContent(IEnumerable<XslNode> collection)
		{
			if (this.content == null)
			{
				this.content = new List<XslNode>(collection);
				return;
			}
			this.content.InsertRange(0, collection);
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060028B9 RID: 10425 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal string TraceName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000F4FD0 File Offset: 0x000F31D0
		// Note: this type is marked as 'beforefieldinit'.
		static XslNode()
		{
		}

		// Token: 0x04002044 RID: 8260
		public readonly XslNodeType NodeType;

		// Token: 0x04002045 RID: 8261
		public ISourceLineInfo SourceLine;

		// Token: 0x04002046 RID: 8262
		public NsDecl Namespaces;

		// Token: 0x04002047 RID: 8263
		public readonly QilName Name;

		// Token: 0x04002048 RID: 8264
		public readonly object Arg;

		// Token: 0x04002049 RID: 8265
		public readonly XslVersion XslVersion;

		// Token: 0x0400204A RID: 8266
		public XslFlags Flags;

		// Token: 0x0400204B RID: 8267
		private List<XslNode> content;

		// Token: 0x0400204C RID: 8268
		private static readonly IList<XslNode> EmptyList = new List<XslNode>().AsReadOnly();
	}
}
