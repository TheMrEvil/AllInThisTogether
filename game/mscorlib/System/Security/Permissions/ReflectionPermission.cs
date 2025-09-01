using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Controls access to non-public types and members through the <see cref="N:System.Reflection" /> APIs. Controls some features of the <see cref="N:System.Reflection.Emit" /> APIs.</summary>
	// Token: 0x02000451 RID: 1105
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002CCE RID: 11470 RVA: 0x000A0C22 File Offset: 0x0009EE22
		public ReflectionPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this.flags = ReflectionPermissionFlag.AllFlags;
				return;
			}
			this.flags = ReflectionPermissionFlag.NoFlags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermission" /> class with the specified access.</summary>
		/// <param name="flag">One of the <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" />.</exception>
		// Token: 0x06002CCF RID: 11471 RVA: 0x000A0C43 File Offset: 0x0009EE43
		public ReflectionPermission(ReflectionPermissionFlag flag)
		{
			this.Flags = flag;
		}

		/// <summary>Gets or sets the type of reflection allowed for the current permission.</summary>
		/// <returns>The set flags for the current permission.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> for the valid values.</exception>
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x000A0C52 File Offset: 0x0009EE52
		// (set) Token: 0x06002CD1 RID: 11473 RVA: 0x000A0C5A File Offset: 0x0009EE5A
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				if ((value & (ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess)) != value)
				{
					throw new ArgumentException(string.Format(Locale.GetText("Invalid flags {0}"), value), "ReflectionPermissionFlag");
				}
				this.flags = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002CD2 RID: 11474 RVA: 0x000A0C8A File Offset: 0x0009EE8A
		public override IPermission Copy()
		{
			return new ReflectionPermission(this.flags);
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not valid.</exception>
		// Token: 0x06002CD3 RID: 11475 RVA: 0x000A0C98 File Offset: 0x0009EE98
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this.flags = ReflectionPermissionFlag.AllFlags;
				return;
			}
			this.flags = ReflectionPermissionFlag.NoFlags;
			string text = esd.Attributes["Flags"] as string;
			if (text.IndexOf("MemberAccess") >= 0)
			{
				this.flags |= ReflectionPermissionFlag.MemberAccess;
			}
			if (text.IndexOf("ReflectionEmit") >= 0)
			{
				this.flags |= ReflectionPermissionFlag.ReflectionEmit;
			}
			if (text.IndexOf("TypeInformation") >= 0)
			{
				this.flags |= ReflectionPermissionFlag.TypeInformation;
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002CD4 RID: 11476 RVA: 0x000A0D34 File Offset: 0x0009EF34
		public override IPermission Intersect(IPermission target)
		{
			ReflectionPermission reflectionPermission = this.Cast(target);
			if (reflectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				if (reflectionPermission.Flags == ReflectionPermissionFlag.NoFlags)
				{
					return null;
				}
				return reflectionPermission.Copy();
			}
			else if (reflectionPermission.IsUnrestricted())
			{
				if (this.flags == ReflectionPermissionFlag.NoFlags)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				ReflectionPermission reflectionPermission2 = (ReflectionPermission)reflectionPermission.Copy();
				reflectionPermission2.Flags &= this.flags;
				if (reflectionPermission2.Flags != ReflectionPermissionFlag.NoFlags)
				{
					return reflectionPermission2;
				}
				return null;
			}
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002CD5 RID: 11477 RVA: 0x000A0DAC File Offset: 0x0009EFAC
		public override bool IsSubsetOf(IPermission target)
		{
			ReflectionPermission reflectionPermission = this.Cast(target);
			if (reflectionPermission == null)
			{
				return this.flags == ReflectionPermissionFlag.NoFlags;
			}
			if (this.IsUnrestricted())
			{
				return reflectionPermission.IsUnrestricted();
			}
			return reflectionPermission.IsUnrestricted() || (this.flags & reflectionPermission.Flags) == this.flags;
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>
		///   <see langword="true" /> if the current permission is unrestricted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002CD6 RID: 11478 RVA: 0x000A0DFC File Offset: 0x0009EFFC
		public bool IsUnrestricted()
		{
			return this.flags == ReflectionPermissionFlag.AllFlags;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002CD7 RID: 11479 RVA: 0x000A0E08 File Offset: 0x0009F008
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.flags == ReflectionPermissionFlag.NoFlags)
			{
				securityElement.AddAttribute("Flags", "NoFlags");
			}
			else if ((this.flags & ReflectionPermissionFlag.AllFlags) == ReflectionPermissionFlag.AllFlags)
			{
				securityElement.AddAttribute("Flags", "AllFlags");
			}
			else
			{
				string text = "";
				if ((this.flags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess)
				{
					text = "MemberAccess";
				}
				if ((this.flags & ReflectionPermissionFlag.ReflectionEmit) == ReflectionPermissionFlag.ReflectionEmit)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "ReflectionEmit";
				}
				if ((this.flags & ReflectionPermissionFlag.TypeInformation) == ReflectionPermissionFlag.TypeInformation)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text += "TypeInformation";
				}
				securityElement.AddAttribute("Flags", text);
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002CD8 RID: 11480 RVA: 0x000A0EF0 File Offset: 0x0009F0F0
		public override IPermission Union(IPermission other)
		{
			ReflectionPermission reflectionPermission = this.Cast(other);
			if (other == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || reflectionPermission.IsUnrestricted())
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			ReflectionPermission reflectionPermission2 = (ReflectionPermission)reflectionPermission.Copy();
			reflectionPermission2.Flags |= this.flags;
			return reflectionPermission2;
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x00022807 File Offset: 0x00020A07
		int IBuiltInPermission.GetTokenIndex()
		{
			return 4;
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000A0F44 File Offset: 0x0009F144
		private ReflectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			ReflectionPermission reflectionPermission = target as ReflectionPermission;
			if (reflectionPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(ReflectionPermission));
			}
			return reflectionPermission;
		}

		// Token: 0x04002079 RID: 8313
		private const int version = 1;

		// Token: 0x0400207A RID: 8314
		private ReflectionPermissionFlag flags;
	}
}
