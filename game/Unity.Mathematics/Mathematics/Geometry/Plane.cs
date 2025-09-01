using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics.Geometry
{
	// Token: 0x0200004C RID: 76
	[DebuggerDisplay("{Normal}, {Distance}")]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	internal struct Plane
	{
		// Token: 0x06002456 RID: 9302 RVA: 0x000672AC File Offset: 0x000654AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(float coefficientA, float coefficientB, float coefficientC, float coefficientD)
		{
			this.NormalAndDistance = Plane.Normalize(new float4(coefficientA, coefficientB, coefficientC, coefficientD));
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000672C3 File Offset: 0x000654C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(float3 normal, float distance)
		{
			this.NormalAndDistance = Plane.Normalize(new float4(normal, distance));
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000672D7 File Offset: 0x000654D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(float3 normal, float3 pointInPlane)
		{
			this = new Plane(normal, -math.dot(normal, pointInPlane));
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000672E8 File Offset: 0x000654E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Plane(float3 vector1InPlane, float3 vector2InPlane, float3 pointInPlane)
		{
			this = new Plane(math.cross(vector1InPlane, vector2InPlane), pointInPlane);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000672F8 File Offset: 0x000654F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane CreateFromUnitNormalAndDistance(float3 unitNormal, float distance)
		{
			return new Plane
			{
				NormalAndDistance = new float4(unitNormal, distance)
			};
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0006731C File Offset: 0x0006551C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane CreateFromUnitNormalAndPointInPlane(float3 unitNormal, float3 pointInPlane)
		{
			return new Plane
			{
				NormalAndDistance = new float4(unitNormal, -math.dot(unitNormal, pointInPlane))
			};
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x00067347 File Offset: 0x00065547
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x00067354 File Offset: 0x00065554
		public float3 Normal
		{
			get
			{
				return this.NormalAndDistance.xyz;
			}
			set
			{
				this.NormalAndDistance.xyz = value;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x00067362 File Offset: 0x00065562
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x0006736F File Offset: 0x0006556F
		public float Distance
		{
			get
			{
				return this.NormalAndDistance.w;
			}
			set
			{
				this.NormalAndDistance.w = value;
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00067380 File Offset: 0x00065580
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Normalize(Plane plane)
		{
			return new Plane
			{
				NormalAndDistance = Plane.Normalize(plane.NormalAndDistance)
			};
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000673A8 File Offset: 0x000655A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 Normalize(float4 planeCoefficients)
		{
			float rhs = math.rsqrt(math.lengthsq(planeCoefficients.xyz));
			return new Plane
			{
				NormalAndDistance = planeCoefficients * rhs
			};
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000673E3 File Offset: 0x000655E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float SignedDistanceToPoint(float3 point)
		{
			return math.dot(this.NormalAndDistance, new float4(point, 1f));
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000673FB File Offset: 0x000655FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 Projection(float3 point)
		{
			return point - this.Normal * this.SignedDistanceToPoint(point);
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x00067418 File Offset: 0x00065618
		public Plane Flipped
		{
			get
			{
				return new Plane
				{
					NormalAndDistance = -this.NormalAndDistance
				};
			}
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00067440 File Offset: 0x00065640
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(Plane plane)
		{
			return plane.NormalAndDistance;
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x00067448 File Offset: 0x00065648
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckPlaneIsNormalized()
		{
			float num = math.lengthsq(this.Normal.xyz);
			if (num < 0.99800104f || num > 1.002001f)
			{
				throw new ArgumentException("Plane must be normalized. Call Plane.Normalize() to normalize plane.");
			}
		}

		// Token: 0x0400011D RID: 285
		public float4 NormalAndDistance;
	}
}
