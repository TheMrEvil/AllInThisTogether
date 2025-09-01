using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000044 RID: 68
	public abstract class Monitor
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000A94F File Offset: 0x00008B4F
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000A957 File Offset: 0x00008B57
		public RenderTexture output
		{
			[CompilerGenerated]
			get
			{
				return this.<output>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<output>k__BackingField = value;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000A960 File Offset: 0x00008B60
		public bool IsRequestedAndSupported(PostProcessRenderContext context)
		{
			return this.requested && SystemInfo.supportsComputeShaders && !RuntimeUtilities.isAndroidOpenGL && !RuntimeUtilities.isWebNonWebGPU && this.ShaderResourcesAvailable(context);
		}

		// Token: 0x060000DD RID: 221
		internal abstract bool ShaderResourcesAvailable(PostProcessRenderContext context);

		// Token: 0x060000DE RID: 222 RVA: 0x0000A988 File Offset: 0x00008B88
		internal virtual bool NeedsHalfRes()
		{
			return false;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000A98C File Offset: 0x00008B8C
		protected void CheckOutput(int width, int height)
		{
			if (this.output == null || !this.output.IsCreated() || this.output.width != width || this.output.height != height)
			{
				RuntimeUtilities.Destroy(this.output);
				this.output = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32)
				{
					anisoLevel = 0,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					useMipMap = false
				};
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000AA06 File Offset: 0x00008C06
		internal virtual void OnEnable()
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000AA08 File Offset: 0x00008C08
		internal virtual void OnDisable()
		{
			RuntimeUtilities.Destroy(this.output);
		}

		// Token: 0x060000E2 RID: 226
		internal abstract void Render(PostProcessRenderContext context);

		// Token: 0x060000E3 RID: 227 RVA: 0x0000AA15 File Offset: 0x00008C15
		protected Monitor()
		{
		}

		// Token: 0x04000157 RID: 343
		[CompilerGenerated]
		private RenderTexture <output>k__BackingField;

		// Token: 0x04000158 RID: 344
		internal bool requested;
	}
}
