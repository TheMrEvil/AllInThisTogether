using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A9 RID: 169
	public static class FSRUtils
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x0001B2E4 File Offset: 0x000194E4
		public static void SetEasuConstants(CommandBuffer cmd, Vector2 inputViewportSizeInPixels, Vector2 inputImageSizeInPixels, Vector2 outputImageSizeInPixels)
		{
			Vector4 value;
			value.x = inputViewportSizeInPixels.x / outputImageSizeInPixels.x;
			value.y = inputViewportSizeInPixels.y / outputImageSizeInPixels.y;
			value.z = 0.5f * inputViewportSizeInPixels.x / outputImageSizeInPixels.x - 0.5f;
			value.w = 0.5f * inputViewportSizeInPixels.y / outputImageSizeInPixels.y - 0.5f;
			Vector4 value2;
			value2.x = 1f / inputImageSizeInPixels.x;
			value2.y = 1f / inputImageSizeInPixels.y;
			value2.z = 1f / inputImageSizeInPixels.x;
			value2.w = -1f / inputImageSizeInPixels.y;
			Vector4 value3;
			value3.x = -1f / inputImageSizeInPixels.x;
			value3.y = 2f / inputImageSizeInPixels.y;
			value3.z = 1f / inputImageSizeInPixels.x;
			value3.w = 2f / inputImageSizeInPixels.y;
			Vector4 value4;
			value4.x = 0f / inputImageSizeInPixels.x;
			value4.y = 4f / inputImageSizeInPixels.y;
			value4.z = 0f;
			value4.w = 0f;
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants0, value);
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants1, value2);
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants2, value3);
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants3, value4);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001B460 File Offset: 0x00019660
		public static void SetRcasConstants(CommandBuffer cmd, float sharpnessStops = 0.2f)
		{
			float num = Mathf.Pow(2f, -sharpnessStops);
			ushort num2 = Mathf.FloatToHalf(num);
			float y = BitConverter.Int32BitsToSingle((int)num2 | (int)num2 << 16);
			Vector4 value;
			value.x = num;
			value.y = y;
			value.z = 0f;
			value.w = 0f;
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrRcasConstants, value);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001B4C0 File Offset: 0x000196C0
		public static void SetRcasConstantsLinear(CommandBuffer cmd, float sharpnessLinear = 0.92f)
		{
			float sharpnessStops = (1f - sharpnessLinear) * 2.5f;
			FSRUtils.SetRcasConstants(cmd, sharpnessStops);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001B4E2 File Offset: 0x000196E2
		public static bool IsSupported()
		{
			return SystemInfo.graphicsShaderLevel >= 45;
		}

		// Token: 0x0400036D RID: 877
		internal const float kMaxSharpnessStops = 2.5f;

		// Token: 0x0400036E RID: 878
		public const float kDefaultSharpnessStops = 0.2f;

		// Token: 0x0400036F RID: 879
		public const float kDefaultSharpnessLinear = 0.92f;

		// Token: 0x02000178 RID: 376
		private static class ShaderConstants
		{
			// Token: 0x0600090C RID: 2316 RVA: 0x0002475C File Offset: 0x0002295C
			// Note: this type is marked as 'beforefieldinit'.
			static ShaderConstants()
			{
			}

			// Token: 0x040005A8 RID: 1448
			public static readonly int _FsrEasuConstants0 = Shader.PropertyToID("_FsrEasuConstants0");

			// Token: 0x040005A9 RID: 1449
			public static readonly int _FsrEasuConstants1 = Shader.PropertyToID("_FsrEasuConstants1");

			// Token: 0x040005AA RID: 1450
			public static readonly int _FsrEasuConstants2 = Shader.PropertyToID("_FsrEasuConstants2");

			// Token: 0x040005AB RID: 1451
			public static readonly int _FsrEasuConstants3 = Shader.PropertyToID("_FsrEasuConstants3");

			// Token: 0x040005AC RID: 1452
			public static readonly int _FsrRcasConstants = Shader.PropertyToID("_FsrRcasConstants");
		}
	}
}
