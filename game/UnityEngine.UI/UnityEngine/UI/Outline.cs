using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
	// Token: 0x02000042 RID: 66
	[AddComponentMenu("UI/Effects/Outline", 81)]
	public class Outline : Shadow
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x00016788 File Offset: 0x00014988
		protected Outline()
		{
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00016790 File Offset: 0x00014990
		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			List<UIVertex> list = CollectionPool<List<UIVertex>, UIVertex>.Get();
			vh.GetUIVertexStream(list);
			int num = list.Count * 5;
			if (list.Capacity < num)
			{
				list.Capacity = num;
			}
			int start = 0;
			int count = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, base.effectDistance.x, base.effectDistance.y);
			start = count;
			int count2 = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, base.effectDistance.x, -base.effectDistance.y);
			start = count2;
			int count3 = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, -base.effectDistance.x, base.effectDistance.y);
			start = count3;
			int count4 = list.Count;
			base.ApplyShadowZeroAlloc(list, base.effectColor, start, list.Count, -base.effectDistance.x, -base.effectDistance.y);
			vh.Clear();
			vh.AddUIVertexTriangleStream(list);
			CollectionPool<List<UIVertex>, UIVertex>.Release(list);
		}
	}
}
