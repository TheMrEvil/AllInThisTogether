using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003AF RID: 943
	public static class VectorMath
	{
		// Token: 0x06001F3F RID: 7999 RVA: 0x000BB1F8 File Offset: 0x000B93F8
		public static float GetAngle(Vector3 _vector1, Vector3 _vector2, Vector3 _planeNormal)
		{
			float num = Vector3.Angle(_vector1, _vector2);
			float num2 = Mathf.Sign(Vector3.Dot(_planeNormal, Vector3.Cross(_vector1, _vector2)));
			return num * num2;
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x000BB221 File Offset: 0x000B9421
		public static float GetDotProduct(Vector3 _vector, Vector3 _direction)
		{
			if (_direction.sqrMagnitude != 1f)
			{
				_direction.Normalize();
			}
			return Vector3.Dot(_vector, _direction);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000BB240 File Offset: 0x000B9440
		public static Vector3 RemoveDotVector(Vector3 _vector, Vector3 _direction)
		{
			if (_direction.sqrMagnitude != 1f)
			{
				_direction.Normalize();
			}
			float d = Vector3.Dot(_vector, _direction);
			_vector -= _direction * d;
			return _vector;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x000BB27C File Offset: 0x000B947C
		public static Vector3 ExtractDotVector(Vector3 _vector, Vector3 _direction)
		{
			if (_direction.sqrMagnitude != 1f)
			{
				_direction.Normalize();
			}
			float d = Vector3.Dot(_vector, _direction);
			return _direction * d;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x000BB2AD File Offset: 0x000B94AD
		public static Vector3 RotateVectorOntoPlane(Vector3 _vector, Vector3 _planeNormal, Vector3 _upDirection)
		{
			_vector = Quaternion.FromToRotation(_upDirection, _planeNormal) * _vector;
			return _vector;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x000BB2C0 File Offset: 0x000B94C0
		public static Vector3 ProjectPointOntoLine(Vector3 _lineStartPosition, Vector3 _lineDirection, Vector3 _point)
		{
			float d = Vector3.Dot(_point - _lineStartPosition, _lineDirection);
			return _lineStartPosition + _lineDirection * d;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x000BB2E8 File Offset: 0x000B94E8
		public static Vector3 IncrementVectorTowardTargetVector(Vector3 _currentVector, float _speed, float _deltaTime, Vector3 _targetVector)
		{
			return Vector3.MoveTowards(_currentVector, _targetVector, _speed * _deltaTime);
		}
	}
}
