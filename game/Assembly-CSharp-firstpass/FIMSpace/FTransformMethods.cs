using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace
{
	// Token: 0x0200003C RID: 60
	public static class FTransformMethods
	{
		// Token: 0x06000156 RID: 342 RVA: 0x0000B77C File Offset: 0x0000997C
		public static Transform FindChildByNameInDepth(string name, Transform transform, bool findInDeactivated = true, string[] additionalContains = null)
		{
			if (transform.name == name)
			{
				return transform;
			}
			foreach (Transform transform2 in transform.GetComponentsInChildren<Transform>(findInDeactivated))
			{
				if (transform2.name.ToLower().Contains(name.ToLower()))
				{
					bool flag = false;
					if (additionalContains == null || additionalContains.Length == 0)
					{
						flag = true;
					}
					else
					{
						for (int j = 0; j < additionalContains.Length; j++)
						{
							if (transform2.name.ToLower().Contains(additionalContains[j].ToLower()))
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						return transform2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000B810 File Offset: 0x00009A10
		public static List<T> FindComponentsInAllChildren<T>(Transform transformToSearchIn, bool includeInactive = false, bool tryGetMultipleOutOfSingleObject = false) where T : Component
		{
			List<T> list = new List<T>();
			foreach (T t in transformToSearchIn.GetComponents<T>())
			{
				if (t)
				{
					list.Add(t);
				}
			}
			foreach (Transform transform in transformToSearchIn.GetComponentsInChildren<Transform>(includeInactive))
			{
				if (!tryGetMultipleOutOfSingleObject)
				{
					T component = transform.GetComponent<T>();
					if (component && !list.Contains(component))
					{
						list.Add(component);
					}
				}
				else
				{
					foreach (T t2 in transform.GetComponents<T>())
					{
						if (t2 && !list.Contains(t2))
						{
							list.Add(t2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		public static T FindComponentInAllChildren<T>(Transform transformToSearchIn) where T : Component
		{
			Transform[] componentsInChildren = transformToSearchIn.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				T component = componentsInChildren[i].GetComponent<T>();
				if (component)
				{
					return component;
				}
			}
			return default(T);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000B92C File Offset: 0x00009B2C
		public static T FindComponentInAllParents<T>(Transform transformToSearchIn) where T : Component
		{
			Transform parent = transformToSearchIn.parent;
			for (int i = 0; i < 100; i++)
			{
				T component = parent.GetComponent<T>();
				if (component)
				{
					return component;
				}
				parent = parent.parent;
				if (parent == null)
				{
					return default(T);
				}
			}
			return default(T);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000B988 File Offset: 0x00009B88
		public static void ChangeActiveChildrenInside(Transform parentOfThem, bool active)
		{
			for (int i = 0; i < parentOfThem.childCount; i++)
			{
				parentOfThem.GetChild(i).gameObject.SetActive(active);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public static void ChangeActiveThroughParentTo(Transform start, Transform end, bool active, bool changeParentsChildrenActivation = false)
		{
			start.gameObject.SetActive(active);
			Transform parent = start.parent;
			for (int i = 0; i < 100; i++)
			{
				if (parent == end)
				{
					return;
				}
				if (parent == null)
				{
					return;
				}
				if (changeParentsChildrenActivation)
				{
					FTransformMethods.ChangeActiveChildrenInside(parent, active);
				}
				parent = parent.parent;
			}
		}
	}
}
