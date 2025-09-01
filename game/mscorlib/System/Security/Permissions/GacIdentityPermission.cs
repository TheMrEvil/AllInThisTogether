using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Defines the identity permission for files originating in the global assembly cache. This class cannot be inherited.</summary>
	// Token: 0x0200043B RID: 1083
	[ComVisible(true)]
	[Serializable]
	public sealed class GacIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.GacIdentityPermission" /> class.</summary>
		// Token: 0x06002C01 RID: 11265 RVA: 0x0009E0CA File Offset: 0x0009C2CA
		public GacIdentityPermission()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.GacIdentityPermission" /> class with fully restricted <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value.</exception>
		// Token: 0x06002C02 RID: 11266 RVA: 0x0009EC34 File Offset: 0x0009CE34
		public GacIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002C03 RID: 11267 RVA: 0x0009A06E File Offset: 0x0009826E
		public override IPermission Copy()
		{
			return new GacIdentityPermission();
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. The new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002C04 RID: 11268 RVA: 0x0009EC44 File Offset: 0x0009CE44
		public override IPermission Intersect(IPermission target)
		{
			if (this.Cast(target) == null)
			{
				return null;
			}
			return this.Copy();
		}

		/// <summary>Indicates whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission object to test for the subset relationship. The permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002C05 RID: 11269 RVA: 0x0009EC57 File Offset: 0x0009CE57
		public override bool IsSubsetOf(IPermission target)
		{
			return this.Cast(target) != null;
		}

		/// <summary>Creates and returns a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002C06 RID: 11270 RVA: 0x0009EC63 File Offset: 0x0009CE63
		public override IPermission Union(IPermission target)
		{
			this.Cast(target);
			return this.Copy();
		}

		/// <summary>Creates a permission from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding to use to create the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.  
		/// -or-  
		/// The version number of <paramref name="securityElement" /> is not valid.</exception>
		// Token: 0x06002C07 RID: 11271 RVA: 0x0009EC73 File Offset: 0x0009CE73
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that represents the XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002C08 RID: 11272 RVA: 0x0009EC83 File Offset: 0x0009CE83
		public override SecurityElement ToXml()
		{
			return base.Element(1);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x0006AFCE File Offset: 0x000691CE
		int IBuiltInPermission.GetTokenIndex()
		{
			return 15;
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x0009EC8C File Offset: 0x0009CE8C
		private GacIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			GacIdentityPermission gacIdentityPermission = target as GacIdentityPermission;
			if (gacIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(GacIdentityPermission));
			}
			return gacIdentityPermission;
		}

		// Token: 0x0400202C RID: 8236
		private const int version = 1;
	}
}
