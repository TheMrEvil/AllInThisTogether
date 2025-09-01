using System;
using System.Collections;

namespace System.EnterpriseServices
{
	// Token: 0x02000026 RID: 38
	internal interface ISecurityCallContext
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007B RID: 123
		int Count { get; }

		// Token: 0x0600007C RID: 124
		void GetEnumerator(ref IEnumerator enumerator);

		// Token: 0x0600007D RID: 125
		object GetItem(string user);

		// Token: 0x0600007E RID: 126
		bool IsCallerInRole(string role);

		// Token: 0x0600007F RID: 127
		bool IsSecurityEnabled();

		// Token: 0x06000080 RID: 128
		bool IsUserInRole(ref object user, string role);
	}
}
