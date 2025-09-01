using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000061 RID: 97
	public sealed class PostProcessResources : ScriptableObject
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000FC0F File Offset: 0x0000DE0F
		public PostProcessResources()
		{
		}

		// Token: 0x040001FE RID: 510
		public Texture2D[] blueNoise64;

		// Token: 0x040001FF RID: 511
		public Texture2D[] blueNoise256;

		// Token: 0x04000200 RID: 512
		public PostProcessResources.SMAALuts smaaLuts;

		// Token: 0x04000201 RID: 513
		public PostProcessResources.Shaders shaders;

		// Token: 0x04000202 RID: 514
		public PostProcessResources.ComputeShaders computeShaders;

		// Token: 0x02000091 RID: 145
		[Serializable]
		public sealed class Shaders
		{
			// Token: 0x06000289 RID: 649 RVA: 0x000132F0 File Offset: 0x000114F0
			public PostProcessResources.Shaders Clone()
			{
				return (PostProcessResources.Shaders)base.MemberwiseClone();
			}

			// Token: 0x0600028A RID: 650 RVA: 0x000132FD File Offset: 0x000114FD
			public Shaders()
			{
			}

			// Token: 0x04000359 RID: 857
			public Shader bloom;

			// Token: 0x0400035A RID: 858
			public Shader copy;

			// Token: 0x0400035B RID: 859
			public Shader copyStd;

			// Token: 0x0400035C RID: 860
			public Shader copyStdFromTexArray;

			// Token: 0x0400035D RID: 861
			public Shader copyStdFromDoubleWide;

			// Token: 0x0400035E RID: 862
			public Shader discardAlpha;

			// Token: 0x0400035F RID: 863
			public Shader depthOfField;

			// Token: 0x04000360 RID: 864
			public Shader finalPass;

			// Token: 0x04000361 RID: 865
			public Shader grainBaker;

			// Token: 0x04000362 RID: 866
			public Shader motionBlur;

			// Token: 0x04000363 RID: 867
			public Shader temporalAntialiasing;

			// Token: 0x04000364 RID: 868
			public Shader subpixelMorphologicalAntialiasing;

			// Token: 0x04000365 RID: 869
			public Shader texture2dLerp;

			// Token: 0x04000366 RID: 870
			public Shader uber;

			// Token: 0x04000367 RID: 871
			public Shader lut2DBaker;

			// Token: 0x04000368 RID: 872
			public Shader lightMeter;

			// Token: 0x04000369 RID: 873
			public Shader gammaHistogram;

			// Token: 0x0400036A RID: 874
			public Shader waveform;

			// Token: 0x0400036B RID: 875
			public Shader vectorscope;

			// Token: 0x0400036C RID: 876
			public Shader debugOverlays;

			// Token: 0x0400036D RID: 877
			public Shader deferredFog;

			// Token: 0x0400036E RID: 878
			public Shader scalableAO;

			// Token: 0x0400036F RID: 879
			public Shader multiScaleAO;

			// Token: 0x04000370 RID: 880
			public Shader screenSpaceReflections;
		}

		// Token: 0x02000092 RID: 146
		[Serializable]
		public sealed class ComputeShaders
		{
			// Token: 0x0600028B RID: 651 RVA: 0x00013305 File Offset: 0x00011505
			public PostProcessResources.ComputeShaders Clone()
			{
				return (PostProcessResources.ComputeShaders)base.MemberwiseClone();
			}

			// Token: 0x0600028C RID: 652 RVA: 0x00013314 File Offset: 0x00011514
			public ComputeShader FindComputeShader(string name)
			{
				for (int i = 0; i < this.namedShaders.Length; i++)
				{
					if (this.namedShaders[i].name == name)
					{
						return this.namedShaders[i].shader;
					}
				}
				return null;
			}

			// Token: 0x0600028D RID: 653 RVA: 0x00013358 File Offset: 0x00011558
			public ComputeShaders()
			{
			}

			// Token: 0x04000371 RID: 881
			public ComputeShader autoExposure;

			// Token: 0x04000372 RID: 882
			public ComputeShader exposureHistogram;

			// Token: 0x04000373 RID: 883
			public ComputeShader lut3DBaker;

			// Token: 0x04000374 RID: 884
			public ComputeShader texture3dLerp;

			// Token: 0x04000375 RID: 885
			public ComputeShader gammaHistogram;

			// Token: 0x04000376 RID: 886
			public ComputeShader waveform;

			// Token: 0x04000377 RID: 887
			public ComputeShader vectorscope;

			// Token: 0x04000378 RID: 888
			public ComputeShader multiScaleAODownsample1;

			// Token: 0x04000379 RID: 889
			public ComputeShader multiScaleAODownsample2;

			// Token: 0x0400037A RID: 890
			public ComputeShader multiScaleAORender;

			// Token: 0x0400037B RID: 891
			public ComputeShader multiScaleAOUpsample;

			// Token: 0x0400037C RID: 892
			public ComputeShader gaussianDownsample;

			// Token: 0x0400037D RID: 893
			public PostProcessResources.NamedComputeShader[] namedShaders;
		}

		// Token: 0x02000093 RID: 147
		[Serializable]
		public class NamedComputeShader
		{
			// Token: 0x0600028E RID: 654 RVA: 0x00013360 File Offset: 0x00011560
			public NamedComputeShader()
			{
			}

			// Token: 0x0400037E RID: 894
			public string name;

			// Token: 0x0400037F RID: 895
			public ComputeShader shader;
		}

		// Token: 0x02000094 RID: 148
		[Serializable]
		public sealed class SMAALuts
		{
			// Token: 0x0600028F RID: 655 RVA: 0x00013368 File Offset: 0x00011568
			public SMAALuts()
			{
			}

			// Token: 0x04000380 RID: 896
			public Texture2D area;

			// Token: 0x04000381 RID: 897
			public Texture2D search;
		}
	}
}
