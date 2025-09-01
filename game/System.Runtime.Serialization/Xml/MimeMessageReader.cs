using System;
using System.IO;
using System.Runtime.Serialization;

namespace System.Xml
{
	// Token: 0x02000071 RID: 113
	internal class MimeMessageReader
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x0001C080 File Offset: 0x0001A280
		public MimeMessageReader(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			this.reader = new DelimittedStreamReader(stream);
			this.mimeHeaderReader = new MimeHeaderReader(this.reader.GetNextStream(MimeMessageReader.CRLFCRLF));
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001C0BD File Offset: 0x0001A2BD
		public Stream GetContentStream()
		{
			if (this.getContentStreamCalled)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("On MimeMessage, GetContentStream method is already called.")));
			}
			this.mimeHeaderReader.Close();
			Stream nextStream = this.reader.GetNextStream(null);
			this.getContentStreamCalled = true;
			return nextStream;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001C0FC File Offset: 0x0001A2FC
		public MimeHeaders ReadHeaders(int maxBuffer, ref int remaining)
		{
			MimeHeaders mimeHeaders = new MimeHeaders();
			while (this.mimeHeaderReader.Read(maxBuffer, ref remaining))
			{
				mimeHeaders.Add(this.mimeHeaderReader.Name, this.mimeHeaderReader.Value, ref remaining);
			}
			return mimeHeaders;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001C13E File Offset: 0x0001A33E
		// Note: this type is marked as 'beforefieldinit'.
		static MimeMessageReader()
		{
		}

		// Token: 0x040002D9 RID: 729
		private static byte[] CRLFCRLF = new byte[]
		{
			13,
			10,
			13,
			10
		};

		// Token: 0x040002DA RID: 730
		private bool getContentStreamCalled;

		// Token: 0x040002DB RID: 731
		private MimeHeaderReader mimeHeaderReader;

		// Token: 0x040002DC RID: 732
		private DelimittedStreamReader reader;
	}
}
