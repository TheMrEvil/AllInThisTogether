using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x0200009D RID: 157
	public class TransformRecord : IValid, ITransform
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00018538 File Offset: 0x00016738
		public TransformRecord(Transform t)
		{
			if (t)
			{
				this.transform = t;
				this.id = t.GetInstanceID();
				this.localPosition = t.localPosition;
				this.localRotation = t.localRotation;
				this.position = t.position;
				this.rotation = t.rotation;
				this.scale = t.lossyScale;
				this.localToWorldMatrix = t.localToWorldMatrix;
				this.worldToLocalMatrix = t.worldToLocalMatrix;
				if (t.parent)
				{
					this.pid = t.parent.GetInstanceID();
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000185DB File Offset: 0x000167DB
		public Vector3 InverseTransformDirection(Vector3 dir)
		{
			return Quaternion.Inverse(this.rotation) * dir;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000185EE File Offset: 0x000167EE
		public bool IsValid()
		{
			return this.id != 0;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000185F9 File Offset: 0x000167F9
		public void GetUsedTransform(HashSet<Transform> transformSet)
		{
			transformSet.Add(this.transform);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00018608 File Offset: 0x00016808
		public void ReplaceTransform(Dictionary<int, Transform> replaceDict)
		{
			if (replaceDict.ContainsKey(this.id))
			{
				Transform transform = replaceDict[this.id];
				this.transform = transform;
				this.id = this.transform.GetInstanceID();
			}
			if (replaceDict.ContainsKey(this.pid))
			{
				Transform transform2 = replaceDict[this.pid];
				this.pid = transform2.GetInstanceID();
			}
		}

		// Token: 0x040004CC RID: 1228
		public Transform transform;

		// Token: 0x040004CD RID: 1229
		public int id;

		// Token: 0x040004CE RID: 1230
		public Vector3 localPosition;

		// Token: 0x040004CF RID: 1231
		public Quaternion localRotation;

		// Token: 0x040004D0 RID: 1232
		public Vector3 position;

		// Token: 0x040004D1 RID: 1233
		public Quaternion rotation;

		// Token: 0x040004D2 RID: 1234
		public Vector3 scale;

		// Token: 0x040004D3 RID: 1235
		public Matrix4x4 localToWorldMatrix;

		// Token: 0x040004D4 RID: 1236
		public Matrix4x4 worldToLocalMatrix;

		// Token: 0x040004D5 RID: 1237
		public int pid;
	}
}
