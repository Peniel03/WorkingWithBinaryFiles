using DZen.Security.Cryptography;
//using System.Security.Cryptography;
//using System.Security.Cryptography.HashAlgorithm;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"D:\BADIBANGA_KALUWA_PENIEL\ITRANSITION\Intership_Project\Task-2\task2";
            string email = "badibangakaluwapeniel@gmail.com";
            var files = Directory.GetFiles(folderPath).OrderBy(f => f);

            string[] hashes = new string[files.Count()];

            for (int i = 0; i < files.Count(); i++)
            {
                using (var stream = File.OpenRead(files.ElementAt(i)))
                {
                    byte[] fileBytes = new byte[stream.Length];
                    stream.Read(fileBytes, 0, fileBytes.Length);
                    using (var sha3 = SHA3.Create())
                    {
                        byte[] fileHashBytes = sha3.ComputeHash(fileBytes);
                        hashes[i] = BitConverter.ToString(fileHashBytes).Replace("-", "").ToLower();
                    }
                }
            }

            Array.Sort(hashes);

            string resultHash = string.Join("", hashes) + email.ToLower();

            using (var sha3 = SHA3.Create())
            {
                byte[] resultHashBytes = sha3.ComputeHash(System.Text.Encoding.ASCII.GetBytes(resultHash));
                string finalHash = BitConverter.ToString(resultHashBytes).Replace("-", "").ToLower();

                Console.WriteLine(finalHash);
            }
        }
    }
}

