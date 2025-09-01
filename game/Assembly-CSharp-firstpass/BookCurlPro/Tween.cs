using System;
using UnityEngine;

namespace BookCurlPro
{
	// Token: 0x02000087 RID: 135
	public class Tween : MonoBehaviour
	{
		// Token: 0x060004F7 RID: 1271 RVA: 0x000263CC File Offset: 0x000245CC
		public static Vector3 ValueTo(GameObject obj, Vector3 from, Vector3 to, float duration, Action<Vector3> update = null, Action finish = null)
		{
			Tween tween = obj.GetComponent<Tween>();
			if (!tween)
			{
				tween = obj.AddComponent<Tween>();
			}
			tween.elapsedtime = 0f;
			tween.working = true;
			tween.enabled = true;
			tween.from = from;
			tween.to = to;
			tween.duration = duration;
			tween.update = update;
			tween.finish = finish;
			return Vector3.zero;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00026432 File Offset: 0x00024632
		private static Vector3 QuadOut(Vector3 start, Vector3 end, float duration, float elapsedTime)
		{
			if (elapsedTime >= duration)
			{
				return end;
			}
			return elapsedTime / duration * (elapsedTime / duration - 2f) * -(end - start) + start;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00026460 File Offset: 0x00024660
		private void Update()
		{
			if (this.working)
			{
				this.elapsedtime += Time.deltaTime;
				Vector3 obj = Tween.QuadOut(this.from, this.to, this.duration, this.elapsedtime);
				if (this.update != null)
				{
					this.update(obj);
				}
				if (this.elapsedtime >= this.duration)
				{
					this.working = false;
					base.enabled = false;
					if (this.finish != null)
					{
						this.finish();
					}
				}
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000264E8 File Offset: 0x000246E8
		public Tween()
		{
		}

		// Token: 0x040004B6 RID: 1206
		private Vector3 from;

		// Token: 0x040004B7 RID: 1207
		private Vector3 to;

		// Token: 0x040004B8 RID: 1208
		private float duration;

		// Token: 0x040004B9 RID: 1209
		private Action<Vector3> update;

		// Token: 0x040004BA RID: 1210
		private Action finish;

		// Token: 0x040004BB RID: 1211
		private float elapsedtime;

		// Token: 0x040004BC RID: 1212
		private bool working;
	}
}
