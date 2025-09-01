using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002DF RID: 735
	public class UxmlColorAttributeDescription : TypedUxmlAttributeDescription<Color>
	{
		// Token: 0x0600188A RID: 6282 RVA: 0x00064F20 File Offset: 0x00063120
		public UxmlColorAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = new Color(0f, 0f, 0f, 1f);
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00064F70 File Offset: 0x00063170
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString();
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00064F98 File Offset: 0x00063198
		public override Color GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Color>(bag, cc, (string s, Color color) => UxmlColorAttributeDescription.ConvertValueToColor(s, color), base.defaultValue);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00064FD8 File Offset: 0x000631D8
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Color value)
		{
			return base.TryGetValueFromBag<Color>(bag, cc, (string s, Color color) => UxmlColorAttributeDescription.ConvertValueToColor(s, color), base.defaultValue, ref value);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00065018 File Offset: 0x00063218
		private static Color ConvertValueToColor(string v, Color defaultValue)
		{
			Color color;
			bool flag = v == null || !ColorUtility.TryParseHtmlString(v, out color);
			Color result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				result = color;
			}
			return result;
		}

		// Token: 0x020002E0 RID: 736
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600188F RID: 6287 RVA: 0x00065044 File Offset: 0x00063244
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001890 RID: 6288 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001891 RID: 6289 RVA: 0x00065050 File Offset: 0x00063250
			internal Color <GetValueFromBag>b__3_0(string s, Color color)
			{
				return UxmlColorAttributeDescription.ConvertValueToColor(s, color);
			}

			// Token: 0x06001892 RID: 6290 RVA: 0x00065050 File Offset: 0x00063250
			internal Color <TryGetValueFromBag>b__4_0(string s, Color color)
			{
				return UxmlColorAttributeDescription.ConvertValueToColor(s, color);
			}

			// Token: 0x04000A8F RID: 2703
			public static readonly UxmlColorAttributeDescription.<>c <>9 = new UxmlColorAttributeDescription.<>c();

			// Token: 0x04000A90 RID: 2704
			public static Func<string, Color, Color> <>9__3_0;

			// Token: 0x04000A91 RID: 2705
			public static Func<string, Color, Color> <>9__4_0;
		}
	}
}
