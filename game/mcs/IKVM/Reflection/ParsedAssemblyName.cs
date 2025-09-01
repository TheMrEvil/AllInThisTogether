using System;

namespace IKVM.Reflection
{
	// Token: 0x0200002C RID: 44
	internal struct ParsedAssemblyName
	{
		// Token: 0x04000125 RID: 293
		internal string Name;

		// Token: 0x04000126 RID: 294
		internal string Version;

		// Token: 0x04000127 RID: 295
		internal string Culture;

		// Token: 0x04000128 RID: 296
		internal string PublicKeyToken;

		// Token: 0x04000129 RID: 297
		internal bool? Retargetable;

		// Token: 0x0400012A RID: 298
		internal ProcessorArchitecture ProcessorArchitecture;

		// Token: 0x0400012B RID: 299
		internal bool HasPublicKey;

		// Token: 0x0400012C RID: 300
		internal bool WindowsRuntime;
	}
}
