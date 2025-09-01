using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000364 RID: 868
	internal sealed class ObjectStorage : DataStorage
	{
		// Token: 0x060027B8 RID: 10168 RVA: 0x000B0762 File Offset: 0x000AE962
		internal ObjectStorage(DataColumn column, Type type) : base(column, type, ObjectStorage.s_defaultValue, DBNull.Value, typeof(ICloneable).IsAssignableFrom(type), DataStorage.GetStorageType(type))
		{
			this._implementsIXmlSerializable = typeof(IXmlSerializable).IsAssignableFrom(type);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x000B07A2 File Offset: 0x000AE9A2
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x000B07B0 File Offset: 0x000AE9B0
		public override int Compare(int recordNo1, int recordNo2)
		{
			object obj = this._values[recordNo1];
			object obj2 = this._values[recordNo2];
			if (obj == obj2)
			{
				return 0;
			}
			if (obj == null)
			{
				return -1;
			}
			if (obj2 == null)
			{
				return 1;
			}
			IComparable comparable = obj as IComparable;
			if (comparable != null)
			{
				try
				{
					return comparable.CompareTo(obj2);
				}
				catch (ArgumentException e)
				{
					ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				}
			}
			return this.CompareWithFamilies(obj, obj2);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000B0818 File Offset: 0x000AEA18
		public override int CompareValueTo(int recordNo1, object value)
		{
			object obj = this.Get(recordNo1);
			if (obj is IComparable && value.GetType() == obj.GetType())
			{
				return ((IComparable)obj).CompareTo(value);
			}
			if (obj == value)
			{
				return 0;
			}
			if (obj == null)
			{
				if (this._nullValue == value)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this._nullValue == value || value == null)
				{
					return 1;
				}
				return this.CompareWithFamilies(obj, value);
			}
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000B0884 File Offset: 0x000AEA84
		private int CompareTo(object valueNo1, object valueNo2)
		{
			if (valueNo1 == null)
			{
				return -1;
			}
			if (valueNo2 == null)
			{
				return 1;
			}
			if (valueNo1 == valueNo2)
			{
				return 0;
			}
			if (valueNo1 == this._nullValue)
			{
				return -1;
			}
			if (valueNo2 == this._nullValue)
			{
				return 1;
			}
			if (valueNo1 is IComparable)
			{
				try
				{
					return ((IComparable)valueNo1).CompareTo(valueNo2);
				}
				catch (ArgumentException e)
				{
					ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				}
			}
			return this.CompareWithFamilies(valueNo1, valueNo2);
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000B08F0 File Offset: 0x000AEAF0
		private int CompareWithFamilies(object valueNo1, object valueNo2)
		{
			ObjectStorage.Families family = this.GetFamily(valueNo1.GetType());
			ObjectStorage.Families family2 = this.GetFamily(valueNo2.GetType());
			if (family < family2)
			{
				return -1;
			}
			if (family > family2)
			{
				return 1;
			}
			switch (family)
			{
			case ObjectStorage.Families.DATETIME:
				valueNo1 = Convert.ToDateTime(valueNo1, base.FormatProvider);
				valueNo2 = Convert.ToDateTime(valueNo1, base.FormatProvider);
				goto IL_137;
			case ObjectStorage.Families.NUMBER:
				valueNo1 = Convert.ToDouble(valueNo1, base.FormatProvider);
				valueNo2 = Convert.ToDouble(valueNo2, base.FormatProvider);
				goto IL_137;
			case ObjectStorage.Families.BOOLEAN:
				valueNo1 = Convert.ToBoolean(valueNo1, base.FormatProvider);
				valueNo2 = Convert.ToBoolean(valueNo2, base.FormatProvider);
				goto IL_137;
			case ObjectStorage.Families.ARRAY:
			{
				Array array = (Array)valueNo1;
				Array array2 = (Array)valueNo2;
				if (array.Length > array2.Length)
				{
					return 1;
				}
				if (array.Length < array2.Length)
				{
					return -1;
				}
				for (int i = 0; i < array.Length; i++)
				{
					int num = this.CompareTo(array.GetValue(i), array2.GetValue(i));
					if (num != 0)
					{
						return num;
					}
				}
				return 0;
			}
			}
			valueNo1 = valueNo1.ToString();
			valueNo2 = valueNo2.ToString();
			IL_137:
			return ((IComparable)valueNo1).CompareTo(valueNo2);
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000B0A40 File Offset: 0x000AEC40
		public override void Copy(int recordNo1, int recordNo2)
		{
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000B0A54 File Offset: 0x000AEC54
		public override object Get(int recordNo)
		{
			object obj = this._values[recordNo];
			if (obj != null)
			{
				return obj;
			}
			return this._nullValue;
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000B0A78 File Offset: 0x000AEC78
		private ObjectStorage.Families GetFamily(Type dataType)
		{
			switch (Type.GetTypeCode(dataType))
			{
			case TypeCode.Boolean:
				return ObjectStorage.Families.BOOLEAN;
			case TypeCode.Char:
				return ObjectStorage.Families.STRING;
			case TypeCode.SByte:
				return ObjectStorage.Families.STRING;
			case TypeCode.Byte:
				return ObjectStorage.Families.STRING;
			case TypeCode.Int16:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.UInt16:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Int32:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.UInt32:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Int64:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.UInt64:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Single:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Double:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Decimal:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.DateTime:
				return ObjectStorage.Families.DATETIME;
			case TypeCode.String:
				return ObjectStorage.Families.STRING;
			}
			if (typeof(TimeSpan) == dataType)
			{
				return ObjectStorage.Families.DATETIME;
			}
			if (dataType.IsArray)
			{
				return ObjectStorage.Families.ARRAY;
			}
			return ObjectStorage.Families.STRING;
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000B0B13 File Offset: 0x000AED13
		public override bool IsNull(int record)
		{
			return this._values[record] == null;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000B0B20 File Offset: 0x000AED20
		public override void Set(int recordNo, object value)
		{
			if (this._nullValue == value)
			{
				this._values[recordNo] = null;
				return;
			}
			if (this._dataType == typeof(object) || this._dataType.IsInstanceOfType(value))
			{
				this._values[recordNo] = value;
				return;
			}
			Type type = value.GetType();
			if (this._dataType == typeof(Guid) && type == typeof(string))
			{
				this._values[recordNo] = new Guid((string)value);
				return;
			}
			if (!(this._dataType == typeof(byte[])))
			{
				throw ExceptionBuilder.StorageSetFailed();
			}
			if (type == typeof(bool))
			{
				this._values[recordNo] = BitConverter.GetBytes((bool)value);
				return;
			}
			if (type == typeof(char))
			{
				this._values[recordNo] = BitConverter.GetBytes((char)value);
				return;
			}
			if (type == typeof(short))
			{
				this._values[recordNo] = BitConverter.GetBytes((short)value);
				return;
			}
			if (type == typeof(int))
			{
				this._values[recordNo] = BitConverter.GetBytes((int)value);
				return;
			}
			if (type == typeof(long))
			{
				this._values[recordNo] = BitConverter.GetBytes((long)value);
				return;
			}
			if (type == typeof(ushort))
			{
				this._values[recordNo] = BitConverter.GetBytes((ushort)value);
				return;
			}
			if (type == typeof(uint))
			{
				this._values[recordNo] = BitConverter.GetBytes((uint)value);
				return;
			}
			if (type == typeof(ulong))
			{
				this._values[recordNo] = BitConverter.GetBytes((ulong)value);
				return;
			}
			if (type == typeof(float))
			{
				this._values[recordNo] = BitConverter.GetBytes((float)value);
				return;
			}
			if (type == typeof(double))
			{
				this._values[recordNo] = BitConverter.GetBytes((double)value);
				return;
			}
			throw ExceptionBuilder.StorageSetFailed();
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000B0D5C File Offset: 0x000AEF5C
		public override void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000B0D9C File Offset: 0x000AEF9C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(string s)
		{
			Type dataType = this._dataType;
			if (dataType == typeof(byte[]))
			{
				return Convert.FromBase64String(s);
			}
			if (dataType == typeof(Type))
			{
				return Type.GetType(s);
			}
			if (dataType == typeof(Guid))
			{
				return new Guid(s);
			}
			if (dataType == typeof(Uri))
			{
				return new Uri(s);
			}
			if (this._implementsIXmlSerializable)
			{
				object obj = Activator.CreateInstance(this._dataType, true);
				using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(s)))
				{
					((IXmlSerializable)obj).ReadXml(xmlTextReader);
				}
				return obj;
			}
			StringReader textReader = new StringReader(s);
			return ObjectStorage.GetXmlSerializer(dataType).Deserialize(textReader);
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000B0E78 File Offset: 0x000AF078
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			bool flag = false;
			bool flag2 = false;
			object obj;
			if (xmlAttrib == null)
			{
				Type type = null;
				string attribute = xmlReader.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata");
				if (attribute == null || attribute.Length == 0)
				{
					string text = xmlReader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
					if (text != null && text.Length > 0)
					{
						string[] array = text.Split(':', StringSplitOptions.None);
						if (array.Length == 2 && xmlReader.LookupNamespace(array[0]) == "http://www.w3.org/2001/XMLSchema")
						{
							text = array[1];
						}
						type = XSDSchema.XsdtoClr(text);
						flag = true;
					}
					else if (this._dataType == typeof(object))
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					obj = xmlReader.ReadString();
				}
				else if (attribute == "Type")
				{
					obj = Type.GetType(xmlReader.ReadString());
					xmlReader.Read();
				}
				else
				{
					if (null == type)
					{
						type = ((attribute == null) ? this._dataType : DataStorage.GetType(attribute));
					}
					if (type == typeof(char) || type == typeof(Guid))
					{
						flag = true;
					}
					if (type == typeof(object))
					{
						throw ExceptionBuilder.CanNotDeserializeObjectType();
					}
					TypeLimiter.EnsureTypeIsAllowed(type, null);
					if (!flag)
					{
						obj = Activator.CreateInstance(type, true);
						((IXmlSerializable)obj).ReadXml(xmlReader);
					}
					else
					{
						if (type == typeof(string) && xmlReader.NodeType == XmlNodeType.Element && xmlReader.IsEmptyElement)
						{
							obj = string.Empty;
						}
						else
						{
							obj = xmlReader.ReadString();
							if (type != typeof(byte[]))
							{
								obj = SqlConvert.ChangeTypeForXML(obj, type);
							}
							else
							{
								obj = Convert.FromBase64String(obj.ToString());
							}
						}
						xmlReader.Read();
					}
				}
			}
			else
			{
				obj = ObjectStorage.GetXmlSerializer(this._dataType, xmlAttrib).Deserialize(xmlReader);
			}
			return obj;
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000B1050 File Offset: 0x000AF250
		public override string ConvertObjectToXml(object value)
		{
			if (value == null || value == this._nullValue)
			{
				return string.Empty;
			}
			Type dataType = this._dataType;
			if (dataType == typeof(byte[]) || (dataType == typeof(object) && value is byte[]))
			{
				return Convert.ToBase64String((byte[])value);
			}
			if (dataType == typeof(Type) || (dataType == typeof(object) && value is Type))
			{
				return ((Type)value).AssemblyQualifiedName;
			}
			if (!DataStorage.IsTypeCustomType(value.GetType()))
			{
				return (string)SqlConvert.ChangeTypeForXML(value, typeof(string));
			}
			if (Type.GetTypeCode(value.GetType()) != TypeCode.Object)
			{
				return value.ToString();
			}
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			if (this._implementsIXmlSerializable)
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					((IXmlSerializable)value).WriteXml(xmlTextWriter);
				}
				return stringWriter.ToString();
			}
			ObjectStorage.GetXmlSerializer(value.GetType()).Serialize(stringWriter, value);
			return stringWriter.ToString();
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000B1180 File Offset: 0x000AF380
		public override void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				((IXmlSerializable)value).WriteXml(xmlWriter);
				return;
			}
			ObjectStorage.GetXmlSerializer(value.GetType(), xmlAttrib).Serialize(xmlWriter, value);
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000B11A5 File Offset: 0x000AF3A5
		protected override object GetEmptyStorage(int recordCount)
		{
			return new object[recordCount];
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000B11B0 File Offset: 0x000AF3B0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			object[] array = (object[])store;
			array[storeIndex] = this._values[record];
			bool flag = this.IsNull(record);
			nullbits.Set(storeIndex, flag);
			if (!flag && array[storeIndex] is DateTime)
			{
				DateTime dateTime = (DateTime)array[storeIndex];
				if (dateTime.Kind == DateTimeKind.Local)
				{
					array[storeIndex] = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Local);
				}
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000B121C File Offset: 0x000AF41C
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (object[])store;
			for (int i = 0; i < this._values.Length; i++)
			{
				if (this._values[i] is DateTime)
				{
					DateTime value = (DateTime)this._values[i];
					if (value.Kind == DateTimeKind.Local)
					{
						this._values[i] = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToLocalTime();
					}
				}
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000B128B File Offset: 0x000AF48B
		internal static void VerifyIDynamicMetaObjectProvider(Type type)
		{
			if (typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type) && !typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				throw ADP.InvalidOperation("DataSet will not serialize types that implement IDynamicMetaObjectProvider but do not also implement IXmlSerializable.");
			}
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000B12BC File Offset: 0x000AF4BC
		internal static XmlSerializer GetXmlSerializer(Type type)
		{
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return ObjectStorage.s_serializerFactory.CreateSerializer(type);
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000B12D0 File Offset: 0x000AF4D0
		internal static XmlSerializer GetXmlSerializer(Type type, XmlRootAttribute attribute)
		{
			XmlSerializer xmlSerializer = null;
			KeyValuePair<Type, XmlRootAttribute> key = new KeyValuePair<Type, XmlRootAttribute>(type, attribute);
			Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer> dictionary = ObjectStorage.s_tempAssemblyCache;
			if (dictionary == null || !dictionary.TryGetValue(key, out xmlSerializer))
			{
				object obj = ObjectStorage.s_tempAssemblyCacheLock;
				lock (obj)
				{
					dictionary = ObjectStorage.s_tempAssemblyCache;
					if (dictionary == null || !dictionary.TryGetValue(key, out xmlSerializer))
					{
						ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
						if (dictionary != null)
						{
							Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer> dictionary2 = new Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer>(1 + dictionary.Count, ObjectStorage.TempAssemblyComparer.s_default);
							foreach (KeyValuePair<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer> keyValuePair in dictionary)
							{
								dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
							}
							dictionary = dictionary2;
						}
						else
						{
							dictionary = new Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer>(ObjectStorage.TempAssemblyComparer.s_default);
						}
						key = new KeyValuePair<Type, XmlRootAttribute>(type, new XmlRootAttribute());
						key.Value.ElementName = attribute.ElementName;
						key.Value.Namespace = attribute.Namespace;
						key.Value.DataType = attribute.DataType;
						key.Value.IsNullable = attribute.IsNullable;
						xmlSerializer = ObjectStorage.s_serializerFactory.CreateSerializer(type, attribute);
						dictionary.Add(key, xmlSerializer);
						ObjectStorage.s_tempAssemblyCache = dictionary;
					}
				}
			}
			return xmlSerializer;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000B1430 File Offset: 0x000AF630
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectStorage()
		{
		}

		// Token: 0x040019D2 RID: 6610
		private static readonly object s_defaultValue = null;

		// Token: 0x040019D3 RID: 6611
		private object[] _values;

		// Token: 0x040019D4 RID: 6612
		private readonly bool _implementsIXmlSerializable;

		// Token: 0x040019D5 RID: 6613
		private static readonly object s_tempAssemblyCacheLock = new object();

		// Token: 0x040019D6 RID: 6614
		private static Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer> s_tempAssemblyCache;

		// Token: 0x040019D7 RID: 6615
		private static readonly XmlSerializerFactory s_serializerFactory = new XmlSerializerFactory();

		// Token: 0x02000365 RID: 869
		private enum Families
		{
			// Token: 0x040019D9 RID: 6617
			DATETIME,
			// Token: 0x040019DA RID: 6618
			NUMBER,
			// Token: 0x040019DB RID: 6619
			STRING,
			// Token: 0x040019DC RID: 6620
			BOOLEAN,
			// Token: 0x040019DD RID: 6621
			ARRAY
		}

		// Token: 0x02000366 RID: 870
		private class TempAssemblyComparer : IEqualityComparer<KeyValuePair<Type, XmlRootAttribute>>
		{
			// Token: 0x060027CF RID: 10191 RVA: 0x00003D93 File Offset: 0x00001F93
			private TempAssemblyComparer()
			{
			}

			// Token: 0x060027D0 RID: 10192 RVA: 0x000B144C File Offset: 0x000AF64C
			public bool Equals(KeyValuePair<Type, XmlRootAttribute> x, KeyValuePair<Type, XmlRootAttribute> y)
			{
				return x.Key == y.Key && ((x.Value == null && y.Value == null) || (x.Value != null && y.Value != null && x.Value.ElementName == y.Value.ElementName && x.Value.Namespace == y.Value.Namespace && x.Value.DataType == y.Value.DataType && x.Value.IsNullable == y.Value.IsNullable));
			}

			// Token: 0x060027D1 RID: 10193 RVA: 0x000B1518 File Offset: 0x000AF718
			public int GetHashCode(KeyValuePair<Type, XmlRootAttribute> obj)
			{
				return obj.Key.GetHashCode() + obj.Value.ElementName.GetHashCode();
			}

			// Token: 0x060027D2 RID: 10194 RVA: 0x000B1538 File Offset: 0x000AF738
			// Note: this type is marked as 'beforefieldinit'.
			static TempAssemblyComparer()
			{
			}

			// Token: 0x040019DE RID: 6622
			internal static readonly IEqualityComparer<KeyValuePair<Type, XmlRootAttribute>> s_default = new ObjectStorage.TempAssemblyComparer();
		}
	}
}
