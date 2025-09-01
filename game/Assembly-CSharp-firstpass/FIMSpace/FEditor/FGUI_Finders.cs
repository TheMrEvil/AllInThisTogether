using System;
using System.Collections.Generic;
using UnityEngine;

namespace FIMSpace.FEditor
{
	// Token: 0x02000045 RID: 69
	public static class FGUI_Finders
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x0000F5C1 File Offset: 0x0000D7C1
		public static void ResetFinders(bool resetClicks = true)
		{
			FGUI_Finders.checkForAnim = true;
			FGUI_Finders.FoundAnimator = null;
			if (resetClicks)
			{
				FGUI_Finders.clicks = 0;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000F5D8 File Offset: 0x0000D7D8
		public static bool CheckForAnimator(GameObject root, bool needAnimatorBox = true, bool drawInactiveWarning = true, int clicksTohide = 1)
		{
			bool flag = false;
			if (FGUI_Finders.checkForAnim)
			{
				FGUI_Finders.FoundAnimator = FGUI_Finders.SearchForParentWithAnimator(root);
			}
			if (FGUI_Finders.FoundAnimator)
			{
				Animation animation = FGUI_Finders.FoundAnimator as Animation;
				Animator animator = FGUI_Finders.FoundAnimator as Animator;
				if (animation && animation.enabled)
				{
					flag = true;
				}
				if (animator)
				{
					if (animator.enabled)
					{
						flag = true;
					}
					if (animator.runtimeAnimatorController == null)
					{
						drawInactiveWarning = false;
						flag = false;
					}
				}
				if (needAnimatorBox && drawInactiveWarning && !flag)
				{
				}
			}
			else if (needAnimatorBox)
			{
				int num = FGUI_Finders.clicks;
			}
			FGUI_Finders.checkForAnim = false;
			return flag;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000F670 File Offset: 0x0000D870
		public static Component SearchForParentWithAnimator(GameObject root)
		{
			Animation animation = root.GetComponentInChildren<Animation>();
			if (animation)
			{
				return animation;
			}
			Animator animator = root.GetComponentInChildren<Animator>();
			if (animator)
			{
				return animator;
			}
			if (root.transform.parent != null)
			{
				Transform parent = root.transform.parent;
				while (parent != null)
				{
					animation = parent.GetComponent<Animation>();
					if (animation)
					{
						return animation;
					}
					animator = parent.GetComponent<Animator>();
					if (animator)
					{
						return animator;
					}
					parent = parent.parent;
				}
			}
			return null;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		public static SkinnedMeshRenderer GetBoneSearchArray(Transform root)
		{
			List<SkinnedMeshRenderer> list = new List<SkinnedMeshRenderer>();
			SkinnedMeshRenderer skinnedMeshRenderer = null;
			Transform[] componentsInChildren = root.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				SkinnedMeshRenderer component = componentsInChildren[i].GetComponent<SkinnedMeshRenderer>();
				if (component)
				{
					list.Add(component);
				}
			}
			if (list.Count == 0)
			{
				Transform transform = root;
				while (transform != null && !(transform.parent == null))
				{
					transform = transform.parent;
				}
				componentsInChildren = transform.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					SkinnedMeshRenderer component2 = componentsInChildren[i].GetComponent<SkinnedMeshRenderer>();
					if (!list.Contains(component2) && component2)
					{
						list.Add(component2);
					}
				}
			}
			if (list.Count > 1)
			{
				skinnedMeshRenderer = list[0];
				for (int j = 1; j < list.Count; j++)
				{
					if (list[j].bones.Length > skinnedMeshRenderer.bones.Length)
					{
						skinnedMeshRenderer = list[j];
					}
				}
			}
			else if (list.Count > 0)
			{
				skinnedMeshRenderer = list[0];
			}
			if (skinnedMeshRenderer == null)
			{
				return null;
			}
			return skinnedMeshRenderer;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000F80C File Offset: 0x0000DA0C
		public static bool IsChildOf(Transform child, Transform rootParent)
		{
			Transform transform = child;
			while (transform != null && transform != rootParent)
			{
				transform = transform.parent;
			}
			return !(transform == null);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000F844 File Offset: 0x0000DA44
		public static Transform GetLastChild(Transform rootParent)
		{
			Transform transform = rootParent;
			while (transform.childCount > 0)
			{
				transform = transform.GetChild(0);
			}
			return transform;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000F868 File Offset: 0x0000DA68
		public static bool? IsRightOrLeft(string name, bool includeNotSure = false)
		{
			string text = name.ToLower();
			if (text.Contains("right"))
			{
				return new bool?(true);
			}
			if (text.Contains("left"))
			{
				return new bool?(false);
			}
			if (text.StartsWith("r_"))
			{
				return new bool?(true);
			}
			if (text.StartsWith("l_"))
			{
				return new bool?(false);
			}
			if (text.EndsWith("_r"))
			{
				return new bool?(true);
			}
			if (text.EndsWith("_l"))
			{
				return new bool?(false);
			}
			if (text.StartsWith("r."))
			{
				return new bool?(true);
			}
			if (text.StartsWith("l."))
			{
				return new bool?(false);
			}
			if (text.EndsWith(".r"))
			{
				return new bool?(true);
			}
			if (text.EndsWith(".l"))
			{
				return new bool?(false);
			}
			if (includeNotSure)
			{
				if (text.Contains("r_"))
				{
					return new bool?(true);
				}
				if (text.Contains("l_"))
				{
					return new bool?(false);
				}
				if (text.Contains("_r"))
				{
					return new bool?(true);
				}
				if (text.Contains("_l"))
				{
					return new bool?(false);
				}
				if (text.Contains("r."))
				{
					return new bool?(true);
				}
				if (text.Contains("l."))
				{
					return new bool?(false);
				}
				if (text.Contains(".r"))
				{
					return new bool?(true);
				}
				if (text.Contains(".l"))
				{
					return new bool?(false);
				}
			}
			return null;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		public static bool? IsRightOrLeft(Transform child, Transform itsRoot)
		{
			Vector3 vector = itsRoot.InverseTransformPoint(child.position);
			if (vector.x < 0f)
			{
				return new bool?(false);
			}
			if (vector.x > 0f)
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000FA40 File Offset: 0x0000DC40
		public static bool HaveKey(string text, string[] keys)
		{
			for (int i = 0; i < keys.Length; i++)
			{
				if (text.Contains(keys[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000FA69 File Offset: 0x0000DC69
		// Note: this type is marked as 'beforefieldinit'.
		static FGUI_Finders()
		{
		}

		// Token: 0x040001ED RID: 493
		public static Component FoundAnimator;

		// Token: 0x040001EE RID: 494
		private static bool checkForAnim = true;

		// Token: 0x040001EF RID: 495
		private static int clicks = 0;
	}
}
