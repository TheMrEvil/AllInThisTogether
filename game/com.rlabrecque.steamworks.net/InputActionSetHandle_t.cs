using System;

namespace Steamworks
{
	// Token: 0x0200019F RID: 415
	[Serializable]
	public struct InputActionSetHandle_t : IEquatable<InputActionSetHandle_t>, IComparable<InputActionSetHandle_t>
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x0000EE4B File Offset: 0x0000D04B
		public InputActionSetHandle_t(ulong value)
		{
			this.m_InputActionSetHandle = value;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0000EE54 File Offset: 0x0000D054
		public override string ToString()
		{
			return this.m_InputActionSetHandle.ToString();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0000EE61 File Offset: 0x0000D061
		public override bool Equals(object other)
		{
			return other is InputActionSetHandle_t && this == (InputActionSetHandle_t)other;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0000EE7E File Offset: 0x0000D07E
		public override int GetHashCode()
		{
			return this.m_InputActionSetHandle.GetHashCode();
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0000EE8B File Offset: 0x0000D08B
		public static bool operator ==(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return x.m_InputActionSetHandle == y.m_InputActionSetHandle;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000EE9B File Offset: 0x0000D09B
		public static bool operator !=(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0000EEA7 File Offset: 0x0000D0A7
		public static explicit operator InputActionSetHandle_t(ulong value)
		{
			return new InputActionSetHandle_t(value);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0000EEAF File Offset: 0x0000D0AF
		public static explicit operator ulong(InputActionSetHandle_t that)
		{
			return that.m_InputActionSetHandle;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0000EEB7 File Offset: 0x0000D0B7
		public bool Equals(InputActionSetHandle_t other)
		{
			return this.m_InputActionSetHandle == other.m_InputActionSetHandle;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0000EEC7 File Offset: 0x0000D0C7
		public int CompareTo(InputActionSetHandle_t other)
		{
			return this.m_InputActionSetHandle.CompareTo(other.m_InputActionSetHandle);
		}

		// Token: 0x04000AB2 RID: 2738
		public ulong m_InputActionSetHandle;
	}
}
