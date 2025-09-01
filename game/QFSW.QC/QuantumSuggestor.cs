using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using QFSW.QC.Comparators;

namespace QFSW.QC
{
	// Token: 0x02000044 RID: 68
	public class QuantumSuggestor
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000077C0 File Offset: 0x000059C0
		public QuantumSuggestor(IEnumerable<IQcSuggestor> suggestors, IEnumerable<IQcSuggestionFilter> suggestionFilters)
		{
			this._suggestors = suggestors.ToArray<IQcSuggestor>();
			this._suggestionFilters = suggestionFilters.ToArray<IQcSuggestionFilter>();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000077EB File Offset: 0x000059EB
		public QuantumSuggestor() : this(new InjectionLoader<IQcSuggestor>().GetInjectedInstances(false), new InjectionLoader<IQcSuggestionFilter>().GetInjectedInstances(false))
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000780C File Offset: 0x00005A0C
		public IEnumerable<IQcSuggestion> GetSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			this.PreprocessContext(ref context);
			IEnumerable<IQcSuggestion> collection = from x in this._suggestors.SelectMany((IQcSuggestor x) => x.GetSuggestions(context, options))
			where this.IsSuggestionPermitted(x, context)
			select x;
			this._suggestionBuffer.Clear();
			this._suggestionBuffer.AddRange(collection);
			AlphanumComparator comparer = new AlphanumComparator();
			IOrderedEnumerable<IQcSuggestion> orderedEnumerable = (from x in this._suggestionBuffer
			orderby x.PrimarySignature.Length
			select x).ThenBy((IQcSuggestion x) => x.PrimarySignature, comparer).ThenBy((IQcSuggestion x) => x.SecondarySignature.Length).ThenBy((IQcSuggestion x) => x.SecondarySignature, comparer);
			if (options.Fuzzy)
			{
				StringComparison comparisonType = options.CaseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
				orderedEnumerable = from x in orderedEnumerable
				orderby x.PrimarySignature.IndexOf(context.Prompt, comparisonType)
				select x;
			}
			return orderedEnumerable;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007970 File Offset: 0x00005B70
		private void PreprocessContext(ref SuggestionContext context)
		{
			TextProcessing.ReduceScopeOptions @default = TextProcessing.ReduceScopeOptions.Default;
			@default.ReduceIncompleteScope = true;
			context.Prompt = context.Prompt.ReduceScope(@default);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000079A0 File Offset: 0x00005BA0
		private bool IsSuggestionPermitted(IQcSuggestion suggestion, SuggestionContext context)
		{
			IQcSuggestionFilter[] suggestionFilters = this._suggestionFilters;
			for (int i = 0; i < suggestionFilters.Length; i++)
			{
				if (!suggestionFilters[i].IsSuggestionPermitted(suggestion, context))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000110 RID: 272
		private readonly IQcSuggestor[] _suggestors;

		// Token: 0x04000111 RID: 273
		private readonly IQcSuggestionFilter[] _suggestionFilters;

		// Token: 0x04000112 RID: 274
		private readonly List<IQcSuggestion> _suggestionBuffer = new List<IQcSuggestion>();

		// Token: 0x0200009A RID: 154
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060002FE RID: 766 RVA: 0x0000BB21 File Offset: 0x00009D21
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060002FF RID: 767 RVA: 0x0000BB29 File Offset: 0x00009D29
			internal IEnumerable<IQcSuggestion> <GetSuggestions>b__0(IQcSuggestor x)
			{
				return x.GetSuggestions(this.context, this.options);
			}

			// Token: 0x06000300 RID: 768 RVA: 0x0000BB3D File Offset: 0x00009D3D
			internal bool <GetSuggestions>b__1(IQcSuggestion x)
			{
				return this.<>4__this.IsSuggestionPermitted(x, this.context);
			}

			// Token: 0x040001D7 RID: 471
			public SuggestionContext context;

			// Token: 0x040001D8 RID: 472
			public SuggestorOptions options;

			// Token: 0x040001D9 RID: 473
			public QuantumSuggestor <>4__this;
		}

		// Token: 0x0200009B RID: 155
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_1
		{
			// Token: 0x06000301 RID: 769 RVA: 0x0000BB51 File Offset: 0x00009D51
			public <>c__DisplayClass5_1()
			{
			}

			// Token: 0x06000302 RID: 770 RVA: 0x0000BB59 File Offset: 0x00009D59
			internal int <GetSuggestions>b__6(IQcSuggestion x)
			{
				return x.PrimarySignature.IndexOf(this.CS$<>8__locals1.context.Prompt, this.comparisonType);
			}

			// Token: 0x040001DA RID: 474
			public StringComparison comparisonType;

			// Token: 0x040001DB RID: 475
			public QuantumSuggestor.<>c__DisplayClass5_0 CS$<>8__locals1;
		}

		// Token: 0x0200009C RID: 156
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000303 RID: 771 RVA: 0x0000BB7C File Offset: 0x00009D7C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000BB88 File Offset: 0x00009D88
			public <>c()
			{
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000BB90 File Offset: 0x00009D90
			internal int <GetSuggestions>b__5_2(IQcSuggestion x)
			{
				return x.PrimarySignature.Length;
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000BB9D File Offset: 0x00009D9D
			internal string <GetSuggestions>b__5_3(IQcSuggestion x)
			{
				return x.PrimarySignature;
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000BBA5 File Offset: 0x00009DA5
			internal int <GetSuggestions>b__5_4(IQcSuggestion x)
			{
				return x.SecondarySignature.Length;
			}

			// Token: 0x06000308 RID: 776 RVA: 0x0000BBB2 File Offset: 0x00009DB2
			internal string <GetSuggestions>b__5_5(IQcSuggestion x)
			{
				return x.SecondarySignature;
			}

			// Token: 0x040001DC RID: 476
			public static readonly QuantumSuggestor.<>c <>9 = new QuantumSuggestor.<>c();

			// Token: 0x040001DD RID: 477
			public static Func<IQcSuggestion, int> <>9__5_2;

			// Token: 0x040001DE RID: 478
			public static Func<IQcSuggestion, string> <>9__5_3;

			// Token: 0x040001DF RID: 479
			public static Func<IQcSuggestion, int> <>9__5_4;

			// Token: 0x040001E0 RID: 480
			public static Func<IQcSuggestion, string> <>9__5_5;
		}
	}
}
