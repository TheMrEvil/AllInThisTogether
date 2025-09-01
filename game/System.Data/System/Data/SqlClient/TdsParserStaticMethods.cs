using System;
using System.Data.Common;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x0200027C RID: 636
	internal sealed class TdsParserStaticMethods
	{
		// Token: 0x06001DDB RID: 7643 RVA: 0x0008DAB4 File Offset: 0x0008BCB4
		internal static byte[] ObfuscatePassword(string password)
		{
			byte[] array = new byte[password.Length << 1];
			for (int i = 0; i < password.Length; i++)
			{
				char c = password[i];
				byte b = (byte)(c & 'ÿ');
				byte b2 = (byte)(c >> 8 & 'ÿ');
				array[i << 1] = (byte)(((int)(b & 15) << 4 | b >> 4) ^ 165);
				array[(i << 1) + 1] = (byte)(((int)(b2 & 15) << 4 | b2 >> 4) ^ 165);
			}
			return array;
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x0008DB2C File Offset: 0x0008BD2C
		internal static byte[] ObfuscatePassword(byte[] password)
		{
			for (int i = 0; i < password.Length; i++)
			{
				byte b = password[i] & 15;
				byte b2 = password[i] & 240;
				password[i] = (byte)((b2 >> 4 | (int)b << 4) ^ 165);
			}
			return password;
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x0008DB6C File Offset: 0x0008BD6C
		internal static int GetCurrentProcessIdForTdsLoginOnly()
		{
			if (TdsParserStaticMethods.s_currentProcessId == -1)
			{
				int value = new Random().Next();
				Interlocked.CompareExchange(ref TdsParserStaticMethods.s_currentProcessId, value, -1);
			}
			return TdsParserStaticMethods.s_currentProcessId;
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x0008DB9E File Offset: 0x0008BD9E
		internal static int GetCurrentThreadIdForTdsLoginOnly()
		{
			return Environment.CurrentManagedThreadId;
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0008DBA8 File Offset: 0x0008BDA8
		internal static byte[] GetNetworkPhysicalAddressForTdsLoginOnly()
		{
			if (TdsParserStaticMethods.s_nicAddress == null)
			{
				byte[] array = new byte[6];
				new Random().NextBytes(array);
				Interlocked.CompareExchange<byte[]>(ref TdsParserStaticMethods.s_nicAddress, array, null);
			}
			return TdsParserStaticMethods.s_nicAddress;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0008DBE0 File Offset: 0x0008BDE0
		internal static int GetTimeoutMilliseconds(long timeoutTime)
		{
			if (9223372036854775807L == timeoutTime)
			{
				return -1;
			}
			long num = ADP.TimerRemainingMilliseconds(timeoutTime);
			if (num < 0L)
			{
				return 0;
			}
			if (num > 2147483647L)
			{
				return int.MaxValue;
			}
			return (int)num;
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0008DC1C File Offset: 0x0008BE1C
		internal static long GetTimeout(long timeoutMilliseconds)
		{
			long result;
			if (timeoutMilliseconds <= 0L)
			{
				result = long.MaxValue;
			}
			else
			{
				try
				{
					result = checked(ADP.TimerCurrent() + ADP.TimerFromMilliseconds(timeoutMilliseconds));
				}
				catch (OverflowException)
				{
					result = long.MaxValue;
				}
			}
			return result;
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0008DC68 File Offset: 0x0008BE68
		internal static bool TimeoutHasExpired(long timeoutTime)
		{
			bool result = false;
			if (timeoutTime != 0L && 9223372036854775807L != timeoutTime)
			{
				result = ADP.TimerHasExpired(timeoutTime);
			}
			return result;
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0008DC8E File Offset: 0x0008BE8E
		internal static int NullAwareStringLength(string str)
		{
			if (str == null)
			{
				return 0;
			}
			return str.Length;
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0008DC9C File Offset: 0x0008BE9C
		internal static int GetRemainingTimeout(int timeout, long start)
		{
			if (timeout <= 0)
			{
				return timeout;
			}
			long num = ADP.TimerRemainingSeconds(start + ADP.TimerFromSeconds(timeout));
			if (num <= 0L)
			{
				return 1;
			}
			return checked((int)num);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00003D93 File Offset: 0x00001F93
		public TdsParserStaticMethods()
		{
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0008DCC6 File Offset: 0x0008BEC6
		// Note: this type is marked as 'beforefieldinit'.
		static TdsParserStaticMethods()
		{
		}

		// Token: 0x040014A0 RID: 5280
		private const int NoProcessId = -1;

		// Token: 0x040014A1 RID: 5281
		private static int s_currentProcessId = -1;

		// Token: 0x040014A2 RID: 5282
		private static byte[] s_nicAddress = null;
	}
}
