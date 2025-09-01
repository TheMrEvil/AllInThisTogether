using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000B9 RID: 185
	public class Hierarchy
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x00038974 File Offset: 0x00036B74
		public static bool HierarchyIsValid(Transform[] bones)
		{
			for (int i = 1; i < bones.Length; i++)
			{
				if (!Hierarchy.IsAncestor(bones[i], bones[i - 1]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000389A4 File Offset: 0x00036BA4
		public static UnityEngine.Object ContainsDuplicate(UnityEngine.Object[] objects)
		{
			for (int i = 0; i < objects.Length; i++)
			{
				for (int j = 0; j < objects.Length; j++)
				{
					if (i != j && objects[i] == objects[j])
					{
						return objects[i];
					}
				}
			}
			return null;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000389E4 File Offset: 0x00036BE4
		public static bool IsAncestor(Transform transform, Transform ancestor)
		{
			return transform == null || ancestor == null || (!(transform.parent == null) && (transform.parent == ancestor || Hierarchy.IsAncestor(transform.parent, ancestor)));
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00038A34 File Offset: 0x00036C34
		public static bool ContainsChild(Transform transform, Transform child)
		{
			if (transform == child)
			{
				return true;
			}
			Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i] == child)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00038A70 File Offset: 0x00036C70
		public static void AddAncestors(Transform transform, Transform blocker, ref Transform[] array)
		{
			if (transform.parent != null && transform.parent != blocker)
			{
				if (transform.parent.position != transform.position && transform.parent.position != blocker.position)
				{
					Array.Resize<Transform>(ref array, array.Length + 1);
					array[array.Length - 1] = transform.parent;
				}
				Hierarchy.AddAncestors(transform.parent, blocker, ref array);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00038AF1 File Offset: 0x00036CF1
		public static Transform GetAncestor(Transform transform, int minChildCount)
		{
			if (transform == null)
			{
				return null;
			}
			if (!(transform.parent != null))
			{
				return null;
			}
			if (transform.parent.childCount >= minChildCount)
			{
				return transform.parent;
			}
			return Hierarchy.GetAncestor(transform.parent, minChildCount);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00038B30 File Offset: 0x00036D30
		public static Transform GetFirstCommonAncestor(Transform t1, Transform t2)
		{
			if (t1 == null)
			{
				return null;
			}
			if (t2 == null)
			{
				return null;
			}
			if (t1.parent == null)
			{
				return null;
			}
			if (t2.parent == null)
			{
				return null;
			}
			if (Hierarchy.IsAncestor(t2, t1.parent))
			{
				return t1.parent;
			}
			return Hierarchy.GetFirstCommonAncestor(t1.parent, t2);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00038B94 File Offset: 0x00036D94
		public static Transform GetFirstCommonAncestor(Transform[] transforms)
		{
			if (transforms == null)
			{
				Debug.LogWarning("Transforms is null.");
				return null;
			}
			if (transforms.Length == 0)
			{
				Debug.LogWarning("Transforms.Length is 0.");
				return null;
			}
			for (int i = 0; i < transforms.Length; i++)
			{
				if (transforms[i] == null)
				{
					return null;
				}
				if (Hierarchy.IsCommonAncestor(transforms[i], transforms))
				{
					return transforms[i];
				}
			}
			return Hierarchy.GetFirstCommonAncestorRecursive(transforms[0], transforms);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00038BF4 File Offset: 0x00036DF4
		public static Transform GetFirstCommonAncestorRecursive(Transform transform, Transform[] transforms)
		{
			if (transform == null)
			{
				Debug.LogWarning("Transform is null.");
				return null;
			}
			if (transforms == null)
			{
				Debug.LogWarning("Transforms is null.");
				return null;
			}
			if (transforms.Length == 0)
			{
				Debug.LogWarning("Transforms.Length is 0.");
				return null;
			}
			if (Hierarchy.IsCommonAncestor(transform, transforms))
			{
				return transform;
			}
			if (transform.parent == null)
			{
				return null;
			}
			return Hierarchy.GetFirstCommonAncestorRecursive(transform.parent, transforms);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00038C5C File Offset: 0x00036E5C
		public static bool IsCommonAncestor(Transform transform, Transform[] transforms)
		{
			if (transform == null)
			{
				Debug.LogWarning("Transform is null.");
				return false;
			}
			for (int i = 0; i < transforms.Length; i++)
			{
				if (transforms[i] == null)
				{
					Debug.Log("Transforms[" + i.ToString() + "] is null.");
					return false;
				}
				if (!Hierarchy.IsAncestor(transforms[i], transform) && transforms[i] != transform)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00038CCD File Offset: 0x00036ECD
		public Hierarchy()
		{
		}
	}
}
