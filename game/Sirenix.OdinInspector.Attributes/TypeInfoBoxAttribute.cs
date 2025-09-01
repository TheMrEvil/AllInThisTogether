using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000076 RID: 118
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class TypeInfoBoxAttribute : Attribute
	{
		// Token: 0x06000185 RID: 389 RVA: 0x00003D8D File Offset: 0x00001F8D
		public TypeInfoBoxAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x0400014E RID: 334
		public string Message;
	}
}
