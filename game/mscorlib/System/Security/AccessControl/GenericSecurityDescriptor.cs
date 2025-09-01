using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;

namespace System.Security.AccessControl
{
	/// <summary>Represents a security descriptor. A security descriptor includes an owner, a primary group, a Discretionary Access Control List (DACL), and a System Access Control List (SACL).</summary>
	// Token: 0x0200052E RID: 1326
	public abstract class GenericSecurityDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> class.</summary>
		// Token: 0x06003487 RID: 13447 RVA: 0x0000259F File Offset: 0x0000079F
		protected GenericSecurityDescriptor()
		{
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.GenericSecurityDescriptor.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x000BF538 File Offset: 0x000BD738
		public int BinaryLength
		{
			get
			{
				int num = 20;
				if (this.Owner != null)
				{
					num += this.Owner.BinaryLength;
				}
				if (this.Group != null)
				{
					num += this.Group.BinaryLength;
				}
				if (this.DaclPresent && !this.DaclIsUnmodifiedAefa)
				{
					num += this.InternalDacl.BinaryLength;
				}
				if (this.SaclPresent)
				{
					num += this.InternalSacl.BinaryLength;
				}
				return num;
			}
		}

		/// <summary>Gets values that specify behavior of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>One or more values of the <see cref="T:System.Security.AccessControl.ControlFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06003489 RID: 13449
		public abstract ControlFlags ControlFlags { get; }

		/// <summary>Gets or sets the primary group for this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>The primary group for this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600348A RID: 13450
		// (set) Token: 0x0600348B RID: 13451
		public abstract SecurityIdentifier Group { get; set; }

		/// <summary>Gets or sets the owner of the object associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>The owner of the object associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600348C RID: 13452
		// (set) Token: 0x0600348D RID: 13453
		public abstract SecurityIdentifier Owner { get; set; }

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" />.</returns>
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x000040F7 File Offset: 0x000022F7
		public static byte Revision
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x0000AF5E File Offset: 0x0000915E
		internal virtual GenericAcl InternalDacl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x0000AF5E File Offset: 0x0000915E
		internal virtual GenericAcl InternalSacl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal virtual byte InternalReservedField
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Returns an array of byte values that represents the information contained in this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06003492 RID: 13458 RVA: 0x000BF5B8 File Offset: 0x000BD7B8
		public void GetBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			int binaryLength = this.BinaryLength;
			if (offset < 0 || offset > binaryForm.Length - binaryLength)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			ControlFlags controlFlags = this.ControlFlags;
			if (this.DaclIsUnmodifiedAefa)
			{
				controlFlags &= ~ControlFlags.DiscretionaryAclPresent;
			}
			binaryForm[offset] = GenericSecurityDescriptor.Revision;
			binaryForm[offset + 1] = this.InternalReservedField;
			this.WriteUShort((ushort)controlFlags, binaryForm, offset + 2);
			int num = 20;
			if (this.Owner != null)
			{
				this.WriteInt(num, binaryForm, offset + 4);
				this.Owner.GetBinaryForm(binaryForm, offset + num);
				num += this.Owner.BinaryLength;
			}
			else
			{
				this.WriteInt(0, binaryForm, offset + 4);
			}
			if (this.Group != null)
			{
				this.WriteInt(num, binaryForm, offset + 8);
				this.Group.GetBinaryForm(binaryForm, offset + num);
				num += this.Group.BinaryLength;
			}
			else
			{
				this.WriteInt(0, binaryForm, offset + 8);
			}
			GenericAcl internalSacl = this.InternalSacl;
			if (this.SaclPresent)
			{
				this.WriteInt(num, binaryForm, offset + 12);
				internalSacl.GetBinaryForm(binaryForm, offset + num);
				num += this.InternalSacl.BinaryLength;
			}
			else
			{
				this.WriteInt(0, binaryForm, offset + 12);
			}
			GenericAcl internalDacl = this.InternalDacl;
			if (this.DaclPresent && !this.DaclIsUnmodifiedAefa)
			{
				this.WriteInt(num, binaryForm, offset + 16);
				internalDacl.GetBinaryForm(binaryForm, offset + num);
				num += this.InternalDacl.BinaryLength;
				return;
			}
			this.WriteInt(0, binaryForm, offset + 16);
		}

		/// <summary>Returns the Security Descriptor Definition Language (SDDL) representation of the specified sections of the security descriptor that this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object represents.</summary>
		/// <param name="includeSections">Specifies which sections (access rules, audit rules, primary group, owner) of the security descriptor to get.</param>
		/// <returns>The SDDL representation of the specified sections of the security descriptor associated with this <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object.</returns>
		// Token: 0x06003493 RID: 13459 RVA: 0x000BF738 File Offset: 0x000BD938
		public string GetSddlForm(AccessControlSections includeSections)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None && this.Owner != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "O:{0}", this.Owner.GetSddlForm());
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None && this.Group != null)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "G:{0}", this.Group.GetSddlForm());
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None && this.DaclPresent && !this.DaclIsUnmodifiedAefa)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "D:{0}", this.InternalDacl.GetSddlForm(this.ControlFlags, true));
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None && this.SaclPresent)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "S:{0}", this.InternalSacl.GetSddlForm(this.ControlFlags, false));
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns a boolean value that specifies whether the security descriptor associated with this  <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object can be converted to the Security Descriptor Definition Language (SDDL) format.</summary>
		/// <returns>
		///   <see langword="true" /> if the security descriptor associated with this  <see cref="T:System.Security.AccessControl.GenericSecurityDescriptor" /> object can be converted to the Security Descriptor Definition Language (SDDL) format; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003494 RID: 13460 RVA: 0x000040F7 File Offset: 0x000022F7
		public static bool IsSddlConversionSupported()
		{
			return true;
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal virtual bool DaclIsUnmodifiedAefa
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06003496 RID: 13462 RVA: 0x000BF817 File Offset: 0x000BDA17
		private bool DaclPresent
		{
			get
			{
				return this.InternalDacl != null && (this.ControlFlags & ControlFlags.DiscretionaryAclPresent) > ControlFlags.None;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000BF82E File Offset: 0x000BDA2E
		private bool SaclPresent
		{
			get
			{
				return this.InternalSacl != null && (this.ControlFlags & ControlFlags.SystemAclPresent) > ControlFlags.None;
			}
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000BF846 File Offset: 0x000BDA46
		private void WriteUShort(ushort val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000BF856 File Offset: 0x000BDA56
		private void WriteInt(int val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
			buffer[offset + 2] = (byte)(val >> 16);
			buffer[offset + 3] = (byte)(val >> 24);
		}
	}
}
