using System;
using System.Collections.Generic;

namespace Parse.Infrastructure.Control
{
	// Token: 0x0200006C RID: 108
	public class ParseObjectIdComparer : IEqualityComparer<object>
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x000107B4 File Offset: 0x0000E9B4
		bool IEqualityComparer<object>.Equals(object p1, object p2)
		{
			ParseObject parseObject = p1 as ParseObject;
			ParseObject parseObject2 = p2 as ParseObject;
			if (parseObject != null && parseObject2 != null)
			{
				return object.Equals(parseObject.ObjectId, parseObject2.ObjectId);
			}
			return object.Equals(p1, p2);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000107F0 File Offset: 0x0000E9F0
		public int GetHashCode(object p)
		{
			ParseObject parseObject = p as ParseObject;
			if (parseObject != null)
			{
				return parseObject.ObjectId.GetHashCode();
			}
			return p.GetHashCode();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00010819 File Offset: 0x0000EA19
		public ParseObjectIdComparer()
		{
		}
	}
}
