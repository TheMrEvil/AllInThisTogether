using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000155 RID: 341
	internal class BitFlagsGenerator
	{
		// Token: 0x06001239 RID: 4665 RVA: 0x00046DD4 File Offset: 0x00044FD4
		public BitFlagsGenerator(int bitCount)
		{
			this.bitCount = bitCount;
			int num = (bitCount + 7) / 8;
			this.locals = new byte[num];
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00046E00 File Offset: 0x00045000
		public void Store(int bitIndex, bool value)
		{
			if (value)
			{
				byte[] array = this.locals;
				int byteIndex = BitFlagsGenerator.GetByteIndex(bitIndex);
				array[byteIndex] |= BitFlagsGenerator.GetBitValue(bitIndex);
				return;
			}
			byte[] array2 = this.locals;
			int byteIndex2 = BitFlagsGenerator.GetByteIndex(bitIndex);
			array2[byteIndex2] &= ~BitFlagsGenerator.GetBitValue(bitIndex);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00046E40 File Offset: 0x00045040
		public bool Load(int bitIndex)
		{
			byte b = this.locals[BitFlagsGenerator.GetByteIndex(bitIndex)];
			byte bitValue = BitFlagsGenerator.GetBitValue(bitIndex);
			return (b & bitValue) == bitValue;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00046E66 File Offset: 0x00045066
		public byte[] LoadArray()
		{
			return (byte[])this.locals.Clone();
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00046E78 File Offset: 0x00045078
		public int GetLocalCount()
		{
			return this.locals.Length;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00046E82 File Offset: 0x00045082
		public int GetBitCount()
		{
			return this.bitCount;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00046E8A File Offset: 0x0004508A
		public byte GetLocal(int i)
		{
			return this.locals[i];
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00046E94 File Offset: 0x00045094
		public static bool IsBitSet(byte[] bytes, int bitIndex)
		{
			int byteIndex = BitFlagsGenerator.GetByteIndex(bitIndex);
			byte bitValue = BitFlagsGenerator.GetBitValue(bitIndex);
			return (bytes[byteIndex] & bitValue) == bitValue;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00046EB8 File Offset: 0x000450B8
		public static void SetBit(byte[] bytes, int bitIndex)
		{
			int byteIndex = BitFlagsGenerator.GetByteIndex(bitIndex);
			byte bitValue = BitFlagsGenerator.GetBitValue(bitIndex);
			int num = byteIndex;
			bytes[num] |= bitValue;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00046EE0 File Offset: 0x000450E0
		private static int GetByteIndex(int bitIndex)
		{
			return bitIndex >> 3;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00046EE5 File Offset: 0x000450E5
		private static byte GetBitValue(int bitIndex)
		{
			return (byte)(1 << (bitIndex & 7));
		}

		// Token: 0x04000742 RID: 1858
		private int bitCount;

		// Token: 0x04000743 RID: 1859
		private byte[] locals;
	}
}
