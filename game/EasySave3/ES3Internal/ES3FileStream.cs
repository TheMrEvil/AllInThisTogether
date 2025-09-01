using System;
using System.IO;

namespace ES3Internal
{
	// Token: 0x020000E0 RID: 224
	public class ES3FileStream : FileStream
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x0001E14D File Offset: 0x0001C34D
		public ES3FileStream(string path, ES3FileMode fileMode, int bufferSize, bool useAsync) : base(ES3FileStream.GetPath(path, fileMode), ES3FileStream.GetFileMode(fileMode), ES3FileStream.GetFileAccess(fileMode), FileShare.None, bufferSize, useAsync)
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001E16C File Offset: 0x0001C36C
		protected static string GetPath(string path, ES3FileMode fileMode)
		{
			string directoryPath = ES3IO.GetDirectoryPath(path, '/');
			if (fileMode != ES3FileMode.Read && directoryPath != ES3IO.persistentDataPath)
			{
				ES3IO.CreateDirectory(directoryPath);
			}
			if (fileMode != ES3FileMode.Write || fileMode == ES3FileMode.Append)
			{
				return path;
			}
			if (fileMode != ES3FileMode.Write)
			{
				return path;
			}
			return path + ".tmp";
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001E1B3 File Offset: 0x0001C3B3
		protected static FileMode GetFileMode(ES3FileMode fileMode)
		{
			if (fileMode == ES3FileMode.Read)
			{
				return FileMode.Open;
			}
			if (fileMode == ES3FileMode.Write)
			{
				return FileMode.Create;
			}
			return FileMode.Append;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001E1C1 File Offset: 0x0001C3C1
		protected static FileAccess GetFileAccess(ES3FileMode fileMode)
		{
			if (fileMode == ES3FileMode.Read)
			{
				return FileAccess.Read;
			}
			return FileAccess.Write;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001E1CD File Offset: 0x0001C3CD
		protected override void Dispose(bool disposing)
		{
			if (this.isDisposed)
			{
				return;
			}
			this.isDisposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x04000157 RID: 343
		private bool isDisposed;
	}
}
