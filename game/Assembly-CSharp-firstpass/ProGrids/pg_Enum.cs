using System;
using UnityEngine;

namespace ProGrids
{
	// Token: 0x0200002F RID: 47
	public static class pg_Enum
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00008548 File Offset: 0x00006748
		public static Vector3 InverseAxisMask(Vector3 v, Axis axis)
		{
			if (axis <= Axis.NegX)
			{
				switch (axis)
				{
				case Axis.X:
					break;
				case Axis.Y:
					goto IL_49;
				case (Axis)3:
					return v;
				case Axis.Z:
					goto IL_64;
				default:
					if (axis != Axis.NegX)
					{
						return v;
					}
					break;
				}
				return Vector3.Scale(v, new Vector3(0f, 1f, 1f));
			}
			if (axis != Axis.NegY)
			{
				if (axis != Axis.NegZ)
				{
					return v;
				}
				goto IL_64;
			}
			IL_49:
			return Vector3.Scale(v, new Vector3(1f, 0f, 1f));
			IL_64:
			return Vector3.Scale(v, new Vector3(1f, 1f, 0f));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000085D8 File Offset: 0x000067D8
		public static Vector3 AxisMask(Vector3 v, Axis axis)
		{
			if (axis <= Axis.NegX)
			{
				switch (axis)
				{
				case Axis.X:
					break;
				case Axis.Y:
					goto IL_49;
				case (Axis)3:
					return v;
				case Axis.Z:
					goto IL_64;
				default:
					if (axis != Axis.NegX)
					{
						return v;
					}
					break;
				}
				return Vector3.Scale(v, new Vector3(1f, 0f, 0f));
			}
			if (axis != Axis.NegY)
			{
				if (axis != Axis.NegZ)
				{
					return v;
				}
				goto IL_64;
			}
			IL_49:
			return Vector3.Scale(v, new Vector3(0f, 1f, 0f));
			IL_64:
			return Vector3.Scale(v, new Vector3(0f, 0f, 1f));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00008668 File Offset: 0x00006868
		public static float SnapUnitValue(SnapUnit su)
		{
			switch (su)
			{
			case SnapUnit.Meter:
				return 1f;
			case SnapUnit.Centimeter:
				return 0.01f;
			case SnapUnit.Millimeter:
				return 0.001f;
			case SnapUnit.Inch:
				return 0.025399987f;
			case SnapUnit.Foot:
				return 0.3048f;
			case SnapUnit.Yard:
				return 1.09361f;
			case SnapUnit.Parsec:
				return 5f;
			default:
				return 1f;
			}
		}
	}
}
