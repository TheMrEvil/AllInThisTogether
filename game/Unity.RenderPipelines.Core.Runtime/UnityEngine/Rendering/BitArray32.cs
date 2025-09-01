using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009E RID: 158
	[DebuggerDisplay("{this.GetType().Name} {humanizedData}")]
	[Serializable]
	public struct BitArray32 : IBitArray
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00017C19 File Offset: 0x00015E19
		public uint capacity
		{
			get
			{
				return 32U;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00017C1D File Offset: 0x00015E1D
		public bool allFalse
		{
			get
			{
				return this.data == 0U;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00017C28 File Offset: 0x00015E28
		public bool allTrue
		{
			get
			{
				return this.data == uint.MaxValue;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00017C33 File Offset: 0x00015E33
		private string humanizedVersion
		{
			get
			{
				return Convert.ToString((long)((ulong)this.data), 2);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00017C44 File Offset: 0x00015E44
		public string humanizedData
		{
			get
			{
				return Regex.Replace(string.Format("{0, " + this.capacity.ToString() + "}", Convert.ToString((long)((ulong)this.data), 2)).Replace(' ', '0'), ".{8}", "$0.").TrimEnd('.');
			}
		}

		// Token: 0x170000AA RID: 170
		public bool this[uint index]
		{
			get
			{
				return BitArrayUtilities.Get32(index, this.data);
			}
			set
			{
				BitArrayUtilities.Set32(index, ref this.data, value);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00017CBC File Offset: 0x00015EBC
		public BitArray32(uint initValue)
		{
			this.data = initValue;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00017CC8 File Offset: 0x00015EC8
		public BitArray32(IEnumerable<uint> bitIndexTrue)
		{
			this.data = 0U;
			if (bitIndexTrue == null)
			{
				return;
			}
			for (int i = bitIndexTrue.Count<uint>() - 1; i >= 0; i--)
			{
				uint num = bitIndexTrue.ElementAt(i);
				if (num < this.capacity)
				{
					this.data |= 1U << (int)num;
				}
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00017D17 File Offset: 0x00015F17
		public IBitArray BitAnd(IBitArray other)
		{
			return this & (BitArray32)other;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00017D2F File Offset: 0x00015F2F
		public IBitArray BitOr(IBitArray other)
		{
			return this | (BitArray32)other;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00017D47 File Offset: 0x00015F47
		public IBitArray BitNot()
		{
			return ~this;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00017D59 File Offset: 0x00015F59
		public static BitArray32 operator ~(BitArray32 a)
		{
			return new BitArray32(~a.data);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017D67 File Offset: 0x00015F67
		public static BitArray32 operator |(BitArray32 a, BitArray32 b)
		{
			return new BitArray32(a.data | b.data);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00017D7B File Offset: 0x00015F7B
		public static BitArray32 operator &(BitArray32 a, BitArray32 b)
		{
			return new BitArray32(a.data & b.data);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00017D8F File Offset: 0x00015F8F
		public static bool operator ==(BitArray32 a, BitArray32 b)
		{
			return a.data == b.data;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00017D9F File Offset: 0x00015F9F
		public static bool operator !=(BitArray32 a, BitArray32 b)
		{
			return a.data != b.data;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00017DB2 File Offset: 0x00015FB2
		public override bool Equals(object obj)
		{
			return obj is BitArray32 && ((BitArray32)obj).data == this.data;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017DD1 File Offset: 0x00015FD1
		public override int GetHashCode()
		{
			return 1768953197 + this.data.GetHashCode();
		}

		// Token: 0x04000348 RID: 840
		[SerializeField]
		private uint data;
	}
}
