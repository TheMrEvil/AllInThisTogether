using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D5 RID: 725
	public class UxmlFloatAttributeDescription : TypedUxmlAttributeDescription<float>
	{
		// Token: 0x0600185D RID: 6237 RVA: 0x00064984 File Offset: 0x00062B84
		public UxmlFloatAttributeDescription()
		{
			base.type = "float";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0f;
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x000649B4 File Offset: 0x00062BB4
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x000649E0 File Offset: 0x00062BE0
		public override float GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<float>(bag, cc, (string s, float f) => UxmlFloatAttributeDescription.ConvertValueToFloat(s, f), base.defaultValue);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00064A20 File Offset: 0x00062C20
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref float value)
		{
			return base.TryGetValueFromBag<float>(bag, cc, (string s, float f) => UxmlFloatAttributeDescription.ConvertValueToFloat(s, f), base.defaultValue, ref value);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00064A60 File Offset: 0x00062C60
		private static float ConvertValueToFloat(string v, float defaultValue)
		{
			float num;
			bool flag = v == null || !float.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out num);
			float result;
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

		// Token: 0x020002D6 RID: 726
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001862 RID: 6242 RVA: 0x00064A96 File Offset: 0x00062C96
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001863 RID: 6243 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001864 RID: 6244 RVA: 0x00064AA2 File Offset: 0x00062CA2
			internal float <GetValueFromBag>b__3_0(string s, float f)
			{
				return UxmlFloatAttributeDescription.ConvertValueToFloat(s, f);
			}

			// Token: 0x06001865 RID: 6245 RVA: 0x00064AA2 File Offset: 0x00062CA2
			internal float <TryGetValueFromBag>b__4_0(string s, float f)
			{
				return UxmlFloatAttributeDescription.ConvertValueToFloat(s, f);
			}

			// Token: 0x04000A80 RID: 2688
			public static readonly UxmlFloatAttributeDescription.<>c <>9 = new UxmlFloatAttributeDescription.<>c();

			// Token: 0x04000A81 RID: 2689
			public static Func<string, float, float> <>9__3_0;

			// Token: 0x04000A82 RID: 2690
			public static Func<string, float, float> <>9__4_0;
		}
	}
}
