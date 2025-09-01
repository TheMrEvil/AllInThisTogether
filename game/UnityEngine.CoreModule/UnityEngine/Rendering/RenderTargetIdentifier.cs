using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003C3 RID: 963
	public struct RenderTargetIdentifier : IEquatable<RenderTargetIdentifier>
	{
		// Token: 0x06001F79 RID: 8057 RVA: 0x0003356D File Offset: 0x0003176D
		public RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			this.m_Type = type;
			this.m_NameID = -1;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000335A5 File Offset: 0x000317A5
		public RenderTargetIdentifier(BuiltinRenderTextureType type, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = type;
			this.m_NameID = -1;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x000335DE File Offset: 0x000317DE
		public RenderTargetIdentifier(string name)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = Shader.PropertyToID(name);
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0003361C File Offset: 0x0003181C
		public RenderTargetIdentifier(string name, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = Shader.PropertyToID(name);
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0003365B File Offset: 0x0003185B
		public RenderTargetIdentifier(int nameID)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = nameID;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x00033694 File Offset: 0x00031894
		public RenderTargetIdentifier(int nameID, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.PropertyName;
			this.m_NameID = nameID;
			this.m_InstanceID = 0;
			this.m_BufferPointer = IntPtr.Zero;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000336D0 File Offset: 0x000318D0
		public RenderTargetIdentifier(RenderTargetIdentifier renderTargetIdentifier, int mipLevel, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = renderTargetIdentifier.m_Type;
			this.m_NameID = renderTargetIdentifier.m_NameID;
			this.m_InstanceID = renderTargetIdentifier.m_InstanceID;
			this.m_BufferPointer = renderTargetIdentifier.m_BufferPointer;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x00033724 File Offset: 0x00031924
		public RenderTargetIdentifier(Texture tex)
		{
			bool flag = tex == null;
			if (flag)
			{
				this.m_Type = BuiltinRenderTextureType.None;
			}
			else
			{
				bool flag2 = tex is RenderTexture;
				if (flag2)
				{
					this.m_Type = BuiltinRenderTextureType.RenderTexture;
				}
				else
				{
					this.m_Type = BuiltinRenderTextureType.BindableTexture;
				}
			}
			this.m_BufferPointer = IntPtr.Zero;
			this.m_NameID = -1;
			this.m_InstanceID = (tex ? tex.GetInstanceID() : 0);
			this.m_MipLevel = 0;
			this.m_CubeFace = CubemapFace.Unknown;
			this.m_DepthSlice = 0;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000337A8 File Offset: 0x000319A8
		public RenderTargetIdentifier(Texture tex, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			bool flag = tex == null;
			if (flag)
			{
				this.m_Type = BuiltinRenderTextureType.None;
			}
			else
			{
				bool flag2 = tex is RenderTexture;
				if (flag2)
				{
					this.m_Type = BuiltinRenderTextureType.RenderTexture;
				}
				else
				{
					this.m_Type = BuiltinRenderTextureType.BindableTexture;
				}
			}
			this.m_BufferPointer = IntPtr.Zero;
			this.m_NameID = -1;
			this.m_InstanceID = (tex ? tex.GetInstanceID() : 0);
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0003382D File Offset: 0x00031A2D
		public RenderTargetIdentifier(RenderBuffer buf, int mipLevel = 0, CubemapFace cubeFace = CubemapFace.Unknown, int depthSlice = 0)
		{
			this.m_Type = BuiltinRenderTextureType.BufferPtr;
			this.m_NameID = -1;
			this.m_InstanceID = buf.m_RenderTextureInstanceID;
			this.m_BufferPointer = buf.m_BufferPtr;
			this.m_MipLevel = mipLevel;
			this.m_CubeFace = cubeFace;
			this.m_DepthSlice = depthSlice;
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00033870 File Offset: 0x00031A70
		public static implicit operator RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			return new RenderTargetIdentifier(type);
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00033888 File Offset: 0x00031A88
		public static implicit operator RenderTargetIdentifier(string name)
		{
			return new RenderTargetIdentifier(name);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000338A0 File Offset: 0x00031AA0
		public static implicit operator RenderTargetIdentifier(int nameID)
		{
			return new RenderTargetIdentifier(nameID);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x000338B8 File Offset: 0x00031AB8
		public static implicit operator RenderTargetIdentifier(Texture tex)
		{
			return new RenderTargetIdentifier(tex);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000338D0 File Offset: 0x00031AD0
		public static implicit operator RenderTargetIdentifier(RenderBuffer buf)
		{
			return new RenderTargetIdentifier(buf, 0, CubemapFace.Unknown, 0);
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000338EC File Offset: 0x00031AEC
		public override string ToString()
		{
			return UnityString.Format("Type {0} NameID {1} InstanceID {2} BufferPointer {3} MipLevel {4} CubeFace {5} DepthSlice {6}", new object[]
			{
				this.m_Type,
				this.m_NameID,
				this.m_InstanceID,
				this.m_BufferPointer,
				this.m_MipLevel,
				this.m_CubeFace,
				this.m_DepthSlice
			});
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00033970 File Offset: 0x00031B70
		public override int GetHashCode()
		{
			return (this.m_Type.GetHashCode() * 23 + this.m_NameID.GetHashCode()) * 23 + this.m_InstanceID.GetHashCode();
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x000339B4 File Offset: 0x00031BB4
		public bool Equals(RenderTargetIdentifier rhs)
		{
			return this.m_Type == rhs.m_Type && this.m_NameID == rhs.m_NameID && this.m_InstanceID == rhs.m_InstanceID && this.m_BufferPointer == rhs.m_BufferPointer && this.m_MipLevel == rhs.m_MipLevel && this.m_CubeFace == rhs.m_CubeFace && this.m_DepthSlice == rhs.m_DepthSlice;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00033A30 File Offset: 0x00031C30
		public override bool Equals(object obj)
		{
			bool flag = !(obj is RenderTargetIdentifier);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				RenderTargetIdentifier rhs = (RenderTargetIdentifier)obj;
				result = this.Equals(rhs);
			}
			return result;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00033A64 File Offset: 0x00031C64
		public static bool operator ==(RenderTargetIdentifier lhs, RenderTargetIdentifier rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00033A80 File Offset: 0x00031C80
		public static bool operator !=(RenderTargetIdentifier lhs, RenderTargetIdentifier rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x04000B78 RID: 2936
		public const int AllDepthSlices = -1;

		// Token: 0x04000B79 RID: 2937
		private BuiltinRenderTextureType m_Type;

		// Token: 0x04000B7A RID: 2938
		private int m_NameID;

		// Token: 0x04000B7B RID: 2939
		private int m_InstanceID;

		// Token: 0x04000B7C RID: 2940
		private IntPtr m_BufferPointer;

		// Token: 0x04000B7D RID: 2941
		private int m_MipLevel;

		// Token: 0x04000B7E RID: 2942
		private CubemapFace m_CubeFace;

		// Token: 0x04000B7F RID: 2943
		private int m_DepthSlice;
	}
}
