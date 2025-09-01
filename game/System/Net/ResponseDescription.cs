using System;
using System.Text;

namespace System.Net
{
	// Token: 0x02000584 RID: 1412
	internal class ResponseDescription
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002DBF RID: 11711 RVA: 0x0009CD0E File Offset: 0x0009AF0E
		internal bool PositiveIntermediate
		{
			get
			{
				return this.Status >= 100 && this.Status <= 199;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x0009CD2C File Offset: 0x0009AF2C
		internal bool PositiveCompletion
		{
			get
			{
				return this.Status >= 200 && this.Status <= 299;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x0009CD4D File Offset: 0x0009AF4D
		internal bool TransientFailure
		{
			get
			{
				return this.Status >= 400 && this.Status <= 499;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x0009CD6E File Offset: 0x0009AF6E
		internal bool PermanentFailure
		{
			get
			{
				return this.Status >= 500 && this.Status <= 599;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x0009CD8F File Offset: 0x0009AF8F
		internal bool InvalidStatusCode
		{
			get
			{
				return this.Status < 100 || this.Status > 599;
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x0009CDAA File Offset: 0x0009AFAA
		public ResponseDescription()
		{
		}

		// Token: 0x04001917 RID: 6423
		internal const int NoStatus = -1;

		// Token: 0x04001918 RID: 6424
		internal bool Multiline;

		// Token: 0x04001919 RID: 6425
		internal int Status = -1;

		// Token: 0x0400191A RID: 6426
		internal string StatusDescription;

		// Token: 0x0400191B RID: 6427
		internal StringBuilder StatusBuffer = new StringBuilder();

		// Token: 0x0400191C RID: 6428
		internal string StatusCodeString;
	}
}
