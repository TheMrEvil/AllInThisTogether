using System;
using System.Text;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000033 RID: 51
	public class Type3Message : MessageBase
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		[Obsolete("Use of this API is highly discouraged, it selects legacy-mode LM/NTLM authentication, which sends your password in very weak encryption over the wire even if the server supports the more secure NTLMv2 / NTLMv2 Session. You need to use the new `Type3Message (Type2Message)' constructor to use the more secure NTLMv2 / NTLMv2 Session authentication modes. These require the Type 2 message from the server to compute the response.")]
		public Type3Message() : base(3)
		{
			if (Type3Message.DefaultAuthLevel != NtlmAuthLevel.LM_and_NTLM)
			{
				throw new InvalidOperationException("Refusing to use legacy-mode LM/NTLM authentication unless explicitly enabled using DefaultAuthLevel.");
			}
			this._domain = Environment.UserDomainName;
			this._host = Environment.MachineName;
			this._username = Environment.UserName;
			this._level = NtlmAuthLevel.LM_and_NTLM;
			base.Flags = (NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000E655 File Offset: 0x0000C855
		public Type3Message(byte[] message) : base(3)
		{
			this.Decode(message);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000E668 File Offset: 0x0000C868
		public Type3Message(Type2Message type2) : base(3)
		{
			this._type2 = type2;
			this._level = NtlmSettings.DefaultAuthLevel;
			this._challenge = (byte[])type2.Nonce.Clone();
			this._domain = type2.TargetName;
			this._host = Environment.MachineName;
			this._username = Environment.UserName;
			base.Flags = (NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign);
			if ((type2.Flags & NtlmFlags.NegotiateUnicode) != (NtlmFlags)0)
			{
				base.Flags |= NtlmFlags.NegotiateUnicode;
			}
			else
			{
				base.Flags |= NtlmFlags.NegotiateOem;
			}
			if ((type2.Flags & NtlmFlags.NegotiateNtlm2Key) != (NtlmFlags)0)
			{
				base.Flags |= NtlmFlags.NegotiateNtlm2Key;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000E71C File Offset: 0x0000C91C
		~Type3Message()
		{
			if (this._challenge != null)
			{
				Array.Clear(this._challenge, 0, this._challenge.Length);
			}
			if (this._lm != null)
			{
				Array.Clear(this._lm, 0, this._lm.Length);
			}
			if (this._nt != null)
			{
				Array.Clear(this._nt, 0, this._nt.Length);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000E798 File Offset: 0x0000C998
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000E79F File Offset: 0x0000C99F
		[Obsolete("Use NtlmSettings.DefaultAuthLevel")]
		public static NtlmAuthLevel DefaultAuthLevel
		{
			get
			{
				return NtlmSettings.DefaultAuthLevel;
			}
			set
			{
				NtlmSettings.DefaultAuthLevel = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000E7A7 File Offset: 0x0000C9A7
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000E7AF File Offset: 0x0000C9AF
		public NtlmAuthLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		[Obsolete("Use of this API is highly discouraged, it selects legacy-mode LM/NTLM authentication, which sends your password in very weak encryption over the wire even if the server supports the more secure NTLMv2 / NTLMv2 Session. You need to use the new `Type3Message (Type2Message)' constructor to use the more secure NTLMv2 / NTLMv2 Session authentication modes. These require the Type 2 message from the server to compute the response.")]
		public byte[] Challenge
		{
			get
			{
				if (this._challenge == null)
				{
					return null;
				}
				return (byte[])this._challenge.Clone();
			}
			set
			{
				if (this._type2 != null || this._level != NtlmAuthLevel.LM_and_NTLM)
				{
					throw new InvalidOperationException("Refusing to use legacy-mode LM/NTLM authentication unless explicitly enabled using DefaultAuthLevel.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("Challenge");
				}
				if (value.Length != 8)
				{
					throw new ArgumentException(Locale.GetText("Invalid Challenge Length (should be 8 bytes)."), "Challenge");
				}
				this._challenge = (byte[])value.Clone();
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000E836 File Offset: 0x0000CA36
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000E840 File Offset: 0x0000CA40
		public string Domain
		{
			get
			{
				return this._domain;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == "")
				{
					base.Flags &= ~NtlmFlags.NegotiateDomainSupplied;
				}
				else
				{
					base.Flags |= NtlmFlags.NegotiateDomainSupplied;
				}
				this._domain = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000E891 File Offset: 0x0000CA91
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000E89C File Offset: 0x0000CA9C
		public string Host
		{
			get
			{
				return this._host;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == "")
				{
					base.Flags &= ~NtlmFlags.NegotiateWorkstationSupplied;
				}
				else
				{
					base.Flags |= NtlmFlags.NegotiateWorkstationSupplied;
				}
				this._host = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000E8ED File Offset: 0x0000CAED
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000E8F5 File Offset: 0x0000CAF5
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000E8FE File Offset: 0x0000CAFE
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000E906 File Offset: 0x0000CB06
		public string Username
		{
			get
			{
				return this._username;
			}
			set
			{
				this._username = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000E90F File Offset: 0x0000CB0F
		public byte[] LM
		{
			get
			{
				return this._lm;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000E917 File Offset: 0x0000CB17
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000E91F File Offset: 0x0000CB1F
		public byte[] NT
		{
			get
			{
				return this._nt;
			}
			set
			{
				this._nt = value;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000E928 File Offset: 0x0000CB28
		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			this._password = null;
			if (message.Length >= 64)
			{
				base.Flags = (NtlmFlags)BitConverterLE.ToUInt32(message, 60);
			}
			else
			{
				base.Flags = (NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign);
			}
			int num = (int)BitConverterLE.ToUInt16(message, 12);
			int srcOffset = (int)BitConverterLE.ToUInt16(message, 16);
			this._lm = new byte[num];
			Buffer.BlockCopy(message, srcOffset, this._lm, 0, num);
			int num2 = (int)BitConverterLE.ToUInt16(message, 20);
			int srcOffset2 = (int)BitConverterLE.ToUInt16(message, 24);
			this._nt = new byte[num2];
			Buffer.BlockCopy(message, srcOffset2, this._nt, 0, num2);
			int len = (int)BitConverterLE.ToUInt16(message, 28);
			int offset = (int)BitConverterLE.ToUInt16(message, 32);
			this._domain = this.DecodeString(message, offset, len);
			int len2 = (int)BitConverterLE.ToUInt16(message, 36);
			int offset2 = (int)BitConverterLE.ToUInt16(message, 40);
			this._username = this.DecodeString(message, offset2, len2);
			int len3 = (int)BitConverterLE.ToUInt16(message, 44);
			int offset3 = (int)BitConverterLE.ToUInt16(message, 48);
			this._host = this.DecodeString(message, offset3, len3);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000EA2E File Offset: 0x0000CC2E
		private string DecodeString(byte[] buffer, int offset, int len)
		{
			if ((base.Flags & NtlmFlags.NegotiateUnicode) != (NtlmFlags)0)
			{
				return Encoding.Unicode.GetString(buffer, offset, len);
			}
			return Encoding.ASCII.GetString(buffer, offset, len);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000EA55 File Offset: 0x0000CC55
		private byte[] EncodeString(string text)
		{
			if (text == null)
			{
				return new byte[0];
			}
			if ((base.Flags & NtlmFlags.NegotiateUnicode) != (NtlmFlags)0)
			{
				return Encoding.Unicode.GetBytes(text);
			}
			return Encoding.ASCII.GetBytes(text);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public override byte[] GetBytes()
		{
			byte[] array = this.EncodeString(this._domain);
			byte[] array2 = this.EncodeString(this._username);
			byte[] array3 = this.EncodeString(this._host);
			byte[] lm;
			byte[] nt;
			if (this._type2 == null)
			{
				if (this._level != NtlmAuthLevel.LM_and_NTLM)
				{
					throw new InvalidOperationException("Refusing to use legacy-mode LM/NTLM authentication unless explicitly enabled using DefaultAuthLevel.");
				}
				using (ChallengeResponse challengeResponse = new ChallengeResponse(this._password, this._challenge))
				{
					lm = challengeResponse.LM;
					nt = challengeResponse.NT;
					goto IL_9B;
				}
			}
			ChallengeResponse2.Compute(this._type2, this._level, this._username, this._password, this._domain, out lm, out nt);
			IL_9B:
			int num = (lm != null) ? lm.Length : 0;
			int num2 = (nt != null) ? nt.Length : 0;
			byte[] array4 = base.PrepareMessage(64 + array.Length + array2.Length + array3.Length + num + num2);
			short num3 = (short)(64 + array.Length + array2.Length + array3.Length);
			array4[12] = (byte)num;
			array4[13] = 0;
			array4[14] = (byte)num;
			array4[15] = 0;
			array4[16] = (byte)num3;
			array4[17] = (byte)(num3 >> 8);
			short num4 = (short)((int)num3 + num);
			array4[20] = (byte)num2;
			array4[21] = (byte)(num2 >> 8);
			array4[22] = (byte)num2;
			array4[23] = (byte)(num2 >> 8);
			array4[24] = (byte)num4;
			array4[25] = (byte)(num4 >> 8);
			short num5 = (short)array.Length;
			short num6 = 64;
			array4[28] = (byte)num5;
			array4[29] = (byte)(num5 >> 8);
			array4[30] = array4[28];
			array4[31] = array4[29];
			array4[32] = (byte)num6;
			array4[33] = (byte)(num6 >> 8);
			short num7 = (short)array2.Length;
			short num8 = num6 + num5;
			array4[36] = (byte)num7;
			array4[37] = (byte)(num7 >> 8);
			array4[38] = array4[36];
			array4[39] = array4[37];
			array4[40] = (byte)num8;
			array4[41] = (byte)(num8 >> 8);
			short num9 = (short)array3.Length;
			short num10 = num8 + num7;
			array4[44] = (byte)num9;
			array4[45] = (byte)(num9 >> 8);
			array4[46] = array4[44];
			array4[47] = array4[45];
			array4[48] = (byte)num10;
			array4[49] = (byte)(num10 >> 8);
			short num11 = (short)array4.Length;
			array4[56] = (byte)num11;
			array4[57] = (byte)(num11 >> 8);
			int flags = (int)base.Flags;
			array4[60] = (byte)flags;
			array4[61] = (byte)((uint)flags >> 8);
			array4[62] = (byte)((uint)flags >> 16);
			array4[63] = (byte)((uint)flags >> 24);
			Buffer.BlockCopy(array, 0, array4, (int)num6, array.Length);
			Buffer.BlockCopy(array2, 0, array4, (int)num8, array2.Length);
			Buffer.BlockCopy(array3, 0, array4, (int)num10, array3.Length);
			if (lm != null)
			{
				Buffer.BlockCopy(lm, 0, array4, (int)num3, lm.Length);
				Array.Clear(lm, 0, lm.Length);
			}
			Buffer.BlockCopy(nt, 0, array4, (int)num4, nt.Length);
			Array.Clear(nt, 0, nt.Length);
			return array4;
		}

		// Token: 0x0400011A RID: 282
		private NtlmAuthLevel _level;

		// Token: 0x0400011B RID: 283
		private byte[] _challenge;

		// Token: 0x0400011C RID: 284
		private string _host;

		// Token: 0x0400011D RID: 285
		private string _domain;

		// Token: 0x0400011E RID: 286
		private string _username;

		// Token: 0x0400011F RID: 287
		private string _password;

		// Token: 0x04000120 RID: 288
		private Type2Message _type2;

		// Token: 0x04000121 RID: 289
		private byte[] _lm;

		// Token: 0x04000122 RID: 290
		private byte[] _nt;

		// Token: 0x04000123 RID: 291
		internal const string LegacyAPIWarning = "Use of this API is highly discouraged, it selects legacy-mode LM/NTLM authentication, which sends your password in very weak encryption over the wire even if the server supports the more secure NTLMv2 / NTLMv2 Session. You need to use the new `Type3Message (Type2Message)' constructor to use the more secure NTLMv2 / NTLMv2 Session authentication modes. These require the Type 2 message from the server to compute the response.";
	}
}
