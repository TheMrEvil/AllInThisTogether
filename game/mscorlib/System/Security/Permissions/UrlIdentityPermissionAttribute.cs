using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000463 RID: 1123
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class UrlIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UrlIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002DA7 RID: 11687 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public UrlIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the full URL of the calling code.</summary>
		/// <returns>The URL to match with the URL specified by the host.</returns>
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000A38D4 File Offset: 0x000A1AD4
		// (set) Token: 0x06002DA9 RID: 11689 RVA: 0x000A38DC File Offset: 0x000A1ADC
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.UrlIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002DAA RID: 11690 RVA: 0x000A38E5 File Offset: 0x000A1AE5
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.url == null)
			{
				return new UrlIdentityPermission(PermissionState.None);
			}
			return new UrlIdentityPermission(this.url);
		}

		// Token: 0x040020BF RID: 8383
		private string url;
	}
}
