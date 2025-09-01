using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	// Token: 0x02000031 RID: 49
	internal static class ContractNameServices
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005086 File Offset: 0x00003286
		private static Dictionary<Type, string> TypeIdentityCache
		{
			get
			{
				return ContractNameServices.typeIdentityCache = (ContractNameServices.typeIdentityCache ?? new Dictionary<Type, string>());
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000509C File Offset: 0x0000329C
		internal static string GetTypeIdentity(Type type)
		{
			return ContractNameServices.GetTypeIdentity(type, true);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000050A8 File Offset: 0x000032A8
		internal static string GetTypeIdentity(Type type, bool formatGenericName)
		{
			Assumes.NotNull<Type>(type);
			string text = null;
			if (!ContractNameServices.TypeIdentityCache.TryGetValue(type, out text))
			{
				if (!type.IsAbstract && type.IsSubclassOf(typeof(Delegate)))
				{
					text = ContractNameServices.GetTypeIdentityFromMethod(type.GetMethod("Invoke"));
				}
				else if (type.IsGenericParameter)
				{
					StringBuilder stringBuilder = new StringBuilder();
					ContractNameServices.WriteTypeArgument(stringBuilder, false, type, formatGenericName);
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					text = stringBuilder.ToString();
				}
				else
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					ContractNameServices.WriteTypeWithNamespace(stringBuilder2, type, formatGenericName);
					text = stringBuilder2.ToString();
				}
				Assumes.IsTrue(!string.IsNullOrEmpty(text));
				ContractNameServices.TypeIdentityCache.Add(type, text);
			}
			return text;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005158 File Offset: 0x00003358
		internal static string GetTypeIdentityFromMethod(MethodInfo method)
		{
			return ContractNameServices.GetTypeIdentityFromMethod(method, true);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005164 File Offset: 0x00003364
		internal static string GetTypeIdentityFromMethod(MethodInfo method, bool formatGenericName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ContractNameServices.WriteTypeWithNamespace(stringBuilder, method.ReturnType, formatGenericName);
			stringBuilder.Append("(");
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(",");
				}
				ContractNameServices.WriteTypeWithNamespace(stringBuilder, parameters[i].ParameterType, formatGenericName);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000051D5 File Offset: 0x000033D5
		private static void WriteTypeWithNamespace(StringBuilder typeName, Type type, bool formatGenericName)
		{
			if (!string.IsNullOrEmpty(type.Namespace))
			{
				typeName.Append(type.Namespace);
				typeName.Append('.');
			}
			ContractNameServices.WriteType(typeName, type, formatGenericName);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005204 File Offset: 0x00003404
		private static void WriteType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			if (type.IsGenericType)
			{
				Queue<Type> queue = new Queue<Type>(type.GetGenericArguments());
				ContractNameServices.WriteGenericType(typeName, type, type.IsGenericTypeDefinition, queue, formatGenericName);
				Assumes.IsTrue(queue.Count == 0, "Expecting genericTypeArguments queue to be empty.");
				return;
			}
			ContractNameServices.WriteNonGenericType(typeName, type, formatGenericName);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005250 File Offset: 0x00003450
		private static void WriteNonGenericType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			if (type.DeclaringType != null)
			{
				ContractNameServices.WriteType(typeName, type.DeclaringType, formatGenericName);
				typeName.Append('+');
			}
			if (type.IsArray)
			{
				ContractNameServices.WriteArrayType(typeName, type, formatGenericName);
				return;
			}
			if (type.IsPointer)
			{
				ContractNameServices.WritePointerType(typeName, type, formatGenericName);
				return;
			}
			if (type.IsByRef)
			{
				ContractNameServices.WriteByRefType(typeName, type, formatGenericName);
				return;
			}
			typeName.Append(type.Name);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000052C4 File Offset: 0x000034C4
		private static void WriteArrayType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			Type type2 = ContractNameServices.FindArrayElementType(type);
			ContractNameServices.WriteType(typeName, type2, formatGenericName);
			Type type3 = type;
			do
			{
				ContractNameServices.WriteArrayTypeDimensions(typeName, type3);
			}
			while ((type3 = type3.GetElementType()) != null && type3.IsArray);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005301 File Offset: 0x00003501
		private static void WritePointerType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			ContractNameServices.WriteType(typeName, type.GetElementType(), formatGenericName);
			typeName.Append('*');
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005319 File Offset: 0x00003519
		private static void WriteByRefType(StringBuilder typeName, Type type, bool formatGenericName)
		{
			ContractNameServices.WriteType(typeName, type.GetElementType(), formatGenericName);
			typeName.Append('&');
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005334 File Offset: 0x00003534
		private static void WriteArrayTypeDimensions(StringBuilder typeName, Type type)
		{
			typeName.Append('[');
			int arrayRank = type.GetArrayRank();
			for (int i = 1; i < arrayRank; i++)
			{
				typeName.Append(',');
			}
			typeName.Append(']');
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005370 File Offset: 0x00003570
		private static void WriteGenericType(StringBuilder typeName, Type type, bool isDefinition, Queue<Type> genericTypeArguments, bool formatGenericName)
		{
			if (type.DeclaringType != null)
			{
				if (type.DeclaringType.IsGenericType)
				{
					ContractNameServices.WriteGenericType(typeName, type.DeclaringType, isDefinition, genericTypeArguments, formatGenericName);
				}
				else
				{
					ContractNameServices.WriteNonGenericType(typeName, type.DeclaringType, formatGenericName);
				}
				typeName.Append('+');
			}
			ContractNameServices.WriteGenericTypeName(typeName, type, isDefinition, genericTypeArguments, formatGenericName);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000053CC File Offset: 0x000035CC
		private static void WriteGenericTypeName(StringBuilder typeName, Type type, bool isDefinition, Queue<Type> genericTypeArguments, bool formatGenericName)
		{
			Assumes.IsTrue(type.IsGenericType, "Expecting type to be a generic type");
			int genericArity = ContractNameServices.GetGenericArity(type);
			string value = ContractNameServices.FindGenericTypeName(type.GetGenericTypeDefinition().Name);
			typeName.Append(value);
			ContractNameServices.WriteTypeArgumentsString(typeName, genericArity, isDefinition, genericTypeArguments, formatGenericName);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005414 File Offset: 0x00003614
		private static void WriteTypeArgumentsString(StringBuilder typeName, int argumentsCount, bool isDefinition, Queue<Type> genericTypeArguments, bool formatGenericName)
		{
			if (argumentsCount == 0)
			{
				return;
			}
			typeName.Append('(');
			for (int i = 0; i < argumentsCount; i++)
			{
				Assumes.IsTrue(genericTypeArguments.Count > 0, "Expecting genericTypeArguments to contain at least one Type");
				Type genericTypeArgument = genericTypeArguments.Dequeue();
				ContractNameServices.WriteTypeArgument(typeName, isDefinition, genericTypeArgument, formatGenericName);
			}
			typeName.Remove(typeName.Length - 1, 1);
			typeName.Append(')');
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005478 File Offset: 0x00003678
		private static void WriteTypeArgument(StringBuilder typeName, bool isDefinition, Type genericTypeArgument, bool formatGenericName)
		{
			if (!isDefinition && !genericTypeArgument.IsGenericParameter)
			{
				ContractNameServices.WriteTypeWithNamespace(typeName, genericTypeArgument, formatGenericName);
			}
			if (formatGenericName && genericTypeArgument.IsGenericParameter)
			{
				typeName.Append('{');
				typeName.Append(genericTypeArgument.GenericParameterPosition);
				typeName.Append('}');
			}
			typeName.Append(',');
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000054CC File Offset: 0x000036CC
		internal static void WriteCustomModifiers(StringBuilder typeName, string customKeyword, Type[] types, bool formatGenericName)
		{
			typeName.Append(' ');
			typeName.Append(customKeyword);
			Queue<Type> queue = new Queue<Type>(types);
			ContractNameServices.WriteTypeArgumentsString(typeName, types.Length, false, queue, formatGenericName);
			Assumes.IsTrue(queue.Count == 0, "Expecting genericTypeArguments queue to be empty.");
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005510 File Offset: 0x00003710
		private static Type FindArrayElementType(Type type)
		{
			Type type2 = type;
			while ((type2 = type2.GetElementType()) != null && type2.IsArray)
			{
			}
			return type2;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005538 File Offset: 0x00003738
		private static string FindGenericTypeName(string genericName)
		{
			int num = genericName.IndexOf('`');
			if (num > -1)
			{
				genericName = genericName.Substring(0, num);
			}
			return genericName;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005560 File Offset: 0x00003760
		private static int GetGenericArity(Type type)
		{
			if (type.DeclaringType == null)
			{
				return type.GetGenericArguments().Length;
			}
			int num = type.DeclaringType.GetGenericArguments().Length;
			int num2 = type.GetGenericArguments().Length;
			Assumes.IsTrue(num2 >= num);
			return num2 - num;
		}

		// Token: 0x0400009A RID: 154
		private const char NamespaceSeparator = '.';

		// Token: 0x0400009B RID: 155
		private const char ArrayOpeningBracket = '[';

		// Token: 0x0400009C RID: 156
		private const char ArrayClosingBracket = ']';

		// Token: 0x0400009D RID: 157
		private const char ArraySeparator = ',';

		// Token: 0x0400009E RID: 158
		private const char PointerSymbol = '*';

		// Token: 0x0400009F RID: 159
		private const char ReferenceSymbol = '&';

		// Token: 0x040000A0 RID: 160
		private const char GenericArityBackQuote = '`';

		// Token: 0x040000A1 RID: 161
		private const char NestedClassSeparator = '+';

		// Token: 0x040000A2 RID: 162
		private const char ContractNameGenericOpeningBracket = '(';

		// Token: 0x040000A3 RID: 163
		private const char ContractNameGenericClosingBracket = ')';

		// Token: 0x040000A4 RID: 164
		private const char ContractNameGenericArgumentSeparator = ',';

		// Token: 0x040000A5 RID: 165
		private const char CustomModifiersSeparator = ' ';

		// Token: 0x040000A6 RID: 166
		private const char GenericFormatOpeningBracket = '{';

		// Token: 0x040000A7 RID: 167
		private const char GenericFormatClosingBracket = '}';

		// Token: 0x040000A8 RID: 168
		[ThreadStatic]
		private static Dictionary<Type, string> typeIdentityCache;
	}
}
