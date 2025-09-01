using System;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	// Token: 0x02000381 RID: 897
	internal sealed class DataRecordInternal : DbDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x06002A68 RID: 10856 RVA: 0x000B9321 File Offset: 0x000B7521
		internal DataRecordInternal(SchemaInfo[] schemaInfo, object[] values, PropertyDescriptorCollection descriptors, FieldNameLookup fieldNameLookup)
		{
			this._schemaInfo = schemaInfo;
			this._values = values;
			this._propertyDescriptors = descriptors;
			this._fieldNameLookup = fieldNameLookup;
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x000B9346 File Offset: 0x000B7546
		public override int FieldCount
		{
			get
			{
				return this._schemaInfo.Length;
			}
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000B9350 File Offset: 0x000B7550
		public override int GetValues(object[] values)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = (values.Length < this._schemaInfo.Length) ? values.Length : this._schemaInfo.Length;
			for (int i = 0; i < num; i++)
			{
				values[i] = this._values[i];
			}
			return num;
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000B939E File Offset: 0x000B759E
		public override string GetName(int i)
		{
			return this._schemaInfo[i].name;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000B93B1 File Offset: 0x000B75B1
		public override object GetValue(int i)
		{
			return this._values[i];
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000B93BB File Offset: 0x000B75BB
		public override string GetDataTypeName(int i)
		{
			return this._schemaInfo[i].typeName;
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000B93CE File Offset: 0x000B75CE
		public override Type GetFieldType(int i)
		{
			return this._schemaInfo[i].type;
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000B93E1 File Offset: 0x000B75E1
		public override int GetOrdinal(string name)
		{
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x17000726 RID: 1830
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000727 RID: 1831
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000B9407 File Offset: 0x000B7607
		public override bool GetBoolean(int i)
		{
			return (bool)this._values[i];
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000B9416 File Offset: 0x000B7616
		public override byte GetByte(int i)
		{
			return (byte)this._values[i];
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000B9428 File Offset: 0x000B7628
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			int num = 0;
			byte[] array = (byte[])this._values[i];
			num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				Array.Copy(array, num2, buffer, bufferIndex, num);
			}
			catch (Exception e) when (ADP.IsCatchableExceptionType(e))
			{
				num = array.Length;
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				if (bufferIndex < 0 || bufferIndex >= buffer.Length)
				{
					throw ADP.InvalidDestinationBufferIndex(length, bufferIndex, "bufferIndex");
				}
				if (dataIndex < 0L || dataIndex >= (long)num)
				{
					throw ADP.InvalidSourceBufferIndex(length, dataIndex, "dataIndex");
				}
				if (num + bufferIndex > buffer.Length)
				{
					throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
				}
			}
			return (long)num;
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000B950C File Offset: 0x000B770C
		public override char GetChar(int i)
		{
			return ((string)this._values[i])[0];
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000B9524 File Offset: 0x000B7724
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			char[] array = ((string)this._values[i]).ToCharArray();
			int num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				Array.Copy(array, num2, buffer, bufferIndex, num);
			}
			catch (Exception e) when (ADP.IsCatchableExceptionType(e))
			{
				num = array.Length;
				if (length < 0)
				{
					throw ADP.InvalidDataLength((long)length);
				}
				if (bufferIndex < 0 || bufferIndex >= buffer.Length)
				{
					throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
				}
				if (num2 < 0 || num2 >= num)
				{
					throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
				}
				if (num + bufferIndex > buffer.Length)
				{
					throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
				}
			}
			return (long)num;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000B960C File Offset: 0x000B780C
		public override Guid GetGuid(int i)
		{
			return (Guid)this._values[i];
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000B961B File Offset: 0x000B781B
		public override short GetInt16(int i)
		{
			return (short)this._values[i];
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000B962A File Offset: 0x000B782A
		public override int GetInt32(int i)
		{
			return (int)this._values[i];
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000B9639 File Offset: 0x000B7839
		public override long GetInt64(int i)
		{
			return (long)this._values[i];
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000B9648 File Offset: 0x000B7848
		public override float GetFloat(int i)
		{
			return (float)this._values[i];
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000B9657 File Offset: 0x000B7857
		public override double GetDouble(int i)
		{
			return (double)this._values[i];
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000B9666 File Offset: 0x000B7866
		public override string GetString(int i)
		{
			return (string)this._values[i];
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000B9675 File Offset: 0x000B7875
		public override decimal GetDecimal(int i)
		{
			return (decimal)this._values[i];
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000B9684 File Offset: 0x000B7884
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this._values[i];
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000B9694 File Offset: 0x000B7894
		public override bool IsDBNull(int i)
		{
			object obj = this._values[i];
			return obj == null || Convert.IsDBNull(obj);
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x0003279E File Offset: 0x0003099E
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x00003E32 File Offset: 0x00002032
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x00003E32 File Offset: 0x00002032
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x00003E32 File Offset: 0x00002032
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x00003E32 File Offset: 0x00002032
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x00003E32 File Offset: 0x00002032
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x00003E32 File Offset: 0x00002032
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000327A6 File Offset: 0x000309A6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000327A6 File Offset: 0x000309A6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000327AE File Offset: 0x000309AE
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000B96B5 File Offset: 0x000B78B5
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propertyDescriptors == null)
			{
				this._propertyDescriptors = new PropertyDescriptorCollection(null);
			}
			return this._propertyDescriptors;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00005696 File Offset: 0x00003896
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001AD2 RID: 6866
		private SchemaInfo[] _schemaInfo;

		// Token: 0x04001AD3 RID: 6867
		private object[] _values;

		// Token: 0x04001AD4 RID: 6868
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x04001AD5 RID: 6869
		private FieldNameLookup _fieldNameLookup;
	}
}
