using System;
using System.Reflection;

namespace Microsoft.Internal
{
	// Token: 0x0200000C RID: 12
	internal static class ReflectionInvoke
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000028B2 File Offset: 0x00000AB2
		public static object SafeCreateInstance(this Type type, params object[] arguments)
		{
			ReflectionInvoke.DemandMemberAccessIfNeeded(type);
			return Activator.CreateInstance(type, arguments);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028C1 File Offset: 0x00000AC1
		public static object SafeInvoke(this ConstructorInfo constructor, params object[] arguments)
		{
			ReflectionInvoke.DemandMemberAccessIfNeeded(constructor);
			return constructor.Invoke(arguments);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028D0 File Offset: 0x00000AD0
		public static object SafeInvoke(this MethodInfo method, object instance, params object[] arguments)
		{
			ReflectionInvoke.DemandMemberAccessIfNeeded(method);
			return method.Invoke(instance, arguments);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028E0 File Offset: 0x00000AE0
		public static object SafeGetValue(this FieldInfo field, object instance)
		{
			ReflectionInvoke.DemandMemberAccessIfNeeded(field);
			return field.GetValue(instance);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028EF File Offset: 0x00000AEF
		public static void SafeSetValue(this FieldInfo field, object instance, object value)
		{
			ReflectionInvoke.DemandMemberAccessIfNeeded(field);
			field.SetValue(instance, value);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028FF File Offset: 0x00000AFF
		public static void DemandMemberAccessIfNeeded(MethodInfo method)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028FF File Offset: 0x00000AFF
		private static void DemandMemberAccessIfNeeded(ConstructorInfo constructor)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028FF File Offset: 0x00000AFF
		private static void DemandMemberAccessIfNeeded(FieldInfo field)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028FF File Offset: 0x00000AFF
		public static void DemandMemberAccessIfNeeded(Type type)
		{
		}
	}
}
