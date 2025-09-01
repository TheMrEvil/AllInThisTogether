using System;

namespace MK.Toon
{
	// Token: 0x02000023 RID: 35
	public static class Keywords
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// Note: this type is marked as 'beforefieldinit'.
		static Keywords()
		{
		}

		// Token: 0x040000BD RID: 189
		public static readonly string albedoMap = "_MK_ALBEDO_MAP";

		// Token: 0x040000BE RID: 190
		public static readonly string alphaClipping = "_MK_ALPHA_CLIPPING";

		// Token: 0x040000BF RID: 191
		public static readonly string[] surface = new string[]
		{
			"_MK_SURFACE_TYPE_OPAQUE",
			"_MK_SURFACE_TYPE_TRANSPARENT"
		};

		// Token: 0x040000C0 RID: 192
		public static readonly string[] lightTransmission = new string[]
		{
			"_MK_LIGHT_TRANSMISSION_OFF",
			"_MK_LIGHT_TRANSMISSION_TRANSLUCENT",
			"_MK_LIGHT_TRANSMISSION_SUB_SURFACE_SCATTERING"
		};

		// Token: 0x040000C1 RID: 193
		public static readonly string thicknessMap = "_MK_THICKNESS_MAP";

		// Token: 0x040000C2 RID: 194
		public static readonly string normalMap = "_MK_NORMAL_MAP";

		// Token: 0x040000C3 RID: 195
		public static readonly string heightMap = "_MK_HEIGHT_MAP";

		// Token: 0x040000C4 RID: 196
		public static readonly string parallax = "_MK_PARALLAX";

		// Token: 0x040000C5 RID: 197
		public static readonly string occlusionMap = "_MK_OCCLUSION_MAP";

		// Token: 0x040000C6 RID: 198
		public static readonly string[] blend = new string[]
		{
			"_MK_BLEND_ALPHA",
			"_MK_BLEND_PREMULTIPLY",
			"_MK_BLEND_ADDITIVE",
			"_MK_BLEND_MULTIPLY",
			"_MK_BLEND_CUSTOM"
		};

		// Token: 0x040000C7 RID: 199
		public static readonly string[] light = new string[]
		{
			"_MK_LIGHT_DEFAULT",
			"_MK_LIGHT_CEL",
			"_MK_LIGHT_BANDED",
			"_MK_LIGHT_RAMP"
		};

		// Token: 0x040000C8 RID: 200
		public static readonly string[] artistic = new string[]
		{
			"_MK_ARTISTIC_OFF",
			"_MK_ARTISTIC_DRAWN",
			"_MK_ARTISTIC_HATCHING",
			"_MK_ARTISTIC_SKETCH"
		};

		// Token: 0x040000C9 RID: 201
		public static readonly string[] artisticProjection = new string[]
		{
			"_MK_ARTISTIC_PROJECTION_TANGENT_SPACE",
			"_MK_ARTISTIC_PROJECTION_SCREEN_SPACE"
		};

		// Token: 0x040000CA RID: 202
		public static readonly string artisticAnimation = "_MK_ARTISTIC_ANIMATION_STUTTER";

		// Token: 0x040000CB RID: 203
		public static readonly string[] workflow = new string[]
		{
			"_MK_WORKFLOW_SPECULAR",
			"_MK_WORKFLOW_ROUGHNESS"
		};

		// Token: 0x040000CC RID: 204
		public static readonly string emission = "_MK_EMISSION";

		// Token: 0x040000CD RID: 205
		public static readonly string emissionMap = "_MK_EMISSION_MAP";

		// Token: 0x040000CE RID: 206
		public static readonly string detailMap = "_MK_DETAIL_MAP";

		// Token: 0x040000CF RID: 207
		public static readonly string[] detailBlend = new string[]
		{
			"_MK_DETAIL_BLEND_OFF",
			"_MK_DETAIL_BLEND_MIX",
			"_MK_DETAIL_BLEND_ADD"
		};

		// Token: 0x040000D0 RID: 208
		public static readonly string detailNormalMap = "_MK_DETAIL_NORMAL_MAP";

		// Token: 0x040000D1 RID: 209
		public static readonly string[] rim = new string[]
		{
			"_MK_RIM_OFF",
			"_MK_RIM_DEFAULT",
			"_MK_RIM_SPLIT"
		};

		// Token: 0x040000D2 RID: 210
		public static readonly string[] iridescence = new string[]
		{
			"_MK_IRIDESCENCE_OFF",
			"_MK_IRIDESCENCE_DEFAULT"
		};

		// Token: 0x040000D3 RID: 211
		public static readonly string[] colorGrading = new string[]
		{
			"_MK_COLOR_GRADING_OFF",
			"_MK_COLOR_GRADING_ALBEDO",
			"_MK_COLOR_GRADING_FINAL_OUTPUT"
		};

