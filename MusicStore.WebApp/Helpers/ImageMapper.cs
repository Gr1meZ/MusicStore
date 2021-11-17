using System.IO;
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
            //create path of image that contains of file name and file extension.
            //It will be located in database
            itemDto.ImageName = fileName + extension;
            string path = Path.Combine(wwwRoothPath + "/Image/", fileName + ".jpg");
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await itemDto.ImageFile.CopyToAsync(fileStream);
            }
            
        }
    }
}