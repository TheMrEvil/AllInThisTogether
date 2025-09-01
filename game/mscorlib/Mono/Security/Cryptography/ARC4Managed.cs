using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000085 RID: 133
	internal class ARC4Managed : RC4, ICryptoTransform, IDisposable
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public ARC4Managed()
		{
			this.state = new byte[256];
			this.m_disposed = false;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		~ARC4Managed()
		{
			this.Dispose(true);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000DCD8 File Offset: 0x0000BED8
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000DD46 File Offset: 0x0000BF46
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000DD68 File Offset: 0x0000BF68
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000DDB6 File Offset: 0x0000BFB6
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this.CreateEncryptor();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000DDC5 File Offset: 0x0000BFC5
		public override void GenerateIV()
		{
			this.IV = new byte[0];
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000DDD3 File Offset: 0x0000BFD3
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000040F7 File Offset: 0x000022F7
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002AE RID: 686 RVA: 0x000040F7 File Offset: 0x000022F7
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
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

		// Token: 0x060002B0 RID: 688 RVA: 0x0000DE70 File Offset: 0x0000C070
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

		// Token: 0x060002B1 RID: 689 RVA: 0x0000DED0 File Offset: 0x0000C0D0
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

		// Token: 0x060002B2 RID: 690 RVA: 0x0000DF38 File Offset: 0x0000C138
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

		// Token: 0x060002B3 RID: 691 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			byte[] array = new byte[inputCount];
			this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x04000ED1 RID: 3793
		private byte[] key;

		// Token: 0x04000ED2 RID: 3794
		private byte[] state;

		// Token: 0x04000ED3 RID: 3795
		private byte x;

		// Token: 0x04000ED4 RID: 3796
		private byte y;

		// Token: 0x04000ED5 RID: 3797
		private bool m_disposed;
	}
}
