using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ProGrids
{
	// Token: 0x02000032 RID: 50
	public static class pg_Util
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000086D8 File Offset: 0x000068D8
		public static Color ColorWithString(string value)
		{
			string valid = "01234567890.,";
			value = new string((from c in value
			where valid.Contains(c)
			select c).ToArray<char>());
			string[] array = value.Split(',', StringSplitOptions.None);
			if (array.Length < 4)
			{
				return new Color(1f, 0f, 1f, 1f);
			}
			return new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00008764 File Offset: 0x00006964
		private static Vector3 VectorToMask(Vector3 vec)
		{
			return new Vector3((Mathf.Abs(vec.x) > Mathf.Epsilon) ? 1f : 0f, (Mathf.Abs(vec.y) > Mathf.Epsilon) ? 1f : 0f, (Mathf.Abs(vec.z) > Mathf.Epsilon) ? 1f : 0f);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000087D0 File Offset: 0x000069D0
		private static Axis MaskToAxis(Vector3 vec)
		{
			Axis axis = Axis.None;
			if (Mathf.Abs(vec.x) > 0f)
			{
				axis |= Axis.X;
			}
			if (Mathf.Abs(vec.y) > 0f)
			{
				axis |= Axis.Y;
			}
			if (Mathf.Abs(vec.z) > 0f)
			{
				axis |= Axis.Z;
			}
			return axis;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00008824 File Offset: 0x00006A24
		private static Axis BestAxis(Vector3 vec)
		{
			float num = Mathf.Abs(vec.x);
			float num2 = Mathf.Abs(vec.y);
			float num3 = Mathf.Abs(vec.z);
			if (num > num2 && num > num3)
			{
				return Axis.X;
			}
			if (num2 <= num || num2 <= num3)
			{
				return Axis.Z;
			}
			return Axis.Y;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000886C File Offset: 0x00006A6C
		public static Axis CalcDragAxis(Vector3 movement, Camera cam)
		{
			Vector3 vector = pg_Util.VectorToMask(movement);
			if (vector.x + vector.y + vector.z == 2f)
			{
				return pg_Util.MaskToAxis(Vector3.one - vector);
			}
			switch (pg_Util.MaskToAxis(vector))
			{
			case Axis.X:
				if (Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.up)) < Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.forward)))
				{
					return Axis.Z;
				}
				return Axis.Y;
			case Axis.Y:
				if (Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.right)) < Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.forward)))
				{
					return Axis.Z;
				}
				return Axis.X;
			case Axis.Z:
				if (Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.right)) < Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.up)))
				{
					return Axis.Y;
				}
				return Axis.X;
			}
			return Axis.None;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000897F File Offset: 0x00006B7F
		public static float ValueFromMask(Vector3 val, Vector3 mask)
		{
			if (Mathf.Abs(mask.x) > 0.0001f)
			{
				return val.x;
			}
			if (Mathf.Abs(mask.y) > 0.0001f)
			{
				return val.y;
			}
			return val.z;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000089BC File Offset: 0x00006BBC
		public static Vector3 SnapValue(Vector3 val, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3(pg_Util.Snap(x, snapValue), pg_Util.Snap(y, snapValue), pg_Util.Snap(z, snapValue));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000089F8 File Offset: 0x00006BF8
		private static Type GetType(string type, string assembly = null)
		{
			Type type2 = Type.GetType(type);
			if (type2 == null)
			{
				IEnumerable<Assembly> enumerable = AppDomain.CurrentDomain.GetAssemblies();
				if (assembly != null)
				{
					enumerable = from x in enumerable
					where x.FullName.Contains(assembly)
					select x;
				}
				foreach (Assembly assembly2 in enumerable)
				{
					type2 = assembly2.GetType(type);
					if (type2 != null)
					{
						return type2;
					}
				}
				return type2;
			}
			return type2;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00008A94 File Offset: 0x00006C94
		public static void SetUnityGridEnabled(bool isEnabled)
		{
			try
			{
				pg_Util.GetType("UnityEditor.AnnotationUtility", null).GetProperty("showGrid", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, isEnabled, BindingFlags.Static | BindingFlags.NonPublic, null, null, null);
			}
			catch
			{
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00008AE0 File Offset: 0x00006CE0
		public static bool GetUnityGridEnabled()
		{
			try
			{
				return (bool)pg_Util.GetType("UnityEditor.AnnotationUtility", null).GetProperty("showGrid", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null);
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00008B2C File Offset: 0x00006D2C
		public static Vector3 SnapValue(Vector3 val, Vector3 mask, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3((Mathf.Abs(mask.x) < 0.0001f) ? x : pg_Util.Snap(x, snapValue), (Mathf.Abs(mask.y) < 0.0001f) ? y : pg_Util.Snap(y, snapValue), (Mathf.Abs(mask.z) < 0.0001f) ? z : pg_Util.Snap(z, snapValue));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00008BA8 File Offset: 0x00006DA8
		public static Vector3 SnapToCeil(Vector3 val, Vector3 mask, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3((Mathf.Abs(mask.x) < 0.0001f) ? x : pg_Util.SnapToCeil(x, snapValue), (Mathf.Abs(mask.y) < 0.0001f) ? y : pg_Util.SnapToCeil(y, snapValue), (Mathf.Abs(mask.z) < 0.0001f) ? z : pg_Util.SnapToCeil(z, snapValue));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00008C24 File Offset: 0x00006E24
		public static Vector3 SnapToFloor(Vector3 val, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3(pg_Util.SnapToFloor(x, snapValue), pg_Util.SnapToFloor(y, snapValue), pg_Util.SnapToFloor(z, snapValue));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00008C60 File Offset: 0x00006E60
		public static Vector3 SnapToFloor(Vector3 val, Vector3 mask, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3((Mathf.Abs(mask.x) < 0.0001f) ? x : pg_Util.SnapToFloor(x, snapValue), (Mathf.Abs(mask.y) < 0.0001f) ? y : pg_Util.SnapToFloor(y, snapValue), (Mathf.Abs(mask.z) < 0.0001f) ? z : pg_Util.SnapToFloor(z, snapValue));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00008CDB File Offset: 0x00006EDB
		public static float Snap(float val, float round)
		{
			return round * Mathf.Round(val / round);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00008CE7 File Offset: 0x00006EE7
		public static float SnapToFloor(float val, float snapValue)
		{
			return snapValue * Mathf.Floor(val / snapValue);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00008CF3 File Offset: 0x00006EF3
		public static float SnapToCeil(float val, float snapValue)
		{
			return snapValue * Mathf.Ceil(val / snapValue);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00008D00 File Offset: 0x00006F00
		public static Vector3 CeilFloor(Vector3 v)
		{
			v.x = (float)((v.x < 0f) ? -1 : 1);
			v.y = (float)((v.y < 0f) ? -1 : 1);
			v.z = (float)((v.z < 0f) ? -1 : 1);
			return v;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00008D59 File Offset: 0x00006F59
		public static void ClearSnapEnabledCache()
		{
			pg_Util.m_SnapOverrideCache.Clear();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00008D68 File Offset: 0x00006F68
		public static bool SnapIsEnabled(Transform t)
		{
			pg_Util.SnapEnabledOverride snapEnabledOverride;
			if (pg_Util.m_SnapOverrideCache.TryGetValue(t, out snapEnabledOverride))
			{
				return snapEnabledOverride.IsEnabled();
			}
			object[] source = null;
			MonoBehaviour[] components = t.GetComponents<MonoBehaviour>();
			for (int i = 0; i < components.Length; i++)
			{
				Component c = components[i];
				if (!(c == null))
				{
					Type type = c.GetType();
					bool flag;
					if (pg_Util.m_NoSnapAttributeTypeCache.TryGetValue(type, out flag))
					{
						if (flag)
						{
							pg_Util.m_SnapOverrideCache.Add(t, new pg_Util.SnapIsEnabledOverride(!flag));
							return true;
						}
					}
					else
					{
						source = type.GetCustomAttributes(true);
						flag = source.Any((object x) => x != null && x.ToString().Contains("ProGridsNoSnap"));
						pg_Util.m_NoSnapAttributeTypeCache.Add(type, flag);
						if (flag)
						{
							pg_Util.m_SnapOverrideCache.Add(t, new pg_Util.SnapIsEnabledOverride(!flag));
							return true;
						}
					}
					MethodInfo mi;
					if (pg_Util.m_ConditionalSnapAttributeCache.TryGetValue(type, out mi))
					{
						if (mi != null)
						{
							pg_Util.m_SnapOverrideCache.Add(t, new pg_Util.ConditionalSnapOverride(() => (bool)mi.Invoke(c, null)));
							return (bool)mi.Invoke(c, null);
						}
					}
					else if (source.Any((object x) => x != null && x.ToString().Contains("ProGridsConditionalSnap")))
					{
						mi = type.GetMethod("IsSnapEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
						pg_Util.m_ConditionalSnapAttributeCache.Add(type, mi);
						if (mi != null)
						{
							pg_Util.m_SnapOverrideCache.Add(t, new pg_Util.ConditionalSnapOverride(() => (bool)mi.Invoke(c, null)));
							return (bool)mi.Invoke(c, null);
						}
					}
					else
					{
						pg_Util.m_ConditionalSnapAttributeCache.Add(type, null);
					}
				}
			}
			pg_Util.m_SnapOverrideCache.Add(t, new pg_Util.SnapIsEnabledOverride(true));
			return true;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00008F6F File Offset: 0x0000716F
		// Note: this type is marked as 'beforefieldinit'.
		static pg_Util()
		{
		}

		// Token: 0x0400019E RID: 414
		private const float EPSILON = 0.0001f;

		// Token: 0x0400019F RID: 415
		private static Dictionary<Transform, pg_Util.SnapEnabledOverride> m_SnapOverrideCache = new Dictionary<Transform, pg_Util.SnapEnabledOverride>();

		// Token: 0x040001A0 RID: 416
		private static Dictionary<Type, bool> m_NoSnapAttributeTypeCache = new Dictionary<Type, bool>();

		// Token: 0x040001A1 RID: 417
		private static Dictionary<Type, MethodInfo> m_ConditionalSnapAttributeCache = new Dictionary<Type, MethodInfo>();

		// Token: 0x02000195 RID: 405
		private abstract class SnapEnabledOverride
		{
			// Token: 0x06000EBA RID: 3770
			public abstract bool IsEnabled();

			// Token: 0x06000EBB RID: 3771 RVA: 0x000601B1 File Offset: 0x0005E3B1
			protected SnapEnabledOverride()
			{
			}
		}

		// Token: 0x02000196 RID: 406
		private class SnapIsEnabledOverride : pg_Util.SnapEnabledOverride
		{
			// Token: 0x06000EBC RID: 3772 RVA: 0x000601B9 File Offset: 0x0005E3B9
			public SnapIsEnabledOverride(bool snapIsEnabled)
			{
				this.m_SnapIsEnabled = snapIsEnabled;
			}

			// Token: 0x06000EBD RID: 3773 RVA: 0x000601C8 File Offset: 0x0005E3C8
			public override bool IsEnabled()
			{
				return this.m_SnapIsEnabled;
			}

			// Token: 0x04000CB2 RID: 3250
			private bool m_SnapIsEnabled;
		}

		// Token: 0x02000197 RID: 407
		private class ConditionalSnapOverride : pg_Util.SnapEnabledOverride
		{
			// Token: 0x06000EBE RID: 3774 RVA: 0x000601D0 File Offset: 0x0005E3D0
			public ConditionalSnapOverride(Func<bool> d)
			{
				this.m_IsEnabledDelegate = d;
			}

			// Token: 0x06000EBF RID: 3775 RVA: 0x000601DF File Offset: 0x0005E3DF
			public override bool IsEnabled()
			{
				return this.m_IsEnabledDelegate();
			}

			// Token: 0x04000CB3 RID: 3251
			public Func<bool> m_IsEnabledDelegate;
		}

		// Token: 0x02000198 RID: 408
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0
		{
			// Token: 0x06000EC0 RID: 3776 RVA: 0x000601EC File Offset: 0x0005E3EC
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x06000EC1 RID: 3777 RVA: 0x000601F4 File Offset: 0x0005E3F4
			internal bool <ColorWithString>b__0(char c)
			{
				return this.valid.Contains(c);
			}

			// Token: 0x04000CB4 RID: 3252
			public string valid;
		}

		// Token: 0x02000199 RID: 409
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06000EC2 RID: 3778 RVA: 0x00060202 File Offset: 0x0005E402
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000EC3 RID: 3779 RVA: 0x0006020A File Offset: 0x0005E40A
			internal bool <GetType>b__0(Assembly x)
			{
				return x.FullName.Contains(this.assembly);
			}

			// Token: 0x04000CB5 RID: 3253
			public string assembly;
		}

		// Token: 0x0200019A RID: 410
		[CompilerGenerated]
		private sealed class <>c__DisplayClass26_0
		{
			// Token: 0x06000EC4 RID: 3780 RVA: 0x0006021D File Offset: 0x0005E41D
			public <>c__DisplayClass26_0()
			{
			}

			// Token: 0x06000EC5 RID: 3781 RVA: 0x00060225 File Offset: 0x0005E425
			internal bool <SnapIsEnabled>b__1()
			{
				return (bool)this.mi.Invoke(this.c, null);
			}

			// Token: 0x06000EC6 RID: 3782 RVA: 0x0006023E File Offset: 0x0005E43E
			internal bool <SnapIsEnabled>b__3()
			{
				return (bool)this.mi.Invoke(this.c, null);
			}

			// Token: 0x04000CB6 RID: 3254
			public Component c;

			// Token: 0x04000CB7 RID: 3255
			public MethodInfo mi;
		}

		// Token: 0x0200019B RID: 411
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000EC7 RID: 3783 RVA: 0x00060257 File Offset: 0x0005E457
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000EC8 RID: 3784 RVA: 0x00060263 File Offset: 0x0005E463
			public <>c()
			{
			}

			// Token: 0x06000EC9 RID: 3785 RVA: 0x0006026B File Offset: 0x0005E46B
			internal bool <SnapIsEnabled>b__26_0(object x)
			{
				return x != null && x.ToString().Contains("ProGridsNoSnap");
			}

			// Token: 0x06000ECA RID: 3786 RVA: 0x00060282 File Offset: 0x0005E482
			internal bool <SnapIsEnabled>b__26_2(object x)
			{
				return x != null && x.ToString().Contains("ProGridsConditionalSnap");
			}

			// Token: 0x04000CB8 RID: 3256
			public static readonly pg_Util.<>c <>9 = new pg_Util.<>c();

			// Token: 0x04000CB9 RID: 3257
			public static Func<object, bool> <>9__26_0;

			// Token: 0x04000CBA RID: 3258
			public static Func<object, bool> <>9__26_2;
		}
	}
}
