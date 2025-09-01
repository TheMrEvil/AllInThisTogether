using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200041C RID: 1052
	[UsedByNativeCode]
	public struct VisibleLight : IEquatable<VisibleLight>
	{
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x0003DDBE File Offset: 0x0003BFBE
		public Light light
		{
			get
			{
				return (Light)Object.FindObjectFromInstanceID(this.m_InstanceId);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x0003DDD0 File Offset: 0x0003BFD0
		// (set) Token: 0x06002490 RID: 9360 RVA: 0x0003DDE8 File Offset: 0x0003BFE8
		public LightType lightType
		{
			get
			{
				return this.m_LightType;
			}
			set
			{
				this.m_LightType = value;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x0003DDF4 File Offset: 0x0003BFF4
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x0003DE0C File Offset: 0x0003C00C
		public Color finalColor
		{
			get
			{
				return this.m_FinalColor;
			}
			set
			{
				this.m_FinalColor = value;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x0003DE18 File Offset: 0x0003C018
		// (set) Token: 0x06002494 RID: 9364 RVA: 0x0003DE30 File Offset: 0x0003C030
		public Rect screenRect
		{
			get
			{
				return this.m_ScreenRect;
			}
			set
			{
				this.m_ScreenRect = value;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06002495 RID: 9365 RVA: 0x0003DE3C File Offset: 0x0003C03C
		// (set) Token: 0x06002496 RID: 9366 RVA: 0x0003DE54 File Offset: 0x0003C054
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				return this.m_LocalToWorldMatrix;
			}
			set
			{
				this.m_LocalToWorldMatrix = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x0003DE60 File Offset: 0x0003C060
		// (set) Token: 0x06002498 RID: 9368 RVA: 0x0003DE78 File Offset: 0x0003C078
		public float range
		{
			get
			{
				return this.m_Range;
			}
			set
			{
				this.m_Range = value;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x0003DE84 File Offset: 0x0003C084
		// (set) Token: 0x0600249A RID: 9370 RVA: 0x0003DE9C File Offset: 0x0003C09C
		public float spotAngle
		{
			get
			{
				return this.m_SpotAngle;
			}
			set
			{
				this.m_SpotAngle = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600249B RID: 9371 RVA: 0x0003DEA8 File Offset: 0x0003C0A8
		// (set) Token: 0x0600249C RID: 9372 RVA: 0x0003DEC8 File Offset: 0x0003C0C8
		public bool intersectsNearPlane
		{
			get
			{
				return (this.m_Flags & VisibleLightFlags.IntersectsNearPlane) > (VisibleLightFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= VisibleLightFlags.IntersectsNearPlane;
				}
				else
				{
					this.m_Flags &= ~VisibleLightFlags.IntersectsNearPlane;
				}
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600249D RID: 9373 RVA: 0x0003DEFC File Offset: 0x0003C0FC
		// (set) Token: 0x0600249E RID: 9374 RVA: 0x0003DF1C File Offset: 0x0003C11C
		public bool intersectsFarPlane
		{
			get
			{
				return (this.m_Flags & VisibleLightFlags.IntersectsFarPlane) > (VisibleLightFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= VisibleLightFlags.IntersectsFarPlane;
				}
				else
				{
					this.m_Flags &= ~VisibleLightFlags.IntersectsFarPlane;
				}
			}
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x0003DF50 File Offset: 0x0003C150
		public bool Equals(VisibleLight other)
		{
			return this.m_LightType == other.m_LightType && this.m_FinalColor.Equals(other.m_FinalColor) && this.m_ScreenRect.Equals(other.m_ScreenRect) && this.m_LocalToWorldMatrix.Equals(other.m_LocalToWorldMatrix) && this.m_Range.Equals(other.m_Range) && this.m_SpotAngle.Equals(other.m_SpotAngle) && this.m_InstanceId == other.m_InstanceId && this.m_Flags == other.m_Flags;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0003DFF0 File Offset: 0x0003C1F0
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisibleLight && this.Equals((VisibleLight)obj);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x0003E028 File Offset: 0x0003C228
		public override int GetHashCode()
		{
			int num = (int)this.m_LightType;
			num = (num * 397 ^ this.m_FinalColor.GetHashCode());
			num = (num * 397 ^ this.m_ScreenRect.GetHashCode());
			num = (num * 397 ^ this.m_LocalToWorldMatrix.GetHashCode());
			num = (num * 397 ^ this.m_Range.GetHashCode());
			num = (num * 397 ^ this.m_SpotAngle.GetHashCode());
			num = (num * 397 ^ this.m_InstanceId);
			return num * 397 ^ (int)this.m_Flags;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x0003E0D8 File Offset: 0x0003C2D8
		public static bool operator ==(VisibleLight left, VisibleLight right)
		{
			return left.Equals(right);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0003E0F4 File Offset: 0x0003C2F4
		public static bool operator !=(VisibleLight left, VisibleLight right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D90 RID: 3472
		private LightType m_LightType;

		// Token: 0x04000D91 RID: 3473
		private Color m_FinalColor;

		// Token: 0x04000D92 RID: 3474
		private Rect m_ScreenRect;

		// Token: 0x04000D93 RID: 3475
		private Matrix4x4 m_LocalToWorldMatrix;

		// Token: 0x04000D94 RID: 3476
		private float m_Range;

		// Token: 0x04000D95 RID: 3477
		private float m_SpotAngle;

		// Token: 0x04000D96 RID: 3478
		private int m_InstanceId;

		// Token: 0x04000D97 RID: 3479
		private VisibleLightFlags m_Flags;
	}
}
