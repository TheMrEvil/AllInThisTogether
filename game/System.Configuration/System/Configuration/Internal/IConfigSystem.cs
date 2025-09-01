using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to support the initialization of configuration properties.</summary>
	// Token: 0x0200007C RID: 124
	public interface IConfigSystem
	{
		/// <summary>Gets the configuration host.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object that is used by the .NET Framework to initialize application configuration properties.</returns>
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000429 RID: 1065
		IInternalConfigHost Host { get; }

		/// <summary>Gets the root of the configuration hierarchy.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</returns>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600042A RID: 1066
		IInternalConfigRoot Root { get; }

		/// <summary>Initializes a configuration object.</summary>
		/// <param name="typeConfigHost">The type of configuration host.</param>
		/// <param name="hostInitParams">An array of configuration host parameters.</param>
		// Token: 0x0600042B RID: 1067
		void Init(Type typeConfigHost, params object[] hostInitParams);
	}
}
