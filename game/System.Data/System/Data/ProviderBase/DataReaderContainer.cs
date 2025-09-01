using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x02000355 RID: 853
	internal abstract class DataReaderContainer
	{
		// Token: 0x06002705 RID: 9989 RVA: 0x000ACC58 File Offset: 0x000AAE58
		internal static DataReaderContainer Create(IDataReader dataReader, bool returnProviderSpecificTypes)
		{
			if (returnProviderSpecificTypes)
			{
				DbDataReader dbDataReader = dataReader as DbDataReader;
				if (dbDataReader != null)
				{
					return new DataReaderContainer.ProviderSpecificDataReader(dataReader, dbDataReader);
				}
			}
			return new DataReaderContainer.CommonLanguageSubsetDataReader(dataReader);
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000ACC80 File Offset: 0x000AAE80
		protected DataReaderContainer(IDataReader dataReader)
		{
			this._dataReader = dataReader;
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x000ACC8F File Offset: 0x000AAE8F
		internal int FieldCount
		{
			get
			{
				return this._fieldCount;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002708 RID: 9992
		internal abstract bool ReturnProviderSpecificTypes { get; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002709 RID: 9993
		protected abstract int VisibleFieldCount { get; }

		// Token: 0x0600270A RID: 9994
		internal abstract Type GetFieldType(int ordinal);

		// Token: 0x0600270B RID: 9995
		internal abstract object GetValue(int ordinal);

		// Token: 0x0600270C RID: 9996
		internal abstract int GetValues(object[] values);

		// Token: 0x0600270D RID: 9997 RVA: 0x000ACC98 File Offset: 0x000AAE98
		internal string GetName(int ordinal)
		{
			string name = this._dataReader.GetName(ordinal);
			if (name == null)
			{
				return "";
			}
			return name;
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000ACCBC File Offset: 0x000AAEBC
		internal DataTable GetSchemaTable()
		{
			return this._dataReader.GetSchemaTable();
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000ACCC9 File Offset: 0x000AAEC9
		internal bool NextResult()
		{
			this._fieldCount = 0;
			if (this._dataReader.NextResult())
			{
				this._fieldCount = this.VisibleFieldCount;
				return true;
			}
			return false;
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000ACCEE File Offset: 0x000AAEEE
		internal bool Read()
		{
			return this._dataReader.Read();
		}

		// Token: 0x0400197B RID: 6523
		protected readonly IDataReader _dataReader;

		// Token: 0x0400197C RID: 6524
		protected int _fieldCount;

		// Token: 0x02000356 RID: 854
		private sealed class ProviderSpecificDataReader : DataReaderContainer
		{
			// Token: 0x06002711 RID: 10001 RVA: 0x000ACCFB File Offset: 0x000AAEFB
			internal ProviderSpecificDataReader(IDataReader dataReader, DbDataReader dbDataReader) : base(dataReader)
			{
				this._providerSpecificDataReader = dbDataReader;
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x170006A3 RID: 1699
			// (get) Token: 0x06002712 RID: 10002 RVA: 0x00006D61 File Offset: 0x00004F61
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170006A4 RID: 1700
			// (get) Token: 0x06002713 RID: 10003 RVA: 0x000ACD18 File Offset: 0x000AAF18
			protected override int VisibleFieldCount
			{
				get
				{
					int visibleFieldCount = this._providerSpecificDataReader.VisibleFieldCount;
					if (0 > visibleFieldCount)
					{
						return 0;
					}
					return visibleFieldCount;
				}
			}

			// Token: 0x06002714 RID: 10004 RVA: 0x000ACD38 File Offset: 0x000AAF38
			internal override Type GetFieldType(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificFieldType(ordinal);
			}

			// Token: 0x06002715 RID: 10005 RVA: 0x000ACD46 File Offset: 0x000AAF46
			internal override object GetValue(int ordinal)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValue(ordinal);
			}

			// Token: 0x06002716 RID: 10006 RVA: 0x000ACD54 File Offset: 0x000AAF54
			internal override int GetValues(object[] values)
			{
				return this._providerSpecificDataReader.GetProviderSpecificValues(values);
			}

			// Token: 0x0400197D RID: 6525
			private DbDataReader _providerSpecificDataReader;
		}

		// Token: 0x02000357 RID: 855
		private sealed class CommonLanguageSubsetDataReader : DataReaderContainer
		{
			// Token: 0x06002717 RID: 10007 RVA: 0x000ACD62 File Offset: 0x000AAF62
			internal CommonLanguageSubsetDataReader(IDataReader dataReader) : base(dataReader)
			{
				this._fieldCount = this.VisibleFieldCount;
			}

			// Token: 0x170006A5 RID: 1701
			// (get) Token: 0x06002718 RID: 10008 RVA: 0x00006D64 File Offset: 0x00004F64
			internal override bool ReturnProviderSpecificTypes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170006A6 RID: 1702
			// (get) Token: 0x06002719 RID: 10009 RVA: 0x000ACD78 File Offset: 0x000AAF78
			protected override int VisibleFieldCount
			{
				get
				{
					int fieldCount = this._dataReader.FieldCount;
					if (0 > fieldCount)
					{
						return 0;
					}
					return fieldCount;
				}
			}

			// Token: 0x0600271A RID: 10010 RVA: 0x000ACD98 File Offset: 0x000AAF98
			internal override Type GetFieldType(int ordinal)
			{
				return this._dataReader.GetFieldType(ordinal);
			}

			// Token: 0x0600271B RID: 10011 RVA: 0x000ACDA6 File Offset: 0x000AAFA6
			internal override object GetValue(int ordinal)
			{
				return this._dataReader.GetValue(ordinal);
			}

			// Token: 0x0600271C RID: 10012 RVA: 0x000ACDB4 File Offset: 0x000AAFB4
			internal override int GetValues(object[] values)
			{
				return this._dataReader.GetValues(values);
			}
		}
	}
}
