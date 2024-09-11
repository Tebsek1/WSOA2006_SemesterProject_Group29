using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform gunTransform;        // Gun's transform to apply recoil to
    public Transform playerCamera;        // Camera for view control
    public float recoilAmount = 1.0f;     // How much the gun kicks up
    public float recoilRecoverySpeed = 5f; // Speed of recoil recovery
    public float maxRecoil = 10f;         // Maximum upward recoil amount
    public float recoverySmoothness = 10f; // Smoothing factor for recovery
    public float fireRate = 0.1f;         // Time between shots

    private float recoilCooldown;         // Time remaining before the next shot
    private Quaternion originalGunRotation;  // Original rotation of the gun
    private Quaternion currentRecoilRotation; // Current rotation caused by recoil
    private Quaternion targetRecoilRotation;  // Target rotation for smooth recoil recovery
    private bool isShooting;              // Checks if player is shooting

    private void Start()
    {
        // Store the original rotation of the gun so it doesn't float away
        originalGunRotation = gunTransform.localRotation;
    }

    private void Update()
    {
        HandleShooting();

        // Recoil recovery (for gun rotation)
        currentRecoilRotation = Quaternion.Slerp(currentRecoilRotation, targetRecoilRotation, recoverySmoothness * Time.deltaTime);
        gunTransform.localRotation = originalGunRotation * currentRecoilRotation;

        // Reset target recoil rotation to zero for recovery over time
        if (!isShooting)
        {
            targetRecoilRotation = Quaternion.identity;
        }
    }

    private void HandleShooting()
    {
        if (Input.GetButton("Fire1") && recoilCooldown <= 0f)
        {
            Shoot();
            recoilCooldown = fireRate;
        }

        recoilCooldown -= Time.deltaTime;
    }

    private void Shoot()
    {
        isShooting = true;

        // Apply recoil upwards to the camera
        float verticalRecoil = Mathf.Clamp(recoilAmount, 0, maxRecoil);
        playerCamera.localRotation *= Quaternion.Euler(-verticalRecoil, 0f, 0f);

        // Simulate recoil effect by rotating the gun slightly backwards and upwards
        targetRecoilRotation *= Quaternion.Euler(-recoilAmount, Random.Range(-0.5f, 0.5f), 0f);

        StartCoroutine(RecoilRecovery());
    }

    private IEnumerator RecoilRecovery()
    {
        yield return new WaitForSeconds(fireRate);
        isShooting = false;
    }
}


