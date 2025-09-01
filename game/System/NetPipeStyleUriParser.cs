using System;

namespace System
{
	/// <summary>A parser based on the NetPipe scheme for the "Indigo" system.</summary>
	// Token: 0x02000164 RID: 356
	public class NetPipeStyleUriParser : UriParser
	{
		/// <summary>Create a parser based on the NetPipe scheme for the "Indigo" system.</summary>
		// Token: 0x06000991 RID: 2449 RVA: 0x0002A319 File Offset: 0x00028519
		public NetPipeStyleUriParser() : base(UriParser.NetPipeUri.Flags)
		{
		}
	}
}
