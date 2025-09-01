using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Drawing
{
	/// <summary>The <see cref="T:System.Drawing.SizeConverter" /> class is used to convert from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	// Token: 0x02000013 RID: 19
	public class SizeConverter : TypeConverter
	{
		/// <summary>Determines whether this converter can convert an object in the specified source type to the native type of the converter.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="sourceType">The type you want to convert from.</param>
		/// <returns>This method returns <see langword="true" /> if this object can perform the conversion.</returns>
		// Token: 0x06000031 RID: 49 RVA: 0x00003338 File Offset: 0x00001538
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This can be <see langword="null" />, so always check. Also, properties on the context object can return <see langword="null" />.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		/// <returns>This method returns <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000032 RID: 50 RVA: 0x00003356 File Offset: 0x00001556
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to the converter's native type.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard.</param>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted object.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		// Token: 0x06000033 RID: 51 RVA: 0x00003374 File Offset: 0x00001574
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
			int[] array2 = new int[array.Length];
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = (int)converter.ConvertFromString(context, culture, array[i]);
			}
			if (array2.Length == 2)
			{
				return new Size(array2[0], array2[1]);
			}
			throw new ArgumentException(SR.Format("Text \"{0}\" cannot be parsed. The expected text format is \"{1}\".", new object[]
			{
				text2,
				"Width,Height"
			}));
		}

		/// <summary>Converts the specified object to the specified type.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard.</param>
		/// <param name="value">The object to convert.</param>
		/// <param name="destinationType">The type to convert the object to.</param>
		/// <returns>The converted object.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
		// Token: 0x06000034 RID: 52 RVA: 0x00003450 File Offset: 0x00001650
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (value is Size)
			{
				if (destinationType == typeof(string))
				{
					Size size = (Size)value;
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					string separator = culture.TextInfo.ListSeparator + " ";
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
					string[] array = new string[2];
					int num = 0;
					array[num++] = converter.ConvertToString(context, culture, size.Width);
					array[num++] = converter.ConvertToString(context, culture, size.Height);
					return string.Join(separator, array);
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					Size size2 = (Size)value;
					ConstructorInfo constructor = typeof(Size).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							size2.Width,
							size2.Height
						});
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates an object of this type by using a specified set of property values for the object. This is useful for creating non-changeable objects that have changeable properties.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
		/// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from the <see cref="M:System.Drawing.SizeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method.</param>
		/// <returns>The newly created object, or <see langword="null" /> if the object could not be created. The default implementation returns <see langword="null" />.</returns>
		// Token: 0x06000035 RID: 53 RVA: 0x0000359C File Offset: 0x0000179C
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			object obj = propertyValues["Width"];
			object obj2 = propertyValues["Height"];
			if (obj == null || obj2 == null || !(obj is int) || !(obj2 is int))
			{
				throw new ArgumentException(SR.Format("IDictionary parameter contains at least one entry that is not valid. Ensure all values are consistent with the object's properties.", Array.Empty<object>()));
			}
			return new Size((int)obj, (int)obj2);
		}

		/// <summary>Determines whether changing a value on this object should require a call to the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> method to create a new value.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> object should be called when a change is made to one or more properties of this object.</returns>
		// Token: 0x06000036 RID: 54 RVA: 0x00003610 File Offset: 0x00001810
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Retrieves the set of properties for this type. By default, a type does not have any properties to return.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
		/// <param name="value">The value of the object to get the properties for.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
		/// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
		// Token: 0x06000037 RID: 55 RVA: 0x00003613 File Offset: 0x00001813
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(Size), attributes).Sort(SizeConverter.s_propertySort);
		}

		/// <summary>Determines whether this object supports properties. By default, this is <see langword="false" />.</summary>
		/// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="M:System.Drawing.SizeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method should be called to find the properties of this object.</returns>
		// Token: 0x06000038 RID: 56 RVA: 0x00003610 File Offset: 0x00001810
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.SizeConverter" /> object.</summary>
		// Token: 0x06000039 RID: 57 RVA: 0x0000362F File Offset: 0x0000182F
		public SizeConverter()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003637 File Offset: 0x00001837
		// Note: this type is marked as 'beforefieldinit'.
		static SizeConverter()
		{
		}

		// Token: 0x0400014B RID: 331
		private static readonly string[] s_propertySort = new string[]
		{
			"Width",
			"Height"
		};
	}
}
