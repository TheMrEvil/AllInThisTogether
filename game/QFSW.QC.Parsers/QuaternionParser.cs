using System;
using UnityEngine;

namespace QFSW.QC.Parsers
{
	// Token: 0x0200000C RID: 12
	public class QuaternionParser : BasicCachedQcParser<Quaternion>
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002894 File Offset: 0x00000A94
		public override Quaternion Parse(string value)
		{
			Vector4 vector = base.ParseRecursive<Vector4>(value);
			return new Quaternion(vector.x, vector.y, vector.z, vector.w);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028C6 File Offset: 0x00000AC6
		public QuaternionParser()
		{
		}
	}
}
