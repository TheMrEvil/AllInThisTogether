using System;

namespace UnityEngine.Scripting
{
	// Token: 0x020002DC RID: 732
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true)]
	public class RequiredInterfaceAttribute : Attribute
	{
		// Token: 0x06001E04 RID: 7684 RVA: 0x00002059 File Offset: 0x00000259
		public RequiredInterfaceAttribute(Type interfaceType)
		{
		}
	}
}
