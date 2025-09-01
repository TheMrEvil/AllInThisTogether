using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace System.Drawing.Printing
{
	/// <summary>Provides a <see cref="T:System.Drawing.Printing.MarginsConverter" /> for <see cref="T:System.Drawing.Printing.Margins" />.</summary>
	// Token: 0x020000CC RID: 204
	public class MarginsConverter : ExpandableObjectConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.MarginsConverter" /> class.</summary>
		// Token: 0x06000AD3 RID: 2771 RVA: 0x00010D9A File Offset: 0x0000EF9A
		public MarginsConverter()
		{
		}

		/// <summary>Returns whether this converter can convert an object of the specified source type to the native type of the converter using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type from which you want to convert.</param>
		/// <returns>
		///   <see langword="true" /> if an object can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AD4 RID: 2772 RVA: 0x00003338 File Offset: 0x00001538
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Returns whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type to which you want to convert.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x0000BB98 File Offset: 0x00009D98
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified object to the converter's native type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides the language to convert to.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> does not contain values for all four margins. For example, "100,100,100,100" specifies 1 inch for the left, right, top, and bottom margins.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06000AD6 RID: 2774 RVA: 0x000187A4 File Offset: 0x000169A4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (value == null)
			{
				return new Margins();
			}
			string text = "( |\\t)*";
			text = text + ";" + text;
			Match match = new Regex(string.Concat(new string[]
			{
				"(?<left>\\d+)",
				text,
				"(?<right>\\d+)",
				text,
				"(?<top>\\d+)",
				text,
				"(?<bottom>\\d+)"
			})).Match(value as string);
			if (!match.Success)
			{
				throw new ArgumentException("value");
			}
			int left;
			int right;
			int top;
			int bottom;
			try
			{
				left = int.Parse(match.Groups["left"].Value);
				right = int.Parse(match.Groups["right"].Value);
				top = int.Parse(match.Groups["top"].Value);
				bottom = int.Parse(match.Groups["bottom"].Value);
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("value", innerException);
			}
			return new Margins(left, right, top, bottom);
		}

		/// <summary>Converts the given value object to the specified destination type using the specified context and arguments.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides the language to convert to.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to which to convert the value.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000188D8 File Offset: 0x00016AD8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is Margins)
			{
				Margins margins = value as Margins;
				return string.Format("{0}; {1}; {2}; {3}", new object[]
				{
					margins.Left,
					margins.Right,
					margins.Top,
					margins.Bottom
				});
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Margins)
			{
				Margins margins2 = (Margins)value;
				return new InstanceDescriptor(typeof(Margins).GetTypeInfo().GetConstructor(new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(int)
				}), new object[]
				{
					margins2.Left,
					margins2.Right,
					margins2.Top,
					margins2.Bottom
				});
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Returns whether changing a value on this object requires a call to the <see cref="M:System.Drawing.Printing.MarginsConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> method to create a new value, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> if changing a property on this object requires a call to <see cref="M:System.Drawing.Printing.MarginsConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> to create a new value; otherwise, <see langword="false" />. This method always returns <see langword="true" />.</returns>
		// Token: 0x06000AD8 RID: 2776 RVA: 0x00003610 File Offset: 0x00001810
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Creates an <see cref="T:System.Object" /> given a set of property values for the object.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the specified <see cref="T:System.Collections.IDictionary" />, or <see langword="null" /> if the object cannot be created.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyValues" /> is <see langword="null" />.</exception>
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00018A18 File Offset: 0x00016C18
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object result;
			try
			{
				result = new Margins
				{
					Left = int.Parse(propertyValues["Left"].ToString()),
					Right = int.Parse(propertyValues["Right"].ToString()),
					Top = int.Parse(propertyValues["Top"].ToString()),
					Bottom = int.Parse(propertyValues["Bottom"].ToString())
				};
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}
	}
}
