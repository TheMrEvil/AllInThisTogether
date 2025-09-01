using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	[NativeHeader("Modules/Physics/BatchCommands/CapsulecastCommand.h")]
	public struct CapsulecastCommand
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x000063C0 File Offset: 0x000045C0
		public CapsulecastCommand(Vector3 p1, Vector3 p2, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.point1 = p1;
			this.point2 = p2;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
			this.physicsScene = Physics.defaultPhysicsScene;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00006418 File Offset: 0x00004618
		public CapsulecastCommand(PhysicsScene physicsScene, Vector3 p1, Vector3 p2, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.point1 = p1;
			this.point2 = p2;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
			this.physicsScene = physicsScene;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000646A File Offset: 0x0000466A
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00006472 File Offset: 0x00004672
		public Vector3 point1
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<point1>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<point1>k__BackingField = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000647B File Offset: 0x0000467B
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00006483 File Offset: 0x00004683
		public Vector3 point2
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<point2>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<point2>k__BackingField = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000648C File Offset: 0x0000468C
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00006494 File Offset: 0x00004694
		public float radius
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<radius>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<radius>k__BackingField = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000649D File Offset: 0x0000469D
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x000064A5 File Offset: 0x000046A5
		public Vector3 direction
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<direction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<direction>k__BackingField = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000064AE File Offset: 0x000046AE
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x000064B6 File Offset: 0x000046B6
		public float distance
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<distance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<distance>k__BackingField = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000064BF File Offset: 0x000046BF
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x000064C7 File Offset: 0x000046C7
		public int layerMask
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<layerMask>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<layerMask>k__BackingField = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x000064D0 File Offset: 0x000046D0
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x000064D8 File Offset: 0x000046D8
		internal int maxHits
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<maxHits>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<maxHits>k__BackingField = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x000064E1 File Offset: 0x000046E1
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x000064E9 File Offset: 0x000046E9
		public PhysicsScene physicsScene
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<physicsScene>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<physicsScene>k__BackingField = value;
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000064F4 File Offset: 0x000046F4
		public static JobHandle ScheduleBatch(NativeArray<CapsulecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<CapsulecastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<CapsulecastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<CapsulecastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<CapsulecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return CapsulecastCommand.ScheduleCapsulecastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<CapsulecastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00006548 File Offset: 0x00004748
		[FreeFunction("ScheduleCapsulecastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleCapsulecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle result2;
			CapsulecastCommand.ScheduleCapsulecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out result2);
			return result2;
		}

		// Token: 0x0600046C RID: 1132
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleCapsulecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);

		// Token: 0x040000CF RID: 207
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector3 <point1>k__BackingField;

		// Token: 0x040000D0 RID: 208
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector3 <point2>k__BackingField;

		// Token: 0x040000D1 RID: 209
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <radius>k__BackingField;

		// Token: 0x040000D2 RID: 210
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector3 <direction>k__BackingField;

		// Token: 0x040000D3 RID: 211
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <distance>k__BackingField;

		// Token: 0x040000D4 RID: 212
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <layerMask>k__BackingField;

		// Token: 0x040000D5 RID: 213
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <maxHits>k__BackingField;

		// Token: 0x040000D6 RID: 214
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PhysicsScene <physicsScene>k__BackingField;
	}
}
