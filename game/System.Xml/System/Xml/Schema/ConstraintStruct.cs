using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020004ED RID: 1261
	internal sealed class ConstraintStruct
	{
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x00126918 File Offset: 0x00124B18
		internal int TableDim
		{
			get
			{
				return this.tableDim;
			}
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x00126920 File Offset: 0x00124B20
		internal ConstraintStruct(CompiledIdentityConstraint constraint)
		{
			this.constraint = constraint;
			this.tableDim = constraint.Fields.Length;
			this.axisFields = new ArrayList();
			this.axisSelector = new SelectorActiveAxis(constraint.Selector, this);
			if (this.constraint.Role != CompiledIdentityConstraint.ConstraintRole.Keyref)
			{
				this.qualifiedTable = new Hashtable();
			}
		}

		// Token: 0x040026AD RID: 9901
		internal CompiledIdentityConstraint constraint;

		// Token: 0x040026AE RID: 9902
		internal SelectorActiveAxis axisSelector;

		// Token: 0x040026AF RID: 9903
		internal ArrayList axisFields;

		// Token: 0x040026B0 RID: 9904
		internal Hashtable qualifiedTable;

		// Token: 0x040026B1 RID: 9905
		internal Hashtable keyrefTable;

		// Token: 0x040026B2 RID: 9906
		private int tableDim;
	}
}
