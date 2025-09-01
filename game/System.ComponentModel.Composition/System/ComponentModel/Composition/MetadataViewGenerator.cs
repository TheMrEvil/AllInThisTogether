using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Security;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	// Token: 0x0200004D RID: 77
	internal static class MetadataViewGenerator
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00005FA2 File Offset: 0x000041A2
		private static AssemblyBuilder CreateProxyAssemblyBuilder(ConstructorInfo constructorInfo)
		{
			return AppDomain.CurrentDomain.DefineDynamicAssembly(MetadataViewGenerator.ProxyAssemblyName, AssemblyBuilderAccess.Run);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00005FB4 File Offset: 0x000041B4
		private static ModuleBuilder GetProxyModuleBuilder(bool requiresCritical)
		{
			if (MetadataViewGenerator.transparentProxyModuleBuilder == null)
			{
				MetadataViewGenerator.transparentProxyModuleBuilder = MetadataViewGenerator.CreateProxyAssemblyBuilder(typeof(SecurityTransparentAttribute).GetConstructor(Type.EmptyTypes)).DefineDynamicModule("MetadataViewProxiesModule");
			}
			return MetadataViewGenerator.transparentProxyModuleBuilder;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00005FF0 File Offset: 0x000041F0
		public static Type GenerateView(Type viewType)
		{
			Assumes.NotNull<Type>(viewType);
			Assumes.IsTrue(viewType.IsInterface);
			Type type;
			bool flag;
			using (new ReadLock(MetadataViewGenerator._lock))
			{
				flag = MetadataViewGenerator._proxies.TryGetValue(viewType, out type);
			}
			if (!flag)
			{
				Type type2 = MetadataViewGenerator.GenerateInterfaceViewProxyType(viewType);
				Assumes.NotNull<Type>(type2);
				using (new WriteLock(MetadataViewGenerator._lock))
				{
					if (!MetadataViewGenerator._proxies.TryGetValue(viewType, out type))
					{
						type = type2;
						MetadataViewGenerator._proxies.Add(viewType, type);
					}
				}
			}
			return type;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000060A0 File Offset: 0x000042A0
		private static void GenerateLocalAssignmentFromDefaultAttribute(this ILGenerator IL, DefaultValueAttribute[] attrs, LocalBuilder local)
		{
			if (attrs.Length != 0)
			{
				DefaultValueAttribute defaultValueAttribute = attrs[0];
				IL.LoadValue(defaultValueAttribute.Value);
				if (defaultValueAttribute.Value != null && defaultValueAttribute.Value.GetType().IsValueType)
				{
					IL.Emit(OpCodes.Box, defaultValueAttribute.Value.GetType());
				}
				IL.Emit(OpCodes.Stloc, local);
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006100 File Offset: 0x00004300
		private static void GenerateFieldAssignmentFromLocalValue(this ILGenerator IL, LocalBuilder local, FieldBuilder field)
		{
			IL.Emit(OpCodes.Ldarg_0);
			IL.Emit(OpCodes.Ldloc, local);
			IL.Emit(field.FieldType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, field.FieldType);
			IL.Emit(OpCodes.Stfld, field);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006155 File Offset: 0x00004355
		private static void GenerateLocalAssignmentFromFlag(this ILGenerator IL, LocalBuilder local, bool flag)
		{
			IL.Emit(flag ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
			IL.Emit(OpCodes.Stloc, local);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006178 File Offset: 0x00004378
		private static Type GenerateInterfaceViewProxyType(Type viewType)
		{
			Type[] interfaces = new Type[]
			{
				viewType
			};
			TypeBuilder typeBuilder = MetadataViewGenerator.GetProxyModuleBuilder(false).DefineType(string.Format(CultureInfo.InvariantCulture, "_proxy_{0}_{1}", viewType.FullName, Guid.NewGuid()), TypeAttributes.Public, typeof(object), interfaces);
			ILGenerator ilgenerator = typeBuilder.CreateGeneratorForPublicConstructor(MetadataViewGenerator.CtorArgumentTypes);
			LocalBuilder localBuilder = ilgenerator.DeclareLocal(typeof(Exception));
			LocalBuilder localBuilder2 = ilgenerator.DeclareLocal(typeof(IDictionary));
			LocalBuilder localBuilder3 = ilgenerator.DeclareLocal(typeof(Type));
			LocalBuilder localBuilder4 = ilgenerator.DeclareLocal(typeof(object));
			LocalBuilder local = ilgenerator.DeclareLocal(typeof(bool));
			Label label = ilgenerator.BeginExceptionBlock();
			foreach (PropertyInfo propertyInfo in viewType.GetAllProperties())
			{
				string fieldName = string.Format(CultureInfo.InvariantCulture, "_{0}_{1}", propertyInfo.Name, Guid.NewGuid());
				string text = string.Format(CultureInfo.InvariantCulture, "{0}", propertyInfo.Name);
				Type[] parameterTypes = new Type[]
				{
					propertyInfo.PropertyType
				};
				Type[] returnTypeOptionalCustomModifiers = null;
				Type[] returnTypeRequiredCustomModifiers = null;
				FieldBuilder field = typeBuilder.DefineField(fieldName, propertyInfo.PropertyType, FieldAttributes.Private);
				PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(text, PropertyAttributes.None, propertyInfo.PropertyType, parameterTypes);
				Label label2 = ilgenerator.BeginExceptionBlock();
				DefaultValueAttribute[] attributes = propertyInfo.GetAttributes(false);
				if (attributes.Length != 0)
				{
					ilgenerator.BeginExceptionBlock();
				}
				Label label3 = ilgenerator.DefineLabel();
				ilgenerator.GenerateLocalAssignmentFromFlag(local, true);
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(OpCodes.Ldstr, propertyInfo.Name);
				ilgenerator.Emit(OpCodes.Ldloca, localBuilder4);
				ilgenerator.Emit(OpCodes.Callvirt, MetadataViewGenerator._mdvDictionaryTryGet);
				ilgenerator.Emit(OpCodes.Brtrue, label3);
				ilgenerator.GenerateLocalAssignmentFromFlag(local, false);
				ilgenerator.GenerateLocalAssignmentFromDefaultAttribute(attributes, localBuilder4);
				ilgenerator.MarkLabel(label3);
				ilgenerator.GenerateFieldAssignmentFromLocalValue(localBuilder4, field);
				ilgenerator.Emit(OpCodes.Leave, label2);
				if (attributes.Length != 0)
				{
					ilgenerator.BeginCatchBlock(typeof(InvalidCastException));
					Label label4 = ilgenerator.DefineLabel();
					ilgenerator.Emit(OpCodes.Ldloc, local);
					ilgenerator.Emit(OpCodes.Brtrue, label4);
					ilgenerator.Emit(OpCodes.Rethrow);
					ilgenerator.MarkLabel(label4);
					ilgenerator.GenerateLocalAssignmentFromDefaultAttribute(attributes, localBuilder4);
					ilgenerator.GenerateFieldAssignmentFromLocalValue(localBuilder4, field);
					ilgenerator.EndExceptionBlock();
				}
				ilgenerator.BeginCatchBlock(typeof(NullReferenceException));
				ilgenerator.Emit(OpCodes.Stloc, localBuilder);
				ilgenerator.GetExceptionDataAndStoreInLocal(localBuilder, localBuilder2);
				ilgenerator.AddItemToLocalDictionary(localBuilder2, "MetadataItemKey", text);
				ilgenerator.AddItemToLocalDictionary(localBuilder2, "MetadataItemTargetType", propertyInfo.PropertyType);
				ilgenerator.Emit(OpCodes.Rethrow);
				ilgenerator.BeginCatchBlock(typeof(InvalidCastException));
				ilgenerator.Emit(OpCodes.Stloc, localBuilder);
				ilgenerator.GetExceptionDataAndStoreInLocal(localBuilder, localBuilder2);
				ilgenerator.AddItemToLocalDictionary(localBuilder2, "MetadataItemKey", text);
				ilgenerator.AddItemToLocalDictionary(localBuilder2, "MetadataItemTargetType", propertyInfo.PropertyType);
				ilgenerator.Emit(OpCodes.Rethrow);
				ilgenerator.EndExceptionBlock();
				if (propertyInfo.CanWrite)
				{
					throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Strings.InvalidSetterOnMetadataField, viewType, text));
				}
				if (propertyInfo.CanRead)
				{
					MethodBuilder methodBuilder = typeBuilder.DefineMethod(string.Format(CultureInfo.InvariantCulture, "get_{0}", text), MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.SpecialName, CallingConventions.HasThis, propertyInfo.PropertyType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, Type.EmptyTypes, null, null);
					typeBuilder.DefineMethodOverride(methodBuilder, propertyInfo.GetGetMethod());
					ILGenerator ilgenerator2 = methodBuilder.GetILGenerator();
					ilgenerator2.Emit(OpCodes.Ldarg_0);
					ilgenerator2.Emit(OpCodes.Ldfld, field);
					ilgenerator2.Emit(OpCodes.Ret);
					propertyBuilder.SetGetMethod(methodBuilder);
				}
			}
			ilgenerator.Emit(OpCodes.Leave, label);
			ilgenerator.BeginCatchBlock(typeof(NullReferenceException));
			ilgenerator.Emit(OpCodes.Stloc, localBuilder);
			ilgenerator.GetExceptionDataAndStoreInLocal(localBuilder, localBuilder2);
			ilgenerator.AddItemToLocalDictionary(localBuilder2, "MetadataViewType", viewType);
			ilgenerator.Emit(OpCodes.Rethrow);
			ilgenerator.BeginCatchBlock(typeof(InvalidCastException));
			ilgenerator.Emit(OpCodes.Stloc, localBuilder);
			ilgenerator.GetExceptionDataAndStoreInLocal(localBuilder, localBuilder2);
			ilgenerator.Emit(OpCodes.Ldloc, localBuilder4);
			ilgenerator.Emit(OpCodes.Call, MetadataViewGenerator.ObjectGetType);
			ilgenerator.Emit(OpCodes.Stloc, localBuilder3);
			ilgenerator.AddItemToLocalDictionary(localBuilder2, "MetadataViewType", viewType);
			ilgenerator.AddLocalToLocalDictionary(localBuilder2, "MetadataItemSourceType", localBuilder3);
			ilgenerator.AddLocalToLocalDictionary(localBuilder2, "MetadataItemValue", localBuilder4);
			ilgenerator.Emit(OpCodes.Rethrow);
			ilgenerator.EndExceptionBlock();
			ilgenerator.Emit(OpCodes.Ret);
			return typeBuilder.CreateType();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000663C File Offset: 0x0000483C
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataViewGenerator()
		{
		}

		// Token: 0x040000D6 RID: 214
		public const string MetadataViewType = "MetadataViewType";

		// Token: 0x040000D7 RID: 215
		public const string MetadataItemKey = "MetadataItemKey";

		// Token: 0x040000D8 RID: 216
		public const string MetadataItemTargetType = "MetadataItemTargetType";

		// Token: 0x040000D9 RID: 217
		public const string MetadataItemSourceType = "MetadataItemSourceType";

		// Token: 0x040000DA RID: 218
		public const string MetadataItemValue = "MetadataItemValue";

		// Token: 0x040000DB RID: 219
		private static Lock _lock = new Lock();

		// Token: 0x040000DC RID: 220
		private static Dictionary<Type, Type> _proxies = new Dictionary<Type, Type>();

		// Token: 0x040000DD RID: 221
		private static AssemblyName ProxyAssemblyName = new AssemblyName(string.Format(CultureInfo.InvariantCulture, "MetadataViewProxies_{0}", Guid.NewGuid()));

		// Token: 0x040000DE RID: 222
		private static ModuleBuilder transparentProxyModuleBuilder;

		// Token: 0x040000DF RID: 223
		private static Type[] CtorArgumentTypes = new Type[]
		{
			typeof(IDictionary<string, object>)
		};

		// Token: 0x040000E0 RID: 224
		private static MethodInfo _mdvDictionaryTryGet = MetadataViewGenerator.CtorArgumentTypes[0].GetMethod("TryGetValue");

		// Token: 0x040000E1 RID: 225
		private static readonly MethodInfo ObjectGetType = typeof(object).GetMethod("GetType", Type.EmptyTypes);
	}
}
