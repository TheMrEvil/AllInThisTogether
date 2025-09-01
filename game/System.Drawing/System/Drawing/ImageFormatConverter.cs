using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
	/// <summary>
	///   <see cref="T:System.Drawing.ImageFormatConverter" /> is a class that can be used to convert <see cref="T:System.Drawing.Imaging.ImageFormat" /> objects from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	// Token: 0x02000083 RID: 131
	public class ImageFormatConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.ImageFormatConverter" /> class.</summary>
		// Token: 0x06000636 RID: 1590 RVA: 0x0000362F File Offset: 0x0000182F
		public ImageFormatConverter()
		{
		}

		/// <summary>Indicates whether this converter can convert an object in the specified source type to the native type of the converter.</summary>
		/// <param name="context">A formatter context. This object can be used to get more information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="sourceType">The type you want to convert from.</param>
		/// <returns>This method returns <see langword="true" /> if this object can perform the conversion.</returns>
		// Token: 0x06000637 RID: 1591 RVA: 0x00003338 File Offset: 0x00001538
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the specified destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that specifies the context for this type conversion.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> that represents the type to which you want to convert this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</param>
		/// <returns>This method returns <see langword="true" /> if this object can perform the conversion.</returns>
		// Token: 0x06000638 RID: 1592 RVA: 0x0000BB98 File Offset: 0x00009D98
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to an <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</summary>
		/// <param name="context">A formatter context. This object can be used to get more information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies formatting conventions for a particular culture.</param>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted object.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		// Token: 0x06000639 RID: 1593 RVA: 0x000120F0 File Offset: 0x000102F0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (text[0] == '[')
			{
				if (text.Equals(ImageFormat.Bmp.ToString()))
				{
					return ImageFormat.Bmp;
				}
				if (text.Equals(ImageFormat.Emf.ToString()))
				{
					return ImageFormat.Emf;
				}
				if (text.Equals(ImageFormat.Exif.ToString()))
				{
					return ImageFormat.Exif;
				}
				if (text.Equals(ImageFormat.Gif.ToString()))
				{
					return ImageFormat.Gif;
				}
				if (text.Equals(ImageFormat.Icon.ToString()))
				{
					return ImageFormat.Icon;
				}
				if (text.Equals(ImageFormat.Jpeg.ToString()))
				{
					return ImageFormat.Jpeg;
				}
				if (text.Equals(ImageFormat.MemoryBmp.ToString()))
				{
					return ImageFormat.MemoryBmp;
				}
				if (text.Equals(ImageFormat.Png.ToString()))
				{
					return ImageFormat.Png;
				}
				if (text.Equals(ImageFormat.Tiff.ToString()))
				{
					return ImageFormat.Tiff;
				}
				if (text.Equals(ImageFormat.Wmf.ToString()))
				{
					return ImageFormat.Wmf;
				}
			}
			else
			{
				if (string.Compare(text, "Bmp", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.Bmp;
				}
				if (string.Compare(text, "Emf", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.Emf;
				}
				if (string.Compare(text, "Exif", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.Exif;
				}
				if (string.Compare(text, "Gif", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.Gif;
				}
				if (string.Compare(text, "Icon", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.Icon;
				}
				if (string.Compare(text, "Jpeg", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.Jpeg;
				}
				if (string.Compare(text, "MemoryBmp", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.MemoryBmp;
				}
				if (string.Compare(text, "Png", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.Png;
				}
				if (string.Compare(text, "Tiff", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.Tiff;
				}
				if (string.Compare(text, "Wmf", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return ImageFormat.Wmf;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts the specified object to the specified type.</summary>
		/// <param name="context">A formatter context. This object can be used to get more information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies formatting conventions for a particular culture.</param>
		/// <param name="value">The object to convert.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		/// <returns>The converted object.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null." /></exception>
		// Token: 0x0600063A RID: 1594 RVA: 0x000122E4 File Offset: 0x000104E4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is ImageFormat)
			{
				ImageFormat imageFormat = (ImageFormat)value;
				string text = null;
				if (imageFormat.Guid.Equals(ImageFormat.Bmp.Guid))
				{
					text = "Bmp";
				}
				else if (imageFormat.Guid.Equals(ImageFormat.Emf.Guid))
				{
					text = "Emf";
				}
				else if (imageFormat.Guid.Equals(ImageFormat.Exif.Guid))
				{
					text = "Exif";
				}
				else if (imageFormat.Guid.Equals(ImageFormat.Gif.Guid))
				{
					text = "Gif";
				}
				else if (imageFormat.Guid.Equals(ImageFormat.Icon.Guid))
				{
					text = "Icon";
				}
				else if (imageFormat.Guid.Equals(ImageFormat.Jpeg.Guid))
				{
					text = "Jpeg";
				}
				else if (imageFormat.Guid.Equals(ImageFormat.MemoryBmp.Guid))
				{
					text = "MemoryBmp";
				}
				else if (imageFormat.Guid.Equals(ImageFormat.Png.Guid))
				{
					text = "Png";
				}
				else if (imageFormat.Guid.Equals(ImageFormat.Tiff.Guid))
				{
					text = "Tiff";
				}
				else if (imageFormat.Guid.Equals(ImageFormat.Wmf.Guid))
				{
					text = "Wmf";
				}
				if (destinationType == typeof(string))
				{
					if (text == null)
					{
						return imageFormat.ToString();
					}
					return text;
				}
				else if (destinationType == typeof(InstanceDescriptor))
				{
					if (text != null)
					{
						return new InstanceDescriptor(typeof(ImageFormat).GetTypeInfo().GetProperty(text), null);
					}
					return new InstanceDescriptor(typeof(ImageFormat).GetTypeInfo().GetConstructor(new Type[]
					{
						typeof(Guid)
					}), new object[]
					{
						imageFormat.Guid
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Gets a collection that contains a set of standard values for the data type this validator is designed for. Returns <see langword="null" /> if the data type does not support a standard set of values.</summary>
		/// <param name="context">A formatter context. This object can be used to get more information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <returns>A collection that contains a standard set of valid values, or <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
		// Token: 0x0600063B RID: 1595 RVA: 0x00012508 File Offset: 0x00010708
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(new ImageFormat[]
			{
				ImageFormat.MemoryBmp,
				ImageFormat.Bmp,
				ImageFormat.Emf,
				ImageFormat.Wmf,
				ImageFormat.Gif,
				ImageFormat.Jpeg,
				ImageFormat.Png,
				ImageFormat.Tiff,
				ImageFormat.Exif,
				ImageFormat.Icon
			});
		}

		/// <summary>Indicates whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <param name="context">A type descriptor through which additional context can be provided.</param>
		/// <returns>This method returns <see langword="true" /> if the <see cref="Overload:System.Drawing.ImageFormatConverter.GetStandardValues" /> method should be called to find a common set of values the object supports.</returns>
		// Token: 0x0600063C RID: 1596 RVA: 0x00003610 File Offset: 0x00001810
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
