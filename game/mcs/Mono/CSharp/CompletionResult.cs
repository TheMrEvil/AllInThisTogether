using System;

namespace Mono.CSharp
{
	// Token: 0x020002CE RID: 718
	public class CompletionResult : Exception
	{
		// Token: 0x06002263 RID: 8803 RVA: 0x000A8364 File Offset: 0x000A6564
		public CompletionResult(string base_text, string[] res)
		{
			if (base_text == null)
			{
				throw new ArgumentNullException("base_text");
			}
			this.base_text = base_text;
			this.result = res;
			Array.Sort<string>(this.result);
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x000A8393 File Offset: 0x000A6593
		public string[] Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x000A839B File Offset: 0x000A659B
		public string BaseText
		{
			get
			{
				return this.base_text;
			}
		}

		// Token: 0x04000CAE RID: 3246
		private string[] result;

		// Token: 0x04000CAF RID: 3247
		private string base_text;
	}
}
