using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BA RID: 698
	public class PanelTextSettings : TextSettings
	{
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x00062CDC File Offset: 0x00060EDC
		internal static PanelTextSettings defaultPanelTextSettings
		{
			get
			{
				bool flag = PanelTextSettings.s_DefaultPanelTextSettings == null;
				if (flag)
				{
					bool flag2 = PanelTextSettings.s_DefaultPanelTextSettings == null;
					if (flag2)
					{
						PanelTextSettings.s_DefaultPanelTextSettings = ScriptableObject.CreateInstance<PanelTextSettings>();
					}
				}
				return PanelTextSettings.s_DefaultPanelTextSettings;
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00062D20 File Offset: 0x00060F20
		internal static void UpdateLocalizationFontAsset()
		{
			string str = " - Linux";
			Dictionary<SystemLanguage, string> dictionary = new Dictionary<SystemLanguage, string>
			{
				{
					SystemLanguage.English,
					Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/English" + str + ".asset")
				},
				{
					SystemLanguage.Japanese,
					Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/Japanese" + str + ".asset")
				},
				{
					SystemLanguage.ChineseSimplified,
					Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/ChineseSimplified" + str + ".asset")
				},
				{
					SystemLanguage.ChineseTraditional,
					Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/ChineseTraditional" + str + ".asset")
				},
				{
					SystemLanguage.Korean,
					Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/Korean" + str + ".asset")
				}
			};
			string arg = Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/GlobalFallback/GlobalFallback" + str + ".asset");
			FontAsset value = PanelTextSettings.EditorGUIUtilityLoad(dictionary[PanelTextSettings.GetCurrentLanguage()]) as FontAsset;
			FontAsset value2 = PanelTextSettings.EditorGUIUtilityLoad(arg) as FontAsset;
			PanelTextSettings.defaultPanelTextSettings.fallbackFontAssets[0] = value;
			PanelTextSettings.defaultPanelTextSettings.fallbackFontAssets[PanelTextSettings.defaultPanelTextSettings.fallbackFontAssets.Count - 1] = value2;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00062E6C File Offset: 0x0006106C
		internal FontAsset GetCachedFontAsset(Font font)
		{
			return base.GetCachedFontAssetInternal(font);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00062E85 File Offset: 0x00061085
		public PanelTextSettings()
		{
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00062E8E File Offset: 0x0006108E
		// Note: this type is marked as 'beforefieldinit'.
		static PanelTextSettings()
		{
		}

		// Token: 0x04000A37 RID: 2615
		private static PanelTextSettings s_DefaultPanelTextSettings;

		// Token: 0x04000A38 RID: 2616
		internal static Func<string, Object> EditorGUIUtilityLoad;

		// Token: 0x04000A39 RID: 2617
		internal static Func<SystemLanguage> GetCurrentLanguage;

		// Token: 0x04000A3A RID: 2618
		internal static readonly string s_DefaultEditorPanelTextSettingPath = "UIPackageResources/Default Editor Text Settings.asset";
	}
}
