using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000412 RID: 1042
	[UsedByNativeCode]
	public struct ShadowSplitData : IEquatable<ShadowSplitData>
	{
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x0003CBD4 File Offset: 0x0003ADD4
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x0003CBEC File Offset: 0x0003ADEC
		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
			set
			{
				bool flag = value < 0 || value > 10;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to ShadowSplitData.maximumCullingPlaneCount ({1}), but was {2}.", 0, 10, value));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x0003CC34 File Offset: 0x0003AE34
		// (set) Token: 0x060023FA RID: 9210 RVA: 0x0003CC4C File Offset: 0x0003AE4C
		public Vector4 cullingSphere
		{
			get
			{
				return this.m_CullingSphere;
			}
			set
			{
				this.m_CullingSphere = value;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x0003CC58 File Offset: 0x0003AE58
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x0003CC70 File Offset: 0x0003AE70
		public float shadowCascadeBlendCullingFactor
		{
			get
			{
				return this.m_ShadowCascadeBlendCullingFactor;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentException(string.Format("Value should range from {0} to {1}, but was {2}.", 0, 1, value));
				}
				this.m_ShadowCascadeBlendCullingFactor = value;
			}
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x0003CCC0 File Offset: 0x0003AEC0
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x0003CD3C File Offset: 0x0003AF3C
		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentException("index", string.Format("Index should be at least {0} and less than cullingPlaneCount ({1}), but was {2}.", 0, this.cullingPlaneCount, index));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x0003CDB4 File Offset: 0x0003AFB4
		public bool Equals(ShadowSplitData other)
		{
			bool flag = this.m_CullingPlaneCount != other.m_CullingPlaneCount;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < this.cullingPlaneCount; i++)
				{
					bool flag2 = !this.GetCullingPlane(i).Equals(other.GetCullingPlane(i));
					if (flag2)
					{
						return false;
					}
				}
				result = this.m_CullingSphere.Equals(other.m_CullingSphere);
			}
			return result;
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0003CE3C File Offset: 0x0003B03C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowSplitData && this.Equals((ShadowSplitData)obj);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x0003CE74 File Offset: 0x0003B074
		public override int GetHashCode()
		{
			return this.m_CullingPlaneCount * 397 ^ this.m_CullingSphere.GetHashCode();
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x0003CEA8 File Offset: 0x0003B0A8
		public static bool operator ==(ShadowSplitData left, ShadowSplitData right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0003CEC4 File Offset: 0x0003B0C4
		public static bool operator !=(ShadowSplitData left, ShadowSplitData right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x0003CEE1 File Offset: 0x0003B0E1
		// Note: this type is marked as 'beforefieldinit'.
		static ShadowSplitData()
		{
		}

		// Token: 0x04000D40 RID: 3392
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x04000D41 RID: 3393
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x04000D42 RID: 3394
		private int m_CullingPlaneCount;

		// Token: 0x04000D43 RID: 3395
		[FixedBuffer(typeof(byte), 160)]
		internal ShadowSplitData.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x04000D44 RID: 3396
		private Vector4 m_CullingSphere;

		// Token: 0x04000D45 RID: 3397
		private float m_ShadowCascadeBlendCullingFactor;

		// Token: 0x04000D46 RID: 3398
		private float m_CullingNearPlane;

		// Token: 0x02000413 RID: 1043
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x04000D47 RID: 3399
			public byte FixedElementField;
		}
	}
}
