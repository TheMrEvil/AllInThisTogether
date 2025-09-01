using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000089 RID: 137
	internal class MimeWriter
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x0001F388 File Offset: 0x0001D588
		internal MimeWriter(Stream stream, string boundary)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (boundary == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("boundary");
			}
			this.stream = stream;
			this.boundaryBytes = MimeWriter.GetBoundaryBytes(boundary);
			this.state = MimeWriterState.Start;
			this.bufferedWrite = new BufferedWrite();
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001F3DC File Offset: 0x0001D5DC
		internal static int GetHeaderSize(string name, string value, int maxSizeInBytes)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			int num = XmlMtomWriter.ValidateSizeOfMessage(maxSizeInBytes, 0, MimeGlobals.COLONSPACE.Length + MimeGlobals.CRLF.Length);
			num += XmlMtomWriter.ValidateSizeOfMessage(maxSizeInBytes, num, name.Length);
			return num + XmlMtomWriter.ValidateSizeOfMessage(maxSizeInBytes, num, value.Length);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001F440 File Offset: 0x0001D640
		internal static byte[] GetBoundaryBytes(string boundary)
		{
			byte[] array = new byte[boundary.Length + MimeGlobals.BoundaryPrefix.Length];
			for (int i = 0; i < MimeGlobals.BoundaryPrefix.Length; i++)
			{
				array[i] = MimeGlobals.BoundaryPrefix[i];
			}
			Encoding.ASCII.GetBytes(boundary, 0, boundary.Length, array, MimeGlobals.BoundaryPrefix.Length);
			return array;
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001F499 File Offset: 0x0001D699
		internal MimeWriterState WriteState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001F4A1 File Offset: 0x0001D6A1
		internal int GetBoundarySize()
		{
			return this.boundaryBytes.Length;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001F4AB File Offset: 0x0001D6AB
		internal void StartPreface()
		{
			if (this.state != MimeWriterState.Start)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("MIME writer is at invalid state for starting preface.", new object[]
				{
					this.state.ToString()
				})));
			}
			this.state = MimeWriterState.StartPreface;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001F4EC File Offset: 0x0001D6EC
		internal void StartPart()
		{
			MimeWriterState mimeWriterState = this.state;
			if (mimeWriterState == MimeWriterState.StartPart || mimeWriterState == MimeWriterState.Closed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("MIME writer is at invalid state for starting a part.", new object[]
				{
					this.state.ToString()
				})));
			}
			this.state = MimeWriterState.StartPart;
			if (this.contentStream != null)
			{
				this.contentStream.Flush();
				this.contentStream = null;
			}
			this.bufferedWrite.Write(this.boundaryBytes);
			this.bufferedWrite.Write(MimeGlobals.CRLF);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001F57C File Offset: 0x0001D77C
		internal void Close()
		{
			if (this.state == MimeWriterState.Closed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("MIME writer is at invalid state for closing.", new object[]
				{
					this.state.ToString()
				})));
			}
			this.state = MimeWriterState.Closed;
			if (this.contentStream != null)
			{
				this.contentStream.Flush();
				this.contentStream = null;
			}
			this.bufferedWrite.Write(this.boundaryBytes);
			this.bufferedWrite.Write(MimeGlobals.DASHDASH);
			this.bufferedWrite.Write(MimeGlobals.CRLF);
			this.Flush();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001F619 File Offset: 0x0001D819
		private void Flush()
		{
			if (this.bufferedWrite.Length > 0)
			{
				this.stream.Write(this.bufferedWrite.GetBuffer(), 0, this.bufferedWrite.Length);
				this.bufferedWrite.Reset();
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001F658 File Offset: 0x0001D858
		internal void WriteHeader(string name, string value)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			MimeWriterState mimeWriterState = this.state;
			if (mimeWriterState == MimeWriterState.Start || mimeWriterState - MimeWriterState.Content <= 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("MIME writer is at invalid state for header.", new object[]
				{
					this.state.ToString()
				})));
			}
			this.state = MimeWriterState.Header;
			this.bufferedWrite.Write(name);
			this.bufferedWrite.Write(MimeGlobals.COLONSPACE);
			this.bufferedWrite.Write(value);
			this.bufferedWrite.Write(MimeGlobals.CRLF);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001F700 File Offset: 0x0001D900
		internal Stream GetContentStream()
		{
			MimeWriterState mimeWriterState = this.state;
			if (mimeWriterState == MimeWriterState.Start || mimeWriterState - MimeWriterState.Content <= 1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("MIME writer is at invalid state for content.", new object[]
				{
					this.state.ToString()
				})));
			}
			this.state = MimeWriterState.Content;
			this.bufferedWrite.Write(MimeGlobals.CRLF);
			this.Flush();
			this.contentStream = this.stream;
			return this.contentStream;
		}

		// Token: 0x04000361 RID: 865
		private Stream stream;

		// Token: 0x04000362 RID: 866
		private byte[] boundaryBytes;

		// Token: 0x04000363 RID: 867
		private MimeWriterState state;

		// Token: 0x04000364 RID: 868
		private BufferedWrite bufferedWrite;

		// Token: 0x04000365 RID: 869
		private Stream contentStream;
	}
}
