using System;
using UnityEngine;

namespace MiniTools.BetterGizmos
{
	// Token: 0x02000167 RID: 359
	public static class BetterGizmos
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x0005D1DC File Offset: 0x0005B3DC
		static BetterGizmos()
		{
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0005D1DE File Offset: 0x0005B3DE
		public static void DrawArrow(Color color, Vector3 startPoint, Vector3 endPoint, Vector3 normal, float arrowSize, BetterGizmos.DownsizeDisplay downsizeDisplay = BetterGizmos.DownsizeDisplay.Squash, BetterGizmos.UpsizeDisplay upsizeDisplay = BetterGizmos.UpsizeDisplay.Offset)
		{
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0005D1E0 File Offset: 0x0005B3E0
		public static void DrawViewFacingArrow(Color color, Vector3 startPoint, Vector3 endPoint, float arrowSize, BetterGizmos.DownsizeDisplay downsizeDisplay = BetterGizmos.DownsizeDisplay.Squash, BetterGizmos.UpsizeDisplay upsizeDisplay = BetterGizmos.UpsizeDisplay.Offset)
		{
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0005D1E2 File Offset: 0x0005B3E2
		public static void DrawJoystickAxis(Color color, Color deadZoneColor, float displaySize, Vector3 position, Vector2 joystickAxis, float deadZoneThreshold, float forwardAngle = 0f)
		{
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0005D1E4 File Offset: 0x0005B3E4
		public static void DrawJoystickAxis(Color color, Color deadZoneColor, float displaySize, Vector3 position, Vector2 joystickAxis, float deadZoneThreshold, Vector3 normal, Vector3 forward)
		{
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0005D1E6 File Offset: 0x0005B3E6
		public static void DrawJoystickAxis(Color color, Color deadZoneColor, float displaySize, Vector2 joystickAxis, float deadZoneThreshold, Transform transform)
		{
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0005D1E8 File Offset: 0x0005B3E8
		public static void DrawJoystickAxis(Color color, Color deadZoneColor, float displaySize, Vector3 position, Vector2 joystickAxis, float deadZoneThreshold, Quaternion rotation)
		{
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0005D1EA File Offset: 0x0005B3EA
		public static void DrawBox(Color color, Vector3 center, Quaternion rotation, Vector3 size)
		{
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0005D1EC File Offset: 0x0005B3EC
		public static void DrawBox(Color color, BoxCollider boxCollider, Vector3 positionOffset = default(Vector3))
		{
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0005D1EE File Offset: 0x0005B3EE
		public static bool Boxcast(Color defaultColor, Color hitColor, float displaySize, Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation = default(Quaternion), float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return false;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0005D1F1 File Offset: 0x0005B3F1
		public static bool Boxcast(Color defaultColor, Color hitColor, float displaySize, BoxCollider boxCollider, Vector3 direction, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return false;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0005D1F4 File Offset: 0x0005B3F4
		public static bool Boxcast(Color defaultColor, Color hitColor, float displaySize, Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation = default(Quaternion), float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			return false;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0005D1FF File Offset: 0x0005B3FF
		public static bool Boxcast(Color defaultColor, Color hitColor, float displaySize, BoxCollider boxCollider, Vector3 direction, out RaycastHit hitInfo, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			return false;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0005D20A File Offset: 0x0005B40A
		public static void DrawSphere(Color color, Vector3 position, float radius)
		{
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0005D20C File Offset: 0x0005B40C
		public static void DrawSphere(Color color, SphereCollider sphereCollider, Vector3 positionOffset = default(Vector3))
		{
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0005D210 File Offset: 0x0005B410
		public static bool Spherecast(Color defaultColor, Color hitColor, float displaySize, Vector3 origin, float radius, Vector3 direction, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			RaycastHit raycastHit;
			return BetterGizmos.Spherecast(defaultColor, hitColor, displaySize, origin, radius, direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0005D234 File Offset: 0x0005B434
		public static bool Spherecast(Color defaultColor, Color hitColor, float displaySize, SphereCollider sphereCollider, Vector3 direction, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			RaycastHit raycastHit;
			return BetterGizmos.Spherecast(defaultColor, hitColor, displaySize, sphereCollider, direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0005D254 File Offset: 0x0005B454
		public static bool Spherecast(Color defaultColor, Color hitColor, float displaySize, Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			return false;
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0005D25F File Offset: 0x0005B45F
		public static bool Spherecast(Color defaultColor, Color hitColor, float displaySize, SphereCollider sphereCollider, Vector3 direction, out RaycastHit hitInfo, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			return false;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0005D26A File Offset: 0x0005B46A
		public static void DrawCapsule(Color color, Vector3 point1, Vector3 point2, float radius)
		{
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0005D26C File Offset: 0x0005B46C
		public static void DrawCapsule(Color color, Vector3 center, Vector3 direction, float height, float radius)
		{
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0005D26E File Offset: 0x0005B46E
		public static void DrawCapsule(Color color, CapsuleCollider capsuleCollider, Vector3 positionOffset = default(Vector3))
		{
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0005D270 File Offset: 0x0005B470
		public static bool Capsulecast(Color defaultColor, Color hitColor, float displaySize, Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			RaycastHit raycastHit;
			return BetterGizmos.Capsulecast(defaultColor, hitColor, displaySize, point1, point2, radius, direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0005D294 File Offset: 0x0005B494
		public static bool Capsulecast(Color defaultColor, Color hitColor, float displaySize, Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			return false;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0005D2A0 File Offset: 0x0005B4A0
		public static bool Capsulecast(Color defaultColor, Color hitColor, float displaySize, Vector3 center, Vector3 capsuleDirection, float height, float radius, Vector3 direction, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			RaycastHit raycastHit;
			return BetterGizmos.Capsulecast(defaultColor, hitColor, displaySize, center, capsuleDirection, height, radius, direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0005D2C8 File Offset: 0x0005B4C8
		public static bool Capsulecast(Color defaultColor, Color hitColor, float displaySize, Vector3 center, Vector3 capsuleDirection, float height, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float d = Mathf.Max(0f, height - radius * 2f);
			return BetterGizmos.Capsulecast(defaultColor, hitColor, displaySize, center - capsuleDirection.normalized * d * 0.5f, center + capsuleDirection.normalized * d * 0.5f, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0005D33C File Offset: 0x0005B53C
		public static bool Capsulecast(Color defaultColor, Color hitColor, float displaySize, CapsuleCollider capsuleCollider, Vector3 direction, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			RaycastHit raycastHit;
			return BetterGizmos.Capsulecast(defaultColor, hitColor, displaySize, capsuleCollider, direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0005D35C File Offset: 0x0005B55C
		public static bool Capsulecast(Color defaultColor, Color hitColor, float displaySize, CapsuleCollider capsuleCollider, Vector3 direction, out RaycastHit hitInfo, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			return false;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0005D368 File Offset: 0x0005B568
		public static bool Linecast(Color defaultColor, Color hitColor, float displaySize, Vector3 start, Vector3 end, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			RaycastHit raycastHit;
			return BetterGizmos.LinecastVisualizer(defaultColor, hitColor, displaySize, start, end, out raycastHit, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0005D386 File Offset: 0x0005B586
		public static bool Linecast(Color defaultColor, Color hitColor, float displaySize, Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return BetterGizmos.LinecastVisualizer(defaultColor, hitColor, displaySize, start, end, out hitInfo, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0005D39C File Offset: 0x0005B59C
		public static bool Raycast(Color defaultColor, Color hitColor, float displaySize, Ray ray, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			RaycastHit raycastHit;
			return BetterGizmos.LinecastVisualizer(defaultColor, hitColor, displaySize, ray.origin, ray.origin + ray.direction.normalized * maxDistance, out raycastHit, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0005D3E0 File Offset: 0x0005B5E0
		public static bool Raycast(Color defaultColor, Color hitColor, float displaySize, Ray ray, out RaycastHit hitInfo, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return BetterGizmos.LinecastVisualizer(defaultColor, hitColor, displaySize, ray.origin, ray.origin + ray.direction.normalized * maxDistance, out hitInfo, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0005D424 File Offset: 0x0005B624
		public static bool Raycast(Color defaultColor, Color hitColor, float displaySize, Vector3 origin, Vector3 direction, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			RaycastHit raycastHit;
			return BetterGizmos.LinecastVisualizer(defaultColor, hitColor, displaySize, origin, origin + direction.normalized * maxDistance, out raycastHit, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0005D454 File Offset: 0x0005B654
		public static bool Raycast(Color defaultColor, Color hitColor, float displaySize, Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance = 3.4028235E+38f, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return BetterGizmos.LinecastVisualizer(defaultColor, hitColor, displaySize, origin, origin + direction.normalized * maxDistance, out hitInfo, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0005D479 File Offset: 0x0005B679
		public static bool Linecast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 start, Vector2 end, ContactFilter2D contactFilter2D)
		{
			return BetterGizmos.LinecastVisualizer2D(defaultColor, hitColor, displaySize, start, end, contactFilter2D);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0005D488 File Offset: 0x0005B688
		public static bool Linecast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 start, Vector2 end, int layerMask = -1, float minDepth = -3.4028235E+38f, float maxDepth = 3.4028235E+38f)
		{
			ContactFilter2D contactFilter = new ContactFilter2D
			{
				layerMask = layerMask,
				minDepth = minDepth,
				maxDepth = maxDepth
			};
			return BetterGizmos.LinecastVisualizer2D(defaultColor, hitColor, displaySize, start, end, contactFilter);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0005D4CB File Offset: 0x0005B6CB
		public static bool Raycast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 origin, Vector2 direction, ContactFilter2D contactFilter2D, float distance = 3.4028235E+38f)
		{
			return BetterGizmos.LinecastVisualizer2D(defaultColor, hitColor, displaySize, origin, origin + direction.normalized * distance, contactFilter2D);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0005D4EC File Offset: 0x0005B6EC
		public static bool Raycast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 origin, Vector2 direction, float distance = 3.4028235E+38f, int layerMask = -1, float minDepth = -3.4028235E+38f, float maxDepth = 3.4028235E+38f)
		{
			ContactFilter2D contactFilter = new ContactFilter2D
			{
				layerMask = layerMask,
				minDepth = minDepth,
				maxDepth = maxDepth
			};
			return BetterGizmos.LinecastVisualizer2D(defaultColor, hitColor, displaySize, origin, origin + direction.normalized * distance, contactFilter);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0005D541 File Offset: 0x0005B741
		public static void DrawBox2D(Color color, Vector3 center, Vector2 size, Quaternion rotation, float edgeRadius = 0f)
		{
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0005D543 File Offset: 0x0005B743
		public static void DrawBox2D(Color color, Vector3 center, Vector2 size, float angle, float edgeRadius = 0f)
		{
			BetterGizmos.DrawBox2D(color, center, size, Quaternion.Euler(0f, 0f, angle), edgeRadius);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0005D560 File Offset: 0x0005B760
		public static void DrawBox2D(Color color, BoxCollider2D boxCollider2D, Vector3 positionOffset = default(Vector3))
		{
			Vector3 center = boxCollider2D.transform.position + boxCollider2D.transform.rotation * BetterGizmos.MultiplyVector3(boxCollider2D.offset, boxCollider2D.transform.lossyScale) + positionOffset;
			BetterGizmos.DrawBox2D(color, center, BetterGizmos.MultiplyVector3(boxCollider2D.transform.lossyScale, boxCollider2D.size), boxCollider2D.transform.rotation, boxCollider2D.edgeRadius);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0005D5E7 File Offset: 0x0005B7E7
		public static bool Boxcast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter2D, float distance = 3.4028235E+38f)
		{
			return false;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0005D5EC File Offset: 0x0005B7EC
		public static bool Boxcast2D(Color defaultColor, Color hitColor, float displaySize, BoxCollider2D boxCollider2D, Vector2 direction, ContactFilter2D contactFilter2D, float distance = 3.4028235E+38f)
		{
			Vector2 origin = boxCollider2D.transform.position + boxCollider2D.transform.rotation * BetterGizmos.MultiplyVector3(boxCollider2D.transform.lossyScale, boxCollider2D.offset);
			Vector2 size = boxCollider2D.transform.lossyScale * boxCollider2D.size;
			return BetterGizmos.Boxcast2D(defaultColor, hitColor, displaySize, origin, size, boxCollider2D.transform.rotation.eulerAngles.z, direction, contactFilter2D, distance);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0005D680 File Offset: 0x0005B880
		public static bool Boxcast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance = 3.4028235E+38f, int layerMask = -1, float minDepth = -3.4028235E+38f, float maxDepth = 3.4028235E+38f)
		{
			ContactFilter2D contactFilter2D = new ContactFilter2D
			{
				layerMask = layerMask,
				minDepth = minDepth,
				maxDepth = maxDepth
			};
			return BetterGizmos.Boxcast2D(defaultColor, hitColor, displaySize, origin, size, angle, direction, contactFilter2D, distance);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0005D6CC File Offset: 0x0005B8CC
		public static bool Boxcast2D(Color defaultColor, Color hitColor, float displaySize, BoxCollider2D boxCollider2D, Vector2 direction, float distance = 3.4028235E+38f, int layerMask = -1, float minDepth = -3.4028235E+38f, float maxDepth = 3.4028235E+38f)
		{
			ContactFilter2D contactFilter2D = new ContactFilter2D
			{
				layerMask = layerMask,
				minDepth = minDepth,
				maxDepth = maxDepth
			};
			return BetterGizmos.Boxcast2D(defaultColor, hitColor, displaySize, boxCollider2D, direction, contactFilter2D, distance);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0005D711 File Offset: 0x0005B911
		public static void DrawCircle2D(Color color, Vector3 position, Vector3 normal, float radius)
		{
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0005D713 File Offset: 0x0005B913
		public static void DrawCircle2D(Color color, CircleCollider2D circleCollider2D, Vector2 positionOffset = default(Vector2))
		{
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0005D715 File Offset: 0x0005B915
		public static bool Circlecast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter2D, float distance = 3.4028235E+38f)
		{
			return false;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0005D718 File Offset: 0x0005B918
		public static bool Circlecast2D(Color defaultColor, Color hitColor, float displaySize, CircleCollider2D circleCollider2D, Vector2 direction, ContactFilter2D contactFilter2D, float distance = 3.4028235E+38f)
		{
			return false;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0005D71C File Offset: 0x0005B91C
		public static bool Circlecast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 origin, float radius, Vector2 direction, float distance = 3.4028235E+38f, int layerMask = -1, float minDepth = -3.4028235E+38f, float maxDepth = 3.4028235E+38f)
		{
			ContactFilter2D contactFilter2D = new ContactFilter2D
			{
				layerMask = layerMask,
				minDepth = minDepth,
				maxDepth = maxDepth
			};
			return BetterGizmos.Circlecast2D(defaultColor, hitColor, displaySize, origin, radius, direction, contactFilter2D, distance);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0005D764 File Offset: 0x0005B964
		public static bool Circlecast2D(Color defaultColor, Color hitColor, float displaySize, CircleCollider2D circleCollider2D, Vector2 direction, float distance = 3.4028235E+38f, int layerMask = -1, float minDepth = -3.4028235E+38f, float maxDepth = 3.4028235E+38f)
		{
			ContactFilter2D contactFilter2D = new ContactFilter2D
			{
				layerMask = layerMask,
				minDepth = minDepth,
				maxDepth = maxDepth
			};
			return BetterGizmos.Circlecast2D(defaultColor, hitColor, displaySize, circleCollider2D, direction, contactFilter2D, distance);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0005D7A9 File Offset: 0x0005B9A9
		public static void DrawCapsule2D(Color color, Vector3 center, Vector2 size, Quaternion rotation, CapsuleDirection2D capsuleDirection = CapsuleDirection2D.Vertical)
		{
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0005D7AB File Offset: 0x0005B9AB
		public static void DrawCapsule2D(Color color, Vector3 center, Vector2 size, float angle, CapsuleDirection2D capsuleDirection = CapsuleDirection2D.Vertical)
		{
			BetterGizmos.DrawCapsule2D(color, center, size, Quaternion.Euler(0f, 0f, angle), capsuleDirection);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0005D7C8 File Offset: 0x0005B9C8
		public static void DrawCapsule2D(Color color, CapsuleCollider2D capsuleCollider2D, Vector3 positionOffset = default(Vector3))
		{
			Vector3 center = capsuleCollider2D.transform.position + capsuleCollider2D.transform.rotation * BetterGizmos.MultiplyVector3(capsuleCollider2D.offset, capsuleCollider2D.transform.lossyScale) + positionOffset;
			BetterGizmos.DrawCapsule2D(color, center, BetterGizmos.MultiplyVector3(capsuleCollider2D.transform.lossyScale, capsuleCollider2D.size), capsuleCollider2D.transform.rotation, capsuleCollider2D.direction);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0005D84F File Offset: 0x0005BA4F
		public static bool Capsulecast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter2D, float distance = 3.4028235E+38f)
		{
			return false;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0005D854 File Offset: 0x0005BA54
		public static bool Capsulecast2D(Color defaultColor, Color hitColor, float displaySize, CapsuleCollider2D capsuleCollider2D, Vector2 direction, ContactFilter2D contactFilter2D, float distance = 3.4028235E+38f)
		{
			Vector2 origin = capsuleCollider2D.transform.position + capsuleCollider2D.transform.rotation * BetterGizmos.MultiplyVector3(capsuleCollider2D.transform.lossyScale, capsuleCollider2D.offset);
			Vector2 size = capsuleCollider2D.transform.lossyScale * capsuleCollider2D.size;
			return BetterGizmos.Capsulecast2D(defaultColor, hitColor, displaySize, origin, size, capsuleCollider2D.direction, capsuleCollider2D.transform.rotation.eulerAngles.z, direction, contactFilter2D, distance);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0005D8EC File Offset: 0x0005BAEC
		public static bool Capsulecast2D(Color defaultColor, Color hitColor, float displaySize, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance = 3.4028235E+38f, int layerMask = -1, float minDepth = -3.4028235E+38f, float maxDepth = 3.4028235E+38f)
		{
			ContactFilter2D contactFilter2D = new ContactFilter2D
			{
				layerMask = layerMask,
				minDepth = minDepth,
				maxDepth = maxDepth
			};
			return BetterGizmos.Capsulecast2D(defaultColor, hitColor, displaySize, origin, size, capsuleDirection, angle, direction, contactFilter2D, distance);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0005D938 File Offset: 0x0005BB38
		public static bool Capsulecast2D(Color defaultColor, Color hitColor, float displaySize, CapsuleCollider2D capsuleCollider2D, Vector2 direction, float distance = 3.4028235E+38f, int layerMask = -1, float minDepth = -3.4028235E+38f, float maxDepth = 3.4028235E+38f)
		{
			ContactFilter2D contactFilter2D = new ContactFilter2D
			{
				layerMask = layerMask,
				minDepth = minDepth,
				maxDepth = maxDepth
			};
			return BetterGizmos.Capsulecast2D(defaultColor, hitColor, displaySize, capsuleCollider2D, direction, contactFilter2D, distance);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0005D97D File Offset: 0x0005BB7D
		private static void UpdateAnimationParameters()
		{
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0005D97F File Offset: 0x0005BB7F
		private static bool LinecastVisualizer(Color defaultColor, Color hitColor, float displaySize, Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask = -1, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			return false;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0005D98C File Offset: 0x0005BB8C
		private static void ShapecastVisualizer(Color defaultColor, Color hitColor, out Color targetColor, float displaySize, Vector3 start, Vector3 direction, out Vector3 end, RaycastHit hitInfo, float maxDistance)
		{
			BetterGizmos.ShapecastVisualizer(defaultColor, hitColor, out targetColor, displaySize, start, direction, out end, maxDistance, hitInfo.collider, hitInfo.distance, hitInfo.normal, hitInfo.point);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0005D9CC File Offset: 0x0005BBCC
		private static void Shapecast2DVisualizer(Color defaultColor, Color hitColor, out Color targetColor, float displaySize, Vector2 start, Vector2 direction, out Vector3 end, RaycastHit2D hitInfo, float maxDistance)
		{
			BetterGizmos.ShapecastVisualizer(defaultColor, hitColor, out targetColor, displaySize, start, direction, out end, maxDistance, hitInfo.collider, hitInfo.distance, hitInfo.normal, hitInfo.point);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0005DA1F File Offset: 0x0005BC1F
		private static void ShapecastVisualizer(Color defaultColor, Color hitColor, out Color targetColor, float displaySize, Vector3 start, Vector3 direction, out Vector3 end, float maxDistance, bool hit, float hitDistance, Vector3 hitNormal, Vector3 hitPoint)
		{
			targetColor = Color.black;
			end = Vector3.back;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0005DA38 File Offset: 0x0005BC38
		private static bool LinecastVisualizer2D(Color defaultColor, Color hitColor, float displaySize, Vector2 start, Vector2 end, ContactFilter2D contactFilter)
		{
			return false;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0005DA3B File Offset: 0x0005BC3B
		private static Color MultiplyColorAlpha(Color color, float alpha)
		{
			return new Color(color.r, color.g, color.b, color.a * alpha);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0005DA5C File Offset: 0x0005BC5C
		private static bool GetConvexShape(Vector3[] pointsCloud)
		{
			return true;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0005DA5F File Offset: 0x0005BC5F
		private static void DrawAnimatedCircle(Color color, Vector3 position, Vector3 normal, float radius, bool animated = true)
		{
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0005DA61 File Offset: 0x0005BC61
		private static Vector3 MultiplyVector3(Vector3 v1, Vector3 v2)
		{
			return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0005DA8F File Offset: 0x0005BC8F
		private static void TryDrawConvexShape(Vector3[] pointsCloud, Color color, bool getConvexShape)
		{
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0005DA91 File Offset: 0x0005BC91
		private static void GetSphereDisc(Vector3 position, float radius, out Vector3 discCenter, out Vector3 discDirection, out float discRadius)
		{
			discCenter = Vector3.zero;
			discDirection = Vector3.forward;
			discRadius = 0f;
		}

		// Token: 0x02000242 RID: 578
		public enum UpsizeDisplay
		{
			// Token: 0x040010EA RID: 4330
			Offset,
			// Token: 0x040010EB RID: 4331
			Stretch
		}

		// Token: 0x02000243 RID: 579
		public enum DownsizeDisplay
		{
			// Token: 0x040010ED RID: 4333
			Squash,
			// Token: 0x040010EE RID: 4334
			Scale
		}
	}
}
