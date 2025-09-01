using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access key containers. This class cannot be inherited.</summary>
	// Token: 0x02000445 RID: 1093
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with either restricted or unrestricted permission.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid <see cref="T:System.Security.Permissions.PermissionState" /> value.</exception>
		// Token: 0x06002C4A RID: 11338 RVA: 0x0009F5DA File Offset: 0x0009D7DA
		public KeyContainerPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with the specified access.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</exception>
		// Token: 0x06002C4B RID: 11339 RVA: 0x0009F5F7 File Offset: 0x0009D7F7
		public KeyContainerPermission(KeyContainerPermissionFlags flags)
		{
			this.SetFlags(flags);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.KeyContainerPermission" /> class with the specified global access and specific key container access rights.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</param>
		/// <param name="accessList">An array of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects identifying specific key container access rights.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flags" /> is not a valid combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="accessList" /> is <see langword="null" />.</exception>
		// Token: 0x06002C4C RID: 11340 RVA: 0x0009F608 File Offset: 0x0009D808
		public KeyContainerPermission(KeyContainerPermissionFlags flags, KeyContainerPermissionAccessEntry[] accessList)
		{
			this.SetFlags(flags);
			if (accessList != null)
			{
				this._accessEntries = new KeyContainerPermissionAccessEntryCollection();
				foreach (KeyContainerPermissionAccessEntry accessEntry in accessList)
				{
					this._accessEntries.Add(accessEntry);
				}
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects associated with the current permission.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" /> containing the <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> objects for this <see cref="T:System.Security.Permissions.KeyContainerPermission" />.</returns>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002C4D RID: 11341 RVA: 0x0009F651 File Offset: 0x0009D851
		public KeyContainerPermissionAccessEntryCollection AccessEntries
		{
			get
			{
				return this._accessEntries;
			}
		}

		/// <summary>Gets the key container permission flags that apply to all key containers associated with the permission.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Security.Permissions.KeyContainerPermissionFlags" /> values.</returns>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x0009F659 File Offset: 0x0009D859
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this._flags;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002C4F RID: 11343 RVA: 0x0009F664 File Offset: 0x0009D864
		public override IPermission Copy()
		{
			if (this._accessEntries.Count == 0)
			{
				return new KeyContainerPermission(this._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[this._accessEntries.Count];
			this._accessEntries.CopyTo(array, 0);
			return new KeyContainerPermission(this._flags, array);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">A <see cref="T:System.Security.SecurityElement" /> that contains the XML encoding used to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="securityElement" /> is not a valid permission element.  
		/// -or-  
		/// The version number of <paramref name="securityElement" /> is not supported.</exception>
		// Token: 0x06002C50 RID: 11344 RVA: 0x0009F6B4 File Offset: 0x0009D8B4
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(securityElement))
			{
				this._flags = KeyContainerPermissionFlags.AllFlags;
				return;
			}
			this._flags = (KeyContainerPermissionFlags)Enum.Parse(typeof(KeyContainerPermissionFlags), securityElement.Attribute("Flags"));
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		// Token: 0x06002C51 RID: 11345 RVA: 0x0000AF5E File Offset: 0x0000915E
		[MonoTODO("(2.0)")]
		public override IPermission Intersect(IPermission target)
		{
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission to test for the subset relationship. This permission must be the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		// Token: 0x06002C52 RID: 11346 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("(2.0)")]
		public override bool IsSubsetOf(IPermission target)
		{
			return false;
		}

		/// <summary>Determines whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002C53 RID: 11347 RVA: 0x0009F708 File Offset: 0x0009D908
		public bool IsUnrestricted()
		{
			return this._flags == KeyContainerPermissionFlags.AllFlags;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> that contains an XML encoding of the permission, including state information.</returns>
		// Token: 0x06002C54 RID: 11348 RVA: 0x0009F718 File Offset: 0x0009D918
		[MonoTODO("(2.0) missing support for AccessEntries")]
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not <see langword="null" /> and does not specify a permission of the same type as the current permission.</exception>
		// Token: 0x06002C55 RID: 11349 RVA: 0x0009F748 File Offset: 0x0009D948
		public override IPermission Union(IPermission target)
		{
			KeyContainerPermission keyContainerPermission = this.Cast(target);
			if (keyContainerPermission == null)
			{
				return this.Copy();
			}
			KeyContainerPermissionAccessEntryCollection keyContainerPermissionAccessEntryCollection = new KeyContainerPermissionAccessEntryCollection();
			foreach (KeyContainerPermissionAccessEntry accessEntry in this._accessEntries)
			{
				keyContainerPermissionAccessEntryCollection.Add(accessEntry);
			}
			foreach (KeyContainerPermissionAccessEntry accessEntry2 in keyContainerPermission._accessEntries)
			{
				if (this._accessEntries.IndexOf(accessEntry2) == -1)
				{
					keyContainerPermissionAccessEntryCollection.Add(accessEntry2);
				}
			}
			if (keyContainerPermissionAccessEntryCollection.Count == 0)
			{
				return new KeyContainerPermission(this._flags | keyContainerPermission._flags);
			}
			KeyContainerPermissionAccessEntry[] array = new KeyContainerPermissionAccessEntry[keyContainerPermissionAccessEntryCollection.Count];
			keyContainerPermissionAccessEntryCollection.CopyTo(array, 0);
			return new KeyContainerPermission(this._flags | keyContainerPermission._flags, array);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x00026EE1 File Offset: 0x000250E1
		int IBuiltInPermission.GetTokenIndex()
		{
			return 16;
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0009F80D File Offset: 0x0009DA0D
		private void SetFlags(KeyContainerPermissionFlags flags)
		{
			if ((flags & KeyContainerPermissionFlags.AllFlags) == KeyContainerPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid enum {0}"), flags), "KeyContainerPermissionFlags");
			}
			this._flags = flags;
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x0009F83F File Offset: 0x0009DA3F
		private KeyContainerPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			KeyContainerPermission keyContainerPermission = target as KeyContainerPermission;
			if (keyContainerPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(KeyContainerPermission));
			}
			return keyContainerPermission;
		}

		// Token: 0x0400204A RID: 8266
		private KeyContainerPermissionAccessEntryCollection _accessEntries;

		// Token: 0x0400204B RID: 8267
		private KeyContainerPermissionFlags _flags;

		// Token: 0x0400204C RID: 8268
		private const int version = 1;
	}
}
