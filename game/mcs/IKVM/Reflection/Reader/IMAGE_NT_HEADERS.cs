using System;
using System.IO;

namespace IKVM.Reflection.Reader
{
	// Token: 0x0200009E RID: 158
	internal sealed class IMAGE_NT_HEADERS
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x0001CCE0 File Offset: 0x0001AEE0
		internal void Read(BinaryReader br)
		{
			this.Signature = br.ReadUInt32();
			if (this.Signature != 17744U)
			{
				throw new BadImageFormatException();
			}
			this.FileHeader.Read(br);
			long position = br.BaseStream.Position;
			this.OptionalHeader.Read(br);
			if (br.BaseStream.Position > position + (long)((ulong)this.FileHeader.SizeOfOptionalHeader))
			{
				throw new BadImageFormatException();
			}
			br.BaseStream.Seek(position + (long)((ulong)this.FileHeader.SizeOfOptionalHeader), SeekOrigin.Begin);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		public IMAGE_NT_HEADERS()
		{
		}

		// Token: 0x04000342 RID: 834
		public const uint MAGIC_SIGNATURE = 17744U;

		// Token: 0x04000343 RID: 835
		public uint Signature;

		// Token: 0x04000344 RID: 836
		public IMAGE_FILE_HEADER FileHeader = new IMAGE_FILE_HEADER();

		// Token: 0x04000345 RID: 837
		public IMAGE_OPTIONAL_HEADER OptionalHeader = new IMAGE_OPTIONAL_HEADER();
	}
}
