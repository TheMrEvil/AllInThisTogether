using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000053 RID: 83
	[ExcludeFromPreset]
	public class TMP_SpriteAsset : TMP_Asset
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00025E83 File Offset: 0x00024083
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00025E8B File Offset: 0x0002408B
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00025E94 File Offset: 0x00024094
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00025E9C File Offset: 0x0002409C
		public FaceInfo faceInfo
		{
			get
			{
				return this.m_FaceInfo;
			}
			internal set
			{
				this.m_FaceInfo = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00025EA5 File Offset: 0x000240A5
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x00025EBB File Offset: 0x000240BB
		public List<TMP_SpriteCharacter> spriteCharacterTable
		{
			get
			{
				if (this.m_GlyphIndexLookup == null)
				{
					this.UpdateLookupTables();
				}
				return this.m_SpriteCharacterTable;
			}
			internal set
			{
				this.m_SpriteCharacterTable = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00025EC4 File Offset: 0x000240C4
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00025EDA File Offset: 0x000240DA
		public Dictionary<uint, TMP_SpriteCharacter> spriteCharacterLookupTable
		{
			get
			{
				if (this.m_SpriteCharacterLookup == null)
				{
					this.UpdateLookupTables();
				}
				return this.m_SpriteCharacterLookup;
			}
			internal set
			{
				this.m_SpriteCharacterLookup = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00025EE3 File Offset: 0x000240E3
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00025EEB File Offset: 0x000240EB
		public List<TMP_SpriteGlyph> spriteGlyphTable
		{
			get
			{
				return this.m_SpriteGlyphTable;
			}
			internal set
			{
				this.m_SpriteGlyphTable = value;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00025EF4 File Offset: 0x000240F4
		private void Awake()
		{
			if (this.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeSpriteAsset();
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00025F17 File Offset: 0x00024117
		private Material GetDefaultSpriteMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			Material material = new Material(Shader.Find("TextMeshPro/Sprite"));
			material.SetTexture(ShaderUtilities.ID_MainTex, this.spriteSheet);
			material.hideFlags = HideFlags.HideInHierarchy;
			return material;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00025F48 File Offset: 0x00024148
		public void UpdateLookupTables()
		{
			if (this.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeSpriteAsset();
			}
			if (this.m_GlyphIndexLookup == null)
			{
				this.m_GlyphIndexLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_GlyphIndexLookup.Clear();
			}
			if (this.m_SpriteGlyphLookup == null)
			{
				this.m_SpriteGlyphLookup = new Dictionary<uint, TMP_SpriteGlyph>();
			}
			else
			{
				this.m_SpriteGlyphLookup.Clear();
			}
			for (int i = 0; i < this.m_SpriteGlyphTable.Count; i++)
			{
				TMP_SpriteGlyph tmp_SpriteGlyph = this.m_SpriteGlyphTable[i];
				uint index = tmp_SpriteGlyph.index;
				if (!this.m_GlyphIndexLookup.ContainsKey(index))
				{
					this.m_GlyphIndexLookup.Add(index, i);
				}
				if (!this.m_SpriteGlyphLookup.ContainsKey(index))
				{
					this.m_SpriteGlyphLookup.Add(index, tmp_SpriteGlyph);
				}
			}
			if (this.m_NameLookup == null)
			{
				this.m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				this.m_NameLookup.Clear();
			}
			if (this.m_SpriteCharacterLookup == null)
			{
				this.m_SpriteCharacterLookup = new Dictionary<uint, TMP_SpriteCharacter>();
			}
			else
			{
				this.m_SpriteCharacterLookup.Clear();
			}
			for (int j = 0; j < this.m_SpriteCharacterTable.Count; j++)
			{
				TMP_SpriteCharacter tmp_SpriteCharacter = this.m_SpriteCharacterTable[j];
				if (tmp_SpriteCharacter != null)
				{
					uint glyphIndex = tmp_SpriteCharacter.glyphIndex;
					if (this.m_SpriteGlyphLookup.ContainsKey(glyphIndex))
					{
						tmp_SpriteCharacter.glyph = this.m_SpriteGlyphLookup[glyphIndex];
						tmp_SpriteCharacter.textAsset = this;
						int hashCode = this.m_SpriteCharacterTable[j].hashCode;
						if (!this.m_NameLookup.ContainsKey(hashCode))
						{
							this.m_NameLookup.Add(hashCode, j);
						}
						uint unicode = this.m_SpriteCharacterTable[j].unicode;
						if (unicode != 65534U && !this.m_SpriteCharacterLookup.ContainsKey(unicode))
						{
							this.m_SpriteCharacterLookup.Add(unicode, tmp_SpriteCharacter);
						}
					}
				}
			}
			this.m_IsSpriteAssetLookupTablesDirty = false;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00026130 File Offset: 0x00024330
		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			if (this.m_NameLookup == null)
			{
				this.UpdateLookupTables();
			}
			int result;
			if (this.m_NameLookup.TryGetValue(hashCode, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00026160 File Offset: 0x00024360
		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			if (this.m_SpriteCharacterLookup == null)
			{
				this.UpdateLookupTables();
			}
			TMP_SpriteCharacter tmp_SpriteCharacter;
			if (this.m_SpriteCharacterLookup.TryGetValue(unicode, out tmp_SpriteCharacter))
			{
				return (int)tmp_SpriteCharacter.glyphIndex;
			}
			return -1;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00026194 File Offset: 0x00024394
		public int GetSpriteIndexFromName(string name)
		{
			if (this.m_NameLookup == null)
			{
				this.UpdateLookupTables();
			}
			int simpleHashCode = TMP_TextUtilities.GetSimpleHashCode(name);
			return this.GetSpriteIndexFromHashcode(simpleHashCode);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000261C0 File Offset: 0x000243C0
		public static TMP_SpriteAsset SearchForSpriteByUnicode(TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (TMP_SpriteAsset.k_searchedSpriteAssets == null)
			{
				TMP_SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
			}
			else
			{
				TMP_SpriteAsset.k_searchedSpriteAssets.Clear();
			}
			int instanceID = spriteAsset.GetInstanceID();
			TMP_SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				return TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(TMP_Settings.defaultSpriteAsset, unicode, true, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00026268 File Offset: 0x00024468
		private static TMP_SpriteAsset SearchForSpriteByUnicodeInternal(List<TMP_SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TMP_SpriteAsset tmp_SpriteAsset = spriteAssets[i];
				if (!(tmp_SpriteAsset == null))
				{
					int instanceID = tmp_SpriteAsset.GetInstanceID();
					if (TMP_SpriteAsset.k_searchedSpriteAssets.Add(instanceID))
					{
						tmp_SpriteAsset = TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(tmp_SpriteAsset, unicode, includeFallbacks, out spriteIndex);
						if (tmp_SpriteAsset != null)
						{
							return tmp_SpriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000262C4 File Offset: 0x000244C4
		private static TMP_SpriteAsset SearchForSpriteByUnicodeInternal(TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00026304 File Offset: 0x00024504
		public static TMP_SpriteAsset SearchForSpriteByHashCode(TMP_SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (TMP_SpriteAsset.k_searchedSpriteAssets == null)
			{
				TMP_SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
			}
			else
			{
				TMP_SpriteAsset.k_searchedSpriteAssets.Clear();
			}
			int instanceID = spriteAsset.instanceID;
			TMP_SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				TMP_SpriteAsset result = TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return result;
				}
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				TMP_SpriteAsset result = TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(TMP_Settings.defaultSpriteAsset, hashCode, true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return result;
				}
			}
			TMP_SpriteAsset.k_searchedSpriteAssets.Clear();
			uint missingCharacterSpriteUnicode = TMP_Settings.missingCharacterSpriteUnicode;
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(missingCharacterSpriteUnicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			TMP_SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				TMP_SpriteAsset result = TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, missingCharacterSpriteUnicode, true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return result;
				}
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				TMP_SpriteAsset result = TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(TMP_Settings.defaultSpriteAsset, missingCharacterSpriteUnicode, true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return result;
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00026438 File Offset: 0x00024638
		private static TMP_SpriteAsset SearchForSpriteByHashCodeInternal(List<TMP_SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TMP_SpriteAsset tmp_SpriteAsset = spriteAssets[i];
				if (!(tmp_SpriteAsset == null))
				{
					int instanceID = tmp_SpriteAsset.instanceID;
					if (TMP_SpriteAsset.k_searchedSpriteAssets.Add(instanceID))
					{
						tmp_SpriteAsset = TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(tmp_SpriteAsset, hashCode, searchFallbacks, out spriteIndex);
						if (tmp_SpriteAsset != null)
						{
							return tmp_SpriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00026494 File Offset: 0x00024694
		private static TMP_SpriteAsset SearchForSpriteByHashCodeInternal(TMP_SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000264D4 File Offset: 0x000246D4
		public void SortGlyphTable()
		{
			if (this.m_SpriteGlyphTable == null || this.m_SpriteGlyphTable.Count == 0)
			{
				return;
			}
			this.m_SpriteGlyphTable = (from item in this.m_SpriteGlyphTable
			orderby item.index
			select item).ToList<TMP_SpriteGlyph>();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0002652C File Offset: 0x0002472C
		internal void SortCharacterTable()
		{
			if (this.m_SpriteCharacterTable != null && this.m_SpriteCharacterTable.Count > 0)
			{
				this.m_SpriteCharacterTable = (from c in this.m_SpriteCharacterTable
				orderby c.unicode
				select c).ToList<TMP_SpriteCharacter>();
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00026584 File Offset: 0x00024784
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00026594 File Offset: 0x00024794
		private void UpgradeSpriteAsset()
		{
			this.m_Version = "1.1.0";
			Debug.Log(string.Concat(new string[]
			{
				"Upgrading sprite asset [",
				base.name,
				"] to version ",
				this.m_Version,
				"."
			}), this);
			this.m_SpriteCharacterTable.Clear();
			this.m_SpriteGlyphTable.Clear();
			for (int i = 0; i < this.spriteInfoList.Count; i++)
			{
				TMP_Sprite tmp_Sprite = this.spriteInfoList[i];
				TMP_SpriteGlyph tmp_SpriteGlyph = new TMP_SpriteGlyph();
				tmp_SpriteGlyph.index = (uint)i;
				tmp_SpriteGlyph.sprite = tmp_Sprite.sprite;
				tmp_SpriteGlyph.metrics = new GlyphMetrics(tmp_Sprite.width, tmp_Sprite.height, tmp_Sprite.xOffset, tmp_Sprite.yOffset, tmp_Sprite.xAdvance);
				tmp_SpriteGlyph.glyphRect = new GlyphRect((int)tmp_Sprite.x, (int)tmp_Sprite.y, (int)tmp_Sprite.width, (int)tmp_Sprite.height);
				tmp_SpriteGlyph.scale = 1f;
				tmp_SpriteGlyph.atlasIndex = 0;
				this.m_SpriteGlyphTable.Add(tmp_SpriteGlyph);
				TMP_SpriteCharacter tmp_SpriteCharacter = new TMP_SpriteCharacter();
				tmp_SpriteCharacter.glyph = tmp_SpriteGlyph;
				tmp_SpriteCharacter.unicode = (uint)((tmp_Sprite.unicode == 0) ? 65534 : tmp_Sprite.unicode);
				tmp_SpriteCharacter.name = tmp_Sprite.name;
				tmp_SpriteCharacter.scale = tmp_Sprite.scale;
				this.m_SpriteCharacterTable.Add(tmp_SpriteCharacter);
			}
			this.UpdateLookupTables();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000266FF File Offset: 0x000248FF
		public TMP_SpriteAsset()
		{
		}

		// Token: 0x0400039A RID: 922
		internal Dictionary<int, int> m_NameLookup;

		// Token: 0x0400039B RID: 923
		internal Dictionary<uint, int> m_GlyphIndexLookup;

		// Token: 0x0400039C RID: 924
		[SerializeField]
		private string m_Version;

		// Token: 0x0400039D RID: 925
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x0400039E RID: 926
		public Texture spriteSheet;

		// Token: 0x0400039F RID: 927
		[SerializeField]
		private List<TMP_SpriteCharacter> m_SpriteCharacterTable = new List<TMP_SpriteCharacter>();

		// Token: 0x040003A0 RID: 928
		internal Dictionary<uint, TMP_SpriteCharacter> m_SpriteCharacterLookup;

		// Token: 0x040003A1 RID: 929
		[SerializeField]
		private List<TMP_SpriteGlyph> m_SpriteGlyphTable = new List<TMP_SpriteGlyph>();

		// Token: 0x040003A2 RID: 930
		internal Dictionary<uint, TMP_SpriteGlyph> m_SpriteGlyphLookup;

		// Token: 0x040003A3 RID: 931
		public List<TMP_Sprite> spriteInfoList;

		// Token: 0x040003A4 RID: 932
		[SerializeField]
		public List<TMP_SpriteAsset> fallbackSpriteAssets;

		// Token: 0x040003A5 RID: 933
		internal bool m_IsSpriteAssetLookupTablesDirty;

		// Token: 0x040003A6 RID: 934
		private static HashSet<int> k_searchedSpriteAssets;

		// Token: 0x020000A0 RID: 160
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600063F RID: 1599 RVA: 0x00039097 File Offset: 0x00037297
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x000390A3 File Offset: 0x000372A3
			public <>c()
			{
			}

			// Token: 0x06000641 RID: 1601 RVA: 0x000390AB File Offset: 0x000372AB
			internal uint <SortGlyphTable>b__40_0(TMP_SpriteGlyph item)
			{
				return item.index;
			}

			// Token: 0x06000642 RID: 1602 RVA: 0x000390B3 File Offset: 0x000372B3
			internal uint <SortCharacterTable>b__41_0(TMP_SpriteCharacter c)
			{
				return c.unicode;
			}

			// Token: 0x040005F5 RID: 1525
			public static readonly TMP_SpriteAsset.<>c <>9 = new TMP_SpriteAsset.<>c();

			// Token: 0x040005F6 RID: 1526
			public static Func<TMP_SpriteGlyph, uint> <>9__40_0;

			// Token: 0x040005F7 RID: 1527
			public static Func<TMP_SpriteCharacter, uint> <>9__41_0;
		}
	}
}
