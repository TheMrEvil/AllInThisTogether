using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D9 RID: 729
	public class UxmlIntAttributeDescription : TypedUxmlAttributeDescription<int>
	{
		// Token: 0x0600186F RID: 6255 RVA: 0x00064BD7 File Offset: 0x00062DD7
		public UxmlIntAttributeDescription()
		{
			base.type = "int";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0;
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001870 RID: 6256 RVA: 0x00064C04 File Offset: 0x00062E04
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00064C30 File Offset: 0x00062E30
		public override int GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<int>(bag, cc, (string s, int i) => UxmlIntAttributeDescription.ConvertValueToInt(s, i), base.defaultValue);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x00064C70 File Offset: 0x00062E70
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref int value)
		{
			return base.TryGetValueFromBag<int>(bag, cc, (string s, int i) => UxmlIntAttributeDescription.ConvertValueToInt(s, i), base.defaultValue, ref value);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00064CB0 File Offset: 0x00062EB0
		private static int ConvertValueToInt(string v, int defaultValue)
		{
			int num;
			bool flag = v == null || !int.TryParse(v, out num);
			int result;
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

		// Token: 0x020002DA RID: 730
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001874 RID: 6260 RVA: 0x00064CDC File Offset: 0x00062EDC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001875 RID: 6261 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001876 RID: 6262 RVA: 0x00064CE8 File Offset: 0x00062EE8
			internal int <GetValueFromBag>b__3_0(string s, int i)
			{
				return UxmlIntAttributeDescription.ConvertValueToInt(s, i);
			}

			// Token: 0x06001877 RID: 6263 RVA: 0x00064CE8 File Offset: 0x00062EE8
			internal int <TryGetValueFromBag>b__4_0(string s, int i)
			{
				return UxmlIntAttributeDescription.ConvertValueToInt(s, i);
			}

			// Token: 0x04000A86 RID: 2694
			public static readonly UxmlIntAttributeDescription.<>c <>9 = new UxmlIntAttributeDescription.<>c();

			// Token: 0x04000A87 RID: 2695
			public static Func<string, int, int> <>9__3_0;

			// Token: 0x04000A88 RID: 2696
			public static Func<string, int, int> <>9__4_0;
		}
	}
}
