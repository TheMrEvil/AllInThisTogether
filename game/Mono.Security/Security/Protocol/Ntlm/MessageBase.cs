using System;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x0200002D RID: 45
	public abstract class MessageBase
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0000E008 File Offset: 0x0000C208
		protected MessageBase(int messageType)
		{
			this._type = messageType;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000E017 File Offset: 0x0000C217
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000E01F File Offset: 0x0000C21F
		public NtlmFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000E028 File Offset: 0x0000C228
		public int Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000E030 File Offset: 0x0000C230
		protected byte[] PrepareMessage(int messageSize)
		{
			byte[] array = new byte[messageSize];
			Buffer.BlockCopy(MessageBase.header, 0, array, 0, 8);
			array[8] = (byte)this._type;
			array[9] = (byte)(this._type >> 8);
			array[10] = (byte)(this._type >> 16);
			array[11] = (byte)(this._type >> 24);
			return array;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000E088 File Offset: 0x0000C288
		protected virtual void Decode(byte[] message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (message.Length < 12)
			{
				string text = Locale.GetText("Minimum message length is 12 bytes.");
				throw new ArgumentOutOfRangeException("message", message.Length, text);
			}
			if (!this.CheckHeader(message))
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid Type{0} message."), this._type), "message");
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		protected bool CheckHeader(byte[] message)
		{
			for (int i = 0; i < MessageBase.header.Length; i++)
			{
				if (message[i] != MessageBase.header[i])
				{
					return false;
				}
			}
			return (ulong)BitConverterLE.ToUInt32(message, 8) == (ulong)((long)this._type);
		}

		// Token: 0x0600020B RID: 523
		public abstract byte[] GetBytes();

		// Token: 0x0600020C RID: 524 RVA: 0x0000E136 File Offset: 0x0000C336
		// Note: this type is marked as 'beforefieldinit'.
		static MessageBase()
		{
		}

		// Token: 0x04000101 RID: 257
		private static byte[] header = new byte[]
		{
			78,
			84,
			76,
			77,
			83,
			83,
			80,
			0
		};

		// Token: 0x04000102 RID: 258
		private int _type;

		// Token: 0x04000103 RID: 259
		private NtlmFlags _flags;
	}
}
