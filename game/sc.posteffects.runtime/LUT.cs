using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000028 RID: 40
	[PostProcess(typeof(LUTRenderer), PostProcessEvent.AfterStack, "SC Post Effects/Image/Color Grading LUT", true)]
	[Serializable]
	public sealed class LUT : PostProcessEffectSettings
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00005EDB File Offset: 0x000040DB
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return this.enabled.value;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005EE8 File Offset: 0x000040E8
		public LUT()
		{
		}

		// Token: 0x040000C3 RID: 195
		[DisplayName("Mode")]
		[Tooltip("Distance-based mode blends two LUTs over a distance")]
		public LUT.ModeParam mode = new LUT.ModeParam
		{
			value = LUT.Mode.Single
		};

		// Token: 0x040000C4 RID: 196
		public FloatParameter startFadeDistance = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000C5 RID: 197
		public FloatParameter endFadeDistance = new FloatParameter
		{
			value = 1000f
		};

		// Token: 0x040000C6 RID: 198
		[Range(0f, 1f)]
		[Tooltip("Fades the effect in or out")]
		public FloatParameter intensity = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000C7 RID: 199
		[Tooltip("Supply a LUT strip texture.")]
		public TextureParameter lutNear = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000C8 RID: 200
		[DisplayName("Far")]
		public TextureParameter lutFar = new TextureParameter
		{
			value = null
		};

		// Token: 0x040000C9 RID: 201
		[Range(0f, 1f)]
		[Tooltip("Fades the effect in or out")]
		public FloatParameter invert = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x040000CA RID: 202
		public static bool Bypass;

		// Token: 0x02000070 RID: 112
		public enum Mode
		{
			// Token: 0x040001B7 RID: 439
			Single,
			// Token: 0x040001B8 RID: 440
			DistanceBased
		}

		// Token: 0x02000071 RID: 113
		[Serializable]
		public sealed class ModeParam : ParameterOverride<LUT.Mode>
		{
			// Token: 0x060000FD RID: 253 RVA: 0x00008550 File Offset: 0x00006750
			public ModeParam()
			{
			}
		}
	}
}
