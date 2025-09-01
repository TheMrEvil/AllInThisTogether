using System;

namespace Steamworks
{
	// Token: 0x020001A0 RID: 416
	[Serializable]
	public struct InputAnalogActionHandle_t : IEquatable<InputAnalogActionHandle_t>, IComparable<InputAnalogActionHandle_t>
	{
		// Token: 0x060009DE RID: 2526 RVA: 0x0000EEDA File Offset: 0x0000D0DA
		public InputAnalogActionHandle_t(ulong value)
		{
			this.m_InputAnalogActionHandle = value;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0000EEE3 File Offset: 0x0000D0E3
		public override string ToString()
		{
			return this.m_InputAnalogActionHandle.ToString();
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0000EEF0 File Offset: 0x0000D0F0
		public override bool Equals(object other)
		{
			return other is InputAnalogActionHandle_t && this == (InputAnalogActionHandle_t)other;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0000EF0D File Offset: 0x0000D10D
		public override int GetHashCode()
		{
			return this.m_InputAnalogActionHandle.GetHashCode();
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0000EF1A File Offset: 0x0000D11A
		public static bool operator ==(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return x.m_InputAnalogActionHandle == y.m_InputAnalogActionHandle;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0000EF2A File Offset: 0x0000D12A
		public static bool operator !=(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0000EF36 File Offset: 0x0000D136
		public static explicit operator InputAnalogActionHandle_t(ulong value)
		{
			return new InputAnalogActionHandle_t(value);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000EF3E File Offset: 0x0000D13E
		public static explicit operator ulong(InputAnalogActionHandle_t that)
		{
			return that.m_InputAnalogActionHandle;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0000EF46 File Offset: 0x0000D146
		public bool Equals(InputAnalogActionHandle_t other)
		{
			return this.m_InputAnalogActionHandle == other.m_InputAnalogActionHandle;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0000EF56 File Offset: 0x0000D156
		public int CompareTo(InputAnalogActionHandle_t other)
		{
			return this.m_InputAnalogActionHandle.CompareTo(other.m_InputAnalogActionHandle);
		}

		// Token: 0x04000AB3 RID: 2739
		public ulong m_InputAnalogActionHandle;
	}
}
