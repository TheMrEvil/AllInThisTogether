using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200001F RID: 31
	[PostProcess(typeof(ColorGradingRenderer), "Unity/Color Grading", true)]
	[Serializable]
	public sealed class ColorGrading : PostProcessEffectSettings
	{
		// Token: 0x06000035 RID: 53 RVA: 0x0000347D File Offset: 0x0000167D
		public override bool IsEnabledAndSupported(PostProcessRenderContext context)
		{
			return (this.gradingMode.value != GradingMode.External || (SystemInfo.supports3DRenderTextures && SystemInfo.supportsComputeShaders)) && this.enabled.value;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000034A8 File Offset: 0x000016A8
		public ColorGrading()
		{
		}

		// Token: 0x04000060 RID: 96
		[DisplayName("Mode")]
		[Tooltip("Select a color grading mode that fits your dynamic range and workflow. Use HDR if your camera is set to render in HDR and your target platform supports it. Use LDR for low-end mobiles or devices that don't support HDR. Use External if you prefer authoring a Log LUT in an external software.")]
		public GradingModeParameter gradingMode = new GradingModeParameter
		{
			value = GradingMode.HighDefinitionRange
		};

		// Token: 0x04000061 RID: 97
		[DisplayName("Lookup Texture")]
		[Tooltip("A custom 3D log-encoded texture.")]
		public TextureParameter externalLut = new TextureParameter
		{
			value = null
		};

		// Token: 0x04000062 RID: 98
		[DisplayName("Mode")]
		[Tooltip("Select a tonemapping algorithm to use at the end of the color grading process.")]
		public TonemapperParameter tonemapper = new TonemapperParameter
		{
			value = Tonemapper.None
		};

		// Token: 0x04000063 RID: 99
		[DisplayName("Toe Strength")]
		[Range(0f, 1f)]
		[Tooltip("Affects the transition between the toe and the mid section of the curve. A value of 0 means no toe, a value of 1 means a very hard transition.")]
		public FloatParameter toneCurveToeStrength = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000064 RID: 100
		[DisplayName("Toe Length")]
		[Range(0f, 1f)]
		[Tooltip("Affects how much of the dynamic range is in the toe. With a small value, the toe will be very short and quickly transition into the linear section, with a larger value, the toe will be longer.")]
		public FloatParameter toneCurveToeLength = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x04000065 RID: 101
		[DisplayName("Shoulder Strength")]
		[Range(0f, 1f)]
		[Tooltip("Affects the transition between the mid section and the shoulder of the curve. A value of 0 means no shoulder, a value of 1 means a very hard transition.")]
		public FloatParameter toneCurveShoulderStrength = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000066 RID: 102
		[DisplayName("Shoulder Length")]
		[Min(0f)]
		[Tooltip("Affects how many F-stops (EV) to add to the dynamic range of the curve.")]
		public FloatParameter toneCurveShoulderLength = new FloatParameter
		{
			value = 0.5f
		};

		// Token: 0x04000067 RID: 103
		[DisplayName("Shoulder Angle")]
		[Range(0f, 1f)]
		[Tooltip("Affects how much overshoot to add to the shoulder.")]
		public FloatParameter toneCurveShoulderAngle = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000068 RID: 104
		[DisplayName("Gamma")]
		[Min(0.001f)]
		[Tooltip("Applies a gamma function to the curve.")]
		public FloatParameter toneCurveGamma = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x04000069 RID: 105
		[DisplayName("Lookup Texture")]
		[Tooltip("Custom lookup texture (strip format, for example 256x16) to apply before the rest of the color grading operators. If none is provided, a neutral one will be generated internally.")]
		public TextureParameter ldrLut = new TextureParameter
		{
			value = null,
			defaultState = TextureParameterDefault.Lut2D
		};

		// Token: 0x0400006A RID: 106
		[DisplayName("Contribution")]
		[Range(0f, 1f)]
		[Tooltip("How much of the lookup texture will contribute to the color grading effect.")]
		public FloatParameter ldrLutContribution = new FloatParameter
		{
			value = 1f
		};

		// Token: 0x0400006B RID: 107
		[DisplayName("Temperature")]
		[Range(-100f, 100f)]
		[Tooltip("Sets the white balance to a custom color temperature.")]
		public FloatParameter temperature = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400006C RID: 108
		[DisplayName("Tint")]
		[Range(-100f, 100f)]
		[Tooltip("Sets the white balance to compensate for a green or magenta tint.")]
		public FloatParameter tint = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400006D RID: 109
		[DisplayName("Color Filter")]
		[ColorUsage(false, true)]
		[Tooltip("Tint the render by multiplying a color.")]
		public ColorParameter colorFilter = new ColorParameter
		{
			value = Color.white
		};

		// Token: 0x0400006E RID: 110
		[DisplayName("Hue Shift")]
		[Range(-180f, 180f)]
		[Tooltip("Shift the hue of all colors.")]
		public FloatParameter hueShift = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400006F RID: 111
		[DisplayName("Saturation")]
		[Range(-100f, 100f)]
		[Tooltip("Pushes the intensity of all colors.")]
		public FloatParameter saturation = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000070 RID: 112
		[DisplayName("Brightness")]
		[Range(-100f, 100f)]
		[Tooltip("Makes the image brighter or darker.")]
		public FloatParameter brightness = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000071 RID: 113
		[DisplayName("Post-exposure (EV)")]
		[Tooltip("Adjusts the overall exposure of the scene in EV units. This is applied after the HDR effect and right before tonemapping so it won't affect previous effects in the chain.")]
		public FloatParameter postExposure = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000072 RID: 114
		[DisplayName("Contrast")]
		[Range(-100f, 100f)]
		[Tooltip("Expands or shrinks the overall range of tonal values.")]
		public FloatParameter contrast = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000073 RID: 115
		[DisplayName("Red")]
		[Range(-200f, 200f)]
		[Tooltip("Modify influence of the red channel in the overall mix.")]
		public FloatParameter mixerRedOutRedIn = new FloatParameter
		{
			value = 100f
		};

		// Token: 0x04000074 RID: 116
		[DisplayName("Green")]
		[Range(-200f, 200f)]
		[Tooltip("Modify influence of the green channel in the overall mix.")]
		public FloatParameter mixerRedOutGreenIn = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000075 RID: 117
		[DisplayName("Blue")]
		[Range(-200f, 200f)]
		[Tooltip("Modify influence of the blue channel in the overall mix.")]
		public FloatParameter mixerRedOutBlueIn = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000076 RID: 118
		[DisplayName("Red")]
		[Range(-200f, 200f)]
		[Tooltip("Modify influence of the red channel in the overall mix.")]
		public FloatParameter mixerGreenOutRedIn = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000077 RID: 119
		[DisplayName("Green")]
		[Range(-200f, 200f)]
		[Tooltip("Modify influence of the green channel in the overall mix.")]
		public FloatParameter mixerGreenOutGreenIn = new FloatParameter
		{
			value = 100f
		};

		// Token: 0x04000078 RID: 120
		[DisplayName("Blue")]
		[Range(-200f, 200f)]
		[Tooltip("Modify influence of the blue channel in the overall mix.")]
		public FloatParameter mixerGreenOutBlueIn = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x04000079 RID: 121
		[DisplayName("Red")]
		[Range(-200f, 200f)]
		[Tooltip("Modify influence of the red channel in the overall mix.")]
		public FloatParameter mixerBlueOutRedIn = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400007A RID: 122
		[DisplayName("Green")]
		[Range(-200f, 200f)]
		[Tooltip("Modify influence of the green channel in the overall mix.")]
		public FloatParameter mixerBlueOutGreenIn = new FloatParameter
		{
			value = 0f
		};

		// Token: 0x0400007B RID: 123
		[DisplayName("Blue")]
		[Range(-200f, 200f)]
		[Tooltip("Modify influence of the blue channel in the overall mix.")]
		public FloatParameter mixerBlueOutBlueIn = new FloatParameter
		{
			value = 100f
		};

		// Token: 0x0400007C RID: 124
		[DisplayName("Lift")]
		[Tooltip("Controls the darkest portions of the render.")]
		[Trackball(TrackballAttribute.Mode.Lift)]
		public Vector4Parameter lift = new Vector4Parameter
		{
			value = new Vector4(1f, 1f, 1f, 0f)
		};

		// Token: 0x0400007D RID: 125
		[DisplayName("Gamma")]
		[Tooltip("Power function that controls mid-range tones.")]
		[Trackball(TrackballAttribute.Mode.Gamma)]
		public Vector4Parameter gamma = new Vector4Parameter
		{
			value = new Vector4(1f, 1f, 1f, 0f)
		};

		// Token: 0x0400007E RID: 126
		[DisplayName("Gain")]
		[Tooltip("Controls the lightest portions of the render.")]
		[Trackball(TrackballAttribute.Mode.Gain)]
		public Vector4Parameter gain = new Vector4Parameter
		{
			value = new Vector4(1f, 1f, 1f, 0f)
		};

		// Token: 0x0400007F RID: 127
		public SplineParameter masterCurve = new SplineParameter
		{
			value = new Spline(new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			}), 0f, false, new Vector2(0f, 1f))
		};

		// Token: 0x04000080 RID: 128
		public SplineParameter redCurve = new SplineParameter
		{
			value = new Spline(new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			}), 0f, false, new Vector2(0f, 1f))
		};

		// Token: 0x04000081 RID: 129
		public SplineParameter greenCurve = new SplineParameter
		{
			value = new Spline(new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			}), 0f, false, new Vector2(0f, 1f))
		};

		// Token: 0x04000082 RID: 130
		public SplineParameter blueCurve = new SplineParameter
		{
			value = new Spline(new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f, 1f, 1f),
				new Keyframe(1f, 1f, 1f, 1f)
			}), 0f, false, new Vector2(0f, 1f))
		};

		// Token: 0x04000083 RID: 131
		public SplineParameter hueVsHueCurve = new SplineParameter
		{
			value = new Spline(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f))
		};

		// Token: 0x04000084 RID: 132
		public SplineParameter hueVsSatCurve = new SplineParameter
		{
			value = new Spline(new AnimationCurve(), 0.5f, true, new Vector2(0f, 1f))
		};

		// Token: 0x04000085 RID: 133
		public SplineParameter satVsSatCurve = new SplineParameter
		{
			value = new Spline(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f))
		};

		// Token: 0x04000086 RID: 134
		public SplineParameter lumVsSatCurve = new SplineParameter
		{
			value = new Spline(new AnimationCurve(), 0.5f, false, new Vector2(0f, 1f))
		};
	}
}
