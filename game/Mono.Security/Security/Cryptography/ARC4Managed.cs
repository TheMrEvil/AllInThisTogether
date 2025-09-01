using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200004E RID: 78
	public class ARC4Managed : RC4, ICryptoTransform, IDisposable
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000F3AD File Offset: 0x0000D5AD
		public ARC4Managed()
		{
			this.state = new byte[256];
			this.m_disposed = false;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000F3CC File Offset: 0x0000D5CC
		~ARC4Managed()
		{
			this.Dispose(true);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.x = 0;
				this.y = 0;
				if (this.key != null)
				{
					Array.Clear(this.key, 0, this.key.Length);
					this.key = null;
				}
				Array.Clear(this.state, 0, this.state.Length);
				this.state = null;
				GC.SuppressFinalize(this);
				this.m_disposed = true;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000F46A File Offset: 0x0000D66A
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000F48C File Offset: 0x0000D68C
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Key");
				}
				this.KeyValue = (this.key = (byte[])value.Clone());
				this.KeySetup(this.key);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000F4CD File Offset: 0x0000D6CD
		public bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000F4DA File Offset: 0x0000D6DA
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this.CreateEncryptor();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000F4E9 File Offset: 0x0000D6E9
		public override void GenerateIV()
		{
			this.IV = new byte[0];
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000F4F7 File Offset: 0x0000D6F7
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000F50C File Offset: 0x0000D70C
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000F50F File Offset: 0x0000D70F
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000F512 File Offset: 0x0000D712
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F518 File Offset: 0x0000D718
		private void KeySetup(byte[] key)
		{
			byte b = 0;
			byte b2 = 0;
			for (int i = 0; i < 256; i++)
			{
				this.state[i] = (byte)i;
			}
			this.x = 0;
			this.y = 0;
			for (int j = 0; j < 256; j++)
			{
				b2 = key[(int)b] + this.state[j] + b2;
				byte b3 = this.state[j];
				this.state[j] = this.state[(int)b2];
				this.state[(int)b2] = b3;
				b = (byte)((int)(b + 1) % key.Length);
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		private void CheckInput(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "< 0");
			}
			if (inputCount < 0)
			{
				throw new ArgumentOutOfRangeException("inputCount", "< 0");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException(Locale.GetText("Overflow"), "inputBuffer");
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000F600 File Offset: 0x0000D800
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			if (outputOffset > outputBuffer.Length - inputCount)
			{
				throw new ArgumentException(Locale.GetText("Overflow"), "outputBuffer");
			}
			return this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000F668 File Offset: 0x0000D868
		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = 0; i < inputCount; i++)
			{
				this.x += 1;
				this.y = this.state[(int)this.x] + this.y;
				byte b = this.state[(int)this.x];
				this.state[(int)this.x] = this.state[(int)this.y];
				this.state[(int)this.y] = b;
				byte b2 = this.state[(int)this.x] + this.state[(int)this.y];
				outputBuffer[outputOffset + i] = (inputBuffer[inputOffset + i] ^ this.state[(int)b2]);
			}
			return inputCount;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000F71C File Offset: 0x0000D91C
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			byte[] array = new byte[inputCount];
			this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x0400029F RID: 671
		private byte[] key;

		// Token: 0x040002A0 RID: 672
		private byte[] state;

		// Token: 0x040002A1 RID: 673
		private byte x;

		// Token: 0x040002A2 RID: 674
		private byte y;

		// Token: 0x040002A3 RID: 675
		private bool m_disposed;
	}
}
