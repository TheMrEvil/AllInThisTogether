using System;
using System.Collections.Generic;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	// Token: 0x02000125 RID: 293
	internal class SchemaObjectInfo
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x000393A4 File Offset: 0x000375A4
		internal SchemaObjectInfo(XmlSchemaType type, XmlSchemaElement element, XmlSchema schema, List<XmlSchemaType> knownTypes)
		{
			this.type = type;
			this.element = element;
			this.schema = schema;
			this.knownTypes = knownTypes;
		}

		// Token: 0x0400066B RID: 1643
		internal XmlSchemaType type;

		// Token: 0x0400066C RID: 1644
		internal XmlSchemaElement element;

		// Token: 0x0400066D RID: 1645
		internal XmlSchema schema;

		// Token: 0x0400066E RID: 1646
		internal List<XmlSchemaType> knownTypes;
	}
}
