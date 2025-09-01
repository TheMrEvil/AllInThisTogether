using System;
using UnityEngine;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000013 RID: 19
	public class Vector3Parser : BasicCachedQcParser<Vector3>
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002B90 File Offset: 0x00000D90
		public override Vector3 Parse(string value)
		{
			return base.ParseRecursive<Vector4>(value);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B9E File Offset: 0x00000D9E
		public Vector3Parser()
		{
		}
	}
}
