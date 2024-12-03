using System;
using UnityEngine;
using UnityEngine.Audio;

public class TurretScript : MonoBehaviour
{
    [SerializeField]private string playerTag = "Player";
    private GameObject player;
    [SerializeField] private Transform partToRotate;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private float shootGap = 2;
    float downLook = 0.2f;
    [SerializeField] private ScriptableObjectTemplate env_values;

    [SerializeField] private Transform firePoint;
    private float bulletSpeed;

    private BulletPoolObject bulletObjectPool;
    [SerializeField] private TurretBulletPooler bulletPooler;
    private bool isShootingOn = false;

    [SerializeField] private AudioSource shootSound;

    private void Start() {
        player = GameObject.FindGameObjectWithTag(playerTag);
        TileScript.OnEnterNewTile += EnableShooting;
        TileScript.OnExitNewTile += DisableShooting;

        bulletSpeed = env_values.bulletSpeed;
    }

    private void DisableShooting(object sender, EventArgs e) {
        isShootingOn = false;
    }

    private void EnableShooting(object sender, EventArgs e) {
        isShootingOn = true;
    }

    private void Update() {
        if (player != null) {
            LockOn();
        }
        if (shootGap <= 0 && isShootingOn) {
            shoot();
            shootGap = 2;
        }
        shootGap -= Time.deltaTime;
    }

    void LockOn() {
        Vector3 dir = player.transform.position - transform.position - new Vector3(0,downLook,0);
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rot = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rot);
    }


    private void shoot() {

        bulletObjectPool = bulletPooler.objectPool.Get();

        if (bulletObjectPool == null) {
            return;
        }

        bulletObjectPool.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);

        shootSound.Play();

        bulletObjectPool.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * bulletSpeed, ForceMode.Acceleration);

        bulletObjectPool.Deactivate();


    }

}
