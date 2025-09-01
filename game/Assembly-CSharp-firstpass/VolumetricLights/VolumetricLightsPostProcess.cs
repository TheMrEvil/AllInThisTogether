using System;
using UnityEngine;

namespace VolumetricLights
{
	// Token: 0x02000024 RID: 36
	public class VolumetricLightsPostProcess : VolumetricLightsPostProcessBase
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00008118 File Offset: 0x00006318
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.AddRenderPasses(source, destination);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00008122 File Offset: 0x00006322
		public VolumetricLightsPostProcess()
		{
		}
	}
}
