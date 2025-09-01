using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.RendererUtils;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A7 RID: 167
	public static class CoreUtils
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0001A330 File Offset: 0x00018530
		public static Cubemap blackCubeTexture
		{
			get
			{
				if (CoreUtils.m_BlackCubeTexture == null)
				{
					CoreUtils.m_BlackCubeTexture = new Cubemap(1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						CoreUtils.m_BlackCubeTexture.SetPixel((CubemapFace)i, 0, 0, Color.black);
					}
					CoreUtils.m_BlackCubeTexture.Apply();
				}
				return CoreUtils.m_BlackCubeTexture;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001A384 File Offset: 0x00018584
		public static Cubemap magentaCubeTexture
		{
			get
			{
				if (CoreUtils.m_MagentaCubeTexture == null)
				{
					CoreUtils.m_MagentaCubeTexture = new Cubemap(1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						CoreUtils.m_MagentaCubeTexture.SetPixel((CubemapFace)i, 0, 0, Color.magenta);
					}
					CoreUtils.m_MagentaCubeTexture.Apply();
				}
				return CoreUtils.m_MagentaCubeTexture;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0001A3D8 File Offset: 0x000185D8
		public static CubemapArray magentaCubeTextureArray
		{
			get
			{
				if (CoreUtils.m_MagentaCubeTextureArray == null)
				{
					CoreUtils.m_MagentaCubeTextureArray = new CubemapArray(1, 1, GraphicsFormat.R32G32B32A32_SFloat, TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						Color[] colors = new Color[]
						{
							Color.magenta
						};
						CoreUtils.m_MagentaCubeTextureArray.SetPixels(colors, (CubemapFace)i, 0);
					}
					CoreUtils.m_MagentaCubeTextureArray.Apply();
				}
				return CoreUtils.m_MagentaCubeTextureArray;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001A43C File Offset: 0x0001863C
		public static Cubemap whiteCubeTexture
		{
			get
			{
				if (CoreUtils.m_WhiteCubeTexture == null)
				{
					CoreUtils.m_WhiteCubeTexture = new Cubemap(1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
					for (int i = 0; i < 6; i++)
					{
						CoreUtils.m_WhiteCubeTexture.SetPixel((CubemapFace)i, 0, 0, Color.white);
					}
					CoreUtils.m_WhiteCubeTexture.Apply();
				}
				return CoreUtils.m_WhiteCubeTexture;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001A490 File Offset: 0x00018690
		public static RenderTexture emptyUAV
		{
			get
			{
				if (CoreUtils.m_EmptyUAV == null)
				{
					CoreUtils.m_EmptyUAV = new RenderTexture(1, 1, 0);
					CoreUtils.m_EmptyUAV.enableRandomWrite = true;
					CoreUtils.m_EmptyUAV.Create();
				}
				return CoreUtils.m_EmptyUAV;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001A4C8 File Offset: 0x000186C8
		public static Texture3D blackVolumeTexture
		{
			get
			{
				if (CoreUtils.m_BlackVolumeTexture == null)
				{
					Color[] colors = new Color[]
					{
						Color.black
					};
					CoreUtils.m_BlackVolumeTexture = new Texture3D(1, 1, 1, GraphicsFormat.R8G8B8A8_SRGB, TextureCreationFlags.None);
					CoreUtils.m_BlackVolumeTexture.SetPixels(colors, 0);
					CoreUtils.m_BlackVolumeTexture.Apply();
				}
				return CoreUtils.m_BlackVolumeTexture;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001A51F File Offset: 0x0001871F
		public static void ClearRenderTarget(CommandBuffer cmd, ClearFlag clearFlag, Color clearColor)
		{
			if (clearFlag != ClearFlag.None)
			{
				cmd.ClearRenderTarget((RTClearFlags)clearFlag, clearColor, 1f, 0U);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001A532 File Offset: 0x00018732
		private static int FixupDepthSlice(int depthSlice, RTHandle buffer)
		{
			if (depthSlice == -1)
			{
				RenderTexture rt = buffer.rt;
				if (rt != null && rt.dimension == TextureDimension.Cube)
				{
					depthSlice = 0;
				}
			}
			return depthSlice;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001A553 File Offset: 0x00018753
		private static int FixupDepthSlice(int depthSlice, CubemapFace cubemapFace)
		{
			if (depthSlice == -1 && cubemapFace != CubemapFace.Unknown)
			{
				depthSlice = 0;
			}
			return depthSlice;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001A561 File Offset: 0x00018761
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			cmd.SetRenderTarget(buffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001A583 File Offset: 0x00018783
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, ClearFlag clearFlag = ClearFlag.None, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, buffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001A597 File Offset: 0x00018797
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthBuffer, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, ClearFlag.None, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001A5AC File Offset: 0x000187AC
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001A5C2 File Offset: 0x000187C2
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, cubemapFace);
			cmd.SetRenderTarget(colorBuffer, depthBuffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001A5E6 File Offset: 0x000187E6
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthBuffer)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer, ClearFlag.None, Color.clear);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001A5F6 File Offset: 0x000187F6
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag = ClearFlag.None)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer, clearFlag, Color.clear);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001A606 File Offset: 0x00018806
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthBuffer, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, -1);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001A61C File Offset: 0x0001881C
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(buffer, loadAction, storeAction);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001A631 File Offset: 0x00018831
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction loadAction, RenderBufferStoreAction storeAction, ClearFlag clearFlag)
		{
			CoreUtils.SetRenderTarget(cmd, buffer, loadAction, storeAction, clearFlag, Color.clear);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001A643 File Offset: 0x00018843
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthBuffer, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001A65E File Offset: 0x0001885E
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier buffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(buffer, colorLoadAction, colorStoreAction, depthLoadAction, depthStoreAction);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001A678 File Offset: 0x00018878
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier colorBuffer, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthBuffer, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction, ClearFlag clearFlag)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffer, colorLoadAction, colorStoreAction, depthBuffer, depthLoadAction, depthStoreAction, clearFlag, Color.clear);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001A69B File Offset: 0x0001889B
		private static void SetViewportAndClear(CommandBuffer cmd, RTHandle buffer, ClearFlag clearFlag, Color clearColor)
		{
			CoreUtils.SetViewport(cmd, buffer);
			CoreUtils.ClearRenderTarget(cmd, clearFlag, clearColor);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001A6AC File Offset: 0x000188AC
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle buffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			depthSlice = CoreUtils.FixupDepthSlice(depthSlice, buffer);
			cmd.SetRenderTarget(buffer, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetViewportAndClear(cmd, buffer, clearFlag, clearColor);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001A6D3 File Offset: 0x000188D3
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle buffer, ClearFlag clearFlag = ClearFlag.None, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(cmd, buffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001A6E8 File Offset: 0x000188E8
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RTHandle depthBuffer, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			int width = colorBuffer.rt.width;
			int height = colorBuffer.rt.height;
			int width2 = depthBuffer.rt.width;
			int height2 = depthBuffer.rt.height;
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, ClearFlag.None, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0001A738 File Offset: 0x00018938
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RTHandle depthBuffer, ClearFlag clearFlag, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			int width = colorBuffer.rt.width;
			int height = colorBuffer.rt.height;
			int width2 = depthBuffer.rt.width;
			int height2 = depthBuffer.rt.height;
			CoreUtils.SetRenderTarget(cmd, colorBuffer, depthBuffer, clearFlag, Color.clear, miplevel, cubemapFace, depthSlice);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001A78C File Offset: 0x0001898C
		public static void SetRenderTarget(CommandBuffer cmd, RTHandle colorBuffer, RTHandle depthBuffer, ClearFlag clearFlag, Color clearColor, int miplevel = 0, CubemapFace cubemapFace = CubemapFace.Unknown, int depthSlice = -1)
		{
			int width = colorBuffer.rt.width;
			int height = colorBuffer.rt.height;
			int width2 = depthBuffer.rt.width;
			int height2 = depthBuffer.rt.height;
			CoreUtils.SetRenderTarget(cmd, colorBuffer.rt, depthBuffer.rt, miplevel, cubemapFace, depthSlice);
			CoreUtils.SetViewportAndClear(cmd, colorBuffer, clearFlag, clearColor);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001A7F5 File Offset: 0x000189F5
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RTHandle depthBuffer)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer.rt, ClearFlag.None, Color.clear);
			CoreUtils.SetViewport(cmd, depthBuffer);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0001A816 File Offset: 0x00018A16
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RTHandle depthBuffer, ClearFlag clearFlag = ClearFlag.None)
		{
			CoreUtils.SetRenderTarget(cmd, colorBuffers, depthBuffer.rt);
			CoreUtils.SetViewportAndClear(cmd, depthBuffer, clearFlag, Color.clear);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001A837 File Offset: 0x00018A37
		public static void SetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier[] colorBuffers, RTHandle depthBuffer, ClearFlag clearFlag, Color clearColor)
		{
			cmd.SetRenderTarget(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, -1);
			CoreUtils.SetViewportAndClear(cmd, depthBuffer, clearFlag, clearColor);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001A854 File Offset: 0x00018A54
		public static void SetViewport(CommandBuffer cmd, RTHandle target)
		{
			if (target.useScaling)
			{
				Vector2Int scaledSize = target.GetScaledSize(target.rtHandleProperties.currentViewportSize);
				cmd.SetViewport(new Rect(0f, 0f, (float)scaledSize.x, (float)scaledSize.y));
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001A8A0 File Offset: 0x00018AA0
		public static string GetRenderTargetAutoName(int width, int height, int depth, RenderTextureFormat format, string name, bool mips = false, bool enableMSAA = false, MSAASamples msaaSamples = MSAASamples.None)
		{
			return CoreUtils.GetRenderTargetAutoName(width, height, depth, format.ToString(), TextureDimension.None, name, mips, enableMSAA, msaaSamples, false);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001A8CC File Offset: 0x00018ACC
		public static string GetRenderTargetAutoName(int width, int height, int depth, GraphicsFormat format, string name, bool mips = false, bool enableMSAA = false, MSAASamples msaaSamples = MSAASamples.None)
		{
			return CoreUtils.GetRenderTargetAutoName(width, height, depth, format.ToString(), TextureDimension.None, name, mips, enableMSAA, msaaSamples, false);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001A8F8 File Offset: 0x00018AF8
		public static string GetRenderTargetAutoName(int width, int height, int depth, GraphicsFormat format, TextureDimension dim, string name, bool mips = false, bool enableMSAA = false, MSAASamples msaaSamples = MSAASamples.None, bool dynamicRes = false)
		{
			return CoreUtils.GetRenderTargetAutoName(width, height, depth, format.ToString(), dim, name, mips, enableMSAA, msaaSamples, dynamicRes);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001A928 File Offset: 0x00018B28
		private static string GetRenderTargetAutoName(int width, int height, int depth, string format, TextureDimension dim, string name, bool mips, bool enableMSAA, MSAASamples msaaSamples, bool dynamicRes)
		{
			string text = string.Format("{0}_{1}x{2}", name, width, height);
			if (depth > 1)
			{
				text = string.Format("{0}x{1}", text, depth);
			}
			if (mips)
			{
				text = string.Format("{0}_{1}", text, "Mips");
			}
			text = string.Format("{0}_{1}", text, format);
			if (dim != TextureDimension.None)
			{
				text = string.Format("{0}_{1}", text, dim);
			}
			if (enableMSAA)
			{
				text = string.Format("{0}_{1}", text, msaaSamples.ToString());
			}
			if (dynamicRes)
			{
				text = string.Format("{0}_{1}", text, "dynamic");
			}
			return text;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001A9D0 File Offset: 0x00018BD0
		public static string GetTextureAutoName(int width, int height, TextureFormat format, TextureDimension dim = TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			return CoreUtils.GetTextureAutoName(width, height, format.ToString(), dim, name, mips, depth);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001A9ED File Offset: 0x00018BED
		public static string GetTextureAutoName(int width, int height, GraphicsFormat format, TextureDimension dim = TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			return CoreUtils.GetTextureAutoName(width, height, format.ToString(), dim, name, mips, depth);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001AA0C File Offset: 0x00018C0C
		private static string GetTextureAutoName(int width, int height, string format, TextureDimension dim = TextureDimension.None, string name = "", bool mips = false, int depth = 0)
		{
			string arg;
			if (depth == 0)
			{
				arg = string.Format("{0}x{1}{2}_{3}", new object[]
				{
					width,
					height,
					mips ? "_Mips" : "",
					format
				});
			}
			else
			{
				arg = string.Format("{0}x{1}x{2}{3}_{4}", new object[]
				{
					width,
					height,
					depth,
					mips ? "_Mips" : "",
					format
				});
			}
			return string.Format("{0}_{1}_{2}", (name == "") ? "Texture" : name, (dim == TextureDimension.None) ? "" : dim.ToString(), arg);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001AAD8 File Offset: 0x00018CD8
		public static void ClearCubemap(CommandBuffer cmd, RenderTexture renderTexture, Color clearColor, bool clearMips = false)
		{
			int num = 1;
			if (renderTexture.useMipMap && clearMips)
			{
				num = (int)Mathf.Log((float)renderTexture.width, 2f) + 1;
			}
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < num; j++)
				{
					CoreUtils.SetRenderTarget(cmd, new RenderTargetIdentifier(renderTexture), ClearFlag.Color, clearColor, j, (CubemapFace)i, -1);
				}
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001AB2F File Offset: 0x00018D2F
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001AB42 File Offset: 0x00018D42
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier colorBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffer, 0, CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001AB60 File Offset: 0x00018D60
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier colorBuffer, RenderTargetIdentifier depthStencilBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffer, depthStencilBuffer, 0, CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001AB80 File Offset: 0x00018D80
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier[] colorBuffers, RenderTargetIdentifier depthStencilBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			commandBuffer.SetRenderTarget(colorBuffers, depthStencilBuffer, 0, CubemapFace.Unknown, -1);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier[] colorBuffers, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			CoreUtils.DrawFullScreen(commandBuffer, material, colorBuffers, colorBuffers[0], properties, shaderPassId);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001ABB4 File Offset: 0x00018DB4
		public static Color ConvertSRGBToActiveColorSpace(Color color)
		{
			if (QualitySettings.activeColorSpace != ColorSpace.Linear)
			{
				return color;
			}
			return color.linear;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001ABC7 File Offset: 0x00018DC7
		public static Color ConvertLinearToActiveColorSpace(Color color)
		{
			if (QualitySettings.activeColorSpace != ColorSpace.Linear)
			{
				return color.gamma;
			}
			return color;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001ABDC File Offset: 0x00018DDC
		public static Material CreateEngineMaterial(string shaderPath)
		{
			Shader shader = Shader.Find(shaderPath);
			if (shader == null)
			{
				Debug.LogError("Cannot create required material because shader " + shaderPath + " could not be found");
				return null;
			}
			return new Material(shader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001AC1E File Offset: 0x00018E1E
		public static Material CreateEngineMaterial(Shader shader)
		{
			if (shader == null)
			{
				Debug.LogError("Cannot create required material because shader is null");
				return null;
			}
			return new Material(shader)
			{
				hideFlags = HideFlags.HideAndDontSave
			};
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001AC43 File Offset: 0x00018E43
		public static bool HasFlag<T>(T mask, T flag) where T : IConvertible
		{
			return (mask.ToUInt32(null) & flag.ToUInt32(null)) > 0U;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001AC68 File Offset: 0x00018E68
		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001AC8F File Offset: 0x00018E8F
		public static void SetKeyword(CommandBuffer cmd, string keyword, bool state)
		{
			if (state)
			{
				cmd.EnableShaderKeyword(keyword);
				return;
			}
			cmd.DisableShaderKeyword(keyword);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001ACA3 File Offset: 0x00018EA3
		public static void SetKeyword(Material material, string keyword, bool state)
		{
			if (state)
			{
				material.EnableKeyword(keyword);
				return;
			}
			material.DisableKeyword(keyword);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001ACB7 File Offset: 0x00018EB7
		public static void SetKeyword(ComputeShader cs, string keyword, bool state)
		{
			if (state)
			{
				cs.EnableKeyword(keyword);
				return;
			}
			cs.DisableKeyword(keyword);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001ACCB File Offset: 0x00018ECB
		public static void Destroy(Object obj)
		{
			if (obj != null)
			{
				Object.Destroy(obj);
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001ACDC File Offset: 0x00018EDC
		public static IEnumerable<Type> GetAllAssemblyTypes()
		{
			if (CoreUtils.m_AssemblyTypes == null)
			{
				CoreUtils.m_AssemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(delegate(Assembly t)
				{
					Type[] result = new Type[0];
					try
					{
						result = t.GetTypes();
					}
					catch
					{
					}
					return result;
				});
			}
			return CoreUtils.m_AssemblyTypes;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001AD28 File Offset: 0x00018F28
		public static IEnumerable<Type> GetAllTypesDerivedFrom<T>()
		{
			return from t in CoreUtils.GetAllAssemblyTypes()
			where t.IsSubclassOf(typeof(T))
			select t;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001AD53 File Offset: 0x00018F53
		public static void SafeRelease(ComputeBuffer buffer)
		{
			if (buffer != null)
			{
				buffer.Release();
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001AD60 File Offset: 0x00018F60
		public static Mesh CreateCubeMesh(Vector3 min, Vector3 max)
		{
			return new Mesh
			{
				vertices = new Vector3[]
				{
					new Vector3(min.x, min.y, min.z),
					new Vector3(max.x, min.y, min.z),
					new Vector3(max.x, max.y, min.z),
					new Vector3(min.x, max.y, min.z),
					new Vector3(min.x, min.y, max.z),
					new Vector3(max.x, min.y, max.z),
					new Vector3(max.x, max.y, max.z),
					new Vector3(min.x, max.y, max.z)
				},
				triangles = new int[]
				{
					0,
					2,
					1,
					0,
					3,
					2,
					1,
					6,
					5,
					1,
					2,
					6,
					5,
					7,
					4,
					5,
					6,
					7,
					4,
					3,
					0,
					4,
					7,
					3,
					3,
					6,
					2,
					3,
					7,
					6,
					4,
					1,
					5,
					4,
					0,
					1
				}
			};
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001AF2A File Offset: 0x0001912A
		public static bool ArePostProcessesEnabled(Camera camera)
		{
			return true;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001AF2D File Offset: 0x0001912D
		public static bool AreAnimatedMaterialsEnabled(Camera camera)
		{
			return true;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001AF30 File Offset: 0x00019130
		public static bool IsSceneLightingDisabled(Camera camera)
		{
			return false;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001AF33 File Offset: 0x00019133
		public static bool IsLightOverlapDebugEnabled(Camera camera)
		{
			return false;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001AF36 File Offset: 0x00019136
		public static bool IsSceneViewFogEnabled(Camera camera)
		{
			return true;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001AF39 File Offset: 0x00019139
		public static bool IsSceneFilteringEnabled()
		{
			return false;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001AF3C File Offset: 0x0001913C
		public static bool IsSceneViewPrefabStageContextHidden()
		{
			return false;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001AF40 File Offset: 0x00019140
		[Obsolete("Use the updated RendererList API in the UnityEngine.Rendering.RendererUtils namespace.")]
		public static void DrawRendererList(ScriptableRenderContext renderContext, CommandBuffer cmd, UnityEngine.Experimental.Rendering.RendererList rendererList)
		{
			if (!rendererList.isValid)
			{
				throw new ArgumentException("Invalid renderer list provided to DrawRendererList");
			}
			renderContext.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			if (rendererList.stateBlock == null)
			{
				renderContext.DrawRenderers(rendererList.cullingResult, ref rendererList.drawSettings, ref rendererList.filteringSettings);
				return;
			}
			RenderStateBlock value = rendererList.stateBlock.Value;
			renderContext.DrawRenderers(rendererList.cullingResult, ref rendererList.drawSettings, ref rendererList.filteringSettings, ref value);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001AFC3 File Offset: 0x000191C3
		public static void DrawRendererList(ScriptableRenderContext renderContext, CommandBuffer cmd, UnityEngine.Rendering.RendererUtils.RendererList rendererList)
		{
			if (!rendererList.isValid)
			{
				throw new ArgumentException("Invalid renderer list provided to DrawRendererList");
			}
			cmd.DrawRendererList(rendererList);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001AFE0 File Offset: 0x000191E0
		public static int GetTextureHash(Texture texture)
		{
			int num = texture.GetHashCode();
			num = 23 * num + texture.GetInstanceID().GetHashCode();
			num = 23 * num + texture.graphicsFormat.GetHashCode();
			num = 23 * num + texture.wrapMode.GetHashCode();
			num = 23 * num + texture.width.GetHashCode();
			num = 23 * num + texture.height.GetHashCode();
			num = 23 * num + texture.filterMode.GetHashCode();
			num = 23 * num + texture.anisoLevel.GetHashCode();
			num = 23 * num + texture.mipmapCount.GetHashCode();
			return 23 * num + texture.updateCount.GetHashCode();
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001B0BD File Offset: 0x000192BD
		public static int PreviousPowerOfTwo(int size)
		{
			if (size <= 0)
			{
				return 0;
			}
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			return size - (size >> 1);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001B0EE File Offset: 0x000192EE
		public static T GetLastEnumValue<T>() where T : Enum
		{
			return typeof(T).GetEnumValues().Cast<T>().Last<T>();
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001B109 File Offset: 0x00019309
		internal static string GetCorePath()
		{
			return "Packages/com.unity.render-pipelines.core/";
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001B110 File Offset: 0x00019310
		// Note: this type is marked as 'beforefieldinit'.
		static CoreUtils()
		{
		}

		// Token: 0x0400035B RID: 859
		public static readonly Vector3[] lookAtList = new Vector3[]
		{
			new Vector3(1f, 0f, 0f),
			new Vector3(-1f, 0f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, -1f, 0f),
			new Vector3(0f, 0f, 1f),
			new Vector3(0f, 0f, -1f)
		};

		// Token: 0x0400035C RID: 860
		public static readonly Vector3[] upVectorList = new Vector3[]
		{
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, 1f),
			new Vector3(0f, 1f, 0f),
			new Vector3(0f, 1f, 0f)
		};

		// Token: 0x0400035D RID: 861
		private const string obsoletePriorityMessage = "Use CoreUtils.Priorities instead";

		// Token: 0x0400035E RID: 862
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int editMenuPriority1 = 320;

		// Token: 0x0400035F RID: 863
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int editMenuPriority2 = 331;

		// Token: 0x04000360 RID: 864
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int editMenuPriority3 = 342;

		// Token: 0x04000361 RID: 865
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int editMenuPriority4 = 353;

		// Token: 0x04000362 RID: 866
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int assetCreateMenuPriority1 = 230;

		// Token: 0x04000363 RID: 867
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int assetCreateMenuPriority2 = 241;

		// Token: 0x04000364 RID: 868
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int assetCreateMenuPriority3 = 300;

		// Token: 0x04000365 RID: 869
		[Obsolete("Use CoreUtils.Priorities instead", false)]
		public const int gameObjectMenuPriority = 10;

		// Token: 0x04000366 RID: 870
		private static Cubemap m_BlackCubeTexture;

		// Token: 0x04000367 RID: 871
		private static Cubemap m_MagentaCubeTexture;

		// Token: 0x04000368 RID: 872
		private static CubemapArray m_MagentaCubeTextureArray;

		// Token: 0x04000369 RID: 873
		private static Cubemap m_WhiteCubeTexture;

		// Token: 0x0400036A RID: 874
		private static RenderTexture m_EmptyUAV;

		// Token: 0x0400036B RID: 875
		private static Texture3D m_BlackVolumeTexture;

		// Token: 0x0400036C RID: 876
		private static IEnumerable<Type> m_AssemblyTypes;

		// Token: 0x02000174 RID: 372
		public static class Sections
		{
			// Token: 0x04000597 RID: 1431
			public const int section1 = 10000;

			// Token: 0x04000598 RID: 1432
			public const int section2 = 20000;

			// Token: 0x04000599 RID: 1433
			public const int section3 = 30000;

			// Token: 0x0400059A RID: 1434
			public const int section4 = 40000;

			// Token: 0x0400059B RID: 1435
			public const int section5 = 50000;

			// Token: 0x0400059C RID: 1436
			public const int section6 = 60000;

			// Token: 0x0400059D RID: 1437
			public const int section7 = 70000;

			// Token: 0x0400059E RID: 1438
			public const int section8 = 80000;
		}

		// Token: 0x02000175 RID: 373
		public static class Priorities
		{
			// Token: 0x0400059F RID: 1439
			public const int assetsCreateShaderMenuPriority = 83;

			// Token: 0x040005A0 RID: 1440
			public const int assetsCreateRenderingMenuPriority = 308;

			// Token: 0x040005A1 RID: 1441
			public const int editMenuPriority = 320;

			// Token: 0x040005A2 RID: 1442
			public const int gameObjectMenuPriority = 10;

			// Token: 0x040005A3 RID: 1443
			public const int srpLensFlareMenuPriority = 303;
		}

		// Token: 0x02000176 RID: 374
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000906 RID: 2310 RVA: 0x000246E9 File Offset: 0x000228E9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000907 RID: 2311 RVA: 0x000246F5 File Offset: 0x000228F5
			public <>c()
			{
			}

			// Token: 0x06000908 RID: 2312 RVA: 0x00024700 File Offset: 0x00022900
			internal IEnumerable<Type> <GetAllAssemblyTypes>b__81_0(Assembly t)
			{
				Type[] result = new Type[0];
				try
				{
					result = t.GetTypes();
				}
				catch
				{
				}
				return result;
			}

			// Token: 0x040005A4 RID: 1444
			public static readonly CoreUtils.<>c <>9 = new CoreUtils.<>c();

			// Token: 0x040005A5 RID: 1445
			public static Func<Assembly, IEnumerable<Type>> <>9__81_0;
		}

		// Token: 0x02000177 RID: 375
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__82<T>
		{
			// Token: 0x06000909 RID: 2313 RVA: 0x00024734 File Offset: 0x00022934
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__82()
			{
			}

			// Token: 0x0600090A RID: 2314 RVA: 0x00024740 File Offset: 0x00022940
			public <>c__82()
			{
			}

			// Token: 0x0600090B RID: 2315 RVA: 0x00024748 File Offset: 0x00022948
			internal bool <GetAllTypesDerivedFrom>b__82_0(Type t)
			{
				return t.IsSubclassOf(typeof(T));
			}

			// Token: 0x040005A6 RID: 1446
			public static readonly CoreUtils.<>c__82<T> <>9 = new CoreUtils.<>c__82<T>();

			// Token: 0x040005A7 RID: 1447
			public static Func<Type, bool> <>9__82_0;
		}
	}
}
