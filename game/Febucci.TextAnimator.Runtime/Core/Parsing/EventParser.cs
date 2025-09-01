using System;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000048 RID: 72
	public class EventParser : TagParserBase
	{
		// Token: 0x06000178 RID: 376 RVA: 0x000070E6 File Offset: 0x000052E6
		public EventParser(char openingBracket, char closingBracket, char closingTagSymbol) : base(openingBracket, closingBracket, closingTagSymbol)
		{
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000070F1 File Offset: 0x000052F1
		public EventMarker[] results
		{
			get
			{
				return this._results;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000070F9 File Offset: 0x000052F9
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._results = new EventMarker[0];
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007110 File Offset: 0x00005310
		public override bool TryProcessingTag(string textInsideBrackets, int tagLength, int realTextIndex, int internalOrder)
		{
			if (textInsideBrackets[0] != '?')
			{
				return false;
			}
			int num = textInsideBrackets.IndexOf('=');
			EventMarker eventMarker;
			if (num != -1)
			{
				string name = textInsideBrackets.Substring(1, num - 1);
				string text = textInsideBrackets.Substring(num + 1);
				eventMarker = new EventMarker(name, realTextIndex, internalOrder, text.Replace(" ", "").Split(',', StringSplitOptions.None));
			}
			else
			{
				eventMarker = new EventMarker(textInsideBrackets.Substring(1), realTextIndex, internalOrder, new string[0]);
			}
			Array.Resize<EventMarker>(ref this._results, this._results.Length + 1);
			this._results[this._results.Length - 1] = eventMarker;
			return true;
		}

		// Token: 0x04000102 RID: 258
		private const char eventSymbol = '?';

		// Token: 0x04000103 RID: 259
		private EventMarker[] _results;
	}
}
