using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x020005FB RID: 1531
	internal class XmlListConverter : XmlBaseConverter
	{
		// Token: 0x06003EB1 RID: 16049 RVA: 0x0015ADD6 File Offset: 0x00158FD6
		protected XmlListConverter(XmlBaseConverter atomicConverter) : base(atomicConverter)
		{
			this.atomicConverter = atomicConverter;
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x0015ADE6 File Offset: 0x00158FE6
		protected XmlListConverter(XmlBaseConverter atomicConverter, Type clrTypeDefault) : base(atomicConverter, clrTypeDefault)
		{
			this.atomicConverter = atomicConverter;
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00156AE0 File Offset: 0x00154CE0
		protected XmlListConverter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x0015ADF7 File Offset: 0x00158FF7
		public static XmlValueConverter Create(XmlValueConverter atomicConverter)
		{
			if (atomicConverter == XmlUntypedConverter.Untyped)
			{
				return XmlUntypedConverter.UntypedList;
			}
			if (atomicConverter == XmlAnyConverter.Item)
			{
				return XmlAnyListConverter.ItemList;
			}
			if (atomicConverter == XmlAnyConverter.AnyAtomic)
			{
				return XmlAnyListConverter.AnyAtomicList;
			}
			return new XmlListConverter((XmlBaseConverter)atomicConverter);
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x0015AE2E File Offset: 0x0015902E
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0015AE5C File Offset: 0x0015905C
		protected override object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (!(value is IEnumerable) || !this.IsListType(destinationType))
			{
				throw this.CreateInvalidClrMappingException(type, destinationType);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return value;
				}
				return this.ListAsString((IEnumerable)value, nsResolver);
			}
			else
			{
				if (type == XmlBaseConverter.StringType)
				{
					value = this.StringAsList((string)value);
				}
				if (destinationType.IsArray)
				{
					Type elementType = destinationType.GetElementType();
					if (elementType == XmlBaseConverter.ObjectType)
					{
						return this.ToArray<object>(value, nsResolver);
					}
					if (type == destinationType)
					{
						return value;
					}
					if (elementType == XmlBaseConverter.BooleanType)
					{
						return this.ToArray<bool>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.ByteType)
					{
						return this.ToArray<byte>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.ByteArrayType)
					{
						return this.ToArray<byte[]>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DateTimeType)
					{
						return this.ToArray<DateTime>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DateTimeOffsetType)
					{
						return this.ToArray<DateTimeOffset>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DecimalType)
					{
						return this.ToArray<decimal>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DoubleType)
					{
						return this.ToArray<double>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int16Type)
					{
						return this.ToArray<short>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int32Type)
					{
						return this.ToArray<int>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int64Type)
					{
						return this.ToArray<long>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.SByteType)
					{
						return this.ToArray<sbyte>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.SingleType)
					{
						return this.ToArray<float>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.StringType)
					{
						return this.ToArray<string>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.TimeSpanType)
					{
						return this.ToArray<TimeSpan>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt16Type)
					{
						return this.ToArray<ushort>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt32Type)
					{
						return this.ToArray<uint>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt64Type)
					{
						return this.ToArray<ulong>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UriType)
					{
						return this.ToArray<Uri>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XmlAtomicValueType)
					{
						return this.ToArray<XmlAtomicValue>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XmlQualifiedNameType)
					{
						return this.ToArray<XmlQualifiedName>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XPathItemType)
					{
						return this.ToArray<XPathItem>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XPathNavigatorType)
					{
						return this.ToArray<XPathNavigator>(value, nsResolver);
					}
					throw this.CreateInvalidClrMappingException(type, destinationType);
				}
				else
				{
					if (type == base.DefaultClrType && type != XmlBaseConverter.ObjectArrayType)
					{
						return value;
					}
					return this.ToList(value, nsResolver);
				}
			}
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x0015B129 File Offset: 0x00159329
		private bool IsListType(Type type)
		{
			return type == XmlBaseConverter.IListType || type == XmlBaseConverter.ICollectionType || type == XmlBaseConverter.IEnumerableType || type == XmlBaseConverter.StringType || type.IsArray;
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x0015B168 File Offset: 0x00159368
		private T[] ToArray<T>(object list, IXmlNamespaceResolver nsResolver)
		{
			IList list2 = list as IList;
			if (list2 != null)
			{
				T[] array = new T[list2.Count];
				for (int i = 0; i < list2.Count; i++)
				{
					array[i] = (T)((object)this.atomicConverter.ChangeType(list2[i], typeof(T), nsResolver));
				}
				return array;
			}
			IEnumerable enumerable = list as IEnumerable;
			List<T> list3 = new List<T>();
			foreach (object value in enumerable)
			{
				list3.Add((T)((object)this.atomicConverter.ChangeType(value, typeof(T), nsResolver)));
			}
			return list3.ToArray();
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x0015B240 File Offset: 0x00159440
		private IList ToList(object list, IXmlNamespaceResolver nsResolver)
		{
			IList list2 = list as IList;
			if (list2 != null)
			{
				object[] array = new object[list2.Count];
				for (int i = 0; i < list2.Count; i++)
				{
					array[i] = this.atomicConverter.ChangeType(list2[i], XmlBaseConverter.ObjectType, nsResolver);
				}
				return array;
			}
			IEnumerable enumerable = list as IEnumerable;
			List<object> list3 = new List<object>();
			foreach (object value in enumerable)
			{
				list3.Add(this.atomicConverter.ChangeType(value, XmlBaseConverter.ObjectType, nsResolver));
			}
			return list3;
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x0015B2FC File Offset: 0x001594FC
		private List<string> StringAsList(string value)
		{
			return new List<string>(XmlConvert.SplitString(value));
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0015B30C File Offset: 0x0015950C
		private string ListAsString(IEnumerable list, IXmlNamespaceResolver nsResolver)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				if (obj != null)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(this.atomicConverter.ToString(obj, nsResolver));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x0015B388 File Offset: 0x00159588
		private new Exception CreateInvalidClrMappingException(Type sourceType, Type destinationType)
		{
			if (sourceType == destinationType)
			{
				return new InvalidCastException(Res.GetString("Xml type 'List of {0}' does not support Clr type '{1}'.", new object[]
				{
					base.XmlTypeName,
					sourceType.Name
				}));
			}
			return new InvalidCastException(Res.GetString("Xml type 'List of {0}' does not support a conversion from Clr type '{1}' to Clr type '{2}'.", new object[]
			{
				base.XmlTypeName,
				sourceType.Name,
				destinationType.Name
			}));
		}

		// Token: 0x04002C95 RID: 11413
		protected XmlValueConverter atomicConverter;
	}
}
