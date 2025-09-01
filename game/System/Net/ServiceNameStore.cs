using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x0200063B RID: 1595
	internal class ServiceNameStore
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000ADDB3 File Offset: 0x000ABFB3
		public ServiceNameCollection ServiceNames
		{
			get
			{
				if (this.serviceNameCollection == null)
				{
					this.serviceNameCollection = new ServiceNameCollection(this.serviceNames);
				}
				return this.serviceNameCollection;
			}
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000ADDD4 File Offset: 0x000ABFD4
		public ServiceNameStore()
		{
			this.serviceNames = new List<string>();
			this.serviceNameCollection = null;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000ADDEE File Offset: 0x000ABFEE
		private bool AddSingleServiceName(string spn)
		{
			spn = ServiceNameCollection.NormalizeServiceName(spn);
			if (this.Contains(spn))
			{
				return false;
			}
			this.serviceNames.Add(spn);
			return true;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000ADE10 File Offset: 0x000AC010
		public bool Add(string uriPrefix)
		{
			string[] array = this.BuildServiceNames(uriPrefix);
			bool flag = false;
			foreach (string spn in array)
			{
				if (this.AddSingleServiceName(spn))
				{
					flag = true;
					bool on = Logging.On;
				}
			}
			if (flag)
			{
				this.serviceNameCollection = null;
			}
			else
			{
				bool on2 = Logging.On;
			}
			return flag;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000ADE60 File Offset: 0x000AC060
		public bool Remove(string uriPrefix)
		{
			string text = this.BuildSimpleServiceName(uriPrefix);
			text = ServiceNameCollection.NormalizeServiceName(text);
			bool flag = this.Contains(text);
			if (flag)
			{
				this.serviceNames.Remove(text);
				this.serviceNameCollection = null;
			}
			if (Logging.On)
			{
			}
			return flag;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000ADEA5 File Offset: 0x000AC0A5
		private bool Contains(string newServiceName)
		{
			return newServiceName != null && ServiceNameCollection.Contains(newServiceName, this.serviceNames);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000ADEB8 File Offset: 0x000AC0B8
		public void Clear()
		{
			this.serviceNames.Clear();
			this.serviceNameCollection = null;
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000ADECC File Offset: 0x000AC0CC
		private string ExtractHostname(string uriPrefix, bool allowInvalidUriStrings)
		{
			if (Uri.IsWellFormedUriString(uriPrefix, UriKind.Absolute))
			{
				return new Uri(uriPrefix).Host;
			}
			if (allowInvalidUriStrings)
			{
				int num = uriPrefix.IndexOf("://") + 3;
				int num2 = num;
				bool flag = false;
				while (num2 < uriPrefix.Length && uriPrefix[num2] != '/' && (uriPrefix[num2] != ':' || flag))
				{
					if (uriPrefix[num2] == '[')
					{
						if (flag)
						{
							num2 = num;
							break;
						}
						flag = true;
					}
					if (flag && uriPrefix[num2] == ']')
					{
						flag = false;
					}
					num2++;
				}
				return uriPrefix.Substring(num, num2 - num);
			}
			return null;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000ADF60 File Offset: 0x000AC160
		public string BuildSimpleServiceName(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, false);
			if (text != null)
			{
				return "HTTP/" + text;
			}
			return null;
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000ADF88 File Offset: 0x000AC188
		public string[] BuildServiceNames(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, true);
			IPAddress ipaddress = null;
			if (string.Compare(text, "*", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(text, "+", StringComparison.InvariantCultureIgnoreCase) == 0 || IPAddress.TryParse(text, out ipaddress))
			{
				try
				{
					string hostName = Dns.GetHostEntry(string.Empty).HostName;
					return new string[]
					{
						"HTTP/" + hostName
					};
				}
				catch (SocketException)
				{
					return new string[0];
				}
				catch (SecurityException)
				{
					return new string[0];
				}
			}
			if (!text.Contains("."))
			{
				try
				{
					string hostName2 = Dns.GetHostEntry(text).HostName;
					return new string[]
					{
						"HTTP/" + text,
						"HTTP/" + hostName2
					};
				}
				catch (SocketException)
				{
					return new string[]
					{
						"HTTP/" + text
					};
				}
				catch (SecurityException)
				{
					return new string[]
					{
						"HTTP/" + text
					};
				}
			}
			return new string[]
			{
				"HTTP/" + text
			};
		}

		// Token: 0x04001D63 RID: 7523
		private List<string> serviceNames;

		// Token: 0x04001D64 RID: 7524
		private ServiceNameCollection serviceNameCollection;
	}
}
