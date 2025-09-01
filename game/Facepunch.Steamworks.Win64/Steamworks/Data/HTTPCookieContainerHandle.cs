using System;

namespace Steamworks.Data
{
	// Token: 0x020001D5 RID: 469
	internal struct HTTPCookieContainerHandle : IEquatable<HTTPCookieContainerHandle>, IComparable<HTTPCookieContainerHandle>
	{
		// Token: 0x06000EE1 RID: 3809 RVA: 0x00018AC0 File Offset: 0x00016CC0
		public static implicit operator HTTPCookieContainerHandle(uint value)
		{
			return new HTTPCookieContainerHandle
			{
				Value = value
			};
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00018ADE File Offset: 0x00016CDE
		public static implicit operator uint(HTTPCookieContainerHandle value)
		{
			return value.Value;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00018AE6 File Offset: 0x00016CE6
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00018AF3 File Offset: 0x00016CF3
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00018B00 File Offset: 0x00016D00
		public override bool Equals(object p)
		{
			return this.Equals((HTTPCookieContainerHandle)p);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00018B0E File Offset: 0x00016D0E
		public bool Equals(HTTPCookieContainerHandle p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00018B1E File Offset: 0x00016D1E
		public static bool operator ==(HTTPCookieContainerHandle a, HTTPCookieContainerHandle b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00018B28 File Offset: 0x00016D28
		public static bool operator !=(HTTPCookieContainerHandle a, HTTPCookieContainerHandle b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00018B35 File Offset: 0x00016D35
		public int CompareTo(HTTPCookieContainerHandle other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC4 RID: 3012
		public uint Value;
	}
}
