using System;
using System.Globalization;

namespace System.Security.Permissions
{
	/// <summary>Defines partial-trust access to the <see cref="T:System.ComponentModel.TypeDescriptor" /> class.</summary>
	// Token: 0x02000294 RID: 660
	[Serializable]
	public sealed class TypeDescriptorPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermission" /> class.</summary>
		/// <param name="state">The <see cref="T:System.Security.Permissions.PermissionState" /> to request. Only <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> and <see cref="F:System.Security.Permissions.PermissionState.None" /> are valid.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not a valid permission state. Only <see cref="F:System.Security.Permissions.PermissionState.Unrestricted" /> and <see cref="F:System.Security.Permissions.PermissionState.None" /> are valid.</exception>
		// Token: 0x060014C8 RID: 5320 RVA: 0x0005453A File Offset: 0x0005273A
		public TypeDescriptorPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				return;
			}
			throw new ArgumentException(SR.GetString("Invalid permission state."));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.TypeDescriptorPermission" /> class with the specified permission flags.</summary>
		/// <param name="flag">The permission flags to request.</param>
		// Token: 0x060014C9 RID: 5321 RVA: 0x00054568 File Offset: 0x00052768
		public TypeDescriptorPermission(TypeDescriptorPermissionFlags flag)
		{
			this.VerifyAccess(flag);
			this.SetUnrestricted(false);
			this.m_flags = flag;
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00054585 File Offset: 0x00052785
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_flags = TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
				return;
			}
			this.Reset();
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x00054598 File Offset: 0x00052798
		private void Reset()
		{
			this.m_flags = TypeDescriptorPermissionFlags.NoFlags;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the type descriptor.</summary>
		/// <returns>The <see cref="T:System.Security.Permissions.TypeDescriptorPermissionFlags" /> for the type descriptor.</returns>
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x000545B1 File Offset: 0x000527B1
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x000545A1 File Offset: 0x000527A1
		public TypeDescriptorPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.VerifyAccess(value);
				this.m_flags = value;
			}
		}

		/// <summary>Gets a value that indicates whether the type descriptor may be called from partially trusted code.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Security.Permissions.TypeDescriptorPermission.Flags" /> property is set to <see cref="F:System.Security.Permissions.TypeDescriptorPermissionFlags.RestrictedRegistrationAccess" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014CE RID: 5326 RVA: 0x000545B9 File Offset: 0x000527B9
		public bool IsUnrestricted()
		{
			return this.m_flags == TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
		}

		/// <summary>When overridden in a derived class, creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		// Token: 0x060014CF RID: 5327 RVA: 0x000545C4 File Offset: 0x000527C4
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			IPermission result;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = this.m_flags | typeDescriptorPermission.m_flags;
				if (typeDescriptorPermissionFlags == TypeDescriptorPermissionFlags.NoFlags)
				{
					result = null;
				}
				else
				{
					result = new TypeDescriptorPermission(typeDescriptorPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Operation on type '{0}' attempted with target of incorrect type."), base.GetType().FullName));
			}
			return result;
		}

		/// <summary>When implemented by a derived class, determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		// Token: 0x060014D0 RID: 5328 RVA: 0x00054638 File Offset: 0x00052838
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == TypeDescriptorPermissionFlags.NoFlags;
			}
			bool result;
			try
			{
				TypeDescriptorPermission typeDescriptorPermission = (TypeDescriptorPermission)target;
				TypeDescriptorPermissionFlags flags = this.m_flags;
				TypeDescriptorPermissionFlags flags2 = typeDescriptorPermission.m_flags;
				result = ((flags & flags2) == flags);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Operation on type '{0}' attempted with target of incorrect type."), base.GetType().FullName));
			}
			return result;
		}

		/// <summary>When implemented by a derived class, creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		// Token: 0x060014D1 RID: 5329 RVA: 0x000546A8 File Offset: 0x000528A8
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			IPermission result;
			try
			{
				TypeDescriptorPermissionFlags typeDescriptorPermissionFlags = ((TypeDescriptorPermission)target).m_flags & this.m_flags;
				if (typeDescriptorPermissionFlags == TypeDescriptorPermissionFlags.NoFlags)
				{
					result = null;
				}
				else
				{
					result = new TypeDescriptorPermission(typeDescriptorPermissionFlags);
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Operation on type '{0}' attempted with target of incorrect type."), base.GetType().FullName));
			}
			return result;
		}

		/// <summary>When implemented by a derived class, creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x060014D2 RID: 5330 RVA: 0x00054718 File Offset: 0x00052918
		public override IPermission Copy()
		{
			return new TypeDescriptorPermission(this.m_flags);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00054725 File Offset: 0x00052925
		private void VerifyAccess(TypeDescriptorPermissionFlags type)
		{
			if ((type & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) != TypeDescriptorPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Illegal enum value: {0}."), (int)type));
			}
		}

		/// <summary>When overridden in a derived class, creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x060014D4 RID: 5332 RVA: 0x00054750 File Offset: 0x00052950
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().FullName + ", " + base.GetType().Module.Assembly.FullName.Replace('"', '\''));
			securityElement.AddAttribute("version", "1");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", this.m_flags.ToString());
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		/// <summary>When overridden in a derived class, reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object.</param>
		// Token: 0x060014D5 RID: 5333 RVA: 0x000547F0 File Offset: 0x000529F0
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			string text = securityElement.Attribute("class");
			if (text == null || text.IndexOf(base.GetType().FullName, StringComparison.Ordinal) == -1)
			{
				throw new ArgumentException(SR.GetString("The value of \"class\" attribute is invalid."), "securityElement");
			}
			string text2 = securityElement.Attribute("Unrestricted");
			if (text2 != null && string.Compare(text2, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_flags = TypeDescriptorPermissionFlags.RestrictedRegistrationAccess;
				return;
			}
			this.m_flags = TypeDescriptorPermissionFlags.NoFlags;
			string text3 = securityElement.Attribute("Flags");
			if (text3 != null)
			{
				TypeDescriptorPermissionFlags flags = (TypeDescriptorPermissionFlags)Enum.Parse(typeof(TypeDescriptorPermissionFlags), text3);
				TypeDescriptorPermission.VerifyFlags(flags);
				this.m_flags = flags;
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x000548A2 File Offset: 0x00052AA2
		internal static void VerifyFlags(TypeDescriptorPermissionFlags flags)
		{
			if ((flags & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) != TypeDescriptorPermissionFlags.NoFlags)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("Illegal enum value: {0}."), (int)flags));
			}
		}

		// Token: 0x04000BB8 RID: 3000
		private TypeDescriptorPermissionFlags m_flags;
	}
}
