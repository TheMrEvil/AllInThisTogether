using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D7 RID: 727
	public class UxmlDoubleAttributeDescription : TypedUxmlAttributeDescription<double>
	{
		// Token: 0x06001866 RID: 6246 RVA: 0x00064AAB File Offset: 0x00062CAB
		public UxmlDoubleAttributeDescription()
		{
			base.type = "double";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0.0;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x00064AE0 File Offset: 0x00062CE0
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00064B0C File Offset: 0x00062D0C
		public override double GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<double>(bag, cc, (string s, double d) => UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d), base.defaultValue);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00064B4C File Offset: 0x00062D4C
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref double value)
		{
			return base.TryGetValueFromBag<double>(bag, cc, (string s, double d) => UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d), base.defaultValue, ref value);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00064B8C File Offset: 0x00062D8C
		private static double ConvertValueToDouble(string v, double defaultValue)
		{
			double num;
			bool flag = v == null || !double.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out num);
			double result;
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

		// Token: 0x020002D8 RID: 728
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600186B RID: 6251 RVA: 0x00064BC2 File Offset: 0x00062DC2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600186C RID: 6252 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x0600186D RID: 6253 RVA: 0x00064BCE File Offset: 0x00062DCE
			internal double <GetValueFromBag>b__3_0(string s, double d)
			{
				return UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d);
			}

			// Token: 0x0600186E RID: 6254 RVA: 0x00064BCE File Offset: 0x00062DCE
			internal double <TryGetValueFromBag>b__4_0(string s, double d)
			{
				return UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d);
			}

			// Token: 0x04000A83 RID: 2691
			public static readonly UxmlDoubleAttributeDescription.<>c <>9 = new UxmlDoubleAttributeDescription.<>c();

			// Token: 0x04000A84 RID: 2692
			public static Func<string, double, double> <>9__3_0;

			// Token: 0x04000A85 RID: 2693
			public static Func<string, double, double> <>9__4_0;
		}
	}
}
