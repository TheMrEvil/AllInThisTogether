using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Proxies
{
	/// <summary>Indicates that an object type requires a custom proxy.</summary>
	// Token: 0x0200057E RID: 1406
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class)]
	public class ProxyAttribute : Attribute, IContextAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Proxies.ProxyAttribute" /> class with default values.</summary>
		// Token: 0x06003716 RID: 14102 RVA: 0x00002050 File Offset: 0x00000250
		public ProxyAttribute()
		{
		}

		/// <summary>Creates either an uninitialized <see cref="T:System.MarshalByRefObject" /> or a transparent proxy, depending on whether the specified type can exist in the current context.</summary>
		/// <param name="serverType">The object type to create an instance of.</param>
		/// <returns>An uninitialized <see cref="T:System.MarshalByRefObject" /> or a transparent proxy.</returns>
		// Token: 0x06003717 RID: 14103 RVA: 0x000C6D44 File Offset: 0x000C4F44
		public virtual MarshalByRefObject CreateInstance(Type serverType)
		{
			return (MarshalByRefObject)new RemotingProxy(serverType, ChannelServices.CrossContextUrl, null).GetTransparentProxy();
		}

		/// <summary>Creates an instance of a remoting proxy for a remote object described by the specified <see cref="T:System.Runtime.Remoting.ObjRef" />, and located on the server.</summary>
		/// <param name="objRef">The object reference to the remote object for which to create a proxy.</param>
		/// <param name="serverType">The type of the server where the remote object is located.</param>
		/// <param name="serverObject">The server object.</param>
		/// <param name="serverContext">The context in which the server object is located.</param>
		/// <returns>The new instance of remoting proxy for the remote object that is described in the specified <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		// Token: 0x06003718 RID: 14104 RVA: 0x000C6D5C File Offset: 0x000C4F5C
		public virtual RealProxy CreateProxy(ObjRef objRef, Type serverType, object serverObject, Context serverContext)
		{
			return RemotingServices.GetRealProxy(RemotingServices.GetProxyForRemoteObject(objRef, serverType));
		}

		/// <summary>Gets properties for a new context.</summary>
		/// <param name="msg">The message for which the context is to be retrieved.</param>
		// Token: 0x06003719 RID: 14105 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[SecurityCritical]
		[ComVisible(true)]
		public void GetPropertiesForNewContext(IConstructionCallMessage msg)
		{
		}

		/// <summary>Checks the specified context.</summary>
		/// <param name="ctx">The context to be verified.</param>
		/// <param name="msg">The message for the remote call.</param>
		/// <returns>The specified context.</returns>
		// Token: 0x0600371A RID: 14106 RVA: 0x000040F7 File Offset: 0x000022F7
		[ComVisible(true)]
		[SecurityCritical]
		public bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}
	}
}
