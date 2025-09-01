using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200022E RID: 558
	internal class XmlCachedStream : MemoryStream
	{
		// Token: 0x06001519 RID: 5401 RVA: 0x000830E8 File Offset: 0x000812E8
		internal XmlCachedStream(Uri uri, Stream stream)
		{
			this.uri = uri;
			try
			{
				byte[] buffer = new byte[4096];
				int count;
				while ((count = stream.Read(buffer, 0, 4096)) > 0)
				{
					this.Write(buffer, 0, count);
				}
				base.Position = 0L;
			}
			finally
			{
				stream.Close();
			}
		}

		// Token: 0x040012CD RID: 4813
		private const int MoveBufferSize = 4096;

		// Token: 0x040012CE RID: 4814
		private Uri uri;
	}
}
