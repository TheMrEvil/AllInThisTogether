using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000011 RID: 17
	public sealed class DangerRenderer : PostProcessEffectRenderer<Danger>
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000033FD File Offset: 0x000015FD
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Danger");
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000340F File Offset: 0x0000160F
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003418 File Offset: 0x00001618
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.intensity, base.settings.size, 0f, 0f));
			propertySheet.properties.SetColor("_Color", base.settings.color);
			Texture value = (base.settings.overlayTex.value == null) ? RuntimeUtilities.blackTexture : base.settings.overlayTex.value;
			propertySheet.properties.SetTexture("_Overlay", value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000034FB File Offset: 0x000016FB
		public DangerRenderer()
		{
		}

		// Token: 0x0400003D RID: 61
		private Shader shader;
	}
}
