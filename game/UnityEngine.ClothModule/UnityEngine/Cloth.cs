using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[RequireComponent(typeof(Transform), typeof(SkinnedMeshRenderer))]
	[NativeHeader("Modules/Cloth/Cloth.h")]
	[NativeClass("Unity::Cloth")]
	public sealed class Cloth : Component
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9
		public extern Vector3[] vertices { [NativeName("GetPositions")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10
		public extern Vector3[] normals { [NativeName("GetNormals")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		public extern ClothSkinningCoefficient[] coefficients { [NativeName("GetCoefficients")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SetCoefficients")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13
		// (set) Token: 0x0600000E RID: 14
		public extern CapsuleCollider[] capsuleColliders { [NativeName("GetCapsuleColliders")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SetCapsuleColliders")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15
		// (set) Token: 0x06000010 RID: 16
		public extern ClothSphereColliderPair[] sphereColliders { [NativeName("GetSphereColliders")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SetSphereColliders")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17
		// (set) Token: 0x06000012 RID: 18
		public extern float sleepThreshold { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19
		// (set) Token: 0x06000014 RID: 20
		public extern float bendingStiffness { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21
		// (set) Token: 0x06000016 RID: 22
		public extern float stretchingStiffness { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23
		// (set) Token: 0x06000018 RID: 24
		public extern float damping { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000020BA File Offset: 0x000002BA
		public Vector3 externalAcceleration
		{
			get
			{
				Vector3 result;
				this.get_externalAcceleration_Injected(out result);
				return result;
			}
			set
			{
				this.set_externalAcceleration_Injected(ref value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000020C4 File Offset: 0x000002C4
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000020DA File Offset: 0x000002DA
		public Vector3 randomAcceleration
		{
			get
			{
				Vector3 result;
				this.get_randomAcceleration_Injected(out result);
				return result;
			}
			set
			{
				this.set_randomAcceleration_Injected(ref value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29
		// (set) Token: 0x0600001E RID: 30
		public extern bool useGravity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31
		// (set) Token: 0x06000020 RID: 32
		public extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33
		// (set) Token: 0x06000022 RID: 34
		public extern float friction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000023 RID: 35
		// (set) Token: 0x06000024 RID: 36
		public extern float collisionMassScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37
		// (set) Token: 0x06000026 RID: 38
		public extern bool enableContinuousCollision { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000027 RID: 39
		// (set) Token: 0x06000028 RID: 40
		public extern float useVirtualParticles { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000029 RID: 41
		// (set) Token: 0x0600002A RID: 42
		public extern float worldVelocityScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43
		// (set) Token: 0x0600002C RID: 44
		public extern float worldAccelerationScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002D RID: 45
		// (set) Token: 0x0600002E RID: 46
		public extern float clothSolverFrequency { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000020E4 File Offset: 0x000002E4
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002103 File Offset: 0x00000303
		[Obsolete("Parameter solverFrequency is obsolete and no longer supported. Please use clothSolverFrequency instead.")]
		public bool solverFrequency
		{
			get
			{
				return this.clothSolverFrequency > 0f;
			}
			set
			{
				this.clothSolverFrequency = (value ? 120f : 0f);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000031 RID: 49
		// (set) Token: 0x06000032 RID: 50
		public extern bool useTethers { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000033 RID: 51
		// (set) Token: 0x06000034 RID: 52
		public extern float stiffnessFrequency { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000035 RID: 53
		// (set) Token: 0x06000036 RID: 54
		public extern float selfCollisionDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000037 RID: 55
		// (set) Token: 0x06000038 RID: 56
		public extern float selfCollisionStiffness { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearTransformMotion();

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSelfAndInterCollisionIndices([NotNull("ArgumentNullException")] List<uint> indices);

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSelfAndInterCollisionIndices([NotNull("ArgumentNullException")] List<uint> indices);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetVirtualParticleIndices([NotNull("ArgumentNullException")] List<uint> indicesOutList);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVirtualParticleIndices([NotNull("ArgumentNullException")] List<uint> indicesIn);

		// Token: 0x0600003E RID: 62
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetVirtualParticleWeights([NotNull("ArgumentNullException")] List<Vector3> weightsOutList);

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetVirtualParticleWeights([NotNull("ArgumentNullException")] List<Vector3> weights);

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000211C File Offset: 0x0000031C
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002124 File Offset: 0x00000324
		[Obsolete("useContinuousCollision is no longer supported, use enableContinuousCollision instead")]
		public float useContinuousCollision
		{
			[CompilerGenerated]
			get
			{
				return this.<useContinuousCollision>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<useContinuousCollision>k__BackingField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000212D File Offset: 0x0000032D
		[Obsolete("Deprecated.Cloth.selfCollisions is no longer supported since Unity 5.0.", true)]
		public bool selfCollision
		{
			[CompilerGenerated]
			get
			{
				return this.<selfCollision>k__BackingField;
			}
		}

		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetEnabledFading(bool enabled, float interpolationTime);

		// Token: 0x06000044 RID: 68 RVA: 0x00002135 File Offset: 0x00000335
		[ExcludeFromDocs]
		public void SetEnabledFading(bool enabled)
		{
			this.SetEnabledFading(enabled, 0.5f);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002148 File Offset: 0x00000348
		internal RaycastHit Raycast(Ray ray, float maxDistance, ref bool hasHit)
		{
			RaycastHit result;
			this.Raycast_Injected(ref ray, maxDistance, ref hasHit, out result);
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002162 File Offset: 0x00000362
		public Cloth()
		{
		}

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_externalAcceleration_Injected(out Vector3 ret);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_externalAcceleration_Injected(ref Vector3 value);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_randomAcceleration_Injected(out Vector3 ret);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_randomAcceleration_Injected(ref Vector3 value);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Raycast_Injected(ref Ray ray, float maxDistance, ref bool hasHit, out RaycastHit ret);

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <useContinuousCollision>k__BackingField;

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly bool <selfCollision>k__BackingField;
	}
}
