using System;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000075 RID: 117
	internal static class ReflectionExtensions
	{
		// Token: 0x06000310 RID: 784 RVA: 0x00009C88 File Offset: 0x00007E88
		public static ReflectionMember ToReflectionMember(this LazyMemberInfo lazyMember)
		{
			MemberInfo[] accessors = lazyMember.GetAccessors();
			MemberTypes memberType = lazyMember.MemberType;
			if (memberType <= MemberTypes.Property)
			{
				if (memberType == MemberTypes.Field)
				{
					Assumes.IsTrue(accessors.Length == 1);
					return ((FieldInfo)accessors[0]).ToReflectionField();
				}
				if (memberType == MemberTypes.Property)
				{
					Assumes.IsTrue(accessors.Length == 2);
					return ReflectionExtensions.CreateReflectionProperty((MethodInfo)accessors[0], (MethodInfo)accessors[1]);
				}
			}
			else if (memberType == MemberTypes.TypeInfo || memberType == MemberTypes.NestedType)
			{
				return ((Type)accessors[0]).ToReflectionType();
			}
			Assumes.IsTrue(memberType == MemberTypes.Method);
			return ((MethodInfo)accessors[0]).ToReflectionMethod();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00009D24 File Offset: 0x00007F24
		public static LazyMemberInfo ToLazyMember(this MemberInfo member)
		{
			Assumes.NotNull<MemberInfo>(member);
			if (member.MemberType == MemberTypes.Property)
			{
				PropertyInfo propertyInfo = member as PropertyInfo;
				Assumes.NotNull<PropertyInfo>(propertyInfo);
				MemberInfo[] accessors = new MemberInfo[]
				{
					propertyInfo.GetGetMethod(true),
					propertyInfo.GetSetMethod(true)
				};
				return new LazyMemberInfo(MemberTypes.Property, accessors);
			}
			return new LazyMemberInfo(member);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00009D78 File Offset: 0x00007F78
		public static ReflectionWritableMember ToReflectionWriteableMember(this LazyMemberInfo lazyMember)
		{
			Assumes.IsTrue(lazyMember.MemberType == MemberTypes.Field || lazyMember.MemberType == MemberTypes.Property);
			ReflectionWritableMember reflectionWritableMember = lazyMember.ToReflectionMember() as ReflectionWritableMember;
			Assumes.NotNull<ReflectionWritableMember>(reflectionWritableMember);
			return reflectionWritableMember;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public static ReflectionProperty ToReflectionProperty(this PropertyInfo property)
		{
			Assumes.NotNull<PropertyInfo>(property);
			return ReflectionExtensions.CreateReflectionProperty(property.GetGetMethod(true), property.GetSetMethod(true));
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00009DC3 File Offset: 0x00007FC3
		public static ReflectionProperty CreateReflectionProperty(MethodInfo getMethod, MethodInfo setMethod)
		{
			Assumes.IsTrue(getMethod != null || setMethod != null);
			return new ReflectionProperty(getMethod, setMethod);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00009DE4 File Offset: 0x00007FE4
		public static ReflectionParameter ToReflectionParameter(this ParameterInfo parameter)
		{
			Assumes.NotNull<ParameterInfo>(parameter);
			return new ReflectionParameter(parameter);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00009DF2 File Offset: 0x00007FF2
		public static ReflectionMethod ToReflectionMethod(this MethodInfo method)
		{
			Assumes.NotNull<MethodInfo>(method);
			return new ReflectionMethod(method);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00009E00 File Offset: 0x00008000
		public static ReflectionField ToReflectionField(this FieldInfo field)
		{
			Assumes.NotNull<FieldInfo>(field);
			return new ReflectionField(field);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00009E0E File Offset: 0x0000800E
		public static ReflectionType ToReflectionType(this Type type)
		{
			Assumes.NotNull<Type>(type);
			return new ReflectionType(type);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00009E1C File Offset: 0x0000801C
		public static ReflectionWritableMember ToReflectionWritableMember(this MemberInfo member)
		{
			Assumes.NotNull<MemberInfo>(member);
			if (member.MemberType == MemberTypes.Property)
			{
				return ((PropertyInfo)member).ToReflectionProperty();
			}
			return ((FieldInfo)member).ToReflectionField();
		}
	}
}
