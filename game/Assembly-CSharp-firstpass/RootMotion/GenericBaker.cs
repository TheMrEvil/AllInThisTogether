using System;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000A8 RID: 168
	public class GenericBaker : Baker
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x00034A90 File Offset: 0x00032C90
		private void Awake()
		{
			Transform[] componentsInChildren = this.root.GetComponentsInChildren<Transform>();
			this.children = new BakerTransform[0];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (!this.IsIgnored(componentsInChildren[i]))
				{
					Array.Resize<BakerTransform>(ref this.children, this.children.Length + 1);
					bool flag = componentsInChildren[i] == this.rootNode;
					if (flag)
					{
						this.rootChildIndex = this.children.Length - 1;
					}
					this.children[this.children.Length - 1] = new BakerTransform(componentsInChildren[i], this.root, this.BakePosition(componentsInChildren[i]), flag);
				}
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00034B2E File Offset: 0x00032D2E
		protected override Transform GetCharacterRoot()
		{
			return this.root;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00034B38 File Offset: 0x00032D38
		protected override void OnStartBaking()
		{
			for (int i = 0; i < this.children.Length; i++)
			{
				this.children[i].Reset();
				if (i == this.rootChildIndex)
				{
					this.children[i].SetRelativeSpace(this.root.position, this.root.rotation);
				}
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00034B94 File Offset: 0x00032D94
		protected override void OnSetLoopFrame(float time)
		{
			for (int i = 0; i < this.children.Length; i++)
			{
				this.children[i].AddLoopFrame(time);
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00034BC4 File Offset: 0x00032DC4
		protected override void OnSetCurves(ref AnimationClip clip)
		{
			for (int i = 0; i < this.children.Length; i++)
			{
				this.children[i].SetCurves(ref clip);
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00034BF4 File Offset: 0x00032DF4
		protected override void OnSetKeyframes(float time, bool lastFrame)
		{
			for (int i = 0; i < this.children.Length; i++)
			{
				this.children[i].SetKeyframes(time);
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00034C24 File Offset: 0x00032E24
		private bool IsIgnored(Transform t)
		{
			for (int i = 0; i < this.ignoreList.Length; i++)
			{
				if (t == this.ignoreList[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00034C58 File Offset: 0x00032E58
		private bool BakePosition(Transform t)
		{
			for (int i = 0; i < this.bakePositionList.Length; i++)
			{
				if (t == this.bakePositionList[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00034C8B File Offset: 0x00032E8B
		public GenericBaker()
		{
		}

		// Token: 0x04000613 RID: 1555
		[Tooltip("If true, produced AnimationClips will be marked as Legacy and usable with the Legacy animation system.")]
		public bool markAsLegacy;

		// Token: 0x04000614 RID: 1556
		[Tooltip("Root Transform of the hierarchy to bake.")]
		public Transform root;

		// Token: 0x04000615 RID: 1557
		[Tooltip("Root Node used for root motion.")]
		public Transform rootNode;

		// Token: 0x04000616 RID: 1558
		[Tooltip("List of Transforms to ignore, rotation curves will not be baked for these Transforms.")]
		public Transform[] ignoreList;

		// Token: 0x04000617 RID: 1559
		[Tooltip("LocalPosition curves will be baked for these Transforms only. If you are baking a character, the pelvis bone should be added to this array.")]
		public Transform[] bakePositionList;

		// Token: 0x04000618 RID: 1560
		private BakerTransform[] children = new BakerTransform[0];

		// Token: 0x04000619 RID: 1561
		private BakerTransform rootChild;

		// Token: 0x0400061A RID: 1562
		private int rootChildIndex = -1;
	}
}
