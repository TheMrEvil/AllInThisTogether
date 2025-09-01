using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the methods available for use with a metafile to read and write graphic commands.</summary>
	// Token: 0x020000FF RID: 255
	public enum EmfPlusRecordType
	{
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000873 RID: 2163
		WmfRecordBase = 65536,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000874 RID: 2164
		WmfSetBkColor = 66049,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000875 RID: 2165
		WmfSetBkMode = 65794,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000876 RID: 2166
		WmfSetMapMode,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000877 RID: 2167
		WmfSetROP2,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000878 RID: 2168
		WmfSetRelAbs,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000879 RID: 2169
		WmfSetPolyFillMode,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400087A RID: 2170
		WmfSetStretchBltMode,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400087B RID: 2171
		WmfSetTextCharExtra,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400087C RID: 2172
		WmfSetTextColor = 66057,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400087D RID: 2173
		WmfSetTextJustification,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400087E RID: 2174
		WmfSetWindowOrg,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400087F RID: 2175
		WmfSetWindowExt,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000880 RID: 2176
		WmfSetViewportOrg,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000881 RID: 2177
		WmfSetViewportExt,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000882 RID: 2178
		WmfOffsetWindowOrg,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000883 RID: 2179
		WmfScaleWindowExt = 66576,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000884 RID: 2180
		WmfOffsetViewportOrg = 66065,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000885 RID: 2181
		WmfScaleViewportExt = 66578,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000886 RID: 2182
		WmfLineTo = 66067,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000887 RID: 2183
		WmfMoveTo,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000888 RID: 2184
		WmfExcludeClipRect = 66581,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000889 RID: 2185
		WmfIntersectClipRect,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400088A RID: 2186
		WmfArc = 67607,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400088B RID: 2187
		WmfEllipse = 66584,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400088C RID: 2188
		WmfFloodFill,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400088D RID: 2189
		WmfPie = 67610,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400088E RID: 2190
		WmfRectangle = 66587,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400088F RID: 2191
		WmfRoundRect = 67100,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000890 RID: 2192
		WmfPatBlt,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000891 RID: 2193
		WmfSaveDC = 65566,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000892 RID: 2194
		WmfSetPixel = 66591,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000893 RID: 2195
		WmfOffsetCilpRgn = 66080,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000894 RID: 2196
		WmfTextOut = 66849,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000895 RID: 2197
		WmfBitBlt = 67874,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000896 RID: 2198
		WmfStretchBlt = 68387,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000897 RID: 2199
		WmfPolygon = 66340,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000898 RID: 2200
		WmfPolyline,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000899 RID: 2201
		WmfEscape = 67110,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400089A RID: 2202
		WmfRestoreDC = 65831,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400089B RID: 2203
		WmfFillRegion = 66088,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400089C RID: 2204
		WmfFrameRegion = 66601,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400089D RID: 2205
		WmfInvertRegion = 65834,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400089E RID: 2206
		WmfPaintRegion,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400089F RID: 2207
		WmfSelectClipRegion,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A0 RID: 2208
		WmfSelectObject,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A1 RID: 2209
		WmfSetTextAlign,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A2 RID: 2210
		WmfChord = 67632,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A3 RID: 2211
		WmfSetMapperFlags = 66097,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A4 RID: 2212
		WmfExtTextOut = 68146,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A5 RID: 2213
		WmfSetDibToDev = 68915,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A6 RID: 2214
		WmfSelectPalette = 66100,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A7 RID: 2215
		WmfRealizePalette = 65589,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A8 RID: 2216
		WmfAnimatePalette = 66614,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008A9 RID: 2217
		WmfSetPalEntries = 65591,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008AA RID: 2218
		WmfPolyPolygon = 66872,
		/// <summary>Increases or decreases the size of a logical palette based on the specified value.</summary>
		// Token: 0x040008AB RID: 2219
		WmfResizePalette = 65849,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008AC RID: 2220
		WmfDibBitBlt = 67904,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008AD RID: 2221
		WmfDibStretchBlt = 68417,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008AE RID: 2222
		WmfDibCreatePatternBrush = 65858,
		/// <summary>Copies the color data for a rectangle of pixels in a DIB to the specified destination rectangle.</summary>
		// Token: 0x040008AF RID: 2223
		WmfStretchDib = 69443,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B0 RID: 2224
		WmfExtFloodFill = 66888,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B1 RID: 2225
		WmfSetLayout = 65865,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B2 RID: 2226
		WmfDeleteObject = 66032,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B3 RID: 2227
		WmfCreatePalette = 65783,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B4 RID: 2228
		WmfCreatePatternBrush = 66041,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B5 RID: 2229
		WmfCreatePenIndirect = 66298,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B6 RID: 2230
		WmfCreateFontIndirect,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B7 RID: 2231
		WmfCreateBrushIndirect,
		/// <summary>See "Windows-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B8 RID: 2232
		WmfCreateRegion = 67327,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008B9 RID: 2233
		EmfHeader = 1,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008BA RID: 2234
		EmfPolyBezier,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008BB RID: 2235
		EmfPolygon,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008BC RID: 2236
		EmfPolyline,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008BD RID: 2237
		EmfPolyBezierTo,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008BE RID: 2238
		EmfPolyLineTo,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008BF RID: 2239
		EmfPolyPolyline,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C0 RID: 2240
		EmfPolyPolygon,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C1 RID: 2241
		EmfSetWindowExtEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C2 RID: 2242
		EmfSetWindowOrgEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C3 RID: 2243
		EmfSetViewportExtEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C4 RID: 2244
		EmfSetViewportOrgEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C5 RID: 2245
		EmfSetBrushOrgEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C6 RID: 2246
		EmfEof,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C7 RID: 2247
		EmfSetPixelV,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C8 RID: 2248
		EmfSetMapperFlags,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008C9 RID: 2249
		EmfSetMapMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008CA RID: 2250
		EmfSetBkMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008CB RID: 2251
		EmfSetPolyFillMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008CC RID: 2252
		EmfSetROP2,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008CD RID: 2253
		EmfSetStretchBltMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008CE RID: 2254
		EmfSetTextAlign,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008CF RID: 2255
		EmfSetColorAdjustment,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D0 RID: 2256
		EmfSetTextColor,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D1 RID: 2257
		EmfSetBkColor,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D2 RID: 2258
		EmfOffsetClipRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D3 RID: 2259
		EmfMoveToEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D4 RID: 2260
		EmfSetMetaRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D5 RID: 2261
		EmfExcludeClipRect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D6 RID: 2262
		EmfIntersectClipRect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D7 RID: 2263
		EmfScaleViewportExtEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D8 RID: 2264
		EmfScaleWindowExtEx,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008D9 RID: 2265
		EmfSaveDC,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008DA RID: 2266
		EmfRestoreDC,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008DB RID: 2267
		EmfSetWorldTransform,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008DC RID: 2268
		EmfModifyWorldTransform,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008DD RID: 2269
		EmfSelectObject,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008DE RID: 2270
		EmfCreatePen,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008DF RID: 2271
		EmfCreateBrushIndirect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E0 RID: 2272
		EmfDeleteObject,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E1 RID: 2273
		EmfAngleArc,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E2 RID: 2274
		EmfEllipse,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E3 RID: 2275
		EmfRectangle,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E4 RID: 2276
		EmfRoundRect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E5 RID: 2277
		EmfRoundArc,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E6 RID: 2278
		EmfChord,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E7 RID: 2279
		EmfPie,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E8 RID: 2280
		EmfSelectPalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008E9 RID: 2281
		EmfCreatePalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008EA RID: 2282
		EmfSetPaletteEntries,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008EB RID: 2283
		EmfResizePalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008EC RID: 2284
		EmfRealizePalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008ED RID: 2285
		EmfExtFloodFill,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008EE RID: 2286
		EmfLineTo,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008EF RID: 2287
		EmfArcTo,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F0 RID: 2288
		EmfPolyDraw,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F1 RID: 2289
		EmfSetArcDirection,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F2 RID: 2290
		EmfSetMiterLimit,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F3 RID: 2291
		EmfBeginPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F4 RID: 2292
		EmfEndPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F5 RID: 2293
		EmfCloseFigure,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F6 RID: 2294
		EmfFillPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F7 RID: 2295
		EmfStrokeAndFillPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F8 RID: 2296
		EmfStrokePath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008F9 RID: 2297
		EmfFlattenPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008FA RID: 2298
		EmfWidenPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008FB RID: 2299
		EmfSelectClipPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008FC RID: 2300
		EmfAbortPath,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008FD RID: 2301
		EmfReserved069,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008FE RID: 2302
		EmfGdiComment,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x040008FF RID: 2303
		EmfFillRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000900 RID: 2304
		EmfFrameRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000901 RID: 2305
		EmfInvertRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000902 RID: 2306
		EmfPaintRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000903 RID: 2307
		EmfExtSelectClipRgn,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000904 RID: 2308
		EmfBitBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000905 RID: 2309
		EmfStretchBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000906 RID: 2310
		EmfMaskBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000907 RID: 2311
		EmfPlgBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000908 RID: 2312
		EmfSetDIBitsToDevice,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000909 RID: 2313
		EmfStretchDIBits,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400090A RID: 2314
		EmfExtCreateFontIndirect,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400090B RID: 2315
		EmfExtTextOutA,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400090C RID: 2316
		EmfExtTextOutW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400090D RID: 2317
		EmfPolyBezier16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400090E RID: 2318
		EmfPolygon16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400090F RID: 2319
		EmfPolyline16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000910 RID: 2320
		EmfPolyBezierTo16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000911 RID: 2321
		EmfPolylineTo16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000912 RID: 2322
		EmfPolyPolyline16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000913 RID: 2323
		EmfPolyPolygon16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000914 RID: 2324
		EmfPolyDraw16,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000915 RID: 2325
		EmfCreateMonoBrush,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000916 RID: 2326
		EmfCreateDibPatternBrushPt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000917 RID: 2327
		EmfExtCreatePen,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000918 RID: 2328
		EmfPolyTextOutA,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000919 RID: 2329
		EmfPolyTextOutW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400091A RID: 2330
		EmfSetIcmMode,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400091B RID: 2331
		EmfCreateColorSpace,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400091C RID: 2332
		EmfSetColorSpace,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400091D RID: 2333
		EmfDeleteColorSpace,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400091E RID: 2334
		EmfGlsRecord,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400091F RID: 2335
		EmfGlsBoundedRecord,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000920 RID: 2336
		EmfPixelFormat,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000921 RID: 2337
		EmfDrawEscape,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000922 RID: 2338
		EmfExtEscape,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000923 RID: 2339
		EmfStartDoc,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000924 RID: 2340
		EmfSmallTextOut,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000925 RID: 2341
		EmfForceUfiMapping,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000926 RID: 2342
		EmfNamedEscpae,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000927 RID: 2343
		EmfColorCorrectPalette,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000928 RID: 2344
		EmfSetIcmProfileA,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000929 RID: 2345
		EmfSetIcmProfileW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400092A RID: 2346
		EmfAlphaBlend,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400092B RID: 2347
		EmfSetLayout,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400092C RID: 2348
		EmfTransparentBlt,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400092D RID: 2349
		EmfReserved117,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400092E RID: 2350
		EmfGradientFill,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x0400092F RID: 2351
		EmfSetLinkedUfis,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000930 RID: 2352
		EmfSetTextJustification,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000931 RID: 2353
		EmfColorMatchToTargetW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000932 RID: 2354
		EmfCreateColorSpaceW,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000933 RID: 2355
		EmfMax = 122,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000934 RID: 2356
		EmfMin = 1,
		/// <summary>See "Enhanced-Format Metafiles" in the GDI section of the MSDN Library.</summary>
		// Token: 0x04000935 RID: 2357
		EmfPlusRecordBase = 16384,
		/// <summary>Indicates invalid data.</summary>
		// Token: 0x04000936 RID: 2358
		Invalid = 16384,
		/// <summary>Identifies a record that is the EMF+ header.</summary>
		// Token: 0x04000937 RID: 2359
		Header,
		/// <summary>Identifies a record that marks the last EMF+ record of a metafile.</summary>
		// Token: 0x04000938 RID: 2360
		EndOfFile,
		/// <summary>See <see cref="M:System.Drawing.Graphics.AddMetafileComment(System.Byte[])" />.</summary>
		// Token: 0x04000939 RID: 2361
		Comment,
		/// <summary>See <see cref="M:System.Drawing.Graphics.GetHdc" />.</summary>
		// Token: 0x0400093A RID: 2362
		GetDC,
		/// <summary>Marks the start of a multiple-format section.</summary>
		// Token: 0x0400093B RID: 2363
		MultiFormatStart,
		/// <summary>Marks a multiple-format section.</summary>
		// Token: 0x0400093C RID: 2364
		MultiFormatSection,
		/// <summary>Marks the end of a multiple-format section.</summary>
		// Token: 0x0400093D RID: 2365
		MultiFormatEnd,
		/// <summary>Marks an object.</summary>
		// Token: 0x0400093E RID: 2366
		Object,
		/// <summary>See <see cref="M:System.Drawing.Graphics.Clear(System.Drawing.Color)" />.</summary>
		// Token: 0x0400093F RID: 2367
		Clear,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillRectangles" /> methods.</summary>
		// Token: 0x04000940 RID: 2368
		FillRects,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawRectangles" /> methods.</summary>
		// Token: 0x04000941 RID: 2369
		DrawRects,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillPolygon" /> methods.</summary>
		// Token: 0x04000942 RID: 2370
		FillPolygon,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawLines" /> methods.</summary>
		// Token: 0x04000943 RID: 2371
		DrawLines,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillEllipse" /> methods.</summary>
		// Token: 0x04000944 RID: 2372
		FillEllipse,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawEllipse" /> methods.</summary>
		// Token: 0x04000945 RID: 2373
		DrawEllipse,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillPie" /> methods.</summary>
		// Token: 0x04000946 RID: 2374
		FillPie,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawPie" /> methods.</summary>
		// Token: 0x04000947 RID: 2375
		DrawPie,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawArc" /> methods.</summary>
		// Token: 0x04000948 RID: 2376
		DrawArc,
		/// <summary>See <see cref="M:System.Drawing.Graphics.FillRegion(System.Drawing.Brush,System.Drawing.Region)" />.</summary>
		// Token: 0x04000949 RID: 2377
		FillRegion,
		/// <summary>See <see cref="M:System.Drawing.Graphics.FillPath(System.Drawing.Brush,System.Drawing.Drawing2D.GraphicsPath)" />.</summary>
		// Token: 0x0400094A RID: 2378
		FillPath,
		/// <summary>See <see cref="M:System.Drawing.Graphics.DrawPath(System.Drawing.Pen,System.Drawing.Drawing2D.GraphicsPath)" />.</summary>
		// Token: 0x0400094B RID: 2379
		DrawPath,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.FillClosedCurve" /> methods.</summary>
		// Token: 0x0400094C RID: 2380
		FillClosedCurve,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawClosedCurve" /> methods.</summary>
		// Token: 0x0400094D RID: 2381
		DrawClosedCurve,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawCurve" /> methods.</summary>
		// Token: 0x0400094E RID: 2382
		DrawCurve,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawBeziers" /> methods.</summary>
		// Token: 0x0400094F RID: 2383
		DrawBeziers,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawImage" /> methods.</summary>
		// Token: 0x04000950 RID: 2384
		DrawImage,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawImage" /> methods.</summary>
		// Token: 0x04000951 RID: 2385
		DrawImagePoints,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.DrawString" /> methods.</summary>
		// Token: 0x04000952 RID: 2386
		DrawString,
		/// <summary>See <see cref="P:System.Drawing.Graphics.RenderingOrigin" />.</summary>
		// Token: 0x04000953 RID: 2387
		SetRenderingOrigin,
		/// <summary>See <see cref="P:System.Drawing.Graphics.SmoothingMode" />.</summary>
		// Token: 0x04000954 RID: 2388
		SetAntiAliasMode,
		/// <summary>See <see cref="P:System.Drawing.Graphics.TextRenderingHint" />.</summary>
		// Token: 0x04000955 RID: 2389
		SetTextRenderingHint,
		/// <summary>See <see cref="P:System.Drawing.Graphics.TextContrast" />.</summary>
		// Token: 0x04000956 RID: 2390
		SetTextContrast,
		/// <summary>See <see cref="P:System.Drawing.Graphics.InterpolationMode" />.</summary>
		// Token: 0x04000957 RID: 2391
		SetInterpolationMode,
		/// <summary>See <see cref="P:System.Drawing.Graphics.PixelOffsetMode" />.</summary>
		// Token: 0x04000958 RID: 2392
		SetPixelOffsetMode,
		/// <summary>See <see cref="P:System.Drawing.Graphics.CompositingMode" />.</summary>
		// Token: 0x04000959 RID: 2393
		SetCompositingMode,
		/// <summary>See <see cref="P:System.Drawing.Graphics.CompositingQuality" />.</summary>
		// Token: 0x0400095A RID: 2394
		SetCompositingQuality,
		/// <summary>See <see cref="M:System.Drawing.Graphics.Save" />.</summary>
		// Token: 0x0400095B RID: 2395
		Save,
		/// <summary>See <see cref="M:System.Drawing.Graphics.Restore(System.Drawing.Drawing2D.GraphicsState)" />.</summary>
		// Token: 0x0400095C RID: 2396
		Restore,
		/// <summary>See <see cref="M:System.Drawing.Graphics.BeginContainer" /> methods.</summary>
		// Token: 0x0400095D RID: 2397
		BeginContainer,
		/// <summary>See <see cref="M:System.Drawing.Graphics.BeginContainer" /> methods.</summary>
		// Token: 0x0400095E RID: 2398
		BeginContainerNoParams,
		/// <summary>See <see cref="M:System.Drawing.Graphics.EndContainer(System.Drawing.Drawing2D.GraphicsContainer)" />.</summary>
		// Token: 0x0400095F RID: 2399
		EndContainer,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.TransformPoints" /> methods.</summary>
		// Token: 0x04000960 RID: 2400
		SetWorldTransform,
		/// <summary>See <see cref="M:System.Drawing.Graphics.ResetTransform" />.</summary>
		// Token: 0x04000961 RID: 2401
		ResetWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.MultiplyTransform" /> methods.</summary>
		// Token: 0x04000962 RID: 2402
		MultiplyWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.TransformPoints" /> methods.</summary>
		// Token: 0x04000963 RID: 2403
		TranslateWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.ScaleTransform" /> methods.</summary>
		// Token: 0x04000964 RID: 2404
		ScaleWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.RotateTransform" /> methods.</summary>
		// Token: 0x04000965 RID: 2405
		RotateWorldTransform,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.TransformPoints" /> methods.</summary>
		// Token: 0x04000966 RID: 2406
		SetPageTransform,
		/// <summary>See <see cref="M:System.Drawing.Graphics.ResetClip" />.</summary>
		// Token: 0x04000967 RID: 2407
		ResetClip,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.SetClip" /> methods.</summary>
		// Token: 0x04000968 RID: 2408
		SetClipRect,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.SetClip" /> methods.</summary>
		// Token: 0x04000969 RID: 2409
		SetClipPath,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.SetClip" /> methods.</summary>
		// Token: 0x0400096A RID: 2410
		SetClipRegion,
		/// <summary>See <see cref="Overload:System.Drawing.Graphics.TranslateClip" /> methods.</summary>
		// Token: 0x0400096B RID: 2411
		OffsetClip,
		/// <summary>Specifies a character string, a location, and formatting information.</summary>
		// Token: 0x0400096C RID: 2412
		DrawDriverString,
		/// <summary>Used internally.</summary>
		// Token: 0x0400096D RID: 2413
		Total,
		/// <summary>The maximum value for this enumeration.</summary>
		// Token: 0x0400096E RID: 2414
		Max = 16438,
		/// <summary>The minimum value for this enumeration.</summary>
		// Token: 0x0400096F RID: 2415
		Min = 16385
	}
}
