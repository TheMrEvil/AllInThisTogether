using System;

namespace Mono.Audio
{
	// Token: 0x02000039 RID: 57
	internal class AudioDevice
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000038C4 File Offset: 0x00001AC4
		private static AudioDevice TryAlsa(string name)
		{
			AudioDevice result;
			try
			{
				result = new AlsaDevice(name);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000038F0 File Offset: 0x00001AF0
		public static AudioDevice CreateDevice(string name)
		{
			AudioDevice audioDevice = AudioDevice.TryAlsa(name);
			if (audioDevice == null)
			{
				audioDevice = new AudioDevice();
			}
			return audioDevice;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000390E File Offset: 0x00001B0E
		public virtual bool SetFormat(AudioFormat format, int channels, int rate)
		{
			return true;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003911 File Offset: 0x00001B11
		public virtual int PlaySample(byte[] buffer, int num_frames)
		{
			return num_frames;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003914 File Offset: 0x00001B14
		public virtual int XRunRecovery(int err)
		{
			return err;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Wait()
		{
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003919 File Offset: 0x00001B19
		public uint ChunkSize
		{
			get
			{
				return this.chunk_size;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000219B File Offset: 0x0000039B
		public AudioDevice()
		{
		}

		// Token: 0x0400014E RID: 334
		protected uint chunk_size;
	}
}
