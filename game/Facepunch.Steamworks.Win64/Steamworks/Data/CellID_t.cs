using System;

namespace Steamworks.Data
{
	// Token: 0x020001BD RID: 445
	internal struct CellID_t : IEquatable<CellID_t>, IComparable<CellID_t>
	{
		// Token: 0x06000E09 RID: 3593 RVA: 0x00017DC8 File Offset: 0x00015FC8
		public static implicit operator CellID_t(uint value)
		{
			return new CellID_t
			{
				Value = value
			};
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00017DE6 File Offset: 0x00015FE6
		public static implicit operator uint(CellID_t value)
		{
			return value.Value;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00017DEE File Offset: 0x00015FEE
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00017DFB File Offset: 0x00015FFB
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00017E08 File Offset: 0x00016008
		public override bool Equals(object p)
		{
			return this.Equals((CellID_t)p);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00017E16 File Offset: 0x00016016
		public bool Equals(CellID_t p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00017E26 File Offset: 0x00016026
		public static bool operator ==(CellID_t a, CellID_t b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00017E30 File Offset: 0x00016030
		public static bool operator !=(CellID_t a, CellID_t b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00017E3D File Offset: 0x0001603D
		public int CompareTo(CellID_t other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BAC RID: 2988
		public uint Value;
	}
}
