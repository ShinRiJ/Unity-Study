using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Batlle : MonoBehaviour
{
    [SerializeField] Text _knightText;
    [SerializeField] Text _archerText;
    [SerializeField] Text _dragonText;

    [SerializeField] GameObject _resultPanel;

    Dragon mainEnemy;

    private List<Warrior> _warriorsList;


    public void Start()
    {
        mainEnemy = new Dragon(UnityEngine.Random.Range(280, 360), 30, DamageType.Range);

        _warriorsList = new List<Warrior>()
        {
            new Knight(UnityEngine.Random.Range(80, 120), 20, DamageType.Melee, 30),
            new Archer(UnityEngine.Random.Range(40, 60), 15, 0.7f),
        };

        _knightText.text = _archerText.text = _dragonText.text = "DEAD";

        foreach (var warrior in _warriorsList)
            switch (warrior)
            {
                case Knight knight:
                    _knightText.text = $"{knight.GetType()}\n{knight.GetInfo()}";
                    break;
                case Archer archer:
                    _archerText.text = $"{archer.GetType()}\n{archer.GetInfo()}";
                    break;
            }

        _dragonText.text = $"{mainEnemy.GetType()}\n{mainEnemy.GetInfo()}";
        StartCoroutine(Fight());
    }

    public void Update()
    {
        _knightText.text = _archerText.text = _dragonText.text = "DEAD";

        foreach (var warrior in _warriorsList)
            switch (warrior)
            {
                case Knight knight:
                    if(knight.IsAlive)
                        _knightText.text = $"{knight.GetType()}\n{knight.GetInfo()}";
                    break;
                case Archer archer:
                    if(archer.IsAlive)
                        _archerText.text = $"{archer.GetType()}\n{archer.GetInfo()}";
                    break;
            }

        if(mainEnemy.IsAlive)
            _dragonText.text = $"{mainEnemy.GetType()}\n{mainEnemy.GetInfo()}";
    }

    public void Restart()
    {
        mainEnemy = new Dragon(UnityEngine.Random.Range(280, 360), 30, DamageType.Range);

        _warriorsList = new List<Warrior>()
        {
            new Knight(UnityEngine.Random.Range(80, 120), 20, DamageType.Melee, 30),
            new Archer(UnityEngine.Random.Range(40, 60), 15, 0.7f),
        };

        _resultPanel.gameObject.SetActive(false);
        StartCoroutine(Fight());
    }

    public IEnumerator Fight()
    {
        while (mainEnemy.IsAlive && CheckWarriorsState(_warriorsList))
        {
            mainEnemy.Heal(30);
            yield return new WaitForSeconds(1f);

            foreach (var unit in _warriorsList)
                if(unit.IsAlive)
                    mainEnemy.TakeDamage(unit.Damage);

            yield return new WaitForSeconds(1f);

            if (mainEnemy.IsAlive)
                for (Int32 index = 0; index < _warriorsList.Count; index++)
                    if (_warriorsList[index].IsAlive)
                    {
                        _warriorsList[index].TakeDamage(mainEnemy.Damage);
                        break;
                    }
        }

        _resultPanel.gameObject.SetActive(true);


        if (mainEnemy.IsAlive)
            _resultPanel.gameObject.GetComponentInChildren<Text>().text = "Герои пали!";
        else
            _resultPanel.gameObject.GetComponentInChildren<Text>().text = "Дракон повержен!";
    }

    private Boolean CheckWarriorsState(List<Warrior> src)
    {
        foreach (var item in src)
            if (item.IsAlive)
                return true;
        return false;
    }
}

public enum DamageType
{
    Melee,
    Range,
    Magic
};

public class Warrior
{
    public Int32 Health {  get; protected set; }
    virtual public Int32 Damage {get; protected set; }
    public DamageType DmgType { get; protected set; }

    public Boolean IsAlive => Health > 0;

    public Warrior(Int32 health, Int32 damage, DamageType dmgType)
    {
        Health = health;
        Damage = damage;
        DmgType = dmgType;
    }

    virtual public void TakeDamage(Int32 damage)
    {
        Health -= damage;   
    }

    public String GetInfo() => $"HP: {Health}\nDamage: {Damage}";
}

public class Knight : Warrior
{
    private Int32 _armor;
    public Knight(Int32 health, Int32 damage, DamageType dmgType, Int32 armor) : base(health, damage, dmgType)
    {
        _armor = armor;
    }

    public override void TakeDamage(int damage)
    {
        damage -= _armor/4;
        base.TakeDamage(damage > 0 ? damage : 1);
    }
}

public class Archer : Warrior
{
    private Single _criticalChance;
    public override int Damage
    {
        get
        {
            Single chance = UnityEngine.Random.Range(0, 1);
            if(chance < _criticalChance)
                return base.Damage * 2;
            return base.Damage;
        }
        protected set => base.Damage = value;
    }

    public Archer(Int32 health, Int32 damage, Single criticalChance) : base(health, damage, DamageType.Range)
    {
        _criticalChance = criticalChance;   
    }
}

public class Dragon : Warrior
{
    public Dragon(Int32 health, Int32 damage, DamageType dmgType) : base(health, damage, dmgType)
    {

    }

    public void Heal(Int32 health)
    {
        Single chance = UnityEngine.Random.Range(0, 1);
        if (chance > 0.6)
            Health += health;
    }
}


