using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System
{
	/// <summary>Converts a <see cref="T:System.String" /> type to a <see cref="T:System.Uri" /> type, and vice versa.</summary>
	// Token: 0x02000176 RID: 374
	public class UriTypeConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.UriTypeConverter" /> class.</summary>
		// Token: 0x060009E5 RID: 2533 RVA: 0x00018550 File Offset: 0x00016750
		public UriTypeConverter()
		{
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002BB05 File Offset: 0x00029D05
		private bool CanConvert(Type type)
		{
			return type == typeof(string) || type == typeof(Uri) || type == typeof(InstanceDescriptor);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type that you want to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="sourceType" /> is a <see cref="T:System.String" /> type or a <see cref="T:System.Uri" /> type can be assigned from <paramref name="sourceType" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceType" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060009E7 RID: 2535 RVA: 0x0002BB3F File Offset: 0x00029D3F
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == null)
			{
				throw new ArgumentNullException("sourceType");
			}
			return this.CanConvert(sourceType);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type that you want to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="destinationType" /> is of type <see cref="T:System.ComponentModel.Design.Serialization.InstanceDescriptor" />, <see cref="T:System.String" />, or <see cref="T:System.Uri" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060009E8 RID: 2536 RVA: 0x0002BB5C File Offset: 0x00029D5C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return !(destinationType == null) && this.CanConvert(destinationType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060009E9 RID: 2537 RVA: 0x0002BB70 File Offset: 0x00029D70
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.CanConvertFrom(context, value.GetType()))
			{
				throw new NotSupportedException(Locale.GetText("Cannot convert from value."));
			}
			if (value is Uri)
			{
				return value;
			}
			string text = value as string;
			if (text != null)
			{
				return new Uri(text, UriKind.RelativeOrAbsolute);
			}
			InstanceDescriptor instanceDescriptor = value as InstanceDescriptor;
			if (instanceDescriptor != null)
			{
				return instanceDescriptor.Invoke();
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts a given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If <see langword="null" /> is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x060009EA RID: 2538 RVA: 0x0002BBE0 File Offset: 0x00029DE0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!this.CanConvertTo(context, destinationType))
			{
				throw new NotSupportedException(Locale.GetText("Cannot convert to destination type."));
			}
			Uri uri = value as Uri;
			if (uri != null)
			{
				if (destinationType == typeof(string))
				{
					return uri.ToString();
				}
				if (destinationType == typeof(Uri))
				{
					return uri;
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					return new InstanceDescriptor(typeof(Uri).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(UriKind)
					}), new object[]
					{
						uri.ToString(),
						uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns whether the given value object is a <see cref="T:System.Uri" /> or a <see cref="T:System.Uri" /> can be created from it.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.Uri" /> or a <see cref="T:System.String" /> from which a <see cref="T:System.Uri" /> can be created; otherwise, <see langword="false" />.</returns>
		// Token: 0x060009EB RID: 2539 RVA: 0x0002BCC0 File Offset: 0x00029EC0
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			return value != null && (value is string || value is Uri);
		}
	}
}
