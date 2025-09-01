using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
	/// <summary>Converts <see cref="T:System.Drawing.SizeF" /> objects from one type to another.</summary>
	// Token: 0x02000014 RID: 20
	public class SizeFConverter : TypeConverter
	{
		/// <summary>Returns a value indicating whether the converter can convert from the type specified to the <see cref="T:System.Drawing.SizeF" /> type, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> the represents the type you wish to convert from.</param>
		/// <returns>
		///   <see langword="true" /> to indicate the conversion can be performed; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600003B RID: 59 RVA: 0x00003338 File Offset: 0x00001538
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns a value indicating whether the <see cref="T:System.Drawing.SizeFConverter" /> can convert a <see cref="T:System.Drawing.SizeF" /> to the specified type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion otherwise, <see langword="false" />.</returns>
		// Token: 0x0600003C RID: 60 RVA: 0x00003356 File Offset: 0x00001556
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		// Token: 0x0600003D RID: 61 RVA: 0x00003654 File Offset: 0x00001854
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text2 = text.Trim();
			if (text2.Length == 0)
			{
				return null;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			char separator = culture.TextInfo.ListSeparator[0];
			string[] array = text2.Split(separator, StringSplitOptions.None);
			float[] array2 = new float[array.Length];
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (float)converter.ConvertFromString(context, culture, array[i]);
			}
			if (array2.Length == 2)
			{
				return new SizeF(array2[0], array2[1]);
			}
			throw new ArgumentException(SR.Format("Text \"{0}\" cannot be parsed. The expected text format is \"{1}\".", new object[]
			{
				text2,
				"Width,Height"
			}));
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		// Token: 0x0600003E RID: 62 RVA: 0x00003730 File Offset: 0x00001930
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is SizeF)
			{
				SizeF sizeF = (SizeF)value;
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				string separator = culture.TextInfo.ListSeparator + " ";
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
				string[] array = new string[2];
				int num = 0;
				array[num++] = converter.ConvertToString(context, culture, sizeF.Width);
				array[num++] = converter.ConvertToString(context, culture, sizeF.Height);
				return string.Join(separator, array);
			}
			if (destinationType == typeof(InstanceDescriptor) && value is SizeF)
			{
				SizeF sizeF2 = (SizeF)value;
				ConstructorInfo constructor = typeof(SizeF).GetConstructor(new Type[]
				{
					typeof(float),
					typeof(float)
				});
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[]
					{
						sizeF2.Width,
						sizeF2.Height
					});
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates an instance of a <see cref="T:System.Drawing.SizeF" /> with the specified property values using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> containing property names and values.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the new <see cref="T:System.Drawing.SizeF" />, or <see langword="null" /> if the object cannot be created.</returns>
		// Token: 0x0600003F RID: 63 RVA: 0x00003884 File Offset: 0x00001A84
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			object obj = propertyValues["Width"];
			object obj2 = propertyValues["Height"];
			if (obj == null || obj2 == null || !(obj is float) || !(obj2 is float))
			{
				throw new ArgumentException(SR.Format("IDictionary parameter contains at least one entry that is not valid. Ensure all values are consistent with the object's properties.", Array.Empty<object>()));
			}
			return new SizeF((float)obj, (float)obj2);
		}

		/// <summary>Returns a value indicating whether changing a value on this object requires a call to the <see cref="Overload:System.Drawing.SizeFConverter.CreateInstance" /> method to create a new value.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. This may be <see langword="null" />.</param>
		/// <returns>Always returns <see langword="true" />.</returns>
		// Token: 0x06000040 RID: 64 RVA: 0x00003610 File Offset: 0x00001810
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Retrieves a set of properties for the <see cref="T:System.Drawing.SizeF" /> type using the specified context and attributes.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to return properties for.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties.</returns>
		// Token: 0x06000041 RID: 65 RVA: 0x000038F8 File Offset: 0x00001AF8
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(SizeF), attributes).Sort(SizeFConverter.s_propertySort);
		}

		/// <summary>Returns whether the <see cref="T:System.Drawing.SizeF" /> type supports properties.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
		/// <returns>Always returns <see langword="true" />.</returns>
		// Token: 0x06000042 RID: 66 RVA: 0x00003610 File Offset: 0x00001810
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeFConverter" /> class.</summary>
		// Token: 0x06000043 RID: 67 RVA: 0x0000362F File Offset: 0x0000182F
		public SizeFConverter()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003914 File Offset: 0x00001B14
		// Note: this type is marked as 'beforefieldinit'.
		static SizeFConverter()
		{
		}

		// Token: 0x0400014C RID: 332
		private static readonly string[] s_propertySort = new string[]
		{
			"Width",
			"Height"
		};
	}
}
