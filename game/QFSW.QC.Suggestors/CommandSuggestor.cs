using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using QFSW.QC.Utilities;

namespace QFSW.QC.Suggestors
{
	// Token: 0x02000006 RID: 6
	public class CommandSuggestor : BasicCachedQcSuggestor<CollapsedCommand>
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000025EF File Offset: 0x000007EF
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.Depth == 0;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025FA File Offset: 0x000007FA
		protected override IQcSuggestion ItemToSuggestion(CollapsedCommand collapsedCommand)
		{
			return new CommandSuggestion(collapsedCommand.Command, collapsedCommand.NumOptionalParams);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002610 File Offset: 0x00000810
		protected override IEnumerable<CollapsedCommand> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			string incompleteCommandName = context.Prompt.SplitScopedFirst(' ').SplitFirst('<');
			IEnumerable<CommandData> commands = this.GetCommands(incompleteCommandName, options);
			if (!options.CollapseOverloads)
			{
				return from x in commands
				select new CollapsedCommand(x);
			}
			return this.CollapseCommands(commands);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002670 File Offset: 0x00000870
		public IEnumerable<CommandData> GetCommands(string incompleteCommandName, SuggestorOptions options)
		{
			if (string.IsNullOrWhiteSpace(incompleteCommandName))
			{
				return Enumerable.Empty<CommandData>();
			}
			return from command in QuantumConsoleProcessor.GetAllCommands()
			where SuggestorUtilities.IsCompatible(incompleteCommandName, command.CommandName, options)
			select command;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026BA File Offset: 0x000008BA
		protected override bool IsMatch(SuggestionContext context, IQcSuggestion suggestion, SuggestorOptions options)
		{
			return true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026BD File Offset: 0x000008BD
		private IEnumerable<CollapsedCommand> CollapseCommands(IEnumerable<CommandData> commands)
		{
			foreach (List<CommandData> list in this._commandGroups.Values)
			{
				list.Clear();
			}
			foreach (CommandData commandData in commands)
			{
				List<CommandData> list2;
				if (!this._commandGroups.TryGetValue(commandData.CommandName, out list2))
				{
					list2 = new List<CommandData>();
					this._commandGroups[commandData.CommandName] = list2;
				}
				list2.Add(commandData);
			}
			foreach (List<CommandData> list3 in this._commandGroups.Values)
			{
				list3.InsertionSortBy((CommandData x) => x.ParamCount);
				this._commandCollector.Clear();
				foreach (CommandData command in list3)
				{
					CollapsedCommand collapsedCommand = new CollapsedCommand(command);
					if (this._commandCollector.Count > 0)
					{
						CollapsedCommand collapsedCommand2 = this._commandCollector.Peek();
						CommandData command2 = collapsedCommand.Command;
						CommandData command3 = collapsedCommand2.Command;
						if (command2.ParamCount == command3.ParamCount + 1 && command2.ParameterSignature.StartsWith(command3.ParameterSignature))
						{
							this._commandCollector.Pop();
							collapsedCommand.NumOptionalParams += 1 + collapsedCommand2.NumOptionalParams;
						}
					}
					this._commandCollector.Push(collapsedCommand);
				}
				foreach (CollapsedCommand collapsedCommand3 in this._commandCollector)
				{
					yield return collapsedCommand3;
				}
				Stack<CollapsedCommand>.Enumerator enumerator5 = default(Stack<CollapsedCommand>.Enumerator);
			}
			Dictionary<string, List<CommandData>>.ValueCollection.Enumerator enumerator3 = default(Dictionary<string, List<CommandData>>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026D4 File Offset: 0x000008D4
		public CommandSuggestor()
		{
		}

		// Token: 0x0400000C RID: 12
		private readonly Dictionary<string, List<CommandData>> _commandGroups = new Dictionary<string, List<CommandData>>();

		// Token: 0x0400000D RID: 13
		private readonly Stack<CollapsedCommand> _commandCollector = new Stack<CollapsedCommand>();

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600003F RID: 63 RVA: 0x000029B8 File Offset: 0x00000BB8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000040 RID: 64 RVA: 0x000029C4 File Offset: 0x00000BC4
			public <>c()
			{
			}

			// Token: 0x06000041 RID: 65 RVA: 0x000029CC File Offset: 0x00000BCC
			internal CollapsedCommand <GetItems>b__4_0(CommandData x)
			{
				return new CollapsedCommand(x);
			}

			// Token: 0x06000042 RID: 66 RVA: 0x000029D4 File Offset: 0x00000BD4
			internal int <CollapseCommands>b__7_0(CommandData x)
			{
				return x.ParamCount;
			}

			// Token: 0x04000019 RID: 25
			public static readonly CommandSuggestor.<>c <>9 = new CommandSuggestor.<>c();

			// Token: 0x0400001A RID: 26
			public static Func<CommandData, CollapsedCommand> <>9__4_0;

			// Token: 0x0400001B RID: 27
			public static Func<CommandData, int> <>9__7_0;
		}

		// Token: 0x02000011 RID: 17
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000043 RID: 67 RVA: 0x000029DC File Offset: 0x00000BDC
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000044 RID: 68 RVA: 0x000029E4 File Offset: 0x00000BE4
			internal bool <GetCommands>b__0(CommandData command)
			{
				return SuggestorUtilities.IsCompatible(this.incompleteCommandName, command.CommandName, this.options);
			}

			// Token: 0x0400001C RID: 28
			public string incompleteCommandName;

			// Token: 0x0400001D RID: 29
			public SuggestorOptions options;
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		private sealed class <CollapseCommands>d__7 : IEnumerable<CollapsedCommand>, IEnumerable, IEnumerator<CollapsedCommand>, IEnumerator, IDisposable
		{
			// Token: 0x06000045 RID: 69 RVA: 0x000029FD File Offset: 0x00000BFD
			[DebuggerHidden]
			public <CollapseCommands>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00002A18 File Offset: 0x00000C18
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

			// Token: 0x06000047 RID: 71 RVA: 0x00002A70 File Offset: 0x00000C70
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					CommandSuggestor commandSuggestor = this;
					if (num == 0)
					{
						this.<>1__state = -1;
						foreach (List<CommandData> list in commandSuggestor._commandGroups.Values)
						{
							list.Clear();
						}
						foreach (CommandData commandData in commands)
						{
							List<CommandData> list2;
							if (!commandSuggestor._commandGroups.TryGetValue(commandData.CommandName, out list2))
							{
								list2 = new List<CommandData>();
								commandSuggestor._commandGroups[commandData.CommandName] = list2;
							}
							list2.Add(commandData);
						}
						enumerator3 = commandSuggestor._commandGroups.Values.GetEnumerator();
						this.<>1__state = -3;
						goto IL_242;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_223:
					if (enumerator5.MoveNext())
					{
						CollapsedCommand collapsedCommand = enumerator5.Current;
						this.<>2__current = collapsedCommand;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally2();
					enumerator5 = default(Stack<CollapsedCommand>.Enumerator);
					IL_242:
					if (enumerator3.MoveNext())
					{
						List<CommandData> list3 = enumerator3.Current;
						list3.InsertionSortBy(new Func<CommandData, int>(CommandSuggestor.<>c.<>9.<CollapseCommands>b__7_0));
						commandSuggestor._commandCollector.Clear();
						foreach (CommandData command in list3)
						{
							CollapsedCommand collapsedCommand2 = new CollapsedCommand(command);
							if (commandSuggestor._commandCollector.Count > 0)
							{
								CollapsedCommand collapsedCommand3 = commandSuggestor._commandCollector.Peek();
								CommandData command2 = collapsedCommand2.Command;
								CommandData command3 = collapsedCommand3.Command;
								if (command2.ParamCount == command3.ParamCount + 1 && command2.ParameterSignature.StartsWith(command3.ParameterSignature))
								{
									commandSuggestor._commandCollector.Pop();
									collapsedCommand2.NumOptionalParams += 1 + collapsedCommand3.NumOptionalParams;
								}
							}
							commandSuggestor._commandCollector.Push(collapsedCommand2);
						}
						enumerator5 = commandSuggestor._commandCollector.GetEnumerator();
						this.<>1__state = -4;
						goto IL_223;
					}
					this.<>m__Finally1();
					enumerator3 = default(Dictionary<string, List<CommandData>>.ValueCollection.Enumerator);
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00002D54 File Offset: 0x00000F54
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)enumerator3).Dispose();
			}

			// Token: 0x06000049 RID: 73 RVA: 0x00002D6E File Offset: 0x00000F6E
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				((IDisposable)enumerator5).Dispose();
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600004A RID: 74 RVA: 0x00002D89 File Offset: 0x00000F89
			CollapsedCommand IEnumerator<CollapsedCommand>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00002D91 File Offset: 0x00000F91
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D98 File Offset: 0x00000F98
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600004D RID: 77 RVA: 0x00002DA8 File Offset: 0x00000FA8
			[DebuggerHidden]
			IEnumerator<CollapsedCommand> IEnumerable<CollapsedCommand>.GetEnumerator()
			{
				CommandSuggestor.<CollapseCommands>d__7 <CollapseCommands>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<CollapseCommands>d__ = this;
				}
				else
				{
					<CollapseCommands>d__ = new CommandSuggestor.<CollapseCommands>d__7(0);
					<CollapseCommands>d__.<>4__this = this;
				}
				<CollapseCommands>d__.commands = commands;
				return <CollapseCommands>d__;
			}

			// Token: 0x0600004E RID: 78 RVA: 0x00002DF7 File Offset: 0x00000FF7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<QFSW.QC.Suggestors.CollapsedCommand>.GetEnumerator();
			}

			// Token: 0x0400001E RID: 30
			private int <>1__state;

			// Token: 0x0400001F RID: 31
			private CollapsedCommand <>2__current;

			// Token: 0x04000020 RID: 32
			private int <>l__initialThreadId;

			// Token: 0x04000021 RID: 33
			public CommandSuggestor <>4__this;

			// Token: 0x04000022 RID: 34
			private IEnumerable<CommandData> commands;

			// Token: 0x04000023 RID: 35
			public IEnumerable<CommandData> <>3__commands;

			// Token: 0x04000024 RID: 36
			private Dictionary<string, List<CommandData>>.ValueCollection.Enumerator <>7__wrap1;

			// Token: 0x04000025 RID: 37
			private Stack<CollapsedCommand>.Enumerator <>7__wrap2;
		}
	}
}
