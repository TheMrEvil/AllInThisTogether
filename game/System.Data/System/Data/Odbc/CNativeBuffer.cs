using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020002FE RID: 766
	internal sealed class CNativeBuffer : DbBuffer
	{
		// Token: 0x06002208 RID: 8712 RVA: 0x0009E45E File Offset: 0x0009C65E
		internal CNativeBuffer(int initialSize) : base(initialSize)
		{
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x0009E467 File Offset: 0x0009C667
		internal short ShortLength
		{
			get
			{
				return checked((short)base.Length);
			}
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0009E470 File Offset: 0x0009C670
		internal object MarshalToManaged(int offset, ODBC32.SQL_C sqlctype, int cb)
		{
			if (sqlctype <= ODBC32.SQL_C.SSHORT)
			{
				if (sqlctype <= ODBC32.SQL_C.SBIGINT)
				{
					if (sqlctype == ODBC32.SQL_C.UTINYINT)
					{
						return base.ReadByte(offset);
					}
					if (sqlctype == ODBC32.SQL_C.SBIGINT)
					{
						return base.ReadInt64(offset);
					}
				}
				else
				{
					if (sqlctype == ODBC32.SQL_C.SLONG)
					{
						return base.ReadInt32(offset);
					}
					if (sqlctype == ODBC32.SQL_C.SSHORT)
					{
						return base.ReadInt16(offset);
					}
				}
			}
			else if (sqlctype <= ODBC32.SQL_C.NUMERIC)
			{
				switch (sqlctype)
				{
				case ODBC32.SQL_C.GUID:
					return base.ReadGuid(offset);
				case (ODBC32.SQL_C)(-10):
				case (ODBC32.SQL_C)(-9):
					break;
				case ODBC32.SQL_C.WCHAR:
					if (cb == -3)
					{
						return base.PtrToStringUni(offset);
					}
					cb = Math.Min(cb / 2, (base.Length - 2) / 2);
					return base.PtrToStringUni(offset, cb);
				case ODBC32.SQL_C.BIT:
					return base.ReadByte(offset) > 0;
				default:
					switch (sqlctype)
					{
					case ODBC32.SQL_C.BINARY:
					case ODBC32.SQL_C.CHAR:
						cb = Math.Min(cb, base.Length);
						return base.ReadBytes(offset, cb);
					case ODBC32.SQL_C.NUMERIC:
						return base.ReadNumeric(offset);
					}
					break;
				}
			}
			else
			{
				if (sqlctype == ODBC32.SQL_C.REAL)
				{
					return base.ReadSingle(offset);
				}
				if (sqlctype == ODBC32.SQL_C.DOUBLE)
				{
					return base.ReadDouble(offset);
				}
				switch (sqlctype)
				{
				case ODBC32.SQL_C.TYPE_DATE:
					return base.ReadDate(offset);
				case ODBC32.SQL_C.TYPE_TIME:
					return base.ReadTime(offset);
				case ODBC32.SQL_C.TYPE_TIMESTAMP:
					return base.ReadDateTime(offset);
				}
			}
			return null;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0009E630 File Offset: 0x0009C830
		internal void MarshalToNative(int offset, object value, ODBC32.SQL_C sqlctype, int sizeorprecision, int valueOffset)
		{
			if (sqlctype <= ODBC32.SQL_C.SSHORT)
			{
				if (sqlctype <= ODBC32.SQL_C.SBIGINT)
				{
					if (sqlctype == ODBC32.SQL_C.UTINYINT)
					{
						base.WriteByte(offset, (byte)value);
						return;
					}
					if (sqlctype != ODBC32.SQL_C.SBIGINT)
					{
						return;
					}
					base.WriteInt64(offset, (long)value);
					return;
				}
				else
				{
					if (sqlctype == ODBC32.SQL_C.SLONG)
					{
						base.WriteInt32(offset, (int)value);
						return;
					}
					if (sqlctype != ODBC32.SQL_C.SSHORT)
					{
						return;
					}
					base.WriteInt16(offset, (short)value);
					return;
				}
			}
			else
			{
				if (sqlctype <= ODBC32.SQL_C.NUMERIC)
				{
					switch (sqlctype)
					{
					case ODBC32.SQL_C.GUID:
						base.WriteGuid(offset, (Guid)value);
						return;
					case (ODBC32.SQL_C)(-10):
					case (ODBC32.SQL_C)(-9):
						break;
					case ODBC32.SQL_C.WCHAR:
					{
						int num;
						char[] array;
						if (value is string)
						{
							num = Math.Max(0, ((string)value).Length - valueOffset);
							if (sizeorprecision > 0 && sizeorprecision < num)
							{
								num = sizeorprecision;
							}
							array = ((string)value).ToCharArray(valueOffset, num);
							base.WriteCharArray(offset, array, 0, array.Length);
							base.WriteInt16(offset + array.Length * 2, 0);
							return;
						}
						num = Math.Max(0, ((char[])value).Length - valueOffset);
						if (sizeorprecision > 0 && sizeorprecision < num)
						{
							num = sizeorprecision;
						}
						array = (char[])value;
						base.WriteCharArray(offset, array, valueOffset, num);
						base.WriteInt16(offset + array.Length * 2, 0);
						return;
					}
					case ODBC32.SQL_C.BIT:
						base.WriteByte(offset, ((bool)value) ? 1 : 0);
						return;
					default:
						switch (sqlctype)
						{
						case ODBC32.SQL_C.BINARY:
						case ODBC32.SQL_C.CHAR:
						{
							byte[] array2 = (byte[])value;
							int num2 = array2.Length;
							num2 -= valueOffset;
							if (sizeorprecision > 0 && sizeorprecision < num2)
							{
								num2 = sizeorprecision;
							}
							base.WriteBytes(offset, array2, valueOffset, num2);
							return;
						}
						case (ODBC32.SQL_C)(-1):
						case (ODBC32.SQL_C)0:
							break;
						case ODBC32.SQL_C.NUMERIC:
							base.WriteNumeric(offset, (decimal)value, checked((byte)sizeorprecision));
							break;
						default:
							return;
						}
						break;
					}
					return;
				}
				if (sqlctype == ODBC32.SQL_C.REAL)
				{
					base.WriteSingle(offset, (float)value);
					return;
				}
				if (sqlctype == ODBC32.SQL_C.DOUBLE)
				{
					base.WriteDouble(offset, (double)value);
					return;
				}
				switch (sqlctype)
				{
				case ODBC32.SQL_C.TYPE_DATE:
					base.WriteDate(offset, (DateTime)value);
					return;
				case ODBC32.SQL_C.TYPE_TIME:
					base.WriteTime(offset, (TimeSpan)value);
					return;
				case ODBC32.SQL_C.TYPE_TIMESTAMP:
					this.WriteODBCDateTime(offset, (DateTime)value);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0009E83C File Offset: 0x0009CA3C
		internal HandleRef PtrOffset(int offset, int length)
		{
			base.Validate(offset, length);
			IntPtr handle = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
			return new HandleRef(this, handle);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0009E868 File Offset: 0x0009CA68
		internal void WriteODBCDateTime(int offset, DateTime value)
		{
			short[] source = new short[]
			{
				(short)value.Year,
				(short)value.Month,
				(short)value.Day,
				(short)value.Hour,
				(short)value.Minute,
				(short)value.Second
			};
			base.WriteInt16Array(offset, source, 0, 6);
			base.WriteInt32(offset + 12, value.Millisecond * 1000000);
		}
	}
}
