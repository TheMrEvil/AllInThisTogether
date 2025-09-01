using System;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200002D RID: 45
	public struct ComputeBufferDesc
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000A5C5 File Offset: 0x000087C5
		public ComputeBufferDesc(int count, int stride)
		{
			this = default(ComputeBufferDesc);
			this.count = count;
			this.stride = stride;
			this.type = ComputeBufferType.Default;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A5E3 File Offset: 0x000087E3
		public ComputeBufferDesc(int count, int stride, ComputeBufferType type)
		{
			this = default(ComputeBufferDesc);
			this.count = count;
			this.stride = stride;
			this.type = type;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A601 File Offset: 0x00008801
		public override int GetHashCode()
		{
			return (int)(((17 * 23 + this.count) * 23 + this.stride) * 23 + this.type);
		}

		// Token: 0x04000130 RID: 304
		public int count;

		// Token: 0x04000131 RID: 305
		public int stride;

		// Token: 0x04000132 RID: 306
		public ComputeBufferType type;

		// Token: 0x04000133 RID: 307
		public string name;
	}
}
