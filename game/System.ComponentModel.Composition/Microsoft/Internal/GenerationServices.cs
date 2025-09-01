using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Microsoft.Internal
{
	// Token: 0x02000007 RID: 7
	internal static class GenerationServices
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000021AF File Offset: 0x000003AF
		public static ILGenerator CreateGeneratorForPublicConstructor(this TypeBuilder typeBuilder, Type[] ctrArgumentTypes)
		{
			ILGenerator ilgenerator = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, ctrArgumentTypes).GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, GenerationServices.ObjectCtor);
			return ilgenerator;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021DC File Offset: 0x000003DC
		public static void LoadValue(this ILGenerator ilGenerator, object value)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			if (value == null)
			{
				ilGenerator.LoadNull();
				return;
			}
			Type type = value.GetType();
			object obj = value;
			if (type.IsEnum)
			{
				obj = Convert.ChangeType(value, Enum.GetUnderlyingType(type), null);
				type = obj.GetType();
			}
			if (type == GenerationServices.StringType)
			{
				ilGenerator.LoadString((string)obj);
				return;
			}
			if (GenerationServices.TypeType.IsAssignableFrom(type))
			{
				ilGenerator.LoadTypeOf((Type)obj);
				return;
			}
			if (GenerationServices.IEnumerableType.IsAssignableFrom(type))
			{
				ilGenerator.LoadEnumerable((IEnumerable)obj);
				return;
			}
			if (type == GenerationServices.CharType || type == GenerationServices.BooleanType || type == GenerationServices.ByteType || type == GenerationServices.SByteType || type == GenerationServices.Int16Type || type == GenerationServices.UInt16Type || type == GenerationServices.Int32Type)
			{
				ilGenerator.LoadInt((int)Convert.ChangeType(obj, typeof(int), CultureInfo.InvariantCulture));
				return;
			}
			if (type == GenerationServices.UInt32Type)
			{
				ilGenerator.LoadInt((int)((uint)obj));
				return;
			}
			if (type == GenerationServices.Int64Type)
			{
				ilGenerator.LoadLong((long)obj);
				return;
			}
			if (type == GenerationServices.UInt64Type)
			{
				ilGenerator.LoadLong((long)((ulong)obj));
				return;
			}
			if (type == GenerationServices.SingleType)
			{
				ilGenerator.LoadFloat((float)obj);
				return;
			}
			if (type == GenerationServices.DoubleType)
			{
				ilGenerator.LoadDouble((double)obj);
				return;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidMetadataValue, value.GetType().FullName));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000238C File Offset: 0x0000058C
		public static void AddItemToLocalDictionary(this ILGenerator ilGenerator, LocalBuilder dictionary, object key, object value)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			Assumes.NotNull<LocalBuilder>(dictionary);
			Assumes.NotNull<object>(key);
			Assumes.NotNull<object>(value);
			ilGenerator.Emit(OpCodes.Ldloc, dictionary);
			ilGenerator.LoadValue(key);
			ilGenerator.LoadValue(value);
			ilGenerator.Emit(OpCodes.Callvirt, GenerationServices.DictionaryAdd);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023DC File Offset: 0x000005DC
		public static void AddLocalToLocalDictionary(this ILGenerator ilGenerator, LocalBuilder dictionary, object key, LocalBuilder value)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			Assumes.NotNull<LocalBuilder>(dictionary);
			Assumes.NotNull<object>(key);
			Assumes.NotNull<LocalBuilder>(value);
			ilGenerator.Emit(OpCodes.Ldloc, dictionary);
			ilGenerator.LoadValue(key);
			ilGenerator.Emit(OpCodes.Ldloc, value);
			ilGenerator.Emit(OpCodes.Callvirt, GenerationServices.DictionaryAdd);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002430 File Offset: 0x00000630
		public static void GetExceptionDataAndStoreInLocal(this ILGenerator ilGenerator, LocalBuilder exception, LocalBuilder dataStore)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			Assumes.NotNull<LocalBuilder>(exception);
			Assumes.NotNull<LocalBuilder>(dataStore);
			ilGenerator.Emit(OpCodes.Ldloc, exception);
			ilGenerator.Emit(OpCodes.Callvirt, GenerationServices.ExceptionGetData);
			ilGenerator.Emit(OpCodes.Stloc, dataStore);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000246C File Offset: 0x0000066C
		private static void LoadEnumerable(this ILGenerator ilGenerator, IEnumerable enumerable)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			Assumes.NotNull<IEnumerable>(enumerable);
			Type type = null;
			Type type2;
			if (ReflectionServices.TryGetGenericInterfaceType(enumerable.GetType(), GenerationServices.IEnumerableTypeofT, out type))
			{
				type2 = type.GetGenericArguments()[0];
			}
			else
			{
				type2 = typeof(object);
			}
			Type localType = type2.MakeArrayType();
			LocalBuilder local = ilGenerator.DeclareLocal(localType);
			ilGenerator.LoadInt(enumerable.Cast<object>().Count<object>());
			ilGenerator.Emit(OpCodes.Newarr, type2);
			ilGenerator.Emit(OpCodes.Stloc, local);
			int num = 0;
			foreach (object obj in enumerable)
			{
				ilGenerator.Emit(OpCodes.Ldloc, local);
				ilGenerator.LoadInt(num);
				ilGenerator.LoadValue(obj);
				if (GenerationServices.IsBoxingRequiredForValue(obj) && !type2.IsValueType)
				{
					ilGenerator.Emit(OpCodes.Box, obj.GetType());
				}
				ilGenerator.Emit(OpCodes.Stelem, type2);
				num++;
			}
			ilGenerator.Emit(OpCodes.Ldloc, local);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002590 File Offset: 0x00000790
		private static bool IsBoxingRequiredForValue(object value)
		{
			return value != null && value.GetType().IsValueType;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025A2 File Offset: 0x000007A2
		private static void LoadNull(this ILGenerator ilGenerator)
		{
			ilGenerator.Emit(OpCodes.Ldnull);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025AF File Offset: 0x000007AF
		private static void LoadString(this ILGenerator ilGenerator, string s)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			if (s == null)
			{
				ilGenerator.LoadNull();
				return;
			}
			ilGenerator.Emit(OpCodes.Ldstr, s);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025CD File Offset: 0x000007CD
		private static void LoadInt(this ILGenerator ilGenerator, int value)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldc_I4, value);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025E1 File Offset: 0x000007E1
		private static void LoadLong(this ILGenerator ilGenerator, long value)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldc_I8, value);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025F5 File Offset: 0x000007F5
		private static void LoadFloat(this ILGenerator ilGenerator, float value)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldc_R4, value);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002609 File Offset: 0x00000809
		private static void LoadDouble(this ILGenerator ilGenerator, double value)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldc_R8, value);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000261D File Offset: 0x0000081D
		private static void LoadTypeOf(this ILGenerator ilGenerator, Type type)
		{
			Assumes.NotNull<ILGenerator>(ilGenerator);
			ilGenerator.Emit(OpCodes.Ldtoken, type);
			ilGenerator.EmitCall(OpCodes.Call, GenerationServices._typeGetTypeFromHandleMethod, null);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002644 File Offset: 0x00000844
		// Note: this type is marked as 'beforefieldinit'.
		static GenerationServices()
		{
		}

		// Token: 0x0400002A RID: 42
		private static readonly MethodInfo _typeGetTypeFromHandleMethod = typeof(Type).GetMethod("GetTypeFromHandle");

		// Token: 0x0400002B RID: 43
		private static readonly Type TypeType = typeof(Type);

		// Token: 0x0400002C RID: 44
		private static readonly Type StringType = typeof(string);

		// Token: 0x0400002D RID: 45
		private static readonly Type CharType = typeof(char);

		// Token: 0x0400002E RID: 46
		private static readonly Type BooleanType = typeof(bool);

		// Token: 0x0400002F RID: 47
		private static readonly Type ByteType = typeof(byte);

		// Token: 0x04000030 RID: 48
		private static readonly Type SByteType = typeof(sbyte);

		// Token: 0x04000031 RID: 49
		private static readonly Type Int16Type = typeof(short);

		// Token: 0x04000032 RID: 50
		private static readonly Type UInt16Type = typeof(ushort);

		// Token: 0x04000033 RID: 51
		private static readonly Type Int32Type = typeof(int);

		// Token: 0x04000034 RID: 52
		private static readonly Type UInt32Type = typeof(uint);

		// Token: 0x04000035 RID: 53
		private static readonly Type Int64Type = typeof(long);

		// Token: 0x04000036 RID: 54
		private static readonly Type UInt64Type = typeof(ulong);

		// Token: 0x04000037 RID: 55
		private static readonly Type DoubleType = typeof(double);

		// Token: 0x04000038 RID: 56
		private static readonly Type SingleType = typeof(float);

		// Token: 0x04000039 RID: 57
		private static readonly Type IEnumerableTypeofT = typeof(IEnumerable<>);

		// Token: 0x0400003A RID: 58
		private static readonly Type IEnumerableType = typeof(IEnumerable);

		// Token: 0x0400003B RID: 59
		private static readonly MethodInfo ExceptionGetData = typeof(Exception).GetProperty("Data").GetGetMethod();

		// Token: 0x0400003C RID: 60
		private static readonly MethodInfo DictionaryAdd = typeof(IDictionary).GetMethod("Add");

		// Token: 0x0400003D RID: 61
		private static readonly ConstructorInfo ObjectCtor = typeof(object).GetConstructor(Type.EmptyTypes);
	}
}
