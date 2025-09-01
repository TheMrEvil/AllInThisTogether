using System;
using System.IO;

namespace IKVM.Reflection.Reader
{
	// Token: 0x020000A1 RID: 161
	internal struct IMAGE_DATA_DIRECTORY
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0001D02A File Offset: 0x0001B22A
		internal void Read(BinaryReader br)
		{
			this.VirtualAddress = br.ReadUInt32();
			this.Size = br.ReadUInt32();
		}

		// Token: 0x04000377 RID: 887
		public uint VirtualAddress;

		// Token: 0x04000378 RID: 888
		public uint Size;
	}
}
