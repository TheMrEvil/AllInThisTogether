using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000450 RID: 1104
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002CC6 RID: 11462 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public PublisherIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets a certification file containing an Authenticode X.509v3 certificate.</summary>
		/// <returns>The file path of an X.509 certificate file (usually has the extension.cer).</returns>
		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000A0B7D File Offset: 0x0009ED7D
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x000A0B85 File Offset: 0x0009ED85
		public string CertFile
		{
			get
			{
				return this.certFile;
			}
			set
			{
				this.certFile = value;
			}
		}

		/// <summary>Gets or sets a signed file from which to extract an Authenticode X.509v3 certificate.</summary>
		/// <returns>The file path of a file signed with the Authenticode signature.</returns>
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000A0B8E File Offset: 0x0009ED8E
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x000A0B96 File Offset: 0x0009ED96
		public string SignedFile
		{
			get
			{
				return this.signedFile;
			}
			set
			{
				this.signedFile = value;
			}
		}

		/// <summary>Gets or sets an Authenticode X.509v3 certificate that identifies the publisher of the calling code.</summary>
		/// <returns>A hexadecimal representation of the X.509 certificate.</returns>
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000A0B9F File Offset: 0x0009ED9F
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x000A0BA7 File Offset: 0x0009EDA7
		public string X509Certificate
		{
			get
			{
				return this.x509data;
			}
			set
			{
				this.x509data = value;
			}
		}

		/// <summary>Creates and returns a new instance of <see cref="T:System.Security.Permissions.PublisherIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002CCD RID: 11469 RVA: 0x000A0BB0 File Offset: 0x0009EDB0
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new PublisherIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.x509data != null)
			{
				return new PublisherIdentityPermission(new X509Certificate(CryptoConvert.FromHex(this.x509data)));
			}
			if (this.certFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.certFile));
			}
			if (this.signedFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.signedFile));
			}
			return new PublisherIdentityPermission(PermissionState.None);
		}

		// Token: 0x04002076 RID: 8310
		private string certFile;

		// Token: 0x04002077 RID: 8311
		private string signedFile;

		// Token: 0x04002078 RID: 8312
		private string x509data;
	}
}
