using System;

namespace UnityEngine
{
	// Token: 0x0200003D RID: 61
	internal struct SliderHandler
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000F600 File Offset: 0x0000D800
		public SliderHandler(Rect position, float currentValue, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id, GUIStyle thumbExtent = null)
		{
			this.position = position;
			this.currentValue = currentValue;
			this.size = size;
			this.start = start;
			this.end = end;
			this.slider = slider;
			this.thumb = thumb;
			this.thumbExtent = thumbExtent;
			this.horiz = horiz;
			this.id = id;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000F65C File Offset: 0x0000D85C
		public float Handle()
		{
			bool flag = this.slider == null || this.thumb == null;
			float result;
			if (flag)
			{
				result = this.currentValue;
			}
			else
			{
				EventType eventType = this.CurrentEventType();
				EventType eventType2 = eventType;
				switch (eventType2)
				{
				case EventType.MouseDown:
					return this.OnMouseDown();
				case EventType.MouseUp:
					return this.OnMouseUp();
				case EventType.MouseMove:
					break;
				case EventType.MouseDrag:
					return this.OnMouseDrag();
				default:
					if (eventType2 == EventType.Repaint)
					{
						return this.OnRepaint();
					}
					break;
				}
				result = this.currentValue;
			}
			return result;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000F6E0 File Offset: 0x0000D8E0
		private float OnMouseDown()
		{
			Rect rect = this.ThumbSelectionRect();
			bool flag = GUIUtility.HitTest(rect, this.CurrentEvent());
			Rect zero = Rect.zero;
			zero.xMin = Math.Min(this.position.xMin, rect.xMin);
			zero.xMax = Math.Max(this.position.xMax, rect.xMax);
			zero.yMin = Math.Min(this.position.yMin, rect.yMin);
			zero.yMax = Math.Max(this.position.yMax, rect.yMax);
			bool flag2 = this.IsEmptySlider() || (!GUIUtility.HitTest(zero, this.CurrentEvent()) && !flag);
			float result;
			if (flag2)
			{
				result = this.currentValue;
			}
			else
			{
				GUI.scrollTroughSide = 0;
				GUIUtility.hotControl = this.id;
				this.CurrentEvent().Use();
				bool flag3 = flag;
				if (flag3)
				{
					this.StartDraggingWithValue(this.ClampedCurrentValue());
					result = this.currentValue;
				}
				else
				{
					GUI.changed = true;
					bool flag4 = this.SupportsPageMovements();
					if (flag4)
					{
						this.SliderState().isDragging = false;
						GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(250.0);
						GUI.scrollTroughSide = this.CurrentScrollTroughSide();
						result = this.PageMovementValue();
					}
					else
					{
						float num = this.ValueForCurrentMousePosition();
						this.StartDraggingWithValue(num);
						result = this.Clamp(num);
					}
				}
			}
			return result;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000F874 File Offset: 0x0000DA74
		private float OnMouseDrag()
		{
			bool flag = GUIUtility.hotControl != this.id;
			float result;
			if (flag)
			{
				result = this.currentValue;
			}
			else
			{
				SliderState sliderState = this.SliderState();
				bool flag2 = !sliderState.isDragging;
				if (flag2)
				{
					result = this.currentValue;
				}
				else
				{
					GUI.changed = true;
					this.CurrentEvent().Use();
					float num = this.MousePosition() - sliderState.dragStartPos;
					float value = sliderState.dragStartValue + num / this.ValuesPerPixel();
					result = this.Clamp(value);
				}
			}
			return result;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000F900 File Offset: 0x0000DB00
		private float OnMouseUp()
		{
			bool flag = GUIUtility.hotControl == this.id;
			if (flag)
			{
				this.CurrentEvent().Use();
				GUIUtility.hotControl = 0;
			}
			return this.currentValue;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000F940 File Offset: 0x0000DB40
		private float OnRepaint()
		{
			bool flag = GUIUtility.HitTest(this.position, this.CurrentEvent());
			this.slider.Draw(this.position, GUIContent.none, this.id, false, flag);
			bool flag2 = this.currentValue >= Mathf.Min(this.start, this.end) && this.currentValue <= Mathf.Max(this.start, this.end);
			if (flag2)
			{
				bool flag3 = this.thumbExtent != null;
				if (flag3)
				{
					this.thumbExtent.Draw(this.ThumbExtRect(), GUIContent.none, this.id, false, flag);
				}
				this.thumb.Draw(this.ThumbRect(), GUIContent.none, this.id, false, flag);
			}
			bool flag4 = GUIUtility.hotControl != this.id || !flag || this.IsEmptySlider();
			float result;
			if (flag4)
			{
				result = this.currentValue;
			}
			else
			{
				bool flag5 = GUIUtility.HitTest(this.ThumbRect(), this.CurrentEvent());
				if (flag5)
				{
					bool flag6 = GUI.scrollTroughSide != 0;
					if (flag6)
					{
						GUIUtility.hotControl = 0;
					}
					result = this.currentValue;
				}
				else
				{
					GUI.InternalRepaintEditorWindow();
					bool flag7 = SystemClock.now < GUI.nextScrollStepTime;
					if (flag7)
					{
						result = this.currentValue;
					}
					else
					{
						bool flag8 = this.CurrentScrollTroughSide() != GUI.scrollTroughSide;
						if (flag8)
						{
							result = this.currentValue;
						}
						else
						{
							GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(30.0);
							bool flag9 = this.SupportsPageMovements();
							if (flag9)
							{
								this.SliderState().isDragging = false;
								GUI.changed = true;
								result = this.PageMovementValue();
							}
							else
							{
								result = this.ClampedCurrentValue();
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000FB08 File Offset: 0x0000DD08
		private EventType CurrentEventType()
		{
			return this.CurrentEvent().GetTypeForControl(this.id);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000FB2C File Offset: 0x0000DD2C
		private int CurrentScrollTroughSide()
		{
			float num = this.horiz ? this.CurrentEvent().mousePosition.x : this.CurrentEvent().mousePosition.y;
			float num2 = this.horiz ? this.ThumbRect().x : this.ThumbRect().y;
			return (num > num2) ? 1 : -1;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000FB98 File Offset: 0x0000DD98
		private bool IsEmptySlider()
		{
			return this.start == this.end;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
		private bool SupportsPageMovements()
		{
			return this.size != 0f && GUI.usePageScrollbars;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		private float PageMovementValue()
		{
			float num = this.currentValue;
			int num2 = (this.start > this.end) ? -1 : 1;
			bool flag = this.MousePosition() > this.PageUpMovementBound();
			if (flag)
			{
				num += this.size * (float)num2 * 0.9f;
			}
			else
			{
				num -= this.size * (float)num2 * 0.9f;
			}
			return this.Clamp(num);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		private float PageUpMovementBound()
		{
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = this.ThumbRect().xMax - this.position.x;
			}
			else
			{
				result = this.ThumbRect().yMax - this.position.y;
			}
			return result;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		private Event CurrentEvent()
		{
			return Event.current;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		private float ValueForCurrentMousePosition()
		{
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = (this.MousePosition() - this.ThumbRect().width * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			else
			{
				result = (this.MousePosition() - this.ThumbRect().height * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			return result;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		private float Clamp(float value)
		{
			return Mathf.Clamp(value, this.MinValue(), this.MaxValue());
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000FD70 File Offset: 0x0000DF70
		private Rect ThumbSelectionRect()
		{
			return this.ThumbRect();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000FD8C File Offset: 0x0000DF8C
		private void StartDraggingWithValue(float dragStartValue)
		{
			SliderState sliderState = this.SliderState();
			sliderState.dragStartPos = this.MousePosition();
			sliderState.dragStartValue = dragStartValue;
			sliderState.isDragging = true;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		private SliderState SliderState()
		{
			return (SliderState)GUIUtility.GetStateObject(typeof(SliderState), this.id);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
		private Rect ThumbExtRect()
		{
			return new Rect(0f, 0f, this.thumbExtent.fixedWidth, this.thumbExtent.fixedHeight)
			{
				center = this.ThumbRect().center
			};
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000FE3C File Offset: 0x0000E03C
		private Rect ThumbRect()
		{
			return this.horiz ? this.HorizontalThumbRect() : this.VerticalThumbRect();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000FE64 File Offset: 0x0000E064
		private Rect VerticalThumbRect()
		{
			Rect rect = this.thumb.margin.Remove(this.slider.padding.Remove(this.position));
			float width = (this.thumb.fixedWidth != 0f) ? this.thumb.fixedWidth : rect.width;
			float num = this.ThumbSize();
			float num2 = this.ValuesPerPixel();
			bool flag = this.start < this.end;
			Rect result;
			if (flag)
			{
				result = new Rect(rect.x, (this.ClampedCurrentValue() - this.start) * num2 + rect.y, width, this.size * num2 + num);
			}
			else
			{
				result = new Rect(rect.x, (this.ClampedCurrentValue() + this.size - this.start) * num2 + rect.y, width, this.size * -num2 + num);
			}
			return result;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000FF54 File Offset: 0x0000E154
		private Rect HorizontalThumbRect()
		{
			Rect rect = this.thumb.margin.Remove(this.slider.padding.Remove(this.position));
			float height = (this.thumb.fixedHeight != 0f) ? this.thumb.fixedHeight : rect.height;
			float num = this.ThumbSize();
			float num2 = this.ValuesPerPixel();
			bool flag = this.start < this.end;
			Rect result;
			if (flag)
			{
				result = new Rect((this.ClampedCurrentValue() - this.start) * num2 + rect.x, rect.y, this.size * num2 + num, height);
			}
			else
			{
				result = new Rect((this.ClampedCurrentValue() + this.size - this.start) * num2 + rect.x, rect.y, this.size * -num2 + num, height);
			}
			return result;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00010040 File Offset: 0x0000E240
		private float ClampedCurrentValue()
		{
			return this.Clamp(this.currentValue);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00010060 File Offset: 0x0000E260
		private float MousePosition()
		{
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = this.CurrentEvent().mousePosition.x - this.position.x;
			}
			else
			{
				result = this.CurrentEvent().mousePosition.y - this.position.y;
			}
			return result;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000100C0 File Offset: 0x0000E2C0
		private float ValuesPerPixel()
		{
			float num = (this.end == this.start) ? 1f : (this.end - this.start);
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = (this.position.width - (float)this.slider.padding.horizontal - this.ThumbSize()) / num;
			}
			else
			{
				result = (this.position.height - (float)this.slider.padding.vertical - this.ThumbSize()) / num;
			}
			return result;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00010154 File Offset: 0x0000E354
		private float ThumbSize()
		{
			bool flag = this.horiz;
			float result;
			if (flag)
			{
				result = ((this.thumb.fixedWidth != 0f) ? this.thumb.fixedWidth : ((float)this.thumb.padding.horizontal));
			}
			else
			{
				result = ((this.thumb.fixedHeight != 0f) ? this.thumb.fixedHeight : ((float)this.thumb.padding.vertical));
			}
			return result;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000101D4 File Offset: 0x0000E3D4
		private float MaxValue()
		{
			return Mathf.Max(this.start, this.end) - this.size;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00010200 File Offset: 0x0000E400
		private float MinValue()
		{
			return Mathf.Min(this.start, this.end);
		}

		// Token: 0x04000133 RID: 307
		private readonly Rect position;

		// Token: 0x04000134 RID: 308
		private readonly float currentValue;

		// Token: 0x04000135 RID: 309
		private readonly float size;

		// Token: 0x04000136 RID: 310
		private readonly float start;

		// Token: 0x04000137 RID: 311
		private readonly float end;

		// Token: 0x04000138 RID: 312
		private readonly GUIStyle slider;

		// Token: 0x04000139 RID: 313
		private readonly GUIStyle thumb;

		// Token: 0x0400013A RID: 314
		private readonly GUIStyle thumbExtent;

		// Token: 0x0400013B RID: 315
		private readonly bool horiz;

		// Token: 0x0400013C RID: 316
		private readonly int id;
	}
}
