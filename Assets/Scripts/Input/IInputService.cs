using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Input
{
    public interface IInputService
    {
        Action OnPaddleLeftDown { get; set; }
        Action OnPaddleLeftUp { get; set; }
        Action OnPaddleRightDown { get; set; }
        Action OnPaddleRightUp { get; set; }
    }
}
