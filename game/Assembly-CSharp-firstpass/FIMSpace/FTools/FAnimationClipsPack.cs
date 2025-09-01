using System;
using UnityEngine;

namespace FIMSpace.FTools
{
	// Token: 0x0200004D RID: 77
	[CreateAssetMenu(fileName = "Animation Clips Pack", menuName = "FImpossible Creations/Utilities/Animation Clips Pack", order = 400)]
	public class FAnimationClipsPack : FContainerBase
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0001228C File Offset: 0x0001048C
		public override void AddAsset(UnityEngine.Object obj)
		{
			if (obj == null)
			{
				return;
			}
			bool flag = false;
			if (obj is AnimationClip)
			{
				flag = true;
			}
			if (!flag)
			{
				string str = "[Animation Clips Pack] Wrong asset type! You're trying to add '";
				Type type = obj.GetType();
				Debug.Log(str + ((type != null) ? type.ToString() : null) + "'!'");
				return;
			}
			base.AddAsset(obj);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000122E0 File Offset: 0x000104E0
		public FAnimationClipsPack()
		{
		}
	}
}
