using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a combination of a user's identity, an access mask, and audit conditions. An <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> object also contains information about the type of object to which the rule applies, the type of child object that can inherit the rule, how the rule is inherited by child objects, and how that inheritance is propagated.</summary>
	// Token: 0x02000540 RID: 1344
	public abstract class ObjectAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> class.</summary>
		/// <param name="identity">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">
		///   <see langword="true" /> if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="objectType">The type of object to which the rule applies.</param>
		/// <param name="inheritedObjectType">The type of child object that can inherit the rule.</param>
		/// <param name="auditFlags">The audit conditions.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />, or the <paramref name="type" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is 0, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x06003505 RID: 13573 RVA: 0x000C050A File Offset: 0x000BE70A
		protected ObjectAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AuditFlags auditFlags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, auditFlags)
		{
			this.object_type = objectType;
			this.inherited_object_type = inheritedObjectType;
		}

		/// <summary>Gets the type of child object that can inherit the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> object.</summary>
		/// <returns>The type of child object that can inherit the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> object.</returns>
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06003506 RID: 13574 RVA: 0x000C052B File Offset: 0x000BE72B
		public Guid InheritedObjectType
		{
			get
			{
				return this.inherited_object_type;
			}
		}

		/// <summary>
		///   <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> and <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> properties of the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> object contain valid values.</summary>
		/// <returns>
		///   <see cref="F:System.Security.AccessControl.ObjectAceFlags.ObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAuditRule.ObjectType" /> property contains a valid value. <see cref="F:System.Security.AccessControl.ObjectAceFlags.InheritedObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAuditRule.InheritedObjectType" /> property contains a valid value. These values can be combined with a logical OR.</returns>
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x000C0534 File Offset: 0x000BE734
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				ObjectAceFlags objectAceFlags = ObjectAceFlags.None;
				if (this.object_type != Guid.Empty)
				{
					objectAceFlags |= ObjectAceFlags.ObjectAceTypePresent;
				}
				if (this.inherited_object_type != Guid.Empty)
				{
					objectAceFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
				}
				return objectAceFlags;
			}
		}

		/// <summary>Gets the type of object to which the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> applies.</summary>
		/// <returns>The type of object to which the <see cref="T:System.Security.AccessControl.ObjectAuditRule" /> applies.</returns>
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x000C0570 File Offset: 0x000BE770
		public Guid ObjectType
		{
			get
			{
				return this.object_type;
			}
		}

		// Token: 0x040024CB RID: 9419
		private Guid inherited_object_type;

		// Token: 0x040024CC RID: 9420
		private Guid object_type;
	}
}
