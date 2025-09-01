using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000072 RID: 114
	internal class MimeReader
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x0001C158 File Offset: 0x0001A358
		public MimeReader(Stream stream, string boundary)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (boundary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("boundary");
			}
			this.reader = new DelimittedStreamReader(stream);
			this.boundaryBytes = MimeWriter.GetBoundaryBytes(boundary);
			this.reader.Push(this.boundaryBytes, 0, 2);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001C1BE File Offset: 0x0001A3BE
		public void Close()
		{
			this.reader.Close();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001C1CC File Offset: 0x0001A3CC
		public string Preface
		{
			get
			{
				if (this.content == null)
				{
					Stream nextStream = this.reader.GetNextStream(this.boundaryBytes);
					this.content = new StreamReader(nextStream, Encoding.ASCII, false, 256).ReadToEnd();
					nextStream.Close();
					if (this.content == null)
					{
						this.content = string.Empty;
					}
				}
				return this.content;
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001C22E File Offset: 0x0001A42E
		public Stream GetContentStream()
		{
			this.mimeHeaderReader.Close();
			return this.reader.GetNextStream(this.boundaryBytes);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001C24C File Offset: 0x0001A44C
		public bool ReadNextPart()
		{
			string preface = this.Preface;
			if (this.currentStream != null)
			{
				this.currentStream.Close();
				this.currentStream = null;
			}
			Stream nextStream = this.reader.GetNextStream(MimeReader.CRLFCRLF);
			if (nextStream == null)
			{
				return false;
			}
			if (this.BlockRead(nextStream, this.scratch, 0, 2) == 2)
			{
				if (this.scratch[0] == 13 && this.scratch[1] == 10)
				{
					if (this.mimeHeaderReader == null)
					{
						this.mimeHeaderReader = new MimeHeaderReader(nextStream);
					}
					else
					{
						this.mimeHeaderReader.Reset(nextStream);
					}
					return true;
				}
				if (this.scratch[0] == 45 && this.scratch[1] == 45 && (this.BlockRead(nextStream, this.scratch, 0, 2) < 2 || (this.scratch[0] == 13 && this.scratch[1] == 10)))
				{
					return false;
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME parts are truncated.")));
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001C33C File Offset: 0x0001A53C
		public MimeHeaders ReadHeaders(int maxBuffer, ref int remaining)
		{
			MimeHeaders mimeHeaders = new MimeHeaders();
			while (this.mimeHeaderReader.Read(maxBuffer, ref remaining))
			{
				mimeHeaders.Add(this.mimeHeaderReader.Name, this.mimeHeaderReader.Value, ref remaining);
			}
			return mimeHeaders;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001C380 File Offset: 0x0001A580
		private int BlockRead(Stream stream, byte[] buffer, int offset, int count)
		{
			int num = 0;
			do
			{
				int num2 = stream.Read(buffer, offset + num, count - num);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
			}
			while (num < count);
			return num;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001C3AB File Offset: 0x0001A5AB
		// Note: this type is marked as 'beforefieldinit'.
		static MimeReader()
		{
		}

		// Token: 0x040002DD RID: 733
		private static byte[] CRLFCRLF = new byte[]
		{
			13,
			10,
			13,
			10
		};

		// Token: 0x040002DE RID: 734
		private byte[] boundaryBytes;

		// Token: 0x040002DF RID: 735
		private string content;

		// Token: 0x040002E0 RID: 736
		private Stream currentStream;

		// Token: 0x040002E1 RID: 737
		private MimeHeaderReader mimeHeaderReader;

		// Token: 0x040002E2 RID: 738
		private DelimittedStreamReader reader;

		// Token: 0x040002E3 RID: 739
		private byte[] scratch = new byte[2];
	}
}
