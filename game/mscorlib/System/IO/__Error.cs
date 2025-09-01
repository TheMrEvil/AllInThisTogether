using System;
using System.Security;
using Microsoft.Win32;

namespace System.IO
{
	// Token: 0x02000B55 RID: 2901
	internal static class __Error
	{
		// Token: 0x060068EC RID: 26860 RVA: 0x001664EE File Offset: 0x001646EE
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(Environment.GetResourceString("Unable to read beyond the end of the stream."));
		}

		// Token: 0x060068ED RID: 26861 RVA: 0x001664FF File Offset: 0x001646FF
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed file."));
		}

		// Token: 0x060068EE RID: 26862 RVA: 0x00166511 File Offset: 0x00164711
		internal static void StreamIsClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a closed Stream."));
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x00166523 File Offset: 0x00164723
		internal static void MemoryStreamNotExpandable()
		{
			throw new NotSupportedException(Environment.GetResourceString("Memory stream is not expandable."));
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x00166534 File Offset: 0x00164734
		internal static void ReaderClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot read from a closed TextReader."));
		}

		// Token: 0x060068F1 RID: 26865 RVA: 0x000A84B2 File Offset: 0x000A66B2
		internal static void ReadNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support reading."));
		}

		// Token: 0x060068F2 RID: 26866 RVA: 0x000A84A1 File Offset: 0x000A66A1
		internal static void SeekNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support seeking."));
		}

		// Token: 0x060068F3 RID: 26867 RVA: 0x00166546 File Offset: 0x00164746
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(Environment.GetResourceString("IAsyncResult object did not come from the corresponding async method on this type."));
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x00166557 File Offset: 0x00164757
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(Environment.GetResourceString("EndRead can only be called once for each asynchronous operation."));
		}

		// Token: 0x060068F5 RID: 26869 RVA: 0x00166568 File Offset: 0x00164768
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(Environment.GetResourceString("EndWrite can only be called once for each asynchronous operation."));
		}

		// Token: 0x060068F6 RID: 26870 RVA: 0x0016657C File Offset: 0x0016477C
		[SecurityCritical]
		internal static string GetDisplayablePath(string path, bool isInvalidPath)
		{
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			if (path.Length < 2)
			{
				return path;
			}
			if (PathInternal.IsPartiallyQualified(path) && !isInvalidPath)
			{
				return path;
			}
			bool flag = false;
			try
			{
				if (!isInvalidPath)
				{
					flag = true;
				}
			}
			catch (SecurityException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			if (!flag)
			{
				if (Path.IsDirectorySeparator(path[path.Length - 1]))
				{
					path = Environment.GetResourceString("<Path discovery permission to the specified directory was denied.>");
				}
				else
				{
					path = Path.GetFileName(path);
				}
			}
			return path;
		}

		// Token: 0x060068F7 RID: 26871 RVA: 0x00166618 File Offset: 0x00164818
		[SecurityCritical]
		internal static void WinIOError(int errorCode, string maybeFullPath)
		{
			bool isInvalidPath = errorCode == 123 || errorCode == 161;
			string displayablePath = __Error.GetDisplayablePath(maybeFullPath, isInvalidPath);
			if (errorCode <= 80)
			{
				if (errorCode <= 15)
				{
					switch (errorCode)
					{
					case 2:
						if (displayablePath.Length == 0)
						{
							throw new FileNotFoundException(Environment.GetResourceString("Unable to find the specified file."));
						}
						throw new FileNotFoundException(Environment.GetResourceString("Could not find file '{0}'.", new object[]
						{
							displayablePath
						}), displayablePath);
					case 3:
						if (displayablePath.Length == 0)
						{
							throw new DirectoryNotFoundException(Environment.GetResourceString("Could not find a part of the path."));
						}
						throw new DirectoryNotFoundException(Environment.GetResourceString("Could not find a part of the path '{0}'.", new object[]
						{
							displayablePath
						}));
					case 4:
						break;
					case 5:
						if (displayablePath.Length == 0)
						{
							throw new UnauthorizedAccessException(Environment.GetResourceString("Access to the path is denied."));
						}
						throw new UnauthorizedAccessException(Environment.GetResourceString("Access to the path '{0}' is denied.", new object[]
						{
							displayablePath
						}));
					default:
						if (errorCode == 15)
						{
							throw new DriveNotFoundException(Environment.GetResourceString("Could not find the drive '{0}'. The drive might not be ready or might not be mapped.", new object[]
							{
								displayablePath
							}));
						}
						break;
					}
				}
				else if (errorCode != 32)
				{
					if (errorCode == 80)
					{
						if (displayablePath.Length != 0)
						{
							throw new IOException(Environment.GetResourceString("The file '{0}' already exists.", new object[]
							{
								displayablePath
							}), Win32Native.MakeHRFromErrorCode(errorCode));
						}
					}
				}
				else
				{
					if (displayablePath.Length == 0)
					{
						throw new IOException(Environment.GetResourceString("The process cannot access the file because it is being used by another process."), Win32Native.MakeHRFromErrorCode(errorCode));
					}
					throw new IOException(Environment.GetResourceString("The process cannot access the file '{0}' because it is being used by another process.", new object[]
					{
						displayablePath
					}), Win32Native.MakeHRFromErrorCode(errorCode));
				}
			}
			else if (errorCode <= 183)
			{
				if (errorCode == 87)
				{
					throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode));
				}
				if (errorCode == 183)
				{
					if (displayablePath.Length != 0)
					{
						throw new IOException(Environment.GetResourceString("Cannot create \"{0}\" because a file or directory with the same name already exists.", new object[]
						{
							displayablePath
						}), Win32Native.MakeHRFromErrorCode(errorCode));
					}
				}
			}
			else
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(Environment.GetResourceString("The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters."));
				}
				if (errorCode == 995)
				{
					throw new OperationCanceledException();
				}
			}
			throw new IOException(Win32Native.GetMessage(errorCode), Win32Native.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x00166838 File Offset: 0x00164A38
		internal static void WriteNotSupported()
		{
			throw new NotSupportedException(Environment.GetResourceString("Stream does not support writing."));
		}

		// Token: 0x060068F9 RID: 26873 RVA: 0x00166849 File Offset: 0x00164A49
		internal static void WriterClosed()
		{
			throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot write to a closed TextWriter."));
		}
	}
}
