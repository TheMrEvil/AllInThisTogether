using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using QFSW.QC.Suggestors.Tags;

namespace QFSW.QC.Suggestors
{
	// Token: 0x02000003 RID: 3
	public class CommandNameSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000209C File Offset: 0x0000029C
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.HasTag<CommandNameTag>() && !string.IsNullOrWhiteSpace(context.Prompt);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020B7 File Offset: 0x000002B7
		protected override IQcSuggestion ItemToSuggestion(string commandName)
		{
			return new RawSuggestion(commandName, false);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C0 File Offset: 0x000002C0
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			context.Prompt.SplitScopedFirst(' ').SplitFirst('<');
			return from command in QuantumConsoleProcessor.GetUniqueCommands()
			select command.CommandName;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002100 File Offset: 0x00000300
		public CommandNameSuggestor()
		{
		}

		// Token: 0x0200000D RID: 13
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000037 RID: 55 RVA: 0x0000296A File Offset: 0x00000B6A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002976 File Offset: 0x00000B76
			public <>c()
			{
			}

			// Token: 0x06000039 RID: 57 RVA: 0x0000297E File Offset: 0x00000B7E
			internal string <GetItems>b__2_0(CommandData command)
			{
				return command.CommandName;
			}

			// Token: 0x0400000F RID: 15
			public static readonly CommandNameSuggestor.<>c <>9 = new CommandNameSuggestor.<>c();

			// Token: 0x04000010 RID: 16
			public static Func<CommandData, string> <>9__2_0;
		}
	}
}
