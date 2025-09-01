using System;
using System.Collections;

namespace QFSW.QC.Serializers
{
	// Token: 0x02000002 RID: 2
	public class DictionaryEntrySerializer : BasicQcSerializer<DictionaryEntry>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override string SerializeFormatted(DictionaryEntry value, QuantumTheme theme)
		{
			string str = base.SerializeRecursive(value.Key, theme);
			string str2 = base.SerializeRecursive(value.Value, theme);
			return str + ": " + str2;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002085 File Offset: 0x00000285
		public DictionaryEntrySerializer()
		{
		}
	}
}
