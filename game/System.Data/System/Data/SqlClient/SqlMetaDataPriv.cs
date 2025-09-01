using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000267 RID: 615
	internal class SqlMetaDataPriv
	{
		// Token: 0x06001CDB RID: 7387 RVA: 0x000895E8 File Offset: 0x000877E8
		internal SqlMetaDataPriv()
		{
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00089608 File Offset: 0x00087808
		internal virtual void CopyFrom(SqlMetaDataPriv original)
		{
			this.type = original.type;
			this.tdsType = original.tdsType;
			this.precision = original.precision;
			this.scale = original.scale;
			this.length = original.length;
			this.collation = original.collation;
			this.codePage = original.codePage;
			this.encoding = original.encoding;
			this.isNullable = original.isNullable;
			this.isMultiValued = original.isMultiValued;
			this.udtDatabaseName = original.udtDatabaseName;
			this.udtSchemaName = original.udtSchemaName;
			this.udtTypeName = original.udtTypeName;
			this.udtAssemblyQualifiedName = original.udtAssemblyQualifiedName;
			this.udtType = original.udtType;
			this.xmlSchemaCollectionDatabase = original.xmlSchemaCollectionDatabase;
			this.xmlSchemaCollectionOwningSchema = original.xmlSchemaCollectionOwningSchema;
			this.xmlSchemaCollectionName = original.xmlSchemaCollectionName;
			this.metaType = original.metaType;
			this.structuredTypeDatabaseName = original.structuredTypeDatabaseName;
			this.structuredTypeSchemaName = original.structuredTypeSchemaName;
			this.structuredTypeName = original.structuredTypeName;
			this.structuredFields = original.structuredFields;
		}

		// Token: 0x040013F4 RID: 5108
		internal SqlDbType type;

		// Token: 0x040013F5 RID: 5109
		internal byte tdsType;

		// Token: 0x040013F6 RID: 5110
		internal byte precision = byte.MaxValue;

		// Token: 0x040013F7 RID: 5111
		internal byte scale = byte.MaxValue;

		// Token: 0x040013F8 RID: 5112
		internal int length;

		// Token: 0x040013F9 RID: 5113
		internal SqlCollation collation;

		// Token: 0x040013FA RID: 5114
		internal int codePage;

		// Token: 0x040013FB RID: 5115
		internal Encoding encoding;

		// Token: 0x040013FC RID: 5116
		internal bool isNullable;

		// Token: 0x040013FD RID: 5117
		internal bool isMultiValued;

		// Token: 0x040013FE RID: 5118
		internal string udtDatabaseName;

		// Token: 0x040013FF RID: 5119
		internal string udtSchemaName;

		// Token: 0x04001400 RID: 5120
		internal string udtTypeName;

		// Token: 0x04001401 RID: 5121
		internal string udtAssemblyQualifiedName;

		// Token: 0x04001402 RID: 5122
		internal Type udtType;

		// Token: 0x04001403 RID: 5123
		internal string xmlSchemaCollectionDatabase;

		// Token: 0x04001404 RID: 5124
		internal string xmlSchemaCollectionOwningSchema;

		// Token: 0x04001405 RID: 5125
		internal string xmlSchemaCollectionName;

		// Token: 0x04001406 RID: 5126
		internal MetaType metaType;

		// Token: 0x04001407 RID: 5127
		internal string structuredTypeDatabaseName;

		// Token: 0x04001408 RID: 5128
		internal string structuredTypeSchemaName;

		// Token: 0x04001409 RID: 5129
		internal string structuredTypeName;

		// Token: 0x0400140A RID: 5130
		internal IList<SmiMetaData> structuredFields;
	}
}
