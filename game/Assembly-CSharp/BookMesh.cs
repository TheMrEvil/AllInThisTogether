using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class BookMesh : MonoBehaviour
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
	public void GenerateMesh()
	{
		this.mesh = new BMesh();
		this.lodMesh = new BMesh();
		this.mesh.AddVertexAttribute("uv", BMesh.AttributeBaseType.Float, 2);
		this.lodMesh.AddVertexAttribute("uv", BMesh.AttributeBaseType.Float, 2);
		this.BookCount = 0;
		this.SpacesCreated = 0;
		if (this.ConsistentHeight)
		{
			for (int i = 0; i < this.ShelfCount; i++)
			{
				this.CreateShelf(new Vector3(0f, (float)i * this.ShelfHeight, 0f));
			}
		}
		else
		{
			foreach (float y in this.ShelfHeights)
			{
				this.CreateShelf(new Vector3(0f, y, 0f));
			}
		}
		BMeshUnity.SetInMeshFilter(this.mesh, base.GetComponent<MeshFilter>());
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002158 File Offset: 0x00000358
	private void CreateShelf(Vector3 pt)
	{
		bool flag = false;
		float num = 0f;
		while (num < this.ShelfWidth / 2f)
		{
			if (this.SpacesCreated < this.SpecialSpaces && UnityEngine.Random.Range(0, 100) < 6)
			{
				num += this.LargeEmptySpace();
				flag = true;
			}
			else if (UnityEngine.Random.Range(0, 100) < 20 && num + 1.5f < this.ShelfWidth / 2f && !flag)
			{
				num += this.CreateSpecialSpace(pt, num, this.DoubleSided && UnityEngine.Random.Range(0, 100) > 50);
				flag = true;
			}
			else
			{
				float num2 = UnityEngine.Random.Range(0.1f, 0.35f);
				if (UnityEngine.Random.Range(0, 100) > 10 || flag)
				{
					this.AddBook(pt, num, num2, Mathf.Max(UnityEngine.Random.Range(0.6f, 0.9f), num2 * 2.5f), this.DoubleSided && UnityEngine.Random.Range(0, 100) > 50);
				}
				num += num2;
				flag = false;
			}
		}
		num = 0f;
		while (num < this.ShelfWidth / 2f)
		{
			if (this.SpacesCreated < this.SpecialSpaces && UnityEngine.Random.Range(0, 100) < 6)
			{
				num += this.LargeEmptySpace();
				flag = true;
			}
			else if (UnityEngine.Random.Range(0, 100) < 20 && num + 1.5f < this.ShelfWidth / 2f && !flag)
			{
				num += this.CreateSpecialSpace(pt, -num - 0.001f, this.DoubleSided && UnityEngine.Random.Range(0, 100) > 50);
				flag = true;
			}
			else
			{
				float num3 = UnityEngine.Random.Range(0.1f, 0.35f);
				if (UnityEngine.Random.Range(0, 100) > 10 || flag)
				{
					this.AddBook(pt, -num - num3, num3, Mathf.Max(UnityEngine.Random.Range(0.5f, 0.9f), num3 * 2.5f), this.DoubleSided && UnityEngine.Random.Range(0, 100) > 50);
				}
				num += num3;
				flag = false;
			}
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002353 File Offset: 0x00000553
	private float LargeEmptySpace()
	{
		float result = UnityEngine.Random.Range(0.4f, 1.2f);
		this.SpacesCreated++;
		return result;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002372 File Offset: 0x00000572
	private float CreateSpecialSpace(Vector3 startPt, float offset, bool invert)
	{
		if (UnityEngine.Random.Range(0, 100) > 20)
		{
			return this.AngledBookGroup(startPt, offset, invert);
		}
		return this.BookStack(startPt, offset);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002394 File Offset: 0x00000594
	private float BookStack(Vector3 startPt, float offset)
	{
		float num = 0.8f;
		float num2 = (offset < 0f) ? (offset - num / 2f - 0.2f) : (offset + num / 2f + 0.2f);
		float num3 = this.Curvature * Mathf.Pow(num2 / this.ShelfWidth, 2f);
		num3 += UnityEngine.Random.Range(0.1f, 0.2f);
		Vector3 center = startPt + new Vector3(num2, 0f, num3);
		int num4 = UnityEngine.Random.Range(2, 5);
		float num5 = 0f;
		for (int i = 0; i < num4; i++)
		{
			float rotation = UnityEngine.Random.Range(-15f, 15f);
			num5 += this.CreateStackedBook(center, num5, rotation, this.DoubleSided && UnityEngine.Random.Range(0, 100) > 50);
		}
		return 1.1f;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002470 File Offset: 0x00000670
	private float CreateStackedBook(Vector3 center, float height, float rotation, bool invert)
	{
		int bookIndex = this.GetBookIndex();
		float num = UnityEngine.Random.Range(0.7f, 0.8f);
		float num2 = UnityEngine.Random.Range(0.15f, 0.225f);
		BMesh.Vertex vertex = this.mesh.AddVertex(this.RotateAroundY(center, new Vector3(-num / 2f, height, 0f), rotation));
		BMesh.Vertex vertex2 = this.mesh.AddVertex(this.RotateAroundY(center, new Vector3(num / 2f, height, 0f), rotation));
		BMesh.Vertex vertex3 = this.mesh.AddVertex(this.RotateAroundY(center, new Vector3(num / 2f, height + num2, 0f), rotation));
		BMesh.Vertex vertex4 = this.mesh.AddVertex(this.RotateAroundY(center, new Vector3(-num / 2f, height + num2, 0f), rotation));
		this.mesh.AddFace(vertex, vertex2, vertex3, vertex4);
		float num3 = UnityEngine.Random.Range(0.4f, 0.6f);
		this.StackedCovers(center + new Vector3(0f, height, 0f), num, num2, rotation, num3, bookIndex, invert);
		this.StackedSides(center + new Vector3(0f, height, 0f), num, num2, rotation, num3);
		float num4 = this.OS((float)bookIndex);
		if (this.DoubleSided)
		{
			BMesh.Vertex vertex5 = this.mesh.AddVertex(this.RotateAroundY(center, new Vector3(-num / 2f, height, num3), rotation));
			BMesh.Vertex vertex6 = this.mesh.AddVertex(this.RotateAroundY(center, new Vector3(num / 2f, height, num3), rotation));
			BMesh.Vertex vertex7 = this.mesh.AddVertex(this.RotateAroundY(center, new Vector3(num / 2f, height + num2, num3), rotation));
			BMesh.Vertex vertex8 = this.mesh.AddVertex(this.RotateAroundY(center, new Vector3(-num / 2f, height + num2, num3), rotation));
			this.mesh.AddFace(vertex8, vertex7, vertex6, vertex5);
			if (invert)
			{
				vertex6.attributes["uv"] = new BMesh.FloatAttributeValue(num4, 0.625f);
				vertex7.attributes["uv"] = new BMesh.FloatAttributeValue(num4 + 0.0625f, 0.625f);
				vertex8.attributes["uv"] = new BMesh.FloatAttributeValue(num4 + 0.0625f, 1f);
				vertex5.attributes["uv"] = new BMesh.FloatAttributeValue(num4, 1f);
			}
			else
			{
				vertex5.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
				vertex8.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
				vertex7.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
				vertex6.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
			}
		}
		if (UnityEngine.Random.Range(0, 100) > 50)
		{
			vertex.attributes["uv"] = new BMesh.FloatAttributeValue(num4, 0.625f);
			vertex4.attributes["uv"] = new BMesh.FloatAttributeValue(num4 + 0.0625f, 0.625f);
			vertex3.attributes["uv"] = new BMesh.FloatAttributeValue(num4 + 0.0625f, 1f);
			vertex2.attributes["uv"] = new BMesh.FloatAttributeValue(num4, 1f);
		}
		else
		{
			vertex3.attributes["uv"] = new BMesh.FloatAttributeValue(num4, 0.625f);
			vertex2.attributes["uv"] = new BMesh.FloatAttributeValue(num4 + 0.0625f, 0.625f);
			vertex.attributes["uv"] = new BMesh.FloatAttributeValue(num4 + 0.0625f, 1f);
			vertex4.attributes["uv"] = new BMesh.FloatAttributeValue(num4, 1f);
		}
		return num2;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002898 File Offset: 0x00000A98
	private void StackedCovers(Vector3 spnCenter, float width, float height, float rotation, float depth, int index, bool invert)
	{
		BMesh.Vertex vertex = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(-width / 2f, height, 0f), rotation));
		BMesh.Vertex vertex2 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(width / 2f, height, 0f), rotation));
		BMesh.Vertex vertex3 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(width / 2f, height, depth), rotation));
		BMesh.Vertex vertex4 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(-width / 2f, height, depth), rotation));
		this.mesh.AddFace(vertex, vertex2, vertex3, vertex4);
		this.CapUVs(vertex4, vertex, vertex2, vertex3, index, invert);
		BMesh.Vertex vertex5 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(-width / 2f, 0f, 0f), rotation));
		BMesh.Vertex vertex6 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(width / 2f, 0f, 0f), rotation));
		BMesh.Vertex vertex7 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(width / 2f, 0f, depth), rotation));
		BMesh.Vertex vertex8 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(-width / 2f, 0f, depth), rotation));
		this.mesh.AddFace(vertex8, vertex7, vertex6, vertex5);
		this.CapUVs(vertex8, vertex5, vertex6, vertex7, index, invert);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002A30 File Offset: 0x00000C30
	private void StackedSides(Vector3 spnCenter, float width, float height, float r, float depth)
	{
		BMesh.Vertex vertex = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(-width / 2f, 0f, 0f), r));
		BMesh.Vertex vertex2 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(-width / 2f, height, 0f), r));
		BMesh.Vertex vertex3 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(-width / 2f, height, depth), r));
		BMesh.Vertex vertex4 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(-width / 2f, 0f, depth), r));
		this.mesh.AddFace(vertex, vertex2, vertex3, vertex4);
		vertex.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
		vertex4.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
		vertex3.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
		vertex2.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
		BMesh.Vertex vertex5 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(width / 2f, 0f, 0f), r));
		BMesh.Vertex vertex6 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(width / 2f, height, 0f), r));
		BMesh.Vertex vertex7 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(width / 2f, height, depth), r));
		BMesh.Vertex vertex8 = this.mesh.AddVertex(this.RotateAroundY(spnCenter, new Vector3(width / 2f, 0f, depth), r));
		this.mesh.AddFace(vertex8, vertex7, vertex6, vertex5);
		vertex5.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
		vertex8.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
		vertex7.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
		vertex6.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002CBC File Offset: 0x00000EBC
	private Vector3 RotateAroundY(Vector3 pivot, Vector3 offset, float rotDeg)
	{
		float f = rotDeg * 0.017453292f;
		float num = Mathf.Cos(f);
		float num2 = Mathf.Sin(f);
		Vector3 vector = offset;
		float x = vector.x * num - vector.z * num2;
		float z = vector.x * num2 + vector.z * num;
		vector.x = x;
		vector.z = z;
		return vector + pivot;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002D1C File Offset: 0x00000F1C
	public float AngledBookGroup(Vector3 startPt, float offset, bool invert)
	{
		float num = UnityEngine.Random.Range(0.14f, 0.3f);
		float height = UnityEngine.Random.Range(0.8f, 0.9f);
		float num2 = UnityEngine.Random.Range(7f, 33f) * (float)((UnityEngine.Random.Range(0, 100) > 50) ? -1 : 1);
		float num3 = UnityEngine.Random.Range(0.1f, 0.2f);
		float num4 = UnityEngine.Random.Range(0.55f, 0.75f);
		float num5 = num3 + Mathf.Abs(num4 * Mathf.Sin(num2 * 0.017453292f));
		if (num2 < 0f)
		{
			this.CreateAngledBook(startPt, offset, num3, num4, num2, invert);
			offset += ((offset < 0f) ? (-num5) : num5);
			this.AddBook(startPt, (offset < 0f) ? (offset - num) : offset, num, height, invert);
		}
		else
		{
			this.AddBook(startPt, (offset < 0f) ? (offset - num) : offset, num, height, invert);
			offset += ((offset < 0f) ? (-num) : num);
			this.CreateAngledBook(startPt, offset, num3, num4, num2, invert);
		}
		return num + num5;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002E20 File Offset: 0x00001020
	private void CreateAngledBook(Vector3 startPt, float offset, float width, float height, float angle, bool invert)
	{
		float num = (offset < 0f) ? (offset - width / 2f) : (offset + width / 2f);
		float num2 = this.Curvature * Mathf.Pow(num / this.ShelfWidth, 2f);
		num2 += UnityEngine.Random.Range(0f, 0.1f);
		if (offset < 0f)
		{
			angle *= -1f;
		}
		Vector3 vector = startPt + new Vector3(num, height / 2f, num2);
		float num3 = angle * 0.017453292f;
		float num4 = width / 4f * Mathf.Abs(Mathf.Sin(num3 / 2f));
		Vector3 vector2 = Vector3.right * Mathf.Abs(height * Mathf.Sin(angle * 0.017453292f)) * (float)((offset < 0f) ? -1 : 1) * 0.5f;
		BMesh.Vertex vertex = this.mesh.AddVertex(this.RotateAroundZ(vector, new Vector3(-width / 2f, -height / 2f, 0f), angle, num4) + vector2);
		BMesh.Vertex vertex2 = this.mesh.AddVertex(this.RotateAroundZ(vector, new Vector3(width / 2f, -height / 2f, 0f), angle, num4) + vector2);
		BMesh.Vertex vertex3 = this.mesh.AddVertex(this.RotateAroundZ(vector, new Vector3(width / 2f, height / 2f, 0f), angle, num4) + vector2);
		BMesh.Vertex vertex4 = this.mesh.AddVertex(this.RotateAroundZ(vector, new Vector3(-width / 2f, height / 2f, 0f), angle, num4) + vector2);
		this.mesh.AddFace(vertex, vertex2, vertex3, vertex4);
		int bookIndex = this.GetBookIndex();
		float num5 = UnityEngine.Random.Range(0.4f, 0.6f);
		this.AngledCovers(vector, width, height, angle, vector2, num4, num5, bookIndex, invert);
		this.AngledPages(vector, width, height, angle, vector2, num4, num5);
		float num6 = this.OS((float)bookIndex);
		if (this.DoubleSided)
		{
			BMesh.Vertex vertex5 = this.mesh.AddVertex(this.RotateAroundZ(vector, new Vector3(-width / 2f, -height / 2f, num5), angle, num4) + vector2);
			BMesh.Vertex vertex6 = this.mesh.AddVertex(this.RotateAroundZ(vector, new Vector3(width / 2f, -height / 2f, num5), angle, num4) + vector2);
			BMesh.Vertex vertex7 = this.mesh.AddVertex(this.RotateAroundZ(vector, new Vector3(width / 2f, height / 2f, num5), angle, num4) + vector2);
			BMesh.Vertex vertex8 = this.mesh.AddVertex(this.RotateAroundZ(vector, new Vector3(-width / 2f, height / 2f, num5), angle, num4) + vector2);
			this.mesh.AddFace(vertex8, vertex7, vertex6, vertex5);
			if (invert)
			{
				vertex5.attributes["uv"] = new BMesh.FloatAttributeValue(num6, 0.625f);
				vertex6.attributes["uv"] = new BMesh.FloatAttributeValue(num6 + 0.0625f, 0.625f);
				vertex7.attributes["uv"] = new BMesh.FloatAttributeValue(num6 + 0.0625f, 1f);
				vertex8.attributes["uv"] = new BMesh.FloatAttributeValue(num6, 1f);
			}
			else
			{
				vertex5.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
				vertex8.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
				vertex7.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
				vertex6.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
			}
		}
		if (!invert)
		{
			vertex.attributes["uv"] = new BMesh.FloatAttributeValue(num6, 0.625f);
			vertex2.attributes["uv"] = new BMesh.FloatAttributeValue(num6 + 0.0625f, 0.625f);
			vertex3.attributes["uv"] = new BMesh.FloatAttributeValue(num6 + 0.0625f, 1f);
			vertex4.attributes["uv"] = new BMesh.FloatAttributeValue(num6, 1f);
			return;
		}
		vertex.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
		vertex4.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
		vertex3.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
		vertex2.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00003368 File Offset: 0x00001568
	private Vector3 RotateAroundZ(Vector3 pivot, Vector3 localOffset, float angle, float yOffset)
	{
		float f = angle * 0.017453292f;
		float num = Mathf.Cos(f);
		float num2 = Mathf.Sin(f);
		Vector3 vector = localOffset;
		float x = vector.x * num - vector.y * num2;
		float num3 = vector.x * num2 + vector.y * num;
		vector.x = x;
		vector.y = num3 + yOffset;
		return pivot + vector;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000033CC File Offset: 0x000015CC
	private void AngledCovers(Vector3 spnCenter, float width, float height, float rotation, Vector3 xOffset, float yOffset, float depth, int index, bool invert)
	{
		BMesh.Vertex vertex = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(width / 2f, height / 2f, 0f), rotation, yOffset) + xOffset);
		BMesh.Vertex vertex2 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(width / 2f, -height / 2f, 0f), rotation, yOffset) + xOffset);
		BMesh.Vertex vertex3 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(width / 2f, -height / 2f, depth), rotation, yOffset) + xOffset);
		BMesh.Vertex vertex4 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(width / 2f, height / 2f, depth), rotation, yOffset) + xOffset);
		this.mesh.AddFace(vertex, vertex2, vertex3, vertex4);
		this.CapUVs(vertex4, vertex, vertex2, vertex3, index, invert);
		BMesh.Vertex vertex5 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(-width / 2f, height / 2f, 0f), rotation, yOffset) + xOffset);
		BMesh.Vertex vertex6 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(-width / 2f, -height / 2f, 0f), rotation, yOffset) + xOffset);
		BMesh.Vertex vertex7 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(-width / 2f, -height / 2f, depth), rotation, yOffset) + xOffset);
		BMesh.Vertex vertex8 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(-width / 2f, height / 2f, depth), rotation, yOffset) + xOffset);
		this.mesh.AddFace(vertex8, vertex7, vertex6, vertex5);
		this.CapUVs(vertex8, vertex5, vertex6, vertex7, index, invert);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000035D0 File Offset: 0x000017D0
	private void AngledPages(Vector3 spnCenter, float width, float height, float r, Vector3 xOffset, float y, float depth)
	{
		BMesh.Vertex vertex = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(-width / 2f, height / 2f, 0f), r, y) + xOffset);
		BMesh.Vertex vertex2 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(width / 2f, height / 2f, 0f), r, y) + xOffset);
		BMesh.Vertex vertex3 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(width / 2f, height / 2f, depth), r, y) + xOffset);
		BMesh.Vertex vertex4 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(-width / 2f, height / 2f, depth), r, y) + xOffset);
		this.mesh.AddFace(vertex, vertex2, vertex3, vertex4);
		vertex.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
		vertex4.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
		vertex3.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
		vertex2.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
		BMesh.Vertex vertex5 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(-width / 2f, -height / 2f, 0f), r, y) + xOffset);
		BMesh.Vertex vertex6 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(width / 2f, -height / 2f, 0f), r, y) + xOffset);
		BMesh.Vertex vertex7 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(width / 2f, -height / 2f, depth), r, y) + xOffset);
		BMesh.Vertex vertex8 = this.mesh.AddVertex(this.RotateAroundZ(spnCenter, new Vector3(-width / 2f, -height / 2f, depth), r, y) + xOffset);
		this.mesh.AddFace(vertex8, vertex7, vertex6, vertex5);
		vertex8.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
		vertex5.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
		vertex6.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
		vertex7.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000038C8 File Offset: 0x00001AC8
	private void AddBook(Vector3 startPt, float offset, float width, float height, bool invert)
	{
		this.BookCount++;
		float num = this.Curvature * Mathf.Pow(offset / this.ShelfWidth, 2f);
		num += UnityEngine.Random.Range(0f, 0.1f);
		BMesh.Vertex vertex = this.mesh.AddVertex(startPt + new Vector3(offset, 0f, num));
		BMesh.Vertex vertex2 = this.mesh.AddVertex(startPt + new Vector3(offset + width, 0f, num));
		BMesh.Vertex vertex3 = this.mesh.AddVertex(startPt + new Vector3(offset + width, height, num));
		BMesh.Vertex vertex4 = this.mesh.AddVertex(startPt + new Vector3(offset, height, num));
		this.mesh.AddFace(vertex, vertex2, vertex3, vertex4);
		int bookIndex = this.GetBookIndex();
		float num2 = UnityEngine.Random.Range(0.4f, 0.6f);
		this.AddTop(startPt + new Vector3(offset, height, num), width, num2, invert);
		this.AddLeftCap(startPt + new Vector3(offset, 0f, num), height, num2, bookIndex, invert);
		this.AddRightCap(startPt + new Vector3(offset + width, 0f, num), height, num2, bookIndex, invert);
		float num3 = this.OS((float)bookIndex);
		if (this.DoubleSided)
		{
			BMesh.Vertex vertex5 = this.mesh.AddVertex(startPt + new Vector3(offset, 0f, num + num2));
			BMesh.Vertex vertex6 = this.mesh.AddVertex(startPt + new Vector3(offset + width, 0f, num + num2));
			BMesh.Vertex vertex7 = this.mesh.AddVertex(startPt + new Vector3(offset + width, height, num + num2));
			BMesh.Vertex vertex8 = this.mesh.AddVertex(startPt + new Vector3(offset, height, num + num2));
			this.mesh.AddFace(vertex8, vertex7, vertex6, vertex5);
			if (invert)
			{
				vertex5.attributes["uv"] = new BMesh.FloatAttributeValue(num3, 0.625f);
				vertex6.attributes["uv"] = new BMesh.FloatAttributeValue(num3 + 0.0625f, 0.625f);
				vertex7.attributes["uv"] = new BMesh.FloatAttributeValue(num3 + 0.0625f, 1f);
				vertex8.attributes["uv"] = new BMesh.FloatAttributeValue(num3, 1f);
			}
			else
			{
				vertex8.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
				vertex7.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
				vertex6.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
				vertex5.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
			}
		}
		if (invert)
		{
			vertex.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
			vertex4.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
			vertex3.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
			vertex2.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
			return;
		}
		vertex.attributes["uv"] = new BMesh.FloatAttributeValue(num3, 0.625f);
		vertex2.attributes["uv"] = new BMesh.FloatAttributeValue(num3 + 0.0625f, 0.625f);
		vertex3.attributes["uv"] = new BMesh.FloatAttributeValue(num3 + 0.0625f, 1f);
		vertex4.attributes["uv"] = new BMesh.FloatAttributeValue(num3, 1f);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00003CE4 File Offset: 0x00001EE4
	private void AddTop(Vector3 start, float width, float depth, bool invert)
	{
		BMesh.Vertex vertex = this.mesh.AddVertex(start);
		BMesh.Vertex vertex2 = this.mesh.AddVertex(start + new Vector3(width, 0f, 0f));
		BMesh.Vertex vertex3 = this.mesh.AddVertex(start + new Vector3(width, 0f, depth));
		BMesh.Vertex vertex4 = this.mesh.AddVertex(start + new Vector3(0f, 0f, depth));
		this.mesh.AddFace(vertex, vertex2, vertex3, vertex4);
		vertex.attributes["uv"] = new BMesh.FloatAttributeValue(0f, 0f);
		vertex4.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, 0f);
		vertex3.attributes["uv"] = new BMesh.FloatAttributeValue(0.5f, this.OS(2f));
		vertex2.attributes["uv"] = new BMesh.FloatAttributeValue(0f, this.OS(2f));
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00003DF8 File Offset: 0x00001FF8
	private void AddLeftCap(Vector3 start, float height, float depth, int index, bool invert)
	{
		BMesh.Vertex vertex = this.mesh.AddVertex(start);
		BMesh.Vertex vertex2 = this.mesh.AddVertex(start + new Vector3(0f, height, 0f));
		BMesh.Vertex vertex3 = this.mesh.AddVertex(start + new Vector3(0f, height, depth));
		BMesh.Vertex vertex4 = this.mesh.AddVertex(start + new Vector3(0f, 0f, depth));
		this.mesh.AddFace(vertex4, vertex, vertex2, vertex3);
		this.CapUVs(vertex4, vertex, vertex2, vertex3, index, invert);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00003E94 File Offset: 0x00002094
	private void AddRightCap(Vector3 start, float height, float depth, int index, bool invert)
	{
		BMesh.Vertex vertex = this.mesh.AddVertex(start);
		BMesh.Vertex vertex2 = this.mesh.AddVertex(start + new Vector3(0f, height, 0f));
		BMesh.Vertex vertex3 = this.mesh.AddVertex(start + new Vector3(0f, height, depth));
		BMesh.Vertex vertex4 = this.mesh.AddVertex(start + new Vector3(0f, 0f, depth));
		this.mesh.AddFace(vertex, vertex4, vertex3, vertex2);
		this.CapUVs(vertex3, vertex2, vertex, vertex4, index, invert);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00003F30 File Offset: 0x00002130
	private void CapUVs(BMesh.Vertex v1, BMesh.Vertex v2, BMesh.Vertex v3, BMesh.Vertex v4, int bookIndex, bool invert)
	{
		float num = 1f - this.OS(10f);
		if (bookIndex >= 8)
		{
			num = 1f - this.OS(14f);
			bookIndex -= 8;
		}
		float num2 = (float)bookIndex * this.OS(2f);
		if (invert)
		{
			v3.attributes["uv"] = new BMesh.FloatAttributeValue(num2, num);
			v4.attributes["uv"] = new BMesh.FloatAttributeValue(num2 + this.OS(2f), num);
			v1.attributes["uv"] = new BMesh.FloatAttributeValue(num2 + this.OS(2f), num + this.OS(4f));
			v2.attributes["uv"] = new BMesh.FloatAttributeValue(num2, num + this.OS(4f));
			return;
		}
		v1.attributes["uv"] = new BMesh.FloatAttributeValue(num2, num);
		v2.attributes["uv"] = new BMesh.FloatAttributeValue(num2 + this.OS(2f), num);
		v3.attributes["uv"] = new BMesh.FloatAttributeValue(num2 + this.OS(2f), num + this.OS(4f));
		v4.attributes["uv"] = new BMesh.FloatAttributeValue(num2, num + this.OS(4f));
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000409E File Offset: 0x0000229E
	private float OS(float value)
	{
		return value / 16f;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000040A8 File Offset: 0x000022A8
	private int GetBookIndex()
	{
		int num = UnityEngine.Random.Range(0, 16);
		if (num == this.lastIndex)
		{
			num = UnityEngine.Random.Range(0, 16);
		}
		this.lastIndex = num;
		return num;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000040D8 File Offset: 0x000022D8
	public BookMesh()
	{
	}

	// Token: 0x04000001 RID: 1
	private BMesh mesh;

	// Token: 0x04000002 RID: 2
	private BMesh lodMesh;

	// Token: 0x04000003 RID: 3
	public bool ConsistentHeight = true;

	// Token: 0x04000004 RID: 4
	public int ShelfCount = 5;

	// Token: 0x04000005 RID: 5
	public float ShelfHeight = 2.25f;

	// Token: 0x04000006 RID: 6
	public List<float> ShelfHeights = new List<float>();

	// Token: 0x04000007 RID: 7
	public float ShelfWidth = 6f;

	// Token: 0x04000008 RID: 8
	public int SpecialSpaces;

	// Token: 0x04000009 RID: 9
	public float Curvature;

	// Token: 0x0400000A RID: 10
	public bool DoubleSided;

	// Token: 0x0400000B RID: 11
	public bool GenerateLightmapUVs = true;

	// Token: 0x0400000C RID: 12
	public int BookCount;

	// Token: 0x0400000D RID: 13
	public int SpacesCreated;

	// Token: 0x0400000E RID: 14
	private int lastIndex = -1;
}
