using System;
using System.IO;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Metadata
{
	// Token: 0x020000A7 RID: 167
	internal struct RvaSize
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x0001E185 File Offset: 0x0001C385
		internal void Read(BinaryReader br)
		{
			this.VirtualAddress = br.ReadUInt32();
			this.Size = br.ReadUInt32();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001E19F File Offset: 0x0001C39F
		internal void Write(MetadataWriter mw)
		{
			mw.Write(this.VirtualAddress);
			mw.Write(this.Size);
		}

		// Token: 0x0400039D RID: 925
		internal uint VirtualAddress;

		// Token: 0x0400039E RID: 926
		internal uint Size;
	}
}
