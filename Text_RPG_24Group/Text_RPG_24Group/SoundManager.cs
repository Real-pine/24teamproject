using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
//https://pixabay.com/ko/sound-effects/ 에서 사운드 파일 가져옴
namespace Text_RPG_24Group
{
    class SoundManager
    {
        string SoundBGM { get; set; }
        string SoundButton {  get; set; }
        string SoundHurt { get; set; }
        string SoundHit { get; set; }
        
        void PlaySoundBGM(string soundFilePath) 
        {
            using (var audioFile = new AudioFileReader(soundFilePath))
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
