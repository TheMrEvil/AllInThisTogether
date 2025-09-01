using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LeTai.TrueShadow
{
	// Token: 0x02000009 RID: 9
	[RequireComponent(typeof(TrueShadow))]
	public class InteractiveShadow : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002A80 File Offset: 0x00000C80
		private void OnEnable()
		{
			this.shadow = this.FindTrueShadow();
			this.selectable = base.GetComponent<Selectable>();
			this.isHovered = false;
			if (Input.mousePresent)
			{
				this.isHovered = this.IsOverGameObject(Input.mousePosition);
			}
			if (!this.isHovered)
			{
				for (int i = 0; i < Input.touchCount; i++)
				{
					this.isHovered = this.IsOverGameObject(Input.GetTouch(i).position);
					if (this.isHovered)
					{
						break;
					}
				}
			}
			this.isSelected = (!this.autoDeselect && EventSystem.current.currentSelectedGameObject == base.gameObject);
			this.isClicked = false;
			if (!this.normalStateAcquired)
			{
				this.targetSize = (this.normalSize = this.shadow.Size);
				this.targetDistance = (this.normalDistance = this.shadow.OffsetDistance);
				this.targetColor = (this.normalColor = this.shadow.Color);
				this.normalStateAcquired = true;
			}
			this.shadow.Size = (this.targetSize = this.normalSize);
			this.shadow.OffsetDistance = (this.targetDistance = this.normalDistance);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002BC4 File Offset: 0x00000DC4
		private TrueShadow FindTrueShadow()
		{
			TrueShadow[] components = base.GetComponents<TrueShadow>();
			if (components.Length == 0)
			{
				return null;
			}
			InteractiveShadow[] components2 = base.GetComponents<InteractiveShadow>();
			int num = 0;
			while (num < components2.Length && !(components2[num] == this))
			{
				num++;
			}
			return components[num];
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C04 File Offset: 0x00000E04
		private void OnStateChange()
		{
			if (this.isClicked)
			{
				this.targetSize = this.clickedSize;
				this.targetDistance = this.clickedDistance;
				this.targetColor = this.clickedColor;
				return;
			}
			if (this.isSelected)
			{
				this.targetSize = this.selectedSize;
				this.targetDistance = this.selectedDistance;
				this.targetColor = this.selectedColor;
				return;
			}
			if (this.isHovered)
			{
				this.targetSize = this.hoverSize;
				this.targetDistance = this.hoverDistance;
				this.targetColor = this.hoverColor;
				return;
			}
			this.targetSize = this.normalSize;
			this.targetDistance = this.normalDistance;
			this.targetColor = this.normalColor;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CBC File Offset: 0x00000EBC
		private void Update()
		{
			if (!Mathf.Approximately(this.targetSize, this.shadow.Size))
			{
				this.shadow.Size = Mathf.SmoothDamp(this.shadow.Size, this.targetSize, ref this.currentSizeVelocity, this.smoothTime);
			}
			if (!Mathf.Approximately(this.targetDistance, this.shadow.OffsetDistance))
			{
				this.shadow.OffsetDistance = Mathf.SmoothDamp(this.shadow.OffsetDistance, this.targetDistance, ref this.currentDistanceVelocity, this.smoothTime);
			}
			Color color = this.shadow.Color;
			if (!Mathf.Approximately(this.targetColor.a, color.a))
			{
				float r = Mathf.SmoothDamp(color.r, this.targetColor.r, ref this.currentColorRVelocity, this.smoothTime);
				float g = Mathf.SmoothDamp(color.g, this.targetColor.g, ref this.currentColorGVelocity, this.smoothTime);
				float b = Mathf.SmoothDamp(color.b, this.targetColor.b, ref this.currentColorBVelocity, this.smoothTime);
				float a = Mathf.SmoothDamp(color.a, this.targetColor.a, ref this.currentColorAVelocity, this.smoothTime);
				this.shadow.Color = new Color(r, g, b, a);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002E1C File Offset: 0x0000101C
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.isHovered = true;
			this.OnStateChange();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002E2B File Offset: 0x0000102B
		public void OnPointerExit(PointerEventData eventData)
		{
			this.isHovered = false;
			this.OnStateChange();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E3A File Offset: 0x0000103A
		public void OnSelect(BaseEventData eventData)
		{
			this.isSelected = true;
			this.OnStateChange();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002E49 File Offset: 0x00001049
		public void OnDeselect(BaseEventData eventData)
		{
			this.isSelected = false;
			this.OnStateChange();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002E58 File Offset: 0x00001058
		public void OnPointerDown(PointerEventData eventData)
		{
			this.isClicked = true;
			this.OnStateChange();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002E67 File Offset: 0x00001067
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.autoDeselect && EventSystem.current.currentSelectedGameObject == base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			this.isClicked = false;
			this.OnStateChange();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002EA0 File Offset: 0x000010A0
		private bool IsOverGameObject(Vector2 position)
		{
			PointerEventData eventData = new PointerEventData(EventSystem.current)
			{
				position = position
			};
			EventSystem.current.RaycastAll(eventData, this.raycastResults);
			for (int i = 0; i < this.raycastResults.Count; i++)
			{
				if (this.raycastResults[i].gameObject == base.gameObject)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F0C File Offset: 0x0000110C
		public InteractiveShadow()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002FD4 File Offset: 0x000011D4
		// Note: this type is marked as 'beforefieldinit'.
		static InteractiveShadow()
		{
		}

		// Token: 0x0400001B RID: 27
		public float smoothTime = 0.05f;

		// Token: 0x0400001C RID: 28
		[Tooltip("Deselect on pointer up")]
		public bool autoDeselect;

		// Token: 0x0400001D RID: 29
		[Header("Size")]
		public float selectedSize = 28f;

		// Token: 0x0400001E RID: 30
		public float hoverSize = 28f;

		// Token: 0x0400001F RID: 31
		public float clickedSize = 24f;

		// Token: 0x04000020 RID: 32
		[Header("Distance")]
		public float selectedDistance = 12f;

		// Token: 0x04000021 RID: 33
		public float hoverDistance = 12f;

		// Token: 0x04000022 RID: 34
		public float clickedDistance = 8f;

		// Token: 0x04000023 RID: 35
		[Header("Color")]
		public Color selectedColor = new Color(0f, 0f, 0f, 0.25f);

		// Token: 0x04000024 RID: 36
		public Color hoverColor = new Color(0f, 0f, 0f, 0.2f);

		// Token: 0x04000025 RID: 37
		public Color clickedColor = new Color(0f, 0f, 0f, 0.25f);

		// Token: 0x04000026 RID: 38
		private float normalSize;

		// Token: 0x04000027 RID: 39
		private float normalDistance;

		// Token: 0x04000028 RID: 40
		private Color normalColor;

		// Token: 0x04000029 RID: 41
		private bool normalStateAcquired;

		// Token: 0x0400002A RID: 42
		private bool isSelected;

		// Token: 0x0400002B RID: 43
		private bool isHovered;

		// Token: 0x0400002C RID: 44
		private bool isClicked;

		// Token: 0x0400002D RID: 45
		private TrueShadow shadow;

		// Token: 0x0400002E RID: 46
		private Selectable selectable;

		// Token: 0x0400002F RID: 47
		private float targetSize;

		// Token: 0x04000030 RID: 48
		private float targetDistance;

		// Token: 0x04000031 RID: 49
		private Color targetColor;

		// Token: 0x04000032 RID: 50
		private static readonly Color FADED_COLOR = new Color(0.5f, 0.5f, 0.5f, 0.5f);

		// Token: 0x04000033 RID: 51
		private readonly List<RaycastResult> raycastResults = new List<RaycastResult>();

		// Token: 0x04000034 RID: 52
		private float currentSizeVelocity;

		// Token: 0x04000035 RID: 53
		private float currentDistanceVelocity;

		// Token: 0x04000036 RID: 54
		private float currentColorRVelocity;

		// Token: 0x04000037 RID: 55
		private float currentColorGVelocity;

		// Token: 0x04000038 RID: 56
		private float currentColorBVelocity;

		// Token: 0x04000039 RID: 57
		private float currentColorAVelocity;
	}
}
