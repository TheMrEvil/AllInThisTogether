using System;
using System.Text;

namespace Mono.Net.Dns
{
	// Token: 0x020000B5 RID: 181
	internal class DnsHeader
	{
		// Token: 0x0600037D RID: 893 RVA: 0x0000A8FA File Offset: 0x00008AFA
		public DnsHeader(byte[] bytes) : this(bytes, 0)
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A904 File Offset: 0x00008B04
		public DnsHeader(byte[] bytes, int offset) : this(new ArraySegment<byte>(bytes, offset, 12))
		{
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000A915 File Offset: 0x00008B15
		public DnsHeader(ArraySegment<byte> segment)
		{
			if (segment.Count != 12)
			{
				throw new ArgumentException("Count must be 12", "segment");
			}
			this.bytes = segment;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000A940 File Offset: 0x00008B40
		public void Clear()
		{
			for (int i = 0; i < 12; i++)
			{
				this.bytes.Array[i + this.bytes.Offset] = 0;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000A974 File Offset: 0x00008B74
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		public ushort ID
		{
			get
			{
				return (ushort)((int)this.bytes.Array[this.bytes.Offset] * 256 + (int)this.bytes.Array[this.bytes.Offset + 1]);
			}
			set
			{
				this.bytes.Array[this.bytes.Offset] = (byte)((value & 65280) >> 8);
				this.bytes.Array[this.bytes.Offset + 1] = (byte)(value & 255);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000A9FF File Offset: 0x00008BFF
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000AA24 File Offset: 0x00008C24
		public bool IsQuery
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 128) > 0;
			}
			set
			{
				if (!value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 128;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 127;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000AA80 File Offset: 0x00008C80
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		public DnsOpCode OpCode
		{
			get
			{
				return (DnsOpCode)((this.bytes.Array[2 + this.bytes.Offset] & 120) >> 3);
			}
			set
			{
				if (!Enum.IsDefined(typeof(DnsOpCode), value))
				{
					throw new ArgumentOutOfRangeException("value", "Invalid DnsOpCode value");
				}
				int num = (int)((int)value << 3);
				int num2 = (int)(this.bytes.Array[2 + this.bytes.Offset] & 135);
				num |= num2;
				this.bytes.Array[2 + this.bytes.Offset] = (byte)num;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000AB1D File Offset: 0x00008D1D
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000AB40 File Offset: 0x00008D40
		public bool AuthoritativeAnswer
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 4) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 4;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 251;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000AB9B File Offset: 0x00008D9B
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000ABBC File Offset: 0x00008DBC
		public bool Truncation
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 2) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 2;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 253;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000AC17 File Offset: 0x00008E17
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000AC38 File Offset: 0x00008E38
		public bool RecursionDesired
		{
			get
			{
				return (this.bytes.Array[2 + this.bytes.Offset] & 1) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 2 + this.bytes.Offset;
					array[num] |= 1;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 2 + this.bytes.Offset;
				array2[num2] &= 254;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000AC93 File Offset: 0x00008E93
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		public bool RecursionAvailable
		{
			get
			{
				return (this.bytes.Array[3 + this.bytes.Offset] & 128) > 0;
			}
			set
			{
				if (value)
				{
					byte[] array = this.bytes.Array;
					int num = 3 + this.bytes.Offset;
					array[num] |= 128;
					return;
				}
				byte[] array2 = this.bytes.Array;
				int num2 = 3 + this.bytes.Offset;
				array2[num2] &= 127;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000AD14 File Offset: 0x00008F14
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000AD34 File Offset: 0x00008F34
		public int ZReserved
		{
			get
			{
				return (this.bytes.Array[3 + this.bytes.Offset] & 112) >> 4;
			}
			set
			{
				if (value < 0 || value > 7)
				{
					throw new ArgumentOutOfRangeException("value", "Must be between 0 and 7");
				}
				byte[] array = this.bytes.Array;
				int num = 3 + this.bytes.Offset;
				array[num] &= 143;
				byte[] array2 = this.bytes.Array;
				int num2 = 3 + this.bytes.Offset;
				array2[num2] |= (byte)(value << 4 & 112);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000ADA9 File Offset: 0x00008FA9
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public DnsRCode RCode
		{
			get
			{
				return (DnsRCode)(this.bytes.Array[3 + this.bytes.Offset] & 15);
			}
			set
			{
				if (value < DnsRCode.NoError || value > (DnsRCode)15)
				{
					throw new ArgumentOutOfRangeException("value", "Must be between 0 and 15");
				}
				byte[] array = this.bytes.Array;
				int num = 3 + this.bytes.Offset;
				array[num] &= 15;
				byte[] array2 = this.bytes.Array;
				int num2 = 3 + this.bytes.Offset;
				array2[num2] |= (byte)value;
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000AE38 File Offset: 0x00009038
		private static ushort GetUInt16(byte[] bytes, int offset)
		{
			return (ushort)((int)bytes[offset] * 256 + (int)bytes[offset + 1]);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000AE4A File Offset: 0x0000904A
		private static void SetUInt16(byte[] bytes, int offset, ushort val)
		{
			bytes[offset] = (byte)((val & 65280) >> 8);
			bytes[offset + 1] = (byte)(val & 255);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000AE66 File Offset: 0x00009066
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000AE79 File Offset: 0x00009079
		public ushort QuestionCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 4);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 4, value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000AE8D File Offset: 0x0000908D
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000AEA0 File Offset: 0x000090A0
		public ushort AnswerCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 6);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 6, value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000AEB4 File Offset: 0x000090B4
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000AEC7 File Offset: 0x000090C7
		public ushort AuthorityCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 8);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 8, value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000AEDB File Offset: 0x000090DB
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000AEEF File Offset: 0x000090EF
		public ushort AdditionalCount
		{
			get
			{
				return DnsHeader.GetUInt16(this.bytes.Array, 10);
			}
			set
			{
				DnsHeader.SetUInt16(this.bytes.Array, 10, value);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000AF04 File Offset: 0x00009104
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("ID: {0} QR: {1} Opcode: {2} AA: {3} TC: {4} RD: {5} RA: {6} \r\nRCode: {7} ", new object[]
			{
				this.ID,
				this.IsQuery,
				this.OpCode,
				this.AuthoritativeAnswer,
				this.Truncation,
				this.RecursionDesired,
				this.RecursionAvailable,
				this.RCode
			});
			stringBuilder.AppendFormat("Q: {0} A: {1} NS: {2} AR: {3}\r\n", new object[]
			{
				this.QuestionCount,
				this.AnswerCount,
				this.AuthorityCount,
				this.AdditionalCount
			});
			return stringBuilder.ToString();
		}

		// Token: 0x040002A5 RID: 677
		public const int DnsHeaderLength = 12;

		// Token: 0x040002A6 RID: 678
		private ArraySegment<byte> bytes;
	}
}
