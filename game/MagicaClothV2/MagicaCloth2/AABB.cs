using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public struct AABB : IEquatable<AABB>
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0001E7BB File Offset: 0x0001C9BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public AABB(in float3 min, in float3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001E7D5 File Offset: 0x0001C9D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AABB CreateFromCenterAndExtents(float3 center, float3 extents)
		{
			return AABB.CreateFromCenterAndHalfExtents(center, extents * 0.5f);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001E7E8 File Offset: 0x0001C9E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AABB CreateFromCenterAndHalfExtents(float3 center, float3 halfExtents)
		{
			float3 @float = center - halfExtents;
			float3 float2 = center + halfExtents;
			return new AABB(ref @float, ref float2);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0001E80E File Offset: 0x0001CA0E
		public float3 Extents
		{
			get
			{
				return this.Max - this.Min;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0001E821 File Offset: 0x0001CA21
		public float3 HalfExtents
		{
			get
			{
				return (this.Max - this.Min) * 0.5f;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001E83E File Offset: 0x0001CA3E
		public float3 Center
		{
			get
			{
				return (this.Max + this.Min) * 0.5f;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0001E85C File Offset: 0x0001CA5C
		public float MaxSideLength
		{
			get
			{
				float3 extents = this.Extents;
				return math.max(math.max(extents.x, extents.y), extents.z);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0001E88C File Offset: 0x0001CA8C
		public bool IsValid
		{
			get
			{
				return math.all(this.Min <= this.Max);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		public float SurfaceArea
		{
			get
			{
				float3 x = this.Max - this.Min;
				return 2f * math.dot(x, x.yzx);
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001E8D6 File Offset: 0x0001CAD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(in float3 point)
		{
			return math.all(point >= this.Min & point <= this.Max);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001E904 File Offset: 0x0001CB04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(in AABB aabb)
		{
			return math.all(this.Min <= aabb.Min & this.Max >= aabb.Max);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001E932 File Offset: 0x0001CB32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(in AABB aabb)
		{
			return math.all(this.Max >= aabb.Min & this.Min <= aabb.Max);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001E960 File Offset: 0x0001CB60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Expand(float signedDistance)
		{
			this.Min -= signedDistance;
			this.Max += signedDistance;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001E986 File Offset: 0x0001CB86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(in AABB aabb)
		{
			this.Min = math.min(this.Min, aabb.Min);
			this.Max = math.max(this.Max, aabb.Max);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001E9B6 File Offset: 0x0001CBB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(in float3 point)
		{
			this.Min = math.min(this.Min, point);
			this.Max = math.max(this.Max, point);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001E9E6 File Offset: 0x0001CBE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(AABB other)
		{
			return this.Min.Equals(other.Min) && this.Max.Equals(other.Max);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001EA10 File Offset: 0x0001CC10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Transform(in float4x4 toM)
		{
			float3 x = math.transform(toM, this.Min);
			float3 y = math.transform(toM, this.Max);
			this.Min = math.min(x, y);
			this.Max = math.max(x, y);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001EA5C File Offset: 0x0001CC5C
		public override string ToString()
		{
			return string.Format("AABB Center:{0}, HalfExtents:{1}, Min:{2}, Max:{3}", new object[]
			{
				this.Center,
				this.HalfExtents,
				this.Min,
				this.Max
			});
		}

		// Token: 0x0400062F RID: 1583
		public float3 Min;

		// Token: 0x04000630 RID: 1584
		public float3 Max;
	}
}
