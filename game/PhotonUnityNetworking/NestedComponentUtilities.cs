using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x0200001F RID: 31
	public static class NestedComponentUtilities
	{
		// Token: 0x06000170 RID: 368 RVA: 0x0000908C File Offset: 0x0000728C
		public static T EnsureRootComponentExists<T, NestedT>(this Transform transform) where T : Component where NestedT : Component
		{
			NestedT parentComponent = transform.GetParentComponent<NestedT>();
			if (!parentComponent)
			{
				return default(T);
			}
			T component = parentComponent.GetComponent<T>();
			if (component)
			{
				return component;
			}
			return parentComponent.gameObject.AddComponent<T>();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000090E4 File Offset: 0x000072E4
		public static T GetParentComponent<T>(this Transform t) where T : Component
		{
			T component = t.GetComponent<T>();
			if (component)
			{
				return component;
			}
			Transform parent = t.parent;
			while (parent)
			{
				component = parent.GetComponent<T>();
				if (component)
				{
					return component;
				}
				parent = parent.parent;
			}
			return default(T);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00009140 File Offset: 0x00007340
		public static void GetNestedComponentsInParents<T>(this Transform t, List<T> list) where T : Component
		{
			list.Clear();
			while (t != null)
			{
				T component = t.GetComponent<T>();
				if (component)
				{
					list.Add(component);
				}
				t = t.parent;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009184 File Offset: 0x00007384
		public static T GetNestedComponentInChildren<T, NestedT>(this Transform t, bool includeInactive) where T : class where NestedT : class
		{
			T component = t.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
			NestedComponentUtilities.nodesQueue.Clear();
			NestedComponentUtilities.nodesQueue.Enqueue(t);
			while (NestedComponentUtilities.nodesQueue.Count > 0)
			{
				Transform transform = NestedComponentUtilities.nodesQueue.Dequeue();
				int i = 0;
				int childCount = transform.childCount;
				while (i < childCount)
				{
					Transform child = transform.GetChild(i);
					if ((includeInactive || child.gameObject.activeSelf) && child.GetComponent<NestedT>() == null)
					{
						component = child.GetComponent<T>();
						if (component != null)
						{
							return component;
						}
						NestedComponentUtilities.nodesQueue.Enqueue(child);
					}
					i++;
				}
			}
			return component;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000922C File Offset: 0x0000742C
		public static T GetNestedComponentInParent<T, NestedT>(this Transform t) where T : class where NestedT : class
		{
			T t2 = default(T);
			Transform transform = t;
			for (;;)
			{
				t2 = transform.GetComponent<T>();
				if (t2 != null)
				{
					break;
				}
				if (transform.GetComponent<NestedT>() != null)
				{
					goto Block_2;
				}
				transform = transform.parent;
				if (transform == null)
				{
					goto Block_3;
				}
			}
			return t2;
			Block_2:
			return default(T);
			Block_3:
			return default(T);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00009280 File Offset: 0x00007480
		public static T GetNestedComponentInParents<T, NestedT>(this Transform t) where T : class where NestedT : class
		{
			T component = t.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
			for (Transform parent = t.parent; parent != null; parent = parent.parent)
			{
				component = parent.GetComponent<T>();
				if (component != null)
				{
					return component;
				}
				if (parent.GetComponent<NestedT>() != null)
				{
					return default(T);
				}
			}
			return default(T);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000092E4 File Offset: 0x000074E4
		public static void GetNestedComponentsInParents<T, NestedT>(this Transform t, List<T> list) where T : class where NestedT : class
		{
			t.GetComponents<T>(list);
			if (t.GetComponent<NestedT>() != null)
			{
				return;
			}
			Transform parent = t.parent;
			if (parent == null)
			{
				return;
			}
			NestedComponentUtilities.nodeStack.Clear();
			do
			{
				NestedComponentUtilities.nodeStack.Push(parent);
				if (parent.GetComponent<NestedT>() != null)
				{
					break;
				}
				parent = parent.parent;
			}
			while (parent != null);
			if (NestedComponentUtilities.nodeStack.Count == 0)
			{
				return;
			}
			Type typeFromHandle = typeof(T);
			List<T> list2;
			if (!NestedComponentUtilities.searchLists.ContainsKey(typeFromHandle))
			{
				list2 = new List<T>();
				NestedComponentUtilities.searchLists.Add(typeFromHandle, list2);
			}
			else
			{
				list2 = (NestedComponentUtilities.searchLists[typeFromHandle] as List<T>);
			}
			while (NestedComponentUtilities.nodeStack.Count > 0)
			{
				NestedComponentUtilities.nodeStack.Pop().GetComponents<T>(list2);
				list.AddRange(list2);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000093B0 File Offset: 0x000075B0
		public static List<T> GetNestedComponentsInChildren<T, NestedT>(this Transform t, List<T> list, bool includeInactive = true) where T : class where NestedT : class
		{
			Type typeFromHandle = typeof(T);
			List<T> list2;
			if (!NestedComponentUtilities.searchLists.ContainsKey(typeFromHandle))
			{
				NestedComponentUtilities.searchLists.Add(typeFromHandle, list2 = new List<T>());
			}
			else
			{
				list2 = (NestedComponentUtilities.searchLists[typeFromHandle] as List<T>);
			}
			NestedComponentUtilities.nodesQueue.Clear();
			if (list == null)
			{
				list = new List<T>();
			}
			t.GetComponents<T>(list);
			int i = 0;
			int childCount = t.childCount;
			while (i < childCount)
			{
				Transform child = t.GetChild(i);
				if ((includeInactive || child.gameObject.activeSelf) && child.GetComponent<NestedT>() == null)
				{
					NestedComponentUtilities.nodesQueue.Enqueue(child);
				}
				i++;
			}
			while (NestedComponentUtilities.nodesQueue.Count > 0)
			{
				Transform transform = NestedComponentUtilities.nodesQueue.Dequeue();
				transform.GetComponents<T>(list2);
				list.AddRange(list2);
				int j = 0;
				int childCount2 = transform.childCount;
				while (j < childCount2)
				{
					Transform child2 = transform.GetChild(j);
					if ((includeInactive || child2.gameObject.activeSelf) && child2.GetComponent<NestedT>() == null)
					{
						NestedComponentUtilities.nodesQueue.Enqueue(child2);
					}
					j++;
				}
			}
			return list;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000094D8 File Offset: 0x000076D8
		public static List<T> GetNestedComponentsInChildren<T>(this Transform t, List<T> list, bool includeInactive = true, params Type[] stopOn) where T : class
		{
			Type typeFromHandle = typeof(T);
			List<T> list2;
			if (!NestedComponentUtilities.searchLists.ContainsKey(typeFromHandle))
			{
				NestedComponentUtilities.searchLists.Add(typeFromHandle, list2 = new List<T>());
			}
			else
			{
				list2 = (NestedComponentUtilities.searchLists[typeFromHandle] as List<T>);
			}
			NestedComponentUtilities.nodesQueue.Clear();
			t.GetComponents<T>(list);
			int i = 0;
			int childCount = t.childCount;
			while (i < childCount)
			{
				Transform child = t.GetChild(i);
				if (includeInactive || child.gameObject.activeSelf)
				{
					bool flag = false;
					int j = 0;
					int num = stopOn.Length;
					while (j < num)
					{
						if (child.GetComponent(stopOn[j]) != null)
						{
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						NestedComponentUtilities.nodesQueue.Enqueue(child);
					}
				}
				i++;
			}
			while (NestedComponentUtilities.nodesQueue.Count > 0)
			{
				Transform transform = NestedComponentUtilities.nodesQueue.Dequeue();
				transform.GetComponents<T>(list2);
				list.AddRange(list2);
				int k = 0;
				int childCount2 = transform.childCount;
				while (k < childCount2)
				{
					Transform child2 = transform.GetChild(k);
					if (includeInactive || child2.gameObject.activeSelf)
					{
						bool flag2 = false;
						int l = 0;
						int num2 = stopOn.Length;
						while (l < num2)
						{
							if (child2.GetComponent(stopOn[l]) != null)
							{
								flag2 = true;
								break;
							}
							l++;
						}
						if (!flag2)
						{
							NestedComponentUtilities.nodesQueue.Enqueue(child2);
						}
					}
					k++;
				}
			}
			return list;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00009640 File Offset: 0x00007840
		public static void GetNestedComponentsInChildren<T, SearchT, NestedT>(this Transform t, bool includeInactive, List<T> list) where T : class where SearchT : class
		{
			list.Clear();
			if (!includeInactive && !t.gameObject.activeSelf)
			{
				return;
			}
			Type typeFromHandle = typeof(SearchT);
			List<SearchT> list2;
			if (!NestedComponentUtilities.searchLists.ContainsKey(typeFromHandle))
			{
				NestedComponentUtilities.searchLists.Add(typeFromHandle, list2 = new List<SearchT>());
			}
			else
			{
				list2 = (NestedComponentUtilities.searchLists[typeFromHandle] as List<SearchT>);
			}
			NestedComponentUtilities.nodesQueue.Clear();
			NestedComponentUtilities.nodesQueue.Enqueue(t);
			while (NestedComponentUtilities.nodesQueue.Count > 0)
			{
				Transform transform = NestedComponentUtilities.nodesQueue.Dequeue();
				list2.Clear();
				transform.GetComponents<SearchT>(list2);
				foreach (SearchT searchT in list2)
				{
					T t2 = searchT as T;
					if (t2 != null)
					{
						list.Add(t2);
					}
				}
				int i = 0;
				int childCount = transform.childCount;
				while (i < childCount)
				{
					Transform child = transform.GetChild(i);
					if ((includeInactive || child.gameObject.activeSelf) && child.GetComponent<NestedT>() == null)
					{
						NestedComponentUtilities.nodesQueue.Enqueue(child);
					}
					i++;
				}
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000978C File Offset: 0x0000798C
		// Note: this type is marked as 'beforefieldinit'.
		static NestedComponentUtilities()
		{
		}

		// Token: 0x040000C1 RID: 193
		private static Queue<Transform> nodesQueue = new Queue<Transform>();

		// Token: 0x040000C2 RID: 194
		public static Dictionary<Type, ICollection> searchLists = new Dictionary<Type, ICollection>();

		// Token: 0x040000C3 RID: 195
		private static Stack<Transform> nodeStack = new Stack<Transform>();
	}
}
