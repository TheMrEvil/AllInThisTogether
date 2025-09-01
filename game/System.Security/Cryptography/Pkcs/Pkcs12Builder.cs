using System;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200008E RID: 142
	public sealed class Pkcs12Builder
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool IsSealed
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void AddSafeContentsEncrypted(Pkcs12SafeContents safeContents, byte[] passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void AddSafeContentsEncrypted(Pkcs12SafeContents safeContents, ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void AddSafeContentsEncrypted(Pkcs12SafeContents safeContents, ReadOnlySpan<char> password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void AddSafeContentsEncrypted(Pkcs12SafeContents safeContents, string password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void AddSafeContentsUnencrypted(Pkcs12SafeContents safeContents)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000149B5 File Offset: 0x00012BB5
		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void SealWithMac(ReadOnlySpan<char> password, HashAlgorithmName hashAlgorithm, int iterationCount)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void SealWithMac(string password, HashAlgorithmName hashAlgorithm, int iterationCount)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void SealWithoutIntegrity()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00002145 File Offset: 0x00000345
		public Pkcs12Builder()
		{
		}
	}
}
