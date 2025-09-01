using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the method call message interface.</summary>
	// Token: 0x0200061E RID: 1566
	[ComVisible(true)]
	public interface IMethodCallMessage : IMethodMessage, IMessage
	{
		/// <summary>Gets the number of arguments in the call that are not marked as <see langword="out" /> parameters.</summary>
		/// <returns>The number of arguments in the call that are not marked as <see langword="out" /> parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06003AFD RID: 15101
		int InArgCount { get; }

		/// <summary>Gets an array of arguments that are not marked as <see langword="out" /> parameters.</summary>
		/// <returns>An array of arguments that are not marked as <see langword="out" /> parameters.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06003AFE RID: 15102
		object[] InArgs { get; }

		/// <summary>Returns the specified argument that is not marked as an <see langword="out" /> parameter.</summary>
		/// <param name="argNum">The number of the requested <see langword="in" /> argument.</param>
		/// <returns>The requested argument that is not marked as an <see langword="out" /> parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06003AFF RID: 15103
		object GetInArg(int argNum);

		/// <summary>Returns the name of the specified argument that is not marked as an <see langword="out" /> parameter.</summary>
		/// <param name="index">The number of the requested <see langword="in" /> argument.</param>
		/// <returns>The name of a specific argument that is not marked as an <see langword="out" /> parameter.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x06003B00 RID: 15104
		string GetInArgName(int index);
	}
}
