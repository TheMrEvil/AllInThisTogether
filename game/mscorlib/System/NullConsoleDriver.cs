using System;

namespace System
{
	// Token: 0x02000244 RID: 580
	internal class NullConsoleDriver : IConsoleDriver
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleColor.Black;
			}
			set
			{
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public int BufferHeight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public int BufferWidth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool CapsLock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A64 RID: 6756 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public int CursorLeft
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public int CursorSize
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public int CursorTop
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public bool CursorVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleColor.Black;
			}
			set
			{
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool KeyAvailable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x000040F7 File Offset: 0x000022F7
		public bool Initialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public int LargestWindowHeight
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public int LargestWindowWidth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool NumberLock
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x00061549 File Offset: 0x0005F749
		// (set) Token: 0x06001A73 RID: 6771 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public string Title
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A75 RID: 6773 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public bool TreatControlCAsInput
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public int WindowHeight
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A79 RID: 6777 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public int WindowLeft
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A7B RID: 6779 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public int WindowTop
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001A7C RID: 6780 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		// (set) Token: 0x06001A7D RID: 6781 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public int WindowWidth
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Beep(int frequency, int duration)
		{
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Clear()
		{
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Init()
		{
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0000AF5E File Offset: 0x0000915E
		public string ReadLine()
		{
			return null;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00061550 File Offset: 0x0005F750
		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			return NullConsoleDriver.EmptyConsoleKeyInfo;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void ResetColor()
		{
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void SetBufferSize(int width, int height)
		{
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void SetCursorPosition(int left, int top)
		{
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void SetWindowPosition(int left, int top)
		{
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void SetWindowSize(int width, int height)
		{
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0000259F File Offset: 0x0000079F
		public NullConsoleDriver()
		{
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00061557 File Offset: 0x0005F757
		// Note: this type is marked as 'beforefieldinit'.
		static NullConsoleDriver()
		{
		}

		// Token: 0x04001733 RID: 5939
		private static readonly ConsoleKeyInfo EmptyConsoleKeyInfo = new ConsoleKeyInfo('\0', (ConsoleKey)0, false, false, false);
	}
}
