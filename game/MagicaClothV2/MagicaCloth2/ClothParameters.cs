using System;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x02000007 RID: 7
	public struct ClothParameters
	{
		// Token: 0x0400000A RID: 10
		public int solverFrequency;

		// Token: 0x0400000B RID: 11
		public float gravity;

		// Token: 0x0400000C RID: 12
		public float3 gravityDirection;

		// Token: 0x0400000D RID: 13
		public float gravityFalloff;

		// Token: 0x0400000E RID: 14
		public float stablizationTimeAfterReset;

		// Token: 0x0400000F RID: 15
		public float blendWeight;

		// Token: 0x04000010 RID: 16
		public float4x4 dampingCurveData;

		// Token: 0x04000011 RID: 17
		public float4x4 radiusCurveData;

		// Token: 0x04000012 RID: 18
		public ClothNormalAxis normalAxis;

		// Token: 0x04000013 RID: 19
		public float rotationalInterpolation;

		// Token: 0x04000014 RID: 20
		public float rootRotation;

		// Token: 0x04000015 RID: 21
		public InertiaConstraint.InertiaConstraintParams inertiaConstraint;

		// Token: 0x04000016 RID: 22
		public TetherConstraint.TetherConstraintParams tetherConstraint;

		// Token: 0x04000017 RID: 23
		public DistanceConstraint.DistanceConstraintParams distanceConstraint;

		// Token: 0x04000018 RID: 24
		public TriangleBendingConstraint.TriangleBendingConstraintParams triangleBendingConstraint;

		// Token: 0x04000019 RID: 25
		public AngleConstraint.AngleConstraintParams angleConstraint;

		// Token: 0x0400001A RID: 26
		public MotionConstraint.MotionConstraintParams motionConstraint;

		// Token: 0x0400001B RID: 27
		public ColliderCollisionConstraint.ColliderCollisionConstraintParams colliderCollisionConstraint;

		// Token: 0x0400001C RID: 28
		public SelfCollisionConstraint.SelfCollisionConstraintParams selfCollisionConstraint;
	}
}
