using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002DD RID: 733
	public class UxmlBoolAttributeDescription : TypedUxmlAttributeDescription<bool>
	{
		// Token: 0x06001881 RID: 6273 RVA: 0x00064E09 File Offset: 0x00063009
		public UxmlBoolAttributeDescription()
		{
			base.type = "boolean";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = false;
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00064E34 File Offset: 0x00063034
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString().ToLower();
			}
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x00064E5C File Offset: 0x0006305C
		public override bool GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<bool>(bag, cc, (string s, bool b) => UxmlBoolAttributeDescription.ConvertValueToBool(s, b), base.defaultValue);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x00064E9C File Offset: 0x0006309C
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref bool value)
		{
			return base.TryGetValueFromBag<bool>(bag, cc, (string s, bool b) => UxmlBoolAttributeDescription.ConvertValueToBool(s, b), base.defaultValue, ref value);
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x00064EDC File Offset: 0x000630DC
		private static bool ConvertValueToBool(string v, bool defaultValue)
		{
			bool flag2;
			bool flag = v == null || !bool.TryParse(v, out flag2);
			bool result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = flag2;
			}
			return result;
		}

		// Token: 0x020002DE RID: 734
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001886 RID: 6278 RVA: 0x00064F08 File Offset: 0x00063108
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001887 RID: 6279 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001888 RID: 6280 RVA: 0x00064F14 File Offset: 0x00063114
			internal bool <GetValueFromBag>b__3_0(string s, bool b)
			{
				return UxmlBoolAttributeDescription.ConvertValueToBool(s, b);
			}

			// Token: 0x06001889 RID: 6281 RVA: 0x00064F14 File Offset: 0x00063114
			internal bool <TryGetValueFromBag>b__4_0(string s, bool b)
			{
				return UxmlBoolAttributeDescription.ConvertValueToBool(s, b);
			}

			// Token: 0x04000A8C RID: 2700
			public static readonly UxmlBoolAttributeDescription.<>c <>9 = new UxmlBoolAttributeDescription.<>c();

			// Token: 0x04000A8D RID: 2701
			public static Func<string, bool, bool> <>9__3_0;

			// Token: 0x04000A8E RID: 2702
			public static Func<string, bool, bool> <>9__4_0;
		}
	}
}
