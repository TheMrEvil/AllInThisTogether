using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000287 RID: 647
	internal sealed class LocalDB
	{
		// Token: 0x06001E26 RID: 7718 RVA: 0x0008EF69 File Offset: 0x0008D169
		private LocalDB()
		{
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0008EF7C File Offset: 0x0008D17C
		internal static string GetLocalDBConnectionString(string localDbInstance)
		{
			if (!LocalDB.Instance.LoadUserInstanceDll())
			{
				return null;
			}
			return LocalDB.Instance.GetConnectionString(localDbInstance);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0008EF97 File Offset: 0x0008D197
		internal static IntPtr GetProcAddress(string functionName)
		{
			if (!LocalDB.Instance.LoadUserInstanceDll())
			{
				return IntPtr.Zero;
			}
			return Interop.Kernel32.GetProcAddress(LocalDB.Instance._sqlUserInstanceLibraryHandle, functionName);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0008EFC0 File Offset: 0x0008D1C0
		private string GetConnectionString(string localDbInstance)
		{
			StringBuilder stringBuilder = new StringBuilder(261);
			int capacity = stringBuilder.Capacity;
			this.localDBStartInstanceFunc(localDbInstance, 0, stringBuilder, ref capacity);
			return stringBuilder.ToString();
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0008EFF6 File Offset: 0x0008D1F6
		internal static uint MapLocalDBErrorStateToCode(LocalDB.LocalDBErrorState errorState)
		{
			switch (errorState)
			{
			case LocalDB.LocalDBErrorState.NO_INSTALLATION:
				return 52U;
			case LocalDB.LocalDBErrorState.INVALID_CONFIG:
				return 53U;
			case LocalDB.LocalDBErrorState.NO_SQLUSERINSTANCEDLL_PATH:
				return 54U;
			case LocalDB.LocalDBErrorState.INVALID_SQLUSERINSTANCEDLL_PATH:
				return 55U;
			case LocalDB.LocalDBErrorState.NONE:
				return 0U;
			default:
				return 53U;
			}
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0008F024 File Offset: 0x0008D224
		private bool LoadUserInstanceDll()
		{
			if (this._sqlUserInstanceLibraryHandle != null)
			{
				return true;
			}
			bool result;
			lock (this)
			{
				if (this._sqlUserInstanceLibraryHandle != null)
				{
					result = true;
				}
				else
				{
					LocalDB.LocalDBErrorState errorState;
					string userInstanceDllPath = this.GetUserInstanceDllPath(out errorState);
					if (userInstanceDllPath == null)
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 0U, LocalDB.MapLocalDBErrorStateToCode(errorState), string.Empty);
						result = false;
					}
					else if (string.IsNullOrWhiteSpace(userInstanceDllPath))
					{
						SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 0U, 55U, string.Empty);
						result = false;
					}
					else
					{
						SafeLibraryHandle safeLibraryHandle = Interop.Kernel32.LoadLibraryExW(userInstanceDllPath.Trim(), IntPtr.Zero, 0U);
						if (safeLibraryHandle.IsInvalid)
						{
							SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 0U, 56U, string.Empty);
							safeLibraryHandle.Dispose();
							result = false;
						}
						else
						{
							this._startInstanceHandle = Interop.Kernel32.GetProcAddress(safeLibraryHandle, "LocalDBStartInstance");
							if (this._startInstanceHandle == IntPtr.Zero)
							{
								SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 0U, 57U, string.Empty);
								safeLibraryHandle.Dispose();
								result = false;
							}
							else
							{
								this.localDBStartInstanceFunc = (LocalDB.LocalDBStartInstance)Marshal.GetDelegateForFunctionPointer(this._startInstanceHandle, typeof(LocalDB.LocalDBStartInstance));
								if (this.localDBStartInstanceFunc == null)
								{
									SNILoadHandle.SingletonInstance.LastError = new SNIError(SNIProviders.INVALID_PROV, 0U, 57U, string.Empty);
									safeLibraryHandle.Dispose();
									this._startInstanceHandle = IntPtr.Zero;
									result = false;
								}
								else
								{
									this._sqlUserInstanceLibraryHandle = safeLibraryHandle;
									result = true;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0008F1D4 File Offset: 0x0008D3D4
		private string GetUserInstanceDllPath(out LocalDB.LocalDBErrorState errorState)
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server Local DB\\Installed Versions\\"))
			{
				if (registryKey == null)
				{
					errorState = LocalDB.LocalDBErrorState.NO_INSTALLATION;
					result = null;
				}
				else
				{
					Version version = new Version();
					Version version2 = version;
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						Version version3;
						if (!Version.TryParse(subKeyNames[i], out version3))
						{
							errorState = LocalDB.LocalDBErrorState.INVALID_CONFIG;
							return null;
						}
						if (version2.CompareTo(version3) < 0)
						{
							version2 = version3;
						}
					}
					if (version2.Equals(version))
					{
						errorState = LocalDB.LocalDBErrorState.INVALID_CONFIG;
						result = null;
					}
					else
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(version2.ToString()))
						{
							object value = registryKey2.GetValue("InstanceAPIPath");
							if (value == null)
							{
								errorState = LocalDB.LocalDBErrorState.NO_SQLUSERINSTANCEDLL_PATH;
								result = null;
							}
							else if (registryKey2.GetValueKind("InstanceAPIPath") != RegistryValueKind.String)
							{
								errorState = LocalDB.LocalDBErrorState.INVALID_SQLUSERINSTANCEDLL_PATH;
								result = null;
							}
							else
							{
								string text = (string)value;
								errorState = LocalDB.LocalDBErrorState.NONE;
								result = text;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0008F2D4 File Offset: 0x0008D4D4
		// Note: this type is marked as 'beforefieldinit'.
		static LocalDB()
		{
		}

		// Token: 0x040014C0 RID: 5312
		private static readonly LocalDB Instance = new LocalDB();

		// Token: 0x040014C1 RID: 5313
		private const string LocalDBInstalledVersionRegistryKey = "SOFTWARE\\Microsoft\\Microsoft SQL Server Local DB\\Installed Versions\\";

		// Token: 0x040014C2 RID: 5314
		private const string InstanceAPIPathValueName = "InstanceAPIPath";

		// Token: 0x040014C3 RID: 5315
		private const string ProcLocalDBStartInstance = "LocalDBStartInstance";

		// Token: 0x040014C4 RID: 5316
		private const int MAX_LOCAL_DB_CONNECTION_STRING_SIZE = 260;

		// Token: 0x040014C5 RID: 5317
		private IntPtr _startInstanceHandle = IntPtr.Zero;

		// Token: 0x040014C6 RID: 5318
		private LocalDB.LocalDBStartInstance localDBStartInstanceFunc;

		// Token: 0x040014C7 RID: 5319
		private volatile SafeLibraryHandle _sqlUserInstanceLibraryHandle;

		// Token: 0x02000288 RID: 648
		// (Invoke) Token: 0x06001E2F RID: 7727
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int LocalDBStartInstance([MarshalAs(UnmanagedType.LPWStr)] [In] string localDBInstanceName, [In] int flags, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder sqlConnectionDataSource, [In] [Out] ref int bufferLength);

		// Token: 0x02000289 RID: 649
		internal enum LocalDBErrorState
		{
			// Token: 0x040014C9 RID: 5321
			NO_INSTALLATION,
			// Token: 0x040014CA RID: 5322
			INVALID_CONFIG,
			// Token: 0x040014CB RID: 5323
			NO_SQLUSERINSTANCEDLL_PATH,
			// Token: 0x040014CC RID: 5324
			INVALID_SQLUSERINSTANCEDLL_PATH,
			// Token: 0x040014CD RID: 5325
			NONE
		}
	}
}
