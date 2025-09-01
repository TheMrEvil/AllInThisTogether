using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using QFSW.QC.Utilities;
using UnityEngine;

namespace QFSW.QC.Actions
{
	// Token: 0x0200006F RID: 111
	public class Choice<T> : Composite
	{
		// Token: 0x0600024B RID: 587 RVA: 0x0000A624 File Offset: 0x00008824
		public Choice(IEnumerable<T> choices, Action<T> onSelect) : this(choices, onSelect, Choice<T>.Config.Default)
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A633 File Offset: 0x00008833
		public Choice(IEnumerable<T> choices, Action<T> onSelect, Choice<T>.Config config) : base(Choice<T>.Generate(choices, onSelect, config))
		{
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000A643 File Offset: 0x00008843
		private static IEnumerator<ICommandAction> Generate(IEnumerable<T> choices, Action<T> onSelect, Choice<T>.Config config)
		{
			Choice<T>.<>c__DisplayClass3_0 CS$<>8__locals1 = new Choice<T>.<>c__DisplayClass3_0();
			CS$<>8__locals1.config = config;
			CS$<>8__locals1.console = null;
			CS$<>8__locals1.builder = new StringBuilder();
			CS$<>8__locals1.choiceList = ((choices as IReadOnlyList<T>) ?? choices.ToList<T>());
			CS$<>8__locals1.key = KeyCode.None;
			CS$<>8__locals1.choice = 0;
			yield return new GetContext(delegate(ActionContext ctx)
			{
				CS$<>8__locals1.console = ctx.Console;
			});
			yield return CS$<>8__locals1.<Generate>g__DrawRow|1();
			while (CS$<>8__locals1.key != KeyCode.Return)
			{
				yield return new GetKey(delegate(KeyCode k)
				{
					CS$<>8__locals1.key = k;
				});
				switch (CS$<>8__locals1.key)
				{
				case KeyCode.UpArrow:
				{
					int choice = CS$<>8__locals1.choice;
					CS$<>8__locals1.choice = choice - 1;
					break;
				}
				case KeyCode.DownArrow:
				{
					int choice = CS$<>8__locals1.choice;
					CS$<>8__locals1.choice = choice + 1;
					break;
				}
				case KeyCode.RightArrow:
				{
					int choice = CS$<>8__locals1.choice;
					CS$<>8__locals1.choice = choice + 1;
					break;
				}
				case KeyCode.LeftArrow:
				{
					int choice = CS$<>8__locals1.choice;
					CS$<>8__locals1.choice = choice - 1;
					break;
				}
				}
				CS$<>8__locals1.choice = (CS$<>8__locals1.choice + CS$<>8__locals1.choiceList.Count) % CS$<>8__locals1.choiceList.Count;
				yield return new RemoveLog();
				yield return CS$<>8__locals1.<Generate>g__DrawRow|1();
			}
			onSelect(CS$<>8__locals1.choiceList[CS$<>8__locals1.choice]);
			yield break;
		}

		// Token: 0x020000B9 RID: 185
		public struct Config
		{
			// Token: 0x0600038A RID: 906 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
			// Note: this type is marked as 'beforefieldinit'.
			static Config()
			{
			}

			// Token: 0x0400024D RID: 589
			public string ItemFormat;

			// Token: 0x0400024E RID: 590
			public string Delimiter;

			// Token: 0x0400024F RID: 591
			public Color SelectedColor;

			// Token: 0x04000250 RID: 592
			public static readonly Choice<T>.Config Default = new Choice<T>.Config
			{
				ItemFormat = "{0} [{1}]",
				Delimiter = " ",
				SelectedColor = Color.green
			};
		}

		// Token: 0x020000BA RID: 186
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x0600038B RID: 907 RVA: 0x0000CAFF File Offset: 0x0000ACFF
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x0600038C RID: 908 RVA: 0x0000CB07 File Offset: 0x0000AD07
			internal void <Generate>b__0(ActionContext ctx)
			{
				this.console = ctx.Console;
			}

			// Token: 0x0600038D RID: 909 RVA: 0x0000CB18 File Offset: 0x0000AD18
			internal ICommandAction <Generate>g__DrawRow|1()
			{
				this.builder.Clear();
				for (int i = 0; i < this.choiceList.Count; i++)
				{
					string arg = this.console.Serialize(this.choiceList[i]);
					this.builder.Append((i == this.choice) ? string.Format(this.config.ItemFormat, arg, 'x').ColorText(this.config.SelectedColor) : string.Format(this.config.ItemFormat, arg, ' '));
					if (i != this.choiceList.Count - 1)
					{
						this.builder.Append(this.config.Delimiter);
					}
				}
				return new Value(this.builder.ToString(), true);
			}

			// Token: 0x0600038E RID: 910 RVA: 0x0000CBF9 File Offset: 0x0000ADF9
			internal void <Generate>b__2(KeyCode k)
			{
				this.key = k;
			}

			// Token: 0x04000251 RID: 593
			public QuantumConsole console;

			// Token: 0x04000252 RID: 594
			public StringBuilder builder;

			// Token: 0x04000253 RID: 595
			public IReadOnlyList<T> choiceList;

			// Token: 0x04000254 RID: 596
			public int choice;

			// Token: 0x04000255 RID: 597
			public Choice<T>.Config config;

			// Token: 0x04000256 RID: 598
			public KeyCode key;
		}

