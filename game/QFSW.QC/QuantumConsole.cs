using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using QFSW.QC.Pooling;
using QFSW.QC.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QFSW.QC
{
	// Token: 0x0200002C RID: 44
	[DisallowMultipleComponent]
	public class QuantumConsole : MonoBehaviour
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003FD4 File Offset: 0x000021D4
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003FDB File Offset: 0x000021DB
		public static QuantumConsole Instance
		{
			[CompilerGenerated]
			get
			{
				return QuantumConsole.<Instance>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				QuantumConsole.<Instance>k__BackingField = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003FE3 File Offset: 0x000021E3
		public QuantumTheme Theme
		{
			get
			{
				return this._theme;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003FEB File Offset: 0x000021EB
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003FF3 File Offset: 0x000021F3
		public QuantumKeyConfig KeyConfig
		{
			get
			{
				return this._keyConfig;
			}
			set
			{
				this._keyConfig = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003FFC File Offset: 0x000021FC
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00004004 File Offset: 0x00002204
		public QuantumLocalization Localization
		{
			get
			{
				return this._localization;
			}
			set
			{
				this._localization = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000400D File Offset: 0x0000220D
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004015 File Offset: 0x00002215
		[CommandDescription("The maximum number of logs that may be stored in the log storage before old logs are removed.")]
		public int MaxStoredLogs
		{
			get
			{
				return this._maxStoredLogs;
			}
			set
			{
				this._maxStoredLogs = value;
				if (this._logStorage != null)
				{
					this._logStorage.MaxStoredLogs = value;
				}
				if (this._logQueue != null)
				{
					this._logQueue.MaxStoredLogs = value;
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000B2 RID: 178 RVA: 0x00004048 File Offset: 0x00002248
		// (remove) Token: 0x060000B3 RID: 179 RVA: 0x00004080 File Offset: 0x00002280
		public event Action OnStateChange
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnStateChange;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnStateChange, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnStateChange;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnStateChange, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000B4 RID: 180 RVA: 0x000040B8 File Offset: 0x000022B8
		// (remove) Token: 0x060000B5 RID: 181 RVA: 0x000040F0 File Offset: 0x000022F0
		public event Action<string> OnInvoke
		{
			[CompilerGenerated]
			add
			{
				Action<string> action = this.OnInvoke;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> value2 = (Action<string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref this.OnInvoke, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string> action = this.OnInvoke;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> value2 = (Action<string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref this.OnInvoke, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000B6 RID: 182 RVA: 0x00004128 File Offset: 0x00002328
		// (remove) Token: 0x060000B7 RID: 183 RVA: 0x00004160 File Offset: 0x00002360
		public event Action OnClear
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnClear;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnClear, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnClear;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnClear, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000B8 RID: 184 RVA: 0x00004198 File Offset: 0x00002398
		// (remove) Token: 0x060000B9 RID: 185 RVA: 0x000041D0 File Offset: 0x000023D0
		public event Action<ILog> OnLog
		{
			[CompilerGenerated]
			add
			{
				Action<ILog> action = this.OnLog;
				Action<ILog> action2;
				do
				{
					action2 = action;
					Action<ILog> value2 = (Action<ILog>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ILog>>(ref this.OnLog, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ILog> action = this.OnLog;
				Action<ILog> action2;
				do
				{
					action2 = action;
					Action<ILog> value2 = (Action<ILog>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ILog>>(ref this.OnLog, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060000BA RID: 186 RVA: 0x00004208 File Offset: 0x00002408
		// (remove) Token: 0x060000BB RID: 187 RVA: 0x00004240 File Offset: 0x00002440
		public event Action OnActivate
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnActivate;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnActivate, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnActivate;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnActivate, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000BC RID: 188 RVA: 0x00004278 File Offset: 0x00002478
		// (remove) Token: 0x060000BD RID: 189 RVA: 0x000042B0 File Offset: 0x000024B0
		public event Action OnDeactivate
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnDeactivate;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnDeactivate, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnDeactivate;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnDeactivate, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000BE RID: 190 RVA: 0x000042E8 File Offset: 0x000024E8
		// (remove) Token: 0x060000BF RID: 191 RVA: 0x00004320 File Offset: 0x00002520
		public event Action<SuggestionSet> OnSuggestionSetGenerated
		{
			[CompilerGenerated]
			add
			{
				Action<SuggestionSet> action = this.OnSuggestionSetGenerated;
				Action<SuggestionSet> action2;
				do
				{
					action2 = action;
					Action<SuggestionSet> value2 = (Action<SuggestionSet>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<SuggestionSet>>(ref this.OnSuggestionSetGenerated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<SuggestionSet> action = this.OnSuggestionSetGenerated;
				Action<SuggestionSet> action2;
				do
				{
					action2 = action;
					Action<SuggestionSet> value2 = (Action<SuggestionSet>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<SuggestionSet>>(ref this.OnSuggestionSetGenerated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004355 File Offset: 0x00002555
		private bool IsBlockedByAsync
		{
			get
			{
				return ((this._blockOnAsync && this._currentTasks.Count > 0) || this._currentActions.Count > 0) && !this._isHandlingUserResponse;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004386 File Offset: 0x00002586
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000438E File Offset: 0x0000258E
		public bool IsActive
		{
			[CompilerGenerated]
			get
			{
				return this.<IsActive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsActive>k__BackingField = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004397 File Offset: 0x00002597
		public bool IsFocused
		{
			get
			{
				return this.IsActive && this._consoleInput && this._consoleInput.isFocused;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000043BB File Offset: 0x000025BB
		public bool AreActionsExecuting
		{
			get
			{
				return this._currentActions.Count > 0;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000043CC File Offset: 0x000025CC
		public void ApplyTheme(QuantumTheme theme, bool forceRefresh = false)
		{
			this._theme = theme;
			if (theme)
			{
				if (this._textComponents == null || forceRefresh)
				{
					this._textComponents = base.GetComponentsInChildren<TextMeshProUGUI>(true);
				}
				foreach (TextMeshProUGUI textMeshProUGUI in this._textComponents)
				{
					if (theme.Font)
					{
						textMeshProUGUI.font = theme.Font;
					}
				}
				foreach (Image image in this._panels)
				{
					image.material = theme.PanelMaterial;
					image.color = theme.PanelColor;
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004464 File Offset: 0x00002664
		protected virtual void Update()
		{
			if (!QuantumConsole.IsDev)
			{
				return;
			}
			if (!this.IsActive)
			{
				if (this._keyConfig.ShowConsoleKey.IsPressed() || this._keyConfig.ToggleConsoleVisibilityKey.IsPressed())
				{
					this.Activate(true);
					return;
				}
			}
			else
			{
				this.ProcessAsyncTasks();
				this.ProcessActions();
				this.HandleAsyncJobCounter();
				if (this._keyConfig.HideConsoleKey.IsPressed() || this._keyConfig.ToggleConsoleVisibilityKey.IsPressed())
				{
					this.Deactivate();
					return;
				}
				if (QuantumConsoleProcessor.TableIsGenerating)
				{
					this._consoleInput.interactable = false;
					string text = (this._logStorage.GetLogString() + "\n" + this.GetTableGenerationText()).Trim();
					if (text != this._consoleLogText.text)
					{
						if (this._showInitLogs)
						{
							Action onStateChange = this.OnStateChange;
							if (onStateChange != null)
							{
								onStateChange();
							}
							this._consoleLogText.text = text;
						}
						if (this._inputPlaceholderText)
						{
							this._inputPlaceholderText.text = this._localization.Loading;
						}
					}
					return;
				}
				if (this.IsBlockedByAsync)
				{
					Action onStateChange2 = this.OnStateChange;
					if (onStateChange2 != null)
					{
						onStateChange2();
					}
					this._consoleInput.interactable = false;
					if (this._inputPlaceholderText)
					{
						this._inputPlaceholderText.text = this._localization.ExecutingAsyncCommand;
					}
				}
				else if (!this._consoleInput.interactable)
				{
					Action onStateChange3 = this.OnStateChange;
					if (onStateChange3 != null)
					{
						onStateChange3();
					}
					this._consoleInput.interactable = true;
					if (this._inputPlaceholderText)
					{
						this._inputPlaceholderText.text = this._localization.EnterCommand;
					}
					this.OverrideConsoleInput(string.Empty, true);
					if (this._isGeneratingTable)
					{
						if (this._showInitLogs)
						{
							this.AppendLog(new Log(this.GetTableGenerationText(), LogType.Log, true));
							this._consoleLogText.text = this._logStorage.GetLogString();
						}
						this._isGeneratingTable = false;
						this.ScrollConsoleToLatest();
					}
				}
				this._previousInput = this._currentInput;
				this._currentInput = this._consoleInput.text;
				if (this._currentInput != this._previousInput)
				{
					this.OnInputChange();
					return;
				}
				if (!this.IsBlockedByAsync)
				{
					if (InputHelper.GetKeyDown(this._keyConfig.SubmitCommandKey))
					{
						this.InvokeCommand();
					}
					if (this._storeCommandHistory)
					{
						this.ProcessCommandHistory();
					}
					this.ProcessAutocomplete();
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000046E2 File Offset: 0x000028E2
		private void LateUpdate()
		{
			if (this.IsActive)
			{
				this.FlushQueuedLogs();
				this.FlushToConsoleText();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000046F8 File Offset: 0x000028F8
		private string GetTableGenerationText()
		{
			string text = string.Format(this._localization.InitializationProgress, QuantumConsoleProcessor.LoadedCommandCount);
			if (QuantumConsoleProcessor.TableIsGenerating)
			{
				text += "...";
			}
			else
			{
				string str = (this._theme == null) ? this._localization.InitializationComplete : this._localization.InitializationComplete.ColorText(this._theme.SuccessColor);
				text = text + "\n" + str;
			}
			return text;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000477C File Offset: 0x0000297C
		private void ProcessCommandHistory()
		{
			if (InputHelper.GetKeyDown(this._keyConfig.NextCommandKey) || InputHelper.GetKeyDown(this._keyConfig.PreviousCommandKey))
			{
				if (InputHelper.GetKeyDown(this._keyConfig.NextCommandKey))
				{
					this._selectedPreviousCommandIndex++;
				}
				else if (this._selectedPreviousCommandIndex > 0)
				{
					this._selectedPreviousCommandIndex--;
				}
				this._selectedPreviousCommandIndex = Mathf.Clamp(this._selectedPreviousCommandIndex, -1, this._previousCommands.Count - 1);
				if (this._selectedPreviousCommandIndex > -1)
				{
					string newInput = this._previousCommands[this._previousCommands.Count - this._selectedPreviousCommandIndex - 1];
					this.OverrideConsoleInput(newInput, true);
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000483C File Offset: 0x00002A3C
		private void UpdateSuggestions()
		{
			if (this._isHandlingUserResponse)
			{
				this.ClearSuggestions();
				this.ClearPopup();
				return;
			}
			SuggestorOptions options = new SuggestorOptions
			{
				CaseSensitive = this._caseSensitiveSearch,
				Fuzzy = this._useFuzzySearch,
				CollapseOverloads = this._collapseSuggestionOverloads
			};
			this._suggestionStack.UpdateStack(this._currentInput, options);
			this.UpdateSuggestionText();
			if (this._showPopupDisplay)
			{
				this.UpdatePopupDisplay();
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000048B8 File Offset: 0x00002AB8
		private void ProcessAutocomplete()
		{
			if (!this._enableAutocomplete)
			{
				return;
			}
			if (this._keyConfig.SelectNextSuggestionKey.IsPressed() || this._keyConfig.SelectPreviousSuggestionKey.IsPressed())
			{
				SuggestionSet topmostSuggestionSet = this._suggestionStack.TopmostSuggestionSet;
				if (topmostSuggestionSet != null && topmostSuggestionSet.Suggestions.Count > 0)
				{
					if (this._keyConfig.SelectNextSuggestionKey.IsPressed())
					{
						topmostSuggestionSet.SelectionIndex++;
					}
					if (this._keyConfig.SelectPreviousSuggestionKey.IsPressed())
					{
						topmostSuggestionSet.SelectionIndex--;
					}
					topmostSuggestionSet.SelectionIndex += topmostSuggestionSet.Suggestions.Count;
					topmostSuggestionSet.SelectionIndex %= topmostSuggestionSet.Suggestions.Count;
					this.SetSuggestion(topmostSuggestionSet.SelectionIndex);
				}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004994 File Offset: 0x00002B94
		private void FormatSuggestion(IQcSuggestion suggestion, bool selected, StringBuilder buffer)
		{
			if (!this._theme)
			{
				buffer.Append(suggestion.FullSignature);
				return;
			}
			Color color = Color.white;
			Color color2 = this._theme.SuggestionColor;
			if (selected)
			{
				color *= this._theme.SelectedSuggestionColor;
				color2 *= this._theme.SelectedSuggestionColor;
			}
			buffer.AppendColoredText(suggestion.PrimarySignature, color);
			buffer.AppendColoredText(suggestion.SecondarySignature, color2);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004A10 File Offset: 0x00002C10
		private string GetFormattedSuggestions(SuggestionSet suggestionSet)
		{
			StringBuilder stringBuilder = this._stringBuilderPool.GetStringBuilder(0);
			this.GetFormattedSuggestions(suggestionSet, stringBuilder);
			return this._stringBuilderPool.ReleaseAndToString(stringBuilder);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004A40 File Offset: 0x00002C40
		private void GetFormattedSuggestions(SuggestionSet suggestionSet, StringBuilder buffer)
		{
			int num = suggestionSet.Suggestions.Count;
			if (this._maxSuggestionDisplaySize > 0)
			{
				num = Mathf.Min(num, this._maxSuggestionDisplaySize + 1);
			}
			for (int i = 0; i < num; i++)
			{
				if (this._maxSuggestionDisplaySize > 0 && i >= this._maxSuggestionDisplaySize)
				{
					if (this._theme && suggestionSet.SelectionIndex >= this._maxSuggestionDisplaySize)
					{
						buffer.AppendColoredText("...", this._theme.SelectedSuggestionColor);
					}
					else
					{
						buffer.Append("...");
					}
				}
				else
				{
					bool selected = i == suggestionSet.SelectionIndex;
					buffer.Append("<link=");
					buffer.Append(i);
					buffer.Append(">");
					this.FormatSuggestion(suggestionSet.Suggestions[i], selected, buffer);
					buffer.AppendLine("</link>");
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004B20 File Offset: 0x00002D20
		private void UpdatePopupDisplay()
		{
			SuggestionSet topmostSuggestionSet = this._suggestionStack.TopmostSuggestionSet;
			if (topmostSuggestionSet == null || topmostSuggestionSet.Suggestions.Count == 0)
			{
				this.ClearPopup();
				return;
			}
			if (this._suggestionPopupRect && this._suggestionPopupText)
			{
				string text = this.GetFormattedSuggestions(topmostSuggestionSet);
				if (this._suggestionDisplayOrder == SortOrder.Ascending)
				{
					text = text.ReverseItems('\n');
				}
				this._suggestionPopupRect.gameObject.SetActive(true);
				this._suggestionPopupText.text = text;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004BA0 File Offset: 0x00002DA0
		public void SetSuggestion(int suggestionIndex)
		{
			if (!this._suggestionStack.SetSuggestionIndex(suggestionIndex))
			{
				throw new ArgumentException(string.Format("Cannot set suggestion to index {0}.", suggestionIndex));
			}
			this.OverrideConsoleInput(this._suggestionStack.GetCompletion(), true);
			this.UpdateSuggestionText();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004BE0 File Offset: 0x00002DE0
		private void UpdateSuggestionText()
		{
			Color color = this._theme ? this._theme.SuggestionColor : Color.gray;
			StringBuilder stringBuilder = this._stringBuilderPool.GetStringBuilder(0);
			stringBuilder.AppendColoredText(this._currentInput, Color.clear);
			stringBuilder.AppendColoredText(this._suggestionStack.GetCompletionTail(), color);
			this._consoleSuggestionText.text = this._stringBuilderPool.ReleaseAndToString(stringBuilder);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004C54 File Offset: 0x00002E54
		public void OverrideConsoleInput(string newInput, bool shouldFocus = true)
		{
			this._currentInput = newInput;
			this._previousInput = newInput;
			this._consoleInput.text = newInput;
			if (shouldFocus)
			{
				this.FocusConsoleInput();
			}
			this.OnInputChange();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004C80 File Offset: 0x00002E80
		public void FocusConsoleInput()
		{
			this._consoleInput.Select();
			this._consoleInput.caretPosition = this._consoleInput.text.Length;
			this._consoleInput.selectionAnchorPosition = this._consoleInput.text.Length;
			this._consoleInput.MoveTextEnd(false);
			this._consoleInput.ActivateInputField();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004CE8 File Offset: 0x00002EE8
		private void OnInputChange()
		{
			if (this._selectedPreviousCommandIndex >= 0 && this._currentInput.Trim() != this._previousCommands[this._previousCommands.Count - this._selectedPreviousCommandIndex - 1])
			{
				this.ClearHistoricalSuggestions();
			}
			if (this._enableAutocomplete)
			{
				this.UpdateSuggestions();
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004D43 File Offset: 0x00002F43
		private void ClearHistoricalSuggestions()
		{
			this._selectedPreviousCommandIndex = -1;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004D4C File Offset: 0x00002F4C
		private void ClearSuggestions()
		{
			this._suggestionStack.Clear();
			this._consoleSuggestionText.text = string.Empty;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004D69 File Offset: 0x00002F69
		private void ClearPopup()
		{
			if (this._suggestionPopupRect)
			{
				this._suggestionPopupRect.gameObject.SetActive(false);
			}
			if (this._suggestionPopupText)
			{
				this._suggestionPopupText.text = string.Empty;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public void InvokeCommand()
		{
			string text = this._consoleInput.text;
			if (!string.IsNullOrWhiteSpace(text))
			{
				string command = text.Trim();
				if (this._isHandlingUserResponse)
				{
					this.HandleUserResponse(command);
					return;
				}
				this.InvokeCommand(command);
				this.OverrideConsoleInput(string.Empty, true);
				this.StoreCommand(command);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004DFC File Offset: 0x00002FFC
		private void HandleUserResponse(string command)
		{
			if (this._currentResponseConfig.LogInput)
			{
				this.LogUserInput(command);
				this.StoreCommand(command);
			}
			this._onSubmitResponseCallback(command);
			this._onSubmitResponseCallback = null;
			this._consoleInput.interactable = false;
			this._isHandlingUserResponse = false;
			Action onStateChange = this.OnStateChange;
			if (onStateChange == null)
			{
				return;
			}
			onStateChange();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004E5C File Offset: 0x0000305C
		private void LogUserInput(string input)
		{
			ILog log = this.GenerateCommandLog(input);
			this.LogToConsole(log);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004E78 File Offset: 0x00003078
		protected ILog GenerateCommandLog(string command)
		{
			string format = (this._theme != null) ? this._theme.CommandLogFormat : "> {0}";
			if (command.Contains("<"))
			{
				command = "<noparse>" + command + "</noparse>";
			}
			string text = string.Format(format, command);
			if (this._theme)
			{
				text = text.ColorText(this._theme.CommandLogColor);
			}
			return new Log(text, LogType.Log, true);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004EF8 File Offset: 0x000030F8
		public object InvokeCommand(string command)
		{
			object obj = null;
			if (!string.IsNullOrWhiteSpace(command))
			{
				this.LogUserInput(command);
				string logText = string.Empty;
				try
				{
					obj = QuantumConsoleProcessor.InvokeCommand(command);
					Task task = obj as Task;
					if (task == null)
					{
						IEnumerator<ICommandAction> enumerator = obj as IEnumerator<ICommandAction>;
						if (enumerator == null)
						{
							IEnumerable<ICommandAction> enumerable = obj as IEnumerable<ICommandAction>;
							if (enumerable == null)
							{
								logText = this.Serialize(obj);
							}
							else
							{
								this.StartAction(enumerable.GetEnumerator());
							}
						}
						else
						{
							this.StartAction(enumerator);
						}
					}
					else
					{
						this._currentTasks.Add(task);
					}
				}
				catch (TargetInvocationException ex)
				{
					logText = this.GetInvocationErrorMessage(ex.InnerException);
				}
				catch (Exception e)
				{
					logText = this.GetErrorMessage(e);
				}
				this.LogToConsole(logText, true);
				Action<string> onInvoke = this.OnInvoke;
				if (onInvoke != null)
				{
					onInvoke(command);
				}
				if (this._autoScroll == AutoScrollOptions.OnInvoke)
				{
					this.ScrollConsoleToLatest();
				}
				if (this._closeOnSubmit)
				{
					this.Deactivate();
				}
			}
			else
			{
				this.OverrideConsoleInput(string.Empty, true);
			}
			return obj;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004FFC File Offset: 0x000031FC
		public Task InvokeExternalCommandsAsync(string filePath)
		{
			QuantumConsole.<InvokeExternalCommandsAsync>d__132 <InvokeExternalCommandsAsync>d__;
			<InvokeExternalCommandsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InvokeExternalCommandsAsync>d__.<>4__this = this;
			<InvokeExternalCommandsAsync>d__.filePath = filePath;
			<InvokeExternalCommandsAsync>d__.<>1__state = -1;
			<InvokeExternalCommandsAsync>d__.<>t__builder.Start<QuantumConsole.<InvokeExternalCommandsAsync>d__132>(ref <InvokeExternalCommandsAsync>d__);
			return <InvokeExternalCommandsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005048 File Offset: 0x00003248
		public Task InvokeCommandsAsync(IEnumerable<string> commands)
		{
			QuantumConsole.<InvokeCommandsAsync>d__133 <InvokeCommandsAsync>d__;
			<InvokeCommandsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InvokeCommandsAsync>d__.<>4__this = this;
			<InvokeCommandsAsync>d__.commands = commands;
			<InvokeCommandsAsync>d__.<>1__state = -1;
			<InvokeCommandsAsync>d__.<>t__builder.Start<QuantumConsole.<InvokeCommandsAsync>d__133>(ref <InvokeCommandsAsync>d__);
			return <InvokeCommandsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005093 File Offset: 0x00003293
		private string GetErrorMessage(Exception e)
		{
			return this.GetErrorMessage(e, this._localization.ConsoleError);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000050A7 File Offset: 0x000032A7
		private string GetInvocationErrorMessage(Exception e)
		{
			return this.GetErrorMessage(e, this._localization.CommandError);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000050BC File Offset: 0x000032BC
		private string GetErrorMessage(Exception e, string label)
		{
			string text = this._verboseErrors ? string.Format("{0} ({1}): {2}\n{3}", new object[]
			{
				label,
				e.GetType(),
				e.Message,
				e.StackTrace
			}) : (label + ": " + e.Message);
			if (!this._theme)
			{
				return text;
			}
			return text.ColorText(this._theme.ErrorColor);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005134 File Offset: 0x00003334
		public void LogToConsoleAsync(string logText, LogType logType = LogType.Log)
		{
			if (!string.IsNullOrWhiteSpace(logText))
			{
				Log log = new Log(logText, logType, true);
				this.LogToConsoleAsync(log);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000515F File Offset: 0x0000335F
		public void LogToConsoleAsync(ILog log)
		{
			Action<ILog> onLog = this.OnLog;
			if (onLog != null)
			{
				onLog(log);
			}
			this._logQueue.QueueLog(log);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005180 File Offset: 0x00003380
		private void FlushQueuedLogs()
		{
			bool flag = false;
			bool flag2 = false;
			ILog log;
			while (this._logQueue.TryDequeue(out log))
			{
				this.AppendLog(log);
				LoggingThreshold loggingThreshold = log.Type.ToLoggingThreshold();
				flag |= (this._autoScroll == AutoScrollOptions.Always);
				flag2 |= (loggingThreshold <= this._openOnLogLevel);
			}
			if (flag)
			{
				this.ScrollConsoleToLatest();
			}
			if (flag2)
			{
				this.Activate(false);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000051E4 File Offset: 0x000033E4
		private void ProcessAsyncTasks()
		{
			for (int i = this._currentTasks.Count - 1; i >= 0; i--)
			{
				if (this._currentTasks[i].IsCompleted)
				{
					if (this._currentTasks[i].IsFaulted)
					{
						using (IEnumerator<Exception> enumerator = this._currentTasks[i].Exception.InnerExceptions.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception e = enumerator.Current;
								string invocationErrorMessage = this.GetInvocationErrorMessage(e);
								this.LogToConsole(invocationErrorMessage, true);
							}
							goto IL_109;
						}
						goto IL_88;
					}
					goto IL_88;
					IL_109:
					this._currentTasks.RemoveAt(i);
					goto IL_115;
					IL_88:
					Type type = this._currentTasks[i].GetType();
					if (type.IsGenericTypeOf(typeof(Task<>)) && !this._voidTaskType.IsAssignableFrom(type))
					{
						object value = this._currentTasks[i].GetType().GetProperty("Result").GetValue(this._currentTasks[i]);
						string logText = this._serializer.SerializeFormatted(value, this._theme);
						this.LogToConsole(logText, true);
						goto IL_109;
					}
					goto IL_109;
				}
				IL_115:;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005324 File Offset: 0x00003524
		public void BeginResponse(Action<string> onSubmitResponseCallback, ResponseConfig config)
		{
			if (onSubmitResponseCallback == null)
			{
				throw new ArgumentNullException("onSubmitResponseCallback");
			}
			this._onSubmitResponseCallback = onSubmitResponseCallback;
			this._currentResponseConfig = config;
			this._isHandlingUserResponse = true;
			Action onStateChange = this.OnStateChange;
			if (onStateChange != null)
			{
				onStateChange();
			}
			this._consoleInput.interactable = true;
			if (this._inputPlaceholderText)
			{
				this._inputPlaceholderText.text = this._currentResponseConfig.InputPrompt;
			}
			this.FocusConsoleInput();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000539A File Offset: 0x0000359A
		public void StartAction(IEnumerator<ICommandAction> action)
		{
			this._currentActions.Add(action);
			this.ProcessActions();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000053AE File Offset: 0x000035AE
		public void CancelAllActions()
		{
			this._currentActions.Clear();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000053BC File Offset: 0x000035BC
		private void ProcessActions()
		{
			if (this._keyConfig.CancelActionsKey.IsPressed())
			{
				this.CancelAllActions();
				return;
			}
			ActionContext context = new ActionContext
			{
				Console = this
			};
			for (int i = this._currentActions.Count - 1; i >= 0; i--)
			{
				IEnumerator<ICommandAction> action = this._currentActions[i];
				try
				{
					if (action.Execute(context) != ActionState.Running)
					{
						this._currentActions.RemoveAt(i);
					}
				}
				catch (Exception e)
				{
					this._currentActions.RemoveAt(i);
					string invocationErrorMessage = this.GetInvocationErrorMessage(e);
					this.LogToConsole(invocationErrorMessage, true);
					break;
				}
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005464 File Offset: 0x00003664
		private void HandleAsyncJobCounter()
		{
			if (this._showCurrentJobs && this._jobCounterRect && this._jobCounterText)
			{
				if (this._currentTasks.Count == 0)
				{
					this._jobCounterRect.gameObject.SetActive(false);
					return;
				}
				this._jobCounterRect.gameObject.SetActive(true);
				this._jobCounterText.text = string.Format("{0} job{1} in progress", this._currentTasks.Count, (this._currentTasks.Count == 1) ? "" : "s");
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005505 File Offset: 0x00003705
		public string Serialize(object value)
		{
			return this._serializer.SerializeFormatted(value, this._theme);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005519 File Offset: 0x00003719
		public void LogToConsole(string logText, bool newLine = true)
		{
			if (!string.IsNullOrEmpty(logText))
			{
				this.LogToConsole(new Log(logText, LogType.Log, newLine));
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005539 File Offset: 0x00003739
		public void LogToConsole(ILog log)
		{
			this.FlushQueuedLogs();
			this.AppendLog(log);
			Action<ILog> onLog = this.OnLog;
			if (onLog != null)
			{
				onLog(log);
			}
			if (this._autoScroll == AutoScrollOptions.Always)
			{
				this.ScrollConsoleToLatest();
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005569 File Offset: 0x00003769
		private void FlushToConsoleText()
		{
			if (this._consoleRequiresFlush)
			{
				this._consoleRequiresFlush = false;
				this._consoleLogText.text = this._logStorage.GetLogString();
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005590 File Offset: 0x00003790
		private ILog TruncateLog(ILog log)
		{
			if (log.Text.Length <= this._maxLogSize || this._maxLogSize < 0)
			{
				return log;
			}
			string text = string.Format(this._localization.MaxLogSizeExceeded, log.Text.Length, this._maxLogSize);
			if (this._theme)
			{
				text = text.ColorText(this._theme.ErrorColor);
			}
			return new Log(text, LogType.Error, true);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005613 File Offset: 0x00003813
		protected void AppendLog(ILog log)
		{
			this._logStorage.AddLog(this.TruncateLog(log));
			this.RequireFlush();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000562D File Offset: 0x0000382D
		protected void RequireFlush()
		{
			this._consoleRequiresFlush = true;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005636 File Offset: 0x00003836
		public void RemoveLogTrace()
		{
			this._logStorage.RemoveLog();
			this.RequireFlush();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005649 File Offset: 0x00003849
		private void ScrollConsoleToLatest()
		{
			if (this._scrollRect)
			{
				this._scrollRect.verticalNormalizedPosition = 0f;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005668 File Offset: 0x00003868
		private void StoreCommand(string command)
		{
			if (this._storeCommandHistory)
			{
				if (!this._storeDuplicateCommands)
				{
					this._previousCommands.Remove(command);
				}
				if (this._storeAdjacentDuplicateCommands || this._previousCommands.Count == 0 || this._previousCommands[this._previousCommands.Count - 1] != command)
				{
					this._previousCommands.Add(command);
				}
				if (this._commandHistorySize > 0 && this._previousCommands.Count > this._commandHistorySize)
				{
					this._previousCommands.RemoveAt(0);
				}
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000056FC File Offset: 0x000038FC
		[Command("clear", "Clears the Quantum Console", MonoTargetType.Registry, Platform.AllPlatforms)]
		public void ClearConsole()
		{
			this._logStorage.Clear();
			this._logQueue.Clear();
			this._consoleLogText.text = string.Empty;
			this._consoleLogText.SetLayoutDirty();
			this.ClearBuffers();
			Action onClear = this.OnClear;
			if (onClear == null)
			{
				return;
			}
			onClear();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005750 File Offset: 0x00003950
		public string GetConsoleText()
		{
			return this._consoleLogText.text;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000575D File Offset: 0x0000395D
		protected virtual void ClearBuffers()
		{
			this.ClearHistoricalSuggestions();
			this.ClearSuggestions();
			this.ClearPopup();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005771 File Offset: 0x00003971
		private void Awake()
		{
			this.InitializeLogging();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000577C File Offset: 0x0000397C
		private void OnEnable()
		{
			QuantumRegistry.RegisterObject<QuantumConsole>(this);
			Application.logMessageReceivedThreaded += this.DebugIntercept;
			if (!this.IsSupportedState())
			{
				this.DisableQC();
				return;
			}
			if (this._singletonMode)
			{
				if (QuantumConsole.Instance == null)
				{
					QuantumConsole.Instance = this;
				}
				else if (QuantumConsole.Instance != this)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
			if (this._activateOnStartup)
			{
				bool shouldFocus = SystemInfo.deviceType == DeviceType.Desktop;
				this.Activate(shouldFocus);
				return;
			}
			if (this._initialiseOnStartup)
			{
				this.Initialize();
			}
			this.Deactivate();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005814 File Offset: 0x00003A14
		private bool IsSupportedState()
		{
			SupportedState supportedState = SupportedState.Always;
			return this._supportedState <= supportedState;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000582F File Offset: 0x00003A2F
		private void OnDisable()
		{
			QuantumRegistry.DeregisterObject<QuantumConsole>(this);
			Application.logMessageReceivedThreaded -= this.DebugIntercept;
			this.Deactivate();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000584E File Offset: 0x00003A4E
		private void DisableQC()
		{
			this.Deactivate();
			base.enabled = false;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005860 File Offset: 0x00003A60
		private void Initialize()
		{
			if (!QuantumConsoleProcessor.TableGenerated)
			{
				QuantumConsoleProcessor.GenerateCommandTable(true, false);
				this._consoleInput.interactable = false;
				this._isGeneratingTable = true;
			}
			this.InitializeSuggestionStack();
			this.InitializeLogging();
			this._consoleLogText.richText = true;
			this._consoleSuggestionText.richText = true;
			this.ApplyTheme(this._theme, false);
			if (!this._keyConfig)
			{
				this._keyConfig = ScriptableObject.CreateInstance<QuantumKeyConfig>();
			}
			if (!this._localization)
			{
				this._localization = ScriptableObject.CreateInstance<QuantumLocalization>();
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000058EF File Offset: 0x00003AEF
		private void InitializeSuggestionStack()
		{
			if (this._suggestionStack == null)
			{
				this._suggestionStack = this.CreateSuggestionStack();
				this._suggestionStack.OnSuggestionSetCreated += this.OnSuggestionSetGenerated;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005916 File Offset: 0x00003B16
		private void InitializeLogging()
		{
			this._logStorage = (this._logStorage ?? this.CreateLogStorage());
			this._logQueue = (this._logQueue ?? this.CreateLogQueue());
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005944 File Offset: 0x00003B44
		protected virtual ILogStorage CreateLogStorage()
		{
			return new LogStorage(this._maxStoredLogs);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005951 File Offset: 0x00003B51
		protected virtual ILogQueue CreateLogQueue()
		{
			return new LogQueue(this._maxStoredLogs);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000595E File Offset: 0x00003B5E
		protected virtual SuggestionStack CreateSuggestionStack()
		{
			return new SuggestionStack();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005965 File Offset: 0x00003B65
		public void Toggle()
		{
			if (this.IsActive)
			{
				this.Deactivate();
				return;
			}
			this.Activate(true);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005980 File Offset: 0x00003B80
		public void Activate(bool shouldFocus = true)
		{
			this.Initialize();
			this.IsActive = true;
			this._containerRect.gameObject.SetActive(true);
			this.OverrideConsoleInput(string.Empty, shouldFocus);
			if (!EventSystem.current)
			{
				UnityEngine.Debug.LogWarning("Quantum Console's UI requires an EventSystem in the scene but there were none present.");
			}
			Action onActivate = this.OnActivate;
			if (onActivate == null)
			{
				return;
			}
			onActivate();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000059DD File Offset: 0x00003BDD
		public void Deactivate()
		{
			this.IsActive = false;
			this._containerRect.gameObject.SetActive(false);
			Action onDeactivate = this.OnDeactivate;
			if (onDeactivate == null)
			{
				return;
			}
			onDeactivate();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005A08 File Offset: 0x00003C08
		private void DebugIntercept(string condition, string stackTrace, LogType type)
		{
			if (this._interceptDebugLogger && (this.IsActive || this._interceptWhilstInactive) && this._loggingLevel >= type.ToLoggingThreshold())
			{
				bool appendStackTrace = this._verboseLogging >= type.ToLoggingThreshold();
				ILog log = this.ConstructDebugLog(condition, stackTrace, type, this._prependTimestamps, appendStackTrace);
				this.LogToConsoleAsync(log);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005A68 File Offset: 0x00003C68
		protected virtual ILog ConstructDebugLog(string condition, string stackTrace, LogType type, bool prependTimeStamp, bool appendStackTrace)
		{
			if (prependTimeStamp)
			{
				DateTime now = DateTime.Now;
				condition = string.Format(this._theme ? this._theme.TimestampFormat : "[{0:00}:{1:00}:{2:00}]", now.Hour, now.Minute, now.Second) + " " + condition;
			}
			if (appendStackTrace)
			{
				condition = condition + "\n" + stackTrace;
			}
			if (this._theme)
			{
				switch (type)
				{
				case LogType.Error:
				case LogType.Assert:
				case LogType.Exception:
					condition = condition.ColorText(this._theme.ErrorColor);
					break;
				case LogType.Warning:
					condition = condition.ColorText(this._theme.WarningColor);
					break;
				}
			}
			return new Log(condition, type, true);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005B44 File Offset: 0x00003D44
		protected virtual void OnValidate()
		{
			this.MaxStoredLogs = this._maxStoredLogs;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005B54 File Offset: 0x00003D54
		public QuantumConsole()
		{
		}

		// Token: 0x0400007F RID: 127
		[CompilerGenerated]
		private static QuantumConsole <Instance>k__BackingField;

		// Token: 0x04000080 RID: 128
		public static bool IsDev;

		// Token: 0x04000081 RID: 129
		[SerializeField]
		private RectTransform _containerRect;

		// Token: 0x04000082 RID: 130
		[SerializeField]
		private ScrollRect _scrollRect;

		// Token: 0x04000083 RID: 131
		[SerializeField]
		private RectTransform _suggestionPopupRect;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		private RectTransform _jobCounterRect;

		// Token: 0x04000085 RID: 133
		[SerializeField]
		private Image[] _panels;

		// Token: 0x04000086 RID: 134
		[SerializeField]
		private QuantumTheme _theme;

		// Token: 0x04000087 RID: 135
		[SerializeField]
		private QuantumKeyConfig _keyConfig;

		// Token: 0x04000088 RID: 136
		[SerializeField]
		private QuantumLocalization _localization;

		// Token: 0x04000089 RID: 137
		[SerializeField]
		private bool _verboseErrors;

		// Token: 0x0400008A RID: 138
		[SerializeField]
		private LoggingThreshold _verboseLogging;

		// Token: 0x0400008B RID: 139
		[SerializeField]
		private LoggingThreshold _loggingLevel = LoggingThreshold.Always;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		private LoggingThreshold _openOnLogLevel;

		// Token: 0x0400008D RID: 141
		[SerializeField]
		private bool _interceptDebugLogger = true;

		// Token: 0x0400008E RID: 142
		[SerializeField]
		private bool _interceptWhilstInactive = true;

		// Token: 0x0400008F RID: 143
		[SerializeField]
		private bool _prependTimestamps;

		// Token: 0x04000090 RID: 144
		[SerializeField]
		private SupportedState _supportedState;

		// Token: 0x04000091 RID: 145
		[SerializeField]
		private bool _activateOnStartup = true;

		// Token: 0x04000092 RID: 146
		[SerializeField]
		private bool _initialiseOnStartup;

		// Token: 0x04000093 RID: 147
		[SerializeField]
		private bool _closeOnSubmit;

		// Token: 0x04000094 RID: 148
		[SerializeField]
		private bool _singletonMode;

		// Token: 0x04000095 RID: 149
		[SerializeField]
		private AutoScrollOptions _autoScroll = AutoScrollOptions.OnInvoke;

		// Token: 0x04000096 RID: 150
		[SerializeField]
		private bool _enableAutocomplete = true;

		// Token: 0x04000097 RID: 151
		[SerializeField]
		private bool _showPopupDisplay = true;

		// Token: 0x04000098 RID: 152
		[SerializeField]
		private SortOrder _suggestionDisplayOrder = SortOrder.Descending;

		// Token: 0x04000099 RID: 153
		[SerializeField]
		private int _maxSuggestionDisplaySize = -1;

		// Token: 0x0400009A RID: 154
		[SerializeField]
		private bool _useFuzzySearch;

		// Token: 0x0400009B RID: 155
		[SerializeField]
		private bool _caseSensitiveSearch = true;

		// Token: 0x0400009C RID: 156
		[SerializeField]
		private bool _collapseSuggestionOverloads = true;

		// Token: 0x0400009D RID: 157
		[SerializeField]
		private bool _showCurrentJobs = true;

		// Token: 0x0400009E RID: 158
		[SerializeField]
		private bool _blockOnAsync;

		// Token: 0x0400009F RID: 159
		[SerializeField]
		private bool _storeCommandHistory = true;

		// Token: 0x040000A0 RID: 160
		[SerializeField]
		private bool _storeDuplicateCommands = true;

		// Token: 0x040000A1 RID: 161
		[SerializeField]
		private bool _storeAdjacentDuplicateCommands;

		// Token: 0x040000A2 RID: 162
		[SerializeField]
		private int _commandHistorySize = -1;

		// Token: 0x040000A3 RID: 163
		[SerializeField]
		private int _maxStoredLogs = 1024;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		private int _maxLogSize = 8192;

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		private bool _showInitLogs = true;

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private TMP_InputField _consoleInput;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private TextMeshProUGUI _inputPlaceholderText;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private TextMeshProUGUI _consoleLogText;

		// Token: 0x040000A9 RID: 169
		[SerializeField]
		private TextMeshProUGUI _consoleSuggestionText;

		// Token: 0x040000AA RID: 170
		[SerializeField]
		private TextMeshProUGUI _suggestionPopupText;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private TextMeshProUGUI _jobCounterText;

		// Token: 0x040000AC RID: 172
		[CompilerGenerated]
		private Action OnStateChange;

		// Token: 0x040000AD RID: 173
		[CompilerGenerated]
		private Action<string> OnInvoke;

		// Token: 0x040000AE RID: 174
		[CompilerGenerated]
		private Action OnClear;

		// Token: 0x040000AF RID: 175
		[CompilerGenerated]
		private Action<ILog> OnLog;

		// Token: 0x040000B0 RID: 176
		[CompilerGenerated]
		private Action OnActivate;

		// Token: 0x040000B1 RID: 177
		[CompilerGenerated]
		private Action OnDeactivate;

		// Token: 0x040000B2 RID: 178
		[CompilerGenerated]
		private Action<SuggestionSet> OnSuggestionSetGenerated;

		// Token: 0x040000B3 RID: 179
		private readonly QuantumSerializer _serializer = new QuantumSerializer();

		// Token: 0x040000B4 RID: 180
		private SuggestionStack _suggestionStack;

		// Token: 0x040000B5 RID: 181
		private ILogStorage _logStorage;

		// Token: 0x040000B6 RID: 182
		private ILogQueue _logQueue;

		// Token: 0x040000B7 RID: 183
		[CompilerGenerated]
		private bool <IsActive>k__BackingField;

		// Token: 0x040000B8 RID: 184
		private readonly List<string> _previousCommands = new List<string>();

		// Token: 0x040000B9 RID: 185
		private readonly List<Task> _currentTasks = new List<Task>();

		// Token: 0x040000BA RID: 186
		private readonly List<IEnumerator<ICommandAction>> _currentActions = new List<IEnumerator<ICommandAction>>();

		// Token: 0x040000BB RID: 187
		private readonly StringBuilderPool _stringBuilderPool = new StringBuilderPool();

		// Token: 0x040000BC RID: 188
		private int _selectedPreviousCommandIndex = -1;

		// Token: 0x040000BD RID: 189
		private string _currentInput;

		// Token: 0x040000BE RID: 190
		private string _previousInput;

		// Token: 0x040000BF RID: 191
		private bool _isGeneratingTable;

		// Token: 0x040000C0 RID: 192
		private bool _consoleRequiresFlush;

		// Token: 0x040000C1 RID: 193
		private bool _isHandlingUserResponse;

		// Token: 0x040000C2 RID: 194
		private ResponseConfig _currentResponseConfig;

		// Token: 0x040000C3 RID: 195
		private Action<string> _onSubmitResponseCallback;

		// Token: 0x040000C4 RID: 196
		private TextMeshProUGUI[] _textComponents;

		// Token: 0x040000C5 RID: 197
		private readonly Type _voidTaskType = typeof(Task<>).MakeGenericType(new Type[]
		{
			Type.GetType("System.Threading.Tasks.VoidTaskResult")
		});

		// Token: 0x0200008C RID: 140
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InvokeExternalCommandsAsync>d__132 : IAsyncStateMachine
		{
			// Token: 0x060002C7 RID: 711 RVA: 0x0000B044 File Offset: 0x00009244
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				QuantumConsole quantumConsole = this.<>4__this;
				try
				{
					if (num > 1)
					{
						this.<reader>5__2 = new StreamReader(this.filePath);
					}
					try
					{
						TaskAwaiter awaiter;
						TaskAwaiter<string> awaiter2;
						if (num != 0)
						{
							if (num != 1)
							{
								goto IL_106;
							}
							awaiter = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_F9;
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<string>);
							num = (this.<>1__state = -1);
						}
						IL_8F:
						string result = awaiter2.GetResult();
						Task task = quantumConsole.InvokeCommand(result) as Task;
						if (task == null)
						{
							goto IL_106;
						}
						awaiter = task.GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, QuantumConsole.<InvokeExternalCommandsAsync>d__132>(ref awaiter, ref this);
							return;
						}
						IL_F9:
						awaiter.GetResult();
						quantumConsole.ProcessAsyncTasks();
						IL_106:
						if (!this.<reader>5__2.EndOfStream)
						{
							awaiter2 = this.<reader>5__2.ReadLineAsync().GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, QuantumConsole.<InvokeExternalCommandsAsync>d__132>(ref awaiter2, ref this);
								return;
							}
							goto IL_8F;
						}
					}
					finally
					{
						if (num < 0 && this.<reader>5__2 != null)
						{
							((IDisposable)this.<reader>5__2).Dispose();
						}
					}
					this.<reader>5__2 = null;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x0000B1EC File Offset: 0x000093EC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000198 RID: 408
			public int <>1__state;

			// Token: 0x04000199 RID: 409
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400019A RID: 410
			public string filePath;

			// Token: 0x0400019B RID: 411
			public QuantumConsole <>4__this;

			// Token: 0x0400019C RID: 412
			private StreamReader <reader>5__2;

			// Token: 0x0400019D RID: 413
			private TaskAwaiter<string> <>u__1;

			// Token: 0x0400019E RID: 414
			private TaskAwaiter <>u__2;
		}

		// Token: 0x0200008D RID: 141
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InvokeCommandsAsync>d__133 : IAsyncStateMachine
		{
			// Token: 0x060002C9 RID: 713 RVA: 0x0000B1FC File Offset: 0x000093FC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				QuantumConsole quantumConsole = this.<>4__this;
				try
				{
					if (num != 0)
					{
						this.<>7__wrap1 = this.commands.GetEnumerator();
					}
					try
					{
						TaskAwaiter awaiter;
						if (num == 0)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_96;
						}
						IL_A3:
						while (this.<>7__wrap1.MoveNext())
						{
							string command = this.<>7__wrap1.Current;
							Task task = quantumConsole.InvokeCommand(command) as Task;
							if (task != null)
							{
								awaiter = task.GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, QuantumConsole.<InvokeCommandsAsync>d__133>(ref awaiter, ref this);
									return;
								}
								goto IL_96;
							}
						}
						goto IL_CD;
						IL_96:
						awaiter.GetResult();
						quantumConsole.ProcessAsyncTasks();
						goto IL_A3;
					}
					finally
					{
						if (num < 0 && this.<>7__wrap1 != null)
						{
							this.<>7__wrap1.Dispose();
						}
					}
					IL_CD:
					this.<>7__wrap1 = null;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060002CA RID: 714 RVA: 0x0000B328 File Offset: 0x00009528
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400019F RID: 415
			public int <>1__state;

			// Token: 0x040001A0 RID: 416
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040001A1 RID: 417
			public IEnumerable<string> commands;

			// Token: 0x040001A2 RID: 418
			public QuantumConsole <>4__this;

			// Token: 0x040001A3 RID: 419
			private IEnumerator<string> <>7__wrap1;

			// Token: 0x040001A4 RID: 420
			private TaskAwaiter <>u__1;
		}
	}
}
