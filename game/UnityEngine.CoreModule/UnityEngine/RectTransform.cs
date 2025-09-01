using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200025B RID: 603
	[NativeHeader("Runtime/Transform/RectTransform.h")]
	[NativeClass("UI::RectTransform")]
	public sealed class RectTransform : Transform
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001A09 RID: 6665 RVA: 0x0002A220 File Offset: 0x00028420
		// (remove) Token: 0x06001A0A RID: 6666 RVA: 0x0002A254 File Offset: 0x00028454
		public static event RectTransform.ReapplyDrivenProperties reapplyDrivenProperties
		{
			[CompilerGenerated]
			add
			{
				RectTransform.ReapplyDrivenProperties reapplyDrivenProperties = RectTransform.reapplyDrivenProperties;
				RectTransform.ReapplyDrivenProperties reapplyDrivenProperties2;
				do
				{
					reapplyDrivenProperties2 = reapplyDrivenProperties;
					RectTransform.ReapplyDrivenProperties value2 = (RectTransform.ReapplyDrivenProperties)Delegate.Combine(reapplyDrivenProperties2, value);
					reapplyDrivenProperties = Interlocked.CompareExchange<RectTransform.ReapplyDrivenProperties>(ref RectTransform.reapplyDrivenProperties, value2, reapplyDrivenProperties2);
				}
				while (reapplyDrivenProperties != reapplyDrivenProperties2);
			}
			[CompilerGenerated]
			remove
			{
				RectTransform.ReapplyDrivenProperties reapplyDrivenProperties = RectTransform.reapplyDrivenProperties;
				RectTransform.ReapplyDrivenProperties reapplyDrivenProperties2;
				do
				{
					reapplyDrivenProperties2 = reapplyDrivenProperties;
					RectTransform.ReapplyDrivenProperties value2 = (RectTransform.ReapplyDrivenProperties)Delegate.Remove(reapplyDrivenProperties2, value);
					reapplyDrivenProperties = Interlocked.CompareExchange<RectTransform.ReapplyDrivenProperties>(ref RectTransform.reapplyDrivenProperties, value2, reapplyDrivenProperties2);
				}
				while (reapplyDrivenProperties != reapplyDrivenProperties2);
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x0002A288 File Offset: 0x00028488
		public Rect rect
		{
			get
			{
				Rect result;
				this.get_rect_Injected(out result);
				return result;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x0002A2A0 File Offset: 0x000284A0
		// (set) Token: 0x06001A0D RID: 6669 RVA: 0x0002A2B6 File Offset: 0x000284B6
		public Vector2 anchorMin
		{
			get
			{
				Vector2 result;
				this.get_anchorMin_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchorMin_Injected(ref value);
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x0002A2C0 File Offset: 0x000284C0
		// (set) Token: 0x06001A0F RID: 6671 RVA: 0x0002A2D6 File Offset: 0x000284D6
		public Vector2 anchorMax
		{
			get
			{
				Vector2 result;
				this.get_anchorMax_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchorMax_Injected(ref value);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x0002A2E0 File Offset: 0x000284E0
		// (set) Token: 0x06001A11 RID: 6673 RVA: 0x0002A2F6 File Offset: 0x000284F6
		public Vector2 anchoredPosition
		{
			get
			{
				Vector2 result;
				this.get_anchoredPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchoredPosition_Injected(ref value);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0002A300 File Offset: 0x00028500
		// (set) Token: 0x06001A13 RID: 6675 RVA: 0x0002A316 File Offset: 0x00028516
		public Vector2 sizeDelta
		{
			get
			{
				Vector2 result;
				this.get_sizeDelta_Injected(out result);
				return result;
			}
			set
			{
				this.set_sizeDelta_Injected(ref value);
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0002A320 File Offset: 0x00028520
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x0002A336 File Offset: 0x00028536
		public Vector2 pivot
		{
			get
			{
				Vector2 result;
				this.get_pivot_Injected(out result);
				return result;
			}
			set
			{
				this.set_pivot_Injected(ref value);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0002A340 File Offset: 0x00028540
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x0002A378 File Offset: 0x00028578
		public Vector3 anchoredPosition3D
		{
			get
			{
				Vector2 anchoredPosition = this.anchoredPosition;
				return new Vector3(anchoredPosition.x, anchoredPosition.y, base.localPosition.z);
			}
			set
			{
				this.anchoredPosition = new Vector2(value.x, value.y);
				Vector3 localPosition = base.localPosition;
				localPosition.z = value.z;
				base.localPosition = localPosition;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0002A3BC File Offset: 0x000285BC
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x0002A3EC File Offset: 0x000285EC
		public Vector2 offsetMin
		{
			get
			{
				return this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot));
				this.sizeDelta -= vector;
				this.anchoredPosition += Vector2.Scale(vector, Vector2.one - this.pivot);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0002A458 File Offset: 0x00028658
		// (set) Token: 0x06001A1B RID: 6683 RVA: 0x0002A490 File Offset: 0x00028690
		public Vector2 offsetMax
		{
			get
			{
				return this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot));
				this.sizeDelta += vector;
				this.anchoredPosition += Vector2.Scale(vector, this.pivot);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001A1C RID: 6684
		// (set) Token: 0x06001A1D RID: 6685
		public extern Object drivenByObject { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] internal set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001A1E RID: 6686
		// (set) Token: 0x06001A1F RID: 6687
		internal extern DrivenTransformProperties drivenProperties { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001A20 RID: 6688
		[NativeMethod("UpdateIfTransformDispatchIsDirty")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ForceUpdateRectTransforms();

		// Token: 0x06001A21 RID: 6689 RVA: 0x0002A4FC File Offset: 0x000286FC
		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				Rect rect = this.rect;
				float x = rect.x;
				float y = rect.y;
				float xMax = rect.xMax;
				float yMax = rect.yMax;
				fourCornersArray[0] = new Vector3(x, y, 0f);
				fourCornersArray[1] = new Vector3(x, yMax, 0f);
				fourCornersArray[2] = new Vector3(xMax, yMax, 0f);
				fourCornersArray[3] = new Vector3(xMax, y, 0f);
			}
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0002A5A0 File Offset: 0x000287A0
		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			bool flag = fourCornersArray == null || fourCornersArray.Length < 4;
			if (flag)
			{
				Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
			}
			else
			{
				this.GetLocalCorners(fourCornersArray);
				Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
				for (int i = 0; i < 4; i++)
				{
					fourCornersArray[i] = localToWorldMatrix.MultiplyPoint(fourCornersArray[i]);
				}
			}
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0002A608 File Offset: 0x00028808
		public void SetInsetAndSizeFromParentEdge(RectTransform.Edge edge, float inset, float size)
		{
			int index = (edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Bottom) ? 1 : 0;
			bool flag = edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right;
			float value = (float)(flag ? 1 : 0);
			Vector2 vector = this.anchorMin;
			vector[index] = value;
			this.anchorMin = vector;
			vector = this.anchorMax;
			vector[index] = value;
			this.anchorMax = vector;
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[index] = size;
			this.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = this.anchoredPosition;
			anchoredPosition[index] = (flag ? (-inset - size * (1f - this.pivot[index])) : (inset + size * this.pivot[index]));
			this.anchoredPosition = anchoredPosition;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0002A6D4 File Offset: 0x000288D4
		public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
		{
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[(int)axis] = size - this.GetParentSize()[(int)axis] * (this.anchorMax[(int)axis] - this.anchorMin[(int)axis]);
			this.sizeDelta = sizeDelta;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0002A72D File Offset: 0x0002892D
		[RequiredByNativeCode]
		internal static void SendReapplyDrivenProperties(RectTransform driven)
		{
			RectTransform.ReapplyDrivenProperties reapplyDrivenProperties = RectTransform.reapplyDrivenProperties;
			if (reapplyDrivenProperties != null)
			{
				reapplyDrivenProperties(driven);
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x0002A744 File Offset: 0x00028944
		internal Rect GetRectInParentSpace()
		{
			Rect rect = this.rect;
			Vector2 vector = this.offsetMin + Vector2.Scale(this.pivot, rect.size);
			bool flag = base.transform.parent;
			if (flag)
			{
				RectTransform component = base.transform.parent.GetComponent<RectTransform>();
				bool flag2 = component;
				if (flag2)
				{
					vector += Vector2.Scale(this.anchorMin, component.rect.size);
				}
			}
			rect.x += vector.x;
			rect.y += vector.y;
			return rect;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x0002A7FC File Offset: 0x000289FC
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = base.parent as RectTransform;
			bool flag = !rectTransform;
			Vector2 result;
			if (flag)
			{
				result = Vector2.zero;
			}
			else
			{
				result = rectTransform.rect.size;
			}
			return result;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0002A83D File Offset: 0x00028A3D
		public RectTransform()
		{
		}

		// Token: 0x06001A29 RID: 6697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rect_Injected(out Rect ret);

		// Token: 0x06001A2A RID: 6698
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchorMin_Injected(out Vector2 ret);

		// Token: 0x06001A2B RID: 6699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchorMin_Injected(ref Vector2 value);

		// Token: 0x06001A2C RID: 6700
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchorMax_Injected(out Vector2 ret);

		// Token: 0x06001A2D RID: 6701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchorMax_Injected(ref Vector2 value);

		// Token: 0x06001A2E RID: 6702
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchoredPosition_Injected(out Vector2 ret);

		// Token: 0x06001A2F RID: 6703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchoredPosition_Injected(ref Vector2 value);

		// Token: 0x06001A30 RID: 6704
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_sizeDelta_Injected(out Vector2 ret);

		// Token: 0x06001A31 RID: 6705
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_sizeDelta_Injected(ref Vector2 value);

		// Token: 0x06001A32 RID: 6706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_pivot_Injected(out Vector2 ret);

		// Token: 0x06001A33 RID: 6707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_pivot_Injected(ref Vector2 value);

		// Token: 0x040008B0 RID: 2224
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static RectTransform.ReapplyDrivenProperties reapplyDrivenProperties;

		// Token: 0x0200025C RID: 604
		public enum Edge
		{
			// Token: 0x040008B2 RID: 2226
			Left,
			// Token: 0x040008B3 RID: 2227
			Right,
			// Token: 0x040008B4 RID: 2228
			Top,
			// Token: 0x040008B5 RID: 2229
			Bottom
		}

		// Token: 0x0200025D RID: 605
		public enum Axis
		{
			// Token: 0x040008B7 RID: 2231
			Horizontal,
			// Token: 0x040008B8 RID: 2232
			Vertical
		}

		// Token: 0x0200025E RID: 606
		// (Invoke) Token: 0x06001A35 RID: 6709
		public delegate void ReapplyDrivenProperties(RectTransform driven);
	}
}
