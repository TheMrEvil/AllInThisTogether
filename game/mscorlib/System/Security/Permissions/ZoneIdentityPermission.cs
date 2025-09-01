using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for the zone from which the code originates. This class cannot be inherited.</summary>
	// Token: 0x02000464 RID: 1124
	[ComVisible(true)]
	[Serializable]
	public sealed class ZoneIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002DAB RID: 11691 RVA: 0x000A3910 File Offset: 0x000A1B10
		public ZoneIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
			this.zone = SecurityZone.NoZone;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> class to represent the specified zone identity.</summary>
		/// <param name="zone">The zone identifier.</param>
		// Token: 0x06002DAC RID: 11692 RVA: 0x000A3927 File Offset: 0x000A1B27
		public ZoneIdentityPermission(SecurityZone zone)
		{
			this.SecurityZone = zone;
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002DAD RID: 11693 RVA: 0x000A3936 File Offset: 0x000A1B36
		public override IPermission Copy()
		{
			return new ZoneIdentityPermission(this.zone);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" />, this permission does not represent the <see cref="F:System.Security.SecurityZone.NoZone" /> security zone, and the specified permission is not equal to the current permission.</exception>
		// Token: 0x06002DAE RID: 11694 RVA: 0x000A3944 File Offset: 0x000A1B44
		public override bool IsSubsetOf(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null)
			{
				return this.zone == SecurityZone.NoZone;
			}
			return this.zone == SecurityZone.NoZone || this.zone == zoneIdentityPermission.zone;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.  
		///  -or-  
		///  The two permissions are not equal and the current permission does not represent the <see cref="F:System.Security.SecurityZone.NoZone" /> security zone.</exception>
		// Token: 0x06002DAF RID: 11695 RVA: 0x000A3980 File Offset: 0x000A1B80
		public override IPermission Union(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null)
			{
				if (this.zone != SecurityZone.NoZone)
				{
					return this.Copy();
				}
				return null;
			}
			else
			{
				if (this.zone == zoneIdentityPermission.zone || zoneIdentityPermission.zone == SecurityZone.NoZone)
				{
					return this.Copy();
				}
				if (this.zone == SecurityZone.NoZone)
				{
					return zoneIdentityPermission.Copy();
				}
				throw new ArgumentException(Locale.GetText("Union impossible"));
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002DB0 RID: 11696 RVA: 0x000A39E8 File Offset: 0x000A1BE8
		public override IPermission Intersect(IPermission target)
		{
			ZoneIdentityPermission zoneIdentityPermission = this.Cast(target);
			if (zoneIdentityPermission == null || this.zone == SecurityZone.NoZone)
			{
				return null;
			}
			if (this.zone == zoneIdentityPermission.zone)
			{
				return this.Copy();
			}
			return null;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not valid.</exception>
		// Token: 0x06002DB1 RID: 11697 RVA: 0x000A3A24 File Offset: 0x000A1C24
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attribute("Zone");
			if (text == null)
			{
				this.zone = SecurityZone.NoZone;
				return;
			}
			this.zone = (SecurityZone)Enum.Parse(typeof(SecurityZone), text);
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002DB2 RID: 11698 RVA: 0x000A3A74 File Offset: 0x000A1C74
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.zone != SecurityZone.NoZone)
			{
				securityElement.AddAttribute("Zone", this.zone.ToString());
			}
			return securityElement;
		}

		/// <summary>Gets or sets the zone represented by the current <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />.</summary>
		/// <returns>One of the <see cref="T:System.Security.SecurityZone" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The parameter value is not a valid value of <see cref="T:System.Security.SecurityZone" />.</exception>
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000A3AAF File Offset: 0x000A1CAF
		// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x000A3AB7 File Offset: 0x000A1CB7
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
			set
			{
				if (!Enum.IsDefined(typeof(SecurityZone), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "SecurityZone");
				}
				this.zone = value;
			}
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000286A2 File Offset: 0x000268A2
		int IBuiltInPermission.GetTokenIndex()
		{
			return 14;
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000A3AF7 File Offset: 0x000A1CF7
		private ZoneIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ZoneIdentityPermission zoneIdentityPermission = target as ZoneIdentityPermission;
			if (zoneIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(ZoneIdentityPermission));
			}
			return zoneIdentityPermission;
		}

		// Token: 0x040020C0 RID: 8384
		private const int version = 1;

		// Token: 0x040020C1 RID: 8385
		private SecurityZone zone;
	}
}