		// Token: 0x020000BB RID: 187
		[CompilerGenerated]
		private sealed class <Generate>d__3 : IEnumerator<ICommandAction>, IEnumerator, IDisposable
		{
			// Token: 0x0600038F RID: 911 RVA: 0x0000CC02 File Offset: 0x0000AE02
			[DebuggerHidden]
			public <Generate>d__3(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000390 RID: 912 RVA: 0x0000CC11 File Offset: 0x0000AE11
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000391 RID: 913 RVA: 0x0000CC14 File Offset: 0x0000AE14
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					CS$<>8__locals1 = new Choice<T>.<>c__DisplayClass3_0();
					CS$<>8__locals1.config = config;
					CS$<>8__locals1.console = null;
					CS$<>8__locals1.builder = new StringBuilder();
					CS$<>8__locals1.choiceList = ((choices as IReadOnlyList<T>) ?? choices.ToList<T>());
					CS$<>8__locals1.key = KeyCode.None;
					CS$<>8__locals1.choice = 0;
					this.<>2__current = new GetContext(new Action<ActionContext>(CS$<>8__locals1.<Generate>b__0));
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					this.<>2__current = CS$<>8__locals1.<Generate>g__DrawRow|1();
					this.<>1__state = 2;
					return true;
				case 2:
					this.<>1__state = -1;
					break;
				case 3:
					this.<>1__state = -1;
					switch (CS$<>8__locals1.key)
					{
					case KeyCode.UpArrow:
					{
						int choice = CS$<>8__locals1.choice;
						CS$<>8__locals1.choice = choice - 1;
						break;
					}
					case KeyCode.DownArrow:
					{
						int choice = CS$<>8__locals1.choice;
						CS$<>8__locals1.choice = choice + 1;
						break;
					}
					case KeyCode.RightArrow:
					{
						int choice = CS$<>8__locals1.choice;
						CS$<>8__locals1.choice = choice + 1;
						break;
					}
					case KeyCode.LeftArrow:
					{
						int choice = CS$<>8__locals1.choice;
						CS$<>8__locals1.choice = choice - 1;
						break;
					}
					}
					CS$<>8__locals1.choice = (CS$<>8__locals1.choice + CS$<>8__locals1.choiceList.Count) % CS$<>8__locals1.choiceList.Count;
					this.<>2__current = new RemoveLog();
					this.<>1__state = 4;
					return true;
				case 4:
					this.<>1__state = -1;
					this.<>2__current = CS$<>8__locals1.<Generate>g__DrawRow|1();
					this.<>1__state = 5;
					return true;
				case 5:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				if (CS$<>8__locals1.key == KeyCode.Return)
				{
					onSelect(CS$<>8__locals1.choiceList[CS$<>8__locals1.choice]);
					return false;
				}
				this.<>2__current = new GetKey(new Action<KeyCode>(CS$<>8__locals1.<Generate>b__2));
				this.<>1__state = 3;
				return true;
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000392 RID: 914 RVA: 0x0000CE87 File Offset: 0x0000B087
			ICommandAction IEnumerator<ICommandAction>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000393 RID: 915 RVA: 0x0000CE8F File Offset: 0x0000B08F
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000394 RID: 916 RVA: 0x0000CE96 File Offset: 0x0000B096
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000257 RID: 599
			private int <>1__state;

			// Token: 0x04000258 RID: 600
			private ICommandAction <>2__current;

			// Token: 0x04000259 RID: 601
			public Choice<T>.Config config;

			// Token: 0x0400025A RID: 602
			public IEnumerable<T> choices;

			// Token: 0x0400025B RID: 603
			private Choice<T>.<>c__DisplayClass3_0 <>8__1;

			// Token: 0x0400025C RID: 604
			public Action<T> onSelect;
		}
	}
}
