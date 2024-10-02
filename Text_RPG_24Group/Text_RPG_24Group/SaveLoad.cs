using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//SoundEffectManager.PlaySoundEffect(SoundEffectManager.SoundSelect);
namespace Text_RPG_24Group
{
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
                character.UpdateEquipmentStats(); // 장착 아이템의 스텟 업데이트
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
}
