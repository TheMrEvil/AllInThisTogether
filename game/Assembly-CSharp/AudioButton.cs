using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000133 RID: 307
public class AudioButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ISelectHandler
{
	// Token: 0x06000E34 RID: 3636 RVA: 0x0005A414 File Offset: 0x00058614
	private void Awake()
	{
		this.selectable = base.GetComponent<Selectable>();
		Button button = this.selectable as Button;
		if (button != null)
		{
			CanvasController.SetupButton(button);
		}
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x0005A442 File Offset: 0x00058642
	public void OnPointerEnter(PointerEventData ev)
	{
		if (this.selectable != null && this.selectable.IsInteractable() && !InputManager.IsUsingController)
		{
			this.PlayHover();
		}
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x0005A46C File Offset: 0x0005866C
	public void OnSelect(BaseEventData ev)
	{
		if (InputManager.IsUsingController)
		{
			this.PlayHover();
		}
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x0005A47B File Offset: 0x0005867B
	private void PlayHover()
	{
		if (this.buttonType == AudioButton.ButtonType.Default)
		{
			CanvasController.PlaySelectionChanged();
		}
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x0005A48A File Offset: 0x0005868A
	public AudioButton()
	{
	}

	// Token: 0x04000BA0 RID: 2976
	private Selectable selectable;

	// Token: 0x04000BA1 RID: 2977
	public AudioButton.ButtonType buttonType;

	// Token: 0x02000537 RID: 1335
	public enum ButtonType
	{
		// Token: 0x04002651 RID: 9809
		Default,
		// Token: 0x04002652 RID: 9810
		Silent,
		// Token: 0x04002653 RID: 9811
		Tab
	}
}
