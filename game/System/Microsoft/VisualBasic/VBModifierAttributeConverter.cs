using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.VisualBasic
{
	// Token: 0x02000138 RID: 312
	internal abstract class VBModifierAttributeConverter : TypeConverter
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060007B8 RID: 1976
		protected abstract object[] Values { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060007B9 RID: 1977
		protected abstract string[] Names { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060007BA RID: 1978
		protected abstract object DefaultValue { get; }

		// Token: 0x060007BB RID: 1979 RVA: 0x0001846B File Offset: 0x0001666B
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001848C File Offset: 0x0001668C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] names = this.Names;
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].Equals(text, StringComparison.OrdinalIgnoreCase))
					{
						return this.Values[i];
					}
				}
			}
			return this.DefaultValue;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000184D4 File Offset: 0x000166D4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				object[] values = this.Values;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i].Equals(value))
					{
						return this.Names[i];
					}
				}
				return "(unknown)";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00018543 File Offset: 0x00016743
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00018550 File Offset: 0x00016750
		protected VBModifierAttributeConverter()
		{
		}
	}
}
