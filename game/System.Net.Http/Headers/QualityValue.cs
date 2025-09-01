using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	// Token: 0x02000062 RID: 98
	internal static class QualityValue
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0000BF80 File Offset: 0x0000A180
		public static double? GetValue(List<NameValueHeaderValue> parameters)
		{
			if (parameters == null)
			{
				return null;
			}
			NameValueHeaderValue nameValueHeaderValue = parameters.Find((NameValueHeaderValue l) => string.Equals(l.Name, "q", StringComparison.OrdinalIgnoreCase));
			if (nameValueHeaderValue == null)
			{
				return null;
			}
			double value;
			if (!double.TryParse(nameValueHeaderValue.Value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out value))
			{
				return null;
			}
			return new double?(value);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000BFF4 File Offset: 0x0000A1F4
		public static void SetValue(ref List<NameValueHeaderValue> parameters, double? value)
		{
			double? num = value;
			double num2 = 0.0;
			if (!(num.GetValueOrDefault() < num2 & num != null))
			{
				num = value;
				num2 = (double)1;
				if (!(num.GetValueOrDefault() > num2 & num != null))
				{
					if (parameters == null)
					{
						parameters = new List<NameValueHeaderValue>();
					}
					parameters.SetValue("q", (value == null) ? null : value.Value.ToString(NumberFormatInfo.InvariantInfo));
					return;
				}
			}
			throw new ArgumentOutOfRangeException("Quality");
		}

		// Token: 0x02000063 RID: 99
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000379 RID: 889 RVA: 0x0000C07C File Offset: 0x0000A27C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600037A RID: 890 RVA: 0x000022B8 File Offset: 0x000004B8
			public <>c()
			{
			}

			// Token: 0x0600037B RID: 891 RVA: 0x0000C088 File Offset: 0x0000A288
			internal bool <GetValue>b__0_0(NameValueHeaderValue l)
			{
				return string.Equals(l.Name, "q", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000142 RID: 322
			public static readonly QualityValue.<>c <>9 = new QualityValue.<>c();

			// Token: 0x04000143 RID: 323
			public static Predicate<NameValueHeaderValue> <>9__0_0;
		}
	}
}
