using System;
using System.Collections.Generic;
using Parse.Abstractions.Infrastructure.Control;

namespace Parse.Infrastructure.Control
{
	// Token: 0x0200006D RID: 109
	internal static class ParseFieldOperations
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x00010821 File Offset: 0x0000EA21
		public static IParseFieldOperation Decode(IDictionary<string, object> json)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00010828 File Offset: 0x0000EA28
		public static IEqualityComparer<object> ParseObjectComparer
		{
			get
			{
				if (ParseFieldOperations.comparer == null)
				{
					ParseFieldOperations.comparer = new ParseObjectIdComparer();
				}
				return ParseFieldOperations.comparer;
			}
		}

		// Token: 0x040000F6 RID: 246
		private static ParseObjectIdComparer comparer;
	}
}
