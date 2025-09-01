using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000003 RID: 3
internal static class Interop
{
	// Token: 0x02000004 RID: 4
	internal enum BOOL
	{
		// Token: 0x04000004 RID: 4
		FALSE,
		// Token: 0x04000005 RID: 5
		TRUE
	}

	// Token: 0x02000005 RID: 5
	internal class Errors
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002162 File Offset: 0x00000362
		public Errors()
		{
		}

		// Token: 0x04000006 RID: 6
		internal const int ERROR_SUCCESS = 0;

		// Token: 0x04000007 RID: 7
		internal const int ERROR_INVALID_FUNCTION = 1;

		// Token: 0x04000008 RID: 8
		internal const int ERROR_FILE_NOT_FOUND = 2;

		// Token: 0x04000009 RID: 9
		internal const int ERROR_PATH_NOT_FOUND = 3;

		// Token: 0x0400000A RID: 10
		internal const int ERROR_ACCESS_DENIED = 5;

		// Token: 0x0400000B RID: 11
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x0400000C RID: 12
		internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x0400000D RID: 13
		internal const int ERROR_INVALID_DATA = 13;

		// Token: 0x0400000E RID: 14
		internal const int ERROR_INVALID_DRIVE = 15;

		// Token: 0x0400000F RID: 15
		internal const int ERROR_NO_MORE_FILES = 18;

		// Token: 0x04000010 RID: 16
		internal const int ERROR_NOT_READY = 21;

		// Token: 0x04000011 RID: 17
		internal const int ERROR_BAD_COMMAND = 22;

		// Token: 0x04000012 RID: 18
		internal const int ERROR_BAD_LENGTH = 24;

		// Token: 0x04000013 RID: 19
		internal const int ERROR_SHARING_VIOLATION = 32;

		// Token: 0x04000014 RID: 20
		internal const int ERROR_LOCK_VIOLATION = 33;

		// Token: 0x04000015 RID: 21
		internal const int ERROR_HANDLE_EOF = 38;

		// Token: 0x04000016 RID: 22
		internal const int ERROR_BAD_NETPATH = 53;

		// Token: 0x04000017 RID: 23
		internal const int ERROR_BAD_NET_NAME = 67;

		// Token: 0x04000018 RID: 24
		internal const int ERROR_FILE_EXISTS = 80;

		// Token: 0x04000019 RID: 25
		internal const int ERROR_INVALID_PARAMETER = 87;

		// Token: 0x0400001A RID: 26
		internal const int ERROR_BROKEN_PIPE = 109;

		// Token: 0x0400001B RID: 27
		internal const int ERROR_SEM_TIMEOUT = 121;

		// Token: 0x0400001C RID: 28
		internal const int ERROR_CALL_NOT_IMPLEMENTED = 120;

		// Token: 0x0400001D RID: 29
		internal const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x0400001E RID: 30
		internal const int ERROR_INVALID_NAME = 123;

		// Token: 0x0400001F RID: 31
		internal const int ERROR_NEGATIVE_SEEK = 131;

		// Token: 0x04000020 RID: 32
		internal const int ERROR_DIR_NOT_EMPTY = 145;

		// Token: 0x04000021 RID: 33
		internal const int ERROR_BAD_PATHNAME = 161;

		// Token: 0x04000022 RID: 34
		internal const int ERROR_LOCK_FAILED = 167;

		// Token: 0x04000023 RID: 35
		internal const int ERROR_BUSY = 170;

		// Token: 0x04000024 RID: 36
		internal const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000025 RID: 37
		internal const int ERROR_BAD_EXE_FORMAT = 193;

		// Token: 0x04000026 RID: 38
		internal const int ERROR_ENVVAR_NOT_FOUND = 203;

		// Token: 0x04000027 RID: 39
		internal const int ERROR_FILENAME_EXCED_RANGE = 206;

		// Token: 0x04000028 RID: 40
		internal const int ERROR_EXE_MACHINE_TYPE_MISMATCH = 216;

		// Token: 0x04000029 RID: 41
		internal const int ERROR_PIPE_BUSY = 231;

		// Token: 0x0400002A RID: 42
		internal const int ERROR_NO_DATA = 232;

