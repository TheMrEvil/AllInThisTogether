using System;

namespace Steamworks.Data
{
	// Token: 0x020001CA RID: 458
	internal struct HServerQuery : IEquatable<HServerQuery>, IComparable<HServerQuery>
	{
		// Token: 0x06000E7E RID: 3710 RVA: 0x000184E8 File Offset: 0x000166E8
		public static implicit operator HServerQuery(int value)
		{
			return new HServerQuery
			{
				Value = value
			};
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00018506 File Offset: 0x00016706
		public static implicit operator int(HServerQuery value)
		{
			return value.Value;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0001850E File Offset: 0x0001670E
		public override string ToString()
		{
			return this.Value.ToString();
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0001851B File Offset: 0x0001671B
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00018528 File Offset: 0x00016728
		public override bool Equals(object p)
		{
			return this.Equals((HServerQuery)p);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00018536 File Offset: 0x00016736
		public bool Equals(HServerQuery p)
		{
			return p.Value == this.Value;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00018546 File Offset: 0x00016746
		public static bool operator ==(HServerQuery a, HServerQuery b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00018550 File Offset: 0x00016750
		public static bool operator !=(HServerQuery a, HServerQuery b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0001855D File Offset: 0x0001675D
		public int CompareTo(HServerQuery other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x04000BB9 RID: 3001
		public int Value;
	}
}
