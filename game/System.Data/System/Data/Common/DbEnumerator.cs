using System;
using System.Collections;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	/// <summary>Exposes the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method, which supports a simple iteration over a collection by a .NET Framework data provider.</summary>
	// Token: 0x02000396 RID: 918
	public class DbEnumerator : IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified <see langword="DataReader" />.</summary>
		/// <param name="reader">The <see langword="DataReader" /> through which to iterate.</param>
		// Token: 0x06002CA2 RID: 11426 RVA: 0x000BEA8B File Offset: 0x000BCC8B
		public DbEnumerator(IDataReader reader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified <see langword="DataReader" />, and indicates whether to automatically close the <see langword="DataReader" /> after iterating through its data.</summary>
		/// <param name="reader">The <see langword="DataReader" /> through which to iterate.</param>
		/// <param name="closeReader">
		///   <see langword="true" /> to automatically close the <see langword="DataReader" /> after iterating through its data; otherwise, <see langword="false" />.</param>
		// Token: 0x06002CA3 RID: 11427 RVA: 0x000BEAA8 File Offset: 0x000BCCA8
		public DbEnumerator(IDataReader reader, bool closeReader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
			this._closeReader = closeReader;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class with the give n data reader.</summary>
		/// <param name="reader">The DataReader through which to iterate.</param>
		// Token: 0x06002CA4 RID: 11428 RVA: 0x000BEACC File Offset: 0x000BCCCC
		public DbEnumerator(DbDataReader reader) : this(reader)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified reader and indicates whether to automatically close the reader after iterating through its data.</summary>
		/// <param name="reader">The DataReader through which to iterate.</param>
		/// <param name="closeReader">
		///   <see langword="true" /> to automatically close the DataReader after iterating through its data; otherwise, <see langword="false" />.</param>
		// Token: 0x06002CA5 RID: 11429 RVA: 0x000BEAD5 File Offset: 0x000BCCD5
		public DbEnumerator(DbDataReader reader, bool closeReader) : this(reader, closeReader)
		{
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x000BEADF File Offset: 0x000BCCDF
		public object Current
		{
			get
			{
				return this._current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>
		///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002CA7 RID: 11431 RVA: 0x000BEAE8 File Offset: 0x000BCCE8
		public bool MoveNext()
		{
			if (this._schemaInfo == null)
			{
				this.BuildSchemaInfo();
			}
			this._current = null;
			if (this._reader.Read())
			{
				object[] values = new object[this._schemaInfo.Length];
				this._reader.GetValues(values);
				this._current = new DataRecordInternal(this._schemaInfo, values, this._descriptors, this._fieldNameLookup);
				return true;
			}
			if (this._closeReader)
			{
				this._reader.Close();
			}
			return false;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06002CA8 RID: 11432 RVA: 0x00008E4B File Offset: 0x0000704B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Reset()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000BEB68 File Offset: 0x000BCD68
		private void BuildSchemaInfo()
		{
			int fieldCount = this._reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = this._reader.GetName(i);
			}
			ADP.BuildSchemaTableInfoTableNames(array);
			SchemaInfo[] array2 = new SchemaInfo[fieldCount];
			PropertyDescriptor[] array3 = new PropertyDescriptor[this._reader.FieldCount];
			for (int j = 0; j < array2.Length; j++)
			{
				SchemaInfo schemaInfo = default(SchemaInfo);
				schemaInfo.name = this._reader.GetName(j);
				schemaInfo.type = this._reader.GetFieldType(j);
				schemaInfo.typeName = this._reader.GetDataTypeName(j);
				array3[j] = new DbEnumerator.DbColumnDescriptor(j, array[j], schemaInfo.type);
				array2[j] = schemaInfo;
			}
			this._schemaInfo = array2;
			this._fieldNameLookup = new FieldNameLookup(this._reader, -1);
			this._descriptors = new PropertyDescriptorCollection(array3);
		}

		// Token: 0x04001B51 RID: 6993
		internal IDataReader _reader;

		// Token: 0x04001B52 RID: 6994
		internal DbDataRecord _current;

		// Token: 0x04001B53 RID: 6995
		internal SchemaInfo[] _schemaInfo;

		// Token: 0x04001B54 RID: 6996
		internal PropertyDescriptorCollection _descriptors;

		// Token: 0x04001B55 RID: 6997
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04001B56 RID: 6998
		private bool _closeReader;

		// Token: 0x02000397 RID: 919
		private sealed class DbColumnDescriptor : PropertyDescriptor
		{
			// Token: 0x06002CAA RID: 11434 RVA: 0x000BEC62 File Offset: 0x000BCE62
			internal DbColumnDescriptor(int ordinal, string name, Type type) : base(name, null)
			{
				this._ordinal = ordinal;
				this._type = type;
			}

			// Token: 0x17000793 RID: 1939
			// (get) Token: 0x06002CAB RID: 11435 RVA: 0x000BEC7A File Offset: 0x000BCE7A
			public override Type ComponentType
			{
				get
				{
					return typeof(IDataRecord);
				}
			}

			// Token: 0x17000794 RID: 1940
			// (get) Token: 0x06002CAC RID: 11436 RVA: 0x00006D61 File Offset: 0x00004F61
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000795 RID: 1941
			// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000BEC86 File Offset: 0x000BCE86
			public override Type PropertyType
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x06002CAE RID: 11438 RVA: 0x00006D64 File Offset: 0x00004F64
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x06002CAF RID: 11439 RVA: 0x000BEC8E File Offset: 0x000BCE8E
			public override object GetValue(object component)
			{
				return ((IDataRecord)component)[this._ordinal];
			}

			// Token: 0x06002CB0 RID: 11440 RVA: 0x00008E4B File Offset: 0x0000704B
			public override void ResetValue(object component)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x06002CB1 RID: 11441 RVA: 0x00008E4B File Offset: 0x0000704B
			public override void SetValue(object component, object value)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x06002CB2 RID: 11442 RVA: 0x00006D64 File Offset: 0x00004F64
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x04001B57 RID: 6999
			private int _ordinal;

			// Token: 0x04001B58 RID: 7000
			private Type _type;
		}
	}
}
