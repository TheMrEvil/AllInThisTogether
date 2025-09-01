using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.CSharp
{
	// Token: 0x0200013E RID: 318
	internal abstract class CSharpModifierAttributeConverter : TypeConverter
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000879 RID: 2169
		protected abstract object[] Values { get; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600087A RID: 2170
		protected abstract string[] Names { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600087B RID: 2171
		protected abstract object DefaultValue { get; }

		// Token: 0x0600087C RID: 2172 RVA: 0x0001846B File Offset: 0x0001666B
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				string[] names = this.Names;
				for (int i = 0; i < names.Length; i++)
				{
					if (names[i].Equals(text))
					{
						return this.Values[i];
					}
				}
			}
			return this.DefaultValue;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001E70C File Offset: 0x0001C90C
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

		// Token: 0x0600087F RID: 2175 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001E77B File Offset: 0x0001C97B
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(this.Values);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00018550 File Offset: 0x00016750
		protected CSharpModifierAttributeConverter()
		{
		}
	}
}
