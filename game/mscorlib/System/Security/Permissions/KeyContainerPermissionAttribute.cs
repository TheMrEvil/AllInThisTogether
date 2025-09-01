using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.KeyContainerPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000449 RID: 1097
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class KeyContainerPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAttribute" /> class with the specified security action.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002C7E RID: 11390 RVA: 0x0009FBFE File Offset: 0x0009DDFE
		public KeyContainerPermissionAttribute(SecurityAction action) : base(action)
		{
			this._spec = -1;
			this._type = -1;
		}

		/// <summary>Gets or sets the key container permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x0009FC15 File Offset: 0x0009DE15
		// (set) Token: 0x06002C80 RID: 11392 RVA: 0x0009FC1D File Offset: 0x0009DE1D
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Gets or sets the name of the key container.</summary>
		/// <returns>The name of the key container.</returns>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x0009FC26 File Offset: 0x0009DE26
		// (set) Token: 0x06002C82 RID: 11394 RVA: 0x0009FC2E File Offset: 0x0009DE2E
		public string KeyContainerName
		{
			get
			{
				return this._containerName;
			}
			set
			{
				this._containerName = value;
			}
		}

		/// <summary>Gets or sets the key specification.</summary>
		/// <returns>One of the AT_ values defined in the Wincrypt.h header file.</returns>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x0009FC37 File Offset: 0x0009DE37
		// (set) Token: 0x06002C84 RID: 11396 RVA: 0x0009FC3F File Offset: 0x0009DE3F
		public int KeySpec
		{
			get
			{
				return this._spec;
			}
			set
			{
				this._spec = value;
			}
		}

		/// <summary>Gets or sets the name of the key store.</summary>
		/// <returns>The name of the key store. The default is "*".</returns>
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x0009FC48 File Offset: 0x0009DE48
		// (set) Token: 0x06002C86 RID: 11398 RVA: 0x0009FC50 File Offset: 0x0009DE50
		public string KeyStore
		{
			get
			{
				return this._store;
			}
			set
			{
				this._store = value;
			}
		}

		/// <summary>Gets or sets the provider name.</summary>
		/// <returns>The name of the provider.</returns>
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x0009FC59 File Offset: 0x0009DE59
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x0009FC61 File Offset: 0x0009DE61
		public string ProviderName
		{
			get
			{
				return this._providerName;
			}
			set
			{
				this._providerName = value;
			}
		}

		/// <summary>Gets or sets the provider type.</summary>
		/// <returns>One of the PROV_ values defined in the Wincrypt.h header file.</returns>
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x0009FC6A File Offset: 0x0009DE6A
		// (set) Token: 0x06002C8A RID: 11402 RVA: 0x0009FC72 File Offset: 0x0009DE72
		public int ProviderType
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.KeyContainerPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermission" /> that corresponds to the attribute.</returns>
		// Token: 0x06002C8B RID: 11403 RVA: 0x0009FC7C File Offset: 0x0009DE7C
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			if (this.EmptyEntry())
			{
				return new KeyContainerPermission(this._flags);
			}
			KeyContainerPermissionAccessEntry[] accessList = new KeyContainerPermissionAccessEntry[]
			{
				new KeyContainerPermissionAccessEntry(this._store, this._providerName, this._type, this._containerName, this._spec, this._flags)
			};
			return new KeyContainerPermission(this._flags, accessList);
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x0009FCEB File Offset: 0x0009DEEB
		private bool EmptyEntry()
		{
			return this._containerName == null && this._spec == 0 && this._store == null && this._providerName == null && this._type == 0;
		}

		// Token: 0x04002055 RID: 8277
		private KeyContainerPermissionFlags _flags;

		// Token: 0x04002056 RID: 8278
		private string _containerName;

		// Token: 0x04002057 RID: 8279
		private int _spec;

		// Token: 0x04002058 RID: 8280
		private string _store;

		// Token: 0x04002059 RID: 8281
		private string _providerName;

		// Token: 0x0400205A RID: 8282
		private int _type;
	}
}
