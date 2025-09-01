using System;
using System.Globalization;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Controls access to Directory Services objects. This class represents an Access Control Entry (ACE) associated with a directory object.</summary>
	// Token: 0x0200053E RID: 1342
	public sealed class ObjectAce : QualifiedAce
	{
		/// <summary>Initiates a new instance of the <see cref="T:System.Security.AccessControl.ObjectAce" /> class.</summary>
		/// <param name="aceFlags">The inheritance, inheritance propagation, and auditing conditions for the new Access Control Entry (ACE).</param>
		/// <param name="qualifier">The use of the new ACE.</param>
		/// <param name="accessMask">The access mask for the ACE.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> associated with the new ACE.</param>
		/// <param name="flags">Whether the <paramref name="type" /> and <paramref name="inheritedType" /> parameters contain valid object GUIDs.</param>
		/// <param name="type">A GUID that identifies the object type to which the new ACE applies.</param>
		/// <param name="inheritedType">A GUID that identifies the object type that can inherit the new ACE.</param>
		/// <param name="isCallback">
		///   <see langword="true" /> if the new ACE is a callback type ACE.</param>
		/// <param name="opaque">Opaque data associated with the new ACE. This is allowed only for callback ACE types. The length of this array must not be greater than the return value of the <see cref="M:System.Security.AccessControl.ObjectAce.MaxOpaqueLength(System.Boolean)" /> method.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The qualifier parameter contains an invalid value or the length of the value of the opaque parameter is greater than the return value of the <see cref="M:System.Security.AccessControl.ObjectAce.MaxOpaqueLength(System.Boolean)" /> method.</exception>
		// Token: 0x060034F3 RID: 13555 RVA: 0x000C00B8 File Offset: 0x000BE2B8
		public ObjectAce(AceFlags aceFlags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, ObjectAceFlags flags, Guid type, Guid inheritedType, bool isCallback, byte[] opaque) : base(ObjectAce.ConvertType(qualifier, isCallback), aceFlags, opaque)
		{
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
			this.ObjectAceFlags = flags;
			this.ObjectAceType = type;
			this.InheritedObjectAceType = inheritedType;
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000C00F2 File Offset: 0x000BE2F2
		internal ObjectAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier sid, ObjectAceFlags objFlags, Guid objType, Guid inheritedType, byte[] opaque) : base(type, flags, opaque)
		{
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
			this.ObjectAceFlags = objFlags;
			this.ObjectAceType = objType;
			this.InheritedObjectAceType = inheritedType;
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000C0128 File Offset: 0x000BE328
		internal ObjectAce(byte[] binaryForm, int offset) : base(binaryForm, offset)
		{
			int num = (int)GenericAce.ReadUShort(binaryForm, offset + 2);
			int num2 = 12 + SecurityIdentifier.MinBinaryLength;
			if (offset > binaryForm.Length - num)
			{
				throw new ArgumentException("Invalid ACE - truncated", "binaryForm");
			}
			if (num < num2)
			{
				throw new ArgumentException("Invalid ACE", "binaryForm");
			}
			base.AccessMask = GenericAce.ReadInt(binaryForm, offset + 4);
			this.ObjectAceFlags = (ObjectAceFlags)GenericAce.ReadInt(binaryForm, offset + 8);
			if (this.ObjectAceTypePresent)
			{
				num2 += 16;
			}
			if (this.InheritedObjectAceTypePresent)
			{
				num2 += 16;
			}
			if (num < num2)
			{
				throw new ArgumentException("Invalid ACE", "binaryForm");
			}
			int num3 = 12;
			if (this.ObjectAceTypePresent)
			{
				this.ObjectAceType = this.ReadGuid(binaryForm, offset + num3);
				num3 += 16;
			}
			if (this.InheritedObjectAceTypePresent)
			{
				this.InheritedObjectAceType = this.ReadGuid(binaryForm, offset + num3);
				num3 += 16;
			}
			base.SecurityIdentifier = new SecurityIdentifier(binaryForm, offset + num3);
			num3 += base.SecurityIdentifier.BinaryLength;
			int num4 = num - num3;
			if (num4 > 0)
			{
				byte[] array = new byte[num4];
				Array.Copy(binaryForm, offset + num3, array, 0, num4);
				base.SetOpaque(array);
			}
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.ObjectAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.ObjectAce.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.ObjectAce" /> object.</returns>
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060034F6 RID: 13558 RVA: 0x000C0248 File Offset: 0x000BE448
		public override int BinaryLength
		{
			get
			{
				int num = 12 + base.SecurityIdentifier.BinaryLength + base.OpaqueLength;
				if (this.ObjectAceTypePresent)
				{
					num += 16;
				}
				if (this.InheritedObjectAceTypePresent)
				{
					num += 16;
				}
				return num;
			}
		}

		/// <summary>Gets or sets the GUID of the object type that can inherit the Access Control Entry (ACE) that this <see cref="T:System.Security.AccessControl.ObjectAce" /> object represents.</summary>
		/// <returns>The GUID of the object type that can inherit the Access Control Entry (ACE) that this <see cref="T:System.Security.AccessControl.ObjectAce" /> object represents.</returns>
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000C0286 File Offset: 0x000BE486
		// (set) Token: 0x060034F8 RID: 13560 RVA: 0x000C028E File Offset: 0x000BE48E
		public Guid InheritedObjectAceType
		{
			get
			{
				return this.inherited_object_type;
			}
			set
			{
				this.inherited_object_type = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000C0297 File Offset: 0x000BE497
		private bool InheritedObjectAceTypePresent
		{
			get
			{
				return (this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) > ObjectAceFlags.None;
			}
		}

		/// <summary>Gets or sets flags that specify whether the <see cref="P:System.Security.AccessControl.ObjectAce.ObjectAceType" /> and <see cref="P:System.Security.AccessControl.ObjectAce.InheritedObjectAceType" /> properties contain values that identify valid object types.</summary>
		/// <returns>On or more members of the <see cref="T:System.Security.AccessControl.ObjectAceFlags" /> enumeration combined with a logical OR operation.</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x000C02A4 File Offset: 0x000BE4A4
		// (set) Token: 0x060034FB RID: 13563 RVA: 0x000C02AC File Offset: 0x000BE4AC
		public ObjectAceFlags ObjectAceFlags
		{
			get
			{
				return this.object_ace_flags;
			}
			set
			{
				this.object_ace_flags = value;
			}
		}

		/// <summary>Gets or sets the GUID of the object type associated with this <see cref="T:System.Security.AccessControl.ObjectAce" /> object.</summary>
		/// <returns>The GUID of the object type associated with this <see cref="T:System.Security.AccessControl.ObjectAce" /> object.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x000C02B5 File Offset: 0x000BE4B5
		// (set) Token: 0x060034FD RID: 13565 RVA: 0x000C02BD File Offset: 0x000BE4BD
		public Guid ObjectAceType
		{
			get
			{
				return this.object_ace_type;
			}
			set
			{
				this.object_ace_type = value;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x000C02C6 File Offset: 0x000BE4C6
		private bool ObjectAceTypePresent
		{
			get
			{
				return (this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) > ObjectAceFlags.None;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.ObjectAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.ObjectAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.ObjectAce" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x060034FF RID: 13567 RVA: 0x000C02D4 File Offset: 0x000BE4D4
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			ushort binaryLength = (ushort)this.BinaryLength;
			binaryForm[offset++] = (byte)base.AceType;
			binaryForm[offset++] = (byte)base.AceFlags;
			GenericAce.WriteUShort(binaryLength, binaryForm, offset);
			offset += 2;
			GenericAce.WriteInt(base.AccessMask, binaryForm, offset);
			offset += 4;
			GenericAce.WriteInt((int)this.ObjectAceFlags, binaryForm, offset);
			offset += 4;
			if ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				this.WriteGuid(this.ObjectAceType, binaryForm, offset);
				offset += 16;
			}
			if ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				this.WriteGuid(this.InheritedObjectAceType, binaryForm, offset);
				offset += 16;
			}
			base.SecurityIdentifier.GetBinaryForm(binaryForm, offset);
			offset += base.SecurityIdentifier.BinaryLength;
			byte[] opaque = base.GetOpaque();
			if (opaque != null)
			{
				Array.Copy(opaque, 0, binaryForm, offset, opaque.Length);
				offset += opaque.Length;
			}
		}

		/// <summary>Returns the maximum allowed length, in bytes, of an opaque data BLOB for callback Access Control Entries (ACEs).</summary>
		/// <param name="isCallback">True if the <see cref="T:System.Security.AccessControl.ObjectAce" /> is a callback ACE type.</param>
		/// <returns>The maximum allowed length, in bytes, of an opaque data BLOB for callback Access Control Entries (ACEs).</returns>
		// Token: 0x06003500 RID: 13568 RVA: 0x000C03A9 File Offset: 0x000BE5A9
		public static int MaxOpaqueLength(bool isCallback)
		{
			return 65423;
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000C03B0 File Offset: 0x000BE5B0
		internal override string GetSddlForm()
		{
			if (base.OpaqueLength != 0)
			{
				throw new NotImplementedException("Unable to convert conditional ACEs to SDDL");
			}
			string text = "";
			if ((this.ObjectAceFlags & ObjectAceFlags.ObjectAceTypePresent) != ObjectAceFlags.None)
			{
				text = this.object_ace_type.ToString("D");
			}
			string text2 = "";
			if ((this.ObjectAceFlags & ObjectAceFlags.InheritedObjectAceTypePresent) != ObjectAceFlags.None)
			{
				text2 = this.inherited_object_type.ToString("D");
			}
			return string.Format(CultureInfo.InvariantCulture, "({0};{1};{2};{3};{4};{5})", new object[]
			{
				GenericAce.GetSddlAceType(base.AceType),
				GenericAce.GetSddlAceFlags(base.AceFlags),
				KnownAce.GetSddlAccessRights(base.AccessMask),
				text,
				text2,
				base.SecurityIdentifier.GetSddlForm()
			});
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000C0468 File Offset: 0x000BE668
		private static AceType ConvertType(AceQualifier qualifier, bool isCallback)
		{
			switch (qualifier)
			{
			case AceQualifier.AccessAllowed:
				if (isCallback)
				{
					return AceType.AccessAllowedCallbackObject;
				}
				return AceType.AccessAllowedObject;
			case AceQualifier.AccessDenied:
				if (isCallback)
				{
					return AceType.AccessDeniedCallbackObject;
				}
				return AceType.AccessDeniedObject;
			case AceQualifier.SystemAudit:
				if (isCallback)
				{
					return AceType.SystemAuditCallbackObject;
				}
				return AceType.SystemAuditObject;
			case AceQualifier.SystemAlarm:
				if (isCallback)
				{
					return AceType.SystemAlarmCallbackObject;
				}
				return AceType.SystemAlarmObject;
			default:
				throw new ArgumentException("Unrecognized ACE qualifier: " + qualifier.ToString(), "qualifier");
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000C04CE File Offset: 0x000BE6CE
		private void WriteGuid(Guid val, byte[] buffer, int offset)
		{
			Array.Copy(val.ToByteArray(), 0, buffer, offset, 16);
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000C04E4 File Offset: 0x000BE6E4
		private Guid ReadGuid(byte[] buffer, int offset)
		{
			byte[] array = new byte[16];
			Array.Copy(buffer, offset, array, 0, 16);
			return new Guid(array);
		}

		// Token: 0x040024C4 RID: 9412
		private Guid object_ace_type;

		// Token: 0x040024C5 RID: 9413
		private Guid inherited_object_type;

		// Token: 0x040024C6 RID: 9414
		private ObjectAceFlags object_ace_flags;
	}
}
