using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000025 RID: 37
	[Preserve]
	[Serializable]
	internal sealed class Dithering
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00005518 File Offset: 0x00003718
		internal void Render(PostProcessRenderContext context)
		{
			Texture2D[] blueNoise = context.resources.blueNoise64;
			int num = this.m_NoiseTextureIndex + 1;
			this.m_NoiseTextureIndex = num;
			if (num >= blueNoise.Length)
			{
				this.m_NoiseTextureIndex = 0;
			}
			float z = (float)this.m_Random.NextDouble();
			float w = (float)this.m_Random.NextDouble();
			Texture2D texture2D = blueNoise[this.m_NoiseTextureIndex];
			PropertySheet uberSheet = context.uberSheet;
			uberSheet.properties.SetTexture(ShaderIDs.DitheringTex, texture2D);
			uberSheet.properties.SetVector(ShaderIDs.Dithering_Coords, new Vector4((float)context.screenWidth / (float)texture2D.width, (float)context.screenHeight / (float)texture2D.height, z, w));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000055BF File Offset: 0x000037BF
		public Dithering()
		{
		}

		// Token: 0x0400009C RID: 156
		private int m_NoiseTextureIndex;

		// Token: 0x0400009D RID: 157
		private Random m_Random = new Random(1234);
	}
}
