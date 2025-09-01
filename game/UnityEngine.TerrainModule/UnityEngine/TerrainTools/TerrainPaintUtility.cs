using System;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.TerrainTools
{
	// Token: 0x02000030 RID: 48
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainPaintUtility
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00006B98 File Offset: 0x00004D98
		public static Material GetBuiltinPaintMaterial()
		{
			bool flag = TerrainPaintUtility.s_BuiltinPaintMaterial == null;
			if (flag)
			{
				TerrainPaintUtility.s_BuiltinPaintMaterial = new Material(Shader.Find("Hidden/TerrainEngine/PaintHeight"));
			}
			return TerrainPaintUtility.s_BuiltinPaintMaterial;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006BD4 File Offset: 0x00004DD4
		public static void GetBrushWorldSizeLimits(out float minBrushWorldSize, out float maxBrushWorldSize, float terrainTileWorldSize, int terrainTileTextureResolutionPixels, int minBrushResolutionPixels = 1, int maxBrushResolutionPixels = 8192)
		{
			bool flag = terrainTileTextureResolutionPixels <= 0;
			if (flag)
			{
				minBrushWorldSize = terrainTileWorldSize;
				maxBrushWorldSize = terrainTileWorldSize;
			}
			else
			{
				float num = terrainTileWorldSize / (float)terrainTileTextureResolutionPixels;
				minBrushWorldSize = (float)minBrushResolutionPixels * num;
				float num2 = (float)Mathf.Min(maxBrushResolutionPixels, SystemInfo.maxTextureSize);
				maxBrushWorldSize = num2 * num;
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006C18 File Offset: 0x00004E18
		public static BrushTransform CalculateBrushTransform(Terrain terrain, Vector2 brushCenterTerrainUV, float brushSize, float brushRotationDegrees)
		{
			float f = brushRotationDegrees * 0.017453292f;
			float num = Mathf.Cos(f);
			float num2 = Mathf.Sin(f);
			Vector2 vector = new Vector2(num, -num2) * brushSize;
			Vector2 vector2 = new Vector2(num2, num) * brushSize;
			Vector3 size = terrain.terrainData.size;
			Vector2 a = brushCenterTerrainUV * new Vector2(size.x, size.z);
			Vector2 brushOrigin = a - 0.5f * vector - 0.5f * vector2;
			BrushTransform result = new BrushTransform(brushOrigin, vector, vector2);
			return result;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006CBC File Offset: 0x00004EBC
		public static void BuildTransformPaintContextUVToPaintContextUV(PaintContext src, PaintContext dst, out Vector4 scaleOffset)
		{
			float num = ((float)src.pixelRect.xMin - 0.5f) * src.pixelSize.x;
			float num2 = ((float)src.pixelRect.yMin - 0.5f) * src.pixelSize.y;
			float num3 = (float)src.pixelRect.width * src.pixelSize.x;
			float num4 = (float)src.pixelRect.height * src.pixelSize.y;
			float num5 = ((float)dst.pixelRect.xMin - 0.5f) * dst.pixelSize.x;
			float num6 = ((float)dst.pixelRect.yMin - 0.5f) * dst.pixelSize.y;
			float num7 = (float)dst.pixelRect.width * dst.pixelSize.x;
			float num8 = (float)dst.pixelRect.height * dst.pixelSize.y;
			scaleOffset = new Vector4(num3 / num7, num4 / num8, (num - num5) / num7, (num2 - num6) / num8);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006DF0 File Offset: 0x00004FF0
		public static void SetupTerrainToolMaterialProperties(PaintContext paintContext, in BrushTransform brushXform, Material material)
		{
			float d = ((float)paintContext.pixelRect.xMin - 0.5f) * paintContext.pixelSize.x;
			float d2 = ((float)paintContext.pixelRect.yMin - 0.5f) * paintContext.pixelSize.y;
			float d3 = (float)paintContext.pixelRect.width * paintContext.pixelSize.x;
			float d4 = (float)paintContext.pixelRect.height * paintContext.pixelSize.y;
			Vector2 vector = d3 * brushXform.targetX;
			Vector2 vector2 = d4 * brushXform.targetY;
			Vector2 vector3 = brushXform.targetOrigin + d * brushXform.targetX + d2 * brushXform.targetY;
			material.SetVector("_PCUVToBrushUVScales", new Vector4(vector.x, vector.y, vector2.x, vector2.y));
			material.SetVector("_PCUVToBrushUVOffset", new Vector4(vector3.x, vector3.y, 0f, 0f));
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00006F1C File Offset: 0x0000511C
		internal static bool paintTextureUsesCopyTexture
		{
			get
			{
				return (SystemInfo.copyTextureSupport & (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture)) == (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture);
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006F3C File Offset: 0x0000513C
		internal static PaintContext InitializePaintContext(Terrain terrain, int targetWidth, int targetHeight, RenderTextureFormat pcFormat, Rect boundsInTerrainSpace, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool sharedBoundaryTexel = true, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			PaintContext paintContext = PaintContext.CreateFromBounds(terrain, boundsInTerrainSpace, targetWidth, targetHeight, extraBorderPixels, sharedBoundaryTexel, fillOutsideTerrain);
			paintContext.CreateRenderTargets(pcFormat);
			return paintContext;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006F68 File Offset: 0x00005168
		public static void ReleaseContextResources(PaintContext ctx)
		{
			ctx.Cleanup(true);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006F74 File Offset: 0x00005174
		public static PaintContext BeginPaintHeightmap(Terrain terrain, Rect boundsInTerrainSpace, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			int heightmapResolution = terrain.terrainData.heightmapResolution;
			PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, heightmapResolution, heightmapResolution, Terrain.heightmapRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, true, fillOutsideTerrain);
			paintContext.GatherHeightmap();
			return paintContext;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006FAC File Offset: 0x000051AC
		public static void EndPaintHeightmap(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterHeightmap(editorUndoName);
			ctx.Cleanup(true);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006FC0 File Offset: 0x000051C0
		public static PaintContext BeginPaintHoles(Terrain terrain, Rect boundsInTerrainSpace, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			int holesResolution = terrain.terrainData.holesResolution;
			PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, holesResolution, holesResolution, Terrain.holesRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, false, fillOutsideTerrain);
			paintContext.GatherHoles();
			return paintContext;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006FF8 File Offset: 0x000051F8
		public static void EndPaintHoles(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterHoles(editorUndoName);
			ctx.Cleanup(true);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000700C File Offset: 0x0000520C
		public static PaintContext CollectNormals(Terrain terrain, Rect boundsInTerrainSpace, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			int heightmapResolution = terrain.terrainData.heightmapResolution;
			PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, heightmapResolution, heightmapResolution, Terrain.normalmapRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, true, fillOutsideTerrain);
			paintContext.GatherNormals();
			return paintContext;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007044 File Offset: 0x00005244
		public static PaintContext BeginPaintTexture(Terrain terrain, Rect boundsInTerrainSpace, TerrainLayer inputLayer, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			bool flag = inputLayer == null;
			PaintContext result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int alphamapResolution = terrain.terrainData.alphamapResolution;
				PaintContext paintContext = TerrainPaintUtility.InitializePaintContext(terrain, alphamapResolution, alphamapResolution, RenderTextureFormat.R8, boundsInTerrainSpace, extraBorderPixels, true, fillOutsideTerrain);
				paintContext.GatherAlphamap(inputLayer, true);
				result = paintContext;
			}
			return result;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000708B File Offset: 0x0000528B
		public static void EndPaintTexture(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterAlphamap(editorUndoName);
			ctx.Cleanup(true);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000070A0 File Offset: 0x000052A0
		public static Material GetBlitMaterial()
		{
			bool flag = !TerrainPaintUtility.s_BlitMaterial;
			if (flag)
			{
				TerrainPaintUtility.s_BlitMaterial = new Material(Shader.Find("Hidden/TerrainEngine/TerrainBlitCopyZWrite"));
			}
			return TerrainPaintUtility.s_BlitMaterial;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000070DC File Offset: 0x000052DC
		public static Material GetHeightBlitMaterial()
		{
			bool flag = !TerrainPaintUtility.s_HeightBlitMaterial;
			if (flag)
			{
				TerrainPaintUtility.s_HeightBlitMaterial = new Material(Shader.Find("Hidden/TerrainEngine/HeightBlitCopy"));
			}
			return TerrainPaintUtility.s_HeightBlitMaterial;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007118 File Offset: 0x00005318
		public static Material GetCopyTerrainLayerMaterial()
		{
			bool flag = !TerrainPaintUtility.s_CopyTerrainLayerMaterial;
			if (flag)
			{
				TerrainPaintUtility.s_CopyTerrainLayerMaterial = new Material(Shader.Find("Hidden/TerrainEngine/TerrainLayerUtils"));
			}
			return TerrainPaintUtility.s_CopyTerrainLayerMaterial;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007154 File Offset: 0x00005354
		internal static void DrawQuad(RectInt destinationPixels, RectInt sourcePixels, Texture sourceTexture)
		{
			TerrainPaintUtility.DrawQuad2(destinationPixels, sourcePixels, sourceTexture, sourcePixels, sourceTexture);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007164 File Offset: 0x00005364
		internal static void DrawQuad2(RectInt destinationPixels, RectInt sourcePixels, Texture sourceTexture, RectInt sourcePixels2, Texture sourceTexture2)
		{
			bool flag = destinationPixels.width > 0 && destinationPixels.height > 0;
			if (flag)
			{
				Rect rect = new Rect((float)sourcePixels.x / (float)sourceTexture.width, (float)sourcePixels.y / (float)sourceTexture.height, (float)sourcePixels.width / (float)sourceTexture.width, (float)sourcePixels.height / (float)sourceTexture.height);
				Rect rect2 = new Rect((float)sourcePixels2.x / (float)sourceTexture2.width, (float)sourcePixels2.y / (float)sourceTexture2.height, (float)sourcePixels2.width / (float)sourceTexture2.width, (float)sourcePixels2.height / (float)sourceTexture2.height);
				GL.Begin(7);
				GL.Color(new Color(1f, 1f, 1f, 1f));
				GL.MultiTexCoord2(0, rect.x, rect.y);
				GL.MultiTexCoord2(1, rect2.x, rect2.y);
				GL.Vertex3((float)destinationPixels.x, (float)destinationPixels.y, 0f);
				GL.MultiTexCoord2(0, rect.x, rect.yMax);
				GL.MultiTexCoord2(1, rect2.x, rect2.yMax);
				GL.Vertex3((float)destinationPixels.x, (float)destinationPixels.yMax, 0f);
				GL.MultiTexCoord2(0, rect.xMax, rect.yMax);
				GL.MultiTexCoord2(1, rect2.xMax, rect2.yMax);
				GL.Vertex3((float)destinationPixels.xMax, (float)destinationPixels.yMax, 0f);
				GL.MultiTexCoord2(0, rect.xMax, rect.y);
				GL.MultiTexCoord2(1, rect2.xMax, rect2.y);
				GL.Vertex3((float)destinationPixels.xMax, (float)destinationPixels.y, 0f);
				GL.End();
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007364 File Offset: 0x00005564
		internal static void DrawQuadPadded(RectInt destinationPixels, RectInt destinationPixelsPadded, RectInt sourcePixels, RectInt sourcePixelsPadded, Texture sourceTexture)
		{
			GL.Begin(7);
			GL.Color(new Color(1f, 1f, 1f, 1f));
			for (int i = 0; i < 3; i++)
			{
				bool flag = i == 0;
				Vector2Int vector2Int;
				Vector2Int vector2Int2;
				Vector2 vector;
				if (flag)
				{
					vector2Int = new Vector2Int(sourcePixelsPadded.yMin, sourcePixels.yMin);
					vector2Int2 = new Vector2Int(destinationPixelsPadded.yMin, destinationPixels.yMin);
					vector = new Vector2(-1f, 0f);
				}
				else
				{
					bool flag2 = i == 1;
					if (flag2)
					{
						vector2Int = new Vector2Int(sourcePixels.yMin, sourcePixels.yMax);
						vector2Int2 = new Vector2Int(destinationPixels.yMin, destinationPixels.yMax);
						vector = new Vector2(0f, 0f);
					}
					else
					{
						vector2Int = new Vector2Int(sourcePixels.yMax, sourcePixelsPadded.yMax);
						vector2Int2 = new Vector2Int(destinationPixels.yMax, destinationPixelsPadded.yMax);
						vector = new Vector2(0f, -1f);
					}
				}
				bool flag3 = vector2Int[0] >= vector2Int[1];
				if (!flag3)
				{
					for (int j = 0; j < 3; j++)
					{
						bool flag4 = j == 0;
						Vector2Int vector2Int3;
						Vector2Int vector2Int4;
						Vector2 vector2;
						if (flag4)
						{
							vector2Int3 = new Vector2Int(sourcePixelsPadded.xMin, sourcePixels.xMin);
							vector2Int4 = new Vector2Int(destinationPixelsPadded.xMin, destinationPixels.xMin);
							vector2 = new Vector2(-1f, 0f);
						}
						else
						{
							bool flag5 = j == 1;
							if (flag5)
							{
								vector2Int3 = new Vector2Int(sourcePixels.xMin, sourcePixels.xMax);
								vector2Int4 = new Vector2Int(destinationPixels.xMin, destinationPixels.xMax);
								vector2 = new Vector2(0f, 0f);
							}
							else
							{
								vector2Int3 = new Vector2Int(sourcePixels.xMax, sourcePixelsPadded.xMax);
								vector2Int4 = new Vector2Int(destinationPixels.xMax, destinationPixelsPadded.xMax);
								vector2 = new Vector2(0f, -1f);
							}
						}
						bool flag6 = vector2Int3[0] >= vector2Int3[1];
						if (!flag6)
						{
							Rect rect = new Rect((float)vector2Int3[0] / (float)sourceTexture.width, (float)vector2Int[0] / (float)sourceTexture.height, (float)(vector2Int3[1] - vector2Int3[0]) / (float)sourceTexture.width, (float)(vector2Int[1] - vector2Int[0]) / (float)sourceTexture.height);
							GL.TexCoord2(rect.x, rect.y);
							GL.Vertex3((float)vector2Int4[0], (float)vector2Int2[0], 0.5f * (vector2[0] + vector[0]));
							GL.TexCoord2(rect.x, rect.yMax);
							GL.Vertex3((float)vector2Int4[0], (float)vector2Int2[1], 0.5f * (vector2[0] + vector[1]));
							GL.TexCoord2(rect.xMax, rect.yMax);
							GL.Vertex3((float)vector2Int4[1], (float)vector2Int2[1], 0.5f * (vector2[1] + vector[1]));
							GL.TexCoord2(rect.xMax, rect.y);
							GL.Vertex3((float)vector2Int4[1], (float)vector2Int2[0], 0.5f * (vector2[1] + vector[0]));
						}
					}
				}
			}
			GL.End();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000772C File Offset: 0x0000592C
		internal static RectInt CalcPixelRectFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int textureWidth, int textureHeight, int extraBorderPixels, bool sharedBoundaryTexel)
		{
			float num = ((float)textureWidth - (sharedBoundaryTexel ? 1f : 0f)) / terrain.terrainData.size.x;
			float num2 = ((float)textureHeight - (sharedBoundaryTexel ? 1f : 0f)) / terrain.terrainData.size.z;
			int num3 = Mathf.FloorToInt(boundsInTerrainSpace.xMin * num) - extraBorderPixels;
			int num4 = Mathf.FloorToInt(boundsInTerrainSpace.yMin * num2) - extraBorderPixels;
			int num5 = Mathf.CeilToInt(boundsInTerrainSpace.xMax * num) + extraBorderPixels;
			int num6 = Mathf.CeilToInt(boundsInTerrainSpace.yMax * num2) + extraBorderPixels;
			int width = PaintContext.ClampContextResolution(num5 - num3 + 1);
			int height = PaintContext.ClampContextResolution(num6 - num4 + 1);
			return new RectInt(num3, num4, width, height);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000077FC File Offset: 0x000059FC
		public static Texture2D GetTerrainAlphaMapChecked(Terrain terrain, int mapIndex)
		{
			bool flag = mapIndex >= terrain.terrainData.alphamapTextureCount;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain alphamap information.");
			}
			return terrain.terrainData.GetAlphamapTexture(mapIndex);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000783C File Offset: 0x00005A3C
		public static int FindTerrainLayerIndex(Terrain terrain, TerrainLayer inputLayer)
		{
			TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
			for (int i = 0; i < terrainLayers.Length; i++)
			{
				bool flag = terrainLayers[i] == inputLayer;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007884 File Offset: 0x00005A84
		internal static int AddTerrainLayer(Terrain terrain, TerrainLayer inputLayer)
		{
			TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
			int num = terrainLayers.Length;
			TerrainLayer[] array = new TerrainLayer[num + 1];
			Array.Copy(terrainLayers, 0, array, 0, num);
			array[num] = inputLayer;
			terrain.terrainData.terrainLayers = array;
			return num;
		}

		// Token: 0x040000C1 RID: 193
		private static Material s_BuiltinPaintMaterial;

		// Token: 0x040000C2 RID: 194
		private static Material s_BlitMaterial;

		// Token: 0x040000C3 RID: 195
		private static Material s_HeightBlitMaterial;

		// Token: 0x040000C4 RID: 196
		private static Material s_CopyTerrainLayerMaterial;
	}
}
