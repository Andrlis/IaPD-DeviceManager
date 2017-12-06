using System;

namespace DeviceManager.resources
{
    class SystemFile
    {
        public string Path { get; }
        public string Description { get; }

        public SystemFile(string Path, String Description)
        {
            this.Path = Path;
            this.Description = Description;
        }
    }
}
