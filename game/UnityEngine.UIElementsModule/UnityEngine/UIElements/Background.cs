using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026E RID: 622
	public struct Background : IEquatable<Background>
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0004B8A4 File Offset: 0x00049AA4
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x0004B8BC File Offset: 0x00049ABC
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				bool flag = this.m_Texture == value;
				if (!flag)
				{
					this.m_Texture = value;
					this.m_Sprite = null;
					this.m_RenderTexture = null;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0004B8F8 File Offset: 0x00049AF8
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x0004B910 File Offset: 0x00049B10
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				bool flag = this.m_Sprite == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = value;
					this.m_RenderTexture = null;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0004B94C File Offset: 0x00049B4C
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x0004B964 File Offset: 0x00049B64
		public RenderTexture renderTexture
		{
			get
			{
				return this.m_RenderTexture;
			}
			set
			{
				bool flag = this.m_RenderTexture == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = null;
					this.m_RenderTexture = value;
					this.m_VectorImage = null;
				}
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x0004B9A0 File Offset: 0x00049BA0
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x0004B9B8 File Offset: 0x00049BB8
		public VectorImage vectorImage
		{
			get
			{
				return this.m_VectorImage;
			}
			set
			{
				bool flag = this.vectorImage == value;
				if (!flag)
				{
					this.m_Texture = null;
					this.m_Sprite = null;
					this.m_RenderTexture = null;
					this.m_VectorImage = value;
				}
			}
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0004B9F4 File Offset: 0x00049BF4
		[Obsolete("Use Background.FromTexture2D instead")]
		public Background(Texture2D t)
		{
			this.m_Texture = t;
			this.m_Sprite = null;
			this.m_RenderTexture = null;
			this.m_VectorImage = null;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0004BA14 File Offset: 0x00049C14
		public static Background FromTexture2D(Texture2D t)
		{
			return new Background
			{
				texture = t
			};
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0004BA38 File Offset: 0x00049C38
		public static Background FromRenderTexture(RenderTexture rt)
		{
			return new Background
			{
				renderTexture = rt
			};
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0004BA5C File Offset: 0x00049C5C
		public static Background FromSprite(Sprite s)
		{
			return new Background
			{
				sprite = s
			};
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0004BA80 File Offset: 0x00049C80
		public static Background FromVectorImage(VectorImage vi)
		{
			return new Background
			{
				vectorImage = vi
			};
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0004BAA4 File Offset: 0x00049CA4
		internal static Background FromObject(object obj)
		{
			Texture2D texture2D = obj as Texture2D;
			bool flag = texture2D != null;
			Background result;
			if (flag)
			{
				result = Background.FromTexture2D(texture2D);
			}
			else
			{
				RenderTexture renderTexture = obj as RenderTexture;
				bool flag2 = renderTexture != null;
				if (flag2)
				{
					result = Background.FromRenderTexture(renderTexture);
				}
				else
				{
					Sprite sprite = obj as Sprite;
					bool flag3 = sprite != null;
					if (flag3)
					{
						result = Background.FromSprite(sprite);
					}
					else
					{
						VectorImage vectorImage = obj as VectorImage;
						bool flag4 = vectorImage != null;
						if (flag4)
						{
							result = Background.FromVectorImage(vectorImage);
						}
						else
						{
							result = default(Background);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0004BB3C File Offset: 0x00049D3C
		public static bool operator ==(Background lhs, Background rhs)
		{
			return lhs.texture == rhs.texture && lhs.sprite == rhs.sprite && lhs.renderTexture == rhs.renderTexture && lhs.vectorImage == rhs.vectorImage;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0004BBA4 File Offset: 0x00049DA4
		public static bool operator !=(Background lhs, Background rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0004BBC0 File Offset: 0x00049DC0
		public bool Equals(Background other)
		{
			return other == this;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004BBE0 File Offset: 0x00049DE0
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Background);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Background lhs = (Background)obj;
				result = (lhs == this);
			}
			return result;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0004BC1C File Offset: 0x00049E1C
		public override int GetHashCode()
		{
			int num = 851985039;
			bool flag = this.texture != null;
			if (flag)
			{
				num = num * -1521134295 + this.texture.GetHashCode();
			}
			bool flag2 = this.sprite != null;
			if (flag2)
			{
				num = num * -1521134295 + this.sprite.GetHashCode();
			}
			bool flag3 = this.renderTexture != null;
			if (flag3)
			{
				num = num * -1521134295 + this.renderTexture.GetHashCode();
			}
			bool flag4 = this.vectorImage != null;
			if (flag4)
			{
				num = num * -1521134295 + this.vectorImage.GetHashCode();
			}
			return num;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0004BCC0 File Offset: 0x00049EC0
		public override string ToString()
		{
			bool flag = this.texture != null;
			string result;
			if (flag)
			{
				result = this.texture.ToString();
			}
			else
			{
				bool flag2 = this.sprite != null;
				if (flag2)
				{
					result = this.sprite.ToString();
				}
				else
				{
					bool flag3 = this.renderTexture != null;
					if (flag3)
					{
						result = this.renderTexture.ToString();
					}
					else
					{
						bool flag4 = this.vectorImage != null;
						if (flag4)
						{
							result = this.vectorImage.ToString();
						}
						else
						{
							result = "";
						}
					}
				}
			}
			return result;
		}

		// Token: 0x040008D1 RID: 2257
		private Texture2D m_Texture;

		// Token: 0x040008D2 RID: 2258
		private Sprite m_Sprite;

		// Token: 0x040008D3 RID: 2259
		private RenderTexture m_RenderTexture;

		// Token: 0x040008D4 RID: 2260
		private VectorImage m_VectorImage;
	}
}
