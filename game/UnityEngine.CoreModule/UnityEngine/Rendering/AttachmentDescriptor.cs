using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020003EE RID: 1006
	public struct AttachmentDescriptor : IEquatable<AttachmentDescriptor>
	{
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x000382F4 File Offset: 0x000364F4
		// (set) Token: 0x06002207 RID: 8711 RVA: 0x0003830C File Offset: 0x0003650C
		public RenderBufferLoadAction loadAction
		{
			get
			{
				return this.m_LoadAction;
			}
			set
			{
				this.m_LoadAction = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x00038318 File Offset: 0x00036518
		// (set) Token: 0x06002209 RID: 8713 RVA: 0x00038330 File Offset: 0x00036530
		public RenderBufferStoreAction storeAction
		{
			get
			{
				return this.m_StoreAction;
			}
			set
			{
				this.m_StoreAction = value;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x0003833C File Offset: 0x0003653C
		// (set) Token: 0x0600220B RID: 8715 RVA: 0x00038354 File Offset: 0x00036554
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this.m_Format;
			}
			set
			{
				this.m_Format = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00038360 File Offset: 0x00036560
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x000383B3 File Offset: 0x000365B3
		public RenderTextureFormat format
		{
			get
			{
				bool flag = (GraphicsFormatUtility.IsDepthFormat(this.m_Format) || GraphicsFormatUtility.IsStencilFormat(this.m_Format)) && this.m_Format != GraphicsFormat.ShadowAuto;
				RenderTextureFormat result;
				if (flag)
				{
					result = RenderTextureFormat.Depth;
				}
				else
				{
					result = GraphicsFormatUtility.GetRenderTextureFormat(this.m_Format);
				}
				return result;
			}
			set
			{
				this.m_Format = GraphicsFormatUtility.GetGraphicsFormat(value, RenderTextureReadWrite.Default);
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000383C4 File Offset: 0x000365C4
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x000383DC File Offset: 0x000365DC
		public RenderTargetIdentifier loadStoreTarget
		{
			get
			{
				return this.m_LoadStoreTarget;
			}
			set
			{
				this.m_LoadStoreTarget = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x000383E8 File Offset: 0x000365E8
		// (set) Token: 0x06002211 RID: 8721 RVA: 0x00038400 File Offset: 0x00036600
		public RenderTargetIdentifier resolveTarget
		{
			get
			{
				return this.m_ResolveTarget;
			}
			set
			{
				this.m_ResolveTarget = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x0003840C File Offset: 0x0003660C
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x00038424 File Offset: 0x00036624
		public Color clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
			set
			{
				this.m_ClearColor = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x00038430 File Offset: 0x00036630
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x00038448 File Offset: 0x00036648
		public float clearDepth
		{
			get
			{
				return this.m_ClearDepth;
			}
			set
			{
				this.m_ClearDepth = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x00038454 File Offset: 0x00036654
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x0003846C File Offset: 0x0003666C
		public uint clearStencil
		{
			get
			{
				return this.m_ClearStencil;
			}
			set
			{
				this.m_ClearStencil = value;
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00038478 File Offset: 0x00036678
		public void ConfigureTarget(RenderTargetIdentifier target, bool loadExistingContents, bool storeResults)
		{
			this.m_LoadStoreTarget = target;
			bool flag = loadExistingContents && this.m_LoadAction != RenderBufferLoadAction.Clear;
			if (flag)
			{
				this.m_LoadAction = RenderBufferLoadAction.Load;
			}
			if (storeResults)
			{
				bool flag2 = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Resolve;
				if (flag2)
				{
					this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
				}
				else
				{
					this.m_StoreAction = RenderBufferStoreAction.Store;
				}
			}
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000384DC File Offset: 0x000366DC
		public void ConfigureResolveTarget(RenderTargetIdentifier target)
		{
			this.m_ResolveTarget = target;
			bool flag = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Store;
			if (flag)
			{
				this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
			}
			else
			{
				this.m_StoreAction = RenderBufferStoreAction.Resolve;
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0003851A File Offset: 0x0003671A
		public void ConfigureClear(Color clearColor, float clearDepth = 1f, uint clearStencil = 0U)
		{
			this.m_ClearColor = clearColor;
			this.m_ClearDepth = clearDepth;
			this.m_ClearStencil = clearStencil;
			this.m_LoadAction = RenderBufferLoadAction.Clear;
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0003853C File Offset: 0x0003673C
		public AttachmentDescriptor(GraphicsFormat format)
		{
			this = default(AttachmentDescriptor);
			this.m_LoadAction = RenderBufferLoadAction.DontCare;
			this.m_StoreAction = RenderBufferStoreAction.DontCare;
			this.m_Format = format;
			this.m_LoadStoreTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ResolveTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ClearColor = new Color(0f, 0f, 0f, 0f);
			this.m_ClearDepth = 1f;
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000385A8 File Offset: 0x000367A8
		public AttachmentDescriptor(RenderTextureFormat format)
		{
			this = new AttachmentDescriptor(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000385A8 File Offset: 0x000367A8
		public AttachmentDescriptor(RenderTextureFormat format, RenderTargetIdentifier target, bool loadExistingContents = false, bool storeResults = false, bool resolve = false)
		{
			this = new AttachmentDescriptor(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000385BC File Offset: 0x000367BC
		public bool Equals(AttachmentDescriptor other)
		{
			return this.m_LoadAction == other.m_LoadAction && this.m_StoreAction == other.m_StoreAction && this.m_Format == other.m_Format && this.m_LoadStoreTarget.Equals(other.m_LoadStoreTarget) && this.m_ResolveTarget.Equals(other.m_ResolveTarget) && this.m_ClearColor.Equals(other.m_ClearColor) && this.m_ClearDepth.Equals(other.m_ClearDepth) && this.m_ClearStencil == other.m_ClearStencil;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00038658 File Offset: 0x00036858
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is AttachmentDescriptor && this.Equals((AttachmentDescriptor)obj);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x00038690 File Offset: 0x00036890
		public override int GetHashCode()
		{
			int num = (int)this.m_LoadAction;
			num = (num * 397 ^ (int)this.m_StoreAction);
			num = (num * 397 ^ (int)this.m_Format);
			num = (num * 397 ^ this.m_LoadStoreTarget.GetHashCode());
			num = (num * 397 ^ this.m_ResolveTarget.GetHashCode());
			num = (num * 397 ^ this.m_ClearColor.GetHashCode());
			num = (num * 397 ^ this.m_ClearDepth.GetHashCode());
			return num * 397 ^ (int)this.m_ClearStencil;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0003873C File Offset: 0x0003693C
		public static bool operator ==(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00038758 File Offset: 0x00036958
		public static bool operator !=(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000C67 RID: 3175
		private RenderBufferLoadAction m_LoadAction;

		// Token: 0x04000C68 RID: 3176
		private RenderBufferStoreAction m_StoreAction;

		// Token: 0x04000C69 RID: 3177
		private GraphicsFormat m_Format;

		// Token: 0x04000C6A RID: 3178
		private RenderTargetIdentifier m_LoadStoreTarget;

		// Token: 0x04000C6B RID: 3179
		private RenderTargetIdentifier m_ResolveTarget;

		// Token: 0x04000C6C RID: 3180
		private Color m_ClearColor;

		// Token: 0x04000C6D RID: 3181
		private float m_ClearDepth;

		// Token: 0x04000C6E RID: 3182
		private uint m_ClearStencil;
	}
}
