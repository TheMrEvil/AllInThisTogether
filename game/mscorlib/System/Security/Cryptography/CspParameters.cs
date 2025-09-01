using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Cryptography
{
	/// <summary>Contains parameters that are passed to the cryptographic service provider (CSP) that performs cryptographic computations. This class cannot be inherited.</summary>
	// Token: 0x02000486 RID: 1158
	[ComVisible(true)]
	public sealed class CspParameters
	{
		/// <summary>Represents the flags for <see cref="T:System.Security.Cryptography.CspParameters" /> that modify the behavior of the cryptographic service provider (CSP).</summary>
		/// <returns>An enumeration value, or a bitwise combination of enumeration values.</returns>
		/// <exception cref="T:System.ArgumentException">Value is not a valid enumeration value.</exception>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000A696A File Offset: 0x000A4B6A
		// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x000A6974 File Offset: 0x000A4B74
		public CspProviderFlags Flags
		{
			get
			{
				return (CspProviderFlags)this.m_flags;
			}
			set
			{
				int num = 255;
				if ((value & (CspProviderFlags)(~(CspProviderFlags)num)) != CspProviderFlags.NoFlags)
				{
					throw new ArgumentException(Environment.GetResourceString("Illegal enum value: {0}.", new object[]
					{
						(int)value
					}), "value");
				}
				this.m_flags = (int)value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</summary>
		/// <returns>A <see cref="T:System.Security.AccessControl.CryptoKeySecurity" /> object that represents access rights and audit rules for a container.</returns>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000A69BA File Offset: 0x000A4BBA
		// (set) Token: 0x06002EA4 RID: 11940 RVA: 0x000A69C2 File Offset: 0x000A4BC2
		public CryptoKeySecurity CryptoKeySecurity
		{
			get
			{
				return this.m_cryptoKeySecurity;
			}
			set
			{
				this.m_cryptoKeySecurity = value;
			}
		}

		/// <summary>Gets or sets a password associated with a smart card key.</summary>
		/// <returns>A password associated with a smart card key.</returns>
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000A69CB File Offset: 0x000A4BCB
		// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x000A69D3 File Offset: 0x000A4BD3
		public SecureString KeyPassword
		{
			get
			{
				return this.m_keyPassword;
			}
			set
			{
				this.m_keyPassword = value;
				this.m_parentWindowHandle = IntPtr.Zero;
			}
		}

		/// <summary>Gets or sets a handle to the unmanaged parent window for a smart card password dialog box.</summary>
		/// <returns>A handle to the parent window for a smart card password dialog box.</returns>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000A69E7 File Offset: 0x000A4BE7
		// (set) Token: 0x06002EA8 RID: 11944 RVA: 0x000A69EF File Offset: 0x000A4BEF
		public IntPtr ParentWindowHandle
		{
			get
			{
				return this.m_parentWindowHandle;
			}
			set
			{
				this.m_parentWindowHandle = value;
				this.m_keyPassword = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class.</summary>
		// Token: 0x06002EA9 RID: 11945 RVA: 0x000A69FF File Offset: 0x000A4BFF
		public CspParameters() : this(1, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code.</summary>
		/// <param name="dwTypeIn">A provider type code that specifies the kind of provider to create.</param>
		// Token: 0x06002EAA RID: 11946 RVA: 0x000A6A0A File Offset: 0x000A4C0A
		public CspParameters(int dwTypeIn) : this(dwTypeIn, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code and name.</summary>
		/// <param name="dwTypeIn">A provider type code that specifies the kind of provider to create.</param>
		/// <param name="strProviderNameIn">A provider name.</param>
		// Token: 0x06002EAB RID: 11947 RVA: 0x000A6A15 File Offset: 0x000A4C15
		public CspParameters(int dwTypeIn, string strProviderNameIn) : this(dwTypeIn, strProviderNameIn, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class with the specified provider type code and name, and the specified container name.</summary>
		/// <param name="dwTypeIn">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="strProviderNameIn">A provider name.</param>
		/// <param name="strContainerNameIn">A container name.</param>
		// Token: 0x06002EAC RID: 11948 RVA: 0x000A6A20 File Offset: 0x000A4C20
		public CspParameters(int dwTypeIn, string strProviderNameIn, string strContainerNameIn) : this(dwTypeIn, strProviderNameIn, strContainerNameIn, CspProviderFlags.NoFlags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class using a provider type, a provider name, a container name, access information, and a password associated with a smart card key.</summary>
		/// <param name="providerType">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="providerName">A provider name.</param>
		/// <param name="keyContainerName">A container name.</param>
		/// <param name="cryptoKeySecurity">An object that represents access rights and audit rules for a container.</param>
		/// <param name="keyPassword">A password associated with a smart card key.</param>
		// Token: 0x06002EAD RID: 11949 RVA: 0x000A6A2C File Offset: 0x000A4C2C
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, SecureString keyPassword) : this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_keyPassword = keyPassword;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CspParameters" /> class using a provider type, a provider name, a container name, access information, and a handle to an unmanaged smart card password dialog.</summary>
		/// <param name="providerType">The provider type code that specifies the kind of provider to create.</param>
		/// <param name="providerName">A provider name.</param>
		/// <param name="keyContainerName">A container name.</param>
		/// <param name="cryptoKeySecurity">An object that represents access rights and audit rules for the container.</param>
		/// <param name="parentWindowHandle">A handle to the parent window for a smart card password dialog.</param>
		// Token: 0x06002EAE RID: 11950 RVA: 0x000A6A47 File Offset: 0x000A4C47
		public CspParameters(int providerType, string providerName, string keyContainerName, CryptoKeySecurity cryptoKeySecurity, IntPtr parentWindowHandle) : this(providerType, providerName, keyContainerName)
		{
			this.m_cryptoKeySecurity = cryptoKeySecurity;
			this.m_parentWindowHandle = parentWindowHandle;
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000A6A62 File Offset: 0x000A4C62
		internal CspParameters(int providerType, string providerName, string keyContainerName, CspProviderFlags flags)
		{
			this.ProviderType = providerType;
			this.ProviderName = providerName;
			this.KeyContainerName = keyContainerName;
			this.KeyNumber = -1;
			this.Flags = flags;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000A6A90 File Offset: 0x000A4C90
		internal CspParameters(CspParameters parameters)
		{
			this.ProviderType = parameters.ProviderType;
			this.ProviderName = parameters.ProviderName;
			this.KeyContainerName = parameters.KeyContainerName;
			this.KeyNumber = parameters.KeyNumber;
			this.Flags = parameters.Flags;
			this.m_cryptoKeySecurity = parameters.m_cryptoKeySecurity;
			this.m_keyPassword = parameters.m_keyPassword;
			this.m_parentWindowHandle = parameters.m_parentWindowHandle;
		}

		/// <summary>Represents the provider type code for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x0400214F RID: 8527
		public int ProviderType;

		/// <summary>Represents the provider name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04002150 RID: 8528
		public string ProviderName;

		/// <summary>Represents the key container name for <see cref="T:System.Security.Cryptography.CspParameters" />.</summary>
		// Token: 0x04002151 RID: 8529
		public string KeyContainerName;

		/// <summary>Specifies whether an asymmetric key is created as a signature key or an exchange key.</summary>
		// Token: 0x04002152 RID: 8530
		public int KeyNumber;

		// Token: 0x04002153 RID: 8531
		private int m_flags;

		// Token: 0x04002154 RID: 8532
		private CryptoKeySecurity m_cryptoKeySecurity;

		// Token: 0x04002155 RID: 8533
		private SecureString m_keyPassword;

		// Token: 0x04002156 RID: 8534
		private IntPtr m_parentWindowHandle;
	}
}
