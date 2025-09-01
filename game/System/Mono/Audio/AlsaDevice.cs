using System;
using System.Runtime.InteropServices;

namespace Mono.Audio
{
	// Token: 0x0200003A RID: 58
	internal class AlsaDevice : AudioDevice, IDisposable
	{
		// Token: 0x060000CE RID: 206
		[DllImport("libasound")]
		private static extern int snd_pcm_open(ref IntPtr handle, string pcm_name, int stream, int mode);

		// Token: 0x060000CF RID: 207
		[DllImport("libasound")]
		private static extern int snd_pcm_close(IntPtr handle);

		// Token: 0x060000D0 RID: 208
		[DllImport("libasound")]
		private static extern int snd_pcm_drain(IntPtr handle);

		// Token: 0x060000D1 RID: 209
		[DllImport("libasound")]
		private static extern int snd_pcm_writei(IntPtr handle, byte[] buf, int size);

		// Token: 0x060000D2 RID: 210
		[DllImport("libasound")]
		private static extern int snd_pcm_set_params(IntPtr handle, int format, int access, int channels, int rate, int soft_resample, int latency);

		// Token: 0x060000D3 RID: 211
		[DllImport("libasound")]
		private static extern int snd_pcm_state(IntPtr handle);

		// Token: 0x060000D4 RID: 212
		[DllImport("libasound")]
		private static extern int snd_pcm_prepare(IntPtr handle);

		// Token: 0x060000D5 RID: 213
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params(IntPtr handle, IntPtr param);

		// Token: 0x060000D6 RID: 214
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_malloc(ref IntPtr param);

		// Token: 0x060000D7 RID: 215
		[DllImport("libasound")]
		private static extern void snd_pcm_hw_params_free(IntPtr param);

		// Token: 0x060000D8 RID: 216
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_any(IntPtr handle, IntPtr param);

		// Token: 0x060000D9 RID: 217
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_access(IntPtr handle, IntPtr param, int access);

		// Token: 0x060000DA RID: 218
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_format(IntPtr handle, IntPtr param, int format);

		// Token: 0x060000DB RID: 219
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_channels(IntPtr handle, IntPtr param, uint channel);

		// Token: 0x060000DC RID: 220
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_rate_near(IntPtr handle, IntPtr param, ref uint rate, ref int dir);

		// Token: 0x060000DD RID: 221
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_period_time_near(IntPtr handle, IntPtr param, ref uint period, ref int dir);

		// Token: 0x060000DE RID: 222
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_get_period_size(IntPtr param, ref uint period, ref int dir);

		// Token: 0x060000DF RID: 223
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_buffer_size_near(IntPtr handle, IntPtr param, ref uint buff_size);

		// Token: 0x060000E0 RID: 224
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_get_buffer_time_max(IntPtr param, ref uint buffer_time, ref int dir);

		// Token: 0x060000E1 RID: 225
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_buffer_time_near(IntPtr handle, IntPtr param, ref uint BufferTime, ref int dir);

		// Token: 0x060000E2 RID: 226
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_get_buffer_size(IntPtr param, ref uint BufferSize);

		// Token: 0x060000E3 RID: 227
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params(IntPtr handle, IntPtr param);

		// Token: 0x060000E4 RID: 228
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params_malloc(ref IntPtr param);

		// Token: 0x060000E5 RID: 229
		[DllImport("libasound")]
		private static extern void snd_pcm_sw_params_free(IntPtr param);

		// Token: 0x060000E6 RID: 230
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params_current(IntPtr handle, IntPtr param);

		// Token: 0x060000E7 RID: 231
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params_set_avail_min(IntPtr handle, IntPtr param, uint frames);

		// Token: 0x060000E8 RID: 232
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params_set_start_threshold(IntPtr handle, IntPtr param, uint StartThreshold);

