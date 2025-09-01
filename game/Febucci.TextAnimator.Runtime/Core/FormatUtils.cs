using System;
using System.Collections.Generic;
using System.Globalization;

namespace Febucci.UI.Core
{
	// Token: 0x02000043 RID: 67
	public static class FormatUtils
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00006CB4 File Offset: 0x00004EB4
		public static bool TryGetFloat(List<string> attributes, int index, float defValue, out float result)
		{
			if (index >= attributes.Count || index < 0)
			{
				result = defValue;
				return false;
			}
			return FormatUtils.TryGetFloat(attributes[index], defValue, out result);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006CD6 File Offset: 0x00004ED6
		public static bool TryGetFloat(string attribute, float defValue, out float result)
		{
			if (FormatUtils.ParseFloat(attribute, out result))
			{
				return true;
			}
			result = defValue;
			return false;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006CE7 File Offset: 0x00004EE7
		public static bool ParseFloat(string value, out float result)
		{
			return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
		}
	}
}
