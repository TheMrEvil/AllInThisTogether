using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.Serialization;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200000C RID: 12
	[ExcludeFromPreset]
	[Serializable]
	public class FontAsset : TextAsset
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002714 File Offset: 0x00000914
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000272C File Offset: 0x0000092C
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002738 File Offset: 0x00000938
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002750 File Offset: 0x00000950
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

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000275C File Offset: 0x0000095C
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002774 File Offset: 0x00000974
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002780 File Offset: 0x00000980
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000027BB File Offset: 0x000009BB
		internal int familyNameHashCode
		{
			get
			{
				bool flag = this.m_FamilyNameHashCode == 0;
				if (flag)
				{
					this.m_FamilyNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_FaceInfo.familyName);
				}
				return this.m_FamilyNameHashCode;
			}
			set
			{
				this.m_FamilyNameHashCode = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027C4 File Offset: 0x000009C4
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000027FF File Offset: 0x000009FF
		internal int styleNameHashCode
		{
			get
			{
				bool flag = this.m_StyleNameHashCode == 0;
				if (flag)
				{
					this.m_StyleNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_FaceInfo.styleName);
				}
				return this.m_StyleNameHashCode;
			}
			set
			{
				this.m_StyleNameHashCode = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002808 File Offset: 0x00000A08
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002820 File Offset: 0x00000A20
		public FontWeightPair[] fontWeightTable
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000282C File Offset: 0x00000A2C
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002844 File Offset: 0x00000A44
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002850 File Offset: 0x00000A50
		public Dictionary<uint, Glyph> glyphLookupTable
		{
			get
			{
				bool flag = this.m_GlyphLookupDictionary == null;
				if (flag)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_GlyphLookupDictionary;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000287C File Offset: 0x00000A7C
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002894 File Offset: 0x00000A94
		public List<Character> characterTable
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000028A0 File Offset: 0x00000AA0
		public Dictionary<uint, Character> characterLookupTable
		{
			get
			{
				bool flag = this.m_CharacterLookupDictionary == null;
				if (flag)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_CharacterLookupDictionary;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000028CC File Offset: 0x00000ACC
		public Texture2D atlasTexture
		{
			get
			{
				bool flag = this.m_AtlasTexture == null;
				if (flag)
				{
					this.m_AtlasTexture = this.atlasTextures[0];
				}
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002904 File Offset: 0x00000B04
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000292B File Offset: 0x00000B2B
		public Texture2D[] atlasTextures
		{
			get
			{
				bool flag = this.m_AtlasTextures == null;
				if (flag)
				{
				}
				return this.m_AtlasTextures;
			}
			set
			{
				this.m_AtlasTextures = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002938 File Offset: 0x00000B38
		public int atlasTextureCount
		{
			get
			{
				return this.m_AtlasTextureIndex + 1;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002954 File Offset: 0x00000B54
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000296C File Offset: 0x00000B6C
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002978 File Offset: 0x00000B78
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002990 File Offset: 0x00000B90
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000299C File Offset: 0x00000B9C
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000029B4 File Offset: 0x00000BB4
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000029C0 File Offset: 0x00000BC0
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000029D8 File Offset: 0x00000BD8
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000029E4 File Offset: 0x00000BE4
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000029FC File Offset: 0x00000BFC
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A08 File Offset: 0x00000C08
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A20 File Offset: 0x00000C20
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A2C File Offset: 0x00000C2C
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002A44 File Offset: 0x00000C44
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002A50 File Offset: 0x00000C50
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002A68 File Offset: 0x00000C68
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A74 File Offset: 0x00000C74
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002A8C File Offset: 0x00000C8C
		public FontFeatureTable fontFeatureTable
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002A98 File Offset: 0x00000C98
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public List<FontAsset> fallbackFontAssetTable
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002ABC File Offset: 0x00000CBC
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public FontAssetCreationEditorSettings fontAssetCreationEditorSettings
		{
			get
			{
				return this.m_fontAssetCreationEditorSettings;
			}
			set
			{
				this.m_fontAssetCreationEditorSettings = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public float regularStyleWeight
		{
			get
			{
				return this.m_RegularStyleWeight;
			}
			set
			{
				this.m_RegularStyleWeight = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002B04 File Offset: 0x00000D04
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002B1C File Offset: 0x00000D1C
		public float regularStyleSpacing
		{
			get
			{
				return this.m_RegularStyleSpacing;
			}
			set
			{
				this.m_RegularStyleSpacing = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002B28 File Offset: 0x00000D28
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002B40 File Offset: 0x00000D40
		public float boldStyleWeight
		{
			get
			{
				return this.m_BoldStyleWeight;
			}
			set
			{
				this.m_BoldStyleWeight = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002B4C File Offset: 0x00000D4C
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002B64 File Offset: 0x00000D64
		public float boldStyleSpacing
		{
			get
			{
				return this.m_BoldStyleSpacing;
			}
			set
			{
				this.m_BoldStyleSpacing = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002B70 File Offset: 0x00000D70
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002B88 File Offset: 0x00000D88
		public byte italicStyleSlant
		{
			get
			{
				return this.m_ItalicStyleSlant;
			}
			set
			{
				this.m_ItalicStyleSlant = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002B94 File Offset: 0x00000D94
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002BAC File Offset: 0x00000DAC
		public byte tabMultiple
		{
			get
			{
				return this.m_TabMultiple;
			}
			set
			{
				this.m_TabMultiple = value;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public static FontAsset CreateFontAsset(string familyName, string styleName, int pointSize = 90)
		{
			FontReference fontReference;
			bool flag = FontEngine.TryGetSystemFontReference(familyName, styleName, out fontReference);
			FontAsset result;
			if (flag)
			{
				result = FontAsset.CreateFontAsset(fontReference.filePath, fontReference.faceIndex, pointSize, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.DynamicOS, true);
			}
			else
			{
				Debug.Log(string.Concat(new string[]
				{
					"Unable to find a font file with the specified Family Name [",
					familyName,
					"] and Style [",
					styleName,
					"]."
				}));
				result = null;
			}
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C34 File Offset: 0x00000E34
		private static FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.DynamicOS, bool enableMultiAtlasSupport = true)
		{
			bool flag = FontEngine.LoadFontFace(fontFilePath, samplingPointSize, faceIndex) > FontEngineError.Success;
			FontAsset result;
			if (flag)
			{
				Debug.Log("Unable to load font face for [" + fontFilePath + "].");
				result = null;
			}
			else
			{
				result = FontAsset.CreateFontAssetInstance(null, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C84 File Offset: 0x00000E84
		public static FontAsset CreateFontAsset(Font font)
		{
			return FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public static FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			return FontAsset.CreateFontAsset(font, 0, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CD8 File Offset: 0x00000ED8
		private static FontAsset CreateFontAsset(Font font, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			bool flag = FontEngine.LoadFontFace(font, samplingPointSize, faceIndex) > FontEngineError.Success;
			FontAsset result;
			if (flag)
			{
				bool flag2 = font.name == "Arial";
				if (flag2)
				{
					result = FontAsset.CreateFontAsset("Arial", "Regular", 90);
				}
				else
				{
					Debug.LogWarning("Unable to load font face for [" + font.name + "]. Make sure \"Include Font Data\" is enabled in the Font Import Settings.", font);
					result = null;
				}
			}
			else
			{
				result = FontAsset.CreateFontAssetInstance(font, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D54 File Offset: 0x00000F54
		private static FontAsset CreateFontAssetInstance(Font font, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport)
		{
			FontAsset fontAsset = ScriptableObject.CreateInstance<FontAsset>();
			fontAsset.m_Version = "1.1.0";
			fontAsset.faceInfo = FontEngine.GetFaceInfo();
			bool flag = atlasPopulationMode == AtlasPopulationMode.Dynamic;
			if (flag)
			{
				fontAsset.sourceFontFile = font;
			}
			fontAsset.atlasPopulationMode = atlasPopulationMode;
			fontAsset.atlasWidth = atlasWidth;
			fontAsset.atlasHeight = atlasHeight;
			fontAsset.atlasPadding = atlasPadding;
			fontAsset.atlasRenderMode = renderMode;
			fontAsset.atlasTextures = new Texture2D[1];
			Texture2D texture2D = new Texture2D(1, 1, TextureFormat.Alpha8, false);
			fontAsset.atlasTextures[0] = texture2D;
			fontAsset.isMultiAtlasTexturesEnabled = enableMultiAtlasSupport;
			bool flag2 = (renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16;
			int num;
			if (flag2)
			{
				num = 0;
				Material material = new Material(TextShaderUtilities.ShaderRef_MobileBitmap);
				material.SetTexture(TextShaderUtilities.ID_MainTex, texture2D);
				material.SetFloat(TextShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material.SetFloat(TextShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				fontAsset.material = material;
			}
			else
			{
				num = 1;
				Material material2 = new Material(TextShaderUtilities.ShaderRef_MobileSDF);
				material2.SetTexture(TextShaderUtilities.ID_MainTex, texture2D);
				material2.SetFloat(TextShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				material2.SetFloat(TextShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				material2.SetFloat(TextShaderUtilities.ID_GradientScale, (float)(atlasPadding + num));
				material2.SetFloat(TextShaderUtilities.ID_WeightNormal, fontAsset.regularStyleWeight);
				material2.SetFloat(TextShaderUtilities.ID_WeightBold, fontAsset.boldStyleWeight);
				fontAsset.material = material2;
			}
			fontAsset.freeGlyphRects = new List<GlyphRect>(8)
			{
				new GlyphRect(0, 0, atlasWidth - num, atlasHeight - num)
			};
			fontAsset.usedGlyphRects = new List<GlyphRect>(8);
			fontAsset.ReadFontAssetDefinition();
			return fontAsset;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EF5 File Offset: 0x000010F5
		private void Awake()
		{
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002EF8 File Offset: 0x000010F8
		private void OnDestroy()
		{
			this.DestroyAtlasTextures();
			Object.DestroyImmediate(this.m_Material);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F10 File Offset: 0x00001110
		public void ReadFontAssetDefinition()
		{
			FontAsset.k_ReadFontAssetDefinitionMarker.Begin();
			this.InitializeDictionaryLookupTables();
			this.AddSynthesizedCharactersAndFaceMetrics();
			bool flag = this.m_FaceInfo.capLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(88U);
			if (flag)
			{
				uint glyphIndex = this.m_CharacterLookupDictionary[88U].glyphIndex;
				this.m_FaceInfo.capLine = this.m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
			}
			bool flag2 = this.m_FaceInfo.meanLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(120U);
			if (flag2)
			{
				uint glyphIndex2 = this.m_CharacterLookupDictionary[120U].glyphIndex;
				this.m_FaceInfo.meanLine = this.m_GlyphLookupDictionary[glyphIndex2].metrics.horizontalBearingY;
			}
			bool flag3 = this.m_FaceInfo.scale == 0f;
			if (flag3)
			{
				this.m_FaceInfo.scale = 1f;
			}
			bool flag4 = this.m_FaceInfo.strikethroughOffset == 0f;
			if (flag4)
			{
				this.m_FaceInfo.strikethroughOffset = this.m_FaceInfo.capLine / 2.5f;
			}
			bool flag5 = this.m_AtlasPadding == 0;
			if (flag5)
			{
				bool flag6 = base.material.HasProperty(TextShaderUtilities.ID_GradientScale);
				if (flag6)
				{
					this.m_AtlasPadding = (int)base.material.GetFloat(TextShaderUtilities.ID_GradientScale) - 1;
				}
			}
			base.hashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
			this.familyNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_FaceInfo.familyName);
			this.styleNameHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_FaceInfo.styleName);
			base.materialHashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name + FontAsset.s_DefaultMaterialSuffix);
			TextResourceManager.AddFontAsset(this);
			this.IsFontAssetLookupTablesDirty = false;
			FontAsset.k_ReadFontAssetDefinitionMarker.End();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000310C File Offset: 0x0000130C
		internal void InitializeDictionaryLookupTables()
		{
			this.InitializeGlyphLookupDictionary();
			this.InitializeCharacterLookupDictionary();
			this.InitializeGlyphPaidAdjustmentRecordsLookupDictionary();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003124 File Offset: 0x00001324
		internal void InitializeGlyphLookupDictionary()
		{
			bool flag = this.m_GlyphLookupDictionary == null;
			if (flag)
			{
				this.m_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
			}
			else
			{
				this.m_GlyphLookupDictionary.Clear();
			}
			bool flag2 = this.m_GlyphIndexList == null;
			if (flag2)
			{
				this.m_GlyphIndexList = new List<uint>();
			}
			else
			{
				this.m_GlyphIndexList.Clear();
			}
			bool flag3 = this.m_GlyphIndexListNewlyAdded == null;
			if (flag3)
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
				bool flag4 = !this.m_GlyphLookupDictionary.ContainsKey(index);
				if (flag4)
				{
					this.m_GlyphLookupDictionary.Add(index, glyph);
					this.m_GlyphIndexList.Add(index);
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003218 File Offset: 0x00001418
		internal void InitializeCharacterLookupDictionary()
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.m_CharacterLookupDictionary = new Dictionary<uint, Character>();
			}
			else
			{
				this.m_CharacterLookupDictionary.Clear();
			}
			for (int i = 0; i < this.m_CharacterTable.Count; i++)
			{
				Character character = this.m_CharacterTable[i];
				uint unicode = character.unicode;
				uint glyphIndex = character.glyphIndex;
				bool flag2 = !this.m_CharacterLookupDictionary.ContainsKey(unicode);
				if (flag2)
				{
					this.m_CharacterLookupDictionary.Add(unicode, character);
					character.textAsset = this;
					character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000032C8 File Offset: 0x000014C8
		internal void InitializeGlyphPaidAdjustmentRecordsLookupDictionary()
		{
			bool flag = this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup == null;
			if (flag)
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup = new Dictionary<uint, GlyphPairAdjustmentRecord>();
			}
			else
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Clear();
			}
			List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords;
			bool flag2 = glyphPairAdjustmentRecords != null;
			if (flag2)
			{
				for (int i = 0; i < glyphPairAdjustmentRecords.Count; i++)
				{
					GlyphPairAdjustmentRecord value = glyphPairAdjustmentRecords[i];
					uint key = value.secondAdjustmentRecord.glyphIndex << 16 | value.firstAdjustmentRecord.glyphIndex;
					bool flag3 = !this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key);
					if (flag3)
					{
						this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, value);
					}
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000339C File Offset: 0x0000159C
		internal void AddSynthesizedCharactersAndFaceMetrics()
		{
			FontAsset.k_AddSynthesizedCharactersMarker.Begin();
			bool flag = false;
			bool flag2 = this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS;
			if (flag2)
			{
				flag = (this.LoadFontFace() == FontEngineError.Success);
				bool flag3 = !flag && !this.InternalDynamicOS;
				if (flag3)
				{
					Debug.LogWarning("Unable to load font face for [" + base.name + "] font asset.", this);
				}
			}
			this.AddSynthesizedCharacter(3U, flag, true);
			this.AddSynthesizedCharacter(9U, flag, true);
			this.AddSynthesizedCharacter(10U, flag, false);
			this.AddSynthesizedCharacter(11U, flag, false);
			this.AddSynthesizedCharacter(13U, flag, false);
			this.AddSynthesizedCharacter(1564U, flag, false);
			this.AddSynthesizedCharacter(8203U, flag, false);
			this.AddSynthesizedCharacter(8206U, flag, false);
			this.AddSynthesizedCharacter(8207U, flag, false);
			this.AddSynthesizedCharacter(8232U, flag, false);
			this.AddSynthesizedCharacter(8233U, flag, false);
			this.AddSynthesizedCharacter(8288U, flag, false);
			FontAsset.k_AddSynthesizedCharactersMarker.End();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000034B0 File Offset: 0x000016B0
		private void AddSynthesizedCharacter(uint unicode, bool isFontFaceLoaded, bool addImmediately = false)
		{
			bool flag = this.m_CharacterLookupDictionary.ContainsKey(unicode);
			if (!flag)
			{
				Glyph glyph;
				if (isFontFaceLoaded)
				{
					bool flag2 = FontEngine.GetGlyphIndex(unicode) > 0U;
					if (flag2)
					{
						bool flag3 = !addImmediately;
						if (flag3)
						{
							return;
						}
						GlyphLoadFlags flags = ((this.m_AtlasRenderMode & (GlyphRenderMode)4) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP;
						bool flag4 = FontEngine.TryGetGlyphWithUnicodeValue(unicode, flags, out glyph);
						if (flag4)
						{
							this.m_CharacterLookupDictionary.Add(unicode, new Character(unicode, this, glyph));
						}
						return;
					}
				}
				glyph = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(unicode, new Character(unicode, this, glyph));
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003572 File Offset: 0x00001772
		internal void AddCharacterToLookupCache(uint unicode, Character character)
		{
			this.m_CharacterLookupDictionary.Add(unicode, character);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003584 File Offset: 0x00001784
		private FontEngineError LoadFontFace()
		{
			bool flag = this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic;
			FontEngineError result;
			if (flag)
			{
				result = FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize, this.m_FaceInfo.faceIndex);
			}
			else
			{
				result = FontEngine.LoadFontFace(this.m_FaceInfo.familyName, this.m_FaceInfo.styleName, this.m_FaceInfo.pointSize);
			}
			return result;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000035F0 File Offset: 0x000017F0
		internal void SortCharacterTable()
		{
			bool flag = this.m_CharacterTable != null && this.m_CharacterTable.Count > 0;
			if (flag)
			{
				this.m_CharacterTable = (from c in this.m_CharacterTable
				orderby c.unicode
				select c).ToList<Character>();
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003650 File Offset: 0x00001850
		internal void SortGlyphTable()
		{
			bool flag = this.m_GlyphTable != null && this.m_GlyphTable.Count > 0;
			if (flag)
			{
				this.m_GlyphTable = (from c in this.m_GlyphTable
				orderby c.index
				select c).ToList<Glyph>();
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000036B0 File Offset: 0x000018B0
		internal void SortFontFeatureTable()
		{
			this.m_FontFeatureTable.SortGlyphPairAdjustmentRecords();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000036BF File Offset: 0x000018BF
		internal void SortAllTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
			this.SortFontFeatureTable();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000036D8 File Offset: 0x000018D8
		public bool HasCharacter(int character)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			return !flag && this.m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003708 File Offset: 0x00001908
		public bool HasCharacter(char character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
				bool flag2 = this.m_CharacterLookupDictionary == null;
				if (flag2)
				{
					return false;
				}
			}
			bool flag3 = this.m_CharacterLookupDictionary.ContainsKey((uint)character);
			bool result;
			if (flag3)
			{
				result = true;
			}
			else
			{
				bool flag4 = tryAddCharacter && (this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS);
				if (flag4)
				{
					Character character2;
					bool flag5 = this.TryAddCharacterInternal((uint)character, out character2, false);
					if (flag5)
					{
						return true;
					}
				}
				if (searchFallbacks)
				{
					bool flag6 = FontAsset.k_SearchedFontAssetLookup == null;
					if (flag6)
					{
						FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
					}
					else
					{
						FontAsset.k_SearchedFontAssetLookup.Clear();
					}
					FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
					bool flag7 = this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0;
					if (flag7)
					{
						int num = 0;
						while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
						{
							FontAsset fontAsset = this.fallbackFontAssetTable[num];
							int instanceID = fontAsset.GetInstanceID();
							bool flag8 = FontAsset.k_SearchedFontAssetLookup.Add(instanceID);
							if (flag8)
							{
								bool flag9 = fontAsset.HasCharacter_Internal((uint)character, true, tryAddCharacter);
								if (flag9)
								{
									return true;
								}
							}
							num++;
						}
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003874 File Offset: 0x00001A74
		private bool HasCharacter_Internal(uint character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
				bool flag2 = this.m_CharacterLookupDictionary == null;
				if (flag2)
				{
					return false;
				}
			}
			bool flag3 = this.m_CharacterLookupDictionary.ContainsKey(character);
			bool result;
			if (flag3)
			{
				result = true;
			}
			else
			{
				bool flag4 = tryAddCharacter && (this.atlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS);
				if (flag4)
				{
					Character character2;
					bool flag5 = this.TryAddCharacterInternal(character, out character2, false);
					if (flag5)
					{
						return true;
					}
				}
				if (searchFallbacks)
				{
					bool flag6 = this.fallbackFontAssetTable == null || this.fallbackFontAssetTable.Count == 0;
					if (flag6)
					{
						return false;
					}
					int num = 0;
					while (num < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num] != null)
					{
						FontAsset fontAsset = this.fallbackFontAssetTable[num];
						int instanceID = fontAsset.GetInstanceID();
						bool flag7 = FontAsset.k_SearchedFontAssetLookup.Add(instanceID);
						if (flag7)
						{
							bool flag8 = fontAsset.HasCharacter_Internal(character, true, tryAddCharacter);
							if (flag8)
							{
								return true;
							}
						}
						num++;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000039AC File Offset: 0x00001BAC
		public bool HasCharacters(string text, out List<char> missingCharacters)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			bool result;
			if (flag)
			{
				missingCharacters = null;
				result = false;
			}
			else
			{
				missingCharacters = new List<char>();
				for (int i = 0; i < text.Length; i++)
				{
					bool flag2 = !this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]);
					if (flag2)
					{
						missingCharacters.Add(text[i]);
					}
				}
				bool flag3 = missingCharacters.Count == 0;
				result = flag3;
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A30 File Offset: 0x00001C30
		public bool HasCharacters(string text, out uint[] missingCharacters, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			missingCharacters = null;
			bool flag = this.m_CharacterLookupDictionary == null;
			if (flag)
			{
				this.ReadFontAssetDefinition();
				bool flag2 = this.m_CharacterLookupDictionary == null;
				if (flag2)
				{
					return false;
				}
			}
			this.s_MissingCharacterList.Clear();
			for (int i = 0; i < text.Length; i++)
			{
				bool flag3 = true;
				uint num = (uint)text[i];
				bool flag4 = this.m_CharacterLookupDictionary.ContainsKey(num);
				if (!flag4)
				{
					bool flag5 = tryAddCharacter && (this.atlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS);
					if (flag5)
					{
						Character character;
						bool flag6 = this.TryAddCharacterInternal(num, out character, false);
						if (flag6)
						{
							goto IL_19C;
						}
					}
					if (searchFallbacks)
					{
						bool flag7 = FontAsset.k_SearchedFontAssetLookup == null;
						if (flag7)
						{
							FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
						}
						else
						{
							FontAsset.k_SearchedFontAssetLookup.Clear();
						}
						FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
						bool flag8 = this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0;
						if (flag8)
						{
							int num2 = 0;
							while (num2 < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[num2] != null)
							{
								FontAsset fontAsset = this.fallbackFontAssetTable[num2];
								int instanceID = fontAsset.GetInstanceID();
								bool flag9 = FontAsset.k_SearchedFontAssetLookup.Add(instanceID);
								if (flag9)
								{
									bool flag10 = !fontAsset.HasCharacter_Internal(num, true, tryAddCharacter);
									if (!flag10)
									{
										flag3 = false;
										break;
									}
								}
								num2++;
							}
						}
					}
					bool flag11 = flag3;
					if (flag11)
					{
						this.s_MissingCharacterList.Add(num);
					}
				}
				IL_19C:;
			}
			bool flag12 = this.s_MissingCharacterList.Count > 0;
			bool result;
			if (flag12)
			{
				missingCharacters = this.s_MissingCharacterList.ToArray();
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003C1C File Offset: 0x00001E1C
		public bool HasCharacters(string text)
		{
			bool flag = this.m_CharacterLookupDictionary == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < text.Length; i++)
				{
					bool flag2 = !this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]);
					if (flag2)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003C78 File Offset: 0x00001E78
		public static string GetCharacters(FontAsset fontAsset)
		{
			string text = string.Empty;
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				text += ((char)fontAsset.characterTable[i].unicode).ToString();
			}
			return text;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public static int[] GetCharactersArray(FontAsset fontAsset)
		{
			int[] array = new int[fontAsset.characterTable.Count];
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				array[i] = (int)fontAsset.characterTable[i].unicode;
			}
			return array;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003D24 File Offset: 0x00001F24
		internal uint GetGlyphIndex(uint unicode)
		{
			bool flag = this.m_CharacterLookupDictionary.ContainsKey(unicode);
			uint result;
			if (flag)
			{
				result = this.m_CharacterLookupDictionary[unicode].glyphIndex;
			}
			else
			{
				result = ((this.LoadFontFace() == FontEngineError.Success) ? FontEngine.GetGlyphIndex(unicode) : 0U);
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003D6C File Offset: 0x00001F6C
		internal static void RegisterFontAssetForFontFeatureUpdate(FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			bool flag = FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Add(instanceID);
			if (flag)
			{
				FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Add(fontAsset);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003D9C File Offset: 0x00001F9C
		internal static void UpdateFontFeaturesForFontAssetsInQueue()
		{
			int count = FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Count;
			for (int i = 0; i < count; i++)
			{
				FontAsset.k_FontAssets_FontFeaturesUpdateQueue[i].UpdateGlyphAdjustmentRecords();
			}
			bool flag = count > 0;
			if (flag)
			{
				FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Clear();
				FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003DF8 File Offset: 0x00001FF8
		internal static void RegisterAtlasTextureForApply(Texture2D texture)
		{
			int instanceID = texture.GetInstanceID();
			bool flag = FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Add(instanceID);
			if (flag)
			{
				FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Add(texture);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003E28 File Offset: 0x00002028
		internal static void UpdateAtlasTexturesInQueue()
		{
			int count = FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Count;
			for (int i = 0; i < count; i++)
			{
				FontAsset.k_FontAssets_AtlasTexturesUpdateQueue[i].Apply(false, false);
			}
			bool flag = count > 0;
			if (flag)
			{
				FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Clear();
				FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E84 File Offset: 0x00002084
		internal static void UpdateFontAssetInUpdateQueue()
		{
			FontAsset.UpdateAtlasTexturesInQueue();
			FontAsset.UpdateFontFeaturesForFontAssetsInQueue();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E94 File Offset: 0x00002094
		public bool TryAddCharacters(uint[] unicodes, bool includeFontFeatures = false)
		{
			uint[] array;
			return this.TryAddCharacters(unicodes, out array, includeFontFeatures);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003EB0 File Offset: 0x000020B0
		public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes, bool includeFontFeatures = false)
		{
			FontAsset.k_TryAddCharactersMarker.Begin();
			bool flag = unicodes == null || unicodes.Length == 0 || this.m_AtlasPopulationMode == AtlasPopulationMode.Static;
			bool result;
			if (flag)
			{
				bool flag2 = this.m_AtlasPopulationMode == AtlasPopulationMode.Static;
				if (flag2)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
				}
				missingUnicodes = null;
				FontAsset.k_TryAddCharactersMarker.End();
				result = false;
			}
			else
			{
				bool flag3 = this.LoadFontFace() > FontEngineError.Success;
				if (flag3)
				{
					missingUnicodes = unicodes.ToArray<uint>();
					FontAsset.k_TryAddCharactersMarker.End();
					result = false;
				}
				else
				{
					bool flag4 = this.m_CharacterLookupDictionary == null || this.m_GlyphLookupDictionary == null;
					if (flag4)
					{
						this.ReadFontAssetDefinition();
					}
					this.m_GlyphsToAdd.Clear();
					this.m_GlyphsToAddLookup.Clear();
					this.m_CharactersToAdd.Clear();
					this.m_CharactersToAddLookup.Clear();
					this.s_MissingCharacterList.Clear();
					bool flag5 = false;
					int num = unicodes.Length;
					for (int i = 0; i < num; i++)
					{
						uint num2 = unicodes[i];
						bool flag6 = this.m_CharacterLookupDictionary.ContainsKey(num2);
						if (!flag6)
						{
							uint glyphIndex = FontEngine.GetGlyphIndex(num2);
							bool flag7 = glyphIndex == 0U;
							if (flag7)
							{
								uint num3 = num2;
								uint num4 = num3;
								if (num4 != 160U)
								{
									if (num4 == 173U || num4 == 8209U)
									{
										glyphIndex = FontEngine.GetGlyphIndex(45U);
									}
								}
								else
								{
									glyphIndex = FontEngine.GetGlyphIndex(32U);
								}
								bool flag8 = glyphIndex == 0U;
								if (flag8)
								{
									this.s_MissingCharacterList.Add(num2);
									flag5 = true;
									goto IL_250;
								}
							}
							Character character = new Character(num2, glyphIndex);
							bool flag9 = this.m_GlyphLookupDictionary.ContainsKey(glyphIndex);
							if (flag9)
							{
								character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
								character.textAsset = this;
								this.m_CharacterTable.Add(character);
								this.m_CharacterLookupDictionary.Add(num2, character);
							}
							else
							{
								bool flag10 = this.m_GlyphsToAddLookup.Add(glyphIndex);
								if (flag10)
								{
									this.m_GlyphsToAdd.Add(glyphIndex);
								}
								bool flag11 = this.m_CharactersToAddLookup.Add(num2);
								if (flag11)
								{
									this.m_CharactersToAdd.Add(character);
								}
							}
						}
						IL_250:;
					}
					bool flag12 = this.m_GlyphsToAdd.Count == 0;
					if (flag12)
					{
						missingUnicodes = unicodes;
						FontAsset.k_TryAddCharactersMarker.End();
						result = false;
					}
					else
					{
						bool flag13 = this.m_AtlasTextures[this.m_AtlasTextureIndex].width != this.m_AtlasWidth || this.m_AtlasTextures[this.m_AtlasTextureIndex].height != this.m_AtlasHeight;
						if (flag13)
						{
							this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
							FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
						}
						Glyph[] array;
						bool flag14 = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
						int num5 = 0;
						while (num5 < array.Length && array[num5] != null)
						{
							Glyph glyph = array[num5];
							uint index = glyph.index;
							glyph.atlasIndex = this.m_AtlasTextureIndex;
							this.m_GlyphTable.Add(glyph);
							this.m_GlyphLookupDictionary.Add(index, glyph);
							this.m_GlyphIndexListNewlyAdded.Add(index);
							this.m_GlyphIndexList.Add(index);
							num5++;
						}
						this.m_GlyphsToAdd.Clear();
						for (int j = 0; j < this.m_CharactersToAdd.Count; j++)
						{
							Character character2 = this.m_CharactersToAdd[j];
							Glyph glyph2;
							bool flag15 = !this.m_GlyphLookupDictionary.TryGetValue(character2.glyphIndex, out glyph2);
							if (flag15)
							{
								this.m_GlyphsToAdd.Add(character2.glyphIndex);
							}
							else
							{
								character2.glyph = glyph2;
								character2.textAsset = this;
								this.m_CharacterTable.Add(character2);
								this.m_CharacterLookupDictionary.Add(character2.unicode, character2);
								this.m_CharactersToAdd.RemoveAt(j);
								j--;
							}
						}
						bool flag16 = this.m_IsMultiAtlasTexturesEnabled && !flag14;
						if (flag16)
						{
							while (!flag14)
							{
								flag14 = this.TryAddGlyphsToNewAtlasTexture();
							}
						}
						if (includeFontFeatures)
						{
							this.UpdateGlyphAdjustmentRecords();
						}
						for (int k = 0; k < this.m_CharactersToAdd.Count; k++)
						{
							Character character3 = this.m_CharactersToAdd[k];
							this.s_MissingCharacterList.Add(character3.unicode);
						}
						missingUnicodes = null;
						bool flag17 = this.s_MissingCharacterList.Count > 0;
						if (flag17)
						{
							missingUnicodes = this.s_MissingCharacterList.ToArray();
						}
						FontAsset.k_TryAddCharactersMarker.End();
						result = (flag14 && !flag5);
					}
				}
			}
			return result;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000043EC File Offset: 0x000025EC
		public bool TryAddCharacters(string characters, bool includeFontFeatures = false)
		{
			string text;
			return this.TryAddCharacters(characters, out text, includeFontFeatures);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004408 File Offset: 0x00002608
		public bool TryAddCharacters(string characters, out string missingCharacters, bool includeFontFeatures = false)
		{
			FontAsset.k_TryAddCharactersMarker.Begin();
			bool flag = string.IsNullOrEmpty(characters) || this.m_AtlasPopulationMode == AtlasPopulationMode.Static;
			bool result;
			if (flag)
			{
				bool flag2 = this.m_AtlasPopulationMode == AtlasPopulationMode.Static;
				if (flag2)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
				}
				missingCharacters = characters;
				FontAsset.k_TryAddCharactersMarker.End();
				result = false;
			}
			else
			{
				bool flag3 = this.LoadFontFace() > FontEngineError.Success;
				if (flag3)
				{
					missingCharacters = characters;
					FontAsset.k_TryAddCharactersMarker.End();
					result = false;
				}
				else
				{
					bool flag4 = this.m_CharacterLookupDictionary == null || this.m_GlyphLookupDictionary == null;
					if (flag4)
					{
						this.ReadFontAssetDefinition();
					}
					this.m_GlyphsToAdd.Clear();
					this.m_GlyphsToAddLookup.Clear();
					this.m_CharactersToAdd.Clear();
					this.m_CharactersToAddLookup.Clear();
					this.s_MissingCharacterList.Clear();
					bool flag5 = false;
					int length = characters.Length;
					for (int i = 0; i < length; i++)
					{
						uint num = (uint)characters[i];
						bool flag6 = this.m_CharacterLookupDictionary.ContainsKey(num);
						if (!flag6)
						{
							uint glyphIndex = FontEngine.GetGlyphIndex(num);
							bool flag7 = glyphIndex == 0U;
							if (flag7)
							{
								uint num2 = num;
								uint num3 = num2;
								if (num3 != 160U)
								{
									if (num3 == 173U || num3 == 8209U)
									{
										glyphIndex = FontEngine.GetGlyphIndex(45U);
									}
								}
								else
								{
									glyphIndex = FontEngine.GetGlyphIndex(32U);
								}
								bool flag8 = glyphIndex == 0U;
								if (flag8)
								{
									this.s_MissingCharacterList.Add(num);
									flag5 = true;
									goto IL_255;
								}
							}
							Character character = new Character(num, glyphIndex);
							bool flag9 = this.m_GlyphLookupDictionary.ContainsKey(glyphIndex);
							if (flag9)
							{
								character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
								character.textAsset = this;
								this.m_CharacterTable.Add(character);
								this.m_CharacterLookupDictionary.Add(num, character);
							}
							else
							{
								bool flag10 = this.m_GlyphsToAddLookup.Add(glyphIndex);
								if (flag10)
								{
									this.m_GlyphsToAdd.Add(glyphIndex);
								}
								bool flag11 = this.m_CharactersToAddLookup.Add(num);
								if (flag11)
								{
									this.m_CharactersToAdd.Add(character);
								}
							}
						}
						IL_255:;
					}
					bool flag12 = this.m_GlyphsToAdd.Count == 0;
					if (flag12)
					{
						missingCharacters = characters;
						FontAsset.k_TryAddCharactersMarker.End();
						result = false;
					}
					else
					{
						bool flag13 = this.m_AtlasTextures[this.m_AtlasTextureIndex].width != this.m_AtlasWidth || this.m_AtlasTextures[this.m_AtlasTextureIndex].height != this.m_AtlasHeight;
						if (flag13)
						{
							this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
							FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
						}
						Glyph[] array;
						bool flag14 = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out array);
						int num4 = 0;
						while (num4 < array.Length && array[num4] != null)
						{
							Glyph glyph = array[num4];
							uint index = glyph.index;
							glyph.atlasIndex = this.m_AtlasTextureIndex;
							this.m_GlyphTable.Add(glyph);
							this.m_GlyphLookupDictionary.Add(index, glyph);
							this.m_GlyphIndexListNewlyAdded.Add(index);
							this.m_GlyphIndexList.Add(index);
							num4++;
						}
						this.m_GlyphsToAdd.Clear();
						for (int j = 0; j < this.m_CharactersToAdd.Count; j++)
						{
							Character character2 = this.m_CharactersToAdd[j];
							Glyph glyph2;
							bool flag15 = !this.m_GlyphLookupDictionary.TryGetValue(character2.glyphIndex, out glyph2);
							if (flag15)
							{
								this.m_GlyphsToAdd.Add(character2.glyphIndex);
							}
							else
							{
								character2.glyph = glyph2;
								character2.textAsset = this;
								this.m_CharacterTable.Add(character2);
								this.m_CharacterLookupDictionary.Add(character2.unicode, character2);
								this.m_CharactersToAdd.RemoveAt(j);
								j--;
							}
						}
						bool flag16 = this.m_IsMultiAtlasTexturesEnabled && !flag14;
						if (flag16)
						{
							while (!flag14)
							{
								flag14 = this.TryAddGlyphsToNewAtlasTexture();
							}
						}
						if (includeFontFeatures)
						{
							this.UpdateGlyphAdjustmentRecords();
						}
						missingCharacters = string.Empty;
						for (int k = 0; k < this.m_CharactersToAdd.Count; k++)
						{
							Character character3 = this.m_CharactersToAdd[k];
							this.s_MissingCharacterList.Add(character3.unicode);
						}
						bool flag17 = this.s_MissingCharacterList.Count > 0;
						if (flag17)
						{
							missingCharacters = this.s_MissingCharacterList.UintToString();
						}
						FontAsset.k_TryAddCharactersMarker.End();
						result = (flag14 && !flag5);
					}
				}
			}
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000494C File Offset: 0x00002B4C
		internal bool TryAddCharacterInternal(uint unicode, out Character character, bool shouldGetFontFeatures = false)
		{
			FontAsset.k_TryAddCharacterMarker.Begin();
			character = null;
			bool flag = this.m_MissingUnicodesFromFontFile.Contains(unicode);
			bool result;
			if (flag)
			{
				FontAsset.k_TryAddCharacterMarker.End();
				result = false;
			}
			else
			{
				bool flag2 = this.LoadFontFace() > FontEngineError.Success;
				if (flag2)
				{
					FontAsset.k_TryAddCharacterMarker.End();
					result = false;
				}
				else
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
					bool flag3 = glyphIndex == 0U;
					if (flag3)
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
						bool flag4 = glyphIndex == 0U;
						if (flag4)
						{
							this.m_MissingUnicodesFromFontFile.Add(unicode);
							FontAsset.k_TryAddCharacterMarker.End();
							return false;
						}
					}
					bool flag5 = this.m_GlyphLookupDictionary.ContainsKey(glyphIndex);
					if (flag5)
					{
						character = new Character(unicode, this, this.m_GlyphLookupDictionary[glyphIndex]);
						this.m_CharacterTable.Add(character);
						this.m_CharacterLookupDictionary.Add(unicode, character);
						FontAsset.k_TryAddCharacterMarker.End();
						result = true;
					}
					else
					{
						Glyph glyph = null;
						bool flag6 = !this.m_AtlasTextures[this.m_AtlasTextureIndex].isReadable;
						if (flag6)
						{
							Debug.LogWarning(string.Concat(new string[]
							{
								"Unable to add the requested character to font asset [",
								base.name,
								"]'s atlas texture. Please make the texture [",
								this.m_AtlasTextures[this.m_AtlasTextureIndex].name,
								"] readable."
							}), this.m_AtlasTextures[this.m_AtlasTextureIndex]);
							FontAsset.k_TryAddCharacterMarker.End();
							result = false;
						}
						else
						{
							bool flag7 = this.m_AtlasTextures[this.m_AtlasTextureIndex].width != this.m_AtlasWidth || this.m_AtlasTextures[this.m_AtlasTextureIndex].height != this.m_AtlasHeight;
							if (flag7)
							{
								this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
								FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
							}
							FontEngine.SetTextureUploadMode(false);
							bool flag8 = FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph);
							if (flag8)
							{
								glyph.atlasIndex = this.m_AtlasTextureIndex;
								this.m_GlyphTable.Add(glyph);
								this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
								character = new Character(unicode, this, glyph);
								this.m_CharacterTable.Add(character);
								this.m_CharacterLookupDictionary.Add(unicode, character);
								this.m_GlyphIndexList.Add(glyphIndex);
								this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
								if (shouldGetFontFeatures)
								{
									FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
								}
								FontAsset.RegisterAtlasTextureForApply(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
								FontEngine.SetTextureUploadMode(true);
								FontAsset.k_TryAddCharacterMarker.End();
								result = true;
							}
							else
							{
								bool isMultiAtlasTexturesEnabled = this.m_IsMultiAtlasTexturesEnabled;
								if (isMultiAtlasTexturesEnabled)
								{
									this.SetupNewAtlasTexture();
									bool flag9 = FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph);
									if (flag9)
									{
										glyph.atlasIndex = this.m_AtlasTextureIndex;
										this.m_GlyphTable.Add(glyph);
										this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
										character = new Character(unicode, this, glyph);
										this.m_CharacterTable.Add(character);
										this.m_CharacterLookupDictionary.Add(unicode, character);
										this.m_GlyphIndexList.Add(glyphIndex);
										this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
										if (shouldGetFontFeatures)
										{
											FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
										}
										FontAsset.RegisterAtlasTextureForApply(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
										FontEngine.SetTextureUploadMode(true);
										FontAsset.k_TryAddCharacterMarker.End();
										return true;
									}
								}
								FontAsset.k_TryAddCharacterMarker.End();
								result = false;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004D58 File Offset: 0x00002F58
		internal bool TryGetCharacter_and_QueueRenderToTexture(uint unicode, out Character character, bool shouldGetFontFeatures = false)
		{
			FontAsset.k_TryAddCharacterMarker.Begin();
			character = null;
			bool flag = this.m_MissingUnicodesFromFontFile.Contains(unicode);
			bool result;
			if (flag)
			{
				FontAsset.k_TryAddCharacterMarker.End();
				result = false;
			}
			else
			{
				bool flag2 = this.LoadFontFace() > FontEngineError.Success;
				if (flag2)
				{
					FontAsset.k_TryAddCharacterMarker.End();
					result = false;
				}
				else
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
					bool flag3 = glyphIndex == 0U;
					if (flag3)
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
						bool flag4 = glyphIndex == 0U;
						if (flag4)
						{
							this.m_MissingUnicodesFromFontFile.Add(unicode);
							FontAsset.k_TryAddCharacterMarker.End();
							return false;
						}
					}
					bool flag5 = this.m_GlyphLookupDictionary.ContainsKey(glyphIndex);
					if (flag5)
					{
						character = new Character(unicode, this, this.m_GlyphLookupDictionary[glyphIndex]);
						this.m_CharacterTable.Add(character);
						this.m_CharacterLookupDictionary.Add(unicode, character);
						FontAsset.k_TryAddCharacterMarker.End();
						result = true;
					}
					else
					{
						GlyphLoadFlags flags = (((GlyphRenderMode)4 & this.m_AtlasRenderMode) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP;
						Glyph glyph = null;
						bool flag6 = FontEngine.TryGetGlyphWithIndexValue(glyphIndex, flags, out glyph);
						if (flag6)
						{
							this.m_GlyphTable.Add(glyph);
							this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
							character = new Character(unicode, this, glyph);
							this.m_CharacterTable.Add(character);
							this.m_CharacterLookupDictionary.Add(unicode, character);
							this.m_GlyphIndexList.Add(glyphIndex);
							this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
							if (shouldGetFontFeatures)
							{
								FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
							}
							this.m_GlyphsToRender.Add(glyph);
							FontAsset.k_TryAddCharacterMarker.End();
							result = true;
						}
						else
						{
							FontAsset.k_TryAddCharacterMarker.End();
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002EF5 File Offset: 0x000010F5
		internal void TryAddGlyphsToAtlasTextures()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004F4C File Offset: 0x0000314C
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
				Character character = this.m_CharactersToAdd[i];
				Glyph glyph2;
				bool flag = !this.m_GlyphLookupDictionary.TryGetValue(character.glyphIndex, out glyph2);
				if (flag)
				{
					this.m_GlyphsToAdd.Add(character.glyphIndex);
				}
				else
				{
					character.glyph = glyph2;
					character.textAsset = this;
					this.m_CharacterTable.Add(character);
					this.m_CharacterLookupDictionary.Add(character.unicode, character);
					this.m_CharactersToAdd.RemoveAt(i);
					i--;
				}
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000050CC File Offset: 0x000032CC
		private void SetupNewAtlasTexture()
		{
			this.m_AtlasTextureIndex++;
			bool flag = this.m_AtlasTextures.Length == this.m_AtlasTextureIndex;
			if (flag)
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

		// Token: 0x0600007F RID: 127 RVA: 0x00005194 File Offset: 0x00003394
		internal void UpdateAtlasTexture()
		{
			bool flag = this.m_GlyphsToRender.Count == 0;
			if (!flag)
			{
				bool flag2 = this.m_AtlasTextures[this.m_AtlasTextureIndex].width != this.m_AtlasWidth || this.m_AtlasTextures[this.m_AtlasTextureIndex].height != this.m_AtlasHeight;
				if (flag2)
				{
					this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
					FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				}
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Apply(false, false);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005240 File Offset: 0x00003440
		internal void UpdateGlyphAdjustmentRecords()
		{
			FontAsset.k_UpdateGlyphAdjustmentRecordsMarker.Begin();
			int num;
			GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords = FontEngine.GetGlyphPairAdjustmentRecords(this.m_GlyphIndexList, out num);
			this.m_GlyphIndexListNewlyAdded.Clear();
			bool flag = glyphPairAdjustmentRecords == null || glyphPairAdjustmentRecords.Length == 0;
			if (flag)
			{
				FontAsset.k_UpdateGlyphAdjustmentRecordsMarker.End();
			}
			else
			{
				bool flag2 = this.m_FontFeatureTable == null;
				if (flag2)
				{
					this.m_FontFeatureTable = new FontFeatureTable();
				}
				int num2 = 0;
				while (num2 < glyphPairAdjustmentRecords.Length && glyphPairAdjustmentRecords[num2].firstAdjustmentRecord.glyphIndex > 0U)
				{
					uint key = glyphPairAdjustmentRecords[num2].secondAdjustmentRecord.glyphIndex << 16 | glyphPairAdjustmentRecords[num2].firstAdjustmentRecord.glyphIndex;
					bool flag3 = this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key);
					if (!flag3)
					{
						GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = glyphPairAdjustmentRecords[num2];
						this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
						this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, glyphPairAdjustmentRecord);
					}
					num2++;
				}
				FontAsset.k_UpdateGlyphAdjustmentRecordsMarker.End();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005370 File Offset: 0x00003570
		internal void UpdateGlyphAdjustmentRecords(uint[] glyphIndexes)
		{
			FontAsset.k_UpdateGlyphAdjustmentRecordsMarker.Begin();
			GlyphPairAdjustmentRecord[] glyphPairAdjustmentTable = FontEngine.GetGlyphPairAdjustmentTable(glyphIndexes);
			bool flag = glyphPairAdjustmentTable == null || glyphPairAdjustmentTable.Length == 0;
			if (flag)
			{
				FontAsset.k_UpdateGlyphAdjustmentRecordsMarker.End();
			}
			else
			{
				bool flag2 = this.m_FontFeatureTable == null;
				if (flag2)
				{
					this.m_FontFeatureTable = new FontFeatureTable();
				}
				int num = 0;
				while (num < glyphPairAdjustmentTable.Length && glyphPairAdjustmentTable[num].firstAdjustmentRecord.glyphIndex > 0U)
				{
					uint key = glyphPairAdjustmentTable[num].secondAdjustmentRecord.glyphIndex << 16 | glyphPairAdjustmentTable[num].firstAdjustmentRecord.glyphIndex;
					bool flag3 = this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key);
					if (!flag3)
					{
						GlyphPairAdjustmentRecord glyphPairAdjustmentRecord = glyphPairAdjustmentTable[num];
						this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(glyphPairAdjustmentRecord);
						this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, glyphPairAdjustmentRecord);
					}
					num++;
				}
				FontAsset.k_UpdateGlyphAdjustmentRecordsMarker.End();
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002EF5 File Offset: 0x000010F5
		internal void UpdateGlyphAdjustmentRecords(List<uint> glyphIndexes)
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002EF5 File Offset: 0x000010F5
		internal void UpdateGlyphAdjustmentRecords(List<uint> newGlyphIndexes, List<uint> allGlyphIndexes)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005488 File Offset: 0x00003688
		private void CopyListDataToArray<T>(List<T> srcList, ref T[] dstArray)
		{
			int count = srcList.Count;
			bool flag = dstArray == null;
			if (flag)
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

		// Token: 0x06000085 RID: 133 RVA: 0x000054D7 File Offset: 0x000036D7
		public void ClearFontAssetData(bool setAtlasSizeToZero = false)
		{
			FontAsset.k_ClearFontAssetDataMarker.Begin();
			this.ClearFontAssetTables();
			this.ClearAtlasTextures(setAtlasSizeToZero);
			this.ReadFontAssetDefinition();
			FontAsset.k_ClearFontAssetDataMarker.End();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005506 File Offset: 0x00003706
		internal void ClearFontAssetDataInternal()
		{
			this.ClearFontAssetTables();
			this.ClearAtlasTextures(true);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005518 File Offset: 0x00003718
		internal void UpdateFontAssetData()
		{
			FontAsset.k_UpdateFontAssetDataMarker.Begin();
			uint[] array = new uint[this.m_CharacterTable.Count];
			for (int i = 0; i < this.m_CharacterTable.Count; i++)
			{
				array[i] = this.m_CharacterTable[i].unicode;
			}
			this.ClearFontAssetTables();
			this.ClearAtlasTextures(true);
			this.ReadFontAssetDefinition();
			bool flag = array.Length != 0;
			if (flag)
			{
				this.TryAddCharacters(array, true);
			}
			FontAsset.k_UpdateFontAssetDataMarker.End();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000055A4 File Offset: 0x000037A4
		internal void ClearFontAssetTables()
		{
			bool flag = this.m_GlyphTable != null;
			if (flag)
			{
				this.m_GlyphTable.Clear();
			}
			bool flag2 = this.m_CharacterTable != null;
			if (flag2)
			{
				this.m_CharacterTable.Clear();
			}
			bool flag3 = this.m_UsedGlyphRects != null;
			if (flag3)
			{
				this.m_UsedGlyphRects.Clear();
			}
			bool flag4 = this.m_FreeGlyphRects != null;
			if (flag4)
			{
				int num = ((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1;
				this.m_FreeGlyphRects.Clear();
				this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - num, this.m_AtlasHeight - num));
			}
			bool flag5 = this.m_GlyphsToRender != null;
			if (flag5)
			{
				this.m_GlyphsToRender.Clear();
			}
			bool flag6 = this.m_GlyphsRendered != null;
			if (flag6)
			{
				this.m_GlyphsRendered.Clear();
			}
			bool flag7 = this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords != null;
			if (flag7)
			{
				this.m_FontFeatureTable.glyphPairAdjustmentRecords.Clear();
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000056B8 File Offset: 0x000038B8
		internal void ClearAtlasTextures(bool setAtlasSizeToZero = false)
		{
			this.m_AtlasTextureIndex = 0;
			bool flag = this.m_AtlasTextures == null;
			if (!flag)
			{
				Texture2D texture2D;
				for (int i = 1; i < this.m_AtlasTextures.Length; i++)
				{
					texture2D = this.m_AtlasTextures[i];
					bool flag2 = texture2D == null;
					if (!flag2)
					{
						Object.DestroyImmediate(texture2D, true);
					}
				}
				Array.Resize<Texture2D>(ref this.m_AtlasTextures, 1);
				texture2D = (this.m_AtlasTexture = this.m_AtlasTextures[0]);
				bool flag3 = !texture2D.isReadable;
				if (flag3)
				{
				}
				if (setAtlasSizeToZero)
				{
					texture2D.Reinitialize(1, 1, TextureFormat.Alpha8, false);
				}
				else
				{
					bool flag4 = texture2D.width != this.m_AtlasWidth || texture2D.height != this.m_AtlasHeight;
					if (flag4)
					{
						texture2D.Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight, TextureFormat.Alpha8, false);
					}
				}
				FontEngine.ResetAtlasTexture(texture2D);
				texture2D.Apply();
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000057B0 File Offset: 0x000039B0
		private void DestroyAtlasTextures()
		{
			bool flag = this.m_AtlasTextures == null;
			if (!flag)
			{
				for (int i = 0; i < this.m_AtlasTextures.Length; i++)
				{
					Texture2D texture2D = this.m_AtlasTextures[i];
					bool flag2 = texture2D != null;
					if (flag2)
					{
						Object.DestroyImmediate(texture2D);
					}
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005804 File Offset: 0x00003A04
		public FontAsset()
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000058F0 File Offset: 0x00003AF0
		// Note: this type is marked as 'beforefieldinit'.
		static FontAsset()
		{
		}

		// Token: 0x04000029 RID: 41
		[SerializeField]
		internal string m_SourceFontFileGUID;

		// Token: 0x0400002A RID: 42
		[SerializeField]
		private Font m_SourceFontFile;

		// Token: 0x0400002B RID: 43
		[SerializeField]
		private AtlasPopulationMode m_AtlasPopulationMode;

		// Token: 0x0400002C RID: 44
		[SerializeField]
		internal bool InternalDynamicOS;

		// Token: 0x0400002D RID: 45
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x0400002E RID: 46
		private int m_FamilyNameHashCode;

		// Token: 0x0400002F RID: 47
		private int m_StyleNameHashCode;

		// Token: 0x04000030 RID: 48
		[SerializeField]
		private FontWeightPair[] m_FontWeightTable = new FontWeightPair[10];

		// Token: 0x04000031 RID: 49
		[SerializeField]
		internal List<Glyph> m_GlyphTable = new List<Glyph>();

		// Token: 0x04000032 RID: 50
		internal Dictionary<uint, Glyph> m_GlyphLookupDictionary;

		// Token: 0x04000033 RID: 51
		[SerializeField]
		internal List<Character> m_CharacterTable = new List<Character>();

		// Token: 0x04000034 RID: 52
		internal Dictionary<uint, Character> m_CharacterLookupDictionary;

		// Token: 0x04000035 RID: 53
		internal Texture2D m_AtlasTexture;

		// Token: 0x04000036 RID: 54
		[SerializeField]
		internal Texture2D[] m_AtlasTextures;

		// Token: 0x04000037 RID: 55
		[SerializeField]
		internal int m_AtlasTextureIndex;

		// Token: 0x04000038 RID: 56
		[SerializeField]
		private bool m_IsMultiAtlasTexturesEnabled;

		// Token: 0x04000039 RID: 57
		[SerializeField]
		private bool m_ClearDynamicDataOnBuild;

		// Token: 0x0400003A RID: 58
		[SerializeField]
		internal int m_AtlasWidth;

		// Token: 0x0400003B RID: 59
		[SerializeField]
		internal int m_AtlasHeight;

		// Token: 0x0400003C RID: 60
		[SerializeField]
		internal int m_AtlasPadding;

		// Token: 0x0400003D RID: 61
		[SerializeField]
		internal GlyphRenderMode m_AtlasRenderMode;

		// Token: 0x0400003E RID: 62
		[SerializeField]
		private List<GlyphRect> m_UsedGlyphRects;

		// Token: 0x0400003F RID: 63
		[SerializeField]
		private List<GlyphRect> m_FreeGlyphRects;

		// Token: 0x04000040 RID: 64
		[SerializeField]
		internal FontFeatureTable m_FontFeatureTable = new FontFeatureTable();

		// Token: 0x04000041 RID: 65
		[SerializeField]
		internal List<FontAsset> m_FallbackFontAssetTable;

		// Token: 0x04000042 RID: 66
		[SerializeField]
		internal FontAssetCreationEditorSettings m_fontAssetCreationEditorSettings;

		// Token: 0x04000043 RID: 67
		[SerializeField]
		[FormerlySerializedAs("normalStyle")]
		internal float m_RegularStyleWeight = 0f;

		// Token: 0x04000044 RID: 68
		[SerializeField]
		[FormerlySerializedAs("normalSpacingOffset")]
		internal float m_RegularStyleSpacing = 0f;

		// Token: 0x04000045 RID: 69
		[SerializeField]
		[FormerlySerializedAs("boldStyle")]
		internal float m_BoldStyleWeight = 0.75f;

		// Token: 0x04000046 RID: 70
		[FormerlySerializedAs("boldSpacing")]
		[SerializeField]
		internal float m_BoldStyleSpacing = 7f;

		// Token: 0x04000047 RID: 71
		[SerializeField]
		[FormerlySerializedAs("italicStyle")]
		internal byte m_ItalicStyleSlant = 35;

		// Token: 0x04000048 RID: 72
		[FormerlySerializedAs("tabSize")]
		[SerializeField]
		internal byte m_TabMultiple = 10;

		// Token: 0x04000049 RID: 73
		internal bool IsFontAssetLookupTablesDirty;

		// Token: 0x0400004A RID: 74
		private static ProfilerMarker k_ReadFontAssetDefinitionMarker = new ProfilerMarker("FontAsset.ReadFontAssetDefinition");

		// Token: 0x0400004B RID: 75
		private static ProfilerMarker k_AddSynthesizedCharactersMarker = new ProfilerMarker("FontAsset.AddSynthesizedCharacters");

		// Token: 0x0400004C RID: 76
		private static ProfilerMarker k_TryAddCharacterMarker = new ProfilerMarker("FontAsset.TryAddCharacter");

		// Token: 0x0400004D RID: 77
		private static ProfilerMarker k_TryAddCharactersMarker = new ProfilerMarker("FontAsset.TryAddCharacters");

		// Token: 0x0400004E RID: 78
		private static ProfilerMarker k_UpdateGlyphAdjustmentRecordsMarker = new ProfilerMarker("FontAsset.UpdateGlyphAdjustmentRecords");

		// Token: 0x0400004F RID: 79
		private static ProfilerMarker k_ClearFontAssetDataMarker = new ProfilerMarker("FontAsset.ClearFontAssetData");

		// Token: 0x04000050 RID: 80
		private static ProfilerMarker k_UpdateFontAssetDataMarker = new ProfilerMarker("FontAsset.UpdateFontAssetData");

		// Token: 0x04000051 RID: 81
		private static string s_DefaultMaterialSuffix = " Atlas Material";

		// Token: 0x04000052 RID: 82
		private static HashSet<int> k_SearchedFontAssetLookup;

		// Token: 0x04000053 RID: 83
		private static List<FontAsset> k_FontAssets_FontFeaturesUpdateQueue = new List<FontAsset>();

		// Token: 0x04000054 RID: 84
		private static HashSet<int> k_FontAssets_FontFeaturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x04000055 RID: 85
		private static List<Texture2D> k_FontAssets_AtlasTexturesUpdateQueue = new List<Texture2D>();

		// Token: 0x04000056 RID: 86
		private static HashSet<int> k_FontAssets_AtlasTexturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x04000057 RID: 87
		private List<Glyph> m_GlyphsToRender = new List<Glyph>();

		// Token: 0x04000058 RID: 88
		private List<Glyph> m_GlyphsRendered = new List<Glyph>();

		// Token: 0x04000059 RID: 89
		private List<uint> m_GlyphIndexList = new List<uint>();

		// Token: 0x0400005A RID: 90
		private List<uint> m_GlyphIndexListNewlyAdded = new List<uint>();

		// Token: 0x0400005B RID: 91
		internal List<uint> m_GlyphsToAdd = new List<uint>();

		// Token: 0x0400005C RID: 92
		internal HashSet<uint> m_GlyphsToAddLookup = new HashSet<uint>();

		// Token: 0x0400005D RID: 93
		internal List<Character> m_CharactersToAdd = new List<Character>();

		// Token: 0x0400005E RID: 94
		internal HashSet<uint> m_CharactersToAddLookup = new HashSet<uint>();

		// Token: 0x0400005F RID: 95
		internal List<uint> s_MissingCharacterList = new List<uint>();

		// Token: 0x04000060 RID: 96
		internal HashSet<uint> m_MissingUnicodesFromFontFile = new HashSet<uint>();

		// Token: 0x04000061 RID: 97
		internal static uint[] k_GlyphIndexArray;

		// Token: 0x0200000D RID: 13
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600008D RID: 141 RVA: 0x00005998 File Offset: 0x00003B98
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600008E RID: 142 RVA: 0x000059A4 File Offset: 0x00003BA4
			public <>c()
			{
			}

			// Token: 0x0600008F RID: 143 RVA: 0x000059AD File Offset: 0x00003BAD
			internal uint <SortCharacterTable>b__144_0(Character c)
			{
				return c.unicode;
			}

			// Token: 0x06000090 RID: 144 RVA: 0x000059B5 File Offset: 0x00003BB5
			internal uint <SortGlyphTable>b__145_0(Glyph c)
			{
				return c.index;
			}

			// Token: 0x04000062 RID: 98
			public static readonly FontAsset.<>c <>9 = new FontAsset.<>c();

			// Token: 0x04000063 RID: 99
			public static Func<Character, uint> <>9__144_0;

			// Token: 0x04000064 RID: 100
			public static Func<Glyph, uint> <>9__145_0;
		}
	}
}
