using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004B RID: 75
	[AddComponentMenu("UI Toolkit/Panel Raycaster (UI Toolkit)")]
	public class PanelRaycaster : BaseRaycaster, IRuntimePanelComponent
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00017A89 File Offset: 0x00015C89
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00017A94 File Offset: 0x00015C94
		public IPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)value;
				if (this.m_Panel != baseRuntimePanel)
				{
					this.UnregisterCallbacks();
					this.m_Panel = baseRuntimePanel;
					this.RegisterCallbacks();
				}
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00017AC4 File Offset: 0x00015CC4
		private void RegisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed += this.OnPanelDestroyed;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00017AE5 File Offset: 0x00015CE5
		private void UnregisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed -= this.OnPanelDestroyed;
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00017B06 File Offset: 0x00015D06
		private void OnPanelDestroyed()
		{
			this.panel = null;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00017B0F File Offset: 0x00015D0F
		private GameObject selectableGameObject
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel == null)
				{
					return null;
				}
				return panel.selectableGameObject;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00017B22 File Offset: 0x00015D22
		public override int sortOrderPriority
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				return (int)((panel != null) ? panel.sortingPriority : 0f);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00017B3B File Offset: 0x00015D3B
		public override int renderOrderPriority
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				return PanelRaycaster.ConvertFloatBitsToInt((panel != null) ? panel.sortingPriority : 0f);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00017B58 File Offset: 0x00015D58
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			Vector3 vector = Display.RelativeMouseAt(eventData.position);
			int targetDisplay = this.m_Panel.targetDisplay;
			if (vector != Vector3.zero)
			{
				if ((int)vector.z != targetDisplay)
				{
					return;
				}
			}
			else
			{
				vector = eventData.position;
			}
			Vector3 vector2 = vector;
			Vector2 delta = eventData.delta;
			float num = (float)Screen.height;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				num = (float)Display.displays[targetDisplay].systemHeight;
			}
			vector2.y = num - vector2.y;
			delta.y = -delta.y;
			EventSystem eventSystem = UIElementsRuntimeUtility.activeEventSystem as EventSystem;
			if (eventSystem == null || eventSystem.currentInputModule == null)
			{
				return;
			}
			int pointerId = eventSystem.currentInputModule.ConvertUIToolkitPointerId(eventData);
			IEventHandler capturingElement = this.m_Panel.GetCapturingElement(pointerId);
			VisualElement visualElement = capturingElement as VisualElement;
			if (visualElement != null && visualElement.panel != this.m_Panel)
			{
				return;
			}
			IPanel panel = (PointerDeviceState.GetPressedButtons(pointerId) != 0) ? PointerDeviceState.GetPlayerPanelWithSoftPointerCapture(pointerId) : null;
			if (panel != null && panel != this.m_Panel)
			{
				return;
			}
			if (capturingElement == null && panel == null)
			{
				Vector2 point;
				Vector2 vector3;
				if (!this.m_Panel.ScreenToPanel(vector2, delta, out point, out vector3, false))
				{
					return;
				}
				if (this.m_Panel.Pick(point) == null)
				{
					return;
				}
			}
			resultAppendList.Add(new RaycastResult
			{
				gameObject = this.selectableGameObject,
				module = this,
				screenPosition = vector,
				displayIndex = this.m_Panel.targetDisplay
			});
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00017CF6 File Offset: 0x00015EF6
		public override Camera eventCamera
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00017CFC File Offset: 0x00015EFC
		private static int ConvertFloatBitsToInt(float f)
		{
			return new PanelRaycaster.FloatIntBits
			{
				f = f
			}.i;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00017D1F File Offset: 0x00015F1F
		public PanelRaycaster()
		{
		}

		// Token: 0x040001AA RID: 426
		private BaseRuntimePanel m_Panel;

		// Token: 0x020000BE RID: 190
		[StructLayout(LayoutKind.Explicit, Size = 4)]
		private struct FloatIntBits
		{
			// Token: 0x04000336 RID: 822
			[FieldOffset(0)]
			public float f;

			// Token: 0x04000337 RID: 823
			[FieldOffset(0)]
			public int i;
		}
	}
}
