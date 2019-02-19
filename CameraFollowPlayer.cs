using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;//ターゲットへの参照
    private Vector3 offset;//相対座標

    void Start()
    {
        offset = GetComponent<Transform>().position - target.position;//自分自身とtargetとの相対距離を求める
    }

    void Update()
    {
        GetComponent<Transform>().position = target.position + offset;// 自分自身の座標に、targetの座標に相対座標を足した値を設定する
    }
}