using System;
using System.Collections.Generic;

namespace ES3Internal
{
	// Token: 0x020000E8 RID: 232
	internal static class ES3Binary
	{
		// Token: 0x060004FA RID: 1274 RVA: 0x0001EC98 File Offset: 0x0001CE98
		internal static ES3SpecialByte TypeToByte(Type type)
		{
			ES3SpecialByte result;
			if (ES3Binary.TypeToId.TryGetValue(type, out result))
			{
				return result;
			}
			return ES3SpecialByte.Object;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001ECBB File Offset: 0x0001CEBB
		internal static Type ByteToType(ES3SpecialByte b)
		{
			return ES3Binary.ByteToType((byte)b);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
		internal static Type ByteToType(byte b)
		{
			Type result;
			if (ES3Binary.IdToType.TryGetValue((ES3SpecialByte)b, out result))
			{
				return result;
			}
			return typeof(object);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001ECEC File Offset: 0x0001CEEC
		internal static bool IsPrimitive(ES3SpecialByte b)
		{
			return b - ES3SpecialByte.Bool <= 14;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001ECF8 File Offset: 0x0001CEF8
		// Note: this type is marked as 'beforefieldinit'.
		static ES3Binary()
		{
		}

		// Token: 0x04000181 RID: 385
		internal const string ObjectTerminator = ".";

		// Token: 0x04000182 RID: 386
		internal static readonly Dictionary<ES3SpecialByte, Type> IdToType = new Dictionary<ES3SpecialByte, Type>
		{
			{
				ES3SpecialByte.Null,
				null
			},
			{
				ES3SpecialByte.Bool,
				typeof(bool)
			},
			{
				ES3SpecialByte.Byte,
				typeof(byte)
			},
			{
				ES3SpecialByte.Sbyte,
				typeof(sbyte)
			},
			{
				ES3SpecialByte.Char,
				typeof(char)
			},
			{
				ES3SpecialByte.Decimal,
				typeof(decimal)
			},
			{
				ES3SpecialByte.Double,
				typeof(double)
			},
			{
				ES3SpecialByte.Float,
				typeof(float)
			},
			{
				ES3SpecialByte.Int,
				typeof(int)
			},
			{
				ES3SpecialByte.Uint,
				typeof(uint)
			},
			{
				ES3SpecialByte.Long,
				typeof(long)
			},
			{
				ES3SpecialByte.Ulong,
				typeof(ulong)
			},
			{
				ES3SpecialByte.Short,
				typeof(short)
			},
			{
				ES3SpecialByte.Ushort,
				typeof(ushort)
			},
			{
				ES3SpecialByte.String,
				typeof(string)
			},
			{
				ES3SpecialByte.ByteArray,
				typeof(byte[])
			}
		};

		// Token: 0x04000183 RID: 387
		internal static readonly Dictionary<Type, ES3SpecialByte> TypeToId = new Dictionary<Type, ES3SpecialByte>
		{
			{
				typeof(bool),
				ES3SpecialByte.Bool
			},
			{
				typeof(byte),
				ES3SpecialByte.Byte
			},
			{
				typeof(sbyte),
				ES3SpecialByte.Sbyte
			},
			{
				typeof(char),
				ES3SpecialByte.Char
			},
			{
				typeof(decimal),
				ES3SpecialByte.Decimal
			},
			{
				typeof(double),
				ES3SpecialByte.Double
			},
			{
				typeof(float),
				ES3SpecialByte.Float
			},
			{
				typeof(int),
				ES3SpecialByte.Int
			},
			{
				typeof(uint),
				ES3SpecialByte.Uint
			},
			{
				typeof(long),
				ES3SpecialByte.Long
			},
			{
				typeof(ulong),
				ES3SpecialByte.Ulong
			},
			{
				typeof(short),
				ES3SpecialByte.Short
			},
			{
				typeof(ushort),
				ES3SpecialByte.Ushort
			},
			{
				typeof(string),
				ES3SpecialByte.String
			},
			{
				typeof(byte[]),
				ES3SpecialByte.ByteArray
			}
		};
	}
}
