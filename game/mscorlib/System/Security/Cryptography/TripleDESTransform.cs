using System;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x020004D1 RID: 1233
	internal class TripleDESTransform : SymmetricTransform
	{
		// Token: 0x06003151 RID: 12625 RVA: 0x000B6AAC File Offset: 0x000B4CAC
		public TripleDESTransform(TripleDES algo, bool encryption, byte[] key, byte[] iv) : base(algo, encryption, iv)
		{
			if (key == null)
			{
				key = TripleDESTransform.GetStrongKey();
			}
			if (TripleDES.IsWeakKey(key))
			{
				throw new CryptographicException(Locale.GetText("This is a known weak key."));
			}
			byte[] array = new byte[8];
			byte[] array2 = new byte[8];
			byte[] array3 = new byte[8];
			DES symmAlgo = DES.Create();
			Buffer.BlockCopy(key, 0, array, 0, 8);
			Buffer.BlockCopy(key, 8, array2, 0, 8);
			if (key.Length == 16)
			{
				Buffer.BlockCopy(key, 0, array3, 0, 8);
			}
			else
			{
				Buffer.BlockCopy(key, 16, array3, 0, 8);
			}
			if (encryption || algo.Mode == CipherMode.CFB)
			{
				this.E1 = new DESTransform(symmAlgo, true, array, iv);
				this.D2 = new DESTransform(symmAlgo, false, array2, iv);
				this.E3 = new DESTransform(symmAlgo, true, array3, iv);
				return;
			}
			this.D1 = new DESTransform(symmAlgo, false, array3, iv);
			this.E2 = new DESTransform(symmAlgo, true, array2, iv);
			this.D3 = new DESTransform(symmAlgo, false, array, iv);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000B6BA0 File Offset: 0x000B4DA0
		protected override void ECB(byte[] input, byte[] output)
		{
			DESTransform.Permutation(input, output, DESTransform.ipTab, false);
			if (this.encrypt)
			{
				this.E1.ProcessBlock(output, output);
				this.D2.ProcessBlock(output, output);
				this.E3.ProcessBlock(output, output);
			}
			else
			{
				this.D1.ProcessBlock(output, output);
				this.E2.ProcessBlock(output, output);
				this.D3.ProcessBlock(output, output);
			}
			DESTransform.Permutation(output, output, DESTransform.fpTab, true);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000B6C20 File Offset: 0x000B4E20
		internal static byte[] GetStrongKey()
		{
			int size = DESTransform.BLOCK_BYTE_SIZE * 3;
			byte[] array = KeyBuilder.Key(size);
			while (TripleDES.IsWeakKey(array))
			{
				array = KeyBuilder.Key(size);
			}
			return array;
		}

		// Token: 0x04002278 RID: 8824
		private DESTransform E1;

		// Token: 0x04002279 RID: 8825
		private DESTransform D2;

		// Token: 0x0400227A RID: 8826
		private DESTransform E3;

		// Token: 0x0400227B RID: 8827
		private DESTransform D1;

		// Token: 0x0400227C RID: 8828
		private DESTransform E2;

		// Token: 0x0400227D RID: 8829
		private DESTransform D3;
	}
}
