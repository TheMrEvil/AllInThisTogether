using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000194 RID: 404
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Graphics/Mesh/SkinnedMeshRenderer.h")]
	public class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000ECE RID: 3790
		// (set) Token: 0x06000ECF RID: 3791
		public extern SkinQuality quality { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000ED0 RID: 3792
		// (set) Token: 0x06000ED1 RID: 3793
		public extern bool updateWhenOffscreen { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000ED2 RID: 3794
		// (set) Token: 0x06000ED3 RID: 3795
		public extern bool forceMatrixRecalculationPerRender { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000ED4 RID: 3796
		// (set) Token: 0x06000ED5 RID: 3797
		public extern Transform rootBone { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000ED6 RID: 3798
		// (set) Token: 0x06000ED7 RID: 3799
		public extern Transform[] bones { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000ED8 RID: 3800
		// (set) Token: 0x06000ED9 RID: 3801
		[NativeProperty("Mesh")]
		public extern Mesh sharedMesh { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000EDA RID: 3802
		// (set) Token: 0x06000EDB RID: 3803
		[NativeProperty("SkinnedMeshMotionVectors")]
		public extern bool skinnedMotionVectors { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000EDC RID: 3804
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetBlendShapeWeight(int index);

		// Token: 0x06000EDD RID: 3805
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBlendShapeWeight(int index, float value);

		// Token: 0x06000EDE RID: 3806 RVA: 0x00012D5F File Offset: 0x00010F5F
		public void BakeMesh(Mesh mesh)
		{
			this.BakeMesh(mesh, false);
		}

		// Token: 0x06000EDF RID: 3807
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeMesh([NotNull("NullExceptionObject")] Mesh mesh, bool useScale);

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00012D6C File Offset: 0x00010F6C
		public GraphicsBuffer GetVertexBuffer()
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return this.GetVertexBufferImpl();
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00012D98 File Offset: 0x00010F98
		public GraphicsBuffer GetPreviousVertexBuffer()
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return this.GetPreviousVertexBufferImpl();
		}

		// Token: 0x06000EE2 RID: 3810
		[FreeFunction(Name = "SkinnedMeshRendererScripting::GetVertexBufferPtr", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GraphicsBuffer GetVertexBufferImpl();

		// Token: 0x06000EE3 RID: 3811
		[FreeFunction(Name = "SkinnedMeshRendererScripting::GetPreviousVertexBufferPtr", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GraphicsBuffer GetPreviousVertexBufferImpl();

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000EE4 RID: 3812
		// (set) Token: 0x06000EE5 RID: 3813
		public extern GraphicsBuffer.Target vertexBufferTarget { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0000BF29 File Offset: 0x0000A129
		public SkinnedMeshRenderer()
		{
		}
	}
}
