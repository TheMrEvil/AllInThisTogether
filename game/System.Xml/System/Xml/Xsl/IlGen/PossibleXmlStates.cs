using System;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004A6 RID: 1190
	internal enum PossibleXmlStates
	{
		// Token: 0x040024DD RID: 9437
		None,
		// Token: 0x040024DE RID: 9438
		WithinSequence,
		// Token: 0x040024DF RID: 9439
		EnumAttrs,
		// Token: 0x040024E0 RID: 9440
		WithinContent,
		// Token: 0x040024E1 RID: 9441
		WithinAttr,
		// Token: 0x040024E2 RID: 9442
		WithinComment,
		// Token: 0x040024E3 RID: 9443
		WithinPI,
		// Token: 0x040024E4 RID: 9444
		Any
	}
}
