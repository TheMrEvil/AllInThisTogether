using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Determines access to securable objects. The derived classes <see cref="T:System.Security.AccessControl.AccessRule" /> and <see cref="T:System.Security.AccessControl.AuditRule" /> offer specializations for access and audit functionality.</summary>
	// Token: 0x0200050C RID: 1292
	public abstract class AuthorizationRule
	{
		// Token: 0x06003351 RID: 13137 RVA: 0x0000259F File Offset: 0x0000079F
		internal AuthorizationRule()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class by using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies. This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> to inherit this rule from a parent container.</param>
		/// <param name="inheritanceFlags">The inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is zero, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x06003352 RID: 13138 RVA: 0x000BC964 File Offset: 0x000BAB64
		protected internal AuthorizationRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			if (null == identity)
			{
				throw new ArgumentNullException("identity");
			}
			if (!(identity is SecurityIdentifier) && !(identity is NTAccount))
			{
				throw new ArgumentException("identity");
			}
			if (accessMask == 0)
			{
				throw new ArgumentException("accessMask");
			}
			if ((inheritanceFlags & ~(InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit)) != InheritanceFlags.None)
			{
				throw new ArgumentOutOfRangeException();
			}
			if ((propagationFlags & ~(PropagationFlags.NoPropagateInherit | PropagationFlags.InheritOnly)) != PropagationFlags.None)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.identity = identity;
			this.accessMask = accessMask;
			this.isInherited = isInherited;
			this.inheritanceFlags = inheritanceFlags;
			this.propagationFlags = propagationFlags;
		}

		/// <summary>Gets the <see cref="T:System.Security.Principal.IdentityReference" /> to which this rule applies.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IdentityReference" /> to which this rule applies.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06003353 RID: 13139 RVA: 0x000BC9F3 File Offset: 0x000BABF3
		public IdentityReference IdentityReference
		{
			get
			{
				return this.identity;
			}
		}

		/// <summary>Gets the value of flags that determine how this rule is inherited by child objects.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x000BC9FB File Offset: 0x000BABFB
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				return this.inheritanceFlags;
			}
		}

		/// <summary>Gets a value indicating whether this rule is explicitly set or is inherited from a parent container object.</summary>
		/// <returns>
		///   <see langword="true" /> if this rule is not explicitly set but is instead inherited from a parent container.</returns>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06003355 RID: 13141 RVA: 0x000BCA03 File Offset: 0x000BAC03
		public bool IsInherited
		{
			get
			{
				return this.isInherited;
			}
		}

		/// <summary>Gets the value of the propagation flags, which determine how inheritance of this rule is propagated to child objects. This property is significant only when the value of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> enumeration is not <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</summary>
		/// <returns>A bitwise combination of the enumeration values.</returns>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x000BCA0B File Offset: 0x000BAC0B
		public PropagationFlags PropagationFlags
		{
			get
			{
				return this.propagationFlags;
			}
		}

		/// <summary>Gets the access mask for this rule.</summary>
		/// <returns>The access mask for this rule.</returns>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06003357 RID: 13143 RVA: 0x000BCA13 File Offset: 0x000BAC13
		protected internal int AccessMask
		{
			get
			{
				return this.accessMask;
			}
		}

		// Token: 0x04002439 RID: 9273
		private IdentityReference identity;

		// Token: 0x0400243A RID: 9274
		private int accessMask;

		// Token: 0x0400243B RID: 9275
		private bool isInherited;

		// Token: 0x0400243C RID: 9276
		private InheritanceFlags inheritanceFlags;

		// Token: 0x0400243D RID: 9277
		private PropagationFlags propagationFlags;
	}
}
