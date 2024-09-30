using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
//https://pixabay.com/ko/sound-effects/ 에서 사운드 파일 가져옴
namespace Text_RPG_24Group
{
    public class SoundManager
    {
        static string UserLocation { get; set; }
        string SoundVillage { get; set; }

        string SoundButton {  get; set; }
        string SoundHurt { get; set; }
        string SoundHit { get; set; }
        string SoundSelect { get; set; }

        public SoundManager(string userLocation)
        {
            UserLocation = userLocation;//C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group
            SoundVillage = "";
            SoundButton = "";
            SoundHurt = "";
            SoundHit = "";
            SoundSelect = "";
        }
        public static void PlaySoundVillage()
        {
            // 경로 설정
            string audioFileLocation = @"C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group\Sound\1 -30-village-chronology-of-love.mp3";
            // 파일 존재 여부 확인
            if (!File.Exists(audioFileLocation))
            {
                Console.WriteLine("Sound file not found: " + audioFileLocation);
                return; // 파일이 없으면 메서드 종료
            }

            // 사운드 재생
            using (var audioFile = new AudioFileReader(audioFileLocation))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                Console.WriteLine("Playing sound...");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();  // 사운드가 끝날 때까지 대기
            }
        }
        void PlaySoundButton()
        {

        }
        void PlaySoundHurt()
        {

        }
        void PlaySoundHit()
        {

        }
    }
}
