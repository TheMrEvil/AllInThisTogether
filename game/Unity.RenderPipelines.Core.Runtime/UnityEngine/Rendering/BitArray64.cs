using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009F RID: 159
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray64 : IBitArray
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00017DE4 File Offset: 0x00015FE4
		public uint capacity
		{
			get
			{
				return 64U;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00017DE8 File Offset: 0x00015FE8
		public bool allFalse
		{
			get
			{
				return this.data == 0UL;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00017DF4 File Offset: 0x00015FF4
		public bool allTrue
		{
			get
			{
				return this.data == ulong.MaxValue;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00017E00 File Offset: 0x00016000
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString((long)this.data, 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x170000AF RID: 175
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get64(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set64(index, ref this.data, value);
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00017E77 File Offset: 0x00016077
		public BitArray64(ulong initValue)
		{
			this.data = initValue;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00017E80 File Offset: 0x00016080
		public BitArray64(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0UL;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < this.capacity)
				{
					this.data |= 1UL << (int)num;
				}
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00017ED1 File Offset: 0x000160D1
		public static BitArray64 operator ~(BitArray64 a)
		{
			return new BitArray64(~a.data);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017EDF File Offset: 0x000160DF
		public static BitArray64 operator |(BitArray64 a, BitArray64 b)
		{
			return new BitArray64(a.data | b.data);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00017EF3 File Offset: 0x000160F3
		public static BitArray64 operator &(BitArray64 a, BitArray64 b)
		{
			return new BitArray64(a.data & b.data);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00017F07 File Offset: 0x00016107
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray64)other;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00017F1F File Offset: 0x0001611F
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray64)other;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00017F37 File Offset: 0x00016137
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00017F49 File Offset: 0x00016149
		public static bool operator ==(BitArray64 a, BitArray64 b)
		{
			return a.data == b.data;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00017F59 File Offset: 0x00016159
		public static bool operator !=(BitArray64 a, BitArray64 b)
		{
			return a.data != b.data;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00017F6C File Offset: 0x0001616C
		public override bool Equals(object obj)
		{
			return obj is BitArray64 && ((BitArray64)obj).data == this.data;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00017F8B File Offset: 0x0001618B
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000349 RID: 841
		[SerializeField]
		private ulong data;
	}
}
