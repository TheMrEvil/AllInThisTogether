using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000123 RID: 291
	public class FKOffset : MonoBehaviour
	{
		// Token: 0x06000C8B RID: 3211 RVA: 0x00054DBF File Offset: 0x00052FBF
		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00054DD0 File Offset: 0x00052FD0
		private void LateUpdate()
		{
			FKOffset.Offset[] array = this.offsets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Apply(this.animator);
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00054E00 File Offset: 0x00053000
		private void OnDrawGizmosSelected()
		{
			foreach (FKOffset.Offset offset in this.offsets)
			{
				offset.name = offset.bone.ToString();
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00054E3B File Offset: 0x0005303B
		public FKOffset()
		{
		}

		// Token: 0x040009D1 RID: 2513
		public FKOffset.Offset[] offsets;

		// Token: 0x040009D2 RID: 2514
		private Animator animator;

		// Token: 0x0200022B RID: 555
		[Serializable]
		public class Offset
		{
			// Token: 0x0600119A RID: 4506 RVA: 0x0006D5A8 File Offset: 0x0006B7A8
			public void Apply(Animator animator)
			{
				if (this.t == null)
				{
					this.t = animator.GetBoneTransform(this.bone);
				}
				if (this.t == null)
				{
					return;
				}
				this.t.localRotation *= Quaternion.Euler(this.rotationOffset);
			}

			// Token: 0x0600119B RID: 4507 RVA: 0x0006D605 File Offset: 0x0006B805
			public Offset()
			{
			}

			// Token: 0x04001070 RID: 4208
			[HideInInspector]
			public string name;

			// Token: 0x04001071 RID: 4209
			public HumanBodyBones bone;

			// Token: 0x04001072 RID: 4210
			public Vector3 rotationOffset;

			// Token: 0x04001073 RID: 4211
			private Transform t;
		}
	}
}
