using System;

namespace System.Security.AccessControl
{
	/// <summary>Defines the access rights to use when creating access and audit rules.</summary>
	// Token: 0x0200052A RID: 1322
	[Flags]
	public enum FileSystemRights
	{
		/// <summary>Specifies the right to read the contents of a directory.</summary>
		// Token: 0x0400248E RID: 9358
		ListDirectory = 1,
		/// <summary>Specifies the right to open and copy a file or folder.  This does not include the right to read file system attributes, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x0400248F RID: 9359
		ReadData = 1,
		/// <summary>Specifies the right to create a file.</summary>
		// Token: 0x04002490 RID: 9360
		CreateFiles = 2,
		/// <summary>Specifies the right to open and write to a file or folder.  This does not include the right to open and write file system attributes, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x04002491 RID: 9361
		WriteData = 2,
		/// <summary>Specifies the right to append data to the end of a file.</summary>
		// Token: 0x04002492 RID: 9362
		AppendData = 4,
		/// <summary>Specifies the right to create a folder.</summary>
		// Token: 0x04002493 RID: 9363
		CreateDirectories = 4,
		/// <summary>Specifies the right to open and copy extended file system attributes from a folder or file.  For example, this value specifies the right to view author and content information.  This does not include the right to read data, file system attributes, or access and audit rules.</summary>
		// Token: 0x04002494 RID: 9364
		ReadExtendedAttributes = 8,
		/// <summary>Specifies the right to open and write extended file system attributes to a folder or file.  This does not include the ability to write data, attributes, or access and audit rules.</summary>
		// Token: 0x04002495 RID: 9365
		WriteExtendedAttributes = 16,
		/// <summary>Specifies the right to run an application file.</summary>
		// Token: 0x04002496 RID: 9366
		ExecuteFile = 32,
		/// <summary>Specifies the right to list the contents of a folder and to run applications contained within that folder.</summary>
		// Token: 0x04002497 RID: 9367
		Traverse = 32,
		/// <summary>Specifies the right to delete a folder and any files contained within that folder.</summary>
		// Token: 0x04002498 RID: 9368
		DeleteSubdirectoriesAndFiles = 64,
		/// <summary>Specifies the right to open and copy file system attributes from a folder or file.  For example, this value specifies the right to view the file creation or modified date.  This does not include the right to read data, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x04002499 RID: 9369
		ReadAttributes = 128,
		/// <summary>Specifies the right to open and write file system attributes to a folder or file. This does not include the ability to write data, extended attributes, or access and audit rules.</summary>
		// Token: 0x0400249A RID: 9370
		WriteAttributes = 256,
		/// <summary>Specifies the right to create folders and files, and to add or remove data from files.  This right includes the <see cref="F:System.Security.AccessControl.FileSystemRights.WriteData" /> right, <see cref="F:System.Security.AccessControl.FileSystemRights.AppendData" /> right, <see cref="F:System.Security.AccessControl.FileSystemRights.WriteExtendedAttributes" /> right, and <see cref="F:System.Security.AccessControl.FileSystemRights.WriteAttributes" /> right.</summary>
		// Token: 0x0400249B RID: 9371
		Write = 278,
		/// <summary>Specifies the right to delete a folder or file.</summary>
		// Token: 0x0400249C RID: 9372
		Delete = 65536,
		/// <summary>Specifies the right to open and copy access and audit rules from a folder or file.  This does not include the right to read data, file system attributes, and extended file system attributes.</summary>
		// Token: 0x0400249D RID: 9373
		ReadPermissions = 131072,
		/// <summary>Specifies the right to open and copy folders or files as read-only.  This right includes the <see cref="F:System.Security.AccessControl.FileSystemRights.ReadData" /> right, <see cref="F:System.Security.AccessControl.FileSystemRights.ReadExtendedAttributes" /> right, <see cref="F:System.Security.AccessControl.FileSystemRights.ReadAttributes" /> right, and <see cref="F:System.Security.AccessControl.FileSystemRights.ReadPermissions" /> right.</summary>
		// Token: 0x0400249E RID: 9374
		Read = 131209,
		/// <summary>Specifies the right to open and copy folders or files as read-only, and to run application files.  This right includes the <see cref="F:System.Security.AccessControl.FileSystemRights.Read" /> right and the <see cref="F:System.Security.AccessControl.FileSystemRights.ExecuteFile" /> right.</summary>
		// Token: 0x0400249F RID: 9375
		ReadAndExecute = 131241,
		/// <summary>Specifies the right to read, write, list folder contents, delete folders and files, and run application files.  This right includes the <see cref="F:System.Security.AccessControl.FileSystemRights.ReadAndExecute" /> right, the <see cref="F:System.Security.AccessControl.FileSystemRights.Write" /> right, and the <see cref="F:System.Security.AccessControl.FileSystemRights.Delete" /> right.</summary>
		// Token: 0x040024A0 RID: 9376
		Modify = 197055,
		/// <summary>Specifies the right to change the security and audit rules associated with a file or folder.</summary>
		// Token: 0x040024A1 RID: 9377
		ChangePermissions = 262144,
		/// <summary>Specifies the right to change the owner of a folder or file.  Note that owners of a resource have full access to that resource.</summary>
		// Token: 0x040024A2 RID: 9378
		TakeOwnership = 524288,
		/// <summary>Specifies whether the application can wait for a file handle to synchronize with the completion of an I/O operation.</summary>
		// Token: 0x040024A3 RID: 9379
		Synchronize = 1048576,
		/// <summary>Specifies the right to exert full control over a folder or file, and to modify access control and audit rules.  This value represents the right to do anything with a file and is the combination of all rights in this enumeration.</summary>
		// Token: 0x040024A4 RID: 9380
		FullControl = 2032127
	}
}
