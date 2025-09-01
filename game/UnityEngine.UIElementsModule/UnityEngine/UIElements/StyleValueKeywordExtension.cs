using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B2 RID: 690
	internal static class StyleValueKeywordExtension
	{
		// Token: 0x06001780 RID: 6016 RVA: 0x00062448 File Offset: 0x00060648
		public static string ToUssString(this StyleValueKeyword svk)
		{
			string result;
			switch (svk)
			{
			case StyleValueKeyword.Inherit:
				result = "inherit";
				break;
			case StyleValueKeyword.Initial:
				result = "initial";
				break;
			case StyleValueKeyword.Auto:
				result = "auto";
				break;
			case StyleValueKeyword.Unset:
				result = "unset";
				break;
			case StyleValueKeyword.True:
				result = "true";
				break;
			case StyleValueKeyword.False:
				result = "false";
				break;
			case StyleValueKeyword.None:
				result = "none";
				break;
			default:
				throw new ArgumentOutOfRangeException("svk", svk, "Unknown StyleValueKeyword");
			}
			return result;
		}
	}
}
