using System;

namespace QFSW.QC.Parsers
{
	// Token: 0x02000003 RID: 3
	public class BoolParser : BasicCachedQcParser<bool>
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020B4 File Offset: 0x000002B4
		public override bool Parse(string value)
		{
			value = value.ToLower().Trim();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(value);
			if (num <= 1303515621U)
			{
				if (num <= 873244444U)
				{
					if (num != 184981848U)
					{
						if (num == 873244444U)
						{
							if (value == "1")
							{
								return true;
							}
						}
					}
					else if (value == "false")
					{
						return false;
					}
				}
				else if (num != 890022063U)
				{
					if (num == 1303515621U)
					{
						if (value == "true")
						{
							return true;
						}
					}
				}
				else if (value == "0")
				{
					return false;
				}
			}
			else if (num <= 1630810064U)
			{
				if (num != 1319056784U)
				{
					if (num == 1630810064U)
					{
						if (value == "on")
						{
							return true;
						}
					}
				}
				else if (value == "yes")
				{
					return true;
				}
			}
			else if (num != 1647734778U)
			{
				if (num == 2872740362U)
				{
					if (value == "off")
					{
						return false;
					}
				}
			}
			else if (value == "no")
			{
				return false;
			}
			throw new ParserInputException("Cannot parse '" + value + "' to a bool.");
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021E4 File Offset: 0x000003E4
		public BoolParser()
		{
		}
	}
}
