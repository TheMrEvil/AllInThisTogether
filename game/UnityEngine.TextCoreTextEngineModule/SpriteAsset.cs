using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000019 RID: 25
	[HelpURL("https://docs.unity3d.com/2021.3/Documentation/Manual/UIE-sprite.html")]
	[ExcludeFromPreset]
	public class SpriteAsset : TextAsset
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006FE4 File Offset: 0x000051E4
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00006FFC File Offset: 0x000051FC
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00007008 File Offset: 0x00005208
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00007020 File Offset: 0x00005220
		public Texture spriteSheet
		{
			get
			{
				return this.m_SpriteAtlasTexture;
			}
			internal set
			{
				this.m_SpriteAtlasTexture = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000702C File Offset: 0x0000522C
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00007058 File Offset: 0x00005258
		public List<SpriteCharacter> spriteCharacterTable
		{
			get
			{
				bool flag = this.m_GlyphIndexLookup == null;
				if (flag)
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00007064 File Offset: 0x00005264
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00007090 File Offset: 0x00005290
		public Dictionary<uint, SpriteCharacter> spriteCharacterLookupTable
		{
			get
			{
				bool flag = this.m_SpriteCharacterLookup == null;
				if (flag)
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000709C File Offset: 0x0000529C
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x000070B4 File Offset: 0x000052B4
		public List<SpriteGlyph> spriteGlyphTable
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

		// Token: 0x060000D2 RID: 210 RVA: 0x00002EF5 File Offset: 0x000010F5
		private void Awake()
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000070C0 File Offset: 0x000052C0
		public void UpdateLookupTables()
		{
			bool flag = this.m_GlyphIndexLookup == null;
			if (flag)
			{
				this.m_GlyphIndexLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_GlyphIndexLookup.Clear();
			}
			bool flag2 = this.m_SpriteGlyphLookup == null;
			if (flag2)
			{
				this.m_SpriteGlyphLookup = new Dictionary<uint, SpriteGlyph>();
			}
			else
			{
				this.m_SpriteGlyphLookup.Clear();
			}
			for (int i = 0; i < this.m_SpriteGlyphTable.Count; i++)
			{
				SpriteGlyph spriteGlyph = this.m_SpriteGlyphTable[i];
				uint index = spriteGlyph.index;
				bool flag3 = !this.m_GlyphIndexLookup.ContainsKey(index);
				if (flag3)
				{
					this.m_GlyphIndexLookup.Add(index, i);
				}
				bool flag4 = !this.m_SpriteGlyphLookup.ContainsKey(index);
				if (flag4)
				{
					this.m_SpriteGlyphLookup.Add(index, spriteGlyph);
				}
			}
			bool flag5 = this.m_NameLookup == null;
			if (flag5)
			{
				this.m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				this.m_NameLookup.Clear();
			}
			bool flag6 = this.m_SpriteCharacterLookup == null;
			if (flag6)
			{
				this.m_SpriteCharacterLookup = new Dictionary<uint, SpriteCharacter>();
			}
			else
			{
				this.m_SpriteCharacterLookup.Clear();
			}
			for (int j = 0; j < this.m_SpriteCharacterTable.Count; j++)
			{
				SpriteCharacter spriteCharacter = this.m_SpriteCharacterTable[j];
				bool flag7 = spriteCharacter == null;
				if (!flag7)
				{
					uint glyphIndex = spriteCharacter.glyphIndex;
					bool flag8 = !this.m_SpriteGlyphLookup.ContainsKey(glyphIndex);
					if (!flag8)
					{
						spriteCharacter.glyph = this.m_SpriteGlyphLookup[glyphIndex];
						spriteCharacter.textAsset = this;
						int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(this.m_SpriteCharacterTable[j].name);
						bool flag9 = !this.m_NameLookup.ContainsKey(hashCodeCaseInSensitive);
						if (flag9)
						{
							this.m_NameLookup.Add(hashCodeCaseInSensitive, j);
						}
						uint unicode = this.m_SpriteCharacterTable[j].unicode;
						bool flag10 = unicode != 65534U && !this.m_SpriteCharacterLookup.ContainsKey(unicode);
						if (flag10)
						{
							this.m_SpriteCharacterLookup.Add(unicode, spriteCharacter);
						}
					}
				}
			}
			this.m_IsSpriteAssetLookupTablesDirty = false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000072FC File Offset: 0x000054FC
		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int num;
			bool flag2 = this.m_NameLookup.TryGetValue(hashCode, out num);
			int result;
			if (flag2)
			{
				result = num;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000733C File Offset: 0x0000553C
		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			bool flag = this.m_SpriteCharacterLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			SpriteCharacter spriteCharacter;
			bool flag2 = this.m_SpriteCharacterLookup.TryGetValue(unicode, out spriteCharacter);
			int result;
			if (flag2)
			{
				result = (int)spriteCharacter.glyphIndex;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007380 File Offset: 0x00005580
		public int GetSpriteIndexFromName(string name)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(name);
			return this.GetSpriteIndexFromHashcode(hashCodeCaseInSensitive);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000073B4 File Offset: 0x000055B4
		public static SpriteAsset SearchForSpriteByUnicode(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			bool flag = spriteAsset == null;
			SpriteAsset result;
			if (flag)
			{
				spriteIndex = -1;
				result = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					result = spriteAsset;
				}
				else
				{
					bool flag3 = SpriteAsset.k_searchedSpriteAssets == null;
					if (flag3)
					{
						SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
					}
					else
					{
						SpriteAsset.k_searchedSpriteAssets.Clear();
					}
					int instanceID = spriteAsset.GetInstanceID();
					SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						result = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
					}
					else
					{
						spriteIndex = -1;
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007464 File Offset: 0x00005664
		private static SpriteAsset SearchForSpriteByUnicodeInternal(List<SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				SpriteAsset spriteAsset = spriteAssets[i];
				bool flag = spriteAsset == null;
				if (!flag)
				{
					int instanceID = spriteAsset.GetInstanceID();
					bool flag2 = !SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
					if (!flag2)
					{
						spriteAsset = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset, unicode, includeFallbacks, out spriteIndex);
						bool flag3 = spriteAsset != null;
						if (flag3)
						{
							return spriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000074E4 File Offset: 0x000056E4
		private static SpriteAsset SearchForSpriteByUnicodeInternal(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			bool flag = spriteIndex != -1;
			SpriteAsset result;
			if (flag)
			{
				result = spriteAsset;
			}
			else
			{
				bool flag2 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					result = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007544 File Offset: 0x00005744
		public static SpriteAsset SearchForSpriteByHashCode(SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex, TextSettings textSettings = null)
		{
			bool flag = spriteAsset == null;
			SpriteAsset result;
			if (flag)
			{
				spriteIndex = -1;
				result = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					result = spriteAsset;
				}
				else
				{
					bool flag3 = SpriteAsset.k_searchedSpriteAssets == null;
					if (flag3)
					{
						SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
					}
					else
					{
						SpriteAsset.k_searchedSpriteAssets.Clear();
					}
					int instanceID = spriteAsset.instanceID;
					SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						SpriteAsset result2 = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
						bool flag5 = spriteIndex != -1;
						if (flag5)
						{
							return result2;
						}
					}
					bool flag6 = textSettings == null;
					if (flag6)
					{
						spriteIndex = -1;
						result = null;
					}
					else
					{
						bool flag7 = includeFallbacks && textSettings.defaultSpriteAsset != null;
						if (flag7)
						{
							SpriteAsset result2 = SpriteAsset.SearchForSpriteByHashCodeInternal(textSettings.defaultSpriteAsset, hashCode, true, out spriteIndex);
							bool flag8 = spriteIndex != -1;
							if (flag8)
							{
								return result2;
							}
						}
						SpriteAsset.k_searchedSpriteAssets.Clear();
						uint missingSpriteCharacterUnicode = textSettings.missingSpriteCharacterUnicode;
						spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(missingSpriteCharacterUnicode);
						bool flag9 = spriteIndex != -1;
						if (flag9)
						{
							result = spriteAsset;
						}
						else
						{
							SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
							bool flag10 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
							if (flag10)
							{
								SpriteAsset result2 = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, missingSpriteCharacterUnicode, true, out spriteIndex);
								bool flag11 = spriteIndex != -1;
								if (flag11)
								{
									return result2;
								}
							}
							bool flag12 = includeFallbacks && textSettings.defaultSpriteAsset != null;
							if (flag12)
							{
								SpriteAsset result2 = SpriteAsset.SearchForSpriteByUnicodeInternal(textSettings.defaultSpriteAsset, missingSpriteCharacterUnicode, true, out spriteIndex);
								bool flag13 = spriteIndex != -1;
								if (flag13)
								{
									return result2;
								}
							}
							spriteIndex = -1;
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007730 File Offset: 0x00005930
		private static SpriteAsset SearchForSpriteByHashCodeInternal(List<SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				SpriteAsset spriteAsset = spriteAssets[i];
				bool flag = spriteAsset == null;
				if (!flag)
				{
					int instanceID = spriteAsset.instanceID;
					bool flag2 = !SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
					if (!flag2)
					{
						spriteAsset = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset, hashCode, searchFallbacks, out spriteIndex);
						bool flag3 = spriteAsset != null;
						if (flag3)
						{
							return spriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000077B0 File Offset: 0x000059B0
		private static SpriteAsset SearchForSpriteByHashCodeInternal(SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			bool flag = spriteIndex != -1;
			SpriteAsset result;
			if (flag)
			{
				result = spriteAsset;
			}
			else
			{
				bool flag2 = searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					result = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007810 File Offset: 0x00005A10
		public void SortGlyphTable()
		{
			bool flag = this.m_SpriteGlyphTable == null || this.m_SpriteGlyphTable.Count == 0;
			if (!flag)
			{
				this.m_SpriteGlyphTable = (from item in this.m_SpriteGlyphTable
				orderby item.index
				select item).ToList<SpriteGlyph>();
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007874 File Offset: 0x00005A74
		internal void SortCharacterTable()
		{
			bool flag = this.m_SpriteCharacterTable != null && this.m_SpriteCharacterTable.Count > 0;
			if (flag)
			{
				this.m_SpriteCharacterTable = (from c in this.m_SpriteCharacterTable
				orderby c.unicode
				select c).ToList<SpriteCharacter>();
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000078D4 File Offset: 0x00005AD4
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000078E5 File Offset: 0x00005AE5
		public SpriteAsset()
		{
		}

		// Token: 0x040000A2 RID: 162
		internal Dictionary<int, int> m_NameLookup;

		// Token: 0x040000A3 RID: 163
		internal Dictionary<uint, int> m_GlyphIndexLookup;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		[FormerlySerializedAs("spriteSheet")]
		internal Texture m_SpriteAtlasTexture;

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private List<SpriteCharacter> m_SpriteCharacterTable = new List<SpriteCharacter>();

		// Token: 0x040000A7 RID: 167
		internal Dictionary<uint, SpriteCharacter> m_SpriteCharacterLookup;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private List<SpriteGlyph> m_SpriteGlyphTable = new List<SpriteGlyph>();

		// Token: 0x040000A9 RID: 169
		internal Dictionary<uint, SpriteGlyph> m_SpriteGlyphLookup;

		// Token: 0x040000AA RID: 170
		[SerializeField]
		public List<SpriteAsset> fallbackSpriteAssets;

		// Token: 0x040000AB RID: 171
		internal bool m_IsSpriteAssetLookupTablesDirty = false;

		// Token: 0x040000AC RID: 172
		private static HashSet<int> k_searchedSpriteAssets;

		// Token: 0x0200001A RID: 26
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060000E1 RID: 225 RVA: 0x0000790B File Offset: 0x00005B0B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x000059A4 File Offset: 0x00003BA4
			public <>c()
			{
			}

			// Token: 0x060000E3 RID: 227 RVA: 0x000059B5 File Offset: 0x00003BB5
			internal uint <SortGlyphTable>b__37_0(SpriteGlyph item)
			{
				return item.index;
			}

			// Token: 0x060000E4 RID: 228 RVA: 0x000059AD File Offset: 0x00003BAD
			internal uint <SortCharacterTable>b__38_0(SpriteCharacter c)
			{
				return c.unicode;
			}

			// Token: 0x040000AD RID: 173
			public static readonly SpriteAsset.<>c <>9 = new SpriteAsset.<>c();

			// Token: 0x040000AE RID: 174
			public static Func<SpriteGlyph, uint> <>9__37_0;

			// Token: 0x040000AF RID: 175
			public static Func<SpriteCharacter, uint> <>9__38_0;
		}
	}
}
