using System;

namespace System
{
	/// <summary>Specifies the standard keys on a console.</summary>
	// Token: 0x020001BF RID: 447
	public enum ConsoleKey
	{
		/// <summary>The BACKSPACE key.</summary>
		// Token: 0x040013A9 RID: 5033
		Backspace = 8,
		/// <summary>The TAB key.</summary>
		// Token: 0x040013AA RID: 5034
		Tab,
		/// <summary>The CLEAR key.</summary>
		// Token: 0x040013AB RID: 5035
		Clear = 12,
		/// <summary>The ENTER key.</summary>
		// Token: 0x040013AC RID: 5036
		Enter,
		/// <summary>The PAUSE key.</summary>
		// Token: 0x040013AD RID: 5037
		Pause = 19,
		/// <summary>The ESC (ESCAPE) key.</summary>
		// Token: 0x040013AE RID: 5038
		Escape = 27,
		/// <summary>The SPACEBAR key.</summary>
		// Token: 0x040013AF RID: 5039
		Spacebar = 32,
		/// <summary>The PAGE UP key.</summary>
		// Token: 0x040013B0 RID: 5040
		PageUp,
		/// <summary>The PAGE DOWN key.</summary>
		// Token: 0x040013B1 RID: 5041
		PageDown,
		/// <summary>The END key.</summary>
		// Token: 0x040013B2 RID: 5042
		End,
		/// <summary>The HOME key.</summary>
		// Token: 0x040013B3 RID: 5043
		Home,
		/// <summary>The LEFT ARROW key.</summary>
		// Token: 0x040013B4 RID: 5044
		LeftArrow,
		/// <summary>The UP ARROW key.</summary>
		// Token: 0x040013B5 RID: 5045
		UpArrow,
		/// <summary>The RIGHT ARROW key.</summary>
		// Token: 0x040013B6 RID: 5046
		RightArrow,
		/// <summary>The DOWN ARROW key.</summary>
		// Token: 0x040013B7 RID: 5047
		DownArrow,
		/// <summary>The SELECT key.</summary>
		// Token: 0x040013B8 RID: 5048
		Select,
		/// <summary>The PRINT key.</summary>
		// Token: 0x040013B9 RID: 5049
		Print,
		/// <summary>The EXECUTE key.</summary>
		// Token: 0x040013BA RID: 5050
		Execute,
		/// <summary>The PRINT SCREEN key.</summary>
		// Token: 0x040013BB RID: 5051
		PrintScreen,
		/// <summary>The INS (INSERT) key.</summary>
		// Token: 0x040013BC RID: 5052
		Insert,
		/// <summary>The DEL (DELETE) key.</summary>
		// Token: 0x040013BD RID: 5053
		Delete,
		/// <summary>The HELP key.</summary>
		// Token: 0x040013BE RID: 5054
		Help,
		/// <summary>The 0 key.</summary>
		// Token: 0x040013BF RID: 5055
		D0,
		/// <summary>The 1 key.</summary>
		// Token: 0x040013C0 RID: 5056
		D1,
		/// <summary>The 2 key.</summary>
		// Token: 0x040013C1 RID: 5057
		D2,
		/// <summary>The 3 key.</summary>
		// Token: 0x040013C2 RID: 5058
		D3,
		/// <summary>The 4 key.</summary>
		// Token: 0x040013C3 RID: 5059
		D4,
		/// <summary>The 5 key.</summary>
		// Token: 0x040013C4 RID: 5060
		D5,
		/// <summary>The 6 key.</summary>
		// Token: 0x040013C5 RID: 5061
		D6,
		/// <summary>The 7 key.</summary>
		// Token: 0x040013C6 RID: 5062
		D7,
		/// <summary>The 8 key.</summary>
		// Token: 0x040013C7 RID: 5063
		D8,
		/// <summary>The 9 key.</summary>
		// Token: 0x040013C8 RID: 5064
		D9,
		/// <summary>The A key.</summary>
		// Token: 0x040013C9 RID: 5065
		A = 65,
		/// <summary>The B key.</summary>
		// Token: 0x040013CA RID: 5066
		B,
		/// <summary>The C key.</summary>
		// Token: 0x040013CB RID: 5067
		C,
		/// <summary>The D key.</summary>
		// Token: 0x040013CC RID: 5068
		D,
		/// <summary>The E key.</summary>
		// Token: 0x040013CD RID: 5069
		E,
		/// <summary>The F key.</summary>
		// Token: 0x040013CE RID: 5070
		F,
		/// <summary>The G key.</summary>
		// Token: 0x040013CF RID: 5071
		G,
		/// <summary>The H key.</summary>
		// Token: 0x040013D0 RID: 5072
		H,
		/// <summary>The I key.</summary>
		// Token: 0x040013D1 RID: 5073
		I,
		/// <summary>The J key.</summary>
		// Token: 0x040013D2 RID: 5074
		J,
		/// <summary>The K key.</summary>
		// Token: 0x040013D3 RID: 5075
		K,
		/// <summary>The L key.</summary>
		// Token: 0x040013D4 RID: 5076
		L,
		/// <summary>The M key.</summary>
		// Token: 0x040013D5 RID: 5077
		M,
		/// <summary>The N key.</summary>
		// Token: 0x040013D6 RID: 5078
		N,
		/// <summary>The O key.</summary>
		// Token: 0x040013D7 RID: 5079
		O,
		/// <summary>The P key.</summary>
		// Token: 0x040013D8 RID: 5080
		P,
		/// <summary>The Q key.</summary>
		// Token: 0x040013D9 RID: 5081
		Q,
		/// <summary>The R key.</summary>
		// Token: 0x040013DA RID: 5082
		R,
		/// <summary>The S key.</summary>
		// Token: 0x040013DB RID: 5083
		S,
		/// <summary>The T key.</summary>
		// Token: 0x040013DC RID: 5084
		T,
		/// <summary>The U key.</summary>
		// Token: 0x040013DD RID: 5085
		U,
		/// <summary>The V key.</summary>
		// Token: 0x040013DE RID: 5086
		V,
		/// <summary>The W key.</summary>
		// Token: 0x040013DF RID: 5087
		W,
		/// <summary>The X key.</summary>
		// Token: 0x040013E0 RID: 5088
		X,
		/// <summary>The Y key.</summary>
		// Token: 0x040013E1 RID: 5089
		Y,
		/// <summary>The Z key.</summary>
		// Token: 0x040013E2 RID: 5090
		Z,
		/// <summary>The left Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040013E3 RID: 5091
		LeftWindows,
		/// <summary>The right Windows logo key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040013E4 RID: 5092
		RightWindows,
		/// <summary>The Application key (Microsoft Natural Keyboard).</summary>
		// Token: 0x040013E5 RID: 5093
		Applications,
		/// <summary>The Computer Sleep key.</summary>
		// Token: 0x040013E6 RID: 5094
		Sleep = 95,
		/// <summary>The 0 key on the numeric keypad.</summary>
		// Token: 0x040013E7 RID: 5095
		NumPad0,
		/// <summary>The 1 key on the numeric keypad.</summary>
		// Token: 0x040013E8 RID: 5096
		NumPad1,
		/// <summary>The 2 key on the numeric keypad.</summary>
		// Token: 0x040013E9 RID: 5097
		NumPad2,
		/// <summary>The 3 key on the numeric keypad.</summary>
		// Token: 0x040013EA RID: 5098
		NumPad3,
		/// <summary>The 4 key on the numeric keypad.</summary>
		// Token: 0x040013EB RID: 5099
		NumPad4,
		/// <summary>The 5 key on the numeric keypad.</summary>
		// Token: 0x040013EC RID: 5100
		NumPad5,
		/// <summary>The 6 key on the numeric keypad.</summary>
		// Token: 0x040013ED RID: 5101
		NumPad6,
		/// <summary>The 7 key on the numeric keypad.</summary>
		// Token: 0x040013EE RID: 5102
		NumPad7,
		/// <summary>The 8 key on the numeric keypad.</summary>
		// Token: 0x040013EF RID: 5103
		NumPad8,
		/// <summary>The 9 key on the numeric keypad.</summary>
		// Token: 0x040013F0 RID: 5104
		NumPad9,
		/// <summary>The Multiply key (the multiplication key on the numeric keypad).</summary>
		// Token: 0x040013F1 RID: 5105
		Multiply,
		/// <summary>The Add key (the addition key on the numeric keypad).</summary>
		// Token: 0x040013F2 RID: 5106
		Add,
		/// <summary>The Separator key.</summary>
		// Token: 0x040013F3 RID: 5107
		Separator,
		/// <summary>The Subtract key (the subtraction key on the numeric keypad).</summary>
		// Token: 0x040013F4 RID: 5108
		Subtract,
		/// <summary>The Decimal key (the decimal key on the numeric keypad).</summary>
		// Token: 0x040013F5 RID: 5109
		Decimal,
		/// <summary>The Divide key (the division key on the numeric keypad).</summary>
		// Token: 0x040013F6 RID: 5110
		Divide,
		/// <summary>The F1 key.</summary>
		// Token: 0x040013F7 RID: 5111
		F1,
		/// <summary>The F2 key.</summary>
		// Token: 0x040013F8 RID: 5112
		F2,
		/// <summary>The F3 key.</summary>
		// Token: 0x040013F9 RID: 5113
		F3,
		/// <summary>The F4 key.</summary>
		// Token: 0x040013FA RID: 5114
		F4,
		/// <summary>The F5 key.</summary>
		// Token: 0x040013FB RID: 5115
		F5,
		/// <summary>The F6 key.</summary>
		// Token: 0x040013FC RID: 5116
		F6,
		/// <summary>The F7 key.</summary>
		// Token: 0x040013FD RID: 5117
		F7,
		/// <summary>The F8 key.</summary>
		// Token: 0x040013FE RID: 5118
		F8,
		/// <summary>The F9 key.</summary>
		// Token: 0x040013FF RID: 5119
		F9,
		/// <summary>The F10 key.</summary>
		// Token: 0x04001400 RID: 5120
		F10,
		/// <summary>The F11 key.</summary>
		// Token: 0x04001401 RID: 5121
		F11,
		/// <summary>The F12 key.</summary>
		// Token: 0x04001402 RID: 5122
		F12,
		/// <summary>The F13 key.</summary>
		// Token: 0x04001403 RID: 5123
		F13,
		/// <summary>The F14 key.</summary>
		// Token: 0x04001404 RID: 5124
		F14,
		/// <summary>The F15 key.</summary>
		// Token: 0x04001405 RID: 5125
		F15,
		/// <summary>The F16 key.</summary>
		// Token: 0x04001406 RID: 5126
		F16,
		/// <summary>The F17 key.</summary>
		// Token: 0x04001407 RID: 5127
		F17,
		/// <summary>The F18 key.</summary>
		// Token: 0x04001408 RID: 5128
		F18,
		/// <summary>The F19 key.</summary>
		// Token: 0x04001409 RID: 5129
		F19,
		/// <summary>The F20 key.</summary>
		// Token: 0x0400140A RID: 5130
		F20,
		/// <summary>The F21 key.</summary>
		// Token: 0x0400140B RID: 5131
		F21,
		/// <summary>The F22 key.</summary>
		// Token: 0x0400140C RID: 5132
		F22,
		/// <summary>The F23 key.</summary>
		// Token: 0x0400140D RID: 5133
		F23,
		/// <summary>The F24 key.</summary>
		// Token: 0x0400140E RID: 5134
		F24,
		/// <summary>The Browser Back key (Windows 2000 or later).</summary>
		// Token: 0x0400140F RID: 5135
		BrowserBack = 166,
		/// <summary>The Browser Forward key (Windows 2000 or later).</summary>
		// Token: 0x04001410 RID: 5136
		BrowserForward,
		/// <summary>The Browser Refresh key (Windows 2000 or later).</summary>
		// Token: 0x04001411 RID: 5137
		BrowserRefresh,
		/// <summary>The Browser Stop key (Windows 2000 or later).</summary>
		// Token: 0x04001412 RID: 5138
		BrowserStop,
		/// <summary>The Browser Search key (Windows 2000 or later).</summary>
		// Token: 0x04001413 RID: 5139
		BrowserSearch,
		/// <summary>The Browser Favorites key (Windows 2000 or later).</summary>
		// Token: 0x04001414 RID: 5140
		BrowserFavorites,
		/// <summary>The Browser Home key (Windows 2000 or later).</summary>
		// Token: 0x04001415 RID: 5141
		BrowserHome,
		/// <summary>The Volume Mute key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04001416 RID: 5142
		VolumeMute,
		/// <summary>The Volume Down key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04001417 RID: 5143
		VolumeDown,
		/// <summary>The Volume Up key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04001418 RID: 5144
		VolumeUp,
		/// <summary>The Media Next Track key (Windows 2000 or later).</summary>
		// Token: 0x04001419 RID: 5145
		MediaNext,
		/// <summary>The Media Previous Track key (Windows 2000 or later).</summary>
		// Token: 0x0400141A RID: 5146
		MediaPrevious,
		/// <summary>The Media Stop key (Windows 2000 or later).</summary>
		// Token: 0x0400141B RID: 5147
		MediaStop,
		/// <summary>The Media Play/Pause key (Windows 2000 or later).</summary>
		// Token: 0x0400141C RID: 5148
		MediaPlay,
		/// <summary>The Start Mail key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400141D RID: 5149
		LaunchMail,
		/// <summary>The Select Media key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400141E RID: 5150
		LaunchMediaSelect,
		/// <summary>The Start Application 1 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x0400141F RID: 5151
		LaunchApp1,
		/// <summary>The Start Application 2 key (Microsoft Natural Keyboard, Windows 2000 or later).</summary>
		// Token: 0x04001420 RID: 5152
		LaunchApp2,
		/// <summary>The OEM 1 key (OEM specific).</summary>
		// Token: 0x04001421 RID: 5153
		Oem1 = 186,
		/// <summary>The OEM Plus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001422 RID: 5154
		OemPlus,
		/// <summary>The OEM Comma key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001423 RID: 5155
		OemComma,
		/// <summary>The OEM Minus key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001424 RID: 5156
		OemMinus,
		/// <summary>The OEM Period key on any country/region keyboard (Windows 2000 or later).</summary>
		// Token: 0x04001425 RID: 5157
		OemPeriod,
		/// <summary>The OEM 2 key (OEM specific).</summary>
		// Token: 0x04001426 RID: 5158
		Oem2,
		/// <summary>The OEM 3 key (OEM specific).</summary>
		// Token: 0x04001427 RID: 5159
		Oem3,
		/// <summary>The OEM 4 key (OEM specific).</summary>
		// Token: 0x04001428 RID: 5160
		Oem4 = 219,
		/// <summary>The OEM 5 (OEM specific).</summary>
		// Token: 0x04001429 RID: 5161
		Oem5,
		/// <summary>The OEM 6 key (OEM specific).</summary>
		// Token: 0x0400142A RID: 5162
		Oem6,
		/// <summary>The OEM 7 key (OEM specific).</summary>
		// Token: 0x0400142B RID: 5163
		Oem7,
		/// <summary>The OEM 8 key (OEM specific).</summary>
		// Token: 0x0400142C RID: 5164
		Oem8,
		/// <summary>The OEM 102 key (OEM specific).</summary>
		// Token: 0x0400142D RID: 5165
		Oem102 = 226,
		/// <summary>The IME PROCESS key.</summary>
		// Token: 0x0400142E RID: 5166
		Process = 229,
		/// <summary>The PACKET key (used to pass Unicode characters with keystrokes).</summary>
		// Token: 0x0400142F RID: 5167
		Packet = 231,
		/// <summary>The ATTN key.</summary>
		// Token: 0x04001430 RID: 5168
		Attention = 246,
		/// <summary>The CRSEL (CURSOR SELECT) key.</summary>
		// Token: 0x04001431 RID: 5169
		CrSel,
		/// <summary>The EXSEL (EXTEND SELECTION) key.</summary>
		// Token: 0x04001432 RID: 5170
		ExSel,
		/// <summary>The ERASE EOF key.</summary>
		// Token: 0x04001433 RID: 5171
		EraseEndOfFile,
		/// <summary>The PLAY key.</summary>
		// Token: 0x04001434 RID: 5172
		Play,
		/// <summary>The ZOOM key.</summary>
		// Token: 0x04001435 RID: 5173
		Zoom,
		/// <summary>A constant reserved for future use.</summary>
		// Token: 0x04001436 RID: 5174
		NoName,
		/// <summary>The PA1 key.</summary>
		// Token: 0x04001437 RID: 5175
		Pa1,
		/// <summary>The CLEAR key (OEM specific).</summary>
		// Token: 0x04001438 RID: 5176
		OemClear
	}
}
