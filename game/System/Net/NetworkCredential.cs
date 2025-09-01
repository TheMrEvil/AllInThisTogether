using System;
using System.Security;

namespace System.Net
{
	/// <summary>Provides credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.</summary>
	// Token: 0x020005FF RID: 1535
	public class NetworkCredential : ICredentials, ICredentialsByHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class.</summary>
		// Token: 0x0600307E RID: 12414 RVA: 0x000A5779 File Offset: 0x000A3979
		public NetworkCredential() : this(string.Empty, string.Empty, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		// Token: 0x0600307F RID: 12415 RVA: 0x000A7051 File Offset: 0x000A5251
		public NetworkCredential(string userName, string password) : this(userName, password, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x06003080 RID: 12416 RVA: 0x000A7060 File Offset: 0x000A5260
		public NetworkCredential(string userName, SecureString password) : this(userName, password, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <param name="domain">The domain associated with these credentials.</param>
		// Token: 0x06003081 RID: 12417 RVA: 0x000A706F File Offset: 0x000A526F
		public NetworkCredential(string userName, string password, string domain)
		{
			this.UserName = userName;
			this.Password = password;
			this.Domain = domain;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials.</param>
		/// <param name="password">The password for the user name associated with the credentials.</param>
		/// <param name="domain">The domain associated with these credentials.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x06003082 RID: 12418 RVA: 0x000A708C File Offset: 0x000A528C
		public NetworkCredential(string userName, SecureString password, string domain)
		{
			this.UserName = userName;
			this.SecurePassword = password;
			this.Domain = domain;
		}

		/// <summary>Gets or sets the user name associated with the credentials.</summary>
		/// <returns>The user name associated with the credentials.</returns>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x000A70A9 File Offset: 0x000A52A9
		// (set) Token: 0x06003084 RID: 12420 RVA: 0x000A70B1 File Offset: 0x000A52B1
		public string UserName
		{
			get
			{
				return this.InternalGetUserName();
			}
			set
			{
				if (value == null)
				{
					this.m_userName = string.Empty;
					return;
				}
				this.m_userName = value;
			}
		}

		/// <summary>Gets or sets the password for the user name associated with the credentials.</summary>
		/// <returns>The password associated with the credentials. If this <see cref="T:System.Net.NetworkCredential" /> instance was initialized with the <paramref name="password" /> parameter set to <see langword="null" />, then the <see cref="P:System.Net.NetworkCredential.Password" /> property will return an empty string.</returns>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x000A70C9 File Offset: 0x000A52C9
		// (set) Token: 0x06003086 RID: 12422 RVA: 0x000A70D1 File Offset: 0x000A52D1
		public string Password
		{
			get
			{
				return this.InternalGetPassword();
			}
			set
			{
				this.m_password = UnsafeNclNativeMethods.SecureStringHelper.CreateSecureString(value);
			}
		}

		/// <summary>Gets or sets the password as a <see cref="T:System.Security.SecureString" /> instance.</summary>
		/// <returns>The password for the user name associated with the credentials.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Security.SecureString" /> class is not supported on this platform.</exception>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x000A70DF File Offset: 0x000A52DF
		// (set) Token: 0x06003088 RID: 12424 RVA: 0x000A70EC File Offset: 0x000A52EC
		public SecureString SecurePassword
		{
			get
			{
				return this.InternalGetSecurePassword().Copy();
			}
			set
			{
				if (value == null)
				{
					this.m_password = new SecureString();
					return;
				}
				this.m_password = value.Copy();
			}
		}

		/// <summary>Gets or sets the domain or computer name that verifies the credentials.</summary>
		/// <returns>The name of the domain associated with the credentials.</returns>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000A7109 File Offset: 0x000A5309
		// (set) Token: 0x0600308A RID: 12426 RVA: 0x000A7111 File Offset: 0x000A5311
		public string Domain
		{
			get
			{
				return this.InternalGetDomain();
			}
			set
			{
				if (value == null)
				{
					this.m_domain = string.Empty;
					return;
				}
				this.m_domain = value;
			}
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000A7129 File Offset: 0x000A5329
		internal string InternalGetUserName()
		{
			return this.m_userName;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000A7131 File Offset: 0x000A5331
		internal string InternalGetPassword()
		{
			return UnsafeNclNativeMethods.SecureStringHelper.CreateString(this.m_password);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000A713E File Offset: 0x000A533E
		internal SecureString InternalGetSecurePassword()
		{
			return this.m_password;
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000A7146 File Offset: 0x000A5346
		internal string InternalGetDomain()
		{
			return this.m_domain;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000A7150 File Offset: 0x000A5350
		internal string InternalGetDomainUserName()
		{
			string text = this.InternalGetDomain();
			if (text.Length != 0)
			{
				text += "\\";
			}
			return text + this.InternalGetUserName();
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <param name="uri">The URI that the client provides authentication for.</param>
		/// <param name="authType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property.</param>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> object.</returns>
		// Token: 0x06003090 RID: 12432 RVA: 0x000075E1 File Offset: 0x000057E1
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			return this;
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified host, port, and authentication type.</summary>
		/// <param name="host">The host computer that authenticates the client.</param>
		/// <param name="port">The port on the <paramref name="host" /> that the client communicates with.</param>
		/// <param name="authenticationType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property.</param>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> for the specified host, port, and authentication protocol, or <see langword="null" /> if there are no credentials available for the specified host, port, and authentication protocol.</returns>
		// Token: 0x06003091 RID: 12433 RVA: 0x000075E1 File Offset: 0x000057E1
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return this;
		}

		// Token: 0x04001C2E RID: 7214
		private string m_domain;

		// Token: 0x04001C2F RID: 7215
		private string m_userName;

		// Token: 0x04001C30 RID: 7216
		private SecureString m_password;
	}
}
