using System;

namespace Steamworks.Data
{
	// Token: 0x020001B4 RID: 436
	internal struct GID_t : IEquatable<GID_t>, IComparable<GID_t>
	{
		// Token: 0x06000DB8 RID: 3512 RVA: 0x00017900 File Offset: 0x00015B00
		public static implicit operator GID_t(ulong value)
		{
			return new GID_t
			{
				Value = value
			};
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0001791E File Offset: 0x00015B1E
		public static implicit operator ulong(GID_t value)
		{
			return value.Value;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00017926 File Offset: 0x00015B26
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00017933 File Offset: 0x00015B33
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00017940 File Offset: 0x00015B40
		public override bool Equals(object p)
		{
			return this.Equals((GID_t)p);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0001794E File Offset: 0x00015B4E
		public bool Equals(GID_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0001795E File Offset: 0x00015B5E
		public static bool operator ==(GID_t a, GID_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00017968 File Offset: 0x00015B68
		public static bool operator !=(GID_t a, GID_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00017975 File Offset: 0x00015B75
		public int CompareTo(GID_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BA3 RID: 2979
		public ulong Value;
	}
}
