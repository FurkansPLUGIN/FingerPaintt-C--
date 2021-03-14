using System;
using System.Collections.Generic;
using System.Text;

namespace fingerPaint
{
    public interface IStorage
    {
        string SaveImage(byte[] byteData);
    }
}
