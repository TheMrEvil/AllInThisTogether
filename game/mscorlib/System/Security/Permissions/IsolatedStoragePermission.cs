using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Represents access to generic isolated storage capabilities.</summary>
	// Token: 0x02000443 RID: 1091
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
	[Serializable]
	public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> class with either restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002C3C RID: 11324 RVA: 0x0009F3BC File Offset: 0x0009D5BC
		protected IsolatedStoragePermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
			}
		}

		/// <summary>Gets or sets the quota on the overall size of each user's total store.</summary>
		/// <returns>The size, in bytes, of the resource allocated to the user.</returns>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06002C3D RID: 11325 RVA: 0x0009F3D9 File Offset: 0x0009D5D9
		// (set) Token: 0x06002C3E RID: 11326 RVA: 0x0009F3E1 File Offset: 0x0009D5E1
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		/// <summary>Gets or sets the type of isolated storage containment allowed.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.IsolatedStorageContainment" /> values.</returns>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002C3F RID: 11327 RVA: 0x0009F3EA File Offset: 0x0009D5EA
		// (set) Token: 0x06002C40 RID: 11328 RVA: 0x0009F3F4 File Offset: 0x0009D5F4
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				if (!Enum.IsDefined(typeof(IsolatedStorageContainment), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "IsolatedStorageContainment");
				}
				this.m_allowed = value;
				if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
				{
					this.m_userQuota = long.MaxValue;
					this.m_machineQuota = long.MaxValue;
					this.m_expirationDays = long.MaxValue;
					this.m_permanentData = true;
				}
			}
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002C41 RID: 11329 RVA: 0x0009F480 File Offset: 0x0009D680
		public bool IsUnrestricted()
		{
			return IsolatedStorageContainment.UnrestrictedIsolatedStorage == this.m_allowed;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002C42 RID: 11330 RVA: 0x0009F490 File Offset: 0x0009D690
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("Allowed", this.m_allowed.ToString());
				if (this.m_userQuota > 0L)
				{
					securityElement.AddAttribute("UserQuota", this.m_userQuota.ToString());
				}
			}
			return securityElement;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not valid.</exception>
		// Token: 0x06002C43 RID: 11331 RVA: 0x0009F504 File Offset: 0x0009D704
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			this.m_userQuota = 0L;
			this.m_machineQuota = 0L;
			this.m_expirationDays = 0L;
			this.m_permanentData = false;
			this.m_allowed = IsolatedStorageContainment.None;
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.UsageAllowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
				return;
			}
			string text = esd.Attribute("Allowed");
			if (text != null)
			{
				this.UsageAllowed = (IsolatedStorageContainment)Enum.Parse(typeof(IsolatedStorageContainment), text);
			}
			text = esd.Attribute("UserQuota");
			if (text != null)
			{
				this.m_userQuota = long.Parse(text, CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x0009F5A3 File Offset: 0x0009D7A3
		internal bool IsEmpty()
		{
			return this.m_userQuota == 0L && this.m_allowed == IsolatedStorageContainment.None;
		}

		// Token: 0x04002042 RID: 8258
		private const int version = 1;

		// Token: 0x04002043 RID: 8259
		internal long m_userQuota;

		// Token: 0x04002044 RID: 8260
		internal long m_machineQuota;

		// Token: 0x04002045 RID: 8261
		internal long m_expirationDays;

		// Token: 0x04002046 RID: 8262
		internal bool m_permanentData;

		// Token: 0x04002047 RID: 8263
		internal IsolatedStorageContainment m_allowed;
	}
}
