using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x0200024E RID: 590
	internal class TermInfoDriver : IConsoleDriver
	{
		// Token: 0x06001B3A RID: 6970 RVA: 0x00064D44 File Offset: 0x00062F44
		private static string TryTermInfoDir(string dir, string term)
		{
			string text = string.Format("{0}/{1:x}/{2}", dir, (int)term[0], term);
			if (File.Exists(text))
			{
				return text;
			}
			text = Path.Combine(dir, term.Substring(0, 1), term);
			if (File.Exists(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00064D90 File Offset: 0x00062F90
		private static string SearchTerminfo(string term)
		{
			if (term == null || term == string.Empty)
			{
				return null;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("TERMINFO");
			if (environmentVariable != null && Directory.Exists(environmentVariable))
			{
				string text = TermInfoDriver.TryTermInfoDir(environmentVariable, term);
				if (text != null)
				{
					return text;
				}
			}
			foreach (string text2 in TermInfoDriver.locations)
			{
				if (Directory.Exists(text2))
				{
					string text = TermInfoDriver.TryTermInfoDir(text2, term);
					if (text != null)
					{
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00064E03 File Offset: 0x00063003
		private void WriteConsole(string str)
		{
			if (str == null)
			{
				return;
			}
			this.stdout.InternalWriteString(str);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00064E15 File Offset: 0x00063015
		public TermInfoDriver() : this(Environment.GetEnvironmentVariable("TERM"))
		{
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x00064E28 File Offset: 0x00063028
		public TermInfoDriver(string term)
		{
			this.term = term;
			string text = TermInfoDriver.SearchTerminfo(term);
			if (text != null)
			{
				this.reader = new TermInfoReader(term, text);
			}
			else if (term == "xterm")
			{
				this.reader = new TermInfoReader(term, KnownTerminals.xterm);
			}
			else if (term == "linux")
			{
				this.reader = new TermInfoReader(term, KnownTerminals.linux);
			}
			if (this.reader == null)
			{
				this.reader = new TermInfoReader(term, KnownTerminals.ansi);
			}
			if (!(Console.stdout is CStreamWriter))
			{
				this.stdout = new CStreamWriter(Console.OpenStandardOutput(0), Console.OutputEncoding, false);
				this.stdout.AutoFlush = true;
				return;
			}
			this.stdout = (CStreamWriter)Console.stdout;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00064F30 File Offset: 0x00063130
		public bool Initialized
		{
			get
			{
				return this.inited;
			}
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00064F38 File Offset: 0x00063138
		public void Init()
		{
			if (this.inited)
			{
				return;
			}
			object obj = this.initLock;
			lock (obj)
			{
				if (!this.inited)
				{
					try
					{
						if (!ConsoleDriver.IsConsole)
						{
							throw new IOException("Not a tty.");
						}
						ConsoleDriver.SetEcho(false);
						string text = null;
						this.keypadXmit = this.reader.Get(TermInfoStrings.KeypadXmit);
						this.keypadLocal = this.reader.Get(TermInfoStrings.KeypadLocal);
						if (this.keypadXmit != null)
						{
							this.WriteConsole(this.keypadXmit);
							if (this.keypadLocal != null)
							{
								text += this.keypadLocal;
							}
						}
						this.origPair = this.reader.Get(TermInfoStrings.OrigPair);
						this.origColors = this.reader.Get(TermInfoStrings.OrigColors);
						this.setfgcolor = this.reader.Get(TermInfoStrings.SetAForeground);
						this.setbgcolor = this.reader.Get(TermInfoStrings.SetABackground);
						this.maxColors = this.reader.Get(TermInfoNumbers.MaxColors);
						this.maxColors = Math.Max(Math.Min(this.maxColors, 16), 1);
						string text2 = (this.origColors == null) ? this.origPair : this.origColors;
						if (text2 != null)
						{
							text += text2;
						}
						if (!ConsoleDriver.TtySetup(this.keypadXmit, text, out this.control_characters, out TermInfoDriver.native_terminal_size))
						{
							this.control_characters = new byte[17];
							TermInfoDriver.native_terminal_size = null;
						}
						this.stdin = new StreamReader(Console.OpenStandardInput(0), Console.InputEncoding);
						this.clear = this.reader.Get(TermInfoStrings.ClearScreen);
						this.bell = this.reader.Get(TermInfoStrings.Bell);
						if (this.clear == null)
						{
							this.clear = this.reader.Get(TermInfoStrings.CursorHome);
							this.clear += this.reader.Get(TermInfoStrings.ClrEos);
						}
						this.csrVisible = this.reader.Get(TermInfoStrings.CursorNormal);
						if (this.csrVisible == null)
						{
							this.csrVisible = this.reader.Get(TermInfoStrings.CursorVisible);
						}
						this.csrInvisible = this.reader.Get(TermInfoStrings.CursorInvisible);
						if (this.term == "cygwin" || this.term == "linux" || (this.term != null && this.term.StartsWith("xterm")) || this.term == "rxvt" || this.term == "dtterm")
						{
							this.titleFormat = "\u001b]0;{0}\a";
						}
						else if (this.term == "iris-ansi")
						{
							this.titleFormat = "\u001bP1.y{0}\u001b\\";
						}
						else if (this.term == "sun-cmd")
						{
							this.titleFormat = "\u001b]l{0}\u001b\\";
						}
						this.cursorAddress = this.reader.Get(TermInfoStrings.CursorAddress);
						this.GetCursorPosition();
						if (this.noGetPosition)
						{
							this.WriteConsole(this.clear);
							this.cursorLeft = 0;
							this.cursorTop = 0;
						}
					}
					finally
					{
						this.inited = true;
					}
				}
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0006528C File Offset: 0x0006348C
		private void IncrementX()
		{
			this.cursorLeft++;
			if (this.cursorLeft >= this.WindowWidth)
			{
				this.cursorTop++;
				this.cursorLeft = 0;
				if (this.cursorTop >= this.WindowHeight)
				{
					if (this.rl_starty != -1)
					{
						this.rl_starty--;
					}
					this.cursorTop--;
				}
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00065300 File Offset: 0x00063500
		public void WriteSpecialKey(ConsoleKeyInfo key)
		{
			switch (key.Key)
			{
			case ConsoleKey.Backspace:
				if (this.cursorLeft > 0 && (this.cursorLeft > this.rl_startx || this.cursorTop != this.rl_starty))
				{
					this.cursorLeft--;
					this.SetCursorPosition(this.cursorLeft, this.cursorTop);
					this.WriteConsole(" ");
					this.SetCursorPosition(this.cursorLeft, this.cursorTop);
					return;
				}
				break;
			case ConsoleKey.Tab:
			{
				int num = 8 - this.cursorLeft % 8;
				for (int i = 0; i < num; i++)
				{
					this.IncrementX();
				}
				this.WriteConsole("\t");
				return;
			}
			case (ConsoleKey)10:
			case (ConsoleKey)11:
			case ConsoleKey.Enter:
				break;
			case ConsoleKey.Clear:
				this.WriteConsole(this.clear);
				this.cursorLeft = 0;
				this.cursorTop = 0;
				break;
			default:
				return;
			}
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000653E2 File Offset: 0x000635E2
		public void WriteSpecialKey(char c)
		{
			this.WriteSpecialKey(this.CreateKeyInfoFromInt((int)c, false));
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000653F4 File Offset: 0x000635F4
		public bool IsSpecialKey(ConsoleKeyInfo key)
		{
			if (!this.inited)
			{
				return false;
			}
			switch (key.Key)
			{
			case ConsoleKey.Backspace:
				return true;
			case ConsoleKey.Tab:
				return true;
			case ConsoleKey.Clear:
				return true;
			case ConsoleKey.Enter:
				this.cursorLeft = 0;
				this.cursorTop++;
				if (this.cursorTop >= this.WindowHeight)
				{
					this.cursorTop--;
				}
				return false;
			}
			this.IncrementX();
			return false;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00065475 File Offset: 0x00063675
		public bool IsSpecialKey(char c)
		{
			return this.IsSpecialKey(this.CreateKeyInfoFromInt((int)c, false));
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x00065488 File Offset: 0x00063688
		private void ChangeColor(string format, ConsoleColor color)
		{
			if (string.IsNullOrEmpty(format))
			{
				return;
			}
			if ((color & (ConsoleColor)(-16)) != ConsoleColor.Black)
			{
				throw new ArgumentException("Invalid Console Color");
			}
			int value = TermInfoDriver._consoleColorToAnsiCode[(int)color] % this.maxColors;
			this.WriteConsole(ParameterizedStrings.Evaluate(format, new ParameterizedStrings.FormatParam[]
			{
				value
			}));
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x000654DF File Offset: 0x000636DF
		// (set) Token: 0x06001B48 RID: 6984 RVA: 0x000654F5 File Offset: 0x000636F5
		public ConsoleColor BackgroundColor
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.bgcolor;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.ChangeColor(this.setbgcolor, value);
				this.bgcolor = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00065519 File Offset: 0x00063719
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x0006552F File Offset: 0x0006372F
		public ConsoleColor ForegroundColor
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.fgcolor;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.ChangeColor(this.setfgcolor, value);
				this.fgcolor = value;
			}
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00065554 File Offset: 0x00063754
		private void GetCursorPosition()
		{
			int num = 0;
			int num2 = 0;
			int num3 = ConsoleDriver.InternalKeyAvailable(0);
			int num4;
			while (num3-- > 0)
			{
				num4 = this.stdin.Read();
				this.AddToBuffer(num4);
			}
			this.WriteConsole("\u001b[6n");
			if (ConsoleDriver.InternalKeyAvailable(1000) <= 0)
			{
				this.noGetPosition = true;
				return;
			}
			for (num4 = this.stdin.Read(); num4 != 27; num4 = this.stdin.Read())
			{
				this.AddToBuffer(num4);
				if (ConsoleDriver.InternalKeyAvailable(100) <= 0)
				{
					return;
				}
			}
			num4 = this.stdin.Read();
			if (num4 != 91)
			{
				this.AddToBuffer(27);
				this.AddToBuffer(num4);
				return;
			}
			num4 = this.stdin.Read();
			if (num4 != 59)
			{
				num = num4 - 48;
				num4 = this.stdin.Read();
				while (num4 >= 48 && num4 <= 57)
				{
					num = num * 10 + num4 - 48;
					num4 = this.stdin.Read();
				}
				num--;
			}
			num4 = this.stdin.Read();
			if (num4 != 82)
			{
				num2 = num4 - 48;
				num4 = this.stdin.Read();
				while (num4 >= 48 && num4 <= 57)
				{
					num2 = num2 * 10 + num4 - 48;
					num4 = this.stdin.Read();
				}
				num2--;
			}
			this.cursorLeft = num2;
			this.cursorTop = num;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x00065699 File Offset: 0x00063899
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x000656B5 File Offset: 0x000638B5
		public int BufferHeight
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.bufferHeight;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x000656CA File Offset: 0x000638CA
		// (set) Token: 0x06001B4F RID: 6991 RVA: 0x000656B5 File Offset: 0x000638B5
		public int BufferWidth
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.bufferWidth;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000656E6 File Offset: 0x000638E6
		public bool CapsLock
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return false;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x000656F7 File Offset: 0x000638F7
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0006570D File Offset: 0x0006390D
		public int CursorLeft
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorLeft;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetCursorPosition(value, this.CursorTop);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x0006572A File Offset: 0x0006392A
		// (set) Token: 0x06001B54 RID: 6996 RVA: 0x00065740 File Offset: 0x00063940
		public int CursorTop
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorTop;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.SetCursorPosition(this.CursorLeft, value);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0006575D File Offset: 0x0006395D
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x00065773 File Offset: 0x00063973
		public bool CursorVisible
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.cursorVisible;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.cursorVisible = value;
				this.WriteConsole(value ? this.csrVisible : this.csrInvisible);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x000657A1 File Offset: 0x000639A1
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x000657B2 File Offset: 0x000639B2
		[MonoTODO]
		public int CursorSize
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 1;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x000657C2 File Offset: 0x000639C2
		public bool KeyAvailable
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.writepos > this.readpos || ConsoleDriver.InternalKeyAvailable(0) > 0;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x000657EB File Offset: 0x000639EB
		public int LargestWindowHeight
		{
			get
			{
				return this.WindowHeight;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x000657F3 File Offset: 0x000639F3
		public int LargestWindowWidth
		{
			get
			{
				return this.WindowWidth;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000656E6 File Offset: 0x000638E6
		public bool NumberLock
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return false;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x000657FB File Offset: 0x000639FB
		// (set) Token: 0x06001B5E RID: 7006 RVA: 0x00065811 File Offset: 0x00063A11
		public string Title
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.title;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.title = value;
				this.WriteConsole(string.Format(this.titleFormat, value));
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0006583A File Offset: 0x00063A3A
		// (set) Token: 0x06001B60 RID: 7008 RVA: 0x00065850 File Offset: 0x00063A50
		public bool TreatControlCAsInput
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return this.controlCAsInput;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				if (this.controlCAsInput == value)
				{
					return;
				}
				ConsoleDriver.SetBreak(value);
				this.controlCAsInput = value;
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00065878 File Offset: 0x00063A78
		private unsafe void CheckWindowDimensions()
		{
			if (TermInfoDriver.native_terminal_size == null || TermInfoDriver.terminal_size == *TermInfoDriver.native_terminal_size)
			{
				return;
			}
			if (*TermInfoDriver.native_terminal_size == -1)
			{
				int num = this.reader.Get(TermInfoNumbers.Columns);
				if (num != 0)
				{
					this.windowWidth = num;
				}
				num = this.reader.Get(TermInfoNumbers.Lines);
				if (num != 0)
				{
					this.windowHeight = num;
				}
			}
			else
			{
				TermInfoDriver.terminal_size = *TermInfoDriver.native_terminal_size;
				this.windowWidth = TermInfoDriver.terminal_size >> 16;
				this.windowHeight = (TermInfoDriver.terminal_size & 65535);
			}
			this.bufferHeight = this.windowHeight;
			this.bufferWidth = this.windowWidth;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x00065917 File Offset: 0x00063B17
		// (set) Token: 0x06001B63 RID: 7011 RVA: 0x000656B5 File Offset: 0x000638B5
		public int WindowHeight
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.windowHeight;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x000656E6 File Offset: 0x000638E6
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x000656B5 File Offset: 0x000638B5
		public int WindowLeft
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 0;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x000656E6 File Offset: 0x000638E6
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x000656B5 File Offset: 0x000638B5
		public int WindowTop
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				return 0;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x00065933 File Offset: 0x00063B33
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x000656B5 File Offset: 0x000638B5
		public int WindowWidth
		{
			get
			{
				if (!this.inited)
				{
					this.Init();
				}
				this.CheckWindowDimensions();
				return this.windowWidth;
			}
			set
			{
				if (!this.inited)
				{
					this.Init();
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0006594F File Offset: 0x00063B4F
		public void Clear()
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.WriteConsole(this.clear);
			this.cursorLeft = 0;
			this.cursorTop = 0;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00065979 File Offset: 0x00063B79
		public void Beep(int frequency, int duration)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.WriteConsole(this.bell);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x00065995 File Offset: 0x00063B95
		public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (!this.inited)
			{
				this.Init();
			}
			throw new NotImplementedException();
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x000659AC File Offset: 0x00063BAC
		private void AddToBuffer(int b)
		{
			if (this.buffer == null)
			{
				this.buffer = new char[1024];
			}
			else if (this.writepos >= this.buffer.Length)
			{
				char[] dst = new char[this.buffer.Length * 2];
				Buffer.BlockCopy(this.buffer, 0, dst, 0, this.buffer.Length);
				this.buffer = dst;
			}
			char[] array = this.buffer;
			int num = this.writepos;
			this.writepos = num + 1;
			array[num] = (ushort)b;
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00065A2C File Offset: 0x00063C2C
		private void AdjustBuffer()
		{
			if (this.readpos >= this.writepos)
			{
				this.readpos = (this.writepos = 0);
			}
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00065A58 File Offset: 0x00063C58
		private ConsoleKeyInfo CreateKeyInfoFromInt(int n, bool alt)
		{
			char keyChar = (char)n;
			ConsoleKey key = (ConsoleKey)n;
			bool shift = false;
			bool control = false;
			if (n <= 19)
			{
				switch (n)
				{
				case 8:
				case 9:
				case 12:
				case 13:
					goto IL_C7;
				case 10:
					key = ConsoleKey.Enter;
					goto IL_C7;
				case 11:
					break;
				default:
					if (n == 19)
					{
						goto IL_C7;
					}
					break;
				}
			}
			else
			{
				if (n == 27)
				{
					key = ConsoleKey.Escape;
					goto IL_C7;
				}
				if (n == 32)
				{
					key = ConsoleKey.Spacebar;
					goto IL_C7;
				}
				switch (n)
				{
				case 42:
					key = ConsoleKey.Multiply;
					goto IL_C7;
				case 43:
					key = ConsoleKey.Add;
					goto IL_C7;
				case 45:
					key = ConsoleKey.Subtract;
					goto IL_C7;
				case 47:
					key = ConsoleKey.Divide;
					goto IL_C7;
				}
			}
			if (n >= 1 && n <= 26)
			{
				control = true;
				key = ConsoleKey.A + n - 1;
			}
			else if (n >= 97 && n <= 122)
			{
				key = (ConsoleKey)(-32) + n;
			}
			else if (n >= 65 && n <= 90)
			{
				shift = true;
			}
			else if (n < 48 || n > 57)
			{
				key = (ConsoleKey)0;
			}
			IL_C7:
			return new ConsoleKeyInfo(keyChar, key, shift, alt, control);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00065B38 File Offset: 0x00063D38
		private object GetKeyFromBuffer(bool cooked)
		{
			if (this.readpos >= this.writepos)
			{
				return null;
			}
			int num = (int)this.buffer[this.readpos];
			if (!cooked || !this.rootmap.StartsWith(num))
			{
				this.readpos++;
				this.AdjustBuffer();
				return this.CreateKeyInfoFromInt(num, false);
			}
			int num2;
			TermInfoStrings termInfoStrings = this.rootmap.Match(this.buffer, this.readpos, this.writepos - this.readpos, out num2);
			if (termInfoStrings == (TermInfoStrings)(-1))
			{
				if (this.buffer[this.readpos] != '\u001b' || this.writepos - this.readpos < 2)
				{
					return null;
				}
				this.readpos += 2;
				this.AdjustBuffer();
				if (this.buffer[this.readpos + 1] == '\u007f')
				{
					return new ConsoleKeyInfo('\b', ConsoleKey.Backspace, false, true, false);
				}
				return this.CreateKeyInfoFromInt((int)this.buffer[this.readpos + 1], true);
			}
			else
			{
				if (this.keymap[termInfoStrings] != null)
				{
					ConsoleKeyInfo consoleKeyInfo = (ConsoleKeyInfo)this.keymap[termInfoStrings];
					this.readpos += num2;
					this.AdjustBuffer();
					return consoleKeyInfo;
				}
				this.readpos++;
				this.AdjustBuffer();
				return this.CreateKeyInfoFromInt(num, false);
			}
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00065CA0 File Offset: 0x00063EA0
		private ConsoleKeyInfo ReadKeyInternal(out bool fresh)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.InitKeys();
			object keyFromBuffer;
			if ((keyFromBuffer = this.GetKeyFromBuffer(true)) == null)
			{
				do
				{
					if (ConsoleDriver.InternalKeyAvailable(150) > 0)
					{
						do
						{
							this.AddToBuffer(this.stdin.Read());
						}
						while (ConsoleDriver.InternalKeyAvailable(0) > 0);
					}
					else if (this.stdin.DataAvailable())
					{
						do
						{
							this.AddToBuffer(this.stdin.Read());
						}
						while (this.stdin.DataAvailable());
					}
					else
					{
						if ((keyFromBuffer = this.GetKeyFromBuffer(false)) != null)
						{
							break;
						}
						this.AddToBuffer(this.stdin.Read());
					}
					keyFromBuffer = this.GetKeyFromBuffer(true);
				}
				while (keyFromBuffer == null);
				fresh = true;
			}
			else
			{
				fresh = false;
			}
			return (ConsoleKeyInfo)keyFromBuffer;
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00065D5A File Offset: 0x00063F5A
		private bool InputPending()
		{
			return this.readpos < this.writepos || this.stdin.DataAvailable();
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00065D78 File Offset: 0x00063F78
		private void QueueEcho(char c)
		{
			if (this.echobuf == null)
			{
				this.echobuf = new char[1024];
			}
			char[] array = this.echobuf;
			int num = this.echon;
			this.echon = num + 1;
			array[num] = c;
			if (this.echon == this.echobuf.Length || !this.InputPending())
			{
				this.stdout.InternalWriteChars(this.echobuf, this.echon);
				this.echon = 0;
			}
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00065DEC File Offset: 0x00063FEC
		private void Echo(ConsoleKeyInfo key)
		{
			if (!this.IsSpecialKey(key))
			{
				this.QueueEcho(key.KeyChar);
				return;
			}
			this.EchoFlush();
			this.WriteSpecialKey(key);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00065E12 File Offset: 0x00064012
		private void EchoFlush()
		{
			if (this.echon == 0)
			{
				return;
			}
			this.stdout.InternalWriteChars(this.echobuf, this.echon);
			this.echon = 0;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00065E3C File Offset: 0x0006403C
		public int Read([In] [Out] char[] dest, int index, int count)
		{
			bool flag = false;
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			object keyFromBuffer;
			while ((keyFromBuffer = this.GetKeyFromBuffer(true)) != null)
			{
				ConsoleKeyInfo key = (ConsoleKeyInfo)keyFromBuffer;
				char keyChar = key.KeyChar;
				if (key.Key != ConsoleKey.Backspace)
				{
					if (key.Key == ConsoleKey.Enter)
					{
						num = stringBuilder.Length;
					}
					stringBuilder.Append(keyChar);
				}
				else if (stringBuilder.Length > num)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int length = stringBuilder2.Length;
					stringBuilder2.Length = length - 1;
				}
			}
			this.rl_startx = this.cursorLeft;
			this.rl_starty = this.cursorTop;
			for (;;)
			{
				bool flag2;
				ConsoleKeyInfo key = this.ReadKeyInternal(out flag2);
				flag = (flag || flag2);
				char keyChar = key.KeyChar;
				if (key.Key != ConsoleKey.Backspace)
				{
					if (key.Key == ConsoleKey.Enter)
					{
						num = stringBuilder.Length;
					}
					stringBuilder.Append(keyChar);
					goto IL_E0;
				}
				if (stringBuilder.Length > num)
				{
					StringBuilder stringBuilder3 = stringBuilder;
					int length = stringBuilder3.Length;
					stringBuilder3.Length = length - 1;
					goto IL_E0;
				}
				IL_EA:
				if (key.Key == ConsoleKey.Enter)
				{
					break;
				}
				continue;
				IL_E0:
				if (flag)
				{
					this.Echo(key);
					goto IL_EA;
				}
				goto IL_EA;
			}
			this.EchoFlush();
			this.rl_startx = -1;
			this.rl_starty = -1;
			int num2 = 0;
			while (count > 0 && num2 < stringBuilder.Length)
			{
				dest[index + num2] = stringBuilder[num2];
				num2++;
				count--;
			}
			for (int i = num2; i < stringBuilder.Length; i++)
			{
				this.AddToBuffer((int)stringBuilder[i]);
			}
			return num2;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00065FA4 File Offset: 0x000641A4
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			bool flag;
			ConsoleKeyInfo consoleKeyInfo = this.ReadKeyInternal(out flag);
			if (!intercept && flag)
			{
				this.Echo(consoleKeyInfo);
				this.EchoFlush();
			}
			return consoleKeyInfo;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00065FD0 File Offset: 0x000641D0
		public string ReadLine()
		{
			return this.ReadUntilConditionInternal(true);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00065FD9 File Offset: 0x000641D9
		public string ReadToEnd()
		{
			return this.ReadUntilConditionInternal(false);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00065FE4 File Offset: 0x000641E4
		private string ReadUntilConditionInternal(bool haltOnNewLine)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.GetCursorPosition();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			this.rl_startx = this.cursorLeft;
			this.rl_starty = this.cursorTop;
			char c = (char)this.control_characters[4];
			for (;;)
			{
				bool flag2;
				ConsoleKeyInfo key = this.ReadKeyInternal(out flag2);
				flag = (flag || flag2);
				char keyChar = key.KeyChar;
				if (keyChar == c && keyChar != '\0' && stringBuilder.Length == 0)
				{
					break;
				}
				bool flag3 = haltOnNewLine && key.Key == ConsoleKey.Enter;
				if (flag3)
				{
					goto IL_AC;
				}
				if (key.Key != ConsoleKey.Backspace)
				{
					stringBuilder.Append(keyChar);
					goto IL_AC;
				}
				if (stringBuilder.Length > 0)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int length = stringBuilder2.Length;
					stringBuilder2.Length = length - 1;
					goto IL_AC;
				}
				IL_B6:
				if (flag3)
				{
					goto Block_10;
				}
				continue;
				IL_AC:
				if (flag)
				{
					this.Echo(key);
					goto IL_B6;
				}
				goto IL_B6;
			}
			return null;
			Block_10:
			this.EchoFlush();
			this.rl_startx = -1;
			this.rl_starty = -1;
			return stringBuilder.ToString();
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000660C4 File Offset: 0x000642C4
		public void ResetColor()
		{
			if (!this.inited)
			{
				this.Init();
			}
			string str = (this.origPair != null) ? this.origPair : this.origColors;
			this.WriteConsole(str);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000660FD File Offset: 0x000642FD
		public void SetBufferSize(int width, int height)
		{
			if (!this.inited)
			{
				this.Init();
			}
			throw new NotImplementedException(string.Empty);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00066118 File Offset: 0x00064318
		public void SetCursorPosition(int left, int top)
		{
			if (!this.inited)
			{
				this.Init();
			}
			this.CheckWindowDimensions();
			if (left < 0 || left >= this.bufferWidth)
			{
				throw new ArgumentOutOfRangeException("left", "Value must be positive and below the buffer width.");
			}
			if (top < 0 || top >= this.bufferHeight)
			{
				throw new ArgumentOutOfRangeException("top", "Value must be positive and below the buffer height.");
			}
			if (this.cursorAddress == null)
			{
				throw new NotSupportedException("This terminal does not suport setting the cursor position.");
			}
			this.WriteConsole(ParameterizedStrings.Evaluate(this.cursorAddress, new ParameterizedStrings.FormatParam[]
			{
				top,
				left
			}));
			this.cursorLeft = left;
			this.cursorTop = top;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000657B2 File Offset: 0x000639B2
		public void SetWindowPosition(int left, int top)
		{
			if (!this.inited)
			{
				this.Init();
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x000657B2 File Offset: 0x000639B2
		public void SetWindowSize(int width, int height)
		{
			if (!this.inited)
			{
				this.Init();
			}
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x000661C8 File Offset: 0x000643C8
		private void CreateKeyMap()
		{
			this.keymap = new Hashtable();
			this.keymap[TermInfoStrings.KeyBackspace] = new ConsoleKeyInfo('\0', ConsoleKey.Backspace, false, false, false);
			this.keymap[TermInfoStrings.KeyClear] = new ConsoleKeyInfo('\0', ConsoleKey.Clear, false, false, false);
			this.keymap[TermInfoStrings.KeyDown] = new ConsoleKeyInfo('\0', ConsoleKey.DownArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyF1] = new ConsoleKeyInfo('\0', ConsoleKey.F1, false, false, false);
			this.keymap[TermInfoStrings.KeyF10] = new ConsoleKeyInfo('\0', ConsoleKey.F10, false, false, false);
			this.keymap[TermInfoStrings.KeyF2] = new ConsoleKeyInfo('\0', ConsoleKey.F2, false, false, false);
			this.keymap[TermInfoStrings.KeyF3] = new ConsoleKeyInfo('\0', ConsoleKey.F3, false, false, false);
			this.keymap[TermInfoStrings.KeyF4] = new ConsoleKeyInfo('\0', ConsoleKey.F4, false, false, false);
			this.keymap[TermInfoStrings.KeyF5] = new ConsoleKeyInfo('\0', ConsoleKey.F5, false, false, false);
			this.keymap[TermInfoStrings.KeyF6] = new ConsoleKeyInfo('\0', ConsoleKey.F6, false, false, false);
			this.keymap[TermInfoStrings.KeyF7] = new ConsoleKeyInfo('\0', ConsoleKey.F7, false, false, false);
			this.keymap[TermInfoStrings.KeyF8] = new ConsoleKeyInfo('\0', ConsoleKey.F8, false, false, false);
			this.keymap[TermInfoStrings.KeyF9] = new ConsoleKeyInfo('\0', ConsoleKey.F9, false, false, false);
			this.keymap[TermInfoStrings.KeyHome] = new ConsoleKeyInfo('\0', ConsoleKey.Home, false, false, false);
			this.keymap[TermInfoStrings.KeyLeft] = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyLl] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad1, false, false, false);
			this.keymap[TermInfoStrings.KeyNpage] = new ConsoleKeyInfo('\0', ConsoleKey.PageDown, false, false, false);
			this.keymap[TermInfoStrings.KeyPpage] = new ConsoleKeyInfo('\0', ConsoleKey.PageUp, false, false, false);
			this.keymap[TermInfoStrings.KeyRight] = new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, false, false, false);
			this.keymap[TermInfoStrings.KeySf] = new ConsoleKeyInfo('\0', ConsoleKey.PageDown, false, false, false);
			this.keymap[TermInfoStrings.KeySr] = new ConsoleKeyInfo('\0', ConsoleKey.PageUp, false, false, false);
			this.keymap[TermInfoStrings.KeyUp] = new ConsoleKeyInfo('\0', ConsoleKey.UpArrow, false, false, false);
			this.keymap[TermInfoStrings.KeyA1] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad7, false, false, false);
			this.keymap[TermInfoStrings.KeyA3] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad9, false, false, false);
			this.keymap[TermInfoStrings.KeyB2] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad5, false, false, false);
			this.keymap[TermInfoStrings.KeyC1] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad1, false, false, false);
			this.keymap[TermInfoStrings.KeyC3] = new ConsoleKeyInfo('\0', ConsoleKey.NumPad3, false, false, false);
			this.keymap[TermInfoStrings.KeyBtab] = new ConsoleKeyInfo('\0', ConsoleKey.Tab, true, false, false);
			this.keymap[TermInfoStrings.KeyBeg] = new ConsoleKeyInfo('\0', ConsoleKey.Home, false, false, false);
			this.keymap[TermInfoStrings.KeyCopy] = new ConsoleKeyInfo('C', ConsoleKey.C, false, true, false);
			this.keymap[TermInfoStrings.KeyEnd] = new ConsoleKeyInfo('\0', ConsoleKey.End, false, false, false);
			this.keymap[TermInfoStrings.KeyEnter] = new ConsoleKeyInfo('\n', ConsoleKey.Enter, false, false, false);
			this.keymap[TermInfoStrings.KeyHelp] = new ConsoleKeyInfo('\0', ConsoleKey.Help, false, false, false);
			this.keymap[TermInfoStrings.KeyPrint] = new ConsoleKeyInfo('\0', ConsoleKey.Print, false, false, false);
			this.keymap[TermInfoStrings.KeyUndo] = new ConsoleKeyInfo('Z', ConsoleKey.Z, false, true, false);
			this.keymap[TermInfoStrings.KeySbeg] = new ConsoleKeyInfo('\0', ConsoleKey.Home, true, false, false);
			this.keymap[TermInfoStrings.KeyScopy] = new ConsoleKeyInfo('C', ConsoleKey.C, true, true, false);
			this.keymap[TermInfoStrings.KeySdc] = new ConsoleKeyInfo('\t', ConsoleKey.Delete, true, false, false);
			this.keymap[TermInfoStrings.KeyShelp] = new ConsoleKeyInfo('\0', ConsoleKey.Help, true, false, false);
			this.keymap[TermInfoStrings.KeyShome] = new ConsoleKeyInfo('\0', ConsoleKey.Home, true, false, false);
			this.keymap[TermInfoStrings.KeySleft] = new ConsoleKeyInfo('\0', ConsoleKey.LeftArrow, true, false, false);
			this.keymap[TermInfoStrings.KeySprint] = new ConsoleKeyInfo('\0', ConsoleKey.Print, true, false, false);
			this.keymap[TermInfoStrings.KeySright] = new ConsoleKeyInfo('\0', ConsoleKey.RightArrow, true, false, false);
			this.keymap[TermInfoStrings.KeySundo] = new ConsoleKeyInfo('Z', ConsoleKey.Z, true, false, false);
			this.keymap[TermInfoStrings.KeyF11] = new ConsoleKeyInfo('\0', ConsoleKey.F11, false, false, false);
			this.keymap[TermInfoStrings.KeyF12] = new ConsoleKeyInfo('\0', ConsoleKey.F12, false, false, false);
			this.keymap[TermInfoStrings.KeyF13] = new ConsoleKeyInfo('\0', ConsoleKey.F13, false, false, false);
			this.keymap[TermInfoStrings.KeyF14] = new ConsoleKeyInfo('\0', ConsoleKey.F14, false, false, false);
			this.keymap[TermInfoStrings.KeyF15] = new ConsoleKeyInfo('\0', ConsoleKey.F15, false, false, false);
			this.keymap[TermInfoStrings.KeyF16] = new ConsoleKeyInfo('\0', ConsoleKey.F16, false, false, false);
			this.keymap[TermInfoStrings.KeyF17] = new ConsoleKeyInfo('\0', ConsoleKey.F17, false, false, false);
			this.keymap[TermInfoStrings.KeyF18] = new ConsoleKeyInfo('\0', ConsoleKey.F18, false, false, false);
			this.keymap[TermInfoStrings.KeyF19] = new ConsoleKeyInfo('\0', ConsoleKey.F19, false, false, false);
			this.keymap[TermInfoStrings.KeyF20] = new ConsoleKeyInfo('\0', ConsoleKey.F20, false, false, false);
			this.keymap[TermInfoStrings.KeyF21] = new ConsoleKeyInfo('\0', ConsoleKey.F21, false, false, false);
			this.keymap[TermInfoStrings.KeyF22] = new ConsoleKeyInfo('\0', ConsoleKey.F22, false, false, false);
			this.keymap[TermInfoStrings.KeyF23] = new ConsoleKeyInfo('\0', ConsoleKey.F23, false, false, false);
			this.keymap[TermInfoStrings.KeyF24] = new ConsoleKeyInfo('\0', ConsoleKey.F24, false, false, false);
			this.keymap[TermInfoStrings.KeyDc] = new ConsoleKeyInfo('\0', ConsoleKey.Delete, false, false, false);
			this.keymap[TermInfoStrings.KeyIc] = new ConsoleKeyInfo('\0', ConsoleKey.Insert, false, false, false);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00066A64 File Offset: 0x00064C64
		private void InitKeys()
		{
			if (this.initKeys)
			{
				return;
			}
			this.CreateKeyMap();
			this.rootmap = new ByteMatcher();
			foreach (TermInfoStrings s in new TermInfoStrings[]
			{
				TermInfoStrings.KeyBackspace,
				TermInfoStrings.KeyClear,
				TermInfoStrings.KeyDown,
				TermInfoStrings.KeyF1,
				TermInfoStrings.KeyF10,
				TermInfoStrings.KeyF2,
				TermInfoStrings.KeyF3,
				TermInfoStrings.KeyF4,
				TermInfoStrings.KeyF5,
				TermInfoStrings.KeyF6,
				TermInfoStrings.KeyF7,
				TermInfoStrings.KeyF8,
				TermInfoStrings.KeyF9,
				TermInfoStrings.KeyHome,
				TermInfoStrings.KeyLeft,
				TermInfoStrings.KeyLl,
				TermInfoStrings.KeyNpage,
				TermInfoStrings.KeyPpage,
				TermInfoStrings.KeyRight,
				TermInfoStrings.KeySf,
				TermInfoStrings.KeySr,
				TermInfoStrings.KeyUp,
				TermInfoStrings.KeyA1,
				TermInfoStrings.KeyA3,
				TermInfoStrings.KeyB2,
				TermInfoStrings.KeyC1,
				TermInfoStrings.KeyC3,
				TermInfoStrings.KeyBtab,
				TermInfoStrings.KeyBeg,
				TermInfoStrings.KeyCopy,
				TermInfoStrings.KeyEnd,
				TermInfoStrings.KeyEnter,
				TermInfoStrings.KeyHelp,
				TermInfoStrings.KeyPrint,
				TermInfoStrings.KeyUndo,
				TermInfoStrings.KeySbeg,
				TermInfoStrings.KeyScopy,
				TermInfoStrings.KeySdc,
				TermInfoStrings.KeyShelp,
				TermInfoStrings.KeyShome,
				TermInfoStrings.KeySleft,
				TermInfoStrings.KeySprint,
				TermInfoStrings.KeySright,
				TermInfoStrings.KeySundo,
				TermInfoStrings.KeyF11,
				TermInfoStrings.KeyF12,
				TermInfoStrings.KeyF13,
				TermInfoStrings.KeyF14,
				TermInfoStrings.KeyF15,
				TermInfoStrings.KeyF16,
				TermInfoStrings.KeyF17,
				TermInfoStrings.KeyF18,
				TermInfoStrings.KeyF19,
				TermInfoStrings.KeyF20,
				TermInfoStrings.KeyF21,
				TermInfoStrings.KeyF22,
				TermInfoStrings.KeyF23,
				TermInfoStrings.KeyF24,
				TermInfoStrings.KeyDc,
				TermInfoStrings.KeyIc
			})
			{
				this.AddStringMapping(s);
			}
			this.rootmap.AddMapping(TermInfoStrings.KeyBackspace, new byte[]
			{
				this.control_characters[2]
			});
			this.rootmap.Sort();
			this.initKeys = true;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00066AE8 File Offset: 0x00064CE8
		private void AddStringMapping(TermInfoStrings s)
		{
			byte[] stringBytes = this.reader.GetStringBytes(s);
			if (stringBytes == null)
			{
				return;
			}
			this.rootmap.AddMapping(s, stringBytes);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00066B14 File Offset: 0x00064D14
		// Note: this type is marked as 'beforefieldinit'.
		static TermInfoDriver()
		{
		}

		// Token: 0x040017A2 RID: 6050
		private unsafe static int* native_terminal_size;

		// Token: 0x040017A3 RID: 6051
		private static int terminal_size;

		// Token: 0x040017A4 RID: 6052
		private static readonly string[] locations = new string[]
		{
			"/usr/share/terminfo",
			"/etc/terminfo",
			"/usr/lib/terminfo",
			"/lib/terminfo"
		};

		// Token: 0x040017A5 RID: 6053
		private TermInfoReader reader;

		// Token: 0x040017A6 RID: 6054
		private int cursorLeft;

		// Token: 0x040017A7 RID: 6055
		private int cursorTop;

		// Token: 0x040017A8 RID: 6056
		private string title = string.Empty;

		// Token: 0x040017A9 RID: 6057
		private string titleFormat = string.Empty;

		// Token: 0x040017AA RID: 6058
		private bool cursorVisible = true;

		// Token: 0x040017AB RID: 6059
		private string csrVisible;

		// Token: 0x040017AC RID: 6060
		private string csrInvisible;

		// Token: 0x040017AD RID: 6061
		private string clear;

		// Token: 0x040017AE RID: 6062
		private string bell;

		// Token: 0x040017AF RID: 6063
		private string term;

		// Token: 0x040017B0 RID: 6064
		private StreamReader stdin;

		// Token: 0x040017B1 RID: 6065
		private CStreamWriter stdout;

		// Token: 0x040017B2 RID: 6066
		private int windowWidth;

		// Token: 0x040017B3 RID: 6067
		private int windowHeight;

		// Token: 0x040017B4 RID: 6068
		private int bufferHeight;

		// Token: 0x040017B5 RID: 6069
		private int bufferWidth;

		// Token: 0x040017B6 RID: 6070
		private char[] buffer;

		// Token: 0x040017B7 RID: 6071
		private int readpos;

		// Token: 0x040017B8 RID: 6072
		private int writepos;

		// Token: 0x040017B9 RID: 6073
		private string keypadXmit;

		// Token: 0x040017BA RID: 6074
		private string keypadLocal;

		// Token: 0x040017BB RID: 6075
		private bool controlCAsInput;

		// Token: 0x040017BC RID: 6076
		private bool inited;

		// Token: 0x040017BD RID: 6077
		private object initLock = new object();

		// Token: 0x040017BE RID: 6078
		private bool initKeys;

		// Token: 0x040017BF RID: 6079
		private string origPair;

		// Token: 0x040017C0 RID: 6080
		private string origColors;

		// Token: 0x040017C1 RID: 6081
		private string cursorAddress;

		// Token: 0x040017C2 RID: 6082
		private ConsoleColor fgcolor = ConsoleColor.White;

		// Token: 0x040017C3 RID: 6083
		private ConsoleColor bgcolor;

		// Token: 0x040017C4 RID: 6084
		private string setfgcolor;

		// Token: 0x040017C5 RID: 6085
		private string setbgcolor;

		// Token: 0x040017C6 RID: 6086
		private int maxColors;

		// Token: 0x040017C7 RID: 6087
		private bool noGetPosition;

		// Token: 0x040017C8 RID: 6088
		private Hashtable keymap;

		// Token: 0x040017C9 RID: 6089
		private ByteMatcher rootmap;

		// Token: 0x040017CA RID: 6090
		private int rl_startx = -1;

		// Token: 0x040017CB RID: 6091
		private int rl_starty = -1;

		// Token: 0x040017CC RID: 6092
		private byte[] control_characters;

		// Token: 0x040017CD RID: 6093
		private static readonly int[] _consoleColorToAnsiCode = new int[]
		{
			0,
			4,
			2,
			6,
			1,
			5,
			3,
			7,
			8,
			12,
			10,
			14,
			9,
			13,
			11,
			15
		};

		// Token: 0x040017CE RID: 6094
		private char[] echobuf;

		// Token: 0x040017CF RID: 6095
		private int echon;
	}
}
