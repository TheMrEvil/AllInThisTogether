using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200092C RID: 2348
	internal interface TokenGenerator
	{
		// Token: 0x0600509C RID: 20636
		int GetToken(string str);

		// Token: 0x0600509D RID: 20637
		int GetToken(MemberInfo member, bool create_open_instance);

		// Token: 0x0600509E RID: 20638
		int GetToken(MethodBase method, Type[] opt_param_types);

		// Token: 0x0600509F RID: 20639
		int GetToken(SignatureHelper helper);
	}
}
