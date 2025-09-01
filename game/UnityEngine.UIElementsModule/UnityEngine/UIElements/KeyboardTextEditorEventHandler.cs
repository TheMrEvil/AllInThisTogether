using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014D RID: 333
	internal class KeyboardTextEditorEventHandler : TextEditorEventHandler
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002B6DC File Offset: 0x000298DC
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0002B6E4 File Offset: 0x000298E4
		private bool isClicking
		{
			get
			{
				return this.m_IsClicking;
			}
			set
			{
				bool flag = this.m_IsClicking == value;
				if (!flag)
				{
					this.m_IsClicking = value;
					bool isClicking = this.m_IsClicking;
					if (isClicking)
					{
						base.textInputField.CaptureMouse();
					}
					else
					{
						base.textInputField.ReleaseMouse();
					}
				}
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002B72C File Offset: 0x0002992C
		public KeyboardTextEditorEventHandler(TextEditorEngine editorEngine, ITextInputField textInputField) : base(editorEngine, textInputField)
		{
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002B744 File Offset: 0x00029944
		public override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
			if (flag)
			{
				this.OnFocus(evt as FocusEvent);
			}
			else
			{
				bool flag2 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
				if (flag2)
				{
					this.OnBlur(evt as BlurEvent);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<MouseDownEvent>.TypeId();
					if (flag3)
					{
						this.OnMouseDown(evt as MouseDownEvent);
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<MouseUpEvent>.TypeId();
						if (flag4)
						{
							this.OnMouseUp(evt as MouseUpEvent);
						}
						else
						{
							bool flag5 = evt.eventTypeId == EventBase<MouseMoveEvent>.TypeId();
							if (flag5)
							{
								this.OnMouseMove(evt as MouseMoveEvent);
							}
							else
							{
								bool flag6 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
								if (flag6)
								{
									this.OnKeyDown(evt as KeyDownEvent);
								}
								else
								{
									bool flag7 = evt.eventTypeId == EventBase<ValidateCommandEvent>.TypeId();
									if (flag7)
									{
										this.OnValidateCommandEvent(evt as ValidateCommandEvent);
									}
									else
									{
										bool flag8 = evt.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
										if (flag8)
										{
											this.OnExecuteCommandEvent(evt as ExecuteCommandEvent);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002B87C File Offset: 0x00029A7C
		private void OnFocus(FocusEvent _)
		{
			GUIUtility.imeCompositionMode = IMECompositionMode.On;
			this.m_DragToPosition = false;
			bool flag;
			if (PointerDeviceState.GetPressedButtons(PointerId.mousePointerId) == 0)
			{
				VisualElement visualElement = base.textInputField as VisualElement;
				flag = (visualElement != null && visualElement.panel.contextType == ContextType.Editor && Event.current == null);
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			if (flag2)
			{
				this.m_SelectAllOnMouseUp = true;
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002B8DE File Offset: 0x00029ADE
		private void OnBlur(BlurEvent _)
		{
			GUIUtility.imeCompositionMode = IMECompositionMode.Auto;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002B8E8 File Offset: 0x00029AE8
		private void OnMouseDown(MouseDownEvent evt)
		{
			base.textInputField.SyncTextEngine();
			this.m_Changed = false;
			bool flag = !base.textInputField.hasFocus;
			if (flag)
			{
				base.editorEngine.m_HasFocus = true;
				base.editorEngine.MoveCursorToPosition_Internal(evt.localMousePosition, evt.button == 0 && evt.shiftKey);
				bool flag2 = evt.button == 0;
				if (flag2)
				{
					this.isClicking = true;
					this.m_ClickStartPosition = evt.localMousePosition;
				}
				evt.StopPropagation();
			}
			else
			{
				bool flag3 = evt.button == 0;
				if (flag3)
				{
					bool flag4 = evt.clickCount == 2 && base.textInputField.doubleClickSelectsWord;
					if (flag4)
					{
						base.editorEngine.SelectCurrentWord();
						base.editorEngine.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
						base.editorEngine.MouseDragSelectsWholeWords(true);
						this.m_DragToPosition = false;
					}
					else
					{
						bool flag5 = evt.clickCount == 3 && base.textInputField.tripleClickSelectsLine;
						if (flag5)
						{
							base.editorEngine.SelectCurrentParagraph();
							base.editorEngine.MouseDragSelectsWholeWords(true);
							base.editorEngine.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
							this.m_DragToPosition = false;
						}
						else
						{
							base.editorEngine.MoveCursorToPosition_Internal(evt.localMousePosition, evt.shiftKey);
						}
					}
					this.isClicking = true;
					this.m_ClickStartPosition = evt.localMousePosition;
					evt.StopPropagation();
				}
				else
				{
					bool flag6 = evt.button == 1;
					if (flag6)
					{
						bool flag7 = base.editorEngine.cursorIndex == base.editorEngine.selectIndex;
						if (flag7)
						{
							base.editorEngine.MoveCursorToPosition_Internal(evt.localMousePosition, false);
						}
						this.m_SelectAllOnMouseUp = false;
						this.m_DragToPosition = false;
					}
				}
			}
			base.editorEngine.UpdateScrollOffset();
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002BABC File Offset: 0x00029CBC
		private void OnMouseUp(MouseUpEvent evt)
		{
			bool flag = evt.button != 0;
			if (!flag)
			{
				bool flag2 = !this.isClicking;
				if (!flag2)
				{
					base.textInputField.SyncTextEngine();
					this.m_Changed = false;
					bool flag3 = this.m_Dragged && this.m_DragToPosition;
					if (flag3)
					{
						base.editorEngine.MoveSelectionToAltCursor();
					}
					else
					{
						bool selectAllOnMouseUp = this.m_SelectAllOnMouseUp;
						if (selectAllOnMouseUp)
						{
							base.editorEngine.SelectAll();
						}
					}
					base.editorEngine.MouseDragSelectsWholeWords(false);
					this.isClicking = false;
					this.m_DragToPosition = true;
					this.m_Dragged = false;
					this.m_SelectAllOnMouseUp = false;
					evt.StopPropagation();
					base.editorEngine.UpdateScrollOffset();
				}
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0002BB80 File Offset: 0x00029D80
		private void OnMouseMove(MouseMoveEvent evt)
		{
			bool flag = evt.button != 0;
			if (!flag)
			{
				bool flag2 = !this.isClicking;
				if (!flag2)
				{
					base.textInputField.SyncTextEngine();
					this.m_Changed = false;
					this.m_Dragged = (this.m_Dragged || this.MoveDistanceQualifiesForDrag(this.m_ClickStartPosition, evt.localMousePosition));
					bool dragged = this.m_Dragged;
					if (dragged)
					{
						this.ProcessDragMove(evt);
					}
					evt.StopPropagation();
					base.editorEngine.UpdateScrollOffset();
				}
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002BC0C File Offset: 0x00029E0C
		private void ProcessDragMove(MouseMoveEvent evt)
		{
			bool flag = !evt.shiftKey && base.editorEngine.hasSelection && this.m_DragToPosition;
			if (flag)
			{
				base.editorEngine.MoveAltCursorToPosition(evt.localMousePosition);
			}
			else
			{
				bool shiftKey = evt.shiftKey;
				if (shiftKey)
				{
					base.editorEngine.MoveCursorToPosition_Internal(evt.localMousePosition, evt.shiftKey);
				}
				else
				{
					base.editorEngine.SelectToPosition(evt.localMousePosition);
				}
				this.m_DragToPosition = false;
				this.m_SelectAllOnMouseUp = !base.editorEngine.hasSelection;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002BCAC File Offset: 0x00029EAC
		private bool MoveDistanceQualifiesForDrag(Vector2 start, Vector2 current)
		{
			return (start - current).sqrMagnitude >= 16f;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002BCD8 File Offset: 0x00029ED8
		private void OnKeyDown(KeyDownEvent evt)
		{
			bool flag = !base.textInputField.hasFocus;
			if (!flag)
			{
				base.textInputField.SyncTextEngine();
				this.m_Changed = false;
				evt.GetEquivalentImguiEvent(this.m_ImguiEvent);
				bool flag2 = base.editorEngine.HandleKeyEvent(this.m_ImguiEvent, base.textInputField.isReadOnly);
				if (flag2)
				{
					bool flag3 = base.textInputField.text != base.editorEngine.text;
					if (flag3)
					{
						this.m_Changed = true;
					}
					evt.StopPropagation();
				}
				else
				{
					char character = evt.character;
					bool flag4 = !base.editorEngine.multiline && (evt.keyCode == KeyCode.Tab || character == '\t');
					if (flag4)
					{
						return;
					}
					bool flag5 = base.editorEngine.multiline && (evt.keyCode == KeyCode.Tab || character == '\t') && evt.modifiers > EventModifiers.None;
					if (flag5)
					{
						return;
					}
					bool flag6 = evt.actionKey && (!evt.altKey || character == '\0');
					if (flag6)
					{
						return;
					}
					evt.StopPropagation();
					bool flag7 = character == '\n' && !base.editorEngine.multiline && !evt.altKey;
					if (flag7)
					{
						return;
					}
					bool flag8 = character == '\n' && base.editorEngine.multiline && evt.shiftKey;
					if (flag8)
					{
						return;
					}
					bool flag9 = !base.textInputField.AcceptCharacter(character);
					if (flag9)
					{
						return;
					}
					Font font = base.editorEngine.style.font;
					bool flag10 = (font != null && font.HasCharacter(character)) || character == '\n' || character == '\t';
					if (flag10)
					{
						base.editorEngine.Insert(character);
						this.m_Changed = true;
					}
					else
					{
						bool flag11 = character == '\0';
						if (flag11)
						{
							bool flag12 = !string.IsNullOrEmpty(GUIUtility.compositionString);
							if (flag12)
							{
								base.editorEngine.ReplaceSelection("");
								this.m_Changed = true;
							}
						}
					}
				}
				bool changed = this.m_Changed;
				if (changed)
				{
					base.editorEngine.text = base.textInputField.CullString(base.editorEngine.text);
					base.textInputField.UpdateText(base.editorEngine.text);
				}
				base.editorEngine.UpdateScrollOffset();
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002BF54 File Offset: 0x0002A154
		private void OnValidateCommandEvent(ValidateCommandEvent evt)
		{
			bool flag = !base.textInputField.hasFocus;
			if (!flag)
			{
				base.textInputField.SyncTextEngine();
				this.m_Changed = false;
				string commandName = evt.commandName;
				string a = commandName;
				if (!(a == "Cut"))
				{
					if (!(a == "Copy"))
					{
						if (!(a == "Paste"))
						{
							if (!(a == "SelectAll"))
							{
								if (!(a == "Delete"))
								{
									if (!(a == "UndoRedoPerformed"))
									{
									}
								}
								else
								{
									bool isReadOnly = base.textInputField.isReadOnly;
									if (isReadOnly)
									{
										return;
									}
								}
							}
						}
						else
						{
							bool flag2 = !base.editorEngine.CanPaste() || base.textInputField.isReadOnly;
							if (flag2)
							{
								return;
							}
						}
					}
					else
					{
						bool flag3 = !base.editorEngine.hasSelection;
						if (flag3)
						{
							return;
						}
					}
				}
				else
				{
					bool flag4 = !base.editorEngine.hasSelection || base.textInputField.isReadOnly;
					if (flag4)
					{
						return;
					}
				}
				evt.StopPropagation();
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002C068 File Offset: 0x0002A268
		private void OnExecuteCommandEvent(ExecuteCommandEvent evt)
		{
			bool flag = !base.textInputField.hasFocus;
			if (!flag)
			{
				base.textInputField.SyncTextEngine();
				this.m_Changed = false;
				bool flag2 = false;
				string text = base.editorEngine.text;
				bool flag3 = !base.textInputField.hasFocus;
				if (!flag3)
				{
					string commandName = evt.commandName;
					string a = commandName;
					if (!(a == "OnLostFocus"))
					{
						if (!(a == "Cut"))
						{
							if (a == "Copy")
							{
								base.editorEngine.Copy();
								evt.StopPropagation();
								return;
							}
							if (!(a == "Paste"))
							{
								if (a == "SelectAll")
								{
									base.editorEngine.SelectAll();
									evt.StopPropagation();
									return;
								}
								if (a == "Delete")
								{
									bool flag4 = !base.textInputField.isReadOnly;
									if (flag4)
									{
										bool flag5 = SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX;
										if (flag5)
										{
											base.editorEngine.Delete();
										}
										else
										{
											base.editorEngine.Cut();
										}
										flag2 = true;
									}
								}
							}
							else
							{
								bool flag6 = !base.textInputField.isReadOnly;
								if (flag6)
								{
									base.editorEngine.Paste();
									flag2 = true;
								}
							}
						}
						else
						{
							bool flag7 = !base.textInputField.isReadOnly;
							if (flag7)
							{
								base.editorEngine.Cut();
								flag2 = true;
							}
						}
						bool flag8 = flag2;
						if (flag8)
						{
							bool flag9 = text != base.editorEngine.text;
							if (flag9)
							{
								this.m_Changed = true;
							}
							evt.StopPropagation();
						}
						bool changed = this.m_Changed;
						if (changed)
						{
							base.editorEngine.text = base.textInputField.CullString(base.editorEngine.text);
							base.textInputField.UpdateText(base.editorEngine.text);
							evt.StopPropagation();
						}
						base.editorEngine.UpdateScrollOffset();
					}
					else
					{
						evt.StopPropagation();
					}
				}
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002C288 File Offset: 0x0002A488
		public void PreDrawCursor(string newText)
		{
			base.textInputField.SyncTextEngine();
			this.m_PreDrawCursorText = base.editorEngine.text;
			int num = base.editorEngine.cursorIndex;
			bool flag = !string.IsNullOrEmpty(GUIUtility.compositionString);
			if (flag)
			{
				base.editorEngine.text = newText.Substring(0, base.editorEngine.cursorIndex) + GUIUtility.compositionString + newText.Substring(base.editorEngine.selectIndex);
				num += GUIUtility.compositionString.Length;
			}
			else
			{
				base.editorEngine.text = newText;
			}
			base.editorEngine.text = base.textInputField.CullString(base.editorEngine.text);
			num = Math.Min(num, base.editorEngine.text.Length);
			base.editorEngine.graphicalCursorPos = base.editorEngine.style.GetCursorPixelPosition(base.editorEngine.localPosition, new GUIContent(base.editorEngine.text), num);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0002C399 File Offset: 0x0002A599
		public void PostDrawCursor()
		{
			base.editorEngine.text = this.m_PreDrawCursorText;
		}

		// Token: 0x040004B9 RID: 1209
		internal const int kDragThreshold = 4;

		// Token: 0x040004BA RID: 1210
		internal bool m_Changed;

		// Token: 0x040004BB RID: 1211
		private bool m_Dragged;

		// Token: 0x040004BC RID: 1212
		private bool m_DragToPosition;

		// Token: 0x040004BD RID: 1213
		private bool m_SelectAllOnMouseUp;

		// Token: 0x040004BE RID: 1214
		private string m_PreDrawCursorText;

		// Token: 0x040004BF RID: 1215
		private bool m_IsClicking;

		// Token: 0x040004C0 RID: 1216
		private Vector2 m_ClickStartPosition;

		// Token: 0x040004C1 RID: 1217
		private readonly Event m_ImguiEvent = new Event();
	}
}
