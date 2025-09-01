using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A0 RID: 160
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray128 : IBitArray
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00017F9E File Offset: 0x0001619E
		public uint capacity
		{
			get
			{
				return 128U;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00017FA5 File Offset: 0x000161A5
		public bool allFalse
		{
			get
			{
				return this.data1 == 0UL && this.data2 == 0UL;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00017FBB File Offset: 0x000161BB
		public bool allTrue
		{
			get
			{
				return this.data1 == ulong.MaxValue && this.data2 == ulong.MaxValue;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00017FD4 File Offset: 0x000161D4
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data2, 2)).Replace(' ', '0'), ".{8}", "$0.") + Regex.Replace(string.Format("{0, " + 64U.ToString() + "}", Convert.ToString((long)this.data1, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x170000B4 RID: 180
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get128(index, this.data1, this.data2);
			}
			set
			{
				BitArrayUtilities.Set128(index, ref this.data1, ref this.data2, value);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001809A File Offset: 0x0001629A
		public BitArray128(ulong initValue1, ulong initValue2)
		{
			this.data1 = initValue1;
			this.data2 = initValue2;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000180AC File Offset: 0x000162AC
		public BitArray128(IEnumerable<uint> bitIndexTrue)
		{
			this.data1 = (this.data2 = 0UL);
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < 64U)
				{
					this.data1 |= 1UL << (int)num;
				}
				else if (num < this.capacity)
				{
					this.data2 |= 1UL << (int)(num - 64U);
				}
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00018124 File Offset: 0x00016324
		public static BitArray128 operator ~(BitArray128 a)
		{
			return new BitArray128(~a.data1, ~a.data2);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00018139 File Offset: 0x00016339
		public static BitArray128 operator |(BitArray128 a, BitArray128 b)
		{
			return new BitArray128(a.data1 | b.data1, a.data2 | b.data2);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001815A File Offset: 0x0001635A
		public static BitArray128 operator &(BitArray128 a, BitArray128 b)
		{
			return new BitArray128(a.data1 & b.data1, a.data2 & b.data2);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001817B File Offset: 0x0001637B
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray128)other;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00018193 File Offset: 0x00016393
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray128)other;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000181AB File Offset: 0x000163AB
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000181BD File Offset: 0x000163BD
		public static bool operator ==(BitArray128 a, BitArray128 b)
		{
			return a.data1 == b.data1 && a.data2 == b.data2;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000181DD File Offset: 0x000163DD
		public static bool operator !=(BitArray128 a, BitArray128 b)
		{
			return a.data1 != b.data1 || a.data2 != b.data2;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00018200 File Offset: 0x00016400
		public override bool Equals(object obj)
		{
			return obj is BitArray128 && this.data1.Equals(((BitArray128)obj).data1) && this.data2.Equals(((BitArray128)obj).data2);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001823A File Offset: 0x0001643A
		public override int GetHashCode()
		{
			return (1755735569 * -1521134295 + this.data1.GetHashCode()) * -1521134295 + this.data2.GetHashCode();
		}

		// Token: 0x0400034A RID: 842
		[SerializeField]
		private ulong data1;

		// Token: 0x0400034B RID: 843
		[SerializeField]
		private ulong data2;
	}
}
