using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Controls access to Simple Mail Transport Protocol (SMTP) servers.</summary>
	// Token: 0x0200083C RID: 2108
	[Serializable]
	public sealed class SmtpPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class with the specified state.</summary>
		/// <param name="unrestricted">
		///   <see langword="true" /> if the new permission is unrestricted; otherwise, <see langword="false" />.</param>
		// Token: 0x06004327 RID: 17191 RVA: 0x000EA019 File Offset: 0x000E8219
		public SmtpPermission(bool unrestricted)
		{
			this.unrestricted = unrestricted;
			this.access = (unrestricted ? SmtpAccess.ConnectToUnrestrictedPort : SmtpAccess.None);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class using the specified permission state value.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x06004328 RID: 17192 RVA: 0x000EA035 File Offset: 0x000E8235
		public SmtpPermission(PermissionState state)
		{
			this.unrestricted = (state == PermissionState.Unrestricted);
			this.access = (this.unrestricted ? SmtpAccess.ConnectToUnrestrictedPort : SmtpAccess.None);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermission" /> class using the specified access level.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</param>
		// Token: 0x06004329 RID: 17193 RVA: 0x000EA059 File Offset: 0x000E8259
		public SmtpPermission(SmtpAccess access)
		{
			this.access = access;
		}

		/// <summary>Gets the level of access to SMTP servers controlled by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</returns>
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x0600432A RID: 17194 RVA: 0x000EA068 File Offset: 0x000E8268
		public SmtpAccess Access
		{
			get
			{
				return this.access;
			}
		}

		/// <summary>Adds the specified access level value to the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.Mail.SmtpAccess" /> values.</param>
		// Token: 0x0600432B RID: 17195 RVA: 0x000EA070 File Offset: 0x000E8270
		public void AddPermission(SmtpAccess access)
		{
			if (!this.unrestricted && access > this.access)
			{
				this.access = access;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> that is identical to the current permission.</returns>
		// Token: 0x0600432C RID: 17196 RVA: 0x000EA08A File Offset: 0x000E828A
		public override IPermission Copy()
		{
			if (this.unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.access);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> that represents the intersection of the current permission and the specified permission. Returns <see langword="null" /> if the intersection is empty or <paramref name="target" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x0600432D RID: 17197 RVA: 0x000EA0A8 File Offset: 0x000E82A8
		public override IPermission Intersect(IPermission target)
		{
			SmtpPermission smtpPermission = this.Cast(target);
			if (smtpPermission == null)
			{
				return null;
			}
			if (this.unrestricted && smtpPermission.unrestricted)
			{
				return new SmtpPermission(true);
			}
			if (this.access > smtpPermission.access)
			{
				return new SmtpPermission(smtpPermission.access);
			}
			return new SmtpPermission(this.access);
		}

		/// <summary>Returns a value indicating whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x0600432E RID: 17198 RVA: 0x000EA100 File Offset: 0x000E8300
		public override bool IsSubsetOf(IPermission target)
		{
			SmtpPermission smtpPermission = this.Cast(target);
			if (smtpPermission == null)
			{
				return this.IsEmpty();
			}
			if (this.unrestricted)
			{
				return smtpPermission.unrestricted;
			}
			return this.access <= smtpPermission.access;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600432F RID: 17199 RVA: 0x000EA13F File Offset: 0x000E833F
		public bool IsUnrestricted()
		{
			return this.unrestricted;
		}

		/// <summary>Creates an XML encoding of the state of the permission.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the current permission.</returns>
		// Token: 0x06004330 RID: 17200 RVA: 0x000EA148 File Offset: 0x000E8348
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(typeof(SmtpPermission), 1);
			if (this.unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				SmtpAccess smtpAccess = this.access;
				if (smtpAccess != SmtpAccess.Connect)
				{
					if (smtpAccess == SmtpAccess.ConnectToUnrestrictedPort)
					{
						securityElement.AddAttribute("Access", "ConnectToUnrestrictedPort");
					}
				}
				else
				{
					securityElement.AddAttribute("Access", "Connect");
				}
			}
			return securityElement;
		}

		/// <summary>Sets the state of the permission using the specified XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to set the state of the current permission.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> does not describe an <see cref="T:System.Net.Mail.SmtpPermission" /> object.  
		/// -or-  
		/// <paramref name="securityElement" /> does not contain the required state information to reconstruct the permission.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		// Token: 0x06004331 RID: 17201 RVA: 0x000EA1B4 File Offset: 0x000E83B4
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			if (PermissionHelper.IsUnrestricted(securityElement))
			{
				this.access = SmtpAccess.Connect;
				return;
			}
			this.access = SmtpAccess.None;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">An <see cref="T:System.Security.IPermission" /> to combine with the current permission.</param>
		/// <returns>A new <see cref="T:System.Net.Mail.SmtpPermission" /> permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Net.Mail.SmtpPermission" />.</exception>
		// Token: 0x06004332 RID: 17202 RVA: 0x000EA204 File Offset: 0x000E8404
		public override IPermission Union(IPermission target)
		{
			SmtpPermission smtpPermission = this.Cast(target);
			if (smtpPermission == null)
			{
				return this.Copy();
			}
			if (this.unrestricted || smtpPermission.unrestricted)
			{
				return new SmtpPermission(true);
			}
			if (this.access > smtpPermission.access)
			{
				return new SmtpPermission(this.access);
			}
			return new SmtpPermission(smtpPermission.access);
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x000EA25F File Offset: 0x000E845F
		private bool IsEmpty()
		{
			return !this.unrestricted && this.access == SmtpAccess.None;
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x000EA274 File Offset: 0x000E8474
		private SmtpPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SmtpPermission smtpPermission = target as SmtpPermission;
			if (smtpPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, typeof(SmtpPermission));
			}
			return smtpPermission;
		}

		// Token: 0x040028A7 RID: 10407
		private const int version = 1;

		// Token: 0x040028A8 RID: 10408
		private bool unrestricted;

		// Token: 0x040028A9 RID: 10409
		private SmtpAccess access;
	}
}
