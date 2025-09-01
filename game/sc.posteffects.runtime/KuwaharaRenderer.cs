using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000023 RID: 35
	public sealed class KuwaharaRenderer : PostProcessEffectRenderer<Kuwahara>
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00005387 File Offset: 0x00003587
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Kuwahara");
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005399 File Offset: 0x00003599
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000053A4 File Offset: 0x000035A4
		public override void Render(PostProcessRenderContext context)
		{
			this.mode = (int)base.settings.mode.value;
			if (context.camera.orthographic)
			{
				this.mode = 0;
			}
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetFloat("_Radius", (float)base.settings.radius);
			if (this.mode == 1)
			{
				context.command.SetGlobalVector("_FadeParams", new Vector4(base.settings.startFadeDistance.value, base.settings.endFadeDistance.value, 0f, 0f));
			}
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, this.mode, false, null, false);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000547F File Offset: 0x0000367F
		public override DepthTextureMode GetCameraFlags()
		{
			if (base.settings.mode.value == Kuwahara.KuwaharaMode.DepthFade)
			{
				return DepthTextureMode.Depth;
			}
			return DepthTextureMode.None;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005497 File Offset: 0x00003697
		public KuwaharaRenderer()
		{
		}

		// Token: 0x040000A5 RID: 165
		private Shader shader;

		// Token: 0x040000A6 RID: 166
		private int mode;
	}
}