		// Token: 0x0400002B RID: 43
		internal const int ERROR_PIPE_NOT_CONNECTED = 233;

		// Token: 0x0400002C RID: 44
		internal const int ERROR_MORE_DATA = 234;

		// Token: 0x0400002D RID: 45
		internal const int ERROR_NO_MORE_ITEMS = 259;

		// Token: 0x0400002E RID: 46
		internal const int ERROR_DIRECTORY = 267;

		// Token: 0x0400002F RID: 47
		internal const int ERROR_PARTIAL_COPY = 299;

		// Token: 0x04000030 RID: 48
		internal const int ERROR_ARITHMETIC_OVERFLOW = 534;

		// Token: 0x04000031 RID: 49
		internal const int ERROR_PIPE_CONNECTED = 535;

		// Token: 0x04000032 RID: 50
		internal const int ERROR_PIPE_LISTENING = 536;

		// Token: 0x04000033 RID: 51
		internal const int ERROR_OPERATION_ABORTED = 995;

		// Token: 0x04000034 RID: 52
		internal const int ERROR_IO_INCOMPLETE = 996;

		// Token: 0x04000035 RID: 53
		internal const int ERROR_IO_PENDING = 997;

		// Token: 0x04000036 RID: 54
		internal const int ERROR_NO_TOKEN = 1008;

		// Token: 0x04000037 RID: 55
		internal const int ERROR_DLL_INIT_FAILED = 1114;

		// Token: 0x04000038 RID: 56
		internal const int ERROR_COUNTER_TIMEOUT = 1121;

		// Token: 0x04000039 RID: 57
		internal const int ERROR_NO_ASSOCIATION = 1155;

		// Token: 0x0400003A RID: 58
		internal const int ERROR_DDE_FAIL = 1156;

		// Token: 0x0400003B RID: 59
		internal const int ERROR_DLL_NOT_FOUND = 1157;

		// Token: 0x0400003C RID: 60
		internal const int ERROR_NOT_FOUND = 1168;

		// Token: 0x0400003D RID: 61
		internal const int ERROR_NETWORK_UNREACHABLE = 1231;

		// Token: 0x0400003E RID: 62
		internal const int ERROR_NON_ACCOUNT_SID = 1257;

		// Token: 0x0400003F RID: 63
		internal const int ERROR_NOT_ALL_ASSIGNED = 1300;

		// Token: 0x04000040 RID: 64
		internal const int ERROR_UNKNOWN_REVISION = 1305;

		// Token: 0x04000041 RID: 65
		internal const int ERROR_INVALID_OWNER = 1307;

		// Token: 0x04000042 RID: 66
		internal const int ERROR_INVALID_PRIMARY_GROUP = 1308;

		// Token: 0x04000043 RID: 67
		internal const int ERROR_NO_SUCH_PRIVILEGE = 1313;

		// Token: 0x04000044 RID: 68
		internal const int ERROR_PRIVILEGE_NOT_HELD = 1314;

		// Token: 0x04000045 RID: 69
		internal const int ERROR_INVALID_ACL = 1336;

		// Token: 0x04000046 RID: 70
		internal const int ERROR_INVALID_SECURITY_DESCR = 1338;

		// Token: 0x04000047 RID: 71
		internal const int ERROR_INVALID_SID = 1337;

		// Token: 0x04000048 RID: 72
		internal const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;

		// Token: 0x04000049 RID: 73
		internal const int ERROR_CANT_OPEN_ANONYMOUS = 1347;

		// Token: 0x0400004A RID: 74
		internal const int ERROR_NO_SECURITY_ON_OBJECT = 1350;

		// Token: 0x0400004B RID: 75
		internal const int ERROR_CLASS_ALREADY_EXISTS = 1410;

		// Token: 0x0400004C RID: 76
		internal const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;

		// Token: 0x0400004D RID: 77
		internal const int ERROR_RESOURCE_LANG_NOT_FOUND = 1815;

		// Token: 0x0400004E RID: 78
		internal const int EFail = -2147467259;

