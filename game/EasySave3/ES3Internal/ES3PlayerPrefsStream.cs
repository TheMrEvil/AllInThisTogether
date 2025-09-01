using System;
using System.IO;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000E1 RID: 225
	internal class ES3PlayerPrefsStream : MemoryStream
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0001E1E6 File Offset: 0x0001C3E6
		public ES3PlayerPrefsStream(string path) : base(ES3PlayerPrefsStream.GetData(path, false))
		{
			this.path = path;
			this.append = false;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001E203 File Offset: 0x0001C403
		public ES3PlayerPrefsStream(string path, int bufferSize, bool append = false) : base(bufferSize)
		{
			this.path = path;
			this.append = append;
			this.isWriteStream = true;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001E221 File Offset: 0x0001C421
		private static byte[] GetData(string path, bool isWriteStream)
		{
			if (!PlayerPrefs.HasKey(path))
			{
				throw new FileNotFoundException("File \"" + path + "\" could not be found in PlayerPrefs");
			}
			return Convert.FromBase64String(PlayerPrefs.GetString(path));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001E24C File Offset: 0x0001C44C
		protected override void Dispose(bool disposing)
		{
			if (this.isDisposed)
			{
				return;
			}
			this.isDisposed = true;
			if (this.isWriteStream && this.Length > 0L)
			{
				if (this.append)
				{
					byte[] array = Convert.FromBase64String(PlayerPrefs.GetString(this.path));
					byte[] array2 = this.ToArray();
					byte[] array3 = new byte[array.Length + array2.Length];
					Buffer.BlockCopy(array, 0, array3, 0, array.Length);
					Buffer.BlockCopy(array2, 0, array3, array.Length, array2.Length);
					PlayerPrefs.SetString(this.path, Convert.ToBase64String(array3));
					PlayerPrefs.Save();
				}
				else
				{
					PlayerPrefs.SetString(this.path + ".tmp", Convert.ToBase64String(this.ToArray()));
				}
				PlayerPrefs.SetString("timestamp_" + this.path, DateTime.UtcNow.Ticks.ToString());
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000158 RID: 344
		private string path;

		// Token: 0x04000159 RID: 345
		private bool append;

		// Token: 0x0400015A RID: 346
		private bool isWriteStream;

		// Token: 0x0400015B RID: 347
		private bool isDisposed;
	}
}
