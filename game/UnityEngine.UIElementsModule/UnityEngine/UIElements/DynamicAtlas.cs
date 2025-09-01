using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000A RID: 10
	internal class DynamicAtlas : AtlasBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000221E File Offset: 0x0000041E
		internal bool isInitialized
		{
			get
			{
				return this.m_PointPage != null || this.m_BilinearPage != null;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002234 File Offset: 0x00000434
		protected override void OnAssignedToPanel(IPanel panel)
		{
			base.OnAssignedToPanel(panel);
			this.m_Panels.Add(panel);
			bool flag = this.m_Panels.Count == 1;
			if (flag)
			{
				this.m_ColorSpace = QualitySettings.activeColorSpace;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002274 File Offset: 0x00000474
		protected override void OnRemovedFromPanel(IPanel panel)
		{
			this.m_Panels.Remove(panel);
			bool flag = this.m_Panels.Count == 0 && this.isInitialized;
			if (flag)
			{
				this.DestroyPages();
			}
			base.OnRemovedFromPanel(panel);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022B8 File Offset: 0x000004B8
		public override void Reset()
		{
			bool isInitialized = this.isInitialized;
			if (isInitialized)
			{
				this.DestroyPages();
				int i = 0;
				int count = this.m_Panels.Count;
				while (i < count)
				{
					AtlasBase.RepaintTexturedElements(this.m_Panels[i]);
					i++;
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002308 File Offset: 0x00000508
		private void InitPages()
		{
			int value = Mathf.Max(this.m_MaxSubTextureSize, 1);
			value = Mathf.NextPowerOfTwo(value);
			int num = Mathf.Max(this.m_MaxAtlasSize, 1);
			num = Mathf.NextPowerOfTwo(num);
			num = Mathf.Min(num, SystemInfo.maxRenderTextureSize);
			int num2 = Mathf.Max(this.m_MinAtlasSize, 1);
			num2 = Mathf.NextPowerOfTwo(num2);
			num2 = Mathf.Min(num2, num);
			Vector2Int minSize = new Vector2Int(num2, num2);
			Vector2Int maxSize = new Vector2Int(num, num);
			this.m_PointPage = new DynamicAtlasPage(RenderTextureFormat.ARGB32, FilterMode.Point, minSize, maxSize);
			this.m_BilinearPage = new DynamicAtlasPage(RenderTextureFormat.ARGB32, FilterMode.Bilinear, minSize, maxSize);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002398 File Offset: 0x00000598
		private void DestroyPages()
		{
			this.m_PointPage.Dispose();
			this.m_PointPage = null;
			this.m_BilinearPage.Dispose();
			this.m_BilinearPage = null;
			this.m_Database.Clear();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023D0 File Offset: 0x000005D0
		public override bool TryGetAtlas(VisualElement ve, Texture2D src, out TextureId atlas, out RectInt atlasRect)
		{
			bool flag = this.m_Panels.Count == 0 || src == null;
			bool result;
			if (flag)
			{
				atlas = TextureId.invalid;
				atlasRect = default(RectInt);
				result = false;
			}
			else
			{
				bool flag2 = !this.isInitialized;
				if (flag2)
				{
					this.InitPages();
				}
				DynamicAtlas.TextureInfo textureInfo;
				bool flag3 = this.m_Database.TryGetValue(src, out textureInfo);
				if (flag3)
				{
					atlas = textureInfo.page.textureId;
					atlasRect = textureInfo.rect;
					textureInfo.counter++;
					result = true;
				}
				else
				{
					Allocator2D.Alloc2D alloc;
					bool flag4 = this.IsTextureValid(src, FilterMode.Bilinear) && this.m_BilinearPage.TryAdd(src, out alloc, out atlasRect);
					if (flag4)
					{
						textureInfo = DynamicAtlas.TextureInfo.pool.Get();
						textureInfo.alloc = alloc;
						textureInfo.counter = 1;
						textureInfo.page = this.m_BilinearPage;
						textureInfo.rect = atlasRect;
						this.m_Database[src] = textureInfo;
						atlas = this.m_BilinearPage.textureId;
						result = true;
					}
					else
					{
						bool flag5 = this.IsTextureValid(src, FilterMode.Point) && this.m_PointPage.TryAdd(src, out alloc, out atlasRect);
						if (flag5)
						{
							textureInfo = DynamicAtlas.TextureInfo.pool.Get();
							textureInfo.alloc = alloc;
							textureInfo.counter = 1;
							textureInfo.page = this.m_PointPage;
							textureInfo.rect = atlasRect;
							this.m_Database[src] = textureInfo;
							atlas = this.m_PointPage.textureId;
							result = true;
						}
						else
						{
							atlas = TextureId.invalid;
							atlasRect = default(RectInt);
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002580 File Offset: 0x00000780
		public override void ReturnAtlas(VisualElement ve, Texture2D src, TextureId atlas)
		{
			DynamicAtlas.TextureInfo textureInfo;
			bool flag = this.m_Database.TryGetValue(src, out textureInfo);
			if (flag)
			{
				textureInfo.counter--;
				bool flag2 = textureInfo.counter == 0;
				if (flag2)
				{
					textureInfo.page.Remove(textureInfo.alloc);
					this.m_Database.Remove(src);
					DynamicAtlas.TextureInfo.pool.Return(textureInfo);
				}
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025EC File Offset: 0x000007EC
		protected override void OnUpdateDynamicTextures(IPanel panel)
		{
			bool flag = this.m_PointPage != null;
			if (flag)
			{
				this.m_PointPage.Commit();
				base.SetDynamicTexture(this.m_PointPage.textureId, this.m_PointPage.atlas);
			}
			bool flag2 = this.m_BilinearPage != null;
			if (flag2)
			{
				this.m_BilinearPage.Commit();
				base.SetDynamicTexture(this.m_BilinearPage.textureId, this.m_BilinearPage.atlas);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000266C File Offset: 0x0000086C
		internal static bool IsTextureFormatSupported(TextureFormat format)
		{
			switch (format)
			{
			case TextureFormat.Alpha8:
			case TextureFormat.ARGB4444:
			case TextureFormat.RGB24:
			case TextureFormat.RGBA32:
			case TextureFormat.ARGB32:
			case TextureFormat.RGB565:
			case TextureFormat.R16:
			case TextureFormat.DXT1:
			case TextureFormat.DXT5:
			case TextureFormat.RGBA4444:
			case TextureFormat.BGRA32:
			case TextureFormat.BC7:
			case TextureFormat.BC4:
			case TextureFormat.BC5:
			case TextureFormat.DXT1Crunched:
			case TextureFormat.DXT5Crunched:
			case TextureFormat.PVRTC_RGB2:
			case TextureFormat.PVRTC_RGBA2:
			case TextureFormat.PVRTC_RGB4:
			case TextureFormat.PVRTC_RGBA4:
			case TextureFormat.ETC_RGB4:
			case TextureFormat.EAC_R:
			case TextureFormat.EAC_R_SIGNED:
			case TextureFormat.EAC_RG:
			case TextureFormat.EAC_RG_SIGNED:
			case TextureFormat.ETC2_RGB:
			case TextureFormat.ETC2_RGBA1:
			case TextureFormat.ETC2_RGBA8:
			case TextureFormat.ASTC_4x4:
			case TextureFormat.ASTC_5x5:
			case TextureFormat.ASTC_6x6:
			case TextureFormat.ASTC_8x8:
			case TextureFormat.ASTC_10x10:
			case TextureFormat.ASTC_12x12:
			case TextureFormat.ASTC_RGBA_4x4:
			case TextureFormat.ASTC_RGBA_5x5:
			case TextureFormat.ASTC_RGBA_6x6:
			case TextureFormat.ASTC_RGBA_8x8:
			case TextureFormat.ASTC_RGBA_10x10:
			case TextureFormat.ASTC_RGBA_12x12:
			case TextureFormat.ETC_RGB4_3DS:
			case TextureFormat.ETC_RGBA8_3DS:
			case TextureFormat.RG16:
			case TextureFormat.R8:
			case TextureFormat.ETC_RGB4Crunched:
			case TextureFormat.ETC2_RGBA8Crunched:
				return true;
			case TextureFormat.RHalf:
			case TextureFormat.RGHalf:
			case TextureFormat.RGBAHalf:
			case TextureFormat.RFloat:
			case TextureFormat.RGFloat:
			case TextureFormat.RGBAFloat:
			case TextureFormat.YUY2:
			case TextureFormat.RGB9e5Float:
			case TextureFormat.BC6H:
			case TextureFormat.ASTC_HDR_4x4:
			case TextureFormat.ASTC_HDR_5x5:
			case TextureFormat.ASTC_HDR_6x6:
			case TextureFormat.ASTC_HDR_8x8:
			case TextureFormat.ASTC_HDR_10x10:
			case TextureFormat.ASTC_HDR_12x12:
			case TextureFormat.RG32:
			case TextureFormat.RGB48:
			case TextureFormat.RGBA64:
				return false;
			}
			return false;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027C0 File Offset: 0x000009C0
		public virtual bool IsTextureValid(Texture2D texture, FilterMode atlasFilterMode)
		{
			DynamicAtlasFilters activeFilters = this.m_ActiveFilters;
			bool flag = this.m_CustomFilter != null && !this.m_CustomFilter(texture, ref activeFilters);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = (activeFilters & DynamicAtlasFilters.Readability) > DynamicAtlasFilters.None;
				bool flag3 = (activeFilters & DynamicAtlasFilters.Size) > DynamicAtlasFilters.None;
				bool flag4 = (activeFilters & DynamicAtlasFilters.Format) > DynamicAtlasFilters.None;
				bool flag5 = (activeFilters & DynamicAtlasFilters.ColorSpace) > DynamicAtlasFilters.None;
				bool flag6 = (activeFilters & DynamicAtlasFilters.FilterMode) > DynamicAtlasFilters.None;
				bool flag7 = flag2 && texture.isReadable;
				if (flag7)
				{
					result = false;
				}
				else
				{
					bool flag8 = flag3 && (texture.width > this.maxSubTextureSize || texture.height > this.maxSubTextureSize);
					if (flag8)
					{
						result = false;
					}
					else
					{
						bool flag9 = flag4 && !DynamicAtlas.IsTextureFormatSupported(texture.format);
						if (flag9)
						{
							result = false;
						}
						else
						{
							bool flag10 = flag5 && this.m_ColorSpace == ColorSpace.Linear && texture.activeTextureColorSpace > ColorSpace.Gamma;
							if (flag10)
							{
								result = false;
							}
							else
							{
								bool flag11 = flag6 && texture.filterMode != atlasFilterMode;
								result = !flag11;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028D8 File Offset: 0x00000AD8
		public void SetDirty(Texture2D tex)
		{
			bool flag = tex == null;
			if (!flag)
			{
				DynamicAtlas.TextureInfo textureInfo;
				bool flag2 = this.m_Database.TryGetValue(tex, out textureInfo);
				if (flag2)
				{
					textureInfo.page.Update(tex, textureInfo.rect);
				}
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002918 File Offset: 0x00000B18
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002930 File Offset: 0x00000B30
		public int minAtlasSize
		{
			get
			{
				return this.m_MinAtlasSize;
			}
			set
			{
				bool flag = this.m_MinAtlasSize == value;
				if (!flag)
				{
					this.m_MinAtlasSize = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000295C File Offset: 0x00000B5C
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002974 File Offset: 0x00000B74
		public int maxAtlasSize
		{
			get
			{
				return this.m_MaxAtlasSize;
			}
			set
			{
				bool flag = this.m_MaxAtlasSize == value;
				if (!flag)
				{
					this.m_MaxAtlasSize = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000299F File Offset: 0x00000B9F
		public static DynamicAtlasFilters defaultFilters
		{
			get
			{
				return DynamicAtlasFilters.Readability | DynamicAtlasFilters.Size | DynamicAtlasFilters.Format | DynamicAtlasFilters.ColorSpace | DynamicAtlasFilters.FilterMode;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000029A4 File Offset: 0x00000BA4
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000029BC File Offset: 0x00000BBC
		public DynamicAtlasFilters activeFilters
		{
			get
			{
				return this.m_ActiveFilters;
			}
			set
			{
				bool flag = this.m_ActiveFilters == value;
				if (!flag)
				{
					this.m_ActiveFilters = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000029E8 File Offset: 0x00000BE8
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002A00 File Offset: 0x00000C00
		public int maxSubTextureSize
		{
			get
			{
				return this.m_MaxSubTextureSize;
			}
			set
			{
				bool flag = this.m_MaxSubTextureSize == value;
				if (!flag)
				{
					this.m_MaxSubTextureSize = value;
					this.Reset();
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A2C File Offset: 0x00000C2C
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002A44 File Offset: 0x00000C44
		public DynamicAtlasCustomFilter customFilter
		{
			get
			{
				return this.m_CustomFilter;
			}
			set
			{
				bool flag = this.m_CustomFilter == value;
				if (!flag)
				{
					this.m_CustomFilter = value;
					this.Reset();
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A74 File Offset: 0x00000C74
		public DynamicAtlas()
		{
		}

		// Token: 0x0400000B RID: 11
		private Dictionary<Texture, DynamicAtlas.TextureInfo> m_Database = new Dictionary<Texture, DynamicAtlas.TextureInfo>();

		// Token: 0x0400000C RID: 12
		private DynamicAtlasPage m_PointPage;

		// Token: 0x0400000D RID: 13
		private DynamicAtlasPage m_BilinearPage;

		// Token: 0x0400000E RID: 14
		private ColorSpace m_ColorSpace;

		// Token: 0x0400000F RID: 15
		private List<IPanel> m_Panels = new List<IPanel>(1);

		// Token: 0x04000010 RID: 16
		private int m_MinAtlasSize = 64;

		// Token: 0x04000011 RID: 17
		private int m_MaxAtlasSize = 4096;

		// Token: 0x04000012 RID: 18
		private int m_MaxSubTextureSize = 64;

		// Token: 0x04000013 RID: 19
		private DynamicAtlasFilters m_ActiveFilters = DynamicAtlas.defaultFilters;

		// Token: 0x04000014 RID: 20
		private DynamicAtlasCustomFilter m_CustomFilter;

		// Token: 0x0200000B RID: 11
		private class TextureInfo : LinkedPoolItem<DynamicAtlas.TextureInfo>
		{
			// Token: 0x06000038 RID: 56 RVA: 0x00002AC5 File Offset: 0x00000CC5
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static DynamicAtlas.TextureInfo Create()
			{
				return new DynamicAtlas.TextureInfo();
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00002ACC File Offset: 0x00000CCC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void Reset(DynamicAtlas.TextureInfo info)
			{
				info.page = null;
				info.counter = 0;
				info.alloc = default(Allocator2D.Alloc2D);
				info.rect = default(RectInt);
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00002AF5 File Offset: 0x00000CF5
			public TextureInfo()
			{
			}

			// Token: 0x0600003B RID: 59 RVA: 0x00002AFE File Offset: 0x00000CFE
			// Note: this type is marked as 'beforefieldinit'.
			static TextureInfo()
			{
			}

			// Token: 0x04000015 RID: 21
			public DynamicAtlasPage page;

			// Token: 0x04000016 RID: 22
			public int counter;

			// Token: 0x04000017 RID: 23
			public Allocator2D.Alloc2D alloc;

			// Token: 0x04000018 RID: 24
			public RectInt rect;

			// Token: 0x04000019 RID: 25
			public static readonly LinkedPool<DynamicAtlas.TextureInfo> pool = new LinkedPool<DynamicAtlas.TextureInfo>(new Func<DynamicAtlas.TextureInfo>(DynamicAtlas.TextureInfo.Create), new Action<DynamicAtlas.TextureInfo>(DynamicAtlas.TextureInfo.Reset), 1024);
		}
	}
}
