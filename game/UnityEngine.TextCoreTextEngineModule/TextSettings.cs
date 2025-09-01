using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003E RID: 62
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[Serializable]
	public class TextSettings : ScriptableObject
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0001B165 File Offset: 0x00019365
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0001B16D File Offset: 0x0001936D
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0001B176 File Offset: 0x00019376
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0001B17E File Offset: 0x0001937E
		public FontAsset defaultFontAsset
		{
			get
			{
				return this.m_DefaultFontAsset;
			}
			set
			{
				this.m_DefaultFontAsset = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0001B187 File Offset: 0x00019387
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0001B18F File Offset: 0x0001938F
		public string defaultFontAssetPath
		{
			get
			{
				return this.m_DefaultFontAssetPath;
			}
			set
			{
				this.m_DefaultFontAssetPath = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0001B198 File Offset: 0x00019398
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0001B1A0 File Offset: 0x000193A0
		public List<FontAsset> fallbackFontAssets
		{
			get
			{
				return this.m_FallbackFontAssets;
			}
			set
			{
				this.m_FallbackFontAssets = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0001B1A9 File Offset: 0x000193A9
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0001B1B1 File Offset: 0x000193B1
		public bool matchMaterialPreset
		{
			get
			{
				return this.m_MatchMaterialPreset;
			}
			set
			{
				this.m_MatchMaterialPreset = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0001B1BA File Offset: 0x000193BA
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0001B1C2 File Offset: 0x000193C2
		public int missingCharacterUnicode
		{
			get
			{
				return this.m_MissingCharacterUnicode;
			}
			set
			{
				this.m_MissingCharacterUnicode = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0001B1CB File Offset: 0x000193CB
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0001B1D3 File Offset: 0x000193D3
		public bool clearDynamicDataOnBuild
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0001B1DC File Offset: 0x000193DC
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0001B1E4 File Offset: 0x000193E4
		public SpriteAsset defaultSpriteAsset
		{
			get
			{
				return this.m_DefaultSpriteAsset;
			}
			set
			{
				this.m_DefaultSpriteAsset = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0001B1ED File Offset: 0x000193ED
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0001B1F5 File Offset: 0x000193F5
		public string defaultSpriteAssetPath
		{
			get
			{
				return this.m_DefaultSpriteAssetPath;
			}
			set
			{
				this.m_DefaultSpriteAssetPath = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0001B1FE File Offset: 0x000193FE
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0001B206 File Offset: 0x00019406
		public List<SpriteAsset> fallbackSpriteAssets
		{
			get
			{
				return this.m_FallbackSpriteAssets;
			}
			set
			{
				this.m_FallbackSpriteAssets = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0001B20F File Offset: 0x0001940F
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0001B217 File Offset: 0x00019417
		public uint missingSpriteCharacterUnicode
		{
			get
			{
				return this.m_MissingSpriteCharacterUnicode;
			}
			set
			{
				this.m_MissingSpriteCharacterUnicode = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0001B220 File Offset: 0x00019420
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0001B228 File Offset: 0x00019428
		public TextStyleSheet defaultStyleSheet
		{
			get
			{
				return this.m_DefaultStyleSheet;
			}
			set
			{
				this.m_DefaultStyleSheet = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0001B231 File Offset: 0x00019431
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0001B239 File Offset: 0x00019439
		public string styleSheetsResourcePath
		{
			get
			{
				return this.m_StyleSheetsResourcePath;
			}
			set
			{
				this.m_StyleSheetsResourcePath = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0001B242 File Offset: 0x00019442
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0001B24A File Offset: 0x0001944A
		public string defaultColorGradientPresetsPath
		{
			get
			{
				return this.m_DefaultColorGradientPresetsPath;
			}
			set
			{
				this.m_DefaultColorGradientPresetsPath = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0001B254 File Offset: 0x00019454
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0001B28C File Offset: 0x0001948C
		public UnicodeLineBreakingRules lineBreakingRules
		{
			get
			{
				bool flag = this.m_UnicodeLineBreakingRules == null;
				if (flag)
				{
					this.m_UnicodeLineBreakingRules = new UnicodeLineBreakingRules();
					UnicodeLineBreakingRules.LoadLineBreakingRules();
				}
				return this.m_UnicodeLineBreakingRules;
			}
			set
			{
				this.m_UnicodeLineBreakingRules = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0001B295 File Offset: 0x00019495
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0001B29D File Offset: 0x0001949D
		public bool displayWarnings
		{
			get
			{
				return this.m_DisplayWarnings;
			}
			set
			{
				this.m_DisplayWarnings = value;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0001B2A8 File Offset: 0x000194A8
		protected void InitializeFontReferenceLookup()
		{
			bool flag = this.m_FontReferences == null;
			if (flag)
			{
				this.m_FontReferences = new List<TextSettings.FontReferenceMap>();
			}
			for (int i = 0; i < this.m_FontReferences.Count; i++)
			{
				TextSettings.FontReferenceMap fontReferenceMap = this.m_FontReferences[i];
				bool flag2 = fontReferenceMap.font == null || fontReferenceMap.fontAsset == null;
				if (flag2)
				{
					Debug.Log("Deleting invalid font reference.");
					this.m_FontReferences.RemoveAt(i);
					i--;
				}
				else
				{
					int instanceID = fontReferenceMap.font.GetInstanceID();
					bool flag3 = !this.m_FontLookup.ContainsKey(instanceID);
					if (flag3)
					{
						this.m_FontLookup.Add(instanceID, fontReferenceMap.fontAsset);
					}
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0001B378 File Offset: 0x00019578
		protected FontAsset GetCachedFontAssetInternal(Font font)
		{
			bool flag = this.m_FontLookup == null;
			if (flag)
			{
				this.m_FontLookup = new Dictionary<int, FontAsset>();
				this.InitializeFontReferenceLookup();
			}
			int instanceID = font.GetInstanceID();
			bool flag2 = this.m_FontLookup.ContainsKey(instanceID);
			FontAsset result;
			if (flag2)
			{
				result = this.m_FontLookup[instanceID];
			}
			else
			{
				bool flag3 = font.name == "System Normal";
				FontAsset fontAsset;
				if (flag3)
				{
					fontAsset = FontAsset.CreateFontAsset("Lucida Grande", "Regular", 90);
				}
				else
				{
					fontAsset = FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
				}
				bool flag4 = fontAsset != null;
				if (flag4)
				{
					fontAsset.hideFlags = HideFlags.DontSave;
					fontAsset.atlasTextures[0].hideFlags = HideFlags.DontSave;
					fontAsset.material.hideFlags = HideFlags.DontSave;
					fontAsset.isMultiAtlasTexturesEnabled = true;
					this.m_FontReferences.Add(new TextSettings.FontReferenceMap(font, fontAsset));
					this.m_FontLookup.Add(instanceID, fontAsset);
				}
				result = fontAsset;
			}
			return result;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0001B480 File Offset: 0x00019680
		public TextSettings()
		{
		}

		// Token: 0x04000329 RID: 809
		[SerializeField]
		protected string m_Version;

		// Token: 0x0400032A RID: 810
		[FormerlySerializedAs("m_defaultFontAsset")]
		[SerializeField]
		protected FontAsset m_DefaultFontAsset;

		// Token: 0x0400032B RID: 811
		[FormerlySerializedAs("m_defaultFontAssetPath")]
		[SerializeField]
		protected string m_DefaultFontAssetPath = "Fonts & Materials/";

		// Token: 0x0400032C RID: 812
		[SerializeField]
		[FormerlySerializedAs("m_fallbackFontAssets")]
		protected List<FontAsset> m_FallbackFontAssets;

		// Token: 0x0400032D RID: 813
		[FormerlySerializedAs("m_matchMaterialPreset")]
		[SerializeField]
		protected bool m_MatchMaterialPreset;

		// Token: 0x0400032E RID: 814
		[SerializeField]
		[FormerlySerializedAs("m_missingGlyphCharacter")]
		protected int m_MissingCharacterUnicode;

		// Token: 0x0400032F RID: 815
		[SerializeField]
		protected bool m_ClearDynamicDataOnBuild = true;

		// Token: 0x04000330 RID: 816
		[FormerlySerializedAs("m_defaultSpriteAsset")]
		[SerializeField]
		protected SpriteAsset m_DefaultSpriteAsset;

		// Token: 0x04000331 RID: 817
		[SerializeField]
		[FormerlySerializedAs("m_defaultSpriteAssetPath")]
		protected string m_DefaultSpriteAssetPath = "Sprite Assets/";

		// Token: 0x04000332 RID: 818
		[SerializeField]
		protected List<SpriteAsset> m_FallbackSpriteAssets;

		// Token: 0x04000333 RID: 819
		[SerializeField]
		protected uint m_MissingSpriteCharacterUnicode;

		// Token: 0x04000334 RID: 820
		[FormerlySerializedAs("m_defaultStyleSheet")]
		[SerializeField]
		protected TextStyleSheet m_DefaultStyleSheet;

		// Token: 0x04000335 RID: 821
		[SerializeField]
		protected string m_StyleSheetsResourcePath = "Text Style Sheets/";

		// Token: 0x04000336 RID: 822
		[FormerlySerializedAs("m_defaultColorGradientPresetsPath")]
		[SerializeField]
		protected string m_DefaultColorGradientPresetsPath = "Text Color Gradients/";

		// Token: 0x04000337 RID: 823
		[SerializeField]
		protected UnicodeLineBreakingRules m_UnicodeLineBreakingRules;

		// Token: 0x04000338 RID: 824
		[SerializeField]
		[FormerlySerializedAs("m_warningsDisabled")]
		protected bool m_DisplayWarnings = false;

		// Token: 0x04000339 RID: 825
		internal Dictionary<int, FontAsset> m_FontLookup;

		// Token: 0x0400033A RID: 826
		private List<TextSettings.FontReferenceMap> m_FontReferences = new List<TextSettings.FontReferenceMap>();

		// Token: 0x0200003F RID: 63
		[Serializable]
		private struct FontReferenceMap
		{
			// Token: 0x060001A0 RID: 416 RVA: 0x0001B4D9 File Offset: 0x000196D9
			public FontReferenceMap(Font font, FontAsset fontAsset)
			{
				this.font = font;
				this.fontAsset = fontAsset;
			}

			// Token: 0x0400033B RID: 827
			public Font font;

			// Token: 0x0400033C RID: 828
			public FontAsset fontAsset;
		}
	}
}
