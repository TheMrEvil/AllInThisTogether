using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200005B RID: 91
	internal sealed class Transform2D
	{
		// Token: 0x0600036E RID: 878 RVA: 0x0002123F File Offset: 0x0001F43F
		public Transform2D(Vector2 position, float rotation, Vector2 scale)
		{
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0002125C File Offset: 0x0001F45C
		public Vector2 TransformPoint(Vector2 p)
		{
			p += this.position;
			p.RotateAroundPoint(p, this.rotation);
			p.ScaleAroundPoint(p, this.scale);
			return p;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0002128C File Offset: 0x0001F48C
		public override string ToString()
		{
			string[] array = new string[6];
			array[0] = "T: ";
			int num = 1;
			Vector2 vector = this.position;
			array[num] = vector.ToString();
			array[2] = "\nR: ";
			array[3] = this.rotation.ToString();
			array[4] = "°\nS: ";
			int num2 = 5;
			vector = this.scale;
			array[num2] = vector.ToString();
			return string.Concat(array);
		}

		// Token: 0x04000200 RID: 512
		public Vector2 position;

		// Token: 0x04000201 RID: 513
		public float rotation;

		// Token: 0x04000202 RID: 514
		public Vector2 scale;
	}
}
