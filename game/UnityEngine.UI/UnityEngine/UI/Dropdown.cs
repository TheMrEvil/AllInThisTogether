using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI.CoroutineTween;

namespace UnityEngine.UI
{
	// Token: 0x0200000E RID: 14
	[AddComponentMenu("UI/Legacy/Dropdown", 102)]
	[RequireComponent(typeof(RectTransform))]
	public class Dropdown : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003FD5 File Offset: 0x000021D5
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003FDD File Offset: 0x000021DD
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003FEC File Offset: 0x000021EC
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003FF4 File Offset: 0x000021F4
		public Text captionText
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004003 File Offset: 0x00002203
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000400B File Offset: 0x0000220B
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000401A File Offset: 0x0000221A
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00004022 File Offset: 0x00002222
		public Text itemText
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004031 File Offset: 0x00002231
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00004039 File Offset: 0x00002239
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004048 File Offset: 0x00002248
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00004055 File Offset: 0x00002255
		public List<Dropdown.OptionData> options
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004069 File Offset: 0x00002269
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00004071 File Offset: 0x00002271
		public Dropdown.DropdownEvent onValueChanged
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000407A File Offset: 0x0000227A
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00004082 File Offset: 0x00002282
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000408B File Offset: 0x0000228B
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00004093 File Offset: 0x00002293
		public int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000409D File Offset: 0x0000229D
		public void SetValueWithoutNotify(int input)
		{
			this.Set(input, false);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000040A8 File Offset: 0x000022A8
		private void Set(int value, bool sendCallback = true)
		{
			if (Application.isPlaying && (value == this.m_Value || this.options.Count == 0))
			{
				return;
			}
			this.m_Value = Mathf.Clamp(value, 0, this.options.Count - 1);
			this.RefreshShownValue();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Dropdown.value", this);
				this.m_OnValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004112 File Offset: 0x00002312
		protected Dropdown()
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004148 File Offset: 0x00002348
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

		// Token: 0x0600007B RID: 123 RVA: 0x0000419C File Offset: 0x0000239C
		protected override void Start()
		{
			this.m_AlphaTweenRunner = new TweenRunner<FloatTween>();
			this.m_AlphaTweenRunner.Init(this);
			base.Start();
			this.RefreshShownValue();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000041C1 File Offset: 0x000023C1
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

		// Token: 0x0600007D RID: 125 RVA: 0x000041F0 File Offset: 0x000023F0
		public void RefreshShownValue()
		{
			Dropdown.OptionData optionData = Dropdown.s_NoOptionData;
			if (this.options.Count > 0)
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
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000042BC File Offset: 0x000024BC
		public void AddOptions(List<Dropdown.OptionData> options)
		{
			this.options.AddRange(options);
			this.RefreshShownValue();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000042D0 File Offset: 0x000024D0
		public void AddOptions(List<string> options)
		{
			int count = options.Count;
			for (int i = 0; i < count; i++)
			{
				this.options.Add(new Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004310 File Offset: 0x00002510
		public void AddOptions(List<Sprite> options)
		{
			int count = options.Count;
			for (int i = 0; i < count; i++)
			{
				this.options.Add(new Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000434D File Offset: 0x0000254D
		public void ClearOptions()
		{
			this.options.Clear();
			this.m_Value = 0;
			this.RefreshShownValue();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004368 File Offset: 0x00002568
		private void SetupTemplate(Canvas rootCanvas)
		{
			this.validTemplate = false;
			if (!this.m_Template)
			{
				Debug.LogError("The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Toggle component serving as the item.", this);
				return;
			}
			GameObject gameObject = this.m_Template.gameObject;
			gameObject.SetActive(true);
			Toggle componentInChildren = this.m_Template.GetComponentInChildren<Toggle>();
			this.validTemplate = true;
			if (!componentInChildren || componentInChildren.transform == this.template)
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The template must have a child GameObject with a Toggle component serving as the item.", this.template);
			}
			else if (!(componentInChildren.transform.parent is RectTransform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The child GameObject with a Toggle component (the item) must have a RectTransform on its parent.", this.template);
			}
			else if (this.itemText != null && !this.itemText.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Text must be on the item GameObject or children of it.", this.template);
			}
			else if (this.itemImage != null && !this.itemImage.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Image must be on the item GameObject or children of it.", this.template);
			}
			if (!this.validTemplate)
			{
				gameObject.SetActive(false);
				return;
			}
			Dropdown.DropdownItem dropdownItem = componentInChildren.gameObject.AddComponent<Dropdown.DropdownItem>();
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
			Canvas canvas2;
			if (!gameObject.TryGetComponent<Canvas>(out canvas2))
			{
				Canvas canvas3 = gameObject.AddComponent<Canvas>();
				canvas3.overrideSorting = true;
				canvas3.sortingOrder = 30000;
				canvas3.sortingLayerID = rootCanvas.sortingLayerID;
			}
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
				Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			Dropdown.GetOrAddComponent<CanvasGroup>(gameObject);
			gameObject.SetActive(false);
			this.validTemplate = true;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000045AC File Offset: 0x000027AC
		private static T GetOrAddComponent<T>(GameObject go) where T : Component
		{
			T t = go.GetComponent<T>();
			if (!t)
			{
				t = go.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000045D5 File Offset: 0x000027D5
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Show();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000045DD File Offset: 0x000027DD
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Show();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000045E5 File Offset: 0x000027E5
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Hide();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000045F0 File Offset: 0x000027F0
		public void Show()
		{
			if (!this.IsActive() || !this.IsInteractable() || this.m_Dropdown != null)
			{
				return;
			}
			List<Canvas> list = CollectionPool<List<Canvas>, Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count == 0)
			{
				return;
			}
			int count = list.Count;
			Canvas canvas = list[count - 1];
			for (int i = 0; i < count; i++)
			{
				if (list[i].isRootCanvas || list[i].overrideSorting)
				{
					canvas = list[i];
					break;
				}
			}
			CollectionPool<List<Canvas>, Canvas>.Release(list);
			if (!this.validTemplate)
			{
				this.SetupTemplate(canvas);
				if (!this.validTemplate)
				{
					return;
				}
			}
			this.m_Template.gameObject.SetActive(true);
			this.m_Dropdown = this.CreateDropdownList(this.m_Template.gameObject);
			this.m_Dropdown.name = "Dropdown List";
			this.m_Dropdown.SetActive(true);
			RectTransform rectTransform = this.m_Dropdown.transform as RectTransform;
			rectTransform.SetParent(this.m_Template.transform.parent, false);
			Dropdown.DropdownItem componentInChildren = this.m_Dropdown.GetComponentInChildren<Dropdown.DropdownItem>();
			RectTransform rectTransform2 = componentInChildren.rectTransform.parent.gameObject.transform as RectTransform;
			componentInChildren.rectTransform.gameObject.SetActive(true);
			Rect rect = rectTransform2.rect;
			Rect rect2 = componentInChildren.rectTransform.rect;
			Vector2 vector = rect2.min - rect.min + componentInChildren.rectTransform.localPosition;
			Vector2 vector2 = rect2.max - rect.max + componentInChildren.rectTransform.localPosition;
			Vector2 size = rect2.size;
			this.m_Items.Clear();
			Toggle toggle = null;
			int count2 = this.options.Count;
			for (int j = 0; j < count2; j++)
			{
				Dropdown.OptionData data = this.options[j];
				Dropdown.DropdownItem item = this.AddItem(data, this.value == j, componentInChildren, this.m_Items);
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
			int count3 = this.m_Items.Count;
			for (int m = 0; m < count3; m++)
			{
				RectTransform rectTransform4 = this.m_Items[m].rectTransform;
				rectTransform4.anchorMin = new Vector2(rectTransform4.anchorMin.x, 0f);
				rectTransform4.anchorMax = new Vector2(rectTransform4.anchorMax.x, 0f);
				rectTransform4.anchoredPosition = new Vector2(rectTransform4.anchoredPosition.x, vector.y + size.y * (float)(count3 - 1 - m) + size.y * rectTransform4.pivot.y);
				rectTransform4.sizeDelta = new Vector2(rectTransform4.sizeDelta.x, size.y);
			}
			this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f, 1f);
			this.m_Template.gameObject.SetActive(false);
			componentInChildren.gameObject.SetActive(false);
			this.m_Blocker = this.CreateBlocker(canvas);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004BDC File Offset: 0x00002DDC
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
				Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			}
			gameObject.AddComponent<Image>().color = Color.clear;
			gameObject.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Hide));
			gameObject.AddComponent<CanvasGroup>().ignoreParentGroups = true;
			return gameObject;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004D22 File Offset: 0x00002F22
		protected virtual void DestroyBlocker(GameObject blocker)
		{
			Object.Destroy(blocker);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004D2A File Offset: 0x00002F2A
		protected virtual GameObject CreateDropdownList(GameObject template)
		{
			return Object.Instantiate<GameObject>(template);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004D32 File Offset: 0x00002F32
		protected virtual void DestroyDropdownList(GameObject dropdownList)
		{
			Object.Destroy(dropdownList);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004D3A File Offset: 0x00002F3A
		protected virtual Dropdown.DropdownItem CreateItem(Dropdown.DropdownItem itemTemplate)
		{
			return Object.Instantiate<Dropdown.DropdownItem>(itemTemplate);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004D42 File Offset: 0x00002F42
		protected virtual void DestroyItem(Dropdown.DropdownItem item)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004D44 File Offset: 0x00002F44
		private Dropdown.DropdownItem AddItem(Dropdown.OptionData data, bool selected, Dropdown.DropdownItem itemTemplate, List<Dropdown.DropdownItem> items)
		{
			Dropdown.DropdownItem dropdownItem = this.CreateItem(itemTemplate);
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

		// Token: 0x0600008F RID: 143 RVA: 0x00004E3C File Offset: 0x0000303C
		private void AlphaFadeList(float duration, float alpha)
		{
			CanvasGroup component = this.m_Dropdown.GetComponent<CanvasGroup>();
			this.AlphaFadeList(duration, component.alpha, alpha);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004E64 File Offset: 0x00003064
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

		// Token: 0x06000091 RID: 145 RVA: 0x00004EC5 File Offset: 0x000030C5
		private void SetAlpha(float alpha)
		{
			if (!this.m_Dropdown)
			{
				return;
			}
			this.m_Dropdown.GetComponent<CanvasGroup>().alpha = alpha;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004EE8 File Offset: 0x000030E8
		public void Hide()
		{
			if (this.m_Dropdown != null)
			{
				this.AlphaFadeList(this.m_AlphaFadeSpeed, 0f);
				if (this.IsActive())
				{
					base.StartCoroutine(this.DelayedDestroyDropdownList(this.m_AlphaFadeSpeed));
				}
			}
			if (this.m_Blocker != null)
			{
				this.DestroyBlocker(this.m_Blocker);
			}
			this.m_Blocker = null;
			this.Select();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004F56 File Offset: 0x00003156
		private IEnumerator DelayedDestroyDropdownList(float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			this.ImmediateDestroyDropdownList();
			yield break;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004F6C File Offset: 0x0000316C
		private void ImmediateDestroyDropdownList()
		{
			int count = this.m_Items.Count;
			for (int i = 0; i < count; i++)
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
			this.m_Dropdown = null;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004FE4 File Offset: 0x000031E4
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

		// Token: 0x06000096 RID: 150 RVA: 0x00005048 File Offset: 0x00003248
		// Note: this type is marked as 'beforefieldinit'.
		static Dropdown()
		{
		}

		// Token: 0x04000031 RID: 49
		[SerializeField]
		private RectTransform m_Template;

		// Token: 0x04000032 RID: 50
		[SerializeField]
		private Text m_CaptionText;

		// Token: 0x04000033 RID: 51
		[SerializeField]
		private Image m_CaptionImage;

		// Token: 0x04000034 RID: 52
		[Space]
		[SerializeField]
		private Text m_ItemText;

		// Token: 0x04000035 RID: 53
		[SerializeField]
		private Image m_ItemImage;

		// Token: 0x04000036 RID: 54
		[Space]
		[SerializeField]
		private int m_Value;

		// Token: 0x04000037 RID: 55
		[Space]
		[SerializeField]
		private Dropdown.OptionDataList m_Options = new Dropdown.OptionDataList();

		// Token: 0x04000038 RID: 56
		[Space]
		[SerializeField]
		private Dropdown.DropdownEvent m_OnValueChanged = new Dropdown.DropdownEvent();

		// Token: 0x04000039 RID: 57
		[SerializeField]
		private float m_AlphaFadeSpeed = 0.15f;

		// Token: 0x0400003A RID: 58
		private GameObject m_Dropdown;

		// Token: 0x0400003B RID: 59
		private GameObject m_Blocker;

		// Token: 0x0400003C RID: 60
		private List<Dropdown.DropdownItem> m_Items = new List<Dropdown.DropdownItem>();

		// Token: 0x0400003D RID: 61
		private TweenRunner<FloatTween> m_AlphaTweenRunner;

		// Token: 0x0400003E RID: 62
		private bool validTemplate;

		// Token: 0x0400003F RID: 63
		private const int kHighSortingLayer = 30000;

		// Token: 0x04000040 RID: 64
		private static Dropdown.OptionData s_NoOptionData = new Dropdown.OptionData();

		// Token: 0x0200007B RID: 123
		protected internal class DropdownItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, ICancelHandler
		{
			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001BB65 File Offset: 0x00019D65
			// (set) Token: 0x06000697 RID: 1687 RVA: 0x0001BB6D File Offset: 0x00019D6D
			public Text text
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

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001BB76 File Offset: 0x00019D76
			// (set) Token: 0x06000699 RID: 1689 RVA: 0x0001BB7E File Offset: 0x00019D7E
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

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001BB87 File Offset: 0x00019D87
			// (set) Token: 0x0600069B RID: 1691 RVA: 0x0001BB8F File Offset: 0x00019D8F
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

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001BB98 File Offset: 0x00019D98
			// (set) Token: 0x0600069D RID: 1693 RVA: 0x0001BBA0 File Offset: 0x00019DA0
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

			// Token: 0x0600069E RID: 1694 RVA: 0x0001BBA9 File Offset: 0x00019DA9
			public virtual void OnPointerEnter(PointerEventData eventData)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x0001BBBC File Offset: 0x00019DBC
			public virtual void OnCancel(BaseEventData eventData)
			{
				Dropdown componentInParent = base.GetComponentInParent<Dropdown>();
				if (componentInParent)
				{
					componentInParent.Hide();
				}
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x0001BBDE File Offset: 0x00019DDE
			public DropdownItem()
			{
			}

			// Token: 0x0400024C RID: 588
			[SerializeField]
			private Text m_Text;

			// Token: 0x0400024D RID: 589
			[SerializeField]
			private Image m_Image;

			// Token: 0x0400024E RID: 590
			[SerializeField]
			private RectTransform m_RectTransform;

			// Token: 0x0400024F RID: 591
			[SerializeField]
			private Toggle m_Toggle;
		}

		// Token: 0x0200007C RID: 124
		[Serializable]
		public class OptionData
		{
			// Token: 0x170001CB RID: 459
			// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001BBE6 File Offset: 0x00019DE6
			// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0001BBEE File Offset: 0x00019DEE
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

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001BBF7 File Offset: 0x00019DF7
			// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0001BBFF File Offset: 0x00019DFF
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

			// Token: 0x060006A5 RID: 1701 RVA: 0x0001BC08 File Offset: 0x00019E08
			public OptionData()
			{
			}

			// Token: 0x060006A6 RID: 1702 RVA: 0x0001BC10 File Offset: 0x00019E10
			public OptionData(string text)
			{
				this.text = text;
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x0001BC1F File Offset: 0x00019E1F
			public OptionData(Sprite image)
			{
				this.image = image;
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x0001BC2E File Offset: 0x00019E2E
			public OptionData(string text, Sprite image)
			{
				this.text = text;
				this.image = image;
			}

			// Token: 0x04000250 RID: 592
			[SerializeField]
			private string m_Text;

			// Token: 0x04000251 RID: 593
			[SerializeField]
			private Sprite m_Image;
		}

		// Token: 0x0200007D RID: 125
		[Serializable]
		public class OptionDataList
		{
			// Token: 0x170001CD RID: 461
			// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001BC44 File Offset: 0x00019E44
			// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001BC4C File Offset: 0x00019E4C
			public List<Dropdown.OptionData> options
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

			// Token: 0x060006AB RID: 1707 RVA: 0x0001BC55 File Offset: 0x00019E55
			public OptionDataList()
			{
				this.options = new List<Dropdown.OptionData>();
			}

			// Token: 0x04000252 RID: 594
			[SerializeField]
			private List<Dropdown.OptionData> m_Options;
		}

		// Token: 0x0200007E RID: 126
		[Serializable]
		public class DropdownEvent : UnityEvent<int>
		{
			// Token: 0x060006AC RID: 1708 RVA: 0x0001BC68 File Offset: 0x00019E68
			public DropdownEvent()
			{
			}
		}

		// Token: 0x0200007F RID: 127
		[CompilerGenerated]
		private sealed class <>c__DisplayClass63_0
		{
			// Token: 0x060006AD RID: 1709 RVA: 0x0001BC70 File Offset: 0x00019E70
			public <>c__DisplayClass63_0()
			{
			}

			// Token: 0x060006AE RID: 1710 RVA: 0x0001BC78 File Offset: 0x00019E78
			internal void <Show>b__0(bool x)
			{
				this.<>4__this.OnSelectItem(this.item.toggle);
			}

			// Token: 0x04000253 RID: 595
			public Dropdown.DropdownItem item;

			// Token: 0x04000254 RID: 596
			public Dropdown <>4__this;
		}

		// Token: 0x02000080 RID: 128
		[CompilerGenerated]
		private sealed class <DelayedDestroyDropdownList>d__75 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060006AF RID: 1711 RVA: 0x0001BC90 File Offset: 0x00019E90
			[DebuggerHidden]
			public <DelayedDestroyDropdownList>d__75(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x0001BC9F File Offset: 0x00019E9F
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x0001BCA4 File Offset: 0x00019EA4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Dropdown dropdown = this;
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
				dropdown.ImmediateDestroyDropdownList();
				return false;
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001BCF7 File Offset: 0x00019EF7
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x0001BCFF File Offset: 0x00019EFF
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001BD06 File Offset: 0x00019F06
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000255 RID: 597
			private int <>1__state;

			// Token: 0x04000256 RID: 598
			private object <>2__current;

			// Token: 0x04000257 RID: 599
			public float delay;

			// Token: 0x04000258 RID: 600
			public Dropdown <>4__this;
		}
	}
}
