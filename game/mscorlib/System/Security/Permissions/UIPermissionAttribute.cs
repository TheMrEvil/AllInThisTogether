using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.UIPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x02000461 RID: 1121
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class UIPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		// Token: 0x06002D93 RID: 11667 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public UIPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the type of access to the clipboard that is permitted.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> values.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x000A3577 File Offset: 0x000A1777
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x000A357F File Offset: 0x000A177F
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this.clipboard;
			}
			set
			{
				this.clipboard = value;
			}
		}

		/// <summary>Gets or sets the type of access to the window resources that is permitted.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionWindow" /> values.</returns>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x000A3588 File Offset: 0x000A1788
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x000A3590 File Offset: 0x000A1790
		public UIPermissionWindow Window
		{
			get
			{
				return this.window;
			}
			set
			{
				this.window = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.UIPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.UIPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06002D98 RID: 11672 RVA: 0x000A359C File Offset: 0x000A179C
		public override IPermission CreatePermission()
		{
			UIPermission result;
			if (base.Unrestricted)
			{
				result = new UIPermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new UIPermission(this.window, this.clipboard);
			}
			return result;
		}

		// Token: 0x040020BB RID: 8379
		private UIPermissionClipboard clipboard;

		// Token: 0x040020BC RID: 8380
		private UIPermissionWindow window;
	}
}
