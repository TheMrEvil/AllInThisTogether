using System;
using System.Collections.Generic;
using System.Text;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control List (ACL).</summary>
	// Token: 0x02000545 RID: 1349
	public sealed class RawAcl : GenericAcl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawAcl" /> class with the specified revision level.</summary>
		/// <param name="revision">The revision level of the new Access Control List (ACL).</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.RawAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x06003565 RID: 13669 RVA: 0x000C107D File Offset: 0x000BF27D
		public RawAcl(byte revision, int capacity)
		{
			this.revision = revision;
			this.list = new List<GenericAce>(capacity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawAcl" /> class from the specified binary form.</summary>
		/// <param name="binaryForm">An array of byte values that represent an Access Control List (ACL).</param>
		/// <param name="offset">The offset in the <paramref name="binaryForm" /> parameter at which to begin unmarshaling data.</param>
		// Token: 0x06003566 RID: 13670 RVA: 0x000C1098 File Offset: 0x000BF298
		public RawAcl(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 8)
			{
				throw new ArgumentOutOfRangeException("offset", offset, "Offset out of range");
			}
			this.revision = binaryForm[offset];
			if (this.revision != GenericAcl.AclRevision && this.revision != GenericAcl.AclRevisionDS)
			{
				throw new ArgumentException("Invalid ACL - unknown revision", "binaryForm");
			}
			int num = (int)this.ReadUShort(binaryForm, offset + 2);
			if (offset > binaryForm.Length - num)
			{
				throw new ArgumentException("Invalid ACL - truncated", "binaryForm");
			}
			int num2 = offset + 8;
			int num3 = (int)this.ReadUShort(binaryForm, offset + 4);
			this.list = new List<GenericAce>(num3);
			for (int i = 0; i < num3; i++)
			{
				GenericAce genericAce = GenericAce.CreateFromBinaryForm(binaryForm, num2);
				this.list.Add(genericAce);
				num2 += genericAce.BinaryLength;
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000C1178 File Offset: 0x000BF378
		internal RawAcl(byte revision, List<GenericAce> aces)
		{
			this.revision = revision;
			this.list = aces;
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.RawAcl" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.RawAcl.GetBinaryForm(System.Byte[],System.Int32)" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.RawAcl" /> object.</returns>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06003568 RID: 13672 RVA: 0x000C1190 File Offset: 0x000BF390
		public override int BinaryLength
		{
			get
			{
				int num = 8;
				foreach (GenericAce genericAce in this.list)
				{
					num += genericAce.BinaryLength;
				}
				return num;
			}
		}

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.RawAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.RawAcl" /> object.</returns>
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x000C11E8 File Offset: 0x000BF3E8
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets or sets the Access Control Entry (ACE) at the specified index.</summary>
		/// <param name="index">The zero-based index of the ACE to get or set.</param>
		/// <returns>The ACE at the specified index.</returns>
		// Token: 0x17000763 RID: 1891
		public override GenericAce this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.RawAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.RawAcl" />.</returns>
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600356C RID: 13676 RVA: 0x000C1212 File Offset: 0x000BF412
		public override byte Revision
		{
			get
			{
				return this.revision;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.RawAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.RawAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.RawAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x0600356D RID: 13677 RVA: 0x000C121C File Offset: 0x000BF41C
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - this.BinaryLength)
			{
				throw new ArgumentException("Offset out of range", "offset");
			}
			binaryForm[offset] = this.Revision;
			binaryForm[offset + 1] = 0;
			this.WriteUShort((ushort)this.BinaryLength, binaryForm, offset + 2);
			this.WriteUShort((ushort)this.list.Count, binaryForm, offset + 4);
			this.WriteUShort(0, binaryForm, offset + 6);
			int num = offset + 8;
			foreach (GenericAce genericAce in this.list)
			{
				genericAce.GetBinaryForm(binaryForm, num);
				num += genericAce.BinaryLength;
			}
		}

		/// <summary>Inserts the specified Access Control Entry (ACE) at the specified index.</summary>
		/// <param name="index">The position at which to add the new ACE. Specify the value of the <see cref="P:System.Security.AccessControl.RawAcl.Count" /> property to insert an ACE at the end of the <see cref="T:System.Security.AccessControl.RawAcl" /> object.</param>
		/// <param name="ace">The ACE to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x0600356E RID: 13678 RVA: 0x000C12F0 File Offset: 0x000BF4F0
		public void InsertAce(int index, GenericAce ace)
		{
			if (ace == null)
			{
				throw new ArgumentNullException("ace");
			}
			this.list.Insert(index, ace);
		}

		/// <summary>Removes the Access Control Entry (ACE) at the specified location.</summary>
		/// <param name="index">The zero-based index of the ACE to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="index" /> parameter is higher than the value of the <see cref="P:System.Security.AccessControl.RawAcl.Count" /> property minus one or is negative.</exception>
		// Token: 0x0600356F RID: 13679 RVA: 0x000C1313 File Offset: 0x000BF513
		public void RemoveAce(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000C1324 File Offset: 0x000BF524
		internal override string GetSddlForm(ControlFlags sdFlags, bool isDacl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (isDacl)
			{
				if ((sdFlags & ControlFlags.DiscretionaryAclProtected) != ControlFlags.None)
				{
					stringBuilder.Append("P");
				}
				if ((sdFlags & ControlFlags.DiscretionaryAclAutoInheritRequired) != ControlFlags.None)
				{
					stringBuilder.Append("AR");
				}
				if ((sdFlags & ControlFlags.DiscretionaryAclAutoInherited) != ControlFlags.None)
				{
					stringBuilder.Append("AI");
				}
			}
			else
			{
				if ((sdFlags & ControlFlags.SystemAclProtected) != ControlFlags.None)
				{
					stringBuilder.Append("P");
				}
				if ((sdFlags & ControlFlags.SystemAclAutoInheritRequired) != ControlFlags.None)
				{
					stringBuilder.Append("AR");
				}
				if ((sdFlags & ControlFlags.SystemAclAutoInherited) != ControlFlags.None)
				{
					stringBuilder.Append("AI");
				}
			}
			foreach (GenericAce genericAce in this.list)
			{
				stringBuilder.Append(genericAce.GetSddlForm());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000C140C File Offset: 0x000BF60C
		internal static RawAcl ParseSddlForm(string sddlForm, bool isDacl, ref ControlFlags sdFlags, ref int pos)
		{
			RawAcl.ParseFlags(sddlForm, isDacl, ref sdFlags, ref pos);
			byte b = GenericAcl.AclRevision;
			List<GenericAce> list = new List<GenericAce>();
			while (pos < sddlForm.Length && sddlForm[pos] == '(')
			{
				GenericAce genericAce = GenericAce.CreateFromSddlForm(sddlForm, ref pos);
				if (genericAce as ObjectAce != null)
				{
					b = GenericAcl.AclRevisionDS;
				}
				list.Add(genericAce);
			}
			return new RawAcl(b, list);
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000C1470 File Offset: 0x000BF670
		private static void ParseFlags(string sddlForm, bool isDacl, ref ControlFlags sdFlags, ref int pos)
		{
			char c = char.ToUpperInvariant(sddlForm[pos]);
			while (c == 'P' || c == 'A')
			{
				if (c == 'P')
				{
					if (isDacl)
					{
						sdFlags |= ControlFlags.DiscretionaryAclProtected;
					}
					else
					{
						sdFlags |= ControlFlags.SystemAclProtected;
					}
					pos++;
				}
				else
				{
					if (sddlForm.Length <= pos + 1)
					{
						throw new ArgumentException("Invalid SDDL string.", "sddlForm");
					}
					c = char.ToUpperInvariant(sddlForm[pos + 1]);
					if (c == 'R')
					{
						if (isDacl)
						{
							sdFlags |= ControlFlags.DiscretionaryAclAutoInheritRequired;
						}
						else
						{
							sdFlags |= ControlFlags.SystemAclAutoInheritRequired;
						}
						pos += 2;
					}
					else
					{
						if (c != 'I')
						{
							throw new ArgumentException("Invalid SDDL string.", "sddlForm");
						}
						if (isDacl)
						{
							sdFlags |= ControlFlags.DiscretionaryAclAutoInherited;
						}
						else
						{
							sdFlags |= ControlFlags.SystemAclAutoInherited;
						}
						pos += 2;
					}
				}
				c = char.ToUpperInvariant(sddlForm[pos]);
			}
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000BF846 File Offset: 0x000BDA46
		private void WriteUShort(ushort val, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)val;
			buffer[offset + 1] = (byte)(val >> 8);
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000C155F File Offset: 0x000BF75F
		private ushort ReadUShort(byte[] buffer, int offset)
		{
			return (ushort)((int)buffer[offset] | (int)buffer[offset + 1] << 8);
		}

		// Token: 0x040024D5 RID: 9429
		private byte revision;

		// Token: 0x040024D6 RID: 9430
		private List<GenericAce> list;
	}
}
