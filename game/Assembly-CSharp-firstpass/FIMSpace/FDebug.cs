using System;
using System.Diagnostics;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x02000038 RID: 56
	public static class FDebug
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00009D69 File Offset: 0x00007F69
		public static void Log(string log)
		{
			UnityEngine.Debug.Log("LOG: " + log);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00009D7C File Offset: 0x00007F7C
		public static void Log(string log, string category)
		{
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				FDebug.MarkerColor("#1A6600"),
				"[",
				category,
				"]",
				FDebug.EndColorMarker(),
				" ",
				log
			}));
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009DCE File Offset: 0x00007FCE
		public static void LogRed(string log)
		{
			UnityEngine.Debug.Log(FDebug.MarkerColor("red") + log + FDebug.EndColorMarker());
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00009DEA File Offset: 0x00007FEA
		public static void LogOrange(string log)
		{
			UnityEngine.Debug.Log(FDebug.MarkerColor("#D1681D") + log + FDebug.EndColorMarker());
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00009E06 File Offset: 0x00008006
		public static void LogYellow(string log)
		{
			UnityEngine.Debug.Log(FDebug.MarkerColor("#E0D300") + log + FDebug.EndColorMarker());
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009E22 File Offset: 0x00008022
		public static void StartMeasure()
		{
			FDebug._debugWatch.Reset();
			FDebug._debugWatch.Start();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009E38 File Offset: 0x00008038
		public static void PauseMeasure()
		{
			FDebug._debugWatch.Stop();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009E44 File Offset: 0x00008044
		public static void ResumeMeasure()
		{
			FDebug._debugWatch.Start();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009E50 File Offset: 0x00008050
		public static void EndMeasureAndLog(string v)
		{
			FDebug._debugWatch.Stop();
			FDebug._LastMeasureMilliseconds = FDebug._debugWatch.ElapsedMilliseconds;
			FDebug._LastMeasureTicks = FDebug._debugWatch.ElapsedTicks;
			UnityEngine.Debug.Log(string.Concat(new string[]
			{
				"Measure ",
				v,
				": ",
				FDebug._debugWatch.ElapsedTicks.ToString(),
				" ticks   ",
				FDebug._debugWatch.ElapsedMilliseconds.ToString(),
				"ms"
			}));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009EE3 File Offset: 0x000080E3
		public static long EndMeasureAndGetTicks()
		{
			FDebug._debugWatch.Stop();
			FDebug._LastMeasureMilliseconds = FDebug._debugWatch.ElapsedMilliseconds;
			FDebug._LastMeasureTicks = FDebug._debugWatch.ElapsedTicks;
			return FDebug._debugWatch.ElapsedTicks;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00009F17 File Offset: 0x00008117
		public static string MarkerColor(string color)
		{
			return "<color='" + color + "'>";
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00009F29 File Offset: 0x00008129
		public static string EndColorMarker()
		{
			return "</color>";
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00009F30 File Offset: 0x00008130
		public static void DrawBounds2D(this Bounds b, Color c, float y = 0f, float scale = 1f, float duration = 1.1f)
		{
			Vector3 vector = new Vector3(b.max.x, y, b.max.z) * scale;
			Vector3 vector2 = new Vector3(b.max.x, y, b.min.z) * scale;
			Vector3 vector3 = new Vector3(b.min.x, y, b.min.z) * scale;
			Vector3 vector4 = new Vector3(b.min.x, y, b.max.z) * scale;
			UnityEngine.Debug.DrawLine(vector, vector2, c, duration);
			UnityEngine.Debug.DrawLine(vector2, vector3, c, duration);
			UnityEngine.Debug.DrawLine(vector2, vector3, c, duration);
			UnityEngine.Debug.DrawLine(vector3, vector4, c, duration);
			UnityEngine.Debug.DrawLine(vector4, vector, c, duration);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000A004 File Offset: 0x00008204
		public static void DrawBounds3D(this Bounds b, Color c, float scale = 1f, float time = 1.01f)
		{
			Vector3 vector = new Vector3(b.max.x, b.min.y, b.max.z) * scale;
			Vector3 vector2 = new Vector3(b.max.x, b.min.y, b.min.z) * scale;
			Vector3 vector3 = new Vector3(b.min.x, b.min.y, b.min.z) * scale;
			Vector3 vector4 = new Vector3(b.min.x, b.min.y, b.max.z) * scale;
			UnityEngine.Debug.DrawLine(vector, vector2, c, time);
			UnityEngine.Debug.DrawLine(vector2, vector3, c, time);
			UnityEngine.Debug.DrawLine(vector2, vector3, c, time);
			UnityEngine.Debug.DrawLine(vector3, vector4, c, time);
			UnityEngine.Debug.DrawLine(vector4, vector, c, time);
			Vector3 vector5 = new Vector3(b.max.x, b.max.y, b.max.z) * scale;
			Vector3 vector6 = new Vector3(b.max.x, b.max.y, b.min.z) * scale;
			Vector3 vector7 = new Vector3(b.min.x, b.max.y, b.min.z) * scale;
			Vector3 vector8 = new Vector3(b.min.x, b.max.y, b.max.z) * scale;
			UnityEngine.Debug.DrawLine(vector5, vector6, c, time);
			UnityEngine.Debug.DrawLine(vector6, vector7, c, time);
			UnityEngine.Debug.DrawLine(vector6, vector7, c, time);
			UnityEngine.Debug.DrawLine(vector7, vector8, c, time);
			UnityEngine.Debug.DrawLine(vector8, vector5, c, time);
			UnityEngine.Debug.DrawLine(vector, vector, c, time);
			UnityEngine.Debug.DrawLine(vector6, vector2, c, time);
			UnityEngine.Debug.DrawLine(vector3, vector7, c, time);
			UnityEngine.Debug.DrawLine(vector4, vector8, c, time);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000A220 File Offset: 0x00008420
		// Note: this type is marked as 'beforefieldinit'.
		static FDebug()
		{
		}

		// Token: 0x040001C1 RID: 449
		private static readonly Stopwatch _debugWatch = new Stopwatch();

		// Token: 0x040001C2 RID: 450
		public static long _LastMeasureMilliseconds = 0L;

		// Token: 0x040001C3 RID: 451
		public static long _LastMeasureTicks = 0L;
	}
}
