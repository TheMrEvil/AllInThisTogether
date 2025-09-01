using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006A8 RID: 1704
	internal sealed class Converter
	{
		// Token: 0x06003E88 RID: 16008 RVA: 0x0000259F File Offset: 0x0000079F
		private Converter()
		{
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x000D7E34 File Offset: 0x000D6034
		internal static InternalPrimitiveTypeE ToCode(Type type)
		{
			InternalPrimitiveTypeE result;
			if (type != null && !type.IsPrimitive)
			{
				if (type == Converter.typeofDateTime)
				{
					result = InternalPrimitiveTypeE.DateTime;
				}
				else if (type == Converter.typeofTimeSpan)
				{
					result = InternalPrimitiveTypeE.TimeSpan;
				}
				else if (type == Converter.typeofDecimal)
				{
					result = InternalPrimitiveTypeE.Decimal;
				}
				else
				{
					result = InternalPrimitiveTypeE.Invalid;
				}
			}
			else
			{
				result = Converter.ToPrimitiveTypeEnum(Type.GetTypeCode(type));
			}
			return result;
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x000D7E84 File Offset: 0x000D6084
		internal static bool IsWriteAsByteArray(InternalPrimitiveTypeE code)
		{
			bool result = false;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
			case InternalPrimitiveTypeE.Byte:
			case InternalPrimitiveTypeE.Char:
			case InternalPrimitiveTypeE.Double:
			case InternalPrimitiveTypeE.Int16:
			case InternalPrimitiveTypeE.Int32:
			case InternalPrimitiveTypeE.Int64:
			case InternalPrimitiveTypeE.SByte:
			case InternalPrimitiveTypeE.Single:
			case InternalPrimitiveTypeE.UInt16:
			case InternalPrimitiveTypeE.UInt32:
			case InternalPrimitiveTypeE.UInt64:
				result = true;
				break;
			}
			return result;
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x000D7EE0 File Offset: 0x000D60E0
		internal static int TypeLength(InternalPrimitiveTypeE code)
		{
			int result = 0;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				result = 1;
				break;
			case InternalPrimitiveTypeE.Byte:
				result = 1;
				break;
			case InternalPrimitiveTypeE.Char:
				result = 2;
				break;
			case InternalPrimitiveTypeE.Double:
				result = 8;
				break;
			case InternalPrimitiveTypeE.Int16:
				result = 2;
				break;
			case InternalPrimitiveTypeE.Int32:
				result = 4;
				break;
			case InternalPrimitiveTypeE.Int64:
				result = 8;
				break;
			case InternalPrimitiveTypeE.SByte:
				result = 1;
				break;
			case InternalPrimitiveTypeE.Single:
				result = 4;
				break;
			case InternalPrimitiveTypeE.UInt16:
				result = 2;
				break;
			case InternalPrimitiveTypeE.UInt32:
				result = 4;
				break;
			case InternalPrimitiveTypeE.UInt64:
				result = 8;
				break;
			}
			return result;
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x000D7F68 File Offset: 0x000D6168
		internal static InternalNameSpaceE GetNameSpaceEnum(InternalPrimitiveTypeE code, Type type, WriteObjectInfo objectInfo, out string typeName)
		{
			InternalNameSpaceE internalNameSpaceE = InternalNameSpaceE.None;
			typeName = null;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				switch (code)
				{
				case InternalPrimitiveTypeE.Boolean:
				case InternalPrimitiveTypeE.Byte:
				case InternalPrimitiveTypeE.Char:
				case InternalPrimitiveTypeE.Double:
				case InternalPrimitiveTypeE.Int16:
				case InternalPrimitiveTypeE.Int32:
				case InternalPrimitiveTypeE.Int64:
				case InternalPrimitiveTypeE.SByte:
				case InternalPrimitiveTypeE.Single:
				case InternalPrimitiveTypeE.TimeSpan:
				case InternalPrimitiveTypeE.DateTime:
				case InternalPrimitiveTypeE.UInt16:
				case InternalPrimitiveTypeE.UInt32:
				case InternalPrimitiveTypeE.UInt64:
					internalNameSpaceE = InternalNameSpaceE.XdrPrimitive;
					typeName = "System." + Converter.ToComType(code);
					break;
				case InternalPrimitiveTypeE.Decimal:
					internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					typeName = "System." + Converter.ToComType(code);
					break;
				}
			}
			if (internalNameSpaceE == InternalNameSpaceE.None && type != null)
			{
				if (type == Converter.typeofString)
				{
					internalNameSpaceE = InternalNameSpaceE.XdrString;
				}
				else if (objectInfo == null)
				{
					typeName = type.FullName;
					if (type.Assembly == Converter.urtAssembly)
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
				else
				{
					typeName = objectInfo.GetTypeFullName();
					if (objectInfo.GetAssemblyString().Equals(Converter.urtAssemblyString))
					{
						internalNameSpaceE = InternalNameSpaceE.UrtSystem;
					}
					else
					{
						internalNameSpaceE = InternalNameSpaceE.UrtUser;
					}
				}
			}
			return internalNameSpaceE;
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x000D8049 File Offset: 0x000D6249
		internal static Type ToArrayType(InternalPrimitiveTypeE code)
		{
			if (Converter.arrayTypeA == null)
			{
				Converter.InitArrayTypeA();
			}
			return Converter.arrayTypeA[(int)code];
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x000D8064 File Offset: 0x000D6264
		private static void InitTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBoolean;
			array[2] = Converter.typeofByte;
			array[3] = Converter.typeofChar;
			array[5] = Converter.typeofDecimal;
			array[6] = Converter.typeofDouble;
			array[7] = Converter.typeofInt16;
			array[8] = Converter.typeofInt32;
			array[9] = Converter.typeofInt64;
			array[10] = Converter.typeofSByte;
			array[11] = Converter.typeofSingle;
			array[12] = Converter.typeofTimeSpan;
			array[13] = Converter.typeofDateTime;
			array[14] = Converter.typeofUInt16;
			array[15] = Converter.typeofUInt32;
			array[16] = Converter.typeofUInt64;
			Converter.typeA = array;
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x000D8108 File Offset: 0x000D6308
		private static void InitArrayTypeA()
		{
			Type[] array = new Type[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = Converter.typeofBooleanArray;
			array[2] = Converter.typeofByteArray;
			array[3] = Converter.typeofCharArray;
			array[5] = Converter.typeofDecimalArray;
			array[6] = Converter.typeofDoubleArray;
			array[7] = Converter.typeofInt16Array;
			array[8] = Converter.typeofInt32Array;
			array[9] = Converter.typeofInt64Array;
			array[10] = Converter.typeofSByteArray;
			array[11] = Converter.typeofSingleArray;
			array[12] = Converter.typeofTimeSpanArray;
			array[13] = Converter.typeofDateTimeArray;
			array[14] = Converter.typeofUInt16Array;
			array[15] = Converter.typeofUInt32Array;
			array[16] = Converter.typeofUInt64Array;
			Converter.arrayTypeA = array;
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x000D81AA File Offset: 0x000D63AA
		internal static Type ToType(InternalPrimitiveTypeE code)
		{
			if (Converter.typeA == null)
			{
				Converter.InitTypeA();
			}
			return Converter.typeA[(int)code];
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x000D81C4 File Offset: 0x000D63C4
		internal static Array CreatePrimitiveArray(InternalPrimitiveTypeE code, int length)
		{
			Array result = null;
			switch (code)
			{
			case InternalPrimitiveTypeE.Boolean:
				result = new bool[length];
				break;
			case InternalPrimitiveTypeE.Byte:
				result = new byte[length];
				break;
			case InternalPrimitiveTypeE.Char:
				result = new char[length];
				break;
			case InternalPrimitiveTypeE.Decimal:
				result = new decimal[length];
				break;
			case InternalPrimitiveTypeE.Double:
				result = new double[length];
				break;
			case InternalPrimitiveTypeE.Int16:
				result = new short[length];
				break;
			case InternalPrimitiveTypeE.Int32:
				result = new int[length];
				break;
			case InternalPrimitiveTypeE.Int64:
				result = new long[length];
				break;
			case InternalPrimitiveTypeE.SByte:
				result = new sbyte[length];
				break;
			case InternalPrimitiveTypeE.Single:
				result = new float[length];
				break;
			case InternalPrimitiveTypeE.TimeSpan:
				result = new TimeSpan[length];
				break;
			case InternalPrimitiveTypeE.DateTime:
				result = new DateTime[length];
				break;
			case InternalPrimitiveTypeE.UInt16:
				result = new ushort[length];
				break;
			case InternalPrimitiveTypeE.UInt32:
				result = new uint[length];
				break;
			case InternalPrimitiveTypeE.UInt64:
				result = new ulong[length];
				break;
			}
			return result;
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x000D82A8 File Offset: 0x000D64A8
		internal static bool IsPrimitiveArray(Type type, out object typeInformation)
		{
			typeInformation = null;
			bool result = true;
			if (type == Converter.typeofBooleanArray)
			{
				typeInformation = InternalPrimitiveTypeE.Boolean;
			}
			else if (type == Converter.typeofByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.Byte;
			}
			else if (type == Converter.typeofCharArray)
			{
				typeInformation = InternalPrimitiveTypeE.Char;
			}
			else if (type == Converter.typeofDoubleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Double;
			}
			else if (type == Converter.typeofInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int16;
			}
			else if (type == Converter.typeofInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int32;
			}
			else if (type == Converter.typeofInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.Int64;
			}
			else if (type == Converter.typeofSByteArray)
			{
				typeInformation = InternalPrimitiveTypeE.SByte;
			}
			else if (type == Converter.typeofSingleArray)
			{
				typeInformation = InternalPrimitiveTypeE.Single;
			}
			else if (type == Converter.typeofUInt16Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt16;
			}
			else if (type == Converter.typeofUInt32Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt32;
			}
			else if (type == Converter.typeofUInt64Array)
			{
				typeInformation = InternalPrimitiveTypeE.UInt64;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x000D83AC File Offset: 0x000D65AC
		private static void InitValueA()
		{
			string[] array = new string[Converter.primitiveTypeEnumLength];
			array[0] = null;
			array[1] = "Boolean";
			array[2] = "Byte";
			array[3] = "Char";
			array[5] = "Decimal";
			array[6] = "Double";
			array[7] = "Int16";
			array[8] = "Int32";
			array[9] = "Int64";
			array[10] = "SByte";
			array[11] = "Single";
			array[12] = "TimeSpan";
			array[13] = "DateTime";
			array[14] = "UInt16";
			array[15] = "UInt32";
			array[16] = "UInt64";
			Converter.valueA = array;
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x000D844E File Offset: 0x000D664E
		internal static string ToComType(InternalPrimitiveTypeE code)
		{
			if (Converter.valueA == null)
			{
				Converter.InitValueA();
			}
			return Converter.valueA[(int)code];
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x000D8468 File Offset: 0x000D6668
		private static void InitTypeCodeA()
		{
			TypeCode[] array = new TypeCode[Converter.primitiveTypeEnumLength];
			array[0] = TypeCode.Object;
			array[1] = TypeCode.Boolean;
			array[2] = TypeCode.Byte;
			array[3] = TypeCode.Char;
			array[5] = TypeCode.Decimal;
			array[6] = TypeCode.Double;
			array[7] = TypeCode.Int16;
			array[8] = TypeCode.Int32;
			array[9] = TypeCode.Int64;
			array[10] = TypeCode.SByte;
			array[11] = TypeCode.Single;
			array[12] = TypeCode.Object;
			array[13] = TypeCode.DateTime;
			array[14] = TypeCode.UInt16;
			array[15] = TypeCode.UInt32;
			array[16] = TypeCode.UInt64;
			Converter.typeCodeA = array;
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000D84D6 File Offset: 0x000D66D6
		internal static TypeCode ToTypeCode(InternalPrimitiveTypeE code)
		{
			if (Converter.typeCodeA == null)
			{
				Converter.InitTypeCodeA();
			}
			return Converter.typeCodeA[(int)code];
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000D84F0 File Offset: 0x000D66F0
		private static void InitCodeA()
		{
			Converter.codeA = new InternalPrimitiveTypeE[]
			{
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Boolean,
				InternalPrimitiveTypeE.Char,
				InternalPrimitiveTypeE.SByte,
				InternalPrimitiveTypeE.Byte,
				InternalPrimitiveTypeE.Int16,
				InternalPrimitiveTypeE.UInt16,
				InternalPrimitiveTypeE.Int32,
				InternalPrimitiveTypeE.UInt32,
				InternalPrimitiveTypeE.Int64,
				InternalPrimitiveTypeE.UInt64,
				InternalPrimitiveTypeE.Single,
				InternalPrimitiveTypeE.Double,
				InternalPrimitiveTypeE.Decimal,
				InternalPrimitiveTypeE.DateTime,
				InternalPrimitiveTypeE.Invalid,
				InternalPrimitiveTypeE.Invalid
			};
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x000D8568 File Offset: 0x000D6768
		internal static InternalPrimitiveTypeE ToPrimitiveTypeEnum(TypeCode typeCode)
		{
			if (Converter.codeA == null)
			{
				Converter.InitCodeA();
			}
			return Converter.codeA[(int)typeCode];
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000D8584 File Offset: 0x000D6784
		internal static object FromString(string value, InternalPrimitiveTypeE code)
		{
			object result;
			if (code != InternalPrimitiveTypeE.Invalid)
			{
				result = Convert.ChangeType(value, Converter.ToTypeCode(code), CultureInfo.InvariantCulture);
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000D85AC File Offset: 0x000D67AC
		// Note: this type is marked as 'beforefieldinit'.
		static Converter()
		{
		}

		// Token: 0x0400289F RID: 10399
		private static int primitiveTypeEnumLength = 17;

		// Token: 0x040028A0 RID: 10400
		private static volatile Type[] typeA;

		// Token: 0x040028A1 RID: 10401
		private static volatile Type[] arrayTypeA;

		// Token: 0x040028A2 RID: 10402
		private static volatile string[] valueA;

		// Token: 0x040028A3 RID: 10403
		private static volatile TypeCode[] typeCodeA;

		// Token: 0x040028A4 RID: 10404
		private static volatile InternalPrimitiveTypeE[] codeA;

		// Token: 0x040028A5 RID: 10405
		internal static Type typeofISerializable = typeof(ISerializable);

		// Token: 0x040028A6 RID: 10406
		internal static Type typeofString = typeof(string);

		// Token: 0x040028A7 RID: 10407
		internal static Type typeofConverter = typeof(Converter);

		// Token: 0x040028A8 RID: 10408
		internal static Type typeofBoolean = typeof(bool);

		// Token: 0x040028A9 RID: 10409
		internal static Type typeofByte = typeof(byte);

		// Token: 0x040028AA RID: 10410
		internal static Type typeofChar = typeof(char);

		// Token: 0x040028AB RID: 10411
		internal static Type typeofDecimal = typeof(decimal);

		// Token: 0x040028AC RID: 10412
		internal static Type typeofDouble = typeof(double);

		// Token: 0x040028AD RID: 10413
		internal static Type typeofInt16 = typeof(short);

		// Token: 0x040028AE RID: 10414
		internal static Type typeofInt32 = typeof(int);

		// Token: 0x040028AF RID: 10415
		internal static Type typeofInt64 = typeof(long);

		// Token: 0x040028B0 RID: 10416
		internal static Type typeofSByte = typeof(sbyte);

		// Token: 0x040028B1 RID: 10417
		internal static Type typeofSingle = typeof(float);

		// Token: 0x040028B2 RID: 10418
		internal static Type typeofTimeSpan = typeof(TimeSpan);

		// Token: 0x040028B3 RID: 10419
		internal static Type typeofDateTime = typeof(DateTime);

		// Token: 0x040028B4 RID: 10420
		internal static Type typeofUInt16 = typeof(ushort);

		// Token: 0x040028B5 RID: 10421
		internal static Type typeofUInt32 = typeof(uint);

		// Token: 0x040028B6 RID: 10422
		internal static Type typeofUInt64 = typeof(ulong);

		// Token: 0x040028B7 RID: 10423
		internal static Type typeofObject = typeof(object);

		// Token: 0x040028B8 RID: 10424
		internal static Type typeofSystemVoid = typeof(void);

		// Token: 0x040028B9 RID: 10425
		internal static Assembly urtAssembly = Assembly.GetAssembly(Converter.typeofString);

		// Token: 0x040028BA RID: 10426
		internal static string urtAssemblyString = Converter.urtAssembly.FullName;

		// Token: 0x040028BB RID: 10427
		internal static Type typeofTypeArray = typeof(Type[]);

		// Token: 0x040028BC RID: 10428
		internal static Type typeofObjectArray = typeof(object[]);

		// Token: 0x040028BD RID: 10429
		internal static Type typeofStringArray = typeof(string[]);

		// Token: 0x040028BE RID: 10430
		internal static Type typeofBooleanArray = typeof(bool[]);

		// Token: 0x040028BF RID: 10431
		internal static Type typeofByteArray = typeof(byte[]);

		// Token: 0x040028C0 RID: 10432
		internal static Type typeofCharArray = typeof(char[]);

		// Token: 0x040028C1 RID: 10433
		internal static Type typeofDecimalArray = typeof(decimal[]);

		// Token: 0x040028C2 RID: 10434
		internal static Type typeofDoubleArray = typeof(double[]);

		// Token: 0x040028C3 RID: 10435
		internal static Type typeofInt16Array = typeof(short[]);

		// Token: 0x040028C4 RID: 10436
		internal static Type typeofInt32Array = typeof(int[]);

		// Token: 0x040028C5 RID: 10437
		internal static Type typeofInt64Array = typeof(long[]);

		// Token: 0x040028C6 RID: 10438
		internal static Type typeofSByteArray = typeof(sbyte[]);

		// Token: 0x040028C7 RID: 10439
		internal static Type typeofSingleArray = typeof(float[]);

		// Token: 0x040028C8 RID: 10440
		internal static Type typeofTimeSpanArray = typeof(TimeSpan[]);

		// Token: 0x040028C9 RID: 10441
		internal static Type typeofDateTimeArray = typeof(DateTime[]);

		// Token: 0x040028CA RID: 10442
		internal static Type typeofUInt16Array = typeof(ushort[]);

		// Token: 0x040028CB RID: 10443
		internal static Type typeofUInt32Array = typeof(uint[]);

		// Token: 0x040028CC RID: 10444
		internal static Type typeofUInt64Array = typeof(ulong[]);

		// Token: 0x040028CD RID: 10445
		internal static Type typeofMarshalByRefObject = typeof(MarshalByRefObject);
	}
}
