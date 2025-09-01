using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200001F RID: 31
	internal static class InternalUtility
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00010F4C File Offset: 0x0000F14C
		public static T[] GetComponents<T>(GameObject go) where T : Component
		{
			return go.transform.GetComponentsInChildren<T>();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00010F5C File Offset: 0x0000F15C
		public static T[] GetComponents<T>(this IEnumerable<Transform> transforms) where T : Component
		{
			List<T> list = new List<T>();
			foreach (Transform transform in transforms)
			{
				list.AddRange(transform.GetComponentsInChildren<T>());
			}
			return list.ToArray();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		public static GameObject EmptyGameObjectWithTransform(Transform t)
		{
			return new GameObject
			{
				transform = 
				{
					localPosition = t.localPosition,
					localRotation = t.localRotation,
					localScale = t.localScale
				}
			};
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00010FF4 File Offset: 0x0000F1F4
		public static GameObject MeshGameObjectWithTransform(string name, Transform t, Mesh mesh, Material mat, bool inheritParent)
		{
			GameObject gameObject = InternalUtility.EmptyGameObjectWithTransform(t);
			gameObject.name = name;
			gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
			gameObject.AddComponent<MeshRenderer>().sharedMaterial = mat;
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			if (inheritParent)
			{
				gameObject.transform.SetParent(t.parent, false);
			}
			return gameObject;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00011048 File Offset: 0x0000F248
		public static T NextEnumValue<T>(this T current) where T : IConvertible
		{
			Array values = Enum.GetValues(current.GetType());
			int i = 0;
			int length = values.Length;
			while (i < length)
			{
				if (current.Equals(values.GetValue(i)))
				{
					return (T)((object)values.GetValue((i + 1) % length));
				}
				i++;
			}
			return current;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000110A4 File Offset: 0x0000F2A4
		public static string ControlKeyString(char character)
		{
			if (character == '⌘')
			{
				return "Control";
			}
			if (character == '⇧')
			{
				return "Shift";
			}
			if (character == '⌥')
			{
				return "Alt";
			}
			if (character == '⎇')
			{
				return "Alt";
			}
			if (character == '⌫')
			{
				return "Delete";
			}
			return character.ToString();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00011100 File Offset: 0x0000F300
		public static bool TryParseColor(string value, ref Color col)
		{
			string valid = "01234567890.,";
			value = new string((from c in value
			where valid.Contains(c)
			select c).ToArray<char>());
			string[] array = value.Split(',', StringSplitOptions.None);
			if (array.Length < 4)
			{
				return false;
			}
			try
			{
				float r = float.Parse(array[0]);
				float g = float.Parse(array[1]);
				float b = float.Parse(array[2]);
				float a = float.Parse(array[3]);
				col.r = r;
				col.g = g;
				col.b = b;
				col.a = a;
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000111B0 File Offset: 0x0000F3B0
		public static T DemandComponent<T>(this Component component) where T : Component
		{
			return component.gameObject.DemandComponent<T>();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000111C0 File Offset: 0x0000F3C0
		public static T DemandComponent<T>(this GameObject gameObject) where T : Component
		{
			T result;
			if (!gameObject.TryGetComponent<T>(out result))
			{
				result = gameObject.AddComponent<T>();
			}
			return result;
		}

		// Token: 0x02000096 RID: 150
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000535 RID: 1333 RVA: 0x00035B5E File Offset: 0x00033D5E
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000536 RID: 1334 RVA: 0x00035B66 File Offset: 0x00033D66
			internal bool <TryParseColor>b__0(char c)
			{
				return this.valid.Contains(c);
			}

			// Token: 0x0400029A RID: 666
			public string valid;
		}
	}
}
