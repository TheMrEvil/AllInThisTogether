using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.FileIOPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x0200043A RID: 1082
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class FileIOPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileIOPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="action" /> parameter is not a valid <see cref="T:System.Security.Permissions.SecurityAction" />.</exception>
		// Token: 0x06002BEB RID: 11243 RVA: 0x0009DCDC File Offset: 0x0009BEDC
		public FileIOPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets full access for the file or directory that is specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for full access.</returns>
		/// <exception cref="T:System.NotSupportedException">The get method is not supported for this property.</exception>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x0009DCE5 File Offset: 0x0009BEE5
		// (set) Token: 0x06002BED RID: 11245 RVA: 0x0009EB10 File Offset: 0x0009CD10
		[Obsolete("use newer properties")]
		public string All
		{
			get
			{
				throw new NotSupportedException("All");
			}
			set
			{
				this.append = value;
				this.path = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Gets or sets append access for the file or directory that is specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for append access.</returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x0009EB2E File Offset: 0x0009CD2E
		// (set) Token: 0x06002BEF RID: 11247 RVA: 0x0009EB36 File Offset: 0x0009CD36
		public string Append
		{
			get
			{
				return this.append;
			}
			set
			{
				this.append = value;
			}
		}

		/// <summary>Gets or sets the file or directory to which to grant path discovery.</summary>
		/// <returns>The absolute path of the file or directory.</returns>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x0009EB3F File Offset: 0x0009CD3F
		// (set) Token: 0x06002BF1 RID: 11249 RVA: 0x0009EB47 File Offset: 0x0009CD47
		public string PathDiscovery
		{
			get
			{
				return this.path;
			}
			set
			{
				this.path = value;
			}
		}

		/// <summary>Gets or sets read access for the file or directory specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for read access.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x0009EB50 File Offset: 0x0009CD50
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x0009EB58 File Offset: 0x0009CD58
		public string Read
		{
			get
			{
				return this.read;
			}
			set
			{
				this.read = value;
			}
		}

		/// <summary>Gets or sets write access for the file or directory specified by the string value.</summary>
		/// <returns>The absolute path of the file or directory for write access.</returns>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x0009EB61 File Offset: 0x0009CD61
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x0009EB69 File Offset: 0x0009CD69
		public string Write
		{
			get
			{
				return this.write;
			}
			set
			{
				this.write = value;
			}
		}

		/// <summary>Gets or sets the permitted access to all files.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents the permissions for all files. The default is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.</returns>
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x0009EB72 File Offset: 0x0009CD72
		// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x0009EB7A File Offset: 0x0009CD7A
		public FileIOPermissionAccess AllFiles
		{
			get
			{
				return this.allFiles;
			}
			set
			{
				this.allFiles = value;
			}
		}

		/// <summary>Gets or sets the permitted access to all local files.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> values that represents the permissions for all local files. The default is <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.</returns>
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x0009EB83 File Offset: 0x0009CD83
		// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x0009EB8B File Offset: 0x0009CD8B
		public FileIOPermissionAccess AllLocalFiles
		{
			get
			{
				return this.allLocalFiles;
			}
			set
			{
				this.allLocalFiles = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which access control information can be changed.</summary>
		/// <returns>The absolute path of the file or directory in which access control information can be changed.</returns>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x0009EB94 File Offset: 0x0009CD94
		// (set) Token: 0x06002BFB RID: 11259 RVA: 0x0009EB9C File Offset: 0x0009CD9C
		public string ChangeAccessControl
		{
			get
			{
				return this.changeAccessControl;
			}
			set
			{
				this.changeAccessControl = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which access control information can be viewed.</summary>
		/// <returns>The absolute path of the file or directory in which access control information can be viewed.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x0009EBA5 File Offset: 0x0009CDA5
		// (set) Token: 0x06002BFD RID: 11261 RVA: 0x0009EBAD File Offset: 0x0009CDAD
		public string ViewAccessControl
		{
			get
			{
				return this.viewAccessControl;
			}
			set
			{
				this.viewAccessControl = value;
			}
		}

		/// <summary>Gets or sets the file or directory in which file data can be viewed and modified.</summary>
		/// <returns>The absolute path of the file or directory in which file data can be viewed and modified.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see langword="get" /> accessor is called. The accessor is provided only for C# compiler compatibility.</exception>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000472C8 File Offset: 0x000454C8
		// (set) Token: 0x06002BFF RID: 11263 RVA: 0x0009EB10 File Offset: 0x0009CD10
		public string ViewAndModify
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				this.append = value;
				this.path = value;
				this.read = value;
				this.write = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.FileIOPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.FileIOPermission" /> that corresponds to this attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The path information for a file or directory for which access is to be secured contains invalid characters or wildcard specifiers.</exception>
		// Token: 0x06002C00 RID: 11264 RVA: 0x0009EBB8 File Offset: 0x0009CDB8
		public override IPermission CreatePermission()
		{
			FileIOPermission fileIOPermission;
			if (base.Unrestricted)
			{
				fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
			}
			else
			{
				fileIOPermission = new FileIOPermission(PermissionState.None);
				if (this.append != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Append, this.append);
				}
				if (this.path != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.PathDiscovery, this.path);
				}
				if (this.read != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Read, this.read);
				}
				if (this.write != null)
				{
					fileIOPermission.AddPathList(FileIOPermissionAccess.Write, this.write);
				}
			}
			return fileIOPermission;
		}

		// Token: 0x04002024 RID: 8228
		private string append;

		// Token: 0x04002025 RID: 8229
		private string path;

		// Token: 0x04002026 RID: 8230
		private string read;

		// Token: 0x04002027 RID: 8231
		private string write;

		// Token: 0x04002028 RID: 8232
		private FileIOPermissionAccess allFiles;

		// Token: 0x04002029 RID: 8233
		private FileIOPermissionAccess allLocalFiles;

		// Token: 0x0400202A RID: 8234
		private string changeAccessControl;

		// Token: 0x0400202B RID: 8235
		private string viewAccessControl;
	}
}
