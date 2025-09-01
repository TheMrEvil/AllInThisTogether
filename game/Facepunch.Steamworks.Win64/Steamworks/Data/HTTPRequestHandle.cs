using System;

namespace Steamworks.Data
{
	// Token: 0x020001D4 RID: 468
	internal struct HTTPRequestHandle : IEquatable<HTTPRequestHandle>, IComparable<HTTPRequestHandle>
	{
		// Token: 0x06000ED8 RID: 3800 RVA: 0x00018A38 File Offset: 0x00016C38
		public static implicit operator HTTPRequestHandle(uint value)
		{
			return new HTTPRequestHandle
			{
				Value = value
			};
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00018A56 File Offset: 0x00016C56
		public static implicit operator uint(HTTPRequestHandle value)
		{
			return value.Value;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00018A5E File Offset: 0x00016C5E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00018A6B File Offset: 0x00016C6B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00018A78 File Offset: 0x00016C78
		public override bool Equals(object p)
		{
			return this.Equals((HTTPRequestHandle)p);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00018A86 File Offset: 0x00016C86
		public bool Equals(HTTPRequestHandle p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00018A96 File Offset: 0x00016C96
		public static bool operator ==(HTTPRequestHandle a, HTTPRequestHandle b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00018AA0 File Offset: 0x00016CA0
		public static bool operator !=(HTTPRequestHandle a, HTTPRequestHandle b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00018AAD File Offset: 0x00016CAD
		public int CompareTo(HTTPRequestHandle other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BC3 RID: 3011
		public uint Value;
	}
}
