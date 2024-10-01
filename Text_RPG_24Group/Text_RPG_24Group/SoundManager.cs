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
        public string UserLocation { get; private set; }
        public  string SoundVillage { get; private set; }
        public  string SoundButton {  get; private set; }
        public  string SoundClear { get; private set; }
        public  string SoundDie { get; private set; }
        public  string SoundHit { get; private set; }
        public  string SoundHurt { get; private set; }
        public  string SoundSelect { get; private set; }
        public  string SoundSkill { get; private set; }

        public  bool SoundBGMPlaying { get; set; }
        public  bool SoundBGMUserSet { get; set; }
        public  bool SoundEffectUserSet { get; set; }

        public float SoundSize { get; set; }


        public SoundManager(string userLocation)
        {
            UserLocation = userLocation;//C:\Users\BaekSeungWoo\Documents\GitHub\24teamproject\Text_RPG_24Group
            SoundVillage = @"\Sound\45-village-game-music-loop.mp3";
            SoundButton = @"\Sound\button-new-notification.mp3";
            SoundClear = @"\Sound\clear-level-up.mp3";
            SoundDie = @"\Sound\die-punch.mp3";
            SoundHit = @"\Sound\hit-movement-swipe-whoosh.mp3";
            SoundHurt = @"\Sound\hurt-punch-6.mp3";
            SoundSelect = @"\Sound\select-system-notification.mp3";
            SoundSkill = @"\Sound\skill-camera-flash.mp3";
            SoundBGMUserSet = true;
            SoundEffectUserSet = true;
            SoundSize = 1f;


        }

        public async Task PlayBGM(string soundBGM,SoundManager soundManager, int playTime)
        {
            while (true)
            {
                string audioFileLocation = @$"{UserLocation}{soundBGM}";// 경로 설정
                await Task.Run(() =>
                {
                    using (var audioFile = new AudioFileReader(audioFileLocation))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Volume = soundManager.SoundSize;
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        //Console.WriteLine("사운드 재생 중...");

                        // 사운드가 재생되는 동안 대기
                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(playTime); // 상태 확인 주기
                        }
                    }
                });
            }
            
        }
        public async Task PlaySoundEffect(string soundEffect)
        {
            if(SoundEffectUserSet)
            {
                string audioFileLocation = @$"{UserLocation}{soundEffect}";// 경로 설정
                await Task.Run(() =>
                {
                    using (var audioFile = new AudioFileReader(audioFileLocation))
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Volume = this.SoundSize;
                        outputDevice.Init(audioFile);
                        outputDevice.Play();
                        //Console.WriteLine("사운드 재생 중...");

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
}
