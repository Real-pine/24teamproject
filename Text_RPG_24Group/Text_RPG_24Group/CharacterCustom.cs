using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Text_RPG_24Group;

public class SaveLoadSystem
{
    private static string saveDirectory = "Saves";

    public static void SaveCharacter(CharacterCustom character, string saveName)
    {
        string filePath = Path.Combine(saveDirectory, saveName + ".json");
        string json = JsonConvert.SerializeObject(character, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine($"캐릭터 데이터를 {saveName}에 저장했습니다.");
    }

    public static CharacterCustom LoadCharacter(string saveName)
    {
        string filePath = Path.Combine(saveDirectory, saveName + ".json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CharacterCustom character = JsonConvert.DeserializeObject<CharacterCustom>(json);
            Console.WriteLine($"캐릭터 데이터를 {saveName}에서 불러왔습니다.");
            return character;
        }
        else
        {
            Console.WriteLine($"{saveName}에 저장된 데이터가 없습니다.");
            return null;
        }
    }

    public static void ListSaves()
    {
        string[] files = Directory.GetFiles(saveDirectory, "*.json");
        if (files.Length > 0)
        {
            Console.WriteLine("저장된 파일 목록:");
            foreach (string file in files)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(file));
            }
        }
        else
        {
            Console.WriteLine("저장된 파일이 없습니다.");
        }
    }

    public static void DeleteSaveFile(string saveName)
    {
        string filePath = Path.Combine(saveDirectory, saveName + ".json");
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine("파일이 성공적으로 삭제되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"파일 삭제 중 오류가 발생했습니다: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("삭제할 파일이 존재하지 않습니다.");
        }
    }
}




public class CharacterCustom
{
    public int Level { get;  set;}
    public string Name { get; set; }
    public JobType Job { get;  set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
<<<<<<< HEAD


=======
>>>>>>> (New)Dungeon30
    public int Gold { get; set; }
    public int Experience { get; set; } //현재 경험치
    public int[] ExpToNextLev = { 10, 35, 65, 100 }; //4레벨까지 요구경험치테이블

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
        Gold = 1500; //시작골드
        Job = (JobType)jobNumber;
        
        switch (Job)
        {
            case JobType.Warrior:
                Atk = 10;
                Def = 15;
                MaxHp = 140;
                break;
            case JobType.Mage:
                Atk = 15;
                Def = 8;
                MaxHp = 100;
                break;
            case JobType.Rogue:
                Atk = 12;
                Def = 12;
                MaxHp = 120;
                break;
        }
<<<<<<< HEAD
=======
        Hp = MaxHp; // 현재HP를 최대HP로 초기화
>>>>>>> (New)Dungeon30
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

    public void DisplayCharacterInfo()
    {
        Console.WriteLine($"Lv. {Level:D2}");
        Console.WriteLine($"{Name} {{ {Job} }}");
        Console.WriteLine(ExtraAtk == 0 ? $"공격력 : {Atk}" : $"공격력 : {Atk + ExtraAtk} (+{ExtraAtk})");
        Console.WriteLine(ExtraDef == 0 ? $"방어력 : {Def}" : $"방어력 : {Def + ExtraDef} (+{ExtraDef})");
        Console.WriteLine($"체력 : {Hp}/{MaxHp}");
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

    public void GetItem(Item item)
    {
        Inventory.Add(item);
    }

    public bool HasItem(Item item)
    {
        return Inventory.Contains(item);
    }
}