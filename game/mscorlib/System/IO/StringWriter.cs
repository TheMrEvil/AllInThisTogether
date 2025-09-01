using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Implements a <see cref="T:System.IO.TextWriter" /> for writing information to a string. The information is stored in an underlying <see cref="T:System.Text.StringBuilder" />.</summary>
	// Token: 0x02000B5A RID: 2906
	[ComVisible(true)]
	[Serializable]
	public class StringWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class.</summary>
		// Token: 0x06006948 RID: 26952 RVA: 0x00167E2B File Offset: 0x0016602B
		public StringWriter() : this(new StringBuilder(), CultureInfo.CurrentCulture)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class with the specified format control.</summary>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting.</param>
		// Token: 0x06006949 RID: 26953 RVA: 0x00167E3D File Offset: 0x0016603D
		public StringWriter(IFormatProvider formatProvider) : this(new StringBuilder(), formatProvider)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <param name="sb">The <see cref="T:System.Text.StringBuilder" /> object to write to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is <see langword="null" />.</exception>
		// Token: 0x0600694A RID: 26954 RVA: 0x00167E4B File Offset: 0x0016604B
		public StringWriter(StringBuilder sb) : this(sb, CultureInfo.CurrentCulture)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.StringWriter" /> class that writes to the specified <see cref="T:System.Text.StringBuilder" /> and has the specified format provider.</summary>
		/// <param name="sb">The <see cref="T:System.Text.StringBuilder" /> object to write to.</param>
		/// <param name="formatProvider">An <see cref="T:System.IFormatProvider" /> object that controls formatting.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sb" /> is <see langword="null" />.</exception>
		// Token: 0x0600694B RID: 26955 RVA: 0x00167E59 File Offset: 0x00166059
		public StringWriter(StringBuilder sb, IFormatProvider formatProvider) : base(formatProvider)
		{
			if (sb == null)
			{
				throw new ArgumentNullException("sb", Environment.GetResourceString("Buffer cannot be null."));
			}
			this._sb = sb;
			this._isOpen = true;
		}

		/// <summary>Closes the current <see cref="T:System.IO.StringWriter" /> and the underlying stream.</summary>
		// Token: 0x0600694C RID: 26956 RVA: 0x00167E88 File Offset: 0x00166088
		public override void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.StringWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600694D RID: 26957 RVA: 0x00167E91 File Offset: 0x00166091
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			base.Dispose(disposing);
		}

		/// <summary>Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.</summary>
		/// <returns>The <see langword="Encoding" /> in which the output is written.</returns>
		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x0600694E RID: 26958 RVA: 0x00167EA1 File Offset: 0x001660A1
		public override Encoding Encoding
		{
			get
			{
				if (StringWriter.m_encoding == null)
				{
					StringWriter.m_encoding = new UnicodeEncoding(false, false);
				}
				return StringWriter.m_encoding;
			}
		}

		/// <summary>Returns the underlying <see cref="T:System.Text.StringBuilder" />.</summary>
		/// <returns>The underlying <see langword="StringBuilder" />.</returns>
		// Token: 0x0600694F RID: 26959 RVA: 0x00167EC1 File Offset: 0x001660C1
		public virtual StringBuilder GetStringBuilder()
		{
			return this._sb;
		}

		/// <summary>Writes a character to the string.</summary>
		/// <param name="value">The character to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed.</exception>
		// Token: 0x06006950 RID: 26960 RVA: 0x00167EC9 File Offset: 0x001660C9
		public override void Write(char value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(value);
		}

		/// <summary>Writes a subarray of characters to the string.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">(<paramref name="index" /> + <paramref name="count" />)&gt; <paramref name="buffer" />. <see langword="Length" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed.</exception>
		// Token: 0x06006951 RID: 26961 RVA: 0x00167EE8 File Offset: 0x001660E8
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			this._sb.Append(buffer, index, count);
		}

		/// <summary>Writes a string to the current string.</summary>
		/// <param name="value">The string to write.</param>
		/// <exception cref="T:System.ObjectDisposedException">The writer is closed.</exception>
		// Token: 0x06006952 RID: 26962 RVA: 0x00167F73 File Offset: 0x00166173
		public override void Write(string value)
		{
			if (!this._isOpen)
			{
				__Error.WriterClosed();
			}
			if (value != null)
			{
				this._sb.Append(value);
			}
		}

		/// <summary>Writes a character to the string asynchronously.</summary>
		/// <param name="value">The character to write to the string.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06006953 RID: 26963 RVA: 0x0015D69C File Offset: 0x0015B89C
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a string to the current string asynchronously.</summary>
		/// <param name="value">The string to write. If <paramref name="value" /> is <see langword="null" />, nothing is written to the text stream.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06006954 RID: 26964 RVA: 0x0015D6AA File Offset: 0x0015B8AA
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(string value)
		{
			this.Write(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a subarray of characters to the string asynchronously.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06006955 RID: 26965 RVA: 0x0015D6B8 File Offset: 0x0015B8B8
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			this.Write(buffer, index, count);
			return Task.CompletedTask;
		}

		/// <summary>Writes a character followed by a line terminator asynchronously to the string.</summary>
		/// <param name="value">The character to write to the string.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06006956 RID: 26966 RVA: 0x0015D6C8 File Offset: 0x0015B8C8
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a string followed by a line terminator asynchronously to the current string.</summary>
		/// <param name="value">The string to write. If the value is <see langword="null" />, only a line terminator is written.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06006957 RID: 26967 RVA: 0x0015D6D6 File Offset: 0x0015B8D6
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(string value)
		{
			this.WriteLine(value);
			return Task.CompletedTask;
		}

		/// <summary>Writes a subarray of characters followed by a line terminator asynchronously to the string.</summary>
		/// <param name="buffer">The character array to write data from.</param>
		/// <param name="index">The position in the buffer at which to start reading data.</param>
		/// <param name="count">The maximum number of characters to write.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> plus <paramref name="count" /> is greater than the buffer length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The string writer is disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The string writer is currently in use by a previous write operation.</exception>
		// Token: 0x06006958 RID: 26968 RVA: 0x0015D6E4 File Offset: 0x0015B8E4
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			this.WriteLine(buffer, index, count);
			return Task.CompletedTask;
		}

		/// <summary>Asynchronously clears all buffers for the current writer and causes any buffered data to be written to the underlying device.</summary>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		// Token: 0x06006959 RID: 26969 RVA: 0x00078842 File Offset: 0x00076A42
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task FlushAsync()
		{
			return Task.CompletedTask;
		}

		/// <summary>Returns a string containing the characters written to the current <see langword="StringWriter" /> so far.</summary>
		/// <returns>The string containing the characters written to the current <see langword="StringWriter" />.</returns>
		// Token: 0x0600695A RID: 26970 RVA: 0x00167F92 File Offset: 0x00166192
		public override string ToString()
		{
			return this._sb.ToString();
		}

		// Token: 0x04003D27 RID: 15655
		private static volatile UnicodeEncoding m_encoding;

		// Token: 0x04003D28 RID: 15656
		private StringBuilder _sb;

		// Token: 0x04003D29 RID: 15657
		private bool _isOpen;
	}
}
