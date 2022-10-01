using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IEntity
{
    [SerializeField] private int _Health = 1000;
    [SerializeField] private GameObject _DamageTextUI;
    //[SerializeField] private GameObject HealthBar = null;
    
    public Action<int, string> OnDeath { get; set; }
    public Action<int, string> OnDamaged { get; set; }
    
    public void TakeDamage(int damage, string hitboxName)
    {
        _Health -= damage;
        var textObj = Instantiate(_DamageTextUI, transform.position, transform.rotation);
        textObj.GetComponent<TextMeshPro>().text = damage.ToString();

        if (OnDamaged == null)
            return;
        OnDamaged.Invoke(damage, hitboxName);

        if (_Health > 0)
            return;
        Destroy(gameObject);
        if (OnDeath != null)
            OnDeath.Invoke(damage, hitboxName);
    }

    public void SubToDeathEvent(Action<int, string> function)
    {
        OnDeath += function;
    }

    public void SubToDamageEvent(Action<int, string> function)
    {
        OnDamaged += function;
    }
    
    
}
