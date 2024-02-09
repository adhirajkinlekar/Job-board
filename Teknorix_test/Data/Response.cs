using System.ComponentModel.DataAnnotations;

namespace Teknorix_test.Data
{
    public class Response<T>
    {
        public T? Data { get; set; }

        [Required]
        public bool Success { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;
    }
}
