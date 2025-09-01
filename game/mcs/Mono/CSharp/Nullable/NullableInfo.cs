using System;

namespace Mono.CSharp.Nullable
{
	// Token: 0x020002FC RID: 764
	internal static class NullableInfo
	{
		// Token: 0x06002446 RID: 9286 RVA: 0x000ADC79 File Offset: 0x000ABE79
		public static MethodSpec GetConstructor(TypeSpec nullableType)
		{
			return (MethodSpec)MemberCache.FindMember(nullableType, MemberFilter.Constructor(ParametersCompiled.CreateFullyResolved(new TypeSpec[]
			{
				NullableInfo.GetUnderlyingType(nullableType)
			})), BindingRestriction.DeclaredOnly);
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000ADCA0 File Offset: 0x000ABEA0
		public static MethodSpec GetHasValue(TypeSpec nullableType)
		{
			return (MethodSpec)MemberCache.FindMember(nullableType, MemberFilter.Method("get_HasValue", 0, ParametersCompiled.EmptyReadOnlyParameters, null), BindingRestriction.None);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000ADCBF File Offset: 0x000ABEBF
		public static MethodSpec GetGetValueOrDefault(TypeSpec nullableType)
		{
			return (MethodSpec)MemberCache.FindMember(nullableType, MemberFilter.Method("GetValueOrDefault", 0, ParametersCompiled.EmptyReadOnlyParameters, null), BindingRestriction.None);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000ADCDE File Offset: 0x000ABEDE
		public static MethodSpec GetValue(TypeSpec nullableType)
		{
			return (MethodSpec)MemberCache.FindMember(nullableType, MemberFilter.Method("get_Value", 0, ParametersCompiled.EmptyReadOnlyParameters, null), BindingRestriction.None);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000ADCFD File Offset: 0x000ABEFD
		public static TypeSpec GetUnderlyingType(TypeSpec nullableType)
		{
			return ((InflatedTypeSpec)nullableType).TypeArguments[0];
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000ADD0C File Offset: 0x000ABF0C
		public static TypeSpec GetEnumUnderlyingType(ModuleContainer module, TypeSpec nullableEnum)
		{
			return NullableInfo.MakeType(module, EnumSpec.GetUnderlyingType(NullableInfo.GetUnderlyingType(nullableEnum)));
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000ADD1F File Offset: 0x000ABF1F
		public static TypeSpec MakeType(ModuleContainer module, TypeSpec underlyingType)
		{
			return module.PredefinedTypes.Nullable.TypeSpec.MakeGenericType(module, new TypeSpec[]
			{
				underlyingType
			});
		}
	}
}
