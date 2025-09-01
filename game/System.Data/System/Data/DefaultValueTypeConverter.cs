using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000E0 RID: 224
	internal sealed class DefaultValueTypeConverter : StringConverter
	{
		// Token: 0x06000DD2 RID: 3538 RVA: 0x00037D68 File Offset: 0x00035F68
		public DefaultValueTypeConverter()
		{
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00037D70 File Offset: 0x00035F70
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return "<null>";
				}
				if (value == DBNull.Value)
				{
					return "<DBNull>";
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00037DC8 File Offset: 0x00035FC8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value.GetType() == typeof(string))
			{
				string a = (string)value;
				if (string.Equals(a, "<null>", StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
				if (string.Equals(a, "<DBNull>", StringComparison.OrdinalIgnoreCase))
				{
					return DBNull.Value;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0400087B RID: 2171
		private const string NullString = "<null>";

		// Token: 0x0400087C RID: 2172
		private const string DbNullString = "<DBNull>";
	}
}
