using System;
using System.IO;
using System.Text;

namespace IKVM.Reflection.Reader
{
	// Token: 0x0200009B RID: 155
	internal sealed class StreamHeader
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x0001AC3C File Offset: 0x00018E3C
		internal void Read(BinaryReader br)
		{
			this.Offset = br.ReadUInt32();
			this.Size = br.ReadUInt32();
			byte[] array = new byte[32];
			int num = 0;
			byte b;
			while ((b = br.ReadByte()) != 0)
			{
				array[num++] = b;
			}
			this.Name = Encoding.UTF8.GetString(array, 0, num);
			int num2 = -1 + (num + 4 & -4) - num;
			br.BaseStream.Seek((long)num2, SeekOrigin.Current);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00002CCC File Offset: 0x00000ECC
		public StreamHeader()
		{
		}

		// Token: 0x04000324 RID: 804
		internal uint Offset;

		// Token: 0x04000325 RID: 805
		internal uint Size;

		// Token: 0x04000326 RID: 806
		internal string Name;
	}
}
