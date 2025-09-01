using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000465 RID: 1125
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class ZoneIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ZoneIdentityPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002DB7 RID: 11703 RVA: 0x000A3B17 File Offset: 0x000A1D17
		public ZoneIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
			this.zone = SecurityZone.NoZone;
		}

		/// <summary>Gets or sets membership in the content zone specified by the property value.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityZone" /> values.</returns>
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000A3B27 File Offset: 0x000A1D27
		// (set) Token: 0x06002DB9 RID: 11705 RVA: 0x000A3B2F File Offset: 0x000A1D2F
		public SecurityZone Zone
		{
			get
			{
				return this.zone;
			}
			set
			{
				this.zone = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002DBA RID: 11706 RVA: 0x000A3B38 File Offset: 0x000A1D38
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new ZoneIdentityPermission(PermissionState.Unrestricted);
			}
			return new ZoneIdentityPermission(this.zone);
		}

		// Token: 0x040020C2 RID: 8386
		private SecurityZone zone;
	}
}
