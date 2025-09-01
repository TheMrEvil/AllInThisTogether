using System;

namespace System.Xml
{
	// Token: 0x0200002B RID: 43
	internal class ByteStack
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0000B1A1 File Offset: 0x000093A1
		public ByteStack(int growthRate)
		{
			this.growthRate = growthRate;
			this.top = 0;
			this.stack = new byte[growthRate];
			this.size = growthRate;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B1CC File Offset: 0x000093CC
		public void Push(byte data)
		{
			if (this.size == this.top)
			{
				byte[] dst = new byte[this.size + this.growthRate];
				if (this.top > 0)
				{
					Buffer.BlockCopy(this.stack, 0, dst, 0, this.top);
				}
				this.stack = dst;
				this.size += this.growthRate;
			}
			byte[] array = this.stack;
			int num = this.top;
			this.top = num + 1;
			array[num] = data;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B24C File Offset: 0x0000944C
		public byte Pop()
		{
			if (this.top > 0)
			{
				byte[] array = this.stack;
				int num = this.top - 1;
				this.top = num;
				return array[num];
			}
			return 0;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B27C File Offset: 0x0000947C
		public byte Peek()
		{
			if (this.top > 0)
			{
				return this.stack[this.top - 1];
			}
			return 0;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000B298 File Offset: 0x00009498
		public int Length
		{
			get
			{
				return this.top;
			}
		}

		// Token: 0x040005D3 RID: 1491
		private byte[] stack;

		// Token: 0x040005D4 RID: 1492
		private int growthRate;

		// Token: 0x040005D5 RID: 1493
		private int top;

		// Token: 0x040005D6 RID: 1494
		private int size;
	}
}
