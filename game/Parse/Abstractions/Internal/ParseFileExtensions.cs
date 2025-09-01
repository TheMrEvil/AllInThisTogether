using System;

namespace Parse.Abstractions.Internal
{
	// Token: 0x02000072 RID: 114
	public static class ParseFileExtensions
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x00011C81 File Offset: 0x0000FE81
		public static ParseFile Create(string name, Uri uri, string mimeType = null)
		{
			return new ParseFile(name, uri, mimeType);
		}
	}
}
