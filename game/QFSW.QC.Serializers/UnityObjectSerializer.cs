using System;
using UnityEngine;

namespace QFSW.QC.Serializers
{
	// Token: 0x0200000A RID: 10
	public class UnityObjectSerializer : PolymorphicQcSerializer<UnityEngine.Object>
	{
		// Token: 0x06000015 RID: 21 RVA: 0x0000230F File Offset: 0x0000050F
		public override string SerializeFormatted(UnityEngine.Object value, QuantumTheme theme)
		{
			return value.name;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002317 File Offset: 0x00000517
		public UnityObjectSerializer()
		{
		}
	}
}
