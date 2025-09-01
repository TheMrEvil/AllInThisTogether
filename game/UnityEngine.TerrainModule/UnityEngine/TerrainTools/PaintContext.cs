using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.TerrainUtils;

namespace UnityEngine.TerrainTools
{
	// Token: 0x02000022 RID: 34
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public class PaintContext
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000514C File Offset: 0x0000334C
		public Terrain originTerrain
		{
			[CompilerGenerated]
			get
			{
				return this.<originTerrain>k__BackingField;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005154 File Offset: 0x00003354
		public RectInt pixelRect
		{
			[CompilerGenerated]
			get
			{
				return this.<pixelRect>k__BackingField;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000515C File Offset: 0x0000335C
		public int targetTextureWidth
		{
			[CompilerGenerated]
			get
			{
				return this.<targetTextureWidth>k__BackingField;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00005164 File Offset: 0x00003364
		public int targetTextureHeight
		{
			[CompilerGenerated]
			get
			{
				return this.<targetTextureHeight>k__BackingField;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000516C File Offset: 0x0000336C
		public Vector2 pixelSize
		{
			[CompilerGenerated]
			get
			{
				return this.<pixelSize>k__BackingField;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005174 File Offset: 0x00003374
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000517C File Offset: 0x0000337C
		public RenderTexture sourceRenderTexture
		{
			[CompilerGenerated]
			get
			{
				return this.<sourceRenderTexture>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<sourceRenderTexture>k__BackingField = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00005185 File Offset: 0x00003385
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000518D File Offset: 0x0000338D
		public RenderTexture destinationRenderTexture
		{
			[CompilerGenerated]
			get
			{
				return this.<destinationRenderTexture>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<destinationRenderTexture>k__BackingField = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00005196 File Offset: 0x00003396
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000519E File Offset: 0x0000339E
		public RenderTexture oldRenderTexture
		{
			[CompilerGenerated]
			get
			{
				return this.<oldRenderTexture>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<oldRenderTexture>k__BackingField = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000051A8 File Offset: 0x000033A8
		public int terrainCount
		{
			get
			{
				return this.m_TerrainTiles.Count;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000051C8 File Offset: 0x000033C8
		public Terrain GetTerrain(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].terrain;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000051EC File Offset: 0x000033EC
		public RectInt GetClippedPixelRectInTerrainPixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedTerrainPixels;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005210 File Offset: 0x00003410
		public RectInt GetClippedPixelRectInRenderTexturePixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedPCPixels;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00005233 File Offset: 0x00003433
		public float heightWorldSpaceMin
		{
			get
			{
				return this.m_HeightWorldSpaceMin;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000523B File Offset: 0x0000343B
		public float heightWorldSpaceSize
		{
			get
			{
				return this.m_HeightWorldSpaceMax - this.m_HeightWorldSpaceMin;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000524A File Offset: 0x0000344A
		public static float kNormalizedHeightScale
		{
			get
			{
				return 0.4999771f;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001C5 RID: 453 RVA: 0x00005254 File Offset: 0x00003454
		// (remove) Token: 0x060001C6 RID: 454 RVA: 0x00005288 File Offset: 0x00003488
		internal static event Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> onTerrainTileBeforePaint
		{
			[CompilerGenerated]
			add
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action2;
				do
				{
					action2 = action;
					Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> value2 = (Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string>>(ref PaintContext.onTerrainTileBeforePaint, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action2;
				do
				{
					action2 = action;
					Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> value2 = (Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string>>(ref PaintContext.onTerrainTileBeforePaint, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000052BC File Offset: 0x000034BC
		internal static int ClampContextResolution(int resolution)
		{
			return Mathf.Clamp(resolution, 1, 8192);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000052DC File Offset: 0x000034DC
		public PaintContext(Terrain terrain, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, [DefaultValue("true")] bool sharedBoundaryTexel = true, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			this.originTerrain = terrain;
			this.pixelRect = pixelRect;
			this.targetTextureWidth = targetTextureWidth;
			this.targetTextureHeight = targetTextureHeight;
			TerrainData terrainData = terrain.terrainData;
			this.pixelSize = new Vector2(terrainData.size.x / ((float)targetTextureWidth - (sharedBoundaryTexel ? 1f : 0f)), terrainData.size.z / ((float)targetTextureHeight - (sharedBoundaryTexel ? 1f : 0f)));
			this.FindTerrainTilesUnlimited(sharedBoundaryTexel, fillOutsideTerrain);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000536C File Offset: 0x0000356C
		public static PaintContext CreateFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int inputTextureWidth, int inputTextureHeight, [DefaultValue("0")] int extraBorderPixels = 0, [DefaultValue("true")] bool sharedBoundaryTexel = true, [DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			return new PaintContext(terrain, TerrainPaintUtility.CalcPixelRectFromBounds(terrain, boundsInTerrainSpace, inputTextureWidth, inputTextureHeight, extraBorderPixels, sharedBoundaryTexel), inputTextureWidth, inputTextureHeight, sharedBoundaryTexel, fillOutsideTerrain);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005398 File Offset: 0x00003598
		private void FindTerrainTilesUnlimited(bool sharedBoundaryTexel, bool fillOutsideTerrain)
		{
			float minX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)this.pixelRect.xMin;
			float minZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)this.pixelRect.yMin;
			float maxX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)(this.pixelRect.xMax - 1);
			float maxZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)(this.pixelRect.yMax - 1);
			this.m_HeightWorldSpaceMin = this.originTerrain.GetPosition().y;
			this.m_HeightWorldSpaceMax = this.m_HeightWorldSpaceMin + this.originTerrain.terrainData.size.y;
			Predicate<Terrain> filter = delegate(Terrain t)
			{
				float x = t.transform.position.x;
				float z = t.transform.position.z;
				float num3 = t.transform.position.x + t.terrainData.size.x;
				float num4 = t.transform.position.z + t.terrainData.size.z;
				return x <= maxX && num3 >= minX && z <= maxZ && num4 >= minZ;
			};
			TerrainMap terrainMap = TerrainMap.CreateFromConnectedNeighbors(this.originTerrain, filter, false);
			this.m_TerrainTiles = new List<PaintContext.TerrainTile>();
			bool flag = terrainMap != null;
			if (flag)
			{
				foreach (KeyValuePair<TerrainTileCoord, Terrain> keyValuePair in terrainMap.terrainTiles)
				{
					TerrainTileCoord key = keyValuePair.Key;
					Terrain value = keyValuePair.Value;
					int num = key.tileX * (this.targetTextureWidth - (sharedBoundaryTexel ? 1 : 0));
					int num2 = key.tileZ * (this.targetTextureHeight - (sharedBoundaryTexel ? 1 : 0));
					RectInt other = new RectInt(num, num2, this.targetTextureWidth, this.targetTextureHeight);
					bool flag2 = this.pixelRect.Overlaps(other);
					if (flag2)
					{
						int edgePad = fillOutsideTerrain ? Mathf.Max(this.targetTextureWidth, this.targetTextureHeight) : 0;
						this.m_TerrainTiles.Add(PaintContext.TerrainTile.Make(value, num, num2, this.pixelRect, this.targetTextureWidth, this.targetTextureHeight, edgePad));
						this.m_HeightWorldSpaceMin = Mathf.Min(this.m_HeightWorldSpaceMin, value.GetPosition().y);
						this.m_HeightWorldSpaceMax = Mathf.Max(this.m_HeightWorldSpaceMax, value.GetPosition().y + value.terrainData.size.y);
					}
				}
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005654 File Offset: 0x00003854
		public void CreateRenderTargets(RenderTextureFormat colorFormat)
		{
			int num = PaintContext.ClampContextResolution(this.pixelRect.width);
			int num2 = PaintContext.ClampContextResolution(this.pixelRect.height);
			bool flag = num != this.pixelRect.width || num2 != this.pixelRect.height;
			if (flag)
			{
				Debug.LogWarning(string.Format("\nTERRAIN EDITOR INTERNAL ERROR: An attempt to create a PaintContext with dimensions of {0}x{1} was made,\nwhereas the maximum supported resolution is {2}. The size has been clamped to {3}.", new object[]
				{
					this.pixelRect.width,
					this.pixelRect.height,
					8192,
					8192
				}));
			}
			this.sourceRenderTexture = RenderTexture.GetTemporary(num, num2, 16, colorFormat, RenderTextureReadWrite.Linear);
			this.destinationRenderTexture = RenderTexture.GetTemporary(num, num2, 0, colorFormat, RenderTextureReadWrite.Linear);
			this.sourceRenderTexture.wrapMode = TextureWrapMode.Clamp;
			this.sourceRenderTexture.filterMode = FilterMode.Point;
			this.oldRenderTexture = RenderTexture.active;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005760 File Offset: 0x00003960
		public void Cleanup(bool restoreRenderTexture = true)
		{
			if (restoreRenderTexture)
			{
				RenderTexture.active = this.oldRenderTexture;
			}
			RenderTexture.ReleaseTemporary(this.sourceRenderTexture);
			RenderTexture.ReleaseTemporary(this.destinationRenderTexture);
			this.sourceRenderTexture = null;
			this.destinationRenderTexture = null;
			this.oldRenderTexture = null;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000057B0 File Offset: 0x000039B0
		private void GatherInternal(Func<PaintContext.ITerrainInfo, Texture> terrainToTexture, Color defaultColor, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			RenderTexture.active = this.sourceRenderTexture;
			GL.Clear(true, true, defaultColor);
			GL.PushMatrix();
			GL.LoadPixelMatrix(0f, (float)this.pixelRect.width, 0f, (float)this.pixelRect.height);
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.gatherEnable;
				if (!flag2)
				{
					Texture texture = terrainToTexture(terrainTile);
					bool flag3 = texture == null || !terrainTile.gatherEnable;
					if (!flag3)
					{
						bool flag4 = texture.width != this.targetTextureWidth || texture.height != this.targetTextureHeight;
						if (flag4)
						{
							Debug.LogWarning(operationName + " requires the same resolution texture for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit(terrainTile);
							}
							bool flag5 = !terrainTile.gatherEnable;
							if (!flag5)
							{
								FilterMode filterMode = texture.filterMode;
								texture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", texture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuadPadded(terrainTile.clippedPCPixels, terrainTile.paddedPCPixels, terrainTile.clippedTerrainPixels, terrainTile.paddedTerrainPixels, texture);
								texture.filterMode = filterMode;
								if (afterBlit != null)
								{
									afterBlit(terrainTile);
								}
							}
						}
					}
				}
			}
			GL.PopMatrix();
			RenderTexture.active = this.oldRenderTexture;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005968 File Offset: 0x00003B68
		private void ScatterInternal(Func<PaintContext.ITerrainInfo, RenderTexture> terrainToRT, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			RenderTexture active = RenderTexture.active;
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.scatterEnable;
				if (!flag2)
				{
					RenderTexture renderTexture = terrainToRT(terrainTile);
					bool flag3 = renderTexture == null || !terrainTile.scatterEnable;
					if (!flag3)
					{
						bool flag4 = renderTexture.width != this.targetTextureWidth || renderTexture.height != this.targetTextureHeight;
						if (flag4)
						{
							Debug.LogWarning(operationName + " requires the same resolution for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit(terrainTile);
							}
							bool flag5 = !terrainTile.scatterEnable;
							if (!flag5)
							{
								RenderTexture.active = renderTexture;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)renderTexture.width, 0f, (float)renderTexture.height);
								FilterMode filterMode = this.destinationRenderTexture.filterMode;
								this.destinationRenderTexture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuad(terrainTile.clippedTerrainPixels, terrainTile.clippedPCPixels, this.destinationRenderTexture);
								this.destinationRenderTexture.filterMode = filterMode;
								GL.PopMatrix();
								if (afterBlit != null)
								{
									afterBlit(terrainTile);
								}
							}
						}
					}
				}
			}
			RenderTexture.active = active;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005B10 File Offset: 0x00003D10
		public void Gather(Func<PaintContext.ITerrainInfo, Texture> terrainSource, Color defaultColor, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainSource != null;
			if (flag)
			{
				this.GatherInternal(terrainSource, defaultColor, "PaintContext.Gather", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005B3C File Offset: 0x00003D3C
		public void Scatter(Func<PaintContext.ITerrainInfo, RenderTexture> terrainDest, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainDest != null;
			if (flag)
			{
				this.ScatterInternal(terrainDest, "PaintContext.Scatter", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005B68 File Offset: 0x00003D68
		public void GatherHeightmap()
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHeightmap", blitMaterial, 0, delegate(PaintContext.ITerrainInfo t)
			{
				blitMaterial.SetFloat("_Height_Offset", (t.terrain.GetPosition().y - this.heightWorldSpaceMin) / this.heightWorldSpaceSize * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", t.terrain.terrainData.size.y / this.heightWorldSpaceSize);
			}, null);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005C14 File Offset: 0x00003E14
		public void ScatterHeightmap(string editorUndoName)
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			this.ScatterInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, "PaintContext.ScatterHeightmap", blitMaterial, 0, delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				if (action != null)
				{
					action(t, PaintContext.ToolAction.PaintHeightmap, editorUndoName);
				}
				blitMaterial.SetFloat("_Height_Offset", (this.heightWorldSpaceMin - t.terrain.GetPosition().y) / t.terrain.terrainData.size.y * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", this.heightWorldSpaceSize / t.terrain.terrainData.size.y);
			}, delegate(PaintContext.ITerrainInfo t)
			{
				TerrainHeightmapSyncControl syncControl = t.terrain.drawInstanced ? TerrainHeightmapSyncControl.None : TerrainHeightmapSyncControl.HeightAndLod;
				t.terrain.terrainData.DirtyHeightmapRegion(t.clippedTerrainPixels, syncControl);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHeightmap);
			});
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005CCC File Offset: 0x00003ECC
		public void GatherHoles()
		{
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.holesTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHoles", null, 0, null, null);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005D24 File Offset: 0x00003F24
		public void ScatterHoles(string editorUndoName)
		{
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				if (action != null)
				{
					action(t, PaintContext.ToolAction.PaintHoles, editorUndoName);
				}
				t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.HolesTextureName, 0, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHoles);
				return null;
			}, "PaintContext.ScatterHoles", null, 0, null, null);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005D5C File Offset: 0x00003F5C
		public void GatherNormals()
		{
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.normalmapTexture, new Color(0.5f, 0.5f, 0.5f, 0.5f), "PaintContext.GatherNormals", null, 0, null, null);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005DB4 File Offset: 0x00003FB4
		private PaintContext.SplatmapUserData GetTerrainLayerUserData(PaintContext.ITerrainInfo context, TerrainLayer terrainLayer = null, bool addLayerIfDoesntExist = false)
		{
			PaintContext.SplatmapUserData splatmapUserData = context.userData as PaintContext.SplatmapUserData;
			bool flag = splatmapUserData != null;
			if (flag)
			{
				bool flag2 = terrainLayer == null || terrainLayer == splatmapUserData.terrainLayer;
				if (flag2)
				{
					return splatmapUserData;
				}
				splatmapUserData = null;
			}
			bool flag3 = splatmapUserData == null;
			if (flag3)
			{
				int num = -1;
				bool flag4 = terrainLayer != null;
				if (flag4)
				{
					num = TerrainPaintUtility.FindTerrainLayerIndex(context.terrain, terrainLayer);
					bool flag5 = num == -1 && addLayerIfDoesntExist;
					if (flag5)
					{
						Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
						if (action != null)
						{
							action(context, PaintContext.ToolAction.AddTerrainLayer, "Adding Terrain Layer");
						}
						num = TerrainPaintUtility.AddTerrainLayer(context.terrain, terrainLayer);
					}
				}
				bool flag6 = num != -1;
				if (flag6)
				{
					splatmapUserData = new PaintContext.SplatmapUserData();
					splatmapUserData.terrainLayer = terrainLayer;
					splatmapUserData.terrainLayerIndex = num;
					splatmapUserData.mapIndex = num >> 2;
					splatmapUserData.channelIndex = (num & 3);
				}
				context.userData = splatmapUserData;
			}
			return splatmapUserData;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005EA8 File Offset: 0x000040A8
		public void GatherAlphamap(TerrainLayer inputLayer, bool addLayerIfDoesntExist = true)
		{
			bool flag = inputLayer == null;
			if (!flag)
			{
				Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
				Vector4[] layerMasks = new Vector4[]
				{
					new Vector4(1f, 0f, 0f, 0f),
					new Vector4(0f, 1f, 0f, 0f),
					new Vector4(0f, 0f, 1f, 0f),
					new Vector4(0f, 0f, 0f, 1f)
				};
				this.GatherInternal(delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, inputLayer, addLayerIfDoesntExist);
					bool flag2 = terrainLayerUserData != null;
					Texture result;
					if (flag2)
					{
						result = TerrainPaintUtility.GetTerrainAlphaMapChecked(t.terrain, terrainLayerUserData.mapIndex);
					}
					else
					{
						result = null;
					}
					return result;
				}, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherAlphamap", copyTerrainLayerMaterial, 0, delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, null, false);
					copyTerrainLayerMaterial.SetVector("_LayerMask", layerMasks[terrainLayerUserData.channelIndex]);
				}, null);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005FC4 File Offset: 0x000041C4
		public void ScatterAlphamap(string editorUndoName)
		{
			Vector4[] layerMasks = new Vector4[]
			{
				new Vector4(1f, 0f, 0f, 0f),
				new Vector4(0f, 1f, 0f, 0f),
				new Vector4(0f, 0f, 1f, 0f),
				new Vector4(0f, 0f, 0f, 1f)
			};
			Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
			RenderTexture tempTarget = RenderTexture.GetTemporary(new RenderTextureDescriptor(this.destinationRenderTexture.width, this.destinationRenderTexture.height, GraphicsFormat.R8G8B8A8_UNorm, GraphicsFormat.None)
			{
				sRGB = false,
				useMipMap = false,
				autoGenerateMips = false
			});
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, null, false);
				bool flag = terrainLayerUserData != null;
				if (flag)
				{
					Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
					if (action != null)
					{
						action(t, PaintContext.ToolAction.PaintTexture, editorUndoName);
					}
					int mapIndex = terrainLayerUserData.mapIndex;
					int channelIndex = terrainLayerUserData.channelIndex;
					Texture2D value = t.terrain.terrainData.alphamapTextures[mapIndex];
					this.destinationRenderTexture.filterMode = FilterMode.Point;
					this.sourceRenderTexture.filterMode = FilterMode.Point;
					for (int i = 0; i <= t.terrain.terrainData.alphamapTextureCount; i++)
					{
						bool flag2 = i == mapIndex;
						if (!flag2)
						{
							int num = (i == t.terrain.terrainData.alphamapTextureCount) ? mapIndex : i;
							Texture2D texture2D = t.terrain.terrainData.alphamapTextures[num];
							bool flag3 = texture2D.width != this.targetTextureWidth || texture2D.height != this.targetTextureHeight;
							if (flag3)
							{
								Debug.LogWarning("PaintContext alphamap operations must use the same resolution for all Terrains - mismatched Terrains are ignored.", t.terrain);
							}
							else
							{
								RenderTexture.active = tempTarget;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)tempTarget.width, 0f, (float)tempTarget.height);
								copyTerrainLayerMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OldAlphaMapTexture", this.sourceRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OriginalTargetAlphaMap", value);
								copyTerrainLayerMaterial.SetTexture("_AlphaMapTexture", texture2D);
								copyTerrainLayerMaterial.SetVector("_LayerMask", (num == mapIndex) ? layerMasks[channelIndex] : Vector4.zero);
								copyTerrainLayerMaterial.SetVector("_OriginalTargetAlphaMask", layerMasks[channelIndex]);
								copyTerrainLayerMaterial.SetPass(1);
								TerrainPaintUtility.DrawQuad2(t.clippedPCPixels, t.clippedPCPixels, this.destinationRenderTexture, t.clippedTerrainPixels, texture2D);
								GL.PopMatrix();
								t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.AlphamapTextureName, num, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
							}
						}
					}
					RenderTexture.active = null;
					PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintTexture);
				}
				return null;
			}, "PaintContext.ScatterAlphamap", copyTerrainLayerMaterial, 0, null, null);
			RenderTexture.ReleaseTemporary(tempTarget);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000060F0 File Offset: 0x000042F0
		private static void OnTerrainPainted(PaintContext.ITerrainInfo tile, PaintContext.ToolAction action)
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				bool flag = tile.terrain == PaintContext.s_PaintedTerrain[i].terrain;
				if (flag)
				{
					PaintContext.PaintedTerrain value = PaintContext.s_PaintedTerrain[i];
					value.action |= action;
					PaintContext.s_PaintedTerrain[i] = value;
					return;
				}
			}
			PaintContext.s_PaintedTerrain.Add(new PaintContext.PaintedTerrain
			{
				terrain = tile.terrain,
				action = action
			});
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000618C File Offset: 0x0000438C
		public static void ApplyDelayedActions()
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				PaintContext.PaintedTerrain paintedTerrain = PaintContext.s_PaintedTerrain[i];
				TerrainData terrainData = paintedTerrain.terrain.terrainData;
				bool flag = terrainData == null;
				if (!flag)
				{
					bool flag2 = (paintedTerrain.action & PaintContext.ToolAction.PaintHeightmap) > PaintContext.ToolAction.None;
					if (flag2)
					{
						terrainData.SyncHeightmap();
					}
					bool flag3 = (paintedTerrain.action & PaintContext.ToolAction.PaintHoles) > PaintContext.ToolAction.None;
					if (flag3)
					{
						terrainData.SyncTexture(TerrainData.HolesTextureName);
					}
					bool flag4 = (paintedTerrain.action & PaintContext.ToolAction.PaintTexture) > PaintContext.ToolAction.None;
					if (flag4)
					{
						terrainData.SetBaseMapDirty();
						terrainData.SyncTexture(TerrainData.AlphamapTextureName);
					}
					paintedTerrain.terrain.editorRenderFlags = TerrainRenderFlags.all;
				}
			}
			PaintContext.s_PaintedTerrain.Clear();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006259 File Offset: 0x00004459
		// Note: this type is marked as 'beforefieldinit'.
		static PaintContext()
		{
		}

		// Token: 0x0400007C RID: 124
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly Terrain <originTerrain>k__BackingField;

		// Token: 0x0400007D RID: 125
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly RectInt <pixelRect>k__BackingField;

		// Token: 0x0400007E RID: 126
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <targetTextureWidth>k__BackingField;

		// Token: 0x0400007F RID: 127
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <targetTextureHeight>k__BackingField;

		// Token: 0x04000080 RID: 128
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly Vector2 <pixelSize>k__BackingField;

		// Token: 0x04000081 RID: 129
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private RenderTexture <sourceRenderTexture>k__BackingField;

		// Token: 0x04000082 RID: 130
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private RenderTexture <destinationRenderTexture>k__BackingField;

		// Token: 0x04000083 RID: 131
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private RenderTexture <oldRenderTexture>k__BackingField;

		// Token: 0x04000084 RID: 132
		private List<PaintContext.TerrainTile> m_TerrainTiles;

		// Token: 0x04000085 RID: 133
		private float m_HeightWorldSpaceMin;

		// Token: 0x04000086 RID: 134
		private float m_HeightWorldSpaceMax;

		// Token: 0x04000087 RID: 135
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> onTerrainTileBeforePaint;

		// Token: 0x04000088 RID: 136
		internal const int k_MinimumResolution = 1;

		// Token: 0x04000089 RID: 137
		internal const int k_MaximumResolution = 8192;

		// Token: 0x0400008A RID: 138
		private static List<PaintContext.PaintedTerrain> s_PaintedTerrain = new List<PaintContext.PaintedTerrain>();

		// Token: 0x02000023 RID: 35
		public interface ITerrainInfo
		{
			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060001DC RID: 476
			Terrain terrain { get; }

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060001DD RID: 477
			RectInt clippedTerrainPixels { get; }

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060001DE RID: 478
			RectInt clippedPCPixels { get; }

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060001DF RID: 479
			RectInt paddedTerrainPixels { get; }

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060001E0 RID: 480
			RectInt paddedPCPixels { get; }

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060001E1 RID: 481
			// (set) Token: 0x060001E2 RID: 482
			bool gatherEnable { get; set; }

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060001E3 RID: 483
			// (set) Token: 0x060001E4 RID: 484
			bool scatterEnable { get; set; }

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060001E5 RID: 485
			// (set) Token: 0x060001E6 RID: 486
			object userData { get; set; }
		}

		// Token: 0x02000024 RID: 36
		private class TerrainTile : PaintContext.ITerrainInfo
		{
			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060001E7 RID: 487 RVA: 0x00006268 File Offset: 0x00004468
			Terrain PaintContext.ITerrainInfo.terrain
			{
				get
				{
					return this.terrain;
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060001E8 RID: 488 RVA: 0x00006280 File Offset: 0x00004480
			RectInt PaintContext.ITerrainInfo.clippedTerrainPixels
			{
				get
				{
					return this.clippedTerrainPixels;
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060001E9 RID: 489 RVA: 0x00006298 File Offset: 0x00004498
			RectInt PaintContext.ITerrainInfo.clippedPCPixels
			{
				get
				{
					return this.clippedPCPixels;
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060001EA RID: 490 RVA: 0x000062B0 File Offset: 0x000044B0
			RectInt PaintContext.ITerrainInfo.paddedTerrainPixels
			{
				get
				{
					return this.paddedTerrainPixels;
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060001EB RID: 491 RVA: 0x000062C8 File Offset: 0x000044C8
			RectInt PaintContext.ITerrainInfo.paddedPCPixels
			{
				get
				{
					return this.paddedPCPixels;
				}
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060001EC RID: 492 RVA: 0x000062E0 File Offset: 0x000044E0
			// (set) Token: 0x060001ED RID: 493 RVA: 0x000062F8 File Offset: 0x000044F8
			bool PaintContext.ITerrainInfo.gatherEnable
			{
				get
				{
					return this.gatherEnable;
				}
				set
				{
					this.gatherEnable = value;
				}
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060001EE RID: 494 RVA: 0x00006304 File Offset: 0x00004504
			// (set) Token: 0x060001EF RID: 495 RVA: 0x0000631C File Offset: 0x0000451C
			bool PaintContext.ITerrainInfo.scatterEnable
			{
				get
				{
					return this.scatterEnable;
				}
				set
				{
					this.scatterEnable = value;
				}
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060001F0 RID: 496 RVA: 0x00006328 File Offset: 0x00004528
			// (set) Token: 0x060001F1 RID: 497 RVA: 0x00006340 File Offset: 0x00004540
			object PaintContext.ITerrainInfo.userData
			{
				get
				{
					return this.userData;
				}
				set
				{
					this.userData = value;
				}
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x0000634C File Offset: 0x0000454C
			public static PaintContext.TerrainTile Make(Terrain terrain, int tileOriginPixelsX, int tileOriginPixelsY, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, int edgePad = 0)
			{
				PaintContext.TerrainTile terrainTile = new PaintContext.TerrainTile
				{
					terrain = terrain,
					gatherEnable = true,
					scatterEnable = true,
					tileOriginPixels = new Vector2Int(tileOriginPixelsX, tileOriginPixelsY),
					clippedTerrainPixels = new RectInt
					{
						x = Mathf.Max(0, pixelRect.x - tileOriginPixelsX),
						y = Mathf.Max(0, pixelRect.y - tileOriginPixelsY),
						xMax = Mathf.Min(targetTextureWidth, pixelRect.xMax - tileOriginPixelsX),
						yMax = Mathf.Min(targetTextureHeight, pixelRect.yMax - tileOriginPixelsY)
					}
				};
				terrainTile.clippedPCPixels = new RectInt(terrainTile.clippedTerrainPixels.x + terrainTile.tileOriginPixels.x - pixelRect.x, terrainTile.clippedTerrainPixels.y + terrainTile.tileOriginPixels.y - pixelRect.y, terrainTile.clippedTerrainPixels.width, terrainTile.clippedTerrainPixels.height);
				int num = (terrain.leftNeighbor == null) ? edgePad : 0;
				int num2 = (terrain.rightNeighbor == null) ? edgePad : 0;
				int num3 = (terrain.bottomNeighbor == null) ? edgePad : 0;
				int num4 = (terrain.topNeighbor == null) ? edgePad : 0;
				terrainTile.paddedTerrainPixels = new RectInt
				{
					x = Mathf.Max(-num, pixelRect.x - tileOriginPixelsX - num),
					y = Mathf.Max(-num3, pixelRect.y - tileOriginPixelsY - num3),
					xMax = Mathf.Min(targetTextureWidth + num2, pixelRect.xMax - tileOriginPixelsX + num2),
					yMax = Mathf.Min(targetTextureHeight + num4, pixelRect.yMax - tileOriginPixelsY + num4)
				};
				terrainTile.paddedPCPixels = new RectInt(terrainTile.clippedPCPixels.min + (terrainTile.paddedTerrainPixels.min - terrainTile.clippedTerrainPixels.min), terrainTile.clippedPCPixels.size + (terrainTile.paddedTerrainPixels.size - terrainTile.clippedTerrainPixels.size));
				bool flag = terrainTile.clippedTerrainPixels.width == 0 || terrainTile.clippedTerrainPixels.height == 0;
				if (flag)
				{
					terrainTile.gatherEnable = false;
					terrainTile.scatterEnable = false;
					Debug.LogError("PaintContext.ClipTerrainTiles found 0 content rect");
				}
				return terrainTile;
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x00004B54 File Offset: 0x00002D54
			public TerrainTile()
			{
			}

			// Token: 0x0400008B RID: 139
			public Terrain terrain;

			// Token: 0x0400008C RID: 140
			public Vector2Int tileOriginPixels;

			// Token: 0x0400008D RID: 141
			public RectInt clippedTerrainPixels;

			// Token: 0x0400008E RID: 142
			public RectInt clippedPCPixels;

			// Token: 0x0400008F RID: 143
			public RectInt paddedTerrainPixels;

			// Token: 0x04000090 RID: 144
			public RectInt paddedPCPixels;

			// Token: 0x04000091 RID: 145
			public object userData;

			// Token: 0x04000092 RID: 146
			public bool gatherEnable;

			// Token: 0x04000093 RID: 147
			public bool scatterEnable;
		}

		// Token: 0x02000025 RID: 37
		private class SplatmapUserData
		{
			// Token: 0x060001F4 RID: 500 RVA: 0x00004B54 File Offset: 0x00002D54
			public SplatmapUserData()
			{
			}

			// Token: 0x04000094 RID: 148
			public TerrainLayer terrainLayer;

			// Token: 0x04000095 RID: 149
			public int terrainLayerIndex;

			// Token: 0x04000096 RID: 150
			public int mapIndex;

			// Token: 0x04000097 RID: 151
			public int channelIndex;
		}

		// Token: 0x02000026 RID: 38
		[Flags]
		internal enum ToolAction
		{
			// Token: 0x04000099 RID: 153
			None = 0,
			// Token: 0x0400009A RID: 154
			PaintHeightmap = 1,
			// Token: 0x0400009B RID: 155
			PaintTexture = 2,
			// Token: 0x0400009C RID: 156
			PaintHoles = 4,
			// Token: 0x0400009D RID: 157
			AddTerrainLayer = 8
		}

		// Token: 0x02000027 RID: 39
		private struct PaintedTerrain
		{
			// Token: 0x0400009E RID: 158
			public Terrain terrain;

			// Token: 0x0400009F RID: 159
			public PaintContext.ToolAction action;
		}

		// Token: 0x02000028 RID: 40
		[CompilerGenerated]
		private sealed class <>c__DisplayClass53_0
		{
			// Token: 0x060001F5 RID: 501 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c__DisplayClass53_0()
			{
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x000065C8 File Offset: 0x000047C8
			internal bool <FindTerrainTilesUnlimited>b__0(Terrain t)
			{
				float x = t.transform.position.x;
				float z = t.transform.position.z;
				float num = t.transform.position.x + t.terrainData.size.x;
				float num2 = t.transform.position.z + t.terrainData.size.z;
				return x <= this.maxX && num >= this.minX && z <= this.maxZ && num2 >= this.minZ;
			}

			// Token: 0x040000A0 RID: 160
			public float maxX;

			// Token: 0x040000A1 RID: 161
			public float minX;

			// Token: 0x040000A2 RID: 162
			public float maxZ;

			// Token: 0x040000A3 RID: 163
			public float minZ;
		}

		// Token: 0x02000029 RID: 41
		[CompilerGenerated]
		private sealed class <>c__DisplayClass60_0
		{
			// Token: 0x060001F7 RID: 503 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c__DisplayClass60_0()
			{
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x0000666C File Offset: 0x0000486C
			internal void <GatherHeightmap>b__1(PaintContext.ITerrainInfo t)
			{
				this.blitMaterial.SetFloat("_Height_Offset", (t.terrain.GetPosition().y - this.<>4__this.heightWorldSpaceMin) / this.<>4__this.heightWorldSpaceSize * PaintContext.kNormalizedHeightScale);
				this.blitMaterial.SetFloat("_Height_Scale", t.terrain.terrainData.size.y / this.<>4__this.heightWorldSpaceSize);
			}

			// Token: 0x040000A4 RID: 164
			public Material blitMaterial;

			// Token: 0x040000A5 RID: 165
			public PaintContext <>4__this;
		}

		// Token: 0x0200002A RID: 42
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060001F9 RID: 505 RVA: 0x000066EB File Offset: 0x000048EB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060001FA RID: 506 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c()
			{
			}

			// Token: 0x060001FB RID: 507 RVA: 0x000066F7 File Offset: 0x000048F7
			internal Texture <GatherHeightmap>b__60_0(PaintContext.ITerrainInfo t)
			{
				return t.terrain.terrainData.heightmapTexture;
			}

			// Token: 0x060001FC RID: 508 RVA: 0x000066F7 File Offset: 0x000048F7
			internal RenderTexture <ScatterHeightmap>b__61_0(PaintContext.ITerrainInfo t)
			{
				return t.terrain.terrainData.heightmapTexture;
			}

			// Token: 0x060001FD RID: 509 RVA: 0x0000670C File Offset: 0x0000490C
			internal void <ScatterHeightmap>b__61_2(PaintContext.ITerrainInfo t)
			{
				TerrainHeightmapSyncControl syncControl = t.terrain.drawInstanced ? TerrainHeightmapSyncControl.None : TerrainHeightmapSyncControl.HeightAndLod;
				t.terrain.terrainData.DirtyHeightmapRegion(t.clippedTerrainPixels, syncControl);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHeightmap);
			}

			// Token: 0x060001FE RID: 510 RVA: 0x0000674C File Offset: 0x0000494C
			internal Texture <GatherHoles>b__62_0(PaintContext.ITerrainInfo t)
			{
				return t.terrain.terrainData.holesTexture;
			}

			// Token: 0x060001FF RID: 511 RVA: 0x0000675E File Offset: 0x0000495E
			internal Texture <GatherNormals>b__64_0(PaintContext.ITerrainInfo t)
			{
				return t.terrain.normalmapTexture;
			}

			// Token: 0x040000A6 RID: 166
			public static readonly PaintContext.<>c <>9 = new PaintContext.<>c();

			// Token: 0x040000A7 RID: 167
			public static Func<PaintContext.ITerrainInfo, Texture> <>9__60_0;

			// Token: 0x040000A8 RID: 168
			public static Func<PaintContext.ITerrainInfo, RenderTexture> <>9__61_0;

			// Token: 0x040000A9 RID: 169
			public static Action<PaintContext.ITerrainInfo> <>9__61_2;

			// Token: 0x040000AA RID: 170
			public static Func<PaintContext.ITerrainInfo, Texture> <>9__62_0;

			// Token: 0x040000AB RID: 171
			public static Func<PaintContext.ITerrainInfo, Texture> <>9__64_0;
		}

		// Token: 0x0200002B RID: 43
		[CompilerGenerated]
		private sealed class <>c__DisplayClass61_0
		{
			// Token: 0x06000200 RID: 512 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c__DisplayClass61_0()
			{
			}

			// Token: 0x06000201 RID: 513 RVA: 0x0000676C File Offset: 0x0000496C
			internal void <ScatterHeightmap>b__1(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> onTerrainTileBeforePaint = PaintContext.onTerrainTileBeforePaint;
				if (onTerrainTileBeforePaint != null)
				{
					onTerrainTileBeforePaint(t, PaintContext.ToolAction.PaintHeightmap, this.editorUndoName);
				}
				this.blitMaterial.SetFloat("_Height_Offset", (this.<>4__this.heightWorldSpaceMin - t.terrain.GetPosition().y) / t.terrain.terrainData.size.y * PaintContext.kNormalizedHeightScale);
				this.blitMaterial.SetFloat("_Height_Scale", this.<>4__this.heightWorldSpaceSize / t.terrain.terrainData.size.y);
			}

			// Token: 0x040000AC RID: 172
			public string editorUndoName;

			// Token: 0x040000AD RID: 173
			public Material blitMaterial;

			// Token: 0x040000AE RID: 174
			public PaintContext <>4__this;
		}

		// Token: 0x0200002C RID: 44
		[CompilerGenerated]
		private sealed class <>c__DisplayClass63_0
		{
			// Token: 0x06000202 RID: 514 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c__DisplayClass63_0()
			{
			}

			// Token: 0x06000203 RID: 515 RVA: 0x00006810 File Offset: 0x00004A10
			internal RenderTexture <ScatterHoles>b__0(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> onTerrainTileBeforePaint = PaintContext.onTerrainTileBeforePaint;
				if (onTerrainTileBeforePaint != null)
				{
					onTerrainTileBeforePaint(t, PaintContext.ToolAction.PaintHoles, this.editorUndoName);
				}
				t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.HolesTextureName, 0, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHoles);
				return null;
			}

			// Token: 0x040000AF RID: 175
			public string editorUndoName;
		}

		// Token: 0x0200002D RID: 45
		[CompilerGenerated]
		private sealed class <>c__DisplayClass66_0
		{
			// Token: 0x06000204 RID: 516 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c__DisplayClass66_0()
			{
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00006870 File Offset: 0x00004A70
			internal Texture <GatherAlphamap>b__0(PaintContext.ITerrainInfo t)
			{
				PaintContext.SplatmapUserData terrainLayerUserData = this.<>4__this.GetTerrainLayerUserData(t, this.inputLayer, this.addLayerIfDoesntExist);
				bool flag = terrainLayerUserData != null;
				Texture result;
				if (flag)
				{
					result = TerrainPaintUtility.GetTerrainAlphaMapChecked(t.terrain, terrainLayerUserData.mapIndex);
				}
				else
				{
					result = null;
				}
				return result;
			}

			// Token: 0x06000206 RID: 518 RVA: 0x000068B8 File Offset: 0x00004AB8
			internal void <GatherAlphamap>b__1(PaintContext.ITerrainInfo t)
			{
				PaintContext.SplatmapUserData terrainLayerUserData = this.<>4__this.GetTerrainLayerUserData(t, null, false);
				this.copyTerrainLayerMaterial.SetVector("_LayerMask", this.layerMasks[terrainLayerUserData.channelIndex]);
			}

			// Token: 0x040000B0 RID: 176
			public PaintContext <>4__this;

			// Token: 0x040000B1 RID: 177
			public TerrainLayer inputLayer;

			// Token: 0x040000B2 RID: 178
			public bool addLayerIfDoesntExist;

			// Token: 0x040000B3 RID: 179
			public Material copyTerrainLayerMaterial;

			// Token: 0x040000B4 RID: 180
			public Vector4[] layerMasks;
		}

		// Token: 0x0200002E RID: 46
		[CompilerGenerated]
		private sealed class <>c__DisplayClass67_0
		{
			// Token: 0x06000207 RID: 519 RVA: 0x00004B54 File Offset: 0x00002D54
			public <>c__DisplayClass67_0()
			{
			}

			// Token: 0x06000208 RID: 520 RVA: 0x000068F8 File Offset: 0x00004AF8
			internal RenderTexture <ScatterAlphamap>b__0(PaintContext.ITerrainInfo t)
			{
				PaintContext.SplatmapUserData terrainLayerUserData = this.<>4__this.GetTerrainLayerUserData(t, null, false);
				bool flag = terrainLayerUserData != null;
				if (flag)
				{
					Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> onTerrainTileBeforePaint = PaintContext.onTerrainTileBeforePaint;
					if (onTerrainTileBeforePaint != null)
					{
						onTerrainTileBeforePaint(t, PaintContext.ToolAction.PaintTexture, this.editorUndoName);
					}
					int mapIndex = terrainLayerUserData.mapIndex;
					int channelIndex = terrainLayerUserData.channelIndex;
					Texture2D value = t.terrain.terrainData.alphamapTextures[mapIndex];
					this.<>4__this.destinationRenderTexture.filterMode = FilterMode.Point;
					this.<>4__this.sourceRenderTexture.filterMode = FilterMode.Point;
					for (int i = 0; i <= t.terrain.terrainData.alphamapTextureCount; i++)
					{
						bool flag2 = i == mapIndex;
						if (!flag2)
						{
							int num = (i == t.terrain.terrainData.alphamapTextureCount) ? mapIndex : i;
							Texture2D texture2D = t.terrain.terrainData.alphamapTextures[num];
							bool flag3 = texture2D.width != this.<>4__this.targetTextureWidth || texture2D.height != this.<>4__this.targetTextureHeight;
							if (flag3)
							{
								Debug.LogWarning("PaintContext alphamap operations must use the same resolution for all Terrains - mismatched Terrains are ignored.", t.terrain);
							}
							else
							{
								RenderTexture.active = this.tempTarget;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)this.tempTarget.width, 0f, (float)this.tempTarget.height);
								this.copyTerrainLayerMaterial.SetTexture("_MainTex", this.<>4__this.destinationRenderTexture);
								this.copyTerrainLayerMaterial.SetTexture("_OldAlphaMapTexture", this.<>4__this.sourceRenderTexture);
								this.copyTerrainLayerMaterial.SetTexture("_OriginalTargetAlphaMap", value);
								this.copyTerrainLayerMaterial.SetTexture("_AlphaMapTexture", texture2D);
								this.copyTerrainLayerMaterial.SetVector("_LayerMask", (num == mapIndex) ? this.layerMasks[channelIndex] : Vector4.zero);
								this.copyTerrainLayerMaterial.SetVector("_OriginalTargetAlphaMask", this.layerMasks[channelIndex]);
								this.copyTerrainLayerMaterial.SetPass(1);
								TerrainPaintUtility.DrawQuad2(t.clippedPCPixels, t.clippedPCPixels, this.<>4__this.destinationRenderTexture, t.clippedTerrainPixels, texture2D);
								GL.PopMatrix();
								t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.AlphamapTextureName, num, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
							}
						}
					}
					RenderTexture.active = null;
					PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintTexture);
				}
				return null;
			}

			// Token: 0x040000B5 RID: 181
			public PaintContext <>4__this;

			// Token: 0x040000B6 RID: 182
			public string editorUndoName;

			// Token: 0x040000B7 RID: 183
			public RenderTexture tempTarget;

			// Token: 0x040000B8 RID: 184
			public Material copyTerrainLayerMaterial;

			// Token: 0x040000B9 RID: 185
			public Vector4[] layerMasks;
		}
	}
}
