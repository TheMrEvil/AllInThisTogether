using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000059 RID: 89
	public static class ReflectionUtilities
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0000DA03 File Offset: 0x0000BC03
		public static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type)
		{
			return from constructor in type.GetTypeInfo().DeclaredConstructors
			where (constructor.Attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope
			select constructor;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000DA34 File Offset: 0x0000BC34
		public static ConstructorInfo FindConstructor(this Type self, params Type[] parameterTypes)
		{
			return (from constructor in self.GetConstructors()
			where (from parameter in constructor.GetParameters()
			select parameter.ParameterType).SequenceEqual(parameterTypes)
			select constructor).SingleOrDefault<ConstructorInfo>();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000DA6A File Offset: 0x0000BC6A
		public static bool CheckWrappedWithNullable(this Type type)
		{
			return type.IsConstructedGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000DA8B File Offset: 0x0000BC8B
		public static string GetParseClassName(this Type type)
		{
			ParseClassNameAttribute customAttribute = type.GetCustomAttribute<ParseClassNameAttribute>();
			if (customAttribute == null)
			{
				return null;
			}
			return customAttribute.ClassName;
		}

		// Token: 0x0200012A RID: 298
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000791 RID: 1937 RVA: 0x00017032 File Offset: 0x00015232
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x0001703E File Offset: 0x0001523E
			public <>c()
			{
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x00017046 File Offset: 0x00015246
			internal bool <GetInstanceConstructors>b__0_0(ConstructorInfo constructor)
			{
				return (constructor.Attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope;
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x00017054 File Offset: 0x00015254
			internal Type <FindConstructor>b__1_1(ParameterInfo parameter)
			{
				return parameter.ParameterType;
			}

			// Token: 0x040002B5 RID: 693
			public static readonly ReflectionUtilities.<>c <>9 = new ReflectionUtilities.<>c();

			// Token: 0x040002B6 RID: 694
			public static Func<ConstructorInfo, bool> <>9__0_0;

			// Token: 0x040002B7 RID: 695
			public static Func<ParameterInfo, Type> <>9__1_1;
		}

		// Token: 0x0200012B RID: 299
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x06000795 RID: 1941 RVA: 0x0001705C File Offset: 0x0001525C
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x00017064 File Offset: 0x00015264
			internal bool <FindConstructor>b__0(ConstructorInfo constructor)
			{
				return constructor.GetParameters().Select(new Func<ParameterInfo, Type>(ReflectionUtilities.<>c.<>9.<FindConstructor>b__1_1)).SequenceEqual(this.parameterTypes);
			}

			// Token: 0x040002B8 RID: 696
			public Type[] parameterTypes;
		}
	}
}
