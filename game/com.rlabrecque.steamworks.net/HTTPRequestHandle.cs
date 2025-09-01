using System;

namespace Steamworks
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	public struct HTTPRequestHandle : IEquatable<HTTPRequestHandle>, IComparable<HTTPRequestHandle>
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x0000EDAF File Offset: 0x0000CFAF
		public HTTPRequestHandle(uint value)
		{
			this.m_HTTPRequestHandle = value;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		public override string ToString()
		{
			return this.m_HTTPRequestHandle.ToString();
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0000EDC5 File Offset: 0x0000CFC5
		public override bool Equals(object other)
		{
			return other is HTTPRequestHandle && this == (HTTPRequestHandle)other;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000EDE2 File Offset: 0x0000CFE2
		public override int GetHashCode()
		{
			return this.m_HTTPRequestHandle.GetHashCode();
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0000EDEF File Offset: 0x0000CFEF
		public static bool operator ==(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return x.m_HTTPRequestHandle == y.m_HTTPRequestHandle;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0000EDFF File Offset: 0x0000CFFF
		public static bool operator !=(HTTPRequestHandle x, HTTPRequestHandle y)
		{
			return !(x == y);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000EE0B File Offset: 0x0000D00B
		public static explicit operator HTTPRequestHandle(uint value)
		{
			return new HTTPRequestHandle(value);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0000EE13 File Offset: 0x0000D013
		public static explicit operator uint(HTTPRequestHandle that)
		{
			return that.m_HTTPRequestHandle;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0000EE1B File Offset: 0x0000D01B
		public bool Equals(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle == other.m_HTTPRequestHandle;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0000EE2B File Offset: 0x0000D02B
		public int CompareTo(HTTPRequestHandle other)
		{
			return this.m_HTTPRequestHandle.CompareTo(other.m_HTTPRequestHandle);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0000EE3E File Offset: 0x0000D03E
		// Note: this type is marked as 'beforefieldinit'.
		static HTTPRequestHandle()
		{
		}

		// Token: 0x04000AB0 RID: 2736
		public static readonly HTTPRequestHandle Invalid = new HTTPRequestHandle(0U);

		// Token: 0x04000AB1 RID: 2737
		public uint m_HTTPRequestHandle;
	}
}
