using System;
using System.Collections;

namespace System.EnterpriseServices
{
	// Token: 0x02000028 RID: 40
	internal interface ISecurityIdentityColl
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000084 RID: 132
		int Count { get; }

		// Token: 0x06000085 RID: 133
		void GetEnumerator(out IEnumerator enumerator);

		// Token: 0x06000086 RID: 134
		SecurityIdentity GetItem(int idx);
	}
}
