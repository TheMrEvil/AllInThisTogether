using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020007AC RID: 1964
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IReflect
	{
		// Token: 0x0600453B RID: 17723
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600453C RID: 17724
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x0600453D RID: 17725
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x0600453E RID: 17726
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x0600453F RID: 17727
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x06004540 RID: 17728
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x06004541 RID: 17729
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06004542 RID: 17730
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06004543 RID: 17731
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x06004544 RID: 17732
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x06004545 RID: 17733
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06004546 RID: 17734
		Type UnderlyingSystemType { get; }
	}
}
