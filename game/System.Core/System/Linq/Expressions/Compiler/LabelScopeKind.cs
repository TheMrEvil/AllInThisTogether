using System;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002BA RID: 698
	internal enum LabelScopeKind
	{
		// Token: 0x04000AD2 RID: 2770
		Statement,
		// Token: 0x04000AD3 RID: 2771
		Block,
		// Token: 0x04000AD4 RID: 2772
		Switch,
		// Token: 0x04000AD5 RID: 2773
		Lambda,
		// Token: 0x04000AD6 RID: 2774
		Try,
		// Token: 0x04000AD7 RID: 2775
		Catch,
		// Token: 0x04000AD8 RID: 2776
		Finally,
		// Token: 0x04000AD9 RID: 2777
		Filter,
		// Token: 0x04000ADA RID: 2778
		Expression
	}
}
