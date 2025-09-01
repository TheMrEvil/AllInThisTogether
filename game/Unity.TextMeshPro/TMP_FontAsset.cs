using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x0200002E RID: 46
	[ExcludeFromPreset]
	[Serializable]
	public class TMP_FontAsset : TMP_Asset
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0001973C File Offset: 0x0001793C
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00019744 File Offset: 0x00017944
		public string version
		{
			get
			{
				return this.m_Version;
			}
			internal set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0001974D File Offset: 0x0001794D
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00019755 File Offset: 0x00017955
		public Font sourceFontFile
		{
			get
			{
				return this.m_SourceFontFile;
			}
			internal set
			{
				this.m_SourceFontFile = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0001975E File Offset: 0x0001795E
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00019766 File Offset: 0x00017966
		public AtlasPopulationMode atlasPopulationMode
		{
			get
			{
				return this.m_AtlasPopulationMode;
			}
			set
			{
				this.m_AtlasPopulationMode = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0001976F File Offset: 0x0001796F
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00019777 File Offset: 0x00017977
		public FaceInfo faceInfo
		{
			get
			{
				return this.m_FaceInfo;
			}
			set
			{
				this.m_FaceInfo = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00019780 File Offset: 0x00017980
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00019788 File Offset: 0x00017988
		public List<Glyph> glyphTable
		{
			get
			{
				return this.m_GlyphTable;
			}
			internal set
			{
				this.m_GlyphTable = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00019791 File Offset: 0x00017991
		public Dictionary<uint, Glyph> glyphLookupTable
		{
			get
			{
				if (this.m_GlyphLookupDictionary == null)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_GlyphLookupDictionary;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000197A7 File Offset: 0x000179A7
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000197AF File Offset: 0x000179AF
		public List<TMP_Character> characterTable
		{
			get
			{
				return this.m_CharacterTable;
			}
			internal set
			{
				this.m_CharacterTable = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000197B8 File Offset: 0x000179B8
		public Dictionary<uint, TMP_Character> characterLookupTable
		{
			get
			{
				if (this.m_CharacterLookupDictionary == null)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_CharacterLookupDictionary;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000197CE File Offset: 0x000179CE
		public Texture2D atlasTexture
		{
			get
			{
				if (this.m_AtlasTexture == null)
				{
					this.m_AtlasTexture = this.atlasTextures[0];
				}
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000197F2 File Offset: 0x000179F2
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00019801 File Offset: 0x00017A01
		public Texture2D[] atlasTextures
		{
			get
			{
				Texture2D[] atlasTextures = this.m_AtlasTextures;
				return this.m_AtlasTextures;
			}
			set
			{
				this.m_AtlasTextures = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0001980A File Offset: 0x00017A0A
		public int atlasTextureCount
		{
			get
			{
				return this.m_AtlasTextureIndex + 1;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00019814 File Offset: 0x00017A14
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0001981C File Offset: 0x00017A1C
		public bool isMultiAtlasTexturesEnabled
		{
			get
			{
				return this.m_IsMultiAtlasTexturesEnabled;
			}
			set
			{
				this.m_IsMultiAtlasTexturesEnabled = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00019825 File Offset: 0x00017A25
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0001982D File Offset: 0x00017A2D
		internal bool clearDynamicDataOnBuild
		{
			get
			{
				return this.m_ClearDynamicDataOnBuild;
			}
			set
			{
				this.m_ClearDynamicDataOnBuild = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00019836 File Offset: 0x00017A36
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0001983E File Offset: 0x00017A3E
		internal List<GlyphRect> usedGlyphRects
		{
			get
			{
				return this.m_UsedGlyphRects;
			}
			set
			{
				this.m_UsedGlyphRects = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00019847 File Offset: 0x00017A47
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0001984F File Offset: 0x00017A4F
		internal List<GlyphRect> freeGlyphRects
		{
			get
			{
				return this.m_FreeGlyphRects;
			}
			set
			{
				this.m_FreeGlyphRects = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00019858 File Offset: 0x00017A58
		[Obsolete("The fontInfo property and underlying type is now obsolete. Please use the faceInfo property and FaceInfo type instead.")]
		public FaceInfo_Legacy fontInfo
		{
			get
			{
				return this.m_fontInfo;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00019860 File Offset: 0x00017A60
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00019868 File Offset: 0x00017A68
		public int atlasWidth
		{
			get
			{
				return this.m_AtlasWidth;
			}
			internal set
			{
				this.m_AtlasWidth = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00019871 File Offset: 0x00017A71
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00019879 File Offset: 0x00017A79
		public int atlasHeight
		{
			get
			{
				return this.m_AtlasHeight;
			}
			internal set
			{
				this.m_AtlasHeight = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00019882 File Offset: 0x00017A82
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0001988A File Offset: 0x00017A8A
		public int atlasPadding
		{
			get
			{
				return this.m_AtlasPadding;
			}
			internal set
			{
				this.m_AtlasPadding = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00019893 File Offset: 0x00017A93
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0001989B File Offset: 0x00017A9B
		public GlyphRenderMode atlasRenderMode
		{
			get
			{
				return this.m_AtlasRenderMode;
			}
			internal set
			{
				this.m_AtlasRenderMode = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000198A4 File Offset: 0x00017AA4
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x000198AC File Offset: 0x00017AAC
		public TMP_FontFeatureTable fontFeatureTable
		{
			get
			{
				return this.m_FontFeatureTable;
			}
			internal set
			{
				this.m_FontFeatureTable = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000198B5 File Offset: 0x00017AB5
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000198BD File Offset: 0x00017ABD
		public List<TMP_FontAsset> fallbackFontAssetTable
		{
			get
			{
				return this.m_FallbackFontAssetTable;
			}
			set
			{
				this.m_FallbackFontAssetTable = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000198C6 File Offset: 0x00017AC6
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000198CE File Offset: 0x00017ACE
		public FontAssetCreationSettings creationSettings
		{
			get
			{
				return this.m_CreationSettings;
			}
			set
			{
				this.m_CreationSettings = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000198D7 File Offset: 0x00017AD7
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000198DF File Offset: 0x00017ADF
		public TMP_FontWeightPair[] fontWeightTable
		{
			get
			{
				return this.m_FontWeightTable;
			}
			internal set
			{
				this.m_FontWeightTable = value;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000198E8 File Offset: 0x00017AE8
		public static TMP_FontAsset CreateFontAsset(Font font)
		{
			return TMP_FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00019908 File Offset: 0x00017B08
		public static TMP_FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			FontEngine.InitializeFontEngine();
			if (FontEngine.LoadFontFace(font, samplingPointSize) != FontEngineError.Success)
			{
				Debug.LogWarning("Unable to load font face for [" + font.name + "]. Make sure \"Include Font Data\" is enabled in the Font Import Settings.", font);
				return null;
			}
			TMP_FontAsset tmp_FontAsset = ScriptableObject.CreateInstance<TMP_FontAsset>();
			tmp_FontAsset.m_Version = "1.1.0";
			tmp_FontAsset.faceInfo = FontEngine.GetFaceInfo();
			if (atlasPopulationMode == AtlasPopulationMode.Dynamic)
			{
				tmp_FontAsset.sourceFontFile = font;
			}
			tmp_FontAsset.atlasPopulationMode = atlasPopulationMode;
			tmp_FontAsset.atlasWidth = atlasWidth;
			tmp_FontAsset.atlasHeight = atlasHeight;
			tmp_FontAsset.atlasPadding = atlasPadding;
			tmp_FontAsset.atlasRenderMode = renderMode;
			tmp_FontAsset.atlasTextures = new Texture2D[1];
			Texture2D texture2D = new Texture2D(0, 0, TextureFormat.Alpha8, false);
			tmp_FontAsset.atlasTextures[0] = texture2D;
			tmp_FontAsset.isMultiAtlasTexturesEnabled = enableMultiAtlasSupport;
			int num;
			if ((renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16)
			{
				num = 0;
				Material material = new Material(ShaderUtilities.ShaderRef_MobileBitmap);
				material.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				tmp_FontAsset.material = material;
			}
			else
			{
				num = 1;
				Material material2 = new Material(ShaderUtilities.ShaderRef_MobileSDF);
				material2.SetTexture(ShaderUtilities.ID_MainTex, texture2D);
				material2.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material2.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				material2.SetFloat(ShaderUtilities.ID_GradientScale, (float)(atlasPadding + num));
				material2.SetFloat(ShaderUtilities.ID_WeightNormal, tmp_FontAsset.normalStyle);
				material2.SetFloat(ShaderUtilities.ID_WeightBold, tmp_FontAsset.boldStyle);
				tmp_FontAsset.material = material2;
			}
			tmp_FontAsset.freeGlyphRects = new List<GlyphRect>(8)
			{
				new GlyphRect(0, 0, atlasWidth - num, atlasHeight - num)
			};
			tmp_FontAsset.usedGlyphRects = new List<GlyphRect>(8);
			tmp_FontAsset.ReadFontAssetDefinition();
			return tmp_FontAsset;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00019AA5 File Offset: 0x00017CA5
		private void Awake()
		{
			if (this.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeFontAsset();
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00019AC8 File Offset: 0x00017CC8
		public void ReadFontAssetDefinition()
		{
			if (this.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeFontAsset();
			}
			this.InitializeDictionaryLookupTables();
			this.AddSynthesizedCharactersAndFaceMetrics();
			if (this.m_FaceInfo.scale == 0f)
			{
				this.m_FaceInfo.scale = 1f;
			}
			if (this.m_FaceInfo.strikethroughOffset == 0f)
			{
				this.m_FaceInfo.strikethroughOffset = this.m_FaceInfo.capLine / 2.5f;
			}
			if (this.m_AtlasPadding == 0 && this.material.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				this.m_AtlasPadding = (int)this.material.GetFloat(ShaderUtilities.ID_GradientScale) - 1;
			}
			this.hashCode = TMP_TextUtilities.GetSimpleHashCode(base.name);
			this.materialHashCode = TMP_TextUtilities.GetSimpleHashCode(base.name + TMP_FontAsset.s_DefaultMaterialSuffix);
			this.IsFontAssetLookupTablesDirty = false;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00019BB8 File Offset: 0x00017DB8
		internal void InitializeDictionaryLookupTables()
		{
			this.InitializeGlyphLookupDictionary();
			this.InitializeCharacterLookupDictionary();
			this.InitializeGlyphPaidAdjustmentRecordsLookupDictionary();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00019BCC File Offset: 0x00017DCC
		internal void InitializeGlyphLookupDictionary()
		{
			if (this.m_GlyphLookupDictionary == null)
			{
				this.m_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
			}
			else
			{
				this.m_GlyphLookupDictionary.Clear();
			}
			if (this.m_GlyphIndexList == null)
			{
				this.m_GlyphIndexList = new List<uint>();
			}
			else
			{
				this.m_GlyphIndexList.Clear();
			}
			if (this.m_GlyphIndexListNewlyAdded == null)
			{
				this.m_GlyphIndexListNewlyAdded = new List<uint>();
			}
			else
			{
				this.m_GlyphIndexListNewlyAdded.Clear();
			}
			int count = this.m_GlyphTable.Count;
			for (int i = 0; i < count; i++)
			{
				Glyph glyph = this.m_GlyphTable[i];
				uint index = glyph.index;
				if (!this.m_GlyphLookupDictionary.ContainsKey(index))
				{
					this.m_GlyphLookupDictionary.Add(index, glyph);
					this.m_GlyphIndexList.Add(index);
				}
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00019C8C File Offset: 0x00017E8C
		internal void InitializeCharacterLookupDictionary()
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				this.m_CharacterLookupDictionary = new Dictionary<uint, TMP_Character>();
			}
			else
			{
				this.m_CharacterLookupDictionary.Clear();
			}
			for (int i = 0; i < this.m_CharacterTable.Count; i++)
			{
				TMP_Character tmp_Character = this.m_CharacterTable[i];
				uint unicode = tmp_Character.unicode;
				uint glyphIndex = tmp_Character.glyphIndex;
				if (!this.m_CharacterLookupDictionary.ContainsKey(unicode))
				{
					this.m_CharacterLookupDictionary.Add(unicode, tmp_Character);
					tmp_Character.textAsset = this;
					tmp_Character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
				}
			}
			if (this.FallbackSearchQueryLookup == null)
			{
				this.FallbackSearchQueryLookup = new HashSet<int>();
				return;
			}
			this.FallbackSearchQueryLookup.Clear();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00019D40 File Offset: 0x00017F40
		internal void InitializeGlyphPaidAdjustmentRecordsLookupDictionary()
		{
			if (this.m_KerningTable != null && this.m_KerningTable.kerningPairs != null && this.m_KerningTable.kerningPairs.Count > 0)
			{
				this.UpgradeGlyphAdjustmentTableToFontFeatureTable();
			}
			if (this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary == null)
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary = new Dictionary<uint, TMP_GlyphPairAdjustmentRecord>();
			}
			else
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Clear();
			}
			List<TMP_GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords;
			if (glyphPairAdjustmentRecords != null)
			{
				for (int i = 0; i < glyphPairAdjustmentRecords.Count; i++)
				{
					TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord = glyphPairAdjustmentRecords[i];
					uint key = new GlyphPairKey(tmp_GlyphPairAdjustmentRecord).key;
					if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.ContainsKey(key))
					{
						this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Add(key, tmp_GlyphPairAdjustmentRecord);
					}
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00019E04 File Offset: 0x00018004
		internal void AddSynthesizedCharactersAndFaceMetrics()
		{
			bool isFontFaceLoaded = false;
			if (this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic)
			{
				isFontFaceLoaded = (FontEngine.LoadFontFace(this.sourceFontFile, this.m_FaceInfo.pointSize) == FontEngineError.Success);
			}
			this.AddSynthesizedCharacter(3U, isFontFaceLoaded, true);
			this.AddSynthesizedCharacter(9U, isFontFaceLoaded, true);
			this.AddSynthesizedCharacter(10U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(11U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(13U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(1564U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8203U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8206U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8207U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8232U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8233U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8288U, isFontFaceLoaded, false);
			if (this.m_FaceInfo.capLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(88U))
			{
				uint glyphIndex = this.m_CharacterLookupDictionary[88U].glyphIndex;
				this.m_FaceInfo.capLine = this.m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
			}
			if (this.m_FaceInfo.meanLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(120U))
			{
				uint glyphIndex2 = this.m_CharacterLookupDictionary[120U].glyphIndex;
				this.m_FaceInfo.meanLine = this.m_GlyphLookupDictionary[glyphIndex2].metrics.horizontalBearingY;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00019F74 File Offset: 0x00018174
		private void AddSynthesizedCharacter(uint unicode, bool isFontFaceLoaded, bool addImmediately = false)
		{
			if (this.m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return;
			}
			Glyph glyph;
			if (!isFontFaceLoaded || FontEngine.GetGlyphIndex(unicode) == 0U)
			{
				glyph = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(unicode, new TMP_Character(unicode, this, glyph));
				return;
			}
			if (!addImmediately)
			{
				return;
			}
			GlyphLoadFlags flags = ((this.m_AtlasRenderMode & (GlyphRenderMode)4) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP;
			if (FontEngine.TryGetGlyphWithUnicodeValue(unicode, flags, out glyph))
			{
				this.m_CharacterLookupDictionary.Add(unicode, new TMP_Character(unicode, this, glyph));
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0001A014 File Offset: 0x00018214
		internal void AddCharacterToLookupCache(uint unicode, TMP_Character character)
		{
			this.m_CharacterLookupDictionary.Add(unicode, character);
			this.FallbackSearchQueryLookup.Add(character.textAsset.instanceID);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0001A03C File Offset: 0x0001823C
		internal void SortCharacterTable()
		{
			if (this.m_CharacterTable != null && this.m_CharacterTable.Count > 0)
			{
				this.m_CharacterTable = (from c in this.m_CharacterTable
				orderby c.unicode
				select c).ToList<TMP_Character>();
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0001A094 File Offset: 0x00018294
		internal void SortGlyphTable()
		{
			if (this.m_GlyphTable != null && this.m_GlyphTable.Count > 0)
			{
				this.m_GlyphTable = (from c in this.m_GlyphTable
				orderby c.index
				select c).ToList<Glyph>();
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0001A0EC File Offset: 0x000182EC
		internal void SortFontFeatureTable()
		{
			this.m_FontFeatureTable.SortGlyphPairAdjustmentRecords();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0001A0F9 File Offset: 0x000182F9
		internal void SortAllTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
			this.SortFontFeatureTable();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0001A10D File Offset: 0x0001830D
		public bool HasCharacter(int character)
		{
			return this.m_CharacterLookupDictionary != null && this.m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0001A128 File Offset: 0x00018328
		public bool HasCharacter(char character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
				if (this.m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			if (this.m_CharacterLookupDictionary.ContainsKey((uint)character))
			{
				return true;
			}
			TMP_Character tmp_Character;
			if (tryAddCharacter && this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic && this.TryAddCharacterInternal((uint)character, out tmp_Character))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (TMP_FontAsset.k_SearchedFontAssetLookup == null)
				{
					TMP_FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
				}
				else
				{
					TMP_FontAsset.k_SearchedFontAssetLookup.Clear();
				}
				TMP_FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
				if (this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0)
				{
					int num = 0;
					while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
					{
						TMP_FontAsset tmp_FontAsset = this.fallbackFontAssetTable[num];
						int instanceID = tmp_FontAsset.GetInstanceID();
						if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID) && tmp_FontAsset.HasCharacter_Internal((uint)character, true, tryAddCharacter))
						{
							return true;
						}
						num++;
					}
				}
				if (TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
				{
					int num2 = 0;
					while (num2 < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[num2] != null)
					{
						TMP_FontAsset tmp_FontAsset2 = TMP_Settings.fallbackFontAssets[num2];
						int instanceID2 = tmp_FontAsset2.GetInstanceID();
						if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID2) && tmp_FontAsset2.HasCharacter_Internal((uint)character, true, tryAddCharacter))
						{
							return true;
						}
						num2++;
					}
				}
				if (TMP_Settings.defaultFontAsset != null)
				{
					TMP_FontAsset defaultFontAsset = TMP_Settings.defaultFontAsset;
					int instanceID3 = defaultFontAsset.GetInstanceID();
					if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID3) && defaultFontAsset.HasCharacter_Internal((uint)character, true, tryAddCharacter))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0001A2C8 File Offset: 0x000184C8
		private bool HasCharacter_Internal(uint character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
				if (this.m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			if (this.m_CharacterLookupDictionary.ContainsKey(character))
			{
				return true;
			}
			TMP_Character tmp_Character;
			if (tryAddCharacter && this.atlasPopulationMode == AtlasPopulationMode.Dynamic && this.TryAddCharacterInternal(character, out tmp_Character))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (this.fallbackFontAssetTable == null || this.fallbackFontAssetTable.Count == 0)
				{
					return false;
				}
				int num = 0;
				while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
				{
					TMP_FontAsset tmp_FontAsset = this.fallbackFontAssetTable[num];
					int instanceID = tmp_FontAsset.GetInstanceID();
					if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID) && tmp_FontAsset.HasCharacter_Internal(character, true, tryAddCharacter))
					{
						return true;
					}
					num++;
				}
			}
			return false;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0001A38C File Offset: 0x0001858C
		public bool HasCharacters(string text, out List<char> missingCharacters)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				missingCharacters = null;
				return false;
			}
			missingCharacters = new List<char>();
			for (int i = 0; i < text.Length; i++)
			{
				if (!this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]))
				{
					missingCharacters.Add(text[i]);
				}
			}
			return missingCharacters.Count == 0;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0001A3EC File Offset: 0x000185EC
		public bool HasCharacters(string text, out uint[] missingCharacters, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			missingCharacters = null;
			if (this.m_CharacterLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
				if (this.m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			this.s_MissingCharacterList.Clear();
			for (int i = 0; i < text.Length; i++)
			{
				bool flag = true;
				uint num = (uint)text[i];
				TMP_Character tmp_Character;
				if (!this.m_CharacterLookupDictionary.ContainsKey(num) && (!tryAddCharacter || this.atlasPopulationMode != AtlasPopulationMode.Dynamic || !this.TryAddCharacterInternal(num, out tmp_Character)))
				{
					if (searchFallbacks)
					{
						if (TMP_FontAsset.k_SearchedFontAssetLookup == null)
						{
							TMP_FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
						}
						else
						{
							TMP_FontAsset.k_SearchedFontAssetLookup.Clear();
						}
						TMP_FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
						if (this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0)
						{
							int num2 = 0;
							while (num2 < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num2] != null)
							{
								TMP_FontAsset tmp_FontAsset = this.fallbackFontAssetTable[num2];
								int instanceID = tmp_FontAsset.GetInstanceID();
								if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID) && tmp_FontAsset.HasCharacter_Internal(num, true, tryAddCharacter))
								{
									flag = false;
									break;
								}
								num2++;
							}
						}
						if (flag && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
						{
							int num3 = 0;
							while (num3 < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[num3] != null)
							{
								TMP_FontAsset tmp_FontAsset2 = TMP_Settings.fallbackFontAssets[num3];
								int instanceID2 = tmp_FontAsset2.GetInstanceID();
								if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID2) && tmp_FontAsset2.HasCharacter_Internal(num, true, tryAddCharacter))
								{
									flag = false;
									break;
								}
								num3++;
							}
						}
						if (flag && TMP_Settings.defaultFontAsset != null)
						{
							TMP_FontAsset defaultFontAsset = TMP_Settings.defaultFontAsset;
							int instanceID3 = defaultFontAsset.GetInstanceID();
							if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(instanceID3) && defaultFontAsset.HasCharacter_Internal(num, true, tryAddCharacter))
							{
								flag = false;
							}
						}
					}
					if (flag)
					{
						this.s_MissingCharacterList.Add(num);
					}
				}
			}
			if (this.s_MissingCharacterList.Count > 0)
			{
				missingCharacters = this.s_MissingCharacterList.ToArray();
				return false;
			}
			return true;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0001A600 File Offset: 0x00018800
		public bool HasCharacters(string text)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				return false;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (!this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0001A640 File Offset: 0x00018840
		public static string GetCharacters(TMP_FontAsset fontAsset)
		{
			string text = string.Empty;
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				text += ((char)fontAsset.characterTable[i].unicode).ToString();
			}
			return text;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001A68C File Offset: 0x0001888C
		public static int[] GetCharactersArray(TMP_FontAsset fontAsset)
		{
			int[] array = new int[fontAsset.characterTable.Count];
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				array[i] = (int)fontAsset.characterTable[i].unicode;
			}
			return array;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0001A6D8 File Offset: 0x000188D8
		internal uint GetGlyphIndex(uint unicode)
		{
			if (this.m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return this.m_CharacterLookupDictionary[unicode].glyphIndex;
			}
			if (FontEngine.LoadFontFace(this.sourceFontFile, this.m_FaceInfo.pointSize) != FontEngineError.Success)
			{
				return 0U;
			}
			return FontEngine.GetGlyphIndex(unicode);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0001A728 File Offset: 0x00018928
		internal static void RegisterFontAssetForFontFeatureUpdate(TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			if (TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Add(instanceID))
			{
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Add(fontAsset);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0001A754 File Offset: 0x00018954
		internal static void UpdateFontFeaturesForFontAssetsInQueue()
		{
			int count = TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Count;
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue[i].UpdateGlyphAdjustmentRecords();
			}
			if (count > 0)
			{
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Clear();
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0001A7A0 File Offset: 0x000189A0
		internal static void RegisterFontAssetForAtlasTextureUpdate(TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			if (TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Add(instanceID))
			{
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Add(fontAsset);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0001A7CC File Offset: 0x000189CC
		internal static void UpdateAtlasTexturesForFontAssetsInQueue()
		{
			int count = TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Count;
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueue[i].TryAddGlyphsToAtlasTextures();
			}
			if (count > 0)
			{
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Clear();
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0001A818 File Offset: 0x00018A18
		public bool TryAddCharacters(uint[] unicodes, bool includeFontFeatures = false)
		{
			uint[] array;
			return this.TryAddCharacters(unicodes, out array, includeFontFeatures);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0001A830 File Offset: 0x00018A30
		public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes, bool includeFontFeatures = false)
		{
			if (unicodes == null || unicodes.Length == 0 || this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
				}
				missingUnicodes = null;
				return false;
			}
			if (FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize) != FontEngineError.Success)
			{
				missingUnicodes = unicodes.ToArray<uint>();
				return false;
			}
			if (this.m_CharacterLookupDictionary == null || this.m_GlyphLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
			}
			this.m_GlyphsToAdd.Clear();
			this.m_GlyphsToAddLookup.Clear();
			this.m_CharactersToAdd.Clear();
			this.m_CharactersToAddLookup.Clear();
			this.s_MissingCharacterList.Clear();
			bool flag = false;
			int num = unicodes.Length;
			for (int i = 0; i < num; i++)
			{
				uint num2 = unicodes[i];
				if (!this.m_CharacterLookupDictionary.ContainsKey(num2))
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(num2);
					if (glyphIndex == 0U)
					{
						if (num2 != 160U)
						{
							if (num2 == 173U || num2 == 8209U)
							{
								glyphIndex = FontEngine.GetGlyphIndex(45U);
							}
						}
						else
						{
							glyphIndex = FontEngine.GetGlyphIndex(32U);
						}
						if (glyphIndex == 0U)
						{
							this.s_MissingCharacterList.Add(num2);
							flag = true;
							goto IL_1CB;
						}
					}
					TMP_Character tmp_Character = new TMP_Character(num2, glyphIndex);
					if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
					{
						tmp_Character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
						tmp_Character.textAsset = this;
						this.m_CharacterTable.Add(tmp_Character);
						this.m_CharacterLookupDictionary.Add(num2, tmp_Character);
					}
					else
					{
						if (this.m_GlyphsToAddLookup.Add(glyphIndex))
						{
							this.m_GlyphsToAdd.Add(glyphIndex);
						}
						if (this.m_CharactersToAddLookup.Add(num2))
						{
							this.m_CharactersToAdd.Add(tmp_Character);
						}
					}
				}
				IL_1CB:;
			}
			if (this.m_GlyphsToAdd.Count == 0)
			{
				missingUnicodes = unicodes;
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			Glyph[] array;
			bool flag2 = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
			int num3 = 0;
			while (num3 < array.Length && array[num3] != null)
			{
				Glyph glyph = array[num3];
				uint index = glyph.index;
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(index, glyph);
				this.m_GlyphIndexListNewlyAdded.Add(index);
				this.m_GlyphIndexList.Add(index);
				num3++;
			}
			this.m_GlyphsToAdd.Clear();
			for (int j = 0; j < this.m_CharactersToAdd.Count; j++)
			{
				TMP_Character tmp_Character2 = this.m_CharactersToAdd[j];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(tmp_Character2.glyphIndex, out glyph2))
				{
					this.m_GlyphsToAdd.Add(tmp_Character2.glyphIndex);
				}
				else
				{
					tmp_Character2.glyph = glyph2;
					tmp_Character2.textAsset = this;
					this.m_CharacterTable.Add(tmp_Character2);
					this.m_CharacterLookupDictionary.Add(tmp_Character2.unicode, tmp_Character2);
					this.m_CharactersToAdd.RemoveAt(j);
					j--;
				}
			}
			if (this.m_IsMultiAtlasTexturesEnabled && !flag2)
			{
				while (!flag2)
				{
					flag2 = this.TryAddGlyphsToNewAtlasTexture();
				}
			}
			if (includeFontFeatures)
			{
				this.UpdateGlyphAdjustmentRecords();
			}
			for (int k = 0; k < this.m_CharactersToAdd.Count; k++)
			{
				TMP_Character tmp_Character3 = this.m_CharactersToAdd[k];
				this.s_MissingCharacterList.Add(tmp_Character3.unicode);
			}
			missingUnicodes = null;
			if (this.s_MissingCharacterList.Count > 0)
			{
				missingUnicodes = this.s_MissingCharacterList.ToArray();
			}
			return flag2 && !flag;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0001AC4C File Offset: 0x00018E4C
		public bool TryAddCharacters(string characters, bool includeFontFeatures = false)
		{
			string text;
			return this.TryAddCharacters(characters, out text, includeFontFeatures);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0001AC64 File Offset: 0x00018E64
		public bool TryAddCharacters(string characters, out string missingCharacters, bool includeFontFeatures = false)
		{
			if (string.IsNullOrEmpty(characters) || this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
				}
				missingCharacters = characters;
				return false;
			}
			if (FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize) != FontEngineError.Success)
			{
				missingCharacters = characters;
				return false;
			}
			if (this.m_CharacterLookupDictionary == null || this.m_GlyphLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
			}
			this.m_GlyphsToAdd.Clear();
			this.m_GlyphsToAddLookup.Clear();
			this.m_CharactersToAdd.Clear();
			this.m_CharactersToAddLookup.Clear();
			this.s_MissingCharacterList.Clear();
			bool flag = false;
			int length = characters.Length;
			for (int i = 0; i < length; i++)
			{
				uint num = (uint)characters[i];
				if (!this.m_CharacterLookupDictionary.ContainsKey(num))
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(num);
					if (glyphIndex == 0U)
					{
						if (num != 160U)
						{
							if (num == 173U || num == 8209U)
							{
								glyphIndex = FontEngine.GetGlyphIndex(45U);
							}
						}
						else
						{
							glyphIndex = FontEngine.GetGlyphIndex(32U);
						}
						if (glyphIndex == 0U)
						{
							this.s_MissingCharacterList.Add(num);
							flag = true;
							goto IL_1CE;
						}
					}
					TMP_Character tmp_Character = new TMP_Character(num, glyphIndex);
					if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
					{
						tmp_Character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
						tmp_Character.textAsset = this;
						this.m_CharacterTable.Add(tmp_Character);
						this.m_CharacterLookupDictionary.Add(num, tmp_Character);
					}
					else
					{
						if (this.m_GlyphsToAddLookup.Add(glyphIndex))
						{
							this.m_GlyphsToAdd.Add(glyphIndex);
						}
						if (this.m_CharactersToAddLookup.Add(num))
						{
							this.m_CharactersToAdd.Add(tmp_Character);
						}
					}
				}
				IL_1CE:;
			}
			if (this.m_GlyphsToAdd.Count == 0)
			{
				missingCharacters = characters;
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			Glyph[] array;
			bool flag2 = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
			int num2 = 0;
			while (num2 < array.Length && array[num2] != null)
			{
				Glyph glyph = array[num2];
				uint index = glyph.index;
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(index, glyph);
				this.m_GlyphIndexListNewlyAdded.Add(index);
				this.m_GlyphIndexList.Add(index);
				num2++;
			}
			this.m_GlyphsToAdd.Clear();
			for (int j = 0; j < this.m_CharactersToAdd.Count; j++)
			{
				TMP_Character tmp_Character2 = this.m_CharactersToAdd[j];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(tmp_Character2.glyphIndex, out glyph2))
				{
					this.m_GlyphsToAdd.Add(tmp_Character2.glyphIndex);
				}
				else
				{
					tmp_Character2.glyph = glyph2;
					tmp_Character2.textAsset = this;
					this.m_CharacterTable.Add(tmp_Character2);
					this.m_CharacterLookupDictionary.Add(tmp_Character2.unicode, tmp_Character2);
					this.m_CharactersToAdd.RemoveAt(j);
					j--;
				}
			}
			if (this.m_IsMultiAtlasTexturesEnabled && !flag2)
			{
				while (!flag2)
				{
					flag2 = this.TryAddGlyphsToNewAtlasTexture();
				}
			}
			if (includeFontFeatures)
			{
				this.UpdateGlyphAdjustmentRecords();
			}
			missingCharacters = string.Empty;
			for (int k = 0; k < this.m_CharactersToAdd.Count; k++)
			{
				TMP_Character tmp_Character3 = this.m_CharactersToAdd[k];
				this.s_MissingCharacterList.Add(tmp_Character3.unicode);
			}
			if (this.s_MissingCharacterList.Count > 0)
			{
				missingCharacters = this.s_MissingCharacterList.UintToString();
			}
			return flag2 && !flag;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0001B088 File Offset: 0x00019288
		internal bool TryAddCharacterInternal(uint unicode, out TMP_Character character)
		{
			character = null;
			if (this.m_MissingUnicodesFromFontFile.Contains(unicode))
			{
				return false;
			}
			if (FontEngine.LoadFontFace(this.sourceFontFile, this.m_FaceInfo.pointSize) != FontEngineError.Success)
			{
				return false;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0U)
			{
				if (unicode != 160U)
				{
					if (unicode == 173U || unicode == 8209U)
					{
						glyphIndex = FontEngine.GetGlyphIndex(45U);
					}
				}
				else
				{
					glyphIndex = FontEngine.GetGlyphIndex(32U);
				}
				if (glyphIndex == 0U)
				{
					this.m_MissingUnicodesFromFontFile.Add(unicode);
					return false;
				}
			}
			if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character = new TMP_Character(unicode, this, this.m_GlyphLookupDictionary[glyphIndex]);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				return true;
			}
			Glyph glyph = null;
			if (!this.m_AtlasTextures[this.m_AtlasTextureIndex].isReadable)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to add the requested character to font asset [",
					base.name,
					"]'s atlas texture. Please make the texture [",
					this.m_AtlasTextures[this.m_AtlasTextureIndex].name,
					"] readable."
				}), this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				character = new TMP_Character(unicode, this, glyph);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				this.m_GlyphIndexList.Add(glyphIndex);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (TMP_Settings.getFontFeaturesAtRuntime)
				{
					TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
				}
				return true;
			}
			if (this.m_IsMultiAtlasTexturesEnabled)
			{
				this.SetupNewAtlasTexture();
				if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
				{
					glyph.atlasIndex = this.m_AtlasTextureIndex;
					this.m_GlyphTable.Add(glyph);
					this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
					character = new TMP_Character(unicode, this, glyph);
					this.m_CharacterTable.Add(character);
					this.m_CharacterLookupDictionary.Add(unicode, character);
					this.m_GlyphIndexList.Add(glyphIndex);
					this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
					if (TMP_Settings.getFontFeaturesAtRuntime)
					{
						TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0001B364 File Offset: 0x00019564
		internal bool TryGetCharacter_and_QueueRenderToTexture(uint unicode, out TMP_Character character)
		{
			character = null;
			if (this.m_MissingUnicodesFromFontFile.Contains(unicode))
			{
				return false;
			}
			if (FontEngine.LoadFontFace(this.sourceFontFile, this.m_FaceInfo.pointSize) != FontEngineError.Success)
			{
				return false;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0U)
			{
				if (unicode != 160U)
				{
					if (unicode == 173U || unicode == 8209U)
					{
						glyphIndex = FontEngine.GetGlyphIndex(45U);
					}
				}
				else
				{
					glyphIndex = FontEngine.GetGlyphIndex(32U);
				}
				if (glyphIndex == 0U)
				{
					this.m_MissingUnicodesFromFontFile.Add(unicode);
					return false;
				}
			}
			if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character = new TMP_Character(unicode, this, this.m_GlyphLookupDictionary[glyphIndex]);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				return true;
			}
			GlyphLoadFlags flags = (((GlyphRenderMode)4 & this.m_AtlasRenderMode) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP;
			Glyph glyph = null;
			if (FontEngine.TryGetGlyphWithIndexValue(glyphIndex, flags, out glyph))
			{
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				character = new TMP_Character(unicode, this, glyph);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				this.m_GlyphIndexList.Add(glyphIndex);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (TMP_Settings.getFontFeaturesAtRuntime)
				{
					TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
				}
				this.m_GlyphsToRender.Add(glyph);
				TMP_FontAsset.RegisterFontAssetForAtlasTextureUpdate(this);
				return true;
			}
			return false;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0001B4BC File Offset: 0x000196BC
		internal void TryAddGlyphsToAtlasTextures()
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0001B4C0 File Offset: 0x000196C0
		private bool TryAddGlyphsToNewAtlasTexture()
		{
			this.SetupNewAtlasTexture();
			Glyph[] array;
			bool result = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
			int num = 0;
			while (num < array.Length && array[num] != null)
			{
				Glyph glyph = array[num];
				uint index = glyph.index;
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(index, glyph);
				this.m_GlyphIndexListNewlyAdded.Add(index);
				this.m_GlyphIndexList.Add(index);
				num++;
			}
			this.m_GlyphsToAdd.Clear();
			for (int i = 0; i < this.m_CharactersToAdd.Count; i++)
			{
				TMP_Character tmp_Character = this.m_CharactersToAdd[i];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(tmp_Character.glyphIndex, out glyph2))
				{
					this.m_GlyphsToAdd.Add(tmp_Character.glyphIndex);
				}
				else
				{
					tmp_Character.glyph = glyph2;
					tmp_Character.textAsset = this;
					this.m_CharacterTable.Add(tmp_Character);
					this.m_CharacterLookupDictionary.Add(tmp_Character.unicode, tmp_Character);
					this.m_CharactersToAdd.RemoveAt(i);
					i--;
				}
			}
			return result;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0001B610 File Offset: 0x00019810
		private void SetupNewAtlasTexture()
		{
			this.m_AtlasTextureIndex++;
			if (this.m_AtlasTextures.Length == this.m_AtlasTextureIndex)
			{
				Array.Resize<Texture2D>(ref this.m_AtlasTextures, this.m_AtlasTextures.Length * 2);
			}
			this.m_AtlasTextures[this.m_AtlasTextureIndex] = new Texture2D(this.m_AtlasWidth, this.m_AtlasHeight, TextureFormat.Alpha8, false);
			FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			int num = ((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1;
			this.m_FreeGlyphRects.Clear();
			this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
			this.m_UsedGlyphRects.Clear();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0001B6CC File Offset: 0x000198CC
		internal void UpdateAtlasTexture()
		{
			if (this.m_GlyphsToRender.Count == 0)
			{
				return;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width == 0 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height == 0)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Resize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			this.m_AtlasTextures[this.m_AtlasTextureIndex].Apply(false, false);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0001B754 File Offset: 0x00019954
		internal void UpdateGlyphAdjustmentRecords()
		{
			int num;
			GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords = FontEngine.GetGlyphPairAdjustmentRecords(this.m_GlyphIndexList, out num);
			this.m_GlyphIndexListNewlyAdded.Clear();
			if (glyphPairAdjustmentRecords == null || glyphPairAdjustmentRecords.Length == 0)
			{
				return;
			}
			if (this.m_FontFeatureTable == null)
			{
				this.m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int num2 = 0;
			while (num2 < glyphPairAdjustmentRecords.Length && glyphPairAdjustmentRecords[num2].firstAdjustmentRecord.glyphIndex != 0U)
			{
				uint key = glyphPairAdjustmentRecords[num2].secondAdjustmentRecord.glyphIndex << 16 | glyphPairAdjustmentRecords[num2].firstAdjustmentRecord.glyphIndex;
				if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.ContainsKey(key))
				{
					TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord = new TMP_GlyphPairAdjustmentRecord(glyphPairAdjustmentRecords[num2]);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(tmp_GlyphPairAdjustmentRecord);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Add(key, tmp_GlyphPairAdjustmentRecord);
				}
				num2++;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0001B834 File Offset: 0x00019A34
		internal void UpdateGlyphAdjustmentRecords(uint[] glyphIndexes)
		{
			GlyphPairAdjustmentRecord[] glyphPairAdjustmentTable = FontEngine.GetGlyphPairAdjustmentTable(glyphIndexes);
			if (glyphPairAdjustmentTable == null || glyphPairAdjustmentTable.Length == 0)
			{
				return;
			}
			if (this.m_FontFeatureTable == null)
			{
				this.m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int num = 0;
			while (num < glyphPairAdjustmentTable.Length && glyphPairAdjustmentTable[num].firstAdjustmentRecord.glyphIndex != 0U)
			{
				uint key = glyphPairAdjustmentTable[num].secondAdjustmentRecord.glyphIndex << 16 | glyphPairAdjustmentTable[num].firstAdjustmentRecord.glyphIndex;
				if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.ContainsKey(key))
				{
					TMP_GlyphPairAdjustmentRecord tmp_GlyphPairAdjustmentRecord = new TMP_GlyphPairAdjustmentRecord(glyphPairAdjustmentTable[num]);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(tmp_GlyphPairAdjustmentRecord);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookupDictionary.Add(key, tmp_GlyphPairAdjustmentRecord);
				}
				num++;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0001B8FD File Offset: 0x00019AFD
		internal void UpdateGlyphAdjustmentRecords(List<uint> glyphIndexes)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0001B8FF File Offset: 0x00019AFF
		internal void UpdateGlyphAdjustmentRecords(List<uint> newGlyphIndexes, List<uint> allGlyphIndexes)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0001B904 File Offset: 0x00019B04
		private void CopyListDataToArray<T>(List<T> srcList, ref T[] dstArray)
		{
			int count = srcList.Count;
			if (dstArray == null)
			{
				dstArray = new T[count];
			}
			else
			{
				Array.Resize<T>(ref dstArray, count);
			}
			for (int i = 0; i < count; i++)
			{
				dstArray[i] = srcList[i];
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0001B948 File Offset: 0x00019B48
		public void ClearFontAssetData(bool setAtlasSizeToZero = false)
		{
			this.ClearFontAssetTables();
			this.ClearAtlasTextures(setAtlasSizeToZero);
			this.ReadFontAssetDefinition();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0001B95D File Offset: 0x00019B5D
		internal void ClearFontAssetDataInternal()
		{
			this.ClearFontAssetTables();
			this.ClearAtlasTextures(true);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0001B96C File Offset: 0x00019B6C
		internal void UpdateFontAssetData()
		{
			uint[] array = new uint[this.m_CharacterTable.Count];
			for (int i = 0; i < this.m_CharacterTable.Count; i++)
			{
				array[i] = this.m_CharacterTable[i].unicode;
			}
			this.ClearFontAssetTables();
			this.ClearAtlasTextures(true);
			this.ReadFontAssetDefinition();
			if (array.Length != 0)
			{
				this.TryAddCharacters(array, true);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0001B9D4 File Offset: 0x00019BD4
		internal void ClearFontAssetTables()
		{
			if (this.m_GlyphTable != null)
			{
				this.m_GlyphTable.Clear();
			}
			if (this.m_CharacterTable != null)
			{
				this.m_CharacterTable.Clear();
			}
			if (this.m_UsedGlyphRects != null)
			{
				this.m_UsedGlyphRects.Clear();
			}
			if (this.m_FreeGlyphRects != null)
			{
				int num = ((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1;
				this.m_FreeGlyphRects.Clear();
				this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
			}
			if (this.m_GlyphsToRender != null)
			{
				this.m_GlyphsToRender.Clear();
			}
			if (this.m_GlyphsRendered != null)
			{
				this.m_GlyphsRendered.Clear();
			}
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords != null)
			{
				this.m_FontFeatureTable.glyphPairAdjustmentRecords.Clear();
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0001BAAC File Offset: 0x00019CAC
		internal void ClearAtlasTextures(bool setAtlasSizeToZero = false)
		{
			this.m_AtlasTextureIndex = 0;
			if (this.m_AtlasTextures == null)
			{
				return;
			}
			Texture2D texture2D;
			for (int i = 1; i < this.m_AtlasTextures.Length; i++)
			{
				texture2D = this.m_AtlasTextures[i];
				if (!(texture2D == null))
				{
					UnityEngine.Object.DestroyImmediate(texture2D, true);
				}
			}
			Array.Resize<Texture2D>(ref this.m_AtlasTextures, 1);
			texture2D = (this.m_AtlasTexture = this.m_AtlasTextures[0]);
			if (!texture2D.isReadable)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to reset font asset [",
					base.name,
					"]'s atlas texture. Please make the texture [",
					texture2D.name,
					"] readable."
				}), texture2D);
				return;
			}
			if (setAtlasSizeToZero)
			{
				texture2D.Resize(0, 0, TextureFormat.Alpha8, false);
			}
			else if (texture2D.width != this.m_AtlasWidth || texture2D.height != this.m_AtlasHeight)
			{
				texture2D.Resize(this.m_AtlasWidth, this.m_AtlasHeight, TextureFormat.Alpha8, false);
			}
			FontEngine.ResetAtlasTexture(texture2D);
			texture2D.Apply();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		internal void UpgradeFontAsset()
		{
			this.m_Version = "1.1.0";
			Debug.Log(string.Concat(new string[]
			{
				"Upgrading font asset [",
				base.name,
				"] to version ",
				this.m_Version,
				"."
			}), this);
			this.m_FaceInfo.familyName = this.m_fontInfo.Name;
			this.m_FaceInfo.styleName = string.Empty;
			this.m_FaceInfo.pointSize = (int)this.m_fontInfo.PointSize;
			this.m_FaceInfo.scale = this.m_fontInfo.Scale;
			this.m_FaceInfo.lineHeight = this.m_fontInfo.LineHeight;
			this.m_FaceInfo.ascentLine = this.m_fontInfo.Ascender;
			this.m_FaceInfo.capLine = this.m_fontInfo.CapHeight;
			this.m_FaceInfo.meanLine = this.m_fontInfo.CenterLine;
			this.m_FaceInfo.baseline = this.m_fontInfo.Baseline;
			this.m_FaceInfo.descentLine = this.m_fontInfo.Descender;
			this.m_FaceInfo.superscriptOffset = this.m_fontInfo.SuperscriptOffset;
			this.m_FaceInfo.superscriptSize = this.m_fontInfo.SubSize;
			this.m_FaceInfo.subscriptOffset = this.m_fontInfo.SubscriptOffset;
			this.m_FaceInfo.subscriptSize = this.m_fontInfo.SubSize;
			this.m_FaceInfo.underlineOffset = this.m_fontInfo.Underline;
			this.m_FaceInfo.underlineThickness = this.m_fontInfo.UnderlineThickness;
			this.m_FaceInfo.strikethroughOffset = this.m_fontInfo.strikethrough;
			this.m_FaceInfo.strikethroughThickness = this.m_fontInfo.strikethroughThickness;
			this.m_FaceInfo.tabWidth = this.m_fontInfo.TabWidth;
			if (this.m_AtlasTextures == null || this.m_AtlasTextures.Length == 0)
			{
				this.m_AtlasTextures = new Texture2D[1];
			}
			this.m_AtlasTextures[0] = this.atlas;
			this.m_AtlasWidth = (int)this.m_fontInfo.AtlasWidth;
			this.m_AtlasHeight = (int)this.m_fontInfo.AtlasHeight;
			this.m_AtlasPadding = (int)this.m_fontInfo.Padding;
			switch (this.m_CreationSettings.renderMode)
			{
			case 0:
				this.m_AtlasRenderMode = GlyphRenderMode.SMOOTH_HINTED;
				break;
			case 1:
				this.m_AtlasRenderMode = GlyphRenderMode.SMOOTH;
				break;
			case 2:
				this.m_AtlasRenderMode = GlyphRenderMode.RASTER_HINTED;
				break;
			case 3:
				this.m_AtlasRenderMode = GlyphRenderMode.RASTER;
				break;
			case 6:
				this.m_AtlasRenderMode = GlyphRenderMode.SDF16;
				break;
			case 7:
				this.m_AtlasRenderMode = GlyphRenderMode.SDF32;
				break;
			}
			if (this.fontWeights != null && this.fontWeights.Length != 0)
			{
				this.m_FontWeightTable[4] = this.fontWeights[4];
				this.m_FontWeightTable[7] = this.fontWeights[7];
			}
			if (this.fallbackFontAssets != null && this.fallbackFontAssets.Count > 0)
			{
				if (this.m_FallbackFontAssetTable == null)
				{
					this.m_FallbackFontAssetTable = new List<TMP_FontAsset>(this.fallbackFontAssets.Count);
				}
				for (int i = 0; i < this.fallbackFontAssets.Count; i++)
				{
					this.m_FallbackFontAssetTable.Add(this.fallbackFontAssets[i]);
				}
			}
			if (this.m_CreationSettings.sourceFontFileGUID != null || this.m_CreationSettings.sourceFontFileGUID != string.Empty)
			{
				this.m_SourceFontFileGUID = this.m_CreationSettings.sourceFontFileGUID;
			}
			else
			{
				Debug.LogWarning("Font asset [" + base.name + "] doesn't have a reference to its source font file. Please assign the appropriate source font file for this asset in the Font Atlas & Material section of font asset inspector.", this);
			}
			this.m_GlyphTable.Clear();
			this.m_CharacterTable.Clear();
			bool flag = false;
			for (int j = 0; j < this.m_glyphInfoList.Count; j++)
			{
				TMP_Glyph tmp_Glyph = this.m_glyphInfoList[j];
				Glyph glyph = new Glyph();
				uint index = (uint)(j + 1);
				glyph.index = index;
				glyph.glyphRect = new GlyphRect((int)tmp_Glyph.x, this.m_AtlasHeight - (int)(tmp_Glyph.y + tmp_Glyph.height + 0.5f), (int)(tmp_Glyph.width + 0.5f), (int)(tmp_Glyph.height + 0.5f));
				glyph.metrics = new GlyphMetrics(tmp_Glyph.width, tmp_Glyph.height, tmp_Glyph.xOffset, tmp_Glyph.yOffset, tmp_Glyph.xAdvance);
				glyph.scale = tmp_Glyph.scale;
				glyph.atlasIndex = 0;
				this.m_GlyphTable.Add(glyph);
				TMP_Character item = new TMP_Character((uint)tmp_Glyph.id, this, glyph);
				if (tmp_Glyph.id == 32)
				{
					flag = true;
				}
				this.m_CharacterTable.Add(item);
			}
			if (!flag)
			{
				Debug.Log("Synthesizing Space for [" + base.name + "]");
				Glyph glyph2 = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, this.m_FaceInfo.ascentLine / 5f), GlyphRect.zero, 1f, 0);
				this.m_GlyphTable.Add(glyph2);
				this.m_CharacterTable.Add(new TMP_Character(32U, this, glyph2));
			}
			this.ReadFontAssetDefinition();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0001C110 File Offset: 0x0001A310
		private void UpgradeGlyphAdjustmentTableToFontFeatureTable()
		{
			Debug.Log("Upgrading font asset [" + base.name + "] Glyph Adjustment Table.", this);
			if (this.m_FontFeatureTable == null)
			{
				this.m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int count = this.m_KerningTable.kerningPairs.Count;
			this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords = new List<TMP_GlyphPairAdjustmentRecord>(count);
			for (int i = 0; i < count; i++)
			{
				KerningPair kerningPair = this.m_KerningTable.kerningPairs[i];
				uint glyphIndex = 0U;
				TMP_Character tmp_Character;
				if (this.m_CharacterLookupDictionary.TryGetValue(kerningPair.firstGlyph, out tmp_Character))
				{
					glyphIndex = tmp_Character.glyphIndex;
				}
				uint glyphIndex2 = 0U;
				TMP_Character tmp_Character2;
				if (this.m_CharacterLookupDictionary.TryGetValue(kerningPair.secondGlyph, out tmp_Character2))
				{
					glyphIndex2 = tmp_Character2.glyphIndex;
				}
				TMP_GlyphAdjustmentRecord firstAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphIndex, new TMP_GlyphValueRecord(kerningPair.firstGlyphAdjustments.xPlacement, kerningPair.firstGlyphAdjustments.yPlacement, kerningPair.firstGlyphAdjustments.xAdvance, kerningPair.firstGlyphAdjustments.yAdvance));
				TMP_GlyphAdjustmentRecord secondAdjustmentRecord = new TMP_GlyphAdjustmentRecord(glyphIndex2, new TMP_GlyphValueRecord(kerningPair.secondGlyphAdjustments.xPlacement, kerningPair.secondGlyphAdjustments.yPlacement, kerningPair.secondGlyphAdjustments.xAdvance, kerningPair.secondGlyphAdjustments.yAdvance));
				TMP_GlyphPairAdjustmentRecord item = new TMP_GlyphPairAdjustmentRecord(firstAdjustmentRecord, secondAdjustmentRecord);
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(item);
			}
			this.m_KerningTable.kerningPairs = null;
			this.m_KerningTable = null;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0001C270 File Offset: 0x0001A470
		public TMP_FontAsset()
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0001C35C File Offset: 0x0001A55C
		// Note: this type is marked as 'beforefieldinit'.
		static TMP_FontAsset()
		{
		}

		// Token: 0x04000176 RID: 374
		[SerializeField]
		private string m_Version;

		// Token: 0x04000177 RID: 375
		[SerializeField]
		internal string m_SourceFontFileGUID;

		// Token: 0x04000178 RID: 376
		[SerializeField]
		private Font m_SourceFontFile;

		// Token: 0x04000179 RID: 377
		[SerializeField]
		private AtlasPopulationMode m_AtlasPopulationMode;

		// Token: 0x0400017A RID: 378
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x0400017B RID: 379
		[SerializeField]
		internal List<Glyph> m_GlyphTable = new List<Glyph>();

		// Token: 0x0400017C RID: 380
		internal Dictionary<uint, Glyph> m_GlyphLookupDictionary;

		// Token: 0x0400017D RID: 381
		[SerializeField]
		internal List<TMP_Character> m_CharacterTable = new List<TMP_Character>();

		// Token: 0x0400017E RID: 382
		internal Dictionary<uint, TMP_Character> m_CharacterLookupDictionary;

		// Token: 0x0400017F RID: 383
		internal Texture2D m_AtlasTexture;

		// Token: 0x04000180 RID: 384
		[SerializeField]
		internal Texture2D[] m_AtlasTextures;

		// Token: 0x04000181 RID: 385
		[SerializeField]
		internal int m_AtlasTextureIndex;

		// Token: 0x04000182 RID: 386
		[SerializeField]
		private bool m_IsMultiAtlasTexturesEnabled;

		// Token: 0x04000183 RID: 387
		[SerializeField]
		private bool m_ClearDynamicDataOnBuild;

		// Token: 0x04000184 RID: 388
		[SerializeField]
		private List<GlyphRect> m_UsedGlyphRects;

		// Token: 0x04000185 RID: 389
		[SerializeField]
		private List<GlyphRect> m_FreeGlyphRects;

		// Token: 0x04000186 RID: 390
		[SerializeField]
		private FaceInfo_Legacy m_fontInfo;

		// Token: 0x04000187 RID: 391
		[SerializeField]
		public Texture2D atlas;

		// Token: 0x04000188 RID: 392
		[SerializeField]
		internal int m_AtlasWidth;

		// Token: 0x04000189 RID: 393
		[SerializeField]
		internal int m_AtlasHeight;

		// Token: 0x0400018A RID: 394
		[SerializeField]
		internal int m_AtlasPadding;

		// Token: 0x0400018B RID: 395
		[SerializeField]
		internal GlyphRenderMode m_AtlasRenderMode;

		// Token: 0x0400018C RID: 396
		[SerializeField]
		internal List<TMP_Glyph> m_glyphInfoList;

		// Token: 0x0400018D RID: 397
		[SerializeField]
		[FormerlySerializedAs("m_kerningInfo")]
		internal KerningTable m_KerningTable = new KerningTable();

		// Token: 0x0400018E RID: 398
		[SerializeField]
		internal TMP_FontFeatureTable m_FontFeatureTable = new TMP_FontFeatureTable();

		// Token: 0x0400018F RID: 399
		[SerializeField]
		private List<TMP_FontAsset> fallbackFontAssets;

		// Token: 0x04000190 RID: 400
		[SerializeField]
		internal List<TMP_FontAsset> m_FallbackFontAssetTable;

		// Token: 0x04000191 RID: 401
		[SerializeField]
		internal FontAssetCreationSettings m_CreationSettings;

		// Token: 0x04000192 RID: 402
		[SerializeField]
		private TMP_FontWeightPair[] m_FontWeightTable = new TMP_FontWeightPair[10];

		// Token: 0x04000193 RID: 403
		[SerializeField]
		private TMP_FontWeightPair[] fontWeights;

		// Token: 0x04000194 RID: 404
		public float normalStyle;

		// Token: 0x04000195 RID: 405
		public float normalSpacingOffset;

		// Token: 0x04000196 RID: 406
		public float boldStyle = 0.75f;

		// Token: 0x04000197 RID: 407
		public float boldSpacing = 7f;

		// Token: 0x04000198 RID: 408
		public byte italicStyle = 35;

		// Token: 0x04000199 RID: 409
		public byte tabSize = 10;

		// Token: 0x0400019A RID: 410
		internal bool IsFontAssetLookupTablesDirty;

		// Token: 0x0400019B RID: 411
		private static ProfilerMarker k_ReadFontAssetDefinitionMarker = new ProfilerMarker("TMP.ReadFontAssetDefinition");

		// Token: 0x0400019C RID: 412
		private static ProfilerMarker k_AddSynthesizedCharactersMarker = new ProfilerMarker("TMP.AddSynthesizedCharacters");

		// Token: 0x0400019D RID: 413
		private static ProfilerMarker k_TryAddCharacterMarker = new ProfilerMarker("TMP.TryAddCharacter");

		// Token: 0x0400019E RID: 414
		private static ProfilerMarker k_TryAddCharactersMarker = new ProfilerMarker("TMP.TryAddCharacters");

		// Token: 0x0400019F RID: 415
		private static ProfilerMarker k_UpdateGlyphAdjustmentRecordsMarker = new ProfilerMarker("TMP.UpdateGlyphAdjustmentRecords");

		// Token: 0x040001A0 RID: 416
		private static ProfilerMarker k_ClearFontAssetDataMarker = new ProfilerMarker("TMP.ClearFontAssetData");

		// Token: 0x040001A1 RID: 417
		private static ProfilerMarker k_UpdateFontAssetDataMarker = new ProfilerMarker("TMP.UpdateFontAssetData");

		// Token: 0x040001A2 RID: 418
		private static string s_DefaultMaterialSuffix = " Atlas Material";

		// Token: 0x040001A3 RID: 419
		internal HashSet<int> FallbackSearchQueryLookup = new HashSet<int>();

		// Token: 0x040001A4 RID: 420
		private static HashSet<int> k_SearchedFontAssetLookup;

		// Token: 0x040001A5 RID: 421
		private static List<TMP_FontAsset> k_FontAssets_FontFeaturesUpdateQueue = new List<TMP_FontAsset>();

		// Token: 0x040001A6 RID: 422
		private static HashSet<int> k_FontAssets_FontFeaturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x040001A7 RID: 423
		private static List<TMP_FontAsset> k_FontAssets_AtlasTexturesUpdateQueue = new List<TMP_FontAsset>();

		// Token: 0x040001A8 RID: 424
		private static HashSet<int> k_FontAssets_AtlasTexturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x040001A9 RID: 425
		private List<Glyph> m_GlyphsToRender = new List<Glyph>();

		// Token: 0x040001AA RID: 426
		private List<Glyph> m_GlyphsRendered = new List<Glyph>();

		// Token: 0x040001AB RID: 427
		private List<uint> m_GlyphIndexList = new List<uint>();

		// Token: 0x040001AC RID: 428
		private List<uint> m_GlyphIndexListNewlyAdded = new List<uint>();

		// Token: 0x040001AD RID: 429
		internal List<uint> m_GlyphsToAdd = new List<uint>();

		// Token: 0x040001AE RID: 430
		internal HashSet<uint> m_GlyphsToAddLookup = new HashSet<uint>();

		// Token: 0x040001AF RID: 431
		internal List<TMP_Character> m_CharactersToAdd = new List<TMP_Character>();

		// Token: 0x040001B0 RID: 432
		internal HashSet<uint> m_CharactersToAddLookup = new HashSet<uint>();

		// Token: 0x040001B1 RID: 433
		internal List<uint> s_MissingCharacterList = new List<uint>();

		// Token: 0x040001B2 RID: 434
		internal HashSet<uint> m_MissingUnicodesFromFontFile = new HashSet<uint>();

		// Token: 0x040001B3 RID: 435
		internal static uint[] k_GlyphIndexArray;

		// Token: 0x02000084 RID: 132
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000604 RID: 1540 RVA: 0x000386CA File Offset: 0x000368CA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x000386D6 File Offset: 0x000368D6
			public <>c()
			{
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x000386DE File Offset: 0x000368DE
			internal uint <SortCharacterTable>b__124_0(TMP_Character c)
			{
				return c.unicode;
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x000386E6 File Offset: 0x000368E6
			internal uint <SortGlyphTable>b__125_0(Glyph c)
			{
				return c.index;
			}

			// Token: 0x0400059F RID: 1439
			public static readonly TMP_FontAsset.<>c <>9 = new TMP_FontAsset.<>c();

			// Token: 0x040005A0 RID: 1440
			public static Func<TMP_Character, uint> <>9__124_0;

			// Token: 0x040005A1 RID: 1441
			public static Func<Glyph, uint> <>9__125_0;
		}
	}
}
