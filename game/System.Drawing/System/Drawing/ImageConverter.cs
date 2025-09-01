using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace System.Drawing
{
	/// <summary>
	///   <see cref="T:System.Drawing.ImageConverter" /> is a class that can be used to convert <see cref="T:System.Drawing.Image" /> objects from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	// Token: 0x02000082 RID: 130
	public class ImageConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.ImageConverter" /> class.</summary>
		// Token: 0x0600062F RID: 1583 RVA: 0x0000362F File Offset: 0x0000182F
		public ImageConverter()
		{
		}

		/// <summary>Determines whether this <see cref="T:System.Drawing.ImageConverter" /> can convert an instance of a specified type to an <see cref="T:System.Drawing.Image" />, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that specifies the type you want to convert from.</param>
		/// <returns>This method returns <see langword="true" /> if this <see cref="T:System.Drawing.ImageConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000630 RID: 1584 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(byte[]);
		}

		/// <summary>Determines whether this <see cref="T:System.Drawing.ImageConverter" /> can convert an <see cref="T:System.Drawing.Image" /> to an instance of a specified type, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that specifies the type you want to convert to.</param>
		/// <returns>This method returns <see langword="true" /> if this <see cref="T:System.Drawing.ImageConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000631 RID: 1585 RVA: 0x00010DB9 File Offset: 0x0000EFB9
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(byte[]) || destinationType == typeof(string);
		}

		/// <summary>Converts a specified object to an <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that holds information about a specific culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to be converted.</param>
		/// <returns>If this method succeeds, it returns the <see cref="T:System.Drawing.Image" /> that it created by converting the specified object. Otherwise, it throws an exception.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		// Token: 0x06000632 RID: 1586 RVA: 0x0001200C File Offset: 0x0001020C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			byte[] array = value as byte[];
			if (array == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			return Image.FromStream(new MemoryStream(array));
		}

		/// <summary>Converts an <see cref="T:System.Drawing.Image" /> (or an object that can be cast to an <see cref="T:System.Drawing.Image" />) to the specified type.</summary>
		/// <param name="context">A formatter context. This object can be used to get more information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies formatting conventions used by a particular culture.</param>
		/// <param name="value">The <see cref="T:System.Drawing.Image" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <see cref="T:System.Drawing.Image" /> to.</param>
		/// <returns>This method returns the converted object.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		// Token: 0x06000633 RID: 1587 RVA: 0x00012038 File Offset: 0x00010238
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null)
			{
				return "(none)";
			}
			if (value is Image)
			{
				if (destinationType == typeof(string))
				{
					return value.ToString();
				}
				if (this.CanConvertTo(null, destinationType))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						((Image)value).Save(memoryStream, ((Image)value).RawFormat);
						return memoryStream.ToArray();
					}
				}
			}
			throw new NotSupportedException(Locale.GetText("ImageConverter can not convert from type '{0}'.", new object[]
			{
				value.GetType()
			}));
		}

		/// <summary>Gets the set of properties for this type.</summary>
		/// <param name="context">A type descriptor through which additional context can be provided.</param>
		/// <param name="value">The value of the object to get the properties for.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
		/// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this can return <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
		// Token: 0x06000634 RID: 1588 RVA: 0x000120DC File Offset: 0x000102DC
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(Image), attributes);
		}

		/// <summary>Indicates whether this object supports properties. By default, this is <see langword="false" />.</summary>
		/// <param name="context">A type descriptor through which additional context can be provided.</param>
		/// <returns>This method returns <see langword="true" /> if the <see cref="Overload:System.Drawing.ImageConverter.GetProperties" /> method should be called to find the properties of this object.</returns>
		// Token: 0x06000635 RID: 1589 RVA: 0x00003610 File Offset: 0x00001810
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
