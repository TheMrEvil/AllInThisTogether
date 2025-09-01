using System;

namespace System.Xml.Schema
{
	// Token: 0x020005E9 RID: 1513
	internal enum ValidatorState
	{
		// Token: 0x04002BEA RID: 11242
		None,
		// Token: 0x04002BEB RID: 11243
		Start,
		// Token: 0x04002BEC RID: 11244
		TopLevelAttribute,
		// Token: 0x04002BED RID: 11245
		TopLevelTextOrWS,
		// Token: 0x04002BEE RID: 11246
		Element,
		// Token: 0x04002BEF RID: 11247
		Attribute,
		// Token: 0x04002BF0 RID: 11248
		EndOfAttributes,
		// Token: 0x04002BF1 RID: 11249
		Text,
		// Token: 0x04002BF2 RID: 11250
		Whitespace,
		// Token: 0x04002BF3 RID: 11251
		EndElement,
		// Token: 0x04002BF4 RID: 11252
		SkipToEndElement,
		// Token: 0x04002BF5 RID: 11253
		Finish
	}
}
