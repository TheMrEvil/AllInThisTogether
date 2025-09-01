using System;

namespace System.Net
{
	/// <summary>Contains a global default proxy instance for all HTTP requests.</summary>
	// Token: 0x020005CD RID: 1485
	[Obsolete("This class has been deprecated. Please use WebRequest.DefaultWebProxy instead to access and set the global default proxy. Use 'null' instead of GetEmptyWebProxy. https://go.microsoft.com/fwlink/?linkid=14202")]
	public class GlobalProxySelection
	{
		/// <summary>Gets or sets the global HTTP proxy.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that every call to <see cref="M:System.Net.HttpWebRequest.GetResponse" /> uses.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation was <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission for the requested operation.</exception>
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x000A5C54 File Offset: 0x000A3E54
		// (set) Token: 0x0600300B RID: 12299 RVA: 0x000A5C82 File Offset: 0x000A3E82
		public static IWebProxy Select
		{
			get
			{
				IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
				if (defaultWebProxy == null)
				{
					return GlobalProxySelection.GetEmptyWebProxy();
				}
				WebRequest.WebProxyWrapper webProxyWrapper = defaultWebProxy as WebRequest.WebProxyWrapper;
				if (webProxyWrapper != null)
				{
					return webProxyWrapper.WebProxy;
				}
				return defaultWebProxy;
			}
			set
			{
				WebRequest.DefaultWebProxy = value;
			}
		}

		/// <summary>Returns an empty proxy instance.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that contains no information.</returns>
		// Token: 0x0600300C RID: 12300 RVA: 0x000A5C8A File Offset: 0x000A3E8A
		public static IWebProxy GetEmptyWebProxy()
		{
			return new EmptyWebProxy();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.GlobalProxySelection" /> class.</summary>
		// Token: 0x0600300D RID: 12301 RVA: 0x0000219B File Offset: 0x0000039B
		public GlobalProxySelection()
		{
		}
	}
}
