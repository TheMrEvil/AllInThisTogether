using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200002B RID: 43
	public struct Normal : IEquatable<Normal>
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000155EB File Offset: 0x000137EB
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000155F3 File Offset: 0x000137F3
		public Vector3 normal
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<normal>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<normal>k__BackingField = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000155FC File Offset: 0x000137FC
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00015604 File Offset: 0x00013804
		public Vector4 tangent
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<tangent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<tangent>k__BackingField = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0001560D File Offset: 0x0001380D
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00015615 File Offset: 0x00013815
		public Vector3 bitangent
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<bitangent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<bitangent>k__BackingField = value;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0001561E File Offset: 0x0001381E
		public override bool Equals(object obj)
		{
			return obj is Normal && this.Equals((Normal)obj);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00015636 File Offset: 0x00013836
		public override int GetHashCode()
		{
			return (VectorHash.GetHashCode(this.normal) * 397 ^ VectorHash.GetHashCode(this.tangent)) * 397 ^ VectorHash.GetHashCode(this.bitangent);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00015668 File Offset: 0x00013868
		public bool Equals(Normal other)
		{
			return this.normal.Approx3(other.normal, 0.0001f) && this.tangent.Approx3(other.tangent, 0.0001f) && this.bitangent.Approx3(other.bitangent, 0.0001f);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000156CA File Offset: 0x000138CA
		public static bool operator ==(Normal a, Normal b)
		{
			return a.Equals(b);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000156D4 File Offset: 0x000138D4
		public static bool operator !=(Normal a, Normal b)
		{
			return !(a == b);
		}

		// Token: 0x0400008F RID: 143
		[CompilerGenerated]
		private Vector3 <normal>k__BackingField;

		// Token: 0x04000090 RID: 144
		[CompilerGenerated]
		private Vector4 <tangent>k__BackingField;

		// Token: 0x04000091 RID: 145
		[CompilerGenerated]
		private Vector3 <bitangent>k__BackingField;
	}
}
