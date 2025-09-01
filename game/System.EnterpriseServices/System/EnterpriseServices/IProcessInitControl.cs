using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Supports setting the time-out for the <see cref="M:System.EnterpriseServices.IProcessInitializer.Startup(System.Object)" /> method.</summary>
	// Token: 0x02000022 RID: 34
	[Guid("72380d55-8d2b-43a3-8513-2b6ef31434e9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IProcessInitControl
	{
		/// <summary>Sets the number of seconds remaining before the <see cref="M:System.EnterpriseServices.IProcessInitializer.Startup(System.Object)" /> method times out.</summary>
		/// <param name="dwSecondsRemaining">The number of seconds that remain before the time taken to execute the start up method times out.</param>
		// Token: 0x06000074 RID: 116
		void ResetInitializerTimeout(int dwSecondsRemaining);
	}
}
