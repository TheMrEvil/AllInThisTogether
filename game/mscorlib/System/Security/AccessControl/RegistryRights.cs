using System;

namespace System.Security.AccessControl
{
	/// <summary>Specifies the access control rights that can be applied to registry objects.</summary>
	// Token: 0x02000547 RID: 1351
	[Flags]
	public enum RegistryRights
	{
		/// <summary>The right to query the name/value pairs in a registry key.</summary>
		// Token: 0x040024DE RID: 9438
		QueryValues = 1,
		/// <summary>The right to create, delete, or set name/value pairs in a registry key.</summary>
		// Token: 0x040024DF RID: 9439
		SetValue = 2,
		/// <summary>The right to create subkeys of a registry key.</summary>
		// Token: 0x040024E0 RID: 9440
		CreateSubKey = 4,
		/// <summary>The right to list the subkeys of a registry key.</summary>
		// Token: 0x040024E1 RID: 9441
		EnumerateSubKeys = 8,
		/// <summary>The right to request notification of changes on a registry key.</summary>
		// Token: 0x040024E2 RID: 9442
		Notify = 16,
		/// <summary>Reserved for system use.</summary>
		// Token: 0x040024E3 RID: 9443
		CreateLink = 32,
		/// <summary>The right to delete a registry key.</summary>
		// Token: 0x040024E4 RID: 9444
		Delete = 65536,
		/// <summary>The right to open and copy the access rules and audit rules for a registry key.</summary>
		// Token: 0x040024E5 RID: 9445
		ReadPermissions = 131072,
		/// <summary>The right to create, delete, and set the name/value pairs in a registry key, to create or delete subkeys, to request notification of changes, to enumerate its subkeys, and to read its access rules and audit rules.</summary>
		// Token: 0x040024E6 RID: 9446
		WriteKey = 131078,
		/// <summary>The right to query the name/value pairs in a registry key, to request notification of changes, to enumerate its subkeys, and to read its access rules and audit rules.</summary>
		// Token: 0x040024E7 RID: 9447
		ReadKey = 131097,
		/// <summary>Same as <see cref="F:System.Security.AccessControl.RegistryRights.ReadKey" />.</summary>
		// Token: 0x040024E8 RID: 9448
		ExecuteKey = 131097,
		/// <summary>The right to change the access rules and audit rules associated with a registry key.</summary>
		// Token: 0x040024E9 RID: 9449
		ChangePermissions = 262144,
		/// <summary>The right to change the owner of a registry key.</summary>
		// Token: 0x040024EA RID: 9450
		TakeOwnership = 524288,
		/// <summary>The right to exert full control over a registry key, and to modify its access rules and audit rules.</summary>
		// Token: 0x040024EB RID: 9451
		FullControl = 983103
	}
}
