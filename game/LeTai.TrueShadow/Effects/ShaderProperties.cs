using System;
using UnityEngine;

namespace LeTai.Effects
{
	// Token: 0x0200002B RID: 43
	public static class ShaderProperties
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00006C16 File Offset: 0x00004E16
		public static void Init()
		{
			if (ShaderProperties.isInitialized)
			{
				return;
			}
			ShaderProperties.blurRadius = Shader.PropertyToID("_Radius");
			ShaderProperties.blurTextureCropRegion = Shader.PropertyToID("_CropRegion");
			ShaderProperties.isInitialized = true;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006C44 File Offset: 0x00004E44
		public static void Init(int stackDepth)
		{
			ShaderProperties.intermediateRT = new int[stackDepth * 2 - 1];
			for (int i = 0; i < ShaderProperties.intermediateRT.Length; i++)
			{
				ShaderProperties.intermediateRT[i] = Shader.PropertyToID(string.Format("TI_intermediate_rt_{0}", i));
			}
			ShaderProperties.Init();
		}

		// Token: 0x040000BE RID: 190
		private static bool isInitialized;

		// Token: 0x040000BF RID: 191
		public static int[] intermediateRT;

		// Token: 0x040000C0 RID: 192
		public static int blurRadius;

		// Token: 0x040000C1 RID: 193
		public static int blurTextureCropRegion;
	}
}
