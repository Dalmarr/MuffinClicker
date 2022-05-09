[System.Flags] // this allows us to choose which name to give the component that this enum is attached to
    public enum UpgradeType : byte // this means you can have up to 256 names in the enum, and reduces the 
                               // file size to 1 byte.
{
    Muffin,
    Milkshake,
    FancyMuffin,
}

// ANOTHER KIND OF ENUM WHERE YOU CAN HAVE ALL, SOME, OR NONE OF THE CHOICES:

//[System.Flags]
//public enum Colors : int
//{
//    Blue = (1 << 0),
//    Green = (1 << 1),
//    Red = (1 << 2),
//    Yellow = (1 << 3),
//}