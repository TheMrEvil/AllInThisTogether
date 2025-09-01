using System;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020007A8 RID: 1960
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IExpando : IReflect
	{
		// Token: 0x0600451D RID: 17693
		FieldInfo AddField(string name);

		// Token: 0x0600451E RID: 17694
		PropertyInfo AddProperty(string name);

		// Token: 0x0600451F RID: 17695
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x06004520 RID: 17696
		void RemoveMember(MemberInfo m);
	}
}
