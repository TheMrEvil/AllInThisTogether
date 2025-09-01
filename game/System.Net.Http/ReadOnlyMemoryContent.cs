using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000033 RID: 51
	public sealed class ReadOnlyMemoryContent : HttpContent
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00006A99 File Offset: 0x00004C99
		public ReadOnlyMemoryContent(ReadOnlyMemory<byte> content)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00002874 File Offset: 0x00000A74
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00002874 File Offset: 0x00000A74
		protected internal override bool TryComputeLength(out long length)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
