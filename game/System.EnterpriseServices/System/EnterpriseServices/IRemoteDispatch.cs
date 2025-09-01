using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Implemented by the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class to determine if the <see cref="T:System.EnterpriseServices.AutoCompleteAttribute" /> class attribute is set to <see langword="true" /> or <see langword="false" /> for a remote method invocation.</summary>
	// Token: 0x02000025 RID: 37
	[Guid("6619a740-8154-43be-a186-0319578e02db")]
	public interface IRemoteDispatch
	{
		/// <summary>Ensures that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's done bit is set to <see langword="true" /> after a remote method invocation.</summary>
		/// <param name="s">A string to be converted into a request object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <returns>A string converted from a response object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		// Token: 0x06000079 RID: 121
		[AutoComplete]
		string RemoteDispatchAutoDone(string s);

		/// <summary>Does not ensure that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's done bit is set to <see langword="true" /> after a remote method invocation.</summary>
		/// <param name="s">A string to be converted into a request object implementing the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <returns>A string converted from a response object implementing the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		// Token: 0x0600007A RID: 122
		[AutoComplete(false)]
		string RemoteDispatchNotAutoDone(string s);
	}
}
