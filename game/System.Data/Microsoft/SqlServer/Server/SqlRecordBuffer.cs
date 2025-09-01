using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003C RID: 60
	internal sealed class SqlRecordBuffer
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00008E8B File Offset: 0x0000708B
		internal SqlRecordBuffer(SmiMetaData metaData)
		{
			this._isNull = true;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00008E9A File Offset: 0x0000709A
		internal bool IsNull
		{
			get
			{
				return this._isNull;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00008EA2 File Offset: 0x000070A2
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00008EAF File Offset: 0x000070AF
		internal bool Boolean
		{
			get
			{
				return this._value._boolean;
			}
			set
			{
				this._value._boolean = value;
				this._type = SqlRecordBuffer.StorageType.Boolean;
				this._isNull = false;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00008ECB File Offset: 0x000070CB
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00008ED8 File Offset: 0x000070D8
		internal byte Byte
		{
			get
			{
				return this._value._byte;
			}
			set
			{
				this._value._byte = value;
				this._type = SqlRecordBuffer.StorageType.Byte;
				this._isNull = false;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00008EF4 File Offset: 0x000070F4
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00008F01 File Offset: 0x00007101
		internal DateTime DateTime
		{
			get
			{
				return this._value._dateTime;
			}
			set
			{
				this._value._dateTime = value;
				this._type = SqlRecordBuffer.StorageType.DateTime;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00008F34 File Offset: 0x00007134
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00008F41 File Offset: 0x00007141
		internal DateTimeOffset DateTimeOffset
		{
			get
			{
				return this._value._dateTimeOffset;
			}
			set
			{
				this._value._dateTimeOffset = value;
				this._type = SqlRecordBuffer.StorageType.DateTimeOffset;
				this._isNull = false;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00008F5D File Offset: 0x0000715D
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00008F6A File Offset: 0x0000716A
		internal double Double
		{
			get
			{
				return this._value._double;
			}
			set
			{
				this._value._double = value;
				this._type = SqlRecordBuffer.StorageType.Double;
				this._isNull = false;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00008F86 File Offset: 0x00007186
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00008F93 File Offset: 0x00007193
		internal Guid Guid
		{
			get
			{
				return this._value._guid;
			}
			set
			{
				this._value._guid = value;
				this._type = SqlRecordBuffer.StorageType.Guid;
				this._isNull = false;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00008FAF File Offset: 0x000071AF
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00008FBC File Offset: 0x000071BC
		internal short Int16
		{
			get
			{
				return this._value._int16;
			}
			set
			{
				this._value._int16 = value;
				this._type = SqlRecordBuffer.StorageType.Int16;
				this._isNull = false;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00008FD8 File Offset: 0x000071D8
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00008FE5 File Offset: 0x000071E5
		internal int Int32
		{
			get
			{
				return this._value._int32;
			}
			set
			{
				this._value._int32 = value;
				this._type = SqlRecordBuffer.StorageType.Int32;
				this._isNull = false;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00009002 File Offset: 0x00007202
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000900F File Offset: 0x0000720F
		internal long Int64
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				this._value._int64 = value;
				this._type = SqlRecordBuffer.StorageType.Int64;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00009043 File Offset: 0x00007243
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00009050 File Offset: 0x00007250
		internal float Single
		{
			get
			{
				return this._value._single;
			}
			set
			{
				this._value._single = value;
				this._type = SqlRecordBuffer.StorageType.Single;
				this._isNull = false;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00009070 File Offset: 0x00007270
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x000090D0 File Offset: 0x000072D0
		internal string String
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					return (string)this._object;
				}
				if (SqlRecordBuffer.StorageType.CharArray == this._type)
				{
					return new string((char[])this._object, 0, (int)this.CharsLength);
				}
				return new SqlXml(new MemoryStream((byte[])this._object, false)).Value;
			}
			set
			{
				this._object = value;
				this._value._int64 = (long)value.Length;
				this._type = SqlRecordBuffer.StorageType.String;
				this._isNull = false;
				if (this._isMetaSet)
				{
					this._isMetaSet = false;
					return;
				}
				this._metadata = null;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000911C File Offset: 0x0000731C
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00009129 File Offset: 0x00007329
		internal SqlDecimal SqlDecimal
		{
			get
			{
				return (SqlDecimal)this._object;
			}
			set
			{
				this._object = value;
				this._type = SqlRecordBuffer.StorageType.SqlDecimal;
				this._isNull = false;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00009146 File Offset: 0x00007346
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00009153 File Offset: 0x00007353
		internal TimeSpan TimeSpan
		{
			get
			{
				return this._value._timeSpan;
			}
			set
			{
				this._value._timeSpan = value;
				this._type = SqlRecordBuffer.StorageType.TimeSpan;
				this._isNull = false;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00009170 File Offset: 0x00007370
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000918D File Offset: 0x0000738D
		internal long BytesLength
		{
			get
			{
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this.ConvertXmlStringToByteArray();
				}
				return this._value._int64;
			}
			set
			{
				if (value == 0L)
				{
					this._value._int64 = value;
					this._object = Array.Empty<byte>();
					this._type = SqlRecordBuffer.StorageType.ByteArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00009002 File Offset: 0x00007202
		// (set) Token: 0x060002DC RID: 732 RVA: 0x000091C4 File Offset: 0x000073C4
		internal long CharsLength
		{
			get
			{
				return this._value._int64;
			}
			set
			{
				if (value == 0L)
				{
					this._value._int64 = value;
					this._object = Array.Empty<char>();
					this._type = SqlRecordBuffer.StorageType.CharArray;
					this._isNull = false;
					return;
				}
				this._value._int64 = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000091FC File Offset: 0x000073FC
		// (set) Token: 0x060002DE RID: 734 RVA: 0x000092FE File Offset: 0x000074FE
		internal SmiMetaData VariantType
		{
			get
			{
				switch (this._type)
				{
				case SqlRecordBuffer.StorageType.Boolean:
					return SmiMetaData.DefaultBit;
				case SqlRecordBuffer.StorageType.Byte:
					return SmiMetaData.DefaultTinyInt;
				case SqlRecordBuffer.StorageType.ByteArray:
					return SmiMetaData.DefaultVarBinary;
				case SqlRecordBuffer.StorageType.CharArray:
					return SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.DateTime:
					return this._metadata ?? SmiMetaData.DefaultDateTime;
				case SqlRecordBuffer.StorageType.DateTimeOffset:
					return SmiMetaData.DefaultDateTimeOffset;
				case SqlRecordBuffer.StorageType.Double:
					return SmiMetaData.DefaultFloat;
				case SqlRecordBuffer.StorageType.Guid:
					return SmiMetaData.DefaultUniqueIdentifier;
				case SqlRecordBuffer.StorageType.Int16:
					return SmiMetaData.DefaultSmallInt;
				case SqlRecordBuffer.StorageType.Int32:
					return SmiMetaData.DefaultInt;
				case SqlRecordBuffer.StorageType.Int64:
					return this._metadata ?? SmiMetaData.DefaultBigInt;
				case SqlRecordBuffer.StorageType.Single:
					return SmiMetaData.DefaultReal;
				case SqlRecordBuffer.StorageType.String:
					return this._metadata ?? SmiMetaData.DefaultNVarChar;
				case SqlRecordBuffer.StorageType.SqlDecimal:
					return new SmiMetaData(SqlDbType.Decimal, 17L, ((SqlDecimal)this._object).Precision, ((SqlDecimal)this._object).Scale, 0L, SqlCompareOptions.None, null);
				case SqlRecordBuffer.StorageType.TimeSpan:
					return SmiMetaData.DefaultTime;
				default:
					return null;
				}
			}
			set
			{
				this._metadata = value;
				this._isMetaSet = true;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009310 File Offset: 0x00007510
		internal int GetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int srcOffset = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.String == this._type)
			{
				this.ConvertXmlStringToByteArray();
			}
			Buffer.BlockCopy((byte[])this._object, srcOffset, buffer, bufferOffset, length);
			return length;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009348 File Offset: 0x00007548
		internal int GetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int sourceIndex = (int)fieldOffset;
			if (SqlRecordBuffer.StorageType.CharArray == this._type)
			{
				Array.Copy((char[])this._object, sourceIndex, buffer, bufferOffset, length);
			}
			else
			{
				((string)this._object).CopyTo(sourceIndex, buffer, bufferOffset, length);
			}
			return length;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00009390 File Offset: 0x00007590
		internal int SetBytes(long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || SqlRecordBuffer.StorageType.ByteArray != this._type)
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new byte[length];
				this._type = SqlRecordBuffer.StorageType.ByteArray;
				this._isNull = false;
				this.BytesLength = (long)length;
			}
			else
			{
				if ((long)num > this.BytesLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if ((long)(num + length) > this.BytesLength)
				{
					int num2 = ((byte[])this._object).Length;
					if (num + length > num2)
					{
						byte[] array = new byte[Math.Max(num + length, 2 * num2)];
						Buffer.BlockCopy((byte[])this._object, 0, array, 0, (int)this.BytesLength);
						this._object = array;
					}
					this.BytesLength = (long)(num + length);
				}
			}
			Buffer.BlockCopy(buffer, bufferOffset, (byte[])this._object, num, length);
			return length;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009474 File Offset: 0x00007674
		internal int SetChars(long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = (int)fieldOffset;
			if (this.IsNull || (SqlRecordBuffer.StorageType.CharArray != this._type && SqlRecordBuffer.StorageType.String != this._type))
			{
				if (num != 0)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				this._object = new char[length];
				this._type = SqlRecordBuffer.StorageType.CharArray;
				this._isNull = false;
				this.CharsLength = (long)length;
			}
			else
			{
				if ((long)num > this.CharsLength)
				{
					throw ADP.ArgumentOutOfRange("fieldOffset");
				}
				if (SqlRecordBuffer.StorageType.String == this._type)
				{
					this._object = ((string)this._object).ToCharArray();
					this._type = SqlRecordBuffer.StorageType.CharArray;
				}
				if ((long)(num + length) > this.CharsLength)
				{
					int num2 = ((char[])this._object).Length;
					if (num + length > num2)
					{
						char[] array = new char[Math.Max(num + length, 2 * num2)];
						Array.Copy((char[])this._object, 0, array, 0, (int)this.CharsLength);
						this._object = array;
					}
					this.CharsLength = (long)(num + length);
				}
			}
			Array.Copy(buffer, bufferOffset, (char[])this._object, num, length);
			return length;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009589 File Offset: 0x00007789
		internal void SetNull()
		{
			this._isNull = true;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009594 File Offset: 0x00007794
		private void ConvertXmlStringToByteArray()
		{
			string text = (string)this._object;
			byte[] array = new byte[2 + Encoding.Unicode.GetByteCount(text)];
			array[0] = byte.MaxValue;
			array[1] = 254;
			Encoding.Unicode.GetBytes(text, 0, text.Length, array, 2);
			this._object = array;
			this._value._int64 = (long)array.Length;
			this._type = SqlRecordBuffer.StorageType.ByteArray;
		}

		// Token: 0x040004D6 RID: 1238
		private bool _isNull;

		// Token: 0x040004D7 RID: 1239
		private SqlRecordBuffer.StorageType _type;

		// Token: 0x040004D8 RID: 1240
		private SqlRecordBuffer.Storage _value;

		// Token: 0x040004D9 RID: 1241
		private object _object;

		// Token: 0x040004DA RID: 1242
		private SmiMetaData _metadata;

		// Token: 0x040004DB RID: 1243
		private bool _isMetaSet;

		// Token: 0x0200003D RID: 61
		internal enum StorageType
		{
			// Token: 0x040004DD RID: 1245
			Boolean,
			// Token: 0x040004DE RID: 1246
			Byte,
			// Token: 0x040004DF RID: 1247
			ByteArray,
			// Token: 0x040004E0 RID: 1248
			CharArray,
			// Token: 0x040004E1 RID: 1249
			DateTime,
			// Token: 0x040004E2 RID: 1250
			DateTimeOffset,
			// Token: 0x040004E3 RID: 1251
			Double,
			// Token: 0x040004E4 RID: 1252
			Guid,
			// Token: 0x040004E5 RID: 1253
			Int16,
			// Token: 0x040004E6 RID: 1254
			Int32,
			// Token: 0x040004E7 RID: 1255
			Int64,
			// Token: 0x040004E8 RID: 1256
			Single,
			// Token: 0x040004E9 RID: 1257
			String,
			// Token: 0x040004EA RID: 1258
			SqlDecimal,
			// Token: 0x040004EB RID: 1259
			TimeSpan
		}

		// Token: 0x0200003E RID: 62
		[StructLayout(LayoutKind.Explicit)]
		internal struct Storage
		{
			// Token: 0x040004EC RID: 1260
			[FieldOffset(0)]
			internal bool _boolean;

			// Token: 0x040004ED RID: 1261
			[FieldOffset(0)]
			internal byte _byte;

			// Token: 0x040004EE RID: 1262
			[FieldOffset(0)]
			internal DateTime _dateTime;

			// Token: 0x040004EF RID: 1263
			[FieldOffset(0)]
			internal DateTimeOffset _dateTimeOffset;

			// Token: 0x040004F0 RID: 1264
			[FieldOffset(0)]
			internal double _double;

			// Token: 0x040004F1 RID: 1265
			[FieldOffset(0)]
			internal Guid _guid;

			// Token: 0x040004F2 RID: 1266
			[FieldOffset(0)]
			internal short _int16;

			// Token: 0x040004F3 RID: 1267
			[FieldOffset(0)]
			internal int _int32;

			// Token: 0x040004F4 RID: 1268
			[FieldOffset(0)]
			internal long _int64;

			// Token: 0x040004F5 RID: 1269
			[FieldOffset(0)]
			internal float _single;

			// Token: 0x040004F6 RID: 1270
			[FieldOffset(0)]
			internal TimeSpan _timeSpan;
		}
	}
}
