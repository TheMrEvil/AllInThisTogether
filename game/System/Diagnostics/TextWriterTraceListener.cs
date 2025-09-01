using System;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to a <see cref="T:System.IO.TextWriter" /> or to a <see cref="T:System.IO.Stream" />, such as <see cref="T:System.IO.FileStream" />.</summary>
	// Token: 0x0200022A RID: 554
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class TextWriterTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with <see cref="T:System.IO.TextWriter" /> as the output recipient.</summary>
		// Token: 0x06001021 RID: 4129 RVA: 0x00046FA4 File Offset: 0x000451A4
		public TextWriterTraceListener()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class, using the stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to.</param>
		/// <exception cref="T:System.ArgumentNullException">The stream is <see langword="null" />.</exception>
		// Token: 0x06001022 RID: 4130 RVA: 0x00046FAC File Offset: 0x000451AC
		public TextWriterTraceListener(Stream stream) : this(stream, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the stream as the recipient of the debugging and tracing output.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that represents the stream the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The stream is <see langword="null" />.</exception>
		// Token: 0x06001023 RID: 4131 RVA: 0x00046FBA File Offset: 0x000451BA
		public TextWriterTraceListener(Stream stream, string name) : base(name)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.writer = new StreamWriter(stream);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class using the specified writer as recipient of the tracing or debugging output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the <see cref="T:System.Diagnostics.TextWriterTraceListener" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The writer is <see langword="null" />.</exception>
		// Token: 0x06001024 RID: 4132 RVA: 0x00046FDD File Offset: 0x000451DD
		public TextWriterTraceListener(TextWriter writer) : this(writer, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the specified writer as recipient of the tracing or debugging output.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that receives the output from the <see cref="T:System.Diagnostics.TextWriterTraceListener" />.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The writer is <see langword="null" />.</exception>
		// Token: 0x06001025 RID: 4133 RVA: 0x00046FEB File Offset: 0x000451EB
		public TextWriterTraceListener(TextWriter writer, string name) : base(name)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to.</param>
		/// <exception cref="T:System.ArgumentNullException">The file is <see langword="null" />.</exception>
		// Token: 0x06001026 RID: 4134 RVA: 0x00047009 File Offset: 0x00045209
		public TextWriterTraceListener(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> class with the specified name, using the file as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The name of the file the <see cref="T:System.Diagnostics.TextWriterTraceListener" /> writes to.</param>
		/// <param name="name">The name of the new instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The stream is <see langword="null" />.</exception>
		// Token: 0x06001027 RID: 4135 RVA: 0x00047018 File Offset: 0x00045218
		public TextWriterTraceListener(string fileName, string name) : base(name)
		{
			this.fileName = fileName;
		}

		/// <summary>Gets or sets the text writer that receives the tracing or debugging output.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the writer that receives the tracing or debugging output.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00047028 File Offset: 0x00045228
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x00047037 File Offset: 0x00045237
		public TextWriter Writer
		{
			get
			{
				this.EnsureWriter();
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		/// <summary>Closes the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> so that it no longer receives tracing or debugging output.</summary>
		// Token: 0x0600102A RID: 4138 RVA: 0x00047040 File Offset: 0x00045240
		public override void Close()
		{
			if (this.writer != null)
			{
				try
				{
					this.writer.Close();
				}
				catch (ObjectDisposedException)
				{
				}
			}
			this.writer = null;
		}

		/// <summary>Disposes this <see cref="T:System.Diagnostics.TextWriterTraceListener" /> object.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release managed resources; if <see langword="false" />, <see cref="M:System.Diagnostics.TextWriterTraceListener.Dispose(System.Boolean)" /> has no effect.</param>
		// Token: 0x0600102B RID: 4139 RVA: 0x0004707C File Offset: 0x0004527C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Close();
				}
				else
				{
					if (this.writer != null)
					{
						try
						{
							this.writer.Close();
						}
						catch (ObjectDisposedException)
						{
						}
					}
					this.writer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Flushes the output buffer for the <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" />.</summary>
		// Token: 0x0600102C RID: 4140 RVA: 0x000470DC File Offset: 0x000452DC
		public override void Flush()
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			try
			{
				this.writer.Flush();
			}
			catch (ObjectDisposedException)
			{
			}
		}

		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" />.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x0600102D RID: 4141 RVA: 0x00047114 File Offset: 0x00045314
		public override void Write(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.Write(message);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		/// <summary>Writes a message to this instance's <see cref="P:System.Diagnostics.TextWriterTraceListener.Writer" /> followed by a line terminator. The default line terminator is a carriage return followed by a line feed (\r\n).</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x0600102E RID: 4142 RVA: 0x0004715C File Offset: 0x0004535C
		public override void WriteLine(string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			if (base.NeedIndent)
			{
				this.WriteIndent();
			}
			try
			{
				this.writer.WriteLine(message);
				base.NeedIndent = true;
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000471AC File Offset: 0x000453AC
		private static Encoding GetEncodingWithFallback(Encoding encoding)
		{
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
			encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
			return encoding2;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x000471D0 File Offset: 0x000453D0
		internal bool EnsureWriter()
		{
			bool flag = true;
			if (this.writer == null)
			{
				flag = false;
				if (this.fileName == null)
				{
					return flag;
				}
				Encoding encodingWithFallback = TextWriterTraceListener.GetEncodingWithFallback(new UTF8Encoding(false));
				string path = Path.GetFullPath(this.fileName);
				string directoryName = Path.GetDirectoryName(path);
				string text = Path.GetFileName(path);
				for (int i = 0; i < 2; i++)
				{
					try
					{
						this.writer = new StreamWriter(path, true, encodingWithFallback, 4096);
						flag = true;
						break;
					}
					catch (IOException)
					{
						text = Guid.NewGuid().ToString() + text;
						path = Path.Combine(directoryName, text);
					}
					catch (UnauthorizedAccessException)
					{
						break;
					}
					catch (Exception)
					{
						break;
					}
				}
				if (!flag)
				{
					this.fileName = null;
				}
			}
			return flag;
		}

		// Token: 0x040009D0 RID: 2512
		internal TextWriter writer;

		// Token: 0x040009D1 RID: 2513
		private string fileName;
	}
}
