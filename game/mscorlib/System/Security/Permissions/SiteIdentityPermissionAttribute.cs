using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200045B RID: 1115
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.SiteIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002D51 RID: 11601 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public SiteIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the site name of the calling code.</summary>
		/// <returns>The site name to compare against the site name specified by the security provider.</returns>
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000A247C File Offset: 0x000A067C
		// (set) Token: 0x06002D53 RID: 11603 RVA: 0x000A2484 File Offset: 0x000A0684
		public string Site
		{
			get
			{
				return this.site;
			}
			set
			{
				this.site = value;
			}
		}

		/// <summary>Creates and returns a new instance of <see cref="T:System.Security.Permissions.SiteIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002D54 RID: 11604 RVA: 0x000A2490 File Offset: 0x000A0690
		public override IPermission CreatePermission()
		{
			SiteIdentityPermission result;
			if (base.Unrestricted)
			{
				result = new SiteIdentityPermission(PermissionState.Unrestricted);
			}
			else if (this.site == null)
			{
				result = new SiteIdentityPermission(PermissionState.None);
			}
			else
			{
				result = new SiteIdentityPermission(this.site);
			}
			return result;
		}

		// Token: 0x040020AC RID: 8364
		private string site;
	}
}
