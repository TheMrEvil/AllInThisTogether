using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Controls rights to make or accept connections on a transport address.</summary>
	// Token: 0x020006B6 RID: 1718
	[Serializable]
	public sealed class SocketPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermission" /> class that allows unrestricted access to the <see cref="T:System.Net.Sockets.Socket" /> or disallows access to the <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		// Token: 0x0600373F RID: 14143 RVA: 0x000C1BB2 File Offset: 0x000BFDB2
		public SocketPermission(PermissionState state)
		{
			this.m_noRestriction = (state == PermissionState.Unrestricted);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.SocketPermission" /> class for the given transport address with the specified permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkAccess" /> values.</param>
		/// <param name="transport">One of the <see cref="T:System.Net.TransportType" /> values.</param>
		/// <param name="hostName">The host name for the transport address.</param>
		/// <param name="portNumber">The port number for the transport address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is <see langword="null" />.</exception>
		// Token: 0x06003740 RID: 14144 RVA: 0x000C1BDA File Offset: 0x000BFDDA
		public SocketPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			this.m_noRestriction = false;
			this.AddPermission(access, transport, hostName, portNumber);
		}

		/// <summary>Gets a list of <see cref="T:System.Net.EndpointPermission" /> instances that identifies the endpoints that can be accepted under this permission instance.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Collections.IEnumerator" /> interface that contains <see cref="T:System.Net.EndpointPermission" /> instances.</returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000C1C0A File Offset: 0x000BFE0A
		public IEnumerator AcceptList
		{
			get
			{
				return this.m_acceptList.GetEnumerator();
			}
		}

		/// <summary>Gets a list of <see cref="T:System.Net.EndpointPermission" /> instances that identifies the endpoints that can be connected to under this permission instance.</summary>
		/// <returns>An instance that implements the <see cref="T:System.Collections.IEnumerator" /> interface that contains <see cref="T:System.Net.EndpointPermission" /> instances.</returns>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x000C1C17 File Offset: 0x000BFE17
		public IEnumerator ConnectList
		{
			get
			{
				return this.m_connectList.GetEnumerator();
			}
		}

		/// <summary>Adds a permission to the set of permissions for a transport address.</summary>
		/// <param name="access">One of the <see cref="T:System.Net.NetworkAccess" /> values.</param>
		/// <param name="transport">One of the <see cref="T:System.Net.TransportType" /> values.</param>
		/// <param name="hostName">The host name for the transport address.</param>
		/// <param name="portNumber">The port number for the transport address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostName" /> is <see langword="null" />.</exception>
		// Token: 0x06003743 RID: 14147 RVA: 0x000C1C24 File Offset: 0x000BFE24
		public void AddPermission(NetworkAccess access, TransportType transport, string hostName, int portNumber)
		{
			if (this.m_noRestriction)
			{
				return;
			}
			EndpointPermission value = new EndpointPermission(hostName, portNumber, transport);
			if (access == NetworkAccess.Accept)
			{
				this.m_acceptList.Add(value);
				return;
			}
			this.m_connectList.Add(value);
		}

		/// <summary>Creates a copy of a <see cref="T:System.Net.SocketPermission" /> instance.</summary>
		/// <returns>A new instance of the <see cref="T:System.Net.SocketPermission" /> class that is a copy of the current instance.</returns>
		// Token: 0x06003744 RID: 14148 RVA: 0x000C1C67 File Offset: 0x000BFE67
		public override IPermission Copy()
		{
			return new SocketPermission(this.m_noRestriction ? PermissionState.Unrestricted : PermissionState.None)
			{
				m_connectList = (ArrayList)this.m_connectList.Clone(),
				m_acceptList = (ArrayList)this.m_acceptList.Clone()
			};
		}

		/// <summary>Returns the logical intersection between two <see cref="T:System.Net.SocketPermission" /> instances.</summary>
		/// <param name="target">The <see cref="T:System.Net.SocketPermission" /> instance to intersect with the current instance.</param>
		/// <returns>The <see cref="T:System.Net.SocketPermission" /> instance that represents the intersection of two <see cref="T:System.Net.SocketPermission" /> instances. If the intersection is empty, the method returns <see langword="null" />. If the <paramref name="target" /> parameter is a null reference, the method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not a <see cref="T:System.Net.SocketPermission" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Net.DnsPermission" /> is not granted to the method caller.</exception>
		// Token: 0x06003745 RID: 14149 RVA: 0x000C1CA8 File Offset: 0x000BFEA8
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException("Argument not of type SocketPermission");
			}
			if (this.m_noRestriction)
			{
				if (!this.IntersectEmpty(socketPermission))
				{
					return socketPermission.Copy();
				}
				return null;
			}
			else if (socketPermission.m_noRestriction)
			{
				if (!this.IntersectEmpty(this))
				{
					return this.Copy();
				}
				return null;
			}
			else
			{
				SocketPermission socketPermission2 = new SocketPermission(PermissionState.None);
				this.Intersect(this.m_connectList, socketPermission.m_connectList, socketPermission2.m_connectList);
				this.Intersect(this.m_acceptList, socketPermission.m_acceptList, socketPermission2.m_acceptList);
				if (!this.IntersectEmpty(socketPermission2))
				{
					return socketPermission2;
				}
				return null;
			}
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000C1D46 File Offset: 0x000BFF46
		private bool IntersectEmpty(SocketPermission permission)
		{
			return !permission.m_noRestriction && permission.m_connectList.Count == 0 && permission.m_acceptList.Count == 0;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000C1D70 File Offset: 0x000BFF70
		private void Intersect(ArrayList list1, ArrayList list2, ArrayList result)
		{
			foreach (object obj in list1)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				foreach (object obj2 in list2)
				{
					EndpointPermission perm = (EndpointPermission)obj2;
					EndpointPermission endpointPermission2 = endpointPermission.Intersect(perm);
					if (endpointPermission2 != null)
					{
						bool flag = false;
						for (int i = 0; i < result.Count; i++)
						{
							EndpointPermission perm2 = (EndpointPermission)result[i];
							EndpointPermission endpointPermission3 = endpointPermission2.Intersect(perm2);
							if (endpointPermission3 != null)
							{
								result[i] = endpointPermission3;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							result.Add(endpointPermission2);
						}
					}
				}
			}
		}

		/// <summary>Determines if the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A <see cref="T:System.Net.SocketPermission" /> that is to be tested for the subset relationship.</param>
		/// <returns>If <paramref name="target" /> is <see langword="null" />, this method returns <see langword="true" /> if the current instance defines no permissions; otherwise, <see langword="false" />. If <paramref name="target" /> is not <see langword="null" />, this method returns <see langword="true" /> if the current instance defines a subset of <paramref name="target" /> permissions; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.Sockets.SocketException" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Net.DnsPermission" /> is not granted to the method caller.</exception>
		// Token: 0x06003748 RID: 14152 RVA: 0x000C1E64 File Offset: 0x000C0064
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_noRestriction && this.m_connectList.Count == 0 && this.m_acceptList.Count == 0;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException("Parameter target must be of type SocketPermission");
			}
			return socketPermission.m_noRestriction || (!this.m_noRestriction && ((this.m_acceptList.Count == 0 && this.m_connectList.Count == 0) || ((socketPermission.m_acceptList.Count != 0 || socketPermission.m_connectList.Count != 0) && this.IsSubsetOf(this.m_connectList, socketPermission.m_connectList) && this.IsSubsetOf(this.m_acceptList, socketPermission.m_acceptList))));
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000C1F24 File Offset: 0x000C0124
		private bool IsSubsetOf(ArrayList list1, ArrayList list2)
		{
			foreach (object obj in list1)
			{
				EndpointPermission endpointPermission = (EndpointPermission)obj;
				bool flag = false;
				foreach (object obj2 in list2)
				{
					EndpointPermission perm = (EndpointPermission)obj2;
					if (endpointPermission.IsSubsetOf(perm))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Checks the overall permission state of the object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.SocketPermission" /> instance is created with the <see langword="Unrestricted" /> value from <see cref="T:System.Security.Permissions.PermissionState" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600374A RID: 14154 RVA: 0x000C1FD0 File Offset: 0x000C01D0
		public bool IsUnrestricted()
		{
			return this.m_noRestriction;
		}

		/// <summary>Creates an XML encoding of a <see cref="T:System.Net.SocketPermission" /> instance and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> instance that contains an XML-encoded representation of the <see cref="T:System.Net.SocketPermission" /> instance, including state information.</returns>
		// Token: 0x0600374B RID: 14155 RVA: 0x000C1FD8 File Offset: 0x000C01D8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", base.GetType().AssemblyQualifiedName);
			securityElement.AddAttribute("version", "1");
			if (this.m_noRestriction)
			{
				securityElement.AddAttribute("Unrestricted", "true");
				return securityElement;
			}
			if (this.m_connectList.Count > 0)
			{
				this.ToXml(securityElement, "ConnectAccess", this.m_connectList.GetEnumerator());
			}
			if (this.m_acceptList.Count > 0)
			{
				this.ToXml(securityElement, "AcceptAccess", this.m_acceptList.GetEnumerator());
			}
			return securityElement;
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000C207C File Offset: 0x000C027C
		private void ToXml(SecurityElement root, string childName, IEnumerator enumerator)
		{
			SecurityElement securityElement = new SecurityElement(childName);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				EndpointPermission endpointPermission = obj as EndpointPermission;
				SecurityElement securityElement2 = new SecurityElement("ENDPOINT");
				securityElement2.AddAttribute("host", endpointPermission.Hostname);
				securityElement2.AddAttribute("transport", endpointPermission.Transport.ToString());
				securityElement2.AddAttribute("port", (endpointPermission.Port == -1) ? "All" : endpointPermission.Port.ToString());
				securityElement.AddChild(securityElement2);
			}
			root.AddChild(securityElement);
		}

		/// <summary>Reconstructs a <see cref="T:System.Net.SocketPermission" /> instance for an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding used to reconstruct the <see cref="T:System.Net.SocketPermission" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="securityElement" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="securityElement" /> is not a permission element for this type.</exception>
		// Token: 0x0600374D RID: 14157 RVA: 0x000C211C File Offset: 0x000C031C
		public override void FromXml(SecurityElement securityElement)
		{
			if (securityElement == null)
			{
				throw new ArgumentNullException("securityElement");
			}
			if (securityElement.Tag != "IPermission")
			{
				throw new ArgumentException("securityElement");
			}
			string text = securityElement.Attribute("Unrestricted");
			if (text != null)
			{
				this.m_noRestriction = (string.Compare(text, "true", true) == 0);
				if (this.m_noRestriction)
				{
					return;
				}
			}
			this.m_noRestriction = false;
			this.m_connectList = new ArrayList();
			this.m_acceptList = new ArrayList();
			foreach (object obj in securityElement.Children)
			{
				SecurityElement securityElement2 = (SecurityElement)obj;
				if (securityElement2.Tag == "ConnectAccess")
				{
					this.FromXml(securityElement2.Children, NetworkAccess.Connect);
				}
				else if (securityElement2.Tag == "AcceptAccess")
				{
					this.FromXml(securityElement2.Children, NetworkAccess.Accept);
				}
			}
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000C2228 File Offset: 0x000C0428
		private void FromXml(ArrayList endpoints, NetworkAccess access)
		{
			foreach (object obj in endpoints)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (!(securityElement.Tag != "ENDPOINT"))
				{
					string hostName = securityElement.Attribute("host");
					TransportType transport = (TransportType)Enum.Parse(typeof(TransportType), securityElement.Attribute("transport"), true);
					string text = securityElement.Attribute("port");
					int portNumber;
					if (text == "All")
					{
						portNumber = -1;
					}
					else
					{
						portNumber = int.Parse(text);
					}
					this.AddPermission(access, transport, hostName, portNumber);
				}
			}
		}

		/// <summary>Returns the logical union between two <see cref="T:System.Net.SocketPermission" /> instances.</summary>
		/// <param name="target">The <see cref="T:System.Net.SocketPermission" /> instance to combine with the current instance.</param>
		/// <returns>The <see cref="T:System.Net.SocketPermission" /> instance that represents the union of two <see cref="T:System.Net.SocketPermission" /> instances. If <paramref name="target" /> parameter is <see langword="null" />, it returns a copy of the current instance.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not a <see cref="T:System.Net.SocketPermission" />.</exception>
		// Token: 0x0600374F RID: 14159 RVA: 0x000C22F4 File Offset: 0x000C04F4
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SocketPermission socketPermission = target as SocketPermission;
			if (socketPermission == null)
			{
				throw new ArgumentException("Argument not of type SocketPermission");
			}
			if (this.m_noRestriction || socketPermission.m_noRestriction)
			{
				return new SocketPermission(PermissionState.Unrestricted);
			}
			SocketPermission socketPermission2 = (SocketPermission)socketPermission.Copy();
			socketPermission2.m_acceptList.InsertRange(socketPermission2.m_acceptList.Count, this.m_acceptList);
			socketPermission2.m_connectList.InsertRange(socketPermission2.m_connectList.Count, this.m_connectList);
			return socketPermission2;
		}

		// Token: 0x04002035 RID: 8245
		private ArrayList m_acceptList = new ArrayList();

		// Token: 0x04002036 RID: 8246
		private ArrayList m_connectList = new ArrayList();

		// Token: 0x04002037 RID: 8247
		private bool m_noRestriction;

		/// <summary>Defines a constant that represents all ports.</summary>
		// Token: 0x04002038 RID: 8248
		public const int AllPorts = -1;
	}
}
