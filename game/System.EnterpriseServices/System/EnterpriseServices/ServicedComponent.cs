using System;

namespace System.EnterpriseServices
{
	/// <summary>Represents the base class of all classes using COM+ services.</summary>
	// Token: 0x02000048 RID: 72
	[Serializable]
	public abstract class ServicedComponent : ContextBoundObject, IDisposable, IRemoteDispatch, IServicedComponentInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class.</summary>
		// Token: 0x0600012F RID: 303 RVA: 0x000025AD File Offset: 0x000007AD
		public ServicedComponent()
		{
		}

		/// <summary>Called by the infrastructure when the object is created or allocated from a pool. Override this method to add custom initialization code to objects.</summary>
		// Token: 0x06000130 RID: 304 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		protected internal virtual void Activate()
		{
			throw new NotImplementedException();
		}

		/// <summary>This method is called by the infrastructure before the object is put back into the pool. Override this method to vote on whether the object is put back into the pool.</summary>
		/// <returns>
		///   <see langword="true" /> if the serviced component can be pooled; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000131 RID: 305 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		protected internal virtual bool CanBePooled()
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the infrastructure just after the constructor is called, passing in the constructor string. Override this method to make use of the construction string value.</summary>
		/// <param name="s">The construction string.</param>
		// Token: 0x06000132 RID: 306 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		protected internal virtual void Construct(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the infrastructure when the object is about to be deactivated. Override this method to add custom finalization code to objects when just-in-time (JIT) compiled code or object pooling is used.</summary>
		// Token: 0x06000133 RID: 307 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		protected internal virtual void Deactivate()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.EnterpriseServices.ServicedComponent" />.</summary>
		// Token: 0x06000134 RID: 308 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.EnterpriseServices.ServicedComponent" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; otherwise, <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000135 RID: 309 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finalizes the object and removes the associated COM+ reference.</summary>
		/// <param name="sc">The object to dispose.</param>
		// Token: 0x06000136 RID: 310 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		public static void DisposeObject(ServicedComponent sc)
		{
			throw new NotImplementedException();
		}

		/// <summary>Ensures that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's <see langword="done" /> bit is set to <see langword="true" /> after a remote method invocation.</summary>
		/// <param name="s">A string to be converted into a request object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <returns>A string converted from a response object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		// Token: 0x06000137 RID: 311 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		string IRemoteDispatch.RemoteDispatchAutoDone(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Does not ensure that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's <see langword="done" /> bit is set to <see langword="true" /> after a remote method invocation.</summary>
		/// <param name="s">A string to be converted into a request object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		/// <returns>A string converted from a response object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		// Token: 0x06000138 RID: 312 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		string IRemoteDispatch.RemoteDispatchNotAutoDone(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Obtains certain information about the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class instance.</summary>
		/// <param name="infoMask">A bitmask where 0x00000001 is a key for the serviced component's process ID, 0x00000002 is a key for the application domain ID, and 0x00000004 is a key for the serviced component's remote URI.</param>
		/// <param name="infoArray">A string array that may contain any or all of the following, in order: the serviced component's process ID, the application domain ID, and the serviced component's remote URI.</param>
		// Token: 0x06000139 RID: 313 RVA: 0x00002085 File Offset: 0x00000285
		[MonoTODO]
		void IServicedComponentInfo.GetComponentInfo(ref int infoMask, out string[] infoArray)
		{
			throw new NotImplementedException();
		}
	}
}
