using Amazon.S3;
using Amazon.S3.Model;

namespace EmployeeWriteService.Services
{
    public interface IS3Uploader
    {
        Task<string> UploadAsync(string filePath);
    }

    public class S3Uploader : IS3Uploader
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucketName = "sample-project-raviraj";

        public S3Uploader(IAmazonS3 s3) => _s3 = s3;

        public async Task<string> UploadAsync(string filePath)
        {
            var key = Path.GetFileName(filePath);
            await _s3.PutObjectAsync(new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                FilePath = filePath
            });

            return $"https://{_bucketName}.s3.amazonaws.com/{key}";
        }
    }
}
