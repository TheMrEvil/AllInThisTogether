using System;

namespace System.Net
{
	// Token: 0x02000658 RID: 1624
	internal class FileWebRequestCreator : IWebRequestCreate
	{
		// Token: 0x06003343 RID: 13123 RVA: 0x0000219B File Offset: 0x0000039B
		internal FileWebRequestCreator()
		{
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000B2E9C File Offset: 0x000B109C
		public WebRequest Create(Uri uri)
		{
			return new FileWebRequest(uri);
		}
	}
}
