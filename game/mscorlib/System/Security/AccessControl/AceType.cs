using System;

namespace System.Security.AccessControl
{
	/// <summary>Defines the available access control entry (ACE) types.</summary>
	// Token: 0x02000508 RID: 1288
	public enum AceType : byte
	{
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x04002422 RID: 9250
		AccessAllowed,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x04002423 RID: 9251
		AccessDenied,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		// Token: 0x04002424 RID: 9252
		SystemAudit,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002425 RID: 9253
		SystemAlarm,
		/// <summary>Defined but never used. Included here for completeness.</summary>
		// Token: 0x04002426 RID: 9254
		AccessAllowedCompound,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x04002427 RID: 9255
		AccessAllowedObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x04002428 RID: 9256
		AccessDeniedObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects.</summary>
		// Token: 0x04002429 RID: 9257
		SystemAuditObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x0400242A RID: 9258
		SystemAlarmObject,
		/// <summary>Allows access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type may contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x0400242B RID: 9259
		AccessAllowedCallback,
		/// <summary>Denies access to an object for a specific trustee identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x0400242C RID: 9260
		AccessDeniedCallback,
		/// <summary>Allows access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type may contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x0400242D RID: 9261
		AccessAllowedCallbackObject,
		/// <summary>Denies access to an object, property set, or property. The ACE contains a set of access rights, a GUID that identifies the type of object, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee to whom the system will grant access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x0400242E RID: 9262
		AccessDeniedCallbackObject,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object. The trustee is identified by an <see cref="T:System.Security.Principal.IdentityReference" /> object. This ACE type can contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x0400242F RID: 9263
		SystemAuditCallback,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002430 RID: 9264
		SystemAlarmCallback,
		/// <summary>Causes an audit message to be logged when a specified trustee attempts to gain access to an object or subobjects such as property sets or properties. The ACE contains a set of access rights, a GUID that identifies the type of object or subobject, and an <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee for whom the system will audit access. The ACE also contains a GUID and a set of flags that control inheritance of the ACE by child objects. This ACE type can contain optional callback data. The callback data is a resource manager-specific BLOB that is not interpreted.</summary>
		// Token: 0x04002431 RID: 9265
		SystemAuditCallbackObject,
		/// <summary>Reserved for future use.</summary>
		// Token: 0x04002432 RID: 9266
		SystemAlarmCallbackObject,
		/// <summary>Tracks the maximum defined ACE type in the enumeration.</summary>
		// Token: 0x04002433 RID: 9267
		MaxDefinedAceType = 16
	}
}
