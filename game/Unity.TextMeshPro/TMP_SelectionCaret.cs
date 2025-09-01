using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200004E RID: 78
	[RequireComponent(typeof(CanvasRenderer))]
	public class TMP_SelectionCaret : MaskableGraphic
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00024AC0 File Offset: 0x00022CC0
		public override void Cull(Rect clipRect, bool validRect)
		{
			if (validRect)
			{
				base.canvasRenderer.cull = false;
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
				return;
			}
			base.Cull(clipRect, validRect);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00024AE0 File Offset: 0x00022CE0
		protected override void UpdateGeometry()
		{
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00024AE2 File Offset: 0x00022CE2
		public TMP_SelectionCaret()
		{
		}
	}
}
