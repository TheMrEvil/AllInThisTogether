using System;
using System.Globalization;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access control entry (ACE).</summary>
	// Token: 0x0200050E RID: 1294
	public sealed class CommonAce : QualifiedAce
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CommonAce" /> class.</summary>
		/// <param name="flags">Flags that specify information about the inheritance, inheritance propagation, and auditing conditions for the new access control entry (ACE).</param>
		/// <param name="qualifier">The use of the new ACE.</param>
		/// <param name="accessMask">The access mask for the ACE.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> associated with the new ACE.</param>
		/// <param name="isCallback">
		///   <see langword="true" /> to specify that the new ACE is a callback type ACE.</param>
		/// <param name="opaque">Opaque data associated with the new ACE. Opaque data is allowed only for callback ACE types. The length of this array must not be greater than the return value of the <see cref="M:System.Security.AccessControl.CommonAce.MaxOpaqueLength(System.Boolean)" /> method.</param>
		// Token: 0x0600335D RID: 13149 RVA: 0x000BCA68 File Offset: 0x000BAC68
		public CommonAce(AceFlags flags, AceQualifier qualifier, int accessMask, SecurityIdentifier sid, bool isCallback, byte[] opaque) : base(CommonAce.ConvertType(qualifier, isCallback), flags, opaque)
		{
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000BCA8A File Offset: 0x000BAC8A
		internal CommonAce(AceType type, AceFlags flags, int accessMask, SecurityIdentifier sid, byte[] opaque) : base(type, flags, opaque)
		{
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000BCAA8 File Offset: 0x000BACA8
		internal CommonAce(byte[] binaryForm, int offset) : base(binaryForm, offset)
		{
			int num = (int)GenericAce.ReadUShort(binaryForm, offset + 2);
			if (offset > binaryForm.Length - num)
			{
				throw new ArgumentException("Invalid ACE - truncated", "binaryForm");
			}
			if (num < 8 + SecurityIdentifier.MinBinaryLength)
			{
				throw new ArgumentException("Invalid ACE", "binaryForm");
			}
			base.AccessMask = GenericAce.ReadInt(binaryForm, offset + 4);
			base.SecurityIdentifier = new SecurityIdentifier(binaryForm, offset + 8);
			int num2 = num - (8 + base.SecurityIdentifier.BinaryLength);
			if (num2 > 0)
			{
				byte[] array = new byte[num2];
				Array.Copy(binaryForm, offset + 8 + base.SecurityIdentifier.BinaryLength, array, 0, num2);
				base.SetOpaque(array);
			}
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAce" /> object. Use this length with the <see cref="M:System.Security.AccessControl.CommonAce.GetBinaryForm(System.Byte[],System.Int32)" /> method before marshaling the ACL into a binary array.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAce" /> object.</returns>
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x000BCB51 File Offset: 0x000BAD51
		public override int BinaryLength
		{
			get
			{
				return 8 + base.SecurityIdentifier.BinaryLength + base.OpaqueLength;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CommonAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CommonAce" /> object is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.CommonAce" /> to be copied into the <paramref name="binaryForm" /> array.</exception>
		// Token: 0x06003361 RID: 13153 RVA: 0x000BCB68 File Offset: 0x000BAD68
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			ushort binaryLength = (ushort)this.BinaryLength;
			binaryForm[offset] = (byte)base.AceType;
			binaryForm[offset + 1] = (byte)base.AceFlags;
			GenericAce.WriteUShort(binaryLength, binaryForm, offset + 2);
			GenericAce.WriteInt(base.AccessMask, binaryForm, offset + 4);
			base.SecurityIdentifier.GetBinaryForm(binaryForm, offset + 8);
			byte[] opaque = base.GetOpaque();
			if (opaque != null)
			{
				Array.Copy(opaque, 0, binaryForm, offset + 8 + base.SecurityIdentifier.BinaryLength, opaque.Length);
			}
		}

		/// <summary>Gets the maximum allowed length of an opaque data BLOB for callback access control entries (ACEs).</summary>
		/// <param name="isCallback">
		///   <see langword="true" /> to specify that the <see cref="T:System.Security.AccessControl.CommonAce" /> object is a callback ACE type.</param>
		/// <returns>The allowed length of an opaque data BLOB.</returns>
		// Token: 0x06003362 RID: 13154 RVA: 0x000BCBDB File Offset: 0x000BADDB
		public static int MaxOpaqueLength(bool isCallback)
		{
			return 65459;
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000BCBE4 File Offset: 0x000BADE4
		internal override string GetSddlForm()
		{
			if (base.OpaqueLength != 0)
			{
				throw new NotImplementedException("Unable to convert conditional ACEs to SDDL");
			}
			return string.Format(CultureInfo.InvariantCulture, "({0};{1};{2};;;{3})", new object[]
			{
				GenericAce.GetSddlAceType(base.AceType),
				GenericAce.GetSddlAceFlags(base.AceFlags),
				KnownAce.GetSddlAccessRights(base.AccessMask),
				base.SecurityIdentifier.GetSddlForm()
			});
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000BCC54 File Offset: 0x000BAE54
		private static AceType ConvertType(AceQualifier qualifier, bool isCallback)
		{
			switch (qualifier)
			{
			case AceQualifier.AccessAllowed:
				if (isCallback)
				{
					return AceType.AccessAllowedCallback;
				}
				return AceType.AccessAllowed;
			case AceQualifier.AccessDenied:
				if (isCallback)
				{
					return AceType.AccessDeniedCallback;
				}
				return AceType.AccessDenied;
			case AceQualifier.SystemAudit:
				if (isCallback)
				{
					return AceType.SystemAuditCallback;
				}
				return AceType.SystemAudit;
			case AceQualifier.SystemAlarm:
				if (isCallback)
				{
					return AceType.SystemAlarmCallback;
				}
				return AceType.SystemAlarm;
			default:
				throw new ArgumentException("Unrecognized ACE qualifier: " + qualifier.ToString(), "qualifier");
			}
		}
	}
}
