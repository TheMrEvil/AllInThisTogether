using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000092 RID: 146
	public class SteamApps : SteamSharedClass<SteamApps>
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0000AF0F File Offset: 0x0000910F
		internal static ISteamApps Internal
		{
			get
			{
				return SteamSharedClass<SteamApps>.Interface as ISteamApps;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000AF1B File Offset: 0x0000911B
		internal override void InitializeInterface(bool server)
		{
			this.SetInterface(server, new ISteamApps(server));
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0000AF2C File Offset: 0x0000912C
		internal static void InstallEvents()
		{
			Dispatch.Install<DlcInstalled_t>(delegate(DlcInstalled_t x)
			{
				Action<AppId> onDlcInstalled = SteamApps.OnDlcInstalled;
				if (onDlcInstalled != null)
				{
					onDlcInstalled(x.AppID);
				}
			}, false);
			Dispatch.Install<NewUrlLaunchParameters_t>(delegate(NewUrlLaunchParameters_t x)
			{
				Action onNewLaunchParameters = SteamApps.OnNewLaunchParameters;
				if (onNewLaunchParameters != null)
				{
					onNewLaunchParameters();
				}
			}, false);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600073C RID: 1852 RVA: 0x0000AF88 File Offset: 0x00009188
		// (remove) Token: 0x0600073D RID: 1853 RVA: 0x0000AFBC File Offset: 0x000091BC
		public static event Action<AppId> OnDlcInstalled
		{
			[CompilerGenerated]
			add
			{
				Action<AppId> action = SteamApps.OnDlcInstalled;
				Action<AppId> action2;
				do
				{
					action2 = action;
					Action<AppId> value2 = (Action<AppId>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<AppId>>(ref SteamApps.OnDlcInstalled, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<AppId> action = SteamApps.OnDlcInstalled;
				Action<AppId> action2;
				do
				{
					action2 = action;
					Action<AppId> value2 = (Action<AppId>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<AppId>>(ref SteamApps.OnDlcInstalled, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600073E RID: 1854 RVA: 0x0000AFF0 File Offset: 0x000091F0
		// (remove) Token: 0x0600073F RID: 1855 RVA: 0x0000B024 File Offset: 0x00009224
		public static event Action OnNewLaunchParameters
		{
			[CompilerGenerated]
			add
			{
				Action action = SteamApps.OnNewLaunchParameters;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamApps.OnNewLaunchParameters, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = SteamApps.OnNewLaunchParameters;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref SteamApps.OnNewLaunchParameters, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0000B057 File Offset: 0x00009257
		public static bool IsSubscribed
		{
			get
			{
				return SteamApps.Internal.BIsSubscribed();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0000B063 File Offset: 0x00009263
		public static bool IsSubscribedFromFamilySharing
		{
			get
			{
				return SteamApps.Internal.BIsSubscribedFromFamilySharing();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0000B06F File Offset: 0x0000926F
		public static bool IsLowVoilence
		{
			get
			{
				return SteamApps.Internal.BIsLowViolence();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x0000B07B File Offset: 0x0000927B
		public static bool IsCybercafe
		{
			get
			{
				return SteamApps.Internal.BIsCybercafe();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0000B087 File Offset: 0x00009287
		public static bool IsVACBanned
		{
			get
			{
				return SteamApps.Internal.BIsVACBanned();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0000B093 File Offset: 0x00009293
		public static string GameLanguage
		{
			get
			{
				return SteamApps.Internal.GetCurrentGameLanguage();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0000B09F File Offset: 0x0000929F
		public static string[] AvailableLanguages
		{
			get
			{
				return SteamApps.Internal.GetAvailableGameLanguages().Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000B0BC File Offset: 0x000092BC
		public static bool IsSubscribedToApp(AppId appid)
		{
			return SteamApps.Internal.BIsSubscribedApp(appid.Value);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000B0D3 File Offset: 0x000092D3
		public static bool IsDlcInstalled(AppId appid)
		{
			return SteamApps.Internal.BIsDlcInstalled(appid.Value);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000B0EC File Offset: 0x000092EC
		public static DateTime PurchaseTime(AppId appid = default(AppId))
		{
			bool flag = appid == 0U;
			if (flag)
			{
				appid = SteamClient.AppId;
			}
			return Epoch.ToDateTime(SteamApps.Internal.GetEarliestPurchaseUnixTime(appid.Value));
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0000B131 File Offset: 0x00009331
		public static bool IsSubscribedFromFreeWeekend
		{
			get
			{
				return SteamApps.Internal.BIsSubscribedFromFreeWeekend();
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000B13D File Offset: 0x0000933D
		public static IEnumerable<DlcInformation> DlcInformation()
		{
			AppId appid = default(AppId);
			bool available = false;
			int num;
			for (int i = 0; i < SteamApps.Internal.GetDLCCount(); i = num + 1)
			{
				string strVal;
				bool flag = !SteamApps.Internal.BGetDLCDataByIndex(i, ref appid, ref available, out strVal);
				if (!flag)
				{
					yield return new DlcInformation
					{
						AppId = appid.Value,
						Name = strVal,
						Available = available
					};
					strVal = null;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000B146 File Offset: 0x00009346
		public static void InstallDlc(AppId appid)
		{
			SteamApps.Internal.InstallDLC(appid.Value);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000B15E File Offset: 0x0000935E
		public static void UninstallDlc(AppId appid)
		{
			SteamApps.Internal.UninstallDLC(appid.Value);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0000B178 File Offset: 0x00009378
		public static string CurrentBetaName
		{
			get
			{
				string text;
				bool flag = !SteamApps.Internal.GetCurrentBetaName(out text);
				string result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = text;
				}
				return result;
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000B1A2 File Offset: 0x000093A2
		public static void MarkContentCorrupt(bool missingFilesOnly)
		{
			SteamApps.Internal.MarkContentCorrupt(missingFilesOnly);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public static IEnumerable<DepotId> InstalledDepots(AppId appid = default(AppId))
		{
			bool flag = appid == 0U;
			if (flag)
			{
				appid = SteamClient.AppId;
			}
			DepotId_t[] depots = new DepotId_t[32];
			uint count = SteamApps.Internal.GetInstalledDepots(appid.Value, depots, (uint)depots.Length);
			int i = 0;
			while ((long)i < (long)((ulong)count))
			{
				yield return new DepotId
				{
					Value = depots[i].Value
				};
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000B1C0 File Offset: 0x000093C0
		public static string AppInstallDir(AppId appid = default(AppId))
		{
			bool flag = appid == 0U;
			if (flag)
			{
				appid = SteamClient.AppId;
			}
			string text;
			bool flag2 = SteamApps.Internal.GetAppInstallDir(appid.Value, out text) == 0U;
			string result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				result = text;
			}
			return result;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000B209 File Offset: 0x00009409
		public static bool IsAppInstalled(AppId appid)
		{
			return SteamApps.Internal.BIsAppInstalled(appid.Value);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0000B220 File Offset: 0x00009420
		public static SteamId AppOwner
		{
			get
			{
				return SteamApps.Internal.GetAppOwner().Value;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000B236 File Offset: 0x00009436
		public static string GetLaunchParam(string param)
		{
			return SteamApps.Internal.GetLaunchQueryParam(param);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000B244 File Offset: 0x00009444
		public static DownloadProgress DlcDownloadProgress(AppId appid)
		{
			ulong bytesDownloaded = 0UL;
			ulong bytesTotal = 0UL;
			bool flag = !SteamApps.Internal.GetDlcDownloadProgress(appid.Value, ref bytesDownloaded, ref bytesTotal);
			DownloadProgress result;
			if (flag)
			{
				result = default(DownloadProgress);
			}
			else
			{
				result = new DownloadProgress
				{
					BytesDownloaded = bytesDownloaded,
					BytesTotal = bytesTotal,
					Active = true
				};
			}
			return result;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0000B2AC File Offset: 0x000094AC
		public static int BuildId
		{
			get
			{
				return SteamApps.Internal.GetAppBuildId();
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000B2B8 File Offset: 0x000094B8
		public static async Task<FileDetails?> GetFileDetailsAsync(string filename)
		{
			FileDetailsResult_t? fileDetailsResult_t = await SteamApps.Internal.GetFileDetails(filename);
			FileDetailsResult_t? r = fileDetailsResult_t;
			fileDetailsResult_t = null;
			FileDetails? result;
			if (r == null || r.Value.Result != Result.OK)
			{
				result = null;
			}
			else
			{
				FileDetails value = default(FileDetails);
				value.SizeInBytes = r.Value.FileSize;
				value.Flags = r.Value.Flags;
				value.Sha1 = string.Join("", from x in r.Value.FileSHA
				select x.ToString("x"));
				result = new FileDetails?(value);
			}
			return result;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0000B300 File Offset: 0x00009500
		public static string CommandLine
		{
			get
			{
				string result;
				SteamApps.Internal.GetLaunchCommandLine(out result);
				return result;
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0000B320 File Offset: 0x00009520
		public SteamApps()
		{
		}

		// Token: 0x040006DD RID: 1757
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<AppId> OnDlcInstalled;

		// Token: 0x040006DE RID: 1758
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnNewLaunchParameters;

		// Token: 0x02000212 RID: 530
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001083 RID: 4227 RVA: 0x0001B59B File Offset: 0x0001979B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001084 RID: 4228 RVA: 0x0001B5A7 File Offset: 0x000197A7
			public <>c()
			{
			}

			// Token: 0x06001085 RID: 4229 RVA: 0x0001B5B0 File Offset: 0x000197B0
			internal void <InstallEvents>b__3_0(DlcInstalled_t x)
			{
				Action<AppId> onDlcInstalled = SteamApps.OnDlcInstalled;
				if (onDlcInstalled != null)
				{
					onDlcInstalled(x.AppID);
				}
			}

			// Token: 0x06001086 RID: 4230 RVA: 0x0001B5C9 File Offset: 0x000197C9
			internal void <InstallEvents>b__3_1(NewUrlLaunchParameters_t x)
			{
				Action onNewLaunchParameters = SteamApps.OnNewLaunchParameters;
				if (onNewLaunchParameters != null)
				{
					onNewLaunchParameters();
				}
			}

			// Token: 0x06001087 RID: 4231 RVA: 0x0001B5DC File Offset: 0x000197DC
			internal string <GetFileDetailsAsync>b__44_0(byte x)
			{
				return x.ToString("x");
			}

			// Token: 0x04000C53 RID: 3155
			public static readonly SteamApps.<>c <>9 = new SteamApps.<>c();

			// Token: 0x04000C54 RID: 3156
			public static Action<DlcInstalled_t> <>9__3_0;

			// Token: 0x04000C55 RID: 3157
			public static Action<NewUrlLaunchParameters_t> <>9__3_1;

			// Token: 0x04000C56 RID: 3158
			public static Func<byte, string> <>9__44_0;
		}

		// Token: 0x02000213 RID: 531
		[CompilerGenerated]
		private sealed class <DlcInformation>d__29 : IEnumerable<DlcInformation>, IEnumerable, IEnumerator<DlcInformation>, IDisposable, IEnumerator
		{
			// Token: 0x06001088 RID: 4232 RVA: 0x0001B5EA File Offset: 0x000197EA
			[DebuggerHidden]
			public <DlcInformation>d__29(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06001089 RID: 4233 RVA: 0x0001B605 File Offset: 0x00019805
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600108A RID: 4234 RVA: 0x0001B608 File Offset: 0x00019808
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					appid = default(AppId);
					available = false;
					i = 0;
					goto IL_D9;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				strVal = null;
				IL_C9:
				int num2 = i;
				i = num2 + 1;
				IL_D9:
				if (i >= SteamApps.Internal.GetDLCCount())
				{
					return false;
				}
				bool flag = !SteamApps.Internal.BGetDLCDataByIndex(i, ref appid, ref available, out strVal);
				if (flag)
				{
					goto IL_C9;
				}
				this.<>2__current = new DlcInformation
				{
					AppId = appid.Value,
					Name = strVal,
					Available = available
				};
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x0600108B RID: 4235 RVA: 0x0001B70A File Offset: 0x0001990A
			DlcInformation IEnumerator<DlcInformation>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600108C RID: 4236 RVA: 0x0001B712 File Offset: 0x00019912
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x0600108D RID: 4237 RVA: 0x0001B719 File Offset: 0x00019919
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600108E RID: 4238 RVA: 0x0001B728 File Offset: 0x00019928
			[DebuggerHidden]
			IEnumerator<DlcInformation> IEnumerable<DlcInformation>.GetEnumerator()
			{
				SteamApps.<DlcInformation>d__29 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new SteamApps.<DlcInformation>d__29(0);
				}
				return result;
			}

			// Token: 0x0600108F RID: 4239 RVA: 0x0001B75F File Offset: 0x0001995F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Data.DlcInformation>.GetEnumerator();
			}

			// Token: 0x04000C57 RID: 3159
			private int <>1__state;

			// Token: 0x04000C58 RID: 3160
			private DlcInformation <>2__current;

			// Token: 0x04000C59 RID: 3161
			private int <>l__initialThreadId;

			// Token: 0x04000C5A RID: 3162
			private AppId <appid>5__1;

			// Token: 0x04000C5B RID: 3163
			private bool <available>5__2;

			// Token: 0x04000C5C RID: 3164
			private int <i>5__3;

			// Token: 0x04000C5D RID: 3165
			private string <strVal>5__4;
		}

		// Token: 0x02000214 RID: 532
		[CompilerGenerated]
		private sealed class <InstalledDepots>d__35 : IEnumerable<DepotId>, IEnumerable, IEnumerator<DepotId>, IDisposable, IEnumerator
		{
			// Token: 0x06001090 RID: 4240 RVA: 0x0001B767 File Offset: 0x00019967
			[DebuggerHidden]
			public <InstalledDepots>d__35(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06001091 RID: 4241 RVA: 0x0001B782 File Offset: 0x00019982
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001092 RID: 4242 RVA: 0x0001B784 File Offset: 0x00019984
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
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					bool flag = appid == 0U;
					if (flag)
					{
						appid = SteamClient.AppId;
					}
					depots = new DepotId_t[32];
					count = SteamApps.Internal.GetInstalledDepots(appid.Value, depots, (uint)depots.Length);
					i = 0;
				}
				if ((long)i >= (long)((ulong)count))
				{
					return false;
				}
				this.<>2__current = new DepotId
				{
					Value = depots[i].Value
				};
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06001093 RID: 4243 RVA: 0x0001B87A File Offset: 0x00019A7A
			DepotId IEnumerator<DepotId>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001094 RID: 4244 RVA: 0x0001B882 File Offset: 0x00019A82
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06001095 RID: 4245 RVA: 0x0001B889 File Offset: 0x00019A89
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001096 RID: 4246 RVA: 0x0001B898 File Offset: 0x00019A98
			[DebuggerHidden]
			IEnumerator<DepotId> IEnumerable<DepotId>.GetEnumerator()
			{
				SteamApps.<InstalledDepots>d__35 <InstalledDepots>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<InstalledDepots>d__ = this;
				}
				else
				{
					<InstalledDepots>d__ = new SteamApps.<InstalledDepots>d__35(0);
				}
				<InstalledDepots>d__.appid = appid;
				return <InstalledDepots>d__;
			}

			// Token: 0x06001097 RID: 4247 RVA: 0x0001B8DB File Offset: 0x00019ADB
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Steamworks.Data.DepotId>.GetEnumerator();
			}

			// Token: 0x04000C5E RID: 3166
			private int <>1__state;

			// Token: 0x04000C5F RID: 3167
			private DepotId <>2__current;

			// Token: 0x04000C60 RID: 3168
			private int <>l__initialThreadId;

			// Token: 0x04000C61 RID: 3169
			private AppId appid;

			// Token: 0x04000C62 RID: 3170
			public AppId <>3__appid;

			// Token: 0x04000C63 RID: 3171
			private DepotId_t[] <depots>5__1;

			// Token: 0x04000C64 RID: 3172
			private uint <count>5__2;

			// Token: 0x04000C65 RID: 3173
			private int <i>5__3;
		}

		// Token: 0x02000215 RID: 533
		[CompilerGenerated]
		private sealed class <GetFileDetailsAsync>d__44 : IAsyncStateMachine
		{
			// Token: 0x06001098 RID: 4248 RVA: 0x0001B8E3 File Offset: 0x00019AE3
			public <GetFileDetailsAsync>d__44()
			{
			}

			// Token: 0x06001099 RID: 4249 RVA: 0x0001B8EC File Offset: 0x00019AEC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				FileDetails? result;
				try
				{
					CallResult<FileDetailsResult_t> callResult;
					if (num != 0)
					{
						callResult = SteamApps.Internal.GetFileDetails(filename).GetAwaiter();
						if (!callResult.IsCompleted)
						{
							num2 = 0;
							CallResult<FileDetailsResult_t> callResult2 = callResult;
							SteamApps.<GetFileDetailsAsync>d__44 <GetFileDetailsAsync>d__ = this;
							this.<>t__builder.AwaitOnCompleted<CallResult<FileDetailsResult_t>, SteamApps.<GetFileDetailsAsync>d__44>(ref callResult, ref <GetFileDetailsAsync>d__);
							return;
						}
					}
					else
					{
						CallResult<FileDetailsResult_t> callResult2;
						callResult = callResult2;
						callResult2 = default(CallResult<FileDetailsResult_t>);
						num2 = -1;
					}
					fileDetailsResult_t = callResult.GetResult();
					r = fileDetailsResult_t;
					fileDetailsResult_t = null;
					bool flag = r == null || r.Value.Result != Result.OK;
					if (flag)
					{
						result = null;
					}
					else
					{
						FileDetails value = default(FileDetails);
						value.SizeInBytes = r.Value.FileSize;
						value.Flags = r.Value.Flags;
						value.Sha1 = string.Join("", r.Value.FileSHA.Select(new Func<byte, string>(SteamApps.<>c.<>9.<GetFileDetailsAsync>b__44_0)));
						result = new FileDetails?(value);
					}
				}
				catch (Exception exception)
				{
					num2 = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600109A RID: 4250 RVA: 0x0001BA9C File Offset: 0x00019C9C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			// Token: 0x04000C66 RID: 3174
			public int <>1__state;

			// Token: 0x04000C67 RID: 3175
			public AsyncTaskMethodBuilder<FileDetails?> <>t__builder;

			// Token: 0x04000C68 RID: 3176
			public string filename;

			// Token: 0x04000C69 RID: 3177
			private FileDetailsResult_t? <r>5__1;

			// Token: 0x04000C6A RID: 3178
			private FileDetailsResult_t? <>s__2;

			// Token: 0x04000C6B RID: 3179
			private CallResult<FileDetailsResult_t> <>u__1;
		}
	}
}
