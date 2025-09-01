using System;

namespace QFSW.QC.Serializers
{
	// Token: 0x02000008 RID: 8
	public class StringSerializer : BasicQcSerializer<string>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022EC File Offset: 0x000004EC
		public override int Priority
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022F3 File Offset: 0x000004F3
		public override string SerializeFormatted(string value, QuantumTheme theme)
		{
			return value;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F6 File Offset: 0x000004F6
		public StringSerializer()
		{
		}
	}
}
