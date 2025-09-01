using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the permissions related to user interfaces and the Clipboard. This class cannot be inherited.</summary>
	// Token: 0x02000460 RID: 1120
	[ComVisible(true)]
	[Serializable]
	public sealed class UIPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with either fully restricted or unrestricted access, as specified.</summary>
		/// <param name="state">One of the enumeration values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002D81 RID: 11649 RVA: 0x000A31F9 File Offset: 0x000A13F9
		public UIPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._clipboard = UIPermissionClipboard.AllClipboard;
				this._window = UIPermissionWindow.AllWindows;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the permissions for the Clipboard, and no access to windows.</summary>
		/// <param name="clipboardFlag">One of the enumeration values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="clipboardFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> value.</exception>
		// Token: 0x06002D82 RID: 11650 RVA: 0x000A3219 File Offset: 0x000A1419
		public UIPermission(UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the permissions for windows, and no access to the Clipboard.</summary>
		/// <param name="windowFlag">One of the enumeration values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="windowFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionWindow" /> value.</exception>
		// Token: 0x06002D83 RID: 11651 RVA: 0x000A3228 File Offset: 0x000A1428
		public UIPermission(UIPermissionWindow windowFlag)
		{
			this.Window = windowFlag;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.UIPermission" /> class with the specified permissions for windows and the Clipboard.</summary>
		/// <param name="windowFlag">One of the enumeration values.</param>
		/// <param name="clipboardFlag">One of the enumeration values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="windowFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionWindow" /> value.  
		///  -or-  
		///  The <paramref name="clipboardFlag" /> parameter is not a valid <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> value.</exception>
		// Token: 0x06002D84 RID: 11652 RVA: 0x000A3237 File Offset: 0x000A1437
		public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
		{
			this.Clipboard = clipboardFlag;
			this.Window = windowFlag;
		}

		/// <summary>Gets or sets the Clipboard access represented by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionClipboard" /> values.</returns>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x000A324D File Offset: 0x000A144D
		// (set) Token: 0x06002D86 RID: 11654 RVA: 0x000A3255 File Offset: 0x000A1455
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this._clipboard;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionClipboard), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "UIPermissionClipboard");
				}
				this._clipboard = value;
			}
		}

		/// <summary>Gets or sets the window access represented by the permission.</summary>
		/// <returns>One of the <see cref="T:System.Security.Permissions.UIPermissionWindow" /> values.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x000A3295 File Offset: 0x000A1495
		// (set) Token: 0x06002D88 RID: 11656 RVA: 0x000A329D File Offset: 0x000A149D
		public UIPermissionWindow Window
		{
			get
			{
				return this._window;
			}
			set
			{
				if (!Enum.IsDefined(typeof(UIPermissionWindow), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "UIPermissionWindow");
				}
				this._window = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002D89 RID: 11657 RVA: 0x000A32DD File Offset: 0x000A14DD
		public override IPermission Copy()
		{
			return new UIPermission(this._window, this._clipboard);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not valid.</exception>
		// Token: 0x06002D8A RID: 11658 RVA: 0x000A32F0 File Offset: 0x000A14F0
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._window = UIPermissionWindow.AllWindows;
				this._clipboard = UIPermissionClipboard.AllClipboard;
				return;
			}
			string text = esd.Attribute("Window");
			if (text == null)
			{
				this._window = UIPermissionWindow.NoWindows;
			}
			else
			{
				this._window = (UIPermissionWindow)Enum.Parse(typeof(UIPermissionWindow), text);
			}
			string text2 = esd.Attribute("Clipboard");
			if (text2 == null)
			{
				this._clipboard = UIPermissionClipboard.NoClipboard;
				return;
			}
			this._clipboard = (UIPermissionClipboard)Enum.Parse(typeof(UIPermissionClipboard), text2);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002D8B RID: 11659 RVA: 0x000A3388 File Offset: 0x000A1588
		public override IPermission Intersect(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return null;
			}
			UIPermissionWindow uipermissionWindow = (this._window < uipermission._window) ? this._window : uipermission._window;
			UIPermissionClipboard uipermissionClipboard = (this._clipboard < uipermission._clipboard) ? this._clipboard : uipermission._clipboard;
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002D8C RID: 11660 RVA: 0x000A33F0 File Offset: 0x000A15F0
		public override bool IsSubsetOf(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.IsEmpty(this._window, this._clipboard);
			}
			return uipermission.IsUnrestricted() || (this._window <= uipermission._window && this._clipboard <= uipermission._clipboard);
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002D8D RID: 11661 RVA: 0x000A3446 File Offset: 0x000A1646
		public bool IsUnrestricted()
		{
			return this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002D8E RID: 11662 RVA: 0x000A345C File Offset: 0x000A165C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._window == UIPermissionWindow.AllWindows && this._clipboard == UIPermissionClipboard.AllClipboard)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				if (this._window != UIPermissionWindow.NoWindows)
				{
					securityElement.AddAttribute("Window", this._window.ToString());
				}
				if (this._clipboard != UIPermissionClipboard.NoClipboard)
				{
					securityElement.AddAttribute("Clipboard", this._clipboard.ToString());
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002D8F RID: 11663 RVA: 0x000A34E0 File Offset: 0x000A16E0
		public override IPermission Union(IPermission target)
		{
			UIPermission uipermission = this.Cast(target);
			if (uipermission == null)
			{
				return this.Copy();
			}
			UIPermissionWindow uipermissionWindow = (this._window > uipermission._window) ? this._window : uipermission._window;
			UIPermissionClipboard uipermissionClipboard = (this._clipboard > uipermission._clipboard) ? this._clipboard : uipermission._clipboard;
			if (this.IsEmpty(uipermissionWindow, uipermissionClipboard))
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x0003227E File Offset: 0x0003047E
		int IBuiltInPermission.GetTokenIndex()
		{
			return 7;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000A354C File Offset: 0x000A174C
		private bool IsEmpty(UIPermissionWindow w, UIPermissionClipboard c)
		{
			return w == UIPermissionWindow.NoWindows && c == UIPermissionClipboard.NoClipboard;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000A3557 File Offset: 0x000A1757
		private UIPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UIPermission uipermission = target as UIPermission;
			if (uipermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(UIPermission));
			}
			return uipermission;
		}

		// Token: 0x040020B8 RID: 8376
		private UIPermissionWindow _window;

		// Token: 0x040020B9 RID: 8377
		private UIPermissionClipboard _clipboard;

		// Token: 0x040020BA RID: 8378
		private const int version = 1;
	}
}
