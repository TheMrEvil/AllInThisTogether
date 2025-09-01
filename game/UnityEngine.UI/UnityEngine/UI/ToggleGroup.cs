using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200003B RID: 59
	[AddComponentMenu("UI/Toggle Group", 31)]
	[DisallowMultipleComponent]
	public class ToggleGroup : UIBehaviour
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001598D File Offset: 0x00013B8D
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00015995 File Offset: 0x00013B95
		public bool allowSwitchOff
		{
			get
			{
				return this.m_AllowSwitchOff;
			}
			set
			{
				this.m_AllowSwitchOff = value;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001599E File Offset: 0x00013B9E
		protected ToggleGroup()
		{
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000159B1 File Offset: 0x00013BB1
		protected override void Start()
		{
			this.EnsureValidState();
			base.Start();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000159BF File Offset: 0x00013BBF
		protected override void OnEnable()
		{
			this.EnsureValidState();
			base.OnEnable();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000159CD File Offset: 0x00013BCD
		private void ValidateToggleIsInGroup(Toggle toggle)
		{
			if (toggle == null || !this.m_Toggles.Contains(toggle))
			{
				throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[]
				{
					toggle,
					this
				}));
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00015A04 File Offset: 0x00013C04
		public void NotifyToggleOn(Toggle toggle, bool sendCallback = true)
		{
			this.ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				if (!(this.m_Toggles[i] == toggle))
				{
					if (sendCallback)
					{
						this.m_Toggles[i].isOn = false;
					}
					else
					{
						this.m_Toggles[i].SetIsOnWithoutNotify(false);
					}
				}
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00015A6B File Offset: 0x00013C6B
		public void UnregisterToggle(Toggle toggle)
		{
			if (this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Remove(toggle);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00015A88 File Offset: 0x00013C88
		public void RegisterToggle(Toggle toggle)
		{
			if (!this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Add(toggle);
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00015AA4 File Offset: 0x00013CA4
		public void EnsureValidState()
		{
			if (!this.allowSwitchOff && !this.AnyTogglesOn() && this.m_Toggles.Count != 0)
			{
				this.m_Toggles[0].isOn = true;
				this.NotifyToggleOn(this.m_Toggles[0], true);
			}
			IEnumerable<Toggle> enumerable = this.ActiveToggles();
			if (enumerable.Count<Toggle>() > 1)
			{
				Toggle firstActiveToggle = this.GetFirstActiveToggle();
				foreach (Toggle toggle in enumerable)
				{
					if (!(toggle == firstActiveToggle))
					{
						toggle.isOn = false;
					}
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00015B50 File Offset: 0x00013D50
		public bool AnyTogglesOn()
		{
			return this.m_Toggles.Find((Toggle x) => x.isOn) != null;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00015B82 File Offset: 0x00013D82
		public IEnumerable<Toggle> ActiveToggles()
		{
			return from x in this.m_Toggles
			where x.isOn
			select x;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00015BB0 File Offset: 0x00013DB0
		public Toggle GetFirstActiveToggle()
		{
			IEnumerable<Toggle> source = this.ActiveToggles();
			if (source.Count<Toggle>() <= 0)
			{
				return null;
			}
			return source.First<Toggle>();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00015BD8 File Offset: 0x00013DD8
		public void SetAllTogglesOff(bool sendCallback = true)
		{
			bool allowSwitchOff = this.m_AllowSwitchOff;
			this.m_AllowSwitchOff = true;
			if (sendCallback)
			{
				for (int i = 0; i < this.m_Toggles.Count; i++)
				{
					this.m_Toggles[i].isOn = false;
				}
			}
			else
			{
				for (int j = 0; j < this.m_Toggles.Count; j++)
				{
					this.m_Toggles[j].SetIsOnWithoutNotify(false);
				}
			}
			this.m_AllowSwitchOff = allowSwitchOff;
		}

		// Token: 0x0400017B RID: 379
		[SerializeField]
		private bool m_AllowSwitchOff;

		// Token: 0x0400017C RID: 380
		protected List<Toggle> m_Toggles = new List<Toggle>();

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006F0 RID: 1776 RVA: 0x0001C284 File Offset: 0x0001A484
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x0001C290 File Offset: 0x0001A490
			public <>c()
			{
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x0001C298 File Offset: 0x0001A498
			internal bool <AnyTogglesOn>b__13_0(Toggle x)
			{
				return x.isOn;
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x0001C2A0 File Offset: 0x0001A4A0
			internal bool <ActiveToggles>b__14_0(Toggle x)
			{
				return x.isOn;
			}

			// Token: 0x04000315 RID: 789
			public static readonly ToggleGroup.<>c <>9 = new ToggleGroup.<>c();

			// Token: 0x04000316 RID: 790
			public static Predicate<Toggle> <>9__13_0;

			// Token: 0x04000317 RID: 791
			public static Func<Toggle, bool> <>9__14_0;
		}
	}
}
