using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using QFSW.QC.Pooling;
using QFSW.QC.Utilities;

namespace QFSW.QC
{
	// Token: 0x02000048 RID: 72
	public class SuggestionStack
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00007B79 File Offset: 0x00005D79
		public SuggestionSet TopmostSuggestionSet
		{
			get
			{
				return this._suggestionSets.LastOrDefault<SuggestionSet>();
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00007B86 File Offset: 0x00005D86
		public IQcSuggestion TopmostSuggestion
		{
			get
			{
				SuggestionSet topmostSuggestionSet = this.TopmostSuggestionSet;
				if (topmostSuggestionSet == null)
				{
					return null;
				}
				return topmostSuggestionSet.CurrentSelection;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600017F RID: 383 RVA: 0x00007B9C File Offset: 0x00005D9C
		// (remove) Token: 0x06000180 RID: 384 RVA: 0x00007BD4 File Offset: 0x00005DD4
		public event Action<SuggestionSet> OnSuggestionSetCreated
		{
			[CompilerGenerated]
			add
			{
				Action<SuggestionSet> action = this.OnSuggestionSetCreated;
				Action<SuggestionSet> action2;
				do
				{
					action2 = action;
					Action<SuggestionSet> value2 = (Action<SuggestionSet>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<SuggestionSet>>(ref this.OnSuggestionSetCreated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<SuggestionSet> action = this.OnSuggestionSetCreated;
				Action<SuggestionSet> action2;
				do
				{
					action2 = action;
					Action<SuggestionSet> value2 = (Action<SuggestionSet>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<SuggestionSet>>(ref this.OnSuggestionSetCreated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007C09 File Offset: 0x00005E09
		public SuggestionStack() : this(new QuantumSuggestor())
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007C16 File Offset: 0x00005E16
		public SuggestionStack(QuantumSuggestor suggestor)
		{
			this._suggestor = suggestor;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007C46 File Offset: 0x00005E46
		public void Clear()
		{
			while (this.PopSet())
			{
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007C50 File Offset: 0x00005E50
		public void UpdateStack(string prompt, SuggestorOptions options)
		{
			if (string.IsNullOrWhiteSpace(prompt))
			{
				this.Clear();
				return;
			}
			this.PropagateContextChanges(prompt);
			this.PopInvalidLayers();
			this.BuildInitialLayer(prompt, options);
			this.BuildNewLayers(options);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007C80 File Offset: 0x00005E80
		private SuggestionContext? GetInnerSuggestionContext(SuggestionSet set)
		{
			IQcSuggestion currentSelection = set.CurrentSelection;
			SuggestionContext context = set.Context;
			if (currentSelection == null)
			{
				return null;
			}
			return currentSelection.GetInnerSuggestionContext(context);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007CAE File Offset: 0x00005EAE
		private void InvalidateLayersFrom(int index)
		{
			this.PopSets(this._suggestionSets.Count - index);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007CC4 File Offset: 0x00005EC4
		private void PropagateContextChanges(string prompt)
		{
			if (this._suggestionSets.Count == 0)
			{
				return;
			}
			this._suggestionSets[0].Context.Prompt = prompt;
			for (int i = 0; i < this._suggestionSets.Count - 1; i++)
			{
				SuggestionSet set = this._suggestionSets[i];
				SuggestionContext? innerSuggestionContext = this.GetInnerSuggestionContext(set);
				if (innerSuggestionContext != null)
				{
					this._suggestionSets[i + 1].Context = innerSuggestionContext.Value;
				}
				else
				{
					this.InvalidateLayersFrom(i + 1);
				}
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007D54 File Offset: 0x00005F54
		private void PopInvalidLayers()
		{
			for (int i = 0; i < this._suggestionSets.Count; i++)
			{
				SuggestionSet suggestionSet = this._suggestionSets[i];
				SuggestionContext context = suggestionSet.Context;
				IQcSuggestion currentSelection = suggestionSet.CurrentSelection;
				if (currentSelection == null || !currentSelection.MatchesPrompt(context.Prompt))
				{
					this.InvalidateLayersFrom(i);
				}
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007DA8 File Offset: 0x00005FA8
		private void BuildInitialLayer(string prompt, SuggestorOptions options)
		{
			if (this._suggestionSets.Count == 0)
			{
				SuggestionContext context = new SuggestionContext
				{
					Prompt = prompt,
					Depth = 0,
					TargetType = null
				};
				this.CreateLayer(context, options);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007DF0 File Offset: 0x00005FF0
		private void BuildNewLayers(SuggestorOptions options)
		{
			if (this.TopmostSuggestion != null)
			{
				SuggestionSet topmostSuggestionSet = this.TopmostSuggestionSet;
				SuggestionContext? innerSuggestionContext = this.GetInnerSuggestionContext(topmostSuggestionSet);
				if (innerSuggestionContext != null && this.CreateLayer(innerSuggestionContext.Value, options))
				{
					this.BuildNewLayers(options);
				}
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007E34 File Offset: 0x00006034
		private void TryAutoSelectSuggestion(SuggestionSet set, string prompt)
		{
			if (set.CurrentSelection != null)
			{
				return;
			}
			IQcSuggestion qcSuggestion = set.Suggestions.FirstOrDefault<IQcSuggestion>();
			if (qcSuggestion != null && qcSuggestion.MatchesPrompt(prompt))
			{
				set.SelectionIndex = 0;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007E6C File Offset: 0x0000606C
		private bool CreateLayer(SuggestionContext context, SuggestorOptions options)
		{
			IEnumerable<IQcSuggestion> suggestions = this._suggestor.GetSuggestions(context, options);
			SuggestionSet suggestionSet = this.PushSet();
			suggestionSet.Context = context;
			suggestionSet.Suggestions.AddRange(suggestions);
			if (suggestionSet.Suggestions.Count == 0)
			{
				this.PopSet();
				return false;
			}
			Action<SuggestionSet> onSuggestionSetCreated = this.OnSuggestionSetCreated;
			if (onSuggestionSetCreated != null)
			{
				onSuggestionSetCreated(suggestionSet);
			}
			this.TryAutoSelectSuggestion(suggestionSet, context.Prompt);
			return true;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007ED8 File Offset: 0x000060D8
		public string GetCompletion()
		{
			if (this._suggestionSets.Count == 0)
			{
				return string.Empty;
			}
			IEnumerable<IQcSuggestion> enumerable = from x in this._suggestionSets
			select x.CurrentSelection into x
			where x != null
			select x;
			SuggestionContext context = this._suggestionSets[0].Context;
			this._stringBuilder.Clear();
			foreach (IQcSuggestion qcSuggestion in enumerable)
			{
				string prompt = context.Prompt;
				SuggestionContext? innerSuggestionContext = qcSuggestion.GetInnerSuggestionContext(context);
				if (innerSuggestionContext != null)
				{
					this._stringBuilder.Append(prompt, 0, prompt.Length - innerSuggestionContext.Value.Prompt.Length);
				}
				else
				{
					this._stringBuilder.Append(qcSuggestion.GetCompletion(prompt));
				}
			}
			return this._stringBuilder.ToString();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007FF8 File Offset: 0x000061F8
		public string GetCompletionTail()
		{
			this._stringBuilder.Clear();
			foreach (SuggestionSet suggestionSet in this._suggestionSets.Reversed<SuggestionSet>())
			{
				SuggestionContext context = suggestionSet.Context;
				StringBuilder stringBuilder = this._stringBuilder;
				IQcSuggestion currentSelection = suggestionSet.CurrentSelection;
				stringBuilder.Append((currentSelection != null) ? currentSelection.GetCompletionTail(context.Prompt) : null);
			}
			return this._stringBuilder.ToString();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008088 File Offset: 0x00006288
		public bool SetSuggestionIndex(int suggestionIndex)
		{
			if (this._suggestionSets.Count == 0)
			{
				return false;
			}
			if (suggestionIndex < 0 || suggestionIndex > this.TopmostSuggestionSet.Suggestions.Count)
			{
				return false;
			}
			this.TopmostSuggestionSet.SelectionIndex = suggestionIndex;
			this.TopmostSuggestionSet.Context.Prompt = this.TopmostSuggestion.PrimarySignature;
			return true;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000080E8 File Offset: 0x000062E8
		private SuggestionSet PushSet()
		{
			SuggestionSet @object = this._setPool.GetObject();
			@object.SelectionIndex = -1;
			@object.Suggestions.Clear();
			this._suggestionSets.Add(@object);
			return @object;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008120 File Offset: 0x00006320
		private bool PopSet()
		{
			if (this._suggestionSets.Count > 0)
			{
				int index = this._suggestionSets.Count - 1;
				SuggestionSet obj = this._suggestionSets[index];
				this._suggestionSets.RemoveAt(index);
				this._setPool.Release(obj);
				return true;
			}
			return false;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008174 File Offset: 0x00006374
		private bool PopSets(int count)
		{
			bool flag;
			for (flag = true; flag && count-- > 0; flag &= this.PopSet())
			{
			}
			return flag;
		}

		// Token: 0x0400011D RID: 285
		private readonly QuantumSuggestor _suggestor;

		// Token: 0x0400011E RID: 286
		private readonly List<SuggestionSet> _suggestionSets = new List<SuggestionSet>();

		// Token: 0x0400011F RID: 287
		private readonly Pool<SuggestionSet> _setPool = new Pool<SuggestionSet>();

		// Token: 0x04000120 RID: 288
		private readonly StringBuilder _stringBuilder = new StringBuilder();

		// Token: 0x04000121 RID: 289
		[CompilerGenerated]
		private Action<SuggestionSet> OnSuggestionSetCreated;

		// Token: 0x0200009E RID: 158
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000311 RID: 785 RVA: 0x0000BCE7 File Offset: 0x00009EE7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000312 RID: 786 RVA: 0x0000BCF3 File Offset: 0x00009EF3
			public <>c()
			{
			}

			// Token: 0x06000313 RID: 787 RVA: 0x0000BCFB File Offset: 0x00009EFB
			internal IQcSuggestion <GetCompletion>b__23_0(SuggestionSet x)
			{
				return x.CurrentSelection;
			}

			// Token: 0x06000314 RID: 788 RVA: 0x0000BD03 File Offset: 0x00009F03
			internal bool <GetCompletion>b__23_1(IQcSuggestion x)
			{
				return x != null;
			}

			// Token: 0x040001E8 RID: 488
			public static readonly SuggestionStack.<>c <>9 = new SuggestionStack.<>c();

			// Token: 0x040001E9 RID: 489
			public static Func<SuggestionSet, IQcSuggestion> <>9__23_0;

			// Token: 0x040001EA RID: 490
			public static Func<IQcSuggestion, bool> <>9__23_1;
		}
	}
}
