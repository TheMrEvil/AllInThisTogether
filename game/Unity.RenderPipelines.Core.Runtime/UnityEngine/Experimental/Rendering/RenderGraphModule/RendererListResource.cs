using System;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000034 RID: 52
	internal struct RendererListResource
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0000BB4B File Offset: 0x00009D4B
		internal RendererListResource(in RendererListDesc desc)
		{
			this.desc = desc;
			this.rendererList = default(RendererList);
		}

		// Token: 0x04000147 RID: 327
		public RendererListDesc desc;

		// Token: 0x04000148 RID: 328
		public RendererList rendererList;
	}
}
