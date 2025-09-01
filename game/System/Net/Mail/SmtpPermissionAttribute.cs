using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net.Mail
{
	/// <summary>Controls access to Simple Mail Transport Protocol (SMTP) servers.</summary>
	// Token: 0x0200083D RID: 2109
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SmtpPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values that specifies the permission behavior.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x06004335 RID: 17205 RVA: 0x000A97B6 File Offset: 0x000A79B6
		public SmtpPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the level of access to SMTP servers controlled by the attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> value. Valid values are "Connect" and "None".</returns>
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x000EA294 File Offset: 0x000E8494
		// (set) Token: 0x06004337 RID: 17207 RVA: 0x000EA29C File Offset: 0x000E849C
		public string Access
		{
			get
			{
				return this.access;
			}
			set
			{
				this.access = value;
			}
		}

		// Token: 0x06004338 RID: 17208 RVA: 0x000EA2A8 File Offset: 0x000E84A8
		private SmtpAccess GetSmtpAccess()
		{
			if (this.access == null)
			{
				return SmtpAccess.None;
			}
			string a = this.access.ToLowerInvariant();
			if (a == "connecttounrestrictedport")
			{
				return SmtpAccess.ConnectToUnrestrictedPort;
			}
			if (a == "connect")
			{
				return SmtpAccess.Connect;
			}
			if (!(a == "none"))
			{
				string text = Locale.GetText("Invalid Access='{0}' value.", new object[]
				{
					this.access
				});
				throw new ArgumentException("Access", text);
			}
			return SmtpAccess.None;
		}

		/// <summary>Creates a permission object that can be stored with the <see cref="T:System.Security.Permissions.SecurityAction" /> in an assembly's metadata.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpPermission" /> instance.</returns>
		// Token: 0x06004339 RID: 17209 RVA: 0x000EA31F File Offset: 0x000E851F
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new SmtpPermission(true);
			}
			return new SmtpPermission(this.GetSmtpAccess());
		}

		// Token: 0x040028AA RID: 10410
		private string access;
	}
}
