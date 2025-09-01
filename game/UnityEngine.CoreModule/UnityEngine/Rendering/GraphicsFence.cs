using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003E1 RID: 993
	[NativeHeader("Runtime/Graphics/GPUFence.h")]
	[UsedByNativeCode]
	public struct GraphicsFence
	{
		// Token: 0x06001F9F RID: 8095 RVA: 0x00033CC8 File Offset: 0x00031EC8
		internal static SynchronisationStageFlags TranslateSynchronizationStageToFlags(SynchronisationStage s)
		{
			return (s == SynchronisationStage.VertexProcessing) ? SynchronisationStageFlags.VertexProcessing : SynchronisationStageFlags.PixelProcessing;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x00033CE4 File Offset: 0x00031EE4
		public bool passed
		{
			get
			{
				this.Validate();
				bool flag = !SystemInfo.supportsGraphicsFence;
				if (flag)
				{
					throw new NotSupportedException("Cannot determine if this GraphicsFence has passed as this platform has not implemented GraphicsFences.");
				}
				bool flag2 = this.m_FenceType == GraphicsFenceType.AsyncQueueSynchronisation && !SystemInfo.supportsAsyncCompute;
				if (flag2)
				{
					throw new NotSupportedException("Cannot determine if this AsyncQueueSynchronisation GraphicsFence has passed as this platform does not support async compute.");
				}
				bool flag3 = !this.IsFencePending();
				return flag3 || GraphicsFence.HasFencePassed_Internal(this.m_Ptr);
			}
		}

		// Token: 0x06001FA1 RID: 8097
		[FreeFunction("GPUFenceInternals::HasFencePassed_Internal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasFencePassed_Internal(IntPtr fencePtr);

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00033D54 File Offset: 0x00031F54
		internal void InitPostAllocation()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				bool supportsGraphicsFence = SystemInfo.supportsGraphicsFence;
				if (supportsGraphicsFence)
				{
					throw new NullReferenceException("The internal fence ptr is null, this should not be possible for fences that have been correctly constructed using Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
				}
				this.m_Version = this.GetPlatformNotSupportedVersion();
			}
			else
			{
				this.m_Version = GraphicsFence.GetVersionNumber(this.m_Ptr);
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00033DAC File Offset: 0x00031FAC
		internal bool IsFencePending()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			return !flag && this.m_Version == GraphicsFence.GetVersionNumber(this.m_Ptr);
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00033DEC File Offset: 0x00031FEC
		internal void Validate()
		{
			bool flag = this.m_Version == 0 || (SystemInfo.supportsGraphicsFence && this.m_Version == this.GetPlatformNotSupportedVersion());
			if (flag)
			{
				throw new InvalidOperationException("This GraphicsFence object has not been correctly constructed see Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
			}
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00033E2C File Offset: 0x0003202C
		private int GetPlatformNotSupportedVersion()
		{
			return -1;
		}

		// Token: 0x06001FA6 RID: 8102
		[NativeThrows]
		[FreeFunction("GPUFenceInternals::GetVersionNumber")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVersionNumber(IntPtr fencePtr);

		// Token: 0x04000C2E RID: 3118
		internal IntPtr m_Ptr;

		// Token: 0x04000C2F RID: 3119
		internal int m_Version;

		// Token: 0x04000C30 RID: 3120
		internal GraphicsFenceType m_FenceType;
	}
}
