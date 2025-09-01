using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Defines an attribute that can be used at the call site to specify the URL where the activation will happen. This class cannot be inherited.</summary>
	// Token: 0x020005D5 RID: 1493
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlAttribute : ContextAttribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> class.</summary>
		/// <param name="callsiteURL">The call site URL.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callsiteURL" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060038EB RID: 14571 RVA: 0x000CAF2F File Offset: 0x000C912F
		public UrlAttribute(string callsiteURL) : base(callsiteURL)
		{
			this.url = callsiteURL;
		}

		/// <summary>Gets the URL value of the <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</summary>
		/// <returns>The URL value of the <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x000CAF3F File Offset: 0x000C913F
		public string UrlValue
		{
			get
			{
				return this.url;
			}
		}

		/// <summary>Checks whether the specified object refers to the same URL as the current instance.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</param>
		/// <returns>
		///   <see langword="true" /> if the object is a <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> with the same value; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060038ED RID: 14573 RVA: 0x000CAF47 File Offset: 0x000C9147
		public override bool Equals(object o)
		{
			return o is UrlAttribute && ((UrlAttribute)o).UrlValue == this.url;
		}

		/// <summary>Returns the hash value for the current <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</summary>
		/// <returns>The hash value for the current <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060038EE RID: 14574 RVA: 0x000CAF69 File Offset: 0x000C9169
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		/// <summary>Forces the creation of the context and the server object inside the context at the specified URL.</summary>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> of the server object to create.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060038EF RID: 14575 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[ComVisible(true)]
		[SecurityCritical]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
		}

		/// <summary>Returns a Boolean value that indicates whether the specified <see cref="T:System.Runtime.Remoting.Contexts.Context" /> meets <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" />'s requirements.</summary>
		/// <param name="ctx">The context to check against the current context attribute.</param>
		/// <param name="msg">The construction call, the parameters of which need to be checked against the current context.</param>
		/// <returns>
		///   <see langword="true" /> if the passed-in context is acceptable; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060038F0 RID: 14576 RVA: 0x000040F7 File Offset: 0x000022F7
		[ComVisible(true)]
		[SecurityCritical]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}

		// Token: 0x040025FD RID: 9725
		private string url;
	}
}
