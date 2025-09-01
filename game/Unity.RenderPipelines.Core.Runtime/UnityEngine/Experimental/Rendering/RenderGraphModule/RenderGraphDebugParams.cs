using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200001F RID: 31
	internal class RenderGraphDebugParams
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00007014 File Offset: 0x00005214
		public void RegisterDebug(string name, DebugUI.Panel debugPanel = null)
		{
			this.m_DebugItems = new List<DebugUI.Widget>
			{
				new DebugUI.Container
				{
					displayName = name + " Render Graph",
					children = 
					{
						new DebugUI.BoolField
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.ClearRenderTargetsAtCreation,
							getter = (() => this.clearRenderTargetsAtCreation),
							setter = delegate(bool value)
							{
								this.clearRenderTargetsAtCreation = value;
							}
						},
						new DebugUI.BoolField
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.DisablePassCulling,
							getter = (() => this.disablePassCulling),
							setter = delegate(bool value)
							{
								this.disablePassCulling = value;
							}
						},
						new DebugUI.BoolField
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.ImmediateMode,
							getter = (() => this.immediateMode),
							setter = delegate(bool value)
							{
								this.immediateMode = value;
							}
						},
						new DebugUI.BoolField
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.EnableLogging,
							getter = (() => this.enableLogging),
							setter = delegate(bool value)
							{
								this.enableLogging = value;
							}
						},
						new DebugUI.Button
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.LogFrameInformation,
							action = delegate
							{
								if (!this.enableLogging)
								{
									Debug.Log("You must first enable logging before this logging frame information.");
								}
								this.logFrameInformation = true;
							}
						},
						new DebugUI.Button
						{
							nameAndTooltip = RenderGraphDebugParams.Strings.LogResources,
							action = delegate
							{
								if (!this.enableLogging)
								{
									Debug.Log("You must first enable logging before this logging resources.");
								}
								this.logResources = true;
							}
						}
					}
				}
			}.ToArray();
			this.m_DebugPanel = ((debugPanel != null) ? debugPanel : DebugManager.instance.GetPanel((name.Length == 0) ? "Render Graph" : name, true, 0, false));
			this.m_DebugPanel.children.Add(this.m_DebugItems);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000071E4 File Offset: 0x000053E4
		public void UnRegisterDebug(string name)
		{
			this.m_DebugPanel.children.Remove(this.m_DebugItems);
			this.m_DebugPanel = null;
			this.m_DebugItems = null;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000720B File Offset: 0x0000540B
		public RenderGraphDebugParams()
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007213 File Offset: 0x00005413
		[CompilerGenerated]
		private bool <RegisterDebug>b__10_0()
		{
			return this.clearRenderTargetsAtCreation;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000721B File Offset: 0x0000541B
		[CompilerGenerated]
		private void <RegisterDebug>b__10_1(bool value)
		{
			this.clearRenderTargetsAtCreation = value;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007224 File Offset: 0x00005424
		[CompilerGenerated]
		private bool <RegisterDebug>b__10_2()
		{
			return this.disablePassCulling;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000722C File Offset: 0x0000542C
		[CompilerGenerated]
		private void <RegisterDebug>b__10_3(bool value)
		{
			this.disablePassCulling = value;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007235 File Offset: 0x00005435
		[CompilerGenerated]
		private bool <RegisterDebug>b__10_4()
		{
			return this.immediateMode;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000723D File Offset: 0x0000543D
		[CompilerGenerated]
		private void <RegisterDebug>b__10_5(bool value)
		{
			this.immediateMode = value;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007246 File Offset: 0x00005446
		[CompilerGenerated]
		private bool <RegisterDebug>b__10_6()
		{
			return this.enableLogging;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000724E File Offset: 0x0000544E
		[CompilerGenerated]
		private void <RegisterDebug>b__10_7(bool value)
		{
			this.enableLogging = value;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007257 File Offset: 0x00005457
		[CompilerGenerated]
		private void <RegisterDebug>b__10_8()
		{
			if (!this.enableLogging)
			{
				Debug.Log("You must first enable logging before this logging frame information.");
			}
			this.logFrameInformation = true;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007272 File Offset: 0x00005472
		[CompilerGenerated]
		private void <RegisterDebug>b__10_9()
		{
			if (!this.enableLogging)
			{
				Debug.Log("You must first enable logging before this logging resources.");
			}
			this.logResources = true;
		}

		// Token: 0x040000D0 RID: 208
		private DebugUI.Widget[] m_DebugItems;

		// Token: 0x040000D1 RID: 209
		private DebugUI.Panel m_DebugPanel;

		// Token: 0x040000D2 RID: 210
		public bool clearRenderTargetsAtCreation;

		// Token: 0x040000D3 RID: 211
		public bool clearRenderTargetsAtRelease;

		// Token: 0x040000D4 RID: 212
		public bool disablePassCulling;

		// Token: 0x040000D5 RID: 213
		public bool immediateMode;

		// Token: 0x040000D6 RID: 214
		public bool enableLogging;

		// Token: 0x040000D7 RID: 215
		public bool logFrameInformation;

		// Token: 0x040000D8 RID: 216
		public bool logResources;

		// Token: 0x02000129 RID: 297
		private static class Strings
		{
			// Token: 0x0600080D RID: 2061 RVA: 0x000226F4 File Offset: 0x000208F4
			// Note: this type is marked as 'beforefieldinit'.
			static Strings()
			{
			}

			// Token: 0x040004CB RID: 1227
			public static readonly DebugUI.Widget.NameAndTooltip ClearRenderTargetsAtCreation = new DebugUI.Widget.NameAndTooltip
			{
				name = "Clear Render Targets At Creation",
				tooltip = "Enable to clear all render textures before any rendergraph passes to check if some clears are missing."
			};

			// Token: 0x040004CC RID: 1228
			public static readonly DebugUI.Widget.NameAndTooltip DisablePassCulling = new DebugUI.Widget.NameAndTooltip
			{
				name = "Disable Pass Culling",
				tooltip = "Enable to temporarily disable culling to asses if a pass is culled."
			};

			// Token: 0x040004CD RID: 1229
			public static readonly DebugUI.Widget.NameAndTooltip ImmediateMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "Immediate Mode",
				tooltip = "Enable to force render graph to execute all passes in the order you registered them."
			};

			// Token: 0x040004CE RID: 1230
			public static readonly DebugUI.Widget.NameAndTooltip EnableLogging = new DebugUI.Widget.NameAndTooltip
			{
				name = "Enable Logging",
				tooltip = "Enable to allow HDRP to capture information in the log."
			};

			// Token: 0x040004CF RID: 1231
			public static readonly DebugUI.Widget.NameAndTooltip LogFrameInformation = new DebugUI.Widget.NameAndTooltip
			{
				name = "Log Frame Information",
				tooltip = "Enable to log information output from each frame."
			};

			// Token: 0x040004D0 RID: 1232
			public static readonly DebugUI.Widget.NameAndTooltip LogResources = new DebugUI.Widget.NameAndTooltip
			{
				name = "Log Resources",
				tooltip = "Enable to log the current render graph's global resource usage."
			};
		}
	}
}
