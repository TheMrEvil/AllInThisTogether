using System;
using System.Xml.Schema;

namespace System.Xml
{
	// Token: 0x02000065 RID: 101
	internal class XmlAsyncCheckReaderWithLineInfoNSSchema : XmlAsyncCheckReaderWithLineInfoNS, IXmlSchemaInfo
	{
		// Token: 0x0600036D RID: 877 RVA: 0x00010DE1 File Offset: 0x0000EFE1
		public XmlAsyncCheckReaderWithLineInfoNSSchema(XmlReader reader) : base(reader)
		{
			this.readerAsIXmlSchemaInfo = (IXmlSchemaInfo)reader;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00010DF6 File Offset: 0x0000EFF6
		XmlSchemaValidity IXmlSchemaInfo.Validity
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.Validity;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00010E03 File Offset: 0x0000F003
		bool IXmlSchemaInfo.IsDefault
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.IsDefault;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00010E10 File Offset: 0x0000F010
		bool IXmlSchemaInfo.IsNil
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.IsNil;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00010E1D File Offset: 0x0000F01D
		XmlSchemaSimpleType IXmlSchemaInfo.MemberType
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.MemberType;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00010E2A File Offset: 0x0000F02A
		XmlSchemaType IXmlSchemaInfo.SchemaType
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaType;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00010E37 File Offset: 0x0000F037
		XmlSchemaElement IXmlSchemaInfo.SchemaElement
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaElement;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00010E44 File Offset: 0x0000F044
		XmlSchemaAttribute IXmlSchemaInfo.SchemaAttribute
		{
			get
			{
				return this.readerAsIXmlSchemaInfo.SchemaAttribute;
			}
		}

		// Token: 0x040006B2 RID: 1714
		private readonly IXmlSchemaInfo readerAsIXmlSchemaInfo;
	}
}
