using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006F RID: 111
	public class MousePositionDebug
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0001126E File Offset: 0x0000F46E
		public static MousePositionDebug instance
		{
			get
			{
				if (MousePositionDebug.s_Instance == null)
				{
					MousePositionDebug.s_Instance = new MousePositionDebug();
				}
				return MousePositionDebug.s_Instance;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00011286 File Offset: 0x0000F486
		public void Build()
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011288 File Offset: 0x0000F488
		public void Cleanup()
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001128A File Offset: 0x0000F48A
		public Vector2 GetMousePosition(float ScreenHeight, bool sceneView)
		{
			return this.GetInputMousePosition();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011292 File Offset: 0x0000F492
		private Vector2 GetInputMousePosition()
		{
			return Input.mousePosition;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001129E File Offset: 0x0000F49E
		public Vector2 GetMouseClickPosition(float ScreenHeight)
		{
			return Vector2.zero;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000112A5 File Offset: 0x0000F4A5
		public MousePositionDebug()
		{
		}

		// Token: 0x0400024B RID: 587
		private static MousePositionDebug s_Instance;
	}
}
