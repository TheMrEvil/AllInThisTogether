using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AC RID: 684
	[HelpURL("UIE-USS")]
	[Serializable]
	public class StyleSheet : ScriptableObject
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x00061998 File Offset: 0x0005FB98
		// (set) Token: 0x06001751 RID: 5969 RVA: 0x000619B0 File Offset: 0x0005FBB0
		public bool importedWithErrors
		{
			get
			{
				return this.m_ImportedWithErrors;
			}
			internal set
			{
				this.m_ImportedWithErrors = value;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x000619BC File Offset: 0x0005FBBC
		// (set) Token: 0x06001753 RID: 5971 RVA: 0x000619D4 File Offset: 0x0005FBD4
		public bool importedWithWarnings
		{
			get
			{
				return this.m_ImportedWithWarnings;
			}
			internal set
			{
				this.m_ImportedWithWarnings = value;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x000619E0 File Offset: 0x0005FBE0
		// (set) Token: 0x06001755 RID: 5973 RVA: 0x000619F8 File Offset: 0x0005FBF8
		internal StyleRule[] rules
		{
			get
			{
				return this.m_Rules;
			}
			set
			{
				this.m_Rules = value;
				this.SetupReferences();
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x00061A0C File Offset: 0x0005FC0C
		// (set) Token: 0x06001757 RID: 5975 RVA: 0x00061A24 File Offset: 0x0005FC24
		internal StyleComplexSelector[] complexSelectors
		{
			get
			{
				return this.m_ComplexSelectors;
			}
			set
			{
				this.m_ComplexSelectors = value;
				this.SetupReferences();
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001758 RID: 5976 RVA: 0x00061A38 File Offset: 0x0005FC38
		internal List<StyleSheet> flattenedRecursiveImports
		{
			get
			{
				return this.m_FlattenedImportedStyleSheets;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001759 RID: 5977 RVA: 0x00061A50 File Offset: 0x0005FC50
		// (set) Token: 0x0600175A RID: 5978 RVA: 0x00061A68 File Offset: 0x0005FC68
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x00061A74 File Offset: 0x0005FC74
		// (set) Token: 0x0600175C RID: 5980 RVA: 0x00061A8C File Offset: 0x0005FC8C
		internal bool isDefaultStyleSheet
		{
			get
			{
				return this.m_IsDefaultStyleSheet;
			}
			set
			{
				this.m_IsDefaultStyleSheet = value;
				bool flag = this.flattenedRecursiveImports != null;
				if (flag)
				{
					foreach (StyleSheet styleSheet in this.flattenedRecursiveImports)
					{
						styleSheet.isDefaultStyleSheet = value;
					}
				}
			}
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00061AFC File Offset: 0x0005FCFC
		private static bool TryCheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle, out T value)
		{
			bool result = false;
			value = default(T);
			bool flag = handle.valueType == type && handle.valueIndex >= 0 && handle.valueIndex < list.Length;
			if (flag)
			{
				value = list[handle.valueIndex];
				result = true;
			}
			else
			{
				Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", new object[]
				{
					type,
					handle.valueType
				});
			}
			return result;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00061B80 File Offset: 0x0005FD80
		private static T CheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle)
		{
			T result = default(T);
			bool flag = handle.valueType != type;
			if (flag)
			{
				Debug.LogErrorFormat("Trying to read value of type {0} while reading a value of type {1}", new object[]
				{
					type,
					handle.valueType
				});
			}
			else
			{
				bool flag2 = list == null || handle.valueIndex < 0 || handle.valueIndex >= list.Length;
				if (flag2)
				{
					Debug.LogError("Accessing invalid property");
				}
				else
				{
					result = list[handle.valueIndex];
				}
			}
			return result;
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00061C18 File Offset: 0x0005FE18
		internal virtual void OnEnable()
		{
			this.SetupReferences();
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x00061C22 File Offset: 0x0005FE22
		internal void FlattenImportedStyleSheetsRecursive()
		{
			this.m_FlattenedImportedStyleSheets = new List<StyleSheet>();
			this.FlattenImportedStyleSheetsRecursive(this);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00061C38 File Offset: 0x0005FE38
		private void FlattenImportedStyleSheetsRecursive(StyleSheet sheet)
		{
			bool flag = sheet.imports == null;
			if (!flag)
			{
				for (int i = 0; i < sheet.imports.Length; i++)
				{
					StyleSheet styleSheet = sheet.imports[i].styleSheet;
					bool flag2 = styleSheet == null;
					if (!flag2)
					{
						styleSheet.isDefaultStyleSheet = this.isDefaultStyleSheet;
						this.FlattenImportedStyleSheetsRecursive(styleSheet);
						this.m_FlattenedImportedStyleSheets.Add(styleSheet);
					}
				}
			}
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00061CB4 File Offset: 0x0005FEB4
		private void SetupReferences()
		{
			bool flag = this.complexSelectors == null || this.rules == null;
			if (!flag)
			{
				foreach (StyleRule styleRule in this.rules)
				{
					foreach (StyleProperty styleProperty in styleRule.properties)
					{
						bool flag2 = StyleSheet.CustomStartsWith(styleProperty.name, StyleSheet.kCustomPropertyMarker);
						if (flag2)
						{
							styleRule.customPropertiesCount++;
							styleProperty.isCustomProperty = true;
						}
						foreach (StyleValueHandle handle in styleProperty.values)
						{
							bool flag3 = handle.IsVarFunction();
							if (flag3)
							{
								styleProperty.requireVariableResolve = true;
								break;
							}
						}
					}
				}
				int l = 0;
				int num = this.complexSelectors.Length;
				while (l < num)
				{
					this.complexSelectors[l].CachePseudoStateMasks();
					l++;
				}
				this.orderedClassSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedNameSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedTypeSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				int m = 0;
				while (m < this.complexSelectors.Length)
				{
					StyleComplexSelector styleComplexSelector = this.complexSelectors[m];
					bool flag4 = styleComplexSelector.ruleIndex < this.rules.Length;
					if (flag4)
					{
						styleComplexSelector.rule = this.rules[styleComplexSelector.ruleIndex];
					}
					styleComplexSelector.orderInStyleSheet = m;
					StyleSelector styleSelector = styleComplexSelector.selectors[styleComplexSelector.selectors.Length - 1];
					StyleSelectorPart styleSelectorPart = styleSelector.parts[0];
					string key = styleSelectorPart.value;
					Dictionary<string, StyleComplexSelector> dictionary = null;
					switch (styleSelectorPart.type)
					{
					case StyleSelectorType.Wildcard:
					case StyleSelectorType.Type:
						key = (styleSelectorPart.value ?? "*");
						dictionary = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.Class:
						dictionary = this.orderedClassSelectors;
						break;
					case StyleSelectorType.PseudoClass:
						key = "*";
						dictionary = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.RecursivePseudoClass:
						goto IL_22B;
					case StyleSelectorType.ID:
						dictionary = this.orderedNameSelectors;
						break;
					default:
						goto IL_22B;
					}
					IL_249:
					bool flag5 = dictionary != null;
					if (flag5)
					{
						StyleComplexSelector nextInTable;
						bool flag6 = dictionary.TryGetValue(key, out nextInTable);
						if (flag6)
						{
							styleComplexSelector.nextInTable = nextInTable;
						}
						dictionary[key] = styleComplexSelector;
					}
					m++;
					continue;
					IL_22B:
					Debug.LogError(string.Format("Invalid first part type {0}", styleSelectorPart.type));
					goto IL_249;
				}
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00061F5C File Offset: 0x0006015C
		internal StyleValueKeyword ReadKeyword(StyleValueHandle handle)
		{
			return (StyleValueKeyword)handle.valueIndex;
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00061F74 File Offset: 0x00060174
		internal float ReadFloat(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Dimension;
			float result;
			if (flag)
			{
				Dimension dimension = StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
				result = dimension.value;
			}
			else
			{
				result = StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
			}
			return result;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00061FBC File Offset: 0x000601BC
		internal bool TryReadFloat(StyleValueHandle handle, out float value)
		{
			bool flag = StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out value);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Dimension dimension;
				bool flag2 = StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Float, handle, out dimension);
				value = dimension.value;
				result = flag2;
			}
			return result;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00062000 File Offset: 0x00060200
		internal Dimension ReadDimension(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Float;
			Dimension result;
			if (flag)
			{
				float value = StyleSheet.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
				result = new Dimension(value, Dimension.Unit.Unitless);
			}
			else
			{
				result = StyleSheet.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
			}
			return result;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00062048 File Offset: 0x00060248
		internal bool TryReadDimension(StyleValueHandle handle, out Dimension value)
		{
			bool flag = StyleSheet.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle, out value);
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				float value2 = 0f;
				bool flag2 = StyleSheet.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out value2);
				value = new Dimension(value2, Dimension.Unit.Unitless);
				result = flag2;
			}
			return result;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00062094 File Offset: 0x00060294
		internal Color ReadColor(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<Color>(this.colors, StyleValueType.Color, handle);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x000620B4 File Offset: 0x000602B4
		internal bool TryReadColor(StyleValueHandle handle, out Color value)
		{
			return StyleSheet.TryCheckAccess<Color>(this.colors, StyleValueType.Color, handle, out value);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000620D4 File Offset: 0x000602D4
		internal string ReadString(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.String, handle);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000620F4 File Offset: 0x000602F4
		internal bool TryReadString(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.String, handle, out value);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00062118 File Offset: 0x00060318
		internal string ReadEnum(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Enum, handle);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00062138 File Offset: 0x00060338
		internal bool TryReadEnum(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Enum, handle, out value);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00062158 File Offset: 0x00060358
		internal string ReadVariable(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.Variable, handle);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00062178 File Offset: 0x00060378
		internal bool TryReadVariable(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.Variable, handle, out value);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00062198 File Offset: 0x00060398
		internal string ReadResourcePath(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x000621B8 File Offset: 0x000603B8
		internal bool TryReadResourcePath(StyleValueHandle handle, out string value)
		{
			return StyleSheet.TryCheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle, out value);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x000621D8 File Offset: 0x000603D8
		internal Object ReadAssetReference(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x000621F8 File Offset: 0x000603F8
		internal string ReadMissingAssetReferenceUrl(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<string>(this.strings, StyleValueType.MissingAssetReference, handle);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00062218 File Offset: 0x00060418
		internal bool TryReadAssetReference(StyleValueHandle handle, out Object value)
		{
			return StyleSheet.TryCheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle, out value);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00062238 File Offset: 0x00060438
		internal StyleValueFunction ReadFunction(StyleValueHandle handle)
		{
			return (StyleValueFunction)handle.valueIndex;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00062250 File Offset: 0x00060450
		internal string ReadFunctionName(StyleValueHandle handle)
		{
			bool flag = handle.valueType != StyleValueType.Function;
			string result;
			if (flag)
			{
				Debug.LogErrorFormat(string.Format("Trying to read value of type {0} while reading a value of type {1}", StyleValueType.Function, handle.valueType), new object[0]);
				result = string.Empty;
			}
			else
			{
				StyleValueFunction valueIndex = (StyleValueFunction)handle.valueIndex;
				result = valueIndex.ToUssString();
			}
			return result;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000622B4 File Offset: 0x000604B4
		internal ScalableImage ReadScalableImage(StyleValueHandle handle)
		{
			return StyleSheet.CheckAccess<ScalableImage>(this.scalableImages, StyleValueType.ScalableImage, handle);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x000622D4 File Offset: 0x000604D4
		private static bool CustomStartsWith(string originalString, string pattern)
		{
			int length = originalString.Length;
			int length2 = pattern.Length;
			int num = 0;
			int num2 = 0;
			while (num < length && num2 < length2 && originalString[num] == pattern[num2])
			{
				num++;
				num2++;
			}
			return (num2 == length2 && length >= length2) || (num == length && length2 >= length);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00062340 File Offset: 0x00060540
		public StyleSheet()
		{
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00062349 File Offset: 0x00060549
		// Note: this type is marked as 'beforefieldinit'.
		static StyleSheet()
		{
		}

		// Token: 0x040009EC RID: 2540
		[SerializeField]
		private bool m_ImportedWithErrors;

		// Token: 0x040009ED RID: 2541
		[SerializeField]
		private bool m_ImportedWithWarnings;

		// Token: 0x040009EE RID: 2542
		[SerializeField]
		private StyleRule[] m_Rules;

		// Token: 0x040009EF RID: 2543
		[SerializeField]
		private StyleComplexSelector[] m_ComplexSelectors;

		// Token: 0x040009F0 RID: 2544
		[SerializeField]
		internal float[] floats;

		// Token: 0x040009F1 RID: 2545
		[SerializeField]
		internal Dimension[] dimensions;

		// Token: 0x040009F2 RID: 2546
		[SerializeField]
		internal Color[] colors;

		// Token: 0x040009F3 RID: 2547
		[SerializeField]
		internal string[] strings;

		// Token: 0x040009F4 RID: 2548
		[SerializeField]
		internal Object[] assets;

		// Token: 0x040009F5 RID: 2549
		[SerializeField]
		internal StyleSheet.ImportStruct[] imports;

		// Token: 0x040009F6 RID: 2550
		[SerializeField]
		private List<StyleSheet> m_FlattenedImportedStyleSheets;

		// Token: 0x040009F7 RID: 2551
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x040009F8 RID: 2552
		[SerializeField]
		internal ScalableImage[] scalableImages;

		// Token: 0x040009F9 RID: 2553
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedNameSelectors;

		// Token: 0x040009FA RID: 2554
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedTypeSelectors;

		// Token: 0x040009FB RID: 2555
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedClassSelectors;

		// Token: 0x040009FC RID: 2556
		[NonSerialized]
		private bool m_IsDefaultStyleSheet;

		// Token: 0x040009FD RID: 2557
		private static string kCustomPropertyMarker = "--";

		// Token: 0x020002AD RID: 685
		[Serializable]
		internal struct ImportStruct
		{
			// Token: 0x040009FE RID: 2558
			public StyleSheet styleSheet;

			// Token: 0x040009FF RID: 2559
			public string[] mediaQueries;
		}
	}
}
