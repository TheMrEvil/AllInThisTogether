using System;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004F RID: 79
	internal class SerializationHelperSql9
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00003D93 File Offset: 0x00001F93
		private SerializationHelperSql9()
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000105A4 File Offset: 0x0000E7A4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int SizeInBytes(Type t)
		{
			return SerializationHelperSql9.SizeInBytes(Activator.CreateInstance(t));
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000105B4 File Offset: 0x0000E7B4
		internal static int SizeInBytes(object instance)
		{
			SerializationHelperSql9.GetFormat(instance.GetType());
			DummyStream dummyStream = new DummyStream();
			SerializationHelperSql9.GetSerializer(instance.GetType()).Serialize(dummyStream, instance);
			return (int)dummyStream.Length;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000105EC File Offset: 0x0000E7EC
		internal static void Serialize(Stream s, object instance)
		{
			SerializationHelperSql9.GetSerializer(instance.GetType()).Serialize(s, instance);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00010600 File Offset: 0x0000E800
		internal static object Deserialize(Stream s, Type resultType)
		{
			return SerializationHelperSql9.GetSerializer(resultType).Deserialize(s);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001060E File Offset: 0x0000E80E
		private static Format GetFormat(Type t)
		{
			return SerializationHelperSql9.GetUdtAttribute(t).Format;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001061C File Offset: 0x0000E81C
		private static Serializer GetSerializer(Type t)
		{
			if (SerializationHelperSql9.s_types2Serializers == null)
			{
				SerializationHelperSql9.s_types2Serializers = new Hashtable();
			}
			Serializer serializer = (Serializer)SerializationHelperSql9.s_types2Serializers[t];
			if (serializer == null)
			{
				serializer = SerializationHelperSql9.GetNewSerializer(t);
				SerializationHelperSql9.s_types2Serializers[t] = serializer;
			}
			return serializer;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00010664 File Offset: 0x0000E864
		internal static int GetUdtMaxLength(Type t)
		{
			SqlUdtInfo fromType = SqlUdtInfo.GetFromType(t);
			if (Format.Native == fromType.SerializationFormat)
			{
				return SerializationHelperSql9.SizeInBytes(t);
			}
			return fromType.MaxByteSize;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001068E File Offset: 0x0000E88E
		private static object[] GetCustomAttributes(Type t)
		{
			return t.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000106A4 File Offset: 0x0000E8A4
		internal static SqlUserDefinedTypeAttribute GetUdtAttribute(Type t)
		{
			object[] customAttributes = SerializationHelperSql9.GetCustomAttributes(t);
			if (customAttributes != null && customAttributes.Length == 1)
			{
				return (SqlUserDefinedTypeAttribute)customAttributes[0];
			}
			throw InvalidUdtException.Create(t, "no UDT attribute");
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000106DC File Offset: 0x0000E8DC
		private static Serializer GetNewSerializer(Type t)
		{
			SerializationHelperSql9.GetUdtAttribute(t);
			Format format = SerializationHelperSql9.GetFormat(t);
			switch (format)
			{
			case Format.Native:
				return new NormalizedSerializer(t);
			case Format.UserDefined:
				return new BinarySerializeSerializer(t);
			}
			throw ADP.InvalidUserDefinedTypeSerializationFormat(format);
		}

		// Token: 0x04000534 RID: 1332
		[ThreadStatic]
		private static Hashtable s_types2Serializers;
	}
}
