using System;
using UnityEngine;

namespace PepijnWillekens.Extensions
{
	// Token: 0x02000002 RID: 2
	public static class MonoBehaviourExtension
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static T AddMissingComponent<T>(this GameObject gameObject) where T : Component
		{
			T t = gameObject.GetComponent<T>();
			if (t == null)
			{
				t = gameObject.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207C File Offset: 0x0000027C
		public static void SetParentLocal(this Component component, Transform parent)
		{
			Transform transform = component.transform;
			RectTransform rectTransform = transform as RectTransform;
			Vector2 offsetMin = Vector2.zero;
			Vector2 offsetMax = Vector2.zero;
			bool flag = rectTransform != null;
			if (flag)
			{
				offsetMin = rectTransform.offsetMin;
				offsetMax = rectTransform.offsetMax;
			}
			Vector3 localPosition = transform.localPosition;
			Vector3 localScale = transform.localScale;
			Quaternion localRotation = transform.localRotation;
			transform.SetParent(parent);
			transform.localPosition = localPosition;
			transform.localScale = localScale;
			transform.localRotation = localRotation;
			if (flag)
			{
				rectTransform.offsetMin = offsetMin;
				rectTransform.offsetMax = offsetMax;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002104 File Offset: 0x00000304
		public static void DestroyAllChildren(this Transform transform)
		{
			if (Application.isPlaying)
			{
				for (int i = 0; i < transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
				}
				return;
			}
			while (transform.childCount > 0)
			{
				UnityEngine.Object.DestroyImmediate(transform.GetChild(0).gameObject);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002155 File Offset: 0x00000355
		public static void Reset(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localScale = Vector3.one;
			transform.localRotation = Quaternion.identity;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002178 File Offset: 0x00000378
		public static void SetLayerRecursive(this GameObject gameObject, int layer)
		{
			gameObject.layer = layer;
			for (int i = 0; i < gameObject.transform.childCount; i++)
			{
				gameObject.transform.GetChild(i).gameObject.SetLayerRecursive(layer);
			}
		}
	}
}
