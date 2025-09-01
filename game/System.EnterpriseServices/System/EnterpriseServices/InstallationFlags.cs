using System;

namespace System.EnterpriseServices
{
	/// <summary>Flags used with the <see cref="T:System.EnterpriseServices.RegistrationHelper" /> class.</summary>
	// Token: 0x02000030 RID: 48
	[Flags]
	[Serializable]
	public enum InstallationFlags
	{
		/// <summary>Should not be used.</summary>
		// Token: 0x04000060 RID: 96
		Configure = 1024,
		/// <summary>Configures components only, do not configure methods or interfaces.</summary>
		// Token: 0x04000061 RID: 97
		ConfigureComponentsOnly = 16,
		/// <summary>Creates the target application. An error occurs if the target already exists.</summary>
		// Token: 0x04000062 RID: 98
		CreateTargetApplication = 2,
		/// <summary>Do the default installation, which configures, installs, and registers, and assumes that the application already exists.</summary>
		// Token: 0x04000063 RID: 99
		Default = 0,
		/// <summary>Do not export the type library; one can be found either by the generated or supplied type library name.</summary>
		// Token: 0x04000064 RID: 100
		ExpectExistingTypeLib = 1,
		/// <summary>Creates the application if it does not exist; otherwise use the existing application.</summary>
		// Token: 0x04000065 RID: 101
		FindOrCreateTargetApplication = 4,
		/// <summary>Should not be used.</summary>
		// Token: 0x04000066 RID: 102
		Install = 512,
		/// <summary>If using an existing application, ensures that the properties on this application match those in the assembly.</summary>
		// Token: 0x04000067 RID: 103
		ReconfigureExistingApplication = 8,
		/// <summary>Should not be used.</summary>
		// Token: 0x04000068 RID: 104
		Register = 256,
		/// <summary>When alert text is encountered, writes it to the Console.</summary>
		// Token: 0x04000069 RID: 105
		ReportWarningsToConsole = 32
	}
}
