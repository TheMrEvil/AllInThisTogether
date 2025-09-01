using System;

namespace System.IO.Ports
{
	// Token: 0x02000529 RID: 1321
	internal static class InternalResources
	{
		// Token: 0x06002A87 RID: 10887 RVA: 0x00092CCB File Offset: 0x00090ECB
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(SR.GetString("Unable to read beyond the end of the stream."));
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x00092CDC File Offset: 0x00090EDC
		internal static string GetMessage(int errorCode)
		{
			return SR.GetString("Unknown Error '{0}'.", new object[]
			{
				errorCode
			});
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x00092CF7 File Offset: 0x00090EF7
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, SR.GetString("The port is closed."));
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x00092D09 File Offset: 0x00090F09
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(SR.GetString("IAsyncResult object did not come from the corresponding async method on this type."));
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x00092D1A File Offset: 0x00090F1A
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(SR.GetString("EndRead can only be called once for each asynchronous operation."));
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00092D2B File Offset: 0x00090F2B
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(SR.GetString("EndWrite can only be called once for each asynchronous operation."));
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00092D3C File Offset: 0x00090F3C
		internal static void WinIOError(int errorCode, string str)
		{
			if (errorCode <= 5)
			{
				if (errorCode - 2 > 1)
				{
					if (errorCode == 5)
					{
						if (str.Length == 0)
						{
							throw new UnauthorizedAccessException(SR.GetString("Access to the path is denied."));
						}
						throw new UnauthorizedAccessException(SR.GetString("Access to the path '{0}' is denied.", new object[]
						{
							str
						}));
					}
				}
				else
				{
					if (str.Length == 0)
					{
						throw new IOException(SR.GetString("The specified port does not exist."));
					}
					throw new IOException(SR.GetString("The port '{0}' does not exist.", new object[]
					{
						str
					}));
				}
			}
			else if (errorCode != 32)
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(SR.GetString("The specified file name or path is too long, or a component of the specified path is too long."));
				}
			}
			else
			{
				if (str.Length == 0)
				{
					throw new IOException(SR.GetString("The process cannot access the file because it is being used by another process."));
				}
				throw new IOException(SR.GetString("The process cannot access the file '{0}' because it is being used by another process.", new object[]
				{
					str
				}));
			}
			throw new IOException(InternalResources.GetMessage(errorCode), InternalResources.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x00092E28 File Offset: 0x00091028
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}
	}
}
