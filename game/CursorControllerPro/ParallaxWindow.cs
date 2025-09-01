using System;
using UnityEngine;

namespace SlimUI.CursorControllerPro
{
	// Token: 0x0200000B RID: 11
	public class ParallaxWindow : MonoBehaviour
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00003614 File Offset: 0x00001814
		private void Start()
		{
			if (this.autoFindController)
			{
				this.cursorController = GameObject.Find("CursorControl").GetComponent<CursorController>();
			}
			this.movingCanvas = base.GetComponent<RectTransform>();
			this.cursorRect = this.cursorController.cursorRect;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003650 File Offset: 0x00001850
		private void Update()
		{
			if (this.cursorRect != null)
			{
				this.movingCanvas.anchoredPosition = this.cursorRect.anchoredPosition * -this.cursorController.parallaxStrength;
				return;
			}
			Debug.LogWarning("Cursor Rect is missing! Cannot Parallax Window.");
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000369D File Offset: 0x0000189D
		public ParallaxWindow()
		{
		}

		// Token: 0x04000055 RID: 85
		[Header("CursorControl Scene Object")]
		public CursorController cursorController;

		// Token: 0x04000056 RID: 86
		[Tooltip("If you're using a prefab, the cursorController variable will automatically assign itself. In some cases, a window will be spawned at runtime and needs to find the cursor controller parent and assign it automatically.")]
		public bool autoFindController;

		// Token: 0x04000057 RID: 87
		private RectTransform cursorRect;

		// Token: 0x04000058 RID: 88
		private Vector2 center = new Vector2(0f, 0f);

		// Token: 0x04000059 RID: 89
		private RectTransform movingCanvas;
	}
}
