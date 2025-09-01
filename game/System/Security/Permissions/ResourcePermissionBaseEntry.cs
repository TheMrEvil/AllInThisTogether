using System;

namespace System.Security.Permissions
{
	/// <summary>Defines the smallest unit of a code access security permission set.</summary>
	// Token: 0x02000297 RID: 663
	[Serializable]
	public class ResourcePermissionBaseEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class.</summary>
		// Token: 0x060014F6 RID: 5366 RVA: 0x000552AB File Offset: 0x000534AB
		public ResourcePermissionBaseEntry()
		{
			this.permissionAccessPath = new string[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ResourcePermissionBaseEntry" /> class with the specified permission access and permission access path.</summary>
		/// <param name="permissionAccess">The integer representation of the permission access level enumeration value. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccess" /> property is set to this value.</param>
		/// <param name="permissionAccessPath">The array of strings that identify the resource you are protecting. The <see cref="P:System.Security.Permissions.ResourcePermissionBaseEntry.PermissionAccessPath" /> property is set to this value.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="permissionAccessPath" /> is <see langword="null" />.</exception>
		// Token: 0x060014F7 RID: 5367 RVA: 0x000552BF File Offset: 0x000534BF
		public ResourcePermissionBaseEntry(int permissionAccess, string[] permissionAccessPath)
		{
			if (permissionAccessPath == null)
			{
				throw new ArgumentNullException("permissionAccessPath");
			}
			this.permissionAccess = permissionAccess;
			this.permissionAccessPath = permissionAccessPath;
		}

		/// <summary>Gets an integer representation of the access level enumeration value.</summary>
		/// <returns>The access level enumeration value.</returns>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x000552E3 File Offset: 0x000534E3
		public int PermissionAccess
		{
			get
			{
				return this.permissionAccess;
			}
		}

		/// <summary>Gets an array of strings that identify the resource you are protecting.</summary>
		/// <returns>An array of strings that identify the resource you are protecting.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000552EB File Offset: 0x000534EB
		public string[] PermissionAccessPath
		{
			get
			{
				return this.permissionAccessPath;
			}
		}

		// Token: 0x04000BC1 RID: 3009
		private int permissionAccess;

		// Token: 0x04000BC2 RID: 3010
		private string[] permissionAccessPath;
	}
}
