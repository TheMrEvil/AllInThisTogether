using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000626 RID: 1574
	internal class MCMDictionary : MessageDictionary
	{
		// Token: 0x06003B42 RID: 15170 RVA: 0x000CEDC0 File Offset: 0x000CCFC0
		public MCMDictionary(IMethodMessage message) : base(message)
		{
			base.MethodKeys = MCMDictionary.InternalKeys;
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000CEDD4 File Offset: 0x000CCFD4
		// Note: this type is marked as 'beforefieldinit'.
		static MCMDictionary()
		{
		}

		// Token: 0x0400269F RID: 9887
		public static string[] InternalKeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__TypeName",
			"__MethodSignature",
			"__Args",
			"__CallContext"
		};
	}
}
