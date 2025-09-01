using System;

namespace System
{
	// Token: 0x02000238 RID: 568
	internal interface IConsoleDriver
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060019CB RID: 6603
		// (set) Token: 0x060019CC RID: 6604
		ConsoleColor BackgroundColor { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060019CD RID: 6605
		// (set) Token: 0x060019CE RID: 6606
		int BufferHeight { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060019CF RID: 6607
		// (set) Token: 0x060019D0 RID: 6608
		int BufferWidth { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060019D1 RID: 6609
		bool CapsLock { get; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060019D2 RID: 6610
		// (set) Token: 0x060019D3 RID: 6611
		int CursorLeft { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060019D4 RID: 6612
		// (set) Token: 0x060019D5 RID: 6613
		int CursorSize { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060019D6 RID: 6614
		// (set) Token: 0x060019D7 RID: 6615
		int CursorTop { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060019D8 RID: 6616
		// (set) Token: 0x060019D9 RID: 6617
		bool CursorVisible { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060019DA RID: 6618
		// (set) Token: 0x060019DB RID: 6619
		ConsoleColor ForegroundColor { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060019DC RID: 6620
		bool KeyAvailable { get; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060019DD RID: 6621
		bool Initialized { get; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060019DE RID: 6622
		int LargestWindowHeight { get; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060019DF RID: 6623
		int LargestWindowWidth { get; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060019E0 RID: 6624
		bool NumberLock { get; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060019E1 RID: 6625
		// (set) Token: 0x060019E2 RID: 6626
		string Title { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060019E3 RID: 6627
		// (set) Token: 0x060019E4 RID: 6628
		bool TreatControlCAsInput { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060019E5 RID: 6629
		// (set) Token: 0x060019E6 RID: 6630
		int WindowHeight { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060019E7 RID: 6631
		// (set) Token: 0x060019E8 RID: 6632
		int WindowLeft { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060019E9 RID: 6633
		// (set) Token: 0x060019EA RID: 6634
		int WindowTop { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060019EB RID: 6635
		// (set) Token: 0x060019EC RID: 6636
		int WindowWidth { get; set; }

		// Token: 0x060019ED RID: 6637
		void Init();

		// Token: 0x060019EE RID: 6638
		void Beep(int frequency, int duration);

		// Token: 0x060019EF RID: 6639
		void Clear();

		// Token: 0x060019F0 RID: 6640
		void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor);

		// Token: 0x060019F1 RID: 6641
		ConsoleKeyInfo ReadKey(bool intercept);

		// Token: 0x060019F2 RID: 6642
		void ResetColor();

		// Token: 0x060019F3 RID: 6643
		void SetBufferSize(int width, int height);

		// Token: 0x060019F4 RID: 6644
		void SetCursorPosition(int left, int top);

		// Token: 0x060019F5 RID: 6645
		void SetWindowPosition(int left, int top);

		// Token: 0x060019F6 RID: 6646
		void SetWindowSize(int width, int height);

		// Token: 0x060019F7 RID: 6647
		string ReadLine();
	}
}
