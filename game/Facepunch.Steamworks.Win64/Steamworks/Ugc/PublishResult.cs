using System;
using Steamworks.Data;

namespace Steamworks.Ugc
{
	// Token: 0x020000C6 RID: 198
	public struct PublishResult
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00013693 File Offset: 0x00011893
		public bool Success
		{
			get
			{
				return this.Result == Result.OK;
			}
		}

		// Token: 0x04000796 RID: 1942
		public Result Result;

		// Token: 0x04000797 RID: 1943
		public PublishedFileId FileId;

		// Token: 0x04000798 RID: 1944
		public bool NeedsWorkshopAgreement;
	}
}
