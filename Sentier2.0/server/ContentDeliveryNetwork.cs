using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperData;
using System.IO;

namespace Sentier2._0
{
    public class ContentDeliveryNetwork
    {

        public async Task<MyFile> GetFile([FromQuery] string guid)
        {
            var file = new MyFile();
            //string p = String.Concat(@"./TestMedia/", @guid);
            var temp = Path.Join("./TestMedia", guid);
            //file.Path = Path.GetFullPath(temp);// for some reason, must collate before
            file.Path = Path.GetFullPath("./TestMedia/sequoia.jpg");// ca, ca marche..? Pas capable de mettre la variable dedans...
            file.bytes = await System.IO.File.ReadAllBytesAsync(file.Path); ;
            file.Name = Path.GetFileName(file.Path);
            file.Ext = GetMimeTypes(Path.GetExtension(file.Path));
            return file;

        }

        // Default Template for extensions.
        private static string GetMimeTypes(string ext)
        {
            switch (ext)
            {
                case ".txt": return "text/plain";
                case ".csv": return "text/csv";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".ppt": return "application/vnd.ms-powerpoint";
                case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                default: return "application/octet-stream";
            }
        }

    }

    public class MyFile {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Ext { get; set; }
        public byte[] bytes { get; set; }
    }


}

