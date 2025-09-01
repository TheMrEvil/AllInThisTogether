using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200015E RID: 350
	public abstract class AbstractProgressBar : BindableElement, INotifyValueChanged<float>
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x0002E3BD File Offset: 0x0002C5BD
		public string title
		{
			get
			{
				return this.m_Title.text;
			}
			set
			{
				this.m_Title.text = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0002E3CC File Offset: 0x0002C5CC
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x0002E3D4 File Offset: 0x0002C5D4
		public float lowValue
		{
			get
			{
				return this.m_LowValue;
			}
			set
			{
				this.m_LowValue = value;
				this.SetProgress(this.m_Value);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002E3EB File Offset: 0x0002C5EB
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x0002E3F3 File Offset: 0x0002C5F3
		public float highValue
		{
			get
			{
				return this.m_HighValue;
			}
			set
			{
				this.m_HighValue = value;
				this.SetProgress(this.m_Value);
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002E40C File Offset: 0x0002C60C
		public AbstractProgressBar()
		{
			base.AddToClassList(AbstractProgressBar.ussClassName);
			VisualElement visualElement = new VisualElement
			{
				name = AbstractProgressBar.ussClassName
			};
			this.m_Background = new VisualElement();
			this.m_Background.AddToClassList(AbstractProgressBar.backgroundUssClassName);
			visualElement.Add(this.m_Background);
			this.m_Progress = new VisualElement();
			this.m_Progress.AddToClassList(AbstractProgressBar.progressUssClassName);
			this.m_Background.Add(this.m_Progress);
			VisualElement visualElement2 = new VisualElement();
			visualElement2.AddToClassList(AbstractProgressBar.titleContainerUssClassName);
			this.m_Background.Add(visualElement2);
			this.m_Title = new Label();
			this.m_Title.AddToClassList(AbstractProgressBar.titleUssClassName);
			visualElement2.Add(this.m_Title);
			visualElement.AddToClassList(AbstractProgressBar.containerUssClassName);
			base.hierarchy.Add(visualElement);
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002E519 File Offset: 0x0002C719
		private void OnGeometryChanged(GeometryChangedEvent e)
		{
			this.SetProgress(this.value);
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0002E52C File Offset: 0x0002C72C
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x0002E544 File Offset: 0x0002C744
		public virtual float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = !EqualityComparer<float>.Default.Equals(this.m_Value, value);
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<float> pooled = ChangeEvent<float>.GetPooled(this.m_Value, value))
						{
							pooled.target = this;
							this.SetValueWithoutNotify(value);
							this.SendEvent(pooled);
						}
					}
					else
					{
						this.SetValueWithoutNotify(value);
					}
				}
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002E5CC File Offset: 0x0002C7CC
		public void SetValueWithoutNotify(float newValue)
		{
			this.m_Value = newValue;
			this.SetProgress(this.value);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002E5E4 File Offset: 0x0002C7E4
		private void SetProgress(float p)
		{
			bool flag = p < this.lowValue;
			float num;
			if (flag)
			{
				num = this.lowValue;
			}
			else
			{
				bool flag2 = p > this.highValue;
				if (flag2)
				{
					num = this.highValue;
				}
				else
				{
					num = p;
				}
			}
			num = this.CalculateProgressWidth(num);
			bool flag3 = num >= 0f;
			if (flag3)
			{
				this.m_Progress.style.right = num;
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002E658 File Offset: 0x0002C858
		private float CalculateProgressWidth(float width)
		{
			bool flag = this.m_Background == null || this.m_Progress == null;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				bool flag2 = float.IsNaN(this.m_Background.layout.width);
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					float num = this.m_Background.layout.width - 2f;
					result = num - Mathf.Max(num * width / this.highValue, 1f);
				}
			}
			return result;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002E6E4 File Offset: 0x0002C8E4
		// Note: this type is marked as 'beforefieldinit'.
		static AbstractProgressBar()
		{
		}

		// Token: 0x0400051C RID: 1308
		public static readonly string ussClassName = "unity-progress-bar";

		// Token: 0x0400051D RID: 1309
		public static readonly string containerUssClassName = AbstractProgressBar.ussClassName + "__container";

		// Token: 0x0400051E RID: 1310
		public static readonly string titleUssClassName = AbstractProgressBar.ussClassName + "__title";

		// Token: 0x0400051F RID: 1311
		public static readonly string titleContainerUssClassName = AbstractProgressBar.ussClassName + "__title-container";

		// Token: 0x04000520 RID: 1312
		public static readonly string progressUssClassName = AbstractProgressBar.ussClassName + "__progress";

		// Token: 0x04000521 RID: 1313
		public static readonly string backgroundUssClassName = AbstractProgressBar.ussClassName + "__background";

		// Token: 0x04000522 RID: 1314
		private readonly VisualElement m_Background;

		// Token: 0x04000523 RID: 1315
		private readonly VisualElement m_Progress;

		// Token: 0x04000524 RID: 1316
		private readonly Label m_Title;

		// Token: 0x04000525 RID: 1317
		private float m_LowValue;

		// Token: 0x04000526 RID: 1318
		private float m_HighValue = 100f;

		// Token: 0x04000527 RID: 1319
		private float m_Value;

		// Token: 0x04000528 RID: 1320
		private const float k_MinVisibleProgress = 1f;

		// Token: 0x0200015F RID: 351
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000B50 RID: 2896 RVA: 0x0002E760 File Offset: 0x0002C960
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				AbstractProgressBar abstractProgressBar = ve as AbstractProgressBar;
				abstractProgressBar.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				abstractProgressBar.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				abstractProgressBar.value = this.m_Value.GetValueFromBag(bag, cc);
				abstractProgressBar.title = this.m_Title.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000B51 RID: 2897 RVA: 0x0002E7D0 File Offset: 0x0002C9D0
			public UxmlTraits()
			{
			}

			// Token: 0x04000529 RID: 1321
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value",
				defaultValue = 0f
			};

			// Token: 0x0400052A RID: 1322
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				defaultValue = 100f
			};

			// Token: 0x0400052B RID: 1323
			private UxmlFloatAttributeDescription m_Value = new UxmlFloatAttributeDescription
			{
				name = "value",
				defaultValue = 0f
			};

			// Token: 0x0400052C RID: 1324
			private UxmlStringAttributeDescription m_Title = new UxmlStringAttributeDescription
			{
				name = "title",
				defaultValue = string.Empty
			};
		}
	}
}
