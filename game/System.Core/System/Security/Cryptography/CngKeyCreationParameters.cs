using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Contains advanced properties for key creation.</summary>
	// Token: 0x02000043 RID: 67
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class CngKeyCreationParameters
	{
		/// <summary>Gets or sets the key export policy.</summary>
		/// <returns>An object that specifies a key export policy. The default value is <see langword="null" />, which indicates that the key storage provider's default export policy is set.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000350F File Offset: 0x0000170F
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00003517 File Offset: 0x00001717
		public CngExportPolicies? ExportPolicy
		{
			get
			{
				return this.m_exportPolicy;
			}
			set
			{
				this.m_exportPolicy = value;
			}
		}

		/// <summary>Gets or sets the key creation options.</summary>
		/// <returns>An object that specifies options for creating keys. The default value is <see langword="null" />, which indicates that the key storage provider's default key creation options are set.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00003520 File Offset: 0x00001720
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00003528 File Offset: 0x00001728
		public CngKeyCreationOptions KeyCreationOptions
		{
			get
			{
				return this.m_keyCreationOptions;
			}
			set
			{
				this.m_keyCreationOptions = value;
			}
		}

		/// <summary>Gets or sets the cryptographic operations that apply to the current key. </summary>
		/// <returns>A bitwise combination of one or more enumeration values that specify key usage. The default value is <see langword="null" />, which indicates that the key storage provider's default key usage is set.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00003531 File Offset: 0x00001731
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00003539 File Offset: 0x00001739
		public CngKeyUsages? KeyUsage
		{
			get
			{
				return this.m_keyUsage;
			}
			set
			{
				this.m_keyUsage = value;
			}
		}

		/// <summary>Gets or sets the window handle that should be used as the parent window for dialog boxes that are created by Cryptography Next Generation (CNG) classes.</summary>
		/// <returns>The HWND of the parent window that is used for CNG dialog boxes.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003542 File Offset: 0x00001742
		// (set) Token: 0x06000138 RID: 312 RVA: 0x0000354A File Offset: 0x0000174A
		public IntPtr ParentWindowHandle
		{
			get
			{
				return this.m_parentWindowHandle;
			}
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			set
			{
				this.m_parentWindowHandle = value;
			}
		}

		/// <summary>Enables a <see cref="T:System.Security.Cryptography.CngKey" /> object to be created with additional properties that are set before the key is finalized.</summary>
		/// <returns>A collection object that contains any additional parameters that you must set on a <see cref="T:System.Security.Cryptography.CngKey" /> object during key creation.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003553 File Offset: 0x00001753
		public CngPropertyCollection Parameters
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			get
			{
				return this.m_parameters;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00003553 File Offset: 0x00001753
		internal CngPropertyCollection ParametersNoDemand
		{
			get
			{
				return this.m_parameters;
			}
		}

		/// <summary>Gets or sets the key storage provider (KSP) to create a key in.</summary>
		/// <returns>An object that specifies the KSP that a new key will be created in.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.CngKeyCreationParameters.Provider" /> property is set to a <see langword="null" /> value.</exception>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000355B File Offset: 0x0000175B
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00003563 File Offset: 0x00001763
		public CngProvider Provider
		{
			get
			{
				return this.m_provider;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_provider = value;
			}
		}

		/// <summary>Gets or sets information about the user interface to display when a key is created or accessed.</summary>
		/// <returns>An object that contains details about the user interface shown by Cryptography Next Generation (CNG) classes when a key is created or accessed. A <see langword="null" /> value indicates that the key storage provider's default user interface policy is set.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003580 File Offset: 0x00001780
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00003588 File Offset: 0x00001788
		public CngUIPolicy UIPolicy
		{
			get
			{
				return this.m_uiPolicy;
			}
			[SecuritySafeCritical]
			[HostProtection(SecurityAction.LinkDemand, UI = true)]
			[UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.SafeSubWindows)]
			set
			{
				this.m_uiPolicy = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngKeyCreationParameters" /> class.</summary>
		// Token: 0x0600013F RID: 319 RVA: 0x00003591 File Offset: 0x00001791
		public CngKeyCreationParameters()
		{
		}

		// Token: 0x04000316 RID: 790
		private CngExportPolicies? m_exportPolicy;

		// Token: 0x04000317 RID: 791
		private CngKeyCreationOptions m_keyCreationOptions;

		// Token: 0x04000318 RID: 792
		private CngKeyUsages? m_keyUsage;

		// Token: 0x04000319 RID: 793
		private CngPropertyCollection m_parameters = new CngPropertyCollection();

		// Token: 0x0400031A RID: 794
		private IntPtr m_parentWindowHandle;

		// Token: 0x0400031B RID: 795
		private CngProvider m_provider = CngProvider.MicrosoftSoftwareKeyStorageProvider;

		// Token: 0x0400031C RID: 796
		private CngUIPolicy m_uiPolicy;
	}
}
