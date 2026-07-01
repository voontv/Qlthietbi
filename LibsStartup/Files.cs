

using QlThietBi.Exceptions;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Net.Sockets;

namespace QlThietBi.LibsStartup
{
    public static class Files
    {
        private static string wwwroot = "wwwroot";
        private static readonly log4net.ILog log
           = log4net.LogManager.GetLogger(typeof(Files));
        
        
        public static async Task<bool> SendListFiles(List<IFormFile> iFromFiles, string maThietBi)
        {
            if(iFromFiles == null || iFromFiles.Count == 0)
            {
                throw new BadRequestException($"Không có file nào được chọn để upload ");
            }
            else
            {
                foreach (IFormFile file in iFromFiles)
                {
                    var result = await SendFiles(file, maThietBi);
                    if (!result)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static async Task<bool> SendOneFiles(IFormFile pathFileName, string maThietBi, string directoryRoot = Constants.HOSOQUANLYTHIETBI)
        {
            if (pathFileName == null || pathFileName.Length == 0)
            {
                throw new BadRequestException($"Không có file nào được chọn để upload ");
            }
            else
            {
                return await SendFiles(pathFileName, maThietBi, directoryRoot);
            }
        }

        
        public static async Task<bool> SendFiles(IFormFile pathFileName, string maThietBi, string directoryRoot = Constants.HOSOQUANLYTHIETBI)
        {
            if (!UploadToWebRoot(pathFileName, maThietBi))
            {
                return false;
            }

            using SftpClient client = new SftpClient(Constants.host, Constants.port, Constants.username, Constants.password);
            try
            {
                client.Connect();
                if (client.IsConnected)
                {
                    Console.WriteLine("da connect thanh cong");
                    using (var uplfileStream = pathFileName.OpenReadStream())
                    {
                        if (string.IsNullOrEmpty(maThietBi))
                        {
                            //if (!client.Exists(directoryRoot + "/" + maThietBi))
                            //{
                            //    client.CreateDirectory(directoryRoot + "/" + maThietBi);
                            //}
                            //client.UploadFile(uplfileStream, directoryRoot + "/" + maThietBi + "/" + pathFileName.FileName, null);
                            return false;
                        }
                        else
                        {
                            var PathTB = directoryRoot + maThietBi;

                            if (!client.Exists(directoryRoot + maThietBi))
                            {
                                client.CreateDirectory(directoryRoot + maThietBi);

                            }
                            else
                            {
                                Console.WriteLine("Thu muc da ton tai  ");

                            }

                            client.UploadFile(uplfileStream, directoryRoot + maThietBi + "/" + pathFileName.FileName, null);
                        }

                    }
                    client.Disconnect();
                }
                else
                {
                    throw new BadRequestException($"Error connecting to server ");
                }
            }
            catch (Exception e)
            {
                log.Info("################################## Lỗi tìm thấy khi upload files  " + e.Message);
                throw new BadRequestException($"Error connecting to server: {e.Message}" + " xxxxx  ");
            }
            /*when (e is SshConnectionException || e is SocketException || e is ProxyException)
            {
                throw new BadRequestException($"Error connecting to server: {e.Message}" + " xxxxx  ");
            }
            catch (SshAuthenticationException e)
            {
                throw new BadRequestException($"Failed to authenticate: {e.Message}" + " xxxxx  ");
            }
            catch (SftpPermissionDeniedException e)
            {

                throw new BadRequestException($"Operation denied by the server: {e.Message}");
            }
            catch (SshException e)
            {
                throw new BadRequestException($"Operation denied by the server: {e.Message}");
            }*/

            return true;
        }

        public static bool CheckFiles(string fileName, string maThietBi, string directoryRoot = Constants.HOSOQUANLYTHIETBI)
        {

            using SftpClient client = new SftpClient(Constants.host, Constants.port, Constants.username, Constants.password);

            try
            {
                client.Connect();
                if (client.IsConnected)
                {

                    if (!client.Exists(directoryRoot + maThietBi))
                    {
                        client.CreateDirectory(directoryRoot + maThietBi);
                    }
                    var status = client.Exists(directoryRoot + maThietBi + "/" + fileName);
                    client.Disconnect();
                    return status;
                }
            }
            catch (Exception e) when (e is SshConnectionException || e is SocketException || e is ProxyException)
            {
                throw new BadRequestException($"Error connecting to server: {e.Message}" + " xxxxx  ");
            }
            catch (SshAuthenticationException e)
            {
                throw new BadRequestException($"Failed to authenticate: {e.Message}" + " xxxxx  ");
            }
            catch (SftpPermissionDeniedException e)
            {

                throw new BadRequestException($"Operation denied by the server: {e.Message}");
            }
            catch (SshException e)
            {
                throw new BadRequestException($"Operation denied by the server: {e.Message}");
            }

            return false;
        }

        public static bool UploadToWebRoot(IFormFile file, string maThietBi)
        {
            Console.WriteLine("duong dan moi truong " + Environment.CurrentDirectory);
            string path = Path.Combine(Environment.CurrentDirectory + "/" + wwwroot + "/" + Constants.HOSOQUANLYTHIETBI, "");

            if (!string.IsNullOrEmpty(maThietBi))
            {
                path = Path.Combine(Environment.CurrentDirectory + "/" + wwwroot + "/" + Constants.HOSOQUANLYTHIETBI, maThietBi);

            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();

            string fileName = Path.GetFileName(file.FileName);
            try
            {
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
            }
            catch (Exception e)
            {
                log.Info("################################## Lỗi tìm thấy khi upload files root folder  " + e.Message);
                throw new BadRequestException($"Error connecting to server: {e.Message}" + " xxxxx  ");
            }

            return true;
        }

        /*
        public static bool UploadToWebRoot(IFormFile file, string maDuAn, string maHopDong)
        {

            string path = Path.Combine(Environment.CurrentDirectory + "/" + wwwroot + "/" + DateTime.Now.Year, "");

            if (!string.IsNullOrEmpty(maDuAn))
            {
                path = Path.Combine(Environment.CurrentDirectory + "/" + wwwroot + "/" + DateTime.Now.Year, maDuAn);

            }

            if (!string.IsNullOrEmpty(maHopDong))
            {
                path = Path.Combine(Environment.CurrentDirectory + "/" + wwwroot + "/" + DateTime.Now.Year, maDuAn + "/" + maHopDong);

            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();

            string fileName = Path.GetFileName(file.FileName);
            try
            {
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
            }
            catch (Exception e)
            {
                log.Info("################################## Lỗi tìm thấy khi upload files root folder  " + e.Message);
                throw new BadRequestException($"Error connecting to server: {e.Message}" + " xxxxx  ");
            }

            return true;
        }
        */


    }
}
