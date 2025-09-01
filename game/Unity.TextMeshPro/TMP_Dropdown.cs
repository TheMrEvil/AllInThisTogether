using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200002C RID: 44
	[AddComponentMenu("UI/Dropdown - TextMeshPro", 35)]
	[RequireComponent(typeof(RectTransform))]
	public class TMP_Dropdown : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0001861D File Offset: 0x0001681D
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00018625 File Offset: 0x00016825
		public RectTransform template
		{
			get
			{
				return this.m_Template;
			}
			set
			{
				this.m_Template = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00018634 File Offset: 0x00016834
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0001863C File Offset: 0x0001683C
		public TMP_Text captionText
		{
			get
			{
				return this.m_CaptionText;
			}
			set
			{
				this.m_CaptionText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0001864B File Offset: 0x0001684B
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00018653 File Offset: 0x00016853
		public Image captionImage
		{
			get
			{
				return this.m_CaptionImage;
			}
			set
			{
				this.m_CaptionImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00018662 File Offset: 0x00016862
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0001866A File Offset: 0x0001686A
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				this.m_Placeholder = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00018679 File Offset: 0x00016879
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00018681 File Offset: 0x00016881
		public TMP_Text itemText
		{
			get
			{
				return this.m_ItemText;
			}
			set
			{
				this.m_ItemText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00018690 File Offset: 0x00016890
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00018698 File Offset: 0x00016898
		public Image itemImage
		{
			get
			{
				return this.m_ItemImage;
			}
			set
			{
				this.m_ItemImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000186A7 File Offset: 0x000168A7
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000186B4 File Offset: 0x000168B4
		public List<TMP_Dropdown.OptionData> options
		{
			get
			{
				return this.m_Options.options;
			}
			set
			{
				this.m_Options.options = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000186C8 File Offset: 0x000168C8
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000186D0 File Offset: 0x000168D0
		public TMP_Dropdown.DropdownEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000186D9 File Offset: 0x000168D9
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000186E1 File Offset: 0x000168E1
		public float alphaFadeSpeed
		{
			get
			{
				return this.m_AlphaFadeSpeed;
			}
			set
			{
				this.m_AlphaFadeSpeed = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000186EA File Offset: 0x000168EA
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000186F2 File Offset: 0x000168F2
		public int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.SetValue(value, true);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000186FC File Offset: 0x000168FC
		public void SetValueWithoutNotify(int input)
		{
			this.SetValue(input, false);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00018708 File Offset: 0x00016908
		private void SetValue(int value, bool sendCallback = true)
		{
			if (Application.isPlaying && (value == this.m_Value || this.options.Count == 0))
			{
				return;
			}
			this.m_Value = Mathf.Clamp(value, this.m_Placeholder ? -1 : 0, this.options.Count - 1);
			this.RefreshShownValue();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Dropdown.value", this);
				this.m_OnValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00018782 File Offset: 0x00016982
		public bool IsExpanded
		{
			get
			{
				return this.m_Dropdown != null;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00018790 File Offset: 0x00016990
		protected TMP_Dropdown()
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000187C4 File Offset: 0x000169C4
		protected override void Awake()
		{
			if (this.m_CaptionImage)
			{
				this.m_CaptionImage.enabled = (this.m_CaptionImage.sprite != null);
			}
			if (this.m_Template)
			{
				this.m_Template.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00018818 File Offset: 0x00016A18
		protected override void Start()
		{
			this.m_AlphaTweenRunner = new TweenRunner<FloatTween>();
			this.m_AlphaTweenRunner.Init(this);
			base.Start();
			this.RefreshShownValue();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0001883D File Offset: 0x00016A3D
		protected override void OnDisable()
		{
			this.ImmediateDestroyDropdownList();
			if (this.m_Blocker != null)
			{
				this.DestroyBlocker(this.m_Blocker);
			}
			this.m_Blocker = null;
			base.OnDisable();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0001886C File Offset: 0x00016A6C
		public void RefreshShownValue()
		{
			TMP_Dropdown.OptionData optionData = TMP_Dropdown.s_NoOptionData;
			if (this.options.Count > 0 && this.m_Value >= 0)
			{
				optionData = this.options[Mathf.Clamp(this.m_Value, 0, this.options.Count - 1)];
			}
			if (this.m_CaptionText)
			{
				if (optionData != null && optionData.text != null)
				{
					this.m_CaptionText.text = optionData.text;
				}
				else
				{
					this.m_CaptionText.text = "";
				}
			}
			if (this.m_CaptionImage)
			{
				if (optionData != null)
				{
					this.m_CaptionImage.sprite = optionData.image;
				}
				else
				{
					this.m_CaptionImage.sprite = null;
				}
				this.m_CaptionImage.enabled = (this.m_CaptionImage.sprite != null);
			}
			if (this.m_Placeholder)
			{
				this.m_Placeholder.enabled = (this.options.Count == 0 || this.m_Value == -1);
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00018972 File Offset: 0x00016B72
		public void AddOptions(List<TMP_Dropdown.OptionData> options)
		{
			this.options.AddRange(options);
			this.RefreshShownValue();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00018988 File Offset: 0x00016B88
		public void AddOptions(List<string> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new TMP_Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000189C4 File Offset: 0x00016BC4
		public void AddOptions(List<Sprite> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new TMP_Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000189FF File Offset: 0x00016BFF
		public void ClearOptions()
		{
			this.options.Clear();
			this.m_Value = (this.m_Placeholder ? -1 : 0);
			this.RefreshShownValue();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00018A2C File Offset: 0x00016C2C
		private void SetupTemplate()
		{
			this.validTemplate = false;
			if (!this.m_Template)
			{
				UnityEngine.Debug.LogError("The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Toggle component serving as the item.", this);
				return;
			}
			GameObject gameObject = this.m_Template.gameObject;
			gameObject.SetActive(true);
			Toggle componentInChildren = this.m_Template.GetComponentInChildren<Toggle>();
			this.validTemplate = true;
			if (!componentInChildren || componentInChildren.transform == this.template)
			{
				this.validTemplate = false;
				UnityEngine.Debug.LogError("The dropdown template is not valid. The template must have a child GameObject with a Toggle component serving as the item.", this.template);
			}
			else if (!(componentInChildren.transform.parent is RectTransform))
			{
				this.validTemplate = false;
				UnityEngine.Debug.LogError("The dropdown template is not valid. The child GameObject with a Toggle component (the item) must have a RectTransform on its parent.", this.template);
			}
			else if (this.itemText != null && !this.itemText.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				UnityEngine.Debug.LogError("The dropdown template is not valid. The Item Text must be on the item GameObject or children of it.", this.template);
			}
			else if (this.itemImage != null && !this.itemImage.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				UnityEngine.Debug.LogError("The dropdown template is not valid. The Item Image must be on the item GameObject or children of it.", this.template);
			}
			if (!this.validTemplate)
			{
				gameObject.SetActive(false);
				return;
			}
			TMP_Dropdown.DropdownItem dropdownItem = componentInChildren.gameObject.AddComponent<TMP_Dropdown.DropdownItem>();
			dropdownItem.text = this.m_ItemText;
			dropdownItem.image = this.m_ItemImage;
			dropdownItem.toggle = componentInChildren;
			dropdownItem.rectTransform = (RectTransform)componentInChildren.transform;
			Canvas canvas = null;
			Transform parent = this.m_Template.parent;
			while (parent != null)
			{
				canvas = parent.GetComponent<Canvas>();
				if (canvas != null)
				{
					break;
				}
				parent = parent.parent;
			}
			Canvas orAddComponent = TMP_Dropdown.GetOrAddComponent<Canvas>(gameObject);
			orAddComponent.overrideSorting = true;
			orAddComponent.sortingOrder = 30000;
			if (canvas != null)
			{
				Component[] components = canvas.GetComponents<BaseRaycaster>();
				Component[] array = components;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i].GetType();
					if (gameObject.GetComponent(type) == null)
					{
						gameObject.AddComponent(type);
					}
				}
			}
			else
			{
				TMP_Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			TMP_Dropdown.GetOrAddComponent<CanvasGroup>(gameObject);
			gameObject.SetActive(false);
			this.validTemplate = true;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00018C5C File Offset: 0x00016E5C
		private static T GetOrAddComponent<T>(GameObject go) where T : Component
		{
			T t = go.GetComponent<T>();
			if (!t)
			{
				t = go.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00018C85 File Offset: 0x00016E85
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Show();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00018C8D File Offset: 0x00016E8D
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Show();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00018C95 File Offset: 0x00016E95
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Hide();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00018CA0 File Offset: 0x00016EA0
		public void Show()
		{
			if (this.m_Coroutine != null)
			{
				base.StopCoroutine(this.m_Coroutine);
				this.ImmediateDestroyDropdownList();
			}
			if (!this.IsActive() || !this.IsInteractable() || this.m_Dropdown != null)
			{
				return;
			}
			List<Canvas> list = TMP_ListPool<Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count == 0)
			{
				return;
			}
			Canvas canvas = list[list.Count - 1];
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].isRootCanvas)
				{
					canvas = list[i];
					break;
				}
			}
			TMP_ListPool<Canvas>.Release(list);
			if (!this.validTemplate)
			{
				this.SetupTemplate();
				if (!this.validTemplate)
				{
					return;
				}
			}
			this.m_Template.gameObject.SetActive(true);
			this.m_Template.GetComponent<Canvas>().sortingLayerID = canvas.sortingLayerID;
			this.m_Dropdown = this.CreateDropdownList(this.m_Template.gameObject);
			this.m_Dropdown.name = "Dropdown List";
			this.m_Dropdown.SetActive(true);
			RectTransform rectTransform = this.m_Dropdown.transform as RectTransform;
			rectTransform.SetParent(this.m_Template.transform.parent, false);
			TMP_Dropdown.DropdownItem componentInChildren = this.m_Dropdown.GetComponentInChildren<TMP_Dropdown.DropdownItem>();
			RectTransform rectTransform2 = componentInChildren.rectTransform.parent.gameObject.transform as RectTransform;
			componentInChildren.rectTransform.gameObject.SetActive(true);
			Rect rect = rectTransform2.rect;
			Rect rect2 = componentInChildren.rectTransform.rect;
			Vector2 vector = rect2.min - rect.min + componentInChildren.rectTransform.localPosition;
			Vector2 vector2 = rect2.max - rect.max + componentInChildren.rectTransform.localPosition;
			Vector2 size = rect2.size;
			this.m_Items.Clear();
			Toggle toggle = null;
			for (int j = 0; j < this.options.Count; j++)
			{
				TMP_Dropdown.OptionData data = this.options[j];
				TMP_Dropdown.DropdownItem item = this.AddItem(data, this.value == j, componentInChildren, this.m_Items);
				if (!(item == null))
				{
					item.toggle.isOn = (this.value == j);
					item.toggle.onValueChanged.AddListener(delegate(bool x)
					{
						this.OnSelectItem(item.toggle);
					});
					if (item.toggle.isOn)
					{
						item.toggle.Select();
					}
					if (toggle != null)
					{
						Navigation navigation = toggle.navigation;
						Navigation navigation2 = item.toggle.navigation;
						navigation.mode = Navigation.Mode.Explicit;
						navigation2.mode = Navigation.Mode.Explicit;
						navigation.selectOnDown = item.toggle;
						navigation.selectOnRight = item.toggle;
						navigation2.selectOnLeft = toggle;
						navigation2.selectOnUp = toggle;
						toggle.navigation = navigation;
						item.toggle.navigation = navigation2;
					}
					toggle = item.toggle;
				}
			}
			Vector2 sizeDelta = rectTransform2.sizeDelta;
			sizeDelta.y = size.y * (float)this.m_Items.Count + vector.y - vector2.y;
			rectTransform2.sizeDelta = sizeDelta;
			float num = rectTransform.rect.height - rectTransform2.rect.height;
			if (num > 0f)
			{
				rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - num);
			}
			Vector3[] array = new Vector3[4];
			rectTransform.GetWorldCorners(array);
			RectTransform rectTransform3 = canvas.transform as RectTransform;
			Rect rect3 = rectTransform3.rect;
			for (int k = 0; k < 2; k++)
			{
				bool flag = false;
				for (int l = 0; l < 4; l++)
				{
					Vector3 vector3 = rectTransform3.InverseTransformPoint(array[l]);
					if ((vector3[k] < rect3.min[k] && !Mathf.Approximately(vector3[k], rect3.min[k])) || (vector3[k] > rect3.max[k] && !Mathf.Approximately(vector3[k], rect3.max[k])))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					RectTransformUtility.FlipLayoutOnAxis(rectTransform, k, false, false);
				}
			}
			for (int m = 0; m < this.m_Items.Count; m++)
			{
				RectTransform rectTransform4 = this.m_Items[m].rectTransform;
				rectTransform4.anchorMin = new Vector2(rectTransform4.anchorMin.x, 0f);
				rectTransform4.anchorMax = new Vector2(rectTransform4.anchorMax.x, 0f);
				rectTransform4.anchoredPosition = new Vector2(rectTransform4.anchoredPosition.x, vector.y + size.y * (float)(this.m_Items.Count - 1 - m) + size.y * rectTransform4.pivot.y);
				rectTransform4.sizeDelta = new Vector2(rectTransform4.sizeDelta.x, size.y);
			}
			this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f, 1f);
			this.m_Template.gameObject.SetActive(false);
			componentInChildren.gameObject.SetActive(false);
			this.m_Blocker = this.CreateBlocker(canvas);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000192A8 File Offset: 0x000174A8
		protected virtual GameObject CreateBlocker(Canvas rootCanvas)
		{
			GameObject gameObject = new GameObject("Blocker");
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(rootCanvas.transform, false);
			rectTransform.anchorMin = Vector3.zero;
			rectTransform.anchorMax = Vector3.one;
			rectTransform.sizeDelta = Vector2.zero;
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.overrideSorting = true;
			Canvas component = this.m_Dropdown.GetComponent<Canvas>();
			canvas.sortingLayerID = component.sortingLayerID;
			canvas.sortingOrder = component.sortingOrder - 1;
			Canvas canvas2 = null;
			Transform parent = this.m_Template.parent;
			while (parent != null)
			{
				canvas2 = parent.GetComponent<Canvas>();
				if (canvas2 != null)
				{
					break;
				}
				parent = parent.parent;
			}
			if (canvas2 != null)
			{
				Component[] components = canvas2.GetComponents<BaseRaycaster>();
				Component[] array = components;
				for (int i = 0; i < array.Length; i++)
				{
					Type type = array[i].GetType();
					if (gameObject.GetComponent(type) == null)
					{
						gameObject.AddComponent(type);
					}
				}
			}
			else
			{
				TMP_Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			gameObject.AddComponent<Image>().color = Color.clear;
			gameObject.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Hide));
			return gameObject;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000193E2 File Offset: 0x000175E2
		protected virtual void DestroyBlocker(GameObject blocker)
		{
			UnityEngine.Object.Destroy(blocker);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000193EA File Offset: 0x000175EA
		protected virtual GameObject CreateDropdownList(GameObject template)
		{
			return UnityEngine.Object.Instantiate<GameObject>(template);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000193F2 File Offset: 0x000175F2
		protected virtual void DestroyDropdownList(GameObject dropdownList)
		{
			UnityEngine.Object.Destroy(dropdownList);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000193FA File Offset: 0x000175FA
		protected virtual TMP_Dropdown.DropdownItem CreateItem(TMP_Dropdown.DropdownItem itemTemplate)
		{
			return UnityEngine.Object.Instantiate<TMP_Dropdown.DropdownItem>(itemTemplate);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00019402 File Offset: 0x00017602
		protected virtual void DestroyItem(TMP_Dropdown.DropdownItem item)
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00019404 File Offset: 0x00017604
		private TMP_Dropdown.DropdownItem AddItem(TMP_Dropdown.OptionData data, bool selected, TMP_Dropdown.DropdownItem itemTemplate, List<TMP_Dropdown.DropdownItem> items)
		{
			TMP_Dropdown.DropdownItem dropdownItem = this.CreateItem(itemTemplate);
			dropdownItem.rectTransform.SetParent(itemTemplate.rectTransform.parent, false);
			dropdownItem.gameObject.SetActive(true);
			dropdownItem.gameObject.name = "Item " + items.Count.ToString() + ((data.text != null) ? (": " + data.text) : "");
			if (dropdownItem.toggle != null)
			{
				dropdownItem.toggle.isOn = false;
			}
			if (dropdownItem.text)
			{
				dropdownItem.text.text = data.text;
			}
			if (dropdownItem.image)
			{
				dropdownItem.image.sprite = data.image;
				dropdownItem.image.enabled = (dropdownItem.image.sprite != null);
			}
			items.Add(dropdownItem);
			return dropdownItem;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000194FC File Offset: 0x000176FC
		private void AlphaFadeList(float duration, float alpha)
		{
			CanvasGroup component = this.m_Dropdown.GetComponent<CanvasGroup>();
			this.AlphaFadeList(duration, component.alpha, alpha);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00019524 File Offset: 0x00017724
		private void AlphaFadeList(float duration, float start, float end)
		{
			if (end.Equals(start))
			{
				return;
			}
			FloatTween info = new FloatTween
			{
				duration = duration,
				startValue = start,
				targetValue = end
			};
			info.AddOnChangedCallback(new UnityAction<float>(this.SetAlpha));
			info.ignoreTimeScale = true;
			this.m_AlphaTweenRunner.StartTween(info);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00019585 File Offset: 0x00017785
		private void SetAlpha(float alpha)
		{
			if (!this.m_Dropdown)
			{
				return;
			}
			this.m_Dropdown.GetComponent<CanvasGroup>().alpha = alpha;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000195A8 File Offset: 0x000177A8
		public void Hide()
		{
			if (this.m_Coroutine == null)
			{
				if (this.m_Dropdown != null)
				{
					this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f);
					if (this.IsActive())
					{
						this.m_Coroutine = base.StartCoroutine(this.DelayedDestroyDropdownList(this.m_AlphaFadeSpeed));
					}
				}
				if (this.m_Blocker != null)
				{
					this.DestroyBlocker(this.m_Blocker);
				}
				this.m_Blocker = null;
				this.Select();
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00019623 File Offset: 0x00017823
		private IEnumerator DelayedDestroyDropdownList(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			this.ImmediateDestroyDropdownList();
			yield break;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0001963C File Offset: 0x0001783C
		private void ImmediateDestroyDropdownList()
		{
			for (int i = 0; i < this.m_Items.Count; i++)
			{
				if (this.m_Items[i] != null)
				{
					this.DestroyItem(this.m_Items[i]);
				}
			}
			this.m_Items.Clear();
			if (this.m_Dropdown != null)
			{
				this.DestroyDropdownList(this.m_Dropdown);
			}
			if (this.m_AlphaTweenRunner != null)
			{
				this.m_AlphaTweenRunner.StopTween();
			}
			this.m_Dropdown = null;
			this.m_Coroutine = null;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000196CC File Offset: 0x000178CC
		private void OnSelectItem(Toggle toggle)
		{
			if (!toggle.isOn)
			{
				toggle.isOn = true;
			}
			int num = -1;
			Transform transform = toggle.transform;
			Transform parent = transform.parent;
			for (int i = 0; i < parent.childCount; i++)
			{
				if (parent.GetChild(i) == transform)
				{
					num = i - 1;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			this.value = num;
			this.Hide();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00019730 File Offset: 0x00017930
		// Note: this type is marked as 'beforefieldinit'.
		static TMP_Dropdown()
		{
		}

		// Token: 0x04000162 RID: 354
		[SerializeField]
		private RectTransform m_Template;

		// Token: 0x04000163 RID: 355
		[SerializeField]
		private TMP_Text m_CaptionText;

		// Token: 0x04000164 RID: 356
		[SerializeField]
		private Image m_CaptionImage;

		// Token: 0x04000165 RID: 357
		[SerializeField]
		private Graphic m_Placeholder;

		// Token: 0x04000166 RID: 358
		[Space]
		[SerializeField]
		private TMP_Text m_ItemText;

		// Token: 0x04000167 RID: 359
		[SerializeField]
		private Image m_ItemImage;

		// Token: 0x04000168 RID: 360
		[Space]
		[SerializeField]
		private int m_Value;

		// Token: 0x04000169 RID: 361
		[Space]
		[SerializeField]
		private TMP_Dropdown.OptionDataList m_Options = new TMP_Dropdown.OptionDataList();

		// Token: 0x0400016A RID: 362
		[Space]
		[SerializeField]
		private TMP_Dropdown.DropdownEvent m_OnValueChanged = new TMP_Dropdown.DropdownEvent();

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private float m_AlphaFadeSpeed = 0.15f;

		// Token: 0x0400016C RID: 364
		private GameObject m_Dropdown;

		// Token: 0x0400016D RID: 365
		private GameObject m_Blocker;

		// Token: 0x0400016E RID: 366
		private List<TMP_Dropdown.DropdownItem> m_Items = new List<TMP_Dropdown.DropdownItem>();

		// Token: 0x0400016F RID: 367
		private TweenRunner<FloatTween> m_AlphaTweenRunner;

		// Token: 0x04000170 RID: 368
		private bool validTemplate;

		// Token: 0x04000171 RID: 369
		private Coroutine m_Coroutine;

		// Token: 0x04000172 RID: 370
		private static TMP_Dropdown.OptionData s_NoOptionData = new TMP_Dropdown.OptionData();

		// Token: 0x0200007E RID: 126
		protected internal class DropdownItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ICancelHandler
		{
			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00038522 File Offset: 0x00036722
			// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0003852A File Offset: 0x0003672A
			public TMP_Text text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00038533 File Offset: 0x00036733
			// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0003853B File Offset: 0x0003673B
			public Image image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00038544 File Offset: 0x00036744
			// (set) Token: 0x060005EA RID: 1514 RVA: 0x0003854C File Offset: 0x0003674C
			public RectTransform rectTransform
			{
				get
				{
					return this.m_RectTransform;
				}
				set
				{
					this.m_RectTransform = value;
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x060005EB RID: 1515 RVA: 0x00038555 File Offset: 0x00036755
			// (set) Token: 0x060005EC RID: 1516 RVA: 0x0003855D File Offset: 0x0003675D
			public Toggle toggle
			{
				get
				{
					return this.m_Toggle;
				}
				set
				{
					this.m_Toggle = value;
				}
			}

			// Token: 0x060005ED RID: 1517 RVA: 0x00038566 File Offset: 0x00036766
			public virtual void OnPointerEnter(PointerEventData eventData)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x00038578 File Offset: 0x00036778
			public virtual void OnCancel(BaseEventData eventData)
			{
				TMP_Dropdown componentInParent = base.GetComponentInParent<TMP_Dropdown>();
				if (componentInParent)
				{
					componentInParent.Hide();
				}
			}

			// Token: 0x060005EF RID: 1519 RVA: 0x0003859A File Offset: 0x0003679A
			public DropdownItem()
			{
			}

			// Token: 0x04000592 RID: 1426
			[SerializeField]
			private TMP_Text m_Text;

			// Token: 0x04000593 RID: 1427
			[SerializeField]
			private Image m_Image;

			// Token: 0x04000594 RID: 1428
			[SerializeField]
			private RectTransform m_RectTransform;

			// Token: 0x04000595 RID: 1429
			[SerializeField]
			private Toggle m_Toggle;
		}

		// Token: 0x0200007F RID: 127
		[Serializable]
		public class OptionData
		{
			// Token: 0x17000165 RID: 357
			// (get) Token: 0x060005F0 RID: 1520 RVA: 0x000385A2 File Offset: 0x000367A2
			// (set) Token: 0x060005F1 RID: 1521 RVA: 0x000385AA File Offset: 0x000367AA
			public string text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000385B3 File Offset: 0x000367B3
			// (set) Token: 0x060005F3 RID: 1523 RVA: 0x000385BB File Offset: 0x000367BB
			public Sprite image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x000385C4 File Offset: 0x000367C4
			public OptionData()
			{
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x000385CC File Offset: 0x000367CC
			public OptionData(string text)
			{
				this.text = text;
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x000385DB File Offset: 0x000367DB
			public OptionData(Sprite image)
			{
				this.image = image;
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x000385EA File Offset: 0x000367EA
			public OptionData(string text, Sprite image)
			{
				this.text = text;
				this.image = image;
			}

			// Token: 0x04000596 RID: 1430
			[SerializeField]
			private string m_Text;

			// Token: 0x04000597 RID: 1431
			[SerializeField]
			private Sprite m_Image;
		}

		// Token: 0x02000080 RID: 128
		[Serializable]
		public class OptionDataList
		{
			// Token: 0x17000167 RID: 359
			// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00038600 File Offset: 0x00036800
			// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00038608 File Offset: 0x00036808
			public List<TMP_Dropdown.OptionData> options
			{
				get
				{
					return this.m_Options;
				}
				set
				{
					this.m_Options = value;
				}
			}

			// Token: 0x060005FA RID: 1530 RVA: 0x00038611 File Offset: 0x00036811
			public OptionDataList()
			{
				this.options = new List<TMP_Dropdown.OptionData>();
			}

			// Token: 0x04000598 RID: 1432
			[SerializeField]
			private List<TMP_Dropdown.OptionData> m_Options;
		}

		// Token: 0x02000081 RID: 129
		[Serializable]
		public class DropdownEvent : UnityEvent<int>
		{
			// Token: 0x060005FB RID: 1531 RVA: 0x00038624 File Offset: 0x00036824
			public DropdownEvent()
			{
			}
		}

		// Token: 0x02000082 RID: 130
		[CompilerGenerated]
		private sealed class <>c__DisplayClass69_0
		{
			// Token: 0x060005FC RID: 1532 RVA: 0x0003862C File Offset: 0x0003682C
			public <>c__DisplayClass69_0()
			{
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x00038634 File Offset: 0x00036834
			internal void <Show>b__0(bool x)
			{
				this.<>4__this.OnSelectItem(this.item.toggle);
			}

			// Token: 0x04000599 RID: 1433
			public TMP_Dropdown.DropdownItem item;

			// Token: 0x0400059A RID: 1434
			public TMP_Dropdown <>4__this;
		}

		// Token: 0x02000083 RID: 131
		[CompilerGenerated]
		private sealed class <DelayedDestroyDropdownList>d__81 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060005FE RID: 1534 RVA: 0x0003864C File Offset: 0x0003684C
			[DebuggerHidden]
			public <DelayedDestroyDropdownList>d__81(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x0003865B File Offset: 0x0003685B
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x00038660 File Offset: 0x00036860
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TMP_Dropdown tmp_Dropdown = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<>2__current = new WaitForSecondsRealtime(delay);
					this.<>1__state = 1;
					return true;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				tmp_Dropdown.ImmediateDestroyDropdownList();
				return false;
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000601 RID: 1537 RVA: 0x000386B3 File Offset: 0x000368B3
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000602 RID: 1538 RVA: 0x000386BB File Offset: 0x000368BB
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000603 RID: 1539 RVA: 0x000386C2 File Offset: 0x000368C2
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400059B RID: 1435
			private int <>1__state;

			// Token: 0x0400059C RID: 1436
			private object <>2__current;

			// Token: 0x0400059D RID: 1437
			public float delay;

			// Token: 0x0400059E RID: 1438
			public TMP_Dropdown <>4__this;
		}
	}
}
