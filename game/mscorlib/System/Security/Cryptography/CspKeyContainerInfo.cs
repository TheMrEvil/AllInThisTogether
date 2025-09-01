using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	/// <summary>Provides additional information about a cryptographic key pair. This class cannot be inherited.</summary>
	// Token: 0x020004C7 RID: 1223
	[ComVisible(true)]
	public sealed class CspKeyContainerInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> class using the specified parameters.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" /> object that provides information about the key.</param>
		// Token: 0x060030E5 RID: 12517 RVA: 0x000B31EF File Offset: 0x000B13EF
		public CspKeyContainerInfo(CspParameters parameters)
		{
			this._params = parameters;
			this._random = true;
		}

		/// <summary>Gets a value indicating whether a key in a key container is accessible.</summary>
		/// <returns>
		///   <see langword="true" /> if the key is accessible; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool Accessible
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key type is not supported.</exception>
		/// <exception cref="T:System.NotSupportedException">The cryptographic service provider cannot be found.  
		///  -or-  
		///  The key container was not found.</exception>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x0000AF5E File Offset: 0x0000915E
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a value indicating whether a key can be exported from a key container.</summary>
		/// <returns>
		///   <see langword="true" /> if the key can be exported; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.  
		///  -or-  
		///  The key container was not found.</exception>
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool Exportable
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether a key is a hardware key.</summary>
		/// <returns>
		///   <see langword="true" /> if the key is a hardware key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.</exception>
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool HardwareDevice
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a key container name.</summary>
		/// <returns>The key container name.</returns>
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x000B3205 File Offset: 0x000B1405
		public string KeyContainerName
		{
			get
			{
				return this._params.KeyContainerName;
			}
		}

		/// <summary>Gets a value that describes whether an asymmetric key was created as a signature key or an exchange key.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.KeyNumber" /> values that describes whether an asymmetric key was created as a signature key or an exchange key.</returns>
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x000B3212 File Offset: 0x000B1412
		public KeyNumber KeyNumber
		{
			get
			{
				return (KeyNumber)this._params.KeyNumber;
			}
		}

		/// <summary>Gets a value indicating whether a key is from a machine key set.</summary>
		/// <returns>
		///   <see langword="true" /> if the key is from the machine key set; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool MachineKeyStore
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a key pair is protected.</summary>
		/// <returns>
		///   <see langword="true" /> if the key pair is protected; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.  
		///  -or-  
		///  The key container was not found.</exception>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool Protected
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the provider name of a key.</summary>
		/// <returns>The provider name.</returns>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x000B321F File Offset: 0x000B141F
		public string ProviderName
		{
			get
			{
				return this._params.ProviderName;
			}
		}

		/// <summary>Gets the provider type of a key.</summary>
		/// <returns>The provider type. The default is 1.</returns>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x000B322C File Offset: 0x000B142C
		public int ProviderType
		{
			get
			{
				return this._params.ProviderType;
			}
		}

		/// <summary>Gets a value indicating whether a key container was randomly generated by a managed cryptography class.</summary>
		/// <returns>
		///   <see langword="true" /> if the key container was randomly generated; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000B3239 File Offset: 0x000B1439
		public bool RandomlyGenerated
		{
			get
			{
				return this._random;
			}
		}

		/// <summary>Gets a value indicating whether a key can be removed from a key container.</summary>
		/// <returns>
		///   <see langword="true" /> if the key is removable; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) was not found.</exception>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool Removable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a unique key container name.</summary>
		/// <returns>The unique key container name.</returns>
		/// <exception cref="T:System.NotSupportedException">The key type is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider cannot be found.  
		///  -or-  
		///  The key container was not found.</exception>
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000B3241 File Offset: 0x000B1441
		public string UniqueKeyContainerName
		{
			get
			{
				return this._params.ProviderName + "\\" + this._params.KeyContainerName;
			}
		}

		// Token: 0x04002246 RID: 8774
		private CspParameters _params;

		// Token: 0x04002247 RID: 8775
		internal bool _random;
	}
}
