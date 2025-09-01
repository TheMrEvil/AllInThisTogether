using System;
using System.Diagnostics;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x02000036 RID: 54
	[DebuggerTypeProxy(typeof(BitField32DebugView))]
	[BurstCompatible]
	public struct BitField32
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00003F04 File Offset: 0x00002104
		public BitField32(uint initialValue = 0U)
		{
			this.Value = initialValue;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003F0D File Offset: 0x0000210D
		public void Clear()
		{
			this.Value = 0U;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003F16 File Offset: 0x00002116
		public void SetBits(int pos, bool value)
		{
			this.Value = Bitwise.SetBits(this.Value, pos, 1U, value);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003F2C File Offset: 0x0000212C
		public void SetBits(int pos, bool value, int numBits)
		{
			uint mask = uint.MaxValue >> 32 - numBits;
			this.Value = Bitwise.SetBits(this.Value, pos, mask, value);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003F58 File Offset: 0x00002158
		public uint GetBits(int pos, int numBits = 1)
		{
			uint mask = uint.MaxValue >> 32 - numBits;
			return Bitwise.ExtractBits(this.Value, pos, mask);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003F7C File Offset: 0x0000217C
		public bool IsSet(int pos)
		{
			return this.GetBits(pos, 1) > 0U;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003F89 File Offset: 0x00002189
		public bool TestNone(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) == 0U;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003F96 File Offset: 0x00002196
		public bool TestAny(int pos, int numBits = 1)
		{
			return this.GetBits(pos, numBits) > 0U;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003FA4 File Offset: 0x000021A4
		public bool TestAll(int pos, int numBits = 1)
		{
			uint num = uint.MaxValue >> 32 - numBits;
			return num == Bitwise.ExtractBits(this.Value, pos, num);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003FCB File Offset: 0x000021CB
		public int CountBits()
		{
			return math.countbits(this.Value);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003FD8 File Offset: 0x000021D8
		public int CountLeadingZeros()
		{
			return math.lzcnt(this.Value);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003FE5 File Offset: 0x000021E5
		public int CountTrailingZeros()
		{
			return math.tzcnt(this.Value);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003FF2 File Offset: 0x000021F2
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckArgs(int pos, int numBits)
		{
			if (pos > 31 || numBits == 0 || numBits > 32 || pos + numBits > 32)
			{
				throw new ArgumentException(string.Format("BitField32 invalid arguments: pos {0} (must be 0-31), numBits {1} (must be 1-32).", pos, numBits));
			}
		}

		// Token: 0x0400006E RID: 110
		public uint Value;
	}
}
