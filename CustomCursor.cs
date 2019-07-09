using System.IO;
using System.Windows.Input;

namespace TFT_Overlay
{
    public static class CustomCursor
    {
        public static Cursor FromByteArray(byte[] array)
        {
            using (MemoryStream memoryStream = new MemoryStream(array))
            {
                return new Cursor(memoryStream);
            }
        }
    }
}
