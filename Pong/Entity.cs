﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * An abstract function cannot have functionality. You're basically saying, any child class MUST give their own 
 * version of this method, however it's too general to even try to implement in the parent class.
 * 
 * A virtual function, is basically saying look, here's the functionality that may or may not be good enough for 
 * the child class. So if it is good enough, use this method, if not, then override me, and provide your own functionality.
 */

namespace Pong {
    internal class Entity {
    }
}
