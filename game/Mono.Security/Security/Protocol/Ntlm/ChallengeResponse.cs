using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.Protocol.Ntlm
{
	// Token: 0x0200002B RID: 43
	[Obsolete("Use of this API is highly discouraged, it selects legacy-mode LM/NTLM authentication, which sends your password in very weak encryption over the wire even if the server supports the more secure NTLMv2 / NTLMv2 Session. You need to use the new `Type3Message (Type2Message)' constructor to use the more secure NTLMv2 / NTLMv2 Session authentication modes. These require the Type 2 message from the server to compute the response.")]
	public class ChallengeResponse : IDisposable
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000D666 File Offset: 0x0000B866
		public ChallengeResponse()
		{
			this._disposed = false;
			this._lmpwd = new byte[21];
			this._ntpwd = new byte[21];
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D68F File Offset: 0x0000B88F
		public ChallengeResponse(string password, byte[] challenge) : this()
		{
			this.Password = password;
			this.Challenge = challenge;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		~ChallengeResponse()
		{
			if (!this._disposed)
			{
				this.Dispose();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000D6DC File Offset: 0x0000B8DC
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public string Password
		{
			get
			{
				return null;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				DES des = DES.Create();
				des.Mode = CipherMode.ECB;
				if (value == null || value.Length < 1)
				{
					Buffer.BlockCopy(ChallengeResponse.nullEncMagic, 0, this._lmpwd, 0, 8);
				}
				else
				{
					des.Key = this.PasswordToKey(value, 0);
					des.CreateEncryptor().TransformBlock(ChallengeResponse.magic, 0, 8, this._lmpwd, 0);
				}
				if (value == null || value.Length < 8)
				{
					Buffer.BlockCopy(ChallengeResponse.nullEncMagic, 0, this._lmpwd, 8, 8);
				}
				else
				{
					des.Key = this.PasswordToKey(value, 7);
					des.CreateEncryptor().TransformBlock(ChallengeResponse.magic, 0, 8, this._lmpwd, 8);
				}
				HashAlgorithm hashAlgorithm = MD4.Create();
				byte[] array = (value == null) ? new byte[0] : Encoding.Unicode.GetBytes(value);
				byte[] array2 = hashAlgorithm.ComputeHash(array);
				Buffer.BlockCopy(array2, 0, this._ntpwd, 0, 16);
				Array.Clear(array, 0, array.Length);
				Array.Clear(array2, 0, array2.Length);
				des.Clear();
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000D7EF File Offset: 0x0000B9EF
		public byte[] Challenge
		{
			get
			{
				return null;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Challenge");
				}
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				this._challenge = (byte[])value.Clone();
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000D823 File Offset: 0x0000BA23
		public byte[] LM
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				return this.GetResponse(this._lmpwd);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000D844 File Offset: 0x0000BA44
		public byte[] NT
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				return this.GetResponse(this._ntpwd);
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000D865 File Offset: 0x0000BA65
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000D874 File Offset: 0x0000BA74
		private void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				Array.Clear(this._lmpwd, 0, this._lmpwd.Length);
				Array.Clear(this._ntpwd, 0, this._ntpwd.Length);
				if (this._challenge != null)
				{
					Array.Clear(this._challenge, 0, this._challenge.Length);
				}
				this._disposed = true;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		private byte[] GetResponse(byte[] pwd)
		{
			byte[] array = new byte[24];
			DES des = DES.Create();
			des.Mode = CipherMode.ECB;
			des.Key = this.PrepareDESKey(pwd, 0);
			des.CreateEncryptor().TransformBlock(this._challenge, 0, 8, array, 0);
			des.Key = this.PrepareDESKey(pwd, 7);
			des.CreateEncryptor().TransformBlock(this._challenge, 0, 8, array, 8);
			des.Key = this.PrepareDESKey(pwd, 14);
			des.CreateEncryptor().TransformBlock(this._challenge, 0, 8, array, 16);
			return array;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000D964 File Offset: 0x0000BB64
		private byte[] PrepareDESKey(byte[] key56bits, int position)
		{
			return new byte[]
			{
				key56bits[position],
				(byte)((int)key56bits[position] << 7 | key56bits[position + 1] >> 1),
				(byte)((int)key56bits[position + 1] << 6 | key56bits[position + 2] >> 2),
				(byte)((int)key56bits[position + 2] << 5 | key56bits[position + 3] >> 3),
				(byte)((int)key56bits[position + 3] << 4 | key56bits[position + 4] >> 4),
				(byte)((int)key56bits[position + 4] << 3 | key56bits[position + 5] >> 5),
				(byte)((int)key56bits[position + 5] << 2 | key56bits[position + 6] >> 6),
				(byte)(key56bits[position + 6] << 1)
			};
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		private byte[] PasswordToKey(string password, int position)
		{
			byte[] array = new byte[7];
			int charCount = Math.Min(password.Length - position, 7);
			Encoding.ASCII.GetBytes(password.ToUpper(CultureInfo.CurrentCulture), position, charCount, array, 0);
			byte[] result = this.PrepareDESKey(array, 0);
			Array.Clear(array, 0, array.Length);
			return result;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000DA47 File Offset: 0x0000BC47
		// Note: this type is marked as 'beforefieldinit'.
		static ChallengeResponse()
		{
		}

		// Token: 0x040000F9 RID: 249
		private static byte[] magic = new byte[]
		{
			75,
			71,
			83,
			33,
			64,
			35,
			36,
			37
		};

		// Token: 0x040000FA RID: 250
		private static byte[] nullEncMagic = new byte[]
		{
			170,
			211,
			180,
			53,
			181,
			20,
			4,
			238
		};

		// Token: 0x040000FB RID: 251
		private bool _disposed;

		// Token: 0x040000FC RID: 252
		private byte[] _challenge;

		// Token: 0x040000FD RID: 253
		private byte[] _lmpwd;

		// Token: 0x040000FE RID: 254
		private byte[] _ntpwd;
	}
}
