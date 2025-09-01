using System;

namespace System.Configuration.Assemblies
{
	/// <summary>Defines the different types of assembly version compatibility. This feature is not available in version 1.0 of the .NET Framework.</summary>
	// Token: 0x02000A0D RID: 2573
	public enum AssemblyVersionCompatibility
	{
		/// <summary>The assembly cannot execute with other versions if they are executing on the same machine.</summary>
		// Token: 0x0400385E RID: 14430
		SameMachine = 1,
		/// <summary>The assembly cannot execute with other versions if they are executing in the same process.</summary>
		// Token: 0x0400385F RID: 14431
		SameProcess,
		/// <summary>The assembly cannot execute with other versions if they are executing in the same application domain.</summary>
		// Token: 0x04003860 RID: 14432
		SameDomain
	}
}
