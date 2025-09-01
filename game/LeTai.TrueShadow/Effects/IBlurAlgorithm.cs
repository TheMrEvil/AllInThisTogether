using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace LeTai.Effects
{
	// Token: 0x02000028 RID: 40
	public interface IBlurAlgorithm
	{
		// Token: 0x0600012B RID: 299
		void Configure(BlurConfig config);

		// Token: 0x0600012C RID: 300
		void Blur(CommandBuffer cmd, RenderTargetIdentifier src, Rect srcCropRegion, RenderTexture target);
	}
}
