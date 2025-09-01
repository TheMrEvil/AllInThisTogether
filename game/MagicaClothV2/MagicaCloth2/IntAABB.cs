using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000DD RID: 221
	[Serializable]
	public struct IntAABB : IEquatable<IntAABB>
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0001EAB1 File Offset: 0x0001CCB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntAABB(int3 min, int3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0001EAC1 File Offset: 0x0001CCC1
		public int3 Extents
		{
			get
			{
				return this.Max - this.Min;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0001EAD4 File Offset: 0x0001CCD4
		public int3 Center
		{
			get
			{
				return (this.Max + this.Min) / 2;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0001EAED File Offset: 0x0001CCED
		public bool IsValid
		{
			get
			{
				return math.all(this.Min <= this.Max);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001EB05 File Offset: 0x0001CD05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(int3 point)
		{
			return math.all(point >= this.Min & point <= this.Max);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001EB29 File Offset: 0x0001CD29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(IntAABB aabb)
		{
			return math.all(this.Min <= aabb.Min & this.Max >= aabb.Max);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001EB57 File Offset: 0x0001CD57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(IntAABB aabb)
		{
			return math.all(this.Max >= aabb.Min & this.Min <= aabb.Max);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001EB85 File Offset: 0x0001CD85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Expand(int signedDistance)
		{
			this.Min -= signedDistance;
			this.Max += signedDistance;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001EBAB File Offset: 0x0001CDAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(IntAABB aabb)
		{
			this.Min = math.min(this.Min, aabb.Min);
			this.Max = math.max(this.Max, aabb.Max);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001EBDB File Offset: 0x0001CDDB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(int3 point)
		{
			this.Min = math.min(this.Min, point);
			this.Max = math.max(this.Max, point);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001EC01 File Offset: 0x0001CE01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(IntAABB other)
		{
			return this.Min.Equals(other.Min) && this.Max.Equals(other.Max);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001EC29 File Offset: 0x0001CE29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("AABB({0}, {1})", this.Min, this.Max);
		}

		// Token: 0x04000631 RID: 1585
		public int3 Min;

		// Token: 0x04000632 RID: 1586
		public int3 Max;
	}
}
