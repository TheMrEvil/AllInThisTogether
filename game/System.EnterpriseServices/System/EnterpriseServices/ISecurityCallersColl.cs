using System;
using System.Collections;

namespace System.EnterpriseServices
{
	// Token: 0x02000027 RID: 39
	internal interface ISecurityCallersColl
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000081 RID: 129
		int Count { get; }

		// Token: 0x06000082 RID: 130
		void GetEnumerator(out IEnumerator enumerator);

		// Token: 0x06000083 RID: 131
		ISecurityIdentityColl GetItem(int idx);
	}
}
