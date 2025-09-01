using System;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE) that is not defined by one of the members of the <see cref="T:System.Security.AccessControl.AceType" /> enumeration.</summary>
	// Token: 0x0200051F RID: 1311
	public sealed class CustomAce : GenericAce
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CustomAce" /> class.</summary>
		/// <param name="type">Type of the new Access Control Entry (ACE). This value must be greater than <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" />.</param>
		/// <param name="flags">Flags that specify information about the inheritance, inheritance propagation, and auditing conditions for the new ACE.</param>
		/// <param name="opaque">An array of byte values that contains the data for the new ACE. This value can be <see langword="null" />. The length of this array must not be greater than the value of the <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> field, and must be a multiple of four.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="type" /> parameter is not greater than <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" /> or the length of the <paramref name="opaque" /> array is either greater than the value of the <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> field or not a multiple of four.</exception>
		// Token: 0x060033EC RID: 13292 RVA: 0x000BE0DE File Offset: 0x000BC2DE
		public CustomAce(AceType type, AceFlags flags, byte[] opaque) : base(type, flags)
		{
			this.SetOpaque(opaque);
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CustomAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.CustomAce.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CustomAce" /> object.</returns>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public override int BinaryLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the length of the opaque data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		/// <returns>The length of the opaque callback data.</returns>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x000BE0EF File Offset: 0x000BC2EF
		public int OpaqueLength
		{
			get
			{
				return this.opaque.Length;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CustomAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CustomAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.CustomAce" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x060033EF RID: 13295 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the opaque data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		/// <returns>An array of byte values that represents the opaque data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</returns>
		// Token: 0x060033F0 RID: 13296 RVA: 0x000BE0F9 File Offset: 0x000BC2F9
		public byte[] GetOpaque()
		{
			return (byte[])this.opaque.Clone();
		}

		/// <summary>Sets the opaque callback data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		/// <param name="opaque">An array of byte values that represents the opaque callback data for this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</param>
		// Token: 0x060033F1 RID: 13297 RVA: 0x000BE10B File Offset: 0x000BC30B
		public void SetOpaque(byte[] opaque)
		{
			if (opaque == null)
			{
				this.opaque = null;
				return;
			}
			this.opaque = (byte[])opaque.Clone();
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000472C8 File Offset: 0x000454C8
		internal override string GetSddlForm()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04002483 RID: 9347
		private byte[] opaque;

		/// <summary>Returns the maximum allowed length of an opaque data blob for this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		// Token: 0x04002484 RID: 9348
		[MonoTODO]
		public static readonly int MaxOpaqueLength;
	}
}
