using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	Vector2 start;
	Transform shootPoint;
	bool selected = false;

	// Use this for initialization
	void Start () {
		shootPoint = GameObject.Find ("ShootPoint").transform;
	}
	
	// Update is called once per frame
	void Update () {
		// 计算世界坐标系中鼠标的位置
		// 将鼠标位置的屏幕坐标转为世界坐标
		Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// 忽略z轴
		mouse.z = 0;

		// 判断是否选中了小鸟
		if (Input.GetMouseButtonDown (0)) {
			Vector3 delta = shootPoint.position - mouse;
			if (delta.magnitude < 0.4f) {
				// Selected
				selected = true;
			}
		}

		// 拖动小鸟
		if (selected && Input.GetMouseButton (0)) {
			// 计算发射点到鼠标的偏移向量
			Vector3 delta = mouse - shootPoint.position;
			// 限定最大偏移
			if (delta.magnitude > 1.2f)
				delta = delta.normalized * 1.2f;

			// 小鸟的位置设置为发射点位置＋偏移向量
			transform.position = shootPoint.position + delta;
		}

		// 发射小鸟
		if (selected && Input.GetMouseButtonUp (0)) {
			// 计算发射向量
			Vector3 delta = shootPoint.position - transform.position;

			// 给刚体加力把小鸟发射出去
			rigidbody2D.AddForce (delta * 300f);
//			rigidbody2D.velocity = delta * 2;
			// 恢复重力
			rigidbody2D.gravityScale = 1;

			// 释放小鸟选中
			selected = false;
		}


	}

}
