using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of key container access allowed.</summary>
	// Token: 0x0200044A RID: 1098
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum KeyContainerPermissionFlags
	{
		/// <summary>No access to a key container.</summary>
		// Token: 0x0400205C RID: 8284
		NoFlags = 0,
		/// <summary>Create a key container.</summary>
		// Token: 0x0400205D RID: 8285
		Create = 1,
		/// <summary>Open a key container and use the public key.</summary>
		// Token: 0x0400205E RID: 8286
		Open = 2,
		/// <summary>Delete a key container.</summary>
		// Token: 0x0400205F RID: 8287
		Delete = 4,
		/// <summary>Import a key into a key container.</summary>
		// Token: 0x04002060 RID: 8288
		Import = 16,
		/// <summary>Export a key from a key container.</summary>
		// Token: 0x04002061 RID: 8289
		Export = 32,
		/// <summary>Sign a file using a key.</summary>
		// Token: 0x04002062 RID: 8290
		Sign = 256,
		/// <summary>Decrypt a key container.</summary>
		// Token: 0x04002063 RID: 8291
		Decrypt = 512,
		/// <summary>View the access control list (ACL) for a key container.</summary>
		// Token: 0x04002064 RID: 8292
		ViewAcl = 4096,
		/// <summary>Change the access control list (ACL) for a key container.</summary>
		// Token: 0x04002065 RID: 8293
		ChangeAcl = 8192,
		/// <summary>Create, decrypt, delete, and open a key container; export and import a key; sign files using a key; and view and change the access control list for a key container.</summary>
		// Token: 0x04002066 RID: 8294
		AllFlags = 13111
	}
}
