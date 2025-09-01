using System;
using System.Net;

namespace System.Xml
{
	// Token: 0x02000242 RID: 578
	internal class XmlNullResolver : XmlResolver
	{
		// Token: 0x06001588 RID: 5512 RVA: 0x000021F2 File Offset: 0x000003F2
		private XmlNullResolver()
		{
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000847B7 File Offset: 0x000829B7
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			throw new XmlException("Resolving of external URIs was prohibited.", string.Empty);
		}

		// Token: 0x170003D2 RID: 978
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x0000B528 File Offset: 0x00009728
		public override ICredentials Credentials
		{
			set
			{
			}
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x000847C8 File Offset: 0x000829C8
		// Note: this type is marked as 'beforefieldinit'.
		static XmlNullResolver()
		{
		}

		// Token: 0x04001305 RID: 4869
		public static readonly XmlNullResolver Singleton = new XmlNullResolver();
	}
}
