using System;

namespace Steamworks
{
	// Token: 0x020001A1 RID: 417
	[Serializable]
	public struct InputDigitalActionHandle_t : IEquatable<InputDigitalActionHandle_t>, IComparable<InputDigitalActionHandle_t>
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x0000EF69 File Offset: 0x0000D169
		public InputDigitalActionHandle_t(ulong value)
		{
			this.m_InputDigitalActionHandle = value;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0000EF72 File Offset: 0x0000D172
		public override string ToString()
		{
			return this.m_InputDigitalActionHandle.ToString();
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0000EF7F File Offset: 0x0000D17F
		public override bool Equals(object other)
		{
			return other is InputDigitalActionHandle_t && this == (InputDigitalActionHandle_t)other;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0000EF9C File Offset: 0x0000D19C
		public override int GetHashCode()
		{
			return this.m_InputDigitalActionHandle.GetHashCode();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0000EFA9 File Offset: 0x0000D1A9
		public static bool operator ==(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return x.m_InputDigitalActionHandle == y.m_InputDigitalActionHandle;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0000EFB9 File Offset: 0x0000D1B9
		public static bool operator !=(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0000EFC5 File Offset: 0x0000D1C5
		public static explicit operator InputDigitalActionHandle_t(ulong value)
		{
			return new InputDigitalActionHandle_t(value);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000EFCD File Offset: 0x0000D1CD
		public static explicit operator ulong(InputDigitalActionHandle_t that)
		{
			return that.m_InputDigitalActionHandle;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0000EFD5 File Offset: 0x0000D1D5
		public bool Equals(InputDigitalActionHandle_t other)
		{
			return this.m_InputDigitalActionHandle == other.m_InputDigitalActionHandle;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0000EFE5 File Offset: 0x0000D1E5
		public int CompareTo(InputDigitalActionHandle_t other)
		{
			return this.m_InputDigitalActionHandle.CompareTo(other.m_InputDigitalActionHandle);
		}

		// Token: 0x04000AB4 RID: 2740
		public ulong m_InputDigitalActionHandle;
	}
}
