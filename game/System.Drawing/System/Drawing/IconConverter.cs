using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace System.Drawing
{
	/// <summary>Converts an <see cref="T:System.Drawing.Icon" /> object from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	// Token: 0x0200007C RID: 124
	public class IconConverter : ExpandableObjectConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.IconConverter" /> class.</summary>
		// Token: 0x060005DB RID: 1499 RVA: 0x00010D9A File Offset: 0x0000EF9A
		public IconConverter()
		{
		}

		/// <summary>Determines whether this <see cref="T:System.Drawing.IconConverter" /> can convert an instance of a specified type to an <see cref="T:System.Drawing.Icon" />, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that specifies the type you want to convert from.</param>
		/// <returns>This method returns <see langword="true" /> if this <see cref="T:System.Drawing.IconConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x060005DC RID: 1500 RVA: 0x00010DA2 File Offset: 0x0000EFA2
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(byte[]);
		}

		/// <summary>Determines whether this <see cref="T:System.Drawing.IconConverter" /> can convert an <see cref="T:System.Drawing.Icon" /> to an instance of a specified type, using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that specifies the type you want to convert to.</param>
		/// <returns>This method returns <see langword="true" /> if this <see cref="T:System.Drawing.IconConverter" /> can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x060005DD RID: 1501 RVA: 0x00010DB9 File Offset: 0x0000EFB9
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(byte[]) || destinationType == typeof(string);
		}

		/// <summary>Converts a specified object to an <see cref="T:System.Drawing.Icon" />.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that holds information about a specific culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to be converted.</param>
		/// <returns>If this method succeeds, it returns the <see cref="T:System.Drawing.Icon" /> that it created by converting the specified object. Otherwise, it throws an exception.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
		// Token: 0x060005DE RID: 1502 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			byte[] array = value as byte[];
			if (array == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			return new Icon(new MemoryStream(array));
		}

		/// <summary>Converts an <see cref="T:System.Drawing.Icon" /> (or an object that can be cast to an <see cref="T:System.Drawing.Icon" />) to a specified type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies formatting conventions used by a particular culture.</param>
		/// <param name="value">The object to convert. This object should be of type icon or some type that can be cast to <see cref="T:System.Drawing.Icon" />.</param>
		/// <param name="destinationType">The type to convert the icon to.</param>
		/// <returns>This method returns the converted object.</returns>
		/// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
		// Token: 0x060005DF RID: 1503 RVA: 0x00010E10 File Offset: 0x0000F010
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value is Icon && destinationType == typeof(string))
			{
				return value.ToString();
			}
			if (value == null && destinationType == typeof(string))
			{
				return "(none)";
			}
			if (this.CanConvertTo(null, destinationType))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					((Icon)value).Save(memoryStream);
					return memoryStream.ToArray();
				}
			}
			string str = "IconConverter can not convert from ";
			Type type = value.GetType();
			return new NotSupportedException(str + ((type != null) ? type.ToString() : null));
		}
	}
}
