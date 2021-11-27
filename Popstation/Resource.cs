﻿using System.IO;
using Popstation.Pbp;

namespace Popstation
{
    /// <summary>
    /// A container for a PNG, PMF or AT3
    /// </summary>
    public class Resource
    {
        private readonly string _file;
        private byte[] _buffer;

        /// <summary>
        /// The type of data that this <see cref="Resource"/> contains
        /// </summary>
        public ResourceType ResourceType { get; }

        /// <summary>
        /// The number of bytes allocated for this <see cref="Resource"/>
        /// </summary>
        public uint Size { get; }

        /// <summary>
        /// True if the source for this <see cref="Resource"/> has been set, False if the source file does not exist, or if this <see cref="Resource"/> is empty
        /// </summary>
        public bool Exists { get; }

        public Resource(ResourceType resourceType, byte[] buffer, uint size)
        {
            ResourceType = resourceType;
            _buffer = buffer;
            Size = size;
            Exists = true;
        }

        public Resource(ResourceType resourceType, string file)
        {
            ResourceType = resourceType;
            _file = file;

            if (File.Exists(file))
            {
                var t = new FileInfo(file);
                Size = (uint)t.Length;
                Exists = true;
            }
            else
            {
                Size = 0;
            }
        }

        private Resource(ResourceType resourceType)
        {
            ResourceType = resourceType;
        }

        /// <summary>
        /// Creates an empty <see cref="Resource"/> of the specified type
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public static Resource Empty(ResourceType resourceType)
        {
            return new Resource(resourceType);
        }

        /// <summary>
        /// Writes the contents of this <see cref="Resource"/> to the specified Stream
        /// </summary>
        /// <param name="stream"></param>
        public void Write(Stream stream)
        {
            if (_file != null)
            {
                var buffer = new byte[Size];
                using (var t = new FileStream(_file, FileMode.Open, FileAccess.Read))
                {
                    t.Read(buffer, 0, (int)Size);
                    stream.Write(buffer, 0, (int)Size);
                }
            }
            else
            {
                stream.Write(_buffer, 0, (int)Size);
            }
        }

    }
}