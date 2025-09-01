using System;

namespace QFSW.QC.Parsers
{
	// Token: 0x0200000D RID: 13
	public class StringParser : BasicCachedQcParser<string>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000028CE File Offset: 0x00000ACE
		public override int Priority
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000028D5 File Offset: 0x00000AD5
		public override string Parse(string value)
		{
			return value.ReduceScope('"', '"').UnescapeText('"');
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000028E8 File Offset: 0x00000AE8
		public StringParser()
		{
		}
	}
}
