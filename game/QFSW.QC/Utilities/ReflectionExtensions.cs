using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Utilities
{
	// Token: 0x02000055 RID: 85
	public static class ReflectionExtensions
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00009070 File Offset: 0x00007270
		public static bool IsDelegate(this Type type)
		{
			return typeof(Delegate).IsAssignableFrom(type);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009087 File Offset: 0x00007287
		public static bool IsStrongDelegate(this Type type)
		{
			return type.IsDelegate() && !type.IsAbstract;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000909E File Offset: 0x0000729E
		public static bool IsDelegate(this FieldInfo fieldInfo)
		{
			return fieldInfo.FieldType.IsDelegate();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000090AB File Offset: 0x000072AB
		public static bool IsStrongDelegate(this FieldInfo fieldInfo)
		{
			return fieldInfo.FieldType.IsStrongDelegate();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000090B8 File Offset: 0x000072B8
		public static bool IsGenericTypeOf(this Type genericType, Type nonGenericType)
		{
			return genericType.IsGenericType && genericType.GetGenericTypeDefinition() == nonGenericType;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000090D0 File Offset: 0x000072D0
		public static bool IsDerivedTypeOf(this Type type, Type baseType)
		{
			return baseType.IsAssignableFrom(type);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000090D9 File Offset: 0x000072D9
		public static bool IsCastableTo(this Type from, Type to, bool implicitly = false)
		{
			return to.IsAssignableFrom(from) || from.HasCastDefined(to, implicitly);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000090F0 File Offset: 0x000072F0
		private static bool HasCastDefined(this Type from, Type to, bool implicitly)
		{
			if ((from.IsPrimitive || from.IsEnum) && (to.IsPrimitive || to.IsEnum))
			{
				if (!implicitly)
				{
					return from == to || (from != typeof(bool) && to != typeof(bool));
				}
				IEnumerable<Type> enumerable = Enumerable.Empty<Type>();
				Func<Type, bool> <>9__4;
				Func<Type, bool> <>9__5;
				foreach (Type[] array in ReflectionExtensions._primitiveTypeCastHierarchy)
				{
					IEnumerable<Type> source = array;
					Func<Type, bool> predicate;
					if ((predicate = <>9__4) == null)
					{
						predicate = (<>9__4 = ((Type t) => t == to));
					}
					if (source.Any(predicate))
					{
						IEnumerable<Type> source2 = enumerable;
						Func<Type, bool> predicate2;
						if ((predicate2 = <>9__5) == null)
						{
							predicate2 = (<>9__5 = ((Type t) => t == from));
						}
						return source2.Any(predicate2);
					}
					enumerable = enumerable.Concat(array);
				}
				return false;
			}
			else
			{
				if (!ReflectionExtensions.IsCastDefined(to, (MethodInfo m) => m.GetParameters()[0].ParameterType, (MethodInfo _) => from, implicitly, false))
				{
					return ReflectionExtensions.IsCastDefined(from, (MethodInfo _) => to, (MethodInfo m) => m.ReturnType, implicitly, true);
				}
				return true;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009284 File Offset: 0x00007484
		private static bool IsCastDefined(Type type, Func<MethodInfo, Type> baseType, Func<MethodInfo, Type> derivedType, bool implicitly, bool lookInBase)
		{
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public | (lookInBase ? BindingFlags.FlattenHierarchy : BindingFlags.DeclaredOnly);
			return (from m in type.GetMethods(bindingAttr)
			where m.Name == "op_Implicit" || (!implicitly && m.Name == "op_Explicit")
			select m).Any((MethodInfo m) => baseType(m).IsAssignableFrom(derivedType(m)));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000092E4 File Offset: 0x000074E4
		public static object Cast(this Type type, object data)
		{
			if (type.IsInstanceOfType(data))
			{
				return data;
			}
			object result;
			try
			{
				result = Convert.ChangeType(data, type);
			}
			catch (InvalidCastException)
			{
				Type type2 = data.GetType();
				ParameterExpression parameterExpression = Expression.Parameter(type2, "data");
				result = Expression.Lambda(Expression.Convert(Expression.Convert(parameterExpression, type2), type), new ParameterExpression[]
				{
					parameterExpression
				}).Compile().DynamicInvoke(new object[]
				{
					data
				});
			}
			return result;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009360 File Offset: 0x00007560
		public static bool IsOverride(this MethodInfo methodInfo)
		{
			return methodInfo.GetBaseDefinition().DeclaringType != methodInfo.DeclaringType;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009378 File Offset: 0x00007578
		public static bool HasAttribute<T>(this ICustomAttributeProvider provider, bool searchInherited = true) where T : Attribute
		{
			bool result;
			try
			{
				result = provider.IsDefined(typeof(T), searchInherited);
			}
			catch (MissingMethodException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000093B0 File Offset: 0x000075B0
		public static string GetDisplayName(this Type type, bool includeNamespace = false)
		{
			if (type.IsGenericParameter)
			{
				return type.Name;
			}
			if (type.IsArray)
			{
				int arrayRank = type.GetArrayRank();
				return type.GetElementType().GetDisplayName(includeNamespace) + "[" + new string(',', arrayRank - 1) + "]";
			}
			if (ReflectionExtensions._typeDisplayNames.ContainsKey(type))
			{
				string text = ReflectionExtensions._typeDisplayNames[type];
				if (type.IsGenericType && !type.IsConstructedGenericType)
				{
					Type[] genericArguments = type.GetGenericArguments();
					return text + "<" + new string(',', genericArguments.Length - 1) + ">";
				}
				return text;
			}
			else
			{
				if (type.IsGenericTypeOf(typeof(Nullable<>)))
				{
					return type.GetGenericArguments()[0].GetDisplayName(false) + "?";
				}
				if (type.IsGenericType)
				{
					Type genericTypeDefinition = type.GetGenericTypeDefinition();
					Type[] genericArguments2 = type.GetGenericArguments();
					if (ReflectionExtensions._valueTupleTypes.Contains(genericTypeDefinition))
					{
						return type.GetTupleDisplayName(includeNamespace);
					}
					if (type.IsConstructedGenericType)
					{
						string[] array = new string[genericArguments2.Length];
						for (int i = 0; i < genericArguments2.Length; i++)
						{
							array[i] = genericArguments2[i].GetDisplayName(includeNamespace);
						}
						return genericTypeDefinition.GetDisplayName(includeNamespace).Split('<', StringSplitOptions.None)[0] + "<" + string.Join(", ", array) + ">";
					}
					return (includeNamespace ? type.FullName : type.Name).Split('`', StringSplitOptions.None)[0] + "<" + new string(',', genericArguments2.Length - 1) + ">";
				}
				else
				{
					Type declaringType = type.DeclaringType;
					if (declaringType != null)
					{
						return declaringType.GetDisplayName(includeNamespace) + "." + type.Name;
					}
					if (!includeNamespace)
					{
						return type.Name;
					}
					return type.FullName;
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009580 File Offset: 0x00007780
		private static string GetTupleDisplayName(this Type type, bool includeNamespace = false)
		{
			IEnumerable<string> values = from x in type.GetGenericArguments()
			select x.GetDisplayName(includeNamespace);
			return "(" + string.Join(", ", values) + ")";
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000095CC File Offset: 0x000077CC
		public static bool AreMethodsEqual(MethodInfo a, MethodInfo b)
		{
			if (a.Name != b.Name)
			{
				return false;
			}
			ParameterInfo[] parameters = a.GetParameters();
			ParameterInfo[] parameters2 = b.GetParameters();
			if (parameters.Length != parameters2.Length)
			{
				return false;
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterInfo parameterInfo = parameters[i];
				ParameterInfo parameterInfo2 = parameters2[i];
				if (parameterInfo.Name != parameterInfo2.Name)
				{
					return false;
				}
				if (parameterInfo.HasDefaultValue != parameterInfo2.HasDefaultValue)
				{
					return false;
				}
				Type parameterType = parameterInfo.ParameterType;
				Type parameterType2 = parameterInfo2.ParameterType;
				if (!parameterType.ContainsGenericParameters && !parameterType2.ContainsGenericParameters && parameterType != parameterType2)
				{
					return false;
				}
			}
			if (a.IsGenericMethod != b.IsGenericMethod)
			{
				return false;
			}
			if (a.IsGenericMethod && b.IsGenericMethod)
			{
				Type[] genericArguments = a.GetGenericArguments();
				Type[] genericArguments2 = b.GetGenericArguments();
				if (genericArguments.Length != genericArguments2.Length)
				{
					return false;
				}
				for (int j = 0; j < genericArguments.Length; j++)
				{
					MemberInfo memberInfo = genericArguments[j];
					Type type = genericArguments2[j];
					if (memberInfo.Name != type.Name)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000096E8 File Offset: 0x000078E8
		public static MethodInfo RebaseMethod(this MethodInfo method, Type newBase)
		{
			BindingFlags bindingFlags = BindingFlags.Default;
			bindingFlags |= (method.IsStatic ? BindingFlags.Static : BindingFlags.Instance);
			bindingFlags |= (method.IsPublic ? BindingFlags.Public : BindingFlags.NonPublic);
			MethodInfo[] array = (from x in newBase.GetMethods(bindingFlags)
			where ReflectionExtensions.AreMethodsEqual(x, method)
			select x).ToArray<MethodInfo>();
			if (array.Length == 0)
			{
				throw new ArgumentException(string.Format("Could not rebase method {0} onto type {1} as no matching candidates were found", method, newBase));
			}
			if (array.Length > 1)
			{
				throw new ArgumentException(string.Format("Could not rebase method {0} onto type {1} as too many matching candidates were found", method, newBase));
			}
			return array[0];
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009788 File Offset: 0x00007988
		// Note: this type is marked as 'beforefieldinit'.
		static ReflectionExtensions()
		{
		}

		// Token: 0x04000137 RID: 311
		private static readonly Dictionary<Type, string> _typeDisplayNames = new Dictionary<Type, string>
		{
			{
				typeof(int),
				"int"
			},
			{
				typeof(float),
				"float"
			},
			{
				typeof(decimal),
				"decimal"
			},
			{
				typeof(double),
				"double"
			},
			{
				typeof(string),
				"string"
			},
			{
				typeof(bool),
				"bool"
			},
			{
				typeof(byte),
				"byte"
			},
			{
				typeof(sbyte),
				"sbyte"
			},
			{
				typeof(uint),
				"uint"
			},
			{
				typeof(short),
				"short"
			},
			{
				typeof(ushort),
				"ushort"
			},
			{
				typeof(long),
				"decimal"
			},
			{
				typeof(ulong),
				"ulong"
			},
			{
				typeof(char),
				"char"
			},
			{
				typeof(object),
				"object"
			}
		};

		// Token: 0x04000138 RID: 312
		private static readonly Type[] _valueTupleTypes = new Type[]
		{
			typeof(ValueTuple<>),
			typeof(ValueTuple<, >),
			typeof(ValueTuple<, , >),
			typeof(ValueTuple<, , , >),
			typeof(ValueTuple<, , , , >),
			typeof(ValueTuple<, , , , , >),
			typeof(ValueTuple<, , , , , , >),
			typeof(ValueTuple<, , , , , , , >)
		};

		// Token: 0x04000139 RID: 313
		private static readonly Type[][] _primitiveTypeCastHierarchy = new Type[][]
		{
			new Type[]
			{
				typeof(byte),
				typeof(sbyte),
				typeof(char)
			},
			new Type[]
			{
				typeof(short),
				typeof(ushort)
			},
			new Type[]
			{
				typeof(int),
				typeof(uint)
			},
			new Type[]
			{
				typeof(long),
				typeof(ulong)
			},
			new Type[]
			{
				typeof(float)
			},
			new Type[]
			{
				typeof(double)
			}
		};

		// Token: 0x020000AB RID: 171
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x06000349 RID: 841 RVA: 0x0000C561 File Offset: 0x0000A761
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x0600034A RID: 842 RVA: 0x0000C569 File Offset: 0x0000A769
			internal bool <HasCastDefined>b__4(Type t)
			{
				return t == this.to;
			}

			// Token: 0x0600034B RID: 843 RVA: 0x0000C577 File Offset: 0x0000A777
			internal bool <HasCastDefined>b__5(Type t)
			{
				return t == this.from;
			}

			// Token: 0x0600034C RID: 844 RVA: 0x0000C585 File Offset: 0x0000A785
			internal Type <HasCastDefined>b__1(MethodInfo _)
			{
				return this.from;
			}

			// Token: 0x0600034D RID: 845 RVA: 0x0000C58D File Offset: 0x0000A78D
			internal Type <HasCastDefined>b__2(MethodInfo _)
			{
				return this.to;
			}

			// Token: 0x0400021F RID: 543
			public Type to;

			// Token: 0x04000220 RID: 544
			public Type from;

			// Token: 0x04000221 RID: 545
			public Func<Type, bool> <>9__4;

			// Token: 0x04000222 RID: 546
			public Func<Type, bool> <>9__5;
		}

		// Token: 0x020000AC RID: 172
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600034E RID: 846 RVA: 0x0000C595 File Offset: 0x0000A795
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600034F RID: 847 RVA: 0x0000C5A1 File Offset: 0x0000A7A1
			public <>c()
			{
			}

			// Token: 0x06000350 RID: 848 RVA: 0x0000C5A9 File Offset: 0x0000A7A9
			internal Type <HasCastDefined>b__10_0(MethodInfo m)
			{
				return m.GetParameters()[0].ParameterType;
			}

			// Token: 0x06000351 RID: 849 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
			internal Type <HasCastDefined>b__10_3(MethodInfo m)
			{
				return m.ReturnType;
			}

			// Token: 0x04000223 RID: 547
			public static readonly ReflectionExtensions.<>c <>9 = new ReflectionExtensions.<>c();

			// Token: 0x04000224 RID: 548
			public static Func<MethodInfo, Type> <>9__10_0;

			// Token: 0x04000225 RID: 549
			public static Func<MethodInfo, Type> <>9__10_3;
		}

		// Token: 0x020000AD RID: 173
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x06000352 RID: 850 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06000353 RID: 851 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
			internal bool <IsCastDefined>b__0(MethodInfo m)
			{
				return m.Name == "op_Implicit" || (!this.implicitly && m.Name == "op_Explicit");
			}

			// Token: 0x06000354 RID: 852 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
			internal bool <IsCastDefined>b__1(MethodInfo m)
			{
				return this.baseType(m).IsAssignableFrom(this.derivedType(m));
			}

			// Token: 0x04000226 RID: 550
			public bool implicitly;

			// Token: 0x04000227 RID: 551
			public Func<MethodInfo, Type> baseType;

			// Token: 0x04000228 RID: 552
			public Func<MethodInfo, Type> derivedType;
		}

		// Token: 0x020000AE RID: 174
		[CompilerGenerated]
		private sealed class <>c__DisplayClass16_0
		{
			// Token: 0x06000355 RID: 853 RVA: 0x0000C617 File Offset: 0x0000A817
			public <>c__DisplayClass16_0()
			{
			}

			// Token: 0x06000356 RID: 854 RVA: 0x0000C61F File Offset: 0x0000A81F
			internal string <GetTupleDisplayName>b__0(Type x)
			{
				return x.GetDisplayName(this.includeNamespace);
			}

			// Token: 0x04000229 RID: 553
			public bool includeNamespace;
		}

		// Token: 0x020000AF RID: 175
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0
		{
			// Token: 0x06000357 RID: 855 RVA: 0x0000C62D File Offset: 0x0000A82D
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x06000358 RID: 856 RVA: 0x0000C635 File Offset: 0x0000A835
			internal bool <RebaseMethod>b__0(MethodInfo x)
			{
				return ReflectionExtensions.AreMethodsEqual(x, this.method);
			}

			// Token: 0x0400022A RID: 554
			public MethodInfo method;
		}
	}
}
