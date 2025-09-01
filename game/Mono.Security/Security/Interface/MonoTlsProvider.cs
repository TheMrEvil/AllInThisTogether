using System;
using System.IO;
using System.Security.Authentication;

namespace Mono.Security.Interface
{
	// Token: 0x02000048 RID: 72
	public abstract class MonoTlsProvider
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000F00D File Offset: 0x0000D20D
		internal MonoTlsProvider()
		{
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002A8 RID: 680
		public abstract Guid ID { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002A9 RID: 681
		public abstract string Name { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002AA RID: 682
		public abstract bool SupportsSslStream { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002AB RID: 683
		public abstract bool SupportsConnectionInfo { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002AC RID: 684
		public abstract bool SupportsMonoExtensions { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002AD RID: 685
		public abstract SslProtocols SupportedProtocols { get; }

		// Token: 0x060002AE RID: 686
		public abstract IMonoSslStream CreateSslStream(Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings = null);

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000F015 File Offset: 0x0000D215
		internal virtual bool HasNativeCertificates
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002B0 RID: 688
		internal abstract bool SupportsCleanShutdown { get; }
	}
}
