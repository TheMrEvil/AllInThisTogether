using System;
using System.Data.Common;

namespace System.Data.Sql
{
	// Token: 0x02000178 RID: 376
	internal sealed class SqlGenericUtil
	{
		// Token: 0x060013EC RID: 5100 RVA: 0x00003D93 File Offset: 0x00001F93
		private SqlGenericUtil()
		{
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0005B17A File Offset: 0x0005937A
		internal static Exception NullCommandText()
		{
			return ADP.Argument(Res.GetString("Command parameter must have a non null and non empty command text."));
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0005B18B File Offset: 0x0005938B
		internal static Exception MismatchedMetaDataDirectionArrayLengths()
		{
			return ADP.Argument(Res.GetString("MetaData parameter array must have length equivalent to ParameterDirection array argument."));
		}
	}
}
