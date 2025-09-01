using System;

namespace System.Xml.Schema
{
	// Token: 0x0200057B RID: 1403
	internal sealed class SchemaNotation
	{
		// Token: 0x06003855 RID: 14421 RVA: 0x001421B8 File Offset: 0x001403B8
		internal SchemaNotation(XmlQualifiedName name)
		{
			this.name = name;
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x001421C7 File Offset: 0x001403C7
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06003857 RID: 14423 RVA: 0x001421CF File Offset: 0x001403CF
		// (set) Token: 0x06003858 RID: 14424 RVA: 0x001421D7 File Offset: 0x001403D7
		internal string SystemLiteral
		{
			get
			{
				return this.systemLiteral;
			}
			set
			{
				this.systemLiteral = value;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x001421E0 File Offset: 0x001403E0
		// (set) Token: 0x0600385A RID: 14426 RVA: 0x001421E8 File Offset: 0x001403E8
		internal string Pubid
		{
			get
			{
				return this.pubid;
			}
			set
			{
				this.pubid = value;
			}
		}

		// Token: 0x040029F0 RID: 10736
		internal const int SYSTEM = 0;

		// Token: 0x040029F1 RID: 10737
		internal const int PUBLIC = 1;

		// Token: 0x040029F2 RID: 10738
		private XmlQualifiedName name;

		// Token: 0x040029F3 RID: 10739
		private string systemLiteral;

		// Token: 0x040029F4 RID: 10740
		private string pubid;
	}
}
