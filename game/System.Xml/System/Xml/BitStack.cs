using System;

namespace System.Xml
{
	// Token: 0x02000029 RID: 41
	internal class BitStack
	{
		// Token: 0x06000156 RID: 342 RVA: 0x0000AFAA File Offset: 0x000091AA
		public BitStack()
		{
			this.curr = 1U;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000AFB9 File Offset: 0x000091B9
		public void PushBit(bool bit)
		{
			if ((this.curr & 2147483648U) != 0U)
			{
				this.PushCurr();
			}
			this.curr = (this.curr << 1 | (bit ? 1U : 0U));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000AFE5 File Offset: 0x000091E5
		public bool PopBit()
		{
			bool result = (this.curr & 1U) > 0U;
			this.curr >>= 1;
			if (this.curr == 1U)
			{
				this.PopCurr();
			}
			return result;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000B00F File Offset: 0x0000920F
		public bool PeekBit()
		{
			return (this.curr & 1U) > 0U;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000B01C File Offset: 0x0000921C
		public bool IsEmpty
		{
			get
			{
				return this.curr == 1U;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000B028 File Offset: 0x00009228
		private void PushCurr()
		{
			if (this.bitStack == null)
			{
				this.bitStack = new uint[16];
			}
			uint[] array = this.bitStack;
			int num = this.stackPos;
			this.stackPos = num + 1;
			array[num] = this.curr;
			this.curr = 1U;
			int num2 = this.bitStack.Length;
			if (this.stackPos >= num2)
			{
				uint[] destinationArray = new uint[2 * num2];
				Array.Copy(this.bitStack, destinationArray, num2);
				this.bitStack = destinationArray;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000B0A0 File Offset: 0x000092A0
		private void PopCurr()
		{
			if (this.stackPos > 0)
			{
				uint[] array = this.bitStack;
				int num = this.stackPos - 1;
				this.stackPos = num;
				this.curr = array[num];
			}
		}

		// Token: 0x040005CB RID: 1483
		private uint[] bitStack;

		// Token: 0x040005CC RID: 1484
		private int stackPos;

		// Token: 0x040005CD RID: 1485
		private uint curr;
	}
}
