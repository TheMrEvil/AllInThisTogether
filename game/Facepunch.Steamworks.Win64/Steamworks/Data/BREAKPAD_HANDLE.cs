using System;

namespace Steamworks.Data
{
	// Token: 0x020001C5 RID: 453
	internal struct BREAKPAD_HANDLE : IEquatable<BREAKPAD_HANDLE>, IComparable<BREAKPAD_HANDLE>
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x00018208 File Offset: 0x00016408
		public static implicit operator BREAKPAD_HANDLE(IntPtr value)
		{
			return new BREAKPAD_HANDLE
			{
				Value = value
			};
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00018226 File Offset: 0x00016426
		public static implicit operator IntPtr(BREAKPAD_HANDLE value)
		{
			return value.Value;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0001822E File Offset: 0x0001642E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0001823B File Offset: 0x0001643B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00018248 File Offset: 0x00016448
		public override bool Equals(object p)
		{
			return this.Equals((BREAKPAD_HANDLE)p);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00018256 File Offset: 0x00016456
		public bool Equals(BREAKPAD_HANDLE p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00018269 File Offset: 0x00016469
		public static bool operator ==(BREAKPAD_HANDLE a, BREAKPAD_HANDLE b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00018273 File Offset: 0x00016473
		public static bool operator !=(BREAKPAD_HANDLE a, BREAKPAD_HANDLE b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00018280 File Offset: 0x00016480
		public int CompareTo(BREAKPAD_HANDLE other)
		{
			return this.Value.ToInt64().CompareTo(other.Value.ToInt64());
		}

		// Token: 0x04000BB4 RID: 2996
		public IntPtr Value;
	}
}
