using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace IKVM.Reflection
{
	// Token: 0x02000059 RID: 89
	public sealed class StrongNameKeyPair
	{
		// Token: 0x06000467 RID: 1127 RVA: 0x0000DCB3 File Offset: 0x0000BEB3
		public StrongNameKeyPair(string keyPairContainer)
		{
			if (keyPairContainer == null)
			{
				throw new ArgumentNullException("keyPairContainer");
			}
			if (Universe.MonoRuntime && Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				throw new NotSupportedException("IKVM.Reflection does not support key containers when running on Mono");
			}
			this.keyPairContainer = keyPairContainer;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000DCEF File Offset: 0x0000BEEF
		public StrongNameKeyPair(byte[] keyPairArray)
		{
			if (keyPairArray == null)
			{
				throw new ArgumentNullException("keyPairArray");
			}
			this.keyPairArray = (byte[])keyPairArray.Clone();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000DD16 File Offset: 0x0000BF16
		public StrongNameKeyPair(FileStream keyPairFile) : this(StrongNameKeyPair.ReadAllBytes(keyPairFile))
		{
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000DD24 File Offset: 0x0000BF24
		private static byte[] ReadAllBytes(FileStream keyPairFile)
		{
			if (keyPairFile == null)
			{
				throw new ArgumentNullException("keyPairFile");
			}
			byte[] array = new byte[keyPairFile.Length - keyPairFile.Position];
			keyPairFile.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000DD60 File Offset: 0x0000BF60
		public byte[] PublicKey
		{
			get
			{
				if (Universe.MonoRuntime)
				{
					return this.MonoGetPublicKey();
				}
				byte[] result;
				using (RSACryptoServiceProvider rsacryptoServiceProvider = this.CreateRSA())
				{
					byte[] array = rsacryptoServiceProvider.ExportCspBlob(false);
					byte[] array2 = new byte[12 + array.Length];
					Buffer.BlockCopy(array, 0, array2, 12, array.Length);
					array2[1] = 36;
					array2[4] = 4;
					array2[5] = 128;
					array2[8] = (byte)array.Length;
					array2[9] = (byte)(array.Length >> 8);
					array2[10] = (byte)(array.Length >> 16);
					array2[11] = (byte)(array.Length >> 24);
					result = array2;
				}
				return result;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		internal RSACryptoServiceProvider CreateRSA()
		{
			RSACryptoServiceProvider result;
			try
			{
				if (this.keyPairArray != null)
				{
					RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
					rsacryptoServiceProvider.ImportCspBlob(this.keyPairArray);
					result = rsacryptoServiceProvider;
				}
				else
				{
					CspParameters cspParameters = new CspParameters();
					cspParameters.KeyContainerName = this.keyPairContainer;
					if (!Universe.MonoRuntime)
					{
						cspParameters.Flags = (CspProviderFlags.UseMachineKeyStore | CspProviderFlags.UseExistingKey);
						cspParameters.KeyNumber = 2;
					}
					result = new RSACryptoServiceProvider(cspParameters);
				}
			}
			catch
			{
				throw new ArgumentException("Unable to obtain public key for StrongNameKeyPair.");
			}
			return result;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000DE74 File Offset: 0x0000C074
		[MethodImpl(MethodImplOptions.NoInlining)]
		private byte[] MonoGetPublicKey()
		{
			if (this.keyPairArray == null)
			{
				return new StrongNameKeyPair(this.keyPairContainer).PublicKey;
			}
			return new StrongNameKeyPair(this.keyPairArray).PublicKey;
		}

		// Token: 0x040001F9 RID: 505
		private readonly byte[] keyPairArray;

		// Token: 0x040001FA RID: 506
		private readonly string keyPairContainer;
	}
}
