using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200004C RID: 76
	public sealed class TransitionRenderer : PostProcessEffectRenderer<Transition>
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00008277 File Offset: 0x00006477
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Transition");
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008289 File Offset: 0x00006489
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008294 File Offset: 0x00006494
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			propertySheet.properties.SetFloat("_Progress", base.settings.progress.value);
			Texture value = (base.settings.gradientTex.value == null) ? RuntimeUtilities.whiteTexture : base.settings.gradientTex.value;
			propertySheet.properties.SetTexture("_Gradient", value);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0, false, null, false);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008338 File Offset: 0x00006538
		public TransitionRenderer()
		{
		}

		// Token: 0x04000161 RID: 353
		private Shader shader;
	}
}
