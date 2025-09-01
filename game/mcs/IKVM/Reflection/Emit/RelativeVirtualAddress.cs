using System;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000EB RID: 235
	public struct RelativeVirtualAddress
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x00028C8E File Offset: 0x00026E8E
		internal RelativeVirtualAddress(uint initializedDataOffset)
		{
			this.initializedDataOffset = initializedDataOffset;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00028C97 File Offset: 0x00026E97
		public static RelativeVirtualAddress operator +(RelativeVirtualAddress rva, int offset)
		{
			return new RelativeVirtualAddress(rva.initializedDataOffset + (uint)offset);
		}

		// Token: 0x040004E9 RID: 1257
		internal readonly uint initializedDataOffset;
	}
}
