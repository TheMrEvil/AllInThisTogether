using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x02000546 RID: 1350
	public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class from the specified Security Descriptor Definition Language (SDDL) string.</summary>
		/// <param name="sddlForm">The SDDL string from which to create the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		// Token: 0x06003575 RID: 13685 RVA: 0x000C156D File Offset: 0x000BF76D
		public RawSecurityDescriptor(string sddlForm)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			this.ParseSddl(sddlForm.Replace(" ", ""));
			this.control_flags |= ControlFlags.SelfRelative;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class from the specified array of byte values.</summary>
		/// <param name="binaryForm">The array of byte values from which to create the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="offset">The offset in the  <paramref name="binaryForm" /> array at which to begin copying.</param>
		// Token: 0x06003576 RID: 13686 RVA: 0x000C15AC File Offset: 0x000BF7AC
		public RawSecurityDescriptor(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 20)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			if (binaryForm[offset] != 1)
			{
				throw new ArgumentException("Unrecognized Security Descriptor revision.", "binaryForm");
			}
			this.resourcemgr_control = binaryForm[offset + 1];
			this.control_flags = (ControlFlags)this.ReadUShort(binaryForm, offset + 2);
			int num = this.ReadInt(binaryForm, offset + 4);
			int num2 = this.ReadInt(binaryForm, offset + 8);
			int num3 = this.ReadInt(binaryForm, offset + 12);
			int num4 = this.ReadInt(binaryForm, offset + 16);
			if (num != 0)
			{
				this.owner_sid = new SecurityIdentifier(binaryForm, num);
			}
			if (num2 != 0)
			{
				this.group_sid = new SecurityIdentifier(binaryForm, num2);
			}
			if (num3 != 0)
			{
				this.system_acl = new RawAcl(binaryForm, num3);
			}
			if (num4 != 0)
			{
				this.discretionary_acl = new RawAcl(binaryForm, num4);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> class with the specified values.</summary>
		/// <param name="flags">Flags that specify behavior of the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="owner">The owner for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="group">The primary group for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="systemAcl">The System Access Control List (SACL) for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		/// <param name="discretionaryAcl">The Discretionary Access Control List (DACL) for the new <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</param>
		// Token: 0x06003577 RID: 13687 RVA: 0x000C168F File Offset: 0x000BF88F
		public RawSecurityDescriptor(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
		{
			this.control_flags = flags;
			this.owner_sid = owner;
			this.group_sid = group;
			this.system_acl = systemAcl;
			this.discretionary_acl = discretionaryAcl;
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x000C16BC File Offset: 0x000BF8BC
		public override ControlFlags ControlFlags
		{
			get
			{
				return this.control_flags;
			}
		}

		/// <summary>Gets or sets the Discretionary Access Control List (DACL) for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object. The DACL contains access rules.</summary>
		/// <returns>The DACL for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x000C16C4 File Offset: 0x000BF8C4
		// (set) Token: 0x0600357A RID: 13690 RVA: 0x000C16CC File Offset: 0x000BF8CC
		public RawAcl DiscretionaryAcl
		{
			get
			{
				return this.discretionary_acl;
			}
			set
			{
				this.discretionary_acl = value;
			}
		}

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600357B RID: 13691 RVA: 0x000C16D5 File Offset: 0x000BF8D5
		// (set) Token: 0x0600357C RID: 13692 RVA: 0x000C16DD File Offset: 0x000BF8DD
		public override SecurityIdentifier Group
		{
			get
			{
				return this.group_sid;
			}
			set
			{
				this.group_sid = value;
			}
		}

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000C16E6 File Offset: 0x000BF8E6
		// (set) Token: 0x0600357E RID: 13694 RVA: 0x000C16EE File Offset: 0x000BF8EE
		public override SecurityIdentifier Owner
		{
			get
			{
				return this.owner_sid;
			}
			set
			{
				this.owner_sid = value;
			}
		}

		/// <summary>Gets or sets a byte value that represents the resource manager control bits associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</summary>
		/// <returns>A byte value that represents the resource manager control bits associated with this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x000C16F7 File Offset: 0x000BF8F7
		// (set) Token: 0x06003580 RID: 13696 RVA: 0x000C16FF File Offset: 0x000BF8FF
		public byte ResourceManagerControl
		{
			get
			{
				return this.resourcemgr_control;
			}
			set
			{
				this.resourcemgr_control = value;
			}
		}

		/// <summary>Gets or sets the System Access Control List (SACL) for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object. The SACL contains audit rules.</summary>
		/// <returns>The SACL for this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object.</returns>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x000C1708 File Offset: 0x000BF908
		// (set) Token: 0x06003582 RID: 13698 RVA: 0x000C1710 File Offset: 0x000BF910
		public RawAcl SystemAcl
		{
			get
			{
				return this.system_acl;
			}
			set
			{
				this.system_acl = value;
			}
		}

		/// <summary>Sets the <see cref="P:System.Security.AccessControl.RawSecurityDescriptor.ControlFlags" /> property of this <see cref="T:System.Security.AccessControl.RawSecurityDescriptor" /> object to the specified value.</summary>
		/// <param name="flags">One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</param>
		// Token: 0x06003583 RID: 13699 RVA: 0x000C1719 File Offset: 0x000BF919
		public void SetFlags(ControlFlags flags)
		{
			this.control_flags = (flags | ControlFlags.SelfRelative);
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000C1728 File Offset: 0x000BF928
		internal override GenericAcl InternalDacl
		{
			get
			{
				return this.DiscretionaryAcl;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000C1730 File Offset: 0x000BF930
		internal override GenericAcl InternalSacl
		{
			get
			{
				return this.SystemAcl;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x000C1738 File Offset: 0x000BF938
		internal override byte InternalReservedField
		{
			get
			{
				return this.ResourceManagerControl;
			}
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000C1740 File Offset: 0x000BF940
		private void ParseSddl(string sddlForm)
		{
			ControlFlags controlFlags = ControlFlags.None;
			int i = 0;
			while (i < sddlForm.Length - 2)
			{
				string a = sddlForm.Substring(i, 2);
				if (!(a == "O:"))
				{
					if (!(a == "G:"))
					{
						if (!(a == "D:"))
						{
							if (!(a == "S:"))
							{
								throw new ArgumentException("Invalid SDDL.", "sddlForm");
							}
							i += 2;
							this.SystemAcl = RawAcl.ParseSddlForm(sddlForm, false, ref controlFlags, ref i);
							controlFlags |= ControlFlags.SystemAclPresent;
						}
						else
						{
							i += 2;
							this.DiscretionaryAcl = RawAcl.ParseSddlForm(sddlForm, true, ref controlFlags, ref i);
							controlFlags |= ControlFlags.DiscretionaryAclPresent;
						}
					}
					else
					{
						i += 2;
						this.Group = SecurityIdentifier.ParseSddlForm(sddlForm, ref i);
					}
				}
				else
				{
					i += 2;
					this.Owner = SecurityIdentifier.ParseSddlForm(sddlForm, ref i);
				}
			}
			if (i != sddlForm.Length)
			{
				throw new ArgumentException("Invalid SDDL.", "sddlForm");
			}
			this.SetFlags(controlFlags);
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000C155F File Offset: 0x000BF75F
		private ushort ReadUShort(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | (int)buffer[offset + 1] << 8);
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000C1832 File Offset: 0x000BFA32
		private int ReadInt(byte[] buffer, int offset)
		{
			return (int)buffer[offset] | (int)buffer[offset + 1] << 8 | (int)buffer[offset + 2] << 16 | (int)buffer[offset + 3] << 24;
		}

		// Token: 0x040024D7 RID: 9431
		private ControlFlags control_flags;

		// Token: 0x040024D8 RID: 9432
		private SecurityIdentifier owner_sid;

		// Token: 0x040024D9 RID: 9433
		private SecurityIdentifier group_sid;

		// Token: 0x040024DA RID: 9434
		private RawAcl system_acl;

		// Token: 0x040024DB RID: 9435
		private RawAcl discretionary_acl;

		// Token: 0x040024DC RID: 9436
		private byte resourcemgr_control;
	}
}
