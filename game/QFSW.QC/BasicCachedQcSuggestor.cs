using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QFSW.QC
{
	// Token: 0x0200003F RID: 63
	public abstract class BasicCachedQcSuggestor<TItem> : IQcSuggestor
	{
		// Token: 0x0600015B RID: 347
		protected abstract bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options);

		// Token: 0x0600015C RID: 348
		protected abstract IQcSuggestion ItemToSuggestion(TItem item);

		// Token: 0x0600015D RID: 349
		protected abstract IEnumerable<TItem> GetItems(SuggestionContext context, SuggestorOptions options);

		// Token: 0x0600015E RID: 350 RVA: 0x000076EC File Offset: 0x000058EC
		protected virtual bool IsMatch(SuggestionContext context, IQcSuggestion suggestion, SuggestorOptions options)
		{
			return SuggestorUtilities.IsCompatible(context.Prompt, suggestion.PrimarySignature, options);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007700 File Offset: 0x00005900
		public IEnumerable<IQcSuggestion> GetSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			if (!this.CanProvideSuggestions(context, options))
			{
				return Enumerable.Empty<IQcSuggestion>();
			}
			return from suggestion in this.GetItems(context, options).Select(new Func<TItem, IQcSuggestion>(this.ItemToSuggestionCached))
			where this.IsMatch(context, suggestion, options)
			select suggestion;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007778 File Offset: 0x00005978
		private IQcSuggestion ItemToSuggestionCached(TItem item)
		{
			IQcSuggestion result;
			if (this._suggestionCache.TryGetValue(item, out result))
			{
				return result;
			}
			return this._suggestionCache[item] = this.ItemToSuggestion(item);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000077AD File Offset: 0x000059AD
		protected BasicCachedQcSuggestor()
		{
		}

		// Token: 0x0400010F RID: 271
		private readonly Dictionary<TItem, IQcSuggestion> _suggestionCache = new Dictionary<TItem, IQcSuggestion>();

		// Token: 0x02000099 RID: 153
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060002FC RID: 764 RVA: 0x0000BAFF File Offset: 0x00009CFF
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060002FD RID: 765 RVA: 0x0000BB07 File Offset: 0x00009D07
			internal bool <GetSuggestions>b__0(IQcSuggestion suggestion)
			{
				return this.<>4__this.IsMatch(this.context, suggestion, this.options);
			}

			// Token: 0x040001D4 RID: 468
			public BasicCachedQcSuggestor<TItem> <>4__this;

			// Token: 0x040001D5 RID: 469
			public SuggestionContext context;

			// Token: 0x040001D6 RID: 470
			public SuggestorOptions options;
		}
	}
}
