using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000114 RID: 276
	internal sealed class PrimaryKeyTypeConverter : ReferenceConverter
	{
		// Token: 0x06000F96 RID: 3990 RVA: 0x0003FD2E File Offset: 0x0003DF2E
		public PrimaryKeyTypeConverter() : base(typeof(DataColumn[]))
		{
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00006D64 File Offset: 0x00004F64
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0002B0E4 File Offset: 0x000292E4
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003FD40 File Offset: 0x0003DF40
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			return Array.Empty<DataColumn>().GetType().Name;
		}
	}
}
