using System;

namespace System
{
	/// <summary>A customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
	// Token: 0x02000163 RID: 355
	public class LdapStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Lightweight Directory Access Protocol (LDAP) scheme.</summary>
		// Token: 0x06000990 RID: 2448 RVA: 0x0002A307 File Offset: 0x00028507
		public LdapStyleUriParser() : base(UriParser.LdapUri.Flags)
		{
		}
	}
}
