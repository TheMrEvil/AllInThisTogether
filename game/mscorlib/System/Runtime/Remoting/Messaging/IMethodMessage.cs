using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method message interface.</summary>
	// Token: 0x0200061F RID: 1567
	[ComVisible(true)]
	public interface IMethodMessage : IMessage
	{
		/// <summary>Gets the number of arguments passed to the method.</summary>
		/// <returns>The number of arguments passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06003B01 RID: 15105
		int ArgCount { get; }

		/// <summary>Gets an array of arguments passed to the method.</summary>
		/// <returns>An <see cref="T:System.Object" /> array containing the arguments passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06003B02 RID: 15106
		object[] Args { get; }

		/// <summary>Gets a value indicating whether the message has variable arguments.</summary>
		/// <returns>
		///   <see langword="true" /> if the method can accept a variable number of arguments; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003B03 RID: 15107
		bool HasVarArgs { get; }

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</summary>
		/// <returns>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003B04 RID: 15108
		LogicalCallContext LogicalCallContext { get; }

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003B05 RID: 15109
		MethodBase MethodBase { get; }

		/// <summary>Gets the name of the invoked method.</summary>
		/// <returns>The name of the invoked method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06003B06 RID: 15110
		string MethodName { get; }

		/// <summary>Gets an object containing the method signature.</summary>
		/// <returns>An object containing the method signature.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06003B07 RID: 15111
		object MethodSignature { get; }

		/// <summary>Gets the full <see cref="T:System.Type" /> name of the specific object that the call is destined for.</summary>
		/// <returns>The full <see cref="T:System.Type" /> name of the specific object that the call is destined for.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06003B08 RID: 15112
		string TypeName { get; }

		/// <summary>Gets the URI of the specific object that the call is destined for.</summary>
		/// <returns>The URI of the remote object that contains the invoked method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06003B09 RID: 15113
		string Uri { get; }

		/// <summary>Gets a specific argument as an <see cref="T:System.Object" />.</summary>
		/// <param name="argNum">The number of the requested argument.</param>
		/// <returns>The argument passed to the method.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06003B0A RID: 15114
		object GetArg(int argNum);

		/// <summary>Gets the name of the argument passed to the method.</summary>
		/// <param name="index">The number of the requested argument.</param>
		/// <returns>The name of the specified argument passed to the method, or <see langword="null" /> if the current method is not implemented.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06003B0B RID: 15115
		string GetArgName(int index);
	}
}
