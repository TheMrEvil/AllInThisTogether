using System;
using System.Globalization;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000116 RID: 278
	[NativeClass("Rectf", "template<typename T> class RectT; typedef RectT<float> Rectf;")]
	[NativeHeader("Runtime/Math/Rect.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Rect : IEquatable<Rect>, IFormattable
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x0000A4EB File Offset: 0x000086EB
		public Rect(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000A50B File Offset: 0x0000870B
		public Rect(Vector2 position, Vector2 size)
		{
			this.m_XMin = position.x;
			this.m_YMin = position.y;
			this.m_Width = size.x;
			this.m_Height = size.y;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000A53E File Offset: 0x0000873E
		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0000A571 File Offset: 0x00008771
		public static Rect zero
		{
			get
			{
				return new Rect(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000A58C File Offset: 0x0000878C
		public static Rect MinMaxRect(float xmin, float ymin, float xmax, float ymax)
		{
			return new Rect(xmin, ymin, xmax - xmin, ymax - ymin);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0000A4EB File Offset: 0x000086EB
		public void Set(float x, float y, float width, float height)
		{
			this.m_XMin = x;
			this.m_YMin = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0000A5AC File Offset: 0x000087AC
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x0000A5C4 File Offset: 0x000087C4
		public float x
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				this.m_XMin = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0000A5D0 File Offset: 0x000087D0
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public float y
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				this.m_YMin = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0000A5F4 File Offset: 0x000087F4
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0000A617 File Offset: 0x00008817
		public Vector2 position
		{
			get
			{
				return new Vector2(this.m_XMin, this.m_YMin);
			}
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0000A634 File Offset: 0x00008834
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0000A671 File Offset: 0x00008871
		public Vector2 center
		{
			get
			{
				return new Vector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
			set
			{
				this.m_XMin = value.x - this.m_Width / 2f;
				this.m_YMin = value.y - this.m_Height / 2f;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0000A6A8 File Offset: 0x000088A8
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0000A6CB File Offset: 0x000088CB
		public Vector2 min
		{
			get
			{
				return new Vector2(this.xMin, this.yMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0000A6E8 File Offset: 0x000088E8
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0000A70B File Offset: 0x0000890B
		public Vector2 max
		{
			get
			{
				return new Vector2(this.xMax, this.yMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0000A728 File Offset: 0x00008928
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0000A740 File Offset: 0x00008940
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0000A74C File Offset: 0x0000894C
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0000A764 File Offset: 0x00008964
		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0000A770 File Offset: 0x00008970
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0000A793 File Offset: 0x00008993
		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_Width, this.m_Height);
			}
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0000A7B0 File Offset: 0x000089B0
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0000A7C8 File Offset: 0x000089C8
		public float xMin
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				float xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0000A7F4 File Offset: 0x000089F4
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0000A80C File Offset: 0x00008A0C
		public float yMin
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				float yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0000A838 File Offset: 0x00008A38
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0000A857 File Offset: 0x00008A57
		public float xMax
		{
			get
			{
				return this.m_Width + this.m_XMin;
			}
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0000A868 File Offset: 0x00008A68
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0000A887 File Offset: 0x00008A87
		public float yMax
		{
			get
			{
				return this.m_Height + this.m_YMin;
			}
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000A898 File Offset: 0x00008A98
		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000A938 File Offset: 0x00008B38
		public bool Contains(Vector3 point, bool allowInverse)
		{
			bool flag = !allowInverse;
			bool result;
			if (flag)
			{
				result = this.Contains(point);
			}
			else
			{
				bool flag2 = (this.width < 0f && point.x <= this.xMin && point.x > this.xMax) || (this.width >= 0f && point.x >= this.xMin && point.x < this.xMax);
				bool flag3 = (this.height < 0f && point.y <= this.yMin && point.y > this.yMax) || (this.height >= 0f && point.y >= this.yMin && point.y < this.yMax);
				result = (flag2 && flag3);
			}
			return result;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000AA18 File Offset: 0x00008C18
		private static Rect OrderMinMax(Rect rect)
		{
			bool flag = rect.xMin > rect.xMax;
			if (flag)
			{
				float xMin = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = xMin;
			}
			bool flag2 = rect.yMin > rect.yMax;
			if (flag2)
			{
				float yMin = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = yMin;
			}
			return rect;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000AA9C File Offset: 0x00008C9C
		public bool Overlaps(Rect other)
		{
			return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		public bool Overlaps(Rect other, bool allowInverse)
		{
			Rect rect = this;
			if (allowInverse)
			{
				rect = Rect.OrderMinMax(rect);
				other = Rect.OrderMinMax(other);
			}
			return rect.Overlaps(other);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000AB28 File Offset: 0x00008D28
		public static Vector2 NormalizedToPoint(Rect rectangle, Vector2 normalizedRectCoordinates)
		{
			return new Vector2(Mathf.Lerp(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x), Mathf.Lerp(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000AB74 File Offset: 0x00008D74
		public static Vector2 PointToNormalized(Rect rectangle, Vector2 point)
		{
			return new Vector2(Mathf.InverseLerp(rectangle.x, rectangle.xMax, point.x), Mathf.InverseLerp(rectangle.y, rectangle.yMax, point.y));
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000ABDC File Offset: 0x00008DDC
		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000AC34 File Offset: 0x00008E34
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.width.GetHashCode() << 2 ^ this.y.GetHashCode() >> 2 ^ this.height.GetHashCode() >> 1;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000AC88 File Offset: 0x00008E88
		public override bool Equals(object other)
		{
			bool flag = !(other is Rect);
			return !flag && this.Equals((Rect)other);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public bool Equals(Rect other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.width.Equals(other.width) && this.height.Equals(other.height);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000AD2C File Offset: 0x00008F2C
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000AD48 File Offset: 0x00008F48
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000AD64 File Offset: 0x00008F64
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.width.ToString(format, formatProvider),
				this.height.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		[Obsolete("use xMin")]
		public float left
		{
			get
			{
				return this.m_XMin;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0000AE10 File Offset: 0x00009010
		[Obsolete("use xMax")]
		public float right
		{
			get
			{
				return this.m_XMin + this.m_Width;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0000AE30 File Offset: 0x00009030
		[Obsolete("use yMin")]
		public float top
		{
			get
			{
				return this.m_YMin;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0000AE48 File Offset: 0x00009048
		[Obsolete("use yMax")]
		public float bottom
		{
			get
			{
				return this.m_YMin + this.m_Height;
			}
		}

		// Token: 0x04000395 RID: 917
		[NativeName("x")]
		private float m_XMin;

		// Token: 0x04000396 RID: 918
		[NativeName("y")]
		private float m_YMin;

		// Token: 0x04000397 RID: 919
		[NativeName("width")]
		private float m_Width;

		// Token: 0x04000398 RID: 920
		[NativeName("height")]
		private float m_Height;
	}
}
