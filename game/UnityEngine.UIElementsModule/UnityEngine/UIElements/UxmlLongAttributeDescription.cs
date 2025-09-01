using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002DB RID: 731
	public class UxmlLongAttributeDescription : TypedUxmlAttributeDescription<long>
	{
		// Token: 0x06001878 RID: 6264 RVA: 0x00064CF1 File Offset: 0x00062EF1
		public UxmlLongAttributeDescription()
		{
			base.type = "long";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0L;
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00064D1C File Offset: 0x00062F1C
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00064D48 File Offset: 0x00062F48
		public override long GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<long>(bag, cc, (string s, long l) => UxmlLongAttributeDescription.ConvertValueToLong(s, l), base.defaultValue);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00064D88 File Offset: 0x00062F88
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref long value)
		{
			return base.TryGetValueFromBag<long>(bag, cc, (string s, long l) => UxmlLongAttributeDescription.ConvertValueToLong(s, l), base.defaultValue, ref value);
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x00064DC8 File Offset: 0x00062FC8
		private static long ConvertValueToLong(string v, long defaultValue)
		{
			long num;
			bool flag = v == null || !long.TryParse(v, out num);
			long result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = num;
			}
			return result;
		}

		// Token: 0x020002DC RID: 732
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600187D RID: 6269 RVA: 0x00064DF4 File Offset: 0x00062FF4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600187E RID: 6270 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x0600187F RID: 6271 RVA: 0x00064E00 File Offset: 0x00063000
			internal long <GetValueFromBag>b__3_0(string s, long l)
			{
				return UxmlLongAttributeDescription.ConvertValueToLong(s, l);
			}

			// Token: 0x06001880 RID: 6272 RVA: 0x00064E00 File Offset: 0x00063000
			internal long <TryGetValueFromBag>b__4_0(string s, long l)
			{
				return UxmlLongAttributeDescription.ConvertValueToLong(s, l);
			}

			// Token: 0x04000A89 RID: 2697
			public static readonly UxmlLongAttributeDescription.<>c <>9 = new UxmlLongAttributeDescription.<>c();

			// Token: 0x04000A8A RID: 2698
			public static Func<string, long, long> <>9__3_0;

			// Token: 0x04000A8B RID: 2699
			public static Func<string, long, long> <>9__4_0;
		}
	}
}
