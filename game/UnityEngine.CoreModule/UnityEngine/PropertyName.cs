using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E4 RID: 484
	[UsedByNativeCode]
	public struct PropertyName : IEquatable<PropertyName>
	{
		// Token: 0x060015F1 RID: 5617 RVA: 0x0002340E File Offset: 0x0002160E
		public PropertyName(string name)
		{
			this = new PropertyName(PropertyNameUtils.PropertyNameFromString(name));
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0002341E File Offset: 0x0002161E
		public PropertyName(PropertyName other)
		{
			this.id = other.id;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0002342D File Offset: 0x0002162D
		public PropertyName(int id)
		{
			this.id = id;
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00023438 File Offset: 0x00021638
		public static bool IsNullOrEmpty(PropertyName prop)
		{
			return prop.id == 0;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00023454 File Offset: 0x00021654
		public static bool operator ==(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00023474 File Offset: 0x00021674
		public static bool operator !=(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00023498 File Offset: 0x00021698
		public override int GetHashCode()
		{
			return this.id;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x000234B0 File Offset: 0x000216B0
		public override bool Equals(object other)
		{
			return other is PropertyName && this.Equals((PropertyName)other);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000234DC File Offset: 0x000216DC
		public bool Equals(PropertyName other)
		{
			return this == other;
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000234FC File Offset: 0x000216FC
		public static implicit operator PropertyName(string name)
		{
			return new PropertyName(name);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00023514 File Offset: 0x00021714
		public static implicit operator PropertyName(int id)
		{
			return new PropertyName(id);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0002352C File Offset: 0x0002172C
		public override string ToString()
		{
			return string.Format("Unknown:{0}", this.id);
		}

		// Token: 0x040007C3 RID: 1987
		internal int id;
	}
}
