using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides simple data protection methods.</summary>
	// Token: 0x0200011C RID: 284
	public sealed class DpapiDataProtector : DataProtector
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Security.Cryptography.DpapiDataProtector" /> class by using the specified application name, primary purpose, and specific purposes.</summary>
		/// <param name="appName">The name of the application.</param>
		/// <param name="primaryPurpose">The primary purpose for the data protector.</param>
		/// <param name="specificPurpose">The specific purpose(s) for the data protector.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="appName" /> is an empty string or <see langword="null" />.  
		/// -or-  
		/// <paramref name="primaryPurpose" /> is an empty string or <see langword="null" />.  
		/// -or-  
		/// <paramref name="specificPurposes" /> contains an empty string or <see langword="null" />.</exception>
		// Token: 0x06000730 RID: 1840 RVA: 0x000029A4 File Offset: 0x00000BA4
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Demand, Unrestricted = true)]
		public DpapiDataProtector(string appName, string primaryPurpose, string[] specificPurpose)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the scope of the data protection.</summary>
		/// <returns>One of the enumeration values that specifies the scope of the data protection (either the current user or the local machine). The default is <see cref="F:System.Security.Cryptography.DataProtectionScope.CurrentUser" />.</returns>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x000029A4 File Offset: 0x00000BA4
		public DataProtectionScope Scope
		{
			[CompilerGenerated]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return DataProtectionScope.CurrentUser;
			}
			[CompilerGenerated]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Determines if the data must be re-encrypted.</summary>
		/// <param name="encryptedData">The encrypted data to be checked.</param>
		/// <returns>
		///   <see langword="true" /> if the data must be re-encrypted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000733 RID: 1843 RVA: 0x0001DFC0 File Offset: 0x0001C1C0
		public override bool IsReprotectRequired(byte[] encryptedData)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001DFDB File Offset: 0x0001C1DB
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Assert, ProtectData = true)]
		protected override byte[] ProviderProtect(byte[] userData)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001DFDB File Offset: 0x0001C1DB
		[SecuritySafeCritical]
		[DataProtectionPermission(SecurityAction.Assert, UnprotectData = true)]
		protected override byte[] ProviderUnprotect(byte[] encryptedData)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
