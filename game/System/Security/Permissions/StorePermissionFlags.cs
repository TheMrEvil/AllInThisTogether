using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted access to X.509 certificate stores.</summary>
	// Token: 0x0200029A RID: 666
	[Flags]
	[Serializable]
	public enum StorePermissionFlags
	{
		/// <summary>Permission is not given to perform any certificate or store operations.</summary>
		// Token: 0x04000BC7 RID: 3015
		NoFlags = 0,
		/// <summary>The ability to create a new store.</summary>
		// Token: 0x04000BC8 RID: 3016
		CreateStore = 1,
		/// <summary>The ability to delete a store.</summary>
		// Token: 0x04000BC9 RID: 3017
		DeleteStore = 2,
		/// <summary>The ability to enumerate the stores on a computer.</summary>
		// Token: 0x04000BCA RID: 3018
		EnumerateStores = 4,
		/// <summary>The ability to open a store.</summary>
		// Token: 0x04000BCB RID: 3019
		OpenStore = 16,
		/// <summary>The ability to add a certificate to a store.</summary>
		// Token: 0x04000BCC RID: 3020
		AddToStore = 32,
		/// <summary>The ability to remove a certificate from a store.</summary>
		// Token: 0x04000BCD RID: 3021
		RemoveFromStore = 64,
		/// <summary>The ability to enumerate the certificates in a store.</summary>
		// Token: 0x04000BCE RID: 3022
		EnumerateCertificates = 128,
		/// <summary>The ability to perform all certificate and store operations.</summary>
		// Token: 0x04000BCF RID: 3023
		AllFlags = 247
	}
}
