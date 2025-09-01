using System;

namespace QFSW.QC.Parsers
{
	// Token: 0x0200000F RID: 15
	public class TypeParser : BasicCachedQcParser<Type>
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002ABA File Offset: 0x00000CBA
		public override Type Parse(string value)
		{
			return QuantumParser.ParseType(value);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002AC2 File Offset: 0x00000CC2
		public TypeParser()
		{
		}
	}
}
