using System;
using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Numerics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000384 RID: 900
	internal abstract class DataStorage
	{
		// Token: 0x06002A8D RID: 10893 RVA: 0x000B96D1 File Offset: 0x000B78D1
		protected DataStorage(DataColumn column, Type type, object defaultValue, StorageType storageType) : this(column, type, defaultValue, DBNull.Value, false, storageType)
		{
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000B96E4 File Offset: 0x000B78E4
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, StorageType storageType) : this(column, type, defaultValue, nullValue, false, storageType)
		{
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000B96F4 File Offset: 0x000B78F4
		protected DataStorage(DataColumn column, Type type, object defaultValue, object nullValue, bool isICloneable, StorageType storageType)
		{
			this._column = column;
			this._table = column.Table;
			this._dataType = type;
			this._storageTypeCode = storageType;
			this._defaultValue = defaultValue;
			this._nullValue = nullValue;
			this._isCloneable = isICloneable;
			this._isCustomDefinedType = DataStorage.IsTypeCustomType(this._storageTypeCode);
			this._isStringType = (StorageType.String == this._storageTypeCode || StorageType.SqlString == this._storageTypeCode);
			this._isValueType = DataStorage.DetermineIfValueType(this._storageTypeCode, type);
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x000B9780 File Offset: 0x000B7980
		internal DataSetDateTime DateTimeMode
		{
			get
			{
				return this._column.DateTimeMode;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x000B978D File Offset: 0x000B798D
		internal IFormatProvider FormatProvider
		{
			get
			{
				return this._table.FormatProvider;
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000B979A File Offset: 0x000B799A
		public virtual object Aggregate(int[] recordNos, AggregateType kind)
		{
			if (AggregateType.Count == kind)
			{
				return this.AggregateCount(recordNos);
			}
			return null;
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000B97AC File Offset: 0x000B79AC
		public object AggregateCount(int[] recordNos)
		{
			int num = 0;
			for (int i = 0; i < recordNos.Length; i++)
			{
				if (!this._dbNullBits.Get(recordNos[i]))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x000B97E4 File Offset: 0x000B79E4
		protected int CompareBits(int recordNo1, int recordNo2)
		{
			bool flag = this._dbNullBits.Get(recordNo1);
			bool flag2 = this._dbNullBits.Get(recordNo2);
			if (!(flag ^ flag2))
			{
				return 0;
			}
			if (flag)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06002A95 RID: 10901
		public abstract int Compare(int recordNo1, int recordNo2);

		// Token: 0x06002A96 RID: 10902
		public abstract int CompareValueTo(int recordNo1, object value);

		// Token: 0x06002A97 RID: 10903 RVA: 0x000056BA File Offset: 0x000038BA
		public virtual object ConvertValue(object value)
		{
			return value;
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000B9818 File Offset: 0x000B7A18
		protected void CopyBits(int srcRecordNo, int dstRecordNo)
		{
			this._dbNullBits.Set(dstRecordNo, this._dbNullBits.Get(srcRecordNo));
		}

		// Token: 0x06002A99 RID: 10905
		public abstract void Copy(int recordNo1, int recordNo2);

		// Token: 0x06002A9A RID: 10906
		public abstract object Get(int recordNo);

		// Token: 0x06002A9B RID: 10907 RVA: 0x000B9832 File Offset: 0x000B7A32
		protected object GetBits(int recordNo)
		{
			if (this._dbNullBits.Get(recordNo))
			{
				return this._nullValue;
			}
			return this._defaultValue;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000B984F File Offset: 0x000B7A4F
		public virtual int GetStringLength(int record)
		{
			return int.MaxValue;
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000B9856 File Offset: 0x000B7A56
		protected bool HasValue(int recordNo)
		{
			return !this._dbNullBits.Get(recordNo);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x000B9867 File Offset: 0x000B7A67
		public virtual bool IsNull(int recordNo)
		{
			return this._dbNullBits.Get(recordNo);
		}

		// Token: 0x06002A9F RID: 10911
		public abstract void Set(int recordNo, object value);

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000B9875 File Offset: 0x000B7A75
		protected void SetNullBit(int recordNo, bool flag)
		{
			this._dbNullBits.Set(recordNo, flag);
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000B9884 File Offset: 0x000B7A84
		public virtual void SetCapacity(int capacity)
		{
			if (this._dbNullBits == null)
			{
				this._dbNullBits = new BitArray(capacity);
				return;
			}
			this._dbNullBits.Length = capacity;
		}

		// Token: 0x06002AA2 RID: 10914
		public abstract object ConvertXmlToObject(string s);

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000B98A7 File Offset: 0x000B7AA7
		public virtual object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			return this.ConvertXmlToObject(xmlReader.Value);
		}

		// Token: 0x06002AA4 RID: 10916
		public abstract string ConvertObjectToXml(object value);

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000B98B5 File Offset: 0x000B7AB5
		public virtual void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			xmlWriter.WriteString(this.ConvertObjectToXml(value));
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000B98C4 File Offset: 0x000B7AC4
		public static DataStorage CreateStorage(DataColumn column, Type dataType, StorageType typeCode)
		{
			if (typeCode != StorageType.Empty || !(null != dataType))
			{
				switch (typeCode)
				{
				case StorageType.Empty:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.Empty);
				case StorageType.DBNull:
					throw ExceptionBuilder.InvalidStorageType(TypeCode.DBNull);
				case StorageType.Boolean:
					return new BooleanStorage(column);
				case StorageType.Char:
					return new CharStorage(column);
				case StorageType.SByte:
					return new SByteStorage(column);
				case StorageType.Byte:
					return new ByteStorage(column);
				case StorageType.Int16:
					return new Int16Storage(column);
				case StorageType.UInt16:
					return new UInt16Storage(column);
				case StorageType.Int32:
					return new Int32Storage(column);
				case StorageType.UInt32:
					return new UInt32Storage(column);
				case StorageType.Int64:
					return new Int64Storage(column);
				case StorageType.UInt64:
					return new UInt64Storage(column);
				case StorageType.Single:
					return new SingleStorage(column);
				case StorageType.Double:
					return new DoubleStorage(column);
				case StorageType.Decimal:
					return new DecimalStorage(column);
				case StorageType.DateTime:
					return new DateTimeStorage(column);
				case StorageType.TimeSpan:
					return new TimeSpanStorage(column);
				case StorageType.String:
					return new StringStorage(column);
				case StorageType.Guid:
					return new ObjectStorage(column, dataType);
				case StorageType.ByteArray:
					return new ObjectStorage(column, dataType);
				case StorageType.CharArray:
					return new ObjectStorage(column, dataType);
				case StorageType.Type:
					return new ObjectStorage(column, dataType);
				case StorageType.DateTimeOffset:
					return new DateTimeOffsetStorage(column);
				case StorageType.BigInteger:
					return new BigIntegerStorage(column);
				case StorageType.Uri:
					return new ObjectStorage(column, dataType);
				case StorageType.SqlBinary:
					return new SqlBinaryStorage(column);
				case StorageType.SqlBoolean:
					return new SqlBooleanStorage(column);
				case StorageType.SqlByte:
					return new SqlByteStorage(column);
				case StorageType.SqlBytes:
					return new SqlBytesStorage(column);
				case StorageType.SqlChars:
					return new SqlCharsStorage(column);
				case StorageType.SqlDateTime:
					return new SqlDateTimeStorage(column);
				case StorageType.SqlDecimal:
					return new SqlDecimalStorage(column);
				case StorageType.SqlDouble:
					return new SqlDoubleStorage(column);
				case StorageType.SqlGuid:
					return new SqlGuidStorage(column);
				case StorageType.SqlInt16:
					return new SqlInt16Storage(column);
				case StorageType.SqlInt32:
					return new SqlInt32Storage(column);
				case StorageType.SqlInt64:
					return new SqlInt64Storage(column);
				case StorageType.SqlMoney:
					return new SqlMoneyStorage(column);
				case StorageType.SqlSingle:
					return new SqlSingleStorage(column);
				case StorageType.SqlString:
					return new SqlStringStorage(column);
				}
				return new ObjectStorage(column, dataType);
			}
			if (typeof(INullable).IsAssignableFrom(dataType))
			{
				return new SqlUdtStorage(column, dataType);
			}
			return new ObjectStorage(column, dataType);
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000B9AD0 File Offset: 0x000B7CD0
		internal static StorageType GetStorageType(Type dataType)
		{
			for (int i = 0; i < DataStorage.s_storageClassType.Length; i++)
			{
				if (dataType == DataStorage.s_storageClassType[i])
				{
					return (StorageType)i;
				}
			}
			TypeCode typeCode = Type.GetTypeCode(dataType);
			if (TypeCode.Object != typeCode)
			{
				return (StorageType)typeCode;
			}
			return StorageType.Empty;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000B9B0E File Offset: 0x000B7D0E
		internal static Type GetTypeStorage(StorageType storageType)
		{
			return DataStorage.s_storageClassType[(int)storageType];
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000B9B17 File Offset: 0x000B7D17
		internal static bool IsTypeCustomType(Type type)
		{
			return DataStorage.IsTypeCustomType(DataStorage.GetStorageType(type));
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000B9B24 File Offset: 0x000B7D24
		internal static bool IsTypeCustomType(StorageType typeCode)
		{
			return StorageType.Object == typeCode || typeCode == StorageType.Empty || StorageType.CharArray == typeCode;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000B9B34 File Offset: 0x000B7D34
		internal static bool IsSqlType(StorageType storageType)
		{
			return StorageType.SqlBinary <= storageType;
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000B9B40 File Offset: 0x000B7D40
		public static bool IsSqlType(Type dataType)
		{
			for (int i = 26; i < DataStorage.s_storageClassType.Length; i++)
			{
				if (dataType == DataStorage.s_storageClassType[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000B9B74 File Offset: 0x000B7D74
		private static bool DetermineIfValueType(StorageType typeCode, Type dataType)
		{
			bool result;
			switch (typeCode)
			{
			case StorageType.Boolean:
			case StorageType.Char:
			case StorageType.SByte:
			case StorageType.Byte:
			case StorageType.Int16:
			case StorageType.UInt16:
			case StorageType.Int32:
			case StorageType.UInt32:
			case StorageType.Int64:
			case StorageType.UInt64:
			case StorageType.Single:
			case StorageType.Double:
			case StorageType.Decimal:
			case StorageType.DateTime:
			case StorageType.TimeSpan:
			case StorageType.Guid:
			case StorageType.DateTimeOffset:
			case StorageType.BigInteger:
			case StorageType.SqlBinary:
			case StorageType.SqlBoolean:
			case StorageType.SqlByte:
			case StorageType.SqlDateTime:
			case StorageType.SqlDecimal:
			case StorageType.SqlDouble:
			case StorageType.SqlGuid:
			case StorageType.SqlInt16:
			case StorageType.SqlInt32:
			case StorageType.SqlInt64:
			case StorageType.SqlMoney:
			case StorageType.SqlSingle:
			case StorageType.SqlString:
				result = true;
				break;
			case StorageType.String:
			case StorageType.ByteArray:
			case StorageType.CharArray:
			case StorageType.Type:
			case StorageType.Uri:
			case StorageType.SqlBytes:
			case StorageType.SqlChars:
				result = false;
				break;
			default:
				result = dataType.IsValueType;
				break;
			}
			return result;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000B9C34 File Offset: 0x000B7E34
		internal static void ImplementsInterfaces(StorageType typeCode, Type dataType, out bool sqlType, out bool nullable, out bool xmlSerializable, out bool changeTracking, out bool revertibleChangeTracking)
		{
			if (DataStorage.IsSqlType(typeCode))
			{
				sqlType = true;
				nullable = true;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = true;
				return;
			}
			if (typeCode != StorageType.Empty)
			{
				sqlType = false;
				nullable = false;
				changeTracking = false;
				revertibleChangeTracking = false;
				xmlSerializable = false;
				return;
			}
			Tuple<bool, bool, bool, bool> orAdd = DataStorage.s_typeImplementsInterface.GetOrAdd(dataType, DataStorage.s_inspectTypeForInterfaces);
			sqlType = false;
			nullable = orAdd.Item1;
			changeTracking = orAdd.Item2;
			revertibleChangeTracking = orAdd.Item3;
			xmlSerializable = orAdd.Item4;
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000B9CAC File Offset: 0x000B7EAC
		private static Tuple<bool, bool, bool, bool> InspectTypeForInterfaces(Type dataType)
		{
			return new Tuple<bool, bool, bool, bool>(typeof(INullable).IsAssignableFrom(dataType), typeof(IChangeTracking).IsAssignableFrom(dataType), typeof(IRevertibleChangeTracking).IsAssignableFrom(dataType), typeof(IXmlSerializable).IsAssignableFrom(dataType));
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000B9CFE File Offset: 0x000B7EFE
		internal static bool ImplementsINullableValue(StorageType typeCode, Type dataType)
		{
			return typeCode == StorageType.Empty && dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000B9D22 File Offset: 0x000B7F22
		public static bool IsObjectNull(object value)
		{
			return value == null || DBNull.Value == value || DataStorage.IsObjectSqlNull(value);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000B9D38 File Offset: 0x000B7F38
		public static bool IsObjectSqlNull(object value)
		{
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000B9D57 File Offset: 0x000B7F57
		internal object GetEmptyStorageInternal(int recordCount)
		{
			return this.GetEmptyStorage(recordCount);
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000B9D60 File Offset: 0x000B7F60
		internal void CopyValueInternal(int record, object store, BitArray nullbits, int storeIndex)
		{
			this.CopyValue(record, store, nullbits, storeIndex);
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000B9D6D File Offset: 0x000B7F6D
		internal void SetStorageInternal(object store, BitArray nullbits)
		{
			this.SetStorage(store, nullbits);
		}

		// Token: 0x06002AB6 RID: 10934
		protected abstract object GetEmptyStorage(int recordCount);

		// Token: 0x06002AB7 RID: 10935
		protected abstract void CopyValue(int record, object store, BitArray nullbits, int storeIndex);

		// Token: 0x06002AB8 RID: 10936
		protected abstract void SetStorage(object store, BitArray nullbits);

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000B9D77 File Offset: 0x000B7F77
		protected void SetNullStorage(BitArray nullbits)
		{
			this._dbNullBits = nullbits;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000B9D80 File Offset: 0x000B7F80
		internal static Type GetType(string value)
		{
			Type type = Type.GetType(value);
			if (null == type && "System.Numerics.BigInteger" == value)
			{
				type = typeof(BigInteger);
			}
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type;
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x000B9DBC File Offset: 0x000B7FBC
		internal static string GetQualifiedName(Type type)
		{
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x000B9DCC File Offset: 0x000B7FCC
		// Note: this type is marked as 'beforefieldinit'.
		static DataStorage()
		{
		}

		// Token: 0x04001B03 RID: 6915
		private static readonly Type[] s_storageClassType = new Type[]
		{
			null,
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(string),
			typeof(Guid),
			typeof(byte[]),
			typeof(char[]),
			typeof(Type),
			typeof(DateTimeOffset),
			typeof(BigInteger),
			typeof(Uri),
			typeof(SqlBinary),
			typeof(SqlBoolean),
			typeof(SqlByte),
			typeof(SqlBytes),
			typeof(SqlChars),
			typeof(SqlDateTime),
			typeof(SqlDecimal),
			typeof(SqlDouble),
			typeof(SqlGuid),
			typeof(SqlInt16),
			typeof(SqlInt32),
			typeof(SqlInt64),
			typeof(SqlMoney),
			typeof(SqlSingle),
			typeof(SqlString)
		};

		// Token: 0x04001B04 RID: 6916
		internal readonly DataColumn _column;

		// Token: 0x04001B05 RID: 6917
		internal readonly DataTable _table;

		// Token: 0x04001B06 RID: 6918
		internal readonly Type _dataType;

		// Token: 0x04001B07 RID: 6919
		internal readonly StorageType _storageTypeCode;

		// Token: 0x04001B08 RID: 6920
		private BitArray _dbNullBits;

		// Token: 0x04001B09 RID: 6921
		private readonly object _defaultValue;

		// Token: 0x04001B0A RID: 6922
		internal readonly object _nullValue;

		// Token: 0x04001B0B RID: 6923
		internal readonly bool _isCloneable;

		// Token: 0x04001B0C RID: 6924
		internal readonly bool _isCustomDefinedType;

		// Token: 0x04001B0D RID: 6925
		internal readonly bool _isStringType;

		// Token: 0x04001B0E RID: 6926
		internal readonly bool _isValueType;

		// Token: 0x04001B0F RID: 6927
		private static readonly Func<Type, Tuple<bool, bool, bool, bool>> s_inspectTypeForInterfaces = new Func<Type, Tuple<bool, bool, bool, bool>>(DataStorage.InspectTypeForInterfaces);

		// Token: 0x04001B10 RID: 6928
		private static readonly ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>> s_typeImplementsInterface = new ConcurrentDictionary<Type, Tuple<bool, bool, bool, bool>>();
	}
}
