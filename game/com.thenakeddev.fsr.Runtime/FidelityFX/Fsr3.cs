using System;
using UnityEngine;

namespace FidelityFX
{
	// Token: 0x02000002 RID: 2
	public static class Fsr3
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static Fsr3UpscalerContext CreateContext(Vector2Int displaySize, Vector2Int maxRenderSize, Fsr3UpscalerShaders shaders, Fsr3.InitializationFlags flags = (Fsr3.InitializationFlags)0)
		{
			if (SystemInfo.usesReversedZBuffer)
			{
				flags |= Fsr3.InitializationFlags.EnableDepthInverted;
			}
			else
			{
				flags &= ~Fsr3.InitializationFlags.EnableDepthInverted;
			}
			Fsr3.ContextDescription contextDescription = new Fsr3.ContextDescription
			{
				Flags = flags,
				MaxUpscaleSize = displaySize,
				MaxRenderSize = maxRenderSize,
				Shaders = shaders
			};
			Fsr3UpscalerContext fsr3UpscalerContext = new Fsr3UpscalerContext();
			fsr3UpscalerContext.Create(contextDescription);
			return fsr3UpscalerContext;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000002A8
		public static float GetUpscaleRatioFromQualityMode(Fsr3.QualityMode qualityMode)
		{
			switch (qualityMode)
			{
			case Fsr3.QualityMode.NativeAA:
				return 1f;
			case Fsr3.QualityMode.Quality:
				return 1.5f;
			case Fsr3.QualityMode.Balanced:
				return 1.7f;
			case Fsr3.QualityMode.Performance:
				return 2f;
			case Fsr3.QualityMode.UltraPerformance:
				return 3f;
			case Fsr3.QualityMode.UltraQuality:
				return 1.2f;
			default:
				return 1f;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002100 File Offset: 0x00000300
		public static void GetRenderResolutionFromQualityMode(out int renderWidth, out int renderHeight, int displayWidth, int displayHeight, Fsr3.QualityMode qualityMode)
		{
			float upscaleRatioFromQualityMode = Fsr3.GetUpscaleRatioFromQualityMode(qualityMode);
			renderWidth = Mathf.RoundToInt((float)displayWidth / upscaleRatioFromQualityMode);
			renderHeight = Mathf.RoundToInt((float)displayHeight / upscaleRatioFromQualityMode);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000212B File Offset: 0x0000032B
		public static float GetMipmapBiasOffset(int renderWidth, int displayWidth)
		{
			return Mathf.Log((float)renderWidth / (float)displayWidth, 2f) - 1f;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002142 File Offset: 0x00000342
		public static int GetJitterPhaseCount(int renderWidth, int displayWidth)
		{
			return (int)(8f * Mathf.Pow((float)displayWidth / (float)renderWidth, 2f));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000215A File Offset: 0x0000035A
		public static void GetJitterOffset(out float outX, out float outY, int index, int phaseCount)
		{
			outX = Fsr3.Halton(index % phaseCount + 1, 2) - 0.5f;
			outY = Fsr3.Halton(index % phaseCount + 1, 3) - 0.5f;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002184 File Offset: 0x00000384
		private static float Halton(int index, int @base)
		{
			float num = 1f;
			float num2 = 0f;
			for (int i = index; i > 0; i = (int)Mathf.Floor((float)i / (float)@base))
			{
				num /= (float)@base;
				num2 += num * (float)(i % @base);
			}
			return num2;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021C0 File Offset: 0x000003C0
		public static float Lanczos2(float value)
		{
			if (Mathf.Abs(value) >= Mathf.Epsilon)
			{
				return Mathf.Sin(3.1415927f * value) / (3.1415927f * value) * (Mathf.Sin(1.5707964f * value) / (1.5707964f * value));
			}
			return 1f;
		}

		// Token: 0x02000019 RID: 25
		public enum QualityMode
		{
			// Token: 0x040000C6 RID: 198
			Off,
			// Token: 0x040000C7 RID: 199
			NativeAA,
			// Token: 0x040000C8 RID: 200
			Quality,
			// Token: 0x040000C9 RID: 201
			Balanced,
			// Token: 0x040000CA RID: 202
			Performance,
			// Token: 0x040000CB RID: 203
			UltraPerformance,
			// Token: 0x040000CC RID: 204
			UltraQuality
		}

		// Token: 0x0200001A RID: 26
		[Flags]
		public enum InitializationFlags
		{
			// Token: 0x040000CE RID: 206
			EnableHighDynamicRange = 1,
			// Token: 0x040000CF RID: 207
			EnableDisplayResolutionMotionVectors = 2,
			// Token: 0x040000D0 RID: 208
			EnableMotionVectorsJitterCancellation = 4,
			// Token: 0x040000D1 RID: 209
			EnableDepthInverted = 8,
			// Token: 0x040000D2 RID: 210
			EnableDepthInfinite = 16,
			// Token: 0x040000D3 RID: 211
			EnableAutoExposure = 32,
			// Token: 0x040000D4 RID: 212
			EnableDynamicResolution = 64,
			// Token: 0x040000D5 RID: 213
			EnableFP16Usage = 128,
			// Token: 0x040000D6 RID: 214
			EnableDebugChecking = 256
		}

		// Token: 0x0200001B RID: 27
		[Flags]
		public enum DispatchFlags
		{
			// Token: 0x040000D8 RID: 216
			DrawDebugView = 1
		}

		// Token: 0x0200001C RID: 28
		public struct ContextDescription
		{
			// Token: 0x040000D9 RID: 217
			public Fsr3.InitializationFlags Flags;

			// Token: 0x040000DA RID: 218
			public Vector2Int MaxRenderSize;

			// Token: 0x040000DB RID: 219
			public Vector2Int MaxUpscaleSize;

			// Token: 0x040000DC RID: 220
			public Fsr3UpscalerShaders Shaders;
		}

		// Token: 0x0200001D RID: 29
		public class DispatchDescription
		{
			// Token: 0x06000067 RID: 103 RVA: 0x000059E2 File Offset: 0x00003BE2
			public DispatchDescription()
			{
			}

			// Token: 0x040000DD RID: 221
			public ResourceView Color;

			// Token: 0x040000DE RID: 222
			public ResourceView Depth;

			// Token: 0x040000DF RID: 223
			public ResourceView MotionVectors;

			// Token: 0x040000E0 RID: 224
			public ResourceView Exposure;

			// Token: 0x040000E1 RID: 225
			public ResourceView Reactive;

			// Token: 0x040000E2 RID: 226
			public ResourceView TransparencyAndComposition;

			// Token: 0x040000E3 RID: 227
			public ResourceView Output;

			// Token: 0x040000E4 RID: 228
			public Vector2 JitterOffset;

			// Token: 0x040000E5 RID: 229
			public Vector2 MotionVectorScale;

			// Token: 0x040000E6 RID: 230
			public Vector2Int RenderSize;

			// Token: 0x040000E7 RID: 231
			public Vector2Int UpscaleSize;

			// Token: 0x040000E8 RID: 232
			public bool EnableSharpening;

			// Token: 0x040000E9 RID: 233
			public float Sharpness;

			// Token: 0x040000EA RID: 234
			public float FrameTimeDelta;

			// Token: 0x040000EB RID: 235
			public float PreExposure;

			// Token: 0x040000EC RID: 236
			public bool Reset;

			// Token: 0x040000ED RID: 237
			public float CameraNear;

			// Token: 0x040000EE RID: 238
			public float CameraFar;

			// Token: 0x040000EF RID: 239
			public float CameraFovAngleVertical;

			// Token: 0x040000F0 RID: 240
			public float ViewSpaceToMetersFactor;

			// Token: 0x040000F1 RID: 241
			public Fsr3.DispatchFlags Flags;

			// Token: 0x040000F2 RID: 242
			public bool UseTextureArrays;

			// Token: 0x040000F3 RID: 243
			public bool EnableAutoReactive;

			// Token: 0x040000F4 RID: 244
			public ResourceView ColorOpaqueOnly;

			// Token: 0x040000F5 RID: 245
			public float AutoTcThreshold = 0.05f;

			// Token: 0x040000F6 RID: 246
			public float AutoTcScale = 1f;

			// Token: 0x040000F7 RID: 247
			public float AutoReactiveScale = 5f;

			// Token: 0x040000F8 RID: 248
			public float AutoReactiveMax = 0.9f;
		}

		// Token: 0x0200001E RID: 30
		public class GenerateReactiveDescription
		{
			// Token: 0x06000068 RID: 104 RVA: 0x00005A16 File Offset: 0x00003C16
			public GenerateReactiveDescription()
			{
			}

			// Token: 0x040000F9 RID: 249
			public ResourceView ColorOpaqueOnly;

			// Token: 0x040000FA RID: 250
			public ResourceView ColorPreUpscale;

			// Token: 0x040000FB RID: 251
			public ResourceView OutReactive;

			// Token: 0x040000FC RID: 252
			public Vector2Int RenderSize;

			// Token: 0x040000FD RID: 253
			public float Scale = 0.5f;

			// Token: 0x040000FE RID: 254
			public float CutoffThreshold = 0.2f;

			// Token: 0x040000FF RID: 255
			public float BinaryValue = 0.9f;

			// Token: 0x04000100 RID: 256
			public Fsr3.GenerateReactiveFlags Flags = Fsr3.GenerateReactiveFlags.ApplyTonemap | Fsr3.GenerateReactiveFlags.ApplyThreshold | Fsr3.GenerateReactiveFlags.UseComponentsMax;
		}

		// Token: 0x0200001F RID: 31
		[Flags]
		public enum GenerateReactiveFlags
		{
			// Token: 0x04000102 RID: 258
			ApplyTonemap = 1,
			// Token: 0x04000103 RID: 259
			ApplyInverseTonemap = 2,
			// Token: 0x04000104 RID: 260
			ApplyThreshold = 4,
			// Token: 0x04000105 RID: 261
			UseComponentsMax = 8
		}

		// Token: 0x02000020 RID: 32
		[Serializable]
		internal struct UpscalerConstants
		{
			// Token: 0x04000106 RID: 262
			public Vector2Int renderSize;

			// Token: 0x04000107 RID: 263
			public Vector2Int previousFrameRenderSize;

			// Token: 0x04000108 RID: 264
			public Vector2Int upscaleSize;

			// Token: 0x04000109 RID: 265
			public Vector2Int previousFrameUpscaleSize;

			// Token: 0x0400010A RID: 266
			public Vector2Int maxRenderSize;

			// Token: 0x0400010B RID: 267
			public Vector2Int maxUpscaleSize;

			// Token: 0x0400010C RID: 268
			public Vector4 deviceToViewDepth;

			// Token: 0x0400010D RID: 269
			public Vector2 jitterOffset;

			// Token: 0x0400010E RID: 270
			public Vector2 previousFrameJitterOffset;

			// Token: 0x0400010F RID: 271
			public Vector2 motionVectorScale;

			// Token: 0x04000110 RID: 272
			public Vector2 downscaleFactor;

			// Token: 0x04000111 RID: 273
			public Vector2 motionVectorJitterCancellation;

			// Token: 0x04000112 RID: 274
			public float tanHalfFOV;

			// Token: 0x04000113 RID: 275
			public float jitterPhaseCount;

			// Token: 0x04000114 RID: 276
			public float deltaTime;

			// Token: 0x04000115 RID: 277
			public float deltaPreExposure;

			// Token: 0x04000116 RID: 278
			public float viewSpaceToMetersFactor;

			// Token: 0x04000117 RID: 279
			public float frameIndex;
		}

		// Token: 0x02000021 RID: 33
		[Serializable]
		internal struct SpdConstants
		{
			// Token: 0x04000118 RID: 280
			public uint mips;

			// Token: 0x04000119 RID: 281
			public uint numWorkGroups;

			// Token: 0x0400011A RID: 282
			public uint workGroupOffsetX;

			// Token: 0x0400011B RID: 283
			public uint workGroupOffsetY;

			// Token: 0x0400011C RID: 284
			public uint renderSizeX;

			// Token: 0x0400011D RID: 285
			public uint renderSizeY;
		}

		// Token: 0x02000022 RID: 34
		[Serializable]
		internal struct GenerateReactiveConstants
		{
			// Token: 0x0400011E RID: 286
			public float scale;

			// Token: 0x0400011F RID: 287
			public float threshold;

			// Token: 0x04000120 RID: 288
			public float binaryValue;

			// Token: 0x04000121 RID: 289
			public uint flags;
		}

		// Token: 0x02000023 RID: 35
		[Serializable]
		internal struct GenerateReactiveConstants2
		{
			// Token: 0x04000122 RID: 290
			public float autoTcThreshold;

			// Token: 0x04000123 RID: 291
			public float autoTcScale;

			// Token: 0x04000124 RID: 292
			public float autoReactiveScale;

			// Token: 0x04000125 RID: 293
			public float autoReactiveMax;
		}

		// Token: 0x02000024 RID: 36
		[Serializable]
		internal struct RcasConstants
		{
			// Token: 0x06000069 RID: 105 RVA: 0x00005A48 File Offset: 0x00003C48
			public RcasConstants(uint sharpness, uint halfSharp)
			{
				this.sharpness = sharpness;
				this.halfSharp = halfSharp;
				this.dummy0 = (this.dummy1 = 0U);
			}

			// Token: 0x04000126 RID: 294
			public readonly uint sharpness;

			// Token: 0x04000127 RID: 295
			public readonly uint halfSharp;

			// Token: 0x04000128 RID: 296
			public readonly uint dummy0;

			// Token: 0x04000129 RID: 297
			public readonly uint dummy1;
		}
	}
}
