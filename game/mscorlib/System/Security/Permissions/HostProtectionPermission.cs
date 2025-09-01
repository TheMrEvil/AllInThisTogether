using System;

namespace System.Security.Permissions
{
	// Token: 0x0200043E RID: 1086
	[Serializable]
	internal sealed class HostProtectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002C24 RID: 11300 RVA: 0x0009EECC File Offset: 0x0009D0CC
		public HostProtectionPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._resources = HostProtectionResource.All;
				return;
			}
			this._resources = HostProtectionResource.None;
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x0009EEF1 File Offset: 0x0009D0F1
		public HostProtectionPermission(HostProtectionResource resources)
		{
			this.Resources = this._resources;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x0009EF05 File Offset: 0x0009D105
		// (set) Token: 0x06002C27 RID: 11303 RVA: 0x0009EF0D File Offset: 0x0009D10D
		public HostProtectionResource Resources
		{
			get
			{
				return this._resources;
			}
			set
			{
				if (!Enum.IsDefined(typeof(HostProtectionResource), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "HostProtectionResource");
				}
				this._resources = value;
			}
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x0009EF4D File Offset: 0x0009D14D
		public override IPermission Copy()
		{
			return new HostProtectionPermission(this._resources);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x0009EF5C File Offset: 0x0009D15C
		public override IPermission Intersect(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && hostProtectionPermission.IsUnrestricted())
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return hostProtectionPermission.Copy();
			}
			if (hostProtectionPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			return new HostProtectionPermission(this._resources & hostProtectionPermission._resources);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x0009EFC0 File Offset: 0x0009D1C0
		public override IPermission Union(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || hostProtectionPermission.IsUnrestricted())
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			return new HostProtectionPermission(this._resources | hostProtectionPermission._resources);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0009F008 File Offset: 0x0009D208
		public override bool IsSubsetOf(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return this._resources == HostProtectionResource.None;
			}
			return hostProtectionPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this._resources & ~hostProtectionPermission._resources) == HostProtectionResource.None);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x0009F04F File Offset: 0x0009D24F
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._resources = (HostProtectionResource)Enum.Parse(typeof(HostProtectionResource), e.Attribute("Resources"));
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0009F084 File Offset: 0x0009D284
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			securityElement.AddAttribute("Resources", this._resources.ToString());
			return securityElement;
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x0009F0A9 File Offset: 0x0009D2A9
		public bool IsUnrestricted()
		{
			return this._resources == HostProtectionResource.All;
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000324D2 File Offset: 0x000306D2
		int IBuiltInPermission.GetTokenIndex()
		{
			return 9;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0009F0B8 File Offset: 0x0009D2B8
		private HostProtectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			HostProtectionPermission hostProtectionPermission = target as HostProtectionPermission;
			if (hostProtectionPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(HostProtectionPermission));
			}
			return hostProtectionPermission;
		}

		// Token: 0x0400202E RID: 8238
		private const int version = 1;

		// Token: 0x0400202F RID: 8239
		private HostProtectionResource _resources;
	}
}
