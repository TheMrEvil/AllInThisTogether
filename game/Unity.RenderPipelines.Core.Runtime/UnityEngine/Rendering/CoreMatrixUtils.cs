using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A6 RID: 166
	public static class CoreMatrixUtils
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x00019C84 File Offset: 0x00017E84
		public static void MatrixTimesTranslation(ref Matrix4x4 inOutMatrix, Vector3 translation)
		{
			inOutMatrix.m03 += inOutMatrix.m00 * translation.x + inOutMatrix.m01 * translation.y + inOutMatrix.m02 * translation.z;
			inOutMatrix.m13 += inOutMatrix.m10 * translation.x + inOutMatrix.m11 * translation.y + inOutMatrix.m12 * translation.z;
			inOutMatrix.m23 += inOutMatrix.m20 * translation.x + inOutMatrix.m21 * translation.y + inOutMatrix.m22 * translation.z;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00019D2C File Offset: 0x00017F2C
		public static void TranslationTimesMatrix(ref Matrix4x4 inOutMatrix, Vector3 translation)
		{
			inOutMatrix.m00 += translation.x * inOutMatrix.m30;
			inOutMatrix.m01 += translation.x * inOutMatrix.m31;
			inOutMatrix.m02 += translation.x * inOutMatrix.m32;
			inOutMatrix.m03 += translation.x * inOutMatrix.m33;
			inOutMatrix.m10 += translation.y * inOutMatrix.m30;
			inOutMatrix.m11 += translation.y * inOutMatrix.m31;
			inOutMatrix.m12 += translation.y * inOutMatrix.m32;
			inOutMatrix.m13 += translation.y * inOutMatrix.m33;
			inOutMatrix.m20 += translation.z * inOutMatrix.m30;
			inOutMatrix.m21 += translation.z * inOutMatrix.m31;
			inOutMatrix.m22 += translation.z * inOutMatrix.m32;
			inOutMatrix.m23 += translation.z * inOutMatrix.m33;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00019E50 File Offset: 0x00018050
		public static Matrix4x4 MultiplyPerspectiveMatrix(Matrix4x4 perspective, Matrix4x4 rhs)
		{
			Matrix4x4 result;
			result.m00 = perspective.m00 * rhs.m00;
			result.m01 = perspective.m00 * rhs.m01;
			result.m02 = perspective.m00 * rhs.m02;
			result.m03 = perspective.m00 * rhs.m03;
			result.m10 = perspective.m11 * rhs.m10;
			result.m11 = perspective.m11 * rhs.m11;
			result.m12 = perspective.m11 * rhs.m12;
			result.m13 = perspective.m11 * rhs.m13;
			result.m20 = perspective.m22 * rhs.m20 + perspective.m23 * rhs.m30;
			result.m21 = perspective.m22 * rhs.m21 + perspective.m23 * rhs.m31;
			result.m22 = perspective.m22 * rhs.m22 + perspective.m23 * rhs.m32;
			result.m23 = perspective.m22 * rhs.m23 + perspective.m23 * rhs.m33;
			result.m30 = -rhs.m20;
			result.m31 = -rhs.m21;
			result.m32 = -rhs.m22;
			result.m33 = -rhs.m23;
			return result;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00019FC0 File Offset: 0x000181C0
		private static Matrix4x4 MultiplyOrthoMatrixCentered(Matrix4x4 ortho, Matrix4x4 rhs)
		{
			Matrix4x4 result;
			result.m00 = ortho.m00 * rhs.m00;
			result.m01 = ortho.m00 * rhs.m01;
			result.m02 = ortho.m00 * rhs.m02;
			result.m03 = ortho.m00 * rhs.m03;
			result.m10 = ortho.m11 * rhs.m10;
			result.m11 = ortho.m11 * rhs.m11;
			result.m12 = ortho.m11 * rhs.m12;
			result.m13 = ortho.m11 * rhs.m13;
			result.m20 = ortho.m22 * rhs.m20 + ortho.m23 * rhs.m30;
			result.m21 = ortho.m22 * rhs.m21 + ortho.m23 * rhs.m31;
			result.m22 = ortho.m22 * rhs.m22 + ortho.m23 * rhs.m32;
			result.m23 = ortho.m22 * rhs.m23 + ortho.m23 * rhs.m33;
			result.m30 = rhs.m20;
			result.m31 = rhs.m21;
			result.m32 = rhs.m22;
			result.m33 = rhs.m23;
			return result;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001A12C File Offset: 0x0001832C
		private static Matrix4x4 MultiplyGenericOrthoMatrix(Matrix4x4 ortho, Matrix4x4 rhs)
		{
			Matrix4x4 result;
			result.m00 = ortho.m00 * rhs.m00 + ortho.m03 * rhs.m30;
			result.m01 = ortho.m00 * rhs.m01 + ortho.m03 * rhs.m31;
			result.m02 = ortho.m00 * rhs.m02 + ortho.m03 * rhs.m32;
			result.m03 = ortho.m00 * rhs.m03 + ortho.m03 * rhs.m33;
			result.m10 = ortho.m11 * rhs.m10 + ortho.m13 * rhs.m30;
			result.m11 = ortho.m11 * rhs.m11 + ortho.m13 * rhs.m31;
			result.m12 = ortho.m11 * rhs.m12 + ortho.m13 * rhs.m32;
			result.m13 = ortho.m11 * rhs.m13 + ortho.m13 * rhs.m33;
			result.m20 = ortho.m22 * rhs.m20 + ortho.m23 * rhs.m30;
			result.m21 = ortho.m22 * rhs.m21 + ortho.m23 * rhs.m31;
			result.m22 = ortho.m22 * rhs.m22 + ortho.m23 * rhs.m32;
			result.m23 = ortho.m22 * rhs.m23 + ortho.m23 * rhs.m33;
			result.m30 = rhs.m20;
			result.m31 = rhs.m21;
			result.m32 = rhs.m22;
			result.m33 = rhs.m23;
			return result;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001A306 File Offset: 0x00018506
		public static Matrix4x4 MultiplyOrthoMatrix(Matrix4x4 ortho, Matrix4x4 rhs, bool centered)
		{
			if (!centered)
			{
				return CoreMatrixUtils.MultiplyOrthoMatrixCentered(ortho, rhs);
			}
			return CoreMatrixUtils.MultiplyGenericOrthoMatrix(ortho, rhs);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001A31A File Offset: 0x0001851A
		public static Matrix4x4 MultiplyProjectionMatrix(Matrix4x4 projMatrix, Matrix4x4 rhs, bool orthoCentered)
		{
			if (!orthoCentered)
			{
				return CoreMatrixUtils.MultiplyPerspectiveMatrix(projMatrix, rhs);
			}
			return CoreMatrixUtils.MultiplyOrthoMatrixCentered(projMatrix, rhs);
		}
	}
}