		// Token: 0x060000E9 RID: 233 RVA: 0x00003924 File Offset: 0x00001B24
		public AlsaDevice(string name)
		{
			if (name == null)
			{
				name = "default";
			}
			int num = AlsaDevice.snd_pcm_open(ref this.handle, name, 0, 0);
			if (num < 0)
			{
				throw new Exception("no open " + num.ToString());
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000396C File Offset: 0x00001B6C
		~AlsaDevice()
		{
			this.Dispose(false);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000399C File Offset: 0x00001B9C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000039AC File Offset: 0x00001BAC
		protected virtual void Dispose(bool disposing)
		{
			if (this.sw_param != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_sw_params_free(this.sw_param);
			}
			if (this.hw_param != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_hw_params_free(this.hw_param);
			}
			if (this.handle != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_close(this.handle);
			}
			this.sw_param = IntPtr.Zero;
			this.hw_param = IntPtr.Zero;
			this.handle = IntPtr.Zero;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003A34 File Offset: 0x00001C34
		public override bool SetFormat(AudioFormat format, int channels, int rate)
		{
			uint num = 0U;
			uint chunk_size = 0U;
			uint startThreshold = 0U;
			uint num2 = 0U;
			int num3 = 0;
			uint num4 = (uint)rate;
			if (AlsaDevice.snd_pcm_hw_params_malloc(ref this.hw_param) == 0)
			{
				AlsaDevice.snd_pcm_hw_params_any(this.handle, this.hw_param);
				AlsaDevice.snd_pcm_hw_params_set_access(this.handle, this.hw_param, 3);
				AlsaDevice.snd_pcm_hw_params_set_format(this.handle, this.hw_param, (int)format);
				AlsaDevice.snd_pcm_hw_params_set_channels(this.handle, this.hw_param, (uint)channels);
				num3 = 0;
				AlsaDevice.snd_pcm_hw_params_set_rate_near(this.handle, this.hw_param, ref num4, ref num3);
				num3 = 0;
				AlsaDevice.snd_pcm_hw_params_get_buffer_time_max(this.hw_param, ref num2, ref num3);
				if (num2 > 500000U)
				{
					num2 = 500000U;
				}
				if (num2 > 0U)
				{
					num = num2 / 4U;
				}
				num3 = 0;
				AlsaDevice.snd_pcm_hw_params_set_period_time_near(this.handle, this.hw_param, ref num, ref num3);
				num3 = 0;
				AlsaDevice.snd_pcm_hw_params_set_buffer_time_near(this.handle, this.hw_param, ref num2, ref num3);
				AlsaDevice.snd_pcm_hw_params_get_period_size(this.hw_param, ref chunk_size, ref num3);
				this.chunk_size = chunk_size;
				AlsaDevice.snd_pcm_hw_params_get_buffer_size(this.hw_param, ref startThreshold);
				AlsaDevice.snd_pcm_hw_params(this.handle, this.hw_param);
			}
			else
			{
				Console.WriteLine("failed to alloc Alsa hw param struct");
			}
			int num5 = AlsaDevice.snd_pcm_sw_params_malloc(ref this.sw_param);
			if (num5 == 0)
			{
				AlsaDevice.snd_pcm_sw_params_current(this.handle, this.sw_param);
				AlsaDevice.snd_pcm_sw_params_set_avail_min(this.handle, this.sw_param, this.chunk_size);
				AlsaDevice.snd_pcm_sw_params_set_start_threshold(this.handle, this.sw_param, startThreshold);
				AlsaDevice.snd_pcm_sw_params(this.handle, this.sw_param);
			}
			else
			{
				Console.WriteLine("failed to alloc Alsa sw param struct");
			}
			if (this.hw_param != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_hw_params_free(this.hw_param);
				this.hw_param = IntPtr.Zero;
			}
			if (this.sw_param != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_sw_params_free(this.sw_param);
				this.sw_param = IntPtr.Zero;
			}
			return num5 == 0;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003C20 File Offset: 0x00001E20
		public override int PlaySample(byte[] buffer, int num_frames)
		{
			int num;
			do
			{
				num = AlsaDevice.snd_pcm_writei(this.handle, buffer, num_frames);
				if (num < 0)
				{
					this.XRunRecovery(num);
				}
			}
			while (num < 0);
			return num;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003C4C File Offset: 0x00001E4C
		public override int XRunRecovery(int err)
		{
			int result = 0;
			if (-32 == err)
			{
				result = AlsaDevice.snd_pcm_prepare(this.handle);
			}
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003C6D File Offset: 0x00001E6D
		public override void Wait()
		{
			AlsaDevice.snd_pcm_drain(this.handle);
		}

		// Token: 0x0400014F RID: 335
		private IntPtr handle;

		// Token: 0x04000150 RID: 336
		private IntPtr hw_param;

		// Token: 0x04000151 RID: 337
		private IntPtr sw_param;
	}
}
