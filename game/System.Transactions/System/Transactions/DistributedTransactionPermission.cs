using System;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Transactions
{
	/// <summary>The permission that is demanded by <see cref="N:System.Transactions" /> when management of a transaction is escalated to MSDTC. This class cannot be inherited.</summary>
	// Token: 0x02000030 RID: 48
	[Serializable]
	public sealed class DistributedTransactionPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.DistributedTransactionPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x060000E9 RID: 233 RVA: 0x000021C9 File Offset: 0x000003C9
		public DistributedTransactionPermission(PermissionState state)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x060000EA RID: 234 RVA: 0x00003476 File Offset: 0x00001676
		public override IPermission Copy()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding used to reconstruct the permission.</param>
		// Token: 0x060000EB RID: 235 RVA: 0x000021C9 File Offset: 0x000003C9
		public override void FromXml(SecurityElement securityElement)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		// Token: 0x060000EC RID: 236 RVA: 0x00003476 File Offset: 0x00001676
		public override IPermission Intersect(IPermission target)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a value that indicates whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Security.IPermission" /> is a subset of the specified <see cref="T:System.Security.IPermission" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000ED RID: 237 RVA: 0x00003480 File Offset: 0x00001680
		public override bool IsSubsetOf(IPermission target)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Returns a value that indicates whether unrestricted access to the resource that is protected by the current permission is allowed.</summary>
		/// <returns>
		///   <see langword="true" /> if unrestricted use of the resource protected by the permission is allowed; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000EE RID: 238 RVA: 0x0000349C File Offset: 0x0000169C
		public bool IsUnrestricted()
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding of the security object, including any state information.</returns>
		// Token: 0x060000EF RID: 239 RVA: 0x00003476 File Offset: 0x00001676
		public override SecurityElement ToXml()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
