using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data
{
	// Token: 0x02000153 RID: 339
	internal static class LocalDBAPI
	{
		// Token: 0x0600120C RID: 4620 RVA: 0x00055359 File Offset: 0x00053559
		internal static void ReleaseDLLHandles()
		{
			LocalDBAPI.s_userInstanceDLLHandle = IntPtr.Zero;
			LocalDBAPI.s_localDBFormatMessage = null;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0005536C File Offset: 0x0005356C
		private static LocalDBAPI.LocalDBFormatMessageDelegate LocalDBFormatMessage
		{
			get
			{
				if (LocalDBAPI.s_localDBFormatMessage == null)
				{
					object obj = LocalDBAPI.s_dllLock;
					lock (obj)
					{
						if (LocalDBAPI.s_localDBFormatMessage == null)
						{
							IntPtr intPtr = LocalDBAPI.LoadProcAddress();
							if (intPtr == IntPtr.Zero)
							{
								Marshal.GetLastWin32Error();
								throw LocalDBAPI.CreateLocalDBException("Invalid SQLUserInstance.dll found at the location specified in the registry. Verify that the Local Database Runtime feature of SQL Server Express is properly installed.", null, 0, 0);
							}
							LocalDBAPI.s_localDBFormatMessage = Marshal.GetDelegateForFunctionPointer<LocalDBAPI.LocalDBFormatMessageDelegate>(intPtr);
						}
					}
				}
				return LocalDBAPI.s_localDBFormatMessage;
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000553E8 File Offset: 0x000535E8
		internal static string GetLocalDBMessage(int hrCode)
		{
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				uint capacity = (uint)stringBuilder.Capacity;
				int num = LocalDBAPI.LocalDBFormatMessage(hrCode, 1U, (uint)CultureInfo.CurrentCulture.LCID, stringBuilder, ref capacity);
				if (num >= 0)
				{
					result = stringBuilder.ToString();
				}
				else
				{
					stringBuilder = new StringBuilder(1024);
					capacity = (uint)stringBuilder.Capacity;
					num = LocalDBAPI.LocalDBFormatMessage(hrCode, 1U, 0U, stringBuilder, ref capacity);
					if (num >= 0)
					{
						result = stringBuilder.ToString();
					}
					else
					{
						result = string.Format(CultureInfo.CurrentCulture, "{0} (0x{1:X}).", "Cannot obtain Local Database Runtime error message", num);
					}
				}
			}
			catch (SqlException ex)
			{
				result = string.Format(CultureInfo.CurrentCulture, "{0} ({1}).", "Cannot obtain Local Database Runtime error message", ex.Message);
			}
			return result;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x000554AC File Offset: 0x000536AC
		private static SqlException CreateLocalDBException(string errorMessage, string instance = null, int localDbError = 0, int sniError = 0)
		{
			SqlErrorCollection sqlErrorCollection = new SqlErrorCollection();
			int infoNumber = (localDbError == 0) ? sniError : localDbError;
			if (sniError != 0)
			{
				string snierrorMessage = SQL.GetSNIErrorMessage(sniError);
				errorMessage = string.Format(null, "{0} (error: {1} - {2})", errorMessage, sniError, snierrorMessage);
			}
			sqlErrorCollection.Add(new SqlError(infoNumber, 0, 20, instance, errorMessage, null, 0, null));
			if (localDbError != 0)
			{
				sqlErrorCollection.Add(new SqlError(infoNumber, 0, 20, instance, LocalDBAPI.GetLocalDBMessage(localDbError), null, 0, null));
			}
			SqlException ex = SqlException.CreateException(sqlErrorCollection, null);
			ex._doNotReconnect = true;
			return ex;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00055524 File Offset: 0x00053724
		private static IntPtr LoadProcAddress()
		{
			return SafeNativeMethods.GetProcAddress(LocalDBAPI.UserInstanceDLLHandle, "LocalDBFormatMessage");
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00055538 File Offset: 0x00053738
		private static IntPtr UserInstanceDLLHandle
		{
			get
			{
				if (LocalDBAPI.s_userInstanceDLLHandle == IntPtr.Zero)
				{
					object obj = LocalDBAPI.s_dllLock;
					lock (obj)
					{
						if (LocalDBAPI.s_userInstanceDLLHandle == IntPtr.Zero)
						{
							SNINativeMethodWrapper.SNIQueryInfo(SNINativeMethodWrapper.QTypes.SNI_QUERY_LOCALDB_HMODULE, ref LocalDBAPI.s_userInstanceDLLHandle);
							if (LocalDBAPI.s_userInstanceDLLHandle == IntPtr.Zero)
							{
								SNINativeMethodWrapper.SNI_Error sni_Error;
								SNINativeMethodWrapper.SNIGetLastError(out sni_Error);
								throw LocalDBAPI.CreateLocalDBException(SR.GetString("LocalDB_FailedGetDLLHandle"), null, 0, (int)sni_Error.sniError);
							}
						}
					}
				}
				return LocalDBAPI.s_userInstanceDLLHandle;
			}
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x000555D4 File Offset: 0x000537D4
		internal static string GetLocalDbInstanceNameFromServerName(string serverName)
		{
			if (serverName == null)
			{
				return null;
			}
			serverName = serverName.TrimStart();
			if (!serverName.StartsWith("(localdb)\\", StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			string text = serverName.Substring("(localdb)\\".Length).Trim();
			if (text.Length == 0)
			{
				return null;
			}
			return text;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0005561F File Offset: 0x0005381F
		// Note: this type is marked as 'beforefieldinit'.
		static LocalDBAPI()
		{
		}

		// Token: 0x04000B93 RID: 2963
		private static LocalDBAPI.LocalDBFormatMessageDelegate s_localDBFormatMessage = null;

		// Token: 0x04000B94 RID: 2964
		private static IntPtr s_userInstanceDLLHandle = IntPtr.Zero;

		// Token: 0x04000B95 RID: 2965
		private static readonly object s_dllLock = new object();

		// Token: 0x04000B96 RID: 2966
		private const uint const_LOCALDB_TRUNCATE_ERR_MESSAGE = 1U;

		// Token: 0x04000B97 RID: 2967
		private const int const_ErrorMessageBufferSize = 1024;

		// Token: 0x04000B98 RID: 2968
		private const string const_localDbPrefix = "(localdb)\\";

		// Token: 0x02000154 RID: 340
		// (Invoke) Token: 0x06001215 RID: 4629
		[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private delegate int LocalDBFormatMessageDelegate(int hrLocalDB, uint dwFlags, uint dwLanguageId, StringBuilder buffer, ref uint buflen);
	}
}
