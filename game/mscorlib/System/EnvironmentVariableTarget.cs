using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the location where an environment variable is stored or retrieved in a set or get operation.</summary>
	// Token: 0x02000237 RID: 567
	[ComVisible(true)]
	public enum EnvironmentVariableTarget
	{
		/// <summary>The environment variable is stored or retrieved from the environment block associated with the current process.</summary>
		// Token: 0x04001719 RID: 5913
		Process,
		/// <summary>The environment variable is stored or retrieved from the <see langword="HKEY_CURRENT_USER\Environment" /> key in the Windows operating system registry.</summary>
		// Token: 0x0400171A RID: 5914
		User,
		/// <summary>The environment variable is stored or retrieved from the <see langword="HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Session Manager\Environment" /> key in the Windows operating system registry.</summary>
		// Token: 0x0400171B RID: 5915
		Machine
	}
}
