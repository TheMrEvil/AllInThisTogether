using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a callback mechanism that can create an instance of a service on demand.</summary>
	/// <param name="container">The service container that requested the creation of the service.</param>
	/// <param name="serviceType">The type of service to create.</param>
	/// <returns>The service specified by <paramref name="serviceType" />, or <see langword="null" /> if the service could not be created.</returns>
	// Token: 0x02000471 RID: 1137
	// (Invoke) Token: 0x0600249B RID: 9371
	public delegate object ServiceCreatorCallback(IServiceContainer container, Type serviceType);
}
