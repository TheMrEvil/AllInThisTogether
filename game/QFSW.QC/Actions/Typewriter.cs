using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace QFSW.QC.Actions
{
	// Token: 0x02000077 RID: 119
	public class Typewriter : Composite
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		public Typewriter(string message) : this(message, Typewriter.Config.Default)
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A8B2 File Offset: 0x00008AB2
		public Typewriter(string message, Typewriter.Config config) : base(Typewriter.Generate(message, config))
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A8C1 File Offset: 0x00008AC1
		private static IEnumerator<ICommandAction> Generate(string message, Typewriter.Config config)
		{
			string[] chunks;
			switch (config.Chunks)
			{
			case Typewriter.Config.ChunkType.Character:
				chunks = (from c in message
				select c.ToString()).ToArray<string>();
				break;
			case Typewriter.Config.ChunkType.Word:
				chunks = Typewriter.WhiteRegex.Split(message);
				break;
			case Typewriter.Config.ChunkType.Line:
				chunks = Typewriter.LineRegex.Split(message);
				break;
			default:
				throw new ArgumentException(string.Format("Chunk type {0} is not supported.", config.Chunks));
			}
			int num;
			for (int i = 0; i < chunks.Length; i = num + 1)
			{
				yield return new WaitRealtime(config.PrintInterval);
				yield return new Value(chunks[i], i == 0);
				num = i;
			}
			yield break;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A8D7 File Offset: 0x00008AD7
		// Note: this type is marked as 'beforefieldinit'.
		static Typewriter()
		{
		}

		// Token: 0x0400015E RID: 350
		private static readonly Regex WhiteRegex = new Regex("(?<=[\\s+])", RegexOptions.Compiled);

		// Token: 0x0400015F RID: 351
		private static readonly Regex LineRegex = new Regex("(?<=[\\n+])", RegexOptions.Compiled);

		// Token: 0x020000BF RID: 191
		public struct Config
		{
			// Token: 0x060003A0 RID: 928 RVA: 0x0000CF88 File Offset: 0x0000B188
			// Note: this type is marked as 'beforefieldinit'.
			static Config()
			{
			}

			// Token: 0x04000264 RID: 612
			public float PrintInterval;

			// Token: 0x04000265 RID: 613
			public Typewriter.Config.ChunkType Chunks;

			// Token: 0x04000266 RID: 614
			public static readonly Typewriter.Config Default = new Typewriter.Config
			{
				PrintInterval = 0f,
				Chunks = Typewriter.Config.ChunkType.Character
			};

			// Token: 0x020000C6 RID: 198
			public enum ChunkType
			{
				// Token: 0x04000272 RID: 626
				Character,
				// Token: 0x04000273 RID: 627
				Word,
				// Token: 0x04000274 RID: 628
				Line
			}
		}

		// Token: 0x020000C0 RID: 192
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060003A1 RID: 929 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060003A2 RID: 930 RVA: 0x0000CFC3 File Offset: 0x0000B1C3
			public <>c()
			{
			}

			// Token: 0x060003A3 RID: 931 RVA: 0x0000CFCB File Offset: 0x0000B1CB
			internal string <Generate>b__5_0(char c)
			{
				return c.ToString();
			}

			// Token: 0x04000267 RID: 615
			public static readonly Typewriter.<>c <>9 = new Typewriter.<>c();

			// Token: 0x04000268 RID: 616
			public static Func<char, string> <>9__5_0;
		}

		// Token: 0x020000C1 RID: 193
		[CompilerGenerated]
		private sealed class <Generate>d__5 : IEnumerator<ICommandAction>, IEnumerator, IDisposable
		{
			// Token: 0x060003A4 RID: 932 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
			[DebuggerHidden]
			public <Generate>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x0000CFE3 File Offset: 0x0000B1E3
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060003A6 RID: 934 RVA: 0x0000CFE8 File Offset: 0x0000B1E8
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					switch (config.Chunks)
					{
					case Typewriter.Config.ChunkType.Character:
						chunks = message.Select(new Func<char, string>(Typewriter.<>c.<>9.<Generate>b__5_0)).ToArray<string>();
						break;
					case Typewriter.Config.ChunkType.Word:
						chunks = Typewriter.WhiteRegex.Split(message);
						break;
					case Typewriter.Config.ChunkType.Line:
						chunks = Typewriter.LineRegex.Split(message);
						break;
					default:
						throw new ArgumentException(string.Format("Chunk type {0} is not supported.", config.Chunks));
					}
					i = 0;
					break;
				case 1:
					this.<>1__state = -1;
					this.<>2__current = new Value(chunks[i], i == 0);
					this.<>1__state = 2;
					return true;
				case 2:
				{
					this.<>1__state = -1;
					int num = i;
					i = num + 1;
					break;
				}
				default:
					return false;
				}
				if (i >= chunks.Length)
				{
					return false;
				}
				this.<>2__current = new WaitRealtime(config.PrintInterval);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000D13F File Offset: 0x0000B33F
			ICommandAction IEnumerator<ICommandAction>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x0000D147 File Offset: 0x0000B347
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000D14E File Offset: 0x0000B34E
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000269 RID: 617
			private int <>1__state;

			// Token: 0x0400026A RID: 618
			private ICommandAction <>2__current;

			// Token: 0x0400026B RID: 619
			public Typewriter.Config config;

			// Token: 0x0400026C RID: 620
			public string message;

			// Token: 0x0400026D RID: 621
			private string[] <chunks>5__2;

			// Token: 0x0400026E RID: 622
			private int <i>5__3;
		}
	}
}
