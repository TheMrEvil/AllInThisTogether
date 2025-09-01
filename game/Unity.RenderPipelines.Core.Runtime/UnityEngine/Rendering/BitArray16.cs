using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009D RID: 157
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray16 : IBitArray
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00017A59 File Offset: 0x00015C59
		public uint capacity
		{
			get
			{
				return 16U;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00017A5D File Offset: 0x00015C5D
		public bool allFalse
		{
			get
			{
				return this.data == 0;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00017A68 File Offset: 0x00015C68
		public bool allTrue
		{
			get
			{
				return this.data == ushort.MaxValue;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00017A78 File Offset: 0x00015C78
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString((int)this.data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x170000A4 RID: 164
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get16(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set16(index, ref this.data, value);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00017AEF File Offset: 0x00015CEF
		public BitArray16(ushort initValue)
		{
			this.data = initValue;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public BitArray16(IEnumerable<uint> bitIndexTrue)
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
					this.data |= (ushort)(1 << (int)num);
				}
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00017B49 File Offset: 0x00015D49
		public static BitArray16 operator ~(BitArray16 a)
		{
			return new BitArray16(~a.data);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00017B58 File Offset: 0x00015D58
		public static BitArray16 operator |(BitArray16 a, BitArray16 b)
		{
			return new BitArray16(a.data | b.data);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00017B6D File Offset: 0x00015D6D
		public static BitArray16 operator &(BitArray16 a, BitArray16 b)
		{
			return new BitArray16(a.data & b.data);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00017B82 File Offset: 0x00015D82
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray16)other;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00017B9A File Offset: 0x00015D9A
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray16)other;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00017BB2 File Offset: 0x00015DB2
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017BC4 File Offset: 0x00015DC4
		public static bool operator ==(BitArray16 a, BitArray16 b)
		{
			return a.data == b.data;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00017BD4 File Offset: 0x00015DD4
		public static bool operator !=(BitArray16 a, BitArray16 b)
		{
			return a.data != b.data;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00017BE7 File Offset: 0x00015DE7
		public override bool Equals(object obj)
		{
			return obj is BitArray16 && ((BitArray16)obj).data == this.data;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00017C06 File Offset: 0x00015E06
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000347 RID: 839
		[SerializeField]
		private ushort data;
	}
}
