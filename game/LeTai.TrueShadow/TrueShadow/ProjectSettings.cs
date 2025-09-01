using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace LeTai.TrueShadow
{
	// Token: 0x0200000B RID: 11
	public class ProjectSettings : ScriptableObject
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003176 File Offset: 0x00001376
		public static ProjectSettings Instance
		{
			get
			{
				if (!ProjectSettings.instance)
				{
					ProjectSettings.instance = Resources.Load<ProjectSettings>("True Shadow Project Settings");
				}
				if (!ProjectSettings.instance)
				{
					Debug.LogError("Can't find \"True Shadow Project Settings\" in a Resources folder. Please restore the file or re-install True Shadow.");
				}
				return ProjectSettings.instance;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000031AE File Offset: 0x000013AE
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000031B6 File Offset: 0x000013B6
		public bool UseGlobalAngleByDefault
		{
			get
			{
				return this.useGlobalAngleByDefault;
			}
			private set
			{
				this.useGlobalAngleByDefault = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000031BF File Offset: 0x000013BF
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000031C7 File Offset: 0x000013C7
		public float GlobalAngle
		{
			get
			{
				return this.globalAngle;
			}
			private set
			{
				this.globalAngle = value;
				Action<float> action = this.globalAngleChanged;
				if (action == null)
				{
					return;
				}
				action(this.globalAngle);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000031E6 File Offset: 0x000013E6
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000031EE File Offset: 0x000013EE
		public bool ShowQuickPresetsButtons
		{
			get
			{
				return this.showQuickPresetsButtons;
			}
			private set
			{
				this.showQuickPresetsButtons = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000031F7 File Offset: 0x000013F7
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000031FF File Offset: 0x000013FF
		public List<QuickPreset> QuickPresets
		{
			get
			{
				return this.quickPresets;
			}
			private set
			{
				this.quickPresets = value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600005B RID: 91 RVA: 0x00003208 File Offset: 0x00001408
		// (remove) Token: 0x0600005C RID: 92 RVA: 0x00003240 File Offset: 0x00001440
		public event Action<float> globalAngleChanged
		{
			[CompilerGenerated]
			add
			{
				Action<float> action = this.globalAngleChanged;
				Action<float> action2;
				do
				{
					action2 = action;
					Action<float> value2 = (Action<float>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<float>>(ref this.globalAngleChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<float> action = this.globalAngleChanged;
				Action<float> action2;
				do
				{
					action2 = action;
					Action<float> value2 = (Action<float>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<float>>(ref this.globalAngleChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003275 File Offset: 0x00001475
		public ProjectSettings()
		{
		}

		// Token: 0x0400003D RID: 61
		public const string DEFAULT_RESOURCE_PATH = "True Shadow Project Settings Default";

		// Token: 0x0400003E RID: 62
		public const string RESOURCE_PATH = "True Shadow Project Settings";

		// Token: 0x0400003F RID: 63
		private static ProjectSettings instance;

		// Token: 0x04000040 RID: 64
		[SerializeField]
		internal bool useGlobalAngleByDefault;

		// Token: 0x04000041 RID: 65
		[Knob]
		[SerializeField]
		internal float globalAngle = 90f;

		// Token: 0x04000042 RID: 66
		[SerializeField]
		internal bool showQuickPresetsButtons = true;

		// Token: 0x04000043 RID: 67
		[SerializeField]
		internal List<QuickPreset> quickPresets = new List<QuickPreset>(8);

		// Token: 0x04000044 RID: 68
		[CompilerGenerated]
		private Action<float> globalAngleChanged;
	}
}
