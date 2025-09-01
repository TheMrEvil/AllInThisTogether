using System;

namespace System.Net
{
	// Token: 0x02000592 RID: 1426
	internal class FtpWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x06002E5A RID: 11866 RVA: 0x0000219B File Offset: 0x0000039B
		internal FtpWebRequestCreator()
		{
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000A0919 File Offset: 0x0009EB19
		public WebRequest Create(Uri uri)
		{
			return new FtpWebRequest(uri);
		}
	}
}
