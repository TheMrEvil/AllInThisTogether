using System;
using System.Collections.Generic;
using System.Linq;
using Febucci.UI.Effects;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004A RID: 74
	public class AnimationParser<T> : TagParserBase where T : AnimationScriptableBase
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00007284 File Offset: 0x00005484
		public AnimationParser(char startSymbol, char closingSymbol, char endSymbol, VisibilityMode visibilityMode, Database<T> database) : base(startSymbol, closingSymbol, endSymbol)
		{
			this.visibilityMode = visibilityMode;
			this.database = database;
			this.middleSymbol = '\n';
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000072A7 File Offset: 0x000054A7
		public AnimationParser(char startSymbol, char closingSymbol, char middleSymbol, char endSymbol, VisibilityMode visibilityMode, Database<T> database) : base(startSymbol, closingSymbol, endSymbol)
		{
			this.visibilityMode = visibilityMode;
			this.database = database;
			this.middleSymbol = middleSymbol;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000072CA File Offset: 0x000054CA
		public AnimationRegion[] results
		{
			get
			{
				return this._results.Values.ToArray<AnimationRegion>();
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000072DC File Offset: 0x000054DC
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._results = new Dictionary<string, AnimationRegion>();
			if (this.database)
			{
				this.database.BuildOnce();
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007308 File Offset: 0x00005508
		public override bool TryProcessingTag(string textInsideBrackets, int tagLength, int realTextIndex, int internalOrder)
		{
			if (!this.database)
			{
				return false;
			}
			textInsideBrackets = textInsideBrackets.ToLower();
			this.database.BuildOnce();
			bool flag = textInsideBrackets[0] == this.closingSymbol;
			if (flag && tagLength == 1)
			{
				foreach (AnimationRegion animationRegion in this._results.Values)
				{
					animationRegion.CloseAllOpenedRanges(realTextIndex);
				}
				return true;
			}
			int startIndex = flag ? 1 : 0;
			string[] array = textInsideBrackets.Substring(startIndex).Split(Array.Empty<char>());
			string text = array[0];
			if (flag && array.Length > 1)
			{
				return false;
			}
			if (this.middleSymbol != '\n')
			{
				if (text[0] != this.middleSymbol)
				{
					return false;
				}
				text = text.Substring(1);
			}
			if (!this.database.ContainsKey(text))
			{
				return false;
			}
			if (flag)
			{
				if (this._results.ContainsKey(text))
				{
					this._results[text].TryClosingRange(realTextIndex);
				}
			}
			else
			{
				if (!this._results.ContainsKey(text))
				{
					this._results.Add(text, new AnimationRegion(text, this.visibilityMode, this.database[text]));
				}
				this._results[text].OpenNewRange(realTextIndex, array);
			}
			return true;
		}

		// Token: 0x04000108 RID: 264
		public Database<T> database;

		// Token: 0x04000109 RID: 265
		private VisibilityMode visibilityMode;

		// Token: 0x0400010A RID: 266
		private char middleSymbol;

		// Token: 0x0400010B RID: 267
		private const char middleSymbolDefault = '\n';

		// Token: 0x0400010C RID: 268
		private Dictionary<string, AnimationRegion> _results;
	}
}
