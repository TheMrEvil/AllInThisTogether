using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Creates an activity to do synchronous or asynchronous batch work that can use COM+ services without needing to create a COM+ component. This class cannot be inherited.</summary>
	// Token: 0x0200000B RID: 11
	[ComVisible(false)]
	public sealed class Activity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Activity" /> class.</summary>
		/// <param name="cfg">A <see cref="T:System.EnterpriseServices.ServiceConfig" /> that contains the configuration information for the services to be used.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="T:System.EnterpriseServices.Activity" /> is not supported on the current platform.</exception>
		// Token: 0x06000009 RID: 9 RVA: 0x00002078 File Offset: 0x00000278
		[MonoTODO]
		public Activity(ServiceConfig cfg)
		{
			throw new NotImplementedException();
		}

		/// <summary>Runs the specified user-defined batch work asynchronously.</summary>
		/// <param name="serviceCall">A <see cref="T:System.EnterpriseServices.IServiceCall" /> object that is used to implement the batch work.</param>
		// Token: 0x0600000A RID: 10 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void AsynchronousCall(IServiceCall serviceCall)
		{
			throw new NotImplementedException();
		}

		/// <summary>Binds the user-defined work to the current thread.</summary>
		// Token: 0x0600000B RID: 11 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void BindToCurrentThread()
		{
			throw new NotImplementedException();
		}

		/// <summary>Runs the specified user-defined batch work synchronously.</summary>
		/// <param name="serviceCall">A <see cref="T:System.EnterpriseServices.IServiceCall" /> object that is used to implement the batch work.</param>
		// Token: 0x0600000C RID: 12 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void SynchronousCall(IServiceCall serviceCall)
		{
			throw new NotImplementedException();
		}

		/// <summary>Unbinds the batch work that is submitted by the <see cref="M:System.EnterpriseServices.Activity.SynchronousCall(System.EnterpriseServices.IServiceCall)" /> or <see cref="M:System.EnterpriseServices.Activity.AsynchronousCall(System.EnterpriseServices.IServiceCall)" /> methods from the thread on which the batch work is running.</summary>
		// Token: 0x0600000D RID: 13 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void UnbindFromThread()
		{
			throw new NotImplementedException();
		}
	}
}
