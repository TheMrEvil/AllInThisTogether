using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000202 RID: 514
public class UISelector : MonoBehaviour
{
	// Token: 0x1700016F RID: 367
	// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0008A9A2 File Offset: 0x00088BA2
	public static UIActions Actions
	{
		get
		{
			return InputManager.UIAct;
		}
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x0008A9A9 File Offset: 0x00088BA9
	private void Awake()
	{
		UISelector.instance = this;
	}

	// Token: 0x060015F6 RID: 5622 RVA: 0x0008A9B4 File Offset: 0x00088BB4
	public static void SelectSelectable(Selectable selectable)
	{
		if (selectable == null || selectable.gameObject.activeInHierarchy)
		{
			UISelector.SelectSelectable(selectable, false);
			return;
		}
		int num = 0;
		Selectable selectable2 = selectable;
		while (num < 20 && selectable2 != null)
		{
			selectable2 = selectable2.FindSelectableOnDown();
			if (selectable2 != null && selectable2.gameObject.activeInHierarchy)
			{
				UISelector.SelectSelectable(selectable2);
				return;
			}
			num++;
		}
	}

	// Token: 0x060015F7 RID: 5623 RVA: 0x0008AA1C File Offset: 0x00088C1C
	public static void SelectSelectable(Selectable selectable, bool ignoreOnSelect)
	{
		Button button = selectable as Button;
		if (button != null && !button.interactable)
		{
			return;
		}
		EventSystem current = EventSystem.current;
		UISelector.instance.CurrentSelection = selectable;
		if (UISelector.instance.CurrentSelection)
		{
			UISelector.instance.CurrentSelection.Select();
			UISelector.instance.CurrentSelection.OnSelect(null);
		}
		if (selectable == null && current != null)
		{
			current.SetSelectedGameObject(null);
		}
	}

	// Token: 0x060015F8 RID: 5624 RVA: 0x0008AA98 File Offset: 0x00088C98
	public Selectable GetCurrentSelected()
	{
		if (this.CurrentSelection != null && this.CurrentSelection.isActiveAndEnabled)
		{
			return this.CurrentSelection;
		}
		Selectable selectable = null;
		if (EventSystem.current.currentSelectedGameObject)
		{
			selectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
		}
		UISelector.SelectSelectable(selectable);
		return this.CurrentSelection;
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x0008AAF6 File Offset: 0x00088CF6
	public static void ResetSelected()
	{
		UISelector.instance.CurrentSelection = null;
		EventSystem.current.SetSelectedGameObject(null);
	}

	// Token: 0x060015FA RID: 5626 RVA: 0x0008AB10 File Offset: 0x00088D10
	private void Update()
	{
		if (!PanelManager.inFocus)
		{
			return;
		}
		if (InputManager.IsBindingKeys)
		{
			return;
		}
		if (ConfirmationPrompt.IsInPrompt || ErrorPrompt.IsInPrompt)
		{
			return;
		}
		if (Time.realtimeSinceStartup - this.LockTimeStart < 0.3f)
		{
			return;
		}
		if (UISelector.IgnoreInput || UITutorial.InTutorial)
		{
			return;
		}
		if (PlayerControl.myInstance != null && !PanelManager.curSelect.gameplayInteractable)
		{
			return;
		}
		if (this.CurrentSelection == null && PanelManager.curSelect != null && PanelManager.curSelect.defaultSelect != null)
		{
			UISelector.SelectSelectable(PanelManager.curSelect.defaultSelect);
		}
		if (this.cooldownVal > 0f)
		{
			this.cooldownVal -= Time.unscaledDeltaTime;
		}
		this.ProcessNavigation();
		this.ProcessButtons();
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x0008ABE0 File Offset: 0x00088DE0
	private void ProcessButtons()
	{
		if (UISelector.Actions.UIPrimary.WasPressed && InputManager.IsUsingController)
		{
			Selectable currentSelected = this.GetCurrentSelected();
			if (currentSelected != null)
			{
				Button button = currentSelected as Button;
				if (button != null)
				{
					Button.ButtonClickedEvent onClick = button.onClick;
					if (onClick == null)
					{
						return;
					}
					onClick.Invoke();
					return;
				}
			}
		}
		if (PanelManager.curSelect == null || PanelManager.curSelect.DidJustEnter)
		{
			return;
		}
		if (!UISelector.Actions.UIBack.WasPressed)
		{
			if (UISelector.Actions.UISecondary.WasPressed)
			{
				bool flag = false;
				Selectable currentSelected2 = this.GetCurrentSelected();
				if (currentSelected2 != null)
				{
					UINavReceiver component = currentSelected2.GetComponent<UINavReceiver>();
					if (component != null)
					{
						component.SecondaryInput();
						flag = true;
					}
				}
				if (!flag)
				{
					UIPanel curSelect = PanelManager.curSelect;
					if (curSelect != null)
					{
						Action onSecondaryAction = curSelect.OnSecondaryAction;
						if (onSecondaryAction != null)
						{
							onSecondaryAction();
						}
					}
				}
			}
			else if (UISelector.Actions.UITertiary.WasPressed)
			{
				UIPanel curSelect2 = PanelManager.curSelect;
				if (curSelect2 != null)
				{
					Action onTertiaryAction = curSelect2.OnTertiaryAction;
					if (onTertiaryAction != null)
					{
						onTertiaryAction();
					}
				}
			}
			else if (UISelector.Actions.UIRightStickButton.WasPressed)
			{
				UIPanel curSelect3 = PanelManager.curSelect;
				if (curSelect3 != null)
				{
					Action onRightStickAction = curSelect3.OnRightStickAction;
					if (onRightStickAction != null)
					{
						onRightStickAction();
					}
				}
			}
		}
		if (UISelector.Actions.Tab_Next.WasPressed)
		{
			UIPanel curSelect4 = PanelManager.curSelect;
			if (curSelect4 != null)
			{
				Action onNextTab = curSelect4.OnNextTab;
				if (onNextTab != null)
				{
					onNextTab();
				}
			}
		}
		if (UISelector.Actions.Tab_Prev.WasPressed)
		{
			UIPanel curSelect5 = PanelManager.curSelect;
			if (curSelect5 != null)
			{
				Action onPrevTab = curSelect5.OnPrevTab;
				if (onPrevTab != null)
				{
					onPrevTab();
				}
			}
		}
		if (UISelector.Actions.Page_Next.WasPressed)
		{
			UIPanel curSelect6 = PanelManager.curSelect;
			if (curSelect6 != null)
			{
				Action onPageNext = curSelect6.OnPageNext;
				if (onPageNext != null)
				{
					onPageNext();
				}
			}
		}
		if (UISelector.Actions.Page_Prev.WasPressed)
		{
			UIPanel curSelect7 = PanelManager.curSelect;
			if (curSelect7 == null)
			{
				return;
			}
			Action onPagePrev = curSelect7.OnPagePrev;
			if (onPagePrev == null)
			{
				return;
			}
			onPagePrev();
		}
	}

	// Token: 0x060015FC RID: 5628 RVA: 0x0008ADC4 File Offset: 0x00088FC4
	private void ProcessNavigation()
	{
		bool wasPressed = UISelector.Actions.UILeft.WasPressed;
		bool wasPressed2 = UISelector.Actions.UIRight.WasPressed;
		bool wasPressed3 = UISelector.Actions.UIUp.WasPressed;
		bool wasPressed4 = UISelector.Actions.UIDown.WasPressed;
		Vector2 value = UISelector.Actions.AnalogMove.Value;
		bool left = value.x < -0.9f;
		bool right = value.x > 0.9f;
		bool up = value.y > 0.9f;
		bool down = value.y < -0.9f;
		if (!this.ProcessSelection(wasPressed, wasPressed2, wasPressed3, wasPressed4, true))
		{
			this.ProcessSelection(left, right, up, down, false);
		}
		Selectable currentSelected = this.GetCurrentSelected();
		if (currentSelected != null)
		{
			UINavReceiver component = currentSelected.GetComponent<UINavReceiver>();
			if (component != null && !component.ProcessMovement(wasPressed, wasPressed2, wasPressed3, wasPressed4, true))
			{
				component.ProcessMovement(left, right, up, down, false);
			}
		}
	}

	// Token: 0x060015FD RID: 5629 RVA: 0x0008AEBC File Offset: 0x000890BC
	private bool ProcessSelection(bool left, bool right, bool up, bool down, bool isDigital)
	{
		Selectable currentSelected = this.GetCurrentSelected();
		if (!currentSelected)
		{
			return false;
		}
		if (left)
		{
			this.ChangeSelection(UISelector.GetSelectableDirection(2, currentSelected), isDigital);
		}
		if (right)
		{
			this.ChangeSelection(UISelector.GetSelectableDirection(3, currentSelected), isDigital);
		}
		if (up)
		{
			this.ChangeSelection(UISelector.GetSelectableDirection(0, currentSelected), isDigital);
		}
		if (down)
		{
			this.ChangeSelection(UISelector.GetSelectableDirection(1, currentSelected), isDigital);
		}
		return left || right || up || down;
	}

	// Token: 0x060015FE RID: 5630 RVA: 0x0008AF2C File Offset: 0x0008912C
	public static Selectable GetSelectableDirection(int direction, Selectable sb)
	{
		Selectable selectable = sb.FindSelectableOnUp();
		if (direction == 1)
		{
			selectable = sb.FindSelectableOnDown();
		}
		else if (direction == 2)
		{
			selectable = sb.FindSelectableOnLeft();
		}
		else if (direction == 3)
		{
			selectable = sb.FindSelectableOnRight();
		}
		if (selectable == null || (selectable.gameObject.activeInHierarchy && selectable.interactable))
		{
			return selectable;
		}
		Selectable selectable2 = selectable;
		int num = 0;
		while (num < 5 && selectable != null)
		{
			if (direction == 0)
			{
				selectable = selectable.FindSelectableOnUp();
			}
			else if (direction == 1)
			{
				selectable = selectable.FindSelectableOnDown();
			}
			else if (direction == 2)
			{
				selectable = selectable.FindSelectableOnLeft();
			}
			else if (direction == 3)
			{
				selectable = selectable.FindSelectableOnRight();
			}
			if (selectable != null && selectable.gameObject.activeInHierarchy && selectable.interactable)
			{
				return selectable;
			}
			num++;
		}
		num = 0;
		direction++;
		if (direction > 2)
		{
			direction = 0;
		}
		selectable = selectable2;
		while (num < 20 && selectable != null)
		{
			if (direction == 0)
			{
				selectable = selectable.FindSelectableOnUp();
			}
			else if (direction == 1)
			{
				selectable = selectable.FindSelectableOnDown();
			}
			else if (direction == 2)
			{
				selectable = selectable.FindSelectableOnLeft();
			}
			else if (direction == 3)
			{
				selectable = selectable.FindSelectableOnRight();
			}
			if (selectable != null && selectable != sb && sb.gameObject.activeInHierarchy && sb.interactable)
			{
				return selectable;
			}
			num++;
			if (num % 5 == 0)
			{
				direction++;
				if (direction > 2)
				{
					direction = 0;
				}
				selectable = selectable2;
			}
		}
		return sb;
	}

	// Token: 0x060015FF RID: 5631 RVA: 0x0008B080 File Offset: 0x00089280
	private void ChangeSelection(Selectable selectable, bool isDigital = true)
	{
		if (selectable == null || !selectable.interactable || !selectable.gameObject.activeSelf || !selectable.gameObject.activeInHierarchy || this.cooldownVal > 0f || selectable == this.CurrentSelection)
		{
			return;
		}
		this.cooldownVal = (isDigital ? 0.15f : 0.3f);
		AudioManager.PlayInterfaceSFX(this.UINavClips.GetRandomClip(-1), 1f, 0f);
		UISelector.SelectSelectable(selectable);
	}

	// Token: 0x06001600 RID: 5632 RVA: 0x0008B10A File Offset: 0x0008930A
	public static void SetupVerticalListNav<T>(List<T> buttonList, Selectable aboveList = null, Selectable belowList = null, bool loop = false) where T : Component
	{
		UISelector.SetupVerticalListNav((from g in buttonList
		select g.gameObject).ToList<GameObject>(), aboveList, belowList, loop);
	}

	// Token: 0x06001601 RID: 5633 RVA: 0x0008B140 File Offset: 0x00089340
	public static void SetupVerticalListNav(List<GameObject> buttonList, Selectable aboveList = null, Selectable belowList = null, bool loop = false)
	{
		for (int i = buttonList.Count - 1; i >= 0; i--)
		{
			if (!(buttonList[i].GetComponent<Button>() != null))
			{
				buttonList.RemoveAt(i);
			}
		}
		for (int j = 0; j < buttonList.Count; j++)
		{
			Button component = buttonList[j].GetComponent<Button>();
			Button button = (j == 0) ? null : buttonList[j - 1].GetComponent<Button>();
			if (button != null)
			{
				Navigation navigation = component.navigation;
				navigation.selectOnUp = button;
				component.navigation = navigation;
				navigation = button.navigation;
				navigation.selectOnDown = component;
				button.navigation = navigation;
			}
		}
		if (loop && buttonList.Count > 1)
		{
			Button component2 = buttonList[0].GetComponent<Button>();
			Button component3 = buttonList[buttonList.Count - 1].GetComponent<Button>();
			Navigation navigation2 = component2.navigation;
			navigation2.selectOnUp = component3;
			component2.navigation = navigation2;
			navigation2 = component3.navigation;
			navigation2.selectOnDown = component2;
			component3.navigation = navigation2;
		}
		if (aboveList != null && buttonList.Count > 0)
		{
			Button component4 = buttonList[0].GetComponent<Button>();
			Navigation navigation3 = component4.navigation;
			navigation3.selectOnUp = aboveList;
			component4.navigation = navigation3;
			navigation3 = aboveList.navigation;
			navigation3.selectOnDown = component4;
			aboveList.navigation = navigation3;
		}
		if (belowList != null && buttonList.Count > 0)
		{
			Button component5 = buttonList[buttonList.Count - 1].GetComponent<Button>();
			Navigation navigation4 = component5.navigation;
			navigation4.selectOnDown = belowList;
			component5.navigation = navigation4;
			navigation4 = belowList.navigation;
			navigation4.selectOnUp = component5;
			belowList.navigation = navigation4;
		}
	}

	// Token: 0x06001602 RID: 5634 RVA: 0x0008B2F9 File Offset: 0x000894F9
	public static void SetupHorizontalListNav<T>(List<T> buttonList, Selectable beforeList = null, Selectable afterList = null, bool loop = false) where T : Component
	{
		UISelector.SetupHorizontalListNav((from g in buttonList
		select g.gameObject).ToList<GameObject>(), beforeList, afterList, loop);
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x0008B330 File Offset: 0x00089530
	public static void SetupHorizontalListNav(List<GameObject> buttonList, Selectable beforeList = null, Selectable afterList = null, bool loop = false)
	{
		for (int i = buttonList.Count - 1; i >= 0; i--)
		{
			if (!(buttonList[i].GetComponent<Button>() != null))
			{
				buttonList.RemoveAt(i);
			}
		}
		for (int j = 0; j < buttonList.Count; j++)
		{
			Button component = buttonList[j].GetComponent<Button>();
			Button button = (j == 0) ? null : buttonList[j - 1].GetComponent<Button>();
			if (button != null)
			{
				Navigation navigation = component.navigation;
				navigation.selectOnLeft = button;
				component.navigation = navigation;
				navigation = button.navigation;
				navigation.selectOnRight = component;
				button.navigation = navigation;
			}
		}
		if (loop && buttonList.Count > 1)
		{
			Button component2 = buttonList[0].GetComponent<Button>();
			Button component3 = buttonList[buttonList.Count - 1].GetComponent<Button>();
			Navigation navigation2 = component2.navigation;
			navigation2.selectOnLeft = component3;
			component2.navigation = navigation2;
			navigation2 = component3.navigation;
			navigation2.selectOnRight = component2;
			component3.navigation = navigation2;
		}
		if (beforeList != null && buttonList.Count > 0)
		{
			Button component4 = buttonList[0].GetComponent<Button>();
			Navigation navigation3 = component4.navigation;
			navigation3.selectOnLeft = beforeList;
			component4.navigation = navigation3;
			navigation3 = beforeList.navigation;
			navigation3.selectOnRight = component4;
			beforeList.navigation = navigation3;
		}
		if (afterList != null && buttonList.Count > 0)
		{
			Button component5 = buttonList[buttonList.Count - 1].GetComponent<Button>();
			Navigation navigation4 = component5.navigation;
			navigation4.selectOnRight = afterList;
			component5.navigation = navigation4;
			navigation4 = afterList.navigation;
			navigation4.selectOnLeft = component5;
			afterList.navigation = navigation4;
		}
	}

	// Token: 0x06001604 RID: 5636 RVA: 0x0008B4E9 File Offset: 0x000896E9
	public static void SetupGridListNav<T>(List<T> buttonList, int perRow, Selectable aboveList = null, Selectable belowList = null, bool loop = false) where T : Component
	{
		UISelector.SetupGridListNav((from g in buttonList
		select g.gameObject).ToList<GameObject>(), perRow, aboveList, belowList, loop);
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x0008B520 File Offset: 0x00089720
	public static void SetupGridListNav(List<GameObject> buttonList, int perRow, Selectable aboveList = null, Selectable belowList = null, bool loop = false)
	{
		int num = (int)Mathf.Ceil((float)buttonList.Count / (float)perRow);
		for (int i = 0; i < buttonList.Count; i++)
		{
			Button component = buttonList[i].GetComponent<Button>();
			Navigation navigation = component.navigation;
			int num2 = i / perRow;
			int num3 = i % perRow;
			if (num3 > 0)
			{
				navigation.selectOnLeft = buttonList[i - 1].GetComponent<Button>();
			}
			else if (loop && num2 > 0)
			{
				navigation.selectOnLeft = buttonList[Mathf.Max(0, i - 1)].GetComponent<Button>();
			}
			if (num3 < perRow - 1 && i < buttonList.Count - 1)
			{
				navigation.selectOnRight = buttonList[i + 1].GetComponent<Button>();
			}
			else if (loop && num2 < num - 1)
			{
				navigation.selectOnRight = buttonList[Mathf.Min(i + 1, buttonList.Count - 1)].GetComponent<Button>();
			}
			if (i - perRow >= 0)
			{
				navigation.selectOnUp = buttonList[i - perRow].GetComponent<Button>();
			}
			else if (aboveList != null)
			{
				navigation.selectOnUp = aboveList;
			}
			else if (loop && num > 1)
			{
				int num4 = (num - 1) * perRow + num3;
				if (num4 >= buttonList.Count)
				{
					num4 = buttonList.Count - 1;
				}
				navigation.selectOnUp = buttonList[num4].GetComponent<Button>();
			}
			if (i + perRow < buttonList.Count)
			{
				navigation.selectOnDown = buttonList[i + perRow].GetComponent<Button>();
			}
			else if (belowList != null && num2 == num - 1)
			{
				navigation.selectOnDown = belowList;
			}
			else if (loop && num > 1)
			{
				int num5 = (num2 + 1) * perRow + num3;
				if (num5 >= buttonList.Count)
				{
					num5 = num3;
				}
				navigation.selectOnDown = buttonList[Mathf.Min(num5, buttonList.Count - 1)].GetComponent<Button>();
			}
			component.navigation = navigation;
		}
	}

	// Token: 0x06001606 RID: 5638 RVA: 0x0008B6F4 File Offset: 0x000898F4
	public static void SetupCrossGridNav<T1, T2>(List<T1> topGrid, int topRowCount, List<T2> bottomGrid, int bottomRowCount) where T1 : Component where T2 : Component
	{
		UISelector.SetupCrossGridNav((from g in topGrid
		select g.gameObject).ToList<GameObject>(), topRowCount, (from g in bottomGrid
		select g.gameObject).ToList<GameObject>(), bottomRowCount);
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x0008B75C File Offset: 0x0008995C
	public static void SetupCrossGridNav(List<GameObject> topGrid, int topGridRowCount, List<GameObject> bottomGrid, int bottomGridRowCount)
	{
		if (topGrid.Count <= 0 || bottomGrid.Count <= 0)
		{
			return;
		}
		for (int i = ((topGrid.Count + topGridRowCount - 1) / topGridRowCount - 1) * topGridRowCount; i < topGrid.Count; i++)
		{
			topGrid[i].GetComponent<Button>().SetNavigation(bottomGrid[0].GetComponent<Button>(), UIDirection.Down, false);
		}
		for (int j = 0; j < Mathf.Min(bottomGridRowCount, bottomGrid.Count); j++)
		{
			Selectable component = bottomGrid[j].GetComponent<Button>();
			int index = topGrid.Count - 1;
			component.SetNavigation(topGrid[index].GetComponent<Button>(), UIDirection.Up, false);
		}
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x0008B7FA File Offset: 0x000899FA
	public static void SelectFirstInList<T>(List<T> buttonList) where T : Component
	{
		UISelector.SelectFirstInList((from g in buttonList
		select g.gameObject).ToList<GameObject>());
	}

	// Token: 0x06001609 RID: 5641 RVA: 0x0008B82C File Offset: 0x00089A2C
	public static void SelectFirstInList(List<GameObject> buttonList)
	{
		if (buttonList.Count > 0)
		{
			for (int i = 0; i < buttonList.Count; i++)
			{
				Selectable component = buttonList[i].GetComponent<Selectable>();
				if (!(component == null))
				{
					UISelector.SelectSelectable(component);
					return;
				}
			}
		}
	}

	// Token: 0x0600160A RID: 5642 RVA: 0x0008B870 File Offset: 0x00089A70
	public UISelector()
	{
	}

	// Token: 0x040015AB RID: 5547
	public static UISelector instance;

	// Token: 0x040015AC RID: 5548
	public static bool IgnoreInput;

	// Token: 0x040015AD RID: 5549
	public const float LOCKOUT_DURATION = 0.3f;

	// Token: 0x040015AE RID: 5550
	private const float ACTION_COOLDOWN = 0.15f;

	// Token: 0x040015AF RID: 5551
	private float LockTimeStart = -1f;

	// Token: 0x040015B0 RID: 5552
	public List<AudioClip> UINavClips;

	// Token: 0x040015B1 RID: 5553
	public Selectable CurrentSelection;

	// Token: 0x040015B2 RID: 5554
	private float cooldownVal;

	// Token: 0x020005EC RID: 1516
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c__21<T> where T : Component
	{
		// Token: 0x06002687 RID: 9863 RVA: 0x000D3B32 File Offset: 0x000D1D32
		// Note: this type is marked as 'beforefieldinit'.
		static <>c__21()
		{
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000D3B3E File Offset: 0x000D1D3E
		public <>c__21()
		{
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000D3B46 File Offset: 0x000D1D46
		internal GameObject <SetupVerticalListNav>b__21_0(T g)
		{
			return g.gameObject;
		}

		// Token: 0x04002938 RID: 10552
		public static readonly UISelector.<>c__21<T> <>9 = new UISelector.<>c__21<T>();

		// Token: 0x04002939 RID: 10553
		public static Func<T, GameObject> <>9__21_0;
	}

	// Token: 0x020005ED RID: 1517
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c__23<T> where T : Component
	{
		// Token: 0x0600268A RID: 9866 RVA: 0x000D3B53 File Offset: 0x000D1D53
		// Note: this type is marked as 'beforefieldinit'.
		static <>c__23()
		{
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000D3B5F File Offset: 0x000D1D5F
		public <>c__23()
		{
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000D3B67 File Offset: 0x000D1D67
		internal GameObject <SetupHorizontalListNav>b__23_0(T g)
		{
			return g.gameObject;
		}

		// Token: 0x0400293A RID: 10554
		public static readonly UISelector.<>c__23<T> <>9 = new UISelector.<>c__23<T>();

		// Token: 0x0400293B RID: 10555
		public static Func<T, GameObject> <>9__23_0;
	}

	// Token: 0x020005EE RID: 1518
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c__25<T> where T : Component
	{
		// Token: 0x0600268D RID: 9869 RVA: 0x000D3B74 File Offset: 0x000D1D74
		// Note: this type is marked as 'beforefieldinit'.
		static <>c__25()
		{
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000D3B80 File Offset: 0x000D1D80
		public <>c__25()
		{
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000D3B88 File Offset: 0x000D1D88
		internal GameObject <SetupGridListNav>b__25_0(T g)
		{
			return g.gameObject;
		}

		// Token: 0x0400293C RID: 10556
		public static readonly UISelector.<>c__25<T> <>9 = new UISelector.<>c__25<T>();

		// Token: 0x0400293D RID: 10557
		public static Func<T, GameObject> <>9__25_0;
	}

	// Token: 0x020005EF RID: 1519
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c__27<T1, T2> where T1 : Component where T2 : Component
	{
		// Token: 0x06002690 RID: 9872 RVA: 0x000D3B95 File Offset: 0x000D1D95
		// Note: this type is marked as 'beforefieldinit'.
		static <>c__27()
		{
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000D3BA1 File Offset: 0x000D1DA1
		public <>c__27()
		{
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000D3BA9 File Offset: 0x000D1DA9
		internal GameObject <SetupCrossGridNav>b__27_0(T1 g)
		{
			return g.gameObject;
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000D3BB6 File Offset: 0x000D1DB6
		internal GameObject <SetupCrossGridNav>b__27_1(T2 g)
		{
			return g.gameObject;
		}

		// Token: 0x0400293E RID: 10558
		public static readonly UISelector.<>c__27<T1, T2> <>9 = new UISelector.<>c__27<T1, T2>();

		// Token: 0x0400293F RID: 10559
		public static Func<T1, GameObject> <>9__27_0;

		// Token: 0x04002940 RID: 10560
		public static Func<T2, GameObject> <>9__27_1;
	}

	// Token: 0x020005F0 RID: 1520
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c__29<T> where T : Component
	{
		// Token: 0x06002694 RID: 9876 RVA: 0x000D3BC3 File Offset: 0x000D1DC3
		// Note: this type is marked as 'beforefieldinit'.
		static <>c__29()
		{
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000D3BCF File Offset: 0x000D1DCF
		public <>c__29()
		{
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000D3BD7 File Offset: 0x000D1DD7
		internal GameObject <SelectFirstInList>b__29_0(T g)
		{
			return g.gameObject;
		}

		// Token: 0x04002941 RID: 10561
		public static readonly UISelector.<>c__29<T> <>9 = new UISelector.<>c__29<T>();

		// Token: 0x04002942 RID: 10562
		public static Func<T, GameObject> <>9__29_0;
	}
}
