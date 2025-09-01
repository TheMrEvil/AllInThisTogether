using System;
using QFSW.QC.Utilities;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000008 RID: 8
	public class EnumParser : PolymorphicCachedQcParser<Enum>
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002650 File Offset: 0x00000850
		public override Enum Parse(string value, Type type)
		{
			Enum result;
			try
			{
				result = (Enum)Enum.Parse(type, value);
			}
			catch (Exception innerException)
			{
				throw new ParserInputException(string.Format("Cannot parse '{0}' to the type '{1}'. To see the supported values, use the command `enum-info {2}`", value, type.GetDisplayName(false), type), innerException);
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002698 File Offset: 0x00000898
		public EnumParser()
		{
		}
	}
}
