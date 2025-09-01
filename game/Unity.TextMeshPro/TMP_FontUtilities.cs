using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000037 RID: 55
	public static class TMP_FontUtilities
	{
		// Token: 0x06000219 RID: 537 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		public static TMP_FontAsset SearchForCharacter(TMP_FontAsset font, uint unicode, out TMP_Character character)
		{
			if (TMP_FontUtilities.k_searchedFontAssets == null)
			{
				TMP_FontUtilities.k_searchedFontAssets = new List<int>();
			}
			TMP_FontUtilities.k_searchedFontAssets.Clear();
			return TMP_FontUtilities.SearchForCharacterInternal(font, unicode, out character);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0001C8FD File Offset: 0x0001AAFD
		public static TMP_FontAsset SearchForCharacter(List<TMP_FontAsset> fonts, uint unicode, out TMP_Character character)
		{
			return TMP_FontUtilities.SearchForCharacterInternal(fonts, unicode, out character);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001C908 File Offset: 0x0001AB08
		private static TMP_FontAsset SearchForCharacterInternal(TMP_FontAsset font, uint unicode, out TMP_Character character)
		{
			character = null;
			if (font == null)
			{
				return null;
			}
			if (font.characterLookupTable.TryGetValue(unicode, out character))
			{
				return font;
			}
			if (font.fallbackFontAssetTable != null && font.fallbackFontAssetTable.Count > 0)
			{
				int num = 0;
				while (num < font.fallbackFontAssetTable.Count && character == null)
				{
					TMP_FontAsset tmp_FontAsset = font.fallbackFontAssetTable[num];
					if (!(tmp_FontAsset == null))
					{
						int instanceID = tmp_FontAsset.GetInstanceID();
						if (!TMP_FontUtilities.k_searchedFontAssets.Contains(instanceID))
						{
							TMP_FontUtilities.k_searchedFontAssets.Add(instanceID);
							tmp_FontAsset = TMP_FontUtilities.SearchForCharacterInternal(tmp_FontAsset, unicode, out character);
							if (tmp_FontAsset != null)
							{
								return tmp_FontAsset;
							}
						}
					}
					num++;
				}
			}
			return null;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0001C9B0 File Offset: 0x0001ABB0
		private static TMP_FontAsset SearchForCharacterInternal(List<TMP_FontAsset> fonts, uint unicode, out TMP_Character character)
		{
			character = null;
			if (fonts != null && fonts.Count > 0)
			{
				for (int i = 0; i < fonts.Count; i++)
				{
					TMP_FontAsset tmp_FontAsset = TMP_FontUtilities.SearchForCharacterInternal(fonts[i], unicode, out character);
					if (tmp_FontAsset != null)
					{
						return tmp_FontAsset;
					}
				}
			}
			return null;
		}

		// Token: 0x040001EA RID: 490
		private static List<int> k_searchedFontAssets;
	}
}
