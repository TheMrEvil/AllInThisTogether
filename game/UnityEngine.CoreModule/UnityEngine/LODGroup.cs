using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200019A RID: 410
	[NativeHeader("Runtime/Graphics/LOD/LODGroupManager.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODGroup.h")]
	[StaticAccessor("GetLODGroupManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/LOD/LODUtility.h")]
	public class LODGroup : Component
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00012EF4 File Offset: 0x000110F4
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x00012F0A File Offset: 0x0001110A
		public Vector3 localReferencePoint
		{
			get
			{
				Vector3 result;
				this.get_localReferencePoint_Injected(out result);
				return result;
			}
			set
			{
				this.set_localReferencePoint_Injected(ref value);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000EFC RID: 3836
		// (set) Token: 0x06000EFD RID: 3837
		public extern float size { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000EFE RID: 3838
		public extern int lodCount { [NativeMethod("GetLODCount")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000EFF RID: 3839
		// (set) Token: 0x06000F00 RID: 3840
		public extern LODFadeMode fadeMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F01 RID: 3841
		// (set) Token: 0x06000F02 RID: 3842
		public extern bool animateCrossFading { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F03 RID: 3843
		// (set) Token: 0x06000F04 RID: 3844
		public extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000F05 RID: 3845
		[FreeFunction("UpdateLODGroupBoundingBox", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateBounds();

		// Token: 0x06000F06 RID: 3846
		[FreeFunction("GetLODs_Binding", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern LOD[] GetLODs();

		// Token: 0x06000F07 RID: 3847 RVA: 0x00012F14 File Offset: 0x00011114
		[Obsolete("Use SetLODs instead.")]
		public void SetLODS(LOD[] lods)
		{
			this.SetLODs(lods);
		}

		// Token: 0x06000F08 RID: 3848
		[FreeFunction("SetLODs_Binding", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLODs([Unmarshalled] LOD[] lods);

		// Token: 0x06000F09 RID: 3849
		[FreeFunction("ForceLODLevel", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ForceLOD(int index);

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F0A RID: 3850
		// (set) Token: 0x06000F0B RID: 3851
		[StaticAccessor("GetLODGroupManager()")]
		public static extern float crossFadeAnimationDuration { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00012F20 File Offset: 0x00011120
		internal Vector3 worldReferencePoint
		{
			get
			{
				Vector3 result;
				this.get_worldReferencePoint_Injected(out result);
				return result;
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00010727 File Offset: 0x0000E927
		public LODGroup()
		{
		}

		// Token: 0x06000F0E RID: 3854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_localReferencePoint_Injected(out Vector3 ret);

		// Token: 0x06000F0F RID: 3855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_localReferencePoint_Injected(ref Vector3 value);

		// Token: 0x06000F10 RID: 3856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_worldReferencePoint_Injected(out Vector3 ret);
	}
}
