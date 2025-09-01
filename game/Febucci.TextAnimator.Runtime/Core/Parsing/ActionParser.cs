using System;
using Febucci.UI.Actions;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000046 RID: 70
	public sealed class ActionParser : TagParserBase
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006FCF File Offset: 0x000051CF
		public ActionMarker[] results
		{
			get
			{
				return this._results;
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006FD7 File Offset: 0x000051D7
		public ActionParser(char startSymbol, char closingSymbol, char endSymbol, ActionDatabase actionDatabase) : base(startSymbol, closingSymbol, endSymbol)
		{
			this.database = actionDatabase;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006FEA File Offset: 0x000051EA
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._results = new ActionMarker[0];
			if (this.database)
			{
				this.database.BuildOnce();
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007018 File Offset: 0x00005218
		public override bool TryProcessingTag(string textInsideBrackets, int tagLength, int realTextIndex, int internalOrder)
		{
			if (!this.database)
			{
				return false;
			}
			this.database.BuildOnce();
			int num = textInsideBrackets.IndexOf('=');
			string text = (num == -1) ? textInsideBrackets : textInsideBrackets.Substring(0, num);
			text = text.ToLower();
			if (!this.database.ContainsKey(text))
			{
				return false;
			}
			ActionMarker actionMarker;
			if (num != -1)
			{
				string text2 = textInsideBrackets.Substring(num + 1);
				actionMarker = new ActionMarker(text, realTextIndex, internalOrder, text2.Replace(" ", "").Split(',', StringSplitOptions.None));
			}
			else
			{
				actionMarker = new ActionMarker(text, realTextIndex, internalOrder, new string[0]);
			}
			Array.Resize<ActionMarker>(ref this._results, this._results.Length + 1);
			this._results[this._results.Length - 1] = actionMarker;
			return true;
		}

		// Token: 0x04000100 RID: 256
		public ActionDatabase database;

		// Token: 0x04000101 RID: 257
		private ActionMarker[] _results;
	}
}
