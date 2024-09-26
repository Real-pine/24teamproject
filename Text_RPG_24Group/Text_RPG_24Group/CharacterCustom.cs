using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_24Group;
class CharacterCustom
{
    public int Level { get; private set; }
    public string Name { get; }
    public JobType Job { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int Hp { get; private set; }
    public int Gold { get; private set; }

    public int ExtraAtk { get; private set; }
    public int ExtraDef { get; private set; }

    private List<Item> Inventory = new List<Item>();
    private List<Item> EquipList = new List<Item>();
    
    public enum JobType
    {
        Warrior = 1,
        Mage = 2,
        Rogue = 3
    }

    public CharacterCustom(string name, int jobNumber)
    {
        Name = name;
        Level = 1;
        Gold = 1500;
        Job = (JobType)jobNumber; //jobType할당을 위함 
       
        //Job에 따른 스탯 초기화
        switch (Job)
        {
            case JobType.Warrior:
                Atk = 8;
                Def = 15;
                Hp = 150;
                break;

            case JobType.Mage:
                Atk = 15;
                Def = 8;
                Hp = 100;
                break;
            
            case JobType.Rogue:
                Atk = 12;
                Def = 12;
                Hp = 120;
                break;
        }

    }
     
    public int InventoryCount
    {
        get
        {
            return Inventory.Count;
        }
    }

    public void DisplayCharacterInfo()
    {
        Console.WriteLine($"Lv. {Level:D2}");
        Console.WriteLine($"{Name} {{ {Job} }}");
        Console.WriteLine(ExtraAtk == 0 ? $"공격력 : {Atk}" : $"공격력 : {Atk + ExtraAtk} (+{ExtraAtk})");
        Console.WriteLine(ExtraDef == 0 ? $"방어력 : {Def}" : $"방어력 : {Def + ExtraDef} (+{ExtraDef})");
        Console.WriteLine($"체력 : {Hp}");
        Console.WriteLine($"Gold : {Gold} G");
    }

    public void DisplayInventory(bool showIdx)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            Item targetItem = Inventory[i];

            string displayIdx = showIdx ? $"{i + 1} " : "";
            string displayEquipped = IsEquipped(targetItem) ? "[E]" : "";
            Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}");
        }
    }

    public void EquipItem(Item item)
    {
        if (IsEquipped(item))
        {
            EquipList.Remove(item);
            if (item.Type == 0)
                ExtraAtk -= item.Value;
            else
                ExtraDef -= item.Value;
        }
        else
        {
            EquipList.Add(item);
            if (item.Type == 0)
                ExtraAtk += item.Value;
            else
                ExtraDef += item.Value;
        }
    }

    public bool IsEquipped(Item item)
    {
        return EquipList.Contains(item);
    }

    public void BuyItem(Item item)
    {
        Gold -= item.Price;
        Inventory.Add(item);
    }

    public bool HasItem(Item item)
    {
        return Inventory.Contains(item);
    }
}