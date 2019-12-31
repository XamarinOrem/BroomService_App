﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BroomService_App.Helpers
{
    public static class FileHelper
    {
        public static string GetUniquePath(MediaFileType type, string path, string name)
        {
            string ext = Path.GetExtension(name);
            if (ext == string.Empty)
                ext = ((type == MediaFileType.Image) ? ".jpg" : ".mp4");

            name = Path.GetFileNameWithoutExtension(name);

            string nname = name + ext;
            int i = 1;
            while (File.Exists(Path.Combine(path, nname)))
                nname = name + "_" + (i++) + ext;

            return Path.Combine(path, nname);
        }


        public static string GetOutputPath(MediaFileType type, string path, string name)
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), path);
            Directory.CreateDirectory(path);

            if (string.IsNullOrWhiteSpace(name))
            {
                string timestamp = DateTime.Now.ToString("yyyMMdd_HHmmss");
                if (type == MediaFileType.Image)
                    name = "IMG_" + timestamp + ".jpg";
                else
                    name = "VID_" + timestamp + ".mp4";
            }

            return Path.Combine(path, GetUniquePath(type, path, name));
        }

        public enum MediaFileType
        {
            Image,
            Video
        }

        public class MediaFile
        {
            public string PreviewPath { get; set; }
            public string Path { get; set; }
            public MediaFileType Type { get; set; }
        }
    }
}
