using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000417 RID: 1047
	public struct SortingSettings : IEquatable<SortingSettings>
	{
		// Token: 0x06002410 RID: 9232 RVA: 0x0003D040 File Offset: 0x0003B240
		public SortingSettings(Camera camera)
		{
			ScriptableRenderContext.InitializeSortSettings(camera, out this);
			this.m_Criteria = this.criteria;
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x0003D058 File Offset: 0x0003B258
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x0003D070 File Offset: 0x0003B270
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				return this.m_WorldToCameraMatrix;
			}
			set
			{
				this.m_WorldToCameraMatrix = value;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x0003D07C File Offset: 0x0003B27C
		// (set) Token: 0x06002414 RID: 9236 RVA: 0x0003D094 File Offset: 0x0003B294
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

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x0003D0A0 File Offset: 0x0003B2A0
		// (set) Token: 0x06002416 RID: 9238 RVA: 0x0003D0B8 File Offset: 0x0003B2B8
		public Vector3 customAxis
		{
			get
			{
				return this.m_CustomAxis;
			}
			set
			{
				this.m_CustomAxis = value;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x0003D0C4 File Offset: 0x0003B2C4
		// (set) Token: 0x06002418 RID: 9240 RVA: 0x0003D0DC File Offset: 0x0003B2DC
		public SortingCriteria criteria
		{
			get
			{
				return this.m_Criteria;
			}
			set
			{
				this.m_Criteria = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x0003D0E8 File Offset: 0x0003B2E8
		// (set) Token: 0x0600241A RID: 9242 RVA: 0x0003D100 File Offset: 0x0003B300
		public DistanceMetric distanceMetric
		{
			get
			{
				return this.m_DistanceMetric;
			}
			set
			{
				this.m_DistanceMetric = value;
			}
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x0003D10C File Offset: 0x0003B30C
		public bool Equals(SortingSettings other)
		{
			return this.m_WorldToCameraMatrix.Equals(other.m_WorldToCameraMatrix) && this.m_CameraPosition.Equals(other.m_CameraPosition) && this.m_CustomAxis.Equals(other.m_CustomAxis) && this.m_Criteria == other.m_Criteria && this.m_DistanceMetric == other.m_DistanceMetric && this.m_PreviousVPMatrix.Equals(other.m_PreviousVPMatrix) && this.m_NonJitteredVPMatrix.Equals(other.m_NonJitteredVPMatrix);
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x0003D19C File Offset: 0x0003B39C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is SortingSettings && this.Equals((SortingSettings)obj);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
		public override int GetHashCode()
		{
			int num = this.m_WorldToCameraMatrix.GetHashCode();
			num = (num * 397 ^ this.m_CameraPosition.GetHashCode());
			num = (num * 397 ^ this.m_CustomAxis.GetHashCode());
			num = (num * 397 ^ (int)this.m_Criteria);
			num = (num * 397 ^ (int)this.m_DistanceMetric);
			num = (num * 397 ^ this.m_PreviousVPMatrix.GetHashCode());
			return num * 397 ^ this.m_NonJitteredVPMatrix.GetHashCode();
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x0003D280 File Offset: 0x0003B480
		public static bool operator ==(SortingSettings left, SortingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x0003D29C File Offset: 0x0003B49C
		public static bool operator !=(SortingSettings left, SortingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000D59 RID: 3417
		private Matrix4x4 m_WorldToCameraMatrix;

		// Token: 0x04000D5A RID: 3418
		private Vector3 m_CameraPosition;

		// Token: 0x04000D5B RID: 3419
		private Vector3 m_CustomAxis;

		// Token: 0x04000D5C RID: 3420
		private SortingCriteria m_Criteria;

		// Token: 0x04000D5D RID: 3421
		private DistanceMetric m_DistanceMetric;

		// Token: 0x04000D5E RID: 3422
		private Matrix4x4 m_PreviousVPMatrix;

		// Token: 0x04000D5F RID: 3423
		private Matrix4x4 m_NonJitteredVPMatrix;
	}
}
