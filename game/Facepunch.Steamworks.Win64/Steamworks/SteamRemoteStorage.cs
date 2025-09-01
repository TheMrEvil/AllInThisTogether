using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Steamworks
{
	// Token: 0x020000A0 RID: 160
	public class SteamRemoteStorage : SteamClientClass<SteamRemoteStorage>
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0000DF68 File Offset: 0x0000C168
		internal static ISteamRemoteStorage Internal
		{
			get
			{
				return SteamClientClass<SteamRemoteStorage>.Interface as ISteamRemoteStorage;
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0000DF74 File Offset: 0x0000C174
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamRemoteStorage(server));
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0000DF88 File Offset: 0x0000C188
		public unsafe static bool FileWrite(string filename, byte[] data)
		{
			byte* value;
			if (data == null || data.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &data[0];
			}
			return SteamRemoteStorage.Internal.FileWrite(filename, (IntPtr)((void*)value), data.Length);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		public unsafe static byte[] FileRead(string filename)
		{
			int num = SteamRemoteStorage.FileSize(filename);
			bool flag = num <= 0;
			byte[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] array = new byte[num];
				byte[] array2;
				byte* value;
				if ((array2 = array) == null || array2.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array2[0];
				}
				int num2 = SteamRemoteStorage.Internal.FileRead(filename, (IntPtr)((void*)value), num);
				result = array;
			}
			return result;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0000E02B File Offset: 0x0000C22B
		public static bool FileExists(string filename)
		{
			return SteamRemoteStorage.Internal.FileExists(filename);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0000E038 File Offset: 0x0000C238
		public static bool FilePersisted(string filename)
		{
			return SteamRemoteStorage.Internal.FilePersisted(filename);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000E045 File Offset: 0x0000C245
		public static DateTime FileTime(string filename)
		{
			return Epoch.ToDateTime(SteamRemoteStorage.Internal.GetFileTimestamp(filename));
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000E05C File Offset: 0x0000C25C
		public static int FileSize(string filename)
		{
			return SteamRemoteStorage.Internal.GetFileSize(filename);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000E069 File Offset: 0x0000C269
		public static bool FileForget(string filename)
		{
			return SteamRemoteStorage.Internal.FileForget(filename);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000E076 File Offset: 0x0000C276
		public static bool FileDelete(string filename)
		{
			return SteamRemoteStorage.Internal.FileDelete(filename);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0000E084 File Offset: 0x0000C284
		public static ulong QuotaBytes
		{
			get
			{
				ulong result = 0UL;
				ulong num = 0UL;
				SteamRemoteStorage.Internal.GetQuota(ref result, ref num);
				return result;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		public static ulong QuotaUsedBytes
		{
			get
			{
				ulong num = 0UL;
				ulong num2 = 0UL;
				SteamRemoteStorage.Internal.GetQuota(ref num, ref num2);
				return num - num2;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		public static ulong QuotaRemainingBytes
		{
			get
			{
				ulong num = 0UL;
				ulong result = 0UL;
				SteamRemoteStorage.Internal.GetQuota(ref num, ref result);
				return result;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0000E100 File Offset: 0x0000C300
		public static bool IsCloudEnabled
		{
			get
			{
				return SteamRemoteStorage.IsCloudEnabledForAccount && SteamRemoteStorage.IsCloudEnabledForApp;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0000E111 File Offset: 0x0000C311
		public static bool IsCloudEnabledForAccount
		{
			get
			{
				return SteamRemoteStorage.Internal.IsCloudEnabledForAccount();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0000E11D File Offset: 0x0000C31D
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0000E129 File Offset: 0x0000C329
		public static bool IsCloudEnabledForApp
		{
			get
			{
				return SteamRemoteStorage.Internal.IsCloudEnabledForApp();
			}
			set
			{
				SteamRemoteStorage.Internal.SetCloudEnabledForApp(value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0000E137 File Offset: 0x0000C337
		public static int FileCount
		{
			get
			{
				return SteamRemoteStorage.Internal.GetFileCount();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0000E144 File Offset: 0x0000C344
		public static IEnumerable<string> Files
		{
			get
			{
				int _ = 0;
				int num;
				for (int i = 0; i < SteamRemoteStorage.FileCount; i = num + 1)
				{
					string filename = SteamRemoteStorage.Internal.GetFileNameAndSize(i, ref _);
					yield return filename;
					filename = null;
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0000E15C File Offset: 0x0000C35C
		public SteamRemoteStorage()
		{
		}

		// Token: 0x0200023D RID: 573
		[CompilerGenerated]
		private sealed class <get_Files>d__27 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x06001146 RID: 4422 RVA: 0x0001E5FC File Offset: 0x0001C7FC
			[DebuggerHidden]
			public <get_Files>d__27(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06001147 RID: 4423 RVA: 0x0001E617 File Offset: 0x0001C817
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001148 RID: 4424 RVA: 0x0001E61C File Offset: 0x0001C81C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					filename = null;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					_ = 0;
					i = 0;
				}
				if (i >= SteamRemoteStorage.FileCount)
				{
					return false;
				}
				filename = SteamRemoteStorage.Internal.GetFileNameAndSize(i, ref _);
				this.<>2__current = filename;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002FC RID: 764
			// (get) Token: 0x06001149 RID: 4425 RVA: 0x0001E6BC File Offset: 0x0001C8BC
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600114A RID: 4426 RVA: 0x0001E6C4 File Offset: 0x0001C8C4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002FD RID: 765
			// (get) Token: 0x0600114B RID: 4427 RVA: 0x0001E6CB File Offset: 0x0001C8CB
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600114C RID: 4428 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				SteamRemoteStorage.<get_Files>d__27 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SteamRemoteStorage.<get_Files>d__27(0);
				}
				return result;
			}

			// Token: 0x0600114D RID: 4429 RVA: 0x0001E70B File Offset: 0x0001C90B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x04000D51 RID: 3409
			private int <>1__state;

			// Token: 0x04000D52 RID: 3410
			private string <>2__current;

			// Token: 0x04000D53 RID: 3411
			private int <>l__initialThreadId;

			// Token: 0x04000D54 RID: 3412
			private int <_>5__1;

			// Token: 0x04000D55 RID: 3413
			private int <i>5__2;

			// Token: 0x04000D56 RID: 3414
			private string <filename>5__3;
		}
	}
}
