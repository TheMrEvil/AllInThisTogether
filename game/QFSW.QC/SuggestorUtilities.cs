using System;
using QFSW.QC.Utilities;

namespace QFSW.QC
{
	// Token: 0x0200004B RID: 75
	public static class SuggestorUtilities
	{
		// Token: 0x06000195 RID: 405 RVA: 0x000081A4 File Offset: 0x000063A4
		public static bool IsCompatible(string prompt, string suggestion, SuggestorOptions options)
		{
			if (prompt.Length > suggestion.Length)
			{
				return false;
			}
			if (!options.Fuzzy)
			{
				return suggestion.StartsWith(prompt, !options.CaseSensitive, null);
			}
			if (!options.CaseSensitive)
			{
				return suggestion.ContainsCaseInsensitive(prompt);
			}
			return suggestion.Contains(prompt);
		}
	}
}
