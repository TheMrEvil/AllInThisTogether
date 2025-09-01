using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A3 RID: 163
	public static class Blitter
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x0001898C File Offset: 0x00016B8C
		public static void Initialize(Shader blitPS, Shader blitColorAndDepthPS)
		{
			if (Blitter.s_Blit != null)
			{
				return;
			}
			Blitter.s_Blit = CoreUtils.CreateEngineMaterial(blitPS);
			Blitter.s_BlitColorAndDepth = CoreUtils.CreateEngineMaterial(blitColorAndDepthPS);
			if (TextureXR.useTexArray)
			{
				Blitter.s_Blit.EnableKeyword("DISABLE_TEXTURE2D_X_ARRAY");
				Blitter.s_BlitTexArray = CoreUtils.CreateEngineMaterial(blitPS);
				Blitter.s_BlitTexArraySingleSlice = CoreUtils.CreateEngineMaterial(blitPS);
				Blitter.s_BlitTexArraySingleSlice.EnableKeyword("BLIT_SINGLE_SLICE");
			}
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				float z = -1f;
				if (SystemInfo.usesReversedZBuffer)
				{
					z = 1f;
				}
				if (!Blitter.s_TriangleMesh)
				{
					Blitter.s_TriangleMesh = new Mesh();
					Blitter.s_TriangleMesh.vertices = Blitter.<Initialize>g__GetFullScreenTriangleVertexPosition|8_0(z);
					Blitter.s_TriangleMesh.uv = Blitter.<Initialize>g__GetFullScreenTriangleTexCoord|8_1();
					Blitter.s_TriangleMesh.triangles = new int[]
					{
						0,
						1,
						2
					};
				}
				if (!Blitter.s_QuadMesh)
				{
					Blitter.s_QuadMesh = new Mesh();
					Blitter.s_QuadMesh.vertices = Blitter.<Initialize>g__GetQuadVertexPosition|8_2(z);
					Blitter.s_QuadMesh.uv = Blitter.<Initialize>g__GetQuadTexCoord|8_3();
					Blitter.s_QuadMesh.triangles = new int[]
					{
						0,
						1,
						2,
						0,
						2,
						3
					};
				}
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00018AB4 File Offset: 0x00016CB4
		public static void Cleanup()
		{
			CoreUtils.Destroy(Blitter.s_Blit);
			Blitter.s_Blit = null;
			CoreUtils.Destroy(Blitter.s_BlitColorAndDepth);
			Blitter.s_BlitColorAndDepth = null;
			CoreUtils.Destroy(Blitter.s_BlitTexArray);
			Blitter.s_BlitTexArray = null;
			CoreUtils.Destroy(Blitter.s_BlitTexArraySingleSlice);
			Blitter.s_BlitTexArraySingleSlice = null;
			CoreUtils.Destroy(Blitter.s_TriangleMesh);
			Blitter.s_TriangleMesh = null;
			CoreUtils.Destroy(Blitter.s_QuadMesh);
			Blitter.s_QuadMesh = null;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00018B21 File Offset: 0x00016D21
		public static Material GetBlitMaterial(TextureDimension dimension, bool singleSlice = false)
		{
			if (dimension != TextureDimension.Tex2DArray)
			{
				return Blitter.s_Blit;
			}
			if (!singleSlice)
			{
				return Blitter.s_BlitTexArray;
			}
			return Blitter.s_BlitTexArraySingleSlice;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00018B3D File Offset: 0x00016D3D
		private static void DrawTriangle(CommandBuffer cmd, Material material, int shaderPass)
		{
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				cmd.DrawMesh(Blitter.s_TriangleMesh, Matrix4x4.identity, material, 0, shaderPass, Blitter.s_PropertyBlock);
				return;
			}
			cmd.DrawProcedural(Matrix4x4.identity, material, shaderPass, MeshTopology.Triangles, 3, 1, Blitter.s_PropertyBlock);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00018B76 File Offset: 0x00016D76
		internal static void DrawQuad(CommandBuffer cmd, Material material, int shaderPass)
		{
			if (SystemInfo.graphicsShaderLevel < 30)
			{
				cmd.DrawMesh(Blitter.s_QuadMesh, Matrix4x4.identity, material, 0, shaderPass, Blitter.s_PropertyBlock);
				return;
			}
			cmd.DrawProcedural(Matrix4x4.identity, material, shaderPass, MeshTopology.Quads, 4, 1, Blitter.s_PropertyBlock);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00018BAF File Offset: 0x00016DAF
		public static void BlitTexture(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			Blitter.BlitTexture(cmd, source, scaleBias, Blitter.GetBlitMaterial(TextureXR.dimension, false), bilinear ? 1 : 0);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00018BDC File Offset: 0x00016DDC
		public static void BlitTexture2D(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			Blitter.BlitTexture(cmd, source, scaleBias, Blitter.GetBlitMaterial(TextureDimension.Tex2D, false), bilinear ? 1 : 0);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00018C08 File Offset: 0x00016E08
		public static void BlitColorAndDepth(CommandBuffer cmd, Texture sourceColor, RenderTexture sourceDepth, Vector4 scaleBias, float mipLevel, bool blitDepth)
		{
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, mipLevel);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, sourceColor);
			if (blitDepth)
			{
				Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._InputDepth, sourceDepth, RenderTextureSubElement.Depth);
			}
			Blitter.DrawTriangle(cmd, Blitter.s_BlitColorAndDepth, blitDepth ? 1 : 0);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00018C6E File Offset: 0x00016E6E
		public static void BlitTexture(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, Material material, int pass)
		{
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBias);
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.DrawTriangle(cmd, material, pass);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00018CA0 File Offset: 0x00016EA0
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 v = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, v, mipLevel, bilinear);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00018CF0 File Offset: 0x00016EF0
		public static void BlitCameraTexture2D(CommandBuffer cmd, RTHandle source, RTHandle destination, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 v = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture2D(cmd, source, v, mipLevel, bilinear);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00018D40 File Offset: 0x00016F40
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Material material, int pass)
		{
			Vector2 v = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, v, material, pass);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00018D8F File Offset: 0x00016F8F
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Vector4 scaleBias, float mipLevel = 0f, bool bilinear = false)
		{
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			Blitter.BlitTexture(cmd, source, scaleBias, mipLevel, bilinear);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00018DA8 File Offset: 0x00016FA8
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Rect destViewport, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 v = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			cmd.SetViewport(destViewport);
			Blitter.BlitTexture(cmd, source, v, mipLevel, bilinear);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00018E00 File Offset: 0x00017000
		public static void BlitQuad(CommandBuffer cmd, Texture source, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 3 : 2);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00018E6C File Offset: 0x0001706C
		public static void BlitQuadWithPadding(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			if (source.wrapMode == TextureWrapMode.Repeat)
			{
				Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 7 : 6);
				return;
			}
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 5 : 4);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00018F20 File Offset: 0x00017120
		public static void BlitQuadWithPaddingMultiply(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			if (source.wrapMode == TextureWrapMode.Repeat)
			{
				Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 12 : 11);
				return;
			}
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), bilinear ? 10 : 9);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00018FD8 File Offset: 0x000171D8
		public static void BlitOctahedralWithPadding(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), 8);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00019064 File Offset: 0x00017264
		public static void BlitOctahedralWithPaddingMultiply(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitTextureSize, textureSize);
			Blitter.s_PropertyBlock.SetInt(Blitter.BlitShaderIDs._BlitPaddingSize, paddingInPixels);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), 13);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000190F0 File Offset: 0x000172F0
		public static void BlitCubeToOctahedral2DQuad(CommandBuffer cmd, Texture source, Vector4 scaleBiasRT, int mipLevelTex)
		{
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitCubeTexture, source);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, new Vector4(1f, 1f, 0f, 0f));
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), 14);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001916C File Offset: 0x0001736C
		public static void BlitCubeToOctahedral2DQuadSingleChannel(CommandBuffer cmd, Texture source, Vector4 scaleBiasRT, int mipLevelTex)
		{
			int shaderPass = 15;
			if (GraphicsFormatUtility.GetComponentCount(source.graphicsFormat) == 1U)
			{
				if (GraphicsFormatUtility.IsAlphaOnlyFormat(source.graphicsFormat))
				{
					shaderPass = 16;
				}
				if (GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) == FormatSwizzle.FormatSwizzleR)
				{
					shaderPass = 17;
				}
			}
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitCubeTexture, source);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, new Vector4(1f, 1f, 0f, 0f));
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), shaderPass);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00019218 File Offset: 0x00017418
		public static void BlitQuadSingleChannel(CommandBuffer cmd, Texture source, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex)
		{
			int shaderPass = 18;
			if (GraphicsFormatUtility.GetComponentCount(source.graphicsFormat) == 1U)
			{
				if (GraphicsFormatUtility.IsAlphaOnlyFormat(source.graphicsFormat))
				{
					shaderPass = 19;
				}
				if (GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) == FormatSwizzle.FormatSwizzleR)
				{
					shaderPass = 20;
				}
			}
			Blitter.s_PropertyBlock.SetTexture(Blitter.BlitShaderIDs._BlitTexture, source);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBias, scaleBiasTex);
			Blitter.s_PropertyBlock.SetVector(Blitter.BlitShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			Blitter.s_PropertyBlock.SetFloat(Blitter.BlitShaderIDs._BlitMipLevel, (float)mipLevelTex);
			Blitter.DrawQuad(cmd, Blitter.GetBlitMaterial(source.dimension, false), shaderPass);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000192AB File Offset: 0x000174AB
		// Note: this type is marked as 'beforefieldinit'.
		static Blitter()
		{
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000192B8 File Offset: 0x000174B8
		[CompilerGenerated]
		internal static Vector3[] <Initialize>g__GetFullScreenTriangleVertexPosition|8_0(float z)
		{
			Vector3[] array = new Vector3[3];
			for (int i = 0; i < 3; i++)
			{
				Vector2 vector = new Vector2((float)(i << 1 & 2), (float)(i & 2));
				array[i] = new Vector3(vector.x * 2f - 1f, vector.y * 2f - 1f, z);
			}
			return array;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001931C File Offset: 0x0001751C
		[CompilerGenerated]
		internal static Vector2[] <Initialize>g__GetFullScreenTriangleTexCoord|8_1()
		{
			Vector2[] array = new Vector2[3];
			for (int i = 0; i < 3; i++)
			{
				if (SystemInfo.graphicsUVStartsAtTop)
				{
					array[i] = new Vector2((float)(i << 1 & 2), 1f - (float)(i & 2));
				}
				else
				{
					array[i] = new Vector2((float)(i << 1 & 2), (float)(i & 2));
				}
			}
			return array;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00019378 File Offset: 0x00017578
		[CompilerGenerated]
		internal static Vector3[] <Initialize>g__GetQuadVertexPosition|8_2(float z)
		{
			Vector3[] array = new Vector3[4];
			for (uint num = 0U; num < 4U; num += 1U)
			{
				uint num2 = num >> 1;
				uint num3 = num & 1U;
				float x = num2;
				float y = 1U - (num2 + num3) & 1U;
				array[(int)num] = new Vector3(x, y, z);
			}
			return array;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000193C4 File Offset: 0x000175C4
		[CompilerGenerated]
		internal static Vector2[] <Initialize>g__GetQuadTexCoord|8_3()
		{
			Vector2[] array = new Vector2[4];
			for (uint num = 0U; num < 4U; num += 1U)
			{
				uint num2 = num >> 1;
				uint num3 = num & 1U;
				float x = num2;
				float num4 = num2 + num3 & 1U;
				if (SystemInfo.graphicsUVStartsAtTop)
				{
					num4 = 1f - num4;
				}
				array[(int)num] = new Vector2(x, num4);
			}
			return array;
		}

		// Token: 0x04000350 RID: 848
		private static Material s_Blit;

		// Token: 0x04000351 RID: 849
		private static Material s_BlitTexArray;

		// Token: 0x04000352 RID: 850
		private static Material s_BlitTexArraySingleSlice;

		// Token: 0x04000353 RID: 851
		private static Material s_BlitColorAndDepth;

		// Token: 0x04000354 RID: 852
		private static MaterialPropertyBlock s_PropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000355 RID: 853
		private static Mesh s_TriangleMesh;

		// Token: 0x04000356 RID: 854
		private static Mesh s_QuadMesh;

		// Token: 0x02000173 RID: 371
		private static class BlitShaderIDs
		{
			// Token: 0x06000905 RID: 2309 RVA: 0x00024664 File Offset: 0x00022864
			// Note: this type is marked as 'beforefieldinit'.
			static BlitShaderIDs()
			{
			}

			// Token: 0x0400058F RID: 1423
			public static readonly int _BlitTexture = Shader.PropertyToID("_BlitTexture");

			// Token: 0x04000590 RID: 1424
			public static readonly int _BlitCubeTexture = Shader.PropertyToID("_BlitCubeTexture");

			// Token: 0x04000591 RID: 1425
			public static readonly int _BlitScaleBias = Shader.PropertyToID("_BlitScaleBias");

			// Token: 0x04000592 RID: 1426
			public static readonly int _BlitScaleBiasRt = Shader.PropertyToID("_BlitScaleBiasRt");

			// Token: 0x04000593 RID: 1427
			public static readonly int _BlitMipLevel = Shader.PropertyToID("_BlitMipLevel");

			// Token: 0x04000594 RID: 1428
			public static readonly int _BlitTextureSize = Shader.PropertyToID("_BlitTextureSize");

			// Token: 0x04000595 RID: 1429
			public static readonly int _BlitPaddingSize = Shader.PropertyToID("_BlitPaddingSize");

			// Token: 0x04000596 RID: 1430
			public static readonly int _InputDepth = Shader.PropertyToID("_InputDepthTexture");
		}
	}
}
