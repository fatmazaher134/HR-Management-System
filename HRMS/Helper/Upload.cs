namespace HRMS.Helper
{
    public static class Upload
    {
        public static string UploadFile (string FolderName , IFormFile File)
        {
            try
            { // Catch the folder Path and the file name in the server
                //1)Get Directory
                string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/" + FolderName;
                //2)Get FileName
                //Guid >> word contain from 36 character
                string FileName = Guid.NewGuid()+Path.GetFileName(File.FileName);
                //3)Merge path with FileName
                string FinalPath = Path.Combine(FolderPath, FileName);
                //Combine Put
                //4) save file as stream "Data OverTime"
                using (var Stream = new FileStream(FinalPath,FileMode.Create))
                {
                    File.CopyTo(Stream);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string RemoveFile(string FolderName, string fileName)
        {
            try
            {
                var directory = Path
                    .Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", FolderName,fileName);

                if (File.Exists(directory))
                {
                    File.Delete(directory);
                    return "File Deleted";
                }
                return "File Not Deleted";

            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
        }

    }
}
