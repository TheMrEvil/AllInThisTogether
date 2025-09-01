using System;

namespace Steamworks
{
	// Token: 0x020001A2 RID: 418
	[Serializable]
	public struct InputHandle_t : IEquatable<InputHandle_t>, IComparable<InputHandle_t>
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		public InputHandle_t(ulong value)
		{
			this.m_InputHandle = value;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0000F001 File Offset: 0x0000D201
		public override string ToString()
		{
			return this.m_InputHandle.ToString();
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0000F00E File Offset: 0x0000D20E
		public override bool Equals(object other)
		{
			return other is InputHandle_t && this == (InputHandle_t)other;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0000F02B File Offset: 0x0000D22B
		public override int GetHashCode()
		{
			return this.m_InputHandle.GetHashCode();
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0000F038 File Offset: 0x0000D238
		public static bool operator ==(InputHandle_t x, InputHandle_t y)
		{
			return x.m_InputHandle == y.m_InputHandle;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000F048 File Offset: 0x0000D248
		public static bool operator !=(InputHandle_t x, InputHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0000F054 File Offset: 0x0000D254
		public static explicit operator InputHandle_t(ulong value)
		{
			return new InputHandle_t(value);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0000F05C File Offset: 0x0000D25C
		public static explicit operator ulong(InputHandle_t that)
		{
			return that.m_InputHandle;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0000F064 File Offset: 0x0000D264
		public bool Equals(InputHandle_t other)
		{
			return this.m_InputHandle == other.m_InputHandle;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0000F074 File Offset: 0x0000D274
		public int CompareTo(InputHandle_t other)
		{
			return this.m_InputHandle.CompareTo(other.m_InputHandle);
		}

		// Token: 0x04000AB5 RID: 2741
		public ulong m_InputHandle;
	}
}
