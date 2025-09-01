using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents assembly binding information that can be added to an instance of <see cref="T:System.AppDomain" />.</summary>
	// Token: 0x02000203 RID: 515
	[Guid("27FFF232-A7A8-40dd-8D4A-734AD59FCD41")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComVisible(true)]
	public interface IAppDomainSetup
	{
		/// <summary>Gets or sets the name of the directory containing the application.</summary>
		/// <returns>A <see cref="T:System.String" /> containg the name of the application base directory.</returns>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600165B RID: 5723
		// (set) Token: 0x0600165C RID: 5724
		string ApplicationBase { get; set; }

		/// <summary>Gets or sets the name of the application.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the name of the application.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600165D RID: 5725
		// (set) Token: 0x0600165E RID: 5726
		string ApplicationName { get; set; }

		/// <summary>Gets or sets the name of an area specific to the application where files are shadow copied.</summary>
		/// <returns>A <see cref="T:System.String" /> that is the fully-qualified name of the directory path and file name where files are shadow copied.</returns>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600165F RID: 5727
		// (set) Token: 0x06001660 RID: 5728
		string CachePath { get; set; }

		/// <summary>Gets or sets the name of the configuration file for an application domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the configuration file.</returns>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001661 RID: 5729
		// (set) Token: 0x06001662 RID: 5730
		string ConfigurationFile { get; set; }

		/// <summary>Gets or sets the directory where dynamically generated files are stored and accessed.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the directory containing dynamic assemblies.</returns>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001663 RID: 5731
		// (set) Token: 0x06001664 RID: 5732
		string DynamicBase { get; set; }

		/// <summary>Gets or sets the location of the license file associated with this domain.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the name of the license file.</returns>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001665 RID: 5733
		// (set) Token: 0x06001666 RID: 5734
		string LicenseFile { get; set; }

		/// <summary>Gets or sets the list of directories that is combined with the <see cref="P:System.AppDomainSetup.ApplicationBase" /> directory to probe for private assemblies.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001667 RID: 5735
		// (set) Token: 0x06001668 RID: 5736
		string PrivateBinPath { get; set; }

		/// <summary>Gets or sets the private binary directory path used to locate an application.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001669 RID: 5737
		// (set) Token: 0x0600166A RID: 5738
		string PrivateBinPathProbe { get; set; }

		/// <summary>Gets or sets the names of the directories containing assemblies to be shadow copied.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a list of directory names, where each name is separated by a semicolon.</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600166B RID: 5739
		// (set) Token: 0x0600166C RID: 5740
		string ShadowCopyDirectories { get; set; }

		/// <summary>Gets or sets a string that indicates whether shadow copying is turned on or off.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value "true" to indicate that shadow copying is turned on; or "false" to indicate that shadow copying is turned off.</returns>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600166D RID: 5741
		// (set) Token: 0x0600166E RID: 5742
		string ShadowCopyFiles { get; set; }
	}
}