		// Token: 0x0400004F RID: 79
		internal const int E_FILENOTFOUND = -2147024894;
	}

	// Token: 0x02000006 RID: 6
	internal static class Libraries
	{
		// Token: 0x04000050 RID: 80
		internal const string Advapi32 = "advapi32.dll";

		// Token: 0x04000051 RID: 81
		internal const string BCrypt = "BCrypt.dll";

		// Token: 0x04000052 RID: 82
		internal const string CoreComm_L1_1_1 = "api-ms-win-core-comm-l1-1-1.dll";

		// Token: 0x04000053 RID: 83
		internal const string Crypt32 = "crypt32.dll";

		// Token: 0x04000054 RID: 84
		internal const string Error_L1 = "api-ms-win-core-winrt-error-l1-1-0.dll";

		// Token: 0x04000055 RID: 85
		internal const string HttpApi = "httpapi.dll";

		// Token: 0x04000056 RID: 86
		internal const string IpHlpApi = "iphlpapi.dll";

		// Token: 0x04000057 RID: 87
		internal const string Kernel32 = "kernel32.dll";

		// Token: 0x04000058 RID: 88
		internal const string Memory_L1_3 = "api-ms-win-core-memory-l1-1-3.dll";

		// Token: 0x04000059 RID: 89
		internal const string Mswsock = "mswsock.dll";

		// Token: 0x0400005A RID: 90
		internal const string NCrypt = "ncrypt.dll";

		// Token: 0x0400005B RID: 91
		internal const string NtDll = "ntdll.dll";

		// Token: 0x0400005C RID: 92
		internal const string Odbc32 = "odbc32.dll";

		// Token: 0x0400005D RID: 93
		internal const string OleAut32 = "oleaut32.dll";

		// Token: 0x0400005E RID: 94
		internal const string PerfCounter = "perfcounter.dll";

		// Token: 0x0400005F RID: 95
		internal const string RoBuffer = "api-ms-win-core-winrt-robuffer-l1-1-0.dll";

		// Token: 0x04000060 RID: 96
		internal const string Secur32 = "secur32.dll";

		// Token: 0x04000061 RID: 97
		internal const string Shell32 = "shell32.dll";

		// Token: 0x04000062 RID: 98
		internal const string SspiCli = "sspicli.dll";

		// Token: 0x04000063 RID: 99
		internal const string User32 = "user32.dll";

		// Token: 0x04000064 RID: 100
		internal const string Version = "version.dll";

		// Token: 0x04000065 RID: 101
		internal const string WebSocket = "websocket.dll";

		// Token: 0x04000066 RID: 102
		internal const string WinHttp = "winhttp.dll";

		// Token: 0x04000067 RID: 103
		internal const string Ws2_32 = "ws2_32.dll";

		// Token: 0x04000068 RID: 104
		internal const string Wtsapi32 = "wtsapi32.dll";

		// Token: 0x04000069 RID: 105
		internal const string CompressionNative = "clrcompression.dll";
	}

	// Token: 0x02000007 RID: 7
	internal static class Advapi32
	{
		// Token: 0x06000008 RID: 8
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ImpersonateNamedPipeClient(SafePipeHandle hNamedPipe);

		// Token: 0x06000009 RID: 9
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern bool RevertToSelf();
	}

	// Token: 0x02000008 RID: 8
	internal class Kernel32
	{
		// Token: 0x0600000A RID: 10
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool CancelIoEx(SafeHandle handle, NativeOverlapped* lpOverlapped);

		// Token: 0x0600000B RID: 11
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600000C RID: 12
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool ConnectNamedPipe(SafePipeHandle handle, NativeOverlapped* overlapped);

		// Token: 0x0600000D RID: 13
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ConnectNamedPipe(SafePipeHandle handle, IntPtr overlapped);

		// Token: 0x0600000E RID: 14
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CreateNamedPipeW", SetLastError = true)]
		internal static extern SafePipeHandle CreateNamedPipe(string pipeName, int openMode, int pipeMode, int maxInstances, int outBufferSize, int inBufferSize, int defaultTimeout, ref Interop.Kernel32.SECURITY_ATTRIBUTES securityAttributes);

		// Token: 0x0600000F RID: 15
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CreateFileW", SetLastError = true)]
		internal static extern SafePipeHandle CreateNamedPipeClient(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, ref Interop.Kernel32.SECURITY_ATTRIBUTES secAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000010 RID: 16
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool CreatePipe(out SafePipeHandle hReadPipe, out SafePipeHandle hWritePipe, ref Interop.Kernel32.SECURITY_ATTRIBUTES lpPipeAttributes, int nSize);

		// Token: 0x06000011 RID: 17
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DisconnectNamedPipe(SafePipeHandle hNamedPipe);

		// Token: 0x06000012 RID: 18
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, SafePipeHandle hSourceHandle, IntPtr hTargetProcessHandle, out SafePipeHandle lpTargetHandle, uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

		// Token: 0x06000013 RID: 19
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FlushFileBuffers(SafeHandle hHandle);

		// Token: 0x06000014 RID: 20
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Unicode, EntryPoint = "FormatMessageW", SetLastError = true)]
		private unsafe static extern int FormatMessage(int dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, char* lpBuffer, int nSize, IntPtr[] arguments);

		// Token: 0x06000015 RID: 21 RVA: 0x0000216A File Offset: 0x0000036A
		internal static string GetMessage(int errorCode)
		{
			return Interop.Kernel32.GetMessage(IntPtr.Zero, errorCode);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002178 File Offset: 0x00000378
		internal unsafe static string GetMessage(IntPtr moduleHandle, int errorCode)
		{
			Span<char> buffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
			string result;
			while (!Interop.Kernel32.TryGetErrorMessage(moduleHandle, errorCode, buffer, out result))
			{
				buffer = new char[buffer.Length * 4];
				if (buffer.Length >= 66560)
				{
					return string.Format("Unknown error (0x{0:x})", errorCode);
				}
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021D8 File Offset: 0x000003D8
		private unsafe static bool TryGetErrorMessage(IntPtr moduleHandle, int errorCode, Span<char> buffer, out string errorMsg)
		{
			int num = 12800;
			if (moduleHandle != IntPtr.Zero)
			{
				num |= 2048;
			}
			int num2;
			fixed (char* reference = MemoryMarshal.GetReference<char>(buffer))
			{
				char* lpBuffer = reference;
				num2 = Interop.Kernel32.FormatMessage(num, moduleHandle, (uint)errorCode, 0, lpBuffer, buffer.Length, null);
			}
			if (num2 != 0)
			{
				int i;
				for (i = num2; i > 0; i--)
				{
					char c = *buffer[i - 1];
					if (c > ' ' && c != '.')
					{
						break;
					}
				}
				errorMsg = buffer.Slice(0, i).ToString();
			}
			else
			{
				if (Marshal.GetLastWin32Error() == 122)
				{
					errorMsg = "";
					return false;
				}
				errorMsg = string.Format("Unknown error (0x{0:x})", errorCode);
			}
			return true;
		}

		// Token: 0x06000018 RID: 24
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x06000019 RID: 25
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetFileType(SafeHandle hFile);

		// Token: 0x0600001A RID: 26
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetNamedPipeHandleStateW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeHandleState(SafePipeHandle hNamedPipe, out int lpState, IntPtr lpCurInstances, IntPtr lpMaxCollectionCount, IntPtr lpCollectDataTimeout, IntPtr lpUserName, int nMaxUserNameSize);

		// Token: 0x0600001B RID: 27
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetNamedPipeHandleStateW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeHandleState(SafePipeHandle hNamedPipe, IntPtr lpState, IntPtr lpCurInstances, IntPtr lpMaxCollectionCount, IntPtr lpCollectDataTimeout, [Out] StringBuilder lpUserName, int nMaxUserNameSize);

		// Token: 0x0600001C RID: 28
		[DllImport("kernel32.dll", EntryPoint = "GetNamedPipeHandleStateW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeHandleState(SafePipeHandle hNamedPipe, IntPtr lpState, out int lpCurInstances, IntPtr lpMaxCollectionCount, IntPtr lpCollectDataTimeout, IntPtr lpUserName, int nMaxUserNameSize);

		// Token: 0x0600001D RID: 29
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeInfo(SafePipeHandle hNamedPipe, out int lpFlags, IntPtr lpOutBufferSize, IntPtr lpInBufferSize, IntPtr lpMaxInstances);

		// Token: 0x0600001E RID: 30
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeInfo(SafePipeHandle hNamedPipe, IntPtr lpFlags, out int lpOutBufferSize, IntPtr lpInBufferSize, IntPtr lpMaxInstances);

		// Token: 0x0600001F RID: 31
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetNamedPipeInfo(SafePipeHandle hNamedPipe, IntPtr lpFlags, IntPtr lpOutBufferSize, out int lpInBufferSize, IntPtr lpMaxInstances);

		// Token: 0x06000020 RID: 32
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafeHandle handle, byte* bytes, int numBytesToRead, out int numBytesRead, IntPtr mustBeZero);

		// Token: 0x06000021 RID: 33
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafeHandle handle, byte* bytes, int numBytesToRead, IntPtr numBytesRead_mustBeZero, NativeOverlapped* overlapped);

		// Token: 0x06000022 RID: 34
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool SetNamedPipeHandleState(SafePipeHandle hNamedPipe, int* lpMode, IntPtr lpMaxCollectionCount, IntPtr lpCollectDataTimeout);

		// Token: 0x06000023 RID: 35
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "WaitNamedPipeW", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool WaitNamedPipe(string name, int timeout);

		// Token: 0x06000024 RID: 36
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafeHandle handle, byte* bytes, int numBytesToWrite, out int numBytesWritten, IntPtr mustBeZero);

		// Token: 0x06000025 RID: 37
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafeHandle handle, byte* bytes, int numBytesToWrite, IntPtr numBytesWritten_mustBeZero, NativeOverlapped* lpOverlapped);

		// Token: 0x06000026 RID: 38 RVA: 0x00002162 File Offset: 0x00000362
		public Kernel32()
		{
		}

		// Token: 0x0400006A RID: 106
		internal const uint SEM_FAILCRITICALERRORS = 1U;

		// Token: 0x0400006B RID: 107
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x0400006C RID: 108
		private const int FORMAT_MESSAGE_FROM_HMODULE = 2048;

		// Token: 0x0400006D RID: 109
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x0400006E RID: 110
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

		// Token: 0x0400006F RID: 111
		private const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04000070 RID: 112
		private const int InitialBufferSize = 256;

		// Token: 0x04000071 RID: 113
		private const int BufferSizeIncreaseFactor = 4;

		// Token: 0x04000072 RID: 114
		private const int MaxAllowedBufferSize = 66560;

		// Token: 0x04000073 RID: 115
		internal const int MAX_PATH = 260;

		// Token: 0x04000074 RID: 116
		internal const int CREDUI_MAX_USERNAME_LENGTH = 513;

		// Token: 0x02000009 RID: 9
		internal class IOReparseOptions
		{
			// Token: 0x06000027 RID: 39 RVA: 0x00002162 File Offset: 0x00000362
			public IOReparseOptions()
			{
			}

			// Token: 0x04000075 RID: 117
			internal const uint IO_REPARSE_TAG_FILE_PLACEHOLDER = 2147483669U;

			// Token: 0x04000076 RID: 118
			internal const uint IO_REPARSE_TAG_MOUNT_POINT = 2684354563U;
		}

		// Token: 0x0200000A RID: 10
		internal class FileOperations
		{
			// Token: 0x06000028 RID: 40 RVA: 0x00002162 File Offset: 0x00000362
			public FileOperations()
			{
			}

			// Token: 0x04000077 RID: 119
			internal const int OPEN_EXISTING = 3;

			// Token: 0x04000078 RID: 120
			internal const int COPY_FILE_FAIL_IF_EXISTS = 1;

			// Token: 0x04000079 RID: 121
			internal const int FILE_ACTION_ADDED = 1;

			// Token: 0x0400007A RID: 122
			internal const int FILE_ACTION_REMOVED = 2;

			// Token: 0x0400007B RID: 123
			internal const int FILE_ACTION_MODIFIED = 3;

			// Token: 0x0400007C RID: 124
			internal const int FILE_ACTION_RENAMED_OLD_NAME = 4;

			// Token: 0x0400007D RID: 125
			internal const int FILE_ACTION_RENAMED_NEW_NAME = 5;

			// Token: 0x0400007E RID: 126
			internal const int FILE_FLAG_BACKUP_SEMANTICS = 33554432;

			// Token: 0x0400007F RID: 127
			internal const int FILE_FLAG_FIRST_PIPE_INSTANCE = 524288;

			// Token: 0x04000080 RID: 128
			internal const int FILE_FLAG_OVERLAPPED = 1073741824;

			// Token: 0x04000081 RID: 129
			internal const int FILE_LIST_DIRECTORY = 1;
		}

		// Token: 0x0200000B RID: 11
		internal class FileTypes
		{
			// Token: 0x06000029 RID: 41 RVA: 0x00002162 File Offset: 0x00000362
			public FileTypes()
			{
			}

			// Token: 0x04000082 RID: 130
			internal const int FILE_TYPE_UNKNOWN = 0;

			// Token: 0x04000083 RID: 131
			internal const int FILE_TYPE_DISK = 1;

			// Token: 0x04000084 RID: 132
			internal const int FILE_TYPE_CHAR = 2;

			// Token: 0x04000085 RID: 133
			internal const int FILE_TYPE_PIPE = 3;
		}

		// Token: 0x0200000C RID: 12
		internal class GenericOperations
		{
			// Token: 0x0600002A RID: 42 RVA: 0x00002162 File Offset: 0x00000362
			public GenericOperations()
			{
			}

			// Token: 0x04000086 RID: 134
			internal const int GENERIC_READ = -2147483648;

			// Token: 0x04000087 RID: 135
			internal const int GENERIC_WRITE = 1073741824;
		}

		// Token: 0x0200000D RID: 13
		internal class HandleOptions
		{
			// Token: 0x0600002B RID: 43 RVA: 0x00002162 File Offset: 0x00000362
			public HandleOptions()
			{
			}

			// Token: 0x04000088 RID: 136
			internal const int DUPLICATE_SAME_ACCESS = 2;

			// Token: 0x04000089 RID: 137
			internal const int STILL_ACTIVE = 259;

			// Token: 0x0400008A RID: 138
			internal const int TOKEN_ADJUST_PRIVILEGES = 32;
		}

		// Token: 0x0200000E RID: 14
		internal class PipeOptions
		{
			// Token: 0x0600002C RID: 44 RVA: 0x00002162 File Offset: 0x00000362
			public PipeOptions()
			{
			}

			// Token: 0x0400008B RID: 139
			internal const int PIPE_ACCESS_INBOUND = 1;

			// Token: 0x0400008C RID: 140
			internal const int PIPE_ACCESS_OUTBOUND = 2;

			// Token: 0x0400008D RID: 141
			internal const int PIPE_ACCESS_DUPLEX = 3;

			// Token: 0x0400008E RID: 142
			internal const int PIPE_TYPE_BYTE = 0;

			// Token: 0x0400008F RID: 143
			internal const int PIPE_TYPE_MESSAGE = 4;

			// Token: 0x04000090 RID: 144
			internal const int PIPE_READMODE_BYTE = 0;

			// Token: 0x04000091 RID: 145
			internal const int PIPE_READMODE_MESSAGE = 2;

			// Token: 0x04000092 RID: 146
			internal const int PIPE_UNLIMITED_INSTANCES = 255;
		}

		// Token: 0x0200000F RID: 15
		internal struct SECURITY_ATTRIBUTES
		{
			// Token: 0x04000093 RID: 147
			internal uint nLength;

			// Token: 0x04000094 RID: 148
			internal IntPtr lpSecurityDescriptor;

			// Token: 0x04000095 RID: 149
			internal Interop.BOOL bInheritHandle;
		}

		// Token: 0x02000010 RID: 16
		internal class SecurityOptions
		{
			// Token: 0x0600002D RID: 45 RVA: 0x00002162 File Offset: 0x00000362
			public SecurityOptions()
			{
			}

			// Token: 0x04000096 RID: 150
			internal const int SECURITY_SQOS_PRESENT = 1048576;

			// Token: 0x04000097 RID: 151
			internal const int SECURITY_ANONYMOUS = 0;

			// Token: 0x04000098 RID: 152
			internal const int SECURITY_IDENTIFICATION = 65536;

			// Token: 0x04000099 RID: 153
			internal const int SECURITY_IMPERSONATION = 131072;

			// Token: 0x0400009A RID: 154
			internal const int SECURITY_DELEGATION = 196608;
		}
	}
}
