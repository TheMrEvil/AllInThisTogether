using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the HttpListener element in the configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000875 RID: 2165
	public sealed class HttpListenerElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.HttpListenerElement" /> class.</summary>
		// Token: 0x060044A8 RID: 17576 RVA: 0x00013BCA File Offset: 0x00011DCA
		public HttpListenerElement()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the default timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>The timeout elements used for an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x00032884 File Offset: 0x00030A84
		public HttpListenerTimeoutsElement Timeouts
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI instead of the converted URI.</summary>
		/// <returns>A Boolean value that indicates if <see cref="T:System.Net.HttpListener" /> uses the raw unescaped URI, rather than the converted URI.</returns>
		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x000ED0BC File Offset: 0x000EB2BC
		public bool UnescapeRequestUrl
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}
	}
}
