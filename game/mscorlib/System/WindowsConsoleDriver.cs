using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x02000273 RID: 627
	internal class WindowsConsoleDriver : IConsoleDriver
	{
		// Token: 0x06001C2C RID: 7212 RVA: 0x00069524 File Offset: 0x00067724
		public WindowsConsoleDriver()
		{
			this.outputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_OUTPUT);
			this.inputHandle = WindowsConsoleDriver.GetStdHandle(Handles.STD_INPUT);
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			this.defaultAttribute = consoleScreenBufferInfo.Attribute;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00069573 File Offset: 0x00067773
		private static ConsoleColor GetForeground(short attr)
		{
			attr &= 15;
			return (ConsoleColor)attr;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0006957D File Offset: 0x0006777D
		private static ConsoleColor GetBackground(short attr)
		{
			attr &= 240;
			attr = (short)(attr >> 4);
			return (ConsoleColor)attr;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00069590 File Offset: 0x00067790
		private static short GetAttrForeground(int attr, ConsoleColor color)
		{
			attr &= -16;
			return (short)(attr | (int)color);
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x0006959C File Offset: 0x0006779C
		private static short GetAttrBackground(int attr, ConsoleColor color)
		{
			attr &= -241;
			int num = (int)((int)color << 4);
			return (short)(attr | num);
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x000695BC File Offset: 0x000677BC
		// (set) Token: 0x06001C32 RID: 7218 RVA: 0x000695EC File Offset: 0x000677EC
		public ConsoleColor BackgroundColor
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return WindowsConsoleDriver.GetBackground(consoleScreenBufferInfo.Attribute);
			}
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrBackground = WindowsConsoleDriver.GetAttrBackground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrBackground);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0006962C File Offset: 0x0006782C
		// (set) Token: 0x06001C34 RID: 7220 RVA: 0x0006965A File Offset: 0x0006785A
		public int BufferHeight
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Size.Y;
			}
			set
			{
				this.SetBufferSize(this.BufferWidth, value);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0006966C File Offset: 0x0006786C
		// (set) Token: 0x06001C36 RID: 7222 RVA: 0x0006969A File Offset: 0x0006789A
		public int BufferWidth
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Size.X;
			}
			set
			{
				this.SetBufferSize(value, this.BufferHeight);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x000696A9 File Offset: 0x000678A9
		public bool CapsLock
		{
			get
			{
				return (WindowsConsoleDriver.GetKeyState(20) & 1) == 1;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x000696B8 File Offset: 0x000678B8
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x000696E6 File Offset: 0x000678E6
		public int CursorLeft
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.CursorPosition.X;
			}
			set
			{
				this.SetCursorPosition(value, this.CursorTop);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x000696F8 File Offset: 0x000678F8
		// (set) Token: 0x06001C3B RID: 7227 RVA: 0x00069724 File Offset: 0x00067924
		public int CursorSize
		{
			get
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				return consoleCursorInfo.Size;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				consoleCursorInfo.Size = value;
				if (!WindowsConsoleDriver.SetConsoleCursorInfo(this.outputHandle, ref consoleCursorInfo))
				{
					throw new Exception("SetConsoleCursorInfo failed");
				}
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x00069780 File Offset: 0x00067980
		// (set) Token: 0x06001C3D RID: 7229 RVA: 0x000697AE File Offset: 0x000679AE
		public int CursorTop
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.CursorPosition.Y;
			}
			set
			{
				this.SetCursorPosition(this.CursorLeft, value);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x000697C0 File Offset: 0x000679C0
		// (set) Token: 0x06001C3F RID: 7231 RVA: 0x000697EC File Offset: 0x000679EC
		public bool CursorVisible
		{
			get
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				return consoleCursorInfo.Visible;
			}
			set
			{
				ConsoleCursorInfo consoleCursorInfo = default(ConsoleCursorInfo);
				WindowsConsoleDriver.GetConsoleCursorInfo(this.outputHandle, out consoleCursorInfo);
				if (consoleCursorInfo.Visible == value)
				{
					return;
				}
				consoleCursorInfo.Visible = value;
				if (!WindowsConsoleDriver.SetConsoleCursorInfo(this.outputHandle, ref consoleCursorInfo))
				{
					throw new Exception("SetConsoleCursorInfo failed");
				}
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x0006983C File Offset: 0x00067A3C
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x0006986C File Offset: 0x00067A6C
		public ConsoleColor ForegroundColor
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return WindowsConsoleDriver.GetForeground(consoleScreenBufferInfo.Attribute);
			}
			set
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				short attrForeground = WindowsConsoleDriver.GetAttrForeground((int)consoleScreenBufferInfo.Attribute, value);
				WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, attrForeground);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x000698AC File Offset: 0x00067AAC
		public bool KeyAvailable
		{
			get
			{
				InputRecord inputRecord = default(InputRecord);
				int num;
				while (WindowsConsoleDriver.PeekConsoleInput(this.inputHandle, out inputRecord, 1, out num))
				{
					if (num == 0)
					{
						return false;
					}
					if (inputRecord.EventType == 1 && inputRecord.KeyDown && !WindowsConsoleDriver.IsModifierKey(inputRecord.VirtualKeyCode))
					{
						return true;
					}
					if (!WindowsConsoleDriver.ReadConsoleInput(this.inputHandle, out inputRecord, 1, out num))
					{
						throw new InvalidOperationException("Error in ReadConsoleInput " + Marshal.GetLastWin32Error().ToString());
					}
				}
				throw new InvalidOperationException("Error in PeekConsoleInput " + Marshal.GetLastWin32Error().ToString());
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool Initialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x00069944 File Offset: 0x00067B44
		public int LargestWindowHeight
		{
			get
			{
				Coord largestConsoleWindowSize = WindowsConsoleDriver.GetLargestConsoleWindowSize(this.outputHandle);
				if (largestConsoleWindowSize.X == 0 && largestConsoleWindowSize.Y == 0)
				{
					throw new Exception("GetLargestConsoleWindowSize" + Marshal.GetLastWin32Error().ToString());
				}
				return (int)largestConsoleWindowSize.Y;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00069990 File Offset: 0x00067B90
		public int LargestWindowWidth
		{
			get
			{
				Coord largestConsoleWindowSize = WindowsConsoleDriver.GetLargestConsoleWindowSize(this.outputHandle);
				if (largestConsoleWindowSize.X == 0 && largestConsoleWindowSize.Y == 0)
				{
					throw new Exception("GetLargestConsoleWindowSize" + Marshal.GetLastWin32Error().ToString());
				}
				return (int)largestConsoleWindowSize.X;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x000699DC File Offset: 0x00067BDC
		public bool NumberLock
		{
			get
			{
				return (WindowsConsoleDriver.GetKeyState(144) & 1) == 1;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x000699F0 File Offset: 0x00067BF0
		// (set) Token: 0x06001C48 RID: 7240 RVA: 0x00069A50 File Offset: 0x00067C50
		public string Title
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				if (WindowsConsoleDriver.GetConsoleTitle(stringBuilder, 1024) == 0)
				{
					stringBuilder = new StringBuilder(26001);
					if (WindowsConsoleDriver.GetConsoleTitle(stringBuilder, 26000) == 0)
					{
						throw new Exception("Got " + Marshal.GetLastWin32Error().ToString());
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!WindowsConsoleDriver.SetConsoleTitle(value))
				{
					throw new Exception("Got " + Marshal.GetLastWin32Error().ToString());
				}
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00069A90 File Offset: 0x00067C90
		// (set) Token: 0x06001C4A RID: 7242 RVA: 0x00069AD0 File Offset: 0x00067CD0
		public bool TreatControlCAsInput
		{
			get
			{
				int num;
				if (!WindowsConsoleDriver.GetConsoleMode(this.inputHandle, out num))
				{
					throw new Exception("Failed in GetConsoleMode: " + Marshal.GetLastWin32Error().ToString());
				}
				return (num & 1) == 0;
			}
			set
			{
				int num;
				if (!WindowsConsoleDriver.GetConsoleMode(this.inputHandle, out num))
				{
					throw new Exception("Failed in GetConsoleMode: " + Marshal.GetLastWin32Error().ToString());
				}
				if ((num & 1) == 0 == value)
				{
					return;
				}
				if (value)
				{
					num &= -2;
				}
				else
				{
					num |= 1;
				}
				if (!WindowsConsoleDriver.SetConsoleMode(this.inputHandle, num))
				{
					throw new Exception("Failed in SetConsoleMode: " + Marshal.GetLastWin32Error().ToString());
				}
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00069B4C File Offset: 0x00067D4C
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x00069B88 File Offset: 0x00067D88
		public int WindowHeight
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)(consoleScreenBufferInfo.Window.Bottom - consoleScreenBufferInfo.Window.Top + 1);
			}
			set
			{
				this.SetWindowSize(this.WindowWidth, value);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x00069B98 File Offset: 0x00067D98
		// (set) Token: 0x06001C4E RID: 7246 RVA: 0x00069BC6 File Offset: 0x00067DC6
		public int WindowLeft
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Window.Left;
			}
			set
			{
				this.SetWindowPosition(value, this.WindowTop);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00069BD8 File Offset: 0x00067DD8
		// (set) Token: 0x06001C50 RID: 7248 RVA: 0x00069C06 File Offset: 0x00067E06
		public int WindowTop
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)consoleScreenBufferInfo.Window.Top;
			}
			set
			{
				this.SetWindowPosition(this.WindowLeft, value);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00069C18 File Offset: 0x00067E18
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x00069C54 File Offset: 0x00067E54
		public int WindowWidth
		{
			get
			{
				ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
				WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
				return (int)(consoleScreenBufferInfo.Window.Right - consoleScreenBufferInfo.Window.Left + 1);
			}
			set
			{
				this.SetWindowSize(value, this.WindowHeight);
			}
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00069C63 File Offset: 0x00067E63
		public void Beep(int frequency, int duration)
		{
			WindowsConsoleDriver._Beep(frequency, duration);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00069C6C File Offset: 0x00067E6C
		public void Clear()
		{
			Coord coord = default(Coord);
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			int size = (int)(consoleScreenBufferInfo.Size.X * consoleScreenBufferInfo.Size.Y);
			int num;
			WindowsConsoleDriver.FillConsoleOutputCharacter(this.outputHandle, ' ', size, coord, out num);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			WindowsConsoleDriver.FillConsoleOutputAttribute(this.outputHandle, consoleScreenBufferInfo.Attribute, size, coord, out num);
			WindowsConsoleDriver.SetConsoleCursorPosition(this.outputHandle, coord);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00069CF4 File Offset: 0x00067EF4
		public unsafe void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (sourceForeColor < ConsoleColor.Black)
			{
				throw new ArgumentException("Cannot be less than 0.", "sourceForeColor");
			}
			if (sourceBackColor < ConsoleColor.Black)
			{
				throw new ArgumentException("Cannot be less than 0.", "sourceBackColor");
			}
			if (sourceWidth == 0 || sourceHeight == 0)
			{
				return;
			}
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			CharInfo[] array = new CharInfo[sourceWidth * sourceHeight];
			Coord bsize = new Coord(sourceWidth, sourceHeight);
			Coord coord = new Coord(0, 0);
			SmallRect smallRect = new SmallRect(sourceLeft, sourceTop, sourceLeft + sourceWidth - 1, sourceTop + sourceHeight - 1);
			fixed (CharInfo* ptr = &array[0])
			{
				void* buffer = (void*)ptr;
				if (!WindowsConsoleDriver.ReadConsoleOutput(this.outputHandle, buffer, bsize, coord, ref smallRect))
				{
					throw new ArgumentException(string.Empty, "Cannot read from the specified coordinates.");
				}
			}
			short num = WindowsConsoleDriver.GetAttrForeground(0, sourceForeColor);
			num = WindowsConsoleDriver.GetAttrBackground((int)num, sourceBackColor);
			coord = new Coord(sourceLeft, sourceTop);
			int i = 0;
			while (i < sourceHeight)
			{
				int num2;
				WindowsConsoleDriver.FillConsoleOutputCharacter(this.outputHandle, sourceChar, sourceWidth, coord, out num2);
				WindowsConsoleDriver.FillConsoleOutputAttribute(this.outputHandle, num, sourceWidth, coord, out num2);
				i++;
				coord.Y += 1;
			}
			coord = new Coord(0, 0);
			smallRect = new SmallRect(targetLeft, targetTop, targetLeft + sourceWidth - 1, targetTop + sourceHeight - 1);
			if (!WindowsConsoleDriver.WriteConsoleOutput(this.outputHandle, array, bsize, coord, ref smallRect))
			{
				throw new ArgumentException(string.Empty, "Cannot write to the specified coordinates.");
			}
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Init()
		{
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00069E54 File Offset: 0x00068054
		public string ReadLine()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag;
			do
			{
				ConsoleKeyInfo consoleKeyInfo = this.ReadKey(false);
				flag = (consoleKeyInfo.KeyChar == '\n');
				if (!flag)
				{
					stringBuilder.Append(consoleKeyInfo.KeyChar);
				}
			}
			while (!flag);
			return stringBuilder.ToString();
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00069E98 File Offset: 0x00068098
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			InputRecord inputRecord = default(InputRecord);
			int num;
			while (WindowsConsoleDriver.ReadConsoleInput(this.inputHandle, out inputRecord, 1, out num))
			{
				if (inputRecord.KeyDown && inputRecord.EventType == 1 && !WindowsConsoleDriver.IsModifierKey(inputRecord.VirtualKeyCode))
				{
					bool alt = (inputRecord.ControlKeyState & 3) != 0;
					bool control = (inputRecord.ControlKeyState & 12) != 0;
					bool shift = (inputRecord.ControlKeyState & 16) != 0;
					return new ConsoleKeyInfo(inputRecord.Character, (ConsoleKey)inputRecord.VirtualKeyCode, shift, alt, control);
				}
			}
			throw new InvalidOperationException("Error in ReadConsoleInput " + Marshal.GetLastWin32Error().ToString());
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00069F37 File Offset: 0x00068137
		public void ResetColor()
		{
			WindowsConsoleDriver.SetConsoleTextAttribute(this.outputHandle, this.defaultAttribute);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00069F4C File Offset: 0x0006814C
		public void SetBufferSize(int width, int height)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			if (width - 1 > (int)consoleScreenBufferInfo.Window.Right)
			{
				throw new ArgumentOutOfRangeException("width");
			}
			if (height - 1 > (int)consoleScreenBufferInfo.Window.Bottom)
			{
				throw new ArgumentOutOfRangeException("height");
			}
			Coord newSize = new Coord(width, height);
			if (!WindowsConsoleDriver.SetConsoleScreenBufferSize(this.outputHandle, newSize))
			{
				throw new ArgumentOutOfRangeException("height/width", "Cannot be smaller than the window size.");
			}
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x00069FCC File Offset: 0x000681CC
		public void SetCursorPosition(int left, int top)
		{
			Coord coord = new Coord(left, top);
			WindowsConsoleDriver.SetConsoleCursorPosition(this.outputHandle, coord);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x00069FF0 File Offset: 0x000681F0
		public void SetWindowPosition(int left, int top)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			SmallRect window = consoleScreenBufferInfo.Window;
			window.Left = (short)left;
			window.Top = (short)top;
			if (!WindowsConsoleDriver.SetConsoleWindowInfo(this.outputHandle, true, ref window))
			{
				throw new ArgumentOutOfRangeException("left/top", "Windows error " + Marshal.GetLastWin32Error().ToString());
			}
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0006A060 File Offset: 0x00068260
		public void SetWindowSize(int width, int height)
		{
			ConsoleScreenBufferInfo consoleScreenBufferInfo = default(ConsoleScreenBufferInfo);
			WindowsConsoleDriver.GetConsoleScreenBufferInfo(this.outputHandle, out consoleScreenBufferInfo);
			SmallRect window = consoleScreenBufferInfo.Window;
			window.Right = (short)((int)window.Left + width - 1);
			window.Bottom = (short)((int)window.Top + height - 1);
			if (!WindowsConsoleDriver.SetConsoleWindowInfo(this.outputHandle, true, ref window))
			{
				throw new ArgumentOutOfRangeException("left/top", "Windows error " + Marshal.GetLastWin32Error().ToString());
			}
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0006A0E0 File Offset: 0x000682E0
		private static bool IsModifierKey(short virtualKeyCode)
		{
			return virtualKeyCode - 16 <= 2 || virtualKeyCode == 20 || virtualKeyCode - 144 <= 1;
		}

		// Token: 0x06001C5F RID: 7263
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr GetStdHandle(Handles handle);

		// Token: 0x06001C60 RID: 7264
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "Beep", SetLastError = true)]
		private static extern void _Beep(int frequency, int duration);

		// Token: 0x06001C61 RID: 7265
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleScreenBufferInfo(IntPtr handle, out ConsoleScreenBufferInfo info);

		// Token: 0x06001C62 RID: 7266
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool FillConsoleOutputCharacter(IntPtr handle, char c, int size, Coord coord, out int written);

		// Token: 0x06001C63 RID: 7267
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool FillConsoleOutputAttribute(IntPtr handle, short c, int size, Coord coord, out int written);

		// Token: 0x06001C64 RID: 7268
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleCursorPosition(IntPtr handle, Coord coord);

		// Token: 0x06001C65 RID: 7269
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleTextAttribute(IntPtr handle, short attribute);

		// Token: 0x06001C66 RID: 7270
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleScreenBufferSize(IntPtr handle, Coord newSize);

		// Token: 0x06001C67 RID: 7271
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleWindowInfo(IntPtr handle, bool absolute, ref SmallRect rect);

		// Token: 0x06001C68 RID: 7272
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetConsoleTitle(StringBuilder sb, int size);

		// Token: 0x06001C69 RID: 7273
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleTitle(string title);

		// Token: 0x06001C6A RID: 7274
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleCursorInfo(IntPtr handle, out ConsoleCursorInfo info);

		// Token: 0x06001C6B RID: 7275
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleCursorInfo(IntPtr handle, ref ConsoleCursorInfo info);

		// Token: 0x06001C6C RID: 7276
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern short GetKeyState(int virtKey);

		// Token: 0x06001C6D RID: 7277
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool GetConsoleMode(IntPtr handle, out int mode);

		// Token: 0x06001C6E RID: 7278
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleMode(IntPtr handle, int mode);

		// Token: 0x06001C6F RID: 7279
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool PeekConsoleInput(IntPtr handle, out InputRecord record, int length, out int eventsRead);

		// Token: 0x06001C70 RID: 7280
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool ReadConsoleInput(IntPtr handle, out InputRecord record, int length, out int nread);

		// Token: 0x06001C71 RID: 7281
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern Coord GetLargestConsoleWindowSize(IntPtr handle);

		// Token: 0x06001C72 RID: 7282
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private unsafe static extern bool ReadConsoleOutput(IntPtr handle, void* buffer, Coord bsize, Coord bpos, ref SmallRect region);

		// Token: 0x06001C73 RID: 7283
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool WriteConsoleOutput(IntPtr handle, CharInfo[] buffer, Coord bsize, Coord bpos, ref SmallRect region);

		// Token: 0x040019DC RID: 6620
		private IntPtr inputHandle;

		// Token: 0x040019DD RID: 6621
		private IntPtr outputHandle;

		// Token: 0x040019DE RID: 6622
		private short defaultAttribute;
	}
}
