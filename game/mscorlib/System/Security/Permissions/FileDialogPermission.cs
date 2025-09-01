using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access files or folders through a File dialog box. This class cannot be inherited.</summary>
	// Token: 0x02000436 RID: 1078
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileDialogPermission" /> class with either restricted or unrestricted permission, as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values (<see langword="Unrestricted" /> or <see langword="None" />).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002BB3 RID: 11187 RVA: 0x0009DD76 File Offset: 0x0009BF76
		public FileDialogPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._access = FileDialogPermissionAccess.OpenSave;
				return;
			}
			this._access = FileDialogPermissionAccess.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileDialogPermission" /> class with the specified access.</summary>
		/// <param name="access">A bitwise combination of the <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid combination of the <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" /> values.</exception>
		// Token: 0x06002BB4 RID: 11188 RVA: 0x0009DD97 File Offset: 0x0009BF97
		public FileDialogPermission(FileDialogPermissionAccess access)
		{
			this.Access = access;
		}

		/// <summary>Gets or sets the permitted access to files.</summary>
		/// <returns>The permitted access to files.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set the <paramref name="access" /> parameter to a value that is not a valid combination of the <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" /> values.</exception>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002BB5 RID: 11189 RVA: 0x0009DDA6 File Offset: 0x0009BFA6
		// (set) Token: 0x06002BB6 RID: 11190 RVA: 0x0009DDAE File Offset: 0x0009BFAE
		public FileDialogPermissionAccess Access
		{
			get
			{
				return this._access;
			}
			set
			{
				if (!Enum.IsDefined(typeof(FileDialogPermissionAccess), value))
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), value), "FileDialogPermissionAccess");
				}
				this._access = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002BB7 RID: 11191 RVA: 0x0009DDEE File Offset: 0x0009BFEE
		public override IPermission Copy()
		{
			return new FileDialogPermission(this._access);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The version number of the <paramref name="esd" /> parameter is not supported.</exception>
		// Token: 0x06002BB8 RID: 11192 RVA: 0x0009DDFC File Offset: 0x0009BFFC
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._access = FileDialogPermissionAccess.OpenSave;
				return;
			}
			string text = esd.Attribute("Access");
			if (text == null)
			{
				this._access = FileDialogPermissionAccess.None;
				return;
			}
			this._access = (FileDialogPermissionAccess)Enum.Parse(typeof(FileDialogPermissionAccess), text);
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002BB9 RID: 11193 RVA: 0x0009DE5C File Offset: 0x0009C05C
		public override IPermission Intersect(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			if (fileDialogPermission == null)
			{
				return null;
			}
			FileDialogPermissionAccess fileDialogPermissionAccess = this._access & fileDialogPermission._access;
			if (fileDialogPermissionAccess != FileDialogPermissionAccess.None)
			{
				return new FileDialogPermission(fileDialogPermissionAccess);
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002BBA RID: 11194 RVA: 0x0009DE90 File Offset: 0x0009C090
		public override bool IsSubsetOf(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			return fileDialogPermission != null && (this._access & fileDialogPermission._access) == this._access;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002BBB RID: 11195 RVA: 0x0009DEBF File Offset: 0x0009C0BF
		public bool IsUnrestricted()
		{
			return this._access == FileDialogPermissionAccess.OpenSave;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including state information.</returns>
		// Token: 0x06002BBC RID: 11196 RVA: 0x0009DECC File Offset: 0x0009C0CC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			switch (this._access)
			{
			case FileDialogPermissionAccess.Open:
				securityElement.AddAttribute("Access", "Open");
				break;
			case FileDialogPermissionAccess.Save:
				securityElement.AddAttribute("Access", "Save");
				break;
			case FileDialogPermissionAccess.OpenSave:
				securityElement.AddAttribute("Unrestricted", "true");
				break;
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002BBD RID: 11197 RVA: 0x0009DF34 File Offset: 0x0009C134
		public override IPermission Union(IPermission target)
		{
			FileDialogPermission fileDialogPermission = this.Cast(target);
			if (fileDialogPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || fileDialogPermission.IsUnrestricted())
			{
				return new FileDialogPermission(PermissionState.Unrestricted);
			}
			return new FileDialogPermission(this._access | fileDialogPermission._access);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000040F7 File Offset: 0x000022F7
		int IBuiltInPermission.GetTokenIndex()
		{
			return 1;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x0009DF7C File Offset: 0x0009C17C
		private FileDialogPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			FileDialogPermission fileDialogPermission = target as FileDialogPermission;
			if (fileDialogPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(FileDialogPermission));
			}
			return fileDialogPermission;
		}

		// Token: 0x0400200F RID: 8207
		private const int version = 1;

		// Token: 0x04002010 RID: 8208
		private FileDialogPermissionAccess _access;
	}
}
