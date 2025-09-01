using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000099 RID: 153
	public static class TextureXR
	{
		// Token: 0x17000092 RID: 146
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0001737A File Offset: 0x0001557A
		public static int maxViews
		{
			set
			{
				TextureXR.m_MaxViews = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00017382 File Offset: 0x00015582
		public static int slices
		{
			get
			{
				return TextureXR.m_MaxViews;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001738C File Offset: 0x0001558C
		public static bool useTexArray
		{
			get
			{
				GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
				if (graphicsDeviceType <= GraphicsDeviceType.PlayStation4)
				{
					if (graphicsDeviceType != GraphicsDeviceType.Direct3D11 && graphicsDeviceType != GraphicsDeviceType.PlayStation4)
					{
						return false;
					}
				}
				else if (graphicsDeviceType != GraphicsDeviceType.Direct3D12 && graphicsDeviceType != GraphicsDeviceType.Vulkan && graphicsDeviceType - GraphicsDeviceType.PlayStation5 > 1)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000173C3 File Offset: 0x000155C3
		public static TextureDimension dimension
		{
			get
			{
				if (!TextureXR.useTexArray)
				{
					return TextureDimension.Tex2D;
				}
				return TextureDimension.Tex2DArray;
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000173CF File Offset: 0x000155CF
		public static RTHandle GetBlackUIntTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_BlackUIntTextureRTH;
			}
			return TextureXR.m_BlackUIntTexture2DArrayRTH;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000173E3 File Offset: 0x000155E3
		public static RTHandle GetClearTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_ClearTextureRTH;
			}
			return TextureXR.m_ClearTexture2DArrayRTH;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000173F7 File Offset: 0x000155F7
		public static RTHandle GetMagentaTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_MagentaTextureRTH;
			}
			return TextureXR.m_MagentaTexture2DArrayRTH;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001740B File Offset: 0x0001560B
		public static RTHandle GetBlackTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_BlackTextureRTH;
			}
			return TextureXR.m_BlackTexture2DArrayRTH;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001741F File Offset: 0x0001561F
		public static RTHandle GetBlackTextureArray()
		{
			return TextureXR.m_BlackTexture2DArrayRTH;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00017426 File Offset: 0x00015626
		public static RTHandle GetBlackTexture3D()
		{
			return TextureXR.m_BlackTexture3DRTH;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001742D File Offset: 0x0001562D
		public static RTHandle GetWhiteTexture()
		{
			if (!TextureXR.useTexArray)
			{
				return TextureXR.m_WhiteTextureRTH;
			}
			return TextureXR.m_WhiteTexture2DArrayRTH;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00017444 File Offset: 0x00015644
		public static void Initialize(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			if (TextureXR.m_BlackUIntTexture2DArray == null)
			{
				RTHandles.Release(TextureXR.m_BlackUIntTexture2DArrayRTH);
				TextureXR.m_BlackUIntTexture2DArray = TextureXR.CreateBlackUIntTextureArray(cmd, clearR32_UIntShader);
				TextureXR.m_BlackUIntTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_BlackUIntTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackUIntTextureRTH);
				TextureXR.m_BlackUIntTexture = TextureXR.CreateBlackUintTexture(cmd, clearR32_UIntShader);
				TextureXR.m_BlackUIntTextureRTH = RTHandles.Alloc(TextureXR.m_BlackUIntTexture);
				RTHandles.Release(TextureXR.m_ClearTextureRTH);
				TextureXR.m_ClearTexture = new Texture2D(1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None)
				{
					name = "Clear Texture"
				};
				TextureXR.m_ClearTexture.SetPixel(0, 0, Color.clear);
				TextureXR.m_ClearTexture.Apply();
				TextureXR.m_ClearTextureRTH = RTHandles.Alloc(TextureXR.m_ClearTexture);
				RTHandles.Release(TextureXR.m_ClearTexture2DArrayRTH);
				TextureXR.m_ClearTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_ClearTexture, "Clear Texture2DArray");
				TextureXR.m_ClearTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_ClearTexture2DArray);
				RTHandles.Release(TextureXR.m_MagentaTextureRTH);
				TextureXR.m_MagentaTexture = new Texture2D(1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None)
				{
					name = "Magenta Texture"
				};
				TextureXR.m_MagentaTexture.SetPixel(0, 0, Color.magenta);
				TextureXR.m_MagentaTexture.Apply();
				TextureXR.m_MagentaTextureRTH = RTHandles.Alloc(TextureXR.m_MagentaTexture);
				RTHandles.Release(TextureXR.m_MagentaTexture2DArrayRTH);
				TextureXR.m_MagentaTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_MagentaTexture, "Magenta Texture2DArray");
				TextureXR.m_MagentaTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_MagentaTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackTextureRTH);
				TextureXR.m_BlackTexture = new Texture2D(1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None)
				{
					name = "Black Texture"
				};
				TextureXR.m_BlackTexture.SetPixel(0, 0, Color.black);
				TextureXR.m_BlackTexture.Apply();
				TextureXR.m_BlackTextureRTH = RTHandles.Alloc(TextureXR.m_BlackTexture);
				RTHandles.Release(TextureXR.m_BlackTexture2DArrayRTH);
				TextureXR.m_BlackTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(TextureXR.m_BlackTexture, "Black Texture2DArray");
				TextureXR.m_BlackTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_BlackTexture2DArray);
				RTHandles.Release(TextureXR.m_BlackTexture3DRTH);
				TextureXR.m_BlackTexture3D = TextureXR.CreateBlackTexture3D("Black Texture3D");
				TextureXR.m_BlackTexture3DRTH = RTHandles.Alloc(TextureXR.m_BlackTexture3D);
				RTHandles.Release(TextureXR.m_WhiteTextureRTH);
				TextureXR.m_WhiteTextureRTH = RTHandles.Alloc(Texture2D.whiteTexture);
				RTHandles.Release(TextureXR.m_WhiteTexture2DArrayRTH);
				TextureXR.m_WhiteTexture2DArray = TextureXR.CreateTexture2DArrayFromTexture2D(Texture2D.whiteTexture, "White Texture2DArray");
				TextureXR.m_WhiteTexture2DArrayRTH = RTHandles.Alloc(TextureXR.m_WhiteTexture2DArray);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00017688 File Offset: 0x00015888
		private static Texture2DArray CreateTexture2DArrayFromTexture2D(Texture2D source, string name)
		{
			Texture2DArray texture2DArray = new Texture2DArray(source.width, source.height, TextureXR.slices, source.format, false)
			{
				name = name
			};
			for (int i = 0; i < TextureXR.slices; i++)
			{
				Graphics.CopyTexture(source, 0, 0, texture2DArray, i, 0);
			}
			return texture2DArray;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000176D8 File Offset: 0x000158D8
		private static Texture CreateBlackUIntTextureArray(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			RenderTexture renderTexture = new RenderTexture(1, 1, 0, GraphicsFormat.R32_UInt)
			{
				dimension = TextureDimension.Tex2DArray,
				volumeDepth = TextureXR.slices,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture Array"
			};
			renderTexture.Create();
			int kernelIndex = clearR32_UIntShader.FindKernel("ClearUIntTextureArray");
			cmd.SetComputeTextureParam(clearR32_UIntShader, kernelIndex, "_TargetArray", renderTexture);
			cmd.DispatchCompute(clearR32_UIntShader, kernelIndex, 1, 1, TextureXR.slices);
			return renderTexture;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00017758 File Offset: 0x00015958
		private static Texture CreateBlackUintTexture(CommandBuffer cmd, ComputeShader clearR32_UIntShader)
		{
			RenderTexture renderTexture = new RenderTexture(1, 1, 0, GraphicsFormat.R32_UInt)
			{
				dimension = TextureDimension.Tex2D,
				volumeDepth = TextureXR.slices,
				useMipMap = false,
				autoGenerateMips = false,
				enableRandomWrite = true,
				name = "Black UInt Texture Array"
			};
			renderTexture.Create();
			int kernelIndex = clearR32_UIntShader.FindKernel("ClearUIntTexture");
			cmd.SetComputeTextureParam(clearR32_UIntShader, kernelIndex, "_Target", renderTexture);
			cmd.DispatchCompute(clearR32_UIntShader, kernelIndex, 1, 1, TextureXR.slices);
			return renderTexture;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000177D8 File Offset: 0x000159D8
		private static Texture3D CreateBlackTexture3D(string name)
		{
			Texture3D texture3D = new Texture3D(1, 1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
			texture3D.name = name;
			texture3D.SetPixel(0, 0, 0, Color.black, 0);
			texture3D.Apply(false);
			return texture3D;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00017801 File Offset: 0x00015A01
		// Note: this type is marked as 'beforefieldinit'.
		static TextureXR()
		{
		}

		// Token: 0x04000330 RID: 816
		private static int m_MaxViews = 1;

		// Token: 0x04000331 RID: 817
		private static Texture m_BlackUIntTexture2DArray;

		// Token: 0x04000332 RID: 818
		private static Texture m_BlackUIntTexture;

		// Token: 0x04000333 RID: 819
		private static RTHandle m_BlackUIntTexture2DArrayRTH;

		// Token: 0x04000334 RID: 820
		private static RTHandle m_BlackUIntTextureRTH;

		// Token: 0x04000335 RID: 821
		private static Texture2DArray m_ClearTexture2DArray;

		// Token: 0x04000336 RID: 822
		private static Texture2D m_ClearTexture;

		// Token: 0x04000337 RID: 823
		private static RTHandle m_ClearTexture2DArrayRTH;

		// Token: 0x04000338 RID: 824
		private static RTHandle m_ClearTextureRTH;

		// Token: 0x04000339 RID: 825
		private static Texture2DArray m_MagentaTexture2DArray;

		// Token: 0x0400033A RID: 826
		private static Texture2D m_MagentaTexture;

		// Token: 0x0400033B RID: 827
		private static RTHandle m_MagentaTexture2DArrayRTH;

		// Token: 0x0400033C RID: 828
		private static RTHandle m_MagentaTextureRTH;

		// Token: 0x0400033D RID: 829
		private static Texture2D m_BlackTexture;

		// Token: 0x0400033E RID: 830
		private static Texture3D m_BlackTexture3D;

		// Token: 0x0400033F RID: 831
		private static Texture2DArray m_BlackTexture2DArray;

		// Token: 0x04000340 RID: 832
		private static RTHandle m_BlackTexture2DArrayRTH;

		// Token: 0x04000341 RID: 833
		private static RTHandle m_BlackTextureRTH;

		// Token: 0x04000342 RID: 834
		private static RTHandle m_BlackTexture3DRTH;

		// Token: 0x04000343 RID: 835
		private static Texture2DArray m_WhiteTexture2DArray;

		// Token: 0x04000344 RID: 836
		private static RTHandle m_WhiteTexture2DArrayRTH;

		// Token: 0x04000345 RID: 837
		private static RTHandle m_WhiteTextureRTH;
	}
}
