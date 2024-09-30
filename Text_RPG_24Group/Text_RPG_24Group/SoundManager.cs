using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
//https://pixabay.com/ko/sound-effects/ 에서 사운드 파일 가져옴
namespace Text_RPG_24Group
{
    public class SoundManager
    {
        static string UserLocation { get; set; }
        public static string SoundVillage { get; set; }
        public static string SoundButton {  get; set; }
        public static string SoundHurt { get; set; }
        public static string SoundHit { get; set; }
        public static string SoundSelect { get; set; }
        static bool SoundBGMPlaying { get; set; }
        public SoundManager(string userLocation, bool soundBGMPlaying)
        {
            UserLocation = userLocation;//C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group
            SoundVillage = @"\Sound\1-30-viilage-chonology-of-love.mp3";
            SoundButton = @"\Sound\button-new-notification.mp3";
            SoundHurt = @"\Sound\hurt-punch.mp3";
            SoundHit = @"\Sound\hit-punch-6.mp3";
            SoundSelect = @"\Sound\select-system-notification.mp3";
            soundBGMPlaying = SoundBGMPlaying;
        }
        public static void StopSound()
        {
            SoundBGMPlaying = false; // 사운드 재생 상태 변경
        }

        public static async Task PlayBGM(string soundBGM, int playTime)
        {
            SoundBGMPlaying = true;
            string audioFileLocation = @$"{UserLocation}{soundBGM}";// 경로 설정
            await Task.Run(() =>
            {
                using (var audioFile = new AudioFileReader(audioFileLocation))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    Console.WriteLine("사운드 재생 중...");

                    // 사운드가 재생되는 동안 대기
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(playTime); // 상태 확인 주기
                        if (SoundBGMPlaying == false) break;
                    }
                }
            });
        }
        public static async Task PlaySoundEffect(string soundEffect)
        {
            string audioFileLocation = @$"{UserLocation}{soundEffect}";// 경로 설정
            await Task.Run(() =>
            {
                using (var audioFile = new AudioFileReader(audioFileLocation))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    Console.WriteLine("사운드 재생 중...");

                    // 사운드가 재생되는 동안 대기
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(2); // 상태 확인 주기
                    }
                }
            });
        }       
    }
}
