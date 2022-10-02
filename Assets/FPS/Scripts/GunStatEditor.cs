using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

[CustomEditor(typeof(GunStat))]
[CanEditMultipleObjects]
public class GunStatEditor : Editor
{
    private SerializedProperty _projectile;
    private SerializedProperty _visualProjectile;
    
    private SerializedProperty _gunMesh;
    private SerializedProperty _gunType;
    private SerializedProperty _burstFire;
    private SerializedProperty _shotType;
    private SerializedProperty _shotDelay;
    private SerializedProperty _range;
    private SerializedProperty _spawnRange;
    private SerializedProperty _magasineSize;
    private SerializedProperty _reloadTime;
    private SerializedProperty _pelletCount;
    private SerializedProperty _maxAngle;
    private SerializedProperty _sight;

    private bool _showBurstFire = false;
    private bool _showShotgun = false;

    private void OnEnable()
    {
        _gunMesh = serializedObject.FindProperty("_gunMesh");
        _gunType = serializedObject.FindProperty("_gunType");
        _burstFire = serializedObject.FindProperty("_burstFire");
        _shotType = serializedObject.FindProperty("_shotType");
        _shotDelay = serializedObject.FindProperty("_shotDelay");
        _range = serializedObject.FindProperty("_range");
        _spawnRange = serializedObject.FindProperty("_spawnRange");
        _magasineSize = serializedObject.FindProperty("_magasineSize");
        _reloadTime = serializedObject.FindProperty("_reloadTime");
        _pelletCount = serializedObject.FindProperty("_pelletCount");
        _maxAngle = serializedObject.FindProperty("_maxAngle");

        _projectile = serializedObject.FindProperty("_projectile");
        _visualProjectile = serializedObject.FindProperty("_visualProjectile");
        _sight = serializedObject.FindProperty("_sight");
    }

    // Update is called once per frame
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        //Start drawing the first properties
        EditorGUILayout.PropertyField(_gunMesh);
        EditorGUILayout.PropertyField(_gunType);
        
        //Draw the Burst Fire property only if the gun is a burst weapon
        if (_gunType.intValue == (int) GunTypeEnum.Burst)
        {
            
            _showBurstFire = EditorGUILayout.BeginFoldoutHeaderGroup(_showBurstFire, "Burst Fire");
            
            if (_showBurstFire)
                EditorGUILayout.PropertyField(_burstFire, new GUIContent("Burst Count:"));
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        
        //Draw the bullet type property
        EditorGUILayout.PropertyField(_shotType);
        
        //Draw the Shotgun shell properties only if it's a shotgun type weapon
        if (_shotType.intValue == (int) ShotTypeEnum.ShotShell)
        {
            _showShotgun = EditorGUILayout.BeginFoldoutHeaderGroup(_showShotgun, "Shell Properties");

            if (_showShotgun)
            {
                EditorGUILayout.PropertyField(_pelletCount);
                EditorGUILayout.PropertyField(_maxAngle);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        //Draw the rest
        EditorGUILayout.PropertyField(_projectile);
        EditorGUILayout.PropertyField(_visualProjectile);
        EditorGUILayout.PropertyField(_shotDelay);
        EditorGUILayout.PropertyField(_range);
        EditorGUILayout.PropertyField(_spawnRange);
        EditorGUILayout.PropertyField(_magasineSize);
        EditorGUILayout.PropertyField(_reloadTime);
        EditorGUILayout.PropertyField(_sight);
        

        serializedObject.ApplyModifiedProperties();
    }
}
