using System;
using System.Reflection;

namespace System.Xml.Linq
{
	// Token: 0x0200003E RID: 62
	internal static class XHelper
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000B524 File Offset: 0x00009724
		internal static bool IsInstanceOfType(object o, Type type)
		{
			return o != null && type.GetTypeInfo().IsAssignableFrom(o.GetType().GetTypeInfo());
		}
	}
}
