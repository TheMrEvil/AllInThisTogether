using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x02000090 RID: 144
	public class RTHandle
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00014FC5 File Offset: 0x000131C5
		public void SetCustomHandleProperties(in RTHandleProperties properties)
		{
			this.m_UseCustomHandleScales = true;
			this.m_CustomHandleProperties = properties;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00014FDA File Offset: 0x000131DA
		public void ClearCustomHandleProperties()
		{
			this.m_UseCustomHandleScales = false;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00014FE3 File Offset: 0x000131E3
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00014FEB File Offset: 0x000131EB
		public Vector2 scaleFactor
		{
			[CompilerGenerated]
			get
			{
				return this.<scaleFactor>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<scaleFactor>k__BackingField = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00014FF4 File Offset: 0x000131F4
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00014FFC File Offset: 0x000131FC
		public bool useScaling
		{
			[CompilerGenerated]
			get
			{
				return this.<useScaling>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<useScaling>k__BackingField = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00015005 File Offset: 0x00013205
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0001500D File Offset: 0x0001320D
		public Vector2Int referenceSize
		{
			[CompilerGenerated]
			get
			{
				return this.<referenceSize>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<referenceSize>k__BackingField = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00015016 File Offset: 0x00013216
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				if (!this.m_UseCustomHandleScales)
				{
					return this.m_Owner.rtHandleProperties;
				}
				return this.m_CustomHandleProperties;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00015032 File Offset: 0x00013232
		public RenderTexture rt
		{
			get
			{
				return this.m_RT;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001503A File Offset: 0x0001323A
		public RenderTargetIdentifier nameID
		{
			get
			{
				return this.m_NameID;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00015042 File Offset: 0x00013242
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0001504A File Offset: 0x0001324A
		public bool isMSAAEnabled
		{
			get
			{
				return this.m_EnableMSAA;
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00015052 File Offset: 0x00013252
		internal RTHandle(RTHandleSystem owner)
		{
			this.m_Owner = owner;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00015064 File Offset: 0x00013264
		public static implicit operator RenderTargetIdentifier(RTHandle handle)
		{
			if (handle == null)
			{
				return default(RenderTargetIdentifier);
			}
			return handle.nameID;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00015084 File Offset: 0x00013284
		public static implicit operator Texture(RTHandle handle)
		{
			if (handle == null)
			{
				return null;
			}
			if (!(handle.rt != null))
			{
				return handle.m_ExternalTexture;
			}
			return handle.rt;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000150A6 File Offset: 0x000132A6
		public static implicit operator RenderTexture(RTHandle handle)
		{
			if (handle == null)
			{
				return null;
			}
			return handle.rt;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000150B3 File Offset: 0x000132B3
		internal void SetRenderTexture(RenderTexture rt)
		{
			this.m_RT = rt;
			this.m_ExternalTexture = null;
			this.m_NameID = new RenderTargetIdentifier(rt);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000150CF File Offset: 0x000132CF
		internal void SetTexture(Texture tex)
		{
			this.m_RT = null;
			this.m_ExternalTexture = tex;
			this.m_NameID = new RenderTargetIdentifier(tex);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000150EB File Offset: 0x000132EB
		internal void SetTexture(RenderTargetIdentifier tex)
		{
			this.m_RT = null;
			this.m_ExternalTexture = null;
			this.m_NameID = tex;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00015104 File Offset: 0x00013304
		public int GetInstanceID()
		{
			if (this.m_RT != null)
			{
				return this.m_RT.GetInstanceID();
			}
			if (this.m_ExternalTexture != null)
			{
				return this.m_ExternalTexture.GetInstanceID();
			}
			return this.m_NameID.GetHashCode();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00015156 File Offset: 0x00013356
		public void Release()
		{
			this.m_Owner.Remove(this);
			CoreUtils.Destroy(this.m_RT);
			this.m_NameID = BuiltinRenderTextureType.None;
			this.m_RT = null;
			this.m_ExternalTexture = null;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001518C File Offset: 0x0001338C
		public Vector2Int GetScaledSize(Vector2Int refSize)
		{
			if (!this.useScaling)
			{
				return refSize;
			}
			if (this.scaleFunc != null)
			{
				return this.scaleFunc(refSize);
			}
			return new Vector2Int(Mathf.RoundToInt(this.scaleFactor.x * (float)refSize.x), Mathf.RoundToInt(this.scaleFactor.y * (float)refSize.y));
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000151F0 File Offset: 0x000133F0
		public Vector2Int GetScaledSize()
		{
			if (!this.useScaling)
			{
				return this.referenceSize;
			}
			if (this.scaleFunc != null)
			{
				return this.scaleFunc(this.referenceSize);
			}
			return new Vector2Int(Mathf.RoundToInt(this.scaleFactor.x * (float)this.referenceSize.x), Mathf.RoundToInt(this.scaleFactor.y * (float)this.referenceSize.y));
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001526B File Offset: 0x0001346B
		public void SwitchToFastMemory(CommandBuffer cmd, float residencyFraction = 1f, FastMemoryFlags flags = FastMemoryFlags.SpillTop, bool copyContents = false)
		{
			residencyFraction = Mathf.Clamp01(residencyFraction);
			cmd.SwitchIntoFastMemory(this.m_RT, flags, residencyFraction, copyContents);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001528A File Offset: 0x0001348A
		public void CopyToFastMemory(CommandBuffer cmd, float residencyFraction = 1f, FastMemoryFlags flags = FastMemoryFlags.SpillTop)
		{
			this.SwitchToFastMemory(cmd, residencyFraction, flags, true);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00015296 File Offset: 0x00013496
		public void SwitchOutFastMemory(CommandBuffer cmd, bool copyContents = true)
		{
			cmd.SwitchOutOfFastMemory(this.m_RT, copyContents);
		}

		// Token: 0x040002F5 RID: 757
		internal RTHandleSystem m_Owner;

		// Token: 0x040002F6 RID: 758
		internal RenderTexture m_RT;

		// Token: 0x040002F7 RID: 759
		internal Texture m_ExternalTexture;

		// Token: 0x040002F8 RID: 760
		internal RenderTargetIdentifier m_NameID;

		// Token: 0x040002F9 RID: 761
		internal bool m_EnableMSAA;

		// Token: 0x040002FA RID: 762
		internal bool m_EnableRandomWrite;

		// Token: 0x040002FB RID: 763
		internal bool m_EnableHWDynamicScale;

		// Token: 0x040002FC RID: 764
		internal string m_Name;

		// Token: 0x040002FD RID: 765
		internal bool m_UseCustomHandleScales;

		// Token: 0x040002FE RID: 766
		internal RTHandleProperties m_CustomHandleProperties;

		// Token: 0x040002FF RID: 767
		[CompilerGenerated]
		private Vector2 <scaleFactor>k__BackingField;

		// Token: 0x04000300 RID: 768
		internal ScaleFunc scaleFunc;

		// Token: 0x04000301 RID: 769
		[CompilerGenerated]
		private bool <useScaling>k__BackingField;

		// Token: 0x04000302 RID: 770
		[CompilerGenerated]
		private Vector2Int <referenceSize>k__BackingField;
	}
}
