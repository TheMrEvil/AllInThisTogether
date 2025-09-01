using System;

namespace System.Xml.Schema
{
	// Token: 0x02000569 RID: 1385
	internal class RedefineEntry
	{
		// Token: 0x060036F3 RID: 14067 RVA: 0x00135006 File Offset: 0x00133206
		public RedefineEntry(XmlSchemaRedefine external, XmlSchema schema)
		{
			this.redefine = external;
			this.schemaToUpdate = schema;
		}

		// Token: 0x04002857 RID: 10327
		internal XmlSchemaRedefine redefine;

		// Token: 0x04002858 RID: 10328
		internal XmlSchema schemaToUpdate;
	}
}
