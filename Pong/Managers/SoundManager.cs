using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Managers
{
    /*
     * Game mechanic idea:
     * - increase sound pitch (on hit) when ball speed increases
     * - every 15 hits : paddle could shrink
     * 
     */
    public class SoundManager
    {
        private Dictionary<string, SoundEffect> _sounds = new();

        public float MasterVolume { get; set; } = 1.0f;

        public SoundManager()
        {
        }

        public void LoadSound(string key, SoundEffect sound)
        {
            if (!_sounds.ContainsKey(key))
            {
                _sounds.Add(key, sound);
            }
        }

        public void Play(string key)
        {
            if (_sounds.TryGetValue(key, out var sound))
            {
                sound.Play(MasterVolume, 0f, 0f);
            }
        }

        public void Play(string key, float volume, float pitch = 0f, float pan = 0f)
        {
            if (_sounds.TryGetValue(key, out var sound))
            {
                sound.Play(volume * MasterVolume, pitch, pan);
            }
        }
    }
}
