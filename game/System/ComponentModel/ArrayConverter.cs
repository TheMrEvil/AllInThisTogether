using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Array" /> objects to and from various other representations.</summary>
	// Token: 0x0200037B RID: 891
	public class ArrayConverter : CollectionConverter
	{
		/// <summary>Converts the given value object to the specified destination type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The culture into which <paramref name="value" /> will be converted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06001D55 RID: 7509 RVA: 0x000688C8 File Offset: 0x00066AC8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Array)
			{
				return SR.Format("{0} Array", value.GetType().Name);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Gets a collection of properties for the type of array specified by the value parameter.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array to get the properties for.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for an array, or <see langword="null" /> if there are no properties.</returns>
		// Token: 0x06001D56 RID: 7510 RVA: 0x00068928 File Offset: 0x00066B28
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value == null)
			{
				return null;
			}
			PropertyDescriptor[] array = null;
			if (value.GetType().IsArray)
			{
				int length = ((Array)value).GetLength(0);
				array = new PropertyDescriptor[length];
				Type type = value.GetType();
				Type elementType = type.GetElementType();
				for (int i = 0; i < length; i++)
				{
					array[i] = new ArrayConverter.ArrayPropertyDescriptor(type, elementType, i);
				}
			}
			return new PropertyDescriptorCollection(array);
		}

		/// <summary>Gets a value indicating whether this object supports properties.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> because <see cref="M:System.ComponentModel.ArrayConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object. This method never returns <see langword="false" />.</returns>
		// Token: 0x06001D57 RID: 7511 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ArrayConverter" /> class.</summary>
		// Token: 0x06001D58 RID: 7512 RVA: 0x0006898E File Offset: 0x00066B8E
		public ArrayConverter()
		{
		}

		// Token: 0x0200037C RID: 892
		private class ArrayPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
		{
			// Token: 0x06001D59 RID: 7513 RVA: 0x00068996 File Offset: 0x00066B96
			public ArrayPropertyDescriptor(Type arrayType, Type elementType, int index) : base(arrayType, "[" + index.ToString() + "]", elementType, null)
			{
				this._index = index;
			}

			// Token: 0x06001D5A RID: 7514 RVA: 0x000689C0 File Offset: 0x00066BC0
			public override object GetValue(object instance)
			{
				Array array = instance as Array;
				if (array != null && array.GetLength(0) > this._index)
				{
					return array.GetValue(this._index);
				}
				return null;
			}

			// Token: 0x06001D5B RID: 7515 RVA: 0x000689F4 File Offset: 0x00066BF4
			public override void SetValue(object instance, object value)
			{
				if (instance is Array)
				{
					Array array = (Array)instance;
					if (array.GetLength(0) > this._index)
					{
						array.SetValue(value, this._index);
					}
					this.OnValueChanged(instance, EventArgs.Empty);
				}
			}

			// Token: 0x04000ED0 RID: 3792
			private readonly int _index;
		}
	}
}
