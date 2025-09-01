using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000016 RID: 22
	[PostProcess(typeof(EdgeDetectionRenderer), PostProcessEvent.BeforeUpscaling, "SC Post Effects/Stylized/Edge Detection", true)]
	[Serializable]
	public sealed class EdgeDetection : PostProcessEffectSettings
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000379C File Offset: 0x0000199C
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && this.edgeOpacity > 0f;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000037C0 File Offset: 0x000019C0
		public EdgeDetection()
		{
		}

		// Token: 0x04000046 RID: 70
		[Range(0f, 1f)]
		[DisplayName("Edges Only")]
		[Tooltip("Shows only the effect, to allow for finetuning")]
		public FloatParameter edgesOnly = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000047 RID: 71
		[Tooltip("Choose one of the different edge solvers")]
		public EdgeDetection.EdgeDetectionMode mode = new EdgeDetection.EdgeDetectionMode
		{
			value = EdgeDetection.EdgeDetectMode.DepthNormals
		};

		// Token: 0x04000048 RID: 72
		public BoolParameter invertFadeDistance = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000049 RID: 73
		[Tooltip("Fades out the effect between the cameras near and far clipping plane")]
		public BoolParameter distanceFade = new BoolParameter
		{
			value = false
		};

		// Token: 0x0400004A RID: 74
		public FloatParameter startFadeDistance = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400004B RID: 75
		public FloatParameter endFadeDistance = new FloatParameter
		{
			value = 1000f
		};

		// Token: 0x0400004C RID: 76
		public FloatParameter fadeMult = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x0400004D RID: 77
		[Range(0f, 250f)]
		public FloatParameter edgeDistMin = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400004E RID: 78
		[Tooltip("Distance from POI that edges will draw")]
		[Range(0f, 250f)]
		public FloatParameter edgeDistance = new FloatParameter
		{
			value = 25f
		};

		// Token: 0x0400004F RID: 79
		[DisplayName("Depth")]
		[Range(0f, 1f)]
		[Tooltip("Sets how much difference in depth between pixels contribute to drawing an edge")]
		public FloatParameter sensitivityDepth = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000050 RID: 80
		[DisplayName("Normals")]
		[Range(0f, 2f)]
		[Tooltip("Sets how much difference in normals between pixels contribute to drawing an edge")]
		public FloatParameter sensitivityNormals = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000051 RID: 81
		[Range(0.01f, 1f)]
		[DisplayName("Luminance Threshold")]
		[Tooltip("Luminance threshold, pixels above this threshold will contribute to the effect")]
		public FloatParameter lumThreshold = new FloatParameter
		{
			value = 0.01f
		};

		// Token: 0x04000052 RID: 82
		[DisplayName("Color")]
		[Tooltip("")]
		public ColorParameter edgeColor = new ColorParameter
		{
			value = Color.black
		};

		// Token: 0x04000053 RID: 83
		[Range(1f, 50f)]
		[Tooltip("Edge Exponent")]
		public FloatParameter edgeExp = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000054 RID: 84
		[DisplayName("Size")]
		[Range(1f, 4f)]
		[Tooltip("Edge Distance")]
		public IntParameter edgeSize = new IntParameter
		{
			value = 1
		};

		// Token: 0x04000055 RID: 85
		[DisplayName("Opacity")]
		[Range(0f, 1f)]
		[Tooltip("Opacity")]
		public FloatParameter edgeOpacity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000056 RID: 86
		[DisplayName("Thin")]
		[Tooltip("Limit the effect to inward edges only")]
		public BoolParameter sobelThin = new BoolParameter
		{
			value = false
		};

		// Token: 0x04000057 RID: 87
		[DisplayName("Background Intensity")]
		[Range(0f, 1f)]
		[Tooltip("Edge Background for Depth-Only")]
		public FloatParameter backgroundIntensity = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x0200005C RID: 92
		public enum EdgeDetectMode
		{
			// Token: 0x04000186 RID: 390
			DepthNormals,
			// Token: 0x04000187 RID: 391
			CrossDepthNormals,
			// Token: 0x04000188 RID: 392
			SobelDepth,
			// Token: 0x04000189 RID: 393
			LuminanceBased
		}

		// Token: 0x0200005D RID: 93
		[Serializable]
		public sealed class EdgeDetectionMode : ParameterOverride<EdgeDetection.EdgeDetectMode>
		{
			// Token: 0x060000F5 RID: 245 RVA: 0x00008510 File Offset: 0x00006710
			public EdgeDetectionMode()
			{
			}
		}
	}
}
