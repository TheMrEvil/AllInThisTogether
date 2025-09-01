using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using QFSW.QC.Suggestors.Tags;

namespace QFSW.QC.Suggestors
{
	// Token: 0x0200000A RID: 10
	public class InlineSuggestor : IQcSuggestor
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000028A1 File Offset: 0x00000AA1
		public IEnumerable<IQcSuggestion> GetSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			foreach (InlineSuggestionsTag inlineSuggestionsTag in context.GetTags<InlineSuggestionsTag>())
			{
				foreach (string value in inlineSuggestionsTag.Suggestions)
				{
					yield return new RawSuggestion(value, true);
				}
				IEnumerator<string> enumerator2 = null;
			}
			IEnumerator<InlineSuggestionsTag> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028B1 File Offset: 0x00000AB1
		public InlineSuggestor()
		{
		}

		// Token: 0x02000016 RID: 22
		[CompilerGenerated]
		private sealed class <GetSuggestions>d__0 : IEnumerable<IQcSuggestion>, IEnumerable, IEnumerator<IQcSuggestion>, IEnumerator, IDisposable
		{
			// Token: 0x06000059 RID: 89 RVA: 0x00002E60 File Offset: 0x00001060
			[DebuggerHidden]
			public <GetSuggestions>d__0(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002E7C File Offset: 0x0000107C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002ED4 File Offset: 0x000010D4
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = context.GetTags<InlineSuggestionsTag>().GetEnumerator();
						this.<>1__state = -3;
						goto IL_A9;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_8F:
					if (enumerator2.MoveNext())
					{
						string value = enumerator2.Current;
						this.<>2__current = new RawSuggestion(value, true);
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_A9:
					if (enumerator.MoveNext())
					{
						InlineSuggestionsTag inlineSuggestionsTag = enumerator.Current;
						enumerator2 = inlineSuggestionsTag.Suggestions.GetEnumerator();
						this.<>1__state = -4;
						goto IL_8F;
					}
					this.<>m__Finally1();
					enumerator = null;
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00002FC0 File Offset: 0x000011C0
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00002FDC File Offset: 0x000011DC
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600005E RID: 94 RVA: 0x00002FF9 File Offset: 0x000011F9
			IQcSuggestion IEnumerator<IQcSuggestion>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00003001 File Offset: 0x00001201
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000060 RID: 96 RVA: 0x00003008 File Offset: 0x00001208
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00003010 File Offset: 0x00001210
			[DebuggerHidden]
			IEnumerator<IQcSuggestion> IEnumerable<IQcSuggestion>.GetEnumerator()
			{
				InlineSuggestor.<GetSuggestions>d__0 <GetSuggestions>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetSuggestions>d__ = this;
				}
				else
				{
					<GetSuggestions>d__ = new InlineSuggestor.<GetSuggestions>d__0(0);
				}
				<GetSuggestions>d__.context = context;
				return <GetSuggestions>d__;
			}

			// Token: 0x06000062 RID: 98 RVA: 0x00003053 File Offset: 0x00001253
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<QFSW.QC.IQcSuggestion>.GetEnumerator();
			}

			// Token: 0x0400002D RID: 45
			private int <>1__state;

			// Token: 0x0400002E RID: 46
			private IQcSuggestion <>2__current;

			// Token: 0x0400002F RID: 47
			private int <>l__initialThreadId;

			// Token: 0x04000030 RID: 48
			private SuggestionContext context;

			// Token: 0x04000031 RID: 49
			public SuggestionContext <>3__context;

			// Token: 0x04000032 RID: 50
			private IEnumerator<InlineSuggestionsTag> <>7__wrap1;

			// Token: 0x04000033 RID: 51
			private IEnumerator<string> <>7__wrap2;
		}
	}
}
