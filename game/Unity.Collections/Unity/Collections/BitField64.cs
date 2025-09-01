using System;
using System.Diagnostics;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000038 RID: 56
	[DebuggerTypeProxy(typeof(BitField64DebugView))]
	[BurstCompatible]
	public struct BitField64
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00004066 File Offset: 0x00002266
		public BitField64(ulong initialValue = 0UL)
		{
			this.Value = initialValue;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000406F File Offset: 0x0000226F
		public void Clear()
		{
			this.Value = 0UL;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004079 File Offset: 0x00002279
		public void SetBits(int pos, bool value)
		{
			this.Value = Bitwise.SetBits(this.Value, pos, 1UL, value);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004090 File Offset: 0x00002290
		public void SetBits(int pos, bool value, int numBits = 1)
		{
			ulong mask = ulong.MaxValue >> 64 - numBits;
			this.Value = Bitwise.SetBits(this.Value, pos, mask, value);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000040BC File Offset: 0x000022BC
		public ulong GetBits(int pos, int numBits = 1)
		{
			ulong mask = ulong.MaxValue >> 64 - numBits;
			return Bitwise.ExtractBits(this.Value, pos, mask);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000040E1 File Offset: 0x000022E1
		public bool IsSet(int pos)
		{
			return this.GetBits(pos, 1) > 0UL;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000040EF File Offset: 0x000022EF
		public bool TestNone(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) == 0UL;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000040FD File Offset: 0x000022FD
		public bool TestAny(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) > 0UL;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000410C File Offset: 0x0000230C
		public bool TestAll(int pos, int numBits = 1)
		{
			ulong num = ulong.MaxValue >> 64 - numBits;
			return num == Bitwise.ExtractBits(this.Value, pos, num);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004134 File Offset: 0x00002334
		public int CountBits()
		{
			return math.countbits(this.Value);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004141 File Offset: 0x00002341
		public int CountLeadingZeros()
		{
			return math.lzcnt(this.Value);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000414E File Offset: 0x0000234E
		public int CountTrailingZeros()
		{
			return math.tzcnt(this.Value);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000415B File Offset: 0x0000235B
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckArgs(int pos, int numBits)
		{
			if (pos > 63 || numBits == 0 || numBits > 64 || pos + numBits > 64)
			{
				throw new ArgumentException(string.Format("BitField32 invalid arguments: pos {0} (must be 0-63), numBits {1} (must be 1-64).", pos, numBits));
			}
		}

		// Token: 0x04000070 RID: 112
		public ulong Value;
	}
}
