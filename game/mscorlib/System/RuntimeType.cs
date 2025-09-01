using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using Mono;

namespace System
{
	// Token: 0x02000208 RID: 520
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeType : System.Reflection.TypeInfo, ISerializable, ICloneable
	{
		// Token: 0x06001673 RID: 5747 RVA: 0x00056E65 File Offset: 0x00055065
		internal static RuntimeType GetType(string typeName, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			return RuntimeTypeHandle.GetTypeByName(typeName, throwOnError, ignoreCase, reflectionOnly, ref stackMark, false);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00056E84 File Offset: 0x00055084
		private static void ThrowIfTypeNeverValidGenericArgument(RuntimeType type)
		{
			if (type.IsPointer || type.IsByRef || type == typeof(void))
			{
				throw new ArgumentException(Environment.GetResourceString("The type '{0}' may not be used as a type argument.", new object[]
				{
					type.ToString()
				}));
			}
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00056ED4 File Offset: 0x000550D4
		internal static void SanityCheckGenericArguments(RuntimeType[] genericArguments, RuntimeType[] genericParamters)
		{
			if (genericArguments == null)
			{
				throw new ArgumentNullException();
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (genericArguments[i] == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType.ThrowIfTypeNeverValidGenericArgument(genericArguments[i]);
			}
			if (genericArguments.Length != genericParamters.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The type or method has {1} generic parameter(s), but {0} generic argument(s) were provided. A generic argument must be provided for each generic parameter.", new object[]
				{
					genericArguments.Length,
					genericParamters.Length
				}));
			}
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x00056F48 File Offset: 0x00055148
		private static void SplitName(string fullname, out string name, out string ns)
		{
			name = null;
			ns = null;
			if (fullname == null)
			{
				return;
			}
			int num = fullname.LastIndexOf(".", StringComparison.Ordinal);
			if (num == -1)
			{
				name = fullname;
				return;
			}
			ns = fullname.Substring(0, num);
			int num2 = fullname.Length - ns.Length - 1;
			if (num2 != 0)
			{
				name = fullname.Substring(num + 1, num2);
				return;
			}
			name = "";
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00056FA8 File Offset: 0x000551A8
		internal static BindingFlags FilterPreCalculate(bool isPublic, bool isInherited, bool isStatic)
		{
			BindingFlags bindingFlags = isPublic ? BindingFlags.Public : BindingFlags.NonPublic;
			if (isInherited)
			{
				bindingFlags |= BindingFlags.DeclaredOnly;
				if (isStatic)
				{
					bindingFlags |= (BindingFlags.Static | BindingFlags.FlattenHierarchy);
				}
				else
				{
					bindingFlags |= BindingFlags.Instance;
				}
			}
			else if (isStatic)
			{
				bindingFlags |= BindingFlags.Static;
			}
			else
			{
				bindingFlags |= BindingFlags.Instance;
			}
			return bindingFlags;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00056FE4 File Offset: 0x000551E4
		private static void FilterHelper(BindingFlags bindingFlags, ref string name, bool allowPrefixLookup, out bool prefixLookup, out bool ignoreCase, out RuntimeType.MemberListType listType)
		{
			prefixLookup = false;
			ignoreCase = false;
			if (name != null)
			{
				if ((bindingFlags & BindingFlags.IgnoreCase) != BindingFlags.Default)
				{
					name = name.ToLower(CultureInfo.InvariantCulture);
					ignoreCase = true;
					listType = RuntimeType.MemberListType.CaseInsensitive;
				}
				else
				{
					listType = RuntimeType.MemberListType.CaseSensitive;
				}
				if (allowPrefixLookup && name.EndsWith("*", StringComparison.Ordinal))
				{
					name = name.Substring(0, name.Length - 1);
					prefixLookup = true;
					listType = RuntimeType.MemberListType.All;
					return;
				}
			}
			else
			{
				listType = RuntimeType.MemberListType.All;
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00057050 File Offset: 0x00055250
		private static void FilterHelper(BindingFlags bindingFlags, ref string name, out bool ignoreCase, out RuntimeType.MemberListType listType)
		{
			bool flag;
			RuntimeType.FilterHelper(bindingFlags, ref name, false, out flag, out ignoreCase, out listType);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00057069 File Offset: 0x00055269
		private static bool FilterApplyPrefixLookup(MemberInfo memberInfo, string name, bool ignoreCase)
		{
			if (ignoreCase)
			{
				if (!memberInfo.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			else if (!memberInfo.Name.StartsWith(name, StringComparison.Ordinal))
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00057094 File Offset: 0x00055294
		private static bool FilterApplyBase(MemberInfo memberInfo, BindingFlags bindingFlags, bool isPublic, bool isNonProtectedInternal, bool isStatic, string name, bool prefixLookup)
		{
			if (isPublic)
			{
				if ((bindingFlags & BindingFlags.Public) == BindingFlags.Default)
				{
					return false;
				}
			}
			else if ((bindingFlags & BindingFlags.NonPublic) == BindingFlags.Default)
			{
				return false;
			}
			bool flag = memberInfo.DeclaringType != memberInfo.ReflectedType;
			if ((bindingFlags & BindingFlags.DeclaredOnly) > BindingFlags.Default && flag)
			{
				return false;
			}
			if (memberInfo.MemberType != MemberTypes.TypeInfo && memberInfo.MemberType != MemberTypes.NestedType)
			{
				if (isStatic)
				{
					if ((bindingFlags & BindingFlags.FlattenHierarchy) == BindingFlags.Default && flag)
					{
						return false;
					}
					if ((bindingFlags & BindingFlags.Static) == BindingFlags.Default)
					{
						return false;
					}
				}
				else if ((bindingFlags & BindingFlags.Instance) == BindingFlags.Default)
				{
					return false;
				}
			}
			if (prefixLookup && !RuntimeType.FilterApplyPrefixLookup(memberInfo, name, (bindingFlags & BindingFlags.IgnoreCase) > BindingFlags.Default))
			{
				return false;
			}
			if ((bindingFlags & BindingFlags.DeclaredOnly) == BindingFlags.Default && flag && isNonProtectedInternal && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.Default && !isStatic && (bindingFlags & BindingFlags.Instance) != BindingFlags.Default)
			{
				MethodInfo methodInfo = memberInfo as MethodInfo;
				if (methodInfo == null)
				{
					return false;
				}
				if (!methodInfo.IsVirtual && !methodInfo.IsAbstract)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00057160 File Offset: 0x00055360
		private static bool FilterApplyType(Type type, BindingFlags bindingFlags, string name, bool prefixLookup, string ns)
		{
			bool isPublic = type.IsNestedPublic || type.IsPublic;
			bool isStatic = false;
			return RuntimeType.FilterApplyBase(type, bindingFlags, isPublic, type.IsNestedAssembly, isStatic, name, prefixLookup) && (ns == null || !(ns != type.Namespace));
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000571AC File Offset: 0x000553AC
		private static bool FilterApplyMethodInfo(RuntimeMethodInfo method, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			return RuntimeType.FilterApplyMethodBase(method, method.BindingFlags, bindingFlags, callConv, argumentTypes);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000571BD File Offset: 0x000553BD
		private static bool FilterApplyConstructorInfo(RuntimeConstructorInfo constructor, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			return RuntimeType.FilterApplyMethodBase(constructor, constructor.BindingFlags, bindingFlags, callConv, argumentTypes);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x000571D0 File Offset: 0x000553D0
		private static bool FilterApplyMethodBase(MethodBase methodBase, BindingFlags methodFlags, BindingFlags bindingFlags, CallingConventions callConv, Type[] argumentTypes)
		{
			bindingFlags ^= BindingFlags.DeclaredOnly;
			if ((callConv & CallingConventions.Any) == (CallingConventions)0)
			{
				if ((callConv & CallingConventions.VarArgs) != (CallingConventions)0 && (methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
				{
					return false;
				}
				if ((callConv & CallingConventions.Standard) != (CallingConventions)0 && (methodBase.CallingConvention & CallingConventions.Standard) == (CallingConventions)0)
				{
					return false;
				}
			}
			if (argumentTypes != null)
			{
				ParameterInfo[] parametersNoCopy = methodBase.GetParametersNoCopy();
				if (argumentTypes.Length != parametersNoCopy.Length)
				{
					if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetProperty | BindingFlags.SetProperty)) == BindingFlags.Default)
					{
						return false;
					}
					bool flag = false;
					if (argumentTypes.Length > parametersNoCopy.Length)
					{
						if ((methodBase.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
						{
							flag = true;
						}
					}
					else if ((bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
					{
						flag = true;
					}
					else if (!parametersNoCopy[argumentTypes.Length].IsOptional)
					{
						flag = true;
					}
					if (flag)
					{
						if (parametersNoCopy.Length == 0)
						{
							return false;
						}
						if (argumentTypes.Length < parametersNoCopy.Length - 1)
						{
							return false;
						}
						ParameterInfo parameterInfo = parametersNoCopy[parametersNoCopy.Length - 1];
						if (!parameterInfo.ParameterType.IsArray)
						{
							return false;
						}
						if (!parameterInfo.IsDefined(typeof(ParamArrayAttribute), false))
						{
							return false;
						}
					}
				}
				else if ((bindingFlags & BindingFlags.ExactBinding) != BindingFlags.Default && (bindingFlags & BindingFlags.InvokeMethod) == BindingFlags.Default)
				{
					for (int i = 0; i < parametersNoCopy.Length; i++)
					{
						if (argumentTypes[i] != null && !argumentTypes[i].MatchesParameterTypeExactly(parametersNoCopy[i]))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x000572E0 File Offset: 0x000554E0
		internal RuntimeType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x000572F0 File Offset: 0x000554F0
		internal bool IsSpecialSerializableType()
		{
			RuntimeType runtimeType = this;
			while (!(runtimeType == RuntimeType.DelegateType) && !(runtimeType == RuntimeType.EnumType))
			{
				runtimeType = runtimeType.GetBaseType();
				if (!(runtimeType != null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0005732C File Offset: 0x0005552C
		private RuntimeType.ListBuilder<MethodInfo> GetMethodCandidates(string name, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, int genericParamCount, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimeMethodInfo[] methodsByName = this.GetMethodsByName(name, bindingAttr, listType, this);
			RuntimeType.ListBuilder<MethodInfo> result = new RuntimeType.ListBuilder<MethodInfo>(methodsByName.Length);
			int i = 0;
			while (i < methodsByName.Length)
			{
				RuntimeMethodInfo runtimeMethodInfo = methodsByName[i];
				if (genericParamCount == -1)
				{
					goto IL_5E;
				}
				bool isGenericMethod = runtimeMethodInfo.IsGenericMethod;
				if ((genericParamCount != 0 || !isGenericMethod) && (genericParamCount <= 0 || isGenericMethod) && runtimeMethodInfo.GetGenericArguments().Length == genericParamCount)
				{
					goto IL_5E;
				}
				IL_82:
				i++;
				continue;
				IL_5E:
				if (RuntimeType.FilterApplyMethodInfo(runtimeMethodInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeMethodInfo, name, ignoreCase)))
				{
					result.Add(runtimeMethodInfo);
					goto IL_82;
				}
				goto IL_82;
			}
			return result;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x000573CC File Offset: 0x000555CC
		private RuntimeType.ListBuilder<ConstructorInfo> GetConstructorCandidates(string name, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out memberListType);
			if ((!flag && name != null && name.Length == 0) || (!string.IsNullOrEmpty(name) && name != ConstructorInfo.ConstructorName && name != ConstructorInfo.TypeConstructorName))
			{
				return new RuntimeType.ListBuilder<ConstructorInfo>(0);
			}
			RuntimeConstructorInfo[] constructors_internal = this.GetConstructors_internal(bindingAttr, this);
			RuntimeType.ListBuilder<ConstructorInfo> result = new RuntimeType.ListBuilder<ConstructorInfo>(constructors_internal.Length);
			foreach (RuntimeConstructorInfo runtimeConstructorInfo in constructors_internal)
			{
				if (RuntimeType.FilterApplyConstructorInfo(runtimeConstructorInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeConstructorInfo, name, ignoreCase)))
				{
					result.Add(runtimeConstructorInfo);
				}
			}
			return result;
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00057474 File Offset: 0x00055674
		private RuntimeType.ListBuilder<PropertyInfo> GetPropertyCandidates(string name, BindingFlags bindingAttr, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimePropertyInfo[] propertiesByName = this.GetPropertiesByName(name, bindingAttr, listType, this);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<PropertyInfo> result = new RuntimeType.ListBuilder<PropertyInfo>(propertiesByName.Length);
			foreach (RuntimePropertyInfo runtimePropertyInfo in propertiesByName)
			{
				if ((bindingAttr & runtimePropertyInfo.BindingFlags) == runtimePropertyInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimePropertyInfo, name, ignoreCase)) && (types == null || runtimePropertyInfo.GetIndexParameters().Length == types.Length))
				{
					result.Add(runtimePropertyInfo);
				}
			}
			return result;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00057500 File Offset: 0x00055700
		private RuntimeType.ListBuilder<EventInfo> GetEventCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimeEventInfo[] events_internal = this.GetEvents_internal(name, bindingAttr, listType, this);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<EventInfo> result = new RuntimeType.ListBuilder<EventInfo>(events_internal.Length);
			foreach (RuntimeEventInfo runtimeEventInfo in events_internal)
			{
				if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeEventInfo, name, ignoreCase)))
				{
					result.Add(runtimeEventInfo);
				}
			}
			return result;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0005757C File Offset: 0x0005577C
		private RuntimeType.ListBuilder<FieldInfo> GetFieldCandidates(string name, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimeFieldInfo[] fields_internal = this.GetFields_internal(name, bindingAttr, listType, this);
			bindingAttr ^= BindingFlags.DeclaredOnly;
			RuntimeType.ListBuilder<FieldInfo> result = new RuntimeType.ListBuilder<FieldInfo>(fields_internal.Length);
			foreach (RuntimeFieldInfo runtimeFieldInfo in fields_internal)
			{
				if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeFieldInfo, name, ignoreCase)))
				{
					result.Add(runtimeFieldInfo);
				}
			}
			return result;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x000575F8 File Offset: 0x000557F8
		private RuntimeType.ListBuilder<Type> GetNestedTypeCandidates(string fullname, BindingFlags bindingAttr, bool allowPrefixLookup)
		{
			bindingAttr &= ~BindingFlags.Static;
			string text;
			string ns;
			RuntimeType.SplitName(fullname, out text, out ns);
			bool prefixLookup;
			bool flag;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref text, allowPrefixLookup, out prefixLookup, out flag, out listType);
			RuntimeType[] nestedTypes_internal = this.GetNestedTypes_internal(text, bindingAttr, listType);
			RuntimeType.ListBuilder<Type> result = new RuntimeType.ListBuilder<Type>(nestedTypes_internal.Length);
			foreach (RuntimeType runtimeType in nestedTypes_internal)
			{
				if (RuntimeType.FilterApplyType(runtimeType, bindingAttr, text, prefixLookup, ns))
				{
					result.Add(runtimeType);
				}
			}
			return result;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00057670 File Offset: 0x00055870
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodCandidates(null, bindingAttr, CallingConventions.Any, null, -1, false).ToArray();
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00057694 File Offset: 0x00055894
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, null, false).ToArray();
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x000576B4 File Offset: 0x000558B4
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.GetPropertyCandidates(null, bindingAttr, null, false).ToArray();
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000576D4 File Offset: 0x000558D4
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.GetEventCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000576F4 File Offset: 0x000558F4
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.GetFieldCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00057714 File Offset: 0x00055914
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.GetNestedTypeCandidates(null, bindingAttr, false).ToArray();
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00057734 File Offset: 0x00055934
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates(null, bindingAttr, CallingConventions.Any, null, -1, false);
			RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, null, false);
			RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates(null, bindingAttr, null, false);
			RuntimeType.ListBuilder<EventInfo> eventCandidates = this.GetEventCandidates(null, bindingAttr, false);
			RuntimeType.ListBuilder<FieldInfo> fieldCandidates = this.GetFieldCandidates(null, bindingAttr, false);
			RuntimeType.ListBuilder<Type> nestedTypeCandidates = this.GetNestedTypeCandidates(null, bindingAttr, false);
			MemberInfo[] array = new MemberInfo[methodCandidates.Count + constructorCandidates.Count + propertyCandidates.Count + eventCandidates.Count + fieldCandidates.Count + nestedTypeCandidates.Count];
			int num = 0;
			object[] array2 = array;
			methodCandidates.CopyTo(array2, num);
			num += methodCandidates.Count;
			array2 = array;
			constructorCandidates.CopyTo(array2, num);
			num += constructorCandidates.Count;
			array2 = array;
			propertyCandidates.CopyTo(array2, num);
			num += propertyCandidates.Count;
			array2 = array;
			eventCandidates.CopyTo(array2, num);
			num += eventCandidates.Count;
			array2 = array;
			fieldCandidates.CopyTo(array2, num);
			num += fieldCandidates.Count;
			array2 = array;
			nestedTypeCandidates.CopyTo(array2, num);
			num += nestedTypeCandidates.Count;
			return array;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00057864 File Offset: 0x00055A64
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			RuntimeType.ListBuilder<ConstructorInfo> constructorCandidates = this.GetConstructorCandidates(null, bindingAttr, CallingConventions.Any, types, false);
			if (constructorCandidates.Count == 0)
			{
				return null;
			}
			if (types.Length == 0 && constructorCandidates.Count == 1)
			{
				ConstructorInfo constructorInfo = constructorCandidates[0];
				ParameterInfo[] parametersNoCopy = constructorInfo.GetParametersNoCopy();
				if (parametersNoCopy == null || parametersNoCopy.Length == 0)
				{
					return constructorInfo;
				}
			}
			MethodBase[] match;
			if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
			{
				match = constructorCandidates.ToArray();
				return System.DefaultBinder.ExactBinding(match, types, modifiers) as ConstructorInfo;
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			Binder binder2 = binder;
			match = constructorCandidates.ToArray();
			return binder2.SelectMethod(bindingAttr, match, types, modifiers) as ConstructorInfo;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000578F8 File Offset: 0x00055AF8
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			RuntimeType.ListBuilder<PropertyInfo> propertyCandidates = this.GetPropertyCandidates(name, bindingAttr, types, false);
			if (propertyCandidates.Count == 0)
			{
				return null;
			}
			if (types == null || types.Length == 0)
			{
				if (propertyCandidates.Count == 1)
				{
					PropertyInfo propertyInfo = propertyCandidates[0];
					if (returnType != null && !returnType.IsEquivalentTo(propertyInfo.PropertyType))
					{
						return null;
					}
					return propertyInfo;
				}
				else if (returnType == null)
				{
					throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
				}
			}
			if ((bindingAttr & BindingFlags.ExactBinding) != BindingFlags.Default)
			{
				return System.DefaultBinder.ExactPropertyBinding(propertyCandidates.ToArray(), returnType, types, modifiers);
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			return binder.SelectProperty(bindingAttr, propertyCandidates.ToArray(), returnType, types, modifiers);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000579A8 File Offset: 0x00055BA8
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			bool flag;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, out flag, out listType);
			RuntimeEventInfo[] events_internal = this.GetEvents_internal(name, bindingAttr, listType, this);
			EventInfo eventInfo = null;
			bindingAttr ^= BindingFlags.DeclaredOnly;
			foreach (RuntimeEventInfo runtimeEventInfo in events_internal)
			{
				if ((bindingAttr & runtimeEventInfo.BindingFlags) == runtimeEventInfo.BindingFlags)
				{
					if (eventInfo != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					eventInfo = runtimeEventInfo;
				}
			}
			return eventInfo;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00057A24 File Offset: 0x00055C24
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			bool flag;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, out flag, out listType);
			RuntimeFieldInfo[] fields_internal = this.GetFields_internal(name, bindingAttr, listType, this);
			FieldInfo fieldInfo = null;
			bindingAttr ^= BindingFlags.DeclaredOnly;
			bool flag2 = false;
			foreach (RuntimeFieldInfo runtimeFieldInfo in fields_internal)
			{
				if ((bindingAttr & runtimeFieldInfo.BindingFlags) == runtimeFieldInfo.BindingFlags)
				{
					if (fieldInfo != null)
					{
						if (runtimeFieldInfo.DeclaringType == fieldInfo.DeclaringType)
						{
							throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
						}
						if (fieldInfo.DeclaringType.IsInterface && runtimeFieldInfo.DeclaringType.IsInterface)
						{
							flag2 = true;
						}
					}
					if (fieldInfo == null || runtimeFieldInfo.DeclaringType.IsSubclassOf(fieldInfo.DeclaringType) || fieldInfo.DeclaringType.IsInterface)
					{
						fieldInfo = runtimeFieldInfo;
					}
				}
			}
			if (flag2 && fieldInfo.DeclaringType.IsInterface)
			{
				throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
			}
			return fieldInfo;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00057B20 File Offset: 0x00055D20
		public override Type GetInterface(string fullname, bool ignoreCase)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException();
			}
			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			bindingFlags &= ~BindingFlags.Static;
			if (ignoreCase)
			{
				bindingFlags |= BindingFlags.IgnoreCase;
			}
			string text;
			string ns;
			RuntimeType.SplitName(fullname, out text, out ns);
			RuntimeType.MemberListType memberListType;
			RuntimeType.FilterHelper(bindingFlags, ref text, out ignoreCase, out memberListType);
			List<RuntimeType> list = null;
			StringComparison comparisonType = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			foreach (RuntimeType runtimeType in this.GetInterfaces())
			{
				if (string.Equals(runtimeType.Name, text, comparisonType))
				{
					if (list == null)
					{
						list = new List<RuntimeType>(2);
					}
					list.Add(runtimeType);
				}
			}
			if (list == null)
			{
				return null;
			}
			RuntimeType[] array = list.ToArray();
			RuntimeType runtimeType2 = null;
			foreach (RuntimeType runtimeType3 in array)
			{
				if (RuntimeType.FilterApplyType(runtimeType3, bindingFlags, text, false, ns))
				{
					if (runtimeType2 != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					runtimeType2 = runtimeType3;
				}
			}
			return runtimeType2;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00057C10 File Offset: 0x00055E10
		public override Type GetNestedType(string fullname, BindingFlags bindingAttr)
		{
			if (fullname == null)
			{
				throw new ArgumentNullException();
			}
			bindingAttr &= ~BindingFlags.Static;
			string text;
			string ns;
			RuntimeType.SplitName(fullname, out text, out ns);
			bool flag;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref text, out flag, out listType);
			RuntimeType[] nestedTypes_internal = this.GetNestedTypes_internal(text, bindingAttr, listType);
			RuntimeType runtimeType = null;
			foreach (RuntimeType runtimeType2 in nestedTypes_internal)
			{
				if (RuntimeType.FilterApplyType(runtimeType2, bindingAttr, text, false, ns))
				{
					if (runtimeType != null)
					{
						throw new AmbiguousMatchException(Environment.GetResourceString("Ambiguous match found."));
					}
					runtimeType = runtimeType2;
				}
			}
			return runtimeType;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00057C98 File Offset: 0x00055E98
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			RuntimeType.ListBuilder<MethodInfo> listBuilder = default(RuntimeType.ListBuilder<MethodInfo>);
			RuntimeType.ListBuilder<ConstructorInfo> listBuilder2 = default(RuntimeType.ListBuilder<ConstructorInfo>);
			RuntimeType.ListBuilder<PropertyInfo> listBuilder3 = default(RuntimeType.ListBuilder<PropertyInfo>);
			RuntimeType.ListBuilder<EventInfo> listBuilder4 = default(RuntimeType.ListBuilder<EventInfo>);
			RuntimeType.ListBuilder<FieldInfo> listBuilder5 = default(RuntimeType.ListBuilder<FieldInfo>);
			RuntimeType.ListBuilder<Type> listBuilder6 = default(RuntimeType.ListBuilder<Type>);
			int num = 0;
			if ((type & MemberTypes.Method) != (MemberTypes)0)
			{
				listBuilder = this.GetMethodCandidates(name, bindingAttr, CallingConventions.Any, null, -1, true);
				if (type == MemberTypes.Method)
				{
					return listBuilder.ToArray();
				}
				num += listBuilder.Count;
			}
			if ((type & MemberTypes.Constructor) != (MemberTypes)0)
			{
				listBuilder2 = this.GetConstructorCandidates(name, bindingAttr, CallingConventions.Any, null, true);
				if (type == MemberTypes.Constructor)
				{
					return listBuilder2.ToArray();
				}
				num += listBuilder2.Count;
			}
			if ((type & MemberTypes.Property) != (MemberTypes)0)
			{
				listBuilder3 = this.GetPropertyCandidates(name, bindingAttr, null, true);
				if (type == MemberTypes.Property)
				{
					return listBuilder3.ToArray();
				}
				num += listBuilder3.Count;
			}
			if ((type & MemberTypes.Event) != (MemberTypes)0)
			{
				listBuilder4 = this.GetEventCandidates(name, bindingAttr, true);
				if (type == MemberTypes.Event)
				{
					return listBuilder4.ToArray();
				}
				num += listBuilder4.Count;
			}
			if ((type & MemberTypes.Field) != (MemberTypes)0)
			{
				listBuilder5 = this.GetFieldCandidates(name, bindingAttr, true);
				if (type == MemberTypes.Field)
				{
					return listBuilder5.ToArray();
				}
				num += listBuilder5.Count;
			}
			if ((type & (MemberTypes.TypeInfo | MemberTypes.NestedType)) != (MemberTypes)0)
			{
				listBuilder6 = this.GetNestedTypeCandidates(name, bindingAttr, true);
				if (type == MemberTypes.NestedType || type == MemberTypes.TypeInfo)
				{
					return listBuilder6.ToArray();
				}
				num += listBuilder6.Count;
			}
			MemberInfo[] array;
			if (type != (MemberTypes.Constructor | MemberTypes.Method))
			{
				array = new MemberInfo[num];
			}
			else
			{
				MemberInfo[] array2 = new MethodBase[num];
				array = array2;
			}
			MemberInfo[] array3 = array;
			int num2 = 0;
			object[] array4 = array3;
			listBuilder.CopyTo(array4, num2);
			num2 += listBuilder.Count;
			array4 = array3;
			listBuilder2.CopyTo(array4, num2);
			num2 += listBuilder2.Count;
			array4 = array3;
			listBuilder3.CopyTo(array4, num2);
			num2 += listBuilder3.Count;
			array4 = array3;
			listBuilder4.CopyTo(array4, num2);
			num2 += listBuilder4.Count;
			array4 = array3;
			listBuilder5.CopyTo(array4, num2);
			num2 += listBuilder5.Count;
			array4 = array3;
			listBuilder6.CopyTo(array4, num2);
			num2 += listBuilder6.Count;
			return array3;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x00057EBC File Offset: 0x000560BC
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00057EC4 File Offset: 0x000560C4
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule(this);
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00057ECC File Offset: 0x000560CC
		public override Assembly Assembly
		{
			get
			{
				return this.GetRuntimeAssembly();
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00057ED4 File Offset: 0x000560D4
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return RuntimeTypeHandle.GetAssembly(this);
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x00057EDC File Offset: 0x000560DC
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return new RuntimeTypeHandle(this);
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00057EDC File Offset: 0x000560DC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal sealed override RuntimeTypeHandle GetTypeHandleInternal()
		{
			return new RuntimeTypeHandle(this);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00057EE4 File Offset: 0x000560E4
		[SecuritySafeCritical]
		public override bool IsInstanceOfType(object o)
		{
			return RuntimeTypeHandle.IsInstanceOfType(this, o);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00057EED File Offset: 0x000560ED
		public override bool IsAssignableFrom(System.Reflection.TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00057F08 File Offset: 0x00056108
		public override bool IsAssignableFrom(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (c == this)
			{
				return true;
			}
			RuntimeType runtimeType = c.UnderlyingSystemType as RuntimeType;
			if (runtimeType != null)
			{
				return RuntimeTypeHandle.CanCastTo(runtimeType, this);
			}
			if (RuntimeFeature.IsDynamicCodeSupported && c is TypeBuilder)
			{
				if (c.IsSubclassOf(this))
				{
					return true;
				}
				if (base.IsInterface)
				{
					return c.ImplementInterface(this);
				}
				if (this.IsGenericParameter)
				{
					Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
					for (int i = 0; i < genericParameterConstraints.Length; i++)
					{
						if (!genericParameterConstraints[i].IsAssignableFrom(c))
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00057F94 File Offset: 0x00056194
		public override bool IsEquivalentTo(Type other)
		{
			RuntimeType runtimeType = other as RuntimeType;
			return runtimeType != null && (runtimeType == this || RuntimeTypeHandle.IsEquivalentTo(this, runtimeType));
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00057FBF File Offset: 0x000561BF
		public override Type BaseType
		{
			get
			{
				return this.GetBaseType();
			}
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00057FC8 File Offset: 0x000561C8
		private RuntimeType GetBaseType()
		{
			if (base.IsInterface)
			{
				return null;
			}
			if (RuntimeTypeHandle.IsGenericVariable(this))
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				RuntimeType runtimeType = RuntimeType.ObjectType;
				foreach (RuntimeType runtimeType2 in genericParameterConstraints)
				{
					if (!runtimeType2.IsInterface)
					{
						if (runtimeType2.IsGenericParameter)
						{
							GenericParameterAttributes genericParameterAttributes = runtimeType2.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
							if ((genericParameterAttributes & GenericParameterAttributes.ReferenceTypeConstraint) == GenericParameterAttributes.None && (genericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) == GenericParameterAttributes.None)
							{
								goto IL_55;
							}
						}
						runtimeType = runtimeType2;
					}
					IL_55:;
				}
				if (runtimeType == RuntimeType.ObjectType && (this.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask & GenericParameterAttributes.NotNullableValueTypeConstraint) != GenericParameterAttributes.None)
				{
					runtimeType = RuntimeType.ValueType;
				}
				return runtimeType;
			}
			return RuntimeTypeHandle.GetBaseType(this);
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x0000270D File Offset: 0x0000090D
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0005805C File Offset: 0x0005625C
		[SecuritySafeCritical]
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return RuntimeTypeHandle.GetAttributes(this);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00058064 File Offset: 0x00056264
		[SecuritySafeCritical]
		protected override bool IsContextfulImpl()
		{
			return RuntimeTypeHandle.IsContextful(this);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0005806C File Offset: 0x0005626C
		protected override bool IsByRefImpl()
		{
			return RuntimeTypeHandle.IsByRef(this);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00058074 File Offset: 0x00056274
		protected override bool IsPrimitiveImpl()
		{
			return RuntimeTypeHandle.IsPrimitive(this);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0005807C File Offset: 0x0005627C
		protected override bool IsPointerImpl()
		{
			return RuntimeTypeHandle.IsPointer(this);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00058084 File Offset: 0x00056284
		[SecuritySafeCritical]
		protected override bool IsCOMObjectImpl()
		{
			return RuntimeTypeHandle.IsComObject(this, false);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x0005808D File Offset: 0x0005628D
		[SecuritySafeCritical]
		internal override bool IsWindowsRuntimeObjectImpl()
		{
			return RuntimeType.IsWindowsRuntimeObjectType(this);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00058095 File Offset: 0x00056295
		[SecuritySafeCritical]
		internal override bool IsExportedToWindowsRuntimeImpl()
		{
			return RuntimeType.IsTypeExportedToWindowsRuntime(this);
		}

		// Token: 0x060016AB RID: 5803
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsWindowsRuntimeObjectType(RuntimeType type);

		// Token: 0x060016AC RID: 5804
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTypeExportedToWindowsRuntime(RuntimeType type);

		// Token: 0x060016AD RID: 5805 RVA: 0x0005809D File Offset: 0x0005629D
		[SecuritySafeCritical]
		internal override bool HasProxyAttributeImpl()
		{
			return RuntimeTypeHandle.HasProxyAttribute(this);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000580A5 File Offset: 0x000562A5
		internal bool IsDelegate()
		{
			return this.GetBaseType() == typeof(MulticastDelegate);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000580BC File Offset: 0x000562BC
		protected override bool IsValueTypeImpl()
		{
			return !(this == typeof(ValueType)) && !(this == typeof(Enum)) && this.IsSubclassOf(typeof(ValueType));
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000580F4 File Offset: 0x000562F4
		public override bool IsEnum
		{
			get
			{
				return this.GetBaseType() == RuntimeType.EnumType;
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00058106 File Offset: 0x00056306
		protected override bool HasElementTypeImpl()
		{
			return RuntimeTypeHandle.HasElementType(this);
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x0005810E File Offset: 0x0005630E
		public override GenericParameterAttributes GenericParameterAttributes
		{
			[SecuritySafeCritical]
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
				}
				return this.GetGenericParameterAttributes();
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x0005812E File Offset: 0x0005632E
		internal override bool IsSzArray
		{
			get
			{
				return RuntimeTypeHandle.IsSzArray(this);
			}
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00058136 File Offset: 0x00056336
		protected override bool IsArrayImpl()
		{
			return RuntimeTypeHandle.IsArray(this);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0005813E File Offset: 0x0005633E
		[SecuritySafeCritical]
		public override int GetArrayRank()
		{
			if (!this.IsArrayImpl())
			{
				throw new ArgumentException(Environment.GetResourceString("Must be an array type."));
			}
			return RuntimeTypeHandle.GetArrayRank(this);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0005815E File Offset: 0x0005635E
		public override Type GetElementType()
		{
			return RuntimeTypeHandle.GetElementType(this);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00058168 File Offset: 0x00056368
		public override string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			string[] array = Enum.InternalGetNames(this);
			string[] array2 = new string[array.Length];
			Array.Copy(array, array2, array.Length);
			return array2;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000581B0 File Offset: 0x000563B0
		[SecuritySafeCritical]
		public override Array GetEnumValues()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			ulong[] array = Enum.InternalGetValues(this);
			Array array2 = Array.CreateInstance(this, array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				object value = Enum.ToObject(this, array[i]);
				array2.SetValue(value, i);
			}
			return array2;
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0005820C File Offset: 0x0005640C
		public override Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			return Enum.InternalGetUnderlyingType(this);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00058234 File Offset: 0x00056434
		public override bool IsEnumDefined(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			RuntimeType runtimeType = (RuntimeType)value.GetType();
			if (runtimeType.IsEnum)
			{
				if (!runtimeType.IsEquivalentTo(this))
				{
					throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
					{
						runtimeType.ToString(),
						this.ToString()
					}));
				}
				runtimeType = (RuntimeType)runtimeType.GetEnumUnderlyingType();
			}
			if (runtimeType == RuntimeType.StringType)
			{
				object[] array = Enum.InternalGetNames(this);
				return Array.IndexOf<object>(array, value) >= 0;
			}
			if (Type.IsIntegerType(runtimeType))
			{
				RuntimeType runtimeType2 = Enum.InternalGetUnderlyingType(this);
				if (runtimeType2 != runtimeType)
				{
					throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
					{
						runtimeType.ToString(),
						runtimeType2.ToString()
					}));
				}
				ulong[] array2 = Enum.InternalGetValues(this);
				ulong value2 = Enum.ToUInt64(value);
				return Array.BinarySearch<ulong>(array2, value2) >= 0;
			}
			else
			{
				if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
				{
					throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
					{
						runtimeType.ToString(),
						this.GetEnumUnderlyingType()
					}));
				}
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0005835C File Offset: 0x0005655C
		public override string GetEnumName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			if (!type.IsEnum && !Type.IsIntegerType(type))
			{
				throw new ArgumentException(Environment.GetResourceString("The value passed in must be an enum base or an underlying type for an enum, such as an Int32."), "value");
			}
			ulong[] array = Enum.InternalGetValues(this);
			ulong value2 = Enum.ToUInt64(value);
			int num = Array.BinarySearch<ulong>(array, value2);
			if (num >= 0)
			{
				return Enum.InternalGetNames(this)[num];
			}
			return null;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000583C5 File Offset: 0x000565C5
		internal RuntimeType[] GetGenericArgumentsInternal()
		{
			return (RuntimeType[])this.GetGenericArgumentsInternal(true);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x000583D4 File Offset: 0x000565D4
		public override Type[] GetGenericArguments()
		{
			Type[] array = this.GetGenericArgumentsInternal(false);
			if (array == null)
			{
				array = Array.Empty<Type>();
			}
			return array;
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000583F4 File Offset: 0x000565F4
		[SecuritySafeCritical]
		public override Type MakeGenericType(params Type[] instantiation)
		{
			if (instantiation == null)
			{
				throw new ArgumentNullException("instantiation");
			}
			RuntimeType[] array = new RuntimeType[instantiation.Length];
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException(Environment.GetResourceString("{0} is not a GenericTypeDefinition. MakeGenericType may only be called on a type for which Type.IsGenericTypeDefinition is true.", new object[]
				{
					this
				}));
			}
			if (this.GetGenericArguments().Length != instantiation.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("The number of generic arguments provided doesn't equal the arity of the generic type definition."), "instantiation");
			}
			int i = 0;
			while (i < instantiation.Length)
			{
				Type type = instantiation[i];
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				RuntimeType runtimeType = type as RuntimeType;
				if (runtimeType == null)
				{
					if (type.IsSignatureType)
					{
						return Type.MakeGenericSignatureType(this, instantiation);
					}
					Type[] array2 = new Type[instantiation.Length];
					for (int j = 0; j < instantiation.Length; j++)
					{
						array2[j] = instantiation[j];
					}
					instantiation = array2;
					if (!RuntimeFeature.IsDynamicCodeSupported)
					{
						throw new PlatformNotSupportedException();
					}
					return RuntimeType.MakeTypeBuilderInstantiation(this, instantiation);
				}
				else
				{
					array[i] = runtimeType;
					i++;
				}
			}
			RuntimeType[] genericArgumentsInternal = this.GetGenericArgumentsInternal();
			RuntimeType.SanityCheckGenericArguments(array, genericArgumentsInternal);
			Type[] types = array;
			Type type2 = RuntimeType.MakeGenericType(this, types);
			if (type2 == null)
			{
				throw new TypeLoadException();
			}
			return type2;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00058515 File Offset: 0x00056715
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return RuntimeTypeHandle.IsGenericTypeDefinition(this);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0005851D File Offset: 0x0005671D
		public override bool IsGenericParameter
		{
			get
			{
				return RuntimeTypeHandle.IsGenericVariable(this);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x00058525 File Offset: 0x00056725
		public override int GenericParameterPosition
		{
			get
			{
				if (!this.IsGenericParameter)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
				}
				return this.GetGenericParameterPosition();
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00058545 File Offset: 0x00056745
		public override Type GetGenericTypeDefinition()
		{
			if (!this.IsGenericType)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This operation is only valid on generic types."));
			}
			return RuntimeTypeHandle.GetGenericTypeDefinition(this);
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00058565 File Offset: 0x00056765
		public override bool IsGenericType
		{
			get
			{
				return RuntimeTypeHandle.HasInstantiation(this);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0005856D File Offset: 0x0005676D
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.IsGenericType && !this.IsGenericTypeDefinition;
			}
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00058584 File Offset: 0x00056784
		public override MemberInfo[] GetDefaultMembers()
		{
			MemberInfo[] array = null;
			string defaultMemberName = this.GetDefaultMemberName();
			if (defaultMemberName != null)
			{
				array = base.GetMember(defaultMemberName);
			}
			if (array == null)
			{
				array = Array.Empty<MemberInfo>();
			}
			return array;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x000585B0 File Offset: 0x000567B0
		[DebuggerStepThrough]
		[SecuritySafeCritical]
		[DebuggerHidden]
		public override object InvokeMember(string name, BindingFlags bindingFlags, Binder binder, object target, object[] providedArgs, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParams)
		{
			if (this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method must be called on a Type for which Type.IsGenericParameter is false."));
			}
			if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
			{
				throw new ArgumentException(Environment.GetResourceString("Must specify binding flags describing the invoke operation required (BindingFlags.InvokeMethod CreateInstance GetField SetField GetProperty SetProperty)."), "bindingFlags");
			}
			if ((bindingFlags & (BindingFlags)255) == BindingFlags.Default)
			{
				bindingFlags |= (BindingFlags.Instance | BindingFlags.Public);
				if ((bindingFlags & BindingFlags.CreateInstance) == BindingFlags.Default)
				{
					bindingFlags |= BindingFlags.Static;
				}
			}
			if (namedParams != null)
			{
				if (providedArgs != null)
				{
					if (namedParams.Length > providedArgs.Length)
					{
						throw new ArgumentException(Environment.GetResourceString("Named parameter array cannot be bigger than argument array."), "namedParams");
					}
				}
				else if (namedParams.Length != 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Named parameter array cannot be bigger than argument array."), "namedParams");
				}
			}
			if (target != null && target.GetType().IsCOMObject)
			{
				if ((bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) == BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Must specify property Set or Get or method call for a COM Object."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Cannot specify both Get and Set on a property."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~(BindingFlags.InvokeMethod | BindingFlags.GetProperty)) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Cannot specify Set on a property and Invoke on a method."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.SetProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Only one of the following binding flags can be set: BindingFlags.SetProperty, BindingFlags.PutDispProperty,  BindingFlags.PutRefDispProperty."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.PutDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutDispProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Only one of the following binding flags can be set: BindingFlags.SetProperty, BindingFlags.PutDispProperty,  BindingFlags.PutRefDispProperty."), "bindingFlags");
				}
				if ((bindingFlags & BindingFlags.PutRefDispProperty) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty) & ~BindingFlags.PutRefDispProperty) != BindingFlags.Default)
				{
					throw new ArgumentException(Environment.GetResourceString("Only one of the following binding flags can be set: BindingFlags.SetProperty, BindingFlags.PutDispProperty,  BindingFlags.PutRefDispProperty."), "bindingFlags");
				}
				if (RemotingServices.IsTransparentProxy(target))
				{
					throw new NotImplementedException();
				}
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				throw new NotImplementedException();
			}
			else
			{
				if (namedParams != null && Array.IndexOf<string>(namedParams, null) != -1)
				{
					throw new ArgumentException(Environment.GetResourceString("Named parameter value must not be null."), "namedParams");
				}
				int num = (providedArgs != null) ? providedArgs.Length : 0;
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default)
				{
					if ((bindingFlags & BindingFlags.CreateInstance) != BindingFlags.Default && (bindingFlags & (BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty)) != BindingFlags.Default)
					{
						throw new ArgumentException(Environment.GetResourceString("Cannot specify both CreateInstance and another access type."), "bindingFlags");
					}
					return Activator.CreateInstance(this, bindingFlags, binder, providedArgs, culture);
				}
				else
				{
					if ((bindingFlags & (BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty)) != BindingFlags.Default)
					{
						bindingFlags |= BindingFlags.SetProperty;
					}
					if (name == null)
					{
						throw new ArgumentNullException("name");
					}
					if (name.Length == 0 || name.Equals("[DISPID=0]"))
					{
						name = this.GetDefaultMemberName();
						if (name == null)
						{
							name = "ToString";
						}
					}
					bool flag = (bindingFlags & BindingFlags.GetField) > BindingFlags.Default;
					bool flag2 = (bindingFlags & BindingFlags.SetField) > BindingFlags.Default;
					if (flag || flag2)
					{
						if (flag)
						{
							if (flag2)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both Get and Set on a field."), "bindingFlags");
							}
							if ((bindingFlags & BindingFlags.SetProperty) != BindingFlags.Default)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both GetField and SetProperty."), "bindingFlags");
							}
						}
						else
						{
							if (providedArgs == null)
							{
								throw new ArgumentNullException("providedArgs");
							}
							if ((bindingFlags & BindingFlags.GetProperty) != BindingFlags.Default)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both SetField and GetProperty."), "bindingFlags");
							}
							if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify Set on a Field and Invoke on a method."), "bindingFlags");
							}
						}
						FieldInfo fieldInfo = null;
						FieldInfo[] array = this.GetMember(name, MemberTypes.Field, bindingFlags) as FieldInfo[];
						if (array.Length == 1)
						{
							fieldInfo = array[0];
						}
						else if (array.Length != 0)
						{
							fieldInfo = binder.BindToField(bindingFlags, array, flag ? Empty.Value : providedArgs[0], culture);
						}
						if (fieldInfo != null)
						{
							if (fieldInfo.FieldType.IsArray || fieldInfo.FieldType == typeof(Array))
							{
								int num2;
								if ((bindingFlags & BindingFlags.GetField) != BindingFlags.Default)
								{
									num2 = num;
								}
								else
								{
									num2 = num - 1;
								}
								if (num2 > 0)
								{
									int[] array2 = new int[num2];
									for (int i = 0; i < num2; i++)
									{
										try
										{
											array2[i] = ((IConvertible)providedArgs[i]).ToInt32(null);
										}
										catch (InvalidCastException)
										{
											throw new ArgumentException(Environment.GetResourceString("All indexes must be of type Int32."));
										}
									}
									Array array3 = (Array)fieldInfo.GetValue(target);
									if ((bindingFlags & BindingFlags.GetField) != BindingFlags.Default)
									{
										return array3.GetValue(array2);
									}
									array3.SetValue(providedArgs[num2], array2);
									return null;
								}
							}
							if (flag)
							{
								if (num != 0)
								{
									throw new ArgumentException(Environment.GetResourceString("No arguments can be provided to Get a field value."), "bindingFlags");
								}
								return fieldInfo.GetValue(target);
							}
							else
							{
								if (num != 1)
								{
									throw new ArgumentException(Environment.GetResourceString("Only the field value can be specified to set a field value."), "bindingFlags");
								}
								fieldInfo.SetValue(target, providedArgs[0], bindingFlags, binder, culture);
								return null;
							}
						}
						else if ((bindingFlags & (BindingFlags)16773888) == BindingFlags.Default)
						{
							throw new MissingFieldException(this.FullName, name);
						}
					}
					bool flag3 = (bindingFlags & BindingFlags.GetProperty) > BindingFlags.Default;
					bool flag4 = (bindingFlags & BindingFlags.SetProperty) > BindingFlags.Default;
					if (flag3 || flag4)
					{
						if (flag3)
						{
							if (flag4)
							{
								throw new ArgumentException(Environment.GetResourceString("Cannot specify both Get and Set on a property."), "bindingFlags");
							}
						}
						else if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
						{
							throw new ArgumentException(Environment.GetResourceString("Cannot specify Set on a property and Invoke on a method."), "bindingFlags");
						}
					}
					MethodInfo[] array4 = null;
					MethodInfo methodInfo = null;
					if ((bindingFlags & BindingFlags.InvokeMethod) != BindingFlags.Default)
					{
						MethodInfo[] array5 = this.GetMember(name, MemberTypes.Method, bindingFlags) as MethodInfo[];
						List<MethodInfo> list = null;
						foreach (MethodInfo methodInfo2 in array5)
						{
							if (RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo)methodInfo2, bindingFlags, CallingConventions.Any, new Type[num]))
							{
								if (methodInfo == null)
								{
									methodInfo = methodInfo2;
								}
								else
								{
									if (list == null)
									{
										list = new List<MethodInfo>(array5.Length);
										list.Add(methodInfo);
									}
									list.Add(methodInfo2);
								}
							}
						}
						if (list != null)
						{
							array4 = new MethodInfo[list.Count];
							list.CopyTo(array4);
						}
					}
					if ((methodInfo == null && flag3) || flag4)
					{
						PropertyInfo[] array6 = this.GetMember(name, MemberTypes.Property, bindingFlags) as PropertyInfo[];
						List<MethodInfo> list2 = null;
						for (int k = 0; k < array6.Length; k++)
						{
							MethodInfo methodInfo3;
							if (flag4)
							{
								methodInfo3 = array6[k].GetSetMethod(true);
							}
							else
							{
								methodInfo3 = array6[k].GetGetMethod(true);
							}
							if (!(methodInfo3 == null) && RuntimeType.FilterApplyMethodInfo((RuntimeMethodInfo)methodInfo3, bindingFlags, CallingConventions.Any, new Type[num]))
							{
								if (methodInfo == null)
								{
									methodInfo = methodInfo3;
								}
								else
								{
									if (list2 == null)
									{
										list2 = new List<MethodInfo>(array6.Length);
										list2.Add(methodInfo);
									}
									list2.Add(methodInfo3);
								}
							}
						}
						if (list2 != null)
						{
							array4 = new MethodInfo[list2.Count];
							list2.CopyTo(array4);
						}
					}
					if (!(methodInfo != null))
					{
						throw new MissingMethodException(this.FullName, name);
					}
					if (array4 == null && num == 0 && methodInfo.GetParametersNoCopy().Length == 0 && (bindingFlags & BindingFlags.OptionalParamBinding) == BindingFlags.Default)
					{
						return methodInfo.Invoke(target, bindingFlags, binder, providedArgs, culture);
					}
					if (array4 == null)
					{
						array4 = new MethodInfo[]
						{
							methodInfo
						};
					}
					if (providedArgs == null)
					{
						providedArgs = Array.Empty<object>();
					}
					object obj = null;
					MethodBase methodBase = null;
					try
					{
						Binder binder2 = binder;
						BindingFlags bindingAttr = bindingFlags;
						MethodBase[] match = array4;
						methodBase = binder2.BindToMethod(bindingAttr, match, ref providedArgs, modifiers, culture, namedParams, out obj);
					}
					catch (MissingMethodException)
					{
					}
					if (methodBase == null)
					{
						throw new MissingMethodException(this.FullName, name);
					}
					object result = ((MethodInfo)methodBase).Invoke(target, bindingFlags, binder, providedArgs, culture);
					if (obj != null)
					{
						binder.ReorderArgumentArray(ref providedArgs, obj);
					}
					return result;
				}
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00058CF0 File Offset: 0x00056EF0
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00028426 File Offset: 0x00026626
		public static bool operator ==(RuntimeType left, RuntimeType right)
		{
			return left == right;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0002842C File Offset: 0x0002662C
		public static bool operator !=(RuntimeType left, RuntimeType right)
		{
			return left != right;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0000270D File Offset: 0x0000090D
		public object Clone()
		{
			return this;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00058CF6 File Offset: 0x00056EF6
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, this);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00058D0D File Offset: 0x00056F0D
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, RuntimeType.ObjectType, inherit);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00058D1C File Offset: 0x00056F1C
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "attributeType");
			}
			return MonoCustomAttrs.GetCustomAttributes(this, runtimeType, inherit);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00058D6C File Offset: 0x00056F6C
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "attributeType");
			}
			return MonoCustomAttrs.IsDefined(this, runtimeType, inherit);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00058DB9 File Offset: 0x00056FB9
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00058DC4 File Offset: 0x00056FC4
		internal override string FormatTypeName(bool serialization)
		{
			if (serialization)
			{
				return this.GetCachedName(TypeNameKind.SerializationName);
			}
			Type rootElementType = base.GetRootElementType();
			if (rootElementType.IsNested)
			{
				return this.Name;
			}
			string text = this.ToString();
			if (rootElementType.IsPrimitive || rootElementType == typeof(void) || rootElementType == typeof(TypedReference))
			{
				text = text.Substring("System.".Length);
			}
			return text;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00058E37 File Offset: 0x00057037
		public override MemberTypes MemberType
		{
			get
			{
				if (base.IsPublic || base.IsNotPublic)
				{
					return MemberTypes.TypeInfo;
				}
				return MemberTypes.NestedType;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x00058E51 File Offset: 0x00057051
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00058E59 File Offset: 0x00057059
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return RuntimeTypeHandle.GetToken(this);
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00058E64 File Offset: 0x00057064
		private void CreateInstanceCheckThis()
		{
			if (this is ReflectionOnlyType)
			{
				throw new ArgumentException(Environment.GetResourceString("It is illegal to invoke a method on a Type loaded via ReflectionOnlyGetType."));
			}
			if (this.ContainsGenericParameters)
			{
				throw new ArgumentException(Environment.GetResourceString("Cannot create an instance of {0} because Type.ContainsGenericParameters is true.", new object[]
				{
					this
				}));
			}
			Type rootElementType = base.GetRootElementType();
			if (rootElementType == typeof(ArgIterator))
			{
				throw new NotSupportedException(Environment.GetResourceString("Cannot dynamically create an instance of ArgIterator."));
			}
			if (rootElementType == typeof(void))
			{
				throw new NotSupportedException(Environment.GetResourceString("Cannot dynamically create an instance of System.Void."));
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00058EEC File Offset: 0x000570EC
		[SecurityCritical]
		internal object CreateInstanceImpl(BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, ref StackCrawlMark stackMark)
		{
			this.CreateInstanceCheckThis();
			object result = null;
			try
			{
				try
				{
					if (activationAttributes != null)
					{
						ActivationServices.PushActivationAttributes(this, activationAttributes);
					}
					if (args == null)
					{
						args = Array.Empty<object>();
					}
					int num = args.Length;
					if (binder == null)
					{
						binder = Type.DefaultBinder;
					}
					bool publicOnly = (bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default;
					bool wrapExceptions = (bindingAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default;
					if (num == 0 && (bindingAttr & BindingFlags.Public) != BindingFlags.Default && (bindingAttr & BindingFlags.Instance) != BindingFlags.Default && (this.IsGenericCOMObjectImpl() || base.IsValueType))
					{
						result = this.CreateInstanceDefaultCtor(publicOnly, false, true, wrapExceptions, ref stackMark);
					}
					else
					{
						ConstructorInfo[] constructors = this.GetConstructors(bindingAttr);
						List<MethodBase> list = new List<MethodBase>(constructors.Length);
						Type[] array = new Type[num];
						for (int i = 0; i < num; i++)
						{
							if (args[i] != null)
							{
								array[i] = args[i].GetType();
							}
						}
						for (int j = 0; j < constructors.Length; j++)
						{
							if (RuntimeType.FilterApplyConstructorInfo((RuntimeConstructorInfo)constructors[j], bindingAttr, CallingConventions.Any, array))
							{
								list.Add(constructors[j]);
							}
						}
						MethodBase[] array2 = new MethodBase[list.Count];
						list.CopyTo(array2);
						if (array2 != null && array2.Length == 0)
						{
							array2 = null;
						}
						if (array2 == null)
						{
							if (activationAttributes != null)
							{
								ActivationServices.PopActivationAttributes(this);
								activationAttributes = null;
							}
							throw new MissingMethodException(Environment.GetResourceString("Constructor on type '{0}' not found.", new object[]
							{
								this.FullName
							}));
						}
						object obj = null;
						MethodBase methodBase;
						try
						{
							methodBase = binder.BindToMethod(bindingAttr, array2, ref args, null, culture, null, out obj);
						}
						catch (MissingMethodException)
						{
							methodBase = null;
						}
						if (methodBase == null)
						{
							if (activationAttributes != null)
							{
								ActivationServices.PopActivationAttributes(this);
								activationAttributes = null;
							}
							throw new MissingMethodException(Environment.GetResourceString("Constructor on type '{0}' not found.", new object[]
							{
								this.FullName
							}));
						}
						if (methodBase.GetParametersNoCopy().Length == 0)
						{
							if (args.Length != 0)
							{
								throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Vararg calling convention not supported."), Array.Empty<object>()));
							}
							if (activationAttributes != null && activationAttributes.Length != 0)
							{
								result = this.ActivationCreateInstance(methodBase, bindingAttr, binder, args, culture, activationAttributes);
							}
							else
							{
								result = Activator.CreateInstance(this, true, wrapExceptions);
							}
						}
						else
						{
							if (activationAttributes != null && activationAttributes.Length != 0)
							{
								result = this.ActivationCreateInstance(methodBase, bindingAttr, binder, args, culture, activationAttributes);
							}
							else
							{
								result = ((ConstructorInfo)methodBase).Invoke(bindingAttr, binder, args, culture);
							}
							if (obj != null)
							{
								binder.ReorderArgumentArray(ref args, obj);
							}
						}
					}
				}
				finally
				{
					if (activationAttributes != null)
					{
						ActivationServices.PopActivationAttributes(this);
						activationAttributes = null;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00059174 File Offset: 0x00057374
		private object ActivationCreateInstance(MethodBase invokeMethod, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			object obj = ActivationServices.CreateProxyFromAttributes(this, activationAttributes);
			if (obj != null)
			{
				invokeMethod.Invoke(obj, bindingAttr, binder, args, culture);
			}
			return obj;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0005919C File Offset: 0x0005739C
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal object CreateInstanceDefaultCtor(bool publicOnly, bool skipCheckThis, bool fillCache, bool wrapExceptions, ref StackCrawlMark stackMark)
		{
			if (base.GetType() == typeof(ReflectionOnlyType))
			{
				throw new InvalidOperationException(Environment.GetResourceString("The requested operation is invalid in the ReflectionOnly context."));
			}
			return this.CreateInstanceSlow(publicOnly, wrapExceptions, skipCheckThis, fillCache);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000591D0 File Offset: 0x000573D0
		internal RuntimeType(object obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000591E0 File Offset: 0x000573E0
		internal RuntimeConstructorInfo GetDefaultConstructor()
		{
			RuntimeConstructorInfo runtimeConstructorInfo = null;
			if (this.type_info == null)
			{
				this.type_info = new MonoTypeInfo();
			}
			else
			{
				runtimeConstructorInfo = this.type_info.default_ctor;
			}
			if (runtimeConstructorInfo == null)
			{
				ConstructorInfo[] constructors = this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				for (int i = 0; i < constructors.Length; i++)
				{
					if (constructors[i].GetParametersCount() == 0)
					{
						runtimeConstructorInfo = (this.type_info.default_ctor = (RuntimeConstructorInfo)constructors[i]);
						break;
					}
				}
			}
			return runtimeConstructorInfo;
		}

		// Token: 0x060016DA RID: 5850
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetCorrespondingInflatedMethod(MethodInfo generic);

		// Token: 0x060016DB RID: 5851
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ConstructorInfo GetCorrespondingInflatedConstructor(ConstructorInfo generic);

		// Token: 0x060016DC RID: 5852 RVA: 0x00059252 File Offset: 0x00057452
		internal override MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedMethod(fromNoninstanciated);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0005926F File Offset: 0x0005746F
		internal override ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			if (fromNoninstanciated == null)
			{
				throw new ArgumentNullException("fromNoninstanciated");
			}
			return this.GetCorrespondingInflatedConstructor(fromNoninstanciated);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0005928C File Offset: 0x0005748C
		internal override FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			BindingFlags bindingFlags = fromNoninstanciated.IsStatic ? BindingFlags.Static : BindingFlags.Instance;
			bindingFlags |= (fromNoninstanciated.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			return this.GetField(fromNoninstanciated.Name, bindingFlags);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000592C4 File Offset: 0x000574C4
		private string GetDefaultMemberName()
		{
			object[] customAttributes = this.GetCustomAttributes(typeof(DefaultMemberAttribute), true);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			return ((DefaultMemberAttribute)customAttributes[0]).MemberName;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000592F8 File Offset: 0x000574F8
		internal RuntimeConstructorInfo GetSerializationCtor()
		{
			if (this.m_serializationCtor == null)
			{
				Type[] types = new Type[]
				{
					typeof(SerializationInfo),
					typeof(StreamingContext)
				};
				this.m_serializationCtor = (base.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, types, null) as RuntimeConstructorInfo);
			}
			return this.m_serializationCtor;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00059351 File Offset: 0x00057551
		internal object CreateInstanceSlow(bool publicOnly, bool wrapExceptions, bool skipCheckThis, bool fillCache)
		{
			if (!skipCheckThis)
			{
				this.CreateInstanceCheckThis();
			}
			return this.CreateInstanceMono(!publicOnly, wrapExceptions);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00059368 File Offset: 0x00057568
		private object CreateInstanceMono(bool nonPublic, bool wrapExceptions)
		{
			RuntimeConstructorInfo runtimeConstructorInfo = this.GetDefaultConstructor();
			if (!nonPublic && runtimeConstructorInfo != null && !runtimeConstructorInfo.IsPublic)
			{
				runtimeConstructorInfo = null;
			}
			if (runtimeConstructorInfo == null)
			{
				Type rootElementType = base.GetRootElementType();
				if (rootElementType == typeof(TypedReference) || rootElementType == typeof(RuntimeArgumentHandle))
				{
					throw new NotSupportedException(Environment.GetResourceString("Cannot create boxed TypedReference, ArgIterator, or RuntimeArgumentHandle Objects."));
				}
				if (base.IsValueType)
				{
					return RuntimeType.CreateInstanceInternal(this);
				}
				throw new MissingMethodException("Default constructor not found for type " + this.FullName);
			}
			else
			{
				if (base.IsAbstract)
				{
					throw new MissingMethodException("Cannot create an abstract class '{0}'.", this.FullName);
				}
				return runtimeConstructorInfo.InternalInvoke(null, null, wrapExceptions);
			}
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00059414 File Offset: 0x00057614
		internal object CheckValue(object value, Binder binder, CultureInfo culture, BindingFlags invokeAttr)
		{
			bool flag = false;
			object result = this.TryConvertToType(value, ref flag);
			if (!flag)
			{
				return result;
			}
			if ((invokeAttr & BindingFlags.ExactBinding) == BindingFlags.ExactBinding)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Object of type '{0}' cannot be converted to type '{1}'."), value.GetType(), this));
			}
			if (binder != null && binder != Type.DefaultBinder)
			{
				return binder.ChangeType(value, this, culture);
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Object of type '{0}' cannot be converted to type '{1}'."), value.GetType(), this));
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00059498 File Offset: 0x00057698
		private object TryConvertToType(object value, ref bool failed)
		{
			if (this.IsInstanceOfType(value))
			{
				return value;
			}
			if (base.IsByRef)
			{
				Type elementType = this.GetElementType();
				if (value == null || elementType.IsInstanceOfType(value))
				{
					return value;
				}
			}
			if (value == null)
			{
				return value;
			}
			if (this.IsEnum)
			{
				if (Enum.GetUnderlyingType(this) == value.GetType())
				{
					return value;
				}
				object obj = RuntimeType.IsConvertibleToPrimitiveType(value, this);
				if (obj != null)
				{
					return obj;
				}
			}
			else if (base.IsPrimitive)
			{
				object obj2 = RuntimeType.IsConvertibleToPrimitiveType(value, this);
				if (obj2 != null)
				{
					return obj2;
				}
			}
			else if (base.IsPointer)
			{
				Type type = value.GetType();
				if (type == typeof(IntPtr) || type == typeof(UIntPtr))
				{
					return value;
				}
			}
			failed = true;
			return null;
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x0005954C File Offset: 0x0005774C
		private static object IsConvertibleToPrimitiveType(object value, Type targetType)
		{
			Type type = value.GetType();
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
				if (type == targetType)
				{
					return value;
				}
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (Type.GetTypeCode(targetType))
			{
			case TypeCode.Char:
				if (typeCode == TypeCode.Byte)
				{
					return (char)((byte)value);
				}
				if (typeCode == TypeCode.UInt16)
				{
					return value;
				}
				break;
			case TypeCode.Int16:
				if (typeCode == TypeCode.SByte)
				{
					return (short)((sbyte)value);
				}
				if (typeCode == TypeCode.Byte)
				{
					return (short)((byte)value);
				}
				break;
			case TypeCode.UInt16:
				if (typeCode == TypeCode.Char)
				{
					return value;
				}
				if (typeCode == TypeCode.Byte)
				{
					return (ushort)((byte)value);
				}
				break;
			case TypeCode.Int32:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (int)((char)value);
				case TypeCode.SByte:
					return (int)((sbyte)value);
				case TypeCode.Byte:
					return (int)((byte)value);
				case TypeCode.Int16:
					return (int)((short)value);
				case TypeCode.UInt16:
					return (int)((ushort)value);
				}
				break;
			case TypeCode.UInt32:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (uint)((char)value);
				case TypeCode.Byte:
					return (uint)((byte)value);
				case TypeCode.UInt16:
					return (uint)((ushort)value);
				}
				break;
			case TypeCode.Int64:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (long)((ulong)((char)value));
				case TypeCode.SByte:
					return (long)((sbyte)value);
				case TypeCode.Byte:
					return (long)((ulong)((byte)value));
				case TypeCode.Int16:
					return (long)((short)value);
				case TypeCode.UInt16:
					return (long)((ulong)((ushort)value));
				case TypeCode.Int32:
					return (long)((int)value);
				case TypeCode.UInt32:
					return (long)((ulong)((uint)value));
				}
				break;
			case TypeCode.UInt64:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (ulong)((char)value);
				case TypeCode.Byte:
					return (ulong)((byte)value);
				case TypeCode.UInt16:
					return (ulong)((ushort)value);
				case TypeCode.UInt32:
					return (ulong)((uint)value);
				}
				break;
			case TypeCode.Single:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (float)((char)value);
				case TypeCode.SByte:
					return (float)((sbyte)value);
				case TypeCode.Byte:
					return (float)((byte)value);
				case TypeCode.Int16:
					return (float)((short)value);
				case TypeCode.UInt16:
					return (float)((ushort)value);
				case TypeCode.Int32:
					return (float)((int)value);
				case TypeCode.UInt32:
					return (uint)value;
				case TypeCode.Int64:
					return (float)((long)value);
				case TypeCode.UInt64:
					return (ulong)value;
				}
				break;
			case TypeCode.Double:
				switch (typeCode)
				{
				case TypeCode.Char:
					return (double)((char)value);
				case TypeCode.SByte:
					return (double)((sbyte)value);
				case TypeCode.Byte:
					return (double)((byte)value);
				case TypeCode.Int16:
					return (double)((short)value);
				case TypeCode.UInt16:
					return (double)((ushort)value);
				case TypeCode.Int32:
					return (double)((int)value);
				case TypeCode.UInt32:
					return (uint)value;
				case TypeCode.Int64:
					return (double)((long)value);
				case TypeCode.UInt64:
					return (ulong)value;
				case TypeCode.Single:
					return (double)((float)value);
				}
				break;
			}
			return null;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x000598FD File Offset: 0x00057AFD
		private string GetCachedName(TypeNameKind kind)
		{
			if (kind == TypeNameKind.SerializationName)
			{
				return this.ToString();
			}
			throw new NotImplementedException();
		}

		// Token: 0x060016E7 RID: 5863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type make_array_type(int rank);

		// Token: 0x060016E8 RID: 5864 RVA: 0x0005990F File Offset: 0x00057B0F
		public override Type MakeArrayType()
		{
			return this.make_array_type(0);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00059918 File Offset: 0x00057B18
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1 || rank > 255)
			{
				throw new IndexOutOfRangeException();
			}
			return this.make_array_type(rank);
		}

		// Token: 0x060016EA RID: 5866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type make_byref_type();

		// Token: 0x060016EB RID: 5867 RVA: 0x00059933 File Offset: 0x00057B33
		public override Type MakeByRefType()
		{
			if (base.IsByRef)
			{
				throw new TypeLoadException("Can not call MakeByRefType on a ByRef type");
			}
			return this.make_byref_type();
		}

		// Token: 0x060016EC RID: 5868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type MakePointerType(Type type);

		// Token: 0x060016ED RID: 5869 RVA: 0x0005994E File Offset: 0x00057B4E
		public override Type MakePointerType()
		{
			if (base.IsByRef)
			{
				throw new TypeLoadException(string.Format("Could not load type '{0}' from assembly '{1}", base.GetType(), this.AssemblyQualifiedName));
			}
			return RuntimeType.MakePointerType(this);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x0005997A File Offset: 0x00057B7A
		public override StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				return StructLayoutAttribute.GetCustomAttribute(this);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00059984 File Offset: 0x00057B84
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.IsGenericType)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (genericArguments[i].ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return base.HasElementType && this.GetElementType().ContainsGenericParameters;
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000599DC File Offset: 0x00057BDC
		public override Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
			}
			RuntimeGenericParamInfoHandle runtimeGenericParamInfoHandle = new RuntimeGenericParamInfoHandle(RuntimeTypeHandle.GetGenericParameterInfo(this));
			Type[] array = runtimeGenericParamInfoHandle.Constraints;
			if (array == null)
			{
				array = EmptyArray<Type>.Value;
			}
			return array;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00059A20 File Offset: 0x00057C20
		internal static object CreateInstanceForAnotherGenericParameter(Type genericType, RuntimeType genericArgument)
		{
			return ((RuntimeType)RuntimeType.MakeGenericType(genericType, new Type[]
			{
				genericArgument
			})).GetDefaultConstructor().InternalInvoke(null, null, true);
		}

		// Token: 0x060016F2 RID: 5874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type MakeGenericType(Type gt, Type[] types);

		// Token: 0x060016F3 RID: 5875
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetMethodsByName_native(IntPtr namePtr, BindingFlags bindingAttr, RuntimeType.MemberListType listType);

		// Token: 0x060016F4 RID: 5876 RVA: 0x00059A44 File Offset: 0x00057C44
		internal RuntimeMethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, RuntimeType.MemberListType listType, RuntimeType reflectedType)
		{
			RuntimeTypeHandle reflectedType2 = new RuntimeTypeHandle(reflectedType);
			RuntimeMethodInfo[] result;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetMethodsByName_native(safeStringMarshal.Value, bindingAttr, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeMethodInfo[] array = new RuntimeMethodInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeMethodHandle handle = new RuntimeMethodHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeMethodInfo)RuntimeMethodInfo.GetMethodFromHandleNoGenericCheck(handle, reflectedType2);
					}
					result = array;
				}
			}
			return result;
		}

		// Token: 0x060016F5 RID: 5877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetPropertiesByName_native(IntPtr name, BindingFlags bindingAttr, RuntimeType.MemberListType listType);

		// Token: 0x060016F6 RID: 5878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetConstructors_native(BindingFlags bindingAttr);

		// Token: 0x060016F7 RID: 5879 RVA: 0x00059AFC File Offset: 0x00057CFC
		private RuntimeConstructorInfo[] GetConstructors_internal(BindingFlags bindingAttr, RuntimeType reflectedType)
		{
			RuntimeTypeHandle reflectedType2 = new RuntimeTypeHandle(reflectedType);
			RuntimeConstructorInfo[] result;
			using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetConstructors_native(bindingAttr)))
			{
				int length = safeGPtrArrayHandle.Length;
				RuntimeConstructorInfo[] array = new RuntimeConstructorInfo[length];
				for (int i = 0; i < length; i++)
				{
					RuntimeMethodHandle handle = new RuntimeMethodHandle(safeGPtrArrayHandle[i]);
					array[i] = (RuntimeConstructorInfo)RuntimeMethodInfo.GetMethodFromHandleNoGenericCheck(handle, reflectedType2);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00059B84 File Offset: 0x00057D84
		private RuntimePropertyInfo[] GetPropertiesByName(string name, BindingFlags bindingAttr, RuntimeType.MemberListType listType, RuntimeType reflectedType)
		{
			RuntimeTypeHandle reflectedType2 = new RuntimeTypeHandle(reflectedType);
			RuntimePropertyInfo[] result;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetPropertiesByName_native(safeStringMarshal.Value, bindingAttr, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimePropertyInfo[] array = new RuntimePropertyInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimePropertyHandle handle = new RuntimePropertyHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimePropertyInfo)RuntimePropertyInfo.GetPropertyFromHandle(handle, reflectedType2);
					}
					result = array;
				}
			}
			return result;
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00059C3C File Offset: 0x00057E3C
		public override InterfaceMapping GetInterfaceMap(Type ifaceType)
		{
			if (this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method must be called on a Type for which Type.IsGenericParameter is false."));
			}
			if (ifaceType == null)
			{
				throw new ArgumentNullException("ifaceType");
			}
			if (ifaceType as RuntimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."), "ifaceType");
			}
			if (!ifaceType.IsInterface)
			{
				throw new ArgumentException("Argument must be an interface.", "ifaceType");
			}
			if (base.IsInterface)
			{
				throw new ArgumentException("'this' type cannot be an interface itself");
			}
			InterfaceMapping interfaceMapping;
			interfaceMapping.TargetType = this;
			interfaceMapping.InterfaceType = ifaceType;
			RuntimeType.GetInterfaceMapData(this, ifaceType, out interfaceMapping.TargetMethods, out interfaceMapping.InterfaceMethods);
			if (interfaceMapping.TargetMethods == null)
			{
				throw new ArgumentException("Interface not found", "ifaceType");
			}
			return interfaceMapping;
		}

		// Token: 0x060016FA RID: 5882
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInterfaceMapData(Type t, Type iface, out MethodInfo[] targets, out MethodInfo[] methods);

		// Token: 0x060016FB RID: 5883
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGUID(Type type, byte[] guid);

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00059CFC File Offset: 0x00057EFC
		public override Guid GUID
		{
			get
			{
				byte[] array = new byte[16];
				RuntimeType.GetGUID(this, array);
				return new Guid(array);
			}
		}

		// Token: 0x060016FD RID: 5885
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetPacking(out int packing, out int size);

		// Token: 0x060016FE RID: 5886 RVA: 0x00059D20 File Offset: 0x00057F20
		internal static Type GetTypeFromCLSIDImpl(Guid clsid, string server, bool throwOnError)
		{
			if (RuntimeType.clsid_types == null)
			{
				Dictionary<Guid, Type> value = new Dictionary<Guid, Type>();
				Interlocked.CompareExchange<Dictionary<Guid, Type>>(ref RuntimeType.clsid_types, value, null);
			}
			Dictionary<Guid, Type> obj = RuntimeType.clsid_types;
			Type result;
			lock (obj)
			{
				Type type;
				if (RuntimeType.clsid_types.TryGetValue(clsid, out type))
				{
					result = type;
				}
				else
				{
					if (RuntimeType.clsid_assemblybuilder == null)
					{
						RuntimeType.clsid_assemblybuilder = new AssemblyBuilder(new AssemblyName
						{
							Name = "GetTypeFromCLSIDDummyAssembly"
						}, null, AssemblyBuilderAccess.Run, true);
					}
					TypeBuilder typeBuilder = RuntimeType.clsid_assemblybuilder.DefineDynamicModule(clsid.ToString()).DefineType("System.__ComObject", TypeAttributes.Public, typeof(__ComObject));
					Type[] types = new Type[]
					{
						typeof(string)
					};
					CustomAttributeBuilder customAttribute = new CustomAttributeBuilder(typeof(GuidAttribute).GetConstructor(types), new object[]
					{
						clsid.ToString()
					});
					typeBuilder.SetCustomAttribute(customAttribute);
					customAttribute = new CustomAttributeBuilder(typeof(ComImportAttribute).GetConstructor(Type.EmptyTypes), new object[0]);
					typeBuilder.SetCustomAttribute(customAttribute);
					type = typeBuilder.CreateType();
					RuntimeType.clsid_types.Add(clsid, type);
					result = type;
				}
			}
			return result;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00059E80 File Offset: 0x00058080
		protected override TypeCode GetTypeCodeImpl()
		{
			return RuntimeType.GetTypeCodeImplInternal(this);
		}

		// Token: 0x06001700 RID: 5888
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TypeCode GetTypeCodeImplInternal(Type type);

		// Token: 0x06001701 RID: 5889 RVA: 0x00059E88 File Offset: 0x00058088
		internal static Type GetTypeFromProgIDImpl(string progID, string server, bool throwOnError)
		{
			throw new NotImplementedException("Unmanaged activation is not supported");
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00059E94 File Offset: 0x00058094
		public override string ToString()
		{
			return this.getFullName(false, false);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		private bool IsGenericCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06001704 RID: 5892
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object CreateInstanceInternal(Type type);

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06001705 RID: 5893
		public override extern MethodBase DeclaringMethod { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001706 RID: 5894
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string getFullName(bool full_name, bool assembly_qualified);

		// Token: 0x06001707 RID: 5895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetGenericArgumentsInternal(bool runtimeArray);

		// Token: 0x06001708 RID: 5896 RVA: 0x00059EA0 File Offset: 0x000580A0
		private GenericParameterAttributes GetGenericParameterAttributes()
		{
			return new RuntimeGenericParamInfoHandle(RuntimeTypeHandle.GetGenericParameterInfo(this)).Attributes;
		}

		// Token: 0x06001709 RID: 5897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetGenericParameterPosition();

		// Token: 0x0600170A RID: 5898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetEvents_native(IntPtr name, RuntimeType.MemberListType listType);

		// Token: 0x0600170B RID: 5899
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetFields_native(IntPtr name, BindingFlags bindingAttr, RuntimeType.MemberListType listType);

		// Token: 0x0600170C RID: 5900 RVA: 0x00059EC0 File Offset: 0x000580C0
		private RuntimeFieldInfo[] GetFields_internal(string name, BindingFlags bindingAttr, RuntimeType.MemberListType listType, RuntimeType reflectedType)
		{
			RuntimeTypeHandle declaringType = new RuntimeTypeHandle(reflectedType);
			RuntimeFieldInfo[] result;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetFields_native(safeStringMarshal.Value, bindingAttr, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeFieldInfo[] array = new RuntimeFieldInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeFieldHandle handle = new RuntimeFieldHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeFieldInfo)FieldInfo.GetFieldFromHandle(handle, declaringType);
					}
					result = array;
				}
			}
			return result;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00059F78 File Offset: 0x00058178
		private RuntimeEventInfo[] GetEvents_internal(string name, BindingFlags bindingAttr, RuntimeType.MemberListType listType, RuntimeType reflectedType)
		{
			RuntimeTypeHandle reflectedType2 = new RuntimeTypeHandle(reflectedType);
			RuntimeEventInfo[] result;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(name))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetEvents_native(safeStringMarshal.Value, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeEventInfo[] array = new RuntimeEventInfo[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeEventHandle handle = new RuntimeEventHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeEventInfo)EventInfo.GetEventFromHandle(handle, reflectedType2);
					}
					result = array;
				}
			}
			return result;
		}

		// Token: 0x0600170E RID: 5902
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetInterfaces();

		// Token: 0x0600170F RID: 5903
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetNestedTypes_native(IntPtr name, BindingFlags bindingAttr, RuntimeType.MemberListType listType);

		// Token: 0x06001710 RID: 5904 RVA: 0x0005A030 File Offset: 0x00058230
		private RuntimeType[] GetNestedTypes_internal(string displayName, BindingFlags bindingAttr, RuntimeType.MemberListType listType)
		{
			string str = null;
			if (displayName != null)
			{
				str = TypeIdentifiers.FromDisplay(displayName).InternalName;
			}
			RuntimeType[] result;
			using (SafeStringMarshal safeStringMarshal = new SafeStringMarshal(str))
			{
				using (SafeGPtrArrayHandle safeGPtrArrayHandle = new SafeGPtrArrayHandle(this.GetNestedTypes_native(safeStringMarshal.Value, bindingAttr, listType)))
				{
					int length = safeGPtrArrayHandle.Length;
					RuntimeType[] array = new RuntimeType[length];
					for (int i = 0; i < length; i++)
					{
						RuntimeTypeHandle handle = new RuntimeTypeHandle(safeGPtrArrayHandle[i]);
						array[i] = (RuntimeType)Type.GetTypeFromHandle(handle);
					}
					result = array;
				}
			}
			return result;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0005A0F0 File Offset: 0x000582F0
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.getFullName(true, true);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06001712 RID: 5906
		public override extern Type DeclaringType { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001713 RID: 5907
		public override extern string Name { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001714 RID: 5908
		public override extern string Namespace { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001715 RID: 5909
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x0005A0FA File Offset: 0x000582FA
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0005A105 File Offset: 0x00058305
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001718 RID: 5912 RVA: 0x0005A110 File Offset: 0x00058310
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0005A11C File Offset: 0x0005831C
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != null && underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return (int)this._impl.Value;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0005A15C File Offset: 0x0005835C
		public override string FullName
		{
			get
			{
				if (this.ContainsGenericParameters && !base.GetRootElementType().IsGenericTypeDefinition)
				{
					return null;
				}
				if (this.type_info == null)
				{
					this.type_info = new MonoTypeInfo();
				}
				string result;
				if ((result = this.type_info.full_name) == null)
				{
					result = (this.type_info.full_name = this.getFullName(true, false));
				}
				return result;
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0005A1BA File Offset: 0x000583BA
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeType>(other);
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x0005A1C3 File Offset: 0x000583C3
		public override bool IsSZArray
		{
			get
			{
				return base.IsArray && this == this.GetElementType().MakeArrayType();
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0005A1E0 File Offset: 0x000583E0
		[ComVisible(true)]
		public override bool IsSubclassOf(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			return !(runtimeType == null) && RuntimeTypeHandle.IsSubclassOf(this, runtimeType);
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x0005A214 File Offset: 0x00058414
		public override bool IsByRefLike
		{
			get
			{
				return RuntimeTypeHandle.IsByRefLike(this);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0005A21C File Offset: 0x0005841C
		public override bool IsTypeDefinition
		{
			get
			{
				return RuntimeTypeHandle.IsTypeDefinition(this);
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0005A224 File Offset: 0x00058424
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodImplCommon(name, -1, bindingAttr, binder, callConv, types, modifiers);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0005A236 File Offset: 0x00058436
		protected override MethodInfo GetMethodImpl(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodImplCommon(name, genericParameterCount, bindingAttr, binder, callConv, types, modifiers);
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0005A24C File Offset: 0x0005844C
		private MethodInfo GetMethodImplCommon(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConv, Type[] types, ParameterModifier[] modifiers)
		{
			RuntimeType.ListBuilder<MethodInfo> methodCandidates = this.GetMethodCandidates(name, genericParameterCount, bindingAttr, callConv, types, false);
			if (methodCandidates.Count == 0)
			{
				return null;
			}
			MethodBase[] match;
			if (types == null || types.Length == 0)
			{
				MethodInfo methodInfo = methodCandidates[0];
				if (methodCandidates.Count == 1)
				{
					return methodInfo;
				}
				if (types == null)
				{
					for (int i = 1; i < methodCandidates.Count; i++)
					{
						if (!System.DefaultBinder.CompareMethodSig(methodCandidates[i], methodInfo))
						{
							throw new AmbiguousMatchException("Ambiguous match found.");
						}
					}
					match = methodCandidates.ToArray();
					return System.DefaultBinder.FindMostDerivedNewSlotMeth(match, methodCandidates.Count) as MethodInfo;
				}
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			Binder binder2 = binder;
			match = methodCandidates.ToArray();
			return binder2.SelectMethod(bindingAttr, match, types, modifiers) as MethodInfo;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0005A304 File Offset: 0x00058504
		private RuntimeType.ListBuilder<MethodInfo> GetMethodCandidates(string name, int genericParameterCount, BindingFlags bindingAttr, CallingConventions callConv, Type[] types, bool allowPrefixLookup)
		{
			bool flag;
			bool ignoreCase;
			RuntimeType.MemberListType listType;
			RuntimeType.FilterHelper(bindingAttr, ref name, allowPrefixLookup, out flag, out ignoreCase, out listType);
			RuntimeMethodInfo[] methodsByName = this.GetMethodsByName(name, bindingAttr, listType, this);
			RuntimeType.ListBuilder<MethodInfo> result = new RuntimeType.ListBuilder<MethodInfo>(methodsByName.Length);
			foreach (RuntimeMethodInfo runtimeMethodInfo in methodsByName)
			{
				if ((genericParameterCount == -1 || genericParameterCount == runtimeMethodInfo.GenericParameterCount) && RuntimeType.FilterApplyMethodInfo(runtimeMethodInfo, bindingAttr, callConv, types) && (!flag || RuntimeType.FilterApplyPrefixLookup(runtimeMethodInfo, name, ignoreCase)))
				{
					result.Add(runtimeMethodInfo);
				}
			}
			return result;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x0005A384 File Offset: 0x00058584
		// Note: this type is marked as 'beforefieldinit'.
		static RuntimeType()
		{
		}

		// Token: 0x0400159F RID: 5535
		internal static readonly RuntimeType ValueType = (RuntimeType)typeof(ValueType);

		// Token: 0x040015A0 RID: 5536
		internal static readonly RuntimeType EnumType = (RuntimeType)typeof(Enum);

		// Token: 0x040015A1 RID: 5537
		private static readonly RuntimeType ObjectType = (RuntimeType)typeof(object);

		// Token: 0x040015A2 RID: 5538
		private static readonly RuntimeType StringType = (RuntimeType)typeof(string);

		// Token: 0x040015A3 RID: 5539
		private static readonly RuntimeType DelegateType = (RuntimeType)typeof(Delegate);

		// Token: 0x040015A4 RID: 5540
		private static Type[] s_SICtorParamTypes;

		// Token: 0x040015A5 RID: 5541
		internal static Func<Type, Type[], Type> MakeTypeBuilderInstantiation;

		// Token: 0x040015A6 RID: 5542
		private const BindingFlags MemberBindingMask = (BindingFlags)255;

		// Token: 0x040015A7 RID: 5543
		private const BindingFlags InvocationMask = BindingFlags.InvokeMethod | BindingFlags.CreateInstance | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty;

		// Token: 0x040015A8 RID: 5544
		private const BindingFlags BinderNonCreateInstance = BindingFlags.InvokeMethod | BindingFlags.GetField | BindingFlags.SetField | BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x040015A9 RID: 5545
		private const BindingFlags BinderGetSetProperty = BindingFlags.GetProperty | BindingFlags.SetProperty;

		// Token: 0x040015AA RID: 5546
		private const BindingFlags BinderSetInvokeProperty = BindingFlags.InvokeMethod | BindingFlags.SetProperty;

		// Token: 0x040015AB RID: 5547
		private const BindingFlags BinderGetSetField = BindingFlags.GetField | BindingFlags.SetField;

		// Token: 0x040015AC RID: 5548
		private const BindingFlags BinderSetInvokeField = BindingFlags.InvokeMethod | BindingFlags.SetField;

		// Token: 0x040015AD RID: 5549
		private const BindingFlags BinderNonFieldGetSet = (BindingFlags)16773888;

		// Token: 0x040015AE RID: 5550
		private const BindingFlags ClassicBindingMask = BindingFlags.InvokeMethod | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.PutDispProperty | BindingFlags.PutRefDispProperty;

		// Token: 0x040015AF RID: 5551
		private static RuntimeType s_typedRef = (RuntimeType)typeof(TypedReference);

		// Token: 0x040015B0 RID: 5552
		[NonSerialized]
		private MonoTypeInfo type_info;

		// Token: 0x040015B1 RID: 5553
		internal object GenericCache;

		// Token: 0x040015B2 RID: 5554
		private RuntimeConstructorInfo m_serializationCtor;

		// Token: 0x040015B3 RID: 5555
		private static Dictionary<Guid, Type> clsid_types;

		// Token: 0x040015B4 RID: 5556
		private static AssemblyBuilder clsid_assemblybuilder;

		// Token: 0x040015B5 RID: 5557
		private const int GenericParameterCountAny = -1;

		// Token: 0x02000209 RID: 521
		internal enum MemberListType
		{
			// Token: 0x040015B7 RID: 5559
			All,
			// Token: 0x040015B8 RID: 5560
			CaseSensitive,
			// Token: 0x040015B9 RID: 5561
			CaseInsensitive,
			// Token: 0x040015BA RID: 5562
			HandleToInfo
		}

		// Token: 0x0200020A RID: 522
		private struct ListBuilder<T> where T : class
		{
			// Token: 0x06001726 RID: 5926 RVA: 0x0005A409 File Offset: 0x00058609
			public ListBuilder(int capacity)
			{
				this._items = null;
				this._item = default(T);
				this._count = 0;
				this._capacity = capacity;
			}

			// Token: 0x17000240 RID: 576
			public T this[int index]
			{
				get
				{
					if (this._items == null)
					{
						return this._item;
					}
					return this._items[index];
				}
			}

			// Token: 0x06001728 RID: 5928 RVA: 0x0005A44C File Offset: 0x0005864C
			public T[] ToArray()
			{
				if (this._count == 0)
				{
					return Array.Empty<T>();
				}
				if (this._count == 1)
				{
					return new T[]
					{
						this._item
					};
				}
				Array.Resize<T>(ref this._items, this._count);
				this._capacity = this._count;
				return this._items;
			}

			// Token: 0x06001729 RID: 5929 RVA: 0x0005A4A7 File Offset: 0x000586A7
			public void CopyTo(object[] array, int index)
			{
				if (this._count == 0)
				{
					return;
				}
				if (this._count == 1)
				{
					array[index] = this._item;
					return;
				}
				Array.Copy(this._items, 0, array, index, this._count);
			}

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x0600172A RID: 5930 RVA: 0x0005A4DE File Offset: 0x000586DE
			public int Count
			{
				get
				{
					return this._count;
				}
			}

			// Token: 0x0600172B RID: 5931 RVA: 0x0005A4E8 File Offset: 0x000586E8
			public void Add(T item)
			{
				if (this._count == 0)
				{
					this._item = item;
				}
				else
				{
					if (this._count == 1)
					{
						if (this._capacity < 2)
						{
							this._capacity = 4;
						}
						this._items = new T[this._capacity];
						this._items[0] = this._item;
					}
					else if (this._capacity == this._count)
					{
						int num = 2 * this._capacity;
						Array.Resize<T>(ref this._items, num);
						this._capacity = num;
					}
					this._items[this._count] = item;
				}
				this._count++;
			}

			// Token: 0x040015BB RID: 5563
			private T[] _items;

			// Token: 0x040015BC RID: 5564
			private T _item;

			// Token: 0x040015BD RID: 5565
			private int _count;

			// Token: 0x040015BE RID: 5566
			private int _capacity;
		}
	}
}
