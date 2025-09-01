using System;
using System.Diagnostics;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200002C RID: 44
	[DebuggerDisplay("ComputeBuffer ({handle.index})")]
	public struct ComputeBufferHandle
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000A586 File Offset: 0x00008786
		public static ComputeBufferHandle nullHandle
		{
			get
			{
				return ComputeBufferHandle.s_NullHandle;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A58D File Offset: 0x0000878D
		internal ComputeBufferHandle(int handle, bool shared = false)
		{
			this.handle = new ResourceHandle(handle, RenderGraphResourceType.ComputeBuffer, shared);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A59D File Offset: 0x0000879D
		public static implicit operator ComputeBuffer(ComputeBufferHandle buffer)
		{
			if (!buffer.IsValid())
			{
				return null;
			}
			return RenderGraphResourceRegistry.current.GetComputeBuffer(buffer);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A5B6 File Offset: 0x000087B6
		public bool IsValid()
		{
			return this.handle.IsValid();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000A5C3 File Offset: 0x000087C3
		// Note: this type is marked as 'beforefieldinit'.
		static ComputeBufferHandle()
		{
		}

		// Token: 0x0400012E RID: 302
		private static ComputeBufferHandle s_NullHandle;

		// Token: 0x0400012F RID: 303
		internal ResourceHandle handle;
	}
}
