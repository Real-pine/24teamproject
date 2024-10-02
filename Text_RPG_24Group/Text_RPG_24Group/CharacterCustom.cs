using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_24Group;






public class CharacterCustom
{
    public int Level { get;  set;}
    public string Name { get; set; }
    public JobType Job { get;  set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int MaxHp { get; set; }
    public int MaxMp { get; set; }
    public int Gold { get; set; }
    public int Experience { get; set; } //현재 경험치
    public int[] ExpToNextLev = { 10, 35, 65, 100 }; //4레벨까지 요구경험치테이블

    public int age { get; set; } // 나이
    public int act { get; set; } // 행동력

    public enum JobType //직업선택을 위해 enum으로 넘버링
    {
        Warrior = 1,
        Mage = 2,
        Rogue = 3
    }

    public int ExtraAtk { get; private set; }
    public int ExtraDef { get; private set; }

    public List<Item> Inventory = new List<Item>();
    public List<Item> EquipList = new List<Item>();

    public int InventoryCount
    {
        get
        {
            return Inventory.Count;
        }
    }
    //캐릭터 생성자
    public CharacterCustom(string name, int jobNumber)
    {
        Level = 1; // 시작레벨
        Name = name;
        Experience = 0; //시작경험치
        Gold = 10000;  //시작골드
        Job = (JobType)jobNumber;
        age = 20;
        act = 5;

        switch (Job)
        {
            case JobType.Warrior:
                Atk = 10;
                Def = 15;
                MaxHp = 140;
                MaxMp = 10;
                break;
            case JobType.Mage:
                Atk = 15;
                Def = 8;
                MaxHp = 100;
                MaxMp = 70;
                break;
            case JobType.Rogue:
                Atk = 12;
                Def = 12;
                MaxHp = 120;
                MaxMp = 55;
                break;
        }
        Hp = MaxHp; // 현재HP를 최대HP로 초기화
        Mp = MaxMp;
    }
    //경험치 획득 메서드
    public void GainExperience(int exp)
    {
        Experience += exp;
        Console.WriteLine($"경험치{exp}를 획득했습니다.");
        Console.WriteLine($"현재 경험치: {Experience}/{GetExpToNextLev()}");

        while (Experience >= GetExpToNextLev())
        {
            LevelUp();
        }
    }
    //레벨업 메서드
    public void LevelUp()
    {
        Experience -= GetExpToNextLev(); //경험치통 초기화
        Level++;
        Console.WriteLine($"레벨업! {Name}의 현재 레벨은 {Level}입니다.");

        Atk += 1;
        Def += 1;
        MaxHp += 20;
    }
    //현재 레벨에서 다음 레벨까지 필요한 경험치 
    public int GetExpToNextLev()
    {
        if (Level <= 4)
        {
            return ExpToNextLev[Level - 1];
        }
        else //5레벨 부터 요구경험치 50씩증가
        {
            return ExpToNextLev[ExpToNextLev.Length - 1] + 50 * (Level - 4);
        }
    }

    public void AgeUP() // 나이 기능 메서드
    {
        Program.GameEnd(age);

        act -= 1;

        if(act <= 0)
        {
            act = 5;
            age++;
        }
    }

    public void DisplayCharacterInfo()
    {
        Console.WriteLine($"Lv. {Level:D2}");
        Console.WriteLine($"{Name} {{ {Job} }}");
        Console.WriteLine($"공격력 : {Atk + ExtraAtk} (+{ExtraAtk})");
        Console.WriteLine($"방어력 : {Def + ExtraDef} (+{ExtraDef})");
        Console.WriteLine($"체력 : {Hp}/{MaxHp}");
        Console.WriteLine($"마나 : {Mp}/{MaxMp}");
        Console.WriteLine($"Gold : {Gold} G");
        Console.WriteLine($"나이 : {age} ({act} / 5)");
    }

    public void DisplayInventory(bool showIdx)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            Item targetItem = Inventory[i];

            string displayIdx = showIdx ? $"{i + 1} " : "";
            string displayEquipped = IsEquipped(targetItem) ? "[E]" : ""; // IsEquipped 메소드 확인
            Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}");
        }
    }
    public void UpdateEquipmentStats()
    {
        ExtraAtk = 0;
        ExtraDef = 0;

        foreach (var item in EquipList)
        {
            if (item.Type == 0)
                ExtraAtk += item.Value;
            else
                ExtraDef += item.Value;
        }
    }
    public void EquipItem(Item item)
    {
        if (IsEquipped(item))
        {
            // 이미 장착된 아이템을 선택한 경우, 장착 해제
            for (int i = 0; i < EquipList.Count; i++)
            {
                if (EquipList[i].Name == item.Name)
                {
                    EquipList.RemoveAt(i);
                    break;
                }
            }
        }
        else
        {
            // 동일한 타입의 아이템이 이미 장착되어 있으면 해제
            Item equippedItem = null;
            for (int i = 0; i < EquipList.Count; i++)
            {
                if (EquipList[i].Type == item.Type)
                {
                    equippedItem = EquipList[i];
                    break;
                }
            }

            if (equippedItem != null)
            {
                EquipList.Remove(equippedItem);
            }

            EquipList.Add(item);
        }

        UpdateEquipmentStats(); // 장착 상태가 변경되면 스탯 업데이트

    }

    public bool IsEquipped(Item item)
    {
        return EquipList.Any(e => e.Name == item.Name);
    }

    public void GetItem(Item item)
    {
        Inventory.Add(item);
    }
    public void RemoveItem(Item item)
    {
        Inventory.Remove(item);
    }
    public bool HasItem(Item item)
    {
        return Inventory.Contains(item);
    }
}