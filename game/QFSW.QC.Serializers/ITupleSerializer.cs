using System;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Serializers
{
	// Token: 0x02000006 RID: 6
	public class ITupleSerializer : PolymorphicQcSerializer<ITuple>
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021AC File Offset: 0x000003AC
		public override string SerializeFormatted(ITuple value, QuantumTheme theme)
		{
			string[] array = new string[value.Length];
			for (int i = 0; i < value.Length; i++)
			{
				array[i] = base.SerializeRecursive(value[i], theme);
			}
			return "(" + string.Join(", ", array) + ")";
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002201 File Offset: 0x00000401
		public ITupleSerializer()
		{
		}
	}
}
