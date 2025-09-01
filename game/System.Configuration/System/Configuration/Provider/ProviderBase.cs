using System;
using System.Collections.Specialized;

namespace System.Configuration.Provider
{
	/// <summary>Provides a base implementation for the extensible provider model.</summary>
	// Token: 0x02000077 RID: 119
	public abstract class ProviderBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Provider.ProviderBase" /> class.</summary>
		// Token: 0x060003E4 RID: 996 RVA: 0x00002050 File Offset: 0x00000250
		protected ProviderBase()
		{
		}

		/// <summary>Initializes the configuration builder.</summary>
		/// <param name="name">The friendly name of the provider.</param>
		/// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
		/// <exception cref="T:System.ArgumentNullException">The name of the provider is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" /> on a provider after the provider has already been initialized.</exception>
		// Token: 0x060003E5 RID: 997 RVA: 0x0000AE50 File Offset: 0x00009050
		public virtual void Initialize(string name, NameValueCollection config)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Provider name cannot be null or empty.", "name");
			}
			if (this.alreadyInitialized)
			{
				throw new InvalidOperationException("This provider instance has already been initialized.");
			}
			this.alreadyInitialized = true;
			this._name = name;
			if (config != null)
			{
				this._description = config["description"];
				config.Remove("description");
			}
			if (string.IsNullOrEmpty(this._description))
			{
				this._description = this._name;
			}
		}

		/// <summary>Gets the friendly name used to refer to the provider during configuration.</summary>
		/// <returns>The friendly name used to refer to the provider during configuration.</returns>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000AEDC File Offset: 0x000090DC
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets a brief, friendly description suitable for display in administrative tools or other user interfaces (UIs).</summary>
		/// <returns>A brief, friendly description suitable for display in administrative tools or other UIs.</returns>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public virtual string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x04000160 RID: 352
		private bool alreadyInitialized;

		// Token: 0x04000161 RID: 353
		private string _description;

		// Token: 0x04000162 RID: 354
		private string _name;
	}
}
