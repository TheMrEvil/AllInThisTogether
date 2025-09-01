using System;

namespace UnityEngine.ProBuilder.Csg
{
	// Token: 0x02000009 RID: 9
	internal static class CSG
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000037F6 File Offset: 0x000019F6
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000037FD File Offset: 0x000019FD
		public static float epsilon
		{
			get
			{
				return CSG.s_Epsilon;
			}
			set
			{
				CSG.s_Epsilon = value;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003805 File Offset: 0x00001A05
		public static Model Perform(CSG.BooleanOp op, GameObject lhs, GameObject rhs)
		{
			switch (op)
			{
			case CSG.BooleanOp.Intersection:
				return CSG.Intersect(lhs, rhs);
			case CSG.BooleanOp.Union:
				return CSG.Union(lhs, rhs);
			case CSG.BooleanOp.Subtraction:
				return CSG.Subtract(lhs, rhs);
			default:
				return null;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003834 File Offset: 0x00001A34
		public static Model Union(GameObject lhs, GameObject rhs)
		{
			Model model = new Model(lhs);
			Model model2 = new Model(rhs);
			Node a = new Node(model.ToPolygons());
			Node b = new Node(model2.ToPolygons());
			return new Model(Node.Union(a, b).AllPolygons());
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003874 File Offset: 0x00001A74
		public static Model Subtract(GameObject lhs, GameObject rhs)
		{
			Model model = new Model(lhs);
			Model model2 = new Model(rhs);
			Node a = new Node(model.ToPolygons());
			Node b = new Node(model2.ToPolygons());
			return new Model(Node.Subtract(a, b).AllPolygons());
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000038B4 File Offset: 0x00001AB4
		public static Model Intersect(GameObject lhs, GameObject rhs)
		{
			Model model = new Model(lhs);
			Model model2 = new Model(rhs);
			Node a = new Node(model.ToPolygons());
			Node b = new Node(model2.ToPolygons());
			return new Model(Node.Intersect(a, b).AllPolygons());
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000038F4 File Offset: 0x00001AF4
		// Note: this type is marked as 'beforefieldinit'.
		static CSG()
		{
		}

		// Token: 0x04000021 RID: 33
		private const float k_DefaultEpsilon = 1E-05f;

		// Token: 0x04000022 RID: 34
		private static float s_Epsilon = 1E-05f;

		// Token: 0x0200000C RID: 12
		public enum BooleanOp
		{
			// Token: 0x0400002A RID: 42
			Intersection,
			// Token: 0x0400002B RID: 43
			Union,
			// Token: 0x0400002C RID: 44
			Subtraction
		}
	}
}
