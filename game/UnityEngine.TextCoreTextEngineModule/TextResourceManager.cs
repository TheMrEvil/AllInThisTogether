using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003C RID: 60
	internal class TextResourceManager
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0001AD9C File Offset: 0x00018F9C
		internal static void AddFontAsset(FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			bool flag = !TextResourceManager.s_FontAssetReferences.ContainsKey(instanceID);
			if (flag)
			{
				TextResourceManager.FontAssetRef fontAssetRef = new TextResourceManager.FontAssetRef(fontAsset.hashCode, fontAsset.familyNameHashCode, fontAsset.styleNameHashCode, fontAsset);
				TextResourceManager.s_FontAssetReferences.Add(instanceID, fontAssetRef);
				bool flag2 = !TextResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef.nameHashCode);
				if (flag2)
				{
					TextResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef.nameHashCode, fontAsset);
				}
				bool flag3 = !TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef.familyNameAndStyleHashCode);
				if (flag3)
				{
					TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef.familyNameAndStyleHashCode, fontAsset);
				}
			}
			else
			{
				TextResourceManager.FontAssetRef fontAssetRef2 = TextResourceManager.s_FontAssetReferences[instanceID];
				bool flag4 = fontAssetRef2.nameHashCode == fontAsset.hashCode && fontAssetRef2.familyNameHashCode == fontAsset.familyNameHashCode && fontAssetRef2.styleNameHashCode == fontAsset.styleNameHashCode;
				if (!flag4)
				{
					bool flag5 = fontAssetRef2.nameHashCode != fontAsset.hashCode;
					if (flag5)
					{
						TextResourceManager.s_FontAssetNameReferenceLookup.Remove(fontAssetRef2.nameHashCode);
						fontAssetRef2.nameHashCode = fontAsset.hashCode;
						bool flag6 = !TextResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef2.nameHashCode);
						if (flag6)
						{
							TextResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef2.nameHashCode, fontAsset);
						}
					}
					bool flag7 = fontAssetRef2.familyNameHashCode != fontAsset.familyNameHashCode || fontAssetRef2.styleNameHashCode != fontAsset.styleNameHashCode;
					if (flag7)
					{
						TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(fontAssetRef2.familyNameAndStyleHashCode);
						fontAssetRef2.familyNameHashCode = fontAsset.familyNameHashCode;
						fontAssetRef2.styleNameHashCode = fontAsset.styleNameHashCode;
						fontAssetRef2.familyNameAndStyleHashCode = ((long)fontAsset.styleNameHashCode << 32 | (long)((ulong)fontAsset.familyNameHashCode));
						bool flag8 = !TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef2.familyNameAndStyleHashCode);
						if (flag8)
						{
							TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef2.familyNameAndStyleHashCode, fontAsset);
						}
					}
					TextResourceManager.s_FontAssetReferences[instanceID] = fontAssetRef2;
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00002EF5 File Offset: 0x000010F5
		internal static void RemoveFontAsset(FontAsset fontAsset)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0001AFA4 File Offset: 0x000191A4
		internal static bool TryGetFontAssetByName(int nameHashcode, out FontAsset fontAsset)
		{
			return TextResourceManager.s_FontAssetNameReferenceLookup.TryGetValue(nameHashcode, out fontAsset);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0001AFC4 File Offset: 0x000191C4
		internal static bool TryGetFontAssetByFamilyName(int familyNameHashCode, int styleNameHashCode, out FontAsset fontAsset)
		{
			fontAsset = null;
			bool flag = styleNameHashCode == 0;
			if (flag)
			{
				styleNameHashCode = TextResourceManager.k_RegularStyleHashCode;
			}
			long key = (long)styleNameHashCode << 32 | (long)((ulong)familyNameHashCode);
			return TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.TryGetValue(key, out fontAsset);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0001B000 File Offset: 0x00019200
		internal static void RebuildFontAssetCache()
		{
			foreach (KeyValuePair<int, TextResourceManager.FontAssetRef> keyValuePair in TextResourceManager.s_FontAssetReferences)
			{
				TextResourceManager.FontAssetRef value = keyValuePair.Value;
				FontAsset fontAsset = value.fontAsset;
				bool flag = fontAsset == null;
				if (flag)
				{
					TextResourceManager.s_FontAssetNameReferenceLookup.Remove(value.nameHashCode);
					TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(value.familyNameAndStyleHashCode);
					TextResourceManager.s_FontAssetRemovalList.Add(keyValuePair.Key);
				}
				else
				{
					fontAsset.InitializeCharacterLookupDictionary();
					fontAsset.AddSynthesizedCharactersAndFaceMetrics();
				}
			}
			for (int i = 0; i < TextResourceManager.s_FontAssetRemovalList.Count; i++)
			{
				TextResourceManager.s_FontAssetReferences.Remove(TextResourceManager.s_FontAssetRemovalList[i]);
			}
			TextResourceManager.s_FontAssetRemovalList.Clear();
			TextEventManager.ON_FONT_PROPERTY_CHANGED(true, null);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000059A4 File Offset: 0x00003BA4
		public TextResourceManager()
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0001B0FC File Offset: 0x000192FC
		// Note: this type is marked as 'beforefieldinit'.
		static TextResourceManager()
		{
		}

		// Token: 0x0400031F RID: 799
		private static readonly Dictionary<int, TextResourceManager.FontAssetRef> s_FontAssetReferences = new Dictionary<int, TextResourceManager.FontAssetRef>();

		// Token: 0x04000320 RID: 800
		private static readonly Dictionary<int, FontAsset> s_FontAssetNameReferenceLookup = new Dictionary<int, FontAsset>();

		// Token: 0x04000321 RID: 801
		private static readonly Dictionary<long, FontAsset> s_FontAssetFamilyNameAndStyleReferenceLookup = new Dictionary<long, FontAsset>();

		// Token: 0x04000322 RID: 802
		private static readonly List<int> s_FontAssetRemovalList = new List<int>(16);

		// Token: 0x04000323 RID: 803
		private static readonly int k_RegularStyleHashCode = TextUtilities.GetHashCodeCaseInSensitive("Regular");

		// Token: 0x0200003D RID: 61
		private struct FontAssetRef
		{
			// Token: 0x0600017C RID: 380 RVA: 0x0001B137 File Offset: 0x00019337
			public FontAssetRef(int nameHashCode, int familyNameHashCode, int styleNameHashCode, FontAsset fontAsset)
			{
				this.nameHashCode = nameHashCode;
				this.familyNameHashCode = familyNameHashCode;
				this.styleNameHashCode = styleNameHashCode;
				this.familyNameAndStyleHashCode = ((long)styleNameHashCode << 32 | (long)((ulong)familyNameHashCode));
				this.fontAsset = fontAsset;
			}

			// Token: 0x04000324 RID: 804
			public int nameHashCode;

			// Token: 0x04000325 RID: 805
			public int familyNameHashCode;

			// Token: 0x04000326 RID: 806
			public int styleNameHashCode;

			// Token: 0x04000327 RID: 807
			public long familyNameAndStyleHashCode;

			// Token: 0x04000328 RID: 808
			public readonly FontAsset fontAsset;
		}
	}
}
