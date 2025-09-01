using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200062C RID: 1580
	internal class MethodReturnDictionary : MessageDictionary
	{
		// Token: 0x06003BA0 RID: 15264 RVA: 0x000D0059 File Offset: 0x000CE259
		public MethodReturnDictionary(IMethodReturnMessage message) : base(message)
		{
			if (message.Exception == null)
			{
				base.MethodKeys = MethodReturnDictionary.InternalReturnKeys;
				return;
			}
			base.MethodKeys = MethodReturnDictionary.InternalExceptionKeys;
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x000D0084 File Offset: 0x000CE284
		// Note: this type is marked as 'beforefieldinit'.
		static MethodReturnDictionary()
		{
		}

		// Token: 0x040026BB RID: 9915
		public static string[] InternalReturnKeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__TypeName",
			"__MethodSignature",
			"__OutArgs",
			"__Return",
			"__CallContext"
		};

		// Token: 0x040026BC RID: 9916
		public static string[] InternalExceptionKeys = new string[]
		{
			"__CallContext"
		};
	}
}
