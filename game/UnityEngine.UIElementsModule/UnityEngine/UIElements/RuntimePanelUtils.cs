using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000250 RID: 592
	public static class RuntimePanelUtils
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x000469C4 File Offset: 0x00044BC4
		public static Vector2 ScreenToPanel(IPanel panel, Vector2 screenPosition)
		{
			return ((BaseRuntimePanel)panel).ScreenToPanel(screenPosition);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000469E4 File Offset: 0x00044BE4
		public static Vector2 CameraTransformWorldToPanel(IPanel panel, Vector3 worldPosition, Camera camera)
		{
			Vector2 vector = camera.WorldToScreenPoint(worldPosition);
			vector.y = (float)Screen.height - vector.y;
			return ((BaseRuntimePanel)panel).ScreenToPanel(vector);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00046A24 File Offset: 0x00044C24
		public static Rect CameraTransformWorldToPanelRect(IPanel panel, Vector3 worldPosition, Vector2 worldSize, Camera camera)
		{
			worldSize.y = -worldSize.y;
			Vector2 vector = RuntimePanelUtils.CameraTransformWorldToPanel(panel, worldPosition, camera);
			Vector3 worldPosition2 = worldPosition + camera.worldToCameraMatrix.MultiplyVector(worldSize);
			Vector2 a = RuntimePanelUtils.CameraTransformWorldToPanel(panel, worldPosition2, camera);
			return new Rect(vector, a - vector);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00046A80 File Offset: 0x00044C80
		public static void ResetDynamicAtlas(this IPanel panel)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel == null;
			if (!flag)
			{
				DynamicAtlas dynamicAtlas = baseVisualElementPanel.atlas as DynamicAtlas;
				if (dynamicAtlas != null)
				{
					dynamicAtlas.Reset();
				}
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00046AB8 File Offset: 0x00044CB8
		public static void SetTextureDirty(this IPanel panel, Texture2D texture)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel == null;
			if (!flag)
			{
				DynamicAtlas dynamicAtlas = baseVisualElementPanel.atlas as DynamicAtlas;
				if (dynamicAtlas != null)
				{
					dynamicAtlas.SetDirty(texture);
				}
			}
		}
	}
}