		// Token: 0x040000D4 RID: 212
		public static readonly string[] dissolve = new string[]
		{
			"_MK_DISSOLVE_OFF",
			"_MK_DISSOLVE_DEFAULT",
			"_MK_DISSOLVE_BORDER_COLOR",
			"_MK_DISSOLVE_BORDER_RAMP"
		};

		// Token: 0x040000D5 RID: 213
		public static readonly string goochRamp = "_MK_GOOCH_RAMP";

		// Token: 0x040000D6 RID: 214
		public static readonly string goochBrightMap = "_MK_GOOCH_BRIGHT_MAP";

		// Token: 0x040000D7 RID: 215
		public static readonly string goochDarkMap = "_MK_GOOCH_DARK_MAP";

		// Token: 0x040000D8 RID: 216
		public static readonly string[] diffuse = new string[]
		{
			"_MK_DIFFUSE_LAMBERT",
			"_MK_DIFFUSE_OREN_NAYAR",
			"_MK_DIFFUSE_MINNAERT"
		};

		// Token: 0x040000D9 RID: 217
		public static readonly string[] specular = new string[]
		{
			"_MK_SPECULAR_OFF",
			"_MK_SPECULAR_ISOTROPIC",
			"_MK_SPECULAR_ANISOTROPIC"
		};

		// Token: 0x040000DA RID: 218
		public static readonly string[] environmentReflections = new string[]
		{
			"_MK_ENVIRONMENT_REFLECTIONS_OFF",
			"_MK_ENVIRONMENT_REFLECTIONS_AMBIENT",
			"_MK_ENVIRONMENT_REFLECTIONS_ADVANCED"
		};

		// Token: 0x040000DB RID: 219
		public static readonly string fresnelHighlights = "_MK_FRESNEL_HIGHLIGHTS";

		// Token: 0x040000DC RID: 220
		public static readonly string[] outline = new string[]
		{
			"_MK_OUTLINE_HULL_OBJECT",
			"_MK_OUTLINE_HULL_ORIGIN",
			"_MK_OUTLINE_HULL_CLIP"
		};

		// Token: 0x040000DD RID: 221
		public static readonly string outlineData = "_MK_OUTLINE_DATA_UV7";

		// Token: 0x040000DE RID: 222
		public static readonly string outlineMap = "_MK_OUTLINE_MAP";

		// Token: 0x040000DF RID: 223
		public static readonly string refractionDistortionMap = "_MK_REFRACTION_DISTORTION_MAP";

		// Token: 0x040000E0 RID: 224
		public static readonly string indexOfRefraction = "_MK_INDEX_OF_REFRACTION";

		// Token: 0x040000E1 RID: 225
		public static readonly string outlineNoise = "_MK_OUTLINE_NOISE";

		// Token: 0x040000E2 RID: 226
		public static readonly string receiveShadows = "_MK_RECEIVE_SHADOWS";

		// Token: 0x040000E3 RID: 227
		public static readonly string wrappedLighting = "_MK_WRAPPED_DIFFUSE";

		// Token: 0x040000E4 RID: 228
		public static readonly string[] colorBlend = new string[]
		{
			"_MK_COLOR_BLEND_MULTIPLY",
			"_MK_COLOR_BLEND_ADDITIVE",
			"_MK_COLOR_BLEND_SUBTRACTIVE",
			"_MK_COLOR_BLEND_OVERLAY",
			"_MK_COLOR_BLEND_COLOR",
			"_MK_COLOR_BLEND_DIFFERENCE"
		};

		// Token: 0x040000E5 RID: 229
		public static readonly string flipbook = "_MK_FLIPBOOK";

		// Token: 0x040000E6 RID: 230
		public static readonly string softFade = "_MK_SOFT_FADE";

		// Token: 0x040000E7 RID: 231
		public static readonly string cameraFade = "_MK_CAMERA_FADE";

		// Token: 0x040000E8 RID: 232
		public static readonly string thresholdMap = "_MK_THRESHOLD_MAP";

		// Token: 0x040000E9 RID: 233
		public static readonly string pbsMap0 = "_MK_PBS_MAP_0";

		// Token: 0x040000EA RID: 234
		public static readonly string pbsMap1 = "_MK_PBS_MAP_1";

		// Token: 0x040000EB RID: 235
		public static readonly string[] vertexAnimation = new string[]
		{
			"_MK_VERTEX_ANIMATION_OFF",
			"_MK_VERTEX_ANIMATION_SINE",
			"_MK_VERTEX_ANIMATION_PULSE",
			"_MK_VERTEX_ANIMATION_NOISE"
		};

		// Token: 0x040000EC RID: 236
		public static readonly string vertexAnimationMap = "_MK_VERTEX_ANIMATION_MAP";

		// Token: 0x040000ED RID: 237
		public static readonly string vertexAnimationStutter = "_MK_VERTEX_ANIMATION_STUTTER";
	}
}
