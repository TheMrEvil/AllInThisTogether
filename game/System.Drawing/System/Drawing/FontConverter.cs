using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Text;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Drawing
{
	/// <summary>Converts <see cref="T:System.Drawing.Font" /> objects from one data type to another.</summary>
	// Token: 0x0200006D RID: 109
	public class FontConverter : TypeConverter
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.FontConverter" /> object.</summary>
		// Token: 0x0600047A RID: 1146 RVA: 0x0000362F File Offset: 0x0000182F
		public FontConverter()
		{
		}

		/// <summary>Allows the <see cref="T:System.Drawing.FontConverter" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Drawing.FontConverter" /> is reclaimed by garbage collection.</summary>
		// Token: 0x0600047B RID: 1147 RVA: 0x0000BB70 File Offset: 0x00009D70
		~FontConverter()
		{
		}

		/// <summary>Determines whether this converter can convert an object in the specified source type to the native type of the converter.</summary>
		/// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="sourceType">The type you want to convert from.</param>
		/// <returns>This method returns <see langword="true" /> if this object can perform the conversion.</returns>
		// Token: 0x0600047C RID: 1148 RVA: 0x00003338 File Offset: 0x00001538
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see langword="ITypeDescriptorContext" /> object that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> object that represents the type you want to convert to.</param>
		/// <returns>This method returns <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600047D RID: 1149 RVA: 0x0000BB98 File Offset: 0x00009D98
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to another type.</summary>
		/// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies the culture used to represent the object.</param>
		/// <param name="value">The object to convert.</param>
		/// <param name="destinationType">The data type to convert the object to.</param>
		/// <returns>The converted object.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion was not successful.</exception>
		// Token: 0x0600047E RID: 1150 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is Font)
			{
				Font font = (Font)value;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(font.Name).Append(culture.TextInfo.ListSeparator[0].ToString() + " ");
				stringBuilder.Append(font.Size);
				switch (font.Unit)
				{
				case GraphicsUnit.World:
					stringBuilder.Append("world");
					break;
				case GraphicsUnit.Display:
					stringBuilder.Append("display");
					break;
				case GraphicsUnit.Pixel:
					stringBuilder.Append("px");
					break;
				case GraphicsUnit.Point:
					stringBuilder.Append("pt");
					break;
				case GraphicsUnit.Inch:
					stringBuilder.Append("in");
					break;
				case GraphicsUnit.Document:
					stringBuilder.Append("doc");
					break;
				case GraphicsUnit.Millimeter:
					stringBuilder.Append("mm");
					break;
				}
				if (font.Style != FontStyle.Regular)
				{
					stringBuilder.Append(culture.TextInfo.ListSeparator[0].ToString() + " style=").Append(font.Style);
				}
				return stringBuilder.ToString();
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Font)
			{
				Font font2 = (Font)value;
				return new InstanceDescriptor(typeof(Font).GetTypeInfo().GetConstructor(new Type[]
				{
					typeof(string),
					typeof(float),
					typeof(FontStyle),
					typeof(GraphicsUnit)
				}), new object[]
				{
					font2.Name,
					font2.Size,
					font2.Style,
					font2.Unit
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Converts the specified object to the native type of the converter.</summary>
		/// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />.</param>
		/// <param name="culture">A <see langword="CultureInfo" /> object that specifies the culture used to represent the font.</param>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted object.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
		// Token: 0x0600047F RID: 1151 RVA: 0x0000BDEC File Offset: 0x00009FEC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			text = text.Trim();
			if (text.Length == 0)
			{
				return null;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string[] array = text.Split(new char[]
			{
				culture.TextInfo.ListSeparator[0]
			});
			if (array.Length < 1)
			{
				throw new ArgumentException("Failed to parse font format");
			}
			text = array[0];
			float emSize = 8f;
			string a = "px";
			GraphicsUnit unit = GraphicsUnit.Pixel;
			if (array.Length > 1)
			{
				for (int i = 0; i < array[1].Length; i++)
				{
					if (char.IsLetter(array[1][i]))
					{
						emSize = (float)TypeDescriptor.GetConverter(typeof(float)).ConvertFromString(context, culture, array[1].Substring(0, i));
						a = array[1].Substring(i);
						break;
					}
				}
				if (a == "display")
				{
					unit = GraphicsUnit.Display;
				}
				else if (a == "doc")
				{
					unit = GraphicsUnit.Document;
				}
				else if (a == "pt")
				{
					unit = GraphicsUnit.Point;
				}
				else if (a == "in")
				{
					unit = GraphicsUnit.Inch;
				}
				else if (a == "mm")
				{
					unit = GraphicsUnit.Millimeter;
				}
				else if (a == "px")
				{
					unit = GraphicsUnit.Pixel;
				}
				else if (a == "world")
				{
					unit = GraphicsUnit.World;
				}
			}
			FontStyle fontStyle = FontStyle.Regular;
			if (array.Length > 2)
			{
				for (int j = 2; j < array.Length; j++)
				{
					string text2 = array[j];
					if (text2.IndexOf("Regular") != -1)
					{
						fontStyle |= FontStyle.Regular;
					}
					if (text2.IndexOf("Bold") != -1)
					{
						fontStyle |= FontStyle.Bold;
					}
					if (text2.IndexOf("Italic") != -1)
					{
						fontStyle |= FontStyle.Italic;
					}
					if (text2.IndexOf("Strikeout") != -1)
					{
						fontStyle |= FontStyle.Strikeout;
					}
					if (text2.IndexOf("Underline") != -1)
					{
						fontStyle |= FontStyle.Underline;
					}
				}
			}
			return new Font(text, emSize, fontStyle, unit);
		}

		/// <summary>Creates an object of this type by using a specified set of property values for the object.</summary>
		/// <param name="context">A type descriptor through which additional context can be provided.</param>
		/// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from the <see cref="Overload:System.Drawing.FontConverter.GetProperties" /> method.</param>
		/// <returns>The newly created object, or <see langword="null" /> if the object could not be created. The default implementation returns <see langword="null" />.  
		///  <see cref="M:System.Drawing.FontConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> useful for creating non-changeable objects that have changeable properties.</returns>
		// Token: 0x06000480 RID: 1152 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			byte gdiCharSet = 1;
			float emSize = 8f;
			string text = null;
			bool gdiVerticalFont = false;
			FontStyle fontStyle = FontStyle.Regular;
			FontFamily fontFamily = null;
			GraphicsUnit unit = GraphicsUnit.Point;
			object obj;
			if ((obj = propertyValues["GdiCharSet"]) != null)
			{
				gdiCharSet = (byte)obj;
			}
			if ((obj = propertyValues["Size"]) != null)
			{
				emSize = (float)obj;
			}
			if ((obj = propertyValues["Unit"]) != null)
			{
				unit = (GraphicsUnit)obj;
			}
			if ((obj = propertyValues["Name"]) != null)
			{
				text = (string)obj;
			}
			if ((obj = propertyValues["GdiVerticalFont"]) != null)
			{
				gdiVerticalFont = (bool)obj;
			}
			if ((obj = propertyValues["Bold"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Bold;
			}
			if ((obj = propertyValues["Italic"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Italic;
			}
			if ((obj = propertyValues["Strikeout"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Strikeout;
			}
			if ((obj = propertyValues["Underline"]) != null && (bool)obj)
			{
				fontStyle |= FontStyle.Underline;
			}
			if (text == null)
			{
				fontFamily = new FontFamily("Tahoma");
			}
			else
			{
				text = text.ToLower();
				foreach (FontFamily fontFamily2 in new InstalledFontCollection().Families)
				{
					if (text == fontFamily2.Name.ToLower())
					{
						fontFamily = fontFamily2;
						break;
					}
				}
				if (fontFamily == null)
				{
					foreach (FontFamily fontFamily3 in new PrivateFontCollection().Families)
					{
						if (text == fontFamily3.Name.ToLower())
						{
							fontFamily = fontFamily3;
							break;
						}
					}
				}
				if (fontFamily == null)
				{
					fontFamily = FontFamily.GenericSansSerif;
				}
			}
			return new Font(fontFamily, emSize, fontStyle, unit, gdiCharSet, gdiVerticalFont);
		}

		/// <summary>Determines whether changing a value on this object should require a call to the <see cref="Overload:System.Drawing.FontConverter.CreateInstance" /> method to create a new value.</summary>
		/// <param name="context">A type descriptor through which additional context can be provided.</param>
		/// <returns>This method returns <see langword="true" /> if the <see langword="CreateInstance" /> object should be called when a change is made to one or more properties of this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000481 RID: 1153 RVA: 0x00003610 File Offset: 0x00001810
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Retrieves the set of properties for this type. By default, a type does not have any properties to return.</summary>
		/// <param name="context">A type descriptor through which additional context can be provided.</param>
		/// <param name="value">The value of the object to get the properties for.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
		/// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return <see langword="null" />. The default implementation always returns <see langword="null" />.  
		///  An easy implementation of this method can call the <see cref="Overload:System.ComponentModel.TypeConverter.GetProperties" /> method for the correct data type.</returns>
		// Token: 0x06000482 RID: 1154 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value is Font)
			{
				return TypeDescriptor.GetProperties(value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <summary>Determines whether this object supports properties. The default is <see langword="false" />.</summary>
		/// <param name="context">A type descriptor through which additional context can be provided.</param>
		/// <returns>This method returns <see langword="true" /> if the <see cref="M:System.Drawing.FontConverter.GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext)" /> method should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000483 RID: 1155 RVA: 0x00003610 File Offset: 0x00001810
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>
		///   <see cref="T:System.Drawing.FontConverter.FontNameConverter" /> is a type converter that is used to convert a font name to and from various other representations.</summary>
		// Token: 0x0200006E RID: 110
		public sealed class FontNameConverter : TypeConverter, IDisposable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.FontConverter.FontNameConverter" /> class.</summary>
			// Token: 0x06000484 RID: 1156 RVA: 0x0000C1BB File Offset: 0x0000A3BB
			public FontNameConverter()
			{
				this.fonts = FontFamily.Families;
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000485 RID: 1157 RVA: 0x000049FE File Offset: 0x00002BFE
			void IDisposable.Dispose()
			{
			}

			/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
			/// <param name="sourceType">The type you wish to convert from.</param>
			/// <returns>
			///   <see langword="true" /> if the converter can perform the conversion; otherwise, <see langword="false" />.</returns>
			// Token: 0x06000486 RID: 1158 RVA: 0x00003338 File Offset: 0x00001538
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			/// <summary>Converts the given object to the converter's native type.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
			/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to use to perform the conversion</param>
			/// <param name="value">The object to convert.</param>
			/// <returns>The converted object.</returns>
			/// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
			// Token: 0x06000487 RID: 1159 RVA: 0x0000C1CE File Offset: 0x0000A3CE
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					return value;
				}
				return base.ConvertFrom(context, culture, value);
			}

			/// <summary>Retrieves a collection containing a set of standard values for the data type this converter is designed for.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
			/// <returns>A collection containing a standard set of valid values, or <see langword="null" />. The default is <see langword="null" />.</returns>
			// Token: 0x06000488 RID: 1160 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				string[] array = new string[this.fonts.Length];
				int i = this.fonts.Length;
				while (i > 0)
				{
					i--;
					array[i] = this.fonts[i].Name;
				}
				return new TypeConverter.StandardValuesCollection(array);
			}

			/// <summary>Determines if the list of standard values returned from the <see cref="Overload:System.Drawing.FontConverter.FontNameConverter.GetStandardValues" /> method is an exclusive list.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
			/// <returns>
			///   <see langword="true" /> if the collection returned from <see cref="Overload:System.Drawing.FontConverter.FontNameConverter.GetStandardValues" /> is an exclusive list of possible values; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
			// Token: 0x06000489 RID: 1161 RVA: 0x0000C228 File Offset: 0x0000A428
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			/// <summary>Determines if this object supports a standard set of values that can be picked from a list.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to extract additional information about the environment this converter is being invoked from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may return <see langword="null" />.</param>
			/// <returns>
			///   <see langword="true" /> if <see cref="Overload:System.Drawing.FontConverter.FontNameConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600048A RID: 1162 RVA: 0x00003610 File Offset: 0x00001810
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x04000491 RID: 1169
			private FontFamily[] fonts;
		}

		/// <summary>Converts font units to and from other unit types.</summary>
		// Token: 0x0200006F RID: 111
		public class FontUnitConverter : EnumConverter
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.FontConverter.FontUnitConverter" /> class.</summary>
			// Token: 0x0600048B RID: 1163 RVA: 0x0000C22B File Offset: 0x0000A42B
			public FontUnitConverter() : base(typeof(GraphicsUnit))
			{
			}

			/// <summary>Returns a collection of standard values valid for the <see cref="T:System.Drawing.Font" /> type.</summary>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
			// Token: 0x0600048C RID: 1164 RVA: 0x0000C23D File Offset: 0x0000A43D
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				return base.GetStandardValues(context);
			}
		}
	}
}
