using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Provides the strong name of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000420 RID: 1056
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongName : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.StrongName" /> class with the strong name public key blob, name, and version.</summary>
		/// <param name="blob">The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the software publisher.</param>
		/// <param name="name">The simple name section of the strong name.</param>
		/// <param name="version">The <see cref="T:System.Version" /> of the strong name.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="blob" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="name" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="version" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is an empty string ("").</exception>
		// Token: 0x06002B33 RID: 11059 RVA: 0x0009C638 File Offset: 0x0009A838
		public StrongName(StrongNamePublicKeyBlob blob, string name, Version version)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Locale.GetText("Empty"), "name");
			}
			this.publickey = blob;
			this.name = name;
			this.version = version;
		}

		/// <summary>Gets the simple name of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The simple name part of the <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x0009C6AD File Offset: 0x0009A8AD
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x0009C6B5 File Offset: 0x0009A8B5
		public StrongNamePublicKeyBlob PublicKey
		{
			get
			{
				return this.publickey;
			}
		}

		/// <summary>Gets the <see cref="T:System.Version" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The <see cref="T:System.Version" /> of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x0009C6BD File Offset: 0x0009A8BD
		public Version Version
		{
			get
			{
				return this.version;
			}
		}

		/// <summary>Creates an equivalent copy of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A new, identical copy of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06002B37 RID: 11063 RVA: 0x0009C6C5 File Offset: 0x0009A8C5
		public object Copy()
		{
			return new StrongName(this.publickey, this.name, this.version);
		}

		/// <summary>Creates a <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> that corresponds to the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />.</param>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06002B38 RID: 11064 RVA: 0x0009C6DE File Offset: 0x0009A8DE
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new StrongNameIdentityPermission(this.publickey, this.name, this.version);
		}

		/// <summary>Determines whether the specified strong name is equal to the current strong name.</summary>
		/// <param name="o">The strong name to compare against the current strong name.</param>
		/// <returns>
		///   <see langword="true" /> if the specified strong name is equal to the current strong name; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B39 RID: 11065 RVA: 0x0009C6F8 File Offset: 0x0009A8F8
		public override bool Equals(object o)
		{
			StrongName strongName = o as StrongName;
			return strongName != null && !(this.name != strongName.Name) && this.Version.Equals(strongName.Version) && this.PublicKey.Equals(strongName.PublicKey);
		}

		/// <summary>Gets the hash code of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>The hash code of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06002B3A RID: 11066 RVA: 0x0009C74C File Offset: 0x0009A94C
		public override int GetHashCode()
		{
			return this.publickey.GetHashCode();
		}

		/// <summary>Creates a string representation of the current <see cref="T:System.Security.Policy.StrongName" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.StrongName" />.</returns>
		// Token: 0x06002B3B RID: 11067 RVA: 0x0009C75C File Offset: 0x0009A95C
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(typeof(StrongName).Name);
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("Key", this.publickey.ToString());
			securityElement.AddAttribute("Name", this.name);
			securityElement.AddAttribute("Version", this.version.ToString());
			return securityElement.ToString();
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x0009C7CF File Offset: 0x0009A9CF
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 5 : 1) + this.name.Length;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x04001FB5 RID: 8117
		private StrongNamePublicKeyBlob publickey;

		// Token: 0x04001FB6 RID: 8118
		private string name;

		// Token: 0x04001FB7 RID: 8119
		private Version version;
	}
}
