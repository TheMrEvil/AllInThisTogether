using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes the <see cref="T:System.Security.Cryptography.SHA1" /> hash for the input data using the managed library.</summary>
	// Token: 0x020004B1 RID: 1201
	[ComVisible(true)]
	public class SHA1Managed : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1Managed" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">This class is not compliant with the FIPS algorithm.</exception>
		// Token: 0x0600302F RID: 12335 RVA: 0x000AECCC File Offset: 0x000ACECC
		public SHA1Managed()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				throw new InvalidOperationException(Environment.GetResourceString("This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms."));
			}
			this._stateSHA1 = new uint[5];
			this._buffer = new byte[64];
			this._expandedBuffer = new uint[80];
			this.InitializeState();
		}

		/// <summary>Initializes an instance of <see cref="T:System.Security.Cryptography.SHA1Managed" />.</summary>
		// Token: 0x06003030 RID: 12336 RVA: 0x000AED22 File Offset: 0x000ACF22
		public override void Initialize()
		{
			this.InitializeState();
			Array.Clear(this._buffer, 0, this._buffer.Length);
			Array.Clear(this._expandedBuffer, 0, this._expandedBuffer.Length);
		}

		/// <summary>Routes data written to the object into the <see cref="T:System.Security.Cryptography.SHA1Managed" /> hash algorithm for computing the hash.</summary>
		/// <param name="rgb">The input data.</param>
		/// <param name="ibStart">The offset into the byte array from which to begin using data.</param>
		/// <param name="cbSize">The number of bytes in the array to use as data.</param>
		// Token: 0x06003031 RID: 12337 RVA: 0x000AED52 File Offset: 0x000ACF52
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this._HashData(rgb, ibStart, cbSize);
		}

		/// <summary>Returns the computed <see cref="T:System.Security.Cryptography.SHA1" /> hash value after all data has been written to the object.</summary>
		/// <returns>The computed hash code.</returns>
		// Token: 0x06003032 RID: 12338 RVA: 0x000AED5D File Offset: 0x000ACF5D
		protected override byte[] HashFinal()
		{
			return this._EndHash();
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000AED68 File Offset: 0x000ACF68
		private void InitializeState()
		{
			this._count = 0L;
			this._stateSHA1[0] = 1732584193U;
			this._stateSHA1[1] = 4023233417U;
			this._stateSHA1[2] = 2562383102U;
			this._stateSHA1[3] = 271733878U;
			this._stateSHA1[4] = 3285377520U;
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000AEDC0 File Offset: 0x000ACFC0
		[SecuritySafeCritical]
		private unsafe void _HashData(byte[] partIn, int ibStart, int cbSize)
		{
			int i = cbSize;
			int num = ibStart;
			int num2 = (int)(this._count & 63L);
			this._count += (long)i;
			uint[] array;
			uint* state;
			if ((array = this._stateSHA1) == null || array.Length == 0)
			{
				state = null;
			}
			else
			{
				state = &array[0];
			}
			byte[] array2;
			byte* block;
			if ((array2 = this._buffer) == null || array2.Length == 0)
			{
				block = null;
			}
			else
			{
				block = &array2[0];
			}
			uint[] array3;
			uint* expandedBuffer;
			if ((array3 = this._expandedBuffer) == null || array3.Length == 0)
			{
				expandedBuffer = null;
			}
			else
			{
				expandedBuffer = &array3[0];
			}
			if (num2 > 0 && num2 + i >= 64)
			{
				Buffer.InternalBlockCopy(partIn, num, this._buffer, num2, 64 - num2);
				num += 64 - num2;
				i -= 64 - num2;
				SHA1Managed.SHATransform(expandedBuffer, state, block);
				num2 = 0;
			}
			while (i >= 64)
			{
				Buffer.InternalBlockCopy(partIn, num, this._buffer, 0, 64);
				num += 64;
				i -= 64;
				SHA1Managed.SHATransform(expandedBuffer, state, block);
			}
			if (i > 0)
			{
				Buffer.InternalBlockCopy(partIn, num, this._buffer, num2, i);
			}
			array3 = null;
			array2 = null;
			array = null;
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000AEED4 File Offset: 0x000AD0D4
		private byte[] _EndHash()
		{
			byte[] array = new byte[20];
			int num = 64 - (int)(this._count & 63L);
			if (num <= 8)
			{
				num += 64;
			}
			byte[] array2 = new byte[num];
			array2[0] = 128;
			long num2 = this._count * 8L;
			array2[num - 8] = (byte)(num2 >> 56 & 255L);
			array2[num - 7] = (byte)(num2 >> 48 & 255L);
			array2[num - 6] = (byte)(num2 >> 40 & 255L);
			array2[num - 5] = (byte)(num2 >> 32 & 255L);
			array2[num - 4] = (byte)(num2 >> 24 & 255L);
			array2[num - 3] = (byte)(num2 >> 16 & 255L);
			array2[num - 2] = (byte)(num2 >> 8 & 255L);
			array2[num - 1] = (byte)(num2 & 255L);
			this._HashData(array2, 0, array2.Length);
			Utils.DWORDToBigEndian(array, this._stateSHA1, 5);
			this.HashValue = array;
			return array;
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000AEFC0 File Offset: 0x000AD1C0
		[SecurityCritical]
		private unsafe static void SHATransform(uint* expandedBuffer, uint* state, byte* block)
		{
			uint num = *state;
			uint num2 = state[1];
			uint num3 = state[2];
			uint num4 = state[3];
			uint num5 = state[4];
			Utils.DWORDFromBigEndian(expandedBuffer, 16, block);
			SHA1Managed.SHAExpand(expandedBuffer);
			int i;
			for (i = 0; i < 20; i += 5)
			{
				num5 += (num << 5 | num >> 27) + (num4 ^ (num2 & (num3 ^ num4))) + expandedBuffer[i] + 1518500249U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + (num3 ^ (num & (num2 ^ num3))) + expandedBuffer[i + 1] + 1518500249U;
				num = (num << 30 | num >> 2);
				num3 += (num4 << 5 | num4 >> 27) + (num2 ^ (num5 & (num ^ num2))) + expandedBuffer[i + 2] + 1518500249U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + (num ^ (num4 & (num5 ^ num))) + expandedBuffer[i + 3] + 1518500249U;
				num4 = (num4 << 30 | num4 >> 2);
				num += (num2 << 5 | num2 >> 27) + (num5 ^ (num3 & (num4 ^ num5))) + expandedBuffer[i + 4] + 1518500249U;
				num3 = (num3 << 30 | num3 >> 2);
			}
			while (i < 40)
			{
				num5 += (num << 5 | num >> 27) + (num2 ^ num3 ^ num4) + expandedBuffer[i] + 1859775393U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + (num ^ num2 ^ num3) + expandedBuffer[i + 1] + 1859775393U;
				num = (num << 30 | num >> 2);
				num3 += (num4 << 5 | num4 >> 27) + (num5 ^ num ^ num2) + expandedBuffer[i + 2] + 1859775393U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + (num4 ^ num5 ^ num) + expandedBuffer[i + 3] + 1859775393U;
				num4 = (num4 << 30 | num4 >> 2);
				num += (num2 << 5 | num2 >> 27) + (num3 ^ num4 ^ num5) + expandedBuffer[i + 4] + 1859775393U;
				num3 = (num3 << 30 | num3 >> 2);
				i += 5;
			}
			while (i < 60)
			{
				num5 += (num << 5 | num >> 27) + ((num2 & num3) | (num4 & (num2 | num3))) + expandedBuffer[i] + 2400959708U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + ((num & num2) | (num3 & (num | num2))) + expandedBuffer[i + 1] + 2400959708U;
				num = (num << 30 | num >> 2);
				num3 += (num4 << 5 | num4 >> 27) + ((num5 & num) | (num2 & (num5 | num))) + expandedBuffer[i + 2] + 2400959708U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + ((num4 & num5) | (num & (num4 | num5))) + expandedBuffer[i + 3] + 2400959708U;
				num4 = (num4 << 30 | num4 >> 2);
				num += (num2 << 5 | num2 >> 27) + ((num3 & num4) | (num5 & (num3 | num4))) + expandedBuffer[i + 4] + 2400959708U;
				num3 = (num3 << 30 | num3 >> 2);
				i += 5;
			}
			while (i < 80)
			{
				num5 += (num << 5 | num >> 27) + (num2 ^ num3 ^ num4) + expandedBuffer[i] + 3395469782U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + (num ^ num2 ^ num3) + expandedBuffer[i + 1] + 3395469782U;
				num = (num << 30 | num >> 2);
				num3 += (num4 << 5 | num4 >> 27) + (num5 ^ num ^ num2) + expandedBuffer[i + 2] + 3395469782U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + (num4 ^ num5 ^ num) + expandedBuffer[i + 3] + 3395469782U;
				num4 = (num4 << 30 | num4 >> 2);
				num += (num2 << 5 | num2 >> 27) + (num3 ^ num4 ^ num5) + expandedBuffer[i + 4] + 3395469782U;
				num3 = (num3 << 30 | num3 >> 2);
				i += 5;
			}
			*state += num;
			state[1] += num2;
			state[2] += num3;
			state[3] += num4;
			state[4] += num5;
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000AF420 File Offset: 0x000AD620
		[SecurityCritical]
		private unsafe static void SHAExpand(uint* x)
		{
			for (int i = 16; i < 80; i++)
			{
				uint num = x[i - 3] ^ x[i - 8] ^ x[i - 14] ^ x[i - 16];
				x[i] = (num << 1 | num >> 31);
			}
		}

		// Token: 0x040021D6 RID: 8662
		private byte[] _buffer;

		// Token: 0x040021D7 RID: 8663
		private long _count;

		// Token: 0x040021D8 RID: 8664
		private uint[] _stateSHA1;

		// Token: 0x040021D9 RID: 8665
		private uint[] _expandedBuffer;
	}
}
