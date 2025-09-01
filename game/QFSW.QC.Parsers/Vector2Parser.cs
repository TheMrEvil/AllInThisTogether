using System;
using UnityEngine;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000011 RID: 17
	public class Vector2Parser : BasicCachedQcParser<Vector2>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public override Vector2 Parse(string value)
		{
			return base.ParseRecursive<Vector4>(value);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002AEE File Offset: 0x00000CEE
		public Vector2Parser()
		{
		}
	}
}
