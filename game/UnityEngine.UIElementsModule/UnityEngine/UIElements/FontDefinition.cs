using System;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x02000277 RID: 631
	public struct FontDefinition : IEquatable<FontDefinition>
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00055DEC File Offset: 0x00053FEC
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x00055E04 File Offset: 0x00054004
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				bool flag = value != null && this.fontAsset != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
				}
				this.m_Font = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00055E40 File Offset: 0x00054040
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x00055E58 File Offset: 0x00054058
		public FontAsset fontAsset
		{
			get
			{
				return this.m_FontAsset;
			}
			set
			{
				bool flag = value != null && this.font != null;
				if (flag)
				{
					throw new InvalidOperationException("Cannot set both Font and FontAsset on FontDefinition");
				}
				this.m_FontAsset = value;
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00055E94 File Offset: 0x00054094
		public static FontDefinition FromFont(Font f)
		{
			return new FontDefinition
			{
				m_Font = f
			};
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00055EB8 File Offset: 0x000540B8
		public static FontDefinition FromSDFFont(FontAsset f)
		{
			return new FontDefinition
			{
				m_FontAsset = f
			};
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00055EDC File Offset: 0x000540DC
		internal static FontDefinition FromObject(object obj)
		{
			Font font = obj as Font;
			bool flag = font != null;
			FontDefinition result;
			if (flag)
			{
				result = FontDefinition.FromFont(font);
			}
			else
			{
				FontAsset fontAsset = obj as FontAsset;
				bool flag2 = fontAsset != null;
				if (flag2)
				{
					result = FontDefinition.FromSDFFont(fontAsset);
				}
				else
				{
					result = default(FontDefinition);
				}
			}
			return result;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00055F30 File Offset: 0x00054130
		internal bool IsEmpty()
		{
			return this.m_Font == null && this.m_FontAsset == null;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00055F60 File Offset: 0x00054160
		public override string ToString()
		{
			bool flag = this.font != null;
			string result;
			if (flag)
			{
				result = string.Format("{0}", this.font);
			}
			else
			{
				result = string.Format("{0}", this.fontAsset);
			}
			return result;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00055FA8 File Offset: 0x000541A8
		public bool Equals(FontDefinition other)
		{
			return object.Equals(this.m_Font, other.m_Font) && object.Equals(this.m_FontAsset, other.m_FontAsset);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00055FE4 File Offset: 0x000541E4
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is FontDefinition)
			{
				FontDefinition other = (FontDefinition)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00056010 File Offset: 0x00054210
		public override int GetHashCode()
		{
			return ((this.m_Font != null) ? this.m_Font.GetHashCode() : 0) * 397 ^ ((this.m_FontAsset != null) ? this.m_FontAsset.GetHashCode() : 0);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00056064 File Offset: 0x00054264
		public static bool operator ==(FontDefinition left, FontDefinition right)
		{
			return left.Equals(right);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00056080 File Offset: 0x00054280
		public static bool operator !=(FontDefinition left, FontDefinition right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000917 RID: 2327
		private Font m_Font;

		// Token: 0x04000918 RID: 2328
		private FontAsset m_FontAsset;
	}
}
