using System;

namespace System.Xml.Schema
{
	// Token: 0x0200059D RID: 1437
	internal sealed class XmlSchemaCollectionNode
	{
		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x0014C7C7 File Offset: 0x0014A9C7
		// (set) Token: 0x06003A2A RID: 14890 RVA: 0x0014C7CF File Offset: 0x0014A9CF
		internal string NamespaceURI
		{
			get
			{
				return this.namespaceUri;
			}
			set
			{
				this.namespaceUri = value;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x0014C7D8 File Offset: 0x0014A9D8
		// (set) Token: 0x06003A2C RID: 14892 RVA: 0x0014C7E0 File Offset: 0x0014A9E0
		internal SchemaInfo SchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06003A2D RID: 14893 RVA: 0x0014C7E9 File Offset: 0x0014A9E9
		// (set) Token: 0x06003A2E RID: 14894 RVA: 0x0014C7F1 File Offset: 0x0014A9F1
		internal XmlSchema Schema
		{
			get
			{
				return this.schema;
			}
			set
			{
				this.schema = value;
			}
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x0000216B File Offset: 0x0000036B
		public XmlSchemaCollectionNode()
		{
		}

		// Token: 0x04002AF3 RID: 10995
		private string namespaceUri;

		// Token: 0x04002AF4 RID: 10996
		private SchemaInfo schemaInfo;

		// Token: 0x04002AF5 RID: 10997
		private XmlSchema schema;
	}
}
