using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a combination of a user's identity, an access mask, and an access control type (allow or deny). An AccessRule`1 object also contains information about the how the rule is inherited by child objects and how that inheritance is propagated.</summary>
	/// <typeparam name="T">The access rights type for the access rule.</typeparam>
	// Token: 0x02000504 RID: 1284
	public class AccessRule<T> : AccessRule where T : struct
	{
		/// <summary>Initializes a new instance of the AccessRule'1 class by using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.</param>
		/// <param name="rights">The rights of the access rule.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x0600333D RID: 13117 RVA: 0x000BC815 File Offset: 0x000BAA15
		public AccessRule(string identity, T rights, AccessControlType type) : this(new NTAccount(identity), rights, type)
		{
		}

		/// <summary>Initializes a new instance of the AccessRule'1 class by using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.</param>
		/// <param name="rights">The rights of the access rule.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x0600333E RID: 13118 RVA: 0x000BC825 File Offset: 0x000BAA25
		public AccessRule(IdentityReference identity, T rights, AccessControlType type) : this(identity, rights, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		/// <summary>Initializes a new instance of the AccessRule'1 class by using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.</param>
		/// <param name="rights">The rights of the access rule.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x0600333F RID: 13119 RVA: 0x000BC832 File Offset: 0x000BAA32
		public AccessRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(new NTAccount(identity), rights, inheritanceFlags, propagationFlags, type)
		{
		}

		/// <summary>Initializes a new instance of the AccessRule'1 class by using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.</param>
		/// <param name="rights">The rights of the access rule.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x06003340 RID: 13120 RVA: 0x000BC846 File Offset: 0x000BAA46
		public AccessRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(identity, (int)((object)rights), false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000BC6F9 File Offset: 0x000BA8F9
		internal AccessRule(IdentityReference identity, int rights, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, rights, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		/// <summary>Gets the rights of the current instance.</summary>
		/// <returns>The rights, cast as type &lt;T&gt;, of the current instance.</returns>
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000BC860 File Offset: 0x000BAA60
		public T Rights
		{
			get
			{
				return (T)((object)base.AccessMask);
			}
		}
	}
}
