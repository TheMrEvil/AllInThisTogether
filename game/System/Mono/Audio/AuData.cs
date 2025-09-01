using System;
using System.IO;

namespace Mono.Audio
{
	// Token: 0x02000037 RID: 55
	internal class AuData : AudioData
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00003604 File Offset: 0x00001804
		public AuData(Stream data)
		{
			this.stream = data;
			byte[] array = new byte[24];
			int num = this.stream.Read(array, 0, 24);
			if (num != 24 || array[0] != 46 || array[1] != 115 || array[2] != 110 || array[3] != 100)
			{
				throw new Exception("incorrect format" + num.ToString());
			}
			int num2 = (int)array[7];
			num2 |= (int)array[6] << 8;
			num2 |= (int)array[5] << 16;
			num2 |= (int)array[4] << 24;
			this.data_len = (int)array[11];
			this.data_len |= (int)array[10] << 8;
			this.data_len |= (int)array[9] << 16;
			this.data_len |= (int)array[8] << 24;
			int num3 = (int)array[15];
			num3 |= (int)array[14] << 8;
			num3 |= (int)array[13] << 16;
			num3 |= (int)array[12] << 24;
			this.sample_rate = (int)array[19];
			this.sample_rate |= (int)array[18] << 8;
			this.sample_rate |= (int)array[17] << 16;
			this.sample_rate |= (int)array[16] << 24;
			int num4 = (int)array[23];
			num4 |= (int)array[22] << 8;
			num4 |= (int)array[21] << 16;
			num4 |= (int)array[20] << 24;
			this.channels = (short)num4;
			if (num2 < 24 || (num4 != 1 && num4 != 2))
			{
				throw new Exception("incorrect format offset" + num2.ToString());
			}
			if (num2 != 24)
			{
				for (int i = 24; i < num2; i++)
				{
					this.stream.ReadByte();
				}
			}
			if (num3 == 1)
			{
				this.frame_divider = 1;
				this.format = AudioFormat.MU_LAW;
				if (this.data_len == -1)
				{
					this.data_len = (int)this.stream.Length - num2;
				}
				return;
			}
			throw new Exception("incorrect format encoding" + num3.ToString());
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000037FC File Offset: 0x000019FC
		public override void Play(AudioDevice dev)
		{
			int num = 0;
			int chunkSize = (int)dev.ChunkSize;
			int num2 = this.data_len;
			byte[] array = new byte[this.data_len];
			byte[] array2 = new byte[chunkSize];
			this.stream.Position = 0L;
			this.stream.Read(array, 0, this.data_len);
			while (!this.IsStopped && num2 >= 0)
			{
				Buffer.BlockCopy(array, num, array2, 0, chunkSize);
				int num3 = dev.PlaySample(array2, chunkSize / (int)(this.frame_divider * (ushort)this.channels));
				if (num3 > 0)
				{
					num += num3 * (int)this.frame_divider * (int)this.channels;
					num2 -= num3 * (int)this.frame_divider * (int)this.channels;
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000038AC File Offset: 0x00001AAC
		public override int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000038B4 File Offset: 0x00001AB4
		public override int Rate
		{
			get
			{
				return this.sample_rate;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000038BC File Offset: 0x00001ABC
		public override AudioFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x0400012E RID: 302
		private Stream stream;

		// Token: 0x0400012F RID: 303
		private short channels;

		// Token: 0x04000130 RID: 304
		private ushort frame_divider;

		// Token: 0x04000131 RID: 305
		private int sample_rate;

		// Token: 0x04000132 RID: 306
		private int data_len;

		// Token: 0x04000133 RID: 307
		private AudioFormat format;
	}
}
