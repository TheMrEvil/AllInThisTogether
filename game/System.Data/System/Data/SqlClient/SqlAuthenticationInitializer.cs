using System;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Defines the core behavior of authentication initializers that can be registered in the app.config file and provides a base for derived classes.</summary>
	// Token: 0x020003EC RID: 1004
	public abstract class SqlAuthenticationInitializer
	{
		/// <summary>Called from constructors in derived classes to initialize the  <see cref="T:System.Data.SqlClient.SqlAuthenticationInitializer" /> class.</summary>
		// Token: 0x06002F7F RID: 12159 RVA: 0x000108A6 File Offset: 0x0000EAA6
		protected SqlAuthenticationInitializer()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, initializes the authentication initializer. This method is called by the <see cref="M:System.Data.SqlClient.SqlAuthenticationInitializer.#ctor" /> constructor during startup.</summary>
		// Token: 0x06002F80 RID: 12160
		public abstract void Initialize();
	}
}
