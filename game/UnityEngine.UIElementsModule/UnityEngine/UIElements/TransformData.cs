using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029F RID: 671
	internal struct TransformData : IStyleDataGroup<TransformData>, IEquatable<TransformData>
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x00060CF4 File Offset: 0x0005EEF4
		public TransformData Copy()
		{
			return this;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00060D0C File Offset: 0x0005EF0C
		public void CopyFrom(ref TransformData other)
		{
			this = other;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00060D1C File Offset: 0x0005EF1C
		public static bool operator ==(TransformData lhs, TransformData rhs)
		{
			return lhs.rotate == rhs.rotate && lhs.scale == rhs.scale && lhs.transformOrigin == rhs.transformOrigin && lhs.translate == rhs.translate;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00060D7C File Offset: 0x0005EF7C
		public static bool operator !=(TransformData lhs, TransformData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00060D98 File Offset: 0x0005EF98
		public bool Equals(TransformData other)
		{
			return other == this;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00060DB8 File Offset: 0x0005EFB8
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is TransformData && this.Equals((TransformData)obj);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00060DF0 File Offset: 0x0005EFF0
		public override int GetHashCode()
		{
			int num = this.rotate.GetHashCode();
			num = (num * 397 ^ this.scale.GetHashCode());
			num = (num * 397 ^ this.transformOrigin.GetHashCode());
			return num * 397 ^ this.translate.GetHashCode();
		}

		// Token: 0x040009AF RID: 2479
		public Rotate rotate;

		// Token: 0x040009B0 RID: 2480
		public Scale scale;

		// Token: 0x040009B1 RID: 2481
		public TransformOrigin transformOrigin;

		// Token: 0x040009B2 RID: 2482
		public Translate translate;
	}
}
