using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Specifies access rights for specific key containers. This class cannot be inherited.</summary>
	// Token: 0x02000446 RID: 1094
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class, using the specified cryptographic service provider (CSP) parameters and access permissions.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" /> object that contains the cryptographic service provider (CSP) parameters.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x06002C59 RID: 11353 RVA: 0x0009F860 File Offset: 0x0009DA60
		public KeyContainerPermissionAccessEntry(CspParameters parameters, KeyContainerPermissionFlags flags)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.ProviderName = parameters.ProviderName;
			this.ProviderType = parameters.ProviderType;
			this.KeyContainerName = parameters.KeyContainerName;
			this.KeySpec = parameters.KeyNumber;
			this.Flags = flags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class, using the specified key container name and access permissions.</summary>
		/// <param name="keyContainerName">The name of the key container.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x06002C5A RID: 11354 RVA: 0x0009F8B8 File Offset: 0x0009DAB8
		public KeyContainerPermissionAccessEntry(string keyContainerName, KeyContainerPermissionFlags flags)
		{
			this.KeyContainerName = keyContainerName;
			this.Flags = flags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> class with the specified property values.</summary>
		/// <param name="keyStore">The name of the key store.</param>
		/// <param name="providerName">The name of the provider.</param>
		/// <param name="providerType">The type code for the provider. See the <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.ProviderType" /> property for values.</param>
		/// <param name="keyContainerName">The name of the key container.</param>
		/// <param name="keySpec">The key specification. See the <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntry.KeySpec" /> property for values.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x06002C5B RID: 11355 RVA: 0x0009F8CE File Offset: 0x0009DACE
		public KeyContainerPermissionAccessEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec, KeyContainerPermissionFlags flags)
		{
			this.KeyStore = keyStore;
			this.ProviderName = providerName;
			this.ProviderType = providerType;
			this.KeyContainerName = keyContainerName;
			this.KeySpec = keySpec;
			this.Flags = flags;
		}

		/// <summary>Gets or sets the key container permissions.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values. The default is <see cref="F:System.Security.Permissions.KeyContainerPermissionFlags.NoFlags" />.</returns>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x0009F903 File Offset: 0x0009DB03
		// (set) Token: 0x06002C5D RID: 11357 RVA: 0x0009F90B File Offset: 0x0009DB0B
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				if ((value & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.NoFlags)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "KeyContainerPermissionFlags");
				}
				this._flags = value;
			}
		}

		/// <summary>Gets or sets the key container name.</summary>
		/// <returns>The name of the key container.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x0009F93D File Offset: 0x0009DB3D
		// (set) Token: 0x06002C5F RID: 11359 RVA: 0x0009F945 File Offset: 0x0009DB45
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
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x0009F94E File Offset: 0x0009DB4E
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x0009F956 File Offset: 0x0009DB56
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
		/// <returns>The name of the key store.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x0009F95F File Offset: 0x0009DB5F
		// (set) Token: 0x06002C63 RID: 11363 RVA: 0x0009F967 File Offset: 0x0009DB67
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
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002C64 RID: 11364 RVA: 0x0009F970 File Offset: 0x0009DB70
		// (set) Token: 0x06002C65 RID: 11365 RVA: 0x0009F978 File Offset: 0x0009DB78
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
		/// <exception cref="T:System.ArgumentException">The resulting entry would have unrestricted access.</exception>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002C66 RID: 11366 RVA: 0x0009F981 File Offset: 0x0009DB81
		// (set) Token: 0x06002C67 RID: 11367 RVA: 0x0009F989 File Offset: 0x0009DB89
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

		/// <summary>Determines whether the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object is equal to the current instance.</summary>
		/// <param name="o">The <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object to compare with the currentinstance.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> is equal to the current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002C68 RID: 11368 RVA: 0x0009F994 File Offset: 0x0009DB94
		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = o as KeyContainerPermissionAccessEntry;
			return keyContainerPermissionAccessEntry != null && this._flags == keyContainerPermissionAccessEntry._flags && !(this._containerName != keyContainerPermissionAccessEntry._containerName) && !(this._store != keyContainerPermissionAccessEntry._store) && !(this._providerName != keyContainerPermissionAccessEntry._providerName) && this._type == keyContainerPermissionAccessEntry._type;
		}

		/// <summary>Gets a hash code for the current instance that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> object.</returns>
		// Token: 0x06002C69 RID: 11369 RVA: 0x0009FA14 File Offset: 0x0009DC14
		public override int GetHashCode()
		{
			int num = this._type ^ this._spec ^ (int)this._flags;
			if (this._containerName != null)
			{
				num ^= this._containerName.GetHashCode();
			}
			if (this._store != null)
			{
				num ^= this._store.GetHashCode();
			}
			if (this._providerName != null)
			{
				num ^= this._providerName.GetHashCode();
			}
			return num;
		}

		// Token: 0x0400204D RID: 8269
		private KeyContainerPermissionFlags _flags;

		// Token: 0x0400204E RID: 8270
		private string _containerName;

		// Token: 0x0400204F RID: 8271
		private int _spec;

		// Token: 0x04002050 RID: 8272
		private string _store;

		// Token: 0x04002051 RID: 8273
		private string _providerName;

		// Token: 0x04002052 RID: 8274
		private int _type;
	}
}
