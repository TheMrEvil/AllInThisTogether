using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>Specifies permission to access Internet resources. This class cannot be inherited.</summary>
	// Token: 0x0200060F RID: 1551
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class WebPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebPermissionAttribute" /> class with a value that specifies the security actions that can be performed on this class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="action" /> is not a valid <see cref="T:System.Security.Permissions.SecurityAction" /> value.</exception>
		// Token: 0x0600311A RID: 12570 RVA: 0x000A97B6 File Offset: 0x000A79B6
		public WebPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the URI connection string controlled by the current <see cref="T:System.Net.WebPermissionAttribute" />.</summary>
		/// <returns>A string containing the URI connection controlled by the current <see cref="T:System.Net.WebPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebPermissionAttribute.Connect" /> is not <see langword="null" /> when you attempt to set the value. If you wish to specify more than one Connect URI, use an additional attribute declaration statement.</exception>
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000A97BF File Offset: 0x000A79BF
		// (set) Token: 0x0600311C RID: 12572 RVA: 0x000A97CC File Offset: 0x000A79CC
		public string Connect
		{
			get
			{
				return this.m_connect as string;
			}
			set
			{
				if (this.m_connect != null)
				{
					throw new ArgumentException(SR.GetString("The permission '{0}={1}' cannot be added. Add a separate Attribute statement.", new object[]
					{
						"Connect",
						value
					}), "value");
				}
				this.m_connect = value;
			}
		}

		/// <summary>Gets or sets the URI string accepted by the current <see cref="T:System.Net.WebPermissionAttribute" />.</summary>
		/// <returns>A string containing the URI accepted by the current <see cref="T:System.Net.WebPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebPermissionAttribute.Accept" /> is not <see langword="null" /> when you attempt to set the value. If you wish to specify more than one Accept URI, use an additional attribute declaration statement.</exception>
		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x000A9804 File Offset: 0x000A7A04
		// (set) Token: 0x0600311E RID: 12574 RVA: 0x000A9811 File Offset: 0x000A7A11
		public string Accept
		{
			get
			{
				return this.m_accept as string;
			}
			set
			{
				if (this.m_accept != null)
				{
					throw new ArgumentException(SR.GetString("The permission '{0}={1}' cannot be added. Add a separate Attribute statement.", new object[]
					{
						"Accept",
						value
					}), "value");
				}
				this.m_accept = value;
			}
		}

		/// <summary>Gets or sets a regular expression pattern that describes the URI connection controlled by the current <see cref="T:System.Net.WebPermissionAttribute" />.</summary>
		/// <returns>A string containing a regular expression pattern that describes the URI connection controlled by this <see cref="T:System.Net.WebPermissionAttribute" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebPermissionAttribute.ConnectPattern" /> is not <see langword="null" /> when you attempt to set the value. If you wish to specify more than one connect URI, use an additional attribute declaration statement.</exception>
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000A9849 File Offset: 0x000A7A49
		// (set) Token: 0x06003120 RID: 12576 RVA: 0x000A9888 File Offset: 0x000A7A88
		public string ConnectPattern
		{
			get
			{
				if (this.m_connect is DelayedRegex)
				{
					return this.m_connect.ToString();
				}
				if (!(this.m_connect is bool) || !(bool)this.m_connect)
				{
					return null;
				}
				return ".*";
			}
			set
			{
				if (this.m_connect != null)
				{
					throw new ArgumentException(SR.GetString("The permission '{0}={1}' cannot be added. Add a separate Attribute statement.", new object[]
					{
						"ConnectPatern",
						value
					}), "value");
				}
				if (value == ".*")
				{
					this.m_connect = true;
					return;
				}
				this.m_connect = new DelayedRegex(value);
			}
		}

		/// <summary>Gets or sets a regular expression pattern that describes the URI accepted by the current <see cref="T:System.Net.WebPermissionAttribute" />.</summary>
		/// <returns>A string containing a regular expression pattern that describes the URI accepted by the current <see cref="T:System.Net.WebPermissionAttribute" />. This string must be escaped according to the rules for encoding a <see cref="T:System.Text.RegularExpressions.Regex" /> constructor string.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebPermissionAttribute.AcceptPattern" /> is not <see langword="null" /> when you attempt to set the value. If you wish to specify more than one Accept URI, use an additional attribute declaration statement.</exception>
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x000A98EA File Offset: 0x000A7AEA
		// (set) Token: 0x06003122 RID: 12578 RVA: 0x000A9928 File Offset: 0x000A7B28
		public string AcceptPattern
		{
			get
			{
				if (this.m_accept is DelayedRegex)
				{
					return this.m_accept.ToString();
				}
				if (!(this.m_accept is bool) || !(bool)this.m_accept)
				{
					return null;
				}
				return ".*";
			}
			set
			{
				if (this.m_accept != null)
				{
					throw new ArgumentException(SR.GetString("The permission '{0}={1}' cannot be added. Add a separate Attribute statement.", new object[]
					{
						"AcceptPattern",
						value
					}), "value");
				}
				if (value == ".*")
				{
					this.m_accept = true;
					return;
				}
				this.m_accept = new DelayedRegex(value);
			}
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Net.WebPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.WebPermission" /> corresponding to the security declaration.</returns>
		// Token: 0x06003123 RID: 12579 RVA: 0x000A998C File Offset: 0x000A7B8C
		public override IPermission CreatePermission()
		{
			WebPermission webPermission;
			if (base.Unrestricted)
			{
				webPermission = new WebPermission(PermissionState.Unrestricted);
			}
			else
			{
				NetworkAccess networkAccess = (NetworkAccess)0;
				if (this.m_connect is bool)
				{
					if ((bool)this.m_connect)
					{
						networkAccess |= NetworkAccess.Connect;
					}
					this.m_connect = null;
				}
				if (this.m_accept is bool)
				{
					if ((bool)this.m_accept)
					{
						networkAccess |= NetworkAccess.Accept;
					}
					this.m_accept = null;
				}
				webPermission = new WebPermission(networkAccess);
				if (this.m_accept != null)
				{
					if (this.m_accept is DelayedRegex)
					{
						webPermission.AddAsPattern(NetworkAccess.Accept, (DelayedRegex)this.m_accept);
					}
					else
					{
						webPermission.AddPermission(NetworkAccess.Accept, (string)this.m_accept);
					}
				}
				if (this.m_connect != null)
				{
					if (this.m_connect is DelayedRegex)
					{
						webPermission.AddAsPattern(NetworkAccess.Connect, (DelayedRegex)this.m_connect);
					}
					else
					{
						webPermission.AddPermission(NetworkAccess.Connect, (string)this.m_connect);
					}
				}
			}
			return webPermission;
		}

		// Token: 0x04001C96 RID: 7318
		private object m_accept;

		// Token: 0x04001C97 RID: 7319
		private object m_connect;
	}
}
