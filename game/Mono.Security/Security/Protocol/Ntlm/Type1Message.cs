using System;
using System.Globalization;
using System.Text;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000031 RID: 49
	public class Type1Message : MessageBase
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0000E165 File Offset: 0x0000C365
		public Type1Message() : base(1)
		{
			this._domain = Environment.UserDomainName;
			this._host = Environment.MachineName;
			base.Flags = (NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateOem | NtlmFlags.RequestTarget | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateDomainSupplied | NtlmFlags.NegotiateWorkstationSupplied | NtlmFlags.NegotiateAlwaysSign);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000E18F File Offset: 0x0000C38F
		public Type1Message(byte[] message) : base(1)
		{
			this.Decode(message);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000E19F File Offset: 0x0000C39F
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000E1F9 File Offset: 0x0000C3F9
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000E204 File Offset: 0x0000C404
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

		// Token: 0x06000216 RID: 534 RVA: 0x0000E258 File Offset: 0x0000C458
		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			base.Flags = (NtlmFlags)BitConverterLE.ToUInt32(message, 12);
			int count = (int)BitConverterLE.ToUInt16(message, 16);
			int index = (int)BitConverterLE.ToUInt16(message, 20);
			this._domain = Encoding.ASCII.GetString(message, index, count);
			int count2 = (int)BitConverterLE.ToUInt16(message, 24);
			this._host = Encoding.ASCII.GetString(message, 32, count2);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public override byte[] GetBytes()
		{
			short num = (short)this._domain.Length;
			short num2 = (short)this._host.Length;
			byte[] array = base.PrepareMessage((int)(32 + num + num2));
			array[12] = (byte)base.Flags;
			array[13] = (byte)(base.Flags >> 8);
			array[14] = (byte)(base.Flags >> 16);
			array[15] = (byte)(base.Flags >> 24);
			short num3 = 32 + num2;
			array[16] = (byte)num;
			array[17] = (byte)(num >> 8);
			array[18] = array[16];
			array[19] = array[17];
			array[20] = (byte)num3;
			array[21] = (byte)(num3 >> 8);
			array[24] = (byte)num2;
			array[25] = (byte)(num2 >> 8);
			array[26] = array[24];
			array[27] = array[25];
			array[28] = 32;
			array[29] = 0;
			byte[] bytes = Encoding.ASCII.GetBytes(this._host.ToUpper(CultureInfo.InvariantCulture));
			Buffer.BlockCopy(bytes, 0, array, 32, bytes.Length);
			byte[] bytes2 = Encoding.ASCII.GetBytes(this._domain.ToUpper(CultureInfo.InvariantCulture));
			Buffer.BlockCopy(bytes2, 0, array, (int)num3, bytes2.Length);
			return array;
		}

		// Token: 0x04000115 RID: 277
		private string _host;

		// Token: 0x04000116 RID: 278
		private string _domain;
	}
}
