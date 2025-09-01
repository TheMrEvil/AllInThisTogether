using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200045E RID: 1118
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class StrongNameIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.StrongNameIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002D73 RID: 11635 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public StrongNameIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the name of the strong name identity.</summary>
		/// <returns>A name to compare against the name specified by the security provider.</returns>
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x000A2FAC File Offset: 0x000A11AC
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x000A2FB4 File Offset: 0x000A11B4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the public key value of the strong name identity expressed as a hexadecimal string.</summary>
		/// <returns>The public key value of the strong name identity expressed as a hexadecimal string.</returns>
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000A2FBD File Offset: 0x000A11BD
		// (set) Token: 0x06002D77 RID: 11639 RVA: 0x000A2FC5 File Offset: 0x000A11C5
		public string PublicKey
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		/// <summary>Gets or sets the version of the strong name identity.</summary>
		/// <returns>The version number of the strong name identity.</returns>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000A2FCE File Offset: 0x000A11CE
		// (set) Token: 0x06002D79 RID: 11641 RVA: 0x000A2FD6 File Offset: 0x000A11D6
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> that corresponds to this attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The method failed because the key is <see langword="null" />.</exception>
		// Token: 0x06002D7A RID: 11642 RVA: 0x000A2FE0 File Offset: 0x000A11E0
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new StrongNameIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.name == null && this.key == null && this.version == null)
			{
				return new StrongNameIdentityPermission(PermissionState.None);
			}
			if (this.key == null)
			{
				throw new ArgumentException(Locale.GetText("PublicKey is required"));
			}
			StrongNamePublicKeyBlob blob = StrongNamePublicKeyBlob.FromString(this.key);
			Version version = null;
			if (this.version != null)
			{
				version = new Version(this.version);
			}
			return new StrongNameIdentityPermission(blob, this.name, version);
		}

		// Token: 0x040020B4 RID: 8372
		private string name;

		// Token: 0x040020B5 RID: 8373
		private string key;

		// Token: 0x040020B6 RID: 8374
		private string version;
	}
}
