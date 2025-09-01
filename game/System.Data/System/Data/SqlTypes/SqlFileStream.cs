using System;
using System.IO;
using System.Runtime.InteropServices;
using Unity;

namespace System.Data.SqlTypes
{
	/// <summary>Exposes SQL Server data that is stored with the FILESTREAM column attribute as a sequence of bytes.</summary>
	// Token: 0x020003EB RID: 1003
	public sealed class SqlFileStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlFileStream" /> class.</summary>
		/// <param name="path">The logical path to the file. The path can be retrieved by using the Transact-SQL Pathname function on the underlying FILESTREAM column in the table.</param>
		/// <param name="transactionContext">The transaction context for the <see langword="SqlFileStream" /> object. Applications should return the byte array returned by calling the GET_FILESTREAM_TRANSACTION_CONTEXT method.</param>
		/// <param name="access">The access mode to use when opening the file. Supported <see cref="T:System.IO.FileAccess" /> enumeration values are <see cref="F:System.IO.FileAccess.Read" />, <see cref="F:System.IO.FileAccess.Write" />, and <see cref="F:System.IO.FileAccess.ReadWrite" />.  
		///  When using <see langword="FileAccess.Read" />, the <see langword="SqlFileStream" /> object can be used to read all of the existing data.  
		///  When using <see langword="FileAccess.Write" />, <see langword="SqlFileStream" /> points to a zero byte file. Existing data will be overwritten when the object is closed and the transaction is committed.  
		///  When using <see langword="FileAccess.ReadWrite" />, the <see langword="SqlFileStream" /> points to a file which has all the existing data in it. The handle is positioned at the beginning of the file. You can use one of the <see langword="System.IO" /><see langword="Seek" /> methods to move the handle position within the file to write or append new data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is a null reference, or <paramref name="transactionContext" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// <paramref name="path" /> begins with "\\.\", for example "\\.\PHYSICALDRIVE0 ".  
		/// The handle returned by the call to NTCreateFile is not of type FILE_TYPE_DISK.  
		/// <paramref name="options" /> contains an unsupported value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified path. This occurs when Write or ReadWrite access is specified, and the file or directory is set for read-only access.</exception>
		/// <exception cref="T:System.InvalidOperationException">NtCreateFile fails with error code set to ERROR_SHARING_VIOLATION.</exception>
		// Token: 0x06002F70 RID: 12144 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlFileStream(string path, byte[] transactionContext, FileAccess access)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlFileStream" /> class.</summary>
		/// <param name="path">The logical path to the file. The path can be retrieved by using the Transact-SQL Pathname function on the underlying FILESTREAM column in the table.</param>
		/// <param name="transactionContext">The transaction context for the <see langword="SqlFileStream" /> object. When set to null, an implicit transaction will be used for the <see langword="SqlFileStream" /> object. Applications should return the byte array returned by calling the GET_FILESTREAM_TRANSACTION_CONTEXT method.</param>
		/// <param name="access">The access mode to use when opening the file. Supported <see cref="T:System.IO.FileAccess" /> enumeration values are <see cref="F:System.IO.FileAccess.Read" />, <see cref="F:System.IO.FileAccess.Write" />, and <see cref="F:System.IO.FileAccess.ReadWrite" />.  
		///  When using <see langword="FileAccess.Read" />, the <see langword="SqlFileStream" /> object can be used to read all of the existing data.  
		///  When using <see langword="FileAccess.Write" />, <see langword="SqlFileStream" /> points to a zero byte file. Existing data will be overwritten when the object is closed and the transaction is committed.  
		///  When using <see langword="FileAccess.ReadWrite" />, the <see langword="SqlFileStream" /> points to a file which has all the existing data in it. The handle is positioned at the beginning of the file. You can use one of the <see langword="System.IO" /><see langword="Seek" /> methods to move the handle position within the file to write or append new data.</param>
		/// <param name="options">Specifies the option to use while opening the file. Supported <see cref="T:System.IO.FileOptions" /> values are <see cref="F:System.IO.FileOptions.Asynchronous" />, <see cref="F:System.IO.FileOptions.WriteThrough" />, <see cref="F:System.IO.FileOptions.SequentialScan" />, and <see cref="F:System.IO.FileOptions.RandomAccess" />.</param>
		/// <param name="allocationSize">The allocation size to use while creating a file. If set to 0, the default value is used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is a null reference, or <paramref name="transactionContext" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// <paramref name="path" /> begins with "\\.\", for example "\\.\PHYSICALDRIVE0 ".  
		/// The handle returned by call to NTCreateFile is not of type FILE_TYPE_DISK.  
		/// <paramref name="options" /> contains an unsupported value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified <paramref name="path" /> is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified path. This occurs when Write or ReadWrite access is specified, and the file or directory is set for read-only access.</exception>
		/// <exception cref="T:System.InvalidOperationException">NtCreateFile fails with error code set to ERROR_SHARING_VIOLATION.</exception>
		// Token: 0x06002F71 RID: 12145 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public SqlFileStream(string path, byte[] transactionContext, FileAccess access, FileOptions options, long allocationSize)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a value indicating whether the current stream supports reading.</summary>
		/// <returns>
		///   <see langword="true" /> if the current stream supports reading; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000CB9B4 File Offset: 0x000C9BB4
		public override bool CanRead
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seeking.</summary>
		/// <returns>
		///   <see langword="true" /> if the current stream supports seeking; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000CB9D0 File Offset: 0x000C9BD0
		public override bool CanSeek
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports writing.</summary>
		/// <returns>
		///   <see langword="true" /> if the current stream supports writing; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x000CB9EC File Offset: 0x000C9BEC
		public override bool CanWrite
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a value indicating the length of the current stream in bytes.</summary>
		/// <returns>An <see cref="T:System.Int64" /> indicating the length of the current stream in bytes.</returns>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x000CBA08 File Offset: 0x000C9C08
		public override long Length
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the logical path of the <see cref="T:System.Data.SqlTypes.SqlFileStream" /> passed to the constructor.</summary>
		/// <returns>A string value indicating the name of the <see cref="T:System.Data.SqlTypes.SqlFileStream" />.</returns>
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x00060C51 File Offset: 0x0005EE51
		public string Name
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the position within the current stream.</summary>
		/// <returns>The current position within the <see cref="T:System.Data.SqlTypes.SqlFileStream" />.</returns>
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x000CBA24 File Offset: 0x000C9C24
		// (set) Token: 0x06002F78 RID: 12152 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public override long Position
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the transaction context for this <see cref="T:System.Data.SqlTypes.SqlFileStream" /> object.</summary>
		/// <returns>The <paramref name="transactionContext" /> array that was passed to the constructor for this <see cref="T:System.Data.SqlTypes.SqlFileStream" /> object.</returns>
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x00060C51 File Offset: 0x0005EE51
		public byte[] TransactionContext
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>clears all buffers for this stream and causes any buffered data to be written to the underlying device.</summary>
		// Token: 0x06002F7A RID: 12154 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public override void Flush()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.</summary>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.NotSupportedException">The object does not support reading of data.</exception>
		// Token: 0x06002F7B RID: 12155 RVA: 0x000CBA40 File Offset: 0x000C9C40
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Sets the position within the current stream.</summary>
		/// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter</param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position</param>
		/// <returns>The new position within the current stream.</returns>
		// Token: 0x06002F7C RID: 12156 RVA: 0x000CBA5C File Offset: 0x000C9C5C
		public override long Seek(long offset, SeekOrigin origin)
		{
			ThrowStub.ThrowNotSupportedException();
			return 0L;
		}

		/// <summary>Sets the length of the current stream.</summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		/// <exception cref="T:System.NotSupportedException">The object does not support reading of data.</exception>
		// Token: 0x06002F7D RID: 12157 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public override void SetLength(long value)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.</summary>
		/// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		/// <exception cref="T:System.NotSupportedException">The object does not support writing of data.</exception>
		// Token: 0x06002F7E RID: 12158 RVA: 0x000108A6 File Offset: 0x0000EAA6
		public override void Write(byte[] buffer, int offset, int count)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
