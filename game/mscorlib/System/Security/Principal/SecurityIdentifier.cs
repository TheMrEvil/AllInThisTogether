using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Principal
{
	/// <summary>Represents a security identifier (SID) and provides marshaling and comparison operations for SIDs.</summary>
	// Token: 0x020004E7 RID: 1255
	[ComVisible(false)]
	public sealed class SecurityIdentifier : IdentityReference, IComparable<SecurityIdentifier>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using the specified security identifier (SID) in Security Descriptor Definition Language (SDDL) format.</summary>
		/// <param name="sddlForm">SDDL string for the SID used to create the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		// Token: 0x06003214 RID: 12820 RVA: 0x000B7EB7 File Offset: 0x000B60B7
		public SecurityIdentifier(string sddlForm)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			this.buffer = SecurityIdentifier.ParseSddlForm(sddlForm);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using a specified binary representation of a security identifier (SID).</summary>
		/// <param name="binaryForm">The byte array that represents the SID.</param>
		/// <param name="offset">The byte offset to use as the starting index in <paramref name="binaryForm" />.</param>
		// Token: 0x06003215 RID: 12821 RVA: 0x000B7EDC File Offset: 0x000B60DC
		public unsafe SecurityIdentifier(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 2)
			{
				throw new ArgumentException("offset");
			}
			fixed (byte[] array = binaryForm)
			{
				byte* ptr;
				if (binaryForm == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				this.CreateFromBinaryForm((IntPtr)((void*)(ptr + offset)), binaryForm.Length - offset);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using an integer that represents the binary form of a security identifier (SID).</summary>
		/// <param name="binaryForm">An integer that represents the binary form of a SID.</param>
		// Token: 0x06003216 RID: 12822 RVA: 0x000B7F41 File Offset: 0x000B6141
		public SecurityIdentifier(IntPtr binaryForm)
		{
			this.CreateFromBinaryForm(binaryForm, int.MaxValue);
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000B7F58 File Offset: 0x000B6158
		private void CreateFromBinaryForm(IntPtr binaryForm, int length)
		{
			int num = (int)Marshal.ReadByte(binaryForm, 0);
			int num2 = (int)Marshal.ReadByte(binaryForm, 1);
			if (num != 1 || num2 > 15)
			{
				throw new ArgumentException("Value was invalid.");
			}
			if (length < 8 + num2 * 4)
			{
				throw new ArgumentException("offset");
			}
			this.buffer = new byte[8 + num2 * 4];
			Marshal.Copy(binaryForm, this.buffer, 0, this.buffer.Length);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using the specified well known security identifier (SID) type and domain SID.</summary>
		/// <param name="sidType">One of the enumeration values. This value must not be <see cref="F:System.Security.Principal.WellKnownSidType.LogonIdsSid" />.</param>
		/// <param name="domainSid">The domain SID. This value is required for the following <see cref="T:System.Security.Principal.WellKnownSidType" /> values. This parameter is ignored for any other <see cref="T:System.Security.Principal.WellKnownSidType" /> values.  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountAdministratorSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountGuestSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountKrbtgtSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainAdminsSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainUsersSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainGuestsSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountComputersSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountControllersSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountCertAdminsSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountSchemaAdminsSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountEnterpriseAdminsSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountPolicyAdminsSid" />  
		///  - <see cref="F:System.Security.Principal.WellKnownSidType.AccountRasAndIasServersSid" /></param>
		// Token: 0x06003218 RID: 12824 RVA: 0x000B7FC0 File Offset: 0x000B61C0
		public SecurityIdentifier(WellKnownSidType sidType, SecurityIdentifier domainSid)
		{
			WellKnownAccount wellKnownAccount = WellKnownAccount.LookupByType(sidType);
			if (wellKnownAccount == null)
			{
				throw new ArgumentException("Unable to convert SID type: " + sidType.ToString());
			}
			if (wellKnownAccount.IsAbsolute)
			{
				this.buffer = SecurityIdentifier.ParseSddlForm(wellKnownAccount.Sid);
				return;
			}
			if (domainSid == null)
			{
				throw new ArgumentNullException("domainSid");
			}
			this.buffer = SecurityIdentifier.ParseSddlForm(domainSid.Value + "-" + wellKnownAccount.Rid);
		}

		/// <summary>Returns the account domain security identifier (SID) portion from the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object if the SID represents a Windows account SID. If the SID does not represent a Windows account SID, this property returns <see cref="T:System.ArgumentNullException" />.</summary>
		/// <returns>The account domain SID portion from the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object if the SID represents a Windows account SID; otherwise, it returns <see cref="T:System.ArgumentNullException" />.</returns>
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000B804C File Offset: 0x000B624C
		public SecurityIdentifier AccountDomainSid
		{
			get
			{
				if (!this.Value.StartsWith("S-1-5-21") || this.buffer[1] < 4)
				{
					return null;
				}
				byte[] array = new byte[24];
				Array.Copy(this.buffer, 0, array, 0, array.Length);
				array[1] = 4;
				return new SecurityIdentifier(array, 0);
			}
		}

		/// <summary>Returns the length, in bytes, of the security identifier (SID) represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The length, in bytes, of the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x000B809C File Offset: 0x000B629C
		public int BinaryLength
		{
			get
			{
				return this.buffer.Length;
			}
		}

		/// <summary>Returns an uppercase Security Descriptor Definition Language (SDDL) string for the security identifier (SID) represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>An uppercase SDDL string for the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x000B80A8 File Offset: 0x000B62A8
		public override string Value
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				ulong sidAuthority = this.GetSidAuthority();
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "S-1-{0}", sidAuthority);
				for (byte b = 0; b < this.GetSidSubAuthorityCount(); b += 1)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "-{0}", this.GetSidSubAuthority(b));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000B8110 File Offset: 0x000B6310
		private ulong GetSidAuthority()
		{
			return (ulong)this.buffer[2] << 40 | (ulong)this.buffer[3] << 32 | (ulong)this.buffer[4] << 24 | (ulong)this.buffer[5] << 16 | (ulong)this.buffer[6] << 8 | (ulong)this.buffer[7];
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000B8166 File Offset: 0x000B6366
		private byte GetSidSubAuthorityCount()
		{
			return this.buffer[1];
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000B8170 File Offset: 0x000B6370
		private uint GetSidSubAuthority(byte index)
		{
			int num = (int)(8 + index * 4);
			return (uint)((int)this.buffer[num] | (int)this.buffer[num + 1] << 8 | (int)this.buffer[num + 2] << 16 | (int)this.buffer[num + 3] << 24);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="sid">The object to compare with the current object.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="sid" />.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance is less than <paramref name="sid" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="sid" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <paramref name="sid" />.</returns>
		// Token: 0x0600321F RID: 12831 RVA: 0x000B81B4 File Offset: 0x000B63B4
		public int CompareTo(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			int result;
			if ((result = this.GetSidAuthority().CompareTo(sid.GetSidAuthority())) != 0)
			{
				return result;
			}
			if ((result = this.GetSidSubAuthorityCount().CompareTo(sid.GetSidSubAuthorityCount())) != 0)
			{
				return result;
			}
			for (byte b = 0; b < this.GetSidSubAuthorityCount(); b += 1)
			{
				if ((result = this.GetSidSubAuthority(b).CompareTo(sid.GetSidSubAuthority(b))) != 0)
				{
					return result;
				}
			}
			return 0;
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is equal to a specified object.</summary>
		/// <param name="o">An object to compare with this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is an object with the same underlying type and value as this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003220 RID: 12832 RVA: 0x000B8239 File Offset: 0x000B6439
		public override bool Equals(object o)
		{
			return this.Equals(o as SecurityIdentifier);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is equal to the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="sid">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="sid" /> is equal to the value of the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x06003221 RID: 12833 RVA: 0x000B8247 File Offset: 0x000B6447
		public bool Equals(SecurityIdentifier sid)
		{
			return !(sid == null) && sid.Value == this.Value;
		}

		/// <summary>Copies the binary representation of the specified security identifier (SID) represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class to a byte array.</summary>
		/// <param name="binaryForm">The byte array to receive the copied SID.</param>
		/// <param name="offset">The byte offset to use as the starting index in <paramref name="binaryForm" />.</param>
		// Token: 0x06003222 RID: 12834 RVA: 0x000B8268 File Offset: 0x000B6468
		public void GetBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - this.buffer.Length)
			{
				throw new ArgumentException("offset");
			}
			Array.Copy(this.buffer, 0, binaryForm, offset, this.buffer.Length);
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object. The <see cref="M:System.Security.Principal.SecurityIdentifier.GetHashCode" /> method is suitable for hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash value for the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x06003223 RID: 12835 RVA: 0x000B7DFC File Offset: 0x000B5FFC
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether the security identifier (SID) represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is a valid Windows account SID.</summary>
		/// <returns>
		///   <see langword="true" /> if the SID represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is a valid Windows account SID; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003224 RID: 12836 RVA: 0x000B82B7 File Offset: 0x000B64B7
		public bool IsAccountSid()
		{
			return this.AccountDomainSid != null;
		}

		/// <summary>Returns a value that indicates whether the security identifier (SID) represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is from the same domain as the specified SID.</summary>
		/// <param name="sid">The SID to compare with this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the SID represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is in the same domain as the <paramref name="sid" /> SID; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003225 RID: 12837 RVA: 0x000B82C8 File Offset: 0x000B64C8
		public bool IsEqualDomainSid(SecurityIdentifier sid)
		{
			SecurityIdentifier accountDomainSid = this.AccountDomainSid;
			return !(accountDomainSid == null) && accountDomainSid.Equals(sid.AccountDomainSid);
		}

		/// <summary>Returns a value that indicates whether the specified type is a valid translation type for the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class.</summary>
		/// <param name="targetType">The type being queried for validity to serve as a conversion from <see cref="T:System.Security.Principal.SecurityIdentifier" />. The following target types are valid:  
		///  - <see cref="T:System.Security.Principal.NTAccount" />  
		///  - <see cref="T:System.Security.Principal.SecurityIdentifier" /></param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="targetType" /> is a valid translation type for the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003226 RID: 12838 RVA: 0x000B82F3 File Offset: 0x000B64F3
		public override bool IsValidTargetType(Type targetType)
		{
			return targetType == typeof(SecurityIdentifier) || targetType == typeof(NTAccount);
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object matches the specified well known security identifier (SID) type.</summary>
		/// <param name="type">A value to compare with the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="type" /> is the SID type for the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003227 RID: 12839 RVA: 0x000B8320 File Offset: 0x000B6520
		public bool IsWellKnown(WellKnownSidType type)
		{
			WellKnownAccount wellKnownAccount = WellKnownAccount.LookupByType(type);
			if (wellKnownAccount == null)
			{
				return false;
			}
			string value = this.Value;
			if (wellKnownAccount.IsAbsolute)
			{
				return value == wellKnownAccount.Sid;
			}
			return value.StartsWith("S-1-5-21", StringComparison.OrdinalIgnoreCase) && value.EndsWith("-" + wellKnownAccount.Rid, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Returns the security identifier (SID), in Security Descriptor Definition Language (SDDL) format, for the account represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object. An example of the SDDL format is S-1-5-9.</summary>
		/// <returns>The SID, in SDDL format, for the account represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x06003228 RID: 12840 RVA: 0x000B7E34 File Offset: 0x000B6034
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Translates the account name represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object into another <see cref="T:System.Security.Principal.IdentityReference" />-derived type.</summary>
		/// <param name="targetType">The target type for the conversion from <see cref="T:System.Security.Principal.SecurityIdentifier" />. The target type must be a type that is considered valid by the <see cref="M:System.Security.Principal.SecurityIdentifier.IsValidTargetType(System.Type)" /> method.</param>
		/// <returns>The converted identity.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetType" /> is not an <see cref="T:System.Security.Principal.IdentityReference" /> type.</exception>
		/// <exception cref="T:System.Security.Principal.IdentityNotMappedException">Some or all identity references could not be translated.</exception>
		/// <exception cref="T:System.SystemException">A Win32 error code was returned.</exception>
		// Token: 0x06003229 RID: 12841 RVA: 0x000B837C File Offset: 0x000B657C
		public override IdentityReference Translate(Type targetType)
		{
			if (targetType == typeof(SecurityIdentifier))
			{
				return this;
			}
			if (!(targetType == typeof(NTAccount)))
			{
				throw new ArgumentException("Unknown type.", "targetType");
			}
			WellKnownAccount wellKnownAccount = WellKnownAccount.LookupBySid(this.Value);
			if (wellKnownAccount == null || wellKnownAccount.Name == null)
			{
				throw new IdentityNotMappedException("Unable to map SID: " + this.Value);
			}
			return new NTAccount(wellKnownAccount.Name);
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.SecurityIdentifier" /> objects to determine whether they are equal. They are considered equal if they have the same canonical representation as the one returned by the <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> property or if they are both <see langword="null" />.</summary>
		/// <param name="left">The left operand to use for the equality comparison. This parameter can be <see langword="null" />.</param>
		/// <param name="right">The right operand to use for the equality comparison. This parameter can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600322A RID: 12842 RVA: 0x000B7B98 File Offset: 0x000B5D98
		public static bool operator ==(SecurityIdentifier left, SecurityIdentifier right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Value == right.Value;
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.SecurityIdentifier" /> objects to determine whether they are not equal. They are considered not equal if they have different canonical name representations than the one returned by the <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> property or if one of the objects is <see langword="null" /> and the other is not.</summary>
		/// <param name="left">The left operand to use for the inequality comparison. This parameter can be <see langword="null" />.</param>
		/// <param name="right">The right operand to use for the inequality comparison. This parameter can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600322B RID: 12843 RVA: 0x000B7BB8 File Offset: 0x000B5DB8
		public static bool operator !=(SecurityIdentifier left, SecurityIdentifier right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.Value != right.Value;
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000B83F8 File Offset: 0x000B65F8
		internal string GetSddlForm()
		{
			string value = this.Value;
			WellKnownAccount wellKnownAccount = WellKnownAccount.LookupBySid(value);
			if (wellKnownAccount == null || wellKnownAccount.SddlForm == null)
			{
				return value;
			}
			return wellKnownAccount.SddlForm;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000B8428 File Offset: 0x000B6628
		internal static SecurityIdentifier ParseSddlForm(string sddlForm, ref int pos)
		{
			if (sddlForm.Length - pos < 2)
			{
				throw new ArgumentException("Invalid SDDL string.", "sddlForm");
			}
			string text = sddlForm.Substring(pos, 2).ToUpperInvariant();
			string sddlForm2;
			int num2;
			if (text == "S-")
			{
				int num = pos;
				char c = char.ToUpperInvariant(sddlForm[num]);
				while (c == 'S' || c == '-' || c == 'X' || (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'))
				{
					num++;
					c = char.ToUpperInvariant(sddlForm[num]);
				}
				if (c == ':' && sddlForm[num - 1] == 'D')
				{
					num--;
				}
				sddlForm2 = sddlForm.Substring(pos, num - pos);
				num2 = num - pos;
			}
			else
			{
				sddlForm2 = text;
				num2 = 2;
			}
			SecurityIdentifier result = new SecurityIdentifier(sddlForm2);
			pos += num2;
			return result;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000B84F8 File Offset: 0x000B66F8
		private static byte[] ParseSddlForm(string sddlForm)
		{
			string text = sddlForm;
			if (sddlForm.Length == 2)
			{
				WellKnownAccount wellKnownAccount = WellKnownAccount.LookupBySddlForm(sddlForm);
				if (wellKnownAccount == null)
				{
					throw new ArgumentException("Invalid SDDL string - unrecognized account: " + sddlForm, "sddlForm");
				}
				if (!wellKnownAccount.IsAbsolute)
				{
					throw new NotImplementedException("Mono unable to convert account to SID: " + ((wellKnownAccount.Name != null) ? wellKnownAccount.Name : sddlForm));
				}
				text = wellKnownAccount.Sid;
			}
			string[] array = text.ToUpperInvariant().Split('-', StringSplitOptions.None);
			int num = array.Length - 3;
			if (array.Length < 3 || array[0] != "S" || num > 15)
			{
				throw new ArgumentException("Value was invalid.");
			}
			if (array[1] != "1")
			{
				throw new ArgumentException("Only SIDs with revision 1 are supported");
			}
			byte[] array2 = new byte[8 + num * 4];
			array2[0] = 1;
			array2[1] = (byte)num;
			ulong num2;
			if (!SecurityIdentifier.TryParseAuthority(array[2], out num2))
			{
				throw new ArgumentException("Value was invalid.");
			}
			array2[2] = (byte)(num2 >> 40 & 255UL);
			array2[3] = (byte)(num2 >> 32 & 255UL);
			array2[4] = (byte)(num2 >> 24 & 255UL);
			array2[5] = (byte)(num2 >> 16 & 255UL);
			array2[6] = (byte)(num2 >> 8 & 255UL);
			array2[7] = (byte)(num2 & 255UL);
			for (int i = 0; i < num; i++)
			{
				uint num3;
				if (!SecurityIdentifier.TryParseSubAuthority(array[i + 3], out num3))
				{
					throw new ArgumentException("Value was invalid.");
				}
				int num4 = 8 + i * 4;
				array2[num4] = (byte)num3;
				array2[num4 + 1] = (byte)(num3 >> 8);
				array2[num4 + 2] = (byte)(num3 >> 16);
				array2[num4 + 3] = (byte)(num3 >> 24);
			}
			return array2;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000B869E File Offset: 0x000B689E
		private static bool TryParseAuthority(string s, out ulong result)
		{
			if (s.StartsWith("0X"))
			{
				return ulong.TryParse(s.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
			}
			return ulong.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000B86D2 File Offset: 0x000B68D2
		private static bool TryParseSubAuthority(string s, out uint result)
		{
			if (s.StartsWith("0X"))
			{
				return uint.TryParse(s.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
			}
			return uint.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000B8706 File Offset: 0x000B6906
		// Note: this type is marked as 'beforefieldinit'.
		static SecurityIdentifier()
		{
		}

		// Token: 0x040022C2 RID: 8898
		private byte[] buffer;

		/// <summary>Returns the maximum size, in bytes, of the binary representation of the security identifier.</summary>
		// Token: 0x040022C3 RID: 8899
		public static readonly int MaxBinaryLength = 68;

		/// <summary>Returns the minimum size, in bytes, of the binary representation of the security identifier.</summary>
		// Token: 0x040022C4 RID: 8900
		public static readonly int MinBinaryLength = 8;
	}
}
