using System;
using System.Net.Sockets;
using Unity;

namespace System.Net
{
	/// <summary>Defines an endpoint that is authorized by a <see cref="T:System.Net.SocketPermission" /> instance.</summary>
	// Token: 0x02000682 RID: 1666
	[Serializable]
	public class EndpointPermission
	{
		// Token: 0x0600347C RID: 13436 RVA: 0x000B72FD File Offset: 0x000B54FD
		internal EndpointPermission(string hostname, int port, TransportType transport)
		{
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			this.hostname = hostname;
			this.port = port;
			this.transport = transport;
			this.resolved = false;
			this.hasWildcard = false;
			this.addresses = null;
		}

		/// <summary>Gets the DNS host name or IP address of the server that is associated with this endpoint.</summary>
		/// <returns>A string that contains the DNS host name or IP address of the server.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000B733D File Offset: 0x000B553D
		public string Hostname
		{
			get
			{
				return this.hostname;
			}
		}

		/// <summary>Gets the network port number that is associated with this endpoint.</summary>
		/// <returns>The network port number that is associated with this request, or <see cref="F:System.Net.SocketPermission.AllPorts" />.</returns>
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000B7345 File Offset: 0x000B5545
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		/// <summary>Gets the transport type that is associated with this endpoint.</summary>
		/// <returns>One of the <see cref="T:System.Net.TransportType" /> values.</returns>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000B734D File Offset: 0x000B554D
		public TransportType Transport
		{
			get
			{
				return this.transport;
			}
		}

