using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006C1 RID: 1729
	[Flags]
	[Serializable]
	internal enum MessageEnum
	{
		// Token: 0x040029D9 RID: 10713
		NoArgs = 1,
		// Token: 0x040029DA RID: 10714
		ArgsInline = 2,
		// Token: 0x040029DB RID: 10715
		ArgsIsArray = 4,
		// Token: 0x040029DC RID: 10716
		ArgsInArray = 8,
		// Token: 0x040029DD RID: 10717
		NoContext = 16,
		// Token: 0x040029DE RID: 10718
		ContextInline = 32,
		// Token: 0x040029DF RID: 10719
		ContextInArray = 64,
		// Token: 0x040029E0 RID: 10720
		MethodSignatureInArray = 128,
		// Token: 0x040029E1 RID: 10721
		PropertyInArray = 256,
		// Token: 0x040029E2 RID: 10722
		NoReturnValue = 512,
		// Token: 0x040029E3 RID: 10723
		ReturnValueVoid = 1024,
		// Token: 0x040029E4 RID: 10724
		ReturnValueInline = 2048,
		// Token: 0x040029E5 RID: 10725
		ReturnValueInArray = 4096,
		// Token: 0x040029E6 RID: 10726
		ExceptionInArray = 8192,
		// Token: 0x040029E7 RID: 10727
		GenericMethod = 32768
	}
}
