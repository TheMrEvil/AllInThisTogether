using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reflectrify
{
	// Token: 0x02000002 RID: 2
	public static class AttributeMethodExtensions
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static AttributeMethodCache Cache
		{
			[CompilerGenerated]
			get
			{
				return AttributeMethodExtensions.<Cache>k__BackingField;
			}
		} = new AttributeMethodCache();

		// Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		public static T GetAttribute<T>(this MethodInfo method, string parameterName, bool inherit = true) where T : Attribute
		{
			return AttributeMethodExtensions.Cache.GetSingleAttribute<T>(method, parameterName, inherit);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002066 File Offset: 0x00000266
		public static IEnumerable<T> GetAttributes<T>(this MethodInfo method, string parameterName, bool inherit = true) where T : Attribute
		{
			return AttributeMethodExtensions.Cache.GetMultiAttributes<T>(method, parameterName, inherit) ?? new List<T>();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207E File Offset: 0x0000027E
		// Note: this type is marked as 'beforefieldinit'.
		static AttributeMethodExtensions()
		{
		}

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		private static readonly AttributeMethodCache <Cache>k__BackingField;
	}
}
