using System;
using System.Globalization;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000003 RID: 3
	public class TMPTagParser : TagParserBase
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000025A7 File Offset: 0x000007A7
		public override bool shouldPasteTag
		{
			get
			{
				return this.richTagsEnabled;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000025AF File Offset: 0x000007AF
		public TMPTagParser(bool richTagsEnabled, char openingBracket, char closingBracket, char closingTagSymbol) : base(openingBracket, closingBracket, closingTagSymbol)
		{
			this.richTagsEnabled = richTagsEnabled;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025C4 File Offset: 0x000007C4
		public override bool TryProcessingTag(string textInsideBrackets, int tagLength, int realTextIndex, int internalOrder)
		{
			if (!this.richTagsEnabled)
			{
				return false;
			}
			string text = this.startSymbol.ToString() + textInsideBrackets + this.endSymbol.ToString();
			foreach (string value in TMPTagParser.lookup)
			{
				if (text.StartsWith(value, true, CultureInfo.InvariantCulture))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002624 File Offset: 0x00000824
		// Note: this type is marked as 'beforefieldinit'.
		static TMPTagParser()
		{
		}

		// Token: 0x0400000A RID: 10
		private readonly bool richTagsEnabled;

		// Token: 0x0400000B RID: 11
		private static readonly string[] lookup = new string[]
		{
			"<align=",
			"</align>",
			"<allcaps>",
			"</allcaps>",
			"<alpha=",
			"</alpha>",
			"<b>",
			"</b>",
			"<color=",
			"</color>",
			"<cspace=",
			"</cspace>",
			"<font=",
			"</font>",
			"<font-weight=",
			"</font-weight>",
			"<gradient=",
			"</gradient>",
			"<i>",
			"</i>",
			"<indent=",
			"</indent>",
			"<line-height=",
			"</line-height>",
			"<line-indent=",
			"</line-indent>",
			"<link=",
			"</link>",
			"<lowercase>",
			"</lowercase>",
			"<margin=",
			"</margin>",
			"<margin-left>",
			"<margin-right>",
			"<mark=",
			"</mark>",
			"<mspace=",
			"</mspace>",
			"<nobr>",
			"</nobr>",
			"<page>",
			"<pos=",
			"<rotate=",
			"</rotate>",
			"<s>",
			"</s>",
			"<size=",
			"<smallcaps>",
			"</smallcaps>",
			"<space=",
			"<sprite",
			"<sprite ",
			"<style=",
			"</style>",
			"<sub>",
			"</sub>",
			"<sup>",
			"</sup>",
			"<u>",
			"</u>",
			"<uppercase>",
			"</uppercase>",
			"<voffset=",
			"</voffset>",
			"<width=",
			"</width>"
		};
	}
}
