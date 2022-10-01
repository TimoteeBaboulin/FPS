using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

[CustomEditor(typeof(Gun))]
[CanEditMultipleObjects]
public class GunEditor : Editor
{
    private SerializedProperty _Projectile;
    private SerializedProperty _VisualProjectile;
    
    private SerializedProperty _GunMesh;
    private SerializedProperty _GunType;
    private SerializedProperty _BurstFire;
    private SerializedProperty _ShotType;
    private SerializedProperty _ShotDelay;
    private SerializedProperty _BulletDistance;
    private SerializedProperty _MagasineSize;
    private SerializedProperty _CurrentMagasine;
    private SerializedProperty _ReloadTime;
    private SerializedProperty _PelletCount;
    private SerializedProperty _MaxAngle;

    private bool _ShowBurstFire = false;
    private bool _ShowShotgun = false;

    private void OnEnable()
    {
        _GunMesh = serializedObject.FindProperty("GunMesh");
        _GunType = serializedObject.FindProperty("GunType");
        _BurstFire = serializedObject.FindProperty("BurstFire");
        _ShotType = serializedObject.FindProperty("ShotType");
        _ShotDelay = serializedObject.FindProperty("ShotDelay");
        _BulletDistance = serializedObject.FindProperty("BulletDistance");
        _MagasineSize = serializedObject.FindProperty("MagasineSize");
        _CurrentMagasine = serializedObject.FindProperty("CurrentMagasine");
        _ReloadTime = serializedObject.FindProperty("ReloadTime");
        _PelletCount = serializedObject.FindProperty("PelletCount");
        _MaxAngle = serializedObject.FindProperty("MaxAngle");

        _Projectile = serializedObject.FindProperty("Projectile");
        _VisualProjectile = serializedObject.FindProperty("VisualProjectile");
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        //Start drawing the first properties
        EditorGUILayout.PropertyField(_GunMesh);
        EditorGUILayout.PropertyField(_GunType);
        
        //Draw the Burst Fire property only if the gun is a burst weapon
        if (_GunType.intValue == (int) GunTypeEnum.Burst)
        {
            
            _ShowBurstFire = EditorGUILayout.BeginFoldoutHeaderGroup(_ShowBurstFire, "Burst Fire");
            
            if (_ShowBurstFire)
                EditorGUILayout.PropertyField(_BurstFire, new GUIContent("Burst Count:"));
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        
        //Draw the bullet type property
        EditorGUILayout.PropertyField(_ShotType);
        
        //Draw the Shotgun shell properties only if it's a shotgun type weapon
        if (_ShotType.intValue == (int) ShotTypeEnum.ShotShell)
        {
            _ShowShotgun = EditorGUILayout.BeginFoldoutHeaderGroup(_ShowShotgun, "Shell Properties");

            if (_ShowShotgun)
            {
                EditorGUILayout.PropertyField(_PelletCount);
                EditorGUILayout.PropertyField(_MaxAngle);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        //Draw the rest
        EditorGUILayout.PropertyField(_Projectile);
        EditorGUILayout.PropertyField(_VisualProjectile);
        EditorGUILayout.PropertyField(_ShotDelay);
        EditorGUILayout.PropertyField(_BulletDistance);
        EditorGUILayout.PropertyField(_MagasineSize);
        EditorGUILayout.PropertyField(_CurrentMagasine);
        EditorGUILayout.PropertyField(_ReloadTime);
        

        serializedObject.ApplyModifiedProperties();
    }
}
