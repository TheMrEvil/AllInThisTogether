using System;
using UnityEngine;

namespace VolumetricLights
{
	// Token: 0x02000026 RID: 38
	public class VolumetricLightsPostProcessBeforeTransparents : VolumetricLightsPostProcessBase
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000082D4 File Offset: 0x000064D4
		[ImageEffectOpaque]
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.AddRenderPasses(source, destination);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000082DE File Offset: 0x000064DE
		public VolumetricLightsPostProcessBeforeTransparents()
		{
		}
	}
}
