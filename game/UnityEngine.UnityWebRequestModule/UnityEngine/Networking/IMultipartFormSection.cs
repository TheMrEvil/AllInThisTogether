using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000005 RID: 5
	public interface IMultipartFormSection
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46
		string sectionName { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002F RID: 47
		byte[] sectionData { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000030 RID: 48
		string fileName { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000031 RID: 49
		string contentType { get; }
	}
}
