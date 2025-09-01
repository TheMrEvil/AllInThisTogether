using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Net
{
	// Token: 0x020005DD RID: 1501
	internal static class NclUtilities
	{
		// Token: 0x0600303B RID: 12347 RVA: 0x000A6498 File Offset: 0x000A4698
		internal static bool IsThreadPoolLow()
		{
			int num;
			int num2;
			ThreadPool.GetAvailableThreads(out num, out num2);
			return num < 2;
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600303C RID: 12348 RVA: 0x000A64B2 File Offset: 0x000A46B2
		internal static bool HasShutdownStarted
		{
			get
			{
				return Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload();
			}
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000A64C8 File Offset: 0x000A46C8
		internal static bool IsCredentialFailure(SecurityStatus error)
		{
			return error == SecurityStatus.LogonDenied || error == SecurityStatus.UnknownCredentials || error == SecurityStatus.NoImpersonation || error == SecurityStatus.NoAuthenticatingAuthority || error == SecurityStatus.UntrustedRoot || error == SecurityStatus.CertExpired || error == SecurityStatus.SmartcardLogonRequired || error == SecurityStatus.BadBinding;
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000A6518 File Offset: 0x000A4718
		internal static bool IsClientFault(SecurityStatus error)
		{
			return error == SecurityStatus.InvalidToken || error == SecurityStatus.CannotPack || error == SecurityStatus.QopNotSupported || error == SecurityStatus.NoCredentials || error == SecurityStatus.MessageAltered || error == SecurityStatus.OutOfSequence || error == SecurityStatus.IncompleteMessage || error == SecurityStatus.IncompleteCredentials || error == SecurityStatus.WrongPrincipal || error == SecurityStatus.TimeSkew || error == SecurityStatus.IllegalMessage || error == SecurityStatus.CertUnknown || error == SecurityStatus.AlgorithmMismatch || error == SecurityStatus.SecurityQosFailed || error == SecurityStatus.UnsupportedPreauth;
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600303F RID: 12351 RVA: 0x000A659F File Offset: 0x000A479F
		internal static ContextCallback ContextRelativeDemandCallback
		{
			get
			{
				if (NclUtilities.s_ContextRelativeDemandCallback == null)
				{
					NclUtilities.s_ContextRelativeDemandCallback = new ContextCallback(NclUtilities.DemandCallback);
				}
				return NclUtilities.s_ContextRelativeDemandCallback;
			}
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x00003917 File Offset: 0x00001B17
		private static void DemandCallback(object state)
		{
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000A65C4 File Offset: 0x000A47C4
		internal static bool GuessWhetherHostIsLoopback(string host)
		{
			string a = host.ToLowerInvariant();
			return a == "localhost" || a == "loopback";
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000A65F5 File Offset: 0x000A47F5
		internal static bool IsFatal(Exception exception)
		{
			return exception != null && (exception is OutOfMemoryException || exception is StackOverflowException || exception is ThreadAbortException);
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000A6618 File Offset: 0x000A4818
		internal static bool IsAddressLocal(IPAddress ipAddress)
		{
			IPAddress[] localAddresses = NclUtilities.LocalAddresses;
			for (int i = 0; i < localAddresses.Length; i++)
			{
				if (ipAddress.Equals(localAddresses[i], false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000A6648 File Offset: 0x000A4848
		private static IPHostEntry GetLocalHost()
		{
			return Dns.GetHostByName(Dns.GetHostName());
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x000A6654 File Offset: 0x000A4854
		internal static IPAddress[] LocalAddresses
		{
			get
			{
				IPAddress[] array = NclUtilities._LocalAddresses;
				if (array != null)
				{
					return array;
				}
				object localAddressesLock = NclUtilities.LocalAddressesLock;
				IPAddress[] result;
				lock (localAddressesLock)
				{
					array = NclUtilities._LocalAddresses;
					if (array != null)
					{
						result = array;
					}
					else
					{
						List<IPAddress> list = new List<IPAddress>();
						try
						{
							IPHostEntry localHost = NclUtilities.GetLocalHost();
							if (localHost != null)
							{
								if (localHost.HostName != null)
								{
									int num = localHost.HostName.IndexOf('.');
									if (num != -1)
									{
										NclUtilities._LocalDomainName = localHost.HostName.Substring(num);
									}
								}
								IPAddress[] addressList = localHost.AddressList;
								if (addressList != null)
								{
									foreach (IPAddress item in addressList)
									{
										list.Add(item);
									}
								}
							}
						}
						catch
						{
						}
						array = new IPAddress[list.Count];
						int num2 = 0;
						foreach (IPAddress ipaddress in list)
						{
							array[num2] = ipaddress;
							num2++;
						}
						NclUtilities._LocalAddresses = array;
						result = array;
					}
				}
				return result;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x000A6794 File Offset: 0x000A4994
		private static object LocalAddressesLock
		{
			get
			{
				if (NclUtilities._LocalAddressesLock == null)
				{
					Interlocked.CompareExchange(ref NclUtilities._LocalAddressesLock, new object(), null);
				}
				return NclUtilities._LocalAddressesLock;
			}
		}

		// Token: 0x04001AFD RID: 6909
		private static volatile ContextCallback s_ContextRelativeDemandCallback;

		// Token: 0x04001AFE RID: 6910
		private static volatile IPAddress[] _LocalAddresses;

		// Token: 0x04001AFF RID: 6911
		private static object _LocalAddressesLock;

		// Token: 0x04001B00 RID: 6912
		private const int HostNameBufferLength = 256;

		// Token: 0x04001B01 RID: 6913
		internal static string _LocalDomainName;
	}
}
