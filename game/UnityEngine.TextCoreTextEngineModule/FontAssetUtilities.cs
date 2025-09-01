using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200000E RID: 14
	internal static class FontAssetUtilities
	{
		// Token: 0x06000091 RID: 145 RVA: 0x000059C0 File Offset: 0x00003BC0
		internal static Character GetCharacterFromFontAsset(uint unicode, FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface)
		{
			if (includeFallbacks)
			{
				bool flag = FontAssetUtilities.k_SearchedAssets == null;
				if (flag)
				{
					FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			return FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005A0C File Offset: 0x00003C0C
		private static Character GetCharacterFromFontAsset_Internal(uint unicode, FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			Character character = null;
			bool flag = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
			bool flag2 = flag || fontWeight != TextFontWeight.Regular;
			if (flag2)
			{
				FontWeightPair[] fontWeightTable = sourceFontAsset.fontWeightTable;
				int num = 4;
				if (fontWeight <= TextFontWeight.Regular)
				{
					if (fontWeight <= TextFontWeight.ExtraLight)
					{
						if (fontWeight != TextFontWeight.Thin)
						{
							if (fontWeight == TextFontWeight.ExtraLight)
							{
								num = 2;
							}
						}
						else
						{
							num = 1;
						}
					}
					else if (fontWeight != TextFontWeight.Light)
					{
						if (fontWeight == TextFontWeight.Regular)
						{
							num = 4;
						}
					}
					else
					{
						num = 3;
					}
				}
				else if (fontWeight <= TextFontWeight.SemiBold)
				{
					if (fontWeight != TextFontWeight.Medium)
					{
						if (fontWeight == TextFontWeight.SemiBold)
						{
							num = 6;
						}
					}
					else
					{
						num = 5;
					}
				}
				else if (fontWeight != TextFontWeight.Bold)
				{
					if (fontWeight != TextFontWeight.Heavy)
					{
						if (fontWeight == TextFontWeight.Black)
						{
							num = 9;
						}
					}
					else
					{
						num = 8;
					}
				}
				else
				{
					num = 7;
				}
				FontAsset fontAsset = flag ? fontWeightTable[num].italicTypeface : fontWeightTable[num].regularTypeface;
				bool flag3 = fontAsset != null;
				if (flag3)
				{
					bool flag4 = fontAsset.characterLookupTable.TryGetValue(unicode, out character);
					if (flag4)
					{
						isAlternativeTypeface = true;
						return character;
					}
					bool flag5 = fontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || fontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS;
					if (flag5)
					{
						bool flag6 = fontAsset.TryAddCharacterInternal(unicode, out character, false);
						if (flag6)
						{
							isAlternativeTypeface = true;
							return character;
						}
					}
				}
			}
			bool flag7 = sourceFontAsset.characterLookupTable.TryGetValue(unicode, out character);
			Character result;
			if (flag7)
			{
				result = character;
			}
			else
			{
				bool flag8 = sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS;
				if (flag8)
				{
					bool flag9 = sourceFontAsset.TryAddCharacterInternal(unicode, out character, false);
					if (flag9)
					{
						return character;
					}
				}
				bool flag10 = character == null && includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null;
				if (flag10)
				{
					List<FontAsset> fallbackFontAssetTable = sourceFontAsset.fallbackFontAssetTable;
					int count = fallbackFontAssetTable.Count;
					bool flag11 = count == 0;
					if (flag11)
					{
						return null;
					}
					for (int i = 0; i < count; i++)
					{
						FontAsset fontAsset2 = fallbackFontAssetTable[i];
						bool flag12 = fontAsset2 == null;
						if (!flag12)
						{
							int instanceID = fontAsset2.instanceID;
							bool flag13 = !FontAssetUtilities.k_SearchedAssets.Add(instanceID);
							if (!flag13)
							{
								character = FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset2, true, fontStyle, fontWeight, out isAlternativeTypeface);
								bool flag14 = character != null;
								if (flag14)
								{
									return character;
								}
							}
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005C9C File Offset: 0x00003E9C
		public static Character GetCharacterFromFontAssets(uint unicode, FontAsset sourceFontAsset, List<FontAsset> fontAssets, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			bool flag = fontAssets == null || fontAssets.Count == 0;
			Character result;
			if (flag)
			{
				result = null;
			}
			else
			{
				if (includeFallbacks)
				{
					bool flag2 = FontAssetUtilities.k_SearchedAssets == null;
					if (flag2)
					{
						FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
					}
					else
					{
						FontAssetUtilities.k_SearchedAssets.Clear();
					}
				}
				int count = fontAssets.Count;
				for (int i = 0; i < count; i++)
				{
					FontAsset fontAsset = fontAssets[i];
					bool flag3 = fontAsset == null;
					if (!flag3)
					{
						Character characterFromFontAsset_Internal = FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
						bool flag4 = characterFromFontAsset_Internal != null;
						if (flag4)
						{
							return characterFromFontAsset_Internal;
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005D54 File Offset: 0x00003F54
		public static SpriteCharacter GetSpriteCharacterFromSpriteAsset(uint unicode, SpriteAsset spriteAsset, bool includeFallbacks)
		{
			bool flag = spriteAsset == null;
			SpriteCharacter result;
			if (flag)
			{
				result = null;
			}
			else
			{
				SpriteCharacter spriteCharacterFromSpriteAsset_Internal;
				bool flag2 = spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacterFromSpriteAsset_Internal);
				if (flag2)
				{
					result = spriteCharacterFromSpriteAsset_Internal;
				}
				else
				{
					if (includeFallbacks)
					{
						bool flag3 = FontAssetUtilities.k_SearchedAssets == null;
						if (flag3)
						{
							FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
						}
						else
						{
							FontAssetUtilities.k_SearchedAssets.Clear();
						}
						FontAssetUtilities.k_SearchedAssets.Add(spriteAsset.instanceID);
						List<SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
						bool flag4 = fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0;
						if (flag4)
						{
							int count = fallbackSpriteAssets.Count;
							for (int i = 0; i < count; i++)
							{
								SpriteAsset spriteAsset2 = fallbackSpriteAssets[i];
								bool flag5 = spriteAsset2 == null;
								if (!flag5)
								{
									int instanceID = spriteAsset2.instanceID;
									bool flag6 = !FontAssetUtilities.k_SearchedAssets.Add(instanceID);
									if (!flag6)
									{
										spriteCharacterFromSpriteAsset_Internal = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset2, true);
										bool flag7 = spriteCharacterFromSpriteAsset_Internal != null;
										if (flag7)
										{
											return spriteCharacterFromSpriteAsset_Internal;
										}
									}
								}
							}
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005E6C File Offset: 0x0000406C
		private static SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(uint unicode, SpriteAsset spriteAsset, bool includeFallbacks)
		{
			SpriteCharacter spriteCharacterFromSpriteAsset_Internal;
			bool flag = spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacterFromSpriteAsset_Internal);
			SpriteCharacter result;
			if (flag)
			{
				result = spriteCharacterFromSpriteAsset_Internal;
			}
			else
			{
				if (includeFallbacks)
				{
					List<SpriteAsset> fallbackSpriteAssets = spriteAsset.fallbackSpriteAssets;
					bool flag2 = fallbackSpriteAssets != null && fallbackSpriteAssets.Count > 0;
					if (flag2)
					{
						int count = fallbackSpriteAssets.Count;
						for (int i = 0; i < count; i++)
						{
							SpriteAsset spriteAsset2 = fallbackSpriteAssets[i];
							bool flag3 = spriteAsset2 == null;
							if (!flag3)
							{
								int instanceID = spriteAsset2.instanceID;
								bool flag4 = !FontAssetUtilities.k_SearchedAssets.Add(instanceID);
								if (!flag4)
								{
									spriteCharacterFromSpriteAsset_Internal = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset2, true);
									bool flag5 = spriteCharacterFromSpriteAsset_Internal != null;
									if (flag5)
									{
										return spriteCharacterFromSpriteAsset_Internal;
									}
								}
							}
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x04000065 RID: 101
		private static HashSet<int> k_SearchedAssets;
	}
}
