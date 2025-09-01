using System;

namespace System.Net
{
	// Token: 0x02000620 RID: 1568
	internal class BufferOffsetSize
	{
		// Token: 0x060031C8 RID: 12744 RVA: 0x000AC7B0 File Offset: 0x000AA9B0
		internal BufferOffsetSize(byte[] buffer, int offset, int size, bool copyBuffer)
		{
			if (copyBuffer)
			{
				byte[] array = new byte[size];
				System.Buffer.BlockCopy(buffer, offset, array, 0, size);
				offset = 0;
				buffer = array;
			}
			this.Buffer = buffer;
			this.Offset = offset;
			this.Size = size;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000AC7F3 File Offset: 0x000AA9F3
		internal BufferOffsetSize(byte[] buffer, bool copyBuffer) : this(buffer, 0, buffer.Length, copyBuffer)
		{
		}

		// Token: 0x04001CDB RID: 7387
		internal byte[] Buffer;

		// Token: 0x04001CDC RID: 7388
		internal int Offset;

		// Token: 0x04001CDD RID: 7389
		internal int Size;
	}
}
