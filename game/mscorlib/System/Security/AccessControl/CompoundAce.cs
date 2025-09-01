using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a compound Access Control Entry (ACE).</summary>
	// Token: 0x02000518 RID: 1304
	public sealed class CompoundAce : KnownAce
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CompoundAce" /> class.</summary>
		/// <param name="flags">Contains flags that specify information about the inheritance, inheritance propagation, and auditing conditions for the new Access Control Entry (ACE).</param>
		/// <param name="accessMask">The access mask for the ACE.</param>
		/// <param name="compoundAceType">A value from the <see cref="T:System.Security.AccessControl.CompoundAceType" /> enumeration.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> associated with the new ACE.</param>
		// Token: 0x060033CE RID: 13262 RVA: 0x000BE023 File Offset: 0x000BC223
		public CompoundAce(AceFlags flags, int accessMask, CompoundAceType compoundAceType, SecurityIdentifier sid) : base(AceType.AccessAllowedCompound, flags)
		{
			this.compound_ace_type = compoundAceType;
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CompoundAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.CompoundAce.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CompoundAce" /> object.</returns>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public override int BinaryLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the type of this <see cref="T:System.Security.AccessControl.CompoundAce" /> object.</summary>
		/// <returns>The type of this <see cref="T:System.Security.AccessControl.CompoundAce" /> object.</returns>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x000BE043 File Offset: 0x000BC243
		// (set) Token: 0x060033D1 RID: 13265 RVA: 0x000BE04B File Offset: 0x000BC24B
		public CompoundAceType CompoundAceType
		{
			get
			{
				return this.compound_ace_type;
			}
			set
			{
				this.compound_ace_type = value;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CompoundAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CompoundAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.CompoundAce" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x060033D2 RID: 13266 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal override string GetSddlForm()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400245D RID: 9309
		private CompoundAceType compound_ace_type;
	}
}
