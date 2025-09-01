using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000012 RID: 18
	[Preserve]
	internal sealed class AmbientOcclusionRenderer : PostProcessEffectRenderer<AmbientOcclusion>
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000024B7 File Offset: 0x000006B7
		public override void Init()
		{
			if (this.m_Methods == null)
			{
				this.m_Methods = new IAmbientOcclusionMethod[]
				{
					new ScalableAO(base.settings),
					new MultiScaleVO(base.settings)
				};
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024EC File Offset: 0x000006EC
		public bool IsAmbientOnly(PostProcessRenderContext context)
		{
			Camera camera = context.camera;
			return base.settings.ambientOnly.value && camera.actualRenderingPath == RenderingPath.DeferredShading && camera.allowHDR;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002523 File Offset: 0x00000723
		public IAmbientOcclusionMethod Get()
		{
			return this.m_Methods[(int)base.settings.mode.value];
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000253C File Offset: 0x0000073C
		public override DepthTextureMode GetCameraFlags()
		{
			return this.Get().GetCameraFlags();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000254C File Offset: 0x0000074C
		public override void Release()
		{
			IAmbientOcclusionMethod[] methods = this.m_Methods;
			for (int i = 0; i < methods.Length; i++)
			{
				methods[i].Release();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002576 File Offset: 0x00000776
		public ScalableAO GetScalableAO()
		{
			return (ScalableAO)this.m_Methods[0];
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002585 File Offset: 0x00000785
		public MultiScaleVO GetMultiScaleVO()
		{
			return (MultiScaleVO)this.m_Methods[1];
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002594 File Offset: 0x00000794
		public override void Render(PostProcessRenderContext context)
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002596 File Offset: 0x00000796
		public AmbientOcclusionRenderer()
		{
		}

		// Token: 0x04000037 RID: 55
		private IAmbientOcclusionMethod[] m_Methods;
	}
}
