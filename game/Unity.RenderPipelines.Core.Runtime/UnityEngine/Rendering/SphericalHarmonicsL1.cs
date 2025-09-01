using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public struct SphericalHarmonicsL1
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00011678 File Offset: 0x0000F878
		public static SphericalHarmonicsL1 operator +(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr + rhs.shAr,
				shAg = lhs.shAg + rhs.shAg,
				shAb = lhs.shAb + rhs.shAb
			};
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000116D8 File Offset: 0x0000F8D8
		public static SphericalHarmonicsL1 operator -(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr - rhs.shAr,
				shAg = lhs.shAg - rhs.shAg,
				shAb = lhs.shAb - rhs.shAb
			};
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00011738 File Offset: 0x0000F938
		public static SphericalHarmonicsL1 operator *(SphericalHarmonicsL1 lhs, float rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr * rhs,
				shAg = lhs.shAg * rhs,
				shAb = lhs.shAb * rhs
			};
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00011788 File Offset: 0x0000F988
		public static SphericalHarmonicsL1 operator /(SphericalHarmonicsL1 lhs, float rhs)
		{
			return new SphericalHarmonicsL1
			{
				shAr = lhs.shAr / rhs,
				shAg = lhs.shAg / rhs,
				shAb = lhs.shAb / rhs
			};
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000117D7 File Offset: 0x0000F9D7
		public static bool operator ==(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return lhs.shAr == rhs.shAr && lhs.shAg == rhs.shAg && lhs.shAb == rhs.shAb;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011812 File Offset: 0x0000FA12
		public static bool operator !=(SphericalHarmonicsL1 lhs, SphericalHarmonicsL1 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001181E File Offset: 0x0000FA1E
		public override bool Equals(object other)
		{
			return other is SphericalHarmonicsL1 && this == (SphericalHarmonicsL1)other;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001183C File Offset: 0x0000FA3C
		public override int GetHashCode()
		{
			return ((391 + this.shAr.GetHashCode()) * 23 + this.shAg.GetHashCode()) * 23 + this.shAb.GetHashCode();
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001188C File Offset: 0x0000FA8C
		// Note: this type is marked as 'beforefieldinit'.
		static SphericalHarmonicsL1()
		{
		}

		// Token: 0x04000264 RID: 612
		public Vector4 shAr;

		// Token: 0x04000265 RID: 613
		public Vector4 shAg;

		// Token: 0x04000266 RID: 614
		public Vector4 shAb;

		// Token: 0x04000267 RID: 615
		public static readonly SphericalHarmonicsL1 zero = new SphericalHarmonicsL1
		{
			shAr = Vector4.zero,
			shAg = Vector4.zero,
			shAb = Vector4.zero
		};
	}
}
