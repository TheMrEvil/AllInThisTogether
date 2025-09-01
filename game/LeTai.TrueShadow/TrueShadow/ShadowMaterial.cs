using System;
using System.Runtime.CompilerServices;
using System.Threading;
using LeTai.TrueShadow.PluginInterfaces;
using UnityEngine;

namespace LeTai.TrueShadow
{
	// Token: 0x0200000A RID: 10
	[ExecuteAlways]
	public class ShadowMaterial : MonoBehaviour, ITrueShadowRendererMaterialProvider
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000048 RID: 72 RVA: 0x00002FF4 File Offset: 0x000011F4
		// (remove) Token: 0x06000049 RID: 73 RVA: 0x0000302C File Offset: 0x0000122C
		public event Action materialReplaced
		{
			[CompilerGenerated]
			add
			{
				Action action = this.materialReplaced;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.materialReplaced, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.materialReplaced;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.materialReplaced, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600004A RID: 74 RVA: 0x00003064 File Offset: 0x00001264
		// (remove) Token: 0x0600004B RID: 75 RVA: 0x0000309C File Offset: 0x0000129C
		public event Action materialModified
		{
			[CompilerGenerated]
			add
			{
				Action action = this.materialModified;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.materialModified, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.materialModified;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.materialModified, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000030D1 File Offset: 0x000012D1
		public Material GetTrueShadowRendererMaterial()
		{
			if (!base.isActiveAndEnabled)
			{
				return null;
			}
			return this.material;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000030E4 File Offset: 0x000012E4
		private void OnEnable()
		{
			TrueShadow component = base.GetComponent<TrueShadow>();
			if (component)
			{
				component.RefreshPlugins();
			}
			Action action = this.materialReplaced;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003118 File Offset: 0x00001318
		private void OnDisable()
		{
			TrueShadow component = base.GetComponent<TrueShadow>();
			if (component)
			{
				component.RefreshPlugins();
			}
			Action action = this.materialReplaced;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000314A File Offset: 0x0000134A
		private void OnValidate()
		{
			Action action = this.materialReplaced;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000315C File Offset: 0x0000135C
		public void OnMaterialModified()
		{
			Action action = this.materialModified;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000316E File Offset: 0x0000136E
		public ShadowMaterial()
		{
		}

		// Token: 0x0400003A RID: 58
		public Material material;

		// Token: 0x0400003B RID: 59
		[CompilerGenerated]
		private Action materialReplaced;

		// Token: 0x0400003C RID: 60
		[CompilerGenerated]
		private Action materialModified;
	}
}
