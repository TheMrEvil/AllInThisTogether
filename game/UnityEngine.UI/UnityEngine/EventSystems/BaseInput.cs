using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000068 RID: 104
	public class BaseInput : UIBehaviour
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0001932F File Offset: 0x0001752F
		public virtual string compositionString
		{
			get
			{
				return Input.compositionString;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00019336 File Offset: 0x00017536
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0001933D File Offset: 0x0001753D
		public virtual IMECompositionMode imeCompositionMode
		{
			get
			{
				return Input.imeCompositionMode;
			}
			set
			{
				Input.imeCompositionMode = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00019345 File Offset: 0x00017545
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0001934C File Offset: 0x0001754C
		public virtual Vector2 compositionCursorPos
		{
			get
			{
				return Input.compositionCursorPos;
			}
			set
			{
				Input.compositionCursorPos = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00019354 File Offset: 0x00017554
		public virtual bool mousePresent
		{
			get
			{
				return Input.mousePresent;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001935B File Offset: 0x0001755B
		public virtual bool GetMouseButtonDown(int button)
		{
			return Input.GetMouseButtonDown(button);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00019363 File Offset: 0x00017563
		public virtual bool GetMouseButtonUp(int button)
		{
			return Input.GetMouseButtonUp(button);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001936B File Offset: 0x0001756B
		public virtual bool GetMouseButton(int button)
		{
			return Input.GetMouseButton(button);
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00019373 File Offset: 0x00017573
		public virtual Vector2 mousePosition
		{
			get
			{
				return Input.mousePosition;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0001937F File Offset: 0x0001757F
		public virtual Vector2 mouseScrollDelta
		{
			get
			{
				return Input.mouseScrollDelta;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00019386 File Offset: 0x00017586
		public virtual bool touchSupported
		{
			get
			{
				return Input.touchSupported;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0001938D File Offset: 0x0001758D
		public virtual int touchCount
		{
			get
			{
				return Input.touchCount;
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00019394 File Offset: 0x00017594
		public virtual Touch GetTouch(int index)
		{
			return Input.GetTouch(index);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001939C File Offset: 0x0001759C
		public virtual float GetAxisRaw(string axisName)
		{
			return Input.GetAxisRaw(axisName);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000193A4 File Offset: 0x000175A4
		public virtual bool GetButtonDown(string buttonName)
		{
			return Input.GetButtonDown(buttonName);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000193AC File Offset: 0x000175AC
		public BaseInput()
		{
		}
	}
}
