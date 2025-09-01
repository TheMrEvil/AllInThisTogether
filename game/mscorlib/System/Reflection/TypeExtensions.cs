using System;

namespace System.Reflection
{
	// Token: 0x020008D4 RID: 2260
	public static class TypeExtensions
	{
		// Token: 0x06004B4C RID: 19276 RVA: 0x000F0473 File Offset: 0x000EE673
		public static ConstructorInfo GetConstructor(Type type, Type[] types)
		{
			Requires.NotNull(type, "type");
			return type.GetConstructor(types);
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x000F0487 File Offset: 0x000EE687
		public static ConstructorInfo[] GetConstructors(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetConstructors();
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x000F049A File Offset: 0x000EE69A
		public static ConstructorInfo[] GetConstructors(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetConstructors(bindingAttr);
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x000F04AE File Offset: 0x000EE6AE
		public static MemberInfo[] GetDefaultMembers(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetDefaultMembers();
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x000F04C1 File Offset: 0x000EE6C1
		public static EventInfo GetEvent(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetEvent(name);
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x000F04D5 File Offset: 0x000EE6D5
		public static EventInfo GetEvent(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x000F04EA File Offset: 0x000EE6EA
		public static EventInfo[] GetEvents(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetEvents();
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x000F04FD File Offset: 0x000EE6FD
		public static EventInfo[] GetEvents(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetEvents(bindingAttr);
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x000F0511 File Offset: 0x000EE711
		public static FieldInfo GetField(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetField(name);
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x000F0525 File Offset: 0x000EE725
		public static FieldInfo GetField(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetField(name, bindingAttr);
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x000F053A File Offset: 0x000EE73A
		public static FieldInfo[] GetFields(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetFields();
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x000F054D File Offset: 0x000EE74D
		public static FieldInfo[] GetFields(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetFields(bindingAttr);
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x000F0561 File Offset: 0x000EE761
		public static Type[] GetGenericArguments(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetGenericArguments();
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x000F0574 File Offset: 0x000EE774
		public static Type[] GetInterfaces(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetInterfaces();
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x000F0587 File Offset: 0x000EE787
		public static MemberInfo[] GetMember(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetMember(name);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x000F059B File Offset: 0x000EE79B
		public static MemberInfo[] GetMember(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMember(name, bindingAttr);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x000F05B0 File Offset: 0x000EE7B0
		public static MemberInfo[] GetMembers(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetMembers();
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x000F05C3 File Offset: 0x000EE7C3
		public static MemberInfo[] GetMembers(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMembers(bindingAttr);
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x000F05D7 File Offset: 0x000EE7D7
		public static MethodInfo GetMethod(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetMethod(name);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x000F05EB File Offset: 0x000EE7EB
		public static MethodInfo GetMethod(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMethod(name, bindingAttr);
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x000F0600 File Offset: 0x000EE800
		public static MethodInfo GetMethod(Type type, string name, Type[] types)
		{
			Requires.NotNull(type, "type");
			return type.GetMethod(name, types);
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x000F0615 File Offset: 0x000EE815
		public static MethodInfo[] GetMethods(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetMethods();
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x000F0628 File Offset: 0x000EE828
		public static MethodInfo[] GetMethods(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMethods(bindingAttr);
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x000F063C File Offset: 0x000EE83C
		public static Type GetNestedType(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x000F0651 File Offset: 0x000EE851
		public static Type[] GetNestedTypes(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x000F0665 File Offset: 0x000EE865
		public static PropertyInfo[] GetProperties(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetProperties();
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x000F0678 File Offset: 0x000EE878
		public static PropertyInfo[] GetProperties(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetProperties(bindingAttr);
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x000F068C File Offset: 0x000EE88C
		public static PropertyInfo GetProperty(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name);
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x000F06A0 File Offset: 0x000EE8A0
		public static PropertyInfo GetProperty(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name, bindingAttr);
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x000F06B5 File Offset: 0x000EE8B5
		public static PropertyInfo GetProperty(Type type, string name, Type returnType)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name, returnType);
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x000F06CA File Offset: 0x000EE8CA
		public static PropertyInfo GetProperty(Type type, string name, Type returnType, Type[] types)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name, returnType, types);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x000F06E0 File Offset: 0x000EE8E0
		public static bool IsAssignableFrom(Type type, Type c)
		{
			Requires.NotNull(type, "type");
			return type.IsAssignableFrom(c);
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x000F06F4 File Offset: 0x000EE8F4
		public static bool IsInstanceOfType(Type type, object o)
		{
			Requires.NotNull(type, "type");
			return type.IsInstanceOfType(o);
		}
	}
}
