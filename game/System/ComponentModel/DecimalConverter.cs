using System;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Decimal" /> objects to and from various other representations.</summary>
	// Token: 0x02000399 RID: 921
	public class DecimalConverter : BaseNumberConverter
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x00003062 File Offset: 0x00001262
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x0006B8A2 File Offset: 0x00069AA2
		internal override Type TargetType
		{
			get
			{
				return typeof(decimal);
			}
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E2C RID: 7724 RVA: 0x0006B8AE File Offset: 0x00069AAE
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to a <see cref="T:System.Decimal" /> using the arguments.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06001E2D RID: 7725 RVA: 0x0006B8CC File Offset: 0x00069ACC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(InstanceDescriptor)) || !(value is decimal))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			object[] arguments = new object[]
			{
				decimal.GetBits((decimal)value)
			};
			MemberInfo constructor = typeof(decimal).GetConstructor(new Type[]
			{
				typeof(int[])
			});
			if (constructor != null)
			{
				return new InstanceDescriptor(constructor, arguments);
			}
			return null;
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0006B95F File Offset: 0x00069B5F
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDecimal(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0006B971 File Offset: 0x00069B71
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return decimal.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0006B984 File Offset: 0x00069B84
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((decimal)value).ToString("G", formatInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DecimalConverter" /> class.</summary>
		// Token: 0x06001E31 RID: 7729 RVA: 0x00069981 File Offset: 0x00067B81
		public DecimalConverter()
		{
		}
	}
}
