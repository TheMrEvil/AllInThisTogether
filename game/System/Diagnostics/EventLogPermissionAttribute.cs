using System;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Allows declaritive permission checks for event logging.</summary>
	// Token: 0x02000262 RID: 610
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public class EventLogPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventLogPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06001310 RID: 4880 RVA: 0x00050F14 File Offset: 0x0004F114
		public EventLogPermissionAttribute(SecurityAction action) : base(action)
		{
			this.machineName = ".";
			this.permissionAccess = EventLogPermissionAccess.Write;
		}

		/// <summary>Gets or sets the name of the computer on which events might be read.</summary>
		/// <returns>The name of the computer on which events might be read. The default is ".".</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid.</exception>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00050F30 File Offset: 0x0004F130
		// (set) Token: 0x06001312 RID: 4882 RVA: 0x00050F38 File Offset: 0x0004F138
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
			set
			{
				ResourcePermissionBase.ValidateMachineName(value);
				this.machineName = value;
			}
		}

		/// <summary>Gets or sets the access levels used in the permissions request.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Diagnostics.EventLogPermissionAccess" /> values. The default is <see cref="F:System.Diagnostics.EventLogPermissionAccess.Write" />.</returns>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x00050F47 File Offset: 0x0004F147
		// (set) Token: 0x06001314 RID: 4884 RVA: 0x00050F4F File Offset: 0x0004F14F
		public EventLogPermissionAccess PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
			set
			{
				this.permissionAccess = value;
			}
		}

		/// <summary>Creates the permission based on the <see cref="P:System.Diagnostics.EventLogPermissionAttribute.MachineName" /> property and the requested access levels that are set through the <see cref="P:System.Diagnostics.EventLogPermissionAttribute.PermissionAccess" /> property on the attribute.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
		// Token: 0x06001315 RID: 4885 RVA: 0x00050F58 File Offset: 0x0004F158
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new EventLogPermission(PermissionState.Unrestricted);
			}
			return new EventLogPermission(this.permissionAccess, this.machineName);
		}

		// Token: 0x04000AD7 RID: 2775
		private string machineName;

		// Token: 0x04000AD8 RID: 2776
		private EventLogPermissionAccess permissionAccess;
	}
}
