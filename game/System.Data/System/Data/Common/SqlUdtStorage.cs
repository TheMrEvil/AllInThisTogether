using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x020003BD RID: 957
	internal sealed class SqlUdtStorage : DataStorage
	{
		// Token: 0x06002E84 RID: 11908 RVA: 0x000C7517 File Offset: 0x000C5717
		public SqlUdtStorage(DataColumn column, Type type) : this(column, type, SqlUdtStorage.GetStaticNullForUdtType(type))
		{
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000C7528 File Offset: 0x000C5728
		private SqlUdtStorage(DataColumn column, Type type, object nullValue) : base(column, type, nullValue, nullValue, typeof(ICloneable).IsAssignableFrom(type), DataStorage.GetStorageType(type))
		{
			this._implementsIXmlSerializable = typeof(IXmlSerializable).IsAssignableFrom(type);
			this._implementsIComparable = typeof(IComparable).IsAssignableFrom(type);
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000C7584 File Offset: 0x000C5784
		internal static object GetStaticNullForUdtType(Type type)
		{
			return SqlUdtStorage.s_typeToNull.GetOrAdd(type, delegate(Type t)
			{
				PropertyInfo property = type.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property != null)
				{
					return property.GetValue(null, null);
				}
				FieldInfo field = type.GetField("Null", BindingFlags.Static | BindingFlags.Public);
				if (field != null)
				{
					return field.GetValue(null);
				}
				throw ExceptionBuilder.INullableUDTwithoutStaticNull(type.AssemblyQualifiedName);
			});
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000C75BA File Offset: 0x000C57BA
		public override bool IsNull(int record)
		{
			return ((INullable)this._values[record]).IsNull;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000B07A2 File Offset: 0x000AE9A2
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this._dataType);
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000C75CE File Offset: 0x000C57CE
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.CompareValueTo(recordNo1, this._values[recordNo2]);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000C75E0 File Offset: 0x000C57E0
		public override int CompareValueTo(int recordNo1, object value)
		{
			if (DBNull.Value == value)
			{
				value = this._nullValue;
			}
			if (this._implementsIComparable)
			{
				return ((IComparable)this._values[recordNo1]).CompareTo(value);
			}
			if (this._nullValue != value)
			{
				throw ExceptionBuilder.IComparableNotImplemented(this._dataType.AssemblyQualifiedName);
			}
			if (!((INullable)this._values[recordNo1]).IsNull)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000C764A File Offset: 0x000C584A
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this._values[recordNo2] = this._values[recordNo1];
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000C7664 File Offset: 0x000C5864
		public override object Get(int recordNo)
		{
			return this._values[recordNo];
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000C7670 File Offset: 0x000C5870
		public override void Set(int recordNo, object value)
		{
			if (DBNull.Value == value)
			{
				this._values[recordNo] = this._nullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			if (value == null)
			{
				if (this._isValueType)
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this._values[recordNo] = this._nullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			else
			{
				if (!this._dataType.IsInstanceOfType(value))
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this._values[recordNo] = value;
				base.SetNullBit(recordNo, false);
				return;
			}
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000C76EC File Offset: 0x000C58EC
		public override void SetCapacity(int capacity)
		{
			object[] array = new object[capacity];
			if (this._values != null)
			{
				Array.Copy(this._values, 0, array, 0, Math.Min(capacity, this._values.Length));
			}
			this._values = array;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000C7734 File Offset: 0x000C5934
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(string s)
		{
			if (this._implementsIXmlSerializable)
			{
				object obj = Activator.CreateInstance(this._dataType, true);
				using (XmlTextReader xmlTextReader = new XmlTextReader(new StringReader("<col>" + s + "</col>")))
				{
					((IXmlSerializable)obj).ReadXml(xmlTextReader);
				}
				return obj;
			}
			StringReader textReader = new StringReader(s);
			return ObjectStorage.GetXmlSerializer(this._dataType).Deserialize(textReader);
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000C77B4 File Offset: 0x000C59B4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				string text = xmlReader.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata");
				if (text == null)
				{
					string attribute = xmlReader.GetAttribute("InstanceType", "http://www.w3.org/2001/XMLSchema-instance");
					if (attribute != null)
					{
						text = XSDSchema.XsdtoClr(attribute).FullName;
					}
				}
				object obj = Activator.CreateInstance((text == null) ? this._dataType : Type.GetType(text), true);
				((IXmlSerializable)obj).ReadXml(xmlReader);
				return obj;
			}
			return ObjectStorage.GetXmlSerializer(this._dataType, xmlAttrib).Deserialize(xmlReader);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000C7830 File Offset: 0x000C5A30
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			if (this._implementsIXmlSerializable)
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					((IXmlSerializable)value).WriteXml(xmlTextWriter);
					goto IL_45;
				}
			}
			ObjectStorage.GetXmlSerializer(value.GetType()).Serialize(stringWriter, value);
			IL_45:
			return stringWriter.ToString();
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000C7898 File Offset: 0x000C5A98
		public override void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				((IXmlSerializable)value).WriteXml(xmlWriter);
				return;
			}
			ObjectStorage.GetXmlSerializer(this._dataType, xmlAttrib).Serialize(xmlWriter, value);
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000B11A5 File Offset: 0x000AF3A5
		protected override object GetEmptyStorage(int recordCount)
		{
			return new object[recordCount];
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000C78BD File Offset: 0x000C5ABD
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			((object[])store)[storeIndex] = this._values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000C78DF File Offset: 0x000C5ADF
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this._values = (object[])store;
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000C78ED File Offset: 0x000C5AED
		// Note: this type is marked as 'beforefieldinit'.
		static SqlUdtStorage()
		{
		}

		// Token: 0x04001BDC RID: 7132
		private object[] _values;

		// Token: 0x04001BDD RID: 7133
		private readonly bool _implementsIXmlSerializable;

		// Token: 0x04001BDE RID: 7134
		private readonly bool _implementsIComparable;

		// Token: 0x04001BDF RID: 7135
		private static readonly ConcurrentDictionary<Type, object> s_typeToNull = new ConcurrentDictionary<Type, object>();

		// Token: 0x020003BE RID: 958
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06002E97 RID: 11927 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06002E98 RID: 11928 RVA: 0x000C78FC File Offset: 0x000C5AFC
			internal object <GetStaticNullForUdtType>b__0(Type t)
			{
				PropertyInfo property = this.type.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property != null)
				{
					return property.GetValue(null, null);
				}
				FieldInfo field = this.type.GetField("Null", BindingFlags.Static | BindingFlags.Public);
				if (field != null)
				{
					return field.GetValue(null);
				}
				throw ExceptionBuilder.INullableUDTwithoutStaticNull(this.type.AssemblyQualifiedName);
			}

			// Token: 0x04001BE0 RID: 7136
			public Type type;
		}
	}
}
