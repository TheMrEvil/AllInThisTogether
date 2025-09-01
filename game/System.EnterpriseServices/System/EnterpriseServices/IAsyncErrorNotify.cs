using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Implements error trapping on the asynchronous batch work that is submitted by the <see cref="T:System.EnterpriseServices.Activity" /> object.</summary>
	// Token: 0x0200001E RID: 30
	[Guid("FE6777FB-A674-4177-8F32-6D707E113484")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IAsyncErrorNotify
	{
		/// <summary>Handles errors for asynchronous batch work.</summary>
		/// <param name="hresult">The HRESULT of the error that occurred while the batch work was running asynchronously.</param>
		// Token: 0x0600006B RID: 107
		void OnError(int hresult);
	}
}
