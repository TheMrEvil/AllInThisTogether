using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Provides automatic conversion between a nullable type and its underlying primitive type.</summary>
	// Token: 0x020003DD RID: 989
	public class NullableConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NullableConverter" /> class.</summary>
		/// <param name="type">The specified nullable type.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a nullable type.</exception>
		// Token: 0x06002035 RID: 8245 RVA: 0x0006F960 File Offset: 0x0006DB60
		public NullableConverter(Type type)
		{
			this.NullableType = type;
			this.UnderlyingType = Nullable.GetUnderlyingType(type);
			if (this.UnderlyingType == null)
			{
				throw new ArgumentException("The specified type is not a nullable type.", "type");
			}
			this.UnderlyingTypeConverter = TypeDescriptor.GetConverter(this.UnderlyingType);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002036 RID: 8246 RVA: 0x0006F9B5 File Offset: 0x0006DBB5
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == this.UnderlyingType)
			{
				return true;
			}
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.CanConvertFrom(context, sourceType);
			}
			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06002037 RID: 8247 RVA: 0x0006F9E8 File Offset: 0x0006DBE8
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value.GetType() == this.UnderlyingType)
			{
				return value;
			}
			if (value is string && string.IsNullOrEmpty(value as string))
			{
				return null;
			}
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002038 RID: 8248 RVA: 0x0006FA44 File Offset: 0x0006DC44
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == this.UnderlyingType)
			{
				return true;
			}
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.CanConvertTo(context, destinationType);
			}
			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06002039 RID: 8249 RVA: 0x0006FA74 File Offset: 0x0006DC74
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == this.UnderlyingType && value != null && this.NullableType.IsInstanceOfType(value))
			{
				return value;
			}
			if (value == null)
			{
				if (destinationType == typeof(string))
				{
					return string.Empty;
				}
			}
			else if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.ConvertTo(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or <see langword="null" /> if the object cannot be created. This method always returns <see langword="null" />.</returns>
		// Token: 0x0600203A RID: 8250 RVA: 0x0006FAF8 File Offset: 0x0006DCF8
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.CreateInstance(context, propertyValues);
			}
			return base.CreateInstance(context, propertyValues);
		}

		/// <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600203B RID: 8251 RVA: 0x0006FB18 File Offset: 0x0006DD18
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or <see langword="null" /> if there are no properties.</returns>
		// Token: 0x0600203C RID: 8252 RVA: 0x0006FB38 File Offset: 0x0006DD38
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetProperties(context, value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <summary>Returns whether this object supports properties, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600203D RID: 8253 RVA: 0x0006FB67 File Offset: 0x0006DD67
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetPropertiesSupported(context);
			}
			return base.GetPropertiesSupported(context);
		}

		/// <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or <see langword="null" /> if the data type does not support a standard set of values.</returns>
		// Token: 0x0600203E RID: 8254 RVA: 0x0006FB88 File Offset: 0x0006DD88
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				TypeConverter.StandardValuesCollection standardValues = this.UnderlyingTypeConverter.GetStandardValues(context);
				if (this.GetStandardValuesSupported(context) && standardValues != null)
				{
					object[] array = new object[standardValues.Count + 1];
					int num = 0;
					array[num++] = null;
					foreach (object obj in standardValues)
					{
						array[num++] = obj;
					}
					return new TypeConverter.StandardValuesCollection(array);
				}
			}
			return base.GetStandardValues(context);
		}

		/// <summary>Returns whether the collection of standard values returned from <see cref="Overload:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of possible values, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; <see langword="false" /> if other values are possible.</returns>
		// Token: 0x0600203F RID: 8255 RVA: 0x0006FC24 File Offset: 0x0006DE24
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetStandardValuesExclusive(context);
			}
			return base.GetStandardValuesExclusive(context);
		}

		/// <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002040 RID: 8256 RVA: 0x0006FC42 File Offset: 0x0006DE42
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetStandardValuesSupported(context);
			}
			return base.GetStandardValuesSupported(context);
		}

		/// <summary>Returns whether the given value object is valid for this type and for the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity.</param>
		/// <returns>
		///   <see langword="true" /> if the specified value is valid for this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002041 RID: 8257 RVA: 0x0006FC60 File Offset: 0x0006DE60
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return value == null || this.UnderlyingTypeConverter.IsValid(context, value);
			}
			return base.IsValid(context, value);
		}

		/// <summary>Gets the nullable type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the nullable type.</returns>
		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x0006FC92 File Offset: 0x0006DE92
		public Type NullableType
		{
			[CompilerGenerated]
			get
			{
				return this.<NullableType>k__BackingField;
			}
		}

		/// <summary>Gets the underlying type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the underlying type.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x0006FC9A File Offset: 0x0006DE9A
		public Type UnderlyingType
		{
			[CompilerGenerated]
			get
			{
				return this.<UnderlyingType>k__BackingField;
			}
		}

		/// <summary>Gets the underlying type converter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that represents the underlying type converter.</returns>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x0006FCA2 File Offset: 0x0006DEA2
		public TypeConverter UnderlyingTypeConverter
		{
			[CompilerGenerated]
			get
			{
				return this.<UnderlyingTypeConverter>k__BackingField;
			}
		}

		// Token: 0x04000FB8 RID: 4024
		[CompilerGenerated]
		private readonly Type <NullableType>k__BackingField;

		// Token: 0x04000FB9 RID: 4025
		[CompilerGenerated]
		private readonly Type <UnderlyingType>k__BackingField;

		// Token: 0x04000FBA RID: 4026
		[CompilerGenerated]
		private readonly TypeConverter <UnderlyingTypeConverter>k__BackingField;
	}
}
