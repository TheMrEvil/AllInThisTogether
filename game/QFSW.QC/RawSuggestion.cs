using System;

namespace QFSW.QC
{
	// Token: 0x02000045 RID: 69
	public class RawSuggestion : IQcSuggestion
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000079D1 File Offset: 0x00005BD1
		public string FullSignature
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000079D9 File Offset: 0x00005BD9
		public string PrimarySignature
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000079E1 File Offset: 0x00005BE1
		public string SecondarySignature
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000079E8 File Offset: 0x00005BE8
		public RawSuggestion(string value, bool singleLiteral = false)
		{
			this._value = value;
			this._singleLiteral = singleLiteral;
			this._completion = this._value;
			if (this._completion.CanSplitScoped(' ', '"', '"'))
			{
				this._completion = "\"" + this._completion + "\"";
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007A43 File Offset: 0x00005C43
		public bool MatchesPrompt(string prompt)
		{
			if (this._singleLiteral)
			{
				prompt = prompt.Trim('"');
			}
			return prompt == this._value;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007A63 File Offset: 0x00005C63
		public string GetCompletion(string prompt)
		{
			return this._completion;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007A6B File Offset: 0x00005C6B
		public string GetCompletionTail(string prompt)
		{
			return string.Empty;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007A74 File Offset: 0x00005C74
		public SuggestionContext? GetInnerSuggestionContext(SuggestionContext context)
		{
			return null;
		}

		// Token: 0x04000113 RID: 275
		private readonly string _value;

		// Token: 0x04000114 RID: 276
		private readonly bool _singleLiteral;

		// Token: 0x04000115 RID: 277
		private readonly string _completion;
	}
}
