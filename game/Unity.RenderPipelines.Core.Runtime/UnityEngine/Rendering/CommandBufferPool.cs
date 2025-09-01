using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000041 RID: 65
	public static class CommandBufferPool
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000CCBD File Offset: 0x0000AEBD
		public static CommandBuffer Get()
		{
			CommandBuffer commandBuffer = CommandBufferPool.s_BufferPool.Get();
			commandBuffer.name = "";
			return commandBuffer;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		public static CommandBuffer Get(string name)
		{
			CommandBuffer commandBuffer = CommandBufferPool.s_BufferPool.Get();
			commandBuffer.name = name;
			return commandBuffer;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000CCE7 File Offset: 0x0000AEE7
		public static void Release(CommandBuffer buffer)
		{
			CommandBufferPool.s_BufferPool.Release(buffer);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		// Note: this type is marked as 'beforefieldinit'.
		static CommandBufferPool()
		{
		}

		// Token: 0x040001A4 RID: 420
		private static ObjectPool<CommandBuffer> s_BufferPool = new ObjectPool<CommandBuffer>(null, delegate(CommandBuffer x)
		{
			x.Clear();
		}, true);

		// Token: 0x02000139 RID: 313
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000839 RID: 2105 RVA: 0x00022B75 File Offset: 0x00020D75
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600083A RID: 2106 RVA: 0x00022B81 File Offset: 0x00020D81
			public <>c()
			{
			}

			// Token: 0x0600083B RID: 2107 RVA: 0x00022B89 File Offset: 0x00020D89
			internal void <.cctor>b__4_0(CommandBuffer x)
			{
				x.Clear();
			}

			// Token: 0x040004FB RID: 1275
			public static readonly CommandBufferPool.<>c <>9 = new CommandBufferPool.<>c();
		}
	}
}
