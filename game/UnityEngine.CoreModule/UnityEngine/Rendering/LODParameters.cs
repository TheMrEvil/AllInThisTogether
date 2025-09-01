using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000401 RID: 1025
	public struct LODParameters : IEquatable<LODParameters>
	{
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x0003AAE4 File Offset: 0x00038CE4
		// (set) Token: 0x060022DA RID: 8922 RVA: 0x0003AB01 File Offset: 0x00038D01
		public bool isOrthographic
		{
			get
			{
				return Convert.ToBoolean(this.m_IsOrthographic);
			}
			set
			{
				this.m_IsOrthographic = Convert.ToInt32(value);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x0003AB10 File Offset: 0x00038D10
		// (set) Token: 0x060022DC RID: 8924 RVA: 0x0003AB28 File Offset: 0x00038D28
		public Vector3 cameraPosition
		{
			get
			{
				return this.m_CameraPosition;
			}
			set
			{
				this.m_CameraPosition = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x0003AB34 File Offset: 0x00038D34
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x0003AB4C File Offset: 0x00038D4C
		public float fieldOfView
		{
			get
			{
				return this.m_FieldOfView;
			}
			set
			{
				this.m_FieldOfView = value;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x0003AB58 File Offset: 0x00038D58
		// (set) Token: 0x060022E0 RID: 8928 RVA: 0x0003AB70 File Offset: 0x00038D70
		public float orthoSize
		{
			get
			{
				return this.m_OrthoSize;
			}
			set
			{
				this.m_OrthoSize = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x0003AB7C File Offset: 0x00038D7C
		// (set) Token: 0x060022E2 RID: 8930 RVA: 0x0003AB94 File Offset: 0x00038D94
		public int cameraPixelHeight
		{
			get
			{
				return this.m_CameraPixelHeight;
			}
			set
			{
				this.m_CameraPixelHeight = value;
			}
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0003ABA0 File Offset: 0x00038DA0
		public bool Equals(LODParameters other)
		{
			return this.m_IsOrthographic == other.m_IsOrthographic && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_FieldOfView.Equals(other.m_FieldOfView) && this.m_OrthoSize.Equals(other.m_OrthoSize) && this.m_CameraPixelHeight == other.m_CameraPixelHeight;
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0003AC0C File Offset: 0x00038E0C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LODParameters && this.Equals((LODParameters)obj);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0003AC44 File Offset: 0x00038E44
		public override int GetHashCode()
		{
			int num = this.m_IsOrthographic;
			num = (num * 397 ^ this.m_CameraPosition.GetHashCode());
			num = (num * 397 ^ this.m_FieldOfView.GetHashCode());
			num = (num * 397 ^ this.m_OrthoSize.GetHashCode());
			return num * 397 ^ this.m_CameraPixelHeight;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0003ACB0 File Offset: 0x00038EB0
		public static bool operator ==(LODParameters left, LODParameters right)
		{
			return left.Equals(right);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0003ACCC File Offset: 0x00038ECC
		public static bool operator !=(LODParameters left, LODParameters right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CEC RID: 3308
		private int m_IsOrthographic;

		// Token: 0x04000CED RID: 3309
		private Vector3 m_CameraPosition;

		// Token: 0x04000CEE RID: 3310
		private float m_FieldOfView;

		// Token: 0x04000CEF RID: 3311
		private float m_OrthoSize;

		// Token: 0x04000CF0 RID: 3312
		private int m_CameraPixelHeight;
	}
}
