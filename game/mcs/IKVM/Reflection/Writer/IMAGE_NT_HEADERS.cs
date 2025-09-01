using System;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000087 RID: 135
	internal sealed class IMAGE_NT_HEADERS
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x0001656B File Offset: 0x0001476B
		public IMAGE_NT_HEADERS()
		{
		}

		// Token: 0x04000294 RID: 660
		public uint Signature = 17744U;

		// Token: 0x04000295 RID: 661
		public IMAGE_FILE_HEADER FileHeader = new IMAGE_FILE_HEADER();

		// Token: 0x04000296 RID: 662
		public IMAGE_OPTIONAL_HEADER OptionalHeader = new IMAGE_OPTIONAL_HEADER();
	}
}
