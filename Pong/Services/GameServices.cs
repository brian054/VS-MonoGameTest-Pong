using Pong.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Services
{
    // Why?
    /*
     * Going to run into issues later where we have to change so many lines of code anytime we add a manager into the states
     * For example: stateManager.ChangeState(new PongGameState(stateManager)); will now need soundManager, then it'll need some other manager later that 
     * i can't even think of. To build this to be more extensible we'll use this GameServices class which will provide these things all bundled up.
     * 
     * TODO: Design what the end game looks like here, I want a pretty badass Pong game just a couple cool features (particles, better looking paddles, color 
     * customization, TODO-TODO: Define an actual scope so you don't just randomly work on this forever)
     * 
     */
    public class GameServices
    {
        public SoundManager soundManager { get; set; }
        public StateManager stateManager { get; set; }
        
        // ex. 
        // SettingsManager idk, SaveManager to save high scores, settings, etc, 
    }
}
