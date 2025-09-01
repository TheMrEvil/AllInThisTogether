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
	// Token: 0x02000039 RID: 57
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	[NativeHeader("Modules/Physics/BatchCommands/RaycastCommand.h")]
	public struct RaycastCommand
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x000060FF File Offset: 0x000042FF
		public RaycastCommand(Vector3 from, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5, int maxHits = 1)
		{
			this.from = from;
			this.direction = direction;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = maxHits;
			this.physicsScene = Physics.defaultPhysicsScene;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00006138 File Offset: 0x00004338
		public RaycastCommand(PhysicsScene physicsScene, Vector3 from, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5, int maxHits = 1)
		{
			this.from = from;
			this.direction = direction;
			this.distance = distance;
			this.layerMask = layerMask;
			this.maxHits = maxHits;
			this.physicsScene = physicsScene;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000616E File Offset: 0x0000436E
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x00006176 File Offset: 0x00004376
		public Vector3 from
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<from>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<from>k__BackingField = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000617F File Offset: 0x0000437F
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x00006187 File Offset: 0x00004387
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

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00006190 File Offset: 0x00004390
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x00006198 File Offset: 0x00004398
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

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x000061A1 File Offset: 0x000043A1
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x000061A9 File Offset: 0x000043A9
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

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x000061B2 File Offset: 0x000043B2
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x000061BA File Offset: 0x000043BA
		public int maxHits
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000061C3 File Offset: 0x000043C3
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x000061CB File Offset: 0x000043CB
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

		// Token: 0x06000442 RID: 1090 RVA: 0x000061D4 File Offset: 0x000043D4
		public static JobHandle ScheduleBatch(NativeArray<RaycastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<RaycastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<RaycastCommand, RaycastHit>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<RaycastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<RaycastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return RaycastCommand.ScheduleRaycastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00006228 File Offset: 0x00004428
		[FreeFunction("ScheduleRaycastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleRaycastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle result2;
			RaycastCommand.ScheduleRaycastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out result2);
			return result2;
		}

		// Token: 0x06000444 RID: 1092
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleRaycastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);

		// Token: 0x040000C2 RID: 194
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector3 <from>k__BackingField;

		// Token: 0x040000C3 RID: 195
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector3 <direction>k__BackingField;

		// Token: 0x040000C4 RID: 196
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private float <distance>k__BackingField;

		// Token: 0x040000C5 RID: 197
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <layerMask>k__BackingField;

		// Token: 0x040000C6 RID: 198
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <maxHits>k__BackingField;

		// Token: 0x040000C7 RID: 199
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private PhysicsScene <physicsScene>k__BackingField;
	}
}
