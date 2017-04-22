using System;

namespace AdapterPattern
{
    // 1a. Create interfaces for Media Player and Advanced Media Player
    public interface IMediaPlayer
    {
        void play(String audioType, String fileName);
    }

    // 1b. Create interfaces for Media Player and Advanced Media Player
    public interface IAdvancedMediaPlayer
    {
        void playVlc(String fileName);
        void playMp4(String fileName);
    }

    // 2a. Create concrete classes implementing the AdvancedMediaPlayer interface
    public class VlcPlayer : IAdvancedMediaPlayer
    {
        public void playVlc(String fileName)
        {
            Console.WriteLine("Playing vlc file. Name: " + fileName);		
        }

        public void playMp4(String fileName)
        {
            //do nothing
        }
    }

    // 2b. Create concrete classes implementing the AdvancedMediaPlayer interface
    public class Mp4Player : IAdvancedMediaPlayer
    {
        public void playVlc(String fileName)
        {
            //do nothing
        }

        public void playMp4(String fileName)
        {
            Console.WriteLine("Playing mp4 file. Name: " + fileName);		
        }
    }

    // 3. Create adapter class implementing the MediaPlayer interface
    public class MediaAdapter : IMediaPlayer
    {
        IAdvancedMediaPlayer advancedMusicPlayer;

        public MediaAdapter(String audioType)
        {
            if(String.Equals(audioType, "vlc", StringComparison.OrdinalIgnoreCase))
            {
                advancedMusicPlayer = new VlcPlayer();
            }
            else if(String.Equals(audioType, "mp4", StringComparison.OrdinalIgnoreCase))
            {
                advancedMusicPlayer = new Mp4Player();
            }
        }

        public void play(String audioType, String fileName)
        {
            if(String.Equals(audioType, "vlc", StringComparison.OrdinalIgnoreCase))
            {
                advancedMusicPlayer.playVlc(fileName);
            }
            else if(String.Equals(audioType, "mp4", StringComparison.OrdinalIgnoreCase))
            {
                advancedMusicPlayer.playMp4(fileName);
            }
        }
    }

    // 4. Create concrete class implementing the MediaPlayer interface
    public class AudioPlayer : IMediaPlayer
    {
        MediaAdapter mediaAdapter; 

        public void play(String audioType, String fileName)
        {
            //inbuilt support to play mp3 music files
            if(String.Equals(audioType, "mp3", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Playing mp3 file. Name: " + fileName);
            } 

            //mediaAdapter is providing support to play other file formats
            else if(String.Equals(audioType, "vlc", StringComparison.OrdinalIgnoreCase) || String.Equals(audioType, "mp4", StringComparison.OrdinalIgnoreCase))
            {
                mediaAdapter = new MediaAdapter(audioType);
                mediaAdapter.play(audioType, fileName);
            }
            else
            {
                Console.WriteLine("Invalid media. " + audioType + " format not supported");
            }
        }
    }

    // 5. Use the AudioPlayer to play different types of audio formats
    public class AdapterPatternDemo
    {
        public static void Main(String[] args)
        {
            AudioPlayer audioPlayer = new AudioPlayer();

            audioPlayer.play("mp3", "beyond the horizon.mp3");
            audioPlayer.play("mp4", "alone.mp4");
            audioPlayer.play("vlc", "far far away.vlc");
            audioPlayer.play("avi", "mind me.avi");
            
            Console.ReadKey();
       }
    }
}

// 6. Verify the output
// Playing mp3 file. Name: beyond the horizon.mp3
// Playing mp4 file. Name: alone.mp4
// Playing vlc file. Name: far far away.vlc
// Invalid media. avi format not supported