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
	// Token: 0x0200003A RID: 58
	[NativeHeader("Modules/Physics/BatchCommands/SpherecastCommand.h")]
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	public struct SpherecastCommand
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x00006248 File Offset: 0x00004448
		public SpherecastCommand(Vector3 origin, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.origin = origin;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
			this.physicsScene = Physics.defaultPhysicsScene;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00006294 File Offset: 0x00004494
		public SpherecastCommand(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.origin = origin;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = 1;
			this.physicsScene = physicsScene;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x000062D2 File Offset: 0x000044D2
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x000062DA File Offset: 0x000044DA
		public Vector3 origin
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<origin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<origin>k__BackingField = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x000062E3 File Offset: 0x000044E3
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x000062EB File Offset: 0x000044EB
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

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x000062F4 File Offset: 0x000044F4
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x000062FC File Offset: 0x000044FC
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

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00006305 File Offset: 0x00004505
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000630D File Offset: 0x0000450D
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

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00006316 File Offset: 0x00004516
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000631E File Offset: 0x0000451E
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

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00006327 File Offset: 0x00004527
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000632F File Offset: 0x0000452F
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00006338 File Offset: 0x00004538
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00006340 File Offset: 0x00004540
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

		// Token: 0x06000455 RID: 1109 RVA: 0x0000634C File Offset: 0x0000454C
		public static JobHandle ScheduleBatch(NativeArray<SpherecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<SpherecastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<SpherecastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<SpherecastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<SpherecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return SpherecastCommand.ScheduleSpherecastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<SpherecastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000063A0 File Offset: 0x000045A0
		[FreeFunction("ScheduleSpherecastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleSpherecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle result2;
			SpherecastCommand.ScheduleSpherecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out result2);
			return result2;
		}

		// Token: 0x06000457 RID: 1111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleSpherecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);

		// Token: 0x040000C8 RID: 200
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector3 <origin>k__BackingField;

		// Token: 0x040000C9 RID: 201
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <radius>k__BackingField;

		// Token: 0x040000CA RID: 202
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector3 <direction>k__BackingField;

		// Token: 0x040000CB RID: 203
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <distance>k__BackingField;

		// Token: 0x040000CC RID: 204
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <layerMask>k__BackingField;

		// Token: 0x040000CD RID: 205
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <maxHits>k__BackingField;

		// Token: 0x040000CE RID: 206
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private PhysicsScene <physicsScene>k__BackingField;
	}
}
