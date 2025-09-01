using System;
using System.Text;

namespace System.Net.Security
{
	// Token: 0x02000852 RID: 2130
	public readonly struct SslApplicationProtocol : IEquatable<SslApplicationProtocol>
	{
		// Token: 0x060043A2 RID: 17314 RVA: 0x000EC060 File Offset: 0x000EA260
		internal SslApplicationProtocol(byte[] protocol, bool copy)
		{
			if (protocol == null)
			{
				throw new ArgumentNullException("protocol");
			}
			if (protocol.Length == 0 || protocol.Length > 255)
			{
				throw new ArgumentException("The application protocol value is invalid.", "protocol");
			}
			if (copy)
			{
				byte[] array = new byte[protocol.Length];
				Array.Copy(protocol, 0, array, 0, protocol.Length);
				this._readOnlyProtocol = new ReadOnlyMemory<byte>(array);
				return;
			}
			this._readOnlyProtocol = new ReadOnlyMemory<byte>(protocol);
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x000EC0CA File Offset: 0x000EA2CA
		public SslApplicationProtocol(byte[] protocol)
		{
			this = new SslApplicationProtocol(protocol, true);
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x000EC0D4 File Offset: 0x000EA2D4
		public SslApplicationProtocol(string protocol)
		{
			this = new SslApplicationProtocol(SslApplicationProtocol.s_utf8.GetBytes(protocol), false);
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x000EC0E8 File Offset: 0x000EA2E8
		public ReadOnlyMemory<byte> Protocol
		{
			get
			{
				return this._readOnlyProtocol;
			}
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x000EC0F0 File Offset: 0x000EA2F0
		public bool Equals(SslApplicationProtocol other)
		{
			return this._readOnlyProtocol.Length == other._readOnlyProtocol.Length && ((this._readOnlyProtocol.IsEmpty && other._readOnlyProtocol.IsEmpty) || this._readOnlyProtocol.Span.SequenceEqual(other._readOnlyProtocol.Span));
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x000EC154 File Offset: 0x000EA354
		public override bool Equals(object obj)
		{
			if (obj is SslApplicationProtocol)
			{
				SslApplicationProtocol other = (SslApplicationProtocol)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x000EC17C File Offset: 0x000EA37C
		public unsafe override int GetHashCode()
		{
			if (this._readOnlyProtocol.Length == 0)
			{
				return 0;
			}
			int num = 0;
			ReadOnlySpan<byte> span = this._readOnlyProtocol.Span;
			for (int i = 0; i < this._readOnlyProtocol.Length; i++)
			{
				num = ((num << 5) + num ^ (int)(*span[i]));
			}
			return num;
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x000EC1D0 File Offset: 0x000EA3D0
		public unsafe override string ToString()
		{
			string result;
			try
			{
				if (this._readOnlyProtocol.Length == 0)
				{
					result = null;
				}
				else
				{
					result = SslApplicationProtocol.s_utf8.GetString(this._readOnlyProtocol.Span);
				}
			}
			catch
			{
				int num = this._readOnlyProtocol.Length * 5;
				char[] array = new char[num];
				int num2 = 0;
				ReadOnlySpan<byte> span = this._readOnlyProtocol.Span;
				for (int i = 0; i < num; i += 5)
				{
					byte a = *span[num2++];
					array[i] = '0';
					array[i + 1] = 'x';
					int i2;
					array[i + 2] = SslApplicationProtocol.GetHexValue(Math.DivRem((int)a, 16, out i2));
					array[i + 3] = SslApplicationProtocol.GetHexValue(i2);
					array[i + 4] = ' ';
				}
				result = new string(array, 0, num - 1);
			}
			return result;
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x000EC2A8 File Offset: 0x000EA4A8
		private static char GetHexValue(int i)
		{
			if (i < 10)
			{
				return (char)(i + 48);
			}
			return (char)(i - 10 + 97);
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x000EC2BD File Offset: 0x000EA4BD
		public static bool operator ==(SslApplicationProtocol left, SslApplicationProtocol right)
		{
			return left.Equals(right);
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x000EC2C7 File Offset: 0x000EA4C7
		public static bool operator !=(SslApplicationProtocol left, SslApplicationProtocol right)
		{
			return !(left == right);
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x000EC2D4 File Offset: 0x000EA4D4
		// Note: this type is marked as 'beforefieldinit'.
		static SslApplicationProtocol()
		{
		}

		// Token: 0x040028EA RID: 10474
		private readonly ReadOnlyMemory<byte> _readOnlyProtocol;

		// Token: 0x040028EB RID: 10475
		private static readonly Encoding s_utf8 = Encoding.GetEncoding(Encoding.UTF8.CodePage, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);

		// Token: 0x040028EC RID: 10476
		public static readonly SslApplicationProtocol Http2 = new SslApplicationProtocol(new byte[]
		{
			104,
			50
		}, false);

		// Token: 0x040028ED RID: 10477
		public static readonly SslApplicationProtocol Http11 = new SslApplicationProtocol(new byte[]
		{
			104,
			116,
			116,
			112,
			47,
			49,
			46,
			49
		}, false);
	}
}
