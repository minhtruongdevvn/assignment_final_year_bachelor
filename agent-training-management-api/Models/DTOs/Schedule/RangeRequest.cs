namespace AtmAPI.Models.DTOs.Schedule;

public class RangeRequest
{
	private const string _maxDate = "01/01/2100";
	private const string _minDate = "01/01/1900";

	public RangeRequest() { }

	public RangeRequest(DateTime from, DateTime to)
	{
		From = from;
		To = to;
	}

	[Range(typeof(DateTime), _minDate, _maxDate, ErrorMessage = "from is out of range")]
	public DateTime From { get; set; }

	[Range(typeof(DateTime), _minDate, _maxDate, ErrorMessage = "to is out of range")]
	public DateTime To { get; set; }
}
