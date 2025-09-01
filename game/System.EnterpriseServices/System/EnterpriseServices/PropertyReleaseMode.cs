using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the release mode for the properties in the new shared property group.</summary>
	// Token: 0x02000039 RID: 57
	[ComVisible(false)]
	[Serializable]
	public enum PropertyReleaseMode
	{
		/// <summary>The property group is not destroyed until the process in which it was created has terminated.</summary>
		// Token: 0x0400007B RID: 123
		Process = 1,
		/// <summary>When all clients have released their references on the property group, the property group is automatically destroyed. This is the default COM mode.</summary>
		// Token: 0x0400007C RID: 124
		Standard = 0
	}
}
