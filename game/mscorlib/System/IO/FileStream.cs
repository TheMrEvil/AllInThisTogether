using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.IO
{
	/// <summary>Provides a <see cref="T:System.IO.Stream" /> for a file, supporting both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x02000B5E RID: 2910
	[ComVisible(true)]
	public class FileStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission.</summary>
		/// <param name="handle">A file handle for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a field of <see cref="T:System.IO.FileAccess" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file handle is set for read-only access.</exception>
		// Token: 0x06006975 RID: 26997 RVA: 0x001682AB File Offset: 0x001664AB
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access) instead")]
		public FileStream(IntPtr handle, FileAccess access) : this(handle, access, true, 4096, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission and <see langword="FileStream" /> instance ownership.</summary>
		/// <param name="handle">A file handle for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object.</param>
		/// <param name="ownsHandle">
		///   <see langword="true" /> if the file handle will be owned by this <see langword="FileStream" /> instance; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a field of <see cref="T:System.IO.FileAccess" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file handle is set for read-only access.</exception>
		// Token: 0x06006976 RID: 26998 RVA: 0x001682BD File Offset: 0x001664BD
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle) : this(handle, access, ownsHandle, 4096, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, <see langword="FileStream" /> instance ownership, and buffer size.</summary>
		/// <param name="handle">A file handle for the file that this <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object.</param>
		/// <param name="ownsHandle">
		///   <see langword="true" /> if the file handle will be owned by this <see langword="FileStream" /> instance; otherwise, <see langword="false" />.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file handle is set for read-only access.</exception>
		// Token: 0x06006977 RID: 26999 RVA: 0x001682CF File Offset: 0x001664CF
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize) : this(handle, access, ownsHandle, bufferSize, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, <see langword="FileStream" /> instance ownership, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="handle">A file handle for the file that this <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object.</param>
		/// <param name="ownsHandle">
		///   <see langword="true" /> if the file handle will be owned by this <see langword="FileStream" /> instance; otherwise, <see langword="false" />.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
		/// <param name="isAsync">
		///   <see langword="true" /> if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="access" /> is less than <see langword="FileAccess.Read" /> or greater than <see langword="FileAccess.ReadWrite" /> or <paramref name="bufferSize" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.ArgumentException">The handle is invalid.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file handle is set for read-only access.</exception>
		// Token: 0x06006978 RID: 27000 RVA: 0x001682DE File Offset: 0x001664DE
		[Obsolete("Use FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync) instead")]
		public FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync) : this(handle, access, ownsHandle, bufferSize, isAsync, false)
		{
		}

		// Token: 0x06006979 RID: 27001 RVA: 0x001682F0 File Offset: 0x001664F0
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		internal FileStream(IntPtr handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync, bool isConsoleWrapper)
		{
			this.name = "[Unknown]";
			base..ctor();
			if (handle == MonoIO.InvalidHandle)
			{
				throw new ArgumentException("handle", Locale.GetText("Invalid."));
			}
			this.Init(new SafeFileHandle(handle, false), access, ownsHandle, bufferSize, isAsync, isConsoleWrapper);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path and creation mode.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// -or-  
		/// <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is <see langword="FileMode.Truncate" /> or <see langword="FileMode.Open" />, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see langword="FileMode.CreateNew" /> when the file specified by <paramref name="path" /> already exists, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value.</exception>
		// Token: 0x0600697A RID: 27002 RVA: 0x00168345 File Offset: 0x00166545
		public FileStream(string path, FileMode mode) : this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, FileShare.Read, 4096, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, and read/write permission.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="access">A constant that determines how the file can be accessed by the <see langword="FileStream" /> object. This also determines the values returned by the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object. <see cref="P:System.IO.FileStream.CanSeek" /> is <see langword="true" /> if <paramref name="path" /> specifies a disk file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// -or-  
		/// <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is <see langword="FileMode.Truncate" /> or <see langword="FileMode.Open" />, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see langword="FileMode.CreateNew" /> when the file specified by <paramref name="path" /> already exists, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value.</exception>
		// Token: 0x0600697B RID: 27003 RVA: 0x0016835F File Offset: 0x0016655F
		public FileStream(string path, FileMode mode, FileAccess access) : this(path, mode, access, (access == FileAccess.Write) ? FileShare.None : FileShare.Read, 4096, false, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write permission, and sharing permission.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="access">A constant that determines how the file can be accessed by the <see langword="FileStream" /> object. This also determines the values returned by the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object. <see cref="P:System.IO.FileStream.CanSeek" /> is <see langword="true" /> if <paramref name="path" /> specifies a disk file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// -or-  
		/// <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is <see langword="FileMode.Truncate" /> or <see langword="FileMode.Open" />, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see langword="FileMode.CreateNew" /> when the file specified by <paramref name="path" /> already exists, occurred.  
		///  -or-  
		///  The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to <see langword="FileShare.Delete" />.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="mode" /> contains an invalid value.</exception>
		// Token: 0x0600697C RID: 27004 RVA: 0x00168379 File Offset: 0x00166579
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share) : this(path, mode, access, share, 4096, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, and buffer size.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="access">A constant that determines how the file can be accessed by the <see langword="FileStream" /> object. This also determines the values returned by the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object. <see cref="P:System.IO.FileStream.CanSeek" /> is <see langword="true" /> if <paramref name="path" /> specifies a disk file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// -or-  
		/// <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.  
		/// -or-  
		/// <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is <see langword="FileMode.Truncate" /> or <see langword="FileMode.Open" />, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see langword="FileMode.CreateNew" /> when the file specified by <paramref name="path" /> already exists, occurred.  
		///  -or-  
		///  The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to <see langword="FileShare.Delete" />.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x0600697D RID: 27005 RVA: 0x0016838D File Offset: 0x0016658D
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize) : this(path, mode, access, share, bufferSize, false, FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="access">A constant that determines how the file can be accessed by the <see langword="FileStream" /> object. This also determines the values returned by the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object. <see cref="P:System.IO.FileStream.CanSeek" /> is <see langword="true" /> if <paramref name="path" /> specifies a disk file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
		/// <param name="useAsync">Specifies whether to use asynchronous I/O or synchronous I/O. However, note that the underlying operating system might not support asynchronous I/O, so when specifying <see langword="true" />, the handle might be opened synchronously depending on the platform. When opened asynchronously, the <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> and <see cref="M:System.IO.FileStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> methods perform better on large reads or writes, but they might be much slower for small reads or writes. If the application is designed to take advantage of asynchronous I/O, set the <paramref name="useAsync" /> parameter to <see langword="true" />. Using asynchronous I/O correctly can speed up applications by as much as a factor of 10, but using it without redesigning the application for asynchronous I/O can decrease performance by as much as a factor of 10.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// -or-  
		/// <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.  
		/// -or-  
		/// <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is <see langword="FileMode.Truncate" /> or <see langword="FileMode.Open" />, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see langword="FileMode.CreateNew" /> when the file specified by <paramref name="path" /> already exists, occurred.  
		///  -or-  
		///  The system is running Windows 98 or Windows 98 Second Edition and <paramref name="share" /> is set to <see langword="FileShare.Delete" />.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x0600697E RID: 27006 RVA: 0x0016839E File Offset: 0x0016659E
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync) : this(path, mode, access, share, bufferSize, useAsync ? FileOptions.Asynchronous : FileOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, read/write and sharing permission, the access other FileStreams can have to the same file, the buffer size, and additional file options.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="access">A constant that determines how the file can be accessed by the <see langword="FileStream" /> object. This also determines the values returned by the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object. <see cref="P:System.IO.FileStream.CanSeek" /> is <see langword="true" /> if <paramref name="path" /> specifies a disk file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
		/// <param name="options">A value that specifies additional file options.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// -or-  
		/// <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.  
		/// -or-  
		/// <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is <see langword="FileMode.Truncate" /> or <see langword="FileMode.Open" />, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see langword="FileMode.CreateNew" /> when the file specified by <paramref name="path" /> already exists, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access.  
		///  -or-  
		///  <see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x0600697F RID: 27007 RVA: 0x001683B9 File Offset: 0x001665B9
		public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options) : this(path, mode, access, share, bufferSize, false, options)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission.</summary>
		/// <param name="handle">A file handle for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a field of <see cref="T:System.IO.FileAccess" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file handle is set for read-only access.</exception>
		// Token: 0x06006980 RID: 27008 RVA: 0x001683CB File Offset: 0x001665CB
		public FileStream(SafeFileHandle handle, FileAccess access) : this(handle, access, 4096, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, and buffer size.</summary>
		/// <param name="handle">A file handle for the file that the current <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="access">A <see cref="T:System.IO.FileAccess" /> constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> parameter is an invalid handle.  
		///  -or-  
		///  The <paramref name="handle" /> parameter is a synchronous handle and it was used asynchronously.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="bufferSize" /> parameter is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file handle is set for read-only access.</exception>
		// Token: 0x06006981 RID: 27009 RVA: 0x001683DB File Offset: 0x001665DB
		public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize) : this(handle, access, bufferSize, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class for the specified file handle, with the specified read/write permission, buffer size, and synchronous or asynchronous state.</summary>
		/// <param name="handle">A file handle for the file that this <see langword="FileStream" /> object will encapsulate.</param>
		/// <param name="access">A constant that sets the <see cref="P:System.IO.FileStream.CanRead" /> and <see cref="P:System.IO.FileStream.CanWrite" /> properties of the <see langword="FileStream" /> object.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
		/// <param name="isAsync">
		///   <see langword="true" /> if the handle was opened asynchronously (that is, in overlapped I/O mode); otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="handle" /> parameter is an invalid handle.  
		///  -or-  
		///  The <paramref name="handle" /> parameter is a synchronous handle and it was used asynchronously.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="bufferSize" /> parameter is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified file handle, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file handle is set for read-only access.</exception>
		// Token: 0x06006982 RID: 27010 RVA: 0x001683E7 File Offset: 0x001665E7
		public FileStream(SafeFileHandle handle, FileAccess access, int bufferSize, bool isAsync)
		{
			this.name = "[Unknown]";
			base..ctor();
			this.Init(handle, access, false, bufferSize, isAsync, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, access rights and sharing permission, the buffer size, and additional file options.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see cref="T:System.IO.FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="rights">A constant that determines the access rights to use when creating access and audit rules for the file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
		/// <param name="options">A constant that specifies additional file options.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// -or-  
		/// <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.  
		/// -or-  
		/// <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is <see langword="FileMode.Truncate" /> or <see langword="FileMode.Open" />, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see langword="FileMode.CreateNew" /> when the file specified by <paramref name="path" /> already exists, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access.  
		///  -or-  
		///  <see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		// Token: 0x06006983 RID: 27011 RVA: 0x00168407 File Offset: 0x00166607
		[MonoLimitation("This ignores the rights parameter")]
		public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options) : this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, share, bufferSize, false, options)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileStream" /> class with the specified path, creation mode, access rights and sharing permission, the buffer size, additional file options, access control and audit security.</summary>
		/// <param name="path">A relative or absolute path for the file that the current <see cref="T:System.IO.FileStream" /> object will encapsulate.</param>
		/// <param name="mode">A constant that determines how to open or create the file.</param>
		/// <param name="rights">A constant that determines the access rights to use when creating access and audit rules for the file.</param>
		/// <param name="share">A constant that determines how the file will be shared by processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0 indicating the buffer size. The default buffer size is 4096.</param>
		/// <param name="options">A constant that specifies additional file options.</param>
		/// <param name="fileSecurity">A constant that determines the access control and audit security for the file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.  
		/// -or-  
		/// <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in an NTFS environment.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="path" /> refers to a non-file device, such as "con:", "com1:", "lpt1:", etc. in a non-NTFS environment.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is negative or zero.  
		/// -or-  
		/// <paramref name="mode" />, <paramref name="access" />, or <paramref name="share" /> contain an invalid value.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file cannot be found, such as when <paramref name="mode" /> is <see langword="FileMode.Truncate" /> or <see langword="FileMode.Open" />, and the file specified by <paramref name="path" /> does not exist. The file must already exist in these modes.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as specifying <see langword="FileMode.CreateNew" /> when the file specified by <paramref name="path" /> already exists, occurred.  
		///  -or-  
		///  The stream has been closed.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The <paramref name="access" /> requested is not permitted by the operating system for the specified <paramref name="path" />, such as when <paramref name="access" /> is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access.  
		///  -or-  
		///  <see cref="F:System.IO.FileOptions.Encrypted" /> is specified for <paramref name="options" />, but file encryption is not supported on the current platform.</exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified <paramref name="path" />, file name, or both exceed the system-defined maximum length.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		// Token: 0x06006984 RID: 27012 RVA: 0x00168407 File Offset: 0x00166607
		[MonoLimitation("This ignores the rights and fileSecurity parameters")]
		public FileStream(string path, FileMode mode, FileSystemRights rights, FileShare share, int bufferSize, FileOptions options, FileSecurity fileSecurity) : this(path, mode, (mode == FileMode.Append) ? FileAccess.Write : FileAccess.ReadWrite, share, bufferSize, false, options)
		{
		}

		// Token: 0x06006985 RID: 27013 RVA: 0x001683B9 File Offset: 0x001665B9
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, FileOptions options, string msgPath, bool bFromProxy, bool useLongPath = false, bool checkHost = false) : this(path, mode, access, share, bufferSize, false, options)
		{
		}

		// Token: 0x06006986 RID: 27014 RVA: 0x00168420 File Offset: 0x00166620
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool isAsync, bool anonymous) : this(path, mode, access, share, bufferSize, anonymous, isAsync ? FileOptions.Asynchronous : FileOptions.None)
		{
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x00168440 File Offset: 0x00166640
		internal FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool anonymous, FileOptions options)
		{
			this.name = "[Unknown]";
			base..ctor();
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException("Path is empty");
			}
			this.anonymous = anonymous;
			share &= ~FileShare.Inheritable;
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			if (mode < FileMode.CreateNew || mode > FileMode.Append)
			{
				throw new ArgumentOutOfRangeException("mode", "Enum value was out of legal range.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", "Enum value was out of legal range.");
			}
			if (share < FileShare.None || share > (FileShare.Read | FileShare.Write | FileShare.Delete))
			{
				throw new ArgumentOutOfRangeException("share", "Enum value was out of legal range.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Name has invalid chars");
			}
			path = Path.InsecureGetFullPath(path);
			if (Directory.Exists(path))
			{
				throw new UnauthorizedAccessException(string.Format(Locale.GetText("Access to the path '{0}' is denied."), this.GetSecureFileName(path, false)));
			}
			if (mode == FileMode.Append && (access & FileAccess.Read) == FileAccess.Read)
			{
				throw new ArgumentException("Append access can be requested only in write-only mode.");
			}
			if ((access & FileAccess.Write) == (FileAccess)0 && mode != FileMode.Open && mode != FileMode.OpenOrCreate)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Combining FileMode: {0} with FileAccess: {1} is invalid."), access, mode));
			}
			string directoryName = Path.GetDirectoryName(path);
			if (directoryName.Length > 0 && !Directory.Exists(Path.GetFullPath(directoryName)))
			{
				string text = Locale.GetText("Could not find a part of the path \"{0}\".");
				string arg = anonymous ? directoryName : Path.GetFullPath(path);
				throw new DirectoryNotFoundException(string.Format(text, arg));
			}
			if (!anonymous)
			{
				this.name = path;
			}
			MonoIOError error;
			IntPtr intPtr = MonoIO.Open(path, mode, access, share, options, out error);
			if (intPtr == MonoIO.InvalidHandle)
			{
				throw MonoIO.GetException(this.GetSecureFileName(path), error);
			}
			this.safeHandle = new SafeFileHandle(intPtr, false);
			this.access = access;
			this.owner = true;
			if (MonoIO.GetFileType(this.safeHandle, out error) == MonoFileType.Disk)
			{
				this.canseek = true;
				this.async = ((options & FileOptions.Asynchronous) > FileOptions.None);
			}
			else
			{
				this.canseek = false;
				this.async = false;
			}
			if (access == FileAccess.Read && this.canseek && bufferSize == 4096)
			{
				long length = this.Length;
				if ((long)bufferSize > length)
				{
					bufferSize = (int)((length < 1000L) ? 1000L : length);
				}
			}
			this.InitBuffer(bufferSize, false);
			if (mode == FileMode.Append)
			{
				this.Seek(0L, SeekOrigin.End);
				this.append_startpos = this.Position;
				return;
			}
			this.append_startpos = 0L;
		}

		// Token: 0x06006988 RID: 27016 RVA: 0x001686A8 File Offset: 0x001668A8
		private void Init(SafeFileHandle safeHandle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync, bool isConsoleWrapper)
		{
			if (!isConsoleWrapper && safeHandle.IsInvalid)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid handle."), "handle");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (!isConsoleWrapper && bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("Positive number required."));
			}
			MonoIOError monoIOError;
			MonoFileType fileType = MonoIO.GetFileType(safeHandle, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.name, monoIOError);
			}
			if (fileType == MonoFileType.Unknown)
			{
				throw new IOException("Invalid handle.");
			}
			if (fileType == MonoFileType.Disk)
			{
				this.canseek = true;
			}
			else
			{
				this.canseek = false;
			}
			this.safeHandle = safeHandle;
			this.ExposeHandle();
			this.access = access;
			this.owner = ownsHandle;
			this.async = isAsync;
			this.anonymous = false;
			if (this.canseek)
			{
				this.buf_start = MonoIO.Seek(safeHandle, 0L, SeekOrigin.Current, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.name, monoIOError);
				}
			}
			this.append_startpos = 0L;
		}

		/// <summary>Gets a value that indicates whether the current stream supports reading.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports reading; <see langword="false" /> if the stream is closed or was opened with write-only access.</returns>
		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06006989 RID: 27017 RVA: 0x0016879D File Offset: 0x0016699D
		public override bool CanRead
		{
			get
			{
				return this.access == FileAccess.Read || this.access == FileAccess.ReadWrite;
			}
		}

		/// <summary>Gets a value that indicates whether the current stream supports writing.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports writing; <see langword="false" /> if the stream is closed or was opened with read-only access.</returns>
		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x0600698A RID: 27018 RVA: 0x001687B3 File Offset: 0x001669B3
		public override bool CanWrite
		{
			get
			{
				return this.access == FileAccess.Write || this.access == FileAccess.ReadWrite;
			}
		}

		/// <summary>Gets a value that indicates whether the current stream supports seeking.</summary>
		/// <returns>
		///   <see langword="true" /> if the stream supports seeking; <see langword="false" /> if the stream is closed or if the <see langword="FileStream" /> was constructed from an operating-system handle such as a pipe or output to the console.</returns>
		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x0600698B RID: 27019 RVA: 0x001687C9 File Offset: 0x001669C9
		public override bool CanSeek
		{
			get
			{
				return this.canseek;
			}
		}

		/// <summary>Gets a value that indicates whether the <see langword="FileStream" /> was opened asynchronously or synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="FileStream" /> was opened asynchronously; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x0600698C RID: 27020 RVA: 0x001687D1 File Offset: 0x001669D1
		public virtual bool IsAsync
		{
			get
			{
				return this.async;
			}
		}

		/// <summary>Gets the absolute path of the file opened in the <see langword="FileStream" />.</summary>
		/// <returns>A string that is the absolute path of the file.</returns>
		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x0600698D RID: 27021 RVA: 0x001687D9 File Offset: 0x001669D9
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the length in bytes of the stream.</summary>
		/// <returns>A long value representing the length of the stream in bytes.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.IO.FileStream.CanSeek" /> for this stream is <see langword="false" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as the file being closed, occurred.</exception>
		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x0600698E RID: 27022 RVA: 0x001687E4 File Offset: 0x001669E4
		public override long Length
		{
			get
			{
				if (this.safeHandle.IsClosed)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				this.FlushBufferIfDirty();
				MonoIOError monoIOError;
				long length = MonoIO.GetLength(this.safeHandle, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
				}
				return length;
			}
		}

		/// <summary>Gets or sets the current position of this stream.</summary>
		/// <returns>The current position of this stream.</returns>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.  
		/// -or-
		///  The position was set to a very large value beyond the end of the stream in Windows 98 or earlier.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Attempted to set the position to a negative value.</exception>
		/// <exception cref="T:System.IO.EndOfStreamException">Attempted seeking past the end of a stream that does not support this.</exception>
		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x0600698F RID: 27023 RVA: 0x00168848 File Offset: 0x00166A48
		// (set) Token: 0x06006990 RID: 27024 RVA: 0x001688BD File Offset: 0x00166ABD
		public override long Position
		{
			get
			{
				if (this.safeHandle.IsClosed)
				{
					throw new ObjectDisposedException("Stream has been closed");
				}
				if (!this.CanSeek)
				{
					throw new NotSupportedException("The stream does not support seeking");
				}
				if (!this.isExposed)
				{
					return this.buf_start + (long)this.buf_offset;
				}
				MonoIOError monoIOError;
				long result = MonoIO.Seek(this.safeHandle, 0L, SeekOrigin.Current, out monoIOError);
				if (monoIOError != MonoIOError.ERROR_SUCCESS)
				{
					throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
				}
				return result;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("Non-negative number required."));
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		/// <summary>Gets the operating system file handle for the file that the current <see langword="FileStream" /> object encapsulates.</summary>
		/// <returns>The operating system file handle for the file encapsulated by this <see langword="FileStream" /> object, or -1 if the <see langword="FileStream" /> has been closed.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06006991 RID: 27025 RVA: 0x001688E2 File Offset: 0x00166AE2
		[Obsolete("Use SafeFileHandle instead")]
		public virtual IntPtr Handle
		{
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
			get
			{
				IntPtr result = this.safeHandle.DangerousGetHandle();
				if (!this.isExposed)
				{
					this.ExposeHandle();
				}
				return result;
			}
		}

		/// <summary>Gets a <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> object that represents the operating system file handle for the file that the current <see cref="T:System.IO.FileStream" /> object encapsulates.</summary>
		/// <returns>An object that represents the operating system file handle for the file that the current <see cref="T:System.IO.FileStream" /> object encapsulates.</returns>
		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06006992 RID: 27026 RVA: 0x001688FD File Offset: 0x00166AFD
		public virtual SafeFileHandle SafeFileHandle
		{
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			[SecurityPermission(SecurityAction.InheritanceDemand, UnmanagedCode = true)]
			get
			{
				if (!this.isExposed)
				{
					this.ExposeHandle();
				}
				return this.safeHandle;
			}
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x00168913 File Offset: 0x00166B13
		private void ExposeHandle()
		{
			this.isExposed = true;
			this.FlushBuffer();
			this.InitBuffer(0, true);
		}

		/// <summary>Reads a byte from the file and advances the read position one byte.</summary>
		/// <returns>The byte, cast to an <see cref="T:System.Int32" />, or -1 if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.NotSupportedException">The current stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current stream is closed.</exception>
		// Token: 0x06006994 RID: 27028 RVA: 0x0016892C File Offset: 0x00166B2C
		public override int ReadByte()
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			if (this.buf_size != 0)
			{
				if (this.buf_offset >= this.buf_length)
				{
					this.RefillBuffer();
					if (this.buf_length == 0)
					{
						return -1;
					}
				}
				byte[] array = this.buf;
				int num = this.buf_offset;
				this.buf_offset = num + 1;
				return array[num];
			}
			if (this.ReadData(this.safeHandle, this.buf, 0, 1) == 0)
			{
				return -1;
			}
			return (int)this.buf[0];
		}

		/// <summary>Writes a byte to the current position in the file stream.</summary>
		/// <param name="value">A byte to write to the stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		// Token: 0x06006995 RID: 27029 RVA: 0x001689C4 File Offset: 0x00166BC4
		public override void WriteByte(byte value)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.buf_offset == this.buf_size)
			{
				this.FlushBuffer();
			}
			if (this.buf_size == 0)
			{
				this.buf[0] = value;
				this.buf_dirty = true;
				this.buf_length = 1;
				this.FlushBuffer();
				return;
			}
			byte[] array = this.buf;
			int num = this.buf_offset;
			this.buf_offset = num + 1;
			array[num] = value;
			if (this.buf_offset > this.buf_length)
			{
				this.buf_length = this.buf_offset;
			}
			this.buf_dirty = true;
		}

		/// <summary>Reads a block of bytes from the stream and writes the data in a given buffer.</summary>
		/// <param name="array">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which the read bytes will be placed.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <returns>The total number of bytes read into the buffer. This might be less than the number of bytes requested if that number of bytes are not currently available, or zero if the end of the stream is reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> describe an invalid range in <paramref name="array" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x06006996 RID: 27030 RVA: 0x00168A70 File Offset: 0x00166C70
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading");
			}
			int num = array.Length;
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > num)
			{
				throw new ArgumentException("destination offset is beyond array size");
			}
			if (offset > num - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginRead(array, offset, count, null, null);
				return this.EndRead(asyncResult);
			}
			return this.ReadInternal(array, offset, count);
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x00168B28 File Offset: 0x00166D28
		private int ReadInternal(byte[] dest, int offset, int count)
		{
			int num = this.ReadSegment(dest, offset, count);
			if (num == count)
			{
				return count;
			}
			int num2 = num;
			count -= num;
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				num = this.ReadData(this.safeHandle, dest, offset + num, count);
				this.buf_start += (long)num;
			}
			else
			{
				this.RefillBuffer();
				num = this.ReadSegment(dest, offset + num2, count);
			}
			return num2 + num;
		}

		/// <summary>Begins an asynchronous read operation. Consider using <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead.</summary>
		/// <param name="array">The buffer to read data into.</param>
		/// <param name="offset">The byte offset in <paramref name="array" /> at which to begin reading.</param>
		/// <param name="numBytes">The maximum number of bytes to read.</param>
		/// <param name="userCallback">The method to be called when the asynchronous read operation is completed.</param>
		/// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <returns>An object that references the asynchronous read.</returns>
		/// <exception cref="T:System.ArgumentException">The array length minus <paramref name="offset" /> is less than <paramref name="numBytes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="numBytes" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An asynchronous read was attempted past the end of the file.</exception>
		// Token: 0x06006998 RID: 27032 RVA: 0x00168B94 File Offset: 0x00166D94
		public override IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream does not support reading");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("Buffer too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginRead(array, offset, numBytes, userCallback, stateObject);
			}
			return new FileStream.ReadDelegate(this.ReadInternal).BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		/// <summary>Waits for the pending asynchronous read operation to complete. (Consider using <see cref="M:System.IO.FileStream.ReadAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead.)</summary>
		/// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
		/// <returns>The number of bytes read from the stream, between 0 and the number of bytes you requested. Streams only return 0 at the end of the stream, otherwise, they should block until at least 1 byte is available.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.FileStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.IO.FileStream.EndRead(System.IAsyncResult)" /> is called multiple times.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		// Token: 0x06006999 RID: 27033 RVA: 0x00168C44 File Offset: 0x00166E44
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				return base.EndRead(asyncResult);
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.ReadDelegate readDelegate = asyncResult2.AsyncDelegate as FileStream.ReadDelegate;
			if (readDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			return readDelegate.EndInvoke(asyncResult);
		}

		/// <summary>Writes a block of bytes to the file stream.</summary>
		/// <param name="array">The buffer containing data to write to the stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="array" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="offset" /> and <paramref name="count" /> describe an invalid range in <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.  
		/// -or-
		///  Another thread may have caused an unexpected change in the position of the operating system's file handle.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The current stream instance does not support writing.</exception>
		// Token: 0x0600699A RID: 27034 RVA: 0x00168CAC File Offset: 0x00166EAC
		public override void Write(byte[] array, int offset, int count)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "< 0");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "< 0");
			}
			if (offset > array.Length - count)
			{
				throw new ArgumentException("Reading would overrun buffer");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing");
			}
			if (this.async)
			{
				IAsyncResult asyncResult = this.BeginWrite(array, offset, count, null, null);
				this.EndWrite(asyncResult);
				return;
			}
			this.WriteInternal(array, offset, count);
		}

		// Token: 0x0600699B RID: 27035 RVA: 0x00168D54 File Offset: 0x00166F54
		private void WriteInternal(byte[] src, int offset, int count)
		{
			if (count > this.buf_size)
			{
				this.FlushBuffer();
				if (this.CanSeek && !this.isExposed)
				{
					MonoIOError monoIOError;
					MonoIO.Seek(this.safeHandle, this.buf_start, SeekOrigin.Begin, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				int i = count;
				while (i > 0)
				{
					MonoIOError monoIOError;
					int num = MonoIO.Write(this.safeHandle, src, offset, i, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					i -= num;
					offset += num;
				}
				this.buf_start += (long)count;
				return;
			}
			int num2 = 0;
			while (count > 0)
			{
				int num3 = this.WriteSegment(src, offset + num2, count);
				num2 += num3;
				count -= num3;
				if (count == 0)
				{
					break;
				}
				this.FlushBuffer();
			}
		}

		/// <summary>Begins an asynchronous write operation. Consider using <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead.</summary>
		/// <param name="array">The buffer containing data to write to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="array" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="numBytes">The maximum number of bytes to write.</param>
		/// <param name="userCallback">The method to be called when the asynchronous write operation is completed.</param>
		/// <param name="stateObject">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <returns>An object that references the asynchronous write.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> length minus <paramref name="offset" /> is less than <paramref name="numBytes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="numBytes" /> is negative.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		// Token: 0x0600699C RID: 27036 RVA: 0x00168E20 File Offset: 0x00167020
		public override IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream does not support writing");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (numBytes < 0)
			{
				throw new ArgumentOutOfRangeException("numBytes", "Must be >= 0");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be >= 0");
			}
			if (numBytes > array.Length - offset)
			{
				throw new ArgumentException("array too small. numBytes/offset wrong.");
			}
			if (!this.async)
			{
				return base.BeginWrite(array, offset, numBytes, userCallback, stateObject);
			}
			FileStreamAsyncResult fileStreamAsyncResult = new FileStreamAsyncResult(userCallback, stateObject);
			fileStreamAsyncResult.BytesRead = -1;
			fileStreamAsyncResult.Count = numBytes;
			fileStreamAsyncResult.OriginalCount = numBytes;
			return new FileStream.WriteDelegate(this.WriteInternal).BeginInvoke(array, offset, numBytes, userCallback, stateObject);
		}

		/// <summary>Ends an asynchronous write operation and blocks until the I/O operation is complete. (Consider using <see cref="M:System.IO.FileStream.WriteAsync(System.Byte[],System.Int32,System.Int32,System.Threading.CancellationToken)" /> instead.)</summary>
		/// <param name="asyncResult">The pending asynchronous I/O request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">This <see cref="T:System.IAsyncResult" /> object was not created by calling <see cref="M:System.IO.Stream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> on this class.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.IO.FileStream.EndWrite(System.IAsyncResult)" /> is called multiple times.</exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		// Token: 0x0600699D RID: 27037 RVA: 0x00168EEC File Offset: 0x001670EC
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!this.async)
			{
				base.EndWrite(asyncResult);
				return;
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			FileStream.WriteDelegate writeDelegate = asyncResult2.AsyncDelegate as FileStream.WriteDelegate;
			if (writeDelegate == null)
			{
				throw new ArgumentException("Invalid IAsyncResult", "asyncResult");
			}
			writeDelegate.EndInvoke(asyncResult);
		}

		/// <summary>Sets the current position of this stream to the given value.</summary>
		/// <param name="offset">The point relative to <paramref name="origin" /> from which to begin seeking.</param>
		/// <param name="origin">Specifies the beginning, the end, or the current position as a reference point for <paramref name="offset" />, using a value of type <see cref="T:System.IO.SeekOrigin" />.</param>
		/// <returns>The new position in the stream.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the <see langword="FileStream" /> is constructed from a pipe or console output.</exception>
		/// <exception cref="T:System.ArgumentException">Seeking is attempted before the beginning of the stream.</exception>
		/// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed.</exception>
		// Token: 0x0600699E RID: 27038 RVA: 0x00168F54 File Offset: 0x00167154
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Length + offset;
				break;
			default:
				throw new ArgumentException("origin", "Invalid SeekOrigin");
			}
			if (num < 0L)
			{
				throw new IOException("Attempted to Seek before the beginning of the stream");
			}
			if (num < this.append_startpos)
			{
				throw new IOException("Can't seek back over pre-existing data in append mode");
			}
			this.FlushBuffer();
			MonoIOError monoIOError;
			this.buf_start = MonoIO.Seek(this.safeHandle, num, SeekOrigin.Begin, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			return this.buf_start;
		}

		/// <summary>Sets the length of this stream to the given value.</summary>
		/// <param name="value">The new length of the stream.</param>
		/// <exception cref="T:System.IO.IOException">An I/O error has occurred.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Attempted to set the <paramref name="value" /> parameter to less than 0.</exception>
		// Token: 0x0600699F RID: 27039 RVA: 0x00169028 File Offset: 0x00167228
		public override void SetLength(long value)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (!this.CanSeek)
			{
				throw new NotSupportedException("The stream does not support seeking");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The stream does not support writing");
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value is less than 0");
			}
			this.FlushBuffer();
			MonoIOError monoIOError;
			MonoIO.SetLength(this.safeHandle, value, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (this.Position > value)
			{
				this.Position = value;
			}
		}

		/// <summary>Clears buffers for this stream and causes any buffered data to be written to the file.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream is closed.</exception>
		// Token: 0x060069A0 RID: 27040 RVA: 0x001690BE File Offset: 0x001672BE
		public override void Flush()
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			this.FlushBuffer();
		}

		/// <summary>Clears buffers for this stream and causes any buffered data to be written to the file, and also clears all intermediate file buffers.</summary>
		/// <param name="flushToDisk">
		///   <see langword="true" /> to flush all intermediate file buffers; otherwise, <see langword="false" />.</param>
		// Token: 0x060069A1 RID: 27041 RVA: 0x001690E0 File Offset: 0x001672E0
		public virtual void Flush(bool flushToDisk)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			this.FlushBuffer();
			if (flushToDisk)
			{
				MonoIOError monoIOError;
				MonoIO.Flush(this.safeHandle, out monoIOError);
			}
		}

		/// <summary>Prevents other processes from reading from or writing to the <see cref="T:System.IO.FileStream" />.</summary>
		/// <param name="position">The beginning of the range to lock. The value of this parameter must be equal to or greater than zero (0).</param>
		/// <param name="length">The range to be locked.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> or <paramref name="length" /> is negative.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed.</exception>
		/// <exception cref="T:System.IO.IOException">The process cannot access the file because another process has locked a portion of the file.</exception>
		// Token: 0x060069A2 RID: 27042 RVA: 0x0016911C File Offset: 0x0016731C
		public virtual void Lock(long position, long length)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position must not be negative");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length must not be negative");
			}
			MonoIOError monoIOError;
			MonoIO.Lock(this.safeHandle, position, length, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
		}

		/// <summary>Allows access by other processes to all or part of a file that was previously locked.</summary>
		/// <param name="position">The beginning of the range to unlock.</param>
		/// <param name="length">The range to be unlocked.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="position" /> or <paramref name="length" /> is negative.</exception>
		// Token: 0x060069A3 RID: 27043 RVA: 0x00169188 File Offset: 0x00167388
		public virtual void Unlock(long position, long length)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (position < 0L)
			{
				throw new ArgumentOutOfRangeException("position must not be negative");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length must not be negative");
			}
			MonoIOError monoIOError;
			MonoIO.Unlock(this.safeHandle, position, length, out monoIOError);
			if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
		}

		/// <summary>Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the <see langword="FileStream" />.</summary>
		// Token: 0x060069A4 RID: 27044 RVA: 0x001691F4 File Offset: 0x001673F4
		~FileStream()
		{
			this.Dispose(false);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileStream" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060069A5 RID: 27045 RVA: 0x00169224 File Offset: 0x00167424
		protected override void Dispose(bool disposing)
		{
			Exception ex = null;
			if (this.safeHandle != null && !this.safeHandle.IsClosed)
			{
				try
				{
					this.FlushBuffer();
				}
				catch (Exception ex)
				{
				}
				if (this.owner)
				{
					MonoIOError monoIOError;
					MonoIO.Close(this.safeHandle.DangerousGetHandle(), out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					this.safeHandle.DangerousRelease();
				}
			}
			this.canseek = false;
			this.access = (FileAccess)0;
			if (disposing && this.buf != null)
			{
				if (this.buf.Length == 4096 && FileStream.buf_recycle == null)
				{
					object obj = FileStream.buf_recycle_lock;
					lock (obj)
					{
						if (FileStream.buf_recycle == null)
						{
							FileStream.buf_recycle = this.buf;
						}
					}
				}
				this.buf = null;
				GC.SuppressFinalize(this);
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.AccessControl.FileSecurity" /> object that encapsulates the access control list (ACL) entries for the file described by the current <see cref="T:System.IO.FileStream" /> object.</summary>
		/// <returns>An object that encapsulates the access control settings for the file described by the current <see cref="T:System.IO.FileStream" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">This operation is not supported on the current platform.  
		///  -or-  
		///  The caller does not have the required permission.</exception>
		// Token: 0x060069A6 RID: 27046 RVA: 0x0016931C File Offset: 0x0016751C
		public FileSecurity GetAccessControl()
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			return new FileSecurity(this.SafeFileHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		/// <summary>Applies access control list (ACL) entries described by a <see cref="T:System.Security.AccessControl.FileSecurity" /> object to the file described by the current <see cref="T:System.IO.FileStream" /> object.</summary>
		/// <param name="fileSecurity">An object that describes an ACL entry to apply to the current file.</param>
		/// <exception cref="T:System.ObjectDisposedException">The file is closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="fileSecurity" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.SystemException">The file could not be found or modified.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The current process does not have access to open the file.</exception>
		// Token: 0x060069A7 RID: 27047 RVA: 0x00169343 File Offset: 0x00167543
		public void SetAccessControl(FileSecurity fileSecurity)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			if (fileSecurity == null)
			{
				throw new ArgumentNullException("fileSecurity");
			}
			fileSecurity.PersistModifications(this.SafeFileHandle);
		}

		/// <summary>Asynchronously clears all buffers for this stream, causes any buffered data to be written to the underlying device, and monitors cancellation requests.</summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		// Token: 0x060069A8 RID: 27048 RVA: 0x00169377 File Offset: 0x00167577
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (this.safeHandle.IsClosed)
			{
				throw new ObjectDisposedException("Stream has been closed");
			}
			return base.FlushAsync(cancellationToken);
		}

		/// <summary>Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support reading.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous read operation.</exception>
		// Token: 0x060069A9 RID: 27049 RVA: 0x00169398 File Offset: 0x00167598
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.ReadAsync(buffer, offset, count, cancellationToken);
		}

		/// <summary>Asynchronously writes a sequence of bytes to the current stream, advances the current position within this stream by the number of bytes written, and monitors cancellation requests.</summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="count" /> is negative.</exception>
		/// <exception cref="T:System.ArgumentException">The sum of <paramref name="offset" /> and <paramref name="count" /> is larger than the buffer length.</exception>
		/// <exception cref="T:System.NotSupportedException">The stream does not support writing.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The stream has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The stream is currently in use by a previous write operation.</exception>
		// Token: 0x060069AA RID: 27050 RVA: 0x001693A5 File Offset: 0x001675A5
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x001693B2 File Offset: 0x001675B2
		private int ReadSegment(byte[] dest, int dest_offset, int count)
		{
			count = Math.Min(count, this.buf_length - this.buf_offset);
			if (count > 0)
			{
				Buffer.InternalBlockCopy(this.buf, this.buf_offset, dest, dest_offset, count);
				this.buf_offset += count;
			}
			return count;
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x001693F4 File Offset: 0x001675F4
		private int WriteSegment(byte[] src, int src_offset, int count)
		{
			if (count > this.buf_size - this.buf_offset)
			{
				count = this.buf_size - this.buf_offset;
			}
			if (count > 0)
			{
				Buffer.BlockCopy(src, src_offset, this.buf, this.buf_offset, count);
				this.buf_offset += count;
				if (this.buf_offset > this.buf_length)
				{
					this.buf_length = this.buf_offset;
				}
				this.buf_dirty = true;
			}
			return count;
		}

		// Token: 0x060069AD RID: 27053 RVA: 0x00169468 File Offset: 0x00167668
		private void FlushBuffer()
		{
			if (this.buf_dirty)
			{
				if (this.CanSeek && !this.isExposed)
				{
					MonoIOError monoIOError;
					MonoIO.Seek(this.safeHandle, this.buf_start, SeekOrigin.Begin, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
				}
				int i = this.buf_length;
				int num = 0;
				while (i > 0)
				{
					MonoIOError monoIOError;
					int num2 = MonoIO.Write(this.safeHandle, this.buf, num, this.buf_length, out monoIOError);
					if (monoIOError != MonoIOError.ERROR_SUCCESS)
					{
						throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
					}
					i -= num2;
					num += num2;
				}
			}
			this.buf_start += (long)this.buf_offset;
			this.buf_offset = (this.buf_length = 0);
			this.buf_dirty = false;
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x00169530 File Offset: 0x00167730
		private void FlushBufferIfDirty()
		{
			if (this.buf_dirty)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x060069AF RID: 27055 RVA: 0x00169540 File Offset: 0x00167740
		private void RefillBuffer()
		{
			this.FlushBuffer();
			this.buf_length = this.ReadData(this.safeHandle, this.buf, 0, this.buf_size);
		}

		// Token: 0x060069B0 RID: 27056 RVA: 0x00169568 File Offset: 0x00167768
		private int ReadData(SafeHandle safeHandle, byte[] buf, int offset, int count)
		{
			MonoIOError monoIOError;
			int num = MonoIO.Read(safeHandle, buf, offset, count, out monoIOError);
			if (monoIOError == MonoIOError.ERROR_BROKEN_PIPE)
			{
				num = 0;
			}
			else if (monoIOError != MonoIOError.ERROR_SUCCESS)
			{
				throw MonoIO.GetException(this.GetSecureFileName(this.name), monoIOError);
			}
			if (num == -1)
			{
				throw new IOException();
			}
			return num;
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x001695B0 File Offset: 0x001677B0
		private void InitBuffer(int size, bool isZeroSize)
		{
			if (isZeroSize)
			{
				size = 0;
				this.buf = new byte[1];
			}
			else
			{
				if (size <= 0)
				{
					throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
				}
				size = Math.Max(size, 8);
				if (size <= 4096 && FileStream.buf_recycle != null)
				{
					object obj = FileStream.buf_recycle_lock;
					lock (obj)
					{
						if (FileStream.buf_recycle != null)
						{
							this.buf = FileStream.buf_recycle;
							FileStream.buf_recycle = null;
						}
					}
				}
				if (this.buf == null)
				{
					this.buf = new byte[size];
				}
				else
				{
					Array.Clear(this.buf, 0, size);
				}
			}
			this.buf_size = size;
		}

		// Token: 0x060069B2 RID: 27058 RVA: 0x00169670 File Offset: 0x00167870
		private string GetSecureFileName(string filename)
		{
			if (!this.anonymous)
			{
				return Path.GetFullPath(filename);
			}
			return Path.GetFileName(filename);
		}

		// Token: 0x060069B3 RID: 27059 RVA: 0x00169687 File Offset: 0x00167887
		private string GetSecureFileName(string filename, bool full)
		{
			if (this.anonymous)
			{
				return Path.GetFileName(filename);
			}
			if (!full)
			{
				return filename;
			}
			return Path.GetFullPath(filename);
		}

		// Token: 0x060069B4 RID: 27060 RVA: 0x001696A3 File Offset: 0x001678A3
		// Note: this type is marked as 'beforefieldinit'.
		static FileStream()
		{
		}

		// Token: 0x04003D2E RID: 15662
		internal const int DefaultBufferSize = 4096;

		// Token: 0x04003D2F RID: 15663
		private static byte[] buf_recycle;

		// Token: 0x04003D30 RID: 15664
		private static readonly object buf_recycle_lock = new object();

		// Token: 0x04003D31 RID: 15665
		private byte[] buf;

		// Token: 0x04003D32 RID: 15666
		private string name;

		// Token: 0x04003D33 RID: 15667
		private SafeFileHandle safeHandle;

		// Token: 0x04003D34 RID: 15668
		private bool isExposed;

		// Token: 0x04003D35 RID: 15669
		private long append_startpos;

		// Token: 0x04003D36 RID: 15670
		private FileAccess access;

		// Token: 0x04003D37 RID: 15671
		private bool owner;

		// Token: 0x04003D38 RID: 15672
		private bool async;

		// Token: 0x04003D39 RID: 15673
		private bool canseek;

		// Token: 0x04003D3A RID: 15674
		private bool anonymous;

		// Token: 0x04003D3B RID: 15675
		private bool buf_dirty;

		// Token: 0x04003D3C RID: 15676
		private int buf_size;

		// Token: 0x04003D3D RID: 15677
		private int buf_length;

		// Token: 0x04003D3E RID: 15678
		private int buf_offset;

		// Token: 0x04003D3F RID: 15679
		private long buf_start;

		// Token: 0x02000B5F RID: 2911
		// (Invoke) Token: 0x060069B6 RID: 27062
		private delegate int ReadDelegate(byte[] buffer, int offset, int count);

		// Token: 0x02000B60 RID: 2912
		// (Invoke) Token: 0x060069BA RID: 27066
		private delegate void WriteDelegate(byte[] buffer, int offset, int count);
	}
}
