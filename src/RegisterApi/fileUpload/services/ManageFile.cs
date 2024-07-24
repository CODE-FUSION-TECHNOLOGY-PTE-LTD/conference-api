namespace RegisterApi.fileUpload.services;

public class ManageFile
{
    public async Task<string> UploadFile(IFormFile _IFormFile)
    {
        string FileName = "";
        try
        {
            FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
            FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
            var _GetFilePath = CommonUpload.GetFilePath(FileName);
            using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
            {
                await _IFormFile.CopyToAsync(_FileStream);
            }
            return FileName;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
