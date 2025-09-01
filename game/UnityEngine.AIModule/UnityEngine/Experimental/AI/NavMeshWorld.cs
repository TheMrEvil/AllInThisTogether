using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x02000022 RID: 34
	[StaticAccessor("NavMeshWorldBindings", StaticAccessorType.DoubleColon)]
	public struct NavMeshWorld
	{
		// Token: 0x06000172 RID: 370 RVA: 0x000031D0 File Offset: 0x000013D0
		public bool IsValid()
		{
			return this.world != IntPtr.Zero;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000031F4 File Offset: 0x000013F4
		public static NavMeshWorld GetDefaultWorld()
		{
			NavMeshWorld result;
			NavMeshWorld.GetDefaultWorld_Injected(out result);
			return result;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00003209 File Offset: 0x00001409
		private static void AddDependencyInternal(IntPtr navmesh, JobHandle handle)
		{
			NavMeshWorld.AddDependencyInternal_Injected(navmesh, ref handle);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00003213 File Offset: 0x00001413
		public void AddDependency(JobHandle job)
		{
			NavMeshWorld.AddDependencyInternal(this.world, job);
		}

		// Token: 0x06000176 RID: 374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDefaultWorld_Injected(out NavMeshWorld ret);

		// Token: 0x06000177 RID: 375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddDependencyInternal_Injected(IntPtr navmesh, ref JobHandle handle);

		// Token: 0x04000072 RID: 114
		internal IntPtr world;
	}
}
