using System;
using System.Xml.Schema;

namespace System.Data
{
	// Token: 0x0200013D RID: 317
	internal sealed class ConstraintTable
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x0004806C File Offset: 0x0004626C
		public ConstraintTable(DataTable t, XmlSchemaIdentityConstraint c)
		{
			this.table = t;
			this.constraint = c;
		}

		// Token: 0x04000A61 RID: 2657
		public DataTable table;

		// Token: 0x04000A62 RID: 2658
		public XmlSchemaIdentityConstraint constraint;
	}
}
