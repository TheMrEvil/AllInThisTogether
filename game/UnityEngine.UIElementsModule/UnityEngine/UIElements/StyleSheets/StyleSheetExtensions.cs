using System;
using System.Globalization;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200036F RID: 879
	internal static class StyleSheetExtensions
	{
		// Token: 0x06001C57 RID: 7255 RVA: 0x00086724 File Offset: 0x00084924
		public static string ReadAsString(this StyleSheet sheet, StyleValueHandle handle)
		{
			string result = string.Empty;
			switch (handle.valueType)
			{
			case StyleValueType.Keyword:
				result = sheet.ReadKeyword(handle).ToUssString();
				break;
			case StyleValueType.Float:
				result = sheet.ReadFloat(handle).ToString(CultureInfo.InvariantCulture.NumberFormat);
				break;
			case StyleValueType.Dimension:
				result = sheet.ReadDimension(handle).ToString();
				break;
			case StyleValueType.Color:
				result = sheet.ReadColor(handle).ToString();
				break;
			case StyleValueType.ResourcePath:
				result = sheet.ReadResourcePath(handle);
				break;
			case StyleValueType.AssetReference:
				result = sheet.ReadAssetReference(handle).ToString();
				break;
			case StyleValueType.Enum:
				result = sheet.ReadEnum(handle);
				break;
			case StyleValueType.Variable:
				result = sheet.ReadVariable(handle);
				break;
			case StyleValueType.String:
				result = sheet.ReadString(handle);
				break;
			case StyleValueType.Function:
				result = sheet.ReadFunctionName(handle);
				break;
			case StyleValueType.CommaSeparator:
				result = ",";
				break;
			case StyleValueType.ScalableImage:
				result = sheet.ReadScalableImage(handle).ToString();
				break;
			default:
				result = "Error reading value type (" + handle.valueType.ToString() + ") at index " + handle.valueIndex.ToString();
				break;
			}
			return result;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0008688C File Offset: 0x00084A8C
		public static bool IsVarFunction(this StyleValueHandle handle)
		{
			return handle.valueType == StyleValueType.Function && handle.valueIndex == 1;
		}
	}
}
