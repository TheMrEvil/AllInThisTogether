using System;
using System.Security.Cryptography;
using System.Text;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x02000032 RID: 50
	public class Type2Message : MessageBase
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000E3D5 File Offset: 0x0000C5D5
		public Type2Message() : base(2)
		{
			this._nonce = new byte[8];
			RandomNumberGenerator.Create().GetBytes(this._nonce);
			base.Flags = (NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000E405 File Offset: 0x0000C605
		public Type2Message(byte[] message) : base(2)
		{
			this._nonce = new byte[8];
			this.Decode(message);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000E424 File Offset: 0x0000C624
		~Type2Message()
		{
			if (this._nonce != null)
			{
				Array.Clear(this._nonce, 0, this._nonce.Length);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000E468 File Offset: 0x0000C668
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000E47A File Offset: 0x0000C67A
		public byte[] Nonce
		{
			get
			{
				return (byte[])this._nonce.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Nonce");
				}
				if (value.Length != 8)
				{
					throw new ArgumentException(Locale.GetText("Invalid Nonce Length (should be 8 bytes)."), "Nonce");
				}
				this._nonce = (byte[])value.Clone();
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000E4B6 File Offset: 0x0000C6B6
		public string TargetName
		{
			get
			{
				return this._targetName;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000E4BE File Offset: 0x0000C6BE
		public byte[] TargetInfo
		{
			get
			{
				return (byte[])this._targetInfo.Clone();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			base.Flags = (NtlmFlags)BitConverterLE.ToUInt32(message, 20);
			Buffer.BlockCopy(message, 24, this._nonce, 0, 8);
			ushort num = BitConverterLE.ToUInt16(message, 12);
			ushort index = BitConverterLE.ToUInt16(message, 16);
			if (num > 0)
			{
				if ((base.Flags & NtlmFlags.NegotiateOem) != (NtlmFlags)0)
				{
					this._targetName = Encoding.ASCII.GetString(message, (int)index, (int)num);
				}
				else
				{
					this._targetName = Encoding.Unicode.GetString(message, (int)index, (int)num);
				}
			}
			if (message.Length >= 48)
			{
				ushort num2 = BitConverterLE.ToUInt16(message, 40);
				ushort srcOffset = BitConverterLE.ToUInt16(message, 44);
				if (num2 > 0)
				{
					this._targetInfo = new byte[(int)num2];
					Buffer.BlockCopy(message, (int)srcOffset, this._targetInfo, 0, (int)num2);
				}
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000E584 File Offset: 0x0000C784
		public override byte[] GetBytes()
		{
			byte[] array = base.PrepareMessage(40);
			short num = (short)array.Length;
			array[16] = (byte)num;
			array[17] = (byte)(num >> 8);
			array[20] = (byte)base.Flags;
			array[21] = (byte)(base.Flags >> 8);
			array[22] = (byte)(base.Flags >> 16);
			array[23] = (byte)(base.Flags >> 24);
			Buffer.BlockCopy(this._nonce, 0, array, 24, this._nonce.Length);
			return array;
		}

		// Token: 0x04000117 RID: 279
		private byte[] _nonce;

		// Token: 0x04000118 RID: 280
		private string _targetName;

		// Token: 0x04000119 RID: 281
		private byte[] _targetInfo;
	}
}
