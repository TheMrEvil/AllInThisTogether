using System;

namespace UnityEngine
{
	// Token: 0x02000257 RID: 599
	[Serializable]
	public struct Pose : IEquatable<Pose>
	{
		// Token: 0x060019F3 RID: 6643 RVA: 0x00029F14 File Offset: 0x00028114
		public Pose(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00029F28 File Offset: 0x00028128
		public override string ToString()
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(),
				this.rotation.ToString()
			});
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00029F74 File Offset: 0x00028174
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(format),
				this.rotation.ToString(format)
			});
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00029FB4 File Offset: 0x000281B4
		public Pose GetTransformedBy(Pose lhs)
		{
			return new Pose
			{
				position = lhs.position + lhs.rotation * this.position,
				rotation = lhs.rotation * this.rotation
			};
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0002A00C File Offset: 0x0002820C
		public Pose GetTransformedBy(Transform lhs)
		{
			return new Pose
			{
				position = lhs.TransformPoint(this.position),
				rotation = lhs.rotation * this.rotation
			};
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0002A054 File Offset: 0x00028254
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0002A078 File Offset: 0x00028278
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x0002A09C File Offset: 0x0002829C
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0002A0C0 File Offset: 0x000282C0
		public static Pose identity
		{
			get
			{
				return Pose.k_Identity;
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x0002A0D8 File Offset: 0x000282D8
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Pose);
			return !flag && this.Equals((Pose)obj);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0002A10C File Offset: 0x0002830C
		public bool Equals(Pose other)
		{
			return this.position.Equals(other.position) && this.rotation.Equals(other.rotation);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0002A148 File Offset: 0x00028348
		public override int GetHashCode()
		{
			return this.position.GetHashCode() ^ this.rotation.GetHashCode() << 1;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0002A180 File Offset: 0x00028380
		public static bool operator ==(Pose a, Pose b)
		{
			return a.position == b.position && a.rotation.Equals(b.rotation);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0002A1BC File Offset: 0x000283BC
		public static bool operator !=(Pose a, Pose b)
		{
			return !(a == b);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0002A1D8 File Offset: 0x000283D8
		// Note: this type is marked as 'beforefieldinit'.
		static Pose()
		{
		}

		// Token: 0x04000893 RID: 2195
		public Vector3 position;

		// Token: 0x04000894 RID: 2196
		public Quaternion rotation;

		// Token: 0x04000895 RID: 2197
		private static readonly Pose k_Identity = new Pose(Vector3.zero, Quaternion.identity);
	}
}
