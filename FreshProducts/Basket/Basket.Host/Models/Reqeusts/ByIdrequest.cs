using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models.Reqeusts
{
	public class ByIdrequest
	{
		[Required]
		[Range(0, int.MaxValue, ErrorMessage = "The field must be greater than or equal to 0.")]
		public int Id { get; set; }
	}
}
