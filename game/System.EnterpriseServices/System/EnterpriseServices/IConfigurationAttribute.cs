using System;
using System.Collections;

namespace System.EnterpriseServices
{
	// Token: 0x0200001F RID: 31
	internal interface IConfigurationAttribute
	{
		// Token: 0x0600006C RID: 108
		bool AfterSaveChanges(Hashtable info);

		// Token: 0x0600006D RID: 109
		bool Apply(Hashtable info);

		// Token: 0x0600006E RID: 110
		bool IsValidTarget(string s);
	}
}
