using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000095 RID: 149
	public sealed class Pkcs12SafeContents
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12ConfidentialityMode ConfidentialityMode
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool IsReadOnly
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12CertBag AddCertificate(X509Certificate2 certificate)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12KeyBag AddKeyUnencrypted(AsymmetricAlgorithm key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12SafeContentsBag AddNestedContents(Pkcs12SafeContents safeContents)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void AddSafeBag(Pkcs12SafeBag safeBag)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12SecretBag AddSecret(Oid secretType, ReadOnlyMemory<byte> secretValue)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12ShroudedKeyBag AddShroudedKey(AsymmetricAlgorithm key, byte[] passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12ShroudedKeyBag AddShroudedKey(AsymmetricAlgorithm key, ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12ShroudedKeyBag AddShroudedKey(AsymmetricAlgorithm key, ReadOnlySpan<char> password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12ShroudedKeyBag AddShroudedKey(AsymmetricAlgorithm key, string password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void Decrypt(byte[] passwordBytes)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void Decrypt(ReadOnlySpan<byte> passwordBytes)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void Decrypt(ReadOnlySpan<char> password)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000149B5 File Offset: 0x00012BB5
		public void Decrypt(string password)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000149B5 File Offset: 0x00012BB5
		public IEnumerable<Pkcs12SafeBag> GetBags()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00002145 File Offset: 0x00000345
		public Pkcs12SafeContents()
		{
		}
	}
}
