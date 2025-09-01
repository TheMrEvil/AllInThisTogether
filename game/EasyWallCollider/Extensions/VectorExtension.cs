using System;
using UnityEngine;

namespace PepijnWillekens.Extensions
{
	// Token: 0x02000003 RID: 3
	public static class VectorExtension
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021B9 File Offset: 0x000003B9
		public static Vector3 ChangeX(this Vector3 parent, float newX)
		{
			return new Vector3(newX, parent.y, parent.z);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021CD File Offset: 0x000003CD
		public static Vector3 ChangeY(this Vector3 parent, float newY)
		{
			return new Vector3(parent.x, newY, parent.z);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E1 File Offset: 0x000003E1
		public static Vector3 ChangeZ(this Vector3 parent, float newZ)
		{
			return new Vector3(parent.x, parent.y, newZ);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021F5 File Offset: 0x000003F5
		public static Vector3 ChangeX(this Vector3 parent, VectorExtension.FloatEdit edit)
		{
			return new Vector3(edit(parent.x), parent.y, parent.z);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002214 File Offset: 0x00000414
		public static Vector3 ChangeY(this Vector3 parent, VectorExtension.FloatEdit edit)
		{
			return new Vector3(parent.x, edit(parent.y), parent.z);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002233 File Offset: 0x00000433
		public static Vector3 ChangeZ(this Vector3 parent, VectorExtension.FloatEdit edit)
		{
			return new Vector3(parent.x, parent.y, edit(parent.z));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002252 File Offset: 0x00000452
		public static Vector2 ChangeX(this Vector2 parent, float newX)
		{
			return new Vector2(newX, parent.y);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002260 File Offset: 0x00000460
		public static Vector2 ChangeY(this Vector2 parent, float newY)
		{
			return new Vector2(parent.x, newY);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000226E File Offset: 0x0000046E
		public static Vector2 ChangeX(this Vector2 parent, VectorExtension.FloatEdit edit)
		{
			return new Vector2(edit(parent.x), parent.y);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002287 File Offset: 0x00000487
		public static Vector2 ChangeY(this Vector2 parent, VectorExtension.FloatEdit edit)
		{
			return new Vector2(parent.x, edit(parent.y));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022A0 File Offset: 0x000004A0
		public static void SetX(this Vector3 v, float newX)
		{
			v.x = newX;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022AA File Offset: 0x000004AA
		public static void SetY(this Vector3 v, float newY)
		{
			v.y = newY;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022B4 File Offset: 0x000004B4
		public static void SetZ(this Vector3 v, float newZ)
		{
			v.z = newZ;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022BE File Offset: 0x000004BE
		public static void SetX(this Vector2 v, float newX)
		{
			v.x = newX;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022C8 File Offset: 0x000004C8
		public static void SetY(this Vector2 v, float newY)
		{
			v.y = newY;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022D2 File Offset: 0x000004D2
		public static string ToDetailedString(this Vector2 v)
		{
			return v.ToString("F5");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022E0 File Offset: 0x000004E0
		public static string ToDetailedString(this Vector3 v)
		{
			return v.ToString("F5");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000022EE File Offset: 0x000004EE
		public static Vector3 AddX(this Vector3 parent, float changeX)
		{
			return new Vector3(parent.x + changeX, parent.y, parent.z);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002309 File Offset: 0x00000509
		public static Vector3 AddY(this Vector3 parent, float changeY)
		{
			return new Vector3(parent.x, parent.y + changeY, parent.z);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002324 File Offset: 0x00000524
		public static Vector3 AddZ(this Vector3 parent, float changeZ)
		{
			return new Vector3(parent.x, parent.y, parent.z + changeZ);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000233F File Offset: 0x0000053F
		public static Vector2 AddX(this Vector2 parent, float changeX)
		{
			return new Vector2(parent.x + changeX, parent.y);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002354 File Offset: 0x00000554
		public static Vector2 AddY(this Vector2 parent, float changeY)
		{
			return new Vector2(parent.x, parent.y + changeY);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002369 File Offset: 0x00000569
		public static Vector2 Abs(this Vector2 v)
		{
			return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002386 File Offset: 0x00000586
		public static Vector3 Abs(this Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023AE File Offset: 0x000005AE
		public static Vector3 ChangeXY(this Vector3 v, Vector2 xy)
		{
			return new Vector3(xy.x, xy.y, v.z);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023C7 File Offset: 0x000005C7
		public static Vector3 AddXY(this Vector3 v, Vector2 xy)
		{
			return new Vector3(v.x + xy.x, v.y + xy.y, v.z);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023EE File Offset: 0x000005EE
		public static Vector3 ToVector3(this Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002406 File Offset: 0x00000606
		public static Vector3 ToVector3(this Vector2 v, float z)
		{
			return new Vector3(v.x, v.y, z);
		}

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x06000026 RID: 38
		public delegate float FloatEdit(float input);
	}
}
