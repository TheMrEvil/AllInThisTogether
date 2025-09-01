using System;

namespace Steamworks
{
	// Token: 0x0200019D RID: 413
	[Serializable]
	public struct HTTPCookieContainerHandle : IEquatable<HTTPCookieContainerHandle>, IComparable<HTTPCookieContainerHandle>
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x0000ED13 File Offset: 0x0000CF13
		public HTTPCookieContainerHandle(uint value)
		{
			this.m_HTTPCookieContainerHandle = value;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public override string ToString()
		{
			return this.m_HTTPCookieContainerHandle.ToString();
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0000ED29 File Offset: 0x0000CF29
		public override bool Equals(object other)
		{
			return other is HTTPCookieContainerHandle && this == (HTTPCookieContainerHandle)other;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0000ED46 File Offset: 0x0000CF46
		public override int GetHashCode()
		{
			return this.m_HTTPCookieContainerHandle.GetHashCode();
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000ED53 File Offset: 0x0000CF53
		public static bool operator ==(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return x.m_HTTPCookieContainerHandle == y.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0000ED63 File Offset: 0x0000CF63
		public static bool operator !=(HTTPCookieContainerHandle x, HTTPCookieContainerHandle y)
		{
			return !(x == y);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0000ED6F File Offset: 0x0000CF6F
		public static explicit operator HTTPCookieContainerHandle(uint value)
		{
			return new HTTPCookieContainerHandle(value);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0000ED77 File Offset: 0x0000CF77
		public static explicit operator uint(HTTPCookieContainerHandle that)
		{
			return that.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0000ED7F File Offset: 0x0000CF7F
		public bool Equals(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle == other.m_HTTPCookieContainerHandle;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0000ED8F File Offset: 0x0000CF8F
		public int CompareTo(HTTPCookieContainerHandle other)
		{
			return this.m_HTTPCookieContainerHandle.CompareTo(other.m_HTTPCookieContainerHandle);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0000EDA2 File Offset: 0x0000CFA2
		// Note: this type is marked as 'beforefieldinit'.
		static HTTPCookieContainerHandle()
		{
		}

		// Token: 0x04000AAE RID: 2734
		public static readonly HTTPCookieContainerHandle Invalid = new HTTPCookieContainerHandle(0U);

		// Token: 0x04000AAF RID: 2735
		public uint m_HTTPCookieContainerHandle;
	}
}
