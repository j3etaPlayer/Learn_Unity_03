using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �Ҵ����� ������ transform ȸ�� ���� �Ҵ��ϸ� rotation ����
/// </summary>
public class ShakeCamera : SingletonMonoBehaviour<ShakeCamera>
{
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeIntensitry;

    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensitry = 0.1f, bool positionShake = true)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensitry = shakeIntensitry;

        if (positionShake) StartCoroutine(ShakeByPosition());
        else StartCoroutine(ShakeByRotation());
    }
    IEnumerator ShakeByPosition()
    {
        Vector3 startCameraPos = transform.position;

        while (shakeTime > 0.0f)
        {
            transform.position = startCameraPos + UnityEngine.Random.insideUnitSphere * shakeIntensitry;
            shakeTime -= Time.deltaTime;

            yield return null;
        }
    }
    IEnumerator ShakeByRotation()
    {
        Vector3 startCameraRot = transform.eulerAngles;

        float power = 10f;

        while (shakeTime > 0.0f)
        {
            // ���� ����
            float x = 0;
            float y = 0;
            float z = UnityEngine.Random.Range(-1, 1);

            // ī�޶� ������ ���� ��鸲
            transform.rotation = Quaternion.Euler(startCameraRot
                + new Vector3(x, y, z) * shakeIntensitry * power);

            // �ð�����
            shakeTime -= Time.deltaTime;
            yield return null;
        }
    }
}
