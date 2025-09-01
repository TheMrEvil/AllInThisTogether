using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Defines the types of connections to a class object.</summary>
	// Token: 0x020006CD RID: 1741
	[Flags]
	public enum RegistrationConnectionType
	{
		/// <summary>Once an application is connected to a class object with <see langword="CoGetClassObject" />, the class object is removed from public view so that no other applications can connect to it. This value is commonly used for single document interface (SDI) applications.</summary>
		// Token: 0x04002A09 RID: 10761
		SingleUse = 0,
		/// <summary>Multiple applications can connect to the class object through calls to <see langword="CoGetClassObject" />.</summary>
		// Token: 0x04002A0A RID: 10762
		MultipleUse = 1,
		/// <summary>Registers separate CLSCTX_LOCAL_SERVER and CLSCTX_INPROC_SERVER class factories.</summary>
		// Token: 0x04002A0B RID: 10763
		MultiSeparate = 2,
		/// <summary>Suspends registration and activation requests for the specified CLSID until there is a call to <see langword="CoResumeClassObjects" />.</summary>
		// Token: 0x04002A0C RID: 10764
		Suspended = 4,
		/// <summary>The class object is a surrogate process used to run DLL servers.</summary>
		// Token: 0x04002A0D RID: 10765
		Surrogate = 8
	}
}
