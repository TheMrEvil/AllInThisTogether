using System;

namespace System.Drawing
{
	/// <summary>Specifies the known system colors.</summary>
	// Token: 0x02000011 RID: 17
	public enum KnownColor
	{
		/// <summary>The system-defined color of the active window's border.</summary>
		// Token: 0x04000094 RID: 148
		ActiveBorder = 1,
		/// <summary>The system-defined color of the background of the active window's title bar.</summary>
		// Token: 0x04000095 RID: 149
		ActiveCaption,
		/// <summary>The system-defined color of the text in the active window's title bar.</summary>
		// Token: 0x04000096 RID: 150
		ActiveCaptionText,
		/// <summary>The system-defined color of the application workspace. The application workspace is the area in a multiple-document view that is not being occupied by documents.</summary>
		// Token: 0x04000097 RID: 151
		AppWorkspace,
		/// <summary>The system-defined face color of a 3-D element.</summary>
		// Token: 0x04000098 RID: 152
		Control,
		/// <summary>The system-defined shadow color of a 3-D element. The shadow color is applied to parts of a 3-D element that face away from the light source.</summary>
		// Token: 0x04000099 RID: 153
		ControlDark,
		/// <summary>The system-defined color that is the dark shadow color of a 3-D element. The dark shadow color is applied to the parts of a 3-D element that are the darkest color.</summary>
		// Token: 0x0400009A RID: 154
		ControlDarkDark,
		/// <summary>The system-defined color that is the light color of a 3-D element. The light color is applied to parts of a 3-D element that face the light source.</summary>
		// Token: 0x0400009B RID: 155
		ControlLight,
		/// <summary>The system-defined highlight color of a 3-D element. The highlight color is applied to the parts of a 3-D element that are the lightest color.</summary>
		// Token: 0x0400009C RID: 156
		ControlLightLight,
		/// <summary>The system-defined color of text in a 3-D element.</summary>
		// Token: 0x0400009D RID: 157
		ControlText,
		/// <summary>The system-defined color of the desktop.</summary>
		// Token: 0x0400009E RID: 158
		Desktop,
		/// <summary>The system-defined color of dimmed text. Items in a list that are disabled are displayed in dimmed text.</summary>
		// Token: 0x0400009F RID: 159
		GrayText,
		/// <summary>The system-defined color of the background of selected items. This includes selected menu items as well as selected text.</summary>
		// Token: 0x040000A0 RID: 160
		Highlight,
		/// <summary>The system-defined color of the text of selected items.</summary>
		// Token: 0x040000A1 RID: 161
		HighlightText,
		/// <summary>The system-defined color used to designate a hot-tracked item. Single-clicking a hot-tracked item executes the item.</summary>
		// Token: 0x040000A2 RID: 162
		HotTrack,
		/// <summary>The system-defined color of an inactive window's border.</summary>
		// Token: 0x040000A3 RID: 163
		InactiveBorder,
		/// <summary>The system-defined color of the background of an inactive window's title bar.</summary>
		// Token: 0x040000A4 RID: 164
		InactiveCaption,
		/// <summary>The system-defined color of the text in an inactive window's title bar.</summary>
		// Token: 0x040000A5 RID: 165
		InactiveCaptionText,
		/// <summary>The system-defined color of the background of a ToolTip.</summary>
		// Token: 0x040000A6 RID: 166
		Info,
		/// <summary>The system-defined color of the text of a ToolTip.</summary>
		// Token: 0x040000A7 RID: 167
		InfoText,
		/// <summary>The system-defined color of a menu's background.</summary>
		// Token: 0x040000A8 RID: 168
		Menu,
		/// <summary>The system-defined color of a menu's text.</summary>
		// Token: 0x040000A9 RID: 169
		MenuText,
		/// <summary>The system-defined color of the background of a scroll bar.</summary>
		// Token: 0x040000AA RID: 170
		ScrollBar,
		/// <summary>The system-defined color of the background in the client area of a window.</summary>
		// Token: 0x040000AB RID: 171
		Window,
		/// <summary>The system-defined color of a window frame.</summary>
		// Token: 0x040000AC RID: 172
		WindowFrame,
		/// <summary>The system-defined color of the text in the client area of a window.</summary>
		// Token: 0x040000AD RID: 173
		WindowText,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000AE RID: 174
		Transparent,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000AF RID: 175
		AliceBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B0 RID: 176
		AntiqueWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B1 RID: 177
		Aqua,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B2 RID: 178
		Aquamarine,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B3 RID: 179
		Azure,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B4 RID: 180
		Beige,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B5 RID: 181
		Bisque,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B6 RID: 182
		Black,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B7 RID: 183
		BlanchedAlmond,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B8 RID: 184
		Blue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000B9 RID: 185
		BlueViolet,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000BA RID: 186
		Brown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000BB RID: 187
		BurlyWood,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000BC RID: 188
		CadetBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000BD RID: 189
		Chartreuse,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000BE RID: 190
		Chocolate,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000BF RID: 191
		Coral,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C0 RID: 192
		CornflowerBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C1 RID: 193
		Cornsilk,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C2 RID: 194
		Crimson,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C3 RID: 195
		Cyan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C4 RID: 196
		DarkBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C5 RID: 197
		DarkCyan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C6 RID: 198
		DarkGoldenrod,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C7 RID: 199
		DarkGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C8 RID: 200
		DarkGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000C9 RID: 201
		DarkKhaki,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000CA RID: 202
		DarkMagenta,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000CB RID: 203
		DarkOliveGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000CC RID: 204
		DarkOrange,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000CD RID: 205
		DarkOrchid,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000CE RID: 206
		DarkRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000CF RID: 207
		DarkSalmon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D0 RID: 208
		DarkSeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D1 RID: 209
		DarkSlateBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D2 RID: 210
		DarkSlateGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D3 RID: 211
		DarkTurquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D4 RID: 212
		DarkViolet,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D5 RID: 213
		DeepPink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D6 RID: 214
		DeepSkyBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D7 RID: 215
		DimGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D8 RID: 216
		DodgerBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000D9 RID: 217
		Firebrick,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000DA RID: 218
		FloralWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000DB RID: 219
		ForestGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000DC RID: 220
		Fuchsia,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000DD RID: 221
		Gainsboro,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000DE RID: 222
		GhostWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000DF RID: 223
		Gold,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E0 RID: 224
		Goldenrod,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E1 RID: 225
		Gray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E2 RID: 226
		Green,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E3 RID: 227
		GreenYellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E4 RID: 228
		Honeydew,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E5 RID: 229
		HotPink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E6 RID: 230
		IndianRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E7 RID: 231
		Indigo,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E8 RID: 232
		Ivory,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000E9 RID: 233
		Khaki,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000EA RID: 234
		Lavender,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000EB RID: 235
		LavenderBlush,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000EC RID: 236
		LawnGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000ED RID: 237
		LemonChiffon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000EE RID: 238
		LightBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000EF RID: 239
		LightCoral,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F0 RID: 240
		LightCyan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F1 RID: 241
		LightGoldenrodYellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F2 RID: 242
		LightGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F3 RID: 243
		LightGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F4 RID: 244
		LightPink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F5 RID: 245
		LightSalmon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F6 RID: 246
		LightSeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F7 RID: 247
		LightSkyBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F8 RID: 248
		LightSlateGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000F9 RID: 249
		LightSteelBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000FA RID: 250
		LightYellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000FB RID: 251
		Lime,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000FC RID: 252
		LimeGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000FD RID: 253
		Linen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000FE RID: 254
		Magenta,
		/// <summary>A system-defined color.</summary>
		// Token: 0x040000FF RID: 255
		Maroon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000100 RID: 256
		MediumAquamarine,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000101 RID: 257
		MediumBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000102 RID: 258
		MediumOrchid,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000103 RID: 259
		MediumPurple,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000104 RID: 260
		MediumSeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000105 RID: 261
		MediumSlateBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000106 RID: 262
		MediumSpringGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000107 RID: 263
		MediumTurquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000108 RID: 264
		MediumVioletRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000109 RID: 265
		MidnightBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400010A RID: 266
		MintCream,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400010B RID: 267
		MistyRose,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400010C RID: 268
		Moccasin,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400010D RID: 269
		NavajoWhite,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400010E RID: 270
		Navy,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400010F RID: 271
		OldLace,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000110 RID: 272
		Olive,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000111 RID: 273
		OliveDrab,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000112 RID: 274
		Orange,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000113 RID: 275
		OrangeRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000114 RID: 276
		Orchid,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000115 RID: 277
		PaleGoldenrod,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000116 RID: 278
		PaleGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000117 RID: 279
		PaleTurquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000118 RID: 280
		PaleVioletRed,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000119 RID: 281
		PapayaWhip,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400011A RID: 282
		PeachPuff,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400011B RID: 283
		Peru,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400011C RID: 284
		Pink,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400011D RID: 285
		Plum,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400011E RID: 286
		PowderBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400011F RID: 287
		Purple,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000120 RID: 288
		Red,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000121 RID: 289
		RosyBrown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000122 RID: 290
		RoyalBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000123 RID: 291
		SaddleBrown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000124 RID: 292
		Salmon,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000125 RID: 293
		SandyBrown,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000126 RID: 294
		SeaGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000127 RID: 295
		SeaShell,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000128 RID: 296
		Sienna,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000129 RID: 297
		Silver,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400012A RID: 298
		SkyBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400012B RID: 299
		SlateBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400012C RID: 300
		SlateGray,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400012D RID: 301
		Snow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400012E RID: 302
		SpringGreen,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400012F RID: 303
		SteelBlue,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000130 RID: 304
		Tan,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000131 RID: 305
		Teal,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000132 RID: 306
		Thistle,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000133 RID: 307
		Tomato,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000134 RID: 308
		Turquoise,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000135 RID: 309
		Violet,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000136 RID: 310
		Wheat,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000137 RID: 311
		White,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000138 RID: 312
		WhiteSmoke,
		/// <summary>A system-defined color.</summary>
		// Token: 0x04000139 RID: 313
		Yellow,
		/// <summary>A system-defined color.</summary>
		// Token: 0x0400013A RID: 314
		YellowGreen,
		/// <summary>The system-defined face color of a 3-D element.</summary>
		// Token: 0x0400013B RID: 315
		ButtonFace,
		/// <summary>The system-defined color that is the highlight color of a 3-D element. This color is applied to parts of a 3-D element that face the light source.</summary>
		// Token: 0x0400013C RID: 316
		ButtonHighlight,
		/// <summary>The system-defined color that is the shadow color of a 3-D element. This color is applied to parts of a 3-D element that face away from the light source.</summary>
		// Token: 0x0400013D RID: 317
		ButtonShadow,
		/// <summary>The system-defined color of the lightest color in the color gradient of an active window's title bar.</summary>
		// Token: 0x0400013E RID: 318
		GradientActiveCaption,
		/// <summary>The system-defined color of the lightest color in the color gradient of an inactive window's title bar.</summary>
		// Token: 0x0400013F RID: 319
		GradientInactiveCaption,
		/// <summary>The system-defined color of the background of a menu bar.</summary>
		// Token: 0x04000140 RID: 320
		MenuBar,
		/// <summary>The system-defined color used to highlight menu items when the menu appears as a flat menu.</summary>
		// Token: 0x04000141 RID: 321
		MenuHighlight
	}
}
