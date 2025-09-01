using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Mono.Net.Dns
{
	// Token: 0x020000C8 RID: 200
	internal sealed class SimpleResolver : IDisposable
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0000BC00 File Offset: 0x00009E00
		public SimpleResolver()
		{
			this.queries = new Dictionary<int, SimpleResolverEventArgs>();
			this.receive_cb = new AsyncCallback(this.OnReceive);
			this.timeout_cb = new TimerCallback(this.OnTimeout);
			this.InitFromSystem();
			this.InitSocket();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000BC4E File Offset: 0x00009E4E
		void IDisposable.Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.client != null)
				{
					this.client.Close();
					this.client = null;
				}
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000BC79 File Offset: 0x00009E79
		public void Close()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000BC84 File Offset: 0x00009E84
		private void GetLocalHost(SimpleResolverEventArgs args)
		{
			IPHostEntry iphostEntry = new IPHostEntry();
			iphostEntry.HostName = "localhost";
			iphostEntry.AddressList = new IPAddress[]
			{
				IPAddress.Loopback
			};
			iphostEntry.Aliases = SimpleResolver.EmptyStrings;
			args.ResolverError = ResolverError.NoError;
			args.HostEntry = iphostEntry;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public bool GetHostAddressesAsync(SimpleResolverEventArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (args.HostName == null)
			{
				throw new ArgumentNullException("args.HostName is null");
			}
			if (args.HostName.Length > 255)
			{
				throw new ArgumentException("args.HostName is too long");
			}
			args.Reset(ResolverAsyncOperation.GetHostAddresses);
			string hostName = args.HostName;
			if (hostName == "")
			{
				this.GetLocalHost(args);
				return false;
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				args.HostEntry = new IPHostEntry
				{
					HostName = hostName,
					Aliases = SimpleResolver.EmptyStrings,
					AddressList = new IPAddress[]
					{
						ipaddress
					}
				};
				return false;
			}
			this.SendAQuery(args, true);
			return true;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000BD84 File Offset: 0x00009F84
		public bool GetHostEntryAsync(SimpleResolverEventArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (args.HostName == null)
			{
				throw new ArgumentNullException("args.HostName is null");
			}
			if (args.HostName.Length > 255)
			{
				throw new ArgumentException("args.HostName is too long");
			}
			args.Reset(ResolverAsyncOperation.GetHostEntry);
			string hostName = args.HostName;
			if (hostName == "")
			{
				this.GetLocalHost(args);
				return false;
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				args.HostEntry = new IPHostEntry
				{
					HostName = hostName,
					Aliases = SimpleResolver.EmptyStrings,
					AddressList = new IPAddress[]
					{
						ipaddress
					}
				};
				args.PTRAddress = ipaddress;
				this.SendPTRQuery(args, true);
				return true;
			}
			this.SendAQuery(args, true);
			return true;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000BE48 File Offset: 0x0000A048
		private bool AddQuery(DnsQuery query, SimpleResolverEventArgs args)
		{
			Dictionary<int, SimpleResolverEventArgs> obj = this.queries;
			lock (obj)
			{
				if (this.queries.ContainsKey((int)query.Header.ID))
				{
					return false;
				}
				this.queries[(int)query.Header.ID] = args;
			}
			return true;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		private static DnsQuery GetQuery(string host, DnsQType q, DnsQClass c)
		{
			return new DnsQuery(host, q, c);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000BEC2 File Offset: 0x0000A0C2
		private void SendAQuery(SimpleResolverEventArgs args, bool add_it)
		{
			this.SendAQuery(args, args.HostName, add_it);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		private void SendAQuery(SimpleResolverEventArgs args, string host, bool add_it)
		{
			DnsQuery query = SimpleResolver.GetQuery(host, DnsQType.A, DnsQClass.Internet);
			this.SendQuery(args, query, add_it);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		private static string GetPTRName(IPAddress address)
		{
			byte[] addressBytes = address.GetAddressBytes();
			StringBuilder stringBuilder = new StringBuilder(28);
			for (int i = addressBytes.Length - 1; i >= 0; i--)
			{
				stringBuilder.AppendFormat("{0}.", addressBytes[i]);
			}
			stringBuilder.Append("in-addr.arpa");
			return stringBuilder.ToString();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000BF48 File Offset: 0x0000A148
		private void SendPTRQuery(SimpleResolverEventArgs args, bool add_it)
		{
			DnsQuery query = SimpleResolver.GetQuery(SimpleResolver.GetPTRName(args.PTRAddress), DnsQType.PTR, DnsQClass.Internet);
			this.SendQuery(args, query, add_it);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000BF74 File Offset: 0x0000A174
		private void SendQuery(SimpleResolverEventArgs args, DnsQuery query, bool add_it)
		{
			int num = 0;
			if (add_it)
			{
				for (;;)
				{
					query.Header.ID = (ushort)new Random().Next(1, 65534);
					if (num > 500)
					{
						break;
					}
					if (this.AddQuery(query, args))
					{
						goto Block_2;
					}
				}
				throw new InvalidOperationException("Too many pending queries (or really bad luck)");
				Block_2:
				args.QueryID = query.Header.ID;
			}
			else
			{
				query.Header.ID = args.QueryID;
			}
			if (args.Timer == null)
			{
				args.Timer = new Timer(this.timeout_cb, args, 5000, -1);
			}
			else
			{
				args.Timer.Change(5000, -1);
			}
			this.client.BeginSend(query.Packet, 0, query.Length, SocketFlags.None, null, null);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000C033 File Offset: 0x0000A233
		private byte[] GetFreshBuffer()
		{
			return new byte[512];
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00003917 File Offset: 0x00001B17
		private void FreeBuffer(byte[] buffer)
		{
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000C040 File Offset: 0x0000A240
		private void InitSocket()
		{
			this.client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			this.client.Blocking = true;
			this.client.Bind(new IPEndPoint(IPAddress.Any, 0));
			this.client.Connect(this.endpoints[0]);
			this.BeginReceive();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000C098 File Offset: 0x0000A298
		private void BeginReceive()
		{
			byte[] freshBuffer = this.GetFreshBuffer();
			this.client.BeginReceive(freshBuffer, 0, freshBuffer.Length, SocketFlags.None, this.receive_cb, freshBuffer);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		private void OnTimeout(object obj)
		{
			SimpleResolverEventArgs simpleResolverEventArgs = (SimpleResolverEventArgs)obj;
			Dictionary<int, SimpleResolverEventArgs> obj2 = this.queries;
			lock (obj2)
			{
				SimpleResolverEventArgs simpleResolverEventArgs2;
				if (this.queries.TryGetValue((int)simpleResolverEventArgs.QueryID, out simpleResolverEventArgs2))
				{
					if (simpleResolverEventArgs != simpleResolverEventArgs2)
					{
						throw new Exception("Should not happen: args != args2");
					}
					SimpleResolverEventArgs simpleResolverEventArgs3 = simpleResolverEventArgs;
					simpleResolverEventArgs3.Retries += 1;
					if (simpleResolverEventArgs.Retries > 1)
					{
						simpleResolverEventArgs.ResolverError = ResolverError.Timeout;
						simpleResolverEventArgs.OnCompleted(this);
					}
					else
					{
						this.SendAQuery(simpleResolverEventArgs, false);
					}
				}
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000C160 File Offset: 0x0000A360
		private void OnReceive(IAsyncResult ares)
		{
			if (this.disposed)
			{
				return;
			}
			int num = 0;
			EndPoint remoteEndPoint = this.client.RemoteEndPoint;
			try
			{
				num = this.client.EndReceive(ares);
			}
			catch (Exception value)
			{
				Console.Error.WriteLine(value);
			}
			this.BeginReceive();
			byte[] buffer = (byte[])ares.AsyncState;
			if (num > 12)
			{
				DnsResponse dnsResponse = new DnsResponse(buffer, num);
				int id = (int)dnsResponse.Header.ID;
				SimpleResolverEventArgs simpleResolverEventArgs = null;
				Dictionary<int, SimpleResolverEventArgs> obj = this.queries;
				lock (obj)
				{
					if (this.queries.TryGetValue(id, out simpleResolverEventArgs))
					{
						this.queries.Remove(id);
					}
				}
				if (simpleResolverEventArgs != null)
				{
					Timer timer = simpleResolverEventArgs.Timer;
					if (timer != null)
					{
						timer.Change(-1, -1);
					}
					try
					{
						this.ProcessResponse(simpleResolverEventArgs, dnsResponse, remoteEndPoint);
					}
					catch (Exception ex)
					{
						simpleResolverEventArgs.ResolverError = (ResolverError)(-1);
						simpleResolverEventArgs.ErrorMessage = ex.Message;
					}
					IPHostEntry hostEntry = simpleResolverEventArgs.HostEntry;
					if (simpleResolverEventArgs.ResolverError != ResolverError.NoError && simpleResolverEventArgs.PTRAddress != null && hostEntry != null && hostEntry.HostName != null)
					{
						simpleResolverEventArgs.PTRAddress = null;
						this.SendAQuery(simpleResolverEventArgs, hostEntry.HostName, true);
						simpleResolverEventArgs.Timer.Change(5000, -1);
					}
					else
					{
						simpleResolverEventArgs.OnCompleted(this);
					}
				}
			}
			this.FreeBuffer(buffer);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		private void ProcessResponse(SimpleResolverEventArgs args, DnsResponse response, EndPoint server_ep)
		{
			DnsRCode rcode = response.Header.RCode;
			if (rcode != DnsRCode.NoError)
			{
				if (args.PTRAddress != null)
				{
					return;
				}
				args.ResolverError = (ResolverError)rcode;
				return;
			}
			else
			{
				if (((IPEndPoint)server_ep).Port != 53)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "Port";
					return;
				}
				DnsHeader header = response.Header;
				if (!header.IsQuery)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "IsQuery";
					return;
				}
				if (header.QuestionCount > 1)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QuestionCount";
					return;
				}
				ReadOnlyCollection<DnsQuestion> questions = response.GetQuestions();
				if (questions.Count != 1)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QuestionCount 2";
					return;
				}
				DnsQuestion dnsQuestion = questions[0];
				DnsQType type = dnsQuestion.Type;
				if (type != DnsQType.A && type != DnsQType.AAAA && type != DnsQType.PTR)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QType " + dnsQuestion.Type.ToString();
					return;
				}
				if (dnsQuestion.Class != DnsQClass.Internet)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QClass " + dnsQuestion.Class.ToString();
					return;
				}
				ReadOnlyCollection<DnsResourceRecord> answers = response.GetAnswers();
				if (answers.Count != 0)
				{
					List<string> list = null;
					List<IPAddress> list2 = null;
					foreach (DnsResourceRecord dnsResourceRecord in answers)
					{
						if (dnsResourceRecord.Class == DnsClass.Internet)
						{
							if (dnsResourceRecord.Type == DnsType.A || dnsResourceRecord.Type == DnsType.AAAA)
							{
								if (list2 == null)
								{
									list2 = new List<IPAddress>();
								}
								list2.Add(((DnsResourceRecordIPAddress)dnsResourceRecord).Address);
							}
							else if (dnsResourceRecord.Type == DnsType.CNAME)
							{
								if (list == null)
								{
									list = new List<string>();
								}
								list.Add(((DnsResourceRecordCName)dnsResourceRecord).CName);
							}
							else if (dnsResourceRecord.Type == DnsType.PTR)
							{
								args.HostEntry.HostName = ((DnsResourceRecordPTR)dnsResourceRecord).DName;
								args.HostEntry.Aliases = ((list == null) ? SimpleResolver.EmptyStrings : list.ToArray());
								args.HostEntry.AddressList = SimpleResolver.EmptyAddresses;
								return;
							}
						}
					}
					IPHostEntry iphostEntry = args.HostEntry ?? new IPHostEntry();
					if (iphostEntry.HostName == null && list != null && list.Count > 0)
					{
						iphostEntry.HostName = list[0];
						list.RemoveAt(0);
					}
					iphostEntry.Aliases = ((list == null) ? SimpleResolver.EmptyStrings : list.ToArray());
					iphostEntry.AddressList = ((list2 == null) ? SimpleResolver.EmptyAddresses : list2.ToArray());
					args.HostEntry = iphostEntry;
					if ((dnsQuestion.Type == DnsQType.A || dnsQuestion.Type == DnsQType.AAAA) && iphostEntry.AddressList == SimpleResolver.EmptyAddresses)
					{
						args.ResolverError = ResolverError.NameError;
						args.ErrorMessage = "No addresses in response";
						return;
					}
					if (dnsQuestion.Type == DnsQType.PTR && iphostEntry.HostName == null)
					{
						args.ResolverError = ResolverError.NameError;
						args.ErrorMessage = "No PTR in response";
					}
					return;
				}
				if (args.PTRAddress != null)
				{
					return;
				}
				args.ResolverError = ResolverError.NameError;
				args.ErrorMessage = "NoAnswers";
				return;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000C624 File Offset: 0x0000A824
		private void InitFromSystem()
		{
			List<IPEndPoint> list = new List<IPEndPoint>();
			foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (NetworkInterfaceType.Loopback != networkInterface.NetworkInterfaceType)
				{
					foreach (IPAddress ipaddress in networkInterface.GetIPProperties().DnsAddresses)
					{
						if (AddressFamily.InterNetworkV6 != ipaddress.AddressFamily)
						{
							IPEndPoint item = new IPEndPoint(ipaddress, 53);
							if (!list.Contains(item))
							{
								list.Add(item);
							}
						}
					}
				}
			}
			this.endpoints = list.ToArray();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		// Note: this type is marked as 'beforefieldinit'.
		static SimpleResolver()
		{
		}

		// Token: 0x0400037D RID: 893
		private static string[] EmptyStrings = new string[0];

		// Token: 0x0400037E RID: 894
		private static IPAddress[] EmptyAddresses = new IPAddress[0];

		// Token: 0x0400037F RID: 895
		private IPEndPoint[] endpoints;

		// Token: 0x04000380 RID: 896
		private Socket client;

		// Token: 0x04000381 RID: 897
		private Dictionary<int, SimpleResolverEventArgs> queries;

		// Token: 0x04000382 RID: 898
		private AsyncCallback receive_cb;

		// Token: 0x04000383 RID: 899
		private TimerCallback timeout_cb;

		// Token: 0x04000384 RID: 900
		private bool disposed;
	}
}
