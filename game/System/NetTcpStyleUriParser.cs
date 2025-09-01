using System;

namespace System
{
	/// <summary>A parser based on the NetTcp scheme for the "Indigo" system.</summary>
	// Token: 0x02000165 RID: 357
	public class NetTcpStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetTcp scheme for the "Indigo" system.</summary>
		// Token: 0x06000992 RID: 2450 RVA: 0x0002A32B File Offset: 0x0002852B
		public NetTcpStyleUriParser() : base(UriParser.NetTcpUri.Flags)
		{
		}
	}
}
