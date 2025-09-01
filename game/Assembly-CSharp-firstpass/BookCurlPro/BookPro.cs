using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BookCurlPro
{
	// Token: 0x02000083 RID: 131
	public class BookPro : MonoBehaviour
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x000248C0 File Offset: 0x00022AC0
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x000248C8 File Offset: 0x00022AC8
		public int CurrentPaper
		{
			get
			{
				return this.currentPaper;
			}
			set
			{
				if (value != this.currentPaper)
				{
					if (value < this.StartFlippingPaper)
					{
						this.currentPaper = this.StartFlippingPaper;
					}
					else if (value > this.EndFlippingPaper + 1)
					{
						this.currentPaper = this.EndFlippingPaper + 1;
					}
					else
					{
						this.currentPaper = value;
					}
					this.UpdatePages();
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0002491D File Offset: 0x00022B1D
		public Vector3 EndBottomLeft
		{
			get
			{
				return this.ebl;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00024925 File Offset: 0x00022B25
		public Vector3 EndBottomRight
		{
			get
			{
				return this.ebr;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00024930 File Offset: 0x00022B30
		public float Height
		{
			get
			{
				return this.BookPanel.rect.height;
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00024950 File Offset: 0x00022B50
		private void Start()
		{
			Canvas[] componentsInParent = base.GetComponentsInParent<Canvas>();
			if (componentsInParent.Length != 0)
			{
				this.canvas = componentsInParent[componentsInParent.Length - 1];
			}
			else
			{
				Debug.LogError("Book Must be a child to canvas diectly or indirectly");
			}
			this.UpdatePages();
			this.CalcCurlCriticalPoints();
			float num = this.BookPanel.rect.width / 2f;
			float height = this.BookPanel.rect.height;
			this.ClippingPlane.rectTransform.sizeDelta = new Vector2(num * 2f + height, height + height * 2f);
			float num2 = Mathf.Sqrt(num * num + height * height);
			float num3 = num / 2f + num2;
			this.Shadow.rectTransform.sizeDelta = new Vector2(num, num3);
			this.Shadow.rectTransform.pivot = new Vector2(1f, num / 2f / num3);
			this.ShadowLTR.rectTransform.sizeDelta = new Vector2(num, num3);
			this.ShadowLTR.rectTransform.pivot = new Vector2(0f, num / 2f / num3);
			this.RightPageShadow.rectTransform.sizeDelta = new Vector2(num, num3);
			this.RightPageShadow.rectTransform.pivot = new Vector2(0f, num / 2f / num3);
			this.LeftPageShadow.rectTransform.sizeDelta = new Vector2(num, num3);
			this.LeftPageShadow.rectTransform.pivot = new Vector2(1f, num / 2f / num3);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00024AEC File Offset: 0x00022CEC
		public Vector3 transformPoint(Vector3 global)
		{
			return this.BookPanel.InverseTransformPoint(global);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00024B04 File Offset: 0x00022D04
		public Vector3 transformPointMousePosition(Vector3 mouseScreenPos)
		{
			if (this.canvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				Vector3 position = this.canvas.worldCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, this.canvas.planeDistance));
				return this.BookPanel.InverseTransformPoint(position);
			}
			if (this.canvas.renderMode == RenderMode.WorldSpace)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 a = base.transform.TransformPoint(this.ebr);
				Vector3 b = base.transform.TransformPoint(this.ebl);
				Vector3 vector = base.transform.TransformPoint(this.st);
				Plane plane = new Plane(a, b, vector);
				float distance;
				plane.Raycast(ray, out distance);
				return this.BookPanel.InverseTransformPoint(ray.GetPoint(distance));
			}
			return this.BookPanel.InverseTransformPoint(mouseScreenPos);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00024C04 File Offset: 0x00022E04
		public void UpdatePages()
		{
			int num = this.pageDragging ? (this.currentPaper - 2) : (this.currentPaper - 1);
			for (int i = 0; i < this.papers.Length; i++)
			{
				BookUtility.HidePage(this.papers[i].Front);
				this.papers[i].Front.transform.SetParent(this.BookPanel.transform);
				BookUtility.HidePage(this.papers[i].Back);
				this.papers[i].Back.transform.SetParent(this.BookPanel.transform);
			}
			if (this.hasTransparentPages)
			{
				for (int j = 0; j <= num; j++)
				{
					BookUtility.ShowPage(this.papers[j].Back);
					this.papers[j].Back.transform.SetParent(this.BookPanel.transform);
					this.papers[j].Back.transform.SetSiblingIndex(j);
					BookUtility.CopyTransform(this.LeftPageTransform.transform, this.papers[j].Back.transform);
				}
				for (int k = this.papers.Length - 1; k >= this.currentPaper; k--)
				{
					BookUtility.ShowPage(this.papers[k].Front);
					this.papers[k].Front.transform.SetSiblingIndex(this.papers.Length - k + num);
					BookUtility.CopyTransform(this.RightPageTransform.transform, this.papers[k].Front.transform);
				}
			}
			else
			{
				if (num >= 0)
				{
					BookUtility.ShowPage(this.papers[num].Back);
					BookUtility.CopyTransform(this.LeftPageTransform.transform, this.papers[num].Back.transform);
				}
				if (this.currentPaper <= this.papers.Length - 1)
				{
					BookUtility.ShowPage(this.papers[this.currentPaper].Front);
					this.papers[this.currentPaper].Front.transform.SetSiblingIndex(this.papers.Length - this.currentPaper + num);
					BookUtility.CopyTransform(this.RightPageTransform.transform, this.papers[this.currentPaper].Front.transform);
				}
			}
			if (!this.enableShadowEffect)
			{
				this.LeftPageShadow.gameObject.SetActive(false);
				this.LeftPageShadow.transform.SetParent(this.BookPanel, true);
				this.RightPageShadow.gameObject.SetActive(false);
				this.RightPageShadow.transform.SetParent(this.BookPanel, true);
				return;
			}
			if (num >= 0)
			{
				this.LeftPageShadow.gameObject.SetActive(true);
				this.LeftPageShadow.transform.SetParent(this.papers[num].Back.transform, true);
				this.LeftPageShadow.rectTransform.anchoredPosition = default(Vector3);
				this.LeftPageShadow.rectTransform.localRotation = Quaternion.identity;
			}
			else
			{
				this.LeftPageShadow.gameObject.SetActive(false);
				this.LeftPageShadow.transform.SetParent(this.BookPanel, true);
			}
			if (this.currentPaper < this.papers.Length)
			{
				this.RightPageShadow.gameObject.SetActive(true);
				this.RightPageShadow.transform.SetParent(this.papers[this.currentPaper].Front.transform, true);
				this.RightPageShadow.rectTransform.anchoredPosition = default(Vector3);
				this.RightPageShadow.rectTransform.localRotation = Quaternion.identity;
				return;
			}
			this.RightPageShadow.gameObject.SetActive(false);
			this.RightPageShadow.transform.SetParent(this.BookPanel, true);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00024FEF File Offset: 0x000231EF
		public void OnMouseDragRightPage()
		{
			if (this.interactable && !this.tweening)
			{
				this.DragRightPageToPoint(this.transformPointMousePosition(Input.mousePosition));
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00025014 File Offset: 0x00023214
		public void DragRightPageToPoint(Vector3 point)
		{
			if (this.currentPaper > this.EndFlippingPaper)
			{
				return;
			}
			this.pageDragging = true;
			this.mode = FlipMode.RightToLeft;
			this.f = point;
			this.ClippingPlane.rectTransform.pivot = new Vector2(1f, 0.35f);
			this.currentPaper++;
			this.UpdatePages();
			this.Left = this.papers[this.currentPaper - 1].Front.GetComponent<Image>();
			BookUtility.ShowPage(this.Left.gameObject);
			this.Left.rectTransform.pivot = new Vector2(0f, 0f);
			this.Left.transform.position = this.RightPageTransform.transform.position;
			this.Left.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.Right = this.papers[this.currentPaper - 1].Back.GetComponent<Image>();
			BookUtility.ShowPage(this.Right.gameObject);
			this.Right.transform.position = this.RightPageTransform.transform.position;
			this.Right.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			if (this.enableShadowEffect)
			{
				this.Shadow.gameObject.SetActive(true);
			}
			this.ClippingPlane.gameObject.SetActive(true);
			this.UpdateBookRTLToPoint(this.f);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000251B3 File Offset: 0x000233B3
		public void OnMouseDragLeftPage()
		{
			if (this.interactable && !this.tweening)
			{
				this.DragLeftPageToPoint(this.transformPointMousePosition(Input.mousePosition));
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000251D8 File Offset: 0x000233D8
		public void DragLeftPageToPoint(Vector3 point)
		{
			if (this.currentPaper <= this.StartFlippingPaper)
			{
				return;
			}
			this.pageDragging = true;
			this.mode = FlipMode.LeftToRight;
			this.f = point;
			this.UpdatePages();
			this.ClippingPlane.rectTransform.pivot = new Vector2(0f, 0.35f);
			this.Right = this.papers[this.currentPaper - 1].Back.GetComponent<Image>();
			BookUtility.ShowPage(this.Right.gameObject);
			this.Right.transform.position = this.LeftPageTransform.transform.position;
			this.Right.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			this.Right.transform.SetAsFirstSibling();
			this.Left = this.papers[this.currentPaper - 1].Front.GetComponent<Image>();
			BookUtility.ShowPage(this.Left.gameObject);
			this.Left.gameObject.SetActive(true);
			this.Left.rectTransform.pivot = new Vector2(1f, 0f);
			this.Left.transform.position = this.LeftPageTransform.transform.position;
			this.Left.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			if (this.enableShadowEffect)
			{
				this.ShadowLTR.gameObject.SetActive(true);
			}
			this.ClippingPlane.gameObject.SetActive(true);
			this.UpdateBookLTRToPoint(this.f);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0002538A File Offset: 0x0002358A
		public void OnMouseRelease()
		{
			if (this.interactable)
			{
				this.ReleasePage();
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0002539C File Offset: 0x0002359C
		public void ReleasePage()
		{
			if (this.pageDragging)
			{
				this.pageDragging = false;
				float num = Vector2.Distance(this.c, this.ebl);
				float num2 = Vector2.Distance(this.c, this.ebr);
				if (num2 < num && this.mode == FlipMode.RightToLeft)
				{
					this.TweenBack();
					return;
				}
				if (num2 > num && this.mode == FlipMode.LeftToRight)
				{
					this.TweenBack();
					return;
				}
				this.TweenForward();
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0002541D File Offset: 0x0002361D
		private void Update()
		{
			if (this.pageDragging && this.interactable)
			{
				this.UpdateBook();
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00025438 File Offset: 0x00023638
		public void UpdateBook()
		{
			this.f = Vector3.Lerp(this.f, this.transformPointMousePosition(Input.mousePosition), Time.deltaTime * 10f);
			if (this.mode == FlipMode.RightToLeft)
			{
				this.UpdateBookRTLToPoint(this.f);
				return;
			}
			this.UpdateBookLTRToPoint(this.f);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00025490 File Offset: 0x00023690
		public void Flip()
		{
			this.pageDragging = false;
			if (this.mode == FlipMode.LeftToRight)
			{
				this.currentPaper--;
			}
			this.Left.transform.SetParent(this.BookPanel.transform, true);
			this.Left.rectTransform.pivot = new Vector2(0f, 0f);
			this.Right.transform.SetParent(this.BookPanel.transform, true);
			this.UpdatePages();
			this.Shadow.gameObject.SetActive(false);
			this.ShadowLTR.gameObject.SetActive(false);
			this.ClippingPlane.gameObject.SetActive(false);
			if (this.OnFlip != null)
			{
				this.OnFlip.Invoke();
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00025560 File Offset: 0x00023760
		public void TweenForward()
		{
			if (this.mode == FlipMode.RightToLeft)
			{
				this.tweening = true;
				Tween.ValueTo(base.gameObject, this.f, this.ebl * 0.98f, 0.3f, new Action<Vector3>(this.TweenUpdate), delegate
				{
					this.Flip();
					this.tweening = false;
				});
				return;
			}
			this.tweening = true;
			Tween.ValueTo(base.gameObject, this.f, this.ebr * 0.98f, 0.3f, new Action<Vector3>(this.TweenUpdate), delegate
			{
				this.Flip();
				this.tweening = false;
			});
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00025602 File Offset: 0x00023802
		private void TweenUpdate(Vector3 follow)
		{
			if (this.mode == FlipMode.RightToLeft)
			{
				this.UpdateBookRTLToPoint(follow);
				return;
			}
			this.UpdateBookLTRToPoint(follow);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0002561C File Offset: 0x0002381C
		public void TweenBack()
		{
			if (this.mode == FlipMode.RightToLeft)
			{
				this.tweening = true;
				Tween.ValueTo(base.gameObject, this.f, this.ebr * 0.98f, 0.3f, new Action<Vector3>(this.TweenUpdate), delegate
				{
					this.currentPaper--;
					this.Right.transform.SetParent(this.BookPanel.transform);
					this.Left.transform.SetParent(this.BookPanel.transform);
					this.tweening = false;
					this.Shadow.gameObject.SetActive(false);
					this.ShadowLTR.gameObject.SetActive(false);
					this.UpdatePages();
				});
				return;
			}
			this.tweening = true;
			Tween.ValueTo(base.gameObject, this.f, this.ebl * 0.98f, 0.3f, new Action<Vector3>(this.TweenUpdate), delegate
			{
				this.Left.transform.SetParent(this.BookPanel.transform);
				this.Right.transform.SetParent(this.BookPanel.transform);
				this.tweening = false;
				this.Shadow.gameObject.SetActive(false);
				this.ShadowLTR.gameObject.SetActive(false);
				this.UpdatePages();
			});
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000256C0 File Offset: 0x000238C0
		private void CalcCurlCriticalPoints()
		{
			this.sb = new Vector3(0f, -this.BookPanel.rect.height / 2f);
			this.ebr = new Vector3(this.BookPanel.rect.width / 2f, -this.BookPanel.rect.height / 2f);
			this.ebl = new Vector3(-this.BookPanel.rect.width / 2f, -this.BookPanel.rect.height / 2f);
			this.st = new Vector3(0f, this.BookPanel.rect.height / 2f);
			this.radius1 = Vector2.Distance(this.sb, this.ebr);
			float num = this.BookPanel.rect.width / 2f;
			float height = this.BookPanel.rect.height;
			this.radius2 = Mathf.Sqrt(num * num + height * height);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00025800 File Offset: 0x00023A00
		public void UpdateBookRTLToPoint(Vector3 followLocation)
		{
			this.mode = FlipMode.RightToLeft;
			this.f = followLocation;
			if (this.enableShadowEffect)
			{
				this.Shadow.transform.SetParent(this.ClippingPlane.transform, true);
				this.Shadow.transform.localPosition = new Vector3(0f, 0f, 0f);
				this.Shadow.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.ShadowLTR.transform.SetParent(this.Left.transform);
				this.ShadowLTR.rectTransform.anchoredPosition = default(Vector3);
				this.ShadowLTR.transform.localEulerAngles = Vector3.zero;
				this.ShadowLTR.gameObject.SetActive(true);
			}
			this.Right.transform.SetParent(this.ClippingPlane.transform, true);
			this.Left.transform.SetParent(this.BookPanel.transform, true);
			this.c = this.Calc_C_Position(followLocation);
			Vector3 vector;
			float num = this.Calc_T0_T1_Angle(this.c, this.ebr, out vector);
			if (num >= -90f)
			{
				num -= 180f;
			}
			this.ClippingPlane.rectTransform.pivot = new Vector2(1f, 0.35f);
			this.ClippingPlane.transform.localEulerAngles = new Vector3(0f, 0f, num + 90f);
			this.ClippingPlane.transform.position = this.BookPanel.TransformPoint(vector);
			this.RightPageShadow.transform.localEulerAngles = new Vector3(0f, 0f, num + 90f);
			this.RightPageShadow.transform.position = this.BookPanel.TransformPoint(vector);
			this.Right.transform.position = this.BookPanel.TransformPoint(this.c);
			float y = vector.y - this.c.y;
			float x = vector.x - this.c.x;
			float num2 = Mathf.Atan2(y, x) * 57.29578f;
			this.Right.transform.localEulerAngles = new Vector3(0f, 0f, num2 - (num + 90f));
			this.Left.transform.SetParent(this.ClippingPlane.transform, true);
			this.Left.transform.SetAsFirstSibling();
			this.Shadow.rectTransform.SetParent(this.Right.rectTransform, true);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00025ABC File Offset: 0x00023CBC
		public void UpdateBookLTRToPoint(Vector3 followLocation)
		{
			this.mode = FlipMode.LeftToRight;
			this.f = followLocation;
			if (this.enableShadowEffect)
			{
				this.ShadowLTR.transform.SetParent(this.ClippingPlane.transform, true);
				this.ShadowLTR.transform.localPosition = new Vector3(0f, 0f, 0f);
				this.ShadowLTR.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
				this.Shadow.transform.SetParent(this.Right.transform);
				this.Shadow.rectTransform.anchoredPosition = new Vector3(0f, 0f, 0f);
				this.Shadow.transform.localEulerAngles = Vector3.zero;
				this.Shadow.gameObject.SetActive(true);
			}
			this.Left.transform.SetParent(this.ClippingPlane.transform, true);
			this.Right.transform.SetParent(this.BookPanel.transform, true);
			this.c = this.Calc_C_Position(followLocation);
			Vector3 vector;
			float num = this.Calc_T0_T1_Angle(this.c, this.ebl, out vector);
			if (num < 0f)
			{
				num += 180f;
			}
			this.ClippingPlane.transform.localEulerAngles = new Vector3(0f, 0f, num - 90f);
			this.ClippingPlane.transform.position = this.BookPanel.TransformPoint(vector);
			this.LeftPageShadow.transform.localEulerAngles = new Vector3(0f, 0f, num - 90f);
			this.LeftPageShadow.transform.position = this.BookPanel.TransformPoint(vector);
			this.Left.transform.position = this.BookPanel.TransformPoint(this.c);
			float y = vector.y - this.c.y;
			float x = vector.x - this.c.x;
			float num2 = Mathf.Atan2(y, x) * 57.29578f;
			this.Left.transform.localEulerAngles = new Vector3(0f, 0f, num2 - 180f - (num - 90f));
			this.Right.transform.SetParent(this.ClippingPlane.transform, true);
			this.Right.transform.SetAsFirstSibling();
			this.ShadowLTR.rectTransform.SetParent(this.Left.rectTransform, true);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00025D68 File Offset: 0x00023F68
		private float Calc_T0_T1_Angle(Vector3 c, Vector3 bookCorner, out Vector3 t1)
		{
			Vector3 vector = (c + bookCorner) / 2f;
			float num = bookCorner.y - vector.y;
			float x = bookCorner.x - vector.x;
			float num2 = Mathf.Atan2(num, x);
			float num3 = vector.x - num * Mathf.Tan(num2);
			num3 = this.normalizeT1X(num3, bookCorner, this.sb);
			t1 = new Vector3(num3, this.sb.y, 0f);
			float y = t1.y - vector.y;
			float x2 = t1.x - vector.x;
			return Mathf.Atan2(y, x2) * 57.29578f;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00025E13 File Offset: 0x00024013
		private float normalizeT1X(float t1, Vector3 corner, Vector3 sb)
		{
			if (t1 > sb.x && sb.x > corner.x)
			{
				return sb.x;
			}
			if (t1 < sb.x && sb.x < corner.x)
			{
				return sb.x;
			}
			return t1;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00025E54 File Offset: 0x00024054
		private Vector3 Calc_C_Position(Vector3 followLocation)
		{
			this.f = followLocation;
			float y = this.f.y - this.sb.y;
			float x = this.f.x - this.sb.x;
			float num = Mathf.Atan2(y, x);
			Vector3 vector = new Vector3(this.radius1 * Mathf.Cos(num), this.radius1 * Mathf.Sin(num), 0f) + this.sb;
			Vector3 vector2;
			if (Vector2.Distance(this.f, this.sb) < this.radius1)
			{
				vector2 = this.f;
			}
			else
			{
				vector2 = vector;
			}
			float y2 = vector2.y - this.st.y;
			float x2 = vector2.x - this.st.x;
			float num2 = Mathf.Atan2(y2, x2);
			Vector3 vector3 = new Vector3(this.radius2 * Mathf.Cos(num2), this.radius2 * Mathf.Sin(num2), 0f) + this.st;
			if (Vector2.Distance(vector2, this.st) > this.radius2)
			{
				vector2 = vector3;
			}
			return vector2;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00025F80 File Offset: 0x00024180
		public BookPro()
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00025FA4 File Offset: 0x000241A4
		[CompilerGenerated]
		private void <TweenForward>b__44_0()
		{
			this.Flip();
			this.tweening = false;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00025FB3 File Offset: 0x000241B3
		[CompilerGenerated]
		private void <TweenForward>b__44_1()
		{
			this.Flip();
			this.tweening = false;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00025FC4 File Offset: 0x000241C4
		[CompilerGenerated]
		private void <TweenBack>b__46_0()
		{
			this.currentPaper--;
			this.Right.transform.SetParent(this.BookPanel.transform);
			this.Left.transform.SetParent(this.BookPanel.transform);
			this.tweening = false;
			this.Shadow.gameObject.SetActive(false);
			this.ShadowLTR.gameObject.SetActive(false);
			this.UpdatePages();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00026044 File Offset: 0x00024244
		[CompilerGenerated]
		private void <TweenBack>b__46_1()
		{
			this.Left.transform.SetParent(this.BookPanel.transform);
			this.Right.transform.SetParent(this.BookPanel.transform);
			this.tweening = false;
			this.Shadow.gameObject.SetActive(false);
			this.ShadowLTR.gameObject.SetActive(false);
			this.UpdatePages();
		}

		// Token: 0x0400048D RID: 1165
		private Canvas canvas;

		// Token: 0x0400048E RID: 1166
		[SerializeField]
		private RectTransform BookPanel;

		// Token: 0x0400048F RID: 1167
		public Image ClippingPlane;

		// Token: 0x04000490 RID: 1168
		public Image Shadow;

		// Token: 0x04000491 RID: 1169
		public Image LeftPageShadow;

		// Token: 0x04000492 RID: 1170
		public Image RightPageShadow;

		// Token: 0x04000493 RID: 1171
		public Image ShadowLTR;

		// Token: 0x04000494 RID: 1172
		public RectTransform LeftPageTransform;

		// Token: 0x04000495 RID: 1173
		public RectTransform RightPageTransform;

		// Token: 0x04000496 RID: 1174
		public bool interactable = true;

		// Token: 0x04000497 RID: 1175
		public bool enableShadowEffect = true;

		// Token: 0x04000498 RID: 1176
		[Tooltip("Uncheck this if the book does not contain transparent pages to improve the overall performance")]
		public bool hasTransparentPages = true;

		// Token: 0x04000499 RID: 1177
		[HideInInspector]
		public int currentPaper;

		// Token: 0x0400049A RID: 1178
		[HideInInspector]
		public Paper[] papers;

		// Token: 0x0400049B RID: 1179
		public UnityEvent OnFlip;

		// Token: 0x0400049C RID: 1180
		[HideInInspector]
		public int StartFlippingPaper;

		// Token: 0x0400049D RID: 1181
		[HideInInspector]
		public int EndFlippingPaper = 1;

		// Token: 0x0400049E RID: 1182
		private Image Left;

		// Token: 0x0400049F RID: 1183
		private Image Right;

		// Token: 0x040004A0 RID: 1184
		private FlipMode mode;

		// Token: 0x040004A1 RID: 1185
		private bool pageDragging;

		// Token: 0x040004A2 RID: 1186
		private bool tweening;

		// Token: 0x040004A3 RID: 1187
		private float radius1;

		// Token: 0x040004A4 RID: 1188
		private float radius2;

		// Token: 0x040004A5 RID: 1189
		private Vector3 sb;

		// Token: 0x040004A6 RID: 1190
		private Vector3 st;

		// Token: 0x040004A7 RID: 1191
		private Vector3 c;

		// Token: 0x040004A8 RID: 1192
		private Vector3 ebr;

		// Token: 0x040004A9 RID: 1193
		private Vector3 ebl;

		// Token: 0x040004AA RID: 1194
		private Vector3 f;
	}
}
