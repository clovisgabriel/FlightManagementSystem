using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManagementSystem.Web.ViewModels;

public class FlightCreateViewModel
{
    public int DepartureAirportId { get; set; }
    public int DestinationAirportId { get; set; }
    public int AircraftId { get; set; }

    public List<SelectListItem> Airports { get; set; } = new();
    public List<SelectListItem> Aircraft { get; set; } = new();
}