		/// <summary>Determines whether the specified <see langword="Object" /> is equal to the current <see langword="Object" />.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see langword="Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see langword="Object" /> is equal to the current <see langword="Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003480 RID: 13440 RVA: 0x000B7358 File Offset: 0x000B5558
		public override bool Equals(object obj)
		{
			EndpointPermission endpointPermission = obj as EndpointPermission;
			return endpointPermission != null && this.port == endpointPermission.port && this.transport == endpointPermission.transport && string.Compare(this.hostname, endpointPermission.hostname, true) == 0;
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06003481 RID: 13441 RVA: 0x000B73A2 File Offset: 0x000B55A2
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.EndpointPermission" /> instance.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Net.EndpointPermission" /> instance.</returns>
		// Token: 0x06003482 RID: 13442 RVA: 0x000B73B0 File Offset: 0x000B55B0
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = this.hostname;
			array[1] = "#";
			array[2] = this.port.ToString();
			array[3] = "#";
			int num = 4;
			int num2 = (int)this.transport;
			array[num] = num2.ToString();
			return string.Concat(array);
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000B7400 File Offset: 0x000B5600
		internal bool IsSubsetOf(EndpointPermission perm)
		{
			if (perm == null)
			{
				return false;
			}
			if (perm.port != -1 && this.port != perm.port)
			{
				return false;
			}
			if (perm.transport != TransportType.All && this.transport != perm.transport)
			{
				return false;
			}
			this.Resolve();
			perm.Resolve();
			if (this.hasWildcard)
			{
				return perm.hasWildcard && this.IsSubsetOf(this.hostname, perm.hostname);
			}
			if (this.addresses == null)
			{
				return false;
			}
			if (perm.hasWildcard)
			{
				foreach (IPAddress ipaddress in this.addresses)
				{
					if (this.IsSubsetOf(ipaddress.ToString(), perm.hostname))
					{
						return true;
					}
				}
			}
			if (perm.addresses == null)
			{
				return false;
			}
			foreach (IPAddress ipaddress2 in perm.addresses)
			{
				if (this.IsSubsetOf(this.hostname, ipaddress2.ToString()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000B74F0 File Offset: 0x000B56F0
		private bool IsSubsetOf(string addr1, string addr2)
		{
			string[] array = addr1.Split(EndpointPermission.dot_char);
			string[] array2 = addr2.Split(EndpointPermission.dot_char);
			for (int i = 0; i < 4; i++)
			{
				int num = this.ToNumber(array[i]);
				if (num == -1)
				{
					return false;
				}
				int num2 = this.ToNumber(array2[i]);
				if (num2 == -1)
				{
					return false;
				}
				if (num != 256 && num != num2 && num2 != 256)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000B755C File Offset: 0x000B575C
		internal EndpointPermission Intersect(EndpointPermission perm)
		{
			if (perm == null)
			{
				return null;
			}
			int num;
			if (this.port == perm.port)
			{
				num = this.port;
			}
			else if (this.port == -1)
			{
				num = perm.port;
			}
			else
			{
				if (perm.port != -1)
				{
					return null;
				}
				num = this.port;
			}
			TransportType transportType;
			if (this.transport == perm.transport)
			{
				transportType = this.transport;
			}
			else if (this.transport == TransportType.All)
			{
				transportType = perm.transport;
			}
			else
			{
				if (perm.transport != TransportType.All)
				{
					return null;
				}
				transportType = this.transport;
			}
			string text = this.IntersectHostname(perm);
			if (text == null)
			{
				return null;
			}
			if (!this.hasWildcard)
			{
				return this;
			}
			if (!perm.hasWildcard)
			{
				return perm;
			}
			return new EndpointPermission(text, num, transportType)
			{
				hasWildcard = true,
				resolved = true
			};
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000B7620 File Offset: 0x000B5820
		private string IntersectHostname(EndpointPermission perm)
		{
			if (this.hostname == perm.hostname)
			{
				return this.hostname;
			}
			this.Resolve();
			perm.Resolve();
			string text = null;
			if (this.hasWildcard)
			{
				if (perm.hasWildcard)
				{
					text = this.Intersect(this.hostname, perm.hostname);
				}
				else if (perm.addresses != null)
				{
					for (int i = 0; i < perm.addresses.Length; i++)
					{
						text = this.Intersect(this.hostname, perm.addresses[i].ToString());
						if (text != null)
						{
							break;
						}
					}
				}
			}
			else if (this.addresses != null)
			{
				for (int j = 0; j < this.addresses.Length; j++)
				{
					string addr = this.addresses[j].ToString();
					if (perm.hasWildcard)
					{
						text = this.Intersect(addr, perm.hostname);
					}
					else if (perm.addresses != null)
					{
						for (int k = 0; k < perm.addresses.Length; k++)
						{
							text = this.Intersect(addr, perm.addresses[k].ToString());
							if (text != null)
							{
								break;
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000B7738 File Offset: 0x000B5938
		private string Intersect(string addr1, string addr2)
		{
			string[] array = addr1.Split(EndpointPermission.dot_char);
			string[] array2 = addr2.Split(EndpointPermission.dot_char);
			string[] array3 = new string[7];
			for (int i = 0; i < 4; i++)
			{
				int num = this.ToNumber(array[i]);
				if (num == -1)
				{
					return null;
				}
				int num2 = this.ToNumber(array2[i]);
				if (num2 == -1)
				{
					return null;
				}
				if (num == 256)
				{
					array3[i << 1] = ((num2 == 256) ? "*" : (string.Empty + num2.ToString()));
				}
				else if (num2 == 256)
				{
					array3[i << 1] = ((num == 256) ? "*" : (string.Empty + num.ToString()));
				}
				else
				{
					if (num != num2)
					{
						return null;
					}
					array3[i << 1] = string.Empty + num.ToString();
				}
			}
			array3[1] = (array3[3] = (array3[5] = "."));
			return string.Concat(array3);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000B783C File Offset: 0x000B5A3C
		private int ToNumber(string value)
		{
			if (value == "*")
			{
				return 256;
			}
			int length = value.Length;
			if (length < 1 || length > 3)
			{
				return -1;
			}
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if ('0' > c || c > '9')
				{
					return -1;
				}
				num = checked(num * 10 + (int)(c - '0'));
			}
			if (num > 255)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000B78A8 File Offset: 0x000B5AA8
		internal void Resolve()
		{
			if (this.resolved)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			this.addresses = null;
			string[] array = this.hostname.Split(EndpointPermission.dot_char);
			if (array.Length != 4)
			{
				flag = true;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					int num = this.ToNumber(array[i]);
					if (num == -1)
					{
						flag = true;
						break;
					}
					if (num == 256)
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				this.hasWildcard = false;
				try
				{
					this.addresses = Dns.GetHostAddresses(this.hostname);
					goto IL_A3;
				}
				catch (SocketException)
				{
					goto IL_A3;
				}
			}
			this.hasWildcard = flag2;
			if (!flag2)
			{
				this.addresses = new IPAddress[1];
				this.addresses[0] = IPAddress.Parse(this.hostname);
			}
			IL_A3:
			this.resolved = true;
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000B7970 File Offset: 0x000B5B70
		internal void UndoResolve()
		{
			this.resolved = false;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000B7979 File Offset: 0x000B5B79
		// Note: this type is marked as 'beforefieldinit'.
		static EndpointPermission()
		{
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal EndpointPermission()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001E99 RID: 7833
		private static char[] dot_char = new char[]
		{
			'.'
		};

		// Token: 0x04001E9A RID: 7834
		private string hostname;

		// Token: 0x04001E9B RID: 7835
		private int port;

		// Token: 0x04001E9C RID: 7836
		private TransportType transport;

		// Token: 0x04001E9D RID: 7837
		private bool resolved;

		// Token: 0x04001E9E RID: 7838
		private bool hasWildcard;

		// Token: 0x04001E9F RID: 7839
		private IPAddress[] addresses;
	}
}
