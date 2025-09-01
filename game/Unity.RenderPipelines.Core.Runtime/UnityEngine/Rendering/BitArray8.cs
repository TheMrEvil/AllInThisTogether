using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009C RID: 156
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray8 : IBitArray
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000178AC File Offset: 0x00015AAC
		public uint capacity
		{
			get
			{
				return 8U;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000178AF File Offset: 0x00015AAF
		public bool allFalse
		{
			get
			{
				return this.data == 0;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000178BA File Offset: 0x00015ABA
		public bool allTrue
		{
			get
			{
				return this.data == byte.MaxValue;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000178CC File Offset: 0x00015ACC
		public string humanizedData
		{
			get
			{
				return string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString(this.data, 2)).Replace(' ', '0');
			}
		}

		// Token: 0x1700009F RID: 159
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get8(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set8(index, ref this.data, value);
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001792D File Offset: 0x00015B2D
		public BitArray8(byte initValue)
		{
			this.data = initValue;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00017938 File Offset: 0x00015B38
		public BitArray8(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < this.capacity)
				{
					this.data |= (byte)(1 << (int)num);
				}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00017989 File Offset: 0x00015B89
		public static BitArray8 operator ~(BitArray8 a)
		{
			return new BitArray8(~a.data);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00017998 File Offset: 0x00015B98
		public static BitArray8 operator |(BitArray8 a, BitArray8 b)
		{
			return new BitArray8(a.data | b.data);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000179AD File Offset: 0x00015BAD
		public static BitArray8 operator &(BitArray8 a, BitArray8 b)
		{
			return new BitArray8(a.data & b.data);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000179C2 File Offset: 0x00015BC2
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray8)other;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000179DA File Offset: 0x00015BDA
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray8)other;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000179F2 File Offset: 0x00015BF2
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00017A04 File Offset: 0x00015C04
		public static bool operator ==(BitArray8 a, BitArray8 b)
		{
			return a.data == b.data;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00017A14 File Offset: 0x00015C14
		public static bool operator !=(BitArray8 a, BitArray8 b)
		{
			return a.data != b.data;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00017A27 File Offset: 0x00015C27
		public override bool Equals(object obj)
		{
			return obj is BitArray8 && ((BitArray8)obj).data == this.data;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00017A46 File Offset: 0x00015C46
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000346 RID: 838
		[SerializeField]
		private byte data;
	}
}
