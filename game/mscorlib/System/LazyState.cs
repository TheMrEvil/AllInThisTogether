using System;

namespace System
{
	// Token: 0x0200014F RID: 335
	internal enum LazyState
	{
		// Token: 0x0400125C RID: 4700
		NoneViaConstructor,
		// Token: 0x0400125D RID: 4701
		NoneViaFactory,
		// Token: 0x0400125E RID: 4702
		NoneException,
		// Token: 0x0400125F RID: 4703
		PublicationOnlyViaConstructor,
		// Token: 0x04001260 RID: 4704
		PublicationOnlyViaFactory,
		// Token: 0x04001261 RID: 4705
		PublicationOnlyWait,
		// Token: 0x04001262 RID: 4706
		PublicationOnlyException,
		// Token: 0x04001263 RID: 4707
		ExecutionAndPublicationViaConstructor,
		// Token: 0x04001264 RID: 4708
		ExecutionAndPublicationViaFactory,
		// Token: 0x04001265 RID: 4709
		ExecutionAndPublicationException
	}
}
