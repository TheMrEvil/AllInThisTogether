using System;
using UnityEngine;

namespace QFSW.QC.Serializers
{
	// Token: 0x0200000D RID: 13
	public class Vector3IntSerializer : BasicQcSerializer<Vector3Int>
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002375 File Offset: 0x00000575
		public override string SerializeFormatted(Vector3Int value, QuantumTheme theme)
		{
			return string.Format("({0}, {1}, {2})", value.x, value.y, value.z);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023A5 File Offset: 0x000005A5
		public Vector3IntSerializer()
		{
		}
	}
}
