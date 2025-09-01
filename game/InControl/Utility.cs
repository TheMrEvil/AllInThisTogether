using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InControl
{
	// Token: 0x02000071 RID: 113
	public static class Utility
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x00012818 File Offset: 0x00010A18
		public static void DrawCircleGizmo(Vector2 center, float radius)
		{
			Vector2 v = Utility.circleVertexList[0] * radius + center;
			int num = Utility.circleVertexList.Length;
			for (int i = 1; i < num; i++)
			{
				Gizmos.DrawLine(v, v = Utility.circleVertexList[i] * radius + center);
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001287A File Offset: 0x00010A7A
		public static void DrawCircleGizmo(Vector2 center, float radius, Color color)
		{
			Gizmos.color = color;
			Utility.DrawCircleGizmo(center, radius);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001288C File Offset: 0x00010A8C
		public static void DrawOvalGizmo(Vector2 center, Vector2 size)
		{
			Vector2 b = size / 2f;
			Vector2 v = Vector2.Scale(Utility.circleVertexList[0], b) + center;
			int num = Utility.circleVertexList.Length;
			for (int i = 1; i < num; i++)
			{
				Gizmos.DrawLine(v, v = Vector2.Scale(Utility.circleVertexList[i], b) + center);
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000128FA File Offset: 0x00010AFA
		public static void DrawOvalGizmo(Vector2 center, Vector2 size, Color color)
		{
			Gizmos.color = color;
			Utility.DrawOvalGizmo(center, size);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001290C File Offset: 0x00010B0C
		public static void DrawRectGizmo(Rect rect)
		{
			Vector3 vector = new Vector3(rect.xMin, rect.yMin);
			Vector3 vector2 = new Vector3(rect.xMax, rect.yMin);
			Vector3 vector3 = new Vector3(rect.xMax, rect.yMax);
			Vector3 vector4 = new Vector3(rect.xMin, rect.yMax);
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4);
			Gizmos.DrawLine(vector4, vector);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00012989 File Offset: 0x00010B89
		public static void DrawRectGizmo(Rect rect, Color color)
		{
			Gizmos.color = color;
			Utility.DrawRectGizmo(rect);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00012998 File Offset: 0x00010B98
		public static void DrawRectGizmo(Vector2 center, Vector2 size)
		{
			float num = size.x / 2f;
			float num2 = size.y / 2f;
			Vector3 vector = new Vector3(center.x - num, center.y - num2);
			Vector3 vector2 = new Vector3(center.x + num, center.y - num2);
			Vector3 vector3 = new Vector3(center.x + num, center.y + num2);
			Vector3 vector4 = new Vector3(center.x - num, center.y + num2);
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4);
			Gizmos.DrawLine(vector4, vector);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00012A3B File Offset: 0x00010C3B
		public static void DrawRectGizmo(Vector2 center, Vector2 size, Color color)
		{
			Gizmos.color = color;
			Utility.DrawRectGizmo(center, size);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00012A4A File Offset: 0x00010C4A
		public static bool GameObjectIsCulledOnCurrentCamera(GameObject gameObject)
		{
			return (Camera.current.cullingMask & 1 << gameObject.layer) == 0;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00012A68 File Offset: 0x00010C68
		public static Color MoveColorTowards(Color color0, Color color1, float maxDelta)
		{
			float r = Mathf.MoveTowards(color0.r, color1.r, maxDelta);
			float g = Mathf.MoveTowards(color0.g, color1.g, maxDelta);
			float b = Mathf.MoveTowards(color0.b, color1.b, maxDelta);
			float a = Mathf.MoveTowards(color0.a, color1.a, maxDelta);
			return new Color(r, g, b, a);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00012AC8 File Offset: 0x00010CC8
		public static float ApplyDeadZone(float value, float lowerDeadZone, float upperDeadZone)
		{
			float num = upperDeadZone - lowerDeadZone;
			if (value < 0f)
			{
				if (value > -lowerDeadZone)
				{
					return 0f;
				}
				if (value < -upperDeadZone)
				{
					return -1f;
				}
				return (value + lowerDeadZone) / num;
			}
			else
			{
				if (value < lowerDeadZone)
				{
					return 0f;
				}
				if (value > upperDeadZone)
				{
					return 1f;
				}
				return (value - lowerDeadZone) / num;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00012B18 File Offset: 0x00010D18
		public static float ApplySmoothing(float thisValue, float lastValue, float deltaTime, float sensitivity)
		{
			if (Utility.Approximately(sensitivity, 1f))
			{
				return thisValue;
			}
			float maxDelta = deltaTime * sensitivity * 100f;
			if (Utility.IsNotZero(thisValue) && Utility.Sign(lastValue) != Utility.Sign(thisValue))
			{
				lastValue = 0f;
			}
			return Mathf.MoveTowards(lastValue, thisValue, maxDelta);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00012B63 File Offset: 0x00010D63
		public static float ApplySnapping(float value, float threshold)
		{
			if (value < -threshold)
			{
				return -1f;
			}
			if (value > threshold)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00012B7F File Offset: 0x00010D7F
		internal static bool TargetIsButton(InputControlType target)
		{
			return (target >= InputControlType.Action1 && target <= InputControlType.Action12) || (target >= InputControlType.Button0 && target <= InputControlType.Button19);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00012BA2 File Offset: 0x00010DA2
		internal static bool TargetIsStandard(InputControlType target)
		{
			return (target >= InputControlType.LeftStickUp && target <= InputControlType.Action12) || (target >= InputControlType.Command && target <= InputControlType.RightCommand);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00012BC4 File Offset: 0x00010DC4
		internal static bool TargetIsAlias(InputControlType target)
		{
			return target >= InputControlType.Command && target <= InputControlType.RightCommand;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00012BDC File Offset: 0x00010DDC
		public static string ReadFromFile(string path)
		{
			StreamReader streamReader = new StreamReader(path);
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			return result;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00012BFC File Offset: 0x00010DFC
		public static void WriteToFile(string path, string data)
		{
			StreamWriter streamWriter = new StreamWriter(path);
			streamWriter.Write(data);
			streamWriter.Flush();
			streamWriter.Close();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00012C16 File Offset: 0x00010E16
		public static float Abs(float value)
		{
			if (value >= 0f)
			{
				return value;
			}
			return -value;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00012C24 File Offset: 0x00010E24
		public static bool Approximately(float v1, float v2)
		{
			float num = v1 - v2;
			return num >= -1E-07f && num <= 1E-07f;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00012C4A File Offset: 0x00010E4A
		public static bool Approximately(Vector2 v1, Vector2 v2)
		{
			return Utility.Approximately(v1.x, v2.x) && Utility.Approximately(v1.y, v2.y);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00012C72 File Offset: 0x00010E72
		public static bool IsNotZero(float value)
		{
			return value < -1E-07f || value > 1E-07f;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00012C86 File Offset: 0x00010E86
		public static bool IsZero(float value)
		{
			return value >= -1E-07f && value <= 1E-07f;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00012C9D File Offset: 0x00010E9D
		public static int Sign(float f)
		{
			if ((double)f >= 0.0)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00012CAF File Offset: 0x00010EAF
		public static bool AbsoluteIsOverThreshold(float value, float threshold)
		{
			return value < -threshold || value > threshold;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00012CBC File Offset: 0x00010EBC
		public static float NormalizeAngle(float angle)
		{
			while (angle < 0f)
			{
				angle += 360f;
			}
			while (angle > 360f)
			{
				angle -= 360f;
			}
			return angle;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00012CE5 File Offset: 0x00010EE5
		public static float VectorToAngle(Vector2 vector)
		{
			if (Utility.IsZero(vector.x) && Utility.IsZero(vector.y))
			{
				return 0f;
			}
			return Utility.NormalizeAngle(Mathf.Atan2(vector.x, vector.y) * 57.29578f);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00012D23 File Offset: 0x00010F23
		public static float Min(float v0, float v1)
		{
			if (v0 < v1)
			{
				return v0;
			}
			return v1;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00012D2C File Offset: 0x00010F2C
		public static float Max(float v0, float v1)
		{
			if (v0 > v1)
			{
				return v0;
			}
			return v1;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00012D38 File Offset: 0x00010F38
		public static float Min(float v0, float v1, float v2, float v3)
		{
			float num = (v0 >= v1) ? v1 : v0;
			float num2 = (v2 >= v3) ? v3 : v2;
			if (num < num2)
			{
				return num;
			}
			return num2;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00012D60 File Offset: 0x00010F60
		public static float Max(float v0, float v1, float v2, float v3)
		{
			float num = (v0 <= v1) ? v1 : v0;
			float num2 = (v2 <= v3) ? v3 : v2;
			if (num > num2)
			{
				return num;
			}
			return num2;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00012D88 File Offset: 0x00010F88
		internal static float ValueFromSides(float negativeSide, float positiveSide)
		{
			float num = Utility.Abs(negativeSide);
			float num2 = Utility.Abs(positiveSide);
			if (Utility.Approximately(num, num2))
			{
				return 0f;
			}
			if (num <= num2)
			{
				return num2;
			}
			return -num;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00012DBA File Offset: 0x00010FBA
		internal static float ValueFromSides(float negativeSide, float positiveSide, bool invertSides)
		{
			if (invertSides)
			{
				return Utility.ValueFromSides(positiveSide, negativeSide);
			}
			return Utility.ValueFromSides(negativeSide, positiveSide);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00012DCE File Offset: 0x00010FCE
		public static void ArrayResize<T>(ref T[] array, int capacity)
		{
			if (array == null || capacity > array.Length)
			{
				Array.Resize<T>(ref array, Utility.NextPowerOfTwo(capacity));
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00012DE7 File Offset: 0x00010FE7
		public static void ArrayExpand<T>(ref T[] array, int capacity)
		{
			if (array == null || capacity > array.Length)
			{
				array = new T[Utility.NextPowerOfTwo(capacity)];
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00012E01 File Offset: 0x00011001
		public static void ArrayAppend<T>(ref T[] array, T item)
		{
			if (array == null)
			{
				array = new T[1];
				array[0] = item;
				return;
			}
			Array.Resize<T>(ref array, array.Length + 1);
			array[array.Length - 1] = item;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00012E33 File Offset: 0x00011033
		public static void ArrayAppend<T>(ref T[] array, T[] items)
		{
			if (array == null)
			{
				array = new T[items.Length];
				Array.Copy(items, array, items.Length);
				return;
			}
			Array.Resize<T>(ref array, array.Length + items.Length);
			Array.ConstrainedCopy(items, 0, array, array.Length - items.Length, items.Length);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00012E71 File Offset: 0x00011071
		public static int NextPowerOfTwo(int value)
		{
			if (value > 0)
			{
				value--;
				value |= value >> 1;
				value |= value >> 2;
				value |= value >> 4;
				value |= value >> 8;
				value |= value >> 16;
				value++;
				return value;
			}
			return 0;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00012EA8 File Offset: 0x000110A8
		internal static bool Is32Bit
		{
			get
			{
				return IntPtr.Size == 4;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00012EB2 File Offset: 0x000110B2
		internal static bool Is64Bit
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00012EBC File Offset: 0x000110BC
		public static string GetPlatformName(bool uppercase = true)
		{
			string windowsVersion = Utility.GetWindowsVersion();
			if (!uppercase)
			{
				return windowsVersion;
			}
			return windowsVersion.ToUpper();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00012EDC File Offset: 0x000110DC
		private static string GetHumanUnderstandableWindowsVersion()
		{
			Version version = Environment.OSVersion.Version;
			if (version.Major == 6)
			{
				switch (version.Minor)
				{
				case 1:
					return "7";
				case 2:
					return "8";
				case 3:
					return "8.1";
				default:
					return "Vista";
				}
			}
			else
			{
				if (version.Major != 5)
				{
					return version.Major.ToString();
				}
				int minor = version.Minor;
				if (minor - 1 <= 1)
				{
					return "XP";
				}
				return "2000";
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00012F64 File Offset: 0x00011164
		public static string GetWindowsVersion()
		{
			string humanUnderstandableWindowsVersion = Utility.GetHumanUnderstandableWindowsVersion();
			string text = Utility.Is32Bit ? "32Bit" : "64Bit";
			return string.Concat(new string[]
			{
				"Windows ",
				humanUnderstandableWindowsVersion,
				" ",
				text,
				" Build ",
				Utility.GetSystemBuildNumber().ToString()
			});
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00012FC6 File Offset: 0x000111C6
		public static int GetSystemBuildNumber()
		{
			return Environment.OSVersion.Version.Build;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00012FD7 File Offset: 0x000111D7
		public static void LoadScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00012FDF File Offset: 0x000111DF
		internal static string PluginFileExtension()
		{
			return ".dll";
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00012FE8 File Offset: 0x000111E8
		// Note: this type is marked as 'beforefieldinit'.
		static Utility()
		{
		}

		// Token: 0x0400041A RID: 1050
		public const float Epsilon = 1E-07f;

		// Token: 0x0400041B RID: 1051
		private static readonly Vector2[] circleVertexList = new Vector2[]
		{
			new Vector2(0f, 1f),
			new Vector2(0.2588f, 0.9659f),
			new Vector2(0.5f, 0.866f),
			new Vector2(0.7071f, 0.7071f),
			new Vector2(0.866f, 0.5f),
			new Vector2(0.9659f, 0.2588f),
			new Vector2(1f, 0f),
			new Vector2(0.9659f, -0.2588f),
			new Vector2(0.866f, -0.5f),
			new Vector2(0.7071f, -0.7071f),
			new Vector2(0.5f, -0.866f),
			new Vector2(0.2588f, -0.9659f),
			new Vector2(0f, -1f),
			new Vector2(-0.2588f, -0.9659f),
			new Vector2(-0.5f, -0.866f),
			new Vector2(-0.7071f, -0.7071f),
			new Vector2(-0.866f, -0.5f),
			new Vector2(-0.9659f, -0.2588f),
			new Vector2(-1f, --0f),
			new Vector2(-0.9659f, 0.2588f),
			new Vector2(-0.866f, 0.5f),
			new Vector2(-0.7071f, 0.7071f),
			new Vector2(-0.5f, 0.866f),
			new Vector2(-0.2588f, 0.9659f),
			new Vector2(0f, 1f)
		};
	}
}
