using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027E RID: 638
	public struct Rotate : IEquatable<Rotate>
	{
		// Token: 0x060014A5 RID: 5285 RVA: 0x0005A326 File Offset: 0x00058526
		internal Rotate(Angle angle, Vector3 axis)
		{
			this.m_Angle = angle;
			this.m_Axis = axis;
			this.m_IsNone = false;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0005A33E File Offset: 0x0005853E
		public Rotate(Angle angle)
		{
			this.m_Angle = angle;
			this.m_Axis = Vector3.forward;
			this.m_IsNone = false;
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0005A35C File Offset: 0x0005855C
		internal static Rotate Initial()
		{
			return new Rotate(0f);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0005A380 File Offset: 0x00058580
		public static Rotate None()
		{
			Rotate result = Rotate.Initial();
			result.m_IsNone = true;
			return result;
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0005A3A1 File Offset: 0x000585A1
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x0005A3A9 File Offset: 0x000585A9
		public Angle angle
		{
			get
			{
				return this.m_Angle;
			}
			set
			{
				this.m_Angle = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0005A3B2 File Offset: 0x000585B2
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x0005A3BA File Offset: 0x000585BA
		internal Vector3 axis
		{
			get
			{
				return this.m_Axis;
			}
			set
			{
				this.m_Axis = value;
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0005A3C3 File Offset: 0x000585C3
		internal bool IsNone()
		{
			return this.m_IsNone;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0005A3CC File Offset: 0x000585CC
		public static bool operator ==(Rotate lhs, Rotate rhs)
		{
			return lhs.m_Angle == rhs.m_Angle && lhs.m_Axis == rhs.m_Axis && lhs.m_IsNone == rhs.m_IsNone;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0005A418 File Offset: 0x00058618
		public static bool operator !=(Rotate lhs, Rotate rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0005A434 File Offset: 0x00058634
		public bool Equals(Rotate other)
		{
			return other == this;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0005A454 File Offset: 0x00058654
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is Rotate)
			{
				Rotate other = (Rotate)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0005A480 File Offset: 0x00058680
		public override int GetHashCode()
		{
			return this.m_Angle.GetHashCode() * 793 ^ this.m_Axis.GetHashCode() * 791 ^ this.m_IsNone.GetHashCode() * 197;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0005A4D4 File Offset: 0x000586D4
		public override string ToString()
		{
			return this.m_Angle.ToString() + " " + this.m_Axis.ToString();
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0005A514 File Offset: 0x00058714
		internal Quaternion ToQuaternion()
		{
			return Quaternion.AngleAxis(this.m_Angle.ToDegrees(), this.m_Axis);
		}

		// Token: 0x04000938 RID: 2360
		private Angle m_Angle;

		// Token: 0x04000939 RID: 2361
		private Vector3 m_Axis;

		// Token: 0x0400093A RID: 2362
		private bool m_IsNone;
	}
}
