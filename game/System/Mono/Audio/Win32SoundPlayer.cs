using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mono.Audio
{
	// Token: 0x0200003B RID: 59
	internal class Win32SoundPlayer : IDisposable
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00003C7B File Offset: 0x00001E7B
		public Win32SoundPlayer(Stream s)
		{
			if (s != null)
			{
				this._buffer = new byte[s.Length];
				s.Read(this._buffer, 0, this._buffer.Length);
				return;
			}
			this._buffer = new byte[0];
		}

		// Token: 0x060000F2 RID: 242
		[DllImport("winmm.dll", SetLastError = true)]
		private static extern bool PlaySound(byte[] ptrToSound, UIntPtr hmod, Win32SoundPlayer.SoundFlags flags);

		// Token: 0x17000023 RID: 35
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00003CBB File Offset: 0x00001EBB
		public Stream Stream
		{
			set
			{
				this.Stop();
				if (value != null)
				{
					this._buffer = new byte[value.Length];
					value.Read(this._buffer, 0, this._buffer.Length);
					return;
				}
				this._buffer = new byte[0];
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003CFB File Offset: 0x00001EFB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003D0C File Offset: 0x00001F0C
		~Win32SoundPlayer()
		{
			this.Dispose(false);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003D3C File Offset: 0x00001F3C
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this.Stop();
				this._disposed = true;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003D53 File Offset: 0x00001F53
		public void Play()
		{
			Win32SoundPlayer.PlaySound(this._buffer, UIntPtr.Zero, (Win32SoundPlayer.SoundFlags)5U);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003D67 File Offset: 0x00001F67
		public void PlayLooping()
		{
			Win32SoundPlayer.PlaySound(this._buffer, UIntPtr.Zero, (Win32SoundPlayer.SoundFlags)13U);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003D7C File Offset: 0x00001F7C
		public void PlaySync()
		{
			Win32SoundPlayer.PlaySound(this._buffer, UIntPtr.Zero, (Win32SoundPlayer.SoundFlags)6U);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003D90 File Offset: 0x00001F90
		public void Stop()
		{
			Win32SoundPlayer.PlaySound(null, UIntPtr.Zero, Win32SoundPlayer.SoundFlags.SND_SYNC);
		}

		// Token: 0x04000152 RID: 338
		private byte[] _buffer;

		// Token: 0x04000153 RID: 339
		private bool _disposed;

		// Token: 0x0200003C RID: 60
		private enum SoundFlags : uint
		{
			// Token: 0x04000155 RID: 341
			SND_SYNC,
			// Token: 0x04000156 RID: 342
			SND_ASYNC,
			// Token: 0x04000157 RID: 343
			SND_NODEFAULT,
			// Token: 0x04000158 RID: 344
			SND_MEMORY = 4U,
			// Token: 0x04000159 RID: 345
			SND_LOOP = 8U,
			// Token: 0x0400015A RID: 346
			SND_FILENAME = 131072U
		}
	}
}
