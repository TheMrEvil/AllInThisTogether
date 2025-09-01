using System;
using UnityEngine;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000010 RID: 16
	public class Vector2IntParser : BasicCachedQcParser<Vector2Int>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002ACA File Offset: 0x00000CCA
		public override Vector2Int Parse(string value)
		{
			return (Vector2Int)base.ParseRecursive<Vector3Int>(value);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public Vector2IntParser()
		{
		}
	}
}
