﻿using System.IO;
using System.Threading.Tasks;
using MusicStore.WebApp.Models;

namespace MusicStore.WebApp.Helpers
{
    public class ImageMapper
    {
        public static async Task MapImage(ItemViewModel itemDto, string wwwRoothPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(itemDto.ImageFile.FileName);
            string extension = Path.GetExtension(itemDto.ImageFile.FileName);
            itemDto.ImageName = fileName + extension;
            string path = Path.Combine(wwwRoothPath + "/Image/", fileName + ".jpg");
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await itemDto.ImageFile.CopyToAsync(fileStream);
            }
            
        }
    }
}