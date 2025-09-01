using System;

namespace System.Xml.Schema
{
	// Token: 0x020004E9 RID: 1257
	internal sealed class BitSet
	{
		// Token: 0x060033B1 RID: 13233 RVA: 0x0000216B File Offset: 0x0000036B
		private BitSet()
		{
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x001262F8 File Offset: 0x001244F8
		public BitSet(int count)
		{
			this.count = count;
			this.bits = new uint[this.Subscript(count + 31)];
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x0012631C File Offset: 0x0012451C
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000921 RID: 2337
		public bool this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x00126330 File Offset: 0x00124530
		public void Clear()
		{
			int num = this.bits.Length;
			while (num-- > 0)
			{
				this.bits[num] = 0U;
			}
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x0012635C File Offset: 0x0012455C
		public void Clear(int index)
		{
			int num = this.Subscript(index);
			this.EnsureLength(num + 1);
			this.bits[num] &= ~(1U << index);
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x00126394 File Offset: 0x00124594
		public void Set(int index)
		{
			int num = this.Subscript(index);
			this.EnsureLength(num + 1);
			this.bits[num] |= 1U << index;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x001263CC File Offset: 0x001245CC
		public bool Get(int index)
		{
			bool result = false;
			if (index < this.count)
			{
				int num = this.Subscript(index);
				result = (((ulong)this.bits[num] & (ulong)(1L << (index & 31 & 31))) > 0UL);
			}
			return result;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x00126408 File Offset: 0x00124608
		public int NextSet(int startFrom)
		{
			int num = startFrom + 1;
			if (num == this.count)
			{
				return -1;
			}
			int num2 = this.Subscript(num);
			num &= 31;
			uint num3;
			for (num3 = this.bits[num2] >> num; num3 == 0U; num3 = this.bits[num2])
			{
				if (++num2 == this.bits.Length)
				{
					return -1;
				}
				num = 0;
			}
			while ((num3 & 1U) == 0U)
			{
				num3 >>= 1;
				num++;
			}
			return (num2 << 5) + num;
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x00126474 File Offset: 0x00124674
		public void And(BitSet other)
		{
			if (this == other)
			{
				return;
			}
			int num = this.bits.Length;
			int num2 = other.bits.Length;
			int i = (num > num2) ? num2 : num;
			int num3 = i;
			while (num3-- > 0)
			{
				this.bits[num3] &= other.bits[num3];
			}
			while (i < num)
			{
				this.bits[i] = 0U;
				i++;
			}
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x001264D8 File Offset: 0x001246D8
		public void Or(BitSet other)
		{
			if (this == other)
			{
				return;
			}
			int num = other.bits.Length;
			this.EnsureLength(num);
			int num2 = num;
			while (num2-- > 0)
			{
				this.bits[num2] |= other.bits[num2];
			}
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x00126520 File Offset: 0x00124720
		public override int GetHashCode()
		{
			int num = 1234;
			int num2 = this.bits.Length;
			while (--num2 >= 0)
			{
				num ^= (int)(this.bits[num2] * (uint)(num2 + 1));
			}
			return num ^ num;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x00126558 File Offset: 0x00124758
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			BitSet bitSet = (BitSet)obj;
			int num = this.bits.Length;
			int num2 = bitSet.bits.Length;
			int num3 = (num > num2) ? num2 : num;
			int num4 = num3;
			while (num4-- > 0)
			{
				if (this.bits[num4] != bitSet.bits[num4])
				{
					return false;
				}
			}
			if (num > num3)
			{
				int num5 = num;
				while (num5-- > num3)
				{
					if (this.bits[num5] != 0U)
					{
						return false;
					}
				}
			}
			else
			{
				int num6 = num2;
				while (num6-- > num3)
				{
					if (bitSet.bits[num6] != 0U)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x001265F9 File Offset: 0x001247F9
		public BitSet Clone()
		{
			return new BitSet
			{
				count = this.count,
				bits = (uint[])this.bits.Clone()
			};
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x00126624 File Offset: 0x00124824
		public bool IsEmpty
		{
			get
			{
				uint num = 0U;
				for (int i = 0; i < this.bits.Length; i++)
				{
					num |= this.bits[i];
				}
				return num == 0U;
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x00126658 File Offset: 0x00124858
		public bool Intersects(BitSet other)
		{
			int num = Math.Min(this.bits.Length, other.bits.Length);
			while (--num >= 0)
			{
				if ((this.bits[num] & other.bits[num]) != 0U)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x0012669B File Offset: 0x0012489B
		private int Subscript(int bitIndex)
		{
			return bitIndex >> 5;
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x001266A0 File Offset: 0x001248A0
		private void EnsureLength(int nRequiredLength)
		{
			if (nRequiredLength > this.bits.Length)
			{
				int num = 2 * this.bits.Length;
				if (num < nRequiredLength)
				{
					num = nRequiredLength;
				}
				uint[] destinationArray = new uint[num];
				Array.Copy(this.bits, destinationArray, this.bits.Length);
				this.bits = destinationArray;
			}
		}

		// Token: 0x0400269B RID: 9883
		private const int bitSlotShift = 5;

		// Token: 0x0400269C RID: 9884
		private const int bitSlotMask = 31;

		// Token: 0x0400269D RID: 9885
		private int count;

		// Token: 0x0400269E RID: 9886
		private uint[] bits;
	}
}
