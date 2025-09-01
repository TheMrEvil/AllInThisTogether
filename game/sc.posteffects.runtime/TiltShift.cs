using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000049 RID: 73
	[PostProcess(typeof(TiltShiftRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Blurring/Tilt Shift", true)]
	[Serializable]
	public class TiltShift : PostProcessEffectSettings
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00007F38 File Offset: 0x00006138
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value && (this.areaSize != 0f || this.areaFalloff != 0f) && this.amount != 0f;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007F90 File Offset: 0x00006190
		public TiltShift()
		{
		}

		// Token: 0x04000155 RID: 341
		[Range(0f, 1f)]
		[Tooltip("The amount of blurring that must be performed")]
		public FloatParameter amount = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000156 RID: 342
		[DisplayName("Method")]
		public TiltShift.TiltShifMethodParameter mode = new TiltShift.TiltShifMethodParameter
		{
			value = TiltShift.TiltShiftMethod.Horizontal
		};

		// Token: 0x04000157 RID: 343
		[DisplayName("Quality")]
		[Tooltip("Choose to use more texture samples, for a smoother blur when using a high blur amout")]
		public TiltShift.TiltShiftQualityParameter quality = new TiltShift.TiltShiftQualityParameter
		{
			value = TiltShift.Quality.Appearance
		};

		// Token: 0x04000158 RID: 344
		[Range(0f, 1f)]
		public FloatParameter areaSize = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000159 RID: 345
		[Range(0f, 1f)]
		public FloatParameter areaFalloff = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x0400015A RID: 346
		[Range(-1f, 1f)]
		public FloatParameter offset = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400015B RID: 347
		[Range(0f, 360f)]
		public FloatParameter angle = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400015C RID: 348
		public static bool debug;

		// Token: 0x02000084 RID: 132
		public enum TiltShiftMethod
		{
			// Token: 0x040001E1 RID: 481
			Horizontal,
			// Token: 0x040001E2 RID: 482
			Radial
		}

		// Token: 0x02000085 RID: 133
		[Serializable]
		public sealed class TiltShifMethodParameter : ParameterOverride<TiltShift.TiltShiftMethod>
		{
			// Token: 0x06000106 RID: 262 RVA: 0x00008598 File Offset: 0x00006798
			public TiltShifMethodParameter()
			{
			}
		}

		// Token: 0x02000086 RID: 134
		public enum Quality
		{
			// Token: 0x040001E4 RID: 484
			Performance,
			// Token: 0x040001E5 RID: 485
			Appearance
		}

		// Token: 0x02000087 RID: 135
		[Serializable]
		public sealed class TiltShiftQualityParameter : ParameterOverride<TiltShift.Quality>
		{
			// Token: 0x06000107 RID: 263 RVA: 0x000085A0 File Offset: 0x000067A0
			public TiltShiftQualityParameter()
			{
			}
		}
	}
}
