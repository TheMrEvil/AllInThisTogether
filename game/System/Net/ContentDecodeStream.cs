using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000670 RID: 1648
	internal class ContentDecodeStream : WebReadStream
	{
		// Token: 0x060033ED RID: 13293 RVA: 0x000B50FC File Offset: 0x000B32FC
		public static ContentDecodeStream Create(WebOperation operation, Stream innerStream, ContentDecodeStream.Mode mode)
		{
			Stream decodeStream;
			if (mode == ContentDecodeStream.Mode.GZip)
			{
				decodeStream = new GZipStream(innerStream, CompressionMode.Decompress);
			}
			else
			{
				decodeStream = new DeflateStream(innerStream, CompressionMode.Decompress);
			}
			return new ContentDecodeStream(operation, decodeStream, innerStream);
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x000B5126 File Offset: 0x000B3326
		private Stream OriginalInnerStream
		{
			[CompilerGenerated]
			get
			{
				return this.<OriginalInnerStream>k__BackingField;
			}
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000B512E File Offset: 0x000B332E
		private ContentDecodeStream(WebOperation operation, Stream decodeStream, Stream originalInnerStream) : base(operation, decodeStream)
		{
			this.OriginalInnerStream = originalInnerStream;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000B513F File Offset: 0x000B333F
		protected override Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			return base.InnerStream.ReadAsync(buffer, offset, size, cancellationToken);
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000B5154 File Offset: 0x000B3354
		internal override Task FinishReading(CancellationToken cancellationToken)
		{
			WebReadStream webReadStream = this.OriginalInnerStream as WebReadStream;
			if (webReadStream != null)
			{
				return webReadStream.FinishReading(cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x04001E6F RID: 7791
		[CompilerGenerated]
		private readonly Stream <OriginalInnerStream>k__BackingField;

		// Token: 0x02000671 RID: 1649
		internal enum Mode
		{
			// Token: 0x04001E71 RID: 7793
			GZip,
			// Token: 0x04001E72 RID: 7794
			Deflate
		}
	}
}
