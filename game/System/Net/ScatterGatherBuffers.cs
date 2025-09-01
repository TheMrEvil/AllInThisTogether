using System;

namespace System.Net
{
	// Token: 0x02000638 RID: 1592
	internal class ScatterGatherBuffers
	{
		// Token: 0x06003239 RID: 12857 RVA: 0x000ADB6C File Offset: 0x000ABD6C
		internal ScatterGatherBuffers()
		{
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000ADB7F File Offset: 0x000ABD7F
		internal ScatterGatherBuffers(long totalSize)
		{
			if (totalSize > 0L)
			{
				this.currentChunk = this.AllocateMemoryChunk((totalSize > 2147483647L) ? int.MaxValue : ((int)totalSize));
			}
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000ADBB8 File Offset: 0x000ABDB8
		internal BufferOffsetSize[] GetBuffers()
		{
			if (this.Empty)
			{
				return null;
			}
			BufferOffsetSize[] array = new BufferOffsetSize[this.chunkCount];
			int num = 0;
			for (ScatterGatherBuffers.MemoryChunk next = this.headChunk; next != null; next = next.Next)
			{
				array[num] = new BufferOffsetSize(next.Buffer, 0, next.FreeOffset, false);
				num++;
			}
			return array;
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x000ADC0B File Offset: 0x000ABE0B
		private bool Empty
		{
			get
			{
				return this.headChunk == null || this.chunkCount == 0;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000ADC20 File Offset: 0x000ABE20
		internal int Length
		{
			get
			{
				return this.totalLength;
			}
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000ADC28 File Offset: 0x000ABE28
		internal void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int num = this.Empty ? 0 : (this.currentChunk.Buffer.Length - this.currentChunk.FreeOffset);
				if (num == 0)
				{
					ScatterGatherBuffers.MemoryChunk next = this.AllocateMemoryChunk(count);
					if (this.currentChunk != null)
					{
						this.currentChunk.Next = next;
					}
					this.currentChunk = next;
				}
				int num2 = (count < num) ? count : num;
				Buffer.BlockCopy(buffer, offset, this.currentChunk.Buffer, this.currentChunk.FreeOffset, num2);
				offset += num2;
				count -= num2;
				this.totalLength += num2;
				this.currentChunk.FreeOffset += num2;
			}
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000ADCE0 File Offset: 0x000ABEE0
		private ScatterGatherBuffers.MemoryChunk AllocateMemoryChunk(int newSize)
		{
			if (newSize > this.nextChunkLength)
			{
				this.nextChunkLength = newSize;
			}
			ScatterGatherBuffers.MemoryChunk result = new ScatterGatherBuffers.MemoryChunk(this.nextChunkLength);
			if (this.Empty)
			{
				this.headChunk = result;
			}
			this.nextChunkLength *= 2;
			this.chunkCount++;
			return result;
		}

		// Token: 0x04001D5B RID: 7515
		private ScatterGatherBuffers.MemoryChunk headChunk;

		// Token: 0x04001D5C RID: 7516
		private ScatterGatherBuffers.MemoryChunk currentChunk;

		// Token: 0x04001D5D RID: 7517
		private int nextChunkLength = 1024;

		// Token: 0x04001D5E RID: 7518
		private int totalLength;

		// Token: 0x04001D5F RID: 7519
		private int chunkCount;

		// Token: 0x02000639 RID: 1593
		private class MemoryChunk
		{
			// Token: 0x06003240 RID: 12864 RVA: 0x000ADD35 File Offset: 0x000ABF35
			internal MemoryChunk(int bufferSize)
			{
				this.Buffer = new byte[bufferSize];
			}

			// Token: 0x04001D60 RID: 7520
			internal byte[] Buffer;

			// Token: 0x04001D61 RID: 7521
			internal int FreeOffset;

			// Token: 0x04001D62 RID: 7522
			internal ScatterGatherBuffers.MemoryChunk Next;
		}
	}
}
