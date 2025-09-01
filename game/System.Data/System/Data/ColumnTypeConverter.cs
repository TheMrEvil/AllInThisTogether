using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection;

namespace System.Data
{
	// Token: 0x020000A3 RID: 163
	internal sealed class ColumnTypeConverter : TypeConverter
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x0002AFBB File Offset: 0x000291BB
		public ColumnTypeConverter()
		{
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002AFC3 File Offset: 0x000291C3
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002AFE4 File Offset: 0x000291E4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				if (value != null && destinationType == typeof(InstanceDescriptor))
				{
					object obj = value;
					if (value is string)
					{
						for (int i = 0; i < ColumnTypeConverter.s_types.Length; i++)
						{
							if (ColumnTypeConverter.s_types[i].ToString().Equals(value))
							{
								obj = ColumnTypeConverter.s_types[i];
							}
						}
					}
					if (value is Type || value is string)
					{
						MethodInfo method = typeof(Type).GetMethod("GetType", new Type[]
						{
							typeof(string)
						});
						if (method != null)
						{
							return new InstanceDescriptor(method, new object[]
							{
								((Type)obj).AssemblyQualifiedName
							});
						}
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			return value.ToString();
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002B0E4 File Offset: 0x000292E4
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002B104 File Offset: 0x00029304
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value.GetType() == typeof(string))
			{
				for (int i = 0; i < ColumnTypeConverter.s_types.Length; i++)
				{
					if (ColumnTypeConverter.s_types[i].ToString().Equals(value))
					{
						return ColumnTypeConverter.s_types[i];
					}
				}
				return typeof(string);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002B170 File Offset: 0x00029370
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this._values == null)
			{
				object[] array;
				if (ColumnTypeConverter.s_types != null)
				{
					array = new object[ColumnTypeConverter.s_types.Length];
					Array.Copy(ColumnTypeConverter.s_types, 0, array, 0, ColumnTypeConverter.s_types.Length);
				}
				else
				{
					array = null;
				}
				this._values = new TypeConverter.StandardValuesCollection(array);
			}
			return this._values;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00006D61 File Offset: 0x00004F61
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002B1C4 File Offset: 0x000293C4
		// Note: this type is marked as 'beforefieldinit'.
		static ColumnTypeConverter()
		{
		}

		// Token: 0x0400075F RID: 1887
		private static readonly Type[] s_types = new Type[]
		{
			typeof(bool),
			typeof(byte),
			typeof(byte[]),
			typeof(char),
			typeof(DateTime),
			typeof(decimal),
			typeof(double),
			typeof(Guid),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(object),
			typeof(sbyte),
			typeof(float),
			typeof(string),
			typeof(TimeSpan),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(SqlInt16),
			typeof(SqlInt32),
			typeof(SqlInt64),
			typeof(SqlDecimal),
			typeof(SqlSingle),
			typeof(SqlDouble),
			typeof(SqlString),
			typeof(SqlBoolean),
			typeof(SqlBinary),
			typeof(SqlByte),
			typeof(SqlDateTime),
			typeof(SqlGuid),
			typeof(SqlMoney),
			typeof(SqlBytes),
			typeof(SqlChars),
			typeof(SqlXml)
		};

		// Token: 0x04000760 RID: 1888
		private TypeConverter.StandardValuesCollection _values;
	}
}
