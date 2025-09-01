using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AF RID: 687
	internal static class StyleValueFunctionExtension
	{
		// Token: 0x0600177B RID: 6011 RVA: 0x00062358 File Offset: 0x00060558
		public static StyleValueFunction FromUssString(string ussValue)
		{
			ussValue = ussValue.ToLower();
			string text = ussValue;
			string a = text;
			StyleValueFunction result;
			if (!(a == "var"))
			{
				if (!(a == "env"))
				{
					if (!(a == "linear-gradient"))
					{
						throw new ArgumentOutOfRangeException("ussValue", ussValue, "Unknown function name");
					}
					result = StyleValueFunction.LinearGradient;
				}
				else
				{
					result = StyleValueFunction.Env;
				}
			}
			else
			{
				result = StyleValueFunction.Var;
			}
			return result;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000623BC File Offset: 0x000605BC
		public static string ToUssString(this StyleValueFunction svf)
		{
			string result;
			switch (svf)
			{
			case StyleValueFunction.Var:
				result = "var";
				break;
			case StyleValueFunction.Env:
				result = "env";
				break;
			case StyleValueFunction.LinearGradient:
				result = "linear-gradient";
				break;
			default:
				throw new ArgumentOutOfRangeException("svf", svf, "Unknown StyleValueFunction");
			}
			return result;
		}

		// Token: 0x04000A05 RID: 2565
		public const string k_Var = "var";

		// Token: 0x04000A06 RID: 2566
		public const string k_Env = "env";

		// Token: 0x04000A07 RID: 2567
		public const string k_LinearGradient = "linear-gradient";
	}
}
