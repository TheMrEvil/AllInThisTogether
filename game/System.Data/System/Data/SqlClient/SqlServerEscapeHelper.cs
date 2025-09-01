using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x0200023A RID: 570
	internal static class SqlServerEscapeHelper
	{
		// Token: 0x06001BC2 RID: 7106 RVA: 0x0007DA91 File Offset: 0x0007BC91
		internal static string EscapeIdentifier(string name)
		{
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0007DAB2 File Offset: 0x0007BCB2
		internal static void EscapeIdentifier(StringBuilder builder, string name)
		{
			builder.Append("[");
			builder.Append(name.Replace("]", "]]"));
			builder.Append("]");
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0007DAE3 File Offset: 0x0007BCE3
		internal static string EscapeStringAsLiteral(string input)
		{
			return input.Replace("'", "''");
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0007DAF5 File Offset: 0x0007BCF5
		internal static string MakeStringLiteral(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return "''";
			}
			return "'" + SqlServerEscapeHelper.EscapeStringAsLiteral(input) + "'";
		}
	}
}
