using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200018C RID: 396
	internal class TouchScreenTextEditorEventHandler : TextEditorEventHandler
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x000353A6 File Offset: 0x000335A6
		public TouchScreenTextEditorEventHandler(TextEditorEngine editorEngine, ITextInputField textInputField) : base(editorEngine, textInputField)
		{
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000353BC File Offset: 0x000335BC
		private void PollTouchScreenKeyboard()
		{
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				bool flag2 = this.m_TouchKeyboardPoller == null;
				if (flag2)
				{
					VisualElement visualElement = base.textInputField as VisualElement;
					this.m_TouchKeyboardPoller = ((visualElement != null) ? visualElement.schedule.Execute(new Action(this.DoPollTouchScreenKeyboard)).Every(100L) : null);
				}
				else
				{
					this.m_TouchKeyboardPoller.Resume();
				}
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00035438 File Offset: 0x00033638
		private void DoPollTouchScreenKeyboard()
		{
			bool flag = TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
			if (flag)
			{
				bool flag2 = base.textInputField.editorEngine.keyboardOnScreen != null;
				if (flag2)
				{
					base.textInputField.UpdateText(base.textInputField.CullString(base.textInputField.editorEngine.keyboardOnScreen.text));
					bool flag3 = !base.textInputField.isDelayed;
					if (flag3)
					{
						base.textInputField.UpdateValueFromText();
					}
					bool flag4 = base.textInputField.editorEngine.keyboardOnScreen.status > TouchScreenKeyboard.Status.Visible;
					if (flag4)
					{
						base.textInputField.editorEngine.keyboardOnScreen = null;
						this.m_TouchKeyboardPoller.Pause();
						bool isDelayed = base.textInputField.isDelayed;
						if (isDelayed)
						{
							base.textInputField.UpdateValueFromText();
						}
					}
				}
			}
			else
			{
				base.textInputField.editorEngine.keyboardOnScreen.active = false;
				base.textInputField.editorEngine.keyboardOnScreen = null;
				this.m_TouchKeyboardPoller.Pause();
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0003555C File Offset: 0x0003375C
		public override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = base.editorEngine.keyboardOnScreen != null;
			if (!flag)
			{
				bool flag2 = !base.textInputField.isReadOnly && evt.eventTypeId == EventBase<PointerDownEvent>.TypeId();
				if (flag2)
				{
					base.textInputField.CaptureMouse();
					this.m_LastPointerDownTarget = (evt.target as VisualElement);
				}
				else
				{
					bool flag3 = !base.textInputField.isReadOnly && evt.eventTypeId == EventBase<PointerUpEvent>.TypeId();
					if (flag3)
					{
						base.textInputField.ReleaseMouse();
						bool flag4 = this.m_LastPointerDownTarget == null || !this.m_LastPointerDownTarget.worldBound.Contains(((PointerUpEvent)evt).position);
						if (!flag4)
						{
							this.m_LastPointerDownTarget = null;
							base.textInputField.SyncTextEngine();
							base.textInputField.UpdateText(base.editorEngine.text);
							base.editorEngine.keyboardOnScreen = TouchScreenKeyboard.Open(base.textInputField.text, TouchScreenKeyboardType.Default, true, base.editorEngine.multiline, base.textInputField.isPasswordField);
							bool flag5 = base.editorEngine.keyboardOnScreen != null;
							if (flag5)
							{
								this.PollTouchScreenKeyboard();
							}
							base.editorEngine.UpdateScrollOffset();
							evt.StopPropagation();
						}
					}
				}
			}
		}

		// Token: 0x040005FA RID: 1530
		private IVisualElementScheduledItem m_TouchKeyboardPoller = null;

		// Token: 0x040005FB RID: 1531
		private VisualElement m_LastPointerDownTarget;
	}
}
