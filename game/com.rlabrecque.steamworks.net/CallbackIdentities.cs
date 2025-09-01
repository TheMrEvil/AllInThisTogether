using System;

namespace Steamworks
{
	// Token: 0x02000183 RID: 387
	internal class CallbackIdentities
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public static int GetCallbackIdentity(Type callbackStruct)
		{
			object[] customAttributes = callbackStruct.GetCustomAttributes(typeof(CallbackIdentityAttribute), false);
			int num = 0;
			if (num >= customAttributes.Length)
			{
				throw new Exception("Callback number not found for struct " + ((callbackStruct != null) ? callbackStruct.ToString() : null));
			}
			return ((CallbackIdentityAttribute)customAttributes[num]).Identity;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0000CDC7 File Offset: 0x0000AFC7
		public CallbackIdentities()
		{
		}
	}
}
