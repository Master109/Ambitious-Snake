﻿using UnityEngine;

namespace AmbitiousSnake
{
	[ExecuteAlways]
	public class GameCamera : CameraScript
	{
		public int followDist;
		public float smoothTime;
		public float maxSpeed;
		Vector3 nextPos;
		float posZ;
		Vector2 offset;
		Bounds viewportBounds;
		Bounds cameraBounds;
		Vector2 viewportSize;
		Vector3 target;
		Vector3 velocity;
		
		public override void Awake ()
		{
			base.Awake ();
#if UNITY_EDITOR
			if (!Application.isPlaying)
			{
				if (trs == null)
					trs = GetComponent<Transform>();
				if (camera == null)
					camera = GetComponent<Camera>();
				return;
			}
#endif
			posZ = trs.position.z;
			trs.SetParent(null);
			cameraBounds = LevelMap.GetBounds();
			trs.localScale = new Vector3(camera.aspect, 1, 1);
			viewportSize = camera.ViewportToWorldPoint(Vector2.one) - camera.ViewportToWorldPoint(Vector2.zero);
			viewportBounds = new Bounds();
			HandlePosition ();
			trs.position = target;
		}
		
		public override void HandlePosition ()
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				return;
#endif
			offset = Snake.instance.GetHeadPosition() - (Vector2) trs.position;
			if (offset.magnitude > followDist)
			{
				viewportBounds.center = (Vector3) Snake.instance.GetHeadPosition() - (Vector3) (offset.normalized * followDist);
				viewportBounds.size = viewportSize;
				Vector3 minDiff;
				Vector3 maxDiff;
				minDiff = cameraBounds.min - viewportBounds.min;
				maxDiff = viewportBounds.max - cameraBounds.max;
				if (viewportSize.x < cameraBounds.size.x)
				{
					if (minDiff.x > 0)
					{
						viewportBounds.min = new Vector3(cameraBounds.min.x, viewportBounds.min.y);
						viewportBounds.max += new Vector3(minDiff.x, 0);
					}
					if (maxDiff.x > 0)
					{
						viewportBounds.max = new Vector3(cameraBounds.max.x, viewportBounds.max.y);
						viewportBounds.min -= new Vector3(maxDiff.x, 0);
					}
				}
				else
					viewportBounds.center = new Vector3(cameraBounds.center.x, viewportBounds.center.y);
				if (viewportSize.y < cameraBounds.size.y)
				{
					if (minDiff.y > 0)
					{
						viewportBounds.min = new Vector3(viewportBounds.min.x, cameraBounds.min.y);
						viewportBounds.max += new Vector3(0, minDiff.y);
					}
					if (maxDiff.y > 0)
					{
						viewportBounds.max = new Vector3(viewportBounds.max.x, cameraBounds.max.y);
						viewportBounds.min -= new Vector3(0, maxDiff.y);
					}
				}
				else
					viewportBounds.center = new Vector3(viewportBounds.center.x, cameraBounds.center.y);
				target = viewportBounds.center + (Vector3.forward * posZ);
				trs.position = Vector3.SmoothDamp(trs.position, target, ref velocity, smoothTime, maxSpeed);
			}
		}
	}
}