using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200022E RID: 558
	internal static class ConsoleDriver
	{
		// Token: 0x06001958 RID: 6488 RVA: 0x0005EAF4 File Offset: 0x0005CCF4
		static ConsoleDriver()
		{
			if (!ConsoleDriver.IsConsole)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			if (Environment.IsRunningOnWindows)
			{
				ConsoleDriver.driver = ConsoleDriver.CreateWindowsConsoleDriver();
				return;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("TERM");
			if (environmentVariable == "dumb")
			{
				ConsoleDriver.is_console = false;
				ConsoleDriver.driver = ConsoleDriver.CreateNullConsoleDriver();
				return;
			}
			ConsoleDriver.driver = ConsoleDriver.CreateTermInfoDriver(environmentVariable);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0005EB59 File Offset: 0x0005CD59
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateNullConsoleDriver()
		{
			return new NullConsoleDriver();
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0005EB60 File Offset: 0x0005CD60
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateWindowsConsoleDriver()
		{
			return new WindowsConsoleDriver();
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0005EB67 File Offset: 0x0005CD67
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static IConsoleDriver CreateTermInfoDriver(string term)
		{
			return new TermInfoDriver(term);
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0005EB6F File Offset: 0x0005CD6F
		public static bool Initialized
		{
			get
			{
				return ConsoleDriver.driver.Initialized;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0005EB7B File Offset: 0x0005CD7B
		// (set) Token: 0x0600195E RID: 6494 RVA: 0x0005EB87 File Offset: 0x0005CD87
		public static ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleDriver.driver.BackgroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.BackgroundColor = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x0005EBAD File Offset: 0x0005CDAD
		// (set) Token: 0x06001960 RID: 6496 RVA: 0x0005EBB9 File Offset: 0x0005CDB9
		public static int BufferHeight
		{
			get
			{
				return ConsoleDriver.driver.BufferHeight;
			}
			set
			{
				ConsoleDriver.driver.BufferHeight = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x0005EBC6 File Offset: 0x0005CDC6
		// (set) Token: 0x06001962 RID: 6498 RVA: 0x0005EBD2 File Offset: 0x0005CDD2
		public static int BufferWidth
		{
			get
			{
				return ConsoleDriver.driver.BufferWidth;
			}
			set
			{
				ConsoleDriver.driver.BufferWidth = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0005EBDF File Offset: 0x0005CDDF
		public static bool CapsLock
		{
			get
			{
				return ConsoleDriver.driver.CapsLock;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x0005EBEB File Offset: 0x0005CDEB
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x0005EBF7 File Offset: 0x0005CDF7
		public static int CursorLeft
		{
			get
			{
				return ConsoleDriver.driver.CursorLeft;
			}
			set
			{
				ConsoleDriver.driver.CursorLeft = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x0005EC04 File Offset: 0x0005CE04
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x0005EC10 File Offset: 0x0005CE10
		public static int CursorSize
		{
			get
			{
				return ConsoleDriver.driver.CursorSize;
			}
			set
			{
				ConsoleDriver.driver.CursorSize = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0005EC1D File Offset: 0x0005CE1D
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x0005EC29 File Offset: 0x0005CE29
		public static int CursorTop
		{
			get
			{
				return ConsoleDriver.driver.CursorTop;
			}
			set
			{
				ConsoleDriver.driver.CursorTop = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x0005EC36 File Offset: 0x0005CE36
		// (set) Token: 0x0600196B RID: 6507 RVA: 0x0005EC42 File Offset: 0x0005CE42
		public static bool CursorVisible
		{
			get
			{
				return ConsoleDriver.driver.CursorVisible;
			}
			set
			{
				ConsoleDriver.driver.CursorVisible = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x0005EC4F File Offset: 0x0005CE4F
		public static bool KeyAvailable
		{
			get
			{
				return ConsoleDriver.driver.KeyAvailable;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600196D RID: 6509 RVA: 0x0005EC5B File Offset: 0x0005CE5B
		// (set) Token: 0x0600196E RID: 6510 RVA: 0x0005EC67 File Offset: 0x0005CE67
		public static ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleDriver.driver.ForegroundColor;
			}
			set
			{
				if (value < ConsoleColor.Black || value > ConsoleColor.White)
				{
					throw new ArgumentOutOfRangeException("value", "Not a ConsoleColor value.");
				}
				ConsoleDriver.driver.ForegroundColor = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x0005EC8D File Offset: 0x0005CE8D
		public static int LargestWindowHeight
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowHeight;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x0005EC99 File Offset: 0x0005CE99
		public static int LargestWindowWidth
		{
			get
			{
				return ConsoleDriver.driver.LargestWindowWidth;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x0005ECA5 File Offset: 0x0005CEA5
		public static bool NumberLock
		{
			get
			{
				return ConsoleDriver.driver.NumberLock;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0005ECB1 File Offset: 0x0005CEB1
		// (set) Token: 0x06001973 RID: 6515 RVA: 0x0005ECBD File Offset: 0x0005CEBD
		public static string Title
		{
			get
			{
				return ConsoleDriver.driver.Title;
			}
			set
			{
				ConsoleDriver.driver.Title = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0005ECCA File Offset: 0x0005CECA
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x0005ECD6 File Offset: 0x0005CED6
		public static bool TreatControlCAsInput
		{
			get
			{
				return ConsoleDriver.driver.TreatControlCAsInput;
			}
			set
			{
				ConsoleDriver.driver.TreatControlCAsInput = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x0005ECE3 File Offset: 0x0005CEE3
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x0005ECEF File Offset: 0x0005CEEF
		public static int WindowHeight
		{
			get
			{
				return ConsoleDriver.driver.WindowHeight;
			}
			set
			{
				ConsoleDriver.driver.WindowHeight = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x0005ECFC File Offset: 0x0005CEFC
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x0005ED08 File Offset: 0x0005CF08
		public static int WindowLeft
		{
			get
			{
				return ConsoleDriver.driver.WindowLeft;
			}
			set
			{
				ConsoleDriver.driver.WindowLeft = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x0005ED15 File Offset: 0x0005CF15
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x0005ED21 File Offset: 0x0005CF21
		public static int WindowTop
		{
			get
			{
				return ConsoleDriver.driver.WindowTop;
			}
			set
			{
				ConsoleDriver.driver.WindowTop = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x0005ED2E File Offset: 0x0005CF2E
		// (set) Token: 0x0600197D RID: 6525 RVA: 0x0005ED3A File Offset: 0x0005CF3A
		public static int WindowWidth
		{
			get
			{
				return ConsoleDriver.driver.WindowWidth;
			}
			set
			{
				ConsoleDriver.driver.WindowWidth = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0005ED47 File Offset: 0x0005CF47
		public static bool IsErrorRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleError);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600197F RID: 6527 RVA: 0x0005ED56 File Offset: 0x0005CF56
		public static bool IsOutputRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleOutput);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x0005ED65 File Offset: 0x0005CF65
		public static bool IsInputRedirected
		{
			get
			{
				return !ConsoleDriver.Isatty(MonoIO.ConsoleInput);
			}
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0005ED74 File Offset: 0x0005CF74
		public static void Beep(int frequency, int duration)
		{
			ConsoleDriver.driver.Beep(frequency, duration);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0005ED82 File Offset: 0x0005CF82
		public static void Clear()
		{
			ConsoleDriver.driver.Clear();
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0005ED90 File Offset: 0x0005CF90
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, ConsoleColor.Black);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0005EDB0 File Offset: 0x0005CFB0
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			ConsoleDriver.driver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0005EDD5 File Offset: 0x0005CFD5
		public static void Init()
		{
			ConsoleDriver.driver.Init();
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public static int Read()
		{
			return (int)ConsoleDriver.ReadKey(false).KeyChar;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0005EDFF File Offset: 0x0005CFFF
		public static string ReadLine()
		{
			return ConsoleDriver.driver.ReadLine();
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0005EE0B File Offset: 0x0005D00B
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.driver.ReadKey(intercept);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0005EE18 File Offset: 0x0005D018
		public static void ResetColor()
		{
			ConsoleDriver.driver.ResetColor();
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0005EE24 File Offset: 0x0005D024
		public static void SetBufferSize(int width, int height)
		{
			ConsoleDriver.driver.SetBufferSize(width, height);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0005EE32 File Offset: 0x0005D032
		public static void SetCursorPosition(int left, int top)
		{
			ConsoleDriver.driver.SetCursorPosition(left, top);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0005EE40 File Offset: 0x0005D040
		public static void SetWindowPosition(int left, int top)
		{
			ConsoleDriver.driver.SetWindowPosition(left, top);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0005EE4E File Offset: 0x0005D04E
		public static void SetWindowSize(int width, int height)
		{
			ConsoleDriver.driver.SetWindowSize(width, height);
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0005EE5C File Offset: 0x0005D05C
		public static bool IsConsole
		{
			get
			{
				if (ConsoleDriver.called_isatty)
				{
					return ConsoleDriver.is_console;
				}
				ConsoleDriver.is_console = (ConsoleDriver.Isatty(MonoIO.ConsoleOutput) && ConsoleDriver.Isatty(MonoIO.ConsoleInput));
				ConsoleDriver.called_isatty = true;
				return ConsoleDriver.is_console;
			}
		}

		// Token: 0x0600198F RID: 6543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Isatty(IntPtr handle);

		// Token: 0x06001990 RID: 6544
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalKeyAvailable(int ms_timeout);

		// Token: 0x06001991 RID: 6545
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool TtySetup(string keypadXmit, string teardown, out byte[] control_characters, out int* address);

		// Token: 0x06001992 RID: 6546
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetEcho(bool wantEcho);

		// Token: 0x06001993 RID: 6547
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SetBreak(bool wantBreak);

		// Token: 0x040016E8 RID: 5864
		internal static IConsoleDriver driver;

		// Token: 0x040016E9 RID: 5865
		private static bool is_console;

		// Token: 0x040016EA RID: 5866
		private static bool called_isatty;
	}
}
