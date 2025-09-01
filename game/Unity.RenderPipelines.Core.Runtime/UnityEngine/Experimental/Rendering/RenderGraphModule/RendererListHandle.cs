using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000033 RID: 51
	[DebuggerDisplay("RendererList ({handle})")]
	public struct RendererListHandle
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000BAFC File Offset: 0x00009CFC
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000BB04 File Offset: 0x00009D04
		internal int handle
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<handle>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<handle>k__BackingField = value;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000BB0D File Offset: 0x00009D0D
		internal RendererListHandle(int handle)
		{
			this.handle = handle;
			this.m_IsValid = true;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000BB1D File Offset: 0x00009D1D
		public static implicit operator int(RendererListHandle handle)
		{
			return handle.handle;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000BB26 File Offset: 0x00009D26
		public static implicit operator RendererList(RendererListHandle rendererList)
		{
			if (!rendererList.IsValid())
			{
				return RendererList.nullRendererList;
			}
			return RenderGraphResourceRegistry.current.GetRendererList(rendererList);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000BB43 File Offset: 0x00009D43
		public bool IsValid()
		{
			return this.m_IsValid;
		}

		// Token: 0x04000145 RID: 325
		private bool m_IsValid;

		// Token: 0x04000146 RID: 326
		[CompilerGenerated]
		private int <handle>k__BackingField;
	}
}
