using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Febucci.UI.Actions;
using Febucci.UI.Core.Parsing;
using UnityEngine;
using UnityEngine.Events;

namespace Febucci.UI.Core
{
	// Token: 0x02000037 RID: 55
	[DisallowMultipleComponent]
	[RequireComponent(typeof(TAnimCore))]
	public abstract class TypewriterCore : MonoBehaviour
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000062E0 File Offset: 0x000044E0
		public TAnimCore TextAnimator
		{
			get
			{
				if (this._textAnimator != null)
				{
					return this._textAnimator;
				}
				if (!base.TryGetComponent<TAnimCore>(out this._textAnimator))
				{
					Debug.LogError("TextAnimator: Text Animator component is null on GameObject " + base.gameObject.name + ". Please add a component that inherits from TAnimCore");
				}
				return this._textAnimator;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006338 File Offset: 0x00004538
		public void ShowText(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				this.TextAnimator.SetText(string.Empty, true);
				return;
			}
			this.TextAnimator.SetText(text, this.useTypeWriter);
			this.TextAnimator.firstVisibleCharacter = 0;
			if (this.useTypeWriter)
			{
				if (this.startTypewriterMode.HasFlag(TypewriterCore.StartTypewriterMode.OnShowText))
				{
					this.StartShowingText(true);
				}
				return;
			}
			UnityEvent unityEvent = this.onTextShowed;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000063B8 File Offset: 0x000045B8
		public void SkipTypewriter()
		{
			if (this.isShowingText)
			{
				base.StopAllCoroutines();
				this.isShowingText = false;
				this.TextAnimator.SetVisibilityEntireText(true, !this.hideAppearancesOnSkip);
				if (this.triggerEventsOnSkip)
				{
					this.TriggerEventsUntil(int.MaxValue);
				}
				UnityEvent unityEvent = this.onTextShowed;
				if (unityEvent == null)
				{
					return;
				}
				unityEvent.Invoke();
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006412 File Offset: 0x00004612
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000641A File Offset: 0x0000461A
		public bool isShowingText
		{
			[CompilerGenerated]
			get
			{
				return this.<isShowingText>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isShowingText>k__BackingField = value;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006424 File Offset: 0x00004624
		public void StartShowingText(bool restart = false)
		{
			if (this.TextAnimator.CharactersCount == 0)
			{
				return;
			}
			if (!this.useTypeWriter)
			{
				Debug.LogWarning("TextAnimator: couldn't start coroutine because 'useTypewriter' is disabled");
				return;
			}
			if (this.isShowingText)
			{
				this.StopShowingText();
			}
			if (restart)
			{
				this.TextAnimator.SetVisibilityEntireText(false, false);
				this.latestActionTriggered = 0;
				this.latestEventTriggered = 0;
			}
			if (this.resetTypingSpeedAtStartup)
			{
				this.internalSpeed = 1f;
			}
			this.isShowingText = true;
			this.showRoutine = base.StartCoroutine(this.ShowTextRoutine());
		}

		// Token: 0x06000132 RID: 306
		protected abstract float GetWaitAppearanceTimeOf(int charIndex);

		// Token: 0x06000133 RID: 307 RVA: 0x000064AA File Offset: 0x000046AA
		private float GetDeltaTime(TypingInfo typingInfo)
		{
			return this.TextAnimator.time.deltaTime * this.internalSpeed * typingInfo.speed;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000064CA File Offset: 0x000046CA
		private IEnumerator ShowTextRoutine()
		{
			this.isShowingText = true;
			TypingInfo typingInfo = new TypingInfo();
			UnityEvent unityEvent = this.onTypewriterStart;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			TextAnimatorSettings instance = TextAnimatorSettings.Instance;
			bool actionsEnabled = instance && instance.actions.enabled;
			int num;
			for (int i = 0; i < this.TextAnimator.CharactersCount; i = num + 1)
			{
				if (actionsEnabled)
				{
					int maxIndex = i + 1;
					int a = this.latestActionTriggered;
					while (a < this.TextAnimator.Actions.Length && this.TextAnimator.Actions[a].index < maxIndex)
					{
						ActionMarker actionMarker = this.TextAnimator.Actions[a];
						this.TriggerEventsBeforeAction(maxIndex, actionMarker);
						ActionScriptableBase actionScriptableBase = this.TextAnimator.DatabaseActions[actionMarker.name];
						yield return this.nestedActionRoutine = base.StartCoroutine((actionScriptableBase != null) ? actionScriptableBase.DoAction(actionMarker, this, typingInfo) : null);
						this.latestActionTriggered = a + 1;
						num = a;
						a = num + 1;
					}
				}
				this.TriggerEventsUntil(i + 1);
				if (!this.TextAnimator.Characters[i].isVisible)
				{
					this.TextAnimator.SetVisibilityChar(i, true);
					CharacterEvent characterEvent = this.onCharacterVisible;
					if (characterEvent != null)
					{
						characterEvent.Invoke(this.TextAnimator.latestCharacterShown.info.character);
					}
					float timeToWait = this.GetWaitAppearanceTimeOf(i);
					float deltaTime = this.GetDeltaTime(typingInfo);
					if (timeToWait < 0f)
					{
						timeToWait = 0f;
					}
					if (timeToWait < deltaTime)
					{
						typingInfo.timePassed += timeToWait;
						if (typingInfo.timePassed >= deltaTime)
						{
							yield return null;
							typingInfo.timePassed %= deltaTime;
						}
					}
					else
					{
						while (typingInfo.timePassed < timeToWait)
						{
							typingInfo.timePassed += deltaTime;
							yield return null;
							deltaTime = this.GetDeltaTime(typingInfo);
						}
						typingInfo.timePassed %= timeToWait;
					}
				}
				num = i;
			}
			if (actionsEnabled)
			{
				int i = this.latestActionTriggered;
				while (i < this.TextAnimator.Actions.Length && this.TextAnimator.Actions[i].index < 2147483647)
				{
					ActionMarker actionMarker2 = this.TextAnimator.Actions[i];
					this.TriggerEventsBeforeAction(int.MaxValue, actionMarker2);
					ActionScriptableBase actionScriptableBase2 = this.TextAnimator.DatabaseActions[actionMarker2.name];
					yield return this.nestedActionRoutine = base.StartCoroutine((actionScriptableBase2 != null) ? actionScriptableBase2.DoAction(actionMarker2, this, typingInfo) : null);
					this.latestActionTriggered = i + 1;
					num = i;
					i = num + 1;
				}
			}
			this.TriggerEventsUntil(int.MaxValue);
			UnityEvent unityEvent2 = this.onTextShowed;
			if (unityEvent2 != null)
			{
				unityEvent2.Invoke();
			}
			this.isShowingText = false;
			yield break;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000064DC File Offset: 0x000046DC
		public void StopShowingText()
		{
			if (!this.isShowingText)
			{
				return;
			}
			this.isShowingText = false;
			if (this.showRoutine != null)
			{
				base.StopCoroutine(this.showRoutine);
			}
			if (this.nestedActionRoutine != null)
			{
				base.StopCoroutine(this.nestedActionRoutine);
			}
			if (this.isHidingText)
			{
				this.StartDisappearingText();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000652F File Offset: 0x0000472F
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00006537 File Offset: 0x00004737
		public bool isHidingText
		{
			[CompilerGenerated]
			get
			{
				return this.<isHidingText>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isHidingText>k__BackingField = value;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006540 File Offset: 0x00004740
		[ContextMenu("Start Disappearing Text")]
		public void StartDisappearingText()
		{
			if (this.disappearanceOrientation == TypewriterCore.DisappearanceOrientation.Inverted && this.isShowingText)
			{
				Debug.LogWarning("TextAnimatorPlayer: Can't start disappearance routine in the opposite direction of the typewriter, because you're still showing the text! (the typewriter might get stuck trying to show and override letters that keep disappearing)");
				return;
			}
			if (this.isHidingText)
			{
				return;
			}
			this.hideRoutine = base.StartCoroutine(this.HideTextRoutine());
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000657C File Offset: 0x0000477C
		[ContextMenu("Stop Disappearing Text")]
		public void StopDisappearingText()
		{
			if (!this.isHidingText)
			{
				return;
			}
			this.isHidingText = false;
			if (this.hideRoutine != null)
			{
				base.StopCoroutine(this.hideRoutine);
			}
			if (this.nestedHideRoutine != null)
			{
				base.StopCoroutine(this.nestedHideRoutine);
			}
			if (this.isShowingText)
			{
				this.StartShowingText(false);
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000065D0 File Offset: 0x000047D0
		protected virtual float GetWaitDisappearanceTimeOf(int charIndex)
		{
			return this.GetWaitAppearanceTimeOf(charIndex);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000065D9 File Offset: 0x000047D9
		private IEnumerator HideTextRoutine()
		{
			this.isHidingText = true;
			TypingInfo typingInfo = new TypingInfo();
			int num;
			for (int i = 0; i < this.TextAnimator.CharactersCount; i = num + 1)
			{
				if (this.TextAnimator.Characters[i].isVisible)
				{
					this.TextAnimator.SetVisibilityChar(i, false);
					float timeToWait = this.GetWaitDisappearanceTimeOf(i);
					float deltaTime = this.GetDeltaTime(typingInfo);
					if (timeToWait < 0f)
					{
						timeToWait = 0f;
					}
					if (timeToWait < deltaTime)
					{
						typingInfo.timePassed += timeToWait;
						if (typingInfo.timePassed >= deltaTime)
						{
							yield return null;
							typingInfo.timePassed %= deltaTime;
						}
					}
					else
					{
						while (typingInfo.timePassed < timeToWait)
						{
							typingInfo.timePassed += deltaTime;
							yield return null;
							deltaTime = this.GetDeltaTime(typingInfo);
						}
						typingInfo.timePassed %= timeToWait;
					}
				}
				num = i;
			}
			UnityEvent unityEvent = this.onTextDisappeared;
			if (unityEvent != null)
			{
				unityEvent.Invoke();
			}
			this.isHidingText = false;
			yield break;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000065E8 File Offset: 0x000047E8
		public void SetTypewriterSpeed(float value)
		{
			this.internalSpeed = Mathf.Clamp(value, 0.001f, value);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000065FC File Offset: 0x000047FC
		private void TriggerEventsBeforeAction(int maxIndex, ActionMarker action)
		{
			int num = this.latestEventTriggered;
			while (num < this.TextAnimator.Events.Length && this.TextAnimator.Events[num].index < maxIndex && this.TextAnimator.Events[num].internalOrder < action.internalOrder)
			{
				MessageEvent messageEvent = this.onMessage;
				if (messageEvent != null)
				{
					messageEvent.Invoke(this.TextAnimator.Events[num]);
				}
				this.latestEventTriggered = num + 1;
				num++;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000667C File Offset: 0x0000487C
		private void TriggerEventsUntil(int maxIndex)
		{
			int num = this.latestEventTriggered;
			while (num < this.TextAnimator.Events.Length && this.TextAnimator.Events[num].index < maxIndex)
			{
				MessageEvent messageEvent = this.onMessage;
				if (messageEvent != null)
				{
					messageEvent.Invoke(this.TextAnimator.Events[num]);
				}
				this.latestEventTriggered = num + 1;
				num++;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000066E2 File Offset: 0x000048E2
		public void TriggerRemainingEvents()
		{
			this.TriggerEventsUntil(int.MaxValue);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000066EF File Offset: 0x000048EF
		public void TriggerVisibleEvents()
		{
			this.TriggerEventsUntil(this.TextAnimator.latestCharacterShown.index);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006707 File Offset: 0x00004907
		protected virtual void OnEnable()
		{
			if (!this.useTypeWriter)
			{
				return;
			}
			if (!this.startTypewriterMode.HasFlag(TypewriterCore.StartTypewriterMode.OnEnable))
			{
				return;
			}
			this.StartShowingText(false);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006732 File Offset: 0x00004932
		protected virtual void OnDisable()
		{
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006734 File Offset: 0x00004934
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000673C File Offset: 0x0000493C
		[Obsolete("Please set the speed through 'SetTypewriterSpeed' method instead")]
		protected float typewriterPlayerSpeed
		{
			get
			{
				return this.internalSpeed;
			}
			set
			{
				this.SetTypewriterSpeed(value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006745 File Offset: 0x00004945
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000674C File Offset: 0x0000494C
		[Obsolete("Please skip the typewriter via the 'SkipTypewriter' method instead")]
		protected bool wantsToSkip
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				if (value)
				{
					this.SkipTypewriter();
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006757 File Offset: 0x00004957
		[Obsolete("Please use 'isShowingText' instead")]
		protected bool isBaseInsideRoutine
		{
			get
			{
				return this.isShowingText;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000675F File Offset: 0x0000495F
		[Obsolete("Please use 'TextAnimator' instead")]
		public TAnimCore textAnimator
		{
			get
			{
				return this.TextAnimator;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006768 File Offset: 0x00004968
		protected TypewriterCore()
		{
		}

		// Token: 0x040000C6 RID: 198
		private TAnimCore _textAnimator;

		// Token: 0x040000C7 RID: 199
		[Tooltip("True if you want to shows the text dynamically")]
		[SerializeField]
		public bool useTypeWriter = true;

		// Token: 0x040000C8 RID: 200
		[SerializeField]
		[Tooltip("Controls from which method(s) the typewriter will automatically start/resume. Default is 'Automatic'")]
		public TypewriterCore.StartTypewriterMode startTypewriterMode = TypewriterCore.StartTypewriterMode.AutomaticallyFromAllEvents;

		// Token: 0x040000C9 RID: 201
		[SerializeField]
		private bool hideAppearancesOnSkip;

		// Token: 0x040000CA RID: 202
		[SerializeField]
		[Tooltip("True = plays all remaining events once the typewriter has been skipped")]
		private bool triggerEventsOnSkip;

		// Token: 0x040000CB RID: 203
		[SerializeField]
		[Tooltip("True = resets the typewriter speed every time a new text is set/shown")]
		public bool resetTypingSpeedAtStartup = true;

		// Token: 0x040000CC RID: 204
		[SerializeField]
		public TypewriterCore.DisappearanceOrientation disappearanceOrientation;

		// Token: 0x040000CD RID: 205
		public UnityEvent onTextShowed = new UnityEvent();

		// Token: 0x040000CE RID: 206
		public UnityEvent onTypewriterStart = new UnityEvent();

		// Token: 0x040000CF RID: 207
		public UnityEvent onTextDisappeared = new UnityEvent();

		// Token: 0x040000D0 RID: 208
		public CharacterEvent onCharacterVisible = new CharacterEvent();

		// Token: 0x040000D1 RID: 209
		public MessageEvent onMessage = new MessageEvent();

		// Token: 0x040000D2 RID: 210
		[CompilerGenerated]
		private bool <isShowingText>k__BackingField;

		// Token: 0x040000D3 RID: 211
		private Coroutine showRoutine;

		// Token: 0x040000D4 RID: 212
		private Coroutine nestedActionRoutine;

		// Token: 0x040000D5 RID: 213
		[CompilerGenerated]
		private bool <isHidingText>k__BackingField;

		// Token: 0x040000D6 RID: 214
		private Coroutine hideRoutine;

		// Token: 0x040000D7 RID: 215
		private Coroutine nestedHideRoutine;

		// Token: 0x040000D8 RID: 216
		private float internalSpeed = 1f;

		// Token: 0x040000D9 RID: 217
		private int latestActionTriggered;

		// Token: 0x040000DA RID: 218
		private int latestEventTriggered;

		// Token: 0x0200005A RID: 90
		[Flags]
		public enum StartTypewriterMode
		{
			// Token: 0x0400013C RID: 316
			FromScriptOnly = 0,
			// Token: 0x0400013D RID: 317
			OnEnable = 1,
			// Token: 0x0400013E RID: 318
			OnShowText = 2,
			// Token: 0x0400013F RID: 319
			AutomaticallyFromAllEvents = 3
		}

		// Token: 0x0200005B RID: 91
		public enum DisappearanceOrientation
		{
			// Token: 0x04000141 RID: 321
			SameAsTypewriter,
			// Token: 0x04000142 RID: 322
			Inverted
		}

		// Token: 0x0200005C RID: 92
		[CompilerGenerated]
		private sealed class <ShowTextRoutine>d__27 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060001AD RID: 429 RVA: 0x00007D2F File Offset: 0x00005F2F
			[DebuggerHidden]
			public <ShowTextRoutine>d__27(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060001AE RID: 430 RVA: 0x00007D3E File Offset: 0x00005F3E
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060001AF RID: 431 RVA: 0x00007D40 File Offset: 0x00005F40
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TypewriterCore typewriterCore = this;
				int num2;
				switch (num)
				{
				case 0:
				{
					this.<>1__state = -1;
					typewriterCore.isShowingText = true;
					typingInfo = new TypingInfo();
					UnityEvent onTypewriterStart = typewriterCore.onTypewriterStart;
					if (onTypewriterStart != null)
					{
						onTypewriterStart.Invoke();
					}
					TextAnimatorSettings instance = TextAnimatorSettings.Instance;
					actionsEnabled = (instance && instance.actions.enabled);
					i = 0;
					goto IL_304;
				}
				case 1:
					this.<>1__state = -1;
					typewriterCore.latestActionTriggered = a + 1;
					num2 = a;
					a = num2 + 1;
					break;
				case 2:
					this.<>1__state = -1;
					typingInfo.timePassed %= deltaTime;
					goto IL_2F2;
				case 3:
					this.<>1__state = -1;
					deltaTime = typewriterCore.GetDeltaTime(typingInfo);
					goto IL_2C7;
				case 4:
					this.<>1__state = -1;
					typewriterCore.latestActionTriggered = i + 1;
					num2 = i;
					i = num2 + 1;
					goto IL_3CA;
				default:
					return false;
				}
				IL_13D:
				if (a < typewriterCore.TextAnimator.Actions.Length && typewriterCore.TextAnimator.Actions[a].index < maxIndex)
				{
					ActionMarker actionMarker = typewriterCore.TextAnimator.Actions[a];
					typewriterCore.TriggerEventsBeforeAction(maxIndex, actionMarker);
					TypewriterCore typewriterCore2 = typewriterCore;
					MonoBehaviour monoBehaviour = typewriterCore;
					ActionScriptableBase actionScriptableBase = typewriterCore.TextAnimator.DatabaseActions[actionMarker.name];
					this.<>2__current = (typewriterCore2.nestedActionRoutine = monoBehaviour.StartCoroutine((actionScriptableBase != null) ? actionScriptableBase.DoAction(actionMarker, typewriterCore, typingInfo) : null));
					this.<>1__state = 1;
					return true;
				}
				IL_174:
				typewriterCore.TriggerEventsUntil(i + 1);
				if (typewriterCore.TextAnimator.Characters[i].isVisible)
				{
					goto IL_2F2;
				}
				typewriterCore.TextAnimator.SetVisibilityChar(i, true);
				CharacterEvent onCharacterVisible = typewriterCore.onCharacterVisible;
				if (onCharacterVisible != null)
				{
					onCharacterVisible.Invoke(typewriterCore.TextAnimator.latestCharacterShown.info.character);
				}
				timeToWait = typewriterCore.GetWaitAppearanceTimeOf(i);
				deltaTime = typewriterCore.GetDeltaTime(typingInfo);
				if (timeToWait < 0f)
				{
					timeToWait = 0f;
				}
				if (timeToWait < deltaTime)
				{
					typingInfo.timePassed += timeToWait;
					if (typingInfo.timePassed >= deltaTime)
					{
						this.<>2__current = null;
						this.<>1__state = 2;
						return true;
					}
					goto IL_2F2;
				}
				IL_2C7:
				if (typingInfo.timePassed < timeToWait)
				{
					typingInfo.timePassed += deltaTime;
					this.<>2__current = null;
					this.<>1__state = 3;
					return true;
				}
				typingInfo.timePassed %= timeToWait;
				IL_2F2:
				num2 = i;
				i = num2 + 1;
				IL_304:
				if (i >= typewriterCore.TextAnimator.CharactersCount)
				{
					if (!actionsEnabled)
					{
						goto IL_400;
					}
					i = typewriterCore.latestActionTriggered;
				}
				else
				{
					if (actionsEnabled)
					{
						maxIndex = i + 1;
						a = typewriterCore.latestActionTriggered;
						goto IL_13D;
					}
					goto IL_174;
				}
				IL_3CA:
				if (i < typewriterCore.TextAnimator.Actions.Length && typewriterCore.TextAnimator.Actions[i].index < 2147483647)
				{
					ActionMarker actionMarker2 = typewriterCore.TextAnimator.Actions[i];
					typewriterCore.TriggerEventsBeforeAction(int.MaxValue, actionMarker2);
					TypewriterCore typewriterCore3 = typewriterCore;
					MonoBehaviour monoBehaviour2 = typewriterCore;
					ActionScriptableBase actionScriptableBase2 = typewriterCore.TextAnimator.DatabaseActions[actionMarker2.name];
					this.<>2__current = (typewriterCore3.nestedActionRoutine = monoBehaviour2.StartCoroutine((actionScriptableBase2 != null) ? actionScriptableBase2.DoAction(actionMarker2, typewriterCore, typingInfo) : null));
					this.<>1__state = 4;
					return true;
				}
				IL_400:
				typewriterCore.TriggerEventsUntil(int.MaxValue);
				UnityEvent onTextShowed = typewriterCore.onTextShowed;
				if (onTextShowed != null)
				{
					onTextShowed.Invoke();
				}
				typewriterCore.isShowingText = false;
				return false;
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008171 File Offset: 0x00006371
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060001B1 RID: 433 RVA: 0x00008179 File Offset: 0x00006379
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060001B2 RID: 434 RVA: 0x00008180 File Offset: 0x00006380
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000143 RID: 323
			private int <>1__state;

			// Token: 0x04000144 RID: 324
			private object <>2__current;

			// Token: 0x04000145 RID: 325
			public TypewriterCore <>4__this;

			// Token: 0x04000146 RID: 326
			private TypingInfo <typingInfo>5__2;

			// Token: 0x04000147 RID: 327
			private bool <actionsEnabled>5__3;

			// Token: 0x04000148 RID: 328
			private int <i>5__4;

			// Token: 0x04000149 RID: 329
			private float <timeToWait>5__5;

			// Token: 0x0400014A RID: 330
			private float <deltaTime>5__6;

			// Token: 0x0400014B RID: 331
			private int <maxIndex>5__7;

			// Token: 0x0400014C RID: 332
			private int <a>5__8;
		}

		// Token: 0x0200005D RID: 93
		[CompilerGenerated]
		private sealed class <HideTextRoutine>d__38 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060001B3 RID: 435 RVA: 0x00008188 File Offset: 0x00006388
			[DebuggerHidden]
			public <HideTextRoutine>d__38(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x00008197 File Offset: 0x00006397
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x0000819C File Offset: 0x0000639C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TypewriterCore typewriterCore = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					typewriterCore.isHidingText = true;
					typingInfo = new TypingInfo();
					i = 0;
					goto IL_1A1;
				case 1:
					this.<>1__state = -1;
					typingInfo.timePassed %= deltaTime;
					goto IL_191;
				case 2:
					this.<>1__state = -1;
					deltaTime = typewriterCore.GetDeltaTime(typingInfo);
					break;
				default:
					return false;
				}
				IL_166:
				if (typingInfo.timePassed < timeToWait)
				{
					typingInfo.timePassed += deltaTime;
					this.<>2__current = null;
					this.<>1__state = 2;
					return true;
				}
				typingInfo.timePassed %= timeToWait;
				IL_191:
				int num2 = i;
				i = num2 + 1;
				IL_1A1:
				if (i >= typewriterCore.TextAnimator.CharactersCount)
				{
					UnityEvent onTextDisappeared = typewriterCore.onTextDisappeared;
					if (onTextDisappeared != null)
					{
						onTextDisappeared.Invoke();
					}
					typewriterCore.isHidingText = false;
					return false;
				}
				if (!typewriterCore.TextAnimator.Characters[i].isVisible)
				{
					goto IL_191;
				}
				typewriterCore.TextAnimator.SetVisibilityChar(i, false);
				timeToWait = typewriterCore.GetWaitDisappearanceTimeOf(i);
				deltaTime = typewriterCore.GetDeltaTime(typingInfo);
				if (timeToWait < 0f)
				{
					timeToWait = 0f;
				}
				if (timeToWait >= deltaTime)
				{
					goto IL_166;
				}
				typingInfo.timePassed += timeToWait;
				if (typingInfo.timePassed >= deltaTime)
				{
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				}
				goto IL_191;
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008379 File Offset: 0x00006579
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x00008381 File Offset: 0x00006581
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008388 File Offset: 0x00006588
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400014D RID: 333
			private int <>1__state;

			// Token: 0x0400014E RID: 334
			private object <>2__current;

			// Token: 0x0400014F RID: 335
			public TypewriterCore <>4__this;

			// Token: 0x04000150 RID: 336
			private TypingInfo <typingInfo>5__2;

			// Token: 0x04000151 RID: 337
			private int <i>5__3;

			// Token: 0x04000152 RID: 338
			private float <timeToWait>5__4;

			// Token: 0x04000153 RID: 339
			private float <deltaTime>5__5;
		}
	}
}
