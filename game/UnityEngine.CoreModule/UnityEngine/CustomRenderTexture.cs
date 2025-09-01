using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AC RID: 428
	[NativeHeader("Runtime/Graphics/CustomRenderTexture.h")]
	[UsedByNativeCode]
	public sealed class CustomRenderTexture : RenderTexture
	{
		// Token: 0x06001293 RID: 4755
		[FreeFunction(Name = "CustomRenderTextureScripting::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateCustomRenderTexture([Writable] CustomRenderTexture rt);

		// Token: 0x06001294 RID: 4756
		[NativeName("TriggerUpdate")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TriggerUpdate(int count);

		// Token: 0x06001295 RID: 4757 RVA: 0x00019137 File Offset: 0x00017337
		public void Update(int count)
		{
			CustomRenderTextureManager.InvokeTriggerUpdate(this, count);
			this.TriggerUpdate(count);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0001914A File Offset: 0x0001734A
		public void Update()
		{
			this.Update(1);
		}

		// Token: 0x06001297 RID: 4759
		[NativeName("TriggerInitialization")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TriggerInitialization();

		// Token: 0x06001298 RID: 4760 RVA: 0x00019155 File Offset: 0x00017355
		public void Initialize()
		{
			this.TriggerInitialization();
			CustomRenderTextureManager.InvokeTriggerInitialize(this);
		}

		// Token: 0x06001299 RID: 4761
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearUpdateZones();

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600129A RID: 4762
		// (set) Token: 0x0600129B RID: 4763
		public extern Material material { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600129C RID: 4764
		// (set) Token: 0x0600129D RID: 4765
		public extern Material initializationMaterial { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600129E RID: 4766
		// (set) Token: 0x0600129F RID: 4767
		public extern Texture initializationTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060012A0 RID: 4768
		[FreeFunction(Name = "CustomRenderTextureScripting::GetUpdateZonesInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetUpdateZonesInternal([NotNull("ArgumentNullException")] object updateZones);

		// Token: 0x060012A1 RID: 4769 RVA: 0x00019166 File Offset: 0x00017366
		public void GetUpdateZones(List<CustomRenderTextureUpdateZone> updateZones)
		{
			this.GetUpdateZonesInternal(updateZones);
		}

		// Token: 0x060012A2 RID: 4770
		[FreeFunction(Name = "CustomRenderTextureScripting::SetUpdateZonesInternal", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetUpdateZonesInternal([Unmarshalled] CustomRenderTextureUpdateZone[] updateZones);

		// Token: 0x060012A3 RID: 4771
		[FreeFunction(Name = "CustomRenderTextureScripting::GetDoubleBufferRenderTexture", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern RenderTexture GetDoubleBufferRenderTexture();

		// Token: 0x060012A4 RID: 4772
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnsureDoubleBufferConsistency();

		// Token: 0x060012A5 RID: 4773 RVA: 0x00019174 File Offset: 0x00017374
		public void SetUpdateZones(CustomRenderTextureUpdateZone[] updateZones)
		{
			bool flag = updateZones == null;
			if (flag)
			{
				throw new ArgumentNullException("updateZones");
			}
			this.SetUpdateZonesInternal(updateZones);
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060012A6 RID: 4774
		// (set) Token: 0x060012A7 RID: 4775
		public extern CustomRenderTextureInitializationSource initializationSource { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x000191A0 File Offset: 0x000173A0
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x000191B6 File Offset: 0x000173B6
		public Color initializationColor
		{
			get
			{
				Color result;
				this.get_initializationColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_initializationColor_Injected(ref value);
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060012AA RID: 4778
		// (set) Token: 0x060012AB RID: 4779
		public extern CustomRenderTextureUpdateMode updateMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060012AC RID: 4780
		// (set) Token: 0x060012AD RID: 4781
		public extern CustomRenderTextureUpdateMode initializationMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060012AE RID: 4782
		// (set) Token: 0x060012AF RID: 4783
		public extern CustomRenderTextureUpdateZoneSpace updateZoneSpace { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060012B0 RID: 4784
		// (set) Token: 0x060012B1 RID: 4785
		public extern int shaderPass { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060012B2 RID: 4786
		// (set) Token: 0x060012B3 RID: 4787
		public extern uint cubemapFaceMask { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060012B4 RID: 4788
		// (set) Token: 0x060012B5 RID: 4789
		public extern bool doubleBuffered { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060012B6 RID: 4790
		// (set) Token: 0x060012B7 RID: 4791
		public extern bool wrapUpdateZones { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060012B8 RID: 4792
		// (set) Token: 0x060012B9 RID: 4793
		public extern float updatePeriod { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060012BA RID: 4794 RVA: 0x000191C0 File Offset: 0x000173C0
		public CustomRenderTexture(int width, int height, RenderTextureFormat format, [DefaultValue("RenderTextureReadWrite.Default")] RenderTextureReadWrite readWrite) : this(width, height, RenderTexture.GetCompatibleFormat(format, readWrite))
		{
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000191D4 File Offset: 0x000173D4
		[ExcludeFromDocs]
		public CustomRenderTexture(int width, int height, RenderTextureFormat format) : this(width, height, format, RenderTextureReadWrite.Default)
		{
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x000191E2 File Offset: 0x000173E2
		[ExcludeFromDocs]
		public CustomRenderTexture(int width, int height) : this(width, height, SystemInfo.GetGraphicsFormat(DefaultFormat.LDR))
		{
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000191F4 File Offset: 0x000173F4
		[ExcludeFromDocs]
		public CustomRenderTexture(int width, int height, [DefaultValue("DefaultFormat.LDR")] DefaultFormat defaultFormat) : this(width, height, RenderTexture.GetDefaultColorFormat(defaultFormat))
		{
			bool flag = defaultFormat == DefaultFormat.DepthStencil || defaultFormat == DefaultFormat.Shadow;
			if (flag)
			{
				base.depthStencilFormat = SystemInfo.GetGraphicsFormat(defaultFormat);
			}
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00019230 File Offset: 0x00017430
		[ExcludeFromDocs]
		public CustomRenderTexture(int width, int height, GraphicsFormat format)
		{
			bool flag = format != GraphicsFormat.None && !base.ValidateFormat(format, FormatUsage.Render);
			if (!flag)
			{
				CustomRenderTexture.Internal_CreateCustomRenderTexture(this);
				this.width = width;
				this.height = height;
				base.graphicsFormat = format;
				base.SetSRGBReadWrite(GraphicsFormatUtility.IsSRGBFormat(format));
			}
		}

		// Token: 0x060012BF RID: 4799
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_initializationColor_Injected(out Color ret);

		// Token: 0x060012C0 RID: 4800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_initializationColor_Injected(ref Color value);
	}
}
