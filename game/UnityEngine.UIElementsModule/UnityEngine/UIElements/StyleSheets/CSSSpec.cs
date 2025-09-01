using System;
using System.Text.RegularExpressions;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200035D RID: 861
	internal static class CSSSpec
	{
		// Token: 0x06001BE8 RID: 7144 RVA: 0x000828C4 File Offset: 0x00080AC4
		public static int GetSelectorSpecificity(string selector)
		{
			int result = 0;
			StyleSelectorPart[] parts;
			bool flag = CSSSpec.ParseSelector(selector, out parts);
			if (flag)
			{
				result = CSSSpec.GetSelectorSpecificity(parts);
			}
			return result;
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x000828F0 File Offset: 0x00080AF0
		public static int GetSelectorSpecificity(StyleSelectorPart[] parts)
		{
			int num = 1;
			for (int i = 0; i < parts.Length; i++)
			{
				switch (parts[i].type)
				{
				case StyleSelectorType.Type:
					num++;
					break;
				case StyleSelectorType.Class:
				case StyleSelectorType.PseudoClass:
					num += 10;
					break;
				case StyleSelectorType.RecursivePseudoClass:
					throw new ArgumentException("Recursive pseudo classes are not supported");
				case StyleSelectorType.ID:
					num += 100;
					break;
				}
			}
			return num;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0008296C File Offset: 0x00080B6C
		public static bool ParseSelector(string selector, out StyleSelectorPart[] parts)
		{
			MatchCollection matchCollection = CSSSpec.rgx.Matches(selector);
			int count = matchCollection.Count;
			bool flag = count < 1;
			bool result;
			if (flag)
			{
				parts = null;
				result = false;
			}
			else
			{
				parts = new StyleSelectorPart[count];
				for (int i = 0; i < count; i++)
				{
					Match match = matchCollection[i];
					StyleSelectorType type = StyleSelectorType.Unknown;
					string value = string.Empty;
					bool flag2 = !string.IsNullOrEmpty(match.Groups["wildcard"].Value);
					if (flag2)
					{
						value = "*";
						type = StyleSelectorType.Wildcard;
					}
					else
					{
						bool flag3 = !string.IsNullOrEmpty(match.Groups["id"].Value);
						if (flag3)
						{
							value = match.Groups["id"].Value.Substring(1);
							type = StyleSelectorType.ID;
						}
						else
						{
							bool flag4 = !string.IsNullOrEmpty(match.Groups["class"].Value);
							if (flag4)
							{
								value = match.Groups["class"].Value.Substring(1);
								type = StyleSelectorType.Class;
							}
							else
							{
								bool flag5 = !string.IsNullOrEmpty(match.Groups["pseudoclass"].Value);
								if (flag5)
								{
									string value2 = match.Groups["param"].Value;
									bool flag6 = !string.IsNullOrEmpty(value2);
									if (flag6)
									{
										value = value2;
										type = StyleSelectorType.RecursivePseudoClass;
									}
									else
									{
										value = match.Groups["pseudoclass"].Value.Substring(1);
										type = StyleSelectorType.PseudoClass;
									}
								}
								else
								{
									bool flag7 = !string.IsNullOrEmpty(match.Groups["type"].Value);
									if (flag7)
									{
										value = match.Groups["type"].Value;
										type = StyleSelectorType.Type;
									}
								}
							}
						}
					}
					parts[i] = new StyleSelectorPart
					{
						type = type,
						value = value
					};
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x00082B92 File Offset: 0x00080D92
		// Note: this type is marked as 'beforefieldinit'.
		static CSSSpec()
		{
		}

		// Token: 0x04000DDE RID: 3550
		private static readonly Regex rgx = new Regex("(?<id>#[-]?\\w[\\w-]*)|(?<class>\\.[\\w-]+)|(?<pseudoclass>:[\\w-]+(\\((?<param>.+)\\))?)|(?<type>([^\\-]\\w+|\\w+))|(?<wildcard>\\*)|\\s+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000DDF RID: 3551
		private const int typeSelectorWeight = 1;

		// Token: 0x04000DE0 RID: 3552
		private const int classSelectorWeight = 10;

		// Token: 0x04000DE1 RID: 3553
		private const int idSelectorWeight = 100;
	}
}